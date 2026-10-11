using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class AuthorizationPerson
    {
        public AuthorizationPerson()
        {
            CompanyAuthorizations = new HashSet<CompanyAuthorization>();
        }

        public int authorizationID { get; set; }
        public string auth_namea1 { get; set; }
        public string auth_namea2 { get; set; }
        public string auth_namea3 { get; set; }
        public string auth_namea4 { get; set; }
        public string auth_namea5 { get; set; }
        public string auth_namee1 { get; set; }
        public string auth_namee2 { get; set; }
        public string auth_namee3 { get; set; }
        public string auth_namee4 { get; set; }
        public string auth_namee5 { get; set; }
        public string fauth_ullArabicName { get; set; }
        public string fauth_ullEnglishName { get; set; }
        public string auth_CivilID { get; set; }
        public string auth_Designation { get; set; }
        public string auth_ContactNo { get; set; }
        public string auth_remarks { get; set; }
        public bool? auth_isCurrent { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<CompanyAuthorization> CompanyAuthorizations { get; set; }
    }
}
