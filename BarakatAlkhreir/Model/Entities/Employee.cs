using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Dependents = new HashSet<Dependent>();
            EmployeeLicenseShops = new HashSet<EmployeeLicenseShop>();
            EmployeePassports = new HashSet<EmployeePassport>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            EmployeeSponsors = new HashSet<EmployeeSponsor>();
            EmployeeWorkContracts = new HashSet<EmployeeWorkContract>();
        }

        public int employeeID { get; set; }
        public string emp_No { get; set; }
        public string emp_fileNo { get; set; }
        public string emp_civilID { get; set; }
        public string emp_namea1 { get; set; }
        public string emp_namea2 { get; set; }
        public string emp_namea3 { get; set; }
        public string emp_namea4 { get; set; }
        public string emp_namea5 { get; set; }
        public string emp_namee1 { get; set; }
        public string emp_namee2 { get; set; }
        public string emp_namee3 { get; set; }
        public string emp_namee4 { get; set; }
        public string emp_namee5 { get; set; }
        public string emp_namefullar { get; set; }
        public string emp_namefullen { get; set; }
        public string emp_day { get; set; }
        public string emp_month { get; set; }
        public string emp_year { get; set; }
        public int? nationality_placeOfBirthID { get; set; }
        public int? genderID { get; set; }
        public int? nationalityID { get; set; }
        public int? religionID { get; set; }
        public int? educationID { get; set; }
        public int? maritalID { get; set; }
        public int? departmentID { get; set; }
        public string emp_bloodType { get; set; }
        public bool? emp_active { get; set; }
        public bool? emp_isMandoub { get; set; }
        public int? employeeStatusID { get; set; }
        public int? companyID { get; set; }
        public string Mohafaza { get; set; }
        public string Quet3a { get; set; }
        public string Share3 { get; set; }
        public string Jada { get; set; }
        public string BuilduingType { get; set; }
        public string floor { get; set; }
        public string appartment { get; set; }
        public string pobox { get; set; }
        public string poboxOffice { get; set; }
        public string email { get; set; }
        public string homeNo { get; set; }
        public string phoneNo { get; set; }
        public string fulladdress { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public string emp_appDate { get; set; }
        public string emp_appType { get; set; }
        public string emp_res1 { get; set; }
        public string emp_res2 { get; set; }
        public string emp_res3 { get; set; }
        public string emp_res4 { get; set; }
        public string emp_res5 { get; set; }
        public string emp_res6 { get; set; }
        public string emp_res7 { get; set; }
        public string emp_res8 { get; set; }
        public string emp_res9 { get; set; }
        public string emp_res10 { get; set; }
        public string emp_res11 { get; set; }
        public string emp_res12 { get; set; }
        public string emp_res13 { get; set; }
        public string emp_res14 { get; set; }
        public string emp_res15 { get; set; }
        public string emp_res16 { get; set; }
        public string emp_res17 { get; set; }
        public string emp_res18 { get; set; }
        public string emp_res19 { get; set; }
        public string emp_res20 { get; set; }
        public string emp_res21 { get; set; }
        public string emp_res22 { get; set; }
        public string emp_res23 { get; set; }
        public string emp_res24 { get; set; }
        public string emp_res25 { get; set; }
        public string emp_res26 { get; set; }
        public string emp_res27 { get; set; }
        public string emp_res28 { get; set; }
        public string emp_type { get; set; }
        public string emp_OldSalary { get; set; }
        public string emp_OldNoOfYears { get; set; }
        public string emp_OldDesignation { get; set; }
        public string oldoladOwnerName { get; set; }
        public string oldoldAuthName { get; set; }
        public string oldoldCommercialName { get; set; }
        public string oldoldFileNo { get; set; }
        public string oldoldLicenceNO { get; set; }
        public string oldoldGoveContract { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<EmployeeLicenseShop> EmployeeLicenseShops { get; set; }
        public virtual ICollection<EmployeePassport> EmployeePassports { get; set; }
        public virtual EmployeeStatusDef EmployeeStatusDef { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatu MaritalStatu { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Nationality Nationality1 { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual ICollection<EmployeeSponsor> EmployeeSponsors { get; set; }
        public virtual ICollection<EmployeeWorkContract> EmployeeWorkContracts { get; set; }
    }
}
