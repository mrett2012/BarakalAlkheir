using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Nationality
    {
        public Nationality()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Employees1 = new HashSet<Employee>();
        }

        public int nationalityID { get; set; }
        public string natio_Code { get; set; }
        public string natio_namea { get; set; }
        public string natio_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Employee> Employees1 { get; set; }
    }
}
