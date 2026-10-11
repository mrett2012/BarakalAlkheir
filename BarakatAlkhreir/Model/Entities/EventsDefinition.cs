using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class EventsDefinition
    {
        public EventsDefinition()
        {
            FormEvents = new HashSet<FormEvent>();
        }

        public int eventID { get; set; }
        public string event_Name { get; set; }
        public string event_DisplayName { get; set; }
        public string event_Remarks { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<FormEvent> FormEvents { get; set; }
    }
}
