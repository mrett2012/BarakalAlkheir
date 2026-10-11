using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class RentShopPayment
    {
        public int rentLicensePaymentID { get; set; }
        public int? rentShopID { get; set; }
        public string rentShopPay_VouNo { get; set; }
        public float? rentShopPay_Amount { get; set; }
        public DateTime? rentShopPay_PaymentDate { get; set; }
        public DateTime? rentShopPay_ForDate { get; set; }
        public int? rentShopPay_mandoubID { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual RentShop RentShop { get; set; }
    }
}
