param(
    [ValidateSet('Preview','Apply','Rollback')][string]$Mode = 'Preview',
    [Parameter(Mandatory=$true)][string]$ConfigPath,
    [Parameter(Mandatory=$true)][string]$ExpectedDatabase,
    [Parameter(Mandatory=$true)][string]$PackagePath,
    [string]$ManifestPath,
    [int]$AuditUserId
)
# Preview never writes to SQL. Packages contain financial data: keep them private and backed up.
# Apply/Rollback use exact full-row guards and one transaction for the entire reviewed package.
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
$keys = @{ RentLicenses='rentLicenseID'; RentLicensesDetails='rentLicencesDetailsID' }
. (Join-Path $PSScriptRoot "Get-LicenceSqlConnectionString.ps1")
$providerConnectionString = Get-LicenceSqlConnectionString -ConfigPath $ConfigPath
$connection = New-Object System.Data.SqlClient.SqlConnection($providerConnectionString)
$transaction = $null
function Command([string]$sql) {
    $command = $connection.CreateCommand()
    $command.CommandText = $sql
    $command.CommandTimeout = 60
    $command.Transaction = $transaction
    return $command
}
function Add-Value($command, [string]$name, $value) {
    if($null -eq $value){$value = [DBNull]::Value}
    [void]$command.Parameters.AddWithValue($name,$value)
}
function Read-Row([string]$table, [int]$id) {
    if(-not $keys.ContainsKey($table) -or $id -le 0){throw 'Invalid table or identity.'}
    $hint = if($transaction){' WITH (UPDLOCK,HOLDLOCK)'}else{''}
    $command = Command ('SELECT * FROM dbo.['+$table+']'+$hint+' WHERE ['+$keys[$table]+']=@id')
    Add-Value $command '@id' $id
    $data = New-Object System.Data.DataTable
    try { $reader = $command.ExecuteReader(); try {$data.Load($reader)}finally{$reader.Dispose()} } finally {$command.Dispose()}
    if($data.Rows.Count -eq 0){return $null}
    $values = @{}; $types = @{}
    foreach($column in $data.Columns){
        $value = $data.Rows[0][$column.ColumnName]
        $values[$column.ColumnName] = if($value -is [DBNull]){$null}else{$value}
        $types[$column.ColumnName] = $column.DataType.FullName
    }
    return @{ Values=$values; Types=$types }
}
function Equal-Rows($left, $right) {
    if($null -eq $left -or $null -eq $right){return ($null -eq $left -and $null -eq $right)}
    if($left.Count -ne $right.Count){return $false}
    foreach($column in $left.Keys){
        if(-not $right.ContainsKey($column)){return $false}
        if(-not [object]::Equals($left[$column],$right[$column])){return $false}
    }
    return $true
}
function Write-Row($item, $target, [bool]$insert) {
    $table = [string]$item.Table; $key = $keys[$table]
    if($null -eq $target){
        $command = Command ('DELETE FROM dbo.['+$table+'] WHERE ['+$key+']=@id')
    }else{
        $columns = @($target.Keys | Where-Object {$insert -or $_ -ne $key} | Sort-Object)
        $names = @(); $parameters = @(); $assignments = @()
        for($index=0;$index -lt $columns.Count;$index++){
            $name = '['+$columns[$index]+']'; $parameter = '@p'+$index
            $names += $name; $parameters += $parameter; $assignments += $name+'='+$parameter
        }
        $sql = if($insert){
            'SET IDENTITY_INSERT dbo.['+$table+'] ON; INSERT dbo.['+$table+'] ('+($names -join ',')+') VALUES ('+($parameters -join ',')+'); SET IDENTITY_INSERT dbo.['+$table+'] OFF;'
        }else{'UPDATE dbo.['+$table+'] SET '+($assignments -join ',')+' WHERE ['+$key+']=@id'}
        $command = Command $sql
        for($index=0;$index -lt $columns.Count;$index++){Add-Value $command ('@p'+$index) $target[$columns[$index]]}
    }
    Add-Value $command '@id' ([int]$item.Id)
    try {if($command.ExecuteNonQuery() -ne 1){throw 'Expected exactly one changed row.'}} finally {$command.Dispose()}
}
try {
    $connection.Open()
    if($connection.Database -cne $ExpectedDatabase){throw 'Connected database does not match ExpectedDatabase.'}
    # Legacy receipt corrections remain available only before the consolidation migration.
    $probe=Command "SELECT CASE WHEN OBJECT_ID('dbo.RentLicensesPayment','U') IS NULL THEN 0 ELSE 1 END"
    try { if([int]$probe.ExecuteScalar() -eq 1){$keys['RentLicensesPayment']='rentLicensePaymentID'} } finally {$probe.Dispose()}
    if($Mode -eq 'Preview'){
        if(-not $ManifestPath -or $AuditUserId -le 0){throw 'Preview requires ManifestPath and a positive AuditUserId.'}
        if(Test-Path -LiteralPath $PackagePath){throw 'Package already exists. Use a new path to preserve the rollback record.'}
        $parsedManifest = Get-Content -LiteralPath $ManifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
        $manifest = @($parsedManifest)
        if($manifest.Count -eq 0){throw 'No explicit correction decisions supplied.'}
        $items = @(); $seen = @{}
        foreach($decision in $manifest){
            $table = [string]$decision.table; $id = [int]$decision.id; $identity = $table+':'+$id
            if([string]::IsNullOrWhiteSpace([string]$decision.reason)){throw 'Every correction requires a review reason/reference.'}
            if($seen.ContainsKey($identity)){throw 'Only one correction per row is allowed in a package.'}; $seen[$identity]=$true
            if($decision.operation -cnotin @('update','delete')){throw 'Only explicit update/delete decisions are supported.'}
            $row = Read-Row $table $id
            if(-not $row){throw "Missing row $identity"}
            $after = $null
            if($decision.operation -ceq 'update'){
                $after = $row.Values.Clone()
                $changes = @($decision.changes.PSObject.Properties)
                if($changes.Count -eq 0){throw 'An update requires changes.'}
                foreach($change in $changes){
                    $column = $change.Name
                    if(-not $after.ContainsKey($column) -or $column -eq $keys[$table] -or $column -in @('added_Date','addedBy','edited_Date','editedBy','payment_added_Date','payment_addedBy','payment_edited_Date','payment_editedBy','legacyPaymentID')){throw "Column cannot be changed: $column"}
                    $value = $change.Value
                    if($null -ne $value){$value = [Convert]::ChangeType($value,[type]$row.Types[$column],[Globalization.CultureInfo]::InvariantCulture)}
                    # SQL datetime has 1/300-second precision. Round before storing the rollback guard.
                    if($value -is [datetime]){$value = (New-Object System.Data.SqlTypes.SqlDateTime($value)).Value}
                    $after[$column] = $value
                }
                $after['editedBy'] = $AuditUserId
                $after['edited_Date'] = (New-Object System.Data.SqlTypes.SqlDateTime([datetime]::Now)).Value
                if($table -eq 'RentLicensesDetails' -and $row.Values.ContainsKey('payment_edited_Date') -and
                    @($changes | Where-Object {$_.Name -in @('rentLicPay_VouNo','rentLicPay_PaymentDate','rentLicPay_mandoubID','rd_isPaid')}).Count -gt 0){
                    $after['payment_editedBy']=$AuditUserId
                    $after['payment_edited_Date']=$after['edited_Date']
                }
            }
            $items += @{Table=$table; Id=$id; Reason=[string]$decision.reason; Before=$row.Values; After=$after}
        }
        @{Version=1; Server=$connection.DataSource; Database=$connection.Database; CreatedUtc=[datetime]::UtcNow; Items=$items} |
            Export-Clixml -LiteralPath $PackagePath -Depth 8 -Encoding UTF8 -NoClobber
        Write-Output ('Preview saved: '+$items.Count+' reviewed rows; no database changes.')
        return
    }
    $package = Import-Clixml -LiteralPath $PackagePath
    if($package.Version -ne 1 -or $package.Server -cne $connection.DataSource -or $package.Database -cne $connection.Database){throw 'Package version/server/database mismatch.'}
    $items = @($package.Items)
    if($items.Count -eq 0){throw 'Empty package.'}
    if($Mode -eq 'Rollback'){[array]::Reverse($items)}
    $transaction = $connection.BeginTransaction([Data.IsolationLevel]::Serializable)
    $command = Command "DECLARE @r int; EXEC @r=sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000; IF @r<0 RAISERROR('Cannot lock licence rentals.',16,1);"
    try {[void]$command.ExecuteNonQuery()}finally{$command.Dispose()}
    foreach($item in $items){
        $row = Read-Row ([string]$item.Table) ([int]$item.Id)
        $actual = if($row){$row.Values}else{$null}
        $expected = if($Mode -eq 'Apply'){$item.Before}else{$item.After}
        $target = if($Mode -eq 'Apply'){$item.After}else{$item.Before}
        if(-not (Equal-Rows $actual $expected)){throw ('Stale row; entire transaction cancelled: '+$item.Table+':'+$item.Id)}
        # Validate every identifier and the immutable identity even when reading a saved package.
        $command = Command ('SELECT TOP (0) * FROM dbo.['+$item.Table+']')
        $reader = $command.ExecuteReader()
        try {$allowed = @(); for($i=0;$i -lt $reader.FieldCount;$i++){$allowed += $reader.GetName($i)}}finally{$reader.Dispose();$command.Dispose()}
        if($null -ne $target){
            if($target.Count -ne $allowed.Count -or [int]$target[$keys[$item.Table]] -ne [int]$item.Id){throw 'Invalid target shape/identity.'}
            foreach($column in $target.Keys){if($column -cnotin $allowed){throw 'Invalid target column.'}}
        }
        Write-Row $item $target ($null -eq $expected)
        $verified = Read-Row ([string]$item.Table) ([int]$item.Id)
        $values = if($verified){$verified.Values}else{$null}
        if(-not (Equal-Rows $values $target)){throw 'Database did not preserve the exact requested values; transaction cancelled.'}
    }
    $transaction.Commit(); $transaction.Dispose(); $transaction=$null
    Write-Output ($Mode+' committed: '+$items.Count+' rows. Preserve the package for audit/rollback.')
}finally{
    if($transaction){try{$transaction.Rollback()}finally{$transaction.Dispose()}}
    $connection.Dispose()
}
