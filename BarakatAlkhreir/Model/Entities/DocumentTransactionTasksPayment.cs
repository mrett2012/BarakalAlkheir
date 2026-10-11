using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumentTransactionTasksPayment
    {
        public int DocumentTransactionTasksPaymentID { get; set; }
        public int? documentTransTaskID { get; set; }
        public string voucherNo { get; set; }
        public float? voucherAmount { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentTransactionTask DocumentTransactionTask { get; set; }
    }
}
