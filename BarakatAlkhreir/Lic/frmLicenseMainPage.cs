using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Model;
using System.Linq;

namespace BarakalAlkheir.Lic
{
    public partial class frmLicenseMainPage : Telerik.WinControls.UI.RadForm
    {
        public frmLicenseMainPage()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Definitions.frmCustomers frm = new Definitions.frmCustomers();
            frm.ShowDialog();
        }

        private void btnLicencesRent_Click(object sender, EventArgs e)
        {
            Lic.frmRentALicense frm = new frmRentALicense();
            frm.ShowDialog();
        }

        private void btnLicencesRentPayment_Click(object sender, EventArgs e)
        {
            frmRentLicensePayment frm = new frmRentLicensePayment();
            frm.ShowDialog();
        }

        private void btnRentedLicences_Click(object sender, EventArgs e)
        {           
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {

                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               where c.isActive == true && d.rentLicense_active==true
                               orderby c.commercialName
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   rentLicense_to = d.rentLicense_to,
                                   rentLicense_from = d.rentLicense_from ,
                                   Mohafaza = c.Mohafaza

                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = item.rentLicenseNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["eight"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"]="";


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentLicence";
                    rv.ds = ds;
                    rv.title = "الرخص المأجرة";
                    rv.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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


        }

        private void btnLicenceNotActive_Click(object sender, EventArgs e)
        {
           
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.CompanyLicences
                              where c.isActive == false
                              && c.isMainLicences == false
                              orderby c.commercialName
                              select new
                              {
                                  companyLicencesID = c.companyLicencesID,
                                  licenseLinkCode = c.licenseLinkCode,
                                  licenseNo = c.licenseNo,
                                  commercialName = c.commercialName,
            
                                  Mohafaza = c.Mohafaza,

                                  startDate = c.startDate,
                                   endDate = c.endDate,
                                   licenseUniqueNo = c.licenseUniqueNo
                              };

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = "";
                        dr["six"] = "";
                        dr["seven"] = item.startDate.Value.ToShortDateString();
                        dr["eight"] = item.endDate.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = "";


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Licence";
                    rv.ds = ds;
                    rv.title = "الرخص الغير فعالة";
                    rv.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLicensesCloses_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.CompanyLicences
                              where c.isClose == true
                              && c.isMainLicences == false
                              select new
                              {
                                  companyLicencesID = c.companyLicencesID,
                                  licenseLinkCode = c.licenseLinkCode,
                                  licenseNo = c.licenseNo,
                                  commercialName = c.commercialName,
                                  Mohafaza = c.Mohafaza,
                                  startDate = c.startDate,
                                  endDate = c.endDate,
                                  licenseUniqueNo = c.licenseUniqueNo
                              };

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = "";
                        dr["six"] = "";
                        dr["seven"] = item.startDate.Value.ToShortDateString();
                        dr["eight"] = item.endDate.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = "";


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Licence";
                    rv.ds = ds;
                    rv.title = "الرخص المغلقة";
                    rv.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRentWillBeEnd_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Now.Month;
                    int year = DateTime.Now.Year;
                    int day = DateTime.DaysInMonth(year,month);

                    DateTime lastDay = DateTime.Parse(day + "/" + month + "/" + year);

                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               where c.isActive == true && d.rentLicense_active ==true
                                && d.rentLicense_to <= lastDay
                                orderby c.commercialName
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   rentLicense_to = d.rentLicense_to,
                                   rentLicense_from = d.rentLicense_from,
                                   Mohafaza = c.Mohafaza

                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = item.rentLicenseNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["eight"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = "";


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentLicence";
                    rv.ds = ds;
                    rv.title = "ايجار الرخص المنتهية";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Parse(txtDate.Text).Month;
                    int year = DateTime.Parse(txtDate.Text).Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime lastDay = DateTime.Parse(day + "/" + month + "/" + year);

                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               where c.isActive == true
                                && d.rentLicense_to <= lastDay
                                && d.rentLicense_active == true
                                orderby c.commercialName
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   rentLicense_from = d.rentLicense_from,
                                   rentLicense_to = d.rentLicense_to,
                                   Mohafaza = c.Mohafaza

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = item.rentLicenseNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["eight"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = "";


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentLicence";
                    rv.ds = ds;
                    rv.title = "ايجار الرخص المنتهية خلال شهر : " + txtDate.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmLicenseMainPage_Load(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString();

            cmbGovernorate.Enabled = false;
            cmbGovernorateArea.Enabled = false;

            cmbLicenseActivity.Enabled = false;
            cmbMandoub.Enabled = false;
            PanelChooseStatus.Enabled = false;
            btnFindCustomer1.Enabled = false;
            PublicClass.bindGovernorate(cmbGovernorate);
            PublicClass.bindMandoub(cmbMandoub);
            PublicClass.bindLicenseActivity(cmbLicenseActivity);
            cmbMandoub.SelectedIndex = 0;
            cmbGovernorate.SelectedIndex = 0;
            cmbLicenseActivity.SelectedIndex = 0;

            if (!MainClass.isSuperUser)
            {
                if (MainFile.isDeleteSecurity("تقارير الرخص التجارية"))
                {
                    
                }
                else
                {
                    
                }
            }
        }

        private void btnRentCollected_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Parse(txtDate.Text).Month;
                    int year = DateTime.Parse(txtDate.Text).Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime firstDay = DateTime.Parse(MainClass.StartRentDay + "/" + month + "/" + year);
                    DateTime lastDay = firstDay.AddMonths(-1).AddDays(1);

                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               join ee in db.RentLicensesDetails on d.rentLicenseID equals ee.rentLicenseID
                               join eee in db.RentLicensesPayments on ee.rentLicencesDetailsID equals eee.rentLicencesDetailsID
                               where ee.rd_fromDate >= lastDay
                                && ee.rd_fromDate <= firstDay
                                && ee.rd_isPaid == true
                               select new
                               {
                                   rentLicPay_VouNo = eee.rentLicPay_VouNo,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   rentLicense_from = d.rentLicense_from,
                                   rentLicense_to = d.rentLicense_to,
                                   rd_amount = ee.rd_amount.Value,
                                   Mohafaza = c.Mohafaza ,
                                   
                                   

                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.rentLicPay_VouNo;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = item.rentLicenseNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["eight"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = item.rd_amount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentLicence";
                    rv.ds = ds;
                    rv.title = "ايجار الرخص المنتهية خلال شهر : " + txtDate.Text;
                    rv.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRentNotCollected_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Parse(txtDate.Text).Month;
                    int year = DateTime.Parse(txtDate.Text).Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime firstDay = DateTime.Parse(MainClass.StartRentDay + "/" + month + "/" + year);
                    DateTime lastDay = firstDay.AddMonths(-1).AddDays(1);

                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               join ee in db.RentLicensesDetails
                               on d.rentLicenseID equals ee.rentLicenseID
                               where ee.rd_fromDate >= lastDay
                                && ee.rd_fromDate <= firstDay
                                && ee.rd_isPaid == false                              
                               select new
                               {
                                   الرمز = c.companyLicencesID,
                                   كود_الربط = c.licenseLinkCode,
                                   رقم_الرخصة = c.licenseNo,
                                   الاسم_التجاري = c.commercialName,
                                   رقم_الايجار = d.rentLicenseNo,
                                   العميل = d.Customer.customer_Name,
                                   من = d.rentLicense_from,
                                   الى = d.rentLicense_to,
                                   المبلغ = ee.rd_amount.Value

                               }).ToList();

                    Gview.DataSource = val;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //try
            //{
            //    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            //    {
            //        int month = DateTime.Parse(txtDate.Text).Month;
            //        int year = DateTime.Parse(txtDate.Text).Year;
            //        int day = DateTime.DaysInMonth(year, month);

            //        DateTime firstDay = DateTime.Parse("10/" + month + "/" + year);
            //        DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            //        var val = (from c in db.CompanyLicences
            //                   join d in db.RentLicenses
            //                   on c.companyLicencesID equals d.companyLicencesID
            //                   from j in db.RentLicensesPayments.Where(j => j.rentLicenseID == d.rentLicenseID &&
            //                       j.rentLicPay_ForDate>=firstDay
            //                       && j.rentLicPay_ForDate<=lastDay && j.rentLicPay_Amount<=0).DefaultIfEmpty()
            //                   where c.isActive == true
            //                   && d.rentLicense_from <= lastDay &&
            //                       d.rentLicense_to >= firstDay
            //                   select new
            //                   {
            //                      الرمز = c.companyLicencesID,
            //                      كود_الربط = c.licenseLinkCode,
            //                      رقم_الرخصة = c.licenseNo,
            //                      الاسم_التجاري = c.commercialName,
            //                      رقم_الايجار = d.rentLicenseNo,
            //                      العميل = d.Customer.customer_Name,
            //                      من = d.rentLicense_from,
            //                      الى = d.rentLicense_to,
            //                       مبلغ_الايجار = c.RentLicenses.Sum(g => g.rentLicense_RentalAmount),
            //                       الايجار_المحصل = j.rentLicPay_Amount

            //                   }).ToList();

            //        Gview.DataSource = val;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnAllMoneyRental_Click(object sender, EventArgs e)
        {

        }

        private void btnNotRentedLicences_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                                         where !(from cc in db.CompanyLicences
                                                 join d in db.RentLicenses
                                                      on cc.companyLicencesID equals d.companyLicencesID
                                                 where d.rentLicense_active == true
                                                 select cc.companyLicencesID).Contains(c.companyLicencesID)
                                               && c.isActive == true
                                         && c.isMainLicences == false
                                         orderby c.commercialName
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                 licenseLinkCode = c.licenseLinkCode,
                                 licenseNo = c.licenseNo,
                                 commercialName = c.commercialName,
                                 Mohafaza = c.Mohafaza,
                                   startDate = c.startDate,
                                   endDate = c.endDate,
                                   licenseUniqueNo = c.licenseUniqueNo


                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] ="";
                        dr["six"] = item.licenseUniqueNo;
                        dr["seven"] = item.startDate.Value.ToShortDateString();
                        dr["eight"] = item.endDate.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = "";


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Licence";
                    rv.ds = ds;
                    rv.title = "الرخص الغير مأجرة";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMissingDestination_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.CompanyLicences
                              where c.isActive == true &&
                              c.noOfSeats> 0
                              orderby c.commercialName
                              select new
                              {
                                  companyLicencesID = c.companyLicencesID,
                                  licenseUniqueNo = c.licenseUniqueNo,
                                  licenseLinkCode = c.licenseLinkCode,
                                  licenseNo = c.licenseNo,
                                  commercialName = c.commercialName,
                                  startDate = c.startDate,
                                  endDate = c.endDate,
                                  noOfSeatsFree = c.noOfSeats,
                                  noOfSeatsUsed = c.CompanyLicenseDesignations.Sum(cc => cc.noOfSeats),
                                  Mohafaza = c.Mohafaza
                              };

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();
                        dr["one"] = item.companyLicencesID;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = item.noOfSeatsFree;
                        dr["six"] = item.noOfSeatsUsed;
                        dr["seven"] = item.startDate.Value.ToShortDateString();
                        dr["eight"] = item.endDate.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = item.licenseUniqueNo;
                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "LicenceSeat";
                    rv.ds = ds;
                    rv.title = "عدد الأماكن يجب ادخالها";
                    rv.ShowDialog();

                }

                //if (Gview.Rows.Count > 0)
                //{
                //    int tnoS = 0;
                //    int tuos = 0;
                //    //int tlos = 0;

                //    for (int i = 0; i < Gview.Rows.Count; i++)
                //    {
                //        int noS1 = 0;
                //        int uos2 = 0;
                //        //int los3 = 0;

                //        if (Gview.Rows[i].Cells[6].Value != null)
                //        {
                //            noS1 = int.Parse(Gview.Rows[i].Cells[6].Value.ToString());
                //            tnoS += noS1;
                //        }
                //        if (Gview.Rows[i].Cells[7].Value != null)
                //        {
                //            uos2 = int.Parse(Gview.Rows[i].Cells[7].Value.ToString());
                //            tuos += uos2;
                //        }
                //        //if (Gview.Rows[i].Cells[8].Value != null)
                //        //{
                //        //    los3 = int.Parse(Gview.Rows[i].Cells[8].Value.ToString());
                //        //    tlos += los3;
                //        //}
                //    }

                //    lbl1.Text = tnoS.ToString();
                //    lbl2.Text = tuos.ToString();
                //    //lbl3.Text = tlos.ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkchooseMandoub_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkchooseMandoub.Checked)
            {
                cmbMandoub.Enabled = true;
            }
            else
            {
                cmbMandoub.Enabled = false;
            }
        }

        private void chkChooseGovernorate_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkChooseGovernorate.Checked)
            {
                cmbGovernorate.Enabled = true;
            }
            else
            {
                cmbGovernorate.Enabled = false;
                cmbGovernorateArea.Enabled = false;
            }
        }

        private void chkChooseGovernorateArea_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkChooseGovernorateArea.Checked)
            {
                if (chkChooseGovernorate.Checked)
                {
                    cmbGovernorateArea.Enabled = true;
                }
                else
                {
                    cmbGovernorateArea.Enabled = false;
                }
            }
            else
            {
                cmbGovernorateArea.Enabled = false;
            }
        }

        private void chkChooseStatus_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkChooseStatus.Checked)
            {
                PanelChooseStatus.Enabled = true;
            }
            else
            {
                PanelChooseStatus.Enabled = false;
            }
        }

        private void cmbGovernorate_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (chkChooseGovernorate.Checked)
            {
                int _governorateID = int.Parse(cmbGovernorate.SelectedValue.ToString());
                PublicClass.bindGovernorateArea(cmbGovernorateArea, _governorateID);
                cmbGovernorateArea.SelectedIndex = 0;
            }
        }

        private void btnFilteredCompanyLicenses_Click(object sender, EventArgs e)
        {
            try
            {
                lbl1.Text = "";
                lbl2.Text = "";
                lbl3.Text = "";
                string title = "";
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = new List<CompanyLicence>();
                    if (chkChooseCustomer.Checked)
                    {
                        int custID = int.Parse(lblCustomerID1.Text);

                        val = (from c in db.CompanyLicences
                                          join d in db.RentLicenses on c.companyLicencesID equals d.companyLicencesID
                                          where d.rentLicense_active == true &&
                                          d.customerID==custID
                                          select c).ToList();
                                          //{
                                          //    companyLicencesID = c.companyLicencesID,
                                          //    licenseLinkCode = c.licenseLinkCode,
                                          //    licenseNo = c.licenseNo,
                                          //    licenseUniqueNo = c.licenseUniqueNo,
                                          //    commercialName = c.commercialName,
                                          //    Mohafaza = c.Mohafaza,
                                          //    Mantaqa = c.Mantaqa,
                                          //    isActive = c.isActive,
                                          //    isMainLicences = c.isMainLicences,
                                          //    isClose = c.isClose,
                                          //    startDate = c.startDate,
                                          //    endDate = c.endDate
                                          //};

                        title += " حسب العميل: " + this.txtCustomerName1.Text + " ";
                    }
                    else
                    {
                        val = (from c in db.CompanyLicences
                                   select c).ToList();

                        if (chkchooseMandoub.Checked)
                        {
                            int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                            val = val.Where(cc => cc.mandoubID == mID).ToList();
                            title += " المندوب: " + cmbMandoub.Text + " ";

                        }
                        if (this.chkChooseLicenseActivity.Checked)
                        {
                            int laID = int.Parse(this.cmbLicenseActivity.SelectedValue.ToString());
                            val = val.Where(cc => cc.licenseActivityID == laID).ToList();
                            title += " النشاط: " + cmbLicenseActivity.Text + " ";

                        }
                        if (chkChooseGovernorate.Checked)
                        {
                            int GovID = int.Parse(cmbGovernorate.SelectedValue.ToString());
                            val = val.Where(cc => cc.governorateID == GovID).ToList();

                            title += " المحافظة: " + cmbGovernorate.Text + " ";

                            if (chkChooseGovernorateArea.Checked)
                            {
                                int govAreaID = int.Parse(cmbGovernorateArea.SelectedValue.ToString());
                                val = val.Where(cc => cc.goverareaID == govAreaID).ToList();
                                title += "- المنطقة: " + cmbGovernorateArea.Text + " ";
                            }
                        }
                        if (chkChooseStatus.Checked)
                        {
                            if (chkisActive.Checked)
                            {
                                val = val.Where(cc => cc.isActive == chkisActive.Checked).ToList();
                            }
                            if (chkisClose.Checked)
                            {
                                val = val.Where(cc => cc.isClose == chkisClose.Checked).ToList();
                            }
                            if (chkisMainLicences.Checked)
                            {
                                val = val.Where(cc => cc.isMainLicences == chkisMainLicences.Checked).ToList();
                                title += "- الرخصة الرئيسية";
                            }
                        }

                    }

                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     licenseUniqueNo = c.licenseUniqueNo,
                                     commercialName = c.commercialName,
                                     Mohafaza = c.Mohafaza,
                                     Mantaqa = c.Mantaqa,
                                     isActive = c.isActive,
                                     isMainLicences = c.isMainLicences,
                                     isClose = c.isClose,
                                     startDate = c.startDate,
                                     endDate = c.endDate
                                 };
                    Gview.DataSource = newVal.ToList();

                    makePrintTable();
                    foreach (var item in newVal.ToList())
                    {
                        DataRow dr = dt.NewRow();

                        if (item.isActive.Value)
                        {
                            dr["one"] = "فعالة";
                        }
                        else
                        {
                            dr["one"] = "غير فعالة";
                        }
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        if (item.isClose.Value)
                        {
                            dr["five"] = "مغلقة";
                        }
                        else
                        {
                            dr["five"] = "غير مغلقة";
                        }

                        dr["six"] = item.licenseUniqueNo;
                        dr["seven"] = item.startDate.Value.ToShortDateString();
                        dr["eight"] = item.endDate.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = item.Mantaqa;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "LicenceFilter";
                    rv.ds = ds;
                    rv.title = title;
                    rv.ShowDialog();

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDetailsRentingLicenses_Click(object sender, EventArgs e)
        {
            frmRentingPaymentDetails frm = new frmRentingPaymentDetails();
            frm.ShowDialog();
        }

        private void Gview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Gview.CurrentRow.Cells[0].Value != null)
            {
                int LID = -1;
                if (int.TryParse(Gview.CurrentRow.Cells[0].Value.ToString(), out LID))
                {
                    Sponsor.frmCompanyLicences frm = new Sponsor.frmCompanyLicences();
                    frm._companyLicencesID = LID;
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
                        int LID = -1;
                        if (int.TryParse(Gview.SelectedRows[0].Cells[0].Value.ToString(), out LID))
                        {
                            Sponsor.frmCompanyLicences frm = new Sponsor.frmCompanyLicences();
                            frm._companyLicencesID = LID;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }

        private void btnAvailableDesignation_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.CompanyLicences
                              where c.isActive == true &&
                              c.noOfSeats > 0
                              orderby c.commercialName
                              select new
                              {
                                  companyLicencesID = c.companyLicencesID,
                                  licenseUniqueNo = c.licenseUniqueNo,
                                  licenseLinkCode = c.licenseLinkCode,
                                  licenseNo = c.licenseNo,
                                  commercialName = c.commercialName,
                                  startDate = c.startDate,
                                  endDate = c.endDate,
                                  noOfSeatsFree = c.noOfSeats,
                                  noOfSeatsUsed = c.EmployeeSponsors.Count(p => p.Employee.emp_active == true),
                                  Left = c.noOfSeats - c.EmployeeSponsors.Count(p => p.Employee.emp_active == true),
                                  Mohafaza = c.Mohafaza
                              };

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.Left;
                        dr["two"] = item.licenseLinkCode;
                        dr["three"] = item.licenseNo;
                        dr["four"] = item.commercialName;
                        dr["five"] = item.noOfSeatsFree;
                        dr["six"] = item.noOfSeatsUsed;
                        dr["seven"] = item.startDate.Value.ToShortDateString();
                        dr["eight"] = item.endDate.Value.ToShortDateString();
                        dr["nine"] = item.Mohafaza;
                        dr["ten"] = item.licenseUniqueNo;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "LicenceSeatLeft";
                    rv.ds = ds;
                    rv.title = "الأماكن المتاحة";
                    rv.ShowDialog();
                }
                
                //if (Gview.Rows.Count > 0)
                //{
                //    int tnoS = 0;
                //    int tuos = 0;
                //    int tlos = 0;

                //    for (int i = 0; i < Gview.Rows.Count; i++)
                //    {
                //        int noS1 = 0;
                //        int uos2 = 0;
                //        int los3 = 0;

                //        if (Gview.Rows[i].Cells[6].Value != null)
                //        {
                //            noS1 = int.Parse(Gview.Rows[i].Cells[6].Value.ToString());
                //            tnoS += noS1;
                //        }
                //        if (Gview.Rows[i].Cells[7].Value != null)
                //        {
                //            uos2 = int.Parse(Gview.Rows[i].Cells[7].Value.ToString());
                //            tuos += uos2;
                //        }
                //        if (Gview.Rows[i].Cells[8].Value != null)
                //        {
                //            los3 = int.Parse(Gview.Rows[i].Cells[8].Value.ToString());
                //            tlos += los3;
                //        }
                //    }

                //    lbl1.Text = tnoS.ToString();
                //    lbl2.Text = tuos.ToString();
                //    lbl3.Text = tlos.ToString();
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateGViewForUsedLicenses()
        {
            Gview.ReadOnly = false;
            if (Gview.Rows.Count > 0)
            {
                for (int i = 0; i < Gview.Rows.Count; i++)
                {
                    if (Gview.Rows[i].Cells[0].Value != null)
                    {
                        int ID = int.Parse(Gview.Rows[i].Cells[0].Value.ToString());
                        Gview.Rows[i].Cells[7].Value = getNoOFUsedLicenses(ID).ToString();
                    }
                }
            }
        }

        private int getNoOFUsedLicenses(int _companyLicencesID)
        {
            int temp = 0;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.EmployeeSponsors
                           where c.companyLicencesID == _companyLicencesID
                           select c).ToList();
                temp = val.Count;
            }

            return temp;
        }

        private void btnLicencesRemarks_Click(object sender, EventArgs e)
        {
            Sponsor.frmCompanyLicencesRemarks frm = new Sponsor.frmCompanyLicencesRemarks();
            frm.ShowDialog();
        }

        private void btnPaymentReports_Click(object sender, EventArgs e)
        {
            frmPaymentReport frm = new frmPaymentReport();
            frm.ShowDialog();
        }

        private void btnPrintCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.Customers
                               where c.customer_active == true
                               select c).ToList();
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.customer_CivilID;
                        dr["two"] = item.customer_Name;
                        dr["three"] = item.customer_CivilID;
                        dr["four"] = item.Nationality.natio_namea;
                        dr["five"] = item.customer_phoneNumber1;
                        dr["six"] = item.customer_phoneNumber2;
                        dr["seven"] = "";
                        dr["eight"] = "";
                        dr["nine"] = "";
                        dr["ten"] = "";

                        dt.Rows.Add(dr);
                    }
                    

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "CustomersL";
                    rv.ds = ds;
                    rv.title = "العملاء";
                    rv.ShowDialog();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void chkChooseLicenseActivity_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (this.chkChooseLicenseActivity.Checked)
            {
                this.cmbLicenseActivity.Enabled = true;
            }
            else
            {
                cmbLicenseActivity.Enabled = false;
            }
        }

        private void chkChooseCustomer_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (this.chkChooseCustomer.Checked)
            {
                this.btnFindCustomer1.Enabled = true;
            }
            else
            {
                btnFindCustomer1.Enabled = false;
            }
        }

        private void btnFindCustomer1_Click(object sender, EventArgs e)
        {
            Definitions.frmFindCustomers frm = new Definitions.frmFindCustomers();
            frm.ShowDialog();
            if (frm.found)
            {
               lblCustomerID1.Text = frm.cID.ToString();
                txtCustomerName1.Text = frm.cName;
            }
        }

 



    }
}
