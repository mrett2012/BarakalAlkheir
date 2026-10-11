using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CompanyLicencesEnd
    {
        public int companyLicencesEndID { get; set; }
        public int companyLicencesID { get; set; }
        public DateTime? companyLicencesEnd_Date { get; set; }
        public string companyLicencesEnd_reasons { get; set; }
        public bool? companyLicencesEnd_isActive { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CompanyLicence CompanyLicence { get; set; }
    }
}
