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
using BarakalAlkheir.Lic;

namespace BarakalAlkheir.CarsM
{
    public partial class frmRentACar : Telerik.WinControls.UI.RadForm
    {
        string change1 = "";
        int position1 = 0;


        List<CarCustomerRent> lstCarRent = null;
        components.banner mainbar1 = new components.banner("pageRentCar");


        int customerID1 = -1;
        int carID = -1;


        public frmRentACar()
        {
            InitializeComponent();
        }

        private void radPageView1_PageRemoving(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void frmRentALicense_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar1.Dock = DockStyle.Top;
                pageRentCar.Controls.Add(mainbar1);


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

                PublicClass.bindEmployee(CmbDriverName);
                CmbDriverName.SelectedIndex = 0;

                disableControls1();


                getData1();
                bindData1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // rent a License
        void btnAdd_Click1(object sender, EventArgs e)
        {
            try
            {
                change1 = "add";
                enableControls1();

                customerID1 = -1;
                carID = -1;

                txtCarName1.Text = "";
                txtCarBrand.Text = "";
                txtCustomerName1.Text = "";
                txtDamanAmount1.Text = "0";
                CmbDriverName.SelectedIndex = 0;
                txtFrom1.Text = DateTime.Now.ToShortDateString();
                txtCarNo1.Text = "";
                txtRentalAmount1.Text = "0";
                txtNoOfmonths.Value = 0;
                this.txtrentCarNo.Text = "";
                //txtrentShopNo.Text = "";
                txtTo1.Text = DateTime.Now.ToShortDateString();
            }
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

        void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                if (lblID1.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            using (TransactionScope ts = new TransactionScope())
                            {
                                int bbID = int.Parse(lblID1.Text);

                                List<carCustomerRentDetail> bb1 = (from c in db.carCustomerRentDetails
                                                                   where c.carCustomerRentID ==
                                                                       bbID
                                                                   select c).ToList();

                                foreach (var item in bb1)
                                {
                                    db.carCustomerRentDetails.DeleteObject(item);
                                    db.SaveChanges();
                                }


                                CarCustomerRent bb = (from c in db.CarCustomerRents
                                                      where c.carCustomerRentID ==
                                                          bbID
                                                      select c).FirstOrDefault();

                                db.CarCustomerRents.DeleteObject(bb);
                                int noOfAffectedRows = db.SaveChanges();
                                if (noOfAffectedRows > 0)
                                {
                                    position1 = 0;
                                    getData1();
                                    bindData1();
                                }



                                db.AcceptAllChanges();
                                ts.Complete();

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                txtNoOfmonths_ValueChanged(sender, e);

                int rowsAffected = 0;
                if (change1 == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            CarCustomerRent bb = new CarCustomerRent();

                            bb.customerID = customerID1;
                            bb.carID = carID;
                            bb.carCustomerRent_DamanAmount = float.Parse(txtDamanAmount1.Text);
                            bb.carCustomer_from = DateTime.Parse(txtFrom1.Text);
                            bb.carCustomerRent_RentalAmount = float.Parse(txtRentalAmount1.Text);
                            bb.carCustomer_rentNo = txtrentCarNo.Text;
                            bb.carCustomerRent_active = true;

                            //txtrentShopNo.Text = "";
                            bb.carCustomer_to = DateTime.Parse(txtTo1.Text);
                            bb.carCustomer_noOfMonths = int.Parse(txtNoOfmonths.Value.ToString());
                            bb.carCustomer_Date = DateTime.Now;
                            bb.carCustomerRent_DriverID = int.Parse(CmbDriverName.SelectedValue.ToString());
                            bb.carCustomerRent_DriverName = CmbDriverName.Text;
                            bb.carCustomerRent_Remarks = "";

                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.CarCustomerRents.AddObject(bb);
                            rowsAffected = db.SaveChanges();


                            for (int i = 0; i < int.Parse(txtNoOfmonths.Value.ToString()); i++)
                            {
                                carCustomerRentDetail rld = new carCustomerRentDetail();
                                rld.added_Date = DateTime.Now;
                                rld.addedBy = MainClass.userID;
                                rld.edited_Date = DateTime.Now;
                                rld.editedBy = MainClass.userID;

                                rld.rd_isPaid = false;
                                rld.rd_amount = float.Parse(txtRentalAmount1.Text);
                                rld.rd_fromDate = DateTime.Parse(txtFrom1.Text).AddMonths(i);
                                rld.rd_mNo = i + 1;
                                rld.rd_toDate = DateTime.Parse(txtFrom1.Text).AddMonths(i + 1).AddDays(-1);
                                rld.carCustomerRentID = bb.carCustomerRentID;

                                db.carCustomerRentDetails.AddObject(rld);
                                db.SaveChanges();
                            }

                            db.AcceptAllChanges();
                            ts.Complete();
                        }
                    }
                }
                if (change1 == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            int bbID = int.Parse(lblID1.Text);
                            CarCustomerRent bb = (from c in db.CarCustomerRents
                                                  where c.carCustomerRentID ==
                                                      bbID
                                                  select c).FirstOrDefault();

                            bb.customerID = customerID1;
                            bb.carID = carID;
                            bb.carCustomerRent_DamanAmount = float.Parse(txtDamanAmount1.Text);
                            bb.carCustomer_from = DateTime.Parse(txtFrom1.Text);
                            bb.carCustomerRent_RentalAmount = float.Parse(txtRentalAmount1.Text);
                            bb.carCustomer_rentNo = txtrentCarNo.Text;
                            bb.carCustomerRent_active = true;

                            //txtrentShopNo.Text = "";
                            bb.carCustomer_to = DateTime.Parse(txtTo1.Text);
                            bb.carCustomer_noOfMonths = int.Parse(txtNoOfmonths.Value.ToString());
                            bb.carCustomer_Date = DateTime.Now;
                            bb.carCustomerRent_DriverID = int.Parse(CmbDriverName.SelectedValue.ToString());
                            bb.carCustomerRent_DriverName = CmbDriverName.Text;
                            bb.carCustomerRent_Remarks = "";

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            //db.CarCustomerRents.AddObject(bb);
                            rowsAffected = db.SaveChanges();


                            // get the no ofMonths
                            int noOFMonths = int.Parse(txtNoOfmonths.Value.ToString());
                            // delete the remaining 
                            List<carCustomerRentDetail> bb11 = (from c in db.carCustomerRentDetails
                                                                where c.carCustomerRentID == bbID
                                                                && c.rd_mNo > noOFMonths
                                                                select c).ToList();
                            foreach (var item in bb11)
                            {
                                if (item.rd_isPaid.Value)
                                {
                                    ts.Dispose();
                                    MessageBox.Show("لا يمكنك تعديل التاريخ وذلك بسبب تعارضه مع المدفوعات");
                                    return;
                                }
                                else
                                {
                                    db.carCustomerRentDetails.DeleteObject(item);
                                    db.SaveChanges();
                                }
                            }

                            var bb22 = (from c in db.carCustomerRentDetails
                                        where c.carCustomerRentID == bbID
                                        select c).Max(cc => cc.rd_mNo);

                            var bb33 = (from c in db.carCustomerRentDetails
                                        where c.carCustomerRentID == bbID
                                        select c).Max(cc => cc.rd_fromDate);


                            for (int i = 0; i < int.Parse(txtNoOfmonths.Value.ToString()); i++)
                            {


                                // get unpaid rentLicencesDetails
                                carCustomerRentDetail rld = (from c in db.carCustomerRentDetails
                                                             where c.carCustomerRentID == bbID
                                                             && c.rd_mNo == i + 1
                                                             select c).FirstOrDefault();
                                if (rld != null && !rld.rd_isPaid.Value)
                                {
                                    rld.edited_Date = DateTime.Now;
                                    rld.editedBy = MainClass.userID;

                                    rld.rd_amount = float.Parse(txtRentalAmount1.Text);
                                    rld.rd_fromDate = DateTime.Parse(txtFrom1.Text).AddMonths(i);
                                    rld.rd_mNo = i + 1;
                                    rld.rd_toDate = DateTime.Parse(txtFrom1.Text).AddMonths(i + 1).AddDays(-1);
                                    rld.carCustomerRentID = bb.carCustomerRentID;
                                    db.SaveChanges();
                                }

                            }

                            if (bb22 != null)
                            {
                                if (int.Parse(bb22.Value.ToString()) < noOFMonths)
                                {
                                    // add the remainging months 
                                    for (int i = 0; i < noOFMonths - int.Parse(bb22.Value.ToString()); i++)
                                    {
                                        carCustomerRentDetail rld = new carCustomerRentDetail();
                                        rld.added_Date = DateTime.Now;
                                        rld.addedBy = MainClass.userID;
                                        rld.edited_Date = DateTime.Now;
                                        rld.editedBy = MainClass.userID;

                                        rld.rd_isPaid = false;
                                        rld.rd_amount = float.Parse(txtRentalAmount1.Text);
                                        rld.rd_fromDate = bb33.Value.AddMonths(i + 1);
                                        rld.rd_mNo = int.Parse(bb22.Value.ToString()) + i + 1;
                                        rld.rd_toDate = bb33.Value.AddMonths(i + 2).AddDays(-1);
                                        rld.carCustomerRentID = bb.carCustomerRentID;

                                        db.carCustomerRentDetails.AddObject(rld);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < noOFMonths; i++)
                                {
                                    carCustomerRentDetail rld = new carCustomerRentDetail();
                                    rld.added_Date = DateTime.Now;
                                    rld.addedBy = MainClass.userID;
                                    rld.edited_Date = DateTime.Now;
                                    rld.editedBy = MainClass.userID;

                                    rld.rd_isPaid = false;
                                    rld.rd_amount = float.Parse(txtRentalAmount1.Text);
                                    rld.rd_fromDate = DateTime.Parse(txtFrom1.Text).AddMonths(i);
                                    rld.rd_mNo = i + 1;
                                    rld.rd_toDate = DateTime.Parse(txtFrom1.Text).AddMonths(i + 1).AddDays(-1);
                                    rld.carCustomerRentID = bb.carCustomerRentID;

                                    db.carCustomerRentDetails.AddObject(rld);
                                    db.SaveChanges();
                                }
                            }

                            db.AcceptAllChanges();
                            ts.Complete();


                        }
                    }
                }


                disableControls1();

                getData1();
                if (change1 == "add")
                {
                    position1 = lstCarRent.Count - 1;
                }
                change1 = "";

                bindData1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void btnFind_Click1(object sender, EventArgs e)
        {
            CarsM.frmfindCars frm = new frmfindCars(true);
            frm.isCurrentRentedCar = true;
            frm.ShowDialog();
            if (frm.found)
            {
                //carID = frm.carID;
                txtCarNo1.Text = frm.carNO;
                this.txtCarBrand.Text = frm.carName;
               
                //lblCarID.Text = frm.carID.ToString();

                CarCustomerRent cl = this.lstCarRent.Find(c => c.carID == frm.carID && c.carCustomerRent_active==true);
                position1 = lstCarRent.IndexOf(cl);
                bindData1();
            }
        }

        void btnFirst_Click1(object sender, EventArgs e)
        {
            if (position1 > 0)
            {
                position1 = 0;
                bindData1();
            }
        }

        void btnPrevious_Click1(object sender, EventArgs e)
        {
            if (position1 > 0)
            {
                position1 -= 1;
                bindData1();
            }
        }

        void btnNext_Click1(object sender, EventArgs e)
        {
            if (position1 < lstCarRent.Count - 1)
            {
                position1 += 1;
                bindData1();
            }
        }

        void btnLast_Click1(object sender, EventArgs e)
        {
            if (position1 < lstCarRent.Count - 1)
            {
                position1 = lstCarRent.Count - 1;
                bindData1();
            }
        }

        void btnExit_Click1(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls1()
        {
            mainbar1.enableButtons();

            txtCarName1.ReadOnly = true;
            txtCarBrand.ReadOnly = true;
            txtCustomerName1.ReadOnly = true;
            txtDamanAmount1.ReadOnly = true;
            CmbDriverName.Enabled = false;

            txtCarNo1.ReadOnly = true;
            txtRentalAmount1.ReadOnly = true;
            this.txtrentCarNo.ReadOnly = true;
            //txtrentShopNo.Text = "";
            txtTo1.Enabled = false;
            txtFrom1.Enabled = false;
            txtNoOfmonths.Enabled = false;
            btnFindCustomer1.Enabled = false;
            btnFindLicenses1.Enabled = false;

        }

        private void enableControls1()
        {
            mainbar1.disableButton();

           // txtDamanAmount1.ReadOnly = false;
            CmbDriverName.Enabled = true;
            txtRentalAmount1.ReadOnly = false;
            txtrentCarNo.ReadOnly = false;
            //txtTo1.Enabled = true;
            txtNoOfmonths.Enabled = true;
            txtFrom1.Enabled = true;

            btnFindCustomer1.Enabled = true;
            btnFindLicenses1.Enabled = true;
            if (change1 == "edit")
            {
                txtFrom1.Enabled = false;
            }
            else
            {
                txtFrom1.Enabled = true;
            }
        }

        private void getData1()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCarRent = (from c in db.CarCustomerRents orderby c.carCustomerRentID  select c).ToList();
            }
        }

        private void bindData1()
        {
            if (lstCarRent.Count > 0)
            {
                customerID1 =  lstCarRent[position1].customerID.Value;
                carID =  lstCarRent[position1].carID.Value;
                txtDamanAmount1.Text =  lstCarRent[position1].carCustomerRent_DamanAmount.Value.ToString();
                //txtDriverName.Text = lstCarRent[position1].carCustomerRent_DriverName;
                txtFrom1.Text =  lstCarRent[position1].carCustomer_from.Value.ToShortDateString();
                txtRentalAmount1.Text = lstCarRent[position1].carCustomerRent_RentalAmount.Value.ToString();
                this.txtrentCarNo.Text = lstCarRent[position1].carCustomer_rentNo;

                //txtrentShopNo.Text = "";
                txtTo1.Text = lstCarRent[position1].carCustomer_to.Value.ToShortDateString();
                txtNoOfmonths.Value = lstCarRent[position1].carCustomer_noOfMonths.Value;
                lblID1.Text = lstCarRent[position1].carCustomerRentID.ToString();

                for (int i = 0; i < CmbDriverName.Items.Count; i++)
                {
                    if (CmbDriverName.Items[i].Value.ToString() == lstCarRent[position1].carCustomerRent_DriverID.ToString())
                    {
                        CmbDriverName.SelectedIndex = i;
                        break;
                    }
                }


                mainbar1.lblCount.Text = position1 + 1 + " / " + lstCarRent.Count;

                bindCustomer1(customerID1);
                bindCar1(carID);

                           
            }
            else
            {
                customerID1 = -1;
                carID = -1;

                txtCarName1.Text = "";
                txtCarBrand.Text = "";
                txtCustomerName1.Text = "";
                txtDamanAmount1.Text = "0";
                CmbDriverName.SelectedIndex = 0;
                txtFrom1.Text = DateTime.Now.ToShortDateString();
                txtCarNo1.Text = "";
                txtRentalAmount1.Text = "0";
                txtrentCarNo.Text = "";
                //txtrentShopNo.Text = "";
                txtTo1.Text = DateTime.Now.ToShortDateString();
                txtNoOfmonths.Value = 0;
                lblID1.Text = "-1";
                mainbar1.lblCount.Text = " 0 / 0";
            }
        }

        private void bindCar1(int lID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val1 = from c in db.Cars
                           where c.carID == lID
                           select c;
                Car cl1 = val1.FirstOrDefault();

                txtCarName1.Text = cl1.car_Name;
                txtCarNo1.Text = cl1.car_No;
                txtCarBrand.Text = cl1.car_Brand;
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

                txtCustomerName1.Text = cl2.customer_Name;
            }
        }


        private void btnFindLicenses1_Click(object sender, EventArgs e)
        {
            CarsM.frmfindCars frm = new frmfindCars(false);
            if (change1 == "add")
            {
                frm.isCurrentRentedCarInclude = false;
            }
            frm.ShowDialog();
            if (frm.found)
            {
                carID = frm.carID;
                txtCarNo1.Text = frm.carNO;
                this.txtCarBrand.Text = frm.carName;
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

        private void txtRentalAmount1_TextChanged(object sender, EventArgs e)
        {
            if (change1 != "")
            {
                double ra = 0;
                if (double.TryParse(txtRentalAmount1.Text, out ra))
                {
                    int noOfMonths = int.Parse(txtNoOfmonths.Text);
                    txtDamanAmount1.Text = (ra * noOfMonths).ToString();
                }
            }
        }

        private void txtNoOfmonths_ValueChanged(object sender, EventArgs e)
        {
            int nn = 0;
            if (int.TryParse(txtNoOfmonths.Value.ToString(), out nn))
            {
                if (nn > 0)
                {
                    DateTime df = DateTime.Parse(txtFrom1.Text);
                    txtTo1.Text = df.AddMonths(nn).AddDays(-1).ToShortDateString();
                    txtRentalAmount1_TextChanged(sender, e);
                }
            }
        }

        private void ChangeRentingAmount_Click(object sender, EventArgs e)
        {
            if (lblID1.Text != "-1")
            {
                frmChangeRentingAmountCar frm = new frmChangeRentingAmountCar();
                frm._CarCustomerRenteID = int.Parse(lblID1.Text);
                frm.ShowDialog();
            }
        }

        private void btnFindEmployee_Click(object sender, EventArgs e)
        {
            emp.frmFindEmployees frm = new emp.frmFindEmployees();
            frm.ShowDialog();
            if (frm.found)
            {
                for (int i = 0; i < CmbDriverName.Items.Count; i++)
                {
                    if (CmbDriverName.Items[i].Value.ToString() == frm.empID.ToString())
                    {
                        CmbDriverName.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnEndLicencesRent_Click(object sender, EventArgs e)
        {
            try
            {
                int rentID1 = int.Parse(lblID1.Text);

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    float rAmount = 0;
                    float pAmount = 0;

                    var val1 = (from c in db.CarCustomerRents
                                where c.carCustomerRentID == rentID1
                                select c).ToList();

                    foreach (var item in val1)
                    {
                        rAmount += item.carCustomerRent_RentalAmount.Value;
                    }

                    var val2 = (from c in db.CarCustomerRentPayments
                                where c.carCustomerRentID == rentID1
                                select c).ToList();

                    foreach (var item in val2)
                    {
                        pAmount += item.carCustomerPayment_amount.Value;
                    }

                    if (rAmount - pAmount > 0)
                    {
                        MessageBox.Show("لا يمكنك انهاء الايجار , يجب دفع المبلغ المتبقي");
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("هل تريد انهاء الايجار ؟", "Confirmation Of Ending of renting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            CarCustomerRent bb = (from c in db.CarCustomerRents
                                             where c.carCustomerRentID ==
                                                 rentID1
                                             select c).FirstOrDefault();
                            bb.carCustomerRent_active = false;
                            int no = db.SaveChanges();
                            if (no > 0)
                            {
                                MessageBox.Show("تم انهاء الايجار بنجاح");
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }









    }
}
