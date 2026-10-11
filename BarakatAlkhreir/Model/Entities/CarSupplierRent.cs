using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CarSupplierRent
    {
        public CarSupplierRent()
        {
            CarSupplierRentDetails = new HashSet<CarSupplierRentDetail>();
            CarSupplierRentPayments = new HashSet<CarSupplierRentPayment>();
        }

        public int carSupplierRentID { get; set; }
        public int? carID { get; set; }
        public int? carSuppliserID { get; set; }
        public string carRentNo { get; set; }
        public DateTime? carSupplierRent_Date { get; set; }
        public DateTime? carSupplierRent_from { get; set; }
        public DateTime? carSupplierRent_to { get; set; }
        public float? carSupplierRent_totalAmount { get; set; }
        public float? carSupplierRent_MonthlyAmount { get; set; }
        public int? carSupplierRent_noOfMonths { get; set; }
        public DateTime? carSupplierRent_firstPaymentDate { get; set; }
        public float? carSupplierRent_firstPaymentAmount { get; set; }
        public float? carSupplierRent_lastPaymentAmount { get; set; }
        public bool? carSupplierRent_isRented { get; set; }
        public bool? carSupplierRent_isBuyed { get; set; }
        public bool? carSupplierRent_isactive { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Car Car { get; set; }
        public virtual CarSupllier CarSupllier { get; set; }
        public virtual ICollection<CarSupplierRentDetail> CarSupplierRentDetails { get; set; }
        public virtual ICollection<CarSupplierRentPayment> CarSupplierRentPayments { get; set; }
    }
}
