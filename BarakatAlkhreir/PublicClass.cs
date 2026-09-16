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
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Cars
                             orderby ct.car_Brand
                             select ct.car_Brand).Distinct().ToList();

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



        }



        public static void bindNationality(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Nationalities
                             orderby ct.natio_namea
                             select ct).ToList();

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



        }

        public static void bindEmployeeStatus(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.EmployeeStatusDefs
                             orderby ct.employeeStatus_namea
                             select ct).ToList();

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



        }

        public static void bindEmployee(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Employees
                             orderby ct.emp_namefullar
                             select ct).ToList();

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



        }

        public static void bindBanks(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Banks
                             orderby ct.bank_namea
                             select ct).ToList();

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



        }

        public static void bindCars(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Cars
                             orderby ct.car_Name
                             select ct).ToList();

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



        }

        public static void bindCarSupplier(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.CarSuplliers
                             orderby ct.carSuppliser_Name
                             select ct).ToList();

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



        }

        public static void bindCustomers(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Customers
                             orderby ct.customer_Name
                             select ct).ToList();

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



        }
            
        public static void bindDesignations(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Designations
                             orderby ct.desig_namea
                             select ct).ToList();

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



        }

        public static void bindEducations(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Educations
                             orderby ct.edu_namea
                             select ct).ToList();

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



        }

        public static void bindGenders(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Genders
                             orderby ct.gender_namea
                             select ct).ToList();

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



        }

        public static void bindReligions(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Religions
                             orderby ct.religion_namea
                             select ct).ToList();

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



        }

        public static void bindMaritalStatus(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.MaritalStatus
                             orderby ct.marital_namea
                             select ct).ToList();

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



        }

        public static void bindUserGroup(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.UserGroups
                             orderby ct.userGroup_Name
                             select ct).ToList();

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



        }

        public static void bindCompany(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Companies
                             orderby ct.comp_arabicName
                             select ct).ToList();

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



        }

        public static void bindAuthorization(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.AuthorizationPersons
                             orderby ct.fauth_ullArabicName
                             select ct).ToList();

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



        }

        public static void bindMandoub(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.Employees
                             where ct.emp_isMandoub == true
                             orderby ct.emp_namefullar
                             select ct).ToList();

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



        }




        public static void bindLicenseActivity(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var ctype = (from ct in db.LicenseActivities
                             orderby ct.Activity_namea
                             select ct).ToList();

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



        }


        public static void bindDocumentTypeDef(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.DocumentTypeDefinitions
                             orderby ct.documentType_Name
                             select ct).ToList();

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



        }

        public static void bindGovernorate(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.Governorates
                             orderby ct.gover_namea
                             select ct).ToList();

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



        }

        public static void bindGovernorateArea(RadDropDownList cmb, int _governorateID)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.GovernorateAreas
                             where ct.governorateID == _governorateID
                             orderby ct.goverarea_namea
                             select ct).ToList();

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



        }

        public static void bindDocumentDef(RadDropDownList cmb, int _documentTypeDefID)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.DocumentDefs
                             where ct.documentTypeDefID == _documentTypeDefID
                             orderby ct.documentDef_Name
                             select ct).ToList();

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



        }

        public static void bindDocumentDef(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.DocumentDefs
                             orderby ct.documentDef_Name
                             select ct).ToList();

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



        }

        public static void bindTransactionDef(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.TransactionDefs
                             orderby ct.transDef_namea
                             select ct).ToList();

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



        }

        public static void bindTransactionDef1(RadDropDownList cmb)
        {
            cmb.Items.Clear();

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var ctype = (from ct in db.TransactionDefs
                             join ctt in db.DocumentTransactions on ct.transDefID equals ctt.transDefID
                             where ctt.documentDefID == 2
                             orderby ct.transDef_namea
                             select ct).ToList();

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

        }




    }
}
