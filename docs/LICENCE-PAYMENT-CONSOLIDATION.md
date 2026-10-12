# Licence payment consolidation

For the subsequent correction of stale receipt detail IDs and misallocated dates, see [Licence receipt allocation recovery](LICENCE-RECEIPT-RECOVERY.md). The 2026-10-12 migration matches rental, billing month, and amount independently of the historical detail ID; it preserves paid flags and the original archive.

## Application behavior

The active EF6 model no longer contains a `RentLicensesPayment` entity or set. `RentLicensesDetails` is the operational source for both installment schedules and receipts.

- `rd_amount`, `rentLicenseID`, and `rd_fromDate` determine the amount, rental owner, and billing month. No duplicate payment amount or billing-date column is added.
- `rd_isPaid` indicates whether the installment has a payment.
- `rentLicPay_VouNo`, `rentLicPay_PaymentDate`, and `rentLicPay_mandoubID` retain the voucher, actual payment date, and collector.
- `payment_added_Date`, `payment_edited_Date`, `payment_addedBy`, and `payment_editedBy` preserve the receipt audit independently of the schedule audit.
- `legacyPaymentID` retains the old receipt identity for migrated payments. Operational selection/edit/delete uses `rentLicencesDetailsID`.

Saving, editing, and deleting payments preserve permissions, the application lock, `TransactionScope`, and optimistic edit checks. Deleting a payment clears its receipt metadata and paid flag without deleting the installment. A surviving installment edit token prevents a stale deleted receipt from editing a subsequent payment on the same installment.

Updated consumers include payment entry/search, agreement edit/delete/closure, rate changes, monthly collection lists, yearly totals and their Excel source grid, paid/unpaid/due reports, and collector/date filters. The car payment tables remain separate; two unused declarations referencing the former licence receipt type were removed.

The current mapped model contains 62 tables, 794 columns, and 61 relationships. The original 63-table baseline remains historical evidence.

## Database migration

The migration is `202610110924552_MergeLicencePaymentsIntoDetails`. It adds eight metadata columns, retains all original receipts in `dbo.RentLicensesPaymentArchive_20261011`, verifies the copy, and drops the former operational table. The archive is not mapped or queried by the application.

The original installment audit, charge, rental owner, dates, and paid state are preserved. Original conflicting receipt amounts/owners/billing dates remain in the archive; operational amounts and billing months come from the installment as requested. Missing historical voucher/payment-date/collector values remain missing. Reports keep these payments in totals; missing payment dates display blank and cannot match an actual-payment-date filter.

The approved archive-preserving policy retains every original receipt, including duplicate, orphaned, and conflicting records. Receipt metadata is copied only when exactly one receipt matches an existing paid installment's rental, amount, and derived billing month. Billing month uses the existing day-10 cutoff; the receipt's day within that month does not determine the match. The installment must have a valid rental, date, and positive charge.

Multiple matching receipts, unmatched receipts, invalid targets, and unpaid-linked receipts stay in the archive. A paid installment without an unambiguous matching receipt keeps its paid flag and charge, with blank receipt metadata. Archive-only receipts do not appear separately in operational reports. No duplicate is chosen by identity, timestamp, or voucher, and no paid state or charge is inferred.

The migration still stops for null installment paid flags, an existing archive, incoming foreign keys, tracked SQL dependencies, SQL modules mentioning the old table, or enabled receipt triggers. It verifies the archive, copied metadata, installment count, and unchanged original installment fields before dropping the old operational table; any failure rolls back the consolidation.

Read-only investigation on 2026-10-11 found 25,319 receipts in the configured database, five duplicate-installment groups, seven orphan receipts, 19 paid installments without a receipt, 25 detected receipt owner/amount disagreements, and 12 receipts missing a voucher or payment date. No incoming foreign keys, tracked SQL dependencies, enabled receipt triggers, or SQL modules mentioning the receipt table were found. The baseline was not registered at that inspection. These are dated observations; rerun the preflight before deployment. Customer data and schema were not modified.

On 2026-10-11, the requested EF6 command-line update applied ExistingDatabaseBaseline to BarakeitAlkheir2. The consolidation stopped at its duplicate-receipt guard and rolled back. Verification found only the baseline history entry, zero added metadata columns, the original payment table present, no archive, and five duplicate installment groups. No standalone schema SQL script was executed.

After the user approved the archive-preserving matching policy, the revised migration passed 198 assertions in disposable databases and was applied through EF6 to BarakeitAlkheir2 on 2026-10-11 after a fresh verified COPY_ONLY backup. Live verification confirmed both history entries, all eight metadata columns, 25,319 archived original receipts, 25,164 copied metadata sources, 155 archive-only receipts, and 162 paid installments without copied metadata. The old operational table is removed. Expected versus actual metadata comparisons found no missing, changed, or unexpected rows. EF6 schema/SELECT checks passed with writes blocked by an interceptor.

Deployment steps:

1. Back up the intended database and close all application clients.
2. Review [the read-only preflight](../sql/licence-payment-merge-preflight.sql), including which receipts will stay archive-only under the approved matching rule. Resolve null paid flags or dependent database objects before migration. Historical duplicate/orphan/missing-receipt records use the approved archive policy rather than manual deletion or invented receipts.
3. Verify the database selected in App.Config and register ExistingDatabaseBaseline through EF6 once if it is not registered.
4. Review the consolidation and date-recovery migrations and apply them through EF6 using `ef6.exe database update --target RestoreArchivedLicencePaymentDates` with the assembly, configuration, and migration-configuration options in [CodeFirstStep.md](../CodeFirstStep.md). Do not execute standalone SQL scripts for schema updates. EF6 records the snapshot and skips applied migrations.
5. Deploy the matching application build. Do not run this build against the old schema: its mapped receipt columns require the consolidation migration.
6. Verify payment entry/edit/delete, paid and unpaid totals, closure, collector/month/payment-date filters, historical rentals, and relevant permission roles on the designated test/deployment database.

EF-generated automatic migrations remain disabled; the application's startup runner now applies pending explicit migrations before login. Preserve the archive and coordinated database/application backup. Automatic `Down()` is deliberately refused because reconstructing old receipts after new payments could lose financial data; downgrade through a reviewed restore/reconciliation plan. The integrity-rollback SQL only removes explicit integrity objects; it does not undo consolidation.

When the configured database changed to `BarakeitAlkheir`, it had none of the three migration-history entries or eight metadata columns. On 2026-10-11 all three migrations were applied there through EF6 after a verified backup. Verification confirmed all eight columns, all three history entries, unchanged original installment fields, and preservation of all 11,672 original receipts in the archive. Client startup now checks the database selected in the deployed configuration rather than assuming another database's migrations are already applied.

## Payment date recovery and voucher search

The follow-up migration `202610111046253_RestoreArchivedLicencePaymentDates` restores only missing actual payment dates on paid installments from `RentLicensesPaymentArchive_20261011`. It uses the original installment link even when the archived receipt's rental, amount, or billing month conflicts with the authoritative installment. If a `legacyPaymentID` was already selected by consolidation, only that receipt is considered. Otherwise every linked receipt must have the same non-null payment timestamp; missing or conflicting source dates are not guessed. Existing payment dates, all other metadata, installment amounts/owners/paid flags/audit fields, and the entire archive remain unchanged.

On 2026-10-11 the follow-up was applied to `BarakeitAlkheir2` through EF6 after a new verified `COPY_ONLY` backup. It restored 142 of 162 missing dates (119 single receipts with the same rental owner and 23 with conflicting archived rental ownership). Nineteen installments have no archived receipt and one has two conflicting dates; these 20 remain blank. Exact ordered before/after data hashes verified the expected installment result and an unchanged archive. All three migration-history entries are registered. The migration refuses automatic downgrade rather than clearing historical dates that may subsequently have been edited.

The payment form displays unknown dates as blank with the date checkbox unchecked and requires an explicit date before saving. New payments still default to today. Payment search includes null vouchers when its voucher filter is empty; a nonempty filter still requires a matching voucher.

Validation: isolated Debug/x86 build, 2,797 model mapping checks and live read-only EF queries, 283 rental regression assertions plus actual-form null-date/search checks, and 310 migration assertions in disposable databases. Date-recovery cases cover conflicting receipt values, agreeing duplicate dates, conflicting dates, missing dates/receipts, unpaid installments, existing-date preservation, other-field/archive preservation, repeat execution, and downgrade refusal. Disposable databases were removed. Crystal SP24 installation and report runtime verification remain separate; the user will replace the installed SP27 runtime with SP24.

## Crystal Reports

Project references use assembly version **13.0.3500.0**. The bundled Engine, Shared, ReportSource, and Windows.Forms DLLs have file/product version **13.0.24.2970**, corresponding to **SAP Crystal Reports for Visual Studio SP24**. Assembly identity alone does not identify the support pack.

This workstation's installed ReportAppServer/native engine is **13.0.27.3480 (SP27)** with managed assembly identity **13.0.4000.0**. Running the bundled SP24 engine here fails to resolve its required `ReportAppServer.CommLayer` identity. Report QA used isolated copies/redirects to the installed SP27 runtime. Production project references and configuration were not upgraded. A consistent runtime/assembly deployment needs separate verification.

All 86 `.rpt` files under `hrReports`, `otherReports`, and `CarReports`, including their loaded subreports, were inspected for direct table/formula references to the retired receipt table; none were found. Dataset table/column names and viewer selector strings were retained, so no report design edits were needed.

Synthetic PDF exports passed for `PaymentReport`, `PaymentReport1`, `NotPaymentReport`, `DeservedPaymentReport`, and `rentLicence`, using their actual form-created datasets. `OtherPaymentReport` also passed with its document-form dataset. These validate binding/rendering under SP27, not production data, physical printing, Excel COM automation, or the SP24 deployment.

## Verification commands

Use the isolated MSBuild pattern in `AGENTS.md`, then run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-CodeFirstTests.ps1 -BuildBin .codex-build\licence-repair\bin
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-LicenceRentalTests.ps1 -Integration
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-PaymentMergeMigrationTests.ps1 -BuildBin .codex-build\licence-repair\bin
```

The live Code First metadata check requires an already migrated designated database. It is expected to fail against the unmigrated configured database.

The Crystal harness accepts a build directory with a consistent installed runtime:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-CrystalRentalReportTests.ps1 -BuildBin <consistent-runtime-build-directory>
```

It loads all 86 designs and exports synthetic PDFs into `.codex-build/payment-merge/reports/`. No database refresh/query is requested. Use `-SkipInventory` only when rechecking exports after the inventory has already passed.

Verified: isolated Debug/x86 build, 2,797 mapping/snapshot checks, 283 rental lifecycle/concurrency/rollback/form assertions, and 198 migration/script assertions in disposable databases. Those migration tests include exact-match metadata selection, duplicate zero/one/multiple-match outcomes, orphan and paid-without-receipt archival handling, billing cutoff, missing metadata, custom archive columns, unchanged installment/audit fields, repeat application, null-flag/trigger/dependency/archive/FK rejection, transactional rollback, and downgrade refusal. All disposable databases were removed.
