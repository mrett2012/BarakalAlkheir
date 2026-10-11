using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CarCustomerRentPayment
    {
        public int carCustomerRentPaymentID { get; set; }
        public int? carCustomerRentID { get; set; }
        public int? carCustomerRentDetailsID { get; set; }
        public DateTime? carCustomerPayment_ForDate { get; set; }
        public float? carCustomerPayment_amount { get; set; }
        public string carCustomerPayment_VoucherNo { get; set; }
        public DateTime? carCustomerPayment_PaymentDate { get; set; }
        public string carCustomerPayment_Remarks { get; set; }
        public int? carCustomerPayment_mandoubID { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CarCustomerRent CarCustomerRent { get; set; }
    }
}
