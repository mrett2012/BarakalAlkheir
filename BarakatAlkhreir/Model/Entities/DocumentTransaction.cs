using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumentTransaction
    {
        public DocumentTransaction()
        {
            DocumentTransactionSteps = new HashSet<DocumentTransactionStep>();
            DocumentTransactionTasks = new HashSet<DocumentTransactionTask>();
        }

        public int documentTransID { get; set; }
        public int? documentDefID { get; set; }
        public int? transDefID { get; set; }
        public float? documentTrans_Amount { get; set; }
        public string documentTrans_Description { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentDef DocumentDef { get; set; }
        public virtual TransactionDef TransactionDef { get; set; }
        public virtual ICollection<DocumentTransactionStep> DocumentTransactionSteps { get; set; }
        public virtual ICollection<DocumentTransactionTask> DocumentTransactionTasks { get; set; }
    }
}
