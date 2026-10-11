# Project guidance

## Working style

- Use a friendly, professional tone. Answer briefly and directly with enough context to be clear.
- Prioritize accuracy. Verify assumptions and distinguish source findings, successful builds, runtime observations, and uncertainty.
- For engineering work, consider the relevant user, developer, reviewer, QA, security, operations, and business perspectives. Keep changes proportional to the request.
- Ask for clarification only when the missing information would materially change the implementation; otherwise state reasonable assumptions and proceed.
- Verify potentially outdated external information with reliable primary sources when necessary. Local source is the authority for this application's existing behavior.

## Orientation

- Read `docs/ARCHITECTURE.md` for the infrastructure, workflows, data model, and verified initialization findings.
- This is a Windows Forms desktop application, not a website or API service.
- Solution: `BarakalAlkheir.sln`; active project: `BarakatAlkhreir/BarakalAlkheir.csproj`.
- Namespace and executable name: `BarakalAlkheir`; EF context/database name: `BarakeitAlkheirEntities` / `BarakeitAlkheir`.
- Preserve these existing spelling differences in paths, namespaces, entities, and database identifiers.
- Target: full .NET Framework 4.8, x86, legacy non-SDK C# project. Use Visual Studio 2022 MSBuild; the solution header still identifies Visual Studio 2013.
- Dependencies include Telerik WinForms 2026.3.812.48 (Bin48), Telerik.Licensing 1.9.1, Crystal Reports assemblies 13.0.3500.0, EntityFramework 6.5.2 Code First, and Excel COM interop. See docs/TELERIK-UPGRADE.md and docs/CODE-FIRST.md for restore, mapping, and validation.
- At initialization on 2026-09-16 this directory had no Git metadata. Check current status before relying on Git history, diffs, or rollback.

## Source map

| Location under `BarakatAlkhreir/` | Responsibility |
| --- | --- |
| `Program.cs`, `frmMain.cs`, `frmlogin.cs` | Startup, MDI navigation, login |
| `MainClass.cs` | Global session state, cached forms, rental constants |
| `MainFile.cs` | Permission queries and licence seat counts |
| `PublicClass.cs` | Lookup dropdown population |
| `components/banner.cs` | Shared CRUD/navigation toolbar and permission visibility |
| `Definitions/` | Master data, users, groups, screens, permission events |
| `Sponsor/` | Companies, authorizations, licences, remarks, designations |
| `emp/` | Employees, sponsorships, salaries, contracts, dependents, passports, documents |
| `Lic/` | Licence/shop rental agreements, schedules, payments, reports |
| `CarsM/` | Customer car rentals, supplier purchase/payment workflows, reports |
| `Docs/` | Document definitions, expiry, transaction tasks, payments, steps |
| `Model/BarakeitAlkheirEntities.cs`, `Model/Entities/`, `Model/Mapping/`, `Model/Migrations/` | EF6 Code First context, entities, explicit SQL mappings, and reviewed migrations |
| `frmHumanResouceReportViewer.cs` | Report selection, file loading, dataset/parameter binding |
| `hrReports/`, `otherReports/`, `CarReports/` | Crystal `.rpt` designs and generated wrappers |

## Change conventions

- Trace an affected operation through its form, shared toolbar, EF mapping, related records, and report consumers before changing it.
- Business logic currently lives in form event handlers. Follow the nearby pattern for narrow fixes; do not introduce a new architectural layer or framework migration incidentally.
- Forms normally come as `.cs`, `.Designer.cs`, and `.resx`. Preserve event wiring, control names, Arabic text, layout, encoding, and line endings.
- This project explicitly lists compiled files and resources. Add/remove the relevant project entries when adding/removing source or resources.
- The active model is EF6 Code First (`DbContext` and POCO entities). Maintain explicit table, column, nullability, SQL type, key, and relationship mappings in C#; add project entries for new entities/mappings/migrations.
- The former EDMX and older standalone metadata are archived under `docs/legacy-db-first/` for historical comparison only. They are not build/runtime inputs or the current schema authority.
- Database creation and EF-generated automatic migrations are disabled. At the user's request, `Program` runs `DatabaseMigrationRunner.ApplyPending` before login to apply the application's explicit migrations to the configured database. Review/test new migrations before deployment; do not run standalone schema SQL. The initial empty baseline only records the existing model in `__MigrationHistory`; it does not create application tables. See `docs/CODE-FIRST.md`.
- Preserve `TransactionScope` boundaries and `Complete()` behavior when changing related records, payment schedules, paid flags, or task steps.
- Maintain existing `added_Date`, `edited_Date`, `addedBy`, and `editedBy` assignments for affected writes.
- Form/control permission identifiers and event strings are database contracts. Preserve `view`, `add`, `edit`, `delete`, `print`, and `search`, plus superuser behavior, unless explicitly changing permissions.
- Existing permissions often hide controls. Do not assume that a visible form or hidden button proves operation-level authorization.
- `-1` is commonly used for an unselected ID. Check nullable fields, dropdown selections, parsed dates/numbers, and empty lists before dereferencing them.
- Financial paths use floating-point values and culture-sensitive parsing. A numeric-type or date-format change needs coordinated schema, calculation, UI, and report analysis.
- Reports depend on dataset table/column names, parameters, selector strings, and relative file locations. Inspect both the caller and viewer when changing a report.
- Treat `.bak`, configuration files, employee/customer data, and credentials as sensitive. Do not copy their contents into documentation or logs. Do not restore databases or run historical SQL as part of ordinary code inspection.
- Preserve existing `bin/`, `obj/`, `.vs/`, `.suo`, database backups, and `Crystal Reports Backup Files/`. Bundled DLLs are useful build inputs in this snapshot; they are not disposable until dependency provisioning is understood.

## Build and validation

Restore NuGet packages first. Use Visual Studio MSBuild with separate output/intermediate directories. Run this from the workspace root in PowerShell, replacing `task` with an appropriate task name:

```powershell
$projectRoot = (Get-Location).Path
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) { throw 'Visual Studio MSBuild was not found.' }
& $msbuild `
  "$projectRoot\BarakatAlkhreir\BarakalAlkheir.csproj" `
  /t:Build /p:Configuration=Debug /p:Platform=x86 `
  "/p:ReferencePath=$projectRoot\BarakatAlkhreir\bin\Debug" `
  "/p:OutDir=$projectRoot\.codex-build\task\bin\" `
  "/p:IntermediateOutputPath=$projectRoot\.codex-build\task\obj\" `
  /nologo /verbosity:minimal
```

- This pattern builds with the installed Telerik 2026 Q3 Bin48 assemblies and the restored licensing package. `ReferencePath` supplies the preserved bundled Crystal Reports and Flash dependencies; Telerik references use exact versions and explicit hint paths.
- `Microsoft.VisualBasic.PowerPacks.Vs` 10.0.0.0 remained an unresolved reference warning; the reference-assisted build still returned exit code 0. Other compiler warnings also remain; this is not a warning-free baseline.
- Restricted execution initially denied MSBuild access to the installed SDK directory. The successful run had permission to access installed SDKs. Distinguish this environment restriction from a source compilation failure.
- Standalone regression harnesses are in `tests/`, including Code First mapping/schema checks, startup migration/concurrency tests, and Telerik constructor/rendering checks. No CI configuration is configured. Choose focused manual checks for changed behavior after compilation.
- Isolated build output is for compilation verification. It does not have the report directory layout expected by the current viewer and is not a validated deployable package.
- For UI changes, check form load, navigation, add/edit/cancel/save/delete, Arabic layout, and relevant permission roles. For money/workflow changes, also check related records, rollback, paid/closed flags, and affected reports against a designated test database.
- Do not claim database connectivity, login, SQL compatibility, report rendering, printing, or Excel export passed based solely on a build.
