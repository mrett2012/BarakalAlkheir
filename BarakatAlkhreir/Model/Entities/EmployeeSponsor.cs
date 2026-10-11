using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeeSponsor
    {
        public int employeeSponsorID { get; set; }
        public int? employeeID { get; set; }
        public int? companyID { get; set; }
        public int? companyLicencesID { get; set; }
        public int? govermentContractID { get; set; }
        public int? designationID { get; set; }
        public string employeeSponsor_residencyNo { get; set; }
        public DateTime? employeeSponsor_residencyStartDate { get; set; }
        public DateTime? employeeSponsor_residencyEndDate { get; set; }
        public DateTime? employeeSponsor_entryDate { get; set; }
        public string employeeSponsor_salary { get; set; }
        public string employeeSponsor_useDuration { get; set; }
        public string employeeSponsor_WorkPermitNo { get; set; }
        public string employeeSponsor_Shou2oun { get; set; }
        public string employeeSponsor_Jawazat { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyLicence CompanyLicence { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
