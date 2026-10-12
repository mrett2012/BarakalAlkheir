# Licence receipt allocation recovery (2026-10-12)

## Confirmed problem

Historical receipts can have `rentLicencesDetailsID` pointing to another rental or billing month. The receipt's rental, `rentLicPay_ForDate` year/month, and amount must also be reconciled. The earlier report implementation joined receipts by the old detail ID; it could show an already-paid installment as unpaid when that link was stale.

The current `LicenseRentalReports.PaidByInstalment` uses `rd_isPaid` and the installment charge. It does not require a voucher, payment date, or `legacyPaymentID` to recognize a paid installment. This behavior was already in the current source and local executable before this repair. The clients' installed executables and printed report runtime were not inspected.

The original consolidation preserved paid flags and retained every receipt in `RentLicensesPaymentArchive_20261011`, but only exact ID-based matches supplied metadata. The subsequent date-only recovery also followed old detail IDs; some dates therefore belonged to a different rental/month. Do not use that date alone as allocation evidence.

## New migration

`202610121200000_RestoreLicenceReceiptAllocations` runs once through EF6, including the existing startup migration runner. Applied migrations and their snapshots are unchanged.

- Match by rental ID, billing year/month using the existing day-10 cutoff, and exact positive amount. Ignore the archived detail ID for allocation.
- Require exactly one receipt and one installment in that rental/month. Count all rows before filtering by amount, paid state, or prior allocation.
- Recover metadata only for an already-paid installment with no allocated legacy receipt and no new receipt metadata. Never turn an unpaid installment into a paid one or clear an existing paid flag.
- Exclude receipts already allocated elsewhere and installments created/edited since the archive was created.
- Preserve unrelated actual payment dates. Replace a date only when it is blank, already agrees with the correct receipt, or can be reproduced exactly by the prior ID-only recovery (all old-linked receipts have one identical non-null date).
- Copy voucher, actual payment date, collector, original receipt audit fields, and legacy receipt ID from the uniquely matched archive row. Null source fields remain null.
- Preserve charges, schedule dates, rental ownership, paid flags, installment audit fields, and original archive records.

The migration takes the existing transaction-owned application lock and exclusive table locks. Active installment/archive triggers stop it for review. `LicenceReceiptRecovery_20261012` stores every affected row's complete original values, its recovered receipt ID, and the UTC recovery time. Verification failures roll back the transaction. Automatic downgrade is refused because it could overwrite payments edited after recovery; use the retained before-values and coordinated backup for a reviewed reversal.

Duplicate months/receipts, amount differences, already-used receipts, and later edits remain unchanged. No recurring report-time inference or archive modification was added. Car rental tables are outside this repair.

## Verification and local application

On 2026-10-12, after separate checksum-verified SQL Server `COPY_ONLY` backups, the migration was applied through EF6 to both user-selected local catalogs:

| Database | Receipt allocations restored |
| --- | ---: |
| BarakeitAlkheir | 45 |
| BarakeitAlkheir2 | 126 |

Ordered before/after hashes confirmed unchanged installment financial/schedule/audit fields, all paid flags, the complete original receipt archive, and both car customer installment/payment tables. Every recovered metadata row matched its source receipt. The current reporting helper was executed against both databases and excluded the reported historical paid installments. The older catalog contains a different schedule horizon; the databases were reconciled independently, not combined.

After recovery, 21 paid installments in `BarakeitAlkheir` and 36 in `BarakeitAlkheir2` still have no uniquely allocated legacy receipt. They remain paid and unchanged; incomplete or conflicting evidence must not be converted into an unpaid balance.

The local `BarakatAlkhreir/bin/Debug/BarakalAlkheir.exe` and PDB were updated from the verified isolated build. Their previous versions are retained under `.codex-build/payment-recovery/previous-local-build/`. Configuration, dependencies, and report files were preserved. The private application log and verified backup locations are in `.codex-build/payment-recovery/applied-recovery.log`.

Validation:

- Visual Studio 2022 Debug/x86 solution restore/build passed with existing warnings.
- 283 disposable SQL Server assertions covered 42 allocation cases, paid-state reporting, complete before-values, archive preservation, repeated migration, trigger failure/rollback, and retry.
- 42 startup migration assertions passed, including concurrent startup and failure handling.
- 5,457 model parity checks passed.
- `git diff --check` passed.

```powershell
tests\Run-ReceiptAllocationRecoveryTests.ps1 -BuildBin .codex-build\payment-recovery\bin
tests\Run-PaymentMergeMigrationTests.ps1 -BuildBin .codex-build\payment-recovery\bin -StartupOnly
tests\Run-CodeFirstTests.ps1 -BuildBin .codex-build\payment-recovery\bin
```

Client PCs still need the updated application. Deploy the rebuilt executable with its existing compatible EF6 dependencies and correct configuration/report layout. Production client login, permissions, Crystal rendering/printing, and Excel export were not exercised by this repair.
