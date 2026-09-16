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
            try
            {
                mainbar.Dock = DockStyle.Top;
                this.Controls.Add(mainbar);

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
                PublicClass.bindMandoub(cmbMandoub);
                getTotalRow();
                getData();
                bindData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void btnAdd_Click(object sender, EventArgs e)
        {
            change = "add";
            enableControls();
            
            txtrentLicPay_VouNo.Text = "";
            txtrentLicPay_Amount.Text = "";
            txtrentLicPay_PaymentDate.Text = DateTime.Now.ToShortDateString();
            txtrentLicPay_ForDate.Text = DateTime.Now.ToShortDateString();
            cmbMandoub.SelectedIndex = 0;

            txtlicenseNo.Text = "";
            txtrentLicenseNo.Text = "";
            txtcommercialName.Text = "";
            txtCustomerName.Text = "";

            //rentID = -1;
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
                if (lblID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            using (TransactionScope ts = new TransactionScope())
                            {

                                int bbID = int.Parse(lblID.Text);
                                RentLicensesPayment bb = (from c in db.RentLicensesPayments
                                                          where c.rentLicensePaymentID ==
                                                              bbID
                                                          select c).FirstOrDefault();

                                db.RentLicensesPayments.DeleteObject(bb);
                                int noOfAffectedRows = db.SaveChanges();

                                int bbID1 = bb.rentLicencesDetailsID.Value;
                                RentLicensesDetail bb1 = (from c in db.RentLicensesDetails
                                                          where c.rentLicencesDetailsID == bbID1
                                                          select c).FirstOrDefault();
                                if (bb1 != null)
                                {
                                    bb1.rd_isPaid = false;
                                    bb1.edited_Date = DateTime.Now;
                                    bb1.editedBy = MainClass.userID;
                                    db.SaveChanges();
                                    db.AcceptAllChanges();
                                    ts.Complete();
                                }
                                else
                                {
                                    ts.Dispose();
                                }


                            }
                        }
                        lblID.Text = "-1";
                        _rentLicencesDetailsID = -1;
                        rentID = -1;
                        getTotalRow();
                        getData();
                        bindData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                int rowsAffected = 0;
                if (txtrentLicPay_Amount.Text == "0")
                {
                    MessageBox.Show("المبلغ غير صحيح");
                }
                else
                {
                    if (change == "add")
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            using (TransactionScope ts = new TransactionScope())
                            {
                                RentLicensesPayment bb = new RentLicensesPayment();

                                bb.rentLicPay_VouNo = txtrentLicPay_VouNo.Text;
                                bb.rentLicPay_Amount = float.Parse(txtrentLicPay_Amount.Text);
                                bb.rentLicPay_PaymentDate = DateTime.Parse(txtrentLicPay_PaymentDate.Text);
                                bb.rentLicPay_ForDate = DateTime.Parse("15/" +
                                    DateTime.Parse(txtrentLicPay_ForDate.Text).Month + "/" +
                                    +DateTime.Parse(txtrentLicPay_ForDate.Text).Year);
                                bb.rentLicPay_mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());



                                bb.added_Date = DateTime.Now;
                                bb.addedBy = MainClass.userID;

                                bb.edited_Date = DateTime.Now;
                                bb.editedBy = MainClass.userID;



                                RentLicensesDetail bb1 = (from c in db.RentLicensesDetails
                                                          where c.rentLicencesDetailsID == _rentLicencesDetailsID
                                                          select c).FirstOrDefault();
                                if (bb1 != null)
                                {
                                    bb.rentLicenseID = bb1.rentLicenseID.Value;
                                    bb.rentLicencesDetailsID = bb1.rentLicencesDetailsID;
                                    db.RentLicensesPayments.AddObject(bb);

                                    bb1.rd_isPaid = true;

                                    bb1.edited_Date = DateTime.Now;
                                    bb1.editedBy = MainClass.userID;
                                    db.SaveChanges();
                                    lblID.Text = bb.rentLicensePaymentID.ToString();
                                    db.AcceptAllChanges();
                                    ts.Complete();
                                    disableControls();
                                }
                                else
                                {
                                    ts.Dispose();
                                }

                            }
                        }
                    }
                    if (change == "edit")
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            using (TransactionScope ts = new TransactionScope())
                            {
                                int bbID = int.Parse(lblID.Text);
                                RentLicensesPayment bb = (from c in db.RentLicensesPayments
                                                          where c.rentLicensePaymentID ==
                                                              bbID
                                                          select c).FirstOrDefault();

                                bb.rentLicPay_VouNo = txtrentLicPay_VouNo.Text;
                                bb.rentLicPay_Amount = float.Parse(txtrentLicPay_Amount.Text);
                                bb.rentLicPay_PaymentDate = DateTime.Parse(txtrentLicPay_PaymentDate.Text);
                                bb.rentLicPay_ForDate = DateTime.Parse("15/" +
                                    DateTime.Parse(txtrentLicPay_ForDate.Text).Month + "/" +
                                    +DateTime.Parse(txtrentLicPay_ForDate.Text).Year);
                                bb.rentLicPay_mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());
                                bb.rentLicenseID = rentID;

                                bb.edited_Date = DateTime.Now;
                                bb.editedBy = MainClass.userID;

                                rowsAffected = db.SaveChanges();

                                RentLicensesDetail bb1 = (from c in db.RentLicensesDetails
                                                          where c.rentLicencesDetailsID == _rentLicencesDetailsID
                                                          select c).FirstOrDefault();
                                if (bb1 != null)
                                {

                                    bb1.rd_isPaid = true;

                                    bb1.edited_Date = DateTime.Now;
                                    bb1.editedBy = MainClass.userID;

                                    db.AcceptAllChanges();
                                    ts.Complete();
                                    disableControls();
                                }
                                else
                                {
                                    ts.Dispose();
                                }



                            }
                        }
                    }
                }

                getTotalRow();
                if (change == "edit")
                {
                    getData(int.Parse(lblID.Text));
                }
                else
                {
                    getData();
                }
                change = "";
                bindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            btnFindLicenses.Enabled = true;

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
                txtrentLicPay_Amount.Text = lstRentLicensesPayment[0].rentLicPay_Amount.Value.ToString();
                
                txtrentLicPay_ForDate.Text = lstRentLicensesPayment[0].rentLicPay_ForDate.Value.ToShortDateString();
                txtrentLicPay_PaymentDate.Text = lstRentLicensesPayment[0].rentLicPay_PaymentDate.Value.ToShortDateString();

                for (int i = 0; i < cmbMandoub.Items.Count; i++)
                {
                    if (cmbMandoub.Items[i].Value.ToString() == lstRentLicensesPayment[0].rentLicPay_mandoubID.Value.ToString())
                    {
                        cmbMandoub.SelectedIndex = i;
                        break;
                    }
                }
                rentID = lstRentLicensesPayment[0].rentLicenseID.Value;
                _rentLicencesDetailsID = lstRentLicensesPayment[0].rentLicencesDetailsID.Value; 

                lblID.Text = lstRentLicensesPayment[0].rentLicensePaymentID.ToString();
                bindRentData(lstRentLicensesPayment[0].RentLicens.companyLicencesID.Value);

                mainbar.lblCount.Text = position + 1 + " / " + totalRow.ToString();
            }
            else
            {
                txtrentLicPay_VouNo.Text = "";
                txtrentLicPay_Amount.Text = "0";
                cmbMandoub.SelectedIndex = 0;
                txtrentLicPay_ForDate.Text = DateTime.Now.ToShortDateString();
                txtrentLicPay_PaymentDate.Text = DateTime.Now.ToShortDateString();

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
            frmfindLicences frm = new frmfindLicences(true,false);
            frm.isCurrentRentedLicences = true;
            frm.ShowDialog();
            if (frm.found)
            {
                bindRentData(frm.licID);
                if (change == "add")
                {
                    getLastPayment(frm.licID);
                }
            }
        }

        private void bindRentData(int lID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.CompanyLicences
                           join d in db.RentLicenses
                            on c.companyLicencesID equals d.companyLicencesID
                           where c.companyLicencesID == lID
                           && d.rentLicense_active==true
                           select new
                           {
                               c.companyLicencesID,
                               c.commercialName,
                               c.licenseNo,
                               d.rentLicenseNo,
                               d.Customer.customer_Name,
                               d.rentLicenseID
                           }).FirstOrDefault();

                txtlicenseNo.Text = val.licenseNo;
                txtrentLicenseNo.Text = val.rentLicenseNo;
                txtcommercialName.Text = val.commercialName;
                txtCustomerName.Text = val.customer_Name;
                rentID = val.rentLicenseID;
            }
        }

        private void getLastPayment(int _companyLicencesID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.CompanyLicences
                           join d in db.RentLicenses
                            on c.companyLicencesID equals d.companyLicencesID
                           where c.companyLicencesID == _companyLicencesID
                           && d.rentLicense_active == true
                           select new
                           {
                               d.rentLicenseID,
                               d.rentLicense_from,
                               d.rentLicense_to,
                               d.rentLicense_RentalAmount

                           }).FirstOrDefault();


                var val1 = (from c in db.RentLicensesDetails
                           orderby c.rentLicenseID
                           where c.rentLicenseID == rentID
                           select c).ToList();
                txtrentLicPay_Amount.Text = "0";
                foreach (var vv in val1)
                {
                    if (vv.rd_isPaid == false)
                    {
                        txtrentLicPay_Amount.Text = vv.rd_amount.Value.ToString();

                        if (vv.rd_fromDate.Value.Day > MainClass.StartRentDay)
                        {
                            txtrentLicPay_ForDate.Text = vv.rd_fromDate.Value.AddMonths(1).ToShortDateString();
                        }
                        else
                        {
                            txtrentLicPay_ForDate.Text = vv.rd_fromDate.Value.ToShortDateString();
                        }
                        _rentLicencesDetailsID = vv.rentLicencesDetailsID;
                        break;
                    }
                }

            }
        }


    }
}
