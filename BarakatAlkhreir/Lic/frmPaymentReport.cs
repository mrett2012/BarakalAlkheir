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
    public partial class frmPaymentReport : Telerik.WinControls.UI.RadForm
    {
        DataTable dt = new DataTable();
        public frmPaymentReport()
        {
            InitializeComponent();
            chkShowOldPayment.Enabled = true;
        }

        private void btnFilteredCompanyLicenses_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = txtDate1.Value.Date.Month;
                    int year1 = txtDate1.Value.Date.Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);


                    int month2 = txtDate2.Value.Date.Month;
                    int year2 = txtDate2.Value.Date.Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);



                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               join ee in db.RentLicensesDetails.Where(detail => detail.rd_isPaid == true)
                               on d.rentLicenseID equals ee.rentLicenseID
                               join collector in db.Employees on ee.rentLicPay_mandoubID equals collector.employeeID into collectors
                               from ee1 in collectors.DefaultIfEmpty()
                               orderby ee.rd_fromDate

                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.governorateID ?? -1,
                                   goverareaID = c.goverareaID ?? -1,
                                   governorateName = c.Mohafaza,
                                   goverareaName = c.Mantaqa,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = ee1 == null ? "مندوب غير معروف" : ee1.emp_namefullar,
                                   mandoubID = ee1 == null ? -1 : ee1.employeeID,
                                   rentLicense_from = d.rentLicense_from,
                                   rentLicense_to = d.rentLicense_to,
                                   ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rentLicPay_PaymentDate,
                                   Amount = ee.rd_amount ?? 0,
                                   rentLicPay_VouNo = ee.rentLicPay_VouNo,
                                   rentLicense_active = d.rentLicense_active

                               }).ToList();

                    if (chkchooseMandoub.Checked)
                    {
                        int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                        val = val.Where(cc => cc.mandoubID == mID).ToList();

                        title += "المندوب : " + cmbMandoub.Text +" - ";
                    }


                    if (chkDateFrom.Checked && chkDateTo.Checked && txtDate1.Value.Date > txtDate2.Value.Date)
                        throw new InvalidOperationException("فترة التقرير غير صحيحة");
                    val = val.Where(c => LicenseRentalReports.InPeriod(c.ForDate, true,
                        chkDateFrom.Checked ? (DateTime?)txtDate1.Value : null,
                        chkDateTo.Checked ? (DateTime?)txtDate2.Value : (chkDateFrom.Checked ? (DateTime?)txtDate1.Value : null))).ToList();
                    if (chkDateFrom.Checked) title += " من: " + txtDate1.Value.ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (chkDateTo.Checked) title += " إلى: " + txtDate2.Value.ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (chkChooseGovernorate.Checked)
                    {
                        int GovID = int.Parse(cmbGovernorate.SelectedValue.ToString());
                        val = val.Where(cc => cc.governorateID == GovID).ToList();

                        if (chkChooseGovernorateArea.Checked)
                        {
                            int govAreaID = int.Parse(cmbGovernorateArea.SelectedValue.ToString());
                            val = val.Where(cc => cc.goverareaID == govAreaID).ToList();
                        }
                    }
                    if (!chkShowOldPayment.Checked) val = val.Where(c => c.rentLicense_active == true).ToList();
                    if (chkChooseLicences.Checked) val = val.Where(c => c.companyLicencesID == int.Parse(lblLicenseID.Text)).ToList();
                    var newVal = from c in val
                                 select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.governorateID,
                                   goverareaID = c.goverareaID,
                                   governorateName = c.governorateName,
                                   goverareaName = c.goverareaName,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = c.rentLicenseNo,
                                   customer_Name = c.customer_Name,
                                   mandoub = c.mandoub,
                                   mandoubID = c.mandoubID,
                                   rentLicense_from = c.rentLicense_from,
                                   rentLicense_to = c.rentLicense_to,
                                   ForDate = LicenseRentalRules.BillingDate(c.ForDate.Value).ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
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
                        dr["rentLicense_from"] = LicenseRentalRules.FormatDate(item.rentLicense_from.Value);
                        dr["rentLicense_to"] = LicenseRentalRules.FormatDate(item.rentLicense_to.Value);
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"]=item.PaymentDate.HasValue ? LicenseRentalRules.FormatDate(item.PaymentDate.Value) : "";
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
                    rv.printReport = "Payment";
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
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmPaymentReport_LoadData);
        }

        private async System.Threading.Tasks.Task frmPaymentReport_LoadData(object sender, EventArgs e)
        {
            try
            {
                await PublicClass.bindGovernorateAsync(cmbGovernorate);
                await PublicClass.bindMandoubAsync(cmbMandoub);
                cmbMandoub.SelectedIndex = 0;
                cmbGovernorate.SelectedIndex = 0;
                txtDate1.Value = DateTime.Today;
                txtDate2.Value = DateTime.Today;

                this.txtdatefrom.Value = DateTime.Today;
                this.txtdateto.Value = DateTime.Today;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                chkChooseGovernorateArea.Checked = false;
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
                btnFindShop.Enabled = true;
                chkShowOldPayment.Enabled = true;
            }
            else
            {
                btnFindShop.Enabled = false;
                chkShowOldPayment.Enabled = true;
            }
        }

        private void btnFindShop_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, true);
            frm.isCurrentRentedLicencesInclude = true;
            frm.isAllLicences = chkShowOldPayment.Checked;
            frm.ShowDialog();
            if (frm.found)
            {
                int ID2 = frm.licID;
                txtShopNo.Text = frm.licNO;
                txtShopCommercialName.Text = frm.licName;
                lblLicenseID.Text = ID2.ToString();
                
            }
        }

        private void btnNotPaid_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int month1 = txtDate1.Value.Date.Month;
                    int year1 = txtDate1.Value.Date.Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);


                    int month2 = txtDate2.Value.Date.Month;
                    int year2 = txtDate2.Value.Date.Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);



                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               join ee in db.RentLicensesDetails
                               on d.rentLicenseID equals ee.rentLicenseID

                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.governorateID ?? -1,
                                   goverareaID = c.goverareaID ?? -1,
                                   governorateName = c.Mohafaza,
                                   goverareaName = c.Mantaqa,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = "",
                                   mandoubID = "",
                                   rentLicense_from = d.rentLicense_from,
                                   rentLicense_to = d.rentLicense_to,
                                   DetailId = ee.rentLicencesDetailsID,
                                    rentLicense_active = d.rentLicense_active,
                                    ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rd_toDate,
                                   Amount = ee.rd_amount ?? 0,
                                   rentLicPay_VouNo = ""

                               }).ToList();

                    //if (chkchooseMandoub.Checked)
                    //{
                    //    int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                    //    val = val.Where(cc => cc.mandoubID == mID).ToList();

                    //    title += "المندوب : " + cmbMandoub.Text + " - ";
                    //}


                    if (chkDateFrom.Checked && chkDateTo.Checked && txtDate1.Value.Date > txtDate2.Value.Date)
                        throw new InvalidOperationException("فترة التقرير غير صحيحة");
                    val = val.Where(c => LicenseRentalReports.InPeriod(c.ForDate, true,
                        chkDateFrom.Checked ? (DateTime?)txtDate1.Value : null,
                        chkDateTo.Checked ? (DateTime?)txtDate2.Value : (chkDateFrom.Checked ? (DateTime?)txtDate1.Value : null))).ToList();
                    if (chkDateFrom.Checked) title += " من: " + txtDate1.Value.ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (chkDateTo.Checked) title += " إلى: " + txtDate2.Value.ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (chkChooseGovernorate.Checked)
                    {
                        int GovID = int.Parse(cmbGovernorate.SelectedValue.ToString());
                        val = val.Where(cc => cc.governorateID == GovID).ToList();

                        if (chkChooseGovernorateArea.Checked)
                        {
                            int govAreaID = int.Parse(cmbGovernorateArea.SelectedValue.ToString());
                            val = val.Where(cc => cc.goverareaID == govAreaID).ToList();
                        }
                    }
                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblLicenseID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                    }


                    var paidTotals = LicenseRentalReports.PaidByInstalment(db);
                    if (!chkShowOldPayment.Checked) val = val.Where(c => c.rentLicense_active == true).ToList();
                    val = val.Where(c => LicenseRentalRules.Amount(c.Amount) > LicenseRentalReports.Paid(paidTotals, c.DetailId)).ToList();
                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.governorateID,
                                     goverareaID = c.goverareaID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.goverareaName,
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
                                     Amount = LicenseRentalRules.Amount(c.Amount) - LicenseRentalReports.Paid(paidTotals, c.DetailId),
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
                        dr["rentLicense_from"] = LicenseRentalRules.FormatDate(item.rentLicense_from.Value);
                        dr["rentLicense_to"] = LicenseRentalRules.FormatDate(item.rentLicense_to.Value);
                        dr["ForDate"] = LicenseRentalRules.FormatDate(item.ForDate.Value);

                        dr["PaymentDate"] = item.PaymentDate.HasValue ? LicenseRentalRules.FormatDate(item.PaymentDate.Value) : "";
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
                    rv.printReport = "NotPayment";
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
                    int month1 = txtDate1.Value.Date.Month;
                    int year1 = txtDate1.Value.Date.Year;
                    int day1 = DateTime.DaysInMonth(year1, month1);


                    int month2 = txtDate2.Value.Date.Month;
                    int year2 = txtDate2.Value.Date.Year;
                    int day2 = DateTime.DaysInMonth(year2, month2);



                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               join ee in db.RentLicensesDetails
                               on d.rentLicenseID equals ee.rentLicenseID
                               orderby ee.rd_fromDate
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.governorateID ?? -1,
                                   goverareaID = c.goverareaID ?? -1,
                                   governorateName = c.Mohafaza,
                                   goverareaName = c.Mantaqa,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = "",
                                   mandoubID = "",
                                   rentLicense_from = d.rentLicense_from,
                                   rentLicense_to = d.rentLicense_to,
                                   DetailId = ee.rentLicencesDetailsID,
                                    rentLicense_active = d.rentLicense_active,
                                    ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rd_toDate,
                                   Amount = ee.rd_amount ?? 0,
                                   rentLicPay_VouNo = "" ,
                                   rd_isPaid = ee.rd_isPaid

                               }).ToList();

                    //if (chkchooseMandoub.Checked)
                    //{
                    //    int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                    //    val = val.Where(cc => cc.mandoubID == mID).ToList();

                    //    title += "المندوب : " + cmbMandoub.Text + " - ";
                    //}


                    if (chkDateFrom.Checked && chkDateTo.Checked && txtDate1.Value.Date > txtDate2.Value.Date)
                        throw new InvalidOperationException("فترة التقرير غير صحيحة");
                    val = val.Where(c => LicenseRentalReports.InPeriod(c.ForDate, true,
                        chkDateFrom.Checked ? (DateTime?)txtDate1.Value : null,
                        chkDateTo.Checked ? (DateTime?)txtDate2.Value : (chkDateFrom.Checked ? (DateTime?)txtDate1.Value : null))).ToList();
                    if (chkDateFrom.Checked) title += " من: " + txtDate1.Value.ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (chkDateTo.Checked) title += " إلى: " + txtDate2.Value.ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (chkChooseGovernorate.Checked)
                    {
                        int GovID = int.Parse(cmbGovernorate.SelectedValue.ToString());
                        val = val.Where(cc => cc.governorateID == GovID).ToList();

                        if (chkChooseGovernorateArea.Checked)
                        {
                            int govAreaID = int.Parse(cmbGovernorateArea.SelectedValue.ToString());
                            val = val.Where(cc => cc.goverareaID == govAreaID).ToList();
                        }
                    }
                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblLicenseID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                    }


                    var paidTotals = LicenseRentalReports.PaidByInstalment(db);
                    if (!chkShowOldPayment.Checked) val = val.Where(c => c.rentLicense_active == true).ToList();
                    var newVal = from c in val
                                 select new
                                 {
                                     companyLicencesID = c.companyLicencesID,
                                     governorateID = c.governorateID,
                                     goverareaID = c.goverareaID,
                                     governorateName = c.governorateName,
                                     goverareaName = c.goverareaName,
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
                                     rd_isPaid = (bool?)(LicenseRentalReports.Paid(paidTotals, c.DetailId) == LicenseRentalRules.Amount(c.Amount))

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
                        dr["rentLicense_from"] = LicenseRentalRules.FormatDate(item.rentLicense_from.Value);
                        dr["rentLicense_to"] = LicenseRentalRules.FormatDate(item.rentLicense_to.Value);
                        dr["ForDate"] = LicenseRentalRules.FormatDate(item.ForDate.Value);

                        dr["PaymentDate"] = item.PaymentDate.HasValue ? LicenseRentalRules.FormatDate(item.PaymentDate.Value) : "";
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
                    rv.printReport = "DeservedPayment";
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

        private void radPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMandoubPayment_Click(object sender, EventArgs e)
        {
            try
            {
                string title = "";

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {

                    DateTime from = txtdatefrom.Value.Date;
                    DateTime to = txtdateto.Value.Date;




                    var val = (from c in db.CompanyLicences
                               join d in db.RentLicenses
                                on c.companyLicencesID equals d.companyLicencesID
                               join ee in db.RentLicensesDetails.Where(detail => detail.rd_isPaid == true)
                               on d.rentLicenseID equals ee.rentLicenseID
                               join collector in db.Employees on ee.rentLicPay_mandoubID equals collector.employeeID into collectors
                               from ee1 in collectors.DefaultIfEmpty()
                 
                               select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.governorateID ?? -1,
                                   goverareaID = c.goverareaID ?? -1,
                                   governorateName = c.Mohafaza,
                                   goverareaName = c.Mantaqa,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = d.rentLicenseNo,
                                   customer_Name = d.Customer.customer_Name,
                                   mandoub = ee1 == null ? "مندوب غير معروف" : ee1.emp_namefullar,
                                   mandoubID = ee1 == null ? -1 : ee1.employeeID,
                                   rentLicense_from = d.rentLicense_from,
                                   rentLicense_to = d.rentLicense_to,
                                   rentLicense_active = d.rentLicense_active,
                                   ForDate = ee.rd_fromDate,
                                   PaymentDate = ee.rentLicPay_PaymentDate,
                                   Amount = ee.rd_amount ?? 0,
                                   rentLicPay_VouNo = ee.rentLicPay_VouNo

                               }).ToList();

                    if (chkchooseMandoub.Checked)
                    {
                        int mID = int.Parse(cmbMandoub.SelectedValue.ToString());
                        val = val.Where(cc => cc.mandoubID == mID).ToList();

                        title += "المندوب : " + cmbMandoub.Text +" - ";
                    }


                        val = val.Where(cc => cc.PaymentDate >= from && cc.PaymentDate < to.AddDays(1)).ToList();

                        title += "من : " + txtdatefrom.Text + " الى : " + txtdateto.Text;

                    


                    
                    if (chkChooseGovernorate.Checked)
                    {
                        int GovID = int.Parse(cmbGovernorate.SelectedValue.ToString());
                        val = val.Where(cc => cc.governorateID == GovID).ToList();

                        if (chkChooseGovernorateArea.Checked)
                        {
                            int govAreaID = int.Parse(cmbGovernorateArea.SelectedValue.ToString());
                            val = val.Where(cc => cc.goverareaID == govAreaID).ToList();
                        }
                    }
                    if (chkChooseLicences.Checked)
                    {
                        int _licID = int.Parse(lblLicenseID.Text);
                        val = val.Where(cc => cc.companyLicencesID == _licID).ToList();
                    }
                    

                    if (!chkShowOldPayment.Checked) val = val.Where(c => c.rentLicense_active == true).ToList();
                    var newVal = from c in val
                                 select new
                               {
                                   companyLicencesID = c.companyLicencesID,
                                   governorateID = c.governorateID,
                                   goverareaID = c.goverareaID,
                                   governorateName = c.governorateName,
                                   goverareaName = c.goverareaName,
                                   licenseLinkCode = c.licenseLinkCode,
                                   licenseNo = c.licenseNo,
                                   commercialName = c.commercialName,
                                   rentLicenseNo = c.rentLicenseNo,
                                   customer_Name = c.customer_Name,
                                   mandoub = c.mandoub,
                                   mandoubID = c.mandoubID,
                                   rentLicense_from = c.rentLicense_from,
                                   rentLicense_to = c.rentLicense_to,
                                   ForDate = LicenseRentalRules.BillingDate(c.ForDate.Value).ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                                   PaymentDate = c.PaymentDate.HasValue ? LicenseRentalRules.FormatDate(c.PaymentDate.Value) : "",
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
                        dr["rentLicense_from"] = LicenseRentalRules.FormatDate(item.rentLicense_from.Value);
                        dr["rentLicense_to"] = LicenseRentalRules.FormatDate(item.rentLicense_to.Value);
                        dr["ForDate"] = item.ForDate;

                        dr["PaymentDate"]=item.PaymentDate;
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
                    rv.printReport = "Payment1";
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
