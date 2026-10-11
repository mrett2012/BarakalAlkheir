using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumnetStore
    {
        public int documentStoreID { get; set; }
        public int? documentID { get; set; }
        public byte[] documentStore_doc { get; set; }
        public DateTime? documentStore_Date { get; set; }
        public string documentStore_Description { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Document Document { get; set; }
    }
}
