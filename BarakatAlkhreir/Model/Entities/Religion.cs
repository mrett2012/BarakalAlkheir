using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Religion
    {
        public Religion()
        {
            Employees = new HashSet<Employee>();
        }

        public int religionID { get; set; }
        public string religion_Code { get; set; }
        public string religion_namea { get; set; }
        public string religion_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
