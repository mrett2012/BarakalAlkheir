using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class MaritalStatu
    {
        public MaritalStatu()
        {
            Employees = new HashSet<Employee>();
        }

        public int maritalStatusID { get; set; }
        public string marital_Code { get; set; }
        public string marital_namea { get; set; }
        public string marital_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
