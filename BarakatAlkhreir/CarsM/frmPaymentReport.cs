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
namespace BarakalAlkheir.CarsM
{
    public partial class frmPaymentReport : Telerik.WinControls.UI.RadForm
    {
        DataTable dt = new DataTable();
        public frmPaymentReport()
        {
            InitializeComponent();
        }

        private void btnFilteredCompanyLicenses_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = DateTime.Parse(txtDate1.Text).Month;
                    int year1 = DateTime.Parse(txtDate1.Text).Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);

                    DateTime firstDay1 = DateTime.Parse(MainClass.StartRentDay + "/" + month1 + "/" + year1);
                    DateTime lastDay1 = firstDay1.AddMonths(1).AddDays(-1);


                    int month2 = DateTime.Parse(txtDate2.Text).Month;
                    int year2 = DateTime.Parse(txtDate2.Text).Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);

                    DateTime firstDay2 = DateTime.Parse(MainClass.StartRentDay + "/" + month2 + "/" + year2);
                    DateTime lastDay2 = firstDay2.AddMonths(1).AddDays(-1);



                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               join ee in db.CarCustomerRentPayments
                               on d.carCustomerRentID equals ee.carCustomerRentID
                               join ee1 in db.Employees on ee.carCustomerPayment_mandoubID equals ee1.employeeID
                               where d.carCustomerRent_active==true
                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName =c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = ee1.emp_namefullar,
                                   mandoubID = ee1.employeeID,
                                   rentLicense_from = d.carCustomer_from,
                                   rentLicense_to = d.carCustomer_to,
                                   ForDate = ee.carCustomerPayment_ForDate,
                                   PaymentDate = ee.carCustomerPayment_PaymentDate,
                                   Amount = ee.carCustomerPayment_amount.Value,
                                   rentLicPay_VouNo = ee.carCustomerPayment_VoucherNo

                               }).ToList();

                    if (chkchooseMandoub.Checked)
                    {
                        int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                        val = val.Where(cc => cc.mandoubID == mID).ToList();

                        title += "المندوب : " + cmbMandoub.Text +" - ";
                    }


                    if (chkDateFrom.Checked && chkDateTo.Checked)
                    {

                        val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay2).ToList();

                        title += "من : "+txtDate1.Text +" الى : " + txtDate2.Text;

                    }
                    else
                    {
                        if (chkDateFrom.Checked)
                        {
                            val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay1).ToList();

                            title += "لشهر : " + txtDate1.Text;
                        }
                    }



                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();

                        title += "سيارة : " + txtShopCommercialName.Text;
                    }
                    

                    var newVal = from c in val
                                 select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.goverareaID,
                                   goverareaID = c.governorateID,
                                   governorateName = c.governorateName,
                                   goverareaName = c.governorateName,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = c.rentLicenseNo,
                                   customer_Name = c.customer_Name,
                                   mandoub = c.mandoub,
                                   mandoubID = c.mandoubID,
                                   rentLicense_from = c.rentLicense_from,
                                   rentLicense_to = c.rentLicense_to,
                                   ForDate = c.ForDate.Value.ToString("MM - yyyy"),
                                   PaymentDate = c.PaymentDate,
                                   Amount = c.Amount,
                                   rentLicPay_VouNo =  c.rentLicPay_VouNo

                               };
                    
                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"]=item.goverareaID.ToString();
                        dr["governorateName"]=item.governorateName;
                        dr["goverareaName"]=item.goverareaName;

                        dr["licenseLinkCode"]=item.licenseLinkCode;
                        dr["licenseNo"]=item.licenseNo;
                        dr["commercialName"]=item.commercialName;
                        dr["rentLicenseNo"]=item.rentLicenseNo;
                        dr["customer_Name"]=item.customer_Name;

                        dr["mandoub"]=item.mandoub;
                        dr["mandoubID"]=item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"]=item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"]=item.Amount;
                        dr["extra1"]=item.rentLicPay_VouNo;
                        dr["extra2"]="";
                        dr["extra3"]="";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Paymentc";
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

        private void cTable()
        {
            dt = new DataTable();
            dt.Columns.Add("companyLicencesID");
            dt.Columns.Add("governorateID");
            dt.Columns.Add("goverareaID");
            dt.Columns.Add("governorateName");
            dt.Columns.Add("goverareaName");

            dt.Columns.Add("licenseLinkCode");
            dt.Columns.Add("licenseNo");
            dt.Columns.Add("commercialName");
            dt.Columns.Add("rentLicenseNo");
            dt.Columns.Add("customer_Name");

            dt.Columns.Add("mandoub");
            dt.Columns.Add("mandoubID");
            dt.Columns.Add("rentLicense_from");
            dt.Columns.Add("rentLicense_to");
            dt.Columns.Add("ForDate");

            dt.Columns.Add("PaymentDate");
            dt.Columns.Add("Amount");
            dt.Columns.Add("extra1");
            dt.Columns.Add("extra2");
            dt.Columns.Add("extra3");

        }


        private void frmPaymentReport_Load(object sender, EventArgs e)
        {
            try
            {
                //PublicClass.bindGovernorate(cmbGovernorate);
                PublicClass.bindMandoub(cmbMandoub);
                cmbMandoub.SelectedIndex = 0;
                //cmbGovernorate.SelectedIndex = 0;
                txtDate1.Text = DateTime.Now.ToShortDateString();
                txtDate2.Text = DateTime.Now.ToShortDateString();

                this.txtdatefrom.Text = DateTime.Now.ToShortDateString();
                this.txtdateto.Text = DateTime.Now.ToShortDateString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void chkDateFrom_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkDateFrom.Checked)
            {
                txtDate1.Enabled = true;
            }
            else
            {
                txtDate1.Enabled = false;
            }
        }

        private void chkDateTo_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkDateTo.Checked)
            {
                txtDate2.Enabled = true;
            }
            else
            {
                txtDate2.Enabled = false;
            }
        }

        private void chkchooseMandoub_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkchooseMandoub.Checked)
            {
                this.cmbMandoub.Enabled = true;
            }
            else
            {
                cmbMandoub.Enabled = false;
            }
        }

        private void chkChooseLicences_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkChooseLicences.Checked)
            {
                btnFindCar.Enabled = true;
                btnFindCar1.Enabled = true;
            }
            else
            {
                btnFindCar.Enabled = false;
                btnFindCar1.Enabled = true;
            }
        }

        private void btnFindShop_Click(object sender, EventArgs e)
        {
            //Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, true);
            //frm.isCurrentRentedLicencesInclude = true;
            //frm.ShowDialog();
            //if (frm.found)
            //{
            //    int ID2 = frm.licID;
            //    txtShopNo.Text = frm.licNO;
            //    txtShopCommercialName.Text = frm.licName;
            //    lblCarID.Text = ID2.ToString();
                
            //}

            CarsM.frmfindCars frm = new frmfindCars(true);
            frm.isCurrentRentedCar = true;
            frm.ShowDialog();
            if (frm.found)
            {
                //carID = frm.carID;
                txtShopNo.Text = frm.carNO;
                txtShopCommercialName.Text = frm.carName;
                lblCarID.Text = frm.carID.ToString();
            }
        }

        private void btnNotPaid_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = DateTime.Parse(txtDate1.Text).Month;
                    int year1 = DateTime.Parse(txtDate1.Text).Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);

                    DateTime firstDay1 = DateTime.Parse(MainClass.StartRentDay + "/" + month1 + "/" + year1);
                    DateTime lastDay1 = firstDay1.AddMonths(1).AddDays(-1);


                    int month2 = DateTime.Parse(txtDate2.Text).Month;
                    int year2 = DateTime.Parse(txtDate2.Text).Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);

                    DateTime firstDay2 = DateTime.Parse(MainClass.StartRentDay + "/" + month2 + "/" + year2);
                    DateTime lastDay2 = firstDay2.AddMonths(1).AddDays(-1);


                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents on c.carID equals d.carID
                               join ee in db.carCustomerRentDetails on d.carCustomerRentID equals ee.carCustomerRentID
                               where ee.rd_isPaid==false && d.carCustomerRent_active.Value==true
                               orderby ee.rd_fromDate
                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = "",
                                   mandoubID = "",
                                   rentLicense_from = d.carCustomer_from,
                                   rentLicense_to = d.carCustomer_to,
                                   ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rd_toDate,
                                   Amount = ee.rd_amount.Value,
                                   rentLicPay_VouNo = ""

                               }).ToList();

                    //if (chkchooseMandoub.Checked)
                    //{
                    //    int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                    //    val = val.Where(cc => cc.mandoubID == mID).ToList();

                    //    title += "المندوب : " + cmbMandoub.Text + " - ";
                    //}


                    if (chkDateFrom.Checked && chkDateTo.Checked)
                    {

                        val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay2).ToList();

                        title += "من : " + txtDate1.Text + " الى : " + txtDate2.Text;

                    }
                    else
                    {
                        if (chkDateFrom.Checked)
                        {
                            val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay1).ToList();

                            title += "لشهر : " + txtDate1.Text;
                        }
                    }

                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();

                        title += "سيارة : " + txtShopCommercialName.Text;
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate,
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate.Value.ToShortDateString();

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        dr["extra2"] = "";
                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "NotPaymentc";
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

        private void btnDeserved_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = DateTime.Parse(txtDate1.Text).Month;
                    int year1 = DateTime.Parse(txtDate1.Text).Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);

                    DateTime firstDay1 = DateTime.Parse(MainClass.StartRentDay + "/" + month1 + "/" + year1);
                    DateTime lastDay1 = firstDay1.AddMonths(1).AddDays(-1);


                    int month2 = DateTime.Parse(txtDate2.Text).Month;
                    int year2 = DateTime.Parse(txtDate2.Text).Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);

                    DateTime firstDay2 = DateTime.Parse(MainClass.StartRentDay + "/" + month2 + "/" + year2);
                    DateTime lastDay2 = firstDay2.AddMonths(1).AddDays(-1);


                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents on c.carID equals d.carID
                               join ee in db.carCustomerRentDetails on d.carCustomerRentID equals ee.carCustomerRentID
                               where d.carCustomerRent_active==true
                               orderby ee.rd_fromDate
                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = "",
                                   mandoubID = "",
                                   rentLicense_from = d.carCustomer_from,
                                   rentLicense_to = d.carCustomer_to,
                                   ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rd_toDate,
                                   Amount = ee.rd_amount.Value,
                                   rentLicPay_VouNo = "" ,
                                   rd_isPaid = ee.rd_isPaid

                               }).ToList();

                    //if (chkchooseMandoub.Checked)
                    //{
                    //    int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                    //    val = val.Where(cc => cc.mandoubID == mID).ToList();

                    //    title += "المندوب : " + cmbMandoub.Text + " - ";
                    //}


                    if (chkDateFrom.Checked && chkDateTo.Checked)
                    {

                        val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay2).ToList();

                        title += "من : " + txtDate1.Text + " الى : " + txtDate2.Text;

                    }
                    else
                    {
                        if (chkDateFrom.Checked)
                        {
                            val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay1).ToList();

                            title += "لشهر : " + txtDate1.Text;
                        }
                    }

                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();

                        title += "سيارة : " + txtShopCommercialName.Text;
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate,
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo,
                                     rd_isPaid = c.rd_isPaid

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate.Value.ToShortDateString();

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        if (item.rd_isPaid.Value)
                        {
                            dr["extra2"] = "مدفوع";
                        }
                        else
                        {
                            dr["extra2"] = "غير مدفوع";
                        }

                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "DeservedPaymentc";
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

        private void btnPaid1_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = DateTime.Parse(txtDate1.Text).Month;
                    int year1 = DateTime.Parse(txtDate1.Text).Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);

                    DateTime firstDay1 = DateTime.Parse(MainClass.StartRentDay + "/" + month1 + "/" + year1);
                    DateTime lastDay1 = firstDay1.AddMonths(1).AddDays(-1);


                    int month2 = DateTime.Parse(txtDate2.Text).Month;
                    int year2 = DateTime.Parse(txtDate2.Text).Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);

                    DateTime firstDay2 = DateTime.Parse(MainClass.StartRentDay + "/" + month2 + "/" + year2);
                    DateTime lastDay2 = firstDay2.AddMonths(1).AddDays(-1);



                    var val = (from c in db.Cars
                               join d in db.CarSupplierRents
                                on c.carID equals d.carID
                               join ee in db.CarSupplierRentPayments
                               on d.carSupplierRentID equals ee.carSupplierRentID
                               join ee1 in db.Employees on ee.carSupplierRentPayment_mandoubID equals ee1.employeeID

                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carRentNo,
                                   customer_Name = d.CarSupllier.carSuppliser_Name,
                                   mandoub = ee1.emp_namefullar,
                                   mandoubID = ee1.employeeID,
                                   rentLicense_from = d.carSupplierRent_from,
                                   rentLicense_to = d.carSupplierRent_to,
                                   ForDate = ee.carSupplierRentPayment_ForDate,
                                   PaymentDate = ee.carSupplierRentPayment_PayDate,
                                   Amount = ee.carSupplierRentPayment_amount.Value,
                                   rentLicPay_VouNo = ee.carSupplierRentPayment_VoucherNo

                               }).ToList();

                    if (chkchooseMandoub.Checked)
                    {
                        int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                        val = val.Where(cc => cc.mandoubID == mID).ToList();

                        title += "المندوب : " + cmbMandoub.Text + " - ";
                    }


                    if (chkDateFrom.Checked && chkDateTo.Checked)
                    {

                        val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay2).ToList();

                        title += "من : " + txtDate1.Text + " الى : " + txtDate2.Text;

                    }
                    else
                    {
                        if (chkDateFrom.Checked)
                        {
                            val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay1).ToList();

                            title += "لشهر : " + txtDate1.Text;
                        }
                    }



                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID1.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                        title += "سيارة : " + txtShopCommercialName1.Text;
                    }
                    

                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate.Value.ToString("MM - yyyy"),
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        dr["extra2"] = "";
                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Paymentc1";
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

        private void btnNotPaid1_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = DateTime.Parse(txtDate1.Text).Month;
                    int year1 = DateTime.Parse(txtDate1.Text).Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);

                    DateTime firstDay1 = DateTime.Parse(MainClass.StartRentDay + "/" + month1 + "/" + year1);
                    DateTime lastDay1 = firstDay1.AddMonths(1).AddDays(-1);


                    int month2 = DateTime.Parse(txtDate2.Text).Month;
                    int year2 = DateTime.Parse(txtDate2.Text).Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);

                    DateTime firstDay2 = DateTime.Parse(MainClass.StartRentDay + "/" + month2 + "/" + year2);
                    DateTime lastDay2 = firstDay2.AddMonths(1).AddDays(-1);



                    var val = (from c in db.Cars
                               join d in db.CarSupplierRents
                                on c.carID equals d.carID
                               join ee in db.CarSupplierRentDetails
                               on d.carSupplierRentID equals ee.carSupplierRentID
                               where ee.rd_isPaid==false
                               orderby ee.rd_fromDate
                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carRentNo,
                                   customer_Name = d.CarSupllier.carSuppliser_Name,
                                   mandoub = "",
                                   mandoubID = "",
                                   rentLicense_from = d.carSupplierRent_from,
                                   rentLicense_to = d.carSupplierRent_to,
                                   ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rd_toDate,
                                   Amount = ee.rd_amount.Value,
                                   rentLicPay_VouNo = ""

                               }).ToList();

             


                    if (chkDateFrom.Checked && chkDateTo.Checked)
                    {

                        val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay2).ToList();

                        title += "من : " + txtDate1.Text + " الى : " + txtDate2.Text;

                    }
                    else
                    {
                        if (chkDateFrom.Checked)
                        {
                            val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay1).ToList();

                            title += "لشهر : " + txtDate1.Text;
                        }
                    }



                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID1.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                        title += "سيارة : " + txtShopCommercialName1.Text;
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate.Value.ToString("MM - yyyy"),
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        dr["extra2"] = "";
                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "NotPaymentc1";
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

        private void btnDeserved1_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = DateTime.Parse(txtDate1.Text).Month;
                    int year1 = DateTime.Parse(txtDate1.Text).Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);

                    DateTime firstDay1 = DateTime.Parse(MainClass.StartRentDay + "/" + month1 + "/" + year1);
                    DateTime lastDay1 = firstDay1.AddMonths(1).AddDays(-1);


                    int month2 = DateTime.Parse(txtDate2.Text).Month;
                    int year2 = DateTime.Parse(txtDate2.Text).Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);

                    DateTime firstDay2 = DateTime.Parse(MainClass.StartRentDay + "/" + month2 + "/" + year2);
                    DateTime lastDay2 = firstDay2.AddMonths(1).AddDays(-1);



                    var val = (from c in db.Cars
                               join d in db.CarSupplierRents
                                on c.carID equals d.carID
                               join ee in db.CarSupplierRentDetails
                               on d.carSupplierRentID equals ee.carSupplierRentID
                               orderby ee.rd_fromDate
                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carRentNo,
                                   customer_Name = d.CarSupllier.carSuppliser_Name,
                                   mandoub = "",
                                   mandoubID = "",
                                   rentLicense_from = d.carSupplierRent_from,
                                   rentLicense_to = d.carSupplierRent_to,
                                   ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rd_toDate,
                                   Amount = ee.rd_amount.Value,
                                   rentLicPay_VouNo = ""

                               }).ToList();




                    if (chkDateFrom.Checked && chkDateTo.Checked)
                    {

                        val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay2).ToList();

                        title += "من : " + txtDate1.Text + " الى : " + txtDate2.Text;

                    }
                    else
                    {
                        if (chkDateFrom.Checked)
                        {
                            val = val.Where(cc => cc.ForDate >= firstDay1 && cc.ForDate <= lastDay1).ToList();

                            title += "لشهر : " + txtDate1.Text;
                        }
                    }



                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID1.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                        title += "سيارة : " + txtShopCommercialName1.Text;
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate.Value.ToString("MM - yyyy"),
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        dr["extra2"] = "";
                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "DeservedPaymentc1";
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

        private void btnFindCar1_Click(object sender, EventArgs e)
        {
            CarsM.frmfindCars frm = new frmfindCars(false);
            frm.isCurrentbying = true;
            frm.ShowDialog();
            if (frm.found)
            {
                //carID = frm.carID;
                txtShopNo1.Text = frm.carNO;
                txtShopCommercialName1.Text = frm.carName;
                lblCarID1.Text = frm.carID.ToString();
            }
        }

        private void btnMandoubBuyPaid_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    DateTime from = DateTime.Parse(txtdatefrom.Text);
                    DateTime to = DateTime.Parse(txtdateto.Text);


                    var val = (from c in db.Cars
                               join d in db.CarSupplierRents
                                on c.carID equals d.carID
                               join ee in db.CarSupplierRentPayments
                               on d.carSupplierRentID equals ee.carSupplierRentID
                               join ee1 in db.Employees on ee.carSupplierRentPayment_mandoubID equals ee1.employeeID

                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carRentNo,
                                   customer_Name = d.CarSupllier.carSuppliser_Name,
                                   mandoub = ee1.emp_namefullar,
                                   mandoubID = ee1.employeeID,
                                   rentLicense_from = d.carSupplierRent_from,
                                   rentLicense_to = d.carSupplierRent_to,
                                   ForDate = ee.carSupplierRentPayment_ForDate,
                                   PaymentDate = ee.carSupplierRentPayment_PayDate,
                                   Amount = ee.carSupplierRentPayment_amount.Value,
                                   rentLicPay_VouNo = ee.carSupplierRentPayment_VoucherNo

                               }).ToList();

                    if (chkchooseMandoub.Checked)
                    {
                        int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                        val = val.Where(cc => cc.mandoubID == mID).ToList();

                        title += "المندوب : " + cmbMandoub.Text + " - ";
                    }




                        val = val.Where(cc => cc.PaymentDate >= from && cc.PaymentDate <= to).ToList();

                        title += "من : " + txtdatefrom.Text + " الى : " + txtdateto.Text;





                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID1.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                        title += "سيارة : " + txtShopCommercialName1.Text;
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate.Value.ToString("MM - yyyy"),
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        dr["extra2"] = "";
                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Paymentc11";
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

        private void btnMandoubRentPaid_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    DateTime from = DateTime.Parse(txtdatefrom.Text);
                    DateTime to = DateTime.Parse(txtdateto.Text);



                    var val = (from c in db.Cars
                               join d in db.CarCustomerRents
                                on c.carID equals d.carID
                               join ee in db.CarCustomerRentPayments
                               on d.carCustomerRentID equals ee.carCustomerRentID
                               join ee1 in db.Employees on ee.carCustomerPayment_mandoubID equals ee1.employeeID

                               select new
                               {
                                   companyLicencesID = c.carID,
                                   governorateID = "",
                                   goverareaID = "",
                                   governorateName = c.car_Brand,
                                   goverareaName = "",
                                   licenseLinkCode = c.car_Model,
                                   licenseNo = c.car_No,
                                   commercialName = c.car_Name,
                                   rentLicenseNo = d.carCustomer_rentNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = ee1.emp_namefullar,
                                   mandoubID = ee1.employeeID,
                                   rentLicense_from = d.carCustomer_from,
                                   rentLicense_to = d.carCustomer_to,
                                   ForDate = ee.carCustomerPayment_ForDate,
                                   PaymentDate = ee.carCustomerPayment_PaymentDate,
                                   Amount = ee.carCustomerPayment_amount.Value,
                                   rentLicPay_VouNo = ee.carCustomerPayment_VoucherNo

                               }).ToList();

                    if (chkchooseMandoub.Checked)
                    {
                        int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                        val = val.Where(cc => cc.mandoubID == mID).ToList();

                        title += "المندوب : " + cmbMandoub.Text + " - ";
                    }



                        val = val.Where(cc => cc.PaymentDate >= from && cc.PaymentDate <= to).ToList();
                        title += "من : " + txtdatefrom.Text + " الى : " + txtdateto.Text;

                    




                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblCarID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();

                        title += "سيارة : " + txtShopCommercialName.Text;
                    }


                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.goverareaID,
                                     goverareaID = c.governorateID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.governorateName,
                                     licenseLinkCode = c.licenseLinkCode,
                                     licenseNo = c.licenseNo,
                                     commercialName = c.commercialName,
                                     rentLicenseNo = c.rentLicenseNo,
                                     customer_Name = c.customer_Name,
                                     mandoub = c.mandoub,
                                     mandoubID = c.mandoubID,
                                     rentLicense_from = c.rentLicense_from,
                                     rentLicense_to = c.rentLicense_to,
                                     ForDate = c.ForDate.Value.ToString("MM - yyyy"),
                                     PaymentDate = c.PaymentDate,
                                     Amount = c.Amount,
                                     rentLicPay_VouNo = c.rentLicPay_VouNo

                                 };

                    cTable();

                    foreach (var item in newVal)
                    {
                        DataRow dr = dt.NewRow();

                        dr["companyLicencesID"] = item.companyLicencesID;
                        dr["governorateID"] = item.governorateID.ToString();
                        dr["goverareaID"] = item.goverareaID.ToString();
                        dr["governorateName"] = item.governorateName;
                        dr["goverareaName"] = item.goverareaName;

                        dr["licenseLinkCode"] = item.licenseLinkCode;
                        dr["licenseNo"] = item.licenseNo;
                        dr["commercialName"] = item.commercialName;
                        dr["rentLicenseNo"] = item.rentLicenseNo;
                        dr["customer_Name"] = item.customer_Name;

                        dr["mandoub"] = item.mandoub;
                        dr["mandoubID"] = item.mandoubID.ToString();
                        dr["rentLicense_from"] = item.rentLicense_from.Value.ToShortDateString();
                        dr["rentLicense_to"] = item.rentLicense_to.Value.ToShortDateString();
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"] = item.PaymentDate.Value.ToShortDateString();
                        dr["Amount"] = item.Amount;
                        dr["extra1"] = item.rentLicPay_VouNo;
                        dr["extra2"] = "";
                        dr["extra3"] = "";

                        dt.Rows.Add(dr);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);


                    //ds.WriteXmlSchema("Payment1.xsd");
                    //MessageBox.Show("ddd");


                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "Paymentcc1";
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

      

    }
}
