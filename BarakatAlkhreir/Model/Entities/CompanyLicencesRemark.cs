using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CompanyLicencesRemark
    {
        public int comLicenseRemID { get; set; }
        public int? companyLicencesID { get; set; }
        public DateTime? comLicenseRem_Date { get; set; }
        public string comLicenseRem_Remarks { get; set; }
        public string comLicenseRem_Action { get; set; }
        public bool? comLicenseRem_isActive { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CompanyLicence CompanyLicence { get; set; }
    }
}
