using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Company
    {
        public Company()
        {
            CompanyAuthorizations = new HashSet<CompanyAuthorization>();
            CompanyLicences = new HashSet<CompanyLicence>();
            EmployeeSponsors = new HashSet<EmployeeSponsor>();
        }

        public int companyID { get; set; }
        public string comp_nam1 { get; set; }
        public string comp_nam2 { get; set; }
        public string comp_nam3 { get; set; }
        public string comp_nam4 { get; set; }
        public string comp_nam5 { get; set; }
        public string comp_name1 { get; set; }
        public string comp_name2 { get; set; }
        public string comp_name3 { get; set; }
        public string comp_name4 { get; set; }
        public string comp_name5 { get; set; }
        public string comp_arabicName { get; set; }
        public string comp_englishName { get; set; }
        public string comp_fileNo { get; set; }
        public string comp_uniqueNo { get; set; }
        public string comp_ownerName { get; set; }
        public string comp_civilId { get; set; }
        public string comp_nationality { get; set; }
        public string comp_nationalityNo { get; set; }
        public string comp_Salary { get; set; }
        public string comp_Designation { get; set; }
        public string comp_contactNo { get; set; }
        public string comp_remarks { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<CompanyAuthorization> CompanyAuthorizations { get; set; }
        public virtual ICollection<CompanyLicence> CompanyLicences { get; set; }
        public virtual ICollection<EmployeeSponsor> EmployeeSponsors { get; set; }
    }
}
