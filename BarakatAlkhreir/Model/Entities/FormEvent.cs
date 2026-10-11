using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class FormEvent
    {
        public FormEvent()
        {
            UserGroupEvents = new HashSet<UserGroupEvent>();
        }

        public int formEventID { get; set; }
        public int? formID { get; set; }
        public int? eventID { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual EventsDefinition EventsDefinition { get; set; }
        public virtual FormScrean FormScrean { get; set; }
        public virtual ICollection<UserGroupEvent> UserGroupEvents { get; set; }
    }
}
