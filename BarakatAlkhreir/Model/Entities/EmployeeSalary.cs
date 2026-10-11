using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeeSalary
    {
        public int employeeSalaryID { get; set; }
        public int? employeeID { get; set; }
        public int? bankID { get; set; }
        public string accountNo { get; set; }
        public string branchName { get; set; }
        public float? paidFromSalary { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
