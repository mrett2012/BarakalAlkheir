using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Dependent
    {
        public int dependentsID { get; set; }
        public int? employeeID { get; set; }
        public string depend_aName { get; set; }
        public string depend_eName { get; set; }
        public string depend_relation { get; set; }
        public string depend_birthday { get; set; }
        public string depend_gender { get; set; }
        public string depend_nationality { get; set; }
        public string depend_religion { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
