param(
    [Parameter(Mandatory=$true)][string]$ConfigPath,
    [Parameter(Mandatory=$true)][string]$ExpectedDatabase,
    [switch]$PaymentDuplicates
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
. (Join-Path $PSScriptRoot "Get-LicenceSqlConnectionString.ps1")
$providerConnectionString = Get-LicenceSqlConnectionString -ConfigPath $ConfigPath
$provider = New-Object System.Data.SqlClient.SqlConnectionStringBuilder($providerConnectionString)
$provider['Connect Timeout'] = 5
$connection = New-Object System.Data.SqlClient.SqlConnection($provider.ConnectionString)
try {
    $connection.Open()
    if ($connection.Database -cne $ExpectedDatabase) { throw 'Connected database does not match ExpectedDatabase.' }
    $schemaProbe=$connection.CreateCommand()
    $schemaProbe.CommandText="SELECT CASE WHEN COL_LENGTH('dbo.RentLicensesDetails','rentLicPay_VouNo') IS NULL THEN 0 ELSE 1 END"
    try { $merged=[int]$schemaProbe.ExecuteScalar() -eq 1 } finally { $schemaProbe.Dispose() }
    $sqlFolder=if($merged){$PSScriptRoot}else{Join-Path $PSScriptRoot 'legacy-payment-table'}
    $audit = [IO.File]::ReadAllText((Join-Path $sqlFolder 'licence-rental-audit.sql'))
    $projection = 'SELECT Issue,RentalId,DetailId,PaymentId,Explanation FROM Issues ORDER BY Issue,RentalId,DetailId,PaymentId;'
    $index = $audit.IndexOf($projection, [StringComparison]::Ordinal)
    if ($index -lt 0) { throw 'Audit query shape changed; review the aggregate wrapper.' }
    $command = $connection.CreateCommand()
    # Only aggregate counts leave SQL Server. Do not retrieve/export row IDs or financial records.
    $command.CommandText = $audit.Substring(0, $index) + 'SELECT Issue,COUNT_BIG(*) AS IssueRows FROM Issues GROUP BY Issue ORDER BY Issue;'
    if ($PaymentDuplicates) { $command.CommandText = [IO.File]::ReadAllText((Join-Path $sqlFolder 'licence-payment-duplicates-summary.sql')) }
    $command.CommandTimeout = 60
    $reader = $command.ExecuteReader()
    try {
        while ($reader.Read()) {
            [pscustomobject]@{ Issue = $reader.GetString(0); IssueRows = $reader.GetInt64(1) }
        }
    } finally { $reader.Dispose(); $command.Dispose() }
} finally { $connection.Dispose() }
