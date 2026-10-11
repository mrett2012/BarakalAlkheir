using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class documentTransTaskDetail
    {
        public int documentTransTaskDetailsID { get; set; }
        public int? documentTransTaskID { get; set; }
        public string documentTransSetp_name { get; set; }
        public int? documentTransSetp_Period { get; set; }
        public DateTime? documentTransSetp_startDate { get; set; }
        public DateTime? documentTransSetp_endDate { get; set; }
        public string documentTransSetp_pCode { get; set; }
        public int? employeeID { get; set; }
        public bool? documentTransSetp_isClose { get; set; }
        public string documentTransSetp_Remarks { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentTransactionTask DocumentTransactionTask { get; set; }
    }
}
