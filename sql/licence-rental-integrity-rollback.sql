-- Removes only objects introduced by licence-rental-integrity.sql; never changes records.
-- Reconciliation changes have their own before-value rollback manifests.
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @result int;
    EXEC @result=sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
    IF @result<0 RAISERROR('Cannot lock licence rentals.',16,1);
    IF OBJECT_ID('dbo.CK_LicenceInstalment_RequiredKeys','C') IS NOT NULL ALTER TABLE dbo.RentLicensesDetails DROP CONSTRAINT CK_LicenceInstalment_RequiredKeys;
    IF EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name='UX_LicenceInstalment_RentalIdentity') DROP INDEX UX_LicenceInstalment_RentalIdentity ON dbo.RentLicensesDetails;
    IF EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name='UX_LicenceInstalment_Month') DROP INDEX UX_LicenceInstalment_Month ON dbo.RentLicensesDetails;
    IF EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicenses') AND name='UX_LicenceRental_ActiveLicence') DROP INDEX UX_LicenceRental_ActiveLicence ON dbo.RentLicenses;
    IF OBJECT_ID('dbo.CK_LicenceInstalment_PaymentState','C') IS NOT NULL ALTER TABLE dbo.RentLicensesDetails DROP CONSTRAINT CK_LicenceInstalment_PaymentState;
    IF EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name='UX_LicenceInstalment_LegacyPayment') DROP INDEX UX_LicenceInstalment_LegacyPayment ON dbo.RentLicensesDetails;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
    DECLARE @error nvarchar(2048); SET @error=ERROR_MESSAGE(); RAISERROR('%s',16,1,@error);
END CATCH;
