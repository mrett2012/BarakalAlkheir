using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Document
    {
        public Document()
        {
            DocumentTransactionTasks = new HashSet<DocumentTransactionTask>();
            DocumnetStores = new HashSet<DocumnetStore>();
        }

        public int documentID { get; set; }
        public int? documentDefID { get; set; }
        public DateTime? document_startDate { get; set; }
        public DateTime? document_endDate { get; set; }
        public string document_No { get; set; }
        public string document_Type { get; set; }
        public string document_placeOfIssue { get; set; }
        public string document_Status { get; set; }
        public string document_Remarks { get; set; }
        public int? document_TypeID { get; set; }
        public string document_text1 { get; set; }
        public string document_text2 { get; set; }
        public string document_text3 { get; set; }
        public string document_text4 { get; set; }
        public string document_text5 { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual DocumentDef DocumentDef { get; set; }
        public virtual ICollection<DocumentTransactionTask> DocumentTransactionTasks { get; set; }
        public virtual ICollection<DocumnetStore> DocumnetStores { get; set; }
    }
}
