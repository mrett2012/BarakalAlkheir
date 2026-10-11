using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            UserGroupEvents = new HashSet<UserGroupEvent>();
            Users = new HashSet<User>();
        }

        public int userGroupID { get; set; }
        public string userGroup_Name { get; set; }
        public string userGroup_Remarks { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<UserGroupEvent> UserGroupEvents { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
