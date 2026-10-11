namespace BarakalAlkheir.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestoreArchivedLicencePaymentDates : DbMigration
    {
        public override void Up()
        {
            // Recover only the actual payment date. Installment amounts/owners remain authoritative.
            // The original receipt table was retained by the preceding migration as this archive.
            Sql(@"DECLARE @lockResult int;
EXEC @lockResult=sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',
    @LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
IF @lockResult<0 BEGIN RAISERROR('Cannot lock licence rentals for payment date recovery.',16,1); RETURN; END;
IF OBJECT_ID('dbo.RentLicensesPaymentArchive_20261011','U') IS NULL
BEGIN RAISERROR('The original receipt archive is required for payment date recovery.',16,1); RETURN; END;
IF EXISTS(SELECT 1 FROM sys.triggers WHERE parent_id=OBJECT_ID('dbo.RentLicensesDetails') AND is_disabled=0)
BEGIN RAISERROR('Review installment triggers before recovering payment dates.',16,1); RETURN; END;
DECLARE @lockedCount bigint;
SELECT @lockedCount=COUNT_BIG(*) FROM dbo.RentLicensesDetails WITH (TABLOCKX,HOLDLOCK);
SELECT @lockedCount=COUNT_BIG(*) FROM dbo.RentLicensesPaymentArchive_20261011 WITH (TABLOCKX,HOLDLOCK);

SELECT d.rentLicencesDetailsID,MIN(p.rentLicPay_PaymentDate) AS PaymentDate
INTO #RecoveredLicencePaymentDates
FROM dbo.RentLicensesDetails d JOIN dbo.RentLicensesPaymentArchive_20261011 p
 ON p.rentLicencesDetailsID=d.rentLicencesDetailsID
 AND (d.legacyPaymentID IS NULL OR p.rentLicensePaymentID=d.legacyPaymentID)
WHERE d.rd_isPaid=1 AND d.rentLicPay_PaymentDate IS NULL
GROUP BY d.rentLicencesDetailsID
HAVING COUNT_BIG(*)=COUNT_BIG(p.rentLicPay_PaymentDate)
 AND MIN(p.rentLicPay_PaymentDate)=MAX(p.rentLicPay_PaymentDate);

UPDATE d SET rentLicPay_PaymentDate=r.PaymentDate
FROM dbo.RentLicensesDetails d JOIN #RecoveredLicencePaymentDates r
 ON r.rentLicencesDetailsID=d.rentLicencesDetailsID;
IF EXISTS(SELECT 1 FROM #RecoveredLicencePaymentDates r
 LEFT JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=r.rentLicencesDetailsID
 WHERE d.rentLicPay_PaymentDate IS NULL OR d.rentLicPay_PaymentDate<>r.PaymentDate)
BEGIN RAISERROR('Payment date recovery verification failed.',16,1); RETURN; END;
DROP TABLE #RecoveredLicencePaymentDates;");
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Recovered historical dates cannot be cleared automatically. Restore the verified backup if a rollback is required.");
        }
    }
}
