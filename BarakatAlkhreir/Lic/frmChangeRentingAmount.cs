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

namespace BarakalAlkheir.Lic
{
    public partial class frmChangeRentingAmount : Telerik.WinControls.UI.RadForm
    {
        public int _rentLicenseID = -1;
        string change1 = "";

        RentLicens rentlic = null;

        components.banner mainbar1 = new components.banner("frmChangeRentingAmount");

        public frmChangeRentingAmount()
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
                lblID1.Text = _rentLicenseID.ToString();

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
            if (rentlic != null && rentlic.rentLicense_active == true)
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
                if (change1 != "edit" || rentlic == null) return;
                new LicenseRentalWorkflow().ChangeRate(_rentLicenseID, txtFrom1.Value.Date,
                    LicenseRentalRules.ParsePositiveAmount(txtRentalAmountNew.Text), rentlic.edited_Date);
                change1 = "";
                disableControls1();
                getData1();
                bindData1();
            }
            catch (RentalChangedException ex)
            {
                MessageBox.Show(ex.Message);
                change1 = "";
                disableControls1();
                getData1();
                bindData1();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                rentlic = (from c in db.RentLicenses where c.rentLicenseID==_rentLicenseID select c).FirstOrDefault();

                var val = (from c in db.RentLicensesDetails
                           where c.rentLicenseID == _rentLicenseID
                           select c).ToList();
                GView.DataSource = val;
            }
        }

        private void bindData1()
        {
            if (rentlic != null)
            {
                txtFrom1.Value = rentlic.rentLicense_from ?? DateTime.Today;
                txtRentalAmountOld.Text = rentlic.rentLicense_RentalAmount.Value.ToString();
                txtRentalAmountNew.Text = "0";

            }
            else
            {
                txtFrom1.Value = DateTime.Today;
                txtRentalAmountOld.Text = "";
                txtRentalAmountNew.Text = "0";
            }
        }


    }
}
