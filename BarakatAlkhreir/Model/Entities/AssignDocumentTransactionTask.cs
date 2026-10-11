using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class AssignDocumentTransactionTask
    {
        public int assignDocTransTaskID { get; set; }
        public int? employeeID { get; set; }
        public int? documentTransTaskID { get; set; }
        public string assignDocTransTask_Name { get; set; }
        public DateTime? assignDocTransTask_date { get; set; }
        public int? assignDocTransTask_noOfDays { get; set; }
        public string assignDocTransTask_Remarks { get; set; }
        public DateTime? assignDocTransTask_FinishDate { get; set; }
        public bool? assignDocTransTask_IsClose { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentTransactionTask DocumentTransactionTask { get; set; }
    }
}
