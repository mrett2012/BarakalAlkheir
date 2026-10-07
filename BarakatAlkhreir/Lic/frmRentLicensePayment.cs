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
using System.Transactions;
using BarakalAlkheir.Sponsor;

namespace BarakalAlkheir.Lic
{
    public partial class frmRentLicensePayment : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        //RentLicensesPayment RentLicensesPayment = null;
        List<RentLicensesPayment> lstRentLicensesPayment = null;

        components.banner mainbar = new components.banner("frmRentLicensePayment");

        int licenceID = -1;
        int rentID = -1;
        int totalRow = 0;
        int rentLiID = -1;
        int _rentLicencesDetailsID = -1;

        public frmRentLicensePayment()
        {
            InitializeComponent();
        }

        private void frmRentLicensePayment_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmRentLicensePayment_LoadData);
        }

        private async System.Threading.Tasks.Task frmRentLicensePayment_LoadData(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                await mainbar.AddToAsync(this);

                mainbar.btnAdd.Click += new EventHandler(btnAdd_Click);
                mainbar.btnEdit.Click += new EventHandler(btnEdit_Click);
                mainbar.btnDelete.Click += new EventHandler(btnDelete_Click);
                mainbar.btnCancel.Click += new EventHandler(btnCancel_Click);
                mainbar.btnSave.Click += new EventHandler(btnSave_Click);
                mainbar.btnFind.Click += new EventHandler(btnFind_Click);
                mainbar.btnFirst.Click += new EventHandler(btnFirst_Click);
                mainbar.btnPrevious.Click += new EventHandler(btnPrevious_Click);
                mainbar.btnNext.Click += new EventHandler(btnNext_Click);
                mainbar.btnLast.Click += new EventHandler(btnLast_Click);
                mainbar.btnExit.Click += new EventHandler(btnExit_Click);

                disableControls();
                await PublicClass.bindMandoubAsync(cmbMandoub);
                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getTotalRow);
                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
                bindData();


            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void btnAdd_Click(object sender, EventArgs e)
        {
            change = "add";
            rentID = -1;
            _rentLicencesDetailsID = -1;
            lblID.Text = "-1";
            enableControls();
            txtrentLicPay_VouNo.Text = "";
            txtrentLicPay_Amount.Text = "0";
            txtrentLicPay_PaymentDate.Value = DateTime.Today;
            txtrentLicPay_ForDate.Value = DateTime.Today;
            cmbMandoub.SelectedIndex = 0;
            txtlicenseNo.Text = "";
            txtrentLicenseNo.Text = "";
            txtcommercialName.Text = "";
            txtCustomerName.Text = "";
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                change = "edit";
                enableControls();
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text == "-1" || MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                new LicenseRentalWorkflow().DeletePayment(int.Parse(lblID.Text), rentID, lstRentLicensesPayment[0].edited_Date);
                getTotalRow();
                getData();
                bindData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            change = "";
            disableControls();
            bindData();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (change != "add" && change != "edit") return;
                int collector;
                if (cmbMandoub.SelectedValue == null || !int.TryParse(cmbMandoub.SelectedValue.ToString(), out collector))
                    throw new InvalidOperationException("يرجى اختيار المندوب");
                bool adding = change == "add";
                int id = new LicenseRentalWorkflow().SavePayment(adding ? -1 : int.Parse(lblID.Text),
                    rentID, _rentLicencesDetailsID, LicenseRentalRules.ParsePositiveAmount(txtrentLicPay_Amount.Text),
                    txtrentLicPay_VouNo.Text, txtrentLicPay_PaymentDate.Value.Date, collector,
                    adding ? null : lstRentLicensesPayment[0].edited_Date);
                change = "";
                disableControls();
                getTotalRow();
                getData(id);
                bindData();
            }
            catch (RentalChangedException ex)
            {
                MessageBox.Show(ex.Message);
                if (change == "add") getLastPayment(rentID);
                else { getData(int.Parse(lblID.Text)); bindData(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void btnFind_Click(object sender, EventArgs e)
        {
            
            frmfindLicencesPayment frm = new frmfindLicencesPayment();
            frm.ShowDialog();
            if (frm.found)
            {
                //RentLicensesPayment cl = lstRentLicensesPayment.Find(c => c.rentLicensePaymentID == frm.licID);
                //position = lstRentLicensesPayment.IndexOf(cl);
                getData(frm.licID);
                bindData();
            }
        }

        void btnFirst_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position = 0;
                getData();
                bindData();
            }
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position -= 1;
                getData();
                bindData();
            }
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            if (position < totalRow - 1)
            {
                position += 1;
                getData();
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < totalRow - 1)
            {
                position = totalRow - 1;
                getData();
                bindData();
            }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls()
        {
            mainbar.enableButtons();


            txtcommercialName.ReadOnly = true;
            txtlicenseNo.ReadOnly = true;
            txtCustomerName.ReadOnly = true;
            txtlicenseNo.ReadOnly = true;
            txtrentLicenseNo.ReadOnly = true;

            
            txtrentLicPay_VouNo.ReadOnly = true;
            txtrentLicPay_Amount.ReadOnly = true;
            cmbMandoub.Enabled = false;
            txtrentLicPay_ForDate.Enabled = false;
            txtrentLicPay_PaymentDate.Enabled = false;
            btnFindLicenses.Enabled = false;

        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtrentLicPay_VouNo.ReadOnly = false;
            //txtrentLicPay_Amount.ReadOnly = false;
            cmbMandoub.Enabled = true;
            //txtrentLicPay_ForDate.Enabled = true;
            txtrentLicPay_PaymentDate.Enabled = true;
            btnFindLicenses.Enabled = change == "add";

        }

        private void getTotalRow()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var no = db.RentLicensesPayments.Count();
                totalRow = no;
                if (totalRow > 0)
                    position = totalRow - 1;
            }
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.RentLicensesPayments.Include("RentLicens")
                           orderby c.rentLicensePaymentID
                           select c).ToList();

                 RentLicensesPayment val1 = null;
                 if (val.Count > 0)
                 {
                     val1 = new RentLicensesPayment();
                     position = Math.Min(Math.Max(position, 0), val.Count - 1);
                     val1 = val.ElementAt(position);
                 }

                lstRentLicensesPayment = new List<RentLicensesPayment>();

                if(val1!=null)
                lstRentLicensesPayment.Add(val1);
                
                
            }
        }

        private void getData(int _ID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.RentLicensesPayments.Include("RentLicens")
                           orderby c.rentLicensePaymentID
                           select c).ToList();

                RentLicensesPayment val1 = null;
                if (val.Count > 0)
                {
                    val1 = new RentLicensesPayment();
                    position = val.FindIndex(cc => cc.rentLicensePaymentID == _ID);
                    position = Math.Min(Math.Max(position, 0), val.Count - 1);
                     val1 = val.ElementAt(position);
                }

                lstRentLicensesPayment = new List<RentLicensesPayment>();

                if (val1 != null)
                    lstRentLicensesPayment.Add(val1);


            }
        }

        private void bindData()
        {
            if (lstRentLicensesPayment.Count > 0)
            {

                txtrentLicPay_VouNo.Text = lstRentLicensesPayment[0].rentLicPay_VouNo;
                txtrentLicPay_Amount.Text = (lstRentLicensesPayment[0].rentLicPay_Amount ?? 0).ToString();
                
                txtrentLicPay_ForDate.Value = lstRentLicensesPayment[0].rentLicPay_ForDate ?? DateTime.Today;
                txtrentLicPay_PaymentDate.Value = lstRentLicensesPayment[0].rentLicPay_PaymentDate ?? DateTime.Today;

                cmbMandoub.SelectedIndex = 0;
                for (int i = 0; i < cmbMandoub.Items.Count; i++)
                {
                    if (cmbMandoub.Items[i].Value.ToString() == (lstRentLicensesPayment[0].rentLicPay_mandoubID ?? -1).ToString())
                    {
                        cmbMandoub.SelectedIndex = i;
                        break;
                    }
                }
                rentID = lstRentLicensesPayment[0].rentLicenseID ?? -1;
                _rentLicencesDetailsID = lstRentLicensesPayment[0].rentLicencesDetailsID ?? -1;

                lblID.Text = lstRentLicensesPayment[0].rentLicensePaymentID.ToString();
                bindRentData(rentID);

                mainbar.lblCount.Text = position + 1 + " / " + totalRow.ToString();
            }
            else
            {
                txtrentLicPay_VouNo.Text = "";
                txtrentLicPay_Amount.Text = "0";
                cmbMandoub.SelectedIndex = 0;
                txtrentLicPay_ForDate.Value = DateTime.Today;
                txtrentLicPay_PaymentDate.Value = DateTime.Today;

                txtlicenseNo.Text = "";
                txtrentLicenseNo.Text = "";
                txtcommercialName.Text = "";
                txtCustomerName.Text = "";
                _rentLicencesDetailsID = -1;

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }
        

        }

        private void btnFindLicenses_Click(object sender, EventArgs e)
        {
            if (change != "add") return;
            try
            {
                using (var frm = new frmfindLicences(true, false))
                {
                    frm.isCurrentRentedLicences = true;
                    frm.ShowDialog();
                    if (!frm.found) return;
                    rentID = -1;
                    _rentLicencesDetailsID = -1;
                    txtrentLicPay_Amount.Text = "0";
                    using (var db = new BarakeitAlkheirEntities())
                    {
                        var rentals = db.RentLicenses.Where(r => r.companyLicencesID == frm.licID && r.rentLicense_active == true).ToList();
                        if (rentals.Count != 1) throw new InvalidOperationException("لا يوجد عقد نشط واحد للرخصة. يرجى مراجعة العقود");
                        rentID = rentals[0].rentLicenseID;
                    }
                    bindRentData(rentID);
                    getLastPayment(rentID);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void bindRentData(int rentalId)
        {
            using (var db = new BarakeitAlkheirEntities())
            {
                var rental = db.RentLicenses.FirstOrDefault(r => r.rentLicenseID == rentalId);
                if (rental == null)
                {
                    txtlicenseNo.Text = txtcommercialName.Text = txtrentLicenseNo.Text = txtCustomerName.Text = "";
                    return;
                }
                var licence = db.CompanyLicences.FirstOrDefault(c => c.companyLicencesID == rental.companyLicencesID);
                var customer = db.Customers.FirstOrDefault(c => c.customerID == rental.customerID);
                txtlicenseNo.Text = licence == null ? "" : licence.licenseNo;
                txtcommercialName.Text = licence == null ? "" : licence.commercialName;
                txtrentLicenseNo.Text = rental.rentLicenseNo;
                txtCustomerName.Text = customer == null ? "" : customer.customer_Name;
                if (change == "")
                {
                    mainbar.btnEdit.Enabled = rental.rentLicense_active == true;
                    mainbar.btnDelete.Enabled = rental.rentLicense_active == true;
                }
            }
        }

        private void getLastPayment(int rentalId)
        {
            _rentLicencesDetailsID = -1;
            txtrentLicPay_Amount.Text = "0";
            txtrentLicPay_ForDate.Value = DateTime.Today;
            using (var db = new BarakeitAlkheirEntities())
            {
                var detail = db.RentLicensesDetails.Where(d => d.rentLicenseID == rentalId && d.rd_isPaid == false)
                    .OrderBy(d => d.rd_fromDate).ThenBy(d => d.rd_mNo).ThenBy(d => d.rentLicencesDetailsID).FirstOrDefault();
                if (detail == null) return;
                if (!detail.rd_fromDate.HasValue || !detail.rd_amount.HasValue)
                    throw new InvalidOperationException("بيانات القسط غير مكتملة. يرجى مراجعتها");
                _rentLicencesDetailsID = detail.rentLicencesDetailsID;
                txtrentLicPay_Amount.Text = detail.rd_amount.Value.ToString();
                txtrentLicPay_ForDate.Value = LicenseRentalRules.BillingDate(detail.rd_fromDate.Value);
            }
        }


    }
}
