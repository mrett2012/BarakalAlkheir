using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Education
    {
        public Education()
        {
            Employees = new HashSet<Employee>();
        }

        public int educationID { get; set; }
        public string edu_Code { get; set; }
        public string edu_namea { get; set; }
        public string edu_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
