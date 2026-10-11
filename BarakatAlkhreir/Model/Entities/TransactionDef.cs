using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class TransactionDef
    {
        public TransactionDef()
        {
            DocumentTransactions = new HashSet<DocumentTransaction>();
        }

        public int transDefID { get; set; }
        public string transDef_code { get; set; }
        public string transDef_namea { get; set; }
        public string transDef_namee { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<DocumentTransaction> DocumentTransactions { get; set; }
    }
}
