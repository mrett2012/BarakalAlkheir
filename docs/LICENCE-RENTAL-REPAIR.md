# Licence rental and payment repair

Implemented and checked against the workspace on 2026-09-16. Historical data was queried read-only. No historical corrections, database constraints, or application deployment were applied to the configured database.

## Resulting behaviour

- Date formatting is now shared across the application through `AppDateFormat.Pattern` in `BarakatAlkhreir/AppDateFormat.cs`. `Program.Main` applies the Gregorian date culture before opening forms; full-date pickers, grid formats and the licence report helper use that same setting. Month/year filters retain their existing scope. See `docs/DATE-FORMAT.md` for configuration and validation boundaries.

- Instalments remain anchored to the original contract start: a January 31 start produces February 28 and March 31 starts. The May billing period is April 11 through May 10, inclusive.
- New instalments require a positive amount. A payment settles exactly one complete instalment, in schedule order. Receipt creation/deletion and paid flags commit or roll back together.
- Ordinary agreement editing preserves scheduled rates. Extending uses the last scheduled rate; shortening can remove only unpaid tail instalments without receipts. Contract totals come from the schedule. The separate rate dialog changes only unpaid instalments starting on or after the effective date and rejects an empty selection.
- The original start is immutable. Licence/customer changes are blocked after payment. Receipt editing retains its rental, instalment, amount and billing month; only voucher, payment date and collector can change. Historical receipts load their saved rental even after the licence is re-rented.
- Closed contracts are financially read-only. Closure requires a consistent, completely paid schedule. Deleting a rental with receipts is blocked.
- Every financial operation rechecks permissions, current records and ledger consistency inside a transaction. A database application lock serializes licence rental writes across updated clients. Optimistic edit timestamps detect stale agreement/rate/receipt edits; timestamps advance beyond SQL datetime rounding precision.
- Reports share the billing cutoff, use recorded receipt amounts, retain missing collectors as an unknown collector, and map governorate/area correctly. The history option is available without selecting one licence. Annual totals include historical rentals and use decimal sums rather than cell colours to calculate payment totals.

Collections follow the receipt's recorded rental and billing month, including receipts awaiting allocation review. Instalment settlement/outstanding calculations count only receipts whose rental and instalment agree. These figures can differ on corrupt historical data; the audit identifies the conflicts. Neither a paid flag nor an ambiguous receipt is silently treated as proof of settlement.

The existing SQL `real` money columns and EF model remain in place. Calculations use decimal conversions; entered individual charges that cannot round-trip through the existing storage precision are rejected. The legacy contract summary column remains approximate, while the displayed total is recalculated from instalments. A decimal schema migration is outside this repair.

## Verification

Run from the repository root with Windows PowerShell:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-LicenceRentalTests.ps1 -Integration
```

Without `-Integration`, pure rules and date-control culture checks run without connecting to a database. `-ConfigPath` can select another connection configuration. Integration uses the configured server credentials to create a uniquely named `BarakatLicenceTests_<GUID>` database, derives an empty schema from the active EDMX, seeds synthetic records, and drops only that database in `finally`. It does not copy or amend customer data. Temporary UI connection configuration files are removed after use.

Original repair validation: **73 assertions passed**, with the disposable database removed. Date-format follow-up validation: **233 assertions passed without database access**, including actual date-control text under en-US, en-GB, ar-KW, ar-SA and th-TH with an overridden short-date pattern and separator. The isolated build and `git diff --check` passed. The database integration suite and Crystal/Excel rendering were not rerun for the date-format follow-up.

The suite exercises date/culture boundaries, creation, extension/shortening, rate preservation, receipt editing/deletion, closed contracts and re-rental, permissions, stale edits, duplicate/cross-rental receipts, rollback after a forced SQL failure, two concurrent payment attempts, actual receipt totals, correction preview/apply/rollback, stale correction batches, and idempotent constraint installation. Offscreen form checks cover historical payment binding, rental edit/add/cancel, locked controls, the rate dialog, history availability and annual totals. Images and build logs are under `.codex-build/licence-repair/`.

The legacy x86 Debug build succeeds with bundled vendor references. Existing compiler warnings and the unresolved unused PowerPacks reference remain. Arabic rental/payment/annual forms were rendered and inspected. Authenticated production use, Crystal report rendering/printing, Excel COM export and deployment are separate acceptance checks; this build output is not a deployment package.

## Interactive QA follow-up - 2026-10-07

Scope: the **Rent A License** and **Rent License Payment** screens reached from the licence dashboard, using the current .NET Framework 4.8 / Telerik 2026.3.812.48 build. The configured database was assessed read-only; all rental/payment writes were made in disposable databases seeded from the active EDMX with synthetic data.

### Reproduced and repaired

Selecting a licence with a null link code by clicking OK raised an unhandled `NullReferenceException` in `frmfindLicences.btnOK_Click`. The Enter path also returned the commercial name as the link code. Both selection paths now use one guarded data-row selection helper and tolerate null display fields. The related active-rental and customer pickers now guard empty selections and nullable search fields too. Layout, business rules, transactions and database schema were not changed by this follow-up.

### Verified

- The isolated build succeeded with existing warnings. The final SQL integration run passed **283 assertions**, plus the new licence/customer picker regression checks; `git diff --check` passed.
- Manual UI: opened both screens; observed the bilingual loading indicator; rejected zero/blank amounts and a missing collector; selected a licence with a null link code and a customer with missing optional fields; checked a historical closed-rental receipt after re-rental.
- Manual payment: saved synthetic voucher `QA-UI-001` against `REPLACEMENT`. A database query verified exactly one matching receipt for **200**, linked to instalment 1 of the correct rental, its paid flag set, and billing date **15/05/2025**.
- Manual rental: saved synthetic contract `QA-RENT-001`, starting **07/10/2026**, for two months at **125.5**. UI and database agreed on **251** total, two unpaid instalments, and **06/12/2026** end date. The screen returned to view mode with record count 3/3.
- Automated lifecycle coverage includes creation, extension/shortening, rate changes, payment edit/delete, permissions, stale edits, duplicate/cross-rental rejection, paid/closed rules, re-rental history, forced rollback, concurrent payment attempts, report calculations and correction/constraint tooling on synthetic data.
- Test windows were closed, temporary UI configuration removed, and a final server query found **zero** databases with the test harness prefix.

To prepare the isolated interactive launcher:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-LicenceRentalTests.ps1 -PrepareInteractive
```

Run the returned executable, then close its dashboard normally to trigger cleanup. This mode uses the workspace App.Config only for server credentials, creates its own unique database, and seeds a test superuser session. It does not exercise login. Do not combine this switch with `-Integration` or `-ConfigPath`. The separate test executable displayed a Telerik runtime licence watermark; licensing of the deployed application remains a separate acceptance check. A font error after mixing extensive offscreen tests and an interactive session in one process did not recur when the interactive launcher used a fresh process and fixture.

### Current configured-database findings

A read-only aggregate audit of **BarakeitAlkheir2** on 2026-10-07 returned:

| Audit issue | Issue rows |
| --- | ---: |
| InvalidInstalment | 591 |
| ScheduleDateMismatch | 371 |
| BillingMonthMismatch | 133 |
| MissingCollector | 75 |
| InvalidRentalDuration | 31 |
| RentalEndMismatch | 31 |
| ReceiptRentalMismatch | 25 |
| PaidWithoutReceipt | 19 |
| DuplicateReceipt | 10 |
| MissingInstalment | 7 |
| EndedWithUnpaidInstalments | 5 |
| ReceiptAmountMismatch | 2 |

Counts overlap and are not distinct contracts. Duplicate rows do not establish duplicate cash collection, and missing receipts do not establish nonpayment. No customer records were repaired or constraints installed. Inconsistent ledgers can intentionally block financial changes; reconcile them against accounting evidence before rollout.

The detailed CSV exporter was blocked by automatic approval review because it exports financial records. The approved alternative returns aggregate counts only, with no IDs or row exports:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File sql\Test-LicenceRentalSummary.ps1 `
  -ConfigPath BarakatAlkhreir\App.Config -ExpectedDatabase BarakeitAlkheir2
```

This is not a complete production acceptance pass. Real-role login, deployed Telerik licensing, Crystal rendering/printing, Excel COM export and performance at production volume remain unverified. Financial lifecycle tests passed on synthetic data; historical inconsistencies remain open for accounting review.

### Duplicate-payment investigation - 2026-10-07

A follow-up read-only aggregate check confirmed **5 instalment groups with 5 extra receipts** (10 receipt rows total), and **20 rental/billing-month groups containing different instalment IDs**. These categories can overlap; they are not a count of duplicate cash collections. No unpaid-flag instalment already had a receipt at this snapshot.

The payment table has **no enabled unique index other than its primary key** and **no enabled trigger**. Its primary key distinguishes receipt IDs; it does not prohibit two receipts for the same instalment. This confirms a missing database backstop. It does not identify the particular client/version or sequence that created existing duplicates. Repeated billing-month labels across different instalments also require allocation/date review.

The current `LicenseRentalWorkflow.SavePayment` serializes cooperating clients with a transaction-owned application lock, reloads the ledger, and rejects an already-paid or already-receipted instalment. It writes the receipt and paid flag atomically. The preceding integration run exercised sequential duplicate attempts and two concurrent attempts, with exactly one concurrent winner. These safeguards cannot be assumed to exist in every deployed executable, and the application lock is voluntary: older clients or direct database writers can bypass it. No new payment-code defect was reproduced in this follow-up.

Resolution: identify an affected licence/month and the executable used at the time; review the conflicting receipts and their original accounting evidence; reconcile approved records; then install the reviewed `licence-rental-integrity.sql` protections and deploy the current application to every workstation together. The existing integrity script includes a unique receipt-per-instalment index and deliberately refuses conflicting historical data. Do not run it blindly or delete an arbitrary duplicate. No database changes were applied here.

Repeat the aggregate diagnostic without exporting customer records:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File sql\Test-LicenceRentalSummary.ps1 `
  -ConfigPath BarakatAlkhreir\App.Config -ExpectedDatabase BarakeitAlkheir2 -PaymentDuplicates
```

## Reconciliation and rollout

1. Back up the target database and retain the current application package. Close all application clients for reconciliation and rollout: older executables do not participate in the new application lock.
2. Run the read-only audit against the explicitly selected database, using a fresh private output directory:

   ```powershell
   powershell -NoProfile -ExecutionPolicy Bypass -File sql\Export-LicenceRentalAudit.ps1 `
     -ConfigPath BarakatAlkhreir\App.Config -ExpectedDatabase BarakeitAlkheir `
     -OutputDirectory .codex-build\licence-repair\review-001
   ```

   `audit-0.csv` identifies issue rows; the remaining CSVs contain relevant before-values. Counts are affected issue rows, not distinct contracts. The local read-only audit found 14 rental/receipt link conflicts, seven paid instalments without receipts, two receipt rows in a duplicate group, 11 rows in duplicate month groups, 49 billing-month conflicts, 417 schedule-date differences, 258 invalid instalments, two invalid durations, one contract end-date mismatch, 70 missing collectors and one ended rental with unpaid instalments. Use the new audit output as the authority when reviewing a different database or a later snapshot.

3. Review original receipts, contracts and accounting evidence. Decide exact rental/detail mappings, duplicate treatment, agreed dates/rates, and missing-receipt treatment. Missing receipts do not prove nonpayment; duplicate records do not prove that money was collected twice. Do not rewrite paid contract dates or manufacture receipts to make validation pass. The sample `sql/licence-rental-corrections.example.json` deliberately contains invalid identity placeholders.
4. Prepare explicit `update` or `delete` decisions for the three rental tables. Each entry needs a table, identity, operation, reason/evidence reference and, for an update, a `changes` object. Use JSON numbers/booleans and ISO dates. Preview captures the complete original and proposed rows without writing SQL:

   ```powershell
   powershell -NoProfile -ExecutionPolicy Bypass -File sql\Invoke-LicenceRentalCorrection.ps1 `
     -Mode Preview -ConfigPath BarakatAlkhreir\App.Config -ExpectedDatabase BarakeitAlkheir `
     -ManifestPath <reviewed-decisions.json> -AuditUserId <reviewer-user-id> `
     -PackagePath <private-new-package.clixml>
   ```

   Inspect the package with `Import-Clixml`. It contains financial data and original audit fields, but no connection string. Store it privately and back it up before application. The tool does not infer corrections or insert missing receipts. Updates stamp the supplied reviewer user ID and preview timestamp; identity and creation audit fields cannot be edited through the manifest.
5. After record-level approval, use the same command with `-Mode Apply`, `-ConfigPath`, `-ExpectedDatabase` and `-PackagePath`. Every row must still match its entire original snapshot. The tool uses parameterized values, whitelisted tables/columns, a transaction and the same application lock. Any stale row, FK error or value-rounding mismatch rolls back the entire package. New previews never overwrite an existing rollback package. Treat packages as trusted, reviewed operational inputs; do not edit them after review.
6. Repeat the audit. Once blocking conflicts are resolved, execute `sql/licence-rental-integrity.sql` in the explicitly selected database. It preflights the ledger before installing unique active-licence, rental/month and receipt/instalment indexes, required-key checks and a validated composite receipt-to-instalment/rental FK. It stops on conflicts rather than altering records. Missing collector identities remain reportable and do not by themselves block this upgrade.
7. Deploy the rebuilt application to all clients together. Verify login and add/edit/delete permissions with real roles, rental/payment navigation and cancellation, a permitted transaction in a designated acceptance environment, and the collected/unpaid/scheduled/collector/annual outputs. Verify Crystal rendering, printing and Excel export with the deployed runtime and report directories before reopening normal operations.

Contracts with unresolved ledger conflicts intentionally reject financial changes in the updated application. Complete the review before rollout to avoid blocking normal collections.

## Rollback

Keep all clients closed. If newly installed constraints prevent restoring the reviewed original state, first run `sql/licence-rental-integrity-rollback.sql`; this drops only the named objects introduced by this repair. Then invoke the correction tool with `-Mode Rollback` and the original package. It reverses entries in reverse order, requires every row still to match its proposed state, restores complete original values/audit fields, and restores deleted rows with their original identities. Any conflict cancels the whole rollback; investigate rather than overwriting intervening work. Finally restore the retained application package as appropriate.

Correction packages supplement a database backup; they do not replace it or reconcile transactions created after rollout.
