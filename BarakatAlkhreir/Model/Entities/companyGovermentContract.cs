using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class companyGovermentContract
    {
        public int govermentContractID { get; set; }
        public int? companyLicencesID { get; set; }
        public string gov_contractNo { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CompanyLicence CompanyLicence { get; set; }
    }
}
