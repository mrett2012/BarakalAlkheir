$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$buildDir = Join-Path $projectRoot '.codex-build\form-loading-tests'
New-Item -ItemType Directory -Force -Path $buildDir | Out-Null
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$testExe = Join-Path $buildDir 'FormLoading.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$testExe" /r:System.Core.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll (Join-Path $projectRoot 'BarakatAlkhreir\components\FormLoading.cs') (Join-Path $PSScriptRoot 'FormLoading.Tests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Loading test compilation failed.' }
& $testExe
if ($LASTEXITCODE -ne 0) { throw 'Loading tests failed.' }
