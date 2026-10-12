param([Parameter(Mandatory=$true)][string]$BuildBin)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$bin=[IO.Path]::GetFullPath($BuildBin)
$csc=Join-Path $env:WINDIR 'Microsoft.NET/Framework/v4.0.30319/csc.exe'
$exe=Join-Path $bin 'ReceiptAllocationRecovery.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$exe" /r:System.dll /r:System.Core.dll /r:System.Data.dll /r:System.Xml.dll /r:System.Xml.Linq.dll "/r:$bin\EntityFramework.dll" "/r:$bin\EntityFramework.SqlServer.dll" "/r:$bin\BarakalAlkheir.exe" (Join-Path $PSScriptRoot 'ReceiptAllocationRecovery.Tests.cs')
if($LASTEXITCODE -ne 0){throw 'Receipt recovery test compilation failed.'}
Copy-Item -LiteralPath (Join-Path $bin 'BarakalAlkheir.exe.config') -Destination ($exe+'.config')
& $exe (Join-Path $root 'BarakatAlkhreir/App.Config')
if($LASTEXITCODE -ne 0){throw 'Receipt recovery verification failed.'}
