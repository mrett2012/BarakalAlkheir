using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class z_Residency
    {
        public int residencyID { get; set; }
        public int? employeeID { get; set; }
        public int? designationID { get; set; }
        public string residency_No { get; set; }
        public DateTime? residency_startDate { get; set; }
        public DateTime? residency_endDate { get; set; }
        public string residency_type { get; set; }
        public string residency_entryDate { get; set; }
        public string residency_salary { get; set; }
        public string residency_useDuration { get; set; }
        public string residency_WorkPermitNo { get; set; }
        public string residency_Shou2oun { get; set; }
        public string residency_Jawazat { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }
    }
}
