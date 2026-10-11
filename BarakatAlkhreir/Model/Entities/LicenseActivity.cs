using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class LicenseActivity
    {
        public int licenseActivityID { get; set; }
        public string Activity_namea { get; set; }
        public string Activity_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }
    }
}
