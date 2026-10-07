# Telerik UI for WinForms 2026 Q3

Updated on 2026-10-07 for the requested Telerik release.

## Project requirements

- Telerik UI for WinForms **2026.3.812**, using the **Bin48** assemblies with assembly version **2026.3.812.48**.
- Full **.NET Framework 4.8**, **x86**. Install the 4.8 targeting/developer pack for development and the 4.8 runtime on client machines.
- Visual Studio 2022 / its full-framework MSBuild is the verified build toolchain. The project remains a non-SDK WinForms project with its existing EF ObjectContext and Crystal Reports integration.
- `Telerik.Licensing` **1.9.1**, restored using `BarakatAlkhreir/packages.config` into the solution's `packages` directory.

Telerik references have exact versions, explicit hint paths, and Copy Local enabled. The default `TelerikWinFormsDir` points to `C:\Program Files (x86)\Progress\Telerik UI for WinForms 2026 Q3\Bin48`. Override this MSBuild property if installed elsewhere. The unused legacy `Telerik.WinControls.RichTextBox` reference was removed; no source controls used it and it is absent from the new distribution.

The older bundled DLLs in `BarakatAlkhreir/bin/Debug` are preserved. That directory still supplies other legacy dependencies; it is not the Telerik source for upgraded builds. Do not mix its 2011 Telerik DLLs into the new output.

## Restore and license

Restore packages using Visual Studio's NuGet restore or, with NuGet CLI installed:

```powershell
nuget restore .\BarakalAlkheir.sln
```

The Telerik installer also supplies `Telerik.Licensing.1.9.1.nupkg` in its `NuGet` directory, which can be used as an offline package source. On this machine the package was extracted from that installed distribution into `packages/Telerik.Licensing.1.9.1`.

The project imports the package's licensing build target. Keep the personal `telerik-license.txt` in `%AppData%\Telerik`, as described in [Telerik's licensing instructions](https://www.telerik.com/products/winforms/documentation/licensing/license-key). Do not commit license files or generated license source. The local build reported a valid Telerik UI for WinForms license.

## Isolated build and smoke test

Run from the workspace root:

```powershell
$projectRoot = (Get-Location).Path
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) { throw 'Visual Studio MSBuild was not found.' }
& $msbuild "$projectRoot\BarakatAlkhreir\BarakalAlkheir.csproj" `
  /t:Build /p:Configuration=Debug /p:Platform=x86 `
  "/p:ReferencePath=$projectRoot\BarakatAlkhreir\bin\Debug" `
  "/p:OutDir=$projectRoot\.codex-build\telerik-2026\bin\" `
  "/p:IntermediateOutputPath=$projectRoot\.codex-build\telerik-2026\obj\" `
  /nologo /verbosity:minimal
if ($LASTEXITCODE -ne 0) { throw 'Application build failed.' }
& .\tests\Run-TelerikUpgradeTests.ps1
```

MSBuild needs access to installed SDK registry settings. Sandbox registry denial is an environment restriction, not a compilation failure.

## Verification and remaining checks

- Debug/x86 build passed with the 2026 Telerik assemblies; existing compiler warnings remain.
- Licensing build target reported a valid WinForms license.
- All copied Telerik control assemblies were checked as `2026.3.812.48`; licensing runtime is `1.9.1.0`.
- Existing date harness: **2,316 checks passed** across five cultures using the upgraded output, including construction of 25 forms.
- Existing rental harness: **233 assertions passed**, without database integration. Its final SQL pool cleanup needed execution outside the sandbox because of Windows performance-counter restrictions.
- Additional smoke test constructed the main form, login, bank definitions, and document-task steps without invoking database-backed Load handlers. The task grid rendered synthetic Arabic rows, blank expiry cells, and a populated date. The generated `telerik-task-grid.png` is in the isolated build's `bin` directory.

These checks do not verify authenticated form loading, CRUD, permission roles, database transactions, Crystal rendering/printing, Excel COM export, Visual Studio designer editing, or deployment. Validate those against a designated test database before production rollout. The isolated output also lacks the report-directory layout required by the existing report viewer; it is not a deployable package.

References: [Telerik 2026 Q3 release](https://www.telerik.com/support/whats-new/winforms/release-history/telerik-ui-for-winforms-2026-3-812-%282026-q3%29), [system requirements](https://www.telerik.com/products/winforms/documentation/installation-and-upgrades/system-requirements).
