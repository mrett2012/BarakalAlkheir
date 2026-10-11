using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeeWorkContract
    {
        public int workContractID { get; set; }
        public int? employeeID { get; set; }
        public DateTime? wc_date { get; set; }
        public DateTime? wc_startingContract { get; set; }
        public string wc_durationType { get; set; }
        public string wc_duration { get; set; }
        public string wc_payEvery { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
