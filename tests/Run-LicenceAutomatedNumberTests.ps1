param([Parameter(Mandatory=$true)][string]$BuildBin)
$ErrorActionPreference = 'Stop'
$bin = [IO.Path]::GetFullPath($BuildBin)
$csc = Join-Path $env:WINDIR 'Microsoft.NET/Framework/v4.0.30319/csc.exe'
$exe = Join-Path $bin 'LicenceAutomatedNumber.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$exe" /r:System.Core.dll /r:System.Data.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll "/r:$bin\BarakalAlkheir.exe" "/r:$bin\Telerik.WinControls.dll" "/r:$bin\Telerik.WinControls.UI.dll" "/r:$bin\Telerik.WinControls.GridView.dll" "/r:$bin\TelerikCommon.dll" (Join-Path $PSScriptRoot 'LicenceAutomatedNumber.Tests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Licence automated-number test compilation failed.' }
& $exe
if ($LASTEXITCODE -ne 0) { throw 'Licence automated-number tests failed.' }
