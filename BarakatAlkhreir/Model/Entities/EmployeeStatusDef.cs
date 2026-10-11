using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EmployeeStatusDef
    {
        public EmployeeStatusDef()
        {
            Employees = new HashSet<Employee>();
        }

        public int employeeStatusID { get; set; }
        public string employeeStatus_Code { get; set; }
        public string employeeStatus_namea { get; set; }
        public string employeeStatus_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
