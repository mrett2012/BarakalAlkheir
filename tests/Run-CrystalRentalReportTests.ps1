param([Parameter(Mandatory=$true)][string]$BuildBin,[switch]$SkipInventory)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$bin=[IO.Path]::GetFullPath($BuildBin)
$csc=Join-Path $env:WINDIR 'Microsoft.NET/Framework/v4.0.30319/csc.exe'
$exe=Join-Path $bin 'CrystalRentalReports.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$exe" /r:System.dll /r:System.Core.dll /r:System.Data.dll /r:System.Windows.Forms.dll "/r:$bin\CrystalDecisions.CrystalReports.Engine.dll" "/r:$bin\CrystalDecisions.Shared.dll" (Join-Path $PSScriptRoot 'CrystalRentalReports.Tests.cs')
if($LASTEXITCODE -ne 0){throw 'Crystal report harness compilation failed.'}
Copy-Item -LiteralPath (Join-Path $bin 'BarakalAlkheir.exe.config') -Destination ($exe+'.config')
# Synthetic datasets only; no database query/refresh is requested.
$extra=if($SkipInventory){@('--skip-inventory')}else{@()}
& $exe (Join-Path $root 'BarakatAlkhreir') (Join-Path $root '.codex-build/payment-merge/reports') @extra
if($LASTEXITCODE -ne 0){throw 'Crystal report verification failed.'}
