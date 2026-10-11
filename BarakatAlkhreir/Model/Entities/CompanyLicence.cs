using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CompanyLicence
    {
        public CompanyLicence()
        {
            companyGovermentContracts = new HashSet<companyGovermentContract>();
            CompanyLicencesEnds = new HashSet<CompanyLicencesEnd>();
            CompanyLicencesRemarks = new HashSet<CompanyLicencesRemark>();
            CompanyLicenseDesignations = new HashSet<CompanyLicenseDesignation>();
            EmployeeSponsors = new HashSet<EmployeeSponsor>();
            LicensesShops = new HashSet<LicensesShop>();
            RentLicenses = new HashSet<RentLicens>();
        }

        public int companyLicencesID { get; set; }
        public int? companyID { get; set; }
        public string licenseNo { get; set; }
        public string licenseAutomatedNumber { get; set; }
        public string licenseUniqueNo { get; set; }
        public string licenseLinkCode { get; set; }
        public string commercialName { get; set; }
        public string licensecivilID { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? noOfSeats { get; set; }
        public bool? isMainLicences { get; set; }
        public bool? isActive { get; set; }
        public bool? isClose { get; set; }
        public bool? isFack { get; set; }
        public int? mandoubID { get; set; }
        public int? governorateID { get; set; }
        public string Mohafaza { get; set; }
        public int? goverareaID { get; set; }
        public string Mantaqa { get; set; }
        public string Quet3a { get; set; }
        public string Share3 { get; set; }
        public string BuilduingType { get; set; }
        public string floor { get; set; }
        public string appartment { get; set; }
        public string pobox { get; set; }
        public string poboxOffice { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string fulladdress { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }
        public int? licenseActivityID { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<companyGovermentContract> companyGovermentContracts { get; set; }
        public virtual ICollection<CompanyLicencesEnd> CompanyLicencesEnds { get; set; }
        public virtual ICollection<CompanyLicencesRemark> CompanyLicencesRemarks { get; set; }
        public virtual ICollection<CompanyLicenseDesignation> CompanyLicenseDesignations { get; set; }
        public virtual ICollection<EmployeeSponsor> EmployeeSponsors { get; set; }
        public virtual ICollection<LicensesShop> LicensesShops { get; set; }
        public virtual ICollection<RentLicens> RentLicenses { get; set; }
    }
}
