# Steps to Create a New Table with EF6 Code First

This project uses EF6 Code First on .NET Framework 4.8. Create a table through an entity class, its mapping, and an explicit migration.

## 1. Create the entity class

Add a class under `BarakatAlkhreir/Model/Entities/` using the namespace `BarakalAlkheir.Model`.

Define:

- The primary key.
- The required columns and their C# types.
- Nullable properties for optional values.
- Navigation properties if the table has relationships.
- Audit fields where required by the business workflow.

Follow the existing entity classes. Use normal scalar properties; use virtual navigation properties where lazy loading is needed.

## 2. Create the mapping class

Add an `EntityTypeConfiguration<YourEntity>` class under `BarakatAlkhreir/Model/Mapping/`.

Specify the table/schema name, primary key, identity generation, SQL column types, lengths, nullability, and relationships. Review cascade-delete behavior explicitly.

Use [BankMap.cs](BarakatAlkhreir/Model/Mapping/BankMap.cs) as a reference for the project's mapping style.

## 3. Register the entity and mapping

In [BarakeitAlkheirEntities.cs](BarakatAlkhreir/Model/BarakeitAlkheirEntities.cs), add a set property:

```csharp
public virtual DbSet<YourEntity> YourEntities { get; set; }
```

Register the mapping inside `OnModelCreating`:

```csharp
modelBuilder.Configurations.Add(new YourEntityMap());
```

Replace `YourEntity`, `YourEntities`, and `YourEntityMap` with your actual names.

This legacy project explicitly lists its source files. Ensure the entity and mapping are included in `BarakatAlkhreir/BarakalAlkheir.csproj`, then build using the instructions in [AGENTS.md](AGENTS.md).

## 4. Register the existing database baseline once

If the existing database baseline has not already been applied:

1. Back up the intended database and close application clients.
2. Verify that the existing schema matches the current C# mappings.
3. Review the migration and apply `ExistingDatabaseBaseline` through EF6, using the command-line instructions below.

The EF6 commands use the database selected in `App.Config`. Verify that selection before applying migrations.

The baseline records the existing model in `dbo.__MigrationHistory`; it does not create the application's existing tables. It was applied to `BarakeitAlkheir2` through EF6 on 2026-10-11. The payment consolidation was subsequently applied through EF6 on 2026-10-11 using the approved archive-preserving matching policy. Preserve the original baseline and its snapshot.

Existing explicit migrations must also be applied to the designated development/test database before scaffolding another migration. The configured database has both the baseline and payment consolidation registered; follow [the migration guide](docs/LICENCE-PAYMENT-CONSOLIDATION.md) for the approved archive policy. For another database, use a designated test/disposable database to verify its pending migrations before scaffolding.

## 5. Generate the new migration

Open Visual Studio's **Tools > NuGet Package Manager > Package Manager Console**. Select `BarakalAlkheir` as the default project and the startup project.

Run:

```powershell
Add-Migration AddYourTable -ConfigurationTypeName BarakalAlkheir.Model.Migrations.Configuration
```

Replace `AddYourTable` with a descriptive migration name.

Inspect the generated migration under `BarakatAlkhreir/Model/Migrations/`:

- `Up()` should create the intended table, columns, keys, and indexes.
- `Down()` should describe how to reverse the change. Dropping a populated table loses its data.
- Investigate unexpected changes to existing tables before proceeding.

Ensure the migration `.cs`, `.Designer.cs`, and `.resx` files are included in the project's compilation and resources, then rebuild.

## 6. Generate and review the SQL

In Package Manager Console, run:

```powershell
Update-Database -Script -ConfigurationTypeName BarakalAlkheir.Model.Migrations.Configuration
```

`-Script` generates SQL without applying it. Review the target database, table definitions, foreign keys, indexes, and migration-history changes. The generated script may include other pending migrations; verify every included change.

## 7. Apply and verify

1. Apply the reviewed migration through EF6 to a designated test database first.
2. Confirm the table, columns, constraints, and migration-history entry.
3. Verify inserting, reading, editing, and deleting records through the application, including required fields, relationships, and audit assignments.
4. After successful testing, back up the intended database and apply the migration through EF6 there.
5. Deploy the matching application build if the application uses the new table.

At startup, the application applies pending explicit EF6 migrations before login. Adding an entity alone is insufficient: create and review its migration, include its files/resources in the project, test it, then deploy the rebuilt application. EF-generated automatic migrations and database creation remain disabled. Do not execute standalone SQL scripts for schema updates.

For a client PC, deploy the rebuilt application with its EF6 dependencies and set `BarakeitAlkheirEntities` in `BarakalAlkheir.exe.config` to the intended existing database. Take a backup and close other application clients before deploying a schema-changing release. The SQL login needs permission to execute the pending migrations, including table/index changes and migration-history writes. On first launch the application shows a progress window, applies missing migrations, then opens login; subsequent launches skip applied migrations. Concurrent starts wait for the same database migration lock. Errors stop startup and show the failure. The runner does not create a fresh empty application database or take an automatic backup.

## PowerShell command-line alternative

EF6 includes a command-line tool; Visual Studio's Package Manager Console is optional. Use absolute paths because the tool resolves relative configuration paths from its assembly directory.

```powershell
$root = 'D:\OLDPC\RG\CProjects\BarakatAlKheir'
$ef = "$root\packages\EntityFramework.6.5.2\tools\net45\win-x86\ef6.exe"
$efOptions = @(
    '--assembly', "$root\.codex-build\licence-repair\bin\BarakalAlkheir.exe",
    '--config', "$root\BarakatAlkhreir\App.Config",
    '--project-dir', "$root\BarakatAlkhreir",
    '--migrations-config', 'BarakalAlkheir.Model.Migrations.Configuration'
)

# List applied migrations
& $ef migrations list @efOptions

# Generate SQL for review only
& $ef database update --script --target RestoreArchivedLicencePaymentDates @efOptions

# Apply the existing migrations through EF6 after review and backup
& $ef database update --target RestoreArchivedLicencePaymentDates @efOptions
```

The build assembly must match the current source; rebuild it after model or migration edits. Existing migrations must succeed before scaffolding a subsequent model change:

```powershell
& $ef migrations add AddYourTable @efOptions
```

All three migrations are registered on the configured database: `ExistingDatabaseBaseline`, `MergeLicencePaymentsIntoDetails`, and `RestoreArchivedLicencePaymentDates`. The last migration recovered 142 missing dates from the original receipt archive; 20 unresolved dates remain blank. Do not target an earlier migration there: that would request a downgrade, which the data migrations deliberately reject. On an unmigrated designated database, a baseline-only update uses `--target ExistingDatabaseBaseline`.

The conversion parity tests compare against the original schema. For an intentional model change, update the relevant test expectations while retaining the original baseline and archived EDMX as historical evidence.

See [docs/CODE-FIRST.md](docs/CODE-FIRST.md) for the conversion details and validation workflow, and [Microsoft's EF6 migration documentation](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/migrations/) for the migration commands.
