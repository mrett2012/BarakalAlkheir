using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Customer
    {
        public Customer()
        {
            CarCustomerRents = new HashSet<CarCustomerRent>();
            RentLicenses = new HashSet<RentLicens>();
            RentShops = new HashSet<RentShop>();
        }

        public int customerID { get; set; }
        public string customer_Name { get; set; }
        public string customer_CivilID { get; set; }
        public int? customer_nationalityID { get; set; }
        public DateTime? customer_BirthDate { get; set; }
        public string customer_phoneNumber1 { get; set; }
        public string customer_phoneNumber2 { get; set; }
        public string customer_Address { get; set; }
        public bool? customer_active { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<CarCustomerRent> CarCustomerRents { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<RentLicens> RentLicenses { get; set; }
        public virtual ICollection<RentShop> RentShops { get; set; }
    }
}
