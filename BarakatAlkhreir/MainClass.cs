using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using BarakalAlkheir.Definitions;
using BarakalAlkheir.Sponsor;
using BarakalAlkheir.emp;
using BarakalAlkheir.Model;


namespace BarakalAlkheir
{
    public static class MainClass
    {
        public static int userID = -1;
        public static int userGroupID = -1;
        public static string userName = "";
        public static string displayName = "";
        public static bool isSuperUser = false;

        public static int groupID = -1;

        public static RadForm mainForm = null;
        public static frmBanks frmbanks = null;
        public static frmCars frmCars = null;
        public static frmCarSuppliers frmCarsupplier = null;
        public static frmCompany frmCompany = null;
        public static frmCustomers frmCustomers = null;
        public static frmDesignations frmDesignations = null;
        public static frmEducations frmEducation = null;
        public static frmGender frmGender = null;
        public static frmMaritalStatus frmMaritalStatus = null;
        public static frmNationality frmNationality = null;
        public static frmReligions frmReligions = null;
        public static frmUserGroup frmUsergroup = null;
        public static frmUsers frmuser = null;
        public static frmForms frmForms = null;
        public static frmMainDefinitions frmMainDefinitions = null;
        public static fromCompanyMain frmCompanyMain = null;
        public static frmEmployeeMain frmEmplooyeeMain = null;

        public static int StartRentDay = 10;
        public static int StartRentCar = 1;


    }
}
