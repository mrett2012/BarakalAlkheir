using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class FormScrean
    {
        public FormScrean()
        {
            FormEvents = new HashSet<FormEvent>();
        }

        public int formID { get; set; }
        public string form_IDName { get; set; }
        public string form_DisplayName { get; set; }
        public string form_Descriptions { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<FormEvent> FormEvents { get; set; }
    }
}
