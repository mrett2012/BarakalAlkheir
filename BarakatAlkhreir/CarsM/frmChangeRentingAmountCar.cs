using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using BarakalAlkheir.Model;
using System.Transactions;

namespace BarakalAlkheir.CarsM
{
    public partial class frmChangeRentingAmountCar : Telerik.WinControls.UI.RadForm
    {
        public int _CarCustomerRenteID = -1;
        string change1 = "";

        CarCustomerRent RentCar = null;

        components.banner mainbar1 = new components.banner("frmChangeRentingAmountCar");

        public frmChangeRentingAmountCar()
        {
            InitializeComponent();
        }

        private void frmChangeRentingAmount_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmChangeRentingAmount_LoadData);
        }

        private async System.Threading.Tasks.Task frmChangeRentingAmount_LoadData(object sender, EventArgs e)
        {
            try
            {
                mainbar1.Dock = DockStyle.Top;
                await mainbar1.AddToAsync(this);


                mainbar1.btnAdd.Visible = false;
                mainbar1.btnDelete.Visible = false;
                mainbar1.btnFind.Visible = false;
                mainbar1.lblCount.Visible = false;

                mainbar1.btnFirst.Visible = false;
                mainbar1.btnPrevious.Visible = false;
                mainbar1.btnNext.Visible = false;
                mainbar1.btnLast.Visible = false;

                mainbar1.btnEdit.Click += new EventHandler(btnEdit_Click1);
                mainbar1.btnCancel.Click += new EventHandler(btnCancel_Click1);
                mainbar1.btnSave.Click += new EventHandler(btnSave_Click1);
                mainbar1.btnExit.Click += new EventHandler(btnExit_Click1);

                disableControls1();
                lblID1.Text = _CarCustomerRenteID.ToString();

                getData1();
                bindData1();

            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnEdit_Click1(object sender, EventArgs e)
        {
            if (lblID1.Text != "-1")
            {
                change1 = "edit";
                enableControls1();
            }
        }

        void btnCancel_Click1(object sender, EventArgs e)
        {
            change1 = "";
            disableControls1();
            bindData1();
        }

        void btnSave_Click1(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                if (change1 == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            int bbID = int.Parse(lblID1.Text);
                            CarCustomerRent bb = (from c in db.CarCustomerRents
                                                  where c.carCustomerRentID == _CarCustomerRenteID
                                                  select c).FirstOrDefault();
                            bb.carCustomerRent_RentalAmount = float.Parse(txtRentalAmountNew.Text);

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            //db.CarCustomerRentes.AddObject(bb);
                            rowsAffected = db.SaveChanges();






                            DateTime newDate = DateTime.Parse(txtFrom1.Text);
                            // get unpaid RentCarencesDetails
                            List<carCustomerRentDetail> rld = (from c in db.carCustomerRentDetails
                                                               where c.carCustomerRentID == _CarCustomerRenteID
                                                               && c.rd_fromDate >= newDate && c.rd_isPaid == false
                                                               select c).ToList();
                            foreach (var item in rld)
                            {
                                item.edited_Date = DateTime.Now;
                                item.editedBy = MainClass.userID;
                                item.rd_amount = float.Parse(this.txtRentalAmountNew.Text);
                                db.SaveChanges();
                            }




                            db.AcceptAllChanges();
                            ts.Complete();


                        }
                    }
                }


                disableControls1();
                getData1();
                change1 = "";
                bindData1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void btnExit_Click1(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls1()
        {
            mainbar1.enableButtons();

            txtRentalAmountNew.ReadOnly = true;
            txtFrom1.Enabled = false;
            txtRentalAmountOld.ReadOnly = true;
        }

        private void enableControls1()
        {
            mainbar1.disableButton();

            txtRentalAmountNew.ReadOnly=false;
            txtFrom1.Enabled = true;
            txtRentalAmountOld.ReadOnly = true;
            
        }

        private void getData1()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                RentCar = (from c in db.CarCustomerRents where c.carCustomerRentID==_CarCustomerRenteID select c).FirstOrDefault();

                var val = (from c in db.carCustomerRentDetails
                           where c.carCustomerRentID == _CarCustomerRenteID
                           select c).ToList();
                GView.DataSource = val;
            }
        }

        private void bindData1()
        {
            if (RentCar != null)
            {
                txtFrom1.Text = RentCar.carCustomer_from.Value.ToShortDateString();
                txtRentalAmountOld.Text = RentCar.carCustomerRent_RentalAmount.Value.ToString();
                txtRentalAmountNew.Text = "0";

            }
            else
            {
                txtFrom1.Text = DateTime.Now.ToShortDateString();
                txtRentalAmountOld.Text = "";
                txtRentalAmountNew.Text = "0";
            }
        }


    }
}
