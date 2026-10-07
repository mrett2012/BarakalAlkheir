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
    public partial class frmRentALicense : Telerik.WinControls.UI.RadForm
    {
        string change1 = "";
        int position1 = 0;

        string change2 = "";
        int position2 = 0;

        List<RentLicens> lstLicense = null;
        List<RentLicensesDetail> displayedSchedule = new List<RentLicensesDetail>();
        List<RentShop> lstRentShop = null;
        components.banner mainbar1 = new components.banner("pageRentAlicences");
        components.banner mainbar2 = new components.banner("pageRentAAshop");

        int customerID1 = -1;
        int customerID2 = -1;

        int licenseID1 = -1;
        int shopID2 = -1;

        public frmRentALicense()
        {
            InitializeComponent();
            txtDateFrom1.ValueChanged += txtNoOfmonths_ValueChanged;
        }

        private void radPageView1_PageRemoving(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void frmRentALicense_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmRentALicense_LoadData);
        }

        private async System.Threading.Tasks.Task frmRentALicense_LoadData(object sender, EventArgs e)
        {
            try
            {
                mainbar1.Dock = DockStyle.Top;
                await mainbar1.AddToAsync(pageRentAlicences);

                mainbar2.Dock = DockStyle.Top;
                await mainbar2.AddToAsync(pageRentAAshop);

                mainbar1.btnAdd.Click += new EventHandler(btnAdd_Click1);
                mainbar1.btnEdit.Click += new EventHandler(btnEdit_Click1);
                mainbar1.btnDelete.Click += new EventHandler(btnDelete_Click1);
                mainbar1.btnCancel.Click += new EventHandler(btnCancel_Click1);
                mainbar1.btnSave.Click += new EventHandler(btnSave_Click1);
                mainbar1.btnFind.Click += new EventHandler(btnFind_Click1);
                mainbar1.btnFirst.Click += new EventHandler(btnFirst_Click1);
                mainbar1.btnPrevious.Click += new EventHandler(btnPrevious_Click1);
                mainbar1.btnNext.Click += new EventHandler(btnNext_Click1);
                mainbar1.btnLast.Click += new EventHandler(btnLast_Click1);
                mainbar1.btnExit.Click += new EventHandler(btnExit_Click1);

                mainbar2.btnAdd.Click += new EventHandler(btnAdd_Click2);
                mainbar2.btnEdit.Click += new EventHandler(btnEdit_Click2);
                mainbar2.btnDelete.Click += new EventHandler(btnDelete_Click2);
                mainbar2.btnCancel.Click += new EventHandler(btnCancel_Click2);
                mainbar2.btnSave.Click += new EventHandler(btnSave_Click2);
                mainbar2.btnFind.Click += new EventHandler(btnFind_Click2);
                mainbar2.btnFirst.Click += new EventHandler(btnFirst_Click2);
                mainbar2.btnPrevious.Click += new EventHandler(btnPrevious_Click2);
                mainbar2.btnNext.Click += new EventHandler(btnNext_Click2);
                mainbar2.btnLast.Click += new EventHandler(btnLast_Click2);
                mainbar2.btnExit.Click += new EventHandler(btnExit_Click2);


                disableControls1();
                disableControls2();

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData1);
                bindData1();

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData2);
                bindData2();

                pageRentAlicences.Select();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // rent a License
        void btnAdd_Click1(object sender, EventArgs e)
        {
            change1 = "add";
            enableControls1();

            customerID1 = -1;
            licenseID1 = -1;

            txtcommercialName1.Text = "";
            txtCustomerName1.Text = "";
            txtDamanAmount1.Text = "0";
            txtDateFrom1.Value = DateTime.Today;
            txtlicenseNo1.Text = "";
            txtRentalAmount1.Text = "0";
            txtNoOfmonths.Value = 0;
            txtNoOfSeats.Value = 0;
            txtrentLicenseNo.Text = "";
            //txtrentShopNo.Text = "";
            txtTo1.Value = DateTime.Today;

        }

        void btnEdit_Click1(object sender, EventArgs e)
        {
            if (lblID1.Text != "-1")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int rentID1 = int.Parse(lblID1.Text);
                    RentLicens bb = (from c in db.RentLicenses
                                     where c.rentLicenseID ==
                                         rentID1
                                     select c).FirstOrDefault();
                    if (bb.rentLicense_active.Value == false)
                    {
                        MessageBox.Show("لا يمكنك التعديل على ايجار تم انهائه");
                    }
                    else
                    {
                        change1 = "edit";
                        enableControls1();
                    }
                }
            }
        }

        void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                if (lblID1.Text == "-1" || MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                new LicenseRentalWorkflow().DeleteRental(int.Parse(lblID1.Text));
                position1 = 0;
                getData1();
                bindData1();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                if (change1 != "add" && change1 != "edit") return;
                bool adding = change1 == "add";
                decimal amount = adding ? LicenseRentalRules.ParsePositiveAmount(txtRentalAmount1.Text) : 0m;
                DateTime? version = adding ? null : lstLicense[position1].edited_Date;
                int id = new LicenseRentalWorkflow().SaveRental(adding ? -1 : int.Parse(lblID1.Text),
                    licenseID1, customerID1, txtrentLicenseNo.Text, txtDateFrom1.Value.Date,
                    (int)txtNoOfmonths.Value, (int)txtNoOfSeats.Value, amount, version);
                change1 = "";
                disableControls1();
                getData1();
                position1 = Math.Max(0, lstLicense.FindIndex(r => r.rentLicenseID == id));
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

        void btnFind_Click1(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences1 frm = new Sponsor.frmfindLicences1(true, true);
            //if (change1 == "add")
            //{
            //    frm.isCurrentRentedLicencesInclude = false;
            //}
            frm.ShowDialog();
            if (frm.found)
            {
                licenseID1 = frm.licID;
                txtlicenseNo1.Text = frm.licNO;
                txtcommercialName1.Text = frm.licName;

                RentLicens cl = this.lstLicense.Find(c => c.companyLicencesID == frm.licID && c.rentLicense_active==true);
                position1 = lstLicense.IndexOf(cl);
                if (position1 < 0) { position1 = 0; MessageBox.Show("لا يوجد عقد نشط للرخصة المحددة"); }
                bindData1();
                
            }
        }

        void btnFirst_Click1(object sender, EventArgs e)
        {
            try
            {

                if (position1 > 0)
                {
                    position1 = 0;
                    bindData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnPrevious_Click1(object sender, EventArgs e)
        {
            try
            {
                if (position1 > 0)
                {
                    position1 -= 1;
                    bindData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnNext_Click1(object sender, EventArgs e)
        {
            try
            {
                if (position1 < lstLicense.Count - 1)
                {
                    position1 += 1;
                    bindData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnLast_Click1(object sender, EventArgs e)
        {
            try
            {
                if (position1 < lstLicense.Count - 1)
                {
                    position1 = lstLicense.Count - 1;
                    bindData1();
                }
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

            txtcommercialName1.ReadOnly = true;
            txtCustomerName1.ReadOnly = true;
            txtDamanAmount1.ReadOnly = true;
            
            txtlicenseNo1.ReadOnly = true;
            txtRentalAmount1.ReadOnly = true;
            txtrentLicenseNo.ReadOnly = true;
            //txtrentShopNo.Text = "";
            txtTo1.Enabled = false;
            txtDateFrom1.Enabled = false;
            txtNoOfmonths.Enabled = false;
            txtNoOfSeats.Enabled = false;
            btnFindCustomer1.Enabled = false;
            btnFindLicenses1.Enabled = false;
            ChangeRentingAmount.Enabled = true;
            btnEndLicencesRent.Enabled = true;
            pageRentAAshop.Enabled = true;
        }

        private void enableControls1()
        {
            mainbar1.disableButton();

           // txtDamanAmount1.ReadOnly = false;

            txtRentalAmount1.ReadOnly = change1 != "add";
            txtrentLicenseNo.ReadOnly = false;
            //txtTo1.Enabled = true;
            txtNoOfmonths.Enabled = true;
            txtNoOfSeats.Enabled = true;
            

            btnFindCustomer1.Enabled = true;
            btnFindLicenses1.Enabled = true;
            ChangeRentingAmount.Enabled = false;
            btnEndLicencesRent.Enabled = false;
            pageRentAAshop.Enabled = false;
            if(change1 == "edit")
            {
                txtDateFrom1.Enabled = false;
                int id = int.Parse(lblID1.Text);
                using (var db = new BarakeitAlkheirEntities())
                {
                    bool hasPayments = db.RentLicensesPayments.Any(p => p.rentLicenseID == id);
                    btnFindCustomer1.Enabled = !hasPayments;
                    btnFindLicenses1.Enabled = !hasPayments;
                }
            }else
            {
                txtDateFrom1.Enabled = true;
            }
        }

        private void getData1()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstLicense = (from c in db.RentLicenses orderby c.rentLicenseID  select c).ToList();
            }
        }

        private void bindData1()
        {
            if (lstLicense.Count > 0)
            {
                position1 = Math.Min(Math.Max(position1, 0), lstLicense.Count - 1);
                customerID1 =  lstLicense[position1].customerID.Value;
                licenseID1 =  lstLicense[position1].companyLicencesID.Value;
                txtDamanAmount1.Text =  lstLicense[position1].rentLicense_DamanAmount.Value.ToString();
                txtDateFrom1.Value = lstLicense[position1].rentLicense_from ?? DateTime.Today;
                txtRentalAmount1.Text = lstLicense[position1].rentLicense_RentalAmount.Value.ToString();
                txtrentLicenseNo.Text = lstLicense[position1].rentLicenseNo;

                //txtrentShopNo.Text = "";
                txtTo1.Value = lstLicense[position1].rentLicense_to ?? DateTime.Today;
                txtNoOfmonths.Value = lstLicense[position1].rentLicense_noOfMonths.Value;
                txtNoOfSeats.Value = lstLicense[position1].rentLicense_NoOfSeats.Value;
                lblID1.Text = lstLicense[position1].rentLicenseID.ToString();
                using (var db = new BarakeitAlkheirEntities())
                {
                    int id = lstLicense[position1].rentLicenseID;
                    displayedSchedule = db.RentLicensesDetails.Where(d => d.rentLicenseID == id).OrderBy(d => d.rd_mNo).ToList();
                    txtDamanAmount1.Text = displayedSchedule.Sum(d => LicenseRentalRules.Amount(d.rd_amount ?? 0)).ToString();
                }
                bool active = lstLicense[position1].rentLicense_active == true;
                mainbar1.btnEdit.Enabled = mainbar1.btnDelete.Enabled = active;
                ChangeRentingAmount.Enabled = btnEndLicencesRent.Enabled = active;
                mainbar1.lblCount.Text = position1 + 1 + " / " + lstLicense.Count;

                bindCustomer1(customerID1);
                bindLicences1(licenseID1);

                           
            }
            else
            {
                customerID1 = -1;
                licenseID1 = -1;

                txtcommercialName1.Text = "";
                txtCustomerName1.Text = "";
                txtDamanAmount1.Text = "0";
                txtDateFrom1.Value = DateTime.Today;
                txtlicenseNo1.Text = "";
                txtRentalAmount1.Text = "0";
                txtrentLicenseNo.Text = "";
                //txtrentShopNo.Text = "";
                txtTo1.Value = DateTime.Today;
                txtNoOfmonths.Value = 0;
                txtNoOfSeats.Value = 0;
                lblID1.Text = "-1";
                mainbar1.lblCount.Text = " 0 / 0";
            }
        }

        private void bindLicences1(int lID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val1 = from c in db.CompanyLicences
                           where c.companyLicencesID == licenseID1
                           select c;
                CompanyLicence cl1 = val1.FirstOrDefault();

                txtcommercialName1.Text = cl1 == null ? "" : cl1.commercialName;
                txtlicenseNo1.Text = cl1 == null ? "" : cl1.licenseNo;

            }
        }

        private void bindCustomer1(int cID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val2 = from c in db.Customers
                           where c.customerID == cID
                           select c;
                Customer cl2 = val2.FirstOrDefault();

                txtCustomerName1.Text = cl2 == null ? "" : cl2.customer_Name;
            }
        }


       // rent a shop

        void btnAdd_Click2(object sender, EventArgs e)
        {
            change2 = "add";
            enableControls2();

            customerID2 = -1;
            shopID2 = -1;

            txtcommercialName2.Text = "";
            txtCustomerName2.Text = "";
            txtDamanAmount2.Text = "0";
            txtFrom2.Value = DateTime.Today;
            txtlicenseNo2.Text = "";
            txtRentalAmount2.Text = "0";
            //txtrentLicenseNo.Text = "";
            txtrentShopNo.Text = "";
            txtTo2.Value = DateTime.Today;

        }

        void btnEdit_Click2(object sender, EventArgs e)
        {
            if (lblID2.Text != "-1")
            {
                change2 = "edit";
                enableControls2();
            }
        }

        void btnDelete_Click2(object sender, EventArgs e)
        {
            if (lblID2.Text != "-1")
            {
                DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {

                        int bbID = int.Parse(lblID2.Text);
                        RentShop bb = (from c in db.RentShops
                                         where c.rentShopID ==
                                             bbID
                                         select c).FirstOrDefault();

                        db.RentShops.DeleteObject(bb);
                        int noOfAffectedRows = db.SaveChanges();
                        if (noOfAffectedRows > 0)
                        {
                            position2 = 0;
                            getData2();
                            bindData2();
                        }
                    }
                }
            }
        }

        void btnCancel_Click2(object sender, EventArgs e)
        {
            change2 = "";
            disableControls2();
            bindData2();
        }

        void btnSave_Click2(object sender, EventArgs e)
        {
            int rowsAffected = 0;
            if (change2 == "add")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                   
                        RentShop bb = new RentShop();

                        bb.customerID = customerID2;
                        bb.licencesShopID = shopID2;
                        bb.rentShop_DamanAmount = float.Parse(txtDamanAmount2.Text);
                        bb.rentShop_from = txtFrom2.Value.Date;
                        bb.rentShop_RentalAmount = float.Parse(txtRentalAmount2.Text);
                        bb.rentShopNo = txtrentShopNo.Text;
                        bb.rentShop_active = true;
                        //txtrentShopNo.Text = "";
                        bb.rentShop_to = txtTo2.Value.Date;
                        bb.rentShop_date = DateTime.Now;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.RentShops.AddObject(bb);
                        rowsAffected = db.SaveChanges();


                        
                    
                }
            }
            if (change2 == "edit")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int bbID = int.Parse(lblID2.Text);
                    RentShop bb = (from c in db.RentShops
                                     where c.rentShopID ==
                                         bbID
                                     select c).FirstOrDefault();

                    bb.customerID = customerID2;
                    bb.licencesShopID = shopID2;
                    bb.rentShop_DamanAmount = float.Parse(txtDamanAmount2.Text);
                    bb.rentShop_from = txtFrom2.Value.Date;
                    bb.rentShop_RentalAmount = float.Parse(txtRentalAmount2.Text);
                    bb.rentShopNo = txtrentShopNo.Text;
                    //txtrentShopNo.Text = "";
                    bb.rentShop_to = txtTo2.Value.Date;
                    bb.rentShop_date = DateTime.Now;


                    bb.edited_Date = DateTime.Now;
                    bb.editedBy = MainClass.userID;

                    //db.RentLicenses.AddObject(bb);
                    rowsAffected = db.SaveChanges();
                }
            }


            if (rowsAffected > 0)
            {
                disableControls2();

                getData2();
                if (change2 == "add")
                {
                    position2 = lstRentShop.Count - 1;
                }
                change2 = "";

                bindData2();
            }
        }

        void btnFind_Click2(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void btnFirst_Click2(object sender, EventArgs e)
        {
            if (position2 > 0)
            {
                position2 = 0;
                bindData2();
            }
        }

        void btnPrevious_Click2(object sender, EventArgs e)
        {
            if (position2 > 0)
            {
                position2 -= 1;
                bindData2();
            }
        }

        void btnNext_Click2(object sender, EventArgs e)
        {
            if (position2 < lstRentShop.Count - 1)
            {
                position2 += 1;
                bindData2();
            }
        }

        void btnLast_Click2(object sender, EventArgs e)
        {
            if (position2 < lstRentShop.Count - 1)
            {
                position2 = lstRentShop.Count - 1;
                bindData2();
            }
        }

        void btnExit_Click2(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls2()
        {
            mainbar2.enableButtons();

            txtcommercialName2.ReadOnly = true;
            txtCustomerName2.ReadOnly = true;
            txtDamanAmount2.ReadOnly = true;

            txtlicenseNo2.ReadOnly = true;
            txtRentalAmount2.ReadOnly = true;
            txtrentShopNo.ReadOnly = true;
            //txtrentShopNo.Text = "";
            txtTo2.Enabled = false;
            txtFrom2.Enabled = false;

            btnFindCustomer2.Enabled = false;
            btnFindLicenses2.Enabled = false;
            pageRentAlicences.Enabled = true;
        }

        private void enableControls2()
        {
            mainbar2.disableButton();
            //txtcommercialName1.ReadOnly = false;
            //txtCustomerName1.ReadOnly = false;
            //txtrentLicenseNo.ReadOnly = false;

            txtDamanAmount2.ReadOnly = false;

            //txtlicenseNo1.ReadOnly = false;
            txtRentalAmount2.ReadOnly = false;
            //txtrentLicenseNo.ReadOnly = false;
            txtrentShopNo.ReadOnly = false;
            txtTo2.Enabled = true;
            txtFrom2.Enabled = true;

            btnFindCustomer2.Enabled = true;
            btnFindLicenses2.Enabled = true;
            pageRentAlicences.Enabled = false;
            
        }

        private void getData2()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstRentShop = (from c in db.RentShops orderby c.rentShopID descending select c).ToList();
            }
        }

        private void bindData2()
        {
            if (lstRentShop.Count > 0)
            {
                customerID2 = lstRentShop[position2].customerID.Value;
                shopID2 = lstRentShop[position2].licencesShopID.Value;

                txtDamanAmount2.Text = lstRentShop[position2].rentShop_DamanAmount.Value.ToString();
                txtFrom2.Value = lstRentShop[position2].rentShop_from ?? DateTime.Today;
                txtRentalAmount2.Text = lstRentShop[position2].rentShop_RentalAmount.Value.ToString();
                //txtrentLicenseNo.Text = lstRentShop[position2].rentLicenseNo;
                txtrentShopNo.Text = lstRentShop[position2].rentShopNo;
                txtTo2.Value = lstRentShop[position2].rentShop_to ?? DateTime.Today;
                lblID2.Text = lstRentShop[position2].rentShopID.ToString();
                mainbar2.lblCount.Text = position2 + 1 + " / " + lstRentShop.Count;

                bindCustomer2(customerID2);
                bindLicences2(shopID2);


            }
            else
            {
                customerID2 = -1;
                shopID2 = -1;

                txtcommercialName2.Text = "";
                txtCustomerName2.Text = "";
                txtDamanAmount2.Text = "0";
                txtFrom2.Value = DateTime.Today;
                txtlicenseNo2.Text = "";
                txtRentalAmount2.Text = "0";
                //txtrentLicenseNo.Text = "";
                txtrentShopNo.Text = "";
                txtTo2.Value = DateTime.Today;

                lblID2.Text = "-1";
                mainbar2.lblCount.Text = " 0 / 0";
            }
        }

        private void bindLicences2(int lID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val1 = from c in db.CompanyLicences
                           where c.companyLicencesID == shopID2
                           select c;
                CompanyLicence cl1 = val1.FirstOrDefault();

                txtcommercialName2.Text = cl1.commercialName;
                txtlicenseNo2.Text = cl1.licenseNo;

            }
        }

        private void bindCustomer2(int cID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val2 = from c in db.Customers
                           where c.customerID == cID
                           select c;
                Customer cl2 = val2.FirstOrDefault();

                txtCustomerName2.Text = cl2.customer_Name;
            }
        }

        // end of rent shop

        private void btnFindLicenses1_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false,false);
            if (change1 == "add")
            {
                frm.isCurrentRentedLicencesInclude = false;
            }
            frm.ShowDialog();
            if (frm.found)
            {
                licenseID1 = frm.licID;
                txtlicenseNo1.Text = frm.licNO;
                txtcommercialName1.Text = frm.licName;
            }
        }

        private void btnFindCustomer1_Click(object sender, EventArgs e)
        {
            Definitions.frmFindCustomers frm = new Definitions.frmFindCustomers();
            frm.ShowDialog();
            if (frm.found)
            {
                customerID1 = frm.cID;
                txtCustomerName1.Text = frm.cName;
            }
        }

        private void btnFindLicenses2_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false,false);
            //if (change2 == "add")
            //{
            //    frm.isCurrentRentedLicencesInclude = false;
            //}
            frm.ShowDialog();
            if (frm.found)
            {
                int ID2 = frm.licID;
                txtlicenseNo2.Text = frm.licNO;
                txtcommercialName2.Text = frm.licName;
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c in db.LicensesShops
                              where c.companyLicencesID == ID2
                              select new { c.licencesShopID };

                    shopID2 = val.SingleOrDefault().licencesShopID;
                }
            }
        }

        private void btnFindCustomer2_Click(object sender, EventArgs e)
        {
            Definitions.frmFindCustomers frm = new Definitions.frmFindCustomers();
            frm.ShowDialog();
            if (frm.found)
            {
                customerID2 = frm.cID;
                txtCustomerName2.Text = frm.cName;
            }
        }

        private void txtRentalAmount1_TextChanged(object sender, EventArgs e)
        {
            if (change1 == "add")
            {
                decimal rate;
                txtDamanAmount1.Text = decimal.TryParse(txtRentalAmount1.Text, out rate)
                    ? (rate * txtNoOfmonths.Value).ToString() : "0";
            }
            else if (change1 == "edit" && displayedSchedule.Count > 0)
            {
                decimal total = displayedSchedule.Where(d => d.rd_mNo <= txtNoOfmonths.Value)
                    .Sum(d => LicenseRentalRules.Amount(d.rd_amount ?? 0));
                int extraMonths = Math.Max(0, (int)txtNoOfmonths.Value - displayedSchedule.Count);
                total += extraMonths * LicenseRentalRules.Amount(displayedSchedule.Last().rd_amount ?? 0);
                txtDamanAmount1.Text = total.ToString();
            }
        }

        private void txtNoOfmonths_ValueChanged(object sender, EventArgs e)
        {
            if (change1 == "" || txtNoOfmonths.Value < 1) return;
            try
            {
                txtTo1.Value = LicenseRentalRules.InstalmentEnd(txtDateFrom1.Value, (int)txtNoOfmonths.Value);
                txtRentalAmount1_TextChanged(sender, e);
            }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("مدة العقد تتجاوز نطاق التاريخ المتاح"); }
        }

        private void ChangeRentingAmount_Click(object sender, EventArgs e)
        {
            if (change1 != "" || lblID1.Text == "-1") return;
            int id = int.Parse(lblID1.Text);
            using (var frm = new frmChangeRentingAmount())
            {
                frm._rentLicenseID = id;
                frm.ShowDialog();
            }
            getData1();
            position1 = Math.Max(0, lstLicense.FindIndex(r => r.rentLicenseID == id));
            bindData1();
        }


        private void txtrentLicenseNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 18)
            {
                //btnEndLicencesRent.Visible = true;
            }

        }

        private void txtrentLicenseNo_KeyUp(object sender, KeyEventArgs e)
        {
            //btnEndLicencesRent.Visible = false;
        }

        private void btnEndLicencesRent_Click(object sender, EventArgs e)
        {
            try
            {
                if (change1 != "" || lblID1.Text == "-1") return;
                if (MessageBox.Show("هل تريد إنهاء الإيجار ؟", "Confirmation Of Ending of renting",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                new LicenseRentalWorkflow().CloseRental(int.Parse(lblID1.Text));
                getData1();
                bindData1();
                MessageBox.Show("تم إنهاء الإيجار بنجاح");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtrentLicenseNo_Leave(object sender, EventArgs e)
        {
            //btnEndLicencesRent.Visible = false;
        }






    }
}
