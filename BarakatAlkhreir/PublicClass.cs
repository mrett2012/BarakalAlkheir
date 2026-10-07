using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using BarakalAlkheir.Model;

namespace BarakalAlkheir
{
    public class PublicClass
    {
        public PublicClass()
        {
        }


        public static void bindCarBrand(RadDropDownList cmb)
        {
            bindCarBrandAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindCarBrandAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Cars
                             orderby ct.car_Brand
                             select ct.car_Brand).Distinct().ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Text = item;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindCarBrand");
        }



        public static void bindNationality(RadDropDownList cmb)
        {
            bindNationalityAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindNationalityAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Nationalities
                             orderby ct.natio_namea
                             select new { ct.natio_namea, ct.nationalityID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.nationalityID;
                        rl1.Text = item.natio_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindNationality");
        }

        public static void bindEmployeeStatus(RadDropDownList cmb)
        {
            bindEmployeeStatusAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindEmployeeStatusAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.EmployeeStatusDefs
                             orderby ct.employeeStatus_namea
                             select new { ct.employeeStatus_namea, ct.employeeStatusID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.employeeStatusID;
                        rl1.Text = item.employeeStatus_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindEmployeeStatus");
        }

        public static void bindEmployee(RadDropDownList cmb)
        {
            bindEmployeeAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindEmployeeAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Employees
                             orderby ct.emp_namefullar
                             select new { ct.emp_namefullar, ct.employeeID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.employeeID;
                        rl1.Text = item.emp_namefullar;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindEmployee");
        }

        public static void bindBanks(RadDropDownList cmb)
        {
            bindBanksAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindBanksAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Banks
                             orderby ct.bank_namea
                             select new { ct.bank_namea, ct.bankID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.bankID;
                        rl1.Text = item.bank_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindBanks");
        }

        public static void bindCars(RadDropDownList cmb)
        {
            bindCarsAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindCarsAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Cars
                             orderby ct.car_Name
                             select new { ct.car_Name, ct.carID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.carID;
                        rl1.Text = item.car_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindCars");
        }

        public static void bindCarSupplier(RadDropDownList cmb)
        {
            bindCarSupplierAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindCarSupplierAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.CarSuplliers
                             orderby ct.carSuppliser_Name
                             select new { ct.carSuppliser_Name, ct.carSuppliserID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.carSuppliserID;
                        rl1.Text = item.carSuppliser_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindCarSupplier");
        }

        public static void bindCustomers(RadDropDownList cmb)
        {
            bindCustomersAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindCustomersAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Customers
                             orderby ct.customer_Name
                             select new { ct.customer_Name, ct.customerID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.customerID;
                        rl1.Text = item.customer_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindCustomers");
        }
            
        public static void bindDesignations(RadDropDownList cmb)
        {
            bindDesignationsAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindDesignationsAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Designations
                             orderby ct.desig_namea
                             select new { ct.desig_namea, ct.designationID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.designationID;
                        rl1.Text = item.desig_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindDesignations");
        }

        public static void bindEducations(RadDropDownList cmb)
        {
            bindEducationsAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindEducationsAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Educations
                             orderby ct.edu_namea
                             select new { ct.edu_namea, ct.educationID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.educationID;
                        rl1.Text = item.edu_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindEducations");
        }

        public static void bindGenders(RadDropDownList cmb)
        {
            bindGendersAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindGendersAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Genders
                             orderby ct.gender_namea
                             select new { ct.gender_namea, ct.genderID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.genderID;
                        rl1.Text = item.gender_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindGenders");
        }

        public static void bindReligions(RadDropDownList cmb)
        {
            bindReligionsAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindReligionsAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Religions
                             orderby ct.religion_namea
                             select new { ct.religion_namea, ct.religionID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.religionID;
                        rl1.Text = item.religion_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindReligions");
        }

        public static void bindMaritalStatus(RadDropDownList cmb)
        {
            bindMaritalStatusAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindMaritalStatusAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.MaritalStatus
                             orderby ct.marital_namea
                             select new { ct.marital_namea, ct.maritalStatusID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.maritalStatusID;
                        rl1.Text = item.marital_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindMaritalStatus");
        }

        public static void bindUserGroup(RadDropDownList cmb)
        {
            bindUserGroupAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindUserGroupAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.UserGroups
                             orderby ct.userGroup_Name
                             select new { ct.userGroup_Name, ct.userGroupID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.userGroupID;
                        rl1.Text = item.userGroup_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindUserGroup");
        }

        public static void bindCompany(RadDropDownList cmb)
        {
            bindCompanyAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindCompanyAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Companies
                             orderby ct.comp_arabicName
                             select new { ct.comp_arabicName, ct.companyID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.companyID;
                        rl1.Text = item.comp_arabicName;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindCompany");
        }

        public static void bindAuthorization(RadDropDownList cmb)
        {
            bindAuthorizationAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindAuthorizationAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.AuthorizationPersons
                             orderby ct.fauth_ullArabicName
                             select new { ct.authorizationID, ct.fauth_ullArabicName }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.authorizationID;
                        rl1.Text = item.fauth_ullArabicName;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindAuthorization");
        }

        public static void bindMandoub(RadDropDownList cmb)
        {
            bindMandoubAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindMandoubAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Employees
                             where ct.emp_isMandoub == true
                             orderby ct.emp_namefullar
                             select new { ct.emp_namefullar, ct.employeeID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.employeeID;
                        rl1.Text = item.emp_namefullar;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindMandoub");
        }




        public static void bindLicenseActivity(RadDropDownList cmb)
        {
            bindLicenseActivityAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindLicenseActivityAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.LicenseActivities
                             orderby ct.Activity_namea
                             select new { ct.Activity_namea, ct.licenseActivityID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.licenseActivityID;
                        rl1.Text = item.Activity_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindLicenseActivity");
        }


        public static void bindDocumentTypeDef(RadDropDownList cmb)
        {
            bindDocumentTypeDefAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindDocumentTypeDefAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.DocumentTypeDefinitions
                             orderby ct.documentType_Name
                             select new { ct.documentType_Name, ct.documentTypeDefID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.documentTypeDefID;
                        rl1.Text = item.documentType_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindDocumentTypeDef");
        }

        public static void bindGovernorate(RadDropDownList cmb)
        {
            bindGovernorateAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindGovernorateAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.Governorates
                             orderby ct.gover_namea
                             select new { ct.gover_namea, ct.governorateID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.governorateID;
                        rl1.Text = item.gover_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindGovernorate");
        }

        public static void bindGovernorateArea(RadDropDownList cmb, int _governorateID)
        {
            bindGovernorateAreaAsync(cmb, _governorateID, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindGovernorateAreaAsync(RadDropDownList cmb, int _governorateID, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.GovernorateAreas
                             where ct.governorateID == _governorateID
                             orderby ct.goverarea_namea
                             select new { ct.goverarea_namea, ct.goverareaID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.goverareaID;
                        rl1.Text = item.goverarea_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindGovernorateArea" + ":" + _governorateID);
        }

        public static void bindDocumentDef(RadDropDownList cmb, int _documentTypeDefID)
        {
            bindDocumentDefAsync(cmb, _documentTypeDefID, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindDocumentDefAsync(RadDropDownList cmb, int _documentTypeDefID, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.DocumentDefs
                             where ct.documentTypeDefID == _documentTypeDefID
                             orderby ct.documentDef_Name
                             select new { ct.documentDef_Name, ct.documentDefID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.documentDefID;
                        rl1.Text = item.documentDef_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindDocumentDef" + ":" + _documentTypeDefID);
        }

        public static void bindDocumentDef(RadDropDownList cmb)
        {
            bindDocumentDefAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindDocumentDefAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.DocumentDefs
                             orderby ct.documentDef_Name
                             select new { ct.documentDef_Name, ct.documentDefID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.documentDefID;
                        rl1.Text = item.documentDef_Name;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindDocumentDef");
        }

        public static void bindTransactionDef(RadDropDownList cmb)
        {
            bindTransactionDefAsync(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindTransactionDefAsync(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.TransactionDefs
                             orderby ct.transDef_namea
                             select new { ct.transDef_namea, ct.transDefID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.transDefID;
                        rl1.Text = item.transDef_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindTransactionDef");
        }

        public static void bindTransactionDef1(RadDropDownList cmb)
        {
            bindTransactionDef1Async(cmb, false).GetAwaiter().GetResult();
        }

        public static async System.Threading.Tasks.Task bindTransactionDef1Async(RadDropDownList cmb, bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(cmb.FindForm(), () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from ct in db.TransactionDefs
                             join ctt in db.DocumentTransactions on ct.transDefID equals ctt.transDefID
                             where ctt.documentDefID == 2
                             orderby ct.transDef_namea
                             select new { ct.transDef_namea, ct.transDefID }).ToList();
                }
            }, ctype =>
            {
                cmb.BeginUpdate();
                try
                {
                    cmb.Items.Clear();
                    RadListDataItem rl = new RadListDataItem();
                    rl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    rl.Value = "-1";
                    rl.Text = "اختيار";
                    cmb.Items.Add(rl);

                    foreach (var item in ctype)
                    {
                        RadListDataItem rl1 = new RadListDataItem();
                        rl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        rl1.Value = item.transDefID;
                        rl1.Text = item.transDef_namea;
                        cmb.Items.Add(rl1);
                    }
                }
                finally { cmb.EndUpdate(); }
            }, background, "bindTransactionDef1");
        }




    }
}
