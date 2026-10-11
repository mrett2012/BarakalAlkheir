using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BarakalAlkheir.Model;

namespace BarakalAlkheir.Lic
{
    internal sealed class RentalChangedException : InvalidOperationException
    {
        internal RentalChangedException() : base("تم تغيير بيانات القسط. يرجى مراجعة البيانات المحدثة ثم الحفظ مرة أخرى") { }
    }

    internal sealed class LicenseRentalWorkflow
    {
        private readonly string connectionString;

        internal LicenseRentalWorkflow() { }
        // An explicit connection is used by the isolated database regression harness.
        internal LicenseRentalWorkflow(string connectionString) { this.connectionString = connectionString; }

        private BarakeitAlkheirEntities OpenContext()
        {
            return connectionString == null ? new BarakeitAlkheirEntities() : new BarakeitAlkheirEntities(connectionString);
        }

        private T Write<T>(Func<BarakeitAlkheirEntities, T> operation)
        {
            var options = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(60) };
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, options))
            using (var db = OpenContext())
            {
                db.Database.Connection.Open();
                // One database-scoped lock covers this small desktop module's financial writes.
                // Keeping the connection open makes the lock live until the transaction completes.
                db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, @"DECLARE @result int;
EXEC @result = sys.sp_getapplock @Resource=N'BarakalAlkheir.LicenceRentals',
 @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=10000;
IF @result < 0 RAISERROR(N'تعذر حجز العقد للتعديل. يرجى المحاولة مرة أخرى',16,1);");
                T result = operation(db);
                db.SaveChanges();
                transaction.Complete();
                return result;
            }
        }

        private static void RequirePermission(BarakeitAlkheirEntities db, string form, string action)
        {
            if (MainClass.userID <= 0)
                throw new InvalidOperationException("يرجى تسجيل الدخول");
            if (MainClass.isSuperUser) return;
            bool allowed = (from screen in db.FormScreans
                           join ev in db.FormEvents on screen.formID equals ev.formID
                           join definition in db.EventsDefinitions on ev.eventID equals definition.eventID
                           join groupEvent in db.UserGroupEvents on ev.formEventID equals groupEvent.formEventID
                           where screen.form_IDName == form && definition.event_Name == action
                               && groupEvent.userGroupID == MainClass.userGroupID
                           select ev.formEventID).Any();
            if (!allowed) throw new InvalidOperationException("ليس لديك صلاحية لتنفيذ هذه العملية");
        }

        private static void RequireActive(RentLicens rental)
        {
            if (rental == null) throw new InvalidOperationException("العقد غير موجود");
            if (rental.rentLicense_active != true)
                throw new InvalidOperationException("لا يمكن تعديل عقد تم إنهاؤه");
        }

        private sealed class Ledger
        {
            internal RentLicens Rental;
            internal List<RentLicensesDetail> Details;
            internal bool HasPayments { get { return Details.Any(d => d.rd_isPaid == true); } }
        }

        private static Ledger LoadLedger(BarakeitAlkheirEntities db, int rentalId)
        {
            var rental = db.RentLicenses.FirstOrDefault(r => r.rentLicenseID == rentalId);
            RequireActive(rental);
            var details = db.RentLicensesDetails.Where(d => d.rentLicenseID == rentalId)
                .OrderBy(d => d.rd_fromDate).ThenBy(d => d.rd_mNo).ThenBy(d => d.rentLicencesDetailsID).ToList();
            const string error = "توجد بيانات غير متطابقة في العقد أو الأقساط أو المدفوعات. يجب مراجعتها قبل التعديل";
            if (!rental.rentLicense_from.HasValue || !rental.rentLicense_noOfMonths.HasValue ||
                rental.rentLicense_noOfMonths < 1 || rental.rentLicense_noOfMonths > 1000 ||
                details.Count != rental.rentLicense_noOfMonths || details.Any(d => !d.rd_mNo.HasValue) ||
                details.Select(d => d.rd_mNo).Distinct().Count() != details.Count)
                throw new InvalidOperationException(error);
            foreach (var detail in details)
            {
                if (detail.rd_mNo < 1 || detail.rd_mNo > details.Count ||
                    detail.rd_fromDate != LicenseRentalRules.InstalmentStart(rental.rentLicense_from.Value, detail.rd_mNo.Value) ||
                    detail.rd_toDate != LicenseRentalRules.InstalmentEnd(rental.rentLicense_from.Value, detail.rd_mNo.Value) ||
                    LicenseRentalRules.Amount(detail.rd_amount) <= 0 || !detail.rd_isPaid.HasValue)
                    throw new InvalidOperationException(error);
                if (detail.rd_isPaid == false && (detail.rentLicPay_VouNo != null || detail.rentLicPay_PaymentDate.HasValue ||
                    detail.rentLicPay_mandoubID.HasValue || detail.payment_added_Date.HasValue || detail.payment_edited_Date.HasValue ||
                    detail.payment_addedBy.HasValue || detail.payment_editedBy.HasValue || detail.legacyPaymentID.HasValue))
                    throw new InvalidOperationException(error);
            }
            return new Ledger { Rental = rental, Details = details };
        }

        private static DateTime NextEdit(DateTime? previous)
        {
            // SQL datetime rounds to roughly 3 ms. Keep optimistic edit tokens distinct even for rapid saves.
            DateTime now = DateTime.Now;
            return previous.HasValue && now <= previous.Value.AddMilliseconds(10) ? previous.Value.AddMilliseconds(10) : now;
        }

        private static void Touch(RentLicens value)
        {
            value.edited_Date = NextEdit(value.edited_Date);
            value.editedBy = MainClass.userID;
        }

        private static void Touch(RentLicensesDetail value)
        {
            value.edited_Date = NextEdit(value.edited_Date);
            value.editedBy = MainClass.userID;
        }

        private static RentLicensesDetail NewDetail(int rentalId, DateTime start, int month, decimal amount)
        {
            return new RentLicensesDetail {
                rentLicenseID = rentalId, rd_mNo = month,
                rd_fromDate = LicenseRentalRules.InstalmentStart(start, month),
                rd_toDate = LicenseRentalRules.InstalmentEnd(start, month),
                rd_amount = LicenseRentalRules.ToStorageAmount(amount), rd_isPaid = false,
                added_Date = DateTime.Now, edited_Date = DateTime.Now,
                addedBy = MainClass.userID, editedBy = MainClass.userID
            };
        }

        internal int SaveRental(int rentalId, int companyLicenceId, int customerId, string number,
            DateTime start, int months, int seats, decimal initialAmount, DateTime? expectedEdit)
        {
            LicenseRentalRules.ValidateText(number);
            DateTime end = LicenseRentalRules.InstalmentEnd(start, months);
            if (seats < 0 || customerId <= 0 || companyLicenceId <= 0 || start.Date < new DateTime(1753, 1, 1))
                throw new InvalidOperationException("يرجى اختيار الرخصة والعميل وإدخال بيانات عقد صحيحة");
            return Write(db => {
                RequirePermission(db, "pageRentAlicences", rentalId <= 0 ? "add" : "edit");
                var licence = db.CompanyLicences.FirstOrDefault(c => c.companyLicencesID == companyLicenceId);
                if (licence == null || licence.isActive != true || licence.isMainLicences != false ||
                    !db.Customers.Any(c => c.customerID == customerId))
                    throw new InvalidOperationException("يرجى اختيار رخصة متاحة وعميل صحيح");
                if (db.RentLicenses.Any(r => r.companyLicencesID == companyLicenceId && r.rentLicense_active == true && r.rentLicenseID != rentalId))
                    throw new InvalidOperationException("الرخصة مؤجرة بالفعل");
                RentLicens rental;
                List<RentLicensesDetail> details;
                if (rentalId <= 0)
                {
                    if (initialAmount <= 0) throw new InvalidOperationException("قيمة الإيجار يجب أن تكون أكبر من صفر");
                    rental = new RentLicens { customerID = customerId, companyLicencesID = companyLicenceId,
                        rentLicense_from = start.Date, rentLicense_RentalAmount = LicenseRentalRules.ToStorageAmount(initialAmount),
                        rentLicense_active = true, rentLicense_date = DateTime.Now, added_Date = DateTime.Now, addedBy = MainClass.userID };
                    db.RentLicenses.Add(rental);
                    db.SaveChanges();
                    details = new List<RentLicensesDetail>();
                }
                else
                {
                    var ledger = LoadLedger(db, rentalId);
                    rental = ledger.Rental;
                    details = ledger.Details;
                    if (rental.edited_Date != expectedEdit) throw new RentalChangedException();
                    if (rental.rentLicense_from.Value.Date != start.Date)
                        throw new InvalidOperationException("لا يمكن تغيير تاريخ بداية العقد");
                    if (ledger.HasPayments && (rental.companyLicencesID != companyLicenceId || rental.customerID != customerId))
                        throw new InvalidOperationException("لا يمكن تغيير الرخصة أو العميل بعد تسجيل المدفوعات");
                    foreach (var detail in details.Where(d => d.rd_mNo > months).ToList())
                    {
                        if (detail.rd_isPaid == true)
                            throw new InvalidOperationException("لا يمكن حذف أشهر مسددة من العقد");
                        db.RentLicensesDetails.Remove(detail);
                        details.Remove(detail);
                    }
                }
                decimal rate = details.Count == 0 ? initialAmount : LicenseRentalRules.Amount(details.OrderBy(d => d.rd_mNo).Last().rd_amount);
                for (int month = details.Count + 1; month <= months; month++)
                {
                    var detail = NewDetail(rental.rentLicenseID, start, month, rate);
                    db.RentLicensesDetails.Add(detail);
                    details.Add(detail);
                }
                rental.customerID = customerId;
                rental.companyLicencesID = companyLicenceId;
                rental.rentLicenseNo = number;
                rental.rentLicense_noOfMonths = months;
                rental.rentLicense_NoOfSeats = seats;
                rental.rentLicense_to = end;
                // The legacy REAL summary is approximate; authoritative totals are calculated from the schedule in decimal.
                rental.rentLicense_DamanAmount = (float)details.Sum(d => LicenseRentalRules.Amount(d.rd_amount));
                rental.rentLicense_RentalAmount = details.OrderBy(d => d.rd_mNo).Last().rd_amount;
                Touch(rental);
                return rental.rentLicenseID;
            });
        }

        internal void ChangeRate(int rentalId, DateTime from, decimal rate, DateTime? expectedEdit)
        {
            if (rate <= 0) throw new InvalidOperationException("قيمة الإيجار يجب أن تكون أكبر من صفر");
            Write(db => {
                RequirePermission(db, "frmChangeRentingAmount", "edit");
                var ledger = LoadLedger(db, rentalId);
                if (ledger.Rental.edited_Date != expectedEdit) throw new RentalChangedException();
                var affected = ledger.Details.Where(d => d.rd_isPaid == false && d.rd_fromDate >= from.Date).ToList();
                if (affected.Count == 0) throw new InvalidOperationException("لا توجد أقساط غير مسددة تبدأ في التاريخ المحدد أو بعده");
                foreach (var detail in affected) { detail.rd_amount = LicenseRentalRules.ToStorageAmount(rate); Touch(detail); }
                ledger.Rental.rentLicense_RentalAmount = ledger.Details.OrderBy(d => d.rd_mNo).Last().rd_amount;
                ledger.Rental.rentLicense_DamanAmount = (float)ledger.Details.Sum(d => LicenseRentalRules.Amount(d.rd_amount));
                Touch(ledger.Rental);
                return true;
            });
        }

        internal void DeleteRental(int rentalId)
        {
            Write(db => {
                RequirePermission(db, "pageRentAlicences", "delete");
                var ledger = LoadLedger(db, rentalId);
                if (ledger.HasPayments) throw new InvalidOperationException("لا يمكن حذف عقد له مدفوعات");
                foreach (var detail in ledger.Details) db.RentLicensesDetails.Remove(detail);
                db.RentLicenses.Remove(ledger.Rental);
                return true;
            });
        }

        internal void CloseRental(int rentalId)
        {
            Write(db => {
                RequirePermission(db, "pageRentAlicences", "edit");
                var ledger = LoadLedger(db, rentalId);
                if (ledger.Details.Any(d => d.rd_isPaid != true))
                    throw new InvalidOperationException("يجب سداد جميع الأقساط قبل إنهاء العقد");
                ledger.Rental.rentLicense_active = false;
                Touch(ledger.Rental);
                return true;
            });
        }

        internal int SavePayment(int paymentId, int rentalId, int detailId, decimal displayedAmount,
            string voucher, DateTime paymentDate, int collectorId, DateTime? expectedEdit)
        {
            LicenseRentalRules.ValidateText(voucher);
            if (paymentDate.Date < new DateTime(1753, 1, 1)) throw new InvalidOperationException("تاريخ الدفع غير صحيح");
            return Write(db => {
                RequirePermission(db, "frmRentLicensePayment", paymentId <= 0 ? "add" : "edit");
                var ledger = LoadLedger(db, rentalId);
                if (!db.Employees.Any(e => e.employeeID == collectorId && e.emp_isMandoub == true))
                    throw new InvalidOperationException("يرجى اختيار مندوب صحيح");
                var detail = ledger.Details.FirstOrDefault(d => d.rentLicencesDetailsID == detailId);
                if (detail == null || LicenseRentalRules.Amount(detail.rd_amount) != displayedAmount)
                    throw new RentalChangedException();
                if (paymentId <= 0)
                {
                    var next = ledger.Details.FirstOrDefault(d => d.rd_isPaid == false);
                    if (detail.rd_isPaid != false || next == null || next.rentLicencesDetailsID != detailId)
                        throw new RentalChangedException();
                    detail.payment_added_Date = DateTime.Now;
                    detail.payment_addedBy = MainClass.userID;
                }
                else if (paymentId != detailId || detail.rd_isPaid != true || detail.payment_edited_Date != expectedEdit)
                    throw new RentalChangedException();
                detail.rentLicPay_VouNo = voucher;
                detail.rentLicPay_PaymentDate = paymentDate.Date;
                detail.rentLicPay_mandoubID = collectorId;
                // The installment edit token survives receipt deletion, so a rapid re-payment cannot reuse an old receipt token.
                DateTime? previousEdit = detail.payment_edited_Date.HasValue &&
                    (!detail.edited_Date.HasValue || detail.payment_edited_Date > detail.edited_Date)
                    ? detail.payment_edited_Date : detail.edited_Date;
                detail.payment_edited_Date = NextEdit(previousEdit);
                detail.payment_editedBy = MainClass.userID;
                detail.rd_isPaid = true;
                Touch(detail);
                Touch(ledger.Rental);
                db.SaveChanges();
                return detail.rentLicencesDetailsID;
            });
        }

        internal void DeletePayment(int paymentId, int rentalId, DateTime? expectedEdit)
        {
            Write(db => {
                RequirePermission(db, "frmRentLicensePayment", "delete");
                var ledger = LoadLedger(db, rentalId);
                var detail = ledger.Details.FirstOrDefault(d => d.rentLicencesDetailsID == paymentId && d.rd_isPaid == true);
                if (detail == null || detail.payment_edited_Date != expectedEdit) throw new RentalChangedException();
                detail.rentLicPay_VouNo = null;
                detail.rentLicPay_PaymentDate = null;
                detail.rentLicPay_mandoubID = null;
                detail.payment_added_Date = null;
                detail.payment_edited_Date = null;
                detail.payment_addedBy = null;
                detail.payment_editedBy = null;
                detail.legacyPaymentID = null;
                detail.rd_isPaid = false;
                Touch(detail);
                Touch(ledger.Rental);
                return true;
            });
        }
    }
}
