param(
    [Parameter(Mandatory=$true)][string]$ConfigPath,
    [Parameter(Mandatory=$true)][string]$ExpectedDatabase,
    [Parameter(Mandatory=$true)][string]$OutputDirectory
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
. (Join-Path $PSScriptRoot "Get-LicenceSqlConnectionString.ps1")
$providerConnectionString = Get-LicenceSqlConnectionString -ConfigPath $ConfigPath
$connection = New-Object System.Data.SqlClient.SqlConnection($providerConnectionString)
try {
    $connection.Open()
    if($connection.Database -cne $ExpectedDatabase){throw 'Connected database does not match ExpectedDatabase.'}
    $schemaProbe=$connection.CreateCommand()
    $schemaProbe.CommandText="SELECT CASE WHEN COL_LENGTH('dbo.RentLicensesDetails','rentLicPay_VouNo') IS NULL THEN 0 ELSE 1 END"
    try { $merged=[int]$schemaProbe.ExecuteScalar() -eq 1 } finally { $schemaProbe.Dispose() }
    $sqlFolder=if($merged){$PSScriptRoot}else{Join-Path $PSScriptRoot 'legacy-payment-table'}
    $command = $connection.CreateCommand()
    $command.CommandText = [IO.File]::ReadAllText((Join-Path $sqlFolder 'licence-rental-audit.sql'))
    $command.CommandTimeout = 120
    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter($command)
    $data = New-Object System.Data.DataSet
    try {[void]$adapter.Fill($data)}finally{$adapter.Dispose();$command.Dispose()}
    New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
    for($index=0;$index -lt $data.Tables.Count;$index++){
        $data.Tables[$index].Rows | Select-Object -Property $data.Tables[$index].Columns.ColumnName |
            Export-Csv -LiteralPath (Join-Path $OutputDirectory ('audit-'+$index+'.csv')) -NoTypeInformation -Encoding UTF8 -NoClobber
    }
    $summary = $data.Tables[0].Rows | Group-Object Issue | Sort-Object Name | Select-Object @{Name='Issue';Expression={$_.Name}},Count
    $summary | Export-Csv -LiteralPath (Join-Path $OutputDirectory 'summary.csv') -NoTypeInformation -Encoding UTF8 -NoClobber
    $summary | Format-Table -AutoSize
    Write-Output 'Read-only audit exported. Counts are issue rows, not necessarily distinct contracts.'
}finally{$connection.Dispose()}
