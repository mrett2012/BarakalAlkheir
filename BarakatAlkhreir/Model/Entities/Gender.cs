using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Gender
    {
        public Gender()
        {
            Employees = new HashSet<Employee>();
        }

        public int genderID { get; set; }
        public string gender_Code { get; set; }
        public string gender_namea { get; set; }
        public string gender_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
