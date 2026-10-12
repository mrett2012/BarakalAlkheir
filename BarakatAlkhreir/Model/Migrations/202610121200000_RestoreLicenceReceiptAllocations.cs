namespace BarakalAlkheir.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RestoreLicenceReceiptAllocations : DbMigration
    {
        public override void Up()
        {
            // The old detail IDs can point to another month or rental. Recover only unique
            // receipts for already-paid installments; never infer nonpayment from a missing link.
            Sql(@"DECLARE @lockResult int, @lockedCount bigint, @archiveCreated datetime;
EXEC @lockResult=sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',
    @LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
IF @lockResult<0 BEGIN RAISERROR('Cannot lock licence rentals for receipt recovery.',16,1); RETURN; END;
IF OBJECT_ID('dbo.RentLicensesPaymentArchive_20261011','U') IS NULL
BEGIN RAISERROR('The original receipt archive is required for receipt recovery.',16,1); RETURN; END;
IF OBJECT_ID('dbo.LicenceReceiptRecovery_20261012','U') IS NOT NULL
BEGIN RAISERROR('Receipt recovery evidence already exists. Review migration history before retrying.',16,1); RETURN; END;
IF EXISTS(SELECT 1 FROM sys.triggers WHERE parent_id IN
 (OBJECT_ID('dbo.RentLicensesDetails'),OBJECT_ID('dbo.RentLicensesPaymentArchive_20261011')) AND is_disabled=0)
BEGIN RAISERROR('Review installment/archive triggers before receipt recovery.',16,1); RETURN; END;
SELECT @lockedCount=COUNT_BIG(*) FROM dbo.RentLicenses WITH (TABLOCKX,HOLDLOCK);
SELECT @lockedCount=COUNT_BIG(*) FROM dbo.RentLicensesDetails WITH (TABLOCKX,HOLDLOCK);
SELECT @lockedCount=COUNT_BIG(*) FROM dbo.RentLicensesPaymentArchive_20261011 WITH (TABLOCKX,HOLDLOCK);
SELECT @archiveCreated=create_date FROM sys.objects WHERE object_id=OBJECT_ID('dbo.RentLicensesPaymentArchive_20261011');
SELECT * INTO #OriginalLicenceDetails FROM dbo.RentLicensesDetails;

-- Count every row in each rental/month before filtering by amount, paid state or allocation.
-- Otherwise an already-consumed receipt or a differently priced duplicate can appear unique.
;WITH Receipts AS (
 SELECT p.*,DATEDIFF(month,0,p.rentLicPay_ForDate) AS BillingMonth,
 COUNT_BIG(*) OVER(PARTITION BY p.rentLicenseID,DATEDIFF(month,0,p.rentLicPay_ForDate)) AS ReceiptCount
 FROM dbo.RentLicensesPaymentArchive_20261011 p),
Installments AS (
 SELECT d.*,DATEDIFF(month,0,DATEADD(month,CASE WHEN DAY(d.rd_fromDate)>10 THEN 1 ELSE 0 END,d.rd_fromDate)) AS BillingMonth,
 COUNT_BIG(*) OVER(PARTITION BY d.rentLicenseID,DATEDIFF(month,0,
 DATEADD(month,CASE WHEN DAY(d.rd_fromDate)>10 THEN 1 ELSE 0 END,d.rd_fromDate))) AS InstallmentCount
 FROM dbo.RentLicensesDetails d),
OldLinkedDates AS (
 -- Identify only dates reproducibly supplied by the previous ID-only date migration.
 SELECT rentLicencesDetailsID,MIN(rentLicPay_PaymentDate) AS PaymentDate
 FROM dbo.RentLicensesPaymentArchive_20261011
 GROUP BY rentLicencesDetailsID
 HAVING COUNT_BIG(*)=COUNT_BIG(rentLicPay_PaymentDate)
 AND MIN(rentLicPay_PaymentDate)=MAX(rentLicPay_PaymentDate))
SELECT d.rentLicencesDetailsID,p.rentLicensePaymentID
INTO #RecoveredLicenceReceipts
FROM Installments d JOIN Receipts p ON p.rentLicenseID=d.rentLicenseID AND p.BillingMonth=d.BillingMonth
LEFT JOIN OldLinkedDates oldDate ON oldDate.rentLicencesDetailsID=d.rentLicencesDetailsID
WHERE d.InstallmentCount=1 AND p.ReceiptCount=1 AND d.rd_amount>0 AND p.rentLicPay_Amount=d.rd_amount
 AND d.rd_isPaid=1 AND d.legacyPaymentID IS NULL
 AND d.rentLicPay_VouNo IS NULL AND d.rentLicPay_mandoubID IS NULL
 AND d.payment_added_Date IS NULL AND d.payment_edited_Date IS NULL
 AND d.payment_addedBy IS NULL AND d.payment_editedBy IS NULL
 AND (d.added_Date IS NULL OR d.added_Date<@archiveCreated)
 AND (d.edited_Date IS NULL OR d.edited_Date<@archiveCreated)
 AND (d.rentLicPay_PaymentDate IS NULL OR d.rentLicPay_PaymentDate=p.rentLicPay_PaymentDate
      OR d.rentLicPay_PaymentDate=oldDate.PaymentDate)
 AND NOT EXISTS(SELECT 1 FROM dbo.RentLicensesDetails used WHERE used.legacyPaymentID=p.rentLicensePaymentID)
 AND EXISTS(SELECT 1 FROM dbo.RentLicenses r WHERE r.rentLicenseID=d.rentLicenseID);

-- Durable before-values support a reviewed reversal without altering the original receipts.
SELECT d.*,m.rentLicensePaymentID AS RecoveryPaymentID,GETUTCDATE() AS RecoveredAt
INTO dbo.LicenceReceiptRecovery_20261012
FROM dbo.RentLicensesDetails d JOIN #RecoveredLicenceReceipts m ON m.rentLicencesDetailsID=d.rentLicencesDetailsID;
ALTER TABLE dbo.LicenceReceiptRecovery_20261012 ADD CONSTRAINT PK_LicenceReceiptRecovery_20261012 PRIMARY KEY(rentLicencesDetailsID);
UPDATE d SET rentLicPay_VouNo=p.rentLicPay_VouNo,rentLicPay_PaymentDate=p.rentLicPay_PaymentDate,
 rentLicPay_mandoubID=p.rentLicPay_mandoubID,payment_added_Date=p.added_Date,payment_edited_Date=p.edited_Date,
 payment_addedBy=p.addedBy,payment_editedBy=p.editedBy,legacyPaymentID=p.rentLicensePaymentID
FROM dbo.RentLicensesDetails d JOIN #RecoveredLicenceReceipts m ON m.rentLicencesDetailsID=d.rentLicencesDetailsID
JOIN dbo.RentLicensesPaymentArchive_20261011 p ON p.rentLicensePaymentID=m.rentLicensePaymentID;

IF (SELECT COUNT_BIG(*) FROM #OriginalLicenceDetails)<>(SELECT COUNT_BIG(*) FROM dbo.RentLicensesDetails)
 OR EXISTS(SELECT rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy
 FROM #OriginalLicenceDetails EXCEPT
 SELECT rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy FROM dbo.RentLicensesDetails)
 OR EXISTS(SELECT d.* FROM #OriginalLicenceDetails d WHERE NOT EXISTS
 (SELECT 1 FROM #RecoveredLicenceReceipts m WHERE m.rentLicencesDetailsID=d.rentLicencesDetailsID)
 EXCEPT SELECT * FROM dbo.RentLicensesDetails)
 OR EXISTS(SELECT m.rentLicencesDetailsID,p.rentLicensePaymentID,p.rentLicPay_VouNo,p.rentLicPay_PaymentDate,p.rentLicPay_mandoubID,
 p.added_Date,p.edited_Date,p.addedBy,p.editedBy FROM #RecoveredLicenceReceipts m
 JOIN dbo.RentLicensesPaymentArchive_20261011 p ON p.rentLicensePaymentID=m.rentLicensePaymentID
 EXCEPT SELECT rentLicencesDetailsID,legacyPaymentID,rentLicPay_VouNo,rentLicPay_PaymentDate,rentLicPay_mandoubID,
 payment_added_Date,payment_edited_Date,payment_addedBy,payment_editedBy FROM dbo.RentLicensesDetails)
BEGIN RAISERROR('Receipt recovery verification failed; transaction must roll back.',16,1); RETURN; END;
DROP TABLE #RecoveredLicenceReceipts;
DROP TABLE #OriginalLicenceDetails;");
        }

        public override void Down()
        {
            throw new NotSupportedException("Use the receipt recovery before-values and verified backup for a reviewed rollback. Automatic reversal could overwrite subsequent payments.");
        }
    }
}
