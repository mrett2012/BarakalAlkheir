using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class RentLicens
    {
        public RentLicens()
        {
            RentLicenseEnds = new HashSet<RentLicenseEnd>();
            RentLicensesDetails = new HashSet<RentLicensesDetail>();
        }

        public int rentLicenseID { get; set; }
        public string rentLicenseNo { get; set; }
        public int? customerID { get; set; }
        public int? companyLicencesID { get; set; }
        public int? licencesShopID { get; set; }
        public DateTime? rentLicense_date { get; set; }
        public int? rentLicense_noOfMonths { get; set; }
        public DateTime? rentLicense_from { get; set; }
        public DateTime? rentLicense_to { get; set; }
        public float? rentLicense_RentalAmount { get; set; }
        public float? rentLicense_DamanAmount { get; set; }
        public bool? rentLicense_active { get; set; }
        public int? rentLicense_NoOfSeats { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CompanyLicence CompanyLicence { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<RentLicenseEnd> RentLicenseEnds { get; set; }
        public virtual ICollection<RentLicensesDetail> RentLicensesDetails { get; set; }
    }
}
