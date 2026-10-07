param([switch]$SkipBuild)
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$buildRoot = [IO.Path]::GetFullPath((Join-Path $projectRoot '.codex-build/global-date-format'))
$buildBin = Join-Path $buildRoot 'bin'
$buildObj = Join-Path $buildRoot 'obj'
New-Item -ItemType Directory -Force -Path $buildBin,$buildObj | Out-Null
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) { throw 'Visual Studio MSBuild was not found.' }
$csc = Join-Path $env:WINDIR 'Microsoft.NET/Framework/v4.0.30319/csc.exe'
if (-not $SkipBuild) {
    & $msbuild (Join-Path $projectRoot 'BarakatAlkhreir/BarakalAlkheir.csproj') /t:Build /p:Configuration=Debug /p:Platform=x86 "/p:ReferencePath=$projectRoot\BarakatAlkhreir\bin\Debug" "/p:OutDir=$buildBin\" "/p:IntermediateOutputPath=$buildObj\" /nologo /verbosity:minimal > (Join-Path $buildRoot 'build.log') 2>&1
    if ($LASTEXITCODE -ne 0) { Get-Content (Join-Path $buildRoot 'build.log'); throw 'Application build failed' }
}
$testExe = Join-Path $buildBin 'GlobalDateFormat.Tests.exe'
& $csc /nologo /target:exe /platform:x86 "/out:$testExe" /r:System.Core.dll /r:System.Data.dll /r:System.Data.Entity.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll "/r:$buildBin/BarakalAlkheir.exe" "/r:$buildBin/Telerik.WinControls.dll" "/r:$buildBin/Telerik.WinControls.UI.dll" "/r:$buildBin/Telerik.WinControls.GridView.dll" "/r:$buildBin/TelerikCommon.dll" (Join-Path $PSScriptRoot 'GlobalDateFormat.Tests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Global date regression harness compilation failed' }
& $testExe
if ($LASTEXITCODE -ne 0) { throw 'Global date regression checks failed' }
