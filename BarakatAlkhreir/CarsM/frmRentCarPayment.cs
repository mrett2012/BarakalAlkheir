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

namespace BarakalAlkheir.CarsM
{
    public partial class frmRentCarPayment : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        //CarCustomerRentPayment CarCustomerRentPayment = null;
        List<CarCustomerRentPayment> lstCarCustomerRentPayment = null;
        components.banner mainbar = new components.banner("frmRentCarPayment");

        int carID = -1;
        int rentID = -1;
        int totalRow = 0;
        int rentCarID = -1;
        int _rentCarDetailsID = -1;

        public frmRentCarPayment()
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
            try
            {
                change = "add";
                enableControls();

                txtrentLicPay_VouNo.Text = "";
                txtrentLicPay_Amount.Text = "";
                txtrentLicPay_PaymentDate.Value = DateTime.Today;
                txtrentLicPay_ForDate.Value = DateTime.Today;
                cmbMandoub.SelectedIndex = 0;

                txtlicenseNo.Text = "";
                txtrentLicenseNo.Text = "";
                txtcommercialName.Text = "";
                txtCustomerName.Text = "";

                //rentID = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text != "-1")
                {
                    change = "edit";
                    enableControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                                CarCustomerRentPayment bb = (from c in db.CarCustomerRentPayments
                                                             where c.carCustomerRentPaymentID ==
                                                                 bbID
                                                             select c).FirstOrDefault();

                                db.CarCustomerRentPayments.Remove(bb);
                                int noOfAffectedRows = db.SaveChanges();

                                int bbID1 = bb.carCustomerRentDetailsID.Value;
                                carCustomerRentDetail bb1 = (from c in db.carCustomerRentDetails
                                                             where c.carCustomerRentDetailsID == bbID1
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
                        _rentCarDetailsID = -1;
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
            try
            {
                change = "";
                disableControls();
                bindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                                CarCustomerRentPayment bb = new CarCustomerRentPayment();

                                bb.carCustomerPayment_VoucherNo = txtrentLicPay_VouNo.Text;
                                bb.carCustomerPayment_amount = float.Parse(txtrentLicPay_Amount.Text);
                                bb.carCustomerPayment_PaymentDate = txtrentLicPay_PaymentDate.Value.Date;
                                bb.carCustomerPayment_ForDate = new DateTime(txtrentLicPay_ForDate.Value.Year, txtrentLicPay_ForDate.Value.Month, 15);
                                bb.carCustomerPayment_mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());



                                bb.added_Date = DateTime.Now;
                                bb.addedBy = MainClass.userID;

                                bb.edited_Date = DateTime.Now;
                                bb.editedBy = MainClass.userID;



                                carCustomerRentDetail bb1 = (from c in db.carCustomerRentDetails
                                                             where c.carCustomerRentDetailsID == _rentCarDetailsID
                                                             select c).FirstOrDefault();
                                if (bb1 != null)
                                {
                                    bb.carCustomerRentID = bb1.carCustomerRentID.Value;
                                    bb.carCustomerRentDetailsID = bb1.carCustomerRentDetailsID;
                                    db.CarCustomerRentPayments.Add(bb);

                                    bb1.rd_isPaid = true;

                                    bb1.edited_Date = DateTime.Now;
                                    bb1.editedBy = MainClass.userID;
                                    db.SaveChanges();
                                    lblID.Text = bb.carCustomerRentPaymentID.ToString();
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
                                CarCustomerRentPayment bb = (from c in db.CarCustomerRentPayments
                                                             where c.carCustomerRentPaymentID ==
                                                                 bbID
                                                             select c).FirstOrDefault();

                                bb.carCustomerPayment_VoucherNo = txtrentLicPay_VouNo.Text;
                                bb.carCustomerPayment_amount = float.Parse(txtrentLicPay_Amount.Text);
                                bb.carCustomerPayment_PaymentDate = txtrentLicPay_PaymentDate.Value.Date;
                                bb.carCustomerPayment_ForDate = new DateTime(txtrentLicPay_ForDate.Value.Year, txtrentLicPay_ForDate.Value.Month, 15);
                                bb.carCustomerPayment_mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());

                                bb.edited_Date = DateTime.Now;
                                bb.editedBy = MainClass.userID;

                                rowsAffected = db.SaveChanges();

                                carCustomerRentDetail bb1 = (from c in db.carCustomerRentDetails
                                                             where c.carCustomerRentDetailsID == _rentCarDetailsID
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
            frmfindRentCarPayment frm = new frmfindRentCarPayment();
            frm.ShowDialog();
            if (frm.found)
            {
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
                var no = db.CarCustomerRentPayments.Count();
                totalRow = no;
                if (totalRow > 0)
                    position = totalRow - 1;
            }
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.CarCustomerRentPayments.Include("CarCustomerRent")
                           orderby c.carCustomerRentPaymentID
                           select c).ToList();

                 CarCustomerRentPayment val1 = null;
                 if (val.Count > 0)
                 {
                     val1 = new CarCustomerRentPayment();
                     val1 = val.ElementAt(position);
                 }

                lstCarCustomerRentPayment = new List<CarCustomerRentPayment>();

                if(val1!=null)
                lstCarCustomerRentPayment.Add(val1);
                
                
            }
        }

        private void getData(int _ID)
        {

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.CarCustomerRentPayments.Include("CarCustomerRent")
                           orderby c.carCustomerRentPaymentID
                           select c).ToList();

                CarCustomerRentPayment val1 = null;
                if (val.Count > 0)
                {
                    val1 = new CarCustomerRentPayment();
                    position = val.FindIndex(cc => cc.carCustomerRentPaymentID == _ID);
                    val1 = val.ElementAt(position);
                }

                lstCarCustomerRentPayment = new List<CarCustomerRentPayment>();

                if (val1 != null)
                    lstCarCustomerRentPayment.Add(val1);


            }

        }

        private void bindData()
        {
            if (lstCarCustomerRentPayment.Count > 0)
            {

                txtrentLicPay_VouNo.Text = lstCarCustomerRentPayment[0].carCustomerPayment_VoucherNo;
                txtrentLicPay_Amount.Text = lstCarCustomerRentPayment[0].carCustomerPayment_amount.Value.ToString();
                
                txtrentLicPay_ForDate.Value = lstCarCustomerRentPayment[0].carCustomerPayment_ForDate.Value.Date;
                txtrentLicPay_PaymentDate.Value = lstCarCustomerRentPayment[0].carCustomerPayment_PaymentDate.Value.Date;

                for (int i = 0; i < cmbMandoub.Items.Count; i++)
                {
                    if (cmbMandoub.Items[i].Value.ToString() == lstCarCustomerRentPayment[0].carCustomerPayment_mandoubID.Value.ToString())
                    {
                        cmbMandoub.SelectedIndex = i;
                        break;
                    }
                }
                rentID = lstCarCustomerRentPayment[0].carCustomerRentID.Value;
                _rentCarDetailsID = lstCarCustomerRentPayment[0].carCustomerRentDetailsID.Value; 

                lblID.Text = lstCarCustomerRentPayment[0].carCustomerRentPaymentID.ToString();
                bindRentData(lstCarCustomerRentPayment[0].CarCustomerRent.carID.Value);

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
                _rentCarDetailsID = -1;

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }
        

        }

        private void btnFindLicenses_Click(object sender, EventArgs e)
        {
            frmfindCars frm = new frmfindCars(true);
            frm.isCurrentRentedCar = true;
            frm.ShowDialog();
            if (frm.found)
            {
                bindRentData(frm.carID);
                if (change == "add")
                {
                    getLastPayment(frm.carID);
                }
            }
        }

        private void bindRentData(int lID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.Cars
                           join d in db.CarCustomerRents
                            on c.carID equals d.carID
                           where c.carID == lID && d.carCustomerRent_active==true
                           select new
                           {
                               c.carID,
                               c.car_Name,
                               c.car_No,
                               d.carCustomer_rentNo,
                               d.Customer.customer_Name,
                               d.carCustomerRentID
                           }).FirstOrDefault();

                txtlicenseNo.Text = val.car_No;
                txtrentLicenseNo.Text = val.carCustomer_rentNo;
                txtcommercialName.Text = val.car_Name;
                txtCustomerName.Text = val.customer_Name;
                rentID = val.carCustomerRentID;
            }
        }

        private void getLastPayment(int _carID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.Cars
                           join d in db.CarCustomerRents
                            on c.carID equals d.carID
                           where c.carID == _carID && d.carCustomerRent_active==true
                           select new
                           {
                               d.carCustomer_rentNo,
                               d.carID,
                               d.carCustomer_from,
                               d.carCustomer_to,
                               d.carCustomerRent_RentalAmount

                           }).FirstOrDefault();


                var val1 = (from c in db.carCustomerRentDetails
                           orderby c.carCustomerRentID
                            where c.carCustomerRentID == rentID
                           select c).ToList();
                txtrentLicPay_Amount.Text = "0";
                foreach (var vv in val1)
                {
                    if (vv.rd_isPaid == false)
                    {
                        txtrentLicPay_Amount.Text = vv.rd_amount.Value.ToString();

                        if (vv.rd_fromDate.Value.Day > MainClass.StartRentDay)
                        {
                            txtrentLicPay_ForDate.Value = vv.rd_fromDate.Value.AddMonths(1).Date;
                        }
                        else
                        {
                            txtrentLicPay_ForDate.Value = vv.rd_fromDate.Value.Date;
                        }
                        _rentCarDetailsID = vv.carCustomerRentDetailsID;
                        break;
                    }
                }


            }
        }


    }
}
