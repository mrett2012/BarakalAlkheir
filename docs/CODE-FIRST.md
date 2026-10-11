# EF6 Code First

Converted on 2026-10-11 to EntityFramework 6.5.2 on the existing .NET Framework 4.8/x86 WinForms application. This uses EF6 Code First as requested; MystroX's separate linq2db/FluentMigrator stack was inspected for comparison and was not introduced here.

## Source of truth

- `BarakatAlkhreir/Model/Entities/`: 62 editable POCO entity classes. Scalar properties use normal setters and EF snapshot tracking; virtual navigation properties support lazy loading.
- `BarakatAlkhreir/Model/Mapping/`: one explicit `EntityTypeConfiguration<T>` per entity, mapping 62 tables, 794 columns, and 61 relationships after payment consolidation.
- `BarakatAlkhreir/Model/BarakeitAlkheirEntities.cs`: `DbContext` with the existing entity-set names. Context lifetime, transaction scopes, permissions, audit assignments, and form behavior remain in their existing locations.
- `BarakatAlkhreir/Model/Migrations/`: explicit EF6 migrations and their snapshots. `ExistingDatabaseBaseline` has empty `Up` and `Down` methods and a complete embedded snapshot.

Table/column spelling, nullability, identity generation, string lengths, `real` money columns, `datetime`/`date` fields, and relationship delete rules are preserved. No financial numeric-type conversion is included. Receipt metadata now resides on installments; the application no longer maps the separate payment table. The approved archive-preserving consolidation migration was applied through EF6 to BarakeitAlkheir2 on 2026-10-11 after a verified backup. See [LICENCE-PAYMENT-CONSOLIDATION.md](LICENCE-PAYMENT-CONSOLIDATION.md) for the archive, data blockers, migration SQL, and report verification.

The application uses `DbSet.Add`/`Remove`, EF6 `DbFunctions`, and `Database.ExecuteSqlCommand`. The rental application lock remains transaction-owned on an explicitly opened connection inside the existing `TransactionScope`. The compatibility `AcceptAllChanges()` method preserves the legacy post-save calls through EF6's underlying tracking context.

The former EDMX and older `.csdl`, `.ssdl`, and `.msl` files are archived in `docs/legacy-db-first/` only for historical comparison and the conversion parity harness. The generated `EntityObject` designer was removed. Neither builds nor the application load the archived metadata; the executable has no embedded EDMX metadata resources.

## Connections and startup

`App.Config` retains the named `BarakeitAlkheirEntities` connection, now a normal `System.Data.SqlClient` connection string. The configured server, catalog, and credentials were preserved. Do not publish connection strings or copy them into logs.

`Database.SetInitializer<BarakeitAlkheirEntities>(null)` disables implicit database creation/schema initialization. EF-generated automatic migrations and automatic data-loss migrations remain disabled. At the user's request, `Program` now explicitly calls `DatabaseMigrationRunner.ApplyPending` before constructing the main form or opening login. A progress dialog remains responsive while reviewed migrations run; failure shows an error and stops startup.

The runner uses the configured `BarakeitAlkheirEntities` connection, holds a SQL session application lock across the pending-history check and update, and rechecks history after acquiring that lock so concurrent client starts cannot run the same migration twice. It skips applied migrations and rejects empty/uninitialized databases, newer migration histories, or missing payment columns despite completed history. The SQL account needs schema-update rights while migrations are pending. Preserve a verified backup and close old client versions before deploying a schema-changing release; startup does not take backups. Forms still never initialize or migrate the schema.

The SQL audit/correction tools accept both current SQL Client configuration and older EntityClient configuration through `sql/Get-LicenceSqlConnectionString.ps1`.

Startup verification on 2026-10-11 passed 337 migration assertions in disposable databases, including first launch without migration history, repeat launch, concurrent clients, failure/retry, empty-database rejection, newer-history rejection, and missing-column detection. The actual startup progress dialog completed against the already upgraded `BarakeitAlkheir`, and EF loaded a full installment including its eight receipt columns with EF writes blocked. The 2,797 model checks also passed. This verifies the local build; deploy the rebuilt executable, configuration, and dependencies to client PCs.

## Existing-database baseline and subsequent changes

The initial conversion worked against the existing tables without applying the baseline. The current installment receipt model requires the consolidation migration. The supplied `sql/code-first-existing-database-baseline.sql` is retained as a historical review artifact; register the model through the explicit EF6 baseline migration. It targets **BarakeitAlkheir2**, creates only `dbo.__MigrationHistory`, and inserts the initial model snapshot. It does not change application tables or business records. It was generated and reviewed but **not executed** during conversion.

Apply schema changes through EF6 migrations instead of standalone SQL scripts. Before applying a migration, back up the intended database, close application clients, and verify its schema against the C# mappings. Review the database selection and preserve all migration history for this context. SQL indexes, triggers, and constraint names installed outside EF migrations still need explicit review when future generated migrations affect them; the baseline does not recreate or rename those objects.

For subsequent changes, edit entity classes and mappings, then scaffold a named migration and review its SQL. In Visual Studio's NuGet Package Manager Console, select `BarakalAlkheir` as the default project:

```powershell
Add-Migration DescribeTheChange -ConfigurationTypeName BarakalAlkheir.Model.Migrations.Configuration
Update-Database -Script -ConfigurationTypeName BarakalAlkheir.Model.Migrations.Configuration
```

`-Script` generates SQL for review. Apply reviewed explicit migrations through EF6 in development/test, retaining a backup and rollback plan; see the PowerShell instructions in CodeFirstStep.md. Client startup applies these same versioned migrations automatically. Do not execute standalone SQL scripts for schema updates or enable EF-generated automatic migrations. Explicitly add every new migration `.cs`, designer `.cs`, and `.resx` to this legacy project's compilation/resources, then rebuild.

New database provisioning is a separate explicit operation. The empty baseline assumes application tables already exist; applying it alone does not provision a fresh database. The test harness generates its empty schema from the C# model inside its own disposable database.

Microsoft documents [Code First against an existing database](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/existing-database) and [baselining existing databases for migrations](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/migrations/existing-database).

## Verification

Restore NuGet packages and use the isolated VS2022 MSBuild pattern in `AGENTS.md`. Run the offline parity test against that build:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-CodeFirstTests.ps1 -BuildBin .codex-build\code-first\bin
```

For the configured local database, the optional live check reads column metadata and executes EF6 SELECT queries. It also installs an EF command interceptor that rejects writes:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-CodeFirstTests.ps1 -BuildBin .codex-build\code-first\bin -ReadOnlyConfigPath BarakatAlkhreir\App.Config
```

This optional check is scoped to the investigated `BarakeitAlkheir2` catalog and known rental/receipt identities. It is not a general tenant reconciliation tool.

The archived-schema and baseline assertions preserve the original conversion, while current parity checks explicitly account for the payment consolidation and its new snapshot. When approved future migrations change the model, update those test expectations to the intended schema evolution; preserve the archived EDMX and original baseline as historical evidence.

The existing lifecycle suite generates schema from the active C# model, seeds synthetic data, and deletes only its unique disposable database:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-LicenceRentalTests.ps1 -Integration
```

Verified on 2026-10-11:

- Debug/x86 build passed with existing compiler warnings.
- 2,808 offline mapping/baseline assertions passed: 63 tables, 798 columns, 62 relationships, embedded migration snapshot, and empty baseline operations.
- Live `BarakeitAlkheir2` column types, lengths, nullability, and identity flags matched the model; EF6 reads, lazy loading, and date-function translation passed with writes blocked.
- 283 rental assertions passed on a disposable database, including add/edit/payment/rate changes/closure, concurrency, rollback, correction-package behavior, constraints, and form smoke checks. The disposable database was removed.
- Large-ID batching, six document grids, and six toolbar permission checks passed without database access.

No customer data or schema changes, baseline application, or deployment were performed during the initial conversion. The subsequent requested EF6 update registered the baseline and consolidation on BarakeitAlkheir2 after a verified backup. All original receipts are archived, and installment charges, schedule dates, paid flags, and audit fields were verified unchanged. The third migration, `RestoreArchivedLicencePaymentDates`, subsequently restored 142 missing actual payment dates from the archive after another verified backup; 20 unresolved dates remain blank. All three migrations are registered. Production login/role workflows, all other modules' CRUD paths, Crystal rendering/printing, Excel export, and deployable report-file layout remain separate acceptance checks.
