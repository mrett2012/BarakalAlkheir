using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Definitions;
using BarakalAlkheir.Model;
using System.Linq;

namespace BarakalAlkheir.CarsM
{
    public partial class frmCarMainPage : Telerik.WinControls.UI.RadForm
    {
        public frmCarMainPage()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.ShowDialog();
        }

        private void frmCars_Click(object sender, EventArgs e)
        {
            frmCars frm = new frmCars();
            frm.ShowDialog();
        }

        private void btnLicencesRent_Click(object sender, EventArgs e)
        {
            frmRentACar frm = new frmRentACar();
            frm.ShowDialog();
        }

        private void btnLicencesRentPayment_Click(object sender, EventArgs e)
        {
            frmRentCarPayment frm = new frmRentCarPayment();
            frm.ShowDialog();
        }

        private void btnDetailsRentingLicenses_Click(object sender, EventArgs e)
        {
            frmRentingPaymentCarDetails frm = new frmRentingPaymentCarDetails();
            frm.ShowDialog();
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
        private void btnRentedLicences_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {

                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               where c.car_isAvailable == true
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand,
                                   carCustomer_rentNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   carCustomer_from = d.carCustomer_from,
                                   carCustomer_to = d.carCustomer_to ,
                                   carCustomerRent_RentalAmount = d.carCustomerRent_RentalAmount

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = item.carCustomer_rentNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.carCustomer_from.Value.ToShortDateString();
                        dr["eight"] = item.carCustomer_to.Value.ToShortDateString();
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = item.carCustomerRent_RentalAmount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentCar";
                    rv.ds = ds;
                    rv.title = "السيارات المأجرة";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNotRentedLicences_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.Cars
                               where !(from cc in db.Cars
                                       join d in db.CarCustomerRents
                                            on cc.carID equals d.carID
                                       where d.carCustomerRent_active == true
                                       select cc.carID).Contains(c.carID)
                                     && c.car_isAvailable == true
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand


                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = "";
                        dr["six"] = "";
                        dr["seven"] = "";
                        dr["eight"] = "";
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Car";
                    rv.ds = ds;
                    rv.title = "السيارات الغير مأجرة";
                    rv.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLicenceNotActive_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.Cars
                              where c.car_isAvailable == false
                              orderby c.car_Name
                              select new
                              {
                                  carID = c.carID,
                                  car_No = c.car_No,
                                  car_Name = c.car_Name,
                                  car_Model = c.car_Model,
                                  car_Brand = c.car_Brand
                              };

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = "";
                        dr["six"] = "";
                        dr["seven"] = "";
                        dr["eight"] = "";
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Car";
                    rv.ds = ds;
                    rv.title = "السيارات الغير فعالة";
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
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Now.Month;
                    int year = DateTime.Now.Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime lastDay = DateTime.Parse(day + "/" + month + "/" + year);

                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               where c.car_isAvailable == true
                                && d.carCustomer_to <= lastDay
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand,
                                   carCustomer_rentNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   carCustomer_from = d.carCustomer_from,
                                   carCustomer_to = d.carCustomer_to,
                                   carCustomerRent_RentalAmount = d.carCustomerRent_RentalAmount

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = item.carCustomer_rentNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.carCustomer_from.Value.ToShortDateString();
                        dr["eight"] = item.carCustomer_to.Value.ToShortDateString();
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = item.carCustomerRent_RentalAmount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentCar";
                    rv.ds = ds;
                    rv.title = "الايجار السيارات المنتهية";
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
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Parse(txtDate.Text).Month;
                    int year = DateTime.Parse(txtDate.Text).Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime lastDay = DateTime.Parse(day + "/" + month + "/" + year);

                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               where c.car_isAvailable == true
                                && d.carCustomer_to <= lastDay
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand,
                                   carCustomer_rentNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   carCustomer_from = d.carCustomer_from,
                                   carCustomer_to = d.carCustomer_to,
                                   carCustomerRent_RentalAmount = d.carCustomerRent_RentalAmount

                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = item.carCustomer_rentNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.carCustomer_from.Value.ToShortDateString();
                        dr["eight"] = item.carCustomer_to.Value.ToShortDateString();
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = item.carCustomerRent_RentalAmount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentCar";
                    rv.ds = ds;
                    rv.title = "الايجار السيارات المنتهية خلال شهر : " + txtDate.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRentCollected_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Parse(txtDate.Text).Month;
                    int year = DateTime.Parse(txtDate.Text).Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime firstDay = DateTime.Parse(MainClass.StartRentDay + "/" + month + "/" + year);
                    DateTime lastDay = firstDay.AddMonths(-1).AddDays(1);

                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               join ee in db.carCustomerRentDetails
                               on d.carCustomerRentID equals ee.carCustomerRentID
                               where ee.rd_fromDate >= lastDay
                                && ee.rd_fromDate <= firstDay
                                && ee.rd_isPaid == true
                                && c.car_isAvailable == true
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand,
                                   carCustomer_rentNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   carCustomer_from = d.carCustomer_from,
                                   carCustomer_to = d.carCustomer_to,
                                   carCustomerRent_RentalAmount = ee.rd_amount.Value

                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = item.carCustomer_rentNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.carCustomer_from.Value.ToShortDateString();
                        dr["eight"] = item.carCustomer_to.Value.ToShortDateString();
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = item.carCustomerRent_RentalAmount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentCar";
                    rv.ds = ds;
                    rv.title = "الايجار المحصل خلال شهر : " + txtDate.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmCarMainPage_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmCarMainPage_LoadData);
        }

        private async System.Threading.Tasks.Task frmCarMainPage_LoadData(object sender, EventArgs e)
        {
            try
            {
                txtDate.Text = DateTime.Now.ToShortDateString();
                await PublicClass.bindCarBrandAsync(cmbcar_Brand);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void btnRentNotCollected_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month = DateTime.Parse(txtDate.Text).Month;
                    int year = DateTime.Parse(txtDate.Text).Year;
                    int day = DateTime.DaysInMonth(year, month);

                    DateTime firstDay = DateTime.Parse(MainClass.StartRentDay + "/" + month + "/" + year);
                    DateTime lastDay = firstDay.AddMonths(-1).AddDays(1);

                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               join ee in db.carCustomerRentDetails
                               on d.carCustomerRentID equals ee.carCustomerRentID
                               where ee.rd_fromDate >= lastDay
                                && ee.rd_fromDate <= firstDay
                                && ee.rd_isPaid == false
                                && c.car_isAvailable == true
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand,
                                   carCustomer_rentNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   carCustomer_from = d.carCustomer_from,
                                   carCustomer_to = d.carCustomer_to,
                                   carCustomerRent_RentalAmount = ee.rd_amount.Value

                               }).ToList();

                    Gview.DataSource = val;
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = item.carCustomer_rentNo;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.carCustomer_from.Value.ToShortDateString();
                        dr["eight"] = item.carCustomer_to.Value.ToShortDateString();
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = item.carCustomerRent_RentalAmount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "rentCar";
                    rv.ds = ds;
                    rv.title = "الايجار الغير المحصل خلال شهر : " + txtDate.Text;
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilteredCompanyLicenses_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.Cars
                               orderby c.car_Name
                               select c).ToList();

                    if (this.chkChooseModel.Checked)
                    {
                        val = val.Where(cc => cc.car_Model == txtcar_Model.Text).ToList();
                    }
                    if (chkchooseBrand.Checked)
                    {
                        val = val.Where(cc => cc.car_Brand == cmbcar_Brand.Text).ToList();
                    }
                    if (chkChooseStatus.Checked)
                    {
                        val = val.Where(cc => cc.car_isAvailable == chkisActive.Checked).ToList();
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     carID = c.carID,
                                     car_No = c.car_No,
                                     car_Name = c.car_Name,
                                     car_Model = c.car_Model,
                                     car_Brand = c.car_Brand
                                 };
                    Gview.DataSource = newVal.ToList();
                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carID;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = "";
                        dr["six"] = "";
                        dr["seven"] = "";
                        dr["eight"] = "";
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Car";
                    rv.ds = ds;
                    rv.title = "السيارات الغير مأجرة";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCarSupplier_Click(object sender, EventArgs e)
        {
            Definitions.frmCarSuppliers frm = new frmCarSuppliers();
            frm.ShowDialog();
        }

        private void btnBuyACar_Click(object sender, EventArgs e)
        {
            CarsM.frmBuyACar frm = new frmBuyACar();
            frm.ShowDialog();
        }

        private void btnBuyACarPayment_Click(object sender, EventArgs e)
        {
            frmBuyCarPayment frm = new frmBuyCarPayment();
            frm.ShowDialog();
        }

        private void btnDetailsBuyingCars_Click(object sender, EventArgs e)
        {
            frmBuyingPaymentCarDetails frm = new frmBuyingPaymentCarDetails();
            frm.ShowDialog();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            CarsM.frmPaymentReport frm = new frmPaymentReport();
            frm.ShowDialog();
        }

        private void btnBoughtCarReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {

                    var val = (from c in db.Cars join d in db.CarSupplierRents
                                on c.carID equals d.carID
                               where c.car_isAvailable == true
                               && d.carSupplierRent_isactive==true
                               orderby c.car_Name
                               select new
                               {
                                   carID = c.carID,
                                   car_No = c.car_No,
                                   car_Name = c.car_Name,
                                   car_Model = c.car_Model,
                                   car_Brand = c.car_Brand,
                                   carCustomer_rentNo = d.carRentNo,
                                   customer_Name = d.CarSupllier.carSuppliser_Name,
                                   carCustomer_from = d.carSupplierRent_from,
                                   carCustomer_to = d.carSupplierRent_to,
                                   carCustomerRent_RentalAmount = d.carSupplierRent_MonthlyAmount,
                                   carSupplierRent_firstPaymentAmount = d.carSupplierRent_firstPaymentAmount,
                                   carSupplierRent_lastPaymentAmount = d.carSupplierRent_lastPaymentAmount

                               }).ToList();

                    Gview.DataSource = val;

                    makePrintTable();
                    foreach (var item in val)
                    {
                        DataRow dr = dt.NewRow();

                        dr["one"] = item.carSupplierRent_lastPaymentAmount;
                        dr["two"] = item.car_Model;
                        dr["three"] = item.car_No;
                        dr["four"] = item.car_Name;
                        dr["five"] = item.carSupplierRent_firstPaymentAmount;
                        dr["six"] = item.customer_Name;
                        dr["seven"] = item.carCustomer_from.Value.ToShortDateString();
                        dr["eight"] = item.carCustomer_to.Value.ToShortDateString();
                        dr["nine"] = item.car_Brand;
                        dr["ten"] = item.carCustomerRent_RentalAmount;


                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    //ds.WriteXmlSchema("Licencesrent.xsd");
                    ////MessageBox.Show("dd");
                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "buyCar";
                    rv.ds = ds;
                    rv.title = "السيارات التي تم شرائها";
                    rv.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
