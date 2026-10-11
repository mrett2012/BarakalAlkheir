using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CompanyLicenseDesignation
    {
        public int companyLicencesDesignationID { get; set; }
        public int? companyLicencesID { get; set; }
        public int? designationID { get; set; }
        public int? noOfSeats { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CompanyLicence CompanyLicence { get; set; }
        public virtual Designation Designation { get; set; }
    }
}
