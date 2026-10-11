-- Install only after reviewing and reconciling existing financial data.
-- Idempotent, transactional, and deliberately stops rather than changing conflicting records.
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @lockResult int;
    EXEC @lockResult=sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
    IF @lockResult<0 RAISERROR('Cannot lock licence rentals for integrity upgrade.',16,1);

    IF EXISTS(SELECT 1 FROM dbo.RentLicenses WHERE rentLicense_active=1 AND companyLicencesID IS NULL)
        OR EXISTS(SELECT companyLicencesID FROM dbo.RentLicenses WHERE rentLicense_active=1 GROUP BY companyLicencesID HAVING COUNT(*)>1)
        OR EXISTS(SELECT rentLicenseID,rd_mNo FROM dbo.RentLicensesDetails GROUP BY rentLicenseID,rd_mNo HAVING COUNT(*)>1)
        RAISERROR('Duplicate active rentals, instalments or receipts require reconciliation.',16,1);

    IF EXISTS(SELECT 1 FROM dbo.RentLicensesDetails d LEFT JOIN dbo.RentLicenses r ON r.rentLicenseID=d.rentLicenseID
        WHERE r.rentLicenseID IS NULL OR d.rd_mNo IS NULL OR d.rd_mNo<1 OR d.rd_amount IS NULL OR d.rd_amount<=0 OR
        d.rd_fromDate IS NULL OR d.rd_toDate IS NULL OR d.rd_isPaid IS NULL OR
        d.rd_fromDate<>DATEADD(month,d.rd_mNo-1,r.rentLicense_from) OR d.rd_toDate<>DATEADD(day,-1,DATEADD(month,d.rd_mNo,r.rentLicense_from)))
        RAISERROR('Invalid instalments or schedule differences require reconciliation.',16,1);

    IF EXISTS(SELECT 1 FROM dbo.RentLicensesDetails WHERE rd_isPaid=0 AND
        (rentLicPay_VouNo IS NOT NULL OR rentLicPay_PaymentDate IS NOT NULL OR rentLicPay_mandoubID IS NOT NULL OR
         payment_added_Date IS NOT NULL OR payment_edited_Date IS NOT NULL OR payment_addedBy IS NOT NULL OR payment_editedBy IS NOT NULL OR legacyPaymentID IS NOT NULL))
        RAISERROR('Unpaid installments retain receipt metadata; reconciliation required.',16,1);
    IF EXISTS(SELECT 1 FROM dbo.RentLicenses r WHERE ISNULL(r.rentLicense_noOfMonths,0) NOT BETWEEN 1 AND 1000 OR
        r.rentLicense_from IS NULL OR r.rentLicense_to IS NULL OR r.rentLicense_active IS NULL OR
        r.rentLicense_noOfMonths<>(SELECT COUNT(*) FROM dbo.RentLicensesDetails d WHERE d.rentLicenseID=r.rentLicenseID) OR
        r.rentLicense_to<>DATEADD(day,-1,DATEADD(month,r.rentLicense_noOfMonths,r.rentLicense_from)) OR
        (r.rentLicense_active=0 AND EXISTS(SELECT 1 FROM dbo.RentLicensesDetails d WHERE d.rentLicenseID=r.rentLicenseID AND d.rd_isPaid=0)))
        RAISERROR('Rental durations or ended-rental balances require reconciliation.',16,1);

    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicenses') AND name='UX_LicenceRental_ActiveLicence')
        CREATE UNIQUE INDEX UX_LicenceRental_ActiveLicence ON dbo.RentLicenses(companyLicencesID) WHERE rentLicense_active=1;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name='UX_LicenceInstalment_Month')
        CREATE UNIQUE INDEX UX_LicenceInstalment_Month ON dbo.RentLicensesDetails(rentLicenseID,rd_mNo);
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name='UX_LicenceInstalment_RentalIdentity')
        CREATE UNIQUE INDEX UX_LicenceInstalment_RentalIdentity ON dbo.RentLicensesDetails(rentLicenseID,rentLicencesDetailsID);
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name='UX_LicenceInstalment_LegacyPayment')
        CREATE UNIQUE INDEX UX_LicenceInstalment_LegacyPayment ON dbo.RentLicensesDetails(legacyPaymentID) WHERE legacyPaymentID IS NOT NULL;
    IF OBJECT_ID('dbo.CK_LicenceInstalment_PaymentState','C') IS NULL
        ALTER TABLE dbo.RentLicensesDetails WITH CHECK ADD CONSTRAINT CK_LicenceInstalment_PaymentState CHECK
            (rd_isPaid IS NOT NULL AND (rd_isPaid=1 OR (rd_isPaid=0 AND rentLicPay_VouNo IS NULL AND rentLicPay_PaymentDate IS NULL
             AND rentLicPay_mandoubID IS NULL AND payment_added_Date IS NULL AND payment_edited_Date IS NULL
             AND payment_addedBy IS NULL AND payment_editedBy IS NULL AND legacyPaymentID IS NULL)));
    IF OBJECT_ID('dbo.CK_LicenceInstalment_RequiredKeys','C') IS NULL
        ALTER TABLE dbo.RentLicensesDetails WITH CHECK ADD CONSTRAINT CK_LicenceInstalment_RequiredKeys CHECK(rentLicenseID IS NOT NULL AND rd_mNo IS NOT NULL AND rd_mNo BETWEEN 1 AND 1000);
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
    DECLARE @error nvarchar(2048); SET @error=ERROR_MESSAGE();
    RAISERROR('%s',16,1,@error);
END CATCH;
