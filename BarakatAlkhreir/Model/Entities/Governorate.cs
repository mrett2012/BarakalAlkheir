using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Governorate
    {
        public Governorate()
        {
            GovernorateAreas = new HashSet<GovernorateArea>();
        }

        public int governorateID { get; set; }
        public string gover_Code { get; set; }
        public string gover_namea { get; set; }
        public string gover_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<GovernorateArea> GovernorateAreas { get; set; }
    }
}
