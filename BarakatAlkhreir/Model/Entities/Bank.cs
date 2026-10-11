using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Bank
    {
        public Bank()
        {
            EmployeeSalaries = new HashSet<EmployeeSalary>();
        }

        public int bankID { get; set; }
        public string bank_Code { get; set; }
        public string bank_namea { get; set; }
        public string bank_namee { get; set; }
        public string bank_accNo { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
