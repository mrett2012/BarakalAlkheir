param([string]$BuildBin,[string]$ReadOnlyConfigPath)
$ErrorActionPreference='Stop'
$projectRoot=Split-Path $PSScriptRoot -Parent
if (-not $BuildBin) { $BuildBin=Join-Path $projectRoot '.codex-build\code-first\bin' }
$buildBinPath=[IO.Path]::GetFullPath($BuildBin)
$csc=Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$testExe=Join-Path $buildBinPath 'CodeFirst.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$testExe" /r:System.Core.dll /r:System.Data.dll /r:System.Xml.dll /r:System.Xml.Linq.dll "/r:$buildBinPath\EntityFramework.dll" "/r:$buildBinPath\BarakalAlkheir.exe" (Join-Path $PSScriptRoot 'CodeFirst.Tests.cs')
if($LASTEXITCODE -ne 0){throw 'Code First harness compilation failed.'}
if ($ReadOnlyConfigPath) {
    & $testExe (Join-Path $projectRoot 'docs\legacy-db-first\BarakeitAlkheir.edmx') ([IO.Path]::GetFullPath($ReadOnlyConfigPath))
} else {
    & $testExe (Join-Path $projectRoot 'docs\legacy-db-first\BarakeitAlkheir.edmx')
}
if($LASTEXITCODE -ne 0){throw 'Code First model parity failed.'}
