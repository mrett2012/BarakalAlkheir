param([string]$BuildBin)
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
if (-not $BuildBin) { $BuildBin = Join-Path $projectRoot '.codex-build\responsive-loading\bin' }
$BuildBin = [IO.Path]::GetFullPath($BuildBin)
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$testExe = Join-Path $BuildBin 'DocumentListLoading.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$testExe" /r:System.Core.dll /r:System.Data.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll "/r:$BuildBin\BarakalAlkheir.exe" "/r:$BuildBin\Telerik.WinControls.dll" "/r:$BuildBin\Telerik.WinControls.UI.dll" "/r:$BuildBin\Telerik.WinControls.GridView.dll" "/r:$BuildBin\TelerikCommon.dll" (Join-Path $PSScriptRoot 'DocumentListLoading.Tests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Document loading test compilation failed.' }
& $testExe
if ($LASTEXITCODE -ne 0) { throw 'Document loading tests failed.' }
