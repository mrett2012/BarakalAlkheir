param(
    [Parameter(Mandatory=$true)][string]$ConfigPath,
    [Parameter(Mandatory=$true)][string]$ExpectedDatabase,
    [Parameter(Mandatory=$true)][string]$OutputDirectory
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
[xml]$config = [IO.File]::ReadAllText((Resolve-Path -LiteralPath $ConfigPath))
$entry = @($config.configuration.connectionStrings.add | Where-Object {$_.name -eq 'BarakeitAlkheirEntities'})
if($entry.Count -ne 1){throw 'Expected one application entity connection.'}
$entity = New-Object System.Data.Common.DbConnectionStringBuilder
$entity.set_ConnectionString([string]$entry[0].connectionString)
$connection = New-Object System.Data.SqlClient.SqlConnection([string]$entity['provider connection string'])
try {
    $connection.Open()
    if($connection.Database -cne $ExpectedDatabase){throw 'Connected database does not match ExpectedDatabase.'}
    $command = $connection.CreateCommand()
    $command.CommandText = [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'licence-rental-audit.sql'))
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
