using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Designation
    {
        public Designation()
        {
            CompanyLicenseDesignations = new HashSet<CompanyLicenseDesignation>();
            EmployeeSponsors = new HashSet<EmployeeSponsor>();
        }

        public int designationID { get; set; }
        public string desig_Code { get; set; }
        public string desig_namea { get; set; }
        public string desig_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<CompanyLicenseDesignation> CompanyLicenseDesignations { get; set; }
        public virtual ICollection<EmployeeSponsor> EmployeeSponsors { get; set; }
    }
}
