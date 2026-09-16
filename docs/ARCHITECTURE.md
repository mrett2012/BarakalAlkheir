# Barakat Al Kheir: architecture and infrastructure

Source and local build inspection: 2026-09-16. This describes the supplied workspace, not a verified production deployment. No application login, database query/restore, report rendering, or Excel export was performed.

## System shape

One C# Windows Forms executable provides Arabic-oriented business administration for employees, company licences and sponsorships, licence/shop rentals, cars, document transactions, and payments. There is no separate service, repository layer, web frontend, or API project in this solution. Form event handlers query and modify SQL Server directly through framework Entity Framework.

```text
Program.Main
  -> frmMain (Telerik MDI shell)
     -> frmlogin (modal login)
        -> Users query -> MainClass session fields
     -> module forms and shared banner toolbar
        -> BarakeitAlkheirEntities (ObjectContext)
           -> System.Data.SqlClient -> SQL Server / BarakeitAlkheir
        -> in-memory DataSet -> Crystal Reports viewer -> .rpt files
        -> Excel COM automation in payment-detail exports
```

The active project explicitly includes 244 C# compilation entries, 74 entries marked as forms, and 86 embedded `.rpt` resources. These counts include generated files and are not counts of independently implemented features. All explicitly included source/resource/content files checked during initialization existed.

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
| Licences | `RentLicens` agreements, `RentLicensesDetail` instalments, `RentLicensesPayment` receipts, shop rentals, amount changes, agreement ending, payment summaries |
| Cars | `CarCustomerRent` with details/payments; supplier-side `CarSupplierRent` with details/payments; agreement changes/ending and financial reports |
| Documents | Document types/definitions, document transactions and step definitions, per-document tasks, task payments, task detail steps, expiry and outstanding-work lists |

`emp/frmEmployees.cs` is the largest handwritten form file (5,872 lines in this snapshot). It combines employee subrecords, document operations, and report-data construction. A change there can affect several tabs and report consumers.

### Rental and payment path

`Lic/frmRentALicense.cs` creates an agreement and monthly `RentLicensesDetail` rows within a `TransactionScope`. The detail rows carry the month number, from/to dates, amount, and `rd_isPaid`. Agreement edits also manipulate those detail schedules.

`Lic/frmRentLicensePayment.cs` creates a receipt associated with a detail row and marks that row paid. Its delete path also updates the associated detail. The car modules implement related agreement/schedule/payment patterns with different entity and field names. Do not assume the licence and car variants have identical calculations or validation.

`MainFile.NoOfSeatForShop` reads the active licence rental's seat count; `NoOfSeatTaken` joins employees, sponsors, and employee licence/shop assignments. The ID relationships deserve care because company-licence IDs and licence/shop IDs appear in these paths.

### Document workflow

Documents reference an owner through `document_TypeID`; the document definition/type determines which owner table it identifies. Observed codes are `01` for employees, `02` for company licences, and `04` for cars. This is a polymorphic association, not a universal employee foreign key.

Document transaction definitions and steps describe the work. `DocumentTransactionTasks` tracks the requested operation with approved, paid, and closed flags. Outstanding-task forms filter those flags. `frmDocumentTransactionTaskPayment` records payment, creates task detail steps from configured definitions, and marks the task paid within a transaction. Step-processing forms then use the paid/open task lists.

Expiry lists and reports join owners, document definitions, warning days, and task states. Several filters run after `ToList()`, and helper calls inside row loops can trigger additional queries.

## Database and EF model

- `App.Config` contains a named EntityClient connection string, `BarakeitAlkheirEntities`, with embedded model resource paths and `System.Data.SqlClient` as provider.
- In the source configuration, the active SQL Server target is the local default instance (`.`), catalog `BarakeitAlkheir`, using SQL authentication and multiple active result sets. Credentials are intentionally omitted here.
- `Model/BarakeitAlkheir.edmx` contains storage, conceptual, and mapping metadata. It specifies the SQL Server provider manifest token `2008`; that is model metadata, not confirmation of the currently installed SQL Server version.
- The EDMX conceptual model has 63 entity types. Generated `BarakeitAlkheir.Designer.cs` uses `ObjectContext`, `ObjectSet`, `EntityObject`, `AddObject`, and `DeleteObject`. Lazy loading is enabled by the context constructors.
- The project's `EntityDeploy` entry processes the EDMX and embeds metadata into the output assembly. There are no stored-function entries in the inspected storage model.
- Separate `BarakeitAlkheir.csdl` and `.ssdl` files contain only 54 entity types. They omit nine types present in the EDMX, including payment/detail entities and `LicenseActivity`. These standalone copies are not the active project's authoritative model inputs.
- Historical database assets include three `.bak` files dated in their names from 2016, 2017, and 2019, `Alter Constraint.sql`, two small SQL files, and a 2016 database-update text file. They are not an ordered, idempotent migration system or a verified current schema export.
- `Alter Constraint.sql` adds/checks existing-table foreign keys; it is not a full database-creation script. A small update script contains a broad `CompanyLicences` update. Inspect and scope any proposed database operation before execution.

The machine's default SQL Server service was running at inspection time. That does not establish that this catalog exists, credentials work, or the live schema matches the model. Backup contents and restore compatibility were not inspected.

## Reports, exports, and deployment layout

`frmHumanResouceReportViewer` selects reports with string values in `printReport`. Calling forms construct a `DataSet`, assign viewer properties/parameters, and display the viewer. Most inspected branches call `ReportDocument.Load`, `SetDataSource(ds)`, and optional `SetParameterValue` before assigning `ReportSource`.

Report paths are built from `Application.StartupPath + "/../../<report-directory>/<name>.rpt"`. They fit an executable under the project's `bin/Debug` or `bin/Release` with report directories at the project root. Embedded resource entries do not remove this external-file dependency: the viewer explicitly loads files from disk. Any installer, publish package, or new output location must account for these paths.

Licence and car payment-detail forms use `Microsoft.Office.Interop.Excel.Application`. The actual Excel COM runtime and report printing/export behavior were not exercised.

The project contains ClickOnce-style publish/bootstrapper properties: local `publish/`, installation from disk, updates disabled, application version `1.0.0.*`. It targets full .NET Framework 4.0, while a bootstrapper entry names the Client Profile; treat packaging prerequisites as needing validation. No installer project, deployment automation, containers, infrastructure-as-code, or CI configuration was found. Do not infer an operational deployment procedure from these project properties alone.

## Build baseline

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
- Report loading depends on the source-tree-like deployment layout, and standalone EF metadata has drifted from the active EDMX.

No remediation or database changes were made during initialization. The durable outputs are this architecture map and the root contributor guidance; isolated build artifacts are local verification output.
