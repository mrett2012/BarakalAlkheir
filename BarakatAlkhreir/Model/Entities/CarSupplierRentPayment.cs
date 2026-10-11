using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CarSupplierRentPayment
    {
        public int carSupplierRentPaymentID { get; set; }
        public int? carSupplierRentID { get; set; }
        public int? carSupplierRentDetailsID { get; set; }
        public int? carSupplierRentPayment_mandoubID { get; set; }
        public DateTime? carSupplierRentPayment_ForDate { get; set; }
        public float? carSupplierRentPayment_amount { get; set; }
        public string carSupplierRentPayment_VoucherNo { get; set; }
        public DateTime? carSupplierRentPayment_PayDate { get; set; }
        public string carSupplierRentPayment_Remarks { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CarSupplierRent CarSupplierRent { get; set; }
    }
}
