# Barakat Al Kheir: architecture and infrastructure

Initial source/build inspection: 2026-09-16; updated for EF6 Code First on 2026-10-11. This describes the local workspace, not a verified production deployment. Current database schema/EF reads were checked read-only; lifecycle writes were tested only in disposable databases. The subsequently requested EF6 update applied the baseline and archive-preserving payment consolidation after a verified backup. No customer database restore, production login, report rendering, or Excel export was performed.

## EF6 Code First conversion (2026-10-11)

The active model is now a C# `DbContext`, 62 POCO entities, and explicit mappings under `BarakatAlkhreir/Model/`. The EDMX is archived for comparison and is no longer used by the build or application. EntityFramework 6.5.2 is restored through NuGet. Implicit database creation and EF-generated automatic migrations are disabled. At the user's request, `Program` applies pending explicit migrations through `DatabaseMigrationRunner` before login, with a progress dialog, database-level startup lock, and failure blocking. The baseline, archive-preserving consolidation, and date recovery were applied through EF6 to both BarakeitAlkheir2 and the subsequently selected BarakeitAlkheir on 2026-10-11 after verified backups. Schema updates use EF6 migrations without executing standalone SQL scripts. See [CODE-FIRST.md](CODE-FIRST.md) for the source map, migration workflow, verification, and remaining acceptance checks.

## Licence payment consolidation (2026-10-11)

The active model uses installment rows for receipts and no longer maps the former payment table. The migration was tested in disposable databases and applied through EF6 to BarakeitAlkheir2 after a verified backup. Every original receipt is archived; only a unique receipt matching an existing paid installment supplies metadata. Ambiguous, orphaned, or conflicting receipts stay archive-only, and historical paid installments keep their amounts and flags. See [LICENCE-PAYMENT-CONSOLIDATION.md](LICENCE-PAYMENT-CONSOLIDATION.md) for deployment, report checks, and the confirmed Crystal SP24/SP27 distinction.

## Telerik upgrade (2026-10-07)

The current project targets .NET Framework 4.8/x86 and Telerik UI for WinForms 2026 Q3 (2026.3.812.48). Telerik.Licensing 1.9.1 is restored through packages.config. Use Visual Studio MSBuild and the installed Bin48 assemblies. See [TELERIK-UPGRADE.md](TELERIK-UPGRADE.md) for current build instructions and verification. The initialization findings below describe the earlier 2011/.NET 4.0 baseline.

## System shape

One C# Windows Forms executable provides Arabic-oriented business administration for employees, company licences and sponsorships, licence/shop rentals, cars, document transactions, and payments. There is no separate service, repository layer, web frontend, or API project in this solution. Form event handlers query and modify SQL Server directly through EF6 Code First.

```text
Program.Main
  -> DatabaseMigrationRunner (pending explicit migrations; stop on failure)
  -> frmMain (Telerik MDI shell)
     -> frmlogin (modal login)
        -> Users query -> MainClass session fields
     -> module forms and shared banner toolbar
        -> BarakeitAlkheirEntities (EF6 DbContext)
           -> System.Data.SqlClient -> SQL Server / BarakeitAlkheir
        -> in-memory DataSet -> Crystal Reports viewer -> .rpt files
        -> Excel COM automation in payment-detail exports
```

At initialization the project explicitly included 244 C# compilation entries, 74 form entries, and 86 embedded `.rpt` resources. These were snapshot counts, including generated files. The Code First conversion replaced one generated model file with explicit entities, mappings, context/configuration, and migration source/resources; see the current project file for current counts.

## Startup, state, and authorization

`Program.Main` uses `[STAThread]`, enables visual styles, and runs `frmMain`. On load, the main form stores itself in `MainClass.mainForm` and displays `frmlogin` modally. Login queries `Users` by username and password, then populates user ID, name, display name, group ID, and superuser state in `MainClass`. Closing the login without success closes the main form.

`MainClass` also stores several reusable MDI child forms and rental constants (`StartRentDay = 10`, `StartRentCar = 1`). Preserve each call site's interpretation when changing date calculations.

The permission data path is `FormScreans -> FormEvents -> EventsDefinitions` joined with `UserGroupEvents` for the logged-in group. `components/banner.cs` hides toolbar buttons and reveals them according to event names. Superusers bypass that toolbar restriction. `MainFile` exposes separate view/add/delete helpers; some forms also perform their own permission checks.

Standard CRUD forms create a shared banner with a form identifier, attach click handlers on load, track a `change` value such as `add` or `edit`, and maintain a list/current position. Save handlers open a context, reload or create an entity, copy control values, set audit fields, save, and refresh the UI. `Definitions/frmBanks.cs` is a compact example.

## Business modules and relationships

| Module | Principal data and behavior |
| --- | --- |
| Definitions | Banks, customers, car suppliers/cars, nationalities, designations, education, religion, gender, geography, status definitions, users/groups, screens/events |
| Sponsor | Companies, authorized persons, company authorizations, company licences, licence remarks, designation allocations, licence documents |
| Employees | Employee identity/details, sponsors, salary records, work contracts, dependents, licence/shop assignment, passport records, document tasks, HR print datasets |
| Licences | `RentLicens` agreements, `RentLicensesDetail` installments with integrated receipts, shop rentals, amount changes, agreement ending, payment summaries |
| Cars | `CarCustomerRent` with details/payments; supplier-side `CarSupplierRent` with details/payments; agreement changes/ending and financial reports |
| Documents | Document types/definitions, document transactions and step definitions, per-document tasks, task payments, task detail steps, expiry and outstanding-work lists |

`emp/frmEmployees.cs` is the largest handwritten form file (5,872 lines in this snapshot). It combines employee subrecords, document operations, and report-data construction. A change there can affect several tabs and report consumers.

### Rental and payment path

`Lic/frmRentALicense.cs` creates an agreement and monthly `RentLicensesDetail` rows within a `TransactionScope`. The detail rows carry the month number, from/to dates, amount, and `rd_isPaid`. Agreement edits also manipulate those detail schedules.

`Lic/frmRentLicensePayment.cs` stores receipt metadata on the installment and marks it paid. Deleting a payment clears its metadata and paid flag without deleting the installment. The car modules implement related agreement/schedule/payment patterns with different entity and field names. Do not assume the licence and car variants have identical calculations or validation.

`MainFile.NoOfSeatForShop` reads the active licence rental's seat count; `NoOfSeatTaken` joins employees, sponsors, and employee licence/shop assignments. The ID relationships deserve care because company-licence IDs and licence/shop IDs appear in these paths.

### Document workflow

Documents reference an owner through `document_TypeID`; the document definition/type determines which owner table it identifies. Observed codes are `01` for employees, `02` for company licences, and `04` for cars. This is a polymorphic association, not a universal employee foreign key.

Document transaction definitions and steps describe the work. `DocumentTransactionTasks` tracks the requested operation with approved, paid, and closed flags. Outstanding-task forms filter those flags. `frmDocumentTransactionTaskPayment` records payment, creates task detail steps from configured definitions, and marks the task paid within a transaction. Step-processing forms then use the paid/open task lists.

Expiry lists and reports join owners, document definitions, warning days, and task states. Several filters run after `ToList()`, and helper calls inside row loops can trigger additional queries.

## Database and EF model

- `App.Config` contains the named `BarakeitAlkheirEntities` SQL Client connection string, without embedded model resource paths.
- The configured SQL Server target is the local default instance (`.`), catalog `BarakeitAlkheir2`, using the existing authentication and multiple active result sets. Credentials are intentionally omitted here.
- The authoritative model is `Model/BarakeitAlkheirEntities.cs`, `Model/Entities/`, and `Model/Mapping/`: 62 entities/tables, 794 columns, and 61 relationships. Existing SQL types, lengths, nullability, identity flags, table/column names, and delete rules were preserved.
- Forms now use `DbSet.Add`/`Remove` and EF6 `DbFunctions`. Scalar properties use snapshot tracking; virtual relationships preserve lazy loading. A compatibility method retains legacy post-save `AcceptAllChanges()` calls.
- The project no longer has an `EntityDeploy` entry or embedded database-first metadata. The generated `EntityObject` designer was removed. The old EDMX and older standalone metadata are archived under `docs/legacy-db-first/` for historical comparison only.
- `Model/Migrations/ExistingDatabaseBaseline` records the initial C# model with empty `Up`/`Down` operations. Initializers and EF-generated automatic migrations are disabled. Review/test explicit migrations before deployment; client startup applies pending versions through EF6. The baseline adds only migration history and cannot provision an empty database.
- Historical database assets include three `.bak` files dated in their names from 2016, 2017, and 2019, `Alter Constraint.sql`, two small SQL files, and a 2016 database-update text file. They are not an ordered, idempotent migration system or a verified current schema export.
- `Alter Constraint.sql` adds/checks existing-table foreign keys; it is not a full database-creation script. A small update script contains a broad `CompanyLicences` update. Inspect and scope any proposed database operation before execution.

During the initial conversion on 2026-10-11, before payment consolidation, the configured database's mapped column metadata matched that original Code First model, and EF6 SELECT queries, lazy loading, and date-function translation passed with writes rejected by an interceptor. Rental lifecycle tests passed on a separate disposable database. These checks do not establish production deployment, every form's CRUD behavior, report rendering, or backup/restore compatibility.

## Reports, exports, and deployment layout

`frmHumanResouceReportViewer` selects reports with string values in `printReport`. Calling forms construct a `DataSet`, assign viewer properties/parameters, and display the viewer. Most inspected branches call `ReportDocument.Load`, `SetDataSource(ds)`, and optional `SetParameterValue` before assigning `ReportSource`.

Report paths are built from `Application.StartupPath + "/../../<report-directory>/<name>.rpt"`. They fit an executable under the project's `bin/Debug` or `bin/Release` with report directories at the project root. Embedded resource entries do not remove this external-file dependency: the viewer explicitly loads files from disk. Any installer, publish package, or new output location must account for these paths.

Licence and car payment-detail forms use `Microsoft.Office.Interop.Excel.Application`. The actual Excel COM runtime and report printing/export behavior were not exercised.

The project contains ClickOnce-style publish/bootstrapper properties: local `publish/`, installation from disk, updates disabled, application version `1.0.0.*`. It targets full .NET Framework 4.0, while a bootstrapper entry names the Client Profile; treat packaging prerequisites as needing validation. No installer project, deployment automation, containers, infrastructure-as-code, or CI configuration was found. Do not infer an operational deployment procedure from these project properties alone.

## Historical build baseline (2026-09-16)

The solution defines Debug/Release x86 configurations. It uses framework assembly references rather than a NuGet package manifest. The current project references Telerik 2011.3.11.1219, Crystal Reports 13.0.3500.0, FlashControlV71, Office Excel interop 15.0.0.0, and Visual Basic PowerPacks 10.0.0.0.

On this machine, .NET Framework 4 MSBuild and the v4.0 reference assembly directory exist. A normal isolated build failed to resolve the vendor libraries. The bundled `BarakatAlkhreir/bin/Debug` DLLs include matching Telerik, Crystal Reports, and FlashControl assembly versions.

Adding that folder as `ReferencePath` produced a successful Debug/x86 build, exit code 0, with output under `.codex-build/init/`. The PowerPacks reference still emitted MSB3245, and existing compiler warnings remain. No application source or dependency references were changed to obtain that result. See the root `AGENTS.md` for the reproducible command pattern.

The successful build verifies compilation and project resource processing with these local dependencies. It does not verify a clean-machine installation, report file layout, SQL connection, authentication, business transactions, or designer compatibility. No automated test project was found.

## Findings to account for in future work

These are source observations and follow-up areas, not claims of a completed security or business-logic audit:

- Login compares the entered password directly with the stored `Users.password`; no password hashing is applied in that path.
- The configuration stores SQL credentials in clear text, including a commented historical connection. Avoid propagating these into new documentation or diagnostics.
- Authorization is largely expressed through UI visibility. Review write handlers when adding or changing sensitive operations.
- `frmMain_Load` catches exceptions without reporting them. Many other forms show `ex.Message`; there is no central structured logging layer in the inspected flow.
- Money is frequently parsed using `float.Parse` and model fields use floating-point types; date parsing commonly depends on the machine culture. Check rounding and date boundaries on financial changes.
- Nullable `.Value`, `FirstOrDefault()` results, and dropdown values are often used with limited validation. Empty or incomplete records are relevant regression cases.
- Synchronous database queries, materialized lists, and per-row helper queries can affect desktop responsiveness as data grows.
- Manual transactions coordinate receipts, schedules, documents, and steps. Preserve those relationships and verify persistence ordering when changing a write flow.
- Report loading depends on the source-tree-like deployment layout. Archived EF metadata is historical; future schema changes belong in the active Code First model and reviewed migrations.

No database changes were made during initialization or the Code First conversion. Isolated build artifacts are local verification output, not a validated deployment package.
