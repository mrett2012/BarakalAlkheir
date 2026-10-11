-- Read-only aggregate checks after payment consolidation.
SET NOCOUNT ON;
SELECT 'DuplicateLegacyReceiptGroups' AS Issue,COUNT_BIG(*) AS IssueRows FROM
 (SELECT legacyPaymentID FROM dbo.RentLicensesDetails WHERE legacyPaymentID IS NOT NULL GROUP BY legacyPaymentID HAVING COUNT(*)>1) g
UNION ALL
SELECT 'DuplicateInstallmentMonthGroups',COUNT_BIG(*) FROM
 (SELECT rentLicenseID,rd_mNo FROM dbo.RentLicensesDetails GROUP BY rentLicenseID,rd_mNo HAVING COUNT(*)>1) g
UNION ALL
SELECT 'EnabledInstallmentTriggers',COUNT_BIG(*) FROM sys.triggers WHERE parent_id=OBJECT_ID('dbo.RentLicensesDetails') AND is_disabled=0;