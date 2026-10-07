using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Model;

namespace BarakalAlkheir.emp
{
    public partial class frmEmployeeMain : Telerik.WinControls.UI.RadForm
    {
        public frmEmployeeMain()
        {
            InitializeComponent();
        }

        private void btnFindShop_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, true);
            frm.isCurrentRentedLicencesInclude = true;
            frm.ShowDialog();
            if (frm.found)
            {
                int ID2 = frm.licID;
                txtShopNo.Text = frm.licNO;
                txtShopCommercialName.Text = frm.licName;
                lblLicenseID.Text = ID2.ToString();
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.LicensesShops
                              where c.companyLicencesID == ID2
                              select new { c.licencesShopID };

                    lblLicenseShopID.Text = val.SingleOrDefault().licencesShopID.ToString();
                }
            }
        }

        DataTable dt = null;
        private void makePrintTable()
        {
            dt = new DataTable();
            dt.Columns.Add("one");
            dt.Columns.Add("two");
            dt.Columns.Add("three");
            dt.Columns.Add("four");
            dt.Columns.Add("five");
            dt.Columns.Add("six");
            dt.Columns.Add("seven");
            dt.Columns.Add("eight");
            dt.Columns.Add("nine");
            dt.Columns.Add("ten");

            dt.Columns.Add("11");
            dt.Columns.Add("12");
            dt.Columns.Add("13");
            dt.Columns.Add("14");
            dt.Columns.Add("15");
            dt.Columns.Add("16");
            dt.Columns.Add("17");
            dt.Columns.Add("18");
            dt.Columns.Add("19");
            dt.Columns.Add("20");

            dt.Columns.Add("21");
            dt.Columns.Add("22");
            dt.Columns.Add("23");
            dt.Columns.Add("24");
            dt.Columns.Add("25");

        }


        private void btnEmployeeSponsor_Click(object sender, EventArgs e)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int LID = int.Parse(lblLicenseID.Text);
                int SID = int.Parse(lblLicenseShopID.Text);

                var val = (from c in db.Employees
                           join d in db.EmployeeSponsors
                           on c.employeeID equals d.employeeID
                           join dd in db.CompanyLicences on
                           d.companyLicencesID equals dd.companyLicencesID
                           where d.companyLicencesID == LID &&
                           c.emp_active ==true
                           select new
                           {
                               //الرمز= c.employeeID,
                               //رقم_الموظف = c.emp_No,
                               //الموظف= c.emp_namefullar,
                               //المهنة= d.Designation.desig_namea,
                               //رقم_الرخصة= dd.licenseNo,
                               //الاسم_التجاري= dd.commercialName,

                               employeeID = c.employeeID,
                               emp_No = c.emp_No,
                               emp_namefullar = c.emp_namefullar,
                               licenseNo = dd.licenseNo,
                               employeeLink = c.EmployeeLicenseShops.FirstOrDefault().employeeLink,
                               shop_Name = c.EmployeeLicenseShops.FirstOrDefault().LicensesShop.shop_Name,
                               pass_no = c.EmployeePassports.FirstOrDefault().pass_no,
                               emp_civilID = c.emp_civilID,
                               employeeSponsor_salary = d.employeeSponsor_salary,
                               paidFromSalary = c.EmployeeSalaries.FirstOrDefault().paidFromSalary,
                               employeeSponsor_residencyEndDate = d.employeeSponsor_residencyEndDate,
                               employeeStatus_namea = c.EmployeeStatusDef.employeeStatus_namea,
                               employeeSponsor_useDuration = d.employeeSponsor_useDuration,
                               desig_namea = d.Designation.desig_namea,
                               natio_namea = c.Nationality.natio_namea,
                               commercialName = d.CompanyLicence.commercialName,
                               comp_arabicName = d.CompanyLicence.Company.comp_arabicName

                           }).ToList();

                Gview.DataSource = val;

                makePrintTable();
                foreach (var item in val)
                {
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.shop_Name;
                    dr["seven"] = item.pass_no;
                    dr["eight"] = item.emp_civilID;
                    dr["nine"] = item.employeeSponsor_salary;
                    dr["ten"] = item.paidFromSalary;
                    if (item.employeeSponsor_residencyEndDate.HasValue)
                    {
                        dr["11"] = item.employeeSponsor_residencyEndDate.Value.ToShortDateString();
                    }
                    else
                    {
                        dr["11"] = "";
                    }

                    dr["12"]=item.employeeStatus_namea;
                    dr["13"]=item.employeeSponsor_useDuration;
                    dr["14"]=item.desig_namea;
                    dr["15"] = item.natio_namea;
                    dr["16"]= item.commercialName;
                    dr["17"] = item.comp_arabicName;
                    dr["18"]="";
                    dr["19"]="";
                    dr["20"]="";

                    dr["21"]="";
                    dr["22"]="";
                    dr["23"]="";
                    dr["24"]="";
                    dr["25"]="";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeLicences";
                rv.ds = ds;
                rv.title = "اقامات الموظفين على الرخصة : " + txtShopCommercialName.Text;
                rv.ShowDialog();
            }
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            emp.frmEmployees frm = new emp.frmEmployees();
            frm.ShowDialog();
        }

        private void btnEmployeeWorkFor_Click(object sender, EventArgs e)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int LID = int.Parse(lblLicenseID.Text);
                int SID = int.Parse(lblLicenseShopID.Text);

                var val = (from c in db.Employees.Include("EmployeeSponsors")
                           join d in db.EmployeeLicenseShops
                           on c.employeeID equals d.employeeID
                           join dd in db.LicensesShops on
                           d.licencesShopID equals dd.licencesShopID
                           where d.licencesShopID == LID
                           && c.emp_active == true
                           select new
                           {
                               employeeID = c.employeeID,
                               emp_No = c.emp_No,
                               emp_namefullar = c.emp_namefullar,
                               shop_Code = dd.shop_Code,
                               licenseNo = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.licenseNo,
                               employeeLink = d.employeeLink,
                               shop_Name = dd.shop_Name,
                               pass_no = c.EmployeePassports.FirstOrDefault().pass_no,
                               emp_civilID = c.emp_civilID,
                               employeeSponsor_salary = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_salary,
                               paidFromSalary = c.EmployeeSalaries.FirstOrDefault().paidFromSalary,
                               employeeSponsor_residencyEndDate = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_residencyEndDate,
                               employeeStatus_namea = c.EmployeeStatusDef.employeeStatus_namea,
                               employeeSponsor_useDuration = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_useDuration,
                               desig_namea = c.EmployeeSponsors.FirstOrDefault().Designation.desig_namea,
                               natio_namea = c.Nationality.natio_namea,
                               commercialName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.commercialName,
                               comp_arabicName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.Company.comp_arabicName

                           }).ToList();

                Gview.DataSource = val;
                makePrintTable();
                foreach (var item in val)
                {
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.shop_Name;
                    dr["seven"] = item.pass_no;
                    dr["eight"] = item.emp_civilID;
                    dr["nine"] = item.employeeSponsor_salary;
                    dr["ten"] = item.paidFromSalary;
                    if (item.employeeSponsor_residencyEndDate.HasValue)
                    {
                        dr["11"] = item.employeeSponsor_residencyEndDate.Value.ToShortDateString();
                    }
                    else
                    {
                        dr["11"] = "";
                    }

                    dr["12"] = item.employeeStatus_namea;
                    dr["13"] = item.employeeSponsor_useDuration;
                    dr["14"] = item.desig_namea;
                    dr["15"] = item.natio_namea;
                    dr["16"] = item.commercialName;
                    dr["17"] = item.comp_arabicName;
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";

                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeLicences";
                rv.ds = ds;
                rv.title = "الموظفين العاملين في المحل : " + txtShopCommercialName.Text;
                rv.ShowDialog();
            }
        }

        private void frmEmployeeMain_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmEmployeeMain_LoadData);
        }

        private async System.Threading.Tasks.Task frmEmployeeMain_LoadData(object sender, EventArgs e)
        {
            try
            {
                await PublicClass.bindEmployeeStatusAsync(cmbEmploeeStatus);
                cmbEmploeeStatus.SelectedIndex = 0;

                await PublicClass.bindTransactionDef1Async(cmbtransDefID);
                cmbtransDefID.SelectedIndex = 0;
                txtDate1.Text = DateTime.Now.ToShortDateString();
                txtDate2.Text = DateTime.Now.ToShortDateString();

            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEmployeesByStatus_Click(object sender, EventArgs e)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int sID = int.Parse(cmbEmploeeStatus.SelectedValue.ToString());
                var val = (from c in db.Employees
                           where c.employeeStatusID == sID
                           && c.emp_active == true
                           select new
                           {

                               employeeID = c.employeeID,
                               emp_No = c.emp_No,
                               emp_namefullar = c.emp_namefullar,
                               licenseNo = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.licenseNo,
                               employeeLink = c.EmployeeLicenseShops.FirstOrDefault().employeeLink,
                               shop_Name = c.EmployeeLicenseShops.FirstOrDefault().LicensesShop.shop_Name,
                               pass_no = c.EmployeePassports.FirstOrDefault().pass_no,
                               emp_civilID = c.emp_civilID,
                               employeeSponsor_salary = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_salary,
                               paidFromSalary = c.EmployeeSalaries.FirstOrDefault().paidFromSalary,
                               employeeSponsor_residencyEndDate = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_residencyEndDate,
                               employeeStatus_namea = c.EmployeeStatusDef.employeeStatus_namea,
                               employeeSponsor_useDuration = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_useDuration,
                               desig_namea = c.EmployeeSponsors.FirstOrDefault().Designation.desig_namea,
                               natio_namea = c.Nationality.natio_namea,
                               commercialName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.commercialName,
                               comp_arabicName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.Company.comp_arabicName

                           }).ToList();

                Gview.DataSource = val;

                makePrintTable();
                foreach (var item in val)
                {
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.shop_Name;
                    dr["seven"] = item.pass_no;
                    dr["eight"] = item.emp_civilID;
                    dr["nine"] = item.employeeSponsor_salary;
                    dr["ten"] = item.paidFromSalary;
                    if (item.employeeSponsor_residencyEndDate.HasValue)
                    {
                        dr["11"] = item.employeeSponsor_residencyEndDate.Value.ToShortDateString();
                    }
                    else
                    {
                        dr["11"] = "";
                    }

                    dr["12"] = item.employeeStatus_namea;
                    dr["13"] = item.employeeSponsor_useDuration;
                    dr["14"] = item.desig_namea;
                    dr["15"] = item.natio_namea;
                    dr["16"] = item.commercialName;
                    dr["17"] = item.comp_arabicName;
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";

                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeLicences";
                rv.ds = ds;
                rv.title = "الموظفين حسب الحالة : " + this.cmbEmploeeStatus.Text;
                rv.ShowDialog();
            }
        }

        private void Gview_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gview.CurrentRow.Cells[0].Value != null)
            {
                int EID = -1;
                if (int.TryParse(Gview.CurrentRow.Cells[0].Value.ToString(), out EID))
                {
                    emp.frmEmployees frm = new emp.frmEmployees();
                    frm._employeeID = EID;
                    frm.ShowDialog();
                }
            }
        }

        private void Gview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (Gview.SelectedRows.Count > 0)
                {
                    if (Gview.SelectedRows[0].Cells[0].Value != null)
                    {
                        int EID = -1;
                        if (int.TryParse(Gview.SelectedRows[0].Cells[0].Value.ToString(), out EID))
                        {
                            emp.frmEmployees frm = new emp.frmEmployees();
                            frm._employeeID = EID;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }

        private void btnAllEmployeeActive_Click(object sender, EventArgs e)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int sID = int.Parse(cmbEmploeeStatus.SelectedValue.ToString());
                var val = (from c in db.Employees
                           where c.emp_active ==true
                           select new
                           {
                               //الرمز = c.employeeID,
                               //رقم_الموظف = c.emp_No,
                               //الموظف = c.emp_namefullar,
                               //الحالة = c.EmployeeStatusDef.employeeStatus_namea,
                               //مفعل = c.emp_active,
                               //مندوب = c.emp_isMandoub

                               employeeID = c.employeeID,
                               emp_No = c.emp_No,
                               emp_namefullar = c.emp_namefullar,
                               licenseNo = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.licenseNo,
                               employeeLink = c.EmployeeLicenseShops.FirstOrDefault().employeeLink,
                               shop_Name = c.EmployeeLicenseShops.FirstOrDefault().LicensesShop.shop_Name,
                               pass_no = c.EmployeePassports.FirstOrDefault().pass_no,
                               emp_civilID = c.emp_civilID,
                               employeeSponsor_salary = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_salary,
                               paidFromSalary = c.EmployeeSalaries.FirstOrDefault().paidFromSalary,
                               employeeSponsor_residencyEndDate = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_residencyEndDate,
                               employeeStatus_namea = c.EmployeeStatusDef.employeeStatus_namea,
                               employeeSponsor_useDuration = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_useDuration,
                               desig_namea = c.EmployeeSponsors.FirstOrDefault().Designation.desig_namea,
                               natio_namea = c.Nationality.natio_namea,
                               commercialName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.commercialName,
                               comp_arabicName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.Company.comp_arabicName,
                               emp_bankAcc = c.EmployeeSalaries.FirstOrDefault().accountNo,
                               carID = c.EmployeeLicenseShops.FirstOrDefault().carID

                           }).ToList();

                Gview.DataSource = val;

                makePrintTable();
                foreach (var item in val)
                {
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.shop_Name;
                    dr["seven"] = item.pass_no;
                    dr["eight"] = item.emp_civilID;
                    dr["nine"] = item.employeeSponsor_salary;
                    dr["ten"] = item.paidFromSalary;
                    if (item.employeeSponsor_residencyEndDate.HasValue)
                    {
                        dr["11"] = item.employeeSponsor_residencyEndDate.Value.ToShortDateString();
                    }
                    else
                    {
                        dr["11"] = "";
                    }

                    dr["12"] = item.employeeStatus_namea;
                    dr["13"] = item.employeeSponsor_useDuration;
                    dr["14"] = item.desig_namea;
                    dr["15"] = item.natio_namea;
                    dr["16"] = item.commercialName;
                    dr["17"] = item.comp_arabicName;
                    dr["18"] = item.emp_bankAcc;
                    var carName = "";
                    if (item.carID != null)
                    {
                        if (item.carID.HasValue && item.carID.Value != -1)
                        {
                            carName = db.Cars.Where(ca => ca.carID == item.carID.Value).FirstOrDefault().car_Name;
                        }
                    }
                    dr["19"] = carName;
                    dr["20"] = "";

                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeLicences";
                rv.ds = ds;
                rv.title = "جميع الموظفين الفعالين  ";
                rv.ShowDialog();
            }
        }

        private void btnAllEmployeeNotActive_Click(object sender, EventArgs e)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int sID = int.Parse(cmbEmploeeStatus.SelectedValue.ToString());
                var val = (from c in db.Employees
                           where c.emp_active == false
                           select new
                           {
                               //الرمز = c.employeeID,
                               //رقم_الموظف = c.emp_No,
                               //الموظف = c.emp_namefullar,
                               //الحالة = c.EmployeeStatusDef.employeeStatus_namea,
                               //مفعل = c.emp_active,
                               //مندوب = c.emp_isMandoub

                               employeeID = c.employeeID,
                               emp_No = c.emp_No,
                               emp_namefullar = c.emp_namefullar,
                               licenseNo = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.licenseNo,
                               employeeLink = c.EmployeeLicenseShops.FirstOrDefault().employeeLink,
                               carID = c.EmployeeLicenseShops.FirstOrDefault().carID,
                               shop_Name = c.EmployeeLicenseShops.FirstOrDefault().LicensesShop.shop_Name,
                               pass_no = c.EmployeePassports.FirstOrDefault().pass_no,
                               emp_civilID = c.emp_civilID,
                               employeeSponsor_salary = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_salary,
                               paidFromSalary = c.EmployeeSalaries.FirstOrDefault().paidFromSalary,
                               employeeSponsor_residencyEndDate = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_residencyEndDate,
                               employeeStatus_namea = c.EmployeeStatusDef.employeeStatus_namea,
                               employeeSponsor_useDuration = c.EmployeeSponsors.FirstOrDefault().employeeSponsor_useDuration,
                               desig_namea = c.EmployeeSponsors.FirstOrDefault().Designation.desig_namea,
                               natio_namea = c.Nationality.natio_namea,
                               commercialName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.commercialName,
                               comp_arabicName = c.EmployeeSponsors.FirstOrDefault().CompanyLicence.Company.comp_arabicName

                           }).ToList();

                Gview.DataSource = val;

                makePrintTable();
                foreach (var item in val)
                {
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.shop_Name;
                    dr["seven"] = item.pass_no;
                    dr["eight"] = item.emp_civilID;
                    dr["nine"] = item.employeeSponsor_salary;
                    dr["ten"] = item.paidFromSalary;
                    if (item.employeeSponsor_residencyEndDate.HasValue)
                    {
                        dr["11"] = item.employeeSponsor_residencyEndDate.Value.ToShortDateString();
                    }
                    else
                    {
                        dr["11"] = "";
                    }

                    dr["12"] = item.employeeStatus_namea;
                    dr["13"] = item.employeeSponsor_useDuration;
                    dr["14"] = item.desig_namea;
                    dr["15"] = item.natio_namea;
                    dr["16"] = item.commercialName;
                    dr["17"] = item.comp_arabicName;
                    dr["18"] = "";
                    var carName = "";
                    if (item.carID != null)
                    {
                        if (item.carID.HasValue && item.carID.Value != -1)
                        {
                            carName = db.Cars.Where(ca => ca.carID == item.carID.Value).FirstOrDefault().car_Name;
                        }
                    }
                    dr["19"] = carName;
                    dr["20"] = "";

                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeLicences";
                rv.ds = ds;
                rv.title = "جميع الموظفين الغير الفعالين  ";
                rv.ShowDialog();
            }
        }

        private void AllLicencesRemarks_Click(object sender, EventArgs e)
        {
            try
            {
                int LID = int.Parse(lblLicenseID.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               join cc in db.CompanyLicencesRemarks on c.companyLicencesID equals cc.companyLicencesID
                               where c.companyLicencesID == LID
                               orderby cc.comLicenseRem_isActive , cc.comLicenseRem_Date
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   comp_arabicName = c.Company.comp_arabicName,
                                   startDate = c.startDate,
                                   endDate = c.endDate,

                                   licenseUniqueNo = c.licenseUniqueNo,
                                   comLicenseRemID = cc.comLicenseRemID,
                                   comLicenseRem_Date = cc.comLicenseRem_Date,
                                   comLicenseRem_Remarks = cc.comLicenseRem_Remarks,
                                   comLicenseRem_Action = cc.comLicenseRem_Action,
                                   comLicenseRem_isActive = cc.comLicenseRem_isActive

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseNo;
                        dr["three"] = item.startDate.Value.ToShortDateString();
                        dr["four"] = "";
                        dr["five"] = item.comp_arabicName;
                        dr["six"] = item.commercialName;
                        dr["seven"] = item.endDate.Value.ToShortDateString();

                        dr["eight"] = item.licenseUniqueNo;
                        dr["nine"] = item.comLicenseRemID;
                        dr["ten"] = item.comLicenseRem_Date.Value.ToShortDateString();
                        dr["11"] = item.comLicenseRem_Remarks;
                        dr["12"] = item.comLicenseRem_Action;

                        if (item.comLicenseRem_isActive.Value)
                        {
                            dr["13"] = "غير مقفلة";
                        }
                        else
                        {
                            dr["13"] = "مقفلة";
                        }

                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                        dr["17"] = "";
                        dr["18"] = "";
                        dr["19"] = "";
                        dr["20"] = "";

                        dr["21"] = "";
                        dr["22"] = "";
                        dr["23"] = "";
                        dr["24"] = "";
                        dr["25"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesemployee.xsd");
                    //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeLicencesRemarks";
                    rv.ds = ds;
                    rv.title = " ملاحظات الرخصة : " + txtShopCommercialName.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllLicencesRemarksActive_Click(object sender, EventArgs e)
        {
            try
            {
                int LID = int.Parse(lblLicenseID.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               join cc in db.CompanyLicencesRemarks on c.companyLicencesID equals cc.companyLicencesID
                               where c.companyLicencesID == LID && cc.comLicenseRem_isActive ==true
                               orderby cc.comLicenseRem_isActive, cc.comLicenseRem_Date
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   comp_arabicName = c.Company.comp_arabicName,
                                   startDate = c.startDate,
                                   endDate = c.endDate,

                                   licenseUniqueNo = c.licenseUniqueNo,
                                   comLicenseRemID = cc.comLicenseRemID,
                                   comLicenseRem_Date = cc.comLicenseRem_Date,
                                   comLicenseRem_Remarks = cc.comLicenseRem_Remarks,
                                   comLicenseRem_Action = cc.comLicenseRem_Action,
                                   comLicenseRem_isActive = cc.comLicenseRem_isActive

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseNo;
                        dr["three"] = item.startDate.Value.ToShortDateString();
                        dr["four"] = "";
                        dr["five"] = item.comp_arabicName;
                        dr["six"] = item.commercialName;
                        dr["seven"] = item.endDate.Value.ToShortDateString();

                        dr["eight"] = item.licenseUniqueNo;
                        dr["nine"] = item.comLicenseRemID;
                        dr["ten"] = item.comLicenseRem_Date.Value.ToShortDateString();
                        dr["11"] = item.comLicenseRem_Remarks;
                        dr["12"] = item.comLicenseRem_Action;

                        if (item.comLicenseRem_isActive.Value)
                        {
                            dr["13"] = "غير مقفلة";
                        }
                        else
                        {
                            dr["13"] = "مقفلة";
                        }

                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                        dr["17"] = "";
                        dr["18"] = "";
                        dr["19"] = "";
                        dr["20"] = "";

                        dr["21"] = "";
                        dr["22"] = "";
                        dr["23"] = "";
                        dr["24"] = "";
                        dr["25"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesemployee.xsd");
                    //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeLicencesRemarks";
                    rv.ds = ds;
                    rv.title = " ملاحظات الرخصة : " + txtShopCommercialName.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllLicencesRemarksNotActive_Click(object sender, EventArgs e)
        {
            try
            {
                int LID = int.Parse(lblLicenseID.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               join cc in db.CompanyLicencesRemarks on c.companyLicencesID equals cc.companyLicencesID
                               where c.companyLicencesID == LID && cc.comLicenseRem_isActive ==false
                               orderby cc.comLicenseRem_isActive, cc.comLicenseRem_Date
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   comp_arabicName = c.Company.comp_arabicName,
                                   startDate = c.startDate,
                                   endDate = c.endDate,

                                   licenseUniqueNo = c.licenseUniqueNo,
                                   comLicenseRemID = cc.comLicenseRemID,
                                   comLicenseRem_Date = cc.comLicenseRem_Date,
                                   comLicenseRem_Remarks = cc.comLicenseRem_Remarks,
                                   comLicenseRem_Action = cc.comLicenseRem_Action,
                                   comLicenseRem_isActive = cc.comLicenseRem_isActive

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseNo;
                        dr["three"] = item.startDate.Value.ToShortDateString();
                        dr["four"] = "";
                        dr["five"] = item.comp_arabicName;
                        dr["six"] = item.commercialName;
                        dr["seven"] = item.endDate.Value.ToShortDateString();

                        dr["eight"] = item.licenseUniqueNo;
                        dr["nine"] = item.comLicenseRemID;
                        dr["ten"] = item.comLicenseRem_Date.Value.ToShortDateString();
                        dr["11"] = item.comLicenseRem_Remarks;
                        dr["12"] = item.comLicenseRem_Action;

                        if (item.comLicenseRem_isActive.Value)
                        {
                            dr["13"] = "غير مقفلة";
                        }
                        else
                        {
                            dr["13"] = "مقفلة";
                        }

                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                        dr["17"] = "";
                        dr["18"] = "";
                        dr["19"] = "";
                        dr["20"] = "";

                        dr["21"] = "";
                        dr["22"] = "";
                        dr["23"] = "";
                        dr["24"] = "";
                        dr["25"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesemployee.xsd");
                    //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeLicencesRemarks";
                    rv.ds = ds;
                    rv.title = " ملاحظات الرخصة : " + txtShopCommercialName.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllLicencesRemarks22_Click(object sender, EventArgs e)
        {
            try
            {
                int LID = int.Parse(lblLicenseID.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               join cc in db.CompanyLicencesRemarks on c.companyLicencesID equals cc.companyLicencesID
                               orderby cc.comLicenseRem_isActive, cc.comLicenseRem_Date
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   comp_arabicName = c.Company.comp_arabicName,
                                   startDate = c.startDate,
                                   endDate = c.endDate,

                                   licenseUniqueNo = c.licenseUniqueNo,
                                   comLicenseRemID = cc.comLicenseRemID,
                                   comLicenseRem_Date = cc.comLicenseRem_Date,
                                   comLicenseRem_Remarks = cc.comLicenseRem_Remarks,
                                   comLicenseRem_Action = cc.comLicenseRem_Action,
                                   comLicenseRem_isActive = cc.comLicenseRem_isActive

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseNo;
                        dr["three"] = item.startDate.Value.ToShortDateString();
                        dr["four"] = "";
                        dr["five"] = item.comp_arabicName;
                        dr["six"] = item.commercialName;
                        dr["seven"] = item.endDate.Value.ToShortDateString();

                        dr["eight"] = item.licenseUniqueNo;
                        dr["nine"] = item.comLicenseRemID;
                        dr["ten"] = item.comLicenseRem_Date.Value.ToShortDateString();
                        dr["11"] = item.comLicenseRem_Remarks;
                        dr["12"] = item.comLicenseRem_Action;

                        if (item.comLicenseRem_isActive.Value)
                        {
                            dr["13"] = "غير مقفلة";
                        }
                        else
                        {
                            dr["13"] = "مقفلة";
                        }

                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                        dr["17"] = "";
                        dr["18"] = "";
                        dr["19"] = "";
                        dr["20"] = "";

                        dr["21"] = "";
                        dr["22"] = "";
                        dr["23"] = "";
                        dr["24"] = "";
                        dr["25"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesemployee.xsd");
                    //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeLicencesRemarks";
                    rv.ds = ds;
                    rv.title = "ملاحظات الرخص التجارية";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllLicencesRemarksActive22_Click(object sender, EventArgs e)
        {
            try
            {
                int LID = int.Parse(lblLicenseID.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               join cc in db.CompanyLicencesRemarks on c.companyLicencesID equals cc.companyLicencesID
                               where cc.comLicenseRem_isActive==true
                               orderby cc.comLicenseRem_isActive, cc.comLicenseRem_Date
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   comp_arabicName = c.Company.comp_arabicName,
                                   startDate = c.startDate,
                                   endDate = c.endDate,

                                   licenseUniqueNo = c.licenseUniqueNo,
                                   comLicenseRemID = cc.comLicenseRemID,
                                   comLicenseRem_Date = cc.comLicenseRem_Date,
                                   comLicenseRem_Remarks = cc.comLicenseRem_Remarks,
                                   comLicenseRem_Action = cc.comLicenseRem_Action,
                                   comLicenseRem_isActive = cc.comLicenseRem_isActive

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseNo;
                        dr["three"] = item.startDate.Value.ToShortDateString();
                        dr["four"] = "";
                        dr["five"] = item.comp_arabicName;
                        dr["six"] = item.commercialName;
                        dr["seven"] = item.endDate.Value.ToShortDateString();

                        dr["eight"] = item.licenseUniqueNo;
                        dr["nine"] = item.comLicenseRemID;
                        dr["ten"] = item.comLicenseRem_Date.Value.ToShortDateString();
                        dr["11"] = item.comLicenseRem_Remarks;
                        dr["12"] = item.comLicenseRem_Action;

                        if (item.comLicenseRem_isActive.Value)
                        {
                            dr["13"] = "غير مقفلة";
                        }
                        else
                        {
                            dr["13"] = "مقفلة";
                        }

                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                        dr["17"] = "";
                        dr["18"] = "";
                        dr["19"] = "";
                        dr["20"] = "";

                        dr["21"] = "";
                        dr["22"] = "";
                        dr["23"] = "";
                        dr["24"] = "";
                        dr["25"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesemployee.xsd");
                    //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeLicencesRemarks";
                    rv.ds = ds;
                    rv.title = "ملاحظات الرخص التجارية";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllLicencesRemarksNotActive22_Click(object sender, EventArgs e)
        {
            try
            {
                int LID = int.Parse(lblLicenseID.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               join cc in db.CompanyLicencesRemarks on c.companyLicencesID equals cc.companyLicencesID
                               where cc.comLicenseRem_isActive == false
                               orderby cc.comLicenseRem_isActive, cc.comLicenseRem_Date
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   comp_arabicName = c.Company.comp_arabicName,
                                   startDate = c.startDate,
                                   endDate = c.endDate,

                                   licenseUniqueNo = c.licenseUniqueNo,
                                   comLicenseRemID = cc.comLicenseRemID,
                                   comLicenseRem_Date = cc.comLicenseRem_Date,
                                   comLicenseRem_Remarks = cc.comLicenseRem_Remarks,
                                   comLicenseRem_Action = cc.comLicenseRem_Action,
                                   comLicenseRem_isActive = cc.comLicenseRem_isActive

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseNo;
                        dr["three"] = item.startDate.Value.ToShortDateString();
                        dr["four"] = "";
                        dr["five"] = item.comp_arabicName;
                        dr["six"] = item.commercialName;
                        dr["seven"] = item.endDate.Value.ToShortDateString();

                        dr["eight"] = item.licenseUniqueNo;
                        dr["nine"] = item.comLicenseRemID;
                        dr["ten"] = item.comLicenseRem_Date.Value.ToShortDateString();
                        dr["11"] = item.comLicenseRem_Remarks;
                        dr["12"] = item.comLicenseRem_Action;

                        if (item.comLicenseRem_isActive.Value)
                        {
                            dr["13"] = "غير مقفلة";
                        }
                        else
                        {
                            dr["13"] = "مقفلة";
                        }

                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                        dr["17"] = "";
                        dr["18"] = "";
                        dr["19"] = "";
                        dr["20"] = "";

                        dr["21"] = "";
                        dr["22"] = "";
                        dr["23"] = "";
                        dr["24"] = "";
                        dr["25"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesemployee.xsd");
                    //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeLicencesRemarks";
                    rv.ds = ds;
                    rv.title = "ملاحظات الرخص التجارية";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            int _transDefID = int.Parse(cmbtransDefID.SelectedValue.ToString());
            DateTime dd1 = DateTime.Parse(txtDate1.Text);
            DateTime dd2 = DateTime.Parse(txtDate2.Text);
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = from c3 in db.DocumentTypeDefinitions
                          join c4 in db.DocumentDefs
                          on c3.documentTypeDefID equals c4.documentTypeDefID
                          join c1 in db.Documents
                          on c4.documentDefID equals c1.documentDefID
                          join cc in db.DocumentTransactionTasks on c1.documentID equals cc.documentID
                          join cc1 in db.DocumentTransactions on cc.documentTransID equals cc1.documentTransID
                          join c2 in db.Employees
                           on c1.document_TypeID equals c2.employeeID
                          join c5 in db.EmployeeLicenseShops on c2.employeeID equals c5.employeeID into jj
                          from c5 in jj.DefaultIfEmpty()
                          join cc5 in db.LicensesShops on c5.licencesShopID equals cc5.licencesShopID into jj1
                          from cc5 in jj1.DefaultIfEmpty()
                          join c6 in db.CompanyLicences on cc5.companyLicencesID equals c6.companyLicencesID into jj2
                          from c6 in jj2.DefaultIfEmpty()
                          where c3.documentType_Code == "01"
                          && cc1.transDefID == _transDefID
                          && cc.documentTransTask_ApprovedDate >= dd1
                          && cc.documentTransTask_ApprovedDate <=dd2
                          select new
                          {
                              c3.documentTypeDefID,
                              c4.documentDefID,
                              companyLicencesID = c6 != null ? c6.companyLicencesID : -1,
                              c1.documentID,
                              c2.employeeID,
                              c3.documentType_Name,
                              cc.documentTransTaskID,
                              c4.documentDef_Name,
                              cc.documentTransTask_Approved,
                              cc.documentTransTask_ApprovedDate,
                              cc.documentTransTask_isClose,
                              cc.documentTransTask_isPaid,
                              c1.document_endDate,

                              c2.emp_namefullar,
                              c2.emp_civilID,
                              c2.emp_No,

                              c6.commercialName,
                              c5.employeeLink,
                              c6.licenseNo,
                              noOfDays = c4.documentDef_WarningDays.Value,
                              cc1.documentTrans_Amount
                          };
                var val1 = val.ToList();
                makePrintTable();
                foreach (var item in val)
                {
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.commercialName;
                    dr["seven"] = item.document_endDate.Value.ToShortDateString();
                    dr["eight"] = cmbtransDefID.Text;
                    dr["nine"] = item.documentTrans_Amount.Value.ToString();
                    dr["ten"] = item.documentTransTask_Approved;


                    dr["12"] = item.documentTransTask_ApprovedDate.Value.ToShortDateString();
                    if (item.documentTransTask_isClose.HasValue)
                    {
                        dr["13"] = "مغلقة";
                    }
                    else
                    {
                        dr["13"] = "غير مغلقة";
                    }



                    var vvv = (from xx in db.EmployeeSponsors
                               where xx.employeeID == item.employeeID
                               select xx).FirstOrDefault();

                    dr["14"] = item.emp_civilID;
                    dr["15"] = vvv.CompanyLicence.licenseNo;
                    dr["16"] = vvv.CompanyLicence.commercialName;
                    dr["17"] = "";
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";

                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeTransaction";
                rv.ds = ds;
                rv.title = "اجراءات الموظفين  : " + cmbtransDefID.Text + " من : " +
                    txtDate1.Text + "  الى : " + txtDate2.Text;
                rv.ShowDialog();   


            }// end of using

            
        }

        //private string getDocumentTransaction(int docID)
        //{
        //    string temp = "";

        //    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
        //    {
        //        var val = (from c in db.DocumentTransactionTasks
        //                   join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
        //                   join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
        //                   where c.documentID == docID
        //                   select new
        //                   {
        //                       c2.transDef_namea
        //                   }).ToList();

        //        foreach (var item in val)
        //        {
        //            temp =  item.transDef_namea;
        //        }
        //    }

        //    return temp;
        //}



    }
}
