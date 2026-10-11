using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CompanyAuthorization
    {
        public int companyAuthorizationID { get; set; }
        public int? companyID { get; set; }
        public int? authorizationID { get; set; }
        public bool? auth_isCurrent { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual AuthorizationPerson AuthorizationPerson { get; set; }
        public virtual Company Company { get; set; }
    }
}
