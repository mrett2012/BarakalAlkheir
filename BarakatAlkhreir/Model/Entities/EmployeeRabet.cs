using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeeRabet
    {
        public int rabetID { get; set; }
        public string rabet_namea { get; set; }
        public string rabet_description { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }
    }
}
