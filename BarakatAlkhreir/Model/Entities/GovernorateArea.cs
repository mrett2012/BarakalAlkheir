using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class GovernorateArea
    {
        public int goverareaID { get; set; }
        public int governorateID { get; set; }
        public string goverarea_Code { get; set; }
        public string goverarea_namea { get; set; }
        public string goverarea_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Governorate Governorate { get; set; }
    }
}
