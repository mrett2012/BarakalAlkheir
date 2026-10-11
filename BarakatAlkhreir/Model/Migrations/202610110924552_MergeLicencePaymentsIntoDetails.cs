namespace BarakalAlkheir.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeLicencePaymentsIntoDetails : DbMigration
    {
        public override void Up()
        {
            // Runs in EF's migration transaction. Table locks also exclude legacy clients that do not use the application lock.
            Sql(@"DECLARE @lockResult int;
EXEC @lockResult=sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',
    @LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
IF @lockResult<0 BEGIN RAISERROR('Cannot lock licence rentals for payment consolidation.',16,1); RETURN; END;
DECLARE @receiptCount bigint, @detailCount bigint;
SELECT @receiptCount=COUNT_BIG(*) FROM dbo.RentLicensesPayment WITH (TABLOCKX,HOLDLOCK);
SELECT @detailCount=COUNT_BIG(*) FROM dbo.RentLicensesDetails WITH (TABLOCKX,HOLDLOCK);
IF OBJECT_ID('dbo.RentLicensesPaymentArchive_20261011','U') IS NOT NULL
BEGIN RAISERROR('Receipt archive already exists. Review the previous migration attempt.',16,1); RETURN; END;
IF EXISTS(SELECT 1 FROM sys.foreign_keys WHERE referenced_object_id=OBJECT_ID('dbo.RentLicensesPayment'))
 OR EXISTS(SELECT 1 FROM sys.sql_expression_dependencies WHERE referenced_id=OBJECT_ID('dbo.RentLicensesPayment') AND referencing_id<>OBJECT_ID('dbo.RentLicensesPayment'))
 OR EXISTS(SELECT 1 FROM sys.sql_modules WHERE CHARINDEX('RentLicensesPayment',definition)>0)
 OR EXISTS(SELECT 1 FROM sys.triggers WHERE parent_id=OBJECT_ID('dbo.RentLicensesPayment') AND is_disabled=0)
BEGIN RAISERROR('A database object depends on the payment table. Review it before consolidation.',16,1); RETURN; END;
IF EXISTS(SELECT 1 FROM dbo.RentLicensesDetails WHERE rd_isPaid IS NULL)
BEGIN RAISERROR('Null paid flags require reconciliation before consolidation.',16,1); RETURN; END;");
            AddColumn("dbo.RentLicensesDetails", "rentLicPay_VouNo", c => c.String(maxLength: 50));
            AddColumn("dbo.RentLicensesDetails", "rentLicPay_PaymentDate", c => c.DateTime());
            AddColumn("dbo.RentLicensesDetails", "rentLicPay_mandoubID", c => c.Int());
            AddColumn("dbo.RentLicensesDetails", "payment_added_Date", c => c.DateTime());
            AddColumn("dbo.RentLicensesDetails", "payment_edited_Date", c => c.DateTime());
            AddColumn("dbo.RentLicensesDetails", "payment_addedBy", c => c.Int());
            AddColumn("dbo.RentLicensesDetails", "payment_editedBy", c => c.Int());
            AddColumn("dbo.RentLicensesDetails", "legacyPaymentID", c => c.Int());
            // The approved policy preserves every receipt, but only a single exact matching source supplies metadata.
            // Ambiguous, orphaned, mismatched, or unpaid-linked receipts remain in the archive; no financial state is inferred.
            Sql(@"SELECT * INTO dbo.RentLicensesPaymentArchive_20261011 FROM dbo.RentLicensesPayment;
ALTER TABLE dbo.RentLicensesPaymentArchive_20261011 ADD CONSTRAINT PK_RentLicensesPaymentArchive_20261011 PRIMARY KEY(rentLicensePaymentID);
IF (SELECT COUNT_BIG(*) FROM dbo.RentLicensesPayment)<>(SELECT COUNT_BIG(*) FROM dbo.RentLicensesPaymentArchive_20261011)
 OR EXISTS(SELECT rentLicensePaymentID,rentLicenseID,rentLicencesDetailsID,rentLicPay_VouNo,rentLicPay_Amount,rentLicPay_PaymentDate,rentLicPay_ForDate,rentLicPay_mandoubID,added_Date,edited_Date,addedBy,editedBy FROM dbo.RentLicensesPayment
    EXCEPT SELECT rentLicensePaymentID,rentLicenseID,rentLicencesDetailsID,rentLicPay_VouNo,rentLicPay_Amount,rentLicPay_PaymentDate,rentLicPay_ForDate,rentLicPay_mandoubID,added_Date,edited_Date,addedBy,editedBy FROM dbo.RentLicensesPaymentArchive_20261011)
BEGIN RAISERROR('Receipt archive verification failed; transaction must roll back.',16,1); RETURN; END;
SELECT rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy
INTO #OriginalLicenceInstallments FROM dbo.RentLicensesDetails;
SELECT p.rentLicensePaymentID,p.rentLicencesDetailsID,COUNT_BIG(*) OVER(PARTITION BY p.rentLicencesDetailsID) AS MatchingReceiptCount
INTO #MatchingLicenceReceipts
FROM dbo.RentLicensesPayment p JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
WHERE d.rd_isPaid=1 AND d.rd_fromDate IS NOT NULL AND d.rd_amount>0
 AND p.rentLicenseID=d.rentLicenseID AND p.rentLicPay_Amount=d.rd_amount
 AND DATEDIFF(month,p.rentLicPay_ForDate,DATEADD(month,CASE WHEN DAY(d.rd_fromDate)>10 THEN 1 ELSE 0 END,d.rd_fromDate))=0
 AND EXISTS(SELECT 1 FROM dbo.RentLicenses r WHERE r.rentLicenseID=d.rentLicenseID);
DELETE FROM #MatchingLicenceReceipts WHERE MatchingReceiptCount<>1;
UPDATE d SET rentLicPay_VouNo=p.rentLicPay_VouNo,rentLicPay_PaymentDate=p.rentLicPay_PaymentDate,
    rentLicPay_mandoubID=p.rentLicPay_mandoubID,payment_added_Date=p.added_Date,payment_edited_Date=p.edited_Date,
    payment_addedBy=p.addedBy,payment_editedBy=p.editedBy,legacyPaymentID=p.rentLicensePaymentID
FROM dbo.RentLicensesDetails d JOIN #MatchingLicenceReceipts m ON m.rentLicencesDetailsID=d.rentLicencesDetailsID
 JOIN dbo.RentLicensesPayment p ON p.rentLicensePaymentID=m.rentLicensePaymentID;
IF (SELECT COUNT_BIG(*) FROM #MatchingLicenceReceipts)<>(SELECT COUNT_BIG(*) FROM dbo.RentLicensesDetails WHERE legacyPaymentID IS NOT NULL)
 OR EXISTS(SELECT p.rentLicencesDetailsID,p.rentLicensePaymentID,p.rentLicPay_VouNo,p.rentLicPay_PaymentDate,p.rentLicPay_mandoubID,p.added_Date,p.edited_Date,p.addedBy,p.editedBy
    FROM dbo.RentLicensesPayment p JOIN #MatchingLicenceReceipts m ON m.rentLicensePaymentID=p.rentLicensePaymentID
    EXCEPT SELECT rentLicencesDetailsID,legacyPaymentID,rentLicPay_VouNo,rentLicPay_PaymentDate,rentLicPay_mandoubID,payment_added_Date,payment_edited_Date,payment_addedBy,payment_editedBy FROM dbo.RentLicensesDetails WHERE legacyPaymentID IS NOT NULL)
 OR (SELECT COUNT_BIG(*) FROM #OriginalLicenceInstallments)<>(SELECT COUNT_BIG(*) FROM dbo.RentLicensesDetails)
 OR EXISTS(SELECT rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy FROM #OriginalLicenceInstallments
    EXCEPT SELECT rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy FROM dbo.RentLicensesDetails)
 OR EXISTS(SELECT 1 FROM dbo.RentLicensesDetails WHERE legacyPaymentID IS NULL AND
    (rentLicPay_VouNo IS NOT NULL OR rentLicPay_PaymentDate IS NOT NULL OR rentLicPay_mandoubID IS NOT NULL
     OR payment_added_Date IS NOT NULL OR payment_edited_Date IS NOT NULL OR payment_addedBy IS NOT NULL OR payment_editedBy IS NOT NULL))
BEGIN RAISERROR('Receipt consolidation verification failed; transaction must roll back.',16,1); RETURN; END;
DROP TABLE #MatchingLicenceReceipts;
DROP TABLE #OriginalLicenceInstallments;
CREATE UNIQUE INDEX UX_LicenceInstalment_LegacyPayment ON dbo.RentLicensesDetails(legacyPaymentID) WHERE legacyPaymentID IS NOT NULL;
ALTER TABLE dbo.RentLicensesDetails WITH CHECK ADD CONSTRAINT CK_LicenceInstalment_PaymentState CHECK
    (rd_isPaid IS NOT NULL AND (rd_isPaid=1 OR (rd_isPaid=0 AND rentLicPay_VouNo IS NULL AND rentLicPay_PaymentDate IS NULL
    AND rentLicPay_mandoubID IS NULL AND payment_added_Date IS NULL AND payment_edited_Date IS NULL
    AND payment_addedBy IS NULL AND payment_editedBy IS NULL AND legacyPaymentID IS NULL)));");
            // SQL Server drops outgoing foreign keys/indexes with the table, regardless of historical constraint names.
            DropTable("dbo.RentLicensesPayment");
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Restore the coordinated pre-merge database backup and application build. An automatic downgrade would lose payments recorded after consolidation.");
        }
    }
}
