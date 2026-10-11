using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeeLicenseShop
    {
        public int employeeLicShopID { get; set; }
        public int? licencesShopID { get; set; }
        public int? employeeID { get; set; }
        public string employeeLink { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }
        public int? carID { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LicensesShop LicensesShop { get; set; }
    }
}
