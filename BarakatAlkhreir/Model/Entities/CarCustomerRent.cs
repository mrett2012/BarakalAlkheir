using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CarCustomerRent
    {
        public CarCustomerRent()
        {
            carCustomerRentDetails = new HashSet<carCustomerRentDetail>();
            CarCustomerRentPayments = new HashSet<CarCustomerRentPayment>();
        }

        public int carCustomerRentID { get; set; }
        public int? carID { get; set; }
        public int? customerID { get; set; }
        public string carCustomer_rentNo { get; set; }
        public DateTime? carCustomer_Date { get; set; }
        public DateTime? carCustomer_from { get; set; }
        public int? carCustomer_noOfMonths { get; set; }
        public DateTime? carCustomer_to { get; set; }
        public float? carCustomerRent_RentalAmount { get; set; }
        public float? carCustomerRent_DamanAmount { get; set; }
        public int? carCustomerRent_DriverID { get; set; }
        public string carCustomerRent_DriverName { get; set; }
        public string carCustomerRent_Remarks { get; set; }
        public bool? carCustomerRent_active { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<carCustomerRentDetail> carCustomerRentDetails { get; set; }
        public virtual ICollection<CarCustomerRentPayment> CarCustomerRentPayments { get; set; }
    }
}
