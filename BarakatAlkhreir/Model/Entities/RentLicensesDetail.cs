using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class RentLicensesDetail
    {
        public int rentLicencesDetailsID { get; set; }
        public int? rentLicenseID { get; set; }
        public DateTime? rd_fromDate { get; set; }
        public DateTime? rd_toDate { get; set; }
        public int? rd_mNo { get; set; }
        public float? rd_amount { get; set; }
        public bool? rd_isPaid { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        // Receipt metadata belongs to this installment; charge and billing period remain on the schedule.
        public string rentLicPay_VouNo { get; set; }
        public DateTime? rentLicPay_PaymentDate { get; set; }
        public int? rentLicPay_mandoubID { get; set; }
        public DateTime? payment_added_Date { get; set; }
        public DateTime? payment_edited_Date { get; set; }
        public int? payment_addedBy { get; set; }
        public int? payment_editedBy { get; set; }
        public int? legacyPaymentID { get; set; }

        public virtual RentLicens RentLicens { get; set; }
    }
}
