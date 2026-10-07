-- Read-only counts only. A billing-month collision is not by itself proof of duplicate cash collection.
SET NOCOUNT ON;
SELECT 'RepeatedInstalmentGroups' AS Issue, COUNT_BIG(*) AS IssueRows
FROM (SELECT rentLicencesDetailsID FROM dbo.RentLicensesPayment
      WHERE rentLicencesDetailsID IS NOT NULL GROUP BY rentLicencesDetailsID HAVING COUNT(*) > 1) g
UNION ALL
SELECT 'ExtraReceiptsForSameInstalment', COALESCE(SUM(g.Receipts - 1), 0)
FROM (SELECT COUNT_BIG(*) AS Receipts FROM dbo.RentLicensesPayment
      WHERE rentLicencesDetailsID IS NOT NULL GROUP BY rentLicencesDetailsID HAVING COUNT(*) > 1) g
UNION ALL
SELECT 'SameRentalBillingMonthGroups', COUNT_BIG(*)
FROM (SELECT rentLicenseID, YEAR(rentLicPay_ForDate) y, MONTH(rentLicPay_ForDate) m
      FROM dbo.RentLicensesPayment WHERE rentLicenseID IS NOT NULL AND rentLicPay_ForDate IS NOT NULL
      GROUP BY rentLicenseID, YEAR(rentLicPay_ForDate), MONTH(rentLicPay_ForDate) HAVING COUNT(*) > 1) g
UNION ALL
SELECT 'SameRentalBillingMonthWithDifferentInstalments', COUNT_BIG(*)
FROM (SELECT rentLicenseID, YEAR(rentLicPay_ForDate) y, MONTH(rentLicPay_ForDate) m
      FROM dbo.RentLicensesPayment WHERE rentLicenseID IS NOT NULL AND rentLicPay_ForDate IS NOT NULL
      GROUP BY rentLicenseID, YEAR(rentLicPay_ForDate), MONTH(rentLicPay_ForDate)
      HAVING COUNT(DISTINCT rentLicencesDetailsID) > 1) g
UNION ALL
SELECT 'UnpaidInstalmentsAlreadyHavingReceipt', COUNT_BIG(*)
FROM dbo.RentLicensesDetails d WHERE d.rd_isPaid=0
  AND EXISTS(SELECT 1 FROM dbo.RentLicensesPayment p WHERE p.rentLicencesDetailsID=d.rentLicencesDetailsID)
UNION ALL
SELECT 'UnfilteredUniqueReceiptInstalmentIndexes', COUNT_BIG(*)
FROM sys.indexes i WHERE i.object_id=OBJECT_ID('dbo.RentLicensesPayment')
  AND i.is_unique=1 AND i.is_disabled=0 AND i.is_hypothetical=0 AND i.has_filter=0
  AND (SELECT COUNT(*) FROM sys.index_columns ic WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.key_ordinal>0)=1
  AND EXISTS(SELECT 1 FROM sys.index_columns ic JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
             WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.key_ordinal=1 AND c.name='rentLicencesDetailsID')
UNION ALL
SELECT 'EnabledPaymentTriggers', COUNT_BIG(*) FROM sys.triggers
WHERE parent_id=OBJECT_ID('dbo.RentLicensesPayment') AND is_disabled=0
UNION ALL
SELECT 'EnabledUniquePaymentIndexesOtherThanPrimaryKey', COUNT_BIG(*) FROM sys.indexes
WHERE object_id=OBJECT_ID('dbo.RentLicensesPayment') AND is_unique=1 AND is_primary_key=0 AND is_disabled=0 AND is_hypothetical=0;
