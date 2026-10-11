# Before-consolidation tools

These files target the former `RentLicensesPayment` table. They are retained for explicit reconciliation before applying the payment consolidation migration.

The audit export and summary PowerShell tools select these queries when the new installment receipt columns are absent. After migration, they select the current root-level queries instead.

Do not run these legacy integrity scripts after consolidation. No script here is applied automatically.
