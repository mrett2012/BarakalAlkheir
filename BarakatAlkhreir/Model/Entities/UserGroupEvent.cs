using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class UserGroupEvent
    {
        public int userGroupEventID { get; set; }
        public int? userGroupID { get; set; }
        public int? formEventID { get; set; }
        public bool? isEnable { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual FormEvent FormEvent { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
