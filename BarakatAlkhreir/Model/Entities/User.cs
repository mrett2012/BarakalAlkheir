using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class User
    {
        public int userID { get; set; }
        public int? userGroupID { get; set; }
        public string displayName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool? isSuperUser { get; set; }
        public bool? isActive { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
