using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumentTransactionTask
    {
        public DocumentTransactionTask()
        {
            AssignDocumentTransactionTasks = new HashSet<AssignDocumentTransactionTask>();
            DocumentTransactionTasksPayments = new HashSet<DocumentTransactionTasksPayment>();
            documentTransTaskDetails = new HashSet<documentTransTaskDetail>();
        }

        public int documentTransTaskID { get; set; }
        public int? documentID { get; set; }
        public int? documentTransID { get; set; }
        public DateTime? documentTransTask_date { get; set; }
        public int? documentTransTask_NoOfDays { get; set; }
        public bool? documentTransTask_Status { get; set; }
        public string documentTransTask_Remarks { get; set; }
        public bool? documentTransTask_Approved { get; set; }
        public DateTime? documentTransTask_ApprovedDate { get; set; }
        public bool? documentTransTask_isPaid { get; set; }
        public bool? documentTransTask_isClose { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<AssignDocumentTransactionTask> AssignDocumentTransactionTasks { get; set; }
        public virtual Document Document { get; set; }
        public virtual DocumentTransaction DocumentTransaction { get; set; }
        public virtual ICollection<DocumentTransactionTasksPayment> DocumentTransactionTasksPayments { get; set; }
        public virtual ICollection<documentTransTaskDetail> documentTransTaskDetails { get; set; }
    }
}
