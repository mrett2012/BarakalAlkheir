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
