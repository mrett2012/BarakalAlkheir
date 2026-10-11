-- Read-only reconciliation. Run against the explicitly selected target database.
-- IDs are database identities, not licence/customer names. No correction is inferred.
SET NOCOUNT ON;
;WITH Issues AS (
    SELECT 'ReceiptRentalMismatch' Issue, p.rentLicenseID RentalId, p.rentLicencesDetailsID DetailId,
        p.rentLicensePaymentID PaymentId, 'Receipt and instalment refer to different rentals' Explanation
    FROM dbo.RentLicensesPayment p JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
    WHERE p.rentLicenseID<>d.rentLicenseID OR p.rentLicenseID IS NULL
    UNION ALL
    SELECT 'MissingInstalment',p.rentLicenseID,p.rentLicencesDetailsID,p.rentLicensePaymentID,'Receipt has no matching instalment'
    FROM dbo.RentLicensesPayment p LEFT JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
    WHERE d.rentLicencesDetailsID IS NULL
    UNION ALL
    SELECT 'PaidWithoutReceipt',d.rentLicenseID,d.rentLicencesDetailsID,NULL,'Paid flag has no receipt'
    FROM dbo.RentLicensesDetails d WHERE d.rd_isPaid=1 AND NOT EXISTS(SELECT 1 FROM dbo.RentLicensesPayment p WHERE p.rentLicencesDetailsID=d.rentLicencesDetailsID)
    UNION ALL
    SELECT 'UnpaidWithReceipt',d.rentLicenseID,d.rentLicencesDetailsID,p.rentLicensePaymentID,'Receipt exists but instalment is unpaid'
    FROM dbo.RentLicensesDetails d JOIN dbo.RentLicensesPayment p ON p.rentLicencesDetailsID=d.rentLicencesDetailsID
    WHERE ISNULL(d.rd_isPaid,0)=0
    UNION ALL
    SELECT 'DuplicateReceipt',p.rentLicenseID,p.rentLicencesDetailsID,p.rentLicensePaymentID,'More than one receipt for this instalment'
    FROM dbo.RentLicensesPayment p WHERE (SELECT COUNT(*) FROM dbo.RentLicensesPayment x WHERE x.rentLicencesDetailsID=p.rentLicencesDetailsID)>1
    UNION ALL
    SELECT 'ReceiptAmountMismatch',p.rentLicenseID,p.rentLicencesDetailsID,p.rentLicensePaymentID,'Receipt amount differs from full instalment charge'
    FROM dbo.RentLicensesPayment p JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
    WHERE p.rentLicPay_Amount IS NULL OR p.rentLicPay_Amount<>d.rd_amount
    UNION ALL
    SELECT 'BillingMonthMismatch',p.rentLicenseID,p.rentLicencesDetailsID,p.rentLicensePaymentID,'Receipt billing month differs from instalment cutoff'
    FROM dbo.RentLicensesPayment p JOIN dbo.RentLicensesDetails d ON d.rentLicencesDetailsID=p.rentLicencesDetailsID
    WHERE p.rentLicPay_ForDate IS NULL OR DAY(p.rentLicPay_ForDate)<>15 OR
        DATEDIFF(month,p.rentLicPay_ForDate,DATEADD(month,CASE WHEN DAY(d.rd_fromDate)>10 THEN 1 ELSE 0 END,d.rd_fromDate))<>0
    UNION ALL
    SELECT 'MissingCollector',p.rentLicenseID,p.rentLicencesDetailsID,p.rentLicensePaymentID,'Collector record is missing; receipt remains reportable'
    FROM dbo.RentLicensesPayment p LEFT JOIN dbo.Employees e ON e.employeeID=p.rentLicPay_mandoubID WHERE e.employeeID IS NULL
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
SELECT rentLicensePaymentID,rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate,
    rentLicPay_mandoubID,edited_Date FROM dbo.RentLicensesPayment ORDER BY rentLicensePaymentID;
SELECT rentLicencesDetailsID,rentLicenseID,rd_mNo,rd_fromDate,rd_toDate,rd_amount,rd_isPaid,edited_Date
    FROM dbo.RentLicensesDetails ORDER BY rentLicenseID,rd_mNo;
SELECT rentLicenseID,companyLicencesID,rentLicense_noOfMonths,rentLicense_from,rentLicense_to,
    rentLicense_active,rentLicense_DamanAmount,edited_Date FROM dbo.RentLicenses ORDER BY rentLicenseID;
