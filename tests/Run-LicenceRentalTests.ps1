param(
    [switch]$Integration,
    [switch]$PrepareInteractive,
    [string]$ConfigPath
)
$ErrorActionPreference = 'Stop'
if ($PrepareInteractive -and ($Integration -or $ConfigPath)) { throw '-PrepareInteractive uses the workspace App.Config and cannot be combined with -Integration or -ConfigPath.' }
$projectRoot = Split-Path $PSScriptRoot -Parent
$buildFolder = if ($PrepareInteractive) { '.codex-build/licence-ui' } else { '.codex-build/licence-repair' }
$buildRoot = [IO.Path]::GetFullPath((Join-Path $projectRoot $buildFolder))
$buildBin = Join-Path $buildRoot 'bin'
$buildObj = Join-Path $buildRoot 'obj'
New-Item -ItemType Directory -Force -Path $buildBin,$buildObj | Out-Null
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) { throw 'Visual Studio MSBuild was not found.' }
& $msbuild (Join-Path $projectRoot 'BarakatAlkhreir/BarakalAlkheir.csproj') /t:Rebuild /p:Configuration=Debug /p:Platform=x86 "/p:ReferencePath=$projectRoot\BarakatAlkhreir\bin\Debug" "/p:OutDir=$buildBin\" "/p:IntermediateOutputPath=$buildObj\" /nologo /verbosity:minimal > (Join-Path $buildRoot 'build.log') 2>&1
if($LASTEXITCODE -ne 0){ Get-Content (Join-Path $buildRoot 'build.log'); throw 'Application build failed' }
$testExe = Join-Path $buildBin 'LicenceRental.Tests.exe'
$harnessObj = Join-Path $buildRoot 'harness-obj'
$interactive = if ($PrepareInteractive) { 'true' } else { 'false' }
& $msbuild (Join-Path $PSScriptRoot 'LicenceRental.Tests.csproj') /t:Rebuild /p:Configuration=Debug /p:Platform=x86 "/p:BuildBin=$buildBin" "/p:IntermediateOutputPath=$harnessObj\" "/p:Interactive=$interactive" /nologo /verbosity:minimal > (Join-Path $buildRoot 'harness-build.log') 2>&1
if($LASTEXITCODE -ne 0){ Get-Content (Join-Path $buildRoot 'harness-build.log'); throw 'Regression harness compilation failed' }
if(-not $Integration -and -not $PrepareInteractive){ & $testExe; if($LASTEXITCODE -ne 0){throw 'Regression checks failed'}; return }

# Derive an empty fixture from the active EDMX, never from a customer database/backup.
[xml]$model = Get-Content (Join-Path $projectRoot 'BarakatAlkhreir/Model/BarakeitAlkheir.edmx')
$storage = $model.SelectSingleNode('//*[local-name()="StorageModels"]/*')
$ddl = New-Object Text.StringBuilder
foreach($set in $storage.EntityContainer.EntitySet){
    $entityName = ($set.EntityType -split '\.')[-1]
    $entity = $storage.EntityType | Where-Object {$_.Name -eq $entityName}
    if(-not $entity){throw "Missing fixture entity $entityName"}
    $table = if($set.Table){[string]$set.Table}else{[string]$set.Name}
    $columns = @()
    foreach($property in $entity.Property){
        $type = [string]$property.Type
        if($type -in @('nvarchar','varchar','nchar','char','varbinary','binary')){
            $length = if($property.MaxLength){[string]$property.MaxLength}else{'max'}
            $type += '('+$length+')'
        }elseif($type -in @('decimal','numeric')){ $type += '('+$property.Precision+','+$property.Scale+')' }
        $identity = if($property.StoreGeneratedPattern -eq 'Identity'){' IDENTITY(1,1)'}else{''}
        $nullable = if($property.Nullable -eq 'false'){' NOT NULL'}else{' NULL'}
        $columns += '['+([string]$property.Name).Replace(']',']]')+'] '+$type+$identity+$nullable
    }
    $keys = @($entity.Key.PropertyRef | ForEach-Object {'['+([string]$_.Name).Replace(']',']]')+']'})
    if($keys.Count -gt 0){$columns += 'PRIMARY KEY ('+($keys -join ',')+')'}
    [void]$ddl.AppendLine('CREATE TABLE dbo.['+$table.Replace(']',']]')+'] ('+($columns -join ',')+');')
}
[void]$ddl.AppendLine(@'
ALTER TABLE dbo.RentLicenses ADD FOREIGN KEY(customerID) REFERENCES dbo.Customers(customerID);
ALTER TABLE dbo.RentLicenses ADD FOREIGN KEY(companyLicencesID) REFERENCES dbo.CompanyLicences(companyLicencesID);
ALTER TABLE dbo.RentLicensesDetails ADD FOREIGN KEY(rentLicenseID) REFERENCES dbo.RentLicenses(rentLicenseID);
ALTER TABLE dbo.RentLicensesPayment ADD FOREIGN KEY(rentLicenseID) REFERENCES dbo.RentLicenses(rentLicenseID);
'@)
$fixture = Join-Path $buildRoot 'empty-fixture.sql'
[IO.File]::WriteAllText($fixture,$ddl.ToString(),(New-Object Text.UTF8Encoding($false)))
if ($PrepareInteractive) { Write-Output "Interactive test launcher: $testExe"; return }
if(-not $ConfigPath){$ConfigPath = Join-Path $projectRoot 'BarakatAlkhreir/App.Config'}
& $testExe $ConfigPath $fixture $buildRoot (Join-Path $projectRoot 'sql/licence-rental-integrity.sql')
if($LASTEXITCODE -ne 0){throw 'Integration checks failed'}
