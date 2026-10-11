function Get-LicenceSqlConnectionString {
    param([Parameter(Mandatory=$true)][string]$ConfigPath)
    [xml]$config = [IO.File]::ReadAllText((Resolve-Path -LiteralPath $ConfigPath))
    $entries = @($config.configuration.connectionStrings.add | Where-Object {$_.name -eq 'BarakeitAlkheirEntities'})
    if ($entries.Count -ne 1) { throw 'Expected one application database connection.' }
    $entry = $entries[0]
    if ($entry.providerName -eq 'System.Data.SqlClient') { return [string]$entry.connectionString }
    if ($entry.providerName -ne 'System.Data.EntityClient') { throw 'Unsupported database provider.' }
    # Older deployed configuration files remain usable by read-only/repair tools.
    $entity = New-Object System.Data.Common.DbConnectionStringBuilder
    $entity.set_ConnectionString([string]$entry.connectionString)
    if (-not $entity.ContainsKey('provider connection string')) { throw 'Invalid legacy database connection.' }
    return [string]$entity['provider connection string']
}
