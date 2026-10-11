using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class DocumentTypeDefinition
    {
        public DocumentTypeDefinition()
        {
            DocumentDefs = new HashSet<DocumentDef>();
        }

        public int documentTypeDefID { get; set; }
        public string documentType_Code { get; set; }
        public string documentType_Name { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<DocumentDef> DocumentDefs { get; set; }
    }
}
