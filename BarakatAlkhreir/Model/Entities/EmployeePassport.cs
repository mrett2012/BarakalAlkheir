using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeePassport
    {
        public int passportID { get; set; }
        public int? employeeID { get; set; }
        public string pass_no { get; set; }
        public DateTime? pass_issueDate { get; set; }
        public DateTime? pass_expiryDate { get; set; }
        public string pass_placeOfIssue { get; set; }
        public string pass_type { get; set; }
        public bool? pass_InUsed { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
