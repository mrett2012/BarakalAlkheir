-- Read-only reconciliation after payment consolidation. No correction is inferred.
SET NOCOUNT ON;
;WITH Issues AS (
    SELECT 'UnpaidWithReceiptMetadata' Issue,d.rentLicenseID RentalId,d.rentLicencesDetailsID DetailId,
        d.rentLicencesDetailsID PaymentId,'Unpaid installment retains receipt metadata' Explanation
    FROM dbo.RentLicensesDetails d WHERE ISNULL(d.rd_isPaid,0)=0 AND
        (d.rentLicPay_VouNo IS NOT NULL OR d.rentLicPay_PaymentDate IS NOT NULL OR d.rentLicPay_mandoubID IS NOT NULL
        OR d.payment_added_Date IS NOT NULL OR d.payment_edited_Date IS NOT NULL OR d.payment_addedBy IS NOT NULL
        OR d.payment_editedBy IS NOT NULL OR d.legacyPaymentID IS NOT NULL)
    UNION ALL
    SELECT 'MissingCollector',d.rentLicenseID,d.rentLicencesDetailsID,d.rentLicencesDetailsID,'Collector record missing; paid installment remains reportable'
    FROM dbo.RentLicensesDetails d LEFT JOIN dbo.Employees e ON e.employeeID=d.rentLicPay_mandoubID WHERE d.rd_isPaid=1 AND e.employeeID IS NULL
    UNION ALL
    SELECT 'MissingPaymentMetadata',d.rentLicenseID,d.rentLicencesDetailsID,d.rentLicencesDetailsID,'Historical payment date or voucher missing; no value is inferred'
    FROM dbo.RentLicensesDetails d WHERE d.rd_isPaid=1 AND (d.rentLicPay_PaymentDate IS NULL OR NULLIF(LTRIM(RTRIM(d.rentLicPay_VouNo)),'') IS NULL)
    UNION ALL
    SELECT 'InvalidInstalment',d.rentLicenseID,d.rentLicencesDetailsID,NULL,'Missing date/state/month or nonpositive charge requires review'
    FROM dbo.RentLicensesDetails d WHERE d.rd_fromDate IS NULL OR d.rd_toDate IS NULL OR d.rd_isPaid IS NULL OR ISNULL(d.rd_mNo,0)<1 OR ISNULL(d.rd_amount,0)<=0
    UNION ALL
    SELECT 'ScheduleDateMismatch',d.rentLicenseID,d.rentLicencesDetailsID,NULL,'Dates differ from original contract start plus month number'
    FROM dbo.RentLicensesDetails d JOIN dbo.RentLicenses r ON r.rentLicenseID=d.rentLicenseID
    WHERE d.rd_fromDate<>DATEADD(month,d.rd_mNo-1,r.rentLicense_from) OR d.rd_toDate<>DATEADD(day,-1,DATEADD(month,d.rd_mNo,r.rentLicense_from))
    UNION ALL
    SELECT 'DuplicateMonth',d.rentLicenseID,d.rentLicencesDetailsID,NULL,'Duplicate instalment month number'
    FROM dbo.RentLicensesDetails d WHERE (SELECT COUNT(*) FROM dbo.RentLicensesDetails x WHERE x.rentLicenseID=d.rentLicenseID AND x.rd_mNo=d.rd_mNo)>1
    UNION ALL
    SELECT 'InvalidRentalDuration',r.rentLicenseID,NULL,NULL,'Duration invalid or instalment count differs from duration'
    FROM dbo.RentLicenses r WHERE ISNULL(r.rentLicense_noOfMonths,0) NOT BETWEEN 1 AND 1000 OR r.rentLicense_from IS NULL OR
        r.rentLicense_noOfMonths<>(SELECT COUNT(*) FROM dbo.RentLicensesDetails d WHERE d.rentLicenseID=r.rentLicenseID)
    UNION ALL
    SELECT 'MultipleActiveRentals',r.rentLicenseID,NULL,NULL,'Another active rental exists for this licence'
    FROM dbo.RentLicenses r WHERE r.rentLicense_active=1 AND
        (SELECT COUNT(*) FROM dbo.RentLicenses x WHERE x.companyLicencesID=r.companyLicencesID AND x.rentLicense_active=1)>1
    UNION ALL
    SELECT 'RentalEndMismatch',r.rentLicenseID,NULL,NULL,'Contract end differs from original start plus duration'
    FROM dbo.RentLicenses r WHERE r.rentLicense_to IS NULL OR
        r.rentLicense_to<>DATEADD(day,-1,DATEADD(month,CASE WHEN r.rentLicense_noOfMonths BETWEEN 1 AND 1000 THEN r.rentLicense_noOfMonths ELSE 0 END,r.rentLicense_from))
    UNION ALL
    SELECT 'MissingRentalStateOrLicence',r.rentLicenseID,NULL,NULL,'Rental state or licence is missing'
    FROM dbo.RentLicenses r WHERE r.rentLicense_active IS NULL OR r.companyLicencesID IS NULL
    UNION ALL
    SELECT 'EndedWithUnpaidInstalments',r.rentLicenseID,NULL,NULL,'Ended agreement contains unpaid instalments'
    FROM dbo.RentLicenses r WHERE r.rentLicense_active=0 AND EXISTS(SELECT 1 FROM dbo.RentLicensesDetails d WHERE d.rentLicenseID=r.rentLicenseID AND ISNULL(d.rd_isPaid,0)=0)
)
SELECT Issue,RentalId,DetailId,PaymentId,Explanation FROM Issues ORDER BY Issue,RentalId,DetailId,PaymentId;

-- Before-values for preparing exact, reviewed corrections. Excludes names and credentials.
SELECT rentLicencesDetailsID AS PaymentId,legacyPaymentID,rentLicenseID,rd_amount,rentLicPay_PaymentDate,rentLicPay_mandoubID,payment_edited_Date FROM dbo.RentLicensesDetails WHERE rd_isPaid=1 ORDER BY rentLicencesDetailsID;
SELECT rentLicencesDetailsID,rentLicenseID,rd_mNo,rd_fromDate,rd_toDate,rd_amount,rd_isPaid,edited_Date
    FROM dbo.RentLicensesDetails ORDER BY rentLicenseID,rd_mNo;
SELECT rentLicenseID,companyLicencesID,rentLicense_noOfMonths,rentLicense_from,rentLicense_to,
    rentLicense_active,rentLicense_DamanAmount,edited_Date FROM dbo.RentLicenses ORDER BY rentLicenseID;
