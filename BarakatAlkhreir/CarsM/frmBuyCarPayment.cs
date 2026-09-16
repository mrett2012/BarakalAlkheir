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
    public partial class frmBuyCarPayment : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        //CarSupplierRentPayment CarSupplierRentPayment = null;
        List<CarSupplierRentPayment> lstCarSupplierRentPayment = null;
        components.banner mainbar = new components.banner("frmBuyCarPayment");

        int carID = -1;
        int rentID = -1;
        int totalRow = 0;
        int rentCarID = -1;
        int _rentCarDetailsID = -1;

        public frmBuyCarPayment()
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
                                CarSupplierRentPayment bb = (from c in db.CarSupplierRentPayments
                                                             where c.carSupplierRentPaymentID ==
                                                                 bbID
                                                             select c).FirstOrDefault();

                                db.CarSupplierRentPayments.DeleteObject(bb);
                                int noOfAffectedRows = db.SaveChanges();

                                int bbID1 = bb.carSupplierRentDetailsID.Value;
                                CarSupplierRentDetail bb1 = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentDetailsID == bbID1
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
                                CarSupplierRentPayment bb = new CarSupplierRentPayment();

                                bb.carSupplierRentPayment_VoucherNo = txtrentLicPay_VouNo.Text;
                                bb.carSupplierRentPayment_amount = float.Parse(txtrentLicPay_Amount.Text);
                                bb.carSupplierRentPayment_PayDate = DateTime.Parse(txtrentLicPay_PaymentDate.Text);
                                bb.carSupplierRentPayment_ForDate = DateTime.Parse("15/" +
                                    DateTime.Parse(txtrentLicPay_ForDate.Text).Month + "/" +
                                    +DateTime.Parse(txtrentLicPay_ForDate.Text).Year);
                                bb.carSupplierRentPayment_mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());



                                bb.added_Date = DateTime.Now;
                                bb.addedBy = MainClass.userID;

                                bb.edited_Date = DateTime.Now;
                                bb.editedBy = MainClass.userID;



                                CarSupplierRentDetail bb1 = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentDetailsID == _rentCarDetailsID
                                                             select c).FirstOrDefault();
                                if (bb1 != null)
                                {
                                    bb.carSupplierRentID = bb1.carSupplierRentID.Value;
                                    bb.carSupplierRentDetailsID = bb1.carSupplierRentDetailsID;
                                    db.CarSupplierRentPayments.AddObject(bb);

                                    bb1.rd_isPaid = true;

                                    bb1.edited_Date = DateTime.Now;
                                    bb1.editedBy = MainClass.userID;
                                    db.SaveChanges();
                                    lblID.Text = bb.carSupplierRentPaymentID.ToString();
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
                                CarSupplierRentPayment bb = (from c in db.CarSupplierRentPayments
                                                             where c.carSupplierRentPaymentID ==
                                                                 bbID
                                                             select c).FirstOrDefault();

                                bb.carSupplierRentPayment_VoucherNo = txtrentLicPay_VouNo.Text;
                                bb.carSupplierRentPayment_amount = float.Parse(txtrentLicPay_Amount.Text);
                                bb.carSupplierRentPayment_PayDate = DateTime.Parse(txtrentLicPay_PaymentDate.Text);
                                bb.carSupplierRentPayment_ForDate = DateTime.Parse("15/" +
                                    DateTime.Parse(txtrentLicPay_ForDate.Text).Month + "/" +
                                    +DateTime.Parse(txtrentLicPay_ForDate.Text).Year);
                                bb.carSupplierRentPayment_mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());

                                bb.edited_Date = DateTime.Now;
                                bb.editedBy = MainClass.userID;

                                rowsAffected = db.SaveChanges();

                                CarSupplierRentDetail bb1 = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentDetailsID == _rentCarDetailsID
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
            frmfindBuyCarPayment frm = new frmfindBuyCarPayment();
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
                var no = db.CarSupplierRentPayments.Count();
                totalRow = no;
                if (totalRow > 0)
                    position = totalRow - 1;
            }
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.CarSupplierRentPayments.Include("CarSupplierRent")
                           orderby c.carSupplierRentPaymentID
                           select c).ToList();
                
                 CarSupplierRentPayment val1 = null;
                 if (val.Count > 0)
                 {
                     val1 = new CarSupplierRentPayment();
                     val1 = val.ElementAt(position);
                 }

                lstCarSupplierRentPayment = new List<CarSupplierRentPayment>();

                if(val1!=null)
                lstCarSupplierRentPayment.Add(val1);
                
                
            }
        }

        private void getData(int _ID)
        {

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.CarSupplierRentPayments.Include("CarSupplierRent")
                           orderby c.carSupplierRentPaymentID
                           select c).ToList();

                CarSupplierRentPayment val1 = null;
                if (val.Count > 0)
                {
                    val1 = new CarSupplierRentPayment();
                    position = val.FindIndex(cc => cc.carSupplierRentPaymentID == _ID);
                    val1 = val.ElementAt(position);
                }

                lstCarSupplierRentPayment = new List<CarSupplierRentPayment>();

                if (val1 != null)
                    lstCarSupplierRentPayment.Add(val1);


            }
        }

        private void bindData()
        {
            if (lstCarSupplierRentPayment.Count > 0)
            {

                txtrentLicPay_VouNo.Text = lstCarSupplierRentPayment[0].carSupplierRentPayment_VoucherNo;
                txtrentLicPay_Amount.Text = lstCarSupplierRentPayment[0].carSupplierRentPayment_amount.Value.ToString();
                
                txtrentLicPay_ForDate.Text = lstCarSupplierRentPayment[0].carSupplierRentPayment_ForDate.Value.ToShortDateString();
                txtrentLicPay_PaymentDate.Text = lstCarSupplierRentPayment[0].carSupplierRentPayment_PayDate.Value.ToShortDateString();

                for (int i = 0; i < cmbMandoub.Items.Count; i++)
                {
                    if (cmbMandoub.Items[i].Value.ToString() == lstCarSupplierRentPayment[0].carSupplierRentPayment_mandoubID.Value.ToString())
                    {
                        cmbMandoub.SelectedIndex = i;
                        break;
                    }
                }
                rentID = lstCarSupplierRentPayment[0].carSupplierRentID.Value;
                _rentCarDetailsID = lstCarSupplierRentPayment[0].carSupplierRentDetailsID.Value; 

                lblID.Text = lstCarSupplierRentPayment[0].carSupplierRentPaymentID.ToString();
                bindRentData(lstCarSupplierRentPayment[0].CarSupplierRent.carID.Value);

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
                _rentCarDetailsID = -1;

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }
        

        }

        private void btnFindLicenses_Click(object sender, EventArgs e)
        {
            frmfindCars frm = new frmfindCars(false);
            frm.isCurrentbying = true;
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
                           join d in db.CarSupplierRents
                            on c.carID equals d.carID
                           where c.carID == lID
                           select new
                           {
                               c.carID,
                               c.car_Name,
                               c.car_No,
                               d.carRentNo,
                               d.CarSupllier.carSuppliser_Name,
                               d.carSupplierRentID
                           }).FirstOrDefault();
                if (val != null)
                {
                    txtlicenseNo.Text = val.car_No;
                    txtrentLicenseNo.Text = val.carRentNo;
                    txtcommercialName.Text = val.car_Name;
                    txtCustomerName.Text = val.carSuppliser_Name;
                    rentID = val.carSupplierRentID;
                }
            }
        }

        private void getLastPayment(int _carID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.Cars
                           join d in db.CarSupplierRents
                            on c.carID equals d.carID
                           where c.carID == _carID
                           select new
                           {
                               d.carRentNo,
                               d.carID,
                               d.carSupplierRent_from,
                               d.carSupplierRent_to,
                               d.carSupplierRent_MonthlyAmount

                           }).FirstOrDefault();


                var val1 = (from c in db.CarSupplierRentDetails
                           orderby c.rd_mNo
                            where c.carSupplierRentID == rentID
                           select c).ToList();
                txtrentLicPay_Amount.Text = "0";
                foreach (var vv in val1)
                {
                    if (vv.rd_isPaid == false)
                    {
                        txtrentLicPay_Amount.Text = vv.rd_amount.Value.ToString();

                        if (vv.rd_fromDate.Value.Day > MainClass.StartRentCar)
                        {
                            txtrentLicPay_ForDate.Text = vv.rd_fromDate.Value.AddMonths(1).ToShortDateString();
                        }
                        else
                        {
                            txtrentLicPay_ForDate.Text = vv.rd_fromDate.Value.ToShortDateString();
                        }
                        _rentCarDetailsID = vv.carSupplierRentDetailsID;
                        break;
                    }
                }


            }
        }


    }
}
