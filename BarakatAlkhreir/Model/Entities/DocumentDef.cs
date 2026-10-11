using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumentDef
    {
        public DocumentDef()
        {
            Documents = new HashSet<Document>();
            DocumentTransactions = new HashSet<DocumentTransaction>();
        }

        public int documentDefID { get; set; }
        public string documentDefCode { get; set; }
        public int? documentTypeDefID { get; set; }
        public string documentDef_Name { get; set; }
        public int? documentDef_WarningDays { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentTypeDefinition DocumentTypeDefinition { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentTransaction> DocumentTransactions { get; set; }
    }
}
