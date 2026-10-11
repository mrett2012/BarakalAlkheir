-- Read-only before migration. IDs only; results may still be sensitive financial data.
-- Forecast the approved archive-preserving migration; never infer nonpayment or discard original receipts.
SET NOCOUNT ON;
WITH MatchingReceipts AS (
 SELECT p.rentLicensePaymentID,p.rentLicencesDetailsID,COUNT_BIG(*) OVER(PARTITION BY p.rentLicencesDetailsID) AS MatchCount
 FROM dbo.RentLicensesPayment p JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
 WHERE d.rd_isPaid=1 AND d.rd_fromDate IS NOT NULL AND d.rd_amount>0
 AND p.rentLicenseID=d.rentLicenseID AND p.rentLicPay_Amount=d.rd_amount
 AND DATEDIFF(month,p.rentLicPay_ForDate,DATEADD(month,CASE WHEN DAY(d.rd_fromDate)>10 THEN 1 ELSE 0 END,d.rd_fromDate))=0
 AND EXISTS(SELECT 1 FROM dbo.RentLicenses r WHERE r.rentLicenseID=d.rentLicenseID)
)
SELECT (SELECT COUNT_BIG(*) FROM dbo.RentLicensesPayment) AS OriginalReceiptsToArchive,
 (SELECT COUNT_BIG(*) FROM MatchingReceipts WHERE MatchCount=1) AS MetadataSources,
 (SELECT COUNT_BIG(*) FROM dbo.RentLicensesPayment)-(SELECT COUNT_BIG(*) FROM MatchingReceipts WHERE MatchCount=1) AS ArchiveOnlyReceipts,
 (SELECT COUNT_BIG(*) FROM dbo.RentLicensesDetails WHERE rd_isPaid IS NULL) AS BlockingNullPaidFlags;
-- The following duplicate/orphan/paid-without-receipt results are review inputs, not automatic deletion instructions.
SELECT rentLicencesDetailsID,COUNT_BIG(*) AS ReceiptCount
FROM dbo.RentLicensesPayment GROUP BY rentLicencesDetailsID HAVING COUNT(*)>1;
SELECT p.rentLicensePaymentID,p.rentLicenseID,p.rentLicencesDetailsID
FROM dbo.RentLicensesPayment p LEFT JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
WHERE d.rentLicencesDetailsID IS NULL;
SELECT d.rentLicenseID,d.rentLicencesDetailsID,d.rd_isPaid
FROM dbo.RentLicensesDetails d WHERE d.rd_isPaid IS NULL
 OR (d.rd_isPaid=1 AND NOT EXISTS(SELECT 1 FROM dbo.RentLicensesPayment p WHERE p.rentLicencesDetailsID=d.rentLicencesDetailsID))
 OR (d.rd_isPaid=0 AND EXISTS(SELECT 1 FROM dbo.RentLicensesPayment p WHERE p.rentLicencesDetailsID=d.rentLicencesDetailsID));
-- These disagreements will use the installment's authoritative rental, amount and billing period.
-- All original receipt values are retained in the migration archive.
SELECT p.rentLicensePaymentID,p.rentLicencesDetailsID
FROM dbo.RentLicensesPayment p JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
WHERE p.rentLicenseID IS NULL OR p.rentLicenseID<>d.rentLicenseID OR p.rentLicPay_Amount IS NULL OR p.rentLicPay_Amount<>d.rd_amount
 OR p.rentLicPay_ForDate IS NULL OR DAY(p.rentLicPay_ForDate)<>15
 OR DATEDIFF(month,p.rentLicPay_ForDate,DATEADD(month,CASE WHEN DAY(d.rd_fromDate)>10 THEN 1 ELSE 0 END,d.rd_fromDate))<>0;
SELECT OBJECT_SCHEMA_NAME(referencing_id)+'.'+OBJECT_NAME(referencing_id) AS DependentObject
FROM sys.sql_expression_dependencies WHERE referenced_id=OBJECT_ID('dbo.RentLicensesPayment');
SELECT name AS IncomingForeignKey FROM sys.foreign_keys WHERE referenced_object_id=OBJECT_ID('dbo.RentLicensesPayment');
SELECT name AS EnabledPaymentTrigger FROM sys.triggers WHERE parent_id=OBJECT_ID('dbo.RentLicensesPayment') AND is_disabled=0;
SELECT OBJECT_SCHEMA_NAME(object_id)+'.'+OBJECT_NAME(object_id) AS ModuleToReview
FROM sys.sql_modules WHERE CHARINDEX('RentLicensesPayment',definition)>0;
