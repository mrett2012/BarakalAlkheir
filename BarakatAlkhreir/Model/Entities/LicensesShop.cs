using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class LicensesShop
    {
        public LicensesShop()
        {
            EmployeeLicenseShops = new HashSet<EmployeeLicenseShop>();
            RentShops = new HashSet<RentShop>();
        }

        public int licencesShopID { get; set; }
        public int? companyLicencesID { get; set; }
        public string shop_Code { get; set; }
        public string shop_Name { get; set; }
        public string shop_contact1 { get; set; }
        public string shop_contact2 { get; set; }
        public bool? shop_isCurrent { get; set; }
        public int? governorateID { get; set; }
        public int? goverareaID { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CompanyLicence CompanyLicence { get; set; }
        public virtual ICollection<EmployeeLicenseShop> EmployeeLicenseShops { get; set; }
        public virtual ICollection<RentShop> RentShops { get; set; }
    }
}
