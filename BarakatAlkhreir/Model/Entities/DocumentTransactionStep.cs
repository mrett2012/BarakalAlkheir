using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumentTransactionStep
    {
        public int documentTransSetpID { get; set; }
        public string documentTransSetp_pCode { get; set; }
        public int? documentTransID { get; set; }
        public string documentTransSetp_name { get; set; }
        public int? documentTransSetp_Period { get; set; }
        public int? documentTransSetp_Order { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentTransaction DocumentTransaction { get; set; }
    }
}
