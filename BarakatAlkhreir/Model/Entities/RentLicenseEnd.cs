using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class RentLicenseEnd
    {
        public int rentLicenseEndID { get; set; }
        public int? rentLicenseID { get; set; }
        public DateTime? rentLicEnd_Date { get; set; }
        public string rentLicEnd_reasons { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual RentLicens RentLicens { get; set; }
    }
}
