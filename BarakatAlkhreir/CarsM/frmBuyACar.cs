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
    public partial class frmBuyACar : Telerik.WinControls.UI.RadForm
    {
        string change1 = "";
        int position1 = 0;


        List<CarSupplierRent> lstCarBuy = null;
        components.banner mainbar1 = new components.banner("pageBuyACar");


        int supplierID = -1;
        int carID = -1;


        public frmBuyACar()
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
                pageBuyACar.Controls.Add(mainbar1);


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
            change1 = "add";
            enableControls1();

            supplierID = -1;
            carID = -1;

            txtCarName1.Text = "";
            txtCarBrand.Text = "";
            txtCustomerName1.Text = "";
            txtcarSupplierRent_totalAmount.Text = "0";
         
            txtcarSupplierRent_firstPaymentDate.Text = DateTime.Now.ToShortDateString();
            txtCarNo1.Text = "";
            txtcarSupplierRent_MonthlyAmount.Text = "0";
            txtNoOfmonths.Value = 0;
            this.txtrentCarNo.Text = "";
            //txtrentShopNo.Text = "";
            txtTo1.Text = DateTime.Now.ToShortDateString();
            txtcarSupplierRent_lastPaymentAmount.Text = "0";
            txtcarSupplierRent_firstPaymentAmount.Text = "0";

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

                                List<CarSupplierRentDetail> bb1 = (from c in db.CarSupplierRentDetails
                                                                   where c.carSupplierRentID ==
                                                                       bbID
                                                                   select c).ToList();

                                foreach (var item in bb1)
                                {
                                    db.CarSupplierRentDetails.DeleteObject(item);
                                    db.SaveChanges();
                                }


                                CarSupplierRent bb = (from c in db.CarSupplierRents
                                                      where c.carSupplierRentID ==
                                                          bbID
                                                      select c).FirstOrDefault();

                                db.CarSupplierRents.DeleteObject(bb);
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
                            CarSupplierRent bb = new CarSupplierRent();

                            bb.carSuppliserID = supplierID;
                            bb.carID = carID;

                            bb.carSupplierRent_firstPaymentDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text);
                            bb.carSupplierRent_from = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text);
                            bb.carSupplierRent_Date = DateTime.Now;
                            bb.carSupplierRent_to = DateTime.Parse(txtTo1.Text);

                            bb.carRentNo = txtrentCarNo.Text;
                            bb.carSupplierRent_isactive = true;
                            bb.carSupplierRent_isBuyed = true;
                            bb.carSupplierRent_isRented = false;

                            //txtrentShopNo.Text = "";

                            bb.carSupplierRent_noOfMonths = int.Parse(txtNoOfmonths.Value.ToString());


                            bb.carSupplierRent_totalAmount = float.Parse(txtcarSupplierRent_totalAmount.Text);
                            bb.carSupplierRent_MonthlyAmount = float.Parse(txtcarSupplierRent_MonthlyAmount.Text);
                            bb.carSupplierRent_firstPaymentAmount = float.Parse(txtcarSupplierRent_firstPaymentAmount.Text);
                            bb.carSupplierRent_lastPaymentAmount = float.Parse(txtcarSupplierRent_lastPaymentAmount.Text);


                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.CarSupplierRents.AddObject(bb);
                            rowsAffected = db.SaveChanges();



                            for (int i = 0; i < int.Parse(txtNoOfmonths.Value.ToString()); i++)
                            {
                                CarSupplierRentDetail rld = new CarSupplierRentDetail();
                                rld.added_Date = DateTime.Now;
                                rld.addedBy = MainClass.userID;
                                rld.edited_Date = DateTime.Now;
                                rld.editedBy = MainClass.userID;

                                rld.rd_isPaid = false;
                                rld.rd_amount = float.Parse(txtcarSupplierRent_MonthlyAmount.Text);
                                rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(i);
                                rld.rd_mNo = i + 1;
                                rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(i + 1).AddDays(-1);
                                rld.carSupplierRentID = bb.carSupplierRentID;
                                rld.rd_isMonthlyPayment = true;
                                rld.rd_isFirstPayment = false;
                                rld.rd_isLastPayment = false;
                                db.CarSupplierRentDetails.AddObject(rld);
                                db.SaveChanges();
                            }

                            if (float.Parse(txtcarSupplierRent_firstPaymentAmount.Text) > 0)
                            {
                                CarSupplierRentDetail rld = new CarSupplierRentDetail();
                                rld.added_Date = DateTime.Now;
                                rld.addedBy = MainClass.userID;
                                rld.edited_Date = DateTime.Now;
                                rld.editedBy = MainClass.userID;

                                rld.rd_isPaid = false;
                                rld.rd_amount = float.Parse(txtcarSupplierRent_firstPaymentAmount.Text);
                                rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(-1);
                                rld.rd_mNo = 0;
                                rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddDays(-1);
                                rld.carSupplierRentID = bb.carSupplierRentID;
                                rld.rd_isFirstPayment = true;
                                rld.rd_isLastPayment = false;
                                rld.rd_isMonthlyPayment = false;
                                db.CarSupplierRentDetails.AddObject(rld);
                                db.SaveChanges();
                            }

                            if (float.Parse(txtcarSupplierRent_lastPaymentAmount.Text) > 0)
                            {
                                CarSupplierRentDetail rld = new CarSupplierRentDetail();
                                rld.added_Date = DateTime.Now;
                                rld.addedBy = MainClass.userID;
                                rld.edited_Date = DateTime.Now;
                                rld.editedBy = MainClass.userID;

                                rld.rd_isPaid = false;
                                rld.rd_amount = float.Parse(txtcarSupplierRent_lastPaymentAmount.Text);
                                rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(int.Parse(txtNoOfmonths.Value.ToString()));
                                rld.rd_mNo = 1000;
                                rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(int.Parse(txtNoOfmonths.Value.ToString()) + 1).AddDays(-1);
                                rld.carSupplierRentID = bb.carSupplierRentID;
                                rld.rd_isFirstPayment = false;
                                rld.rd_isLastPayment = true;
                                rld.rd_isMonthlyPayment = false;
                                db.CarSupplierRentDetails.AddObject(rld);
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
                            CarSupplierRent bb = (from c in db.CarSupplierRents
                                                  where c.carSupplierRentID ==
                                                      bbID
                                                  select c).FirstOrDefault();

                            bb.carSuppliserID = supplierID;
                            bb.carID = carID;

                            bb.carSupplierRent_firstPaymentDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text);
                            bb.carSupplierRent_from = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text);
                            bb.carSupplierRent_Date = DateTime.Now;
                            bb.carSupplierRent_to = DateTime.Parse(txtTo1.Text);

                            bb.carRentNo = txtrentCarNo.Text;
                            bb.carSupplierRent_isactive = true;
                            bb.carSupplierRent_isBuyed = true;
                            bb.carSupplierRent_isRented = false;

                            //txtrentShopNo.Text = "";

                            bb.carSupplierRent_noOfMonths = int.Parse(txtNoOfmonths.Value.ToString());


                            bb.carSupplierRent_totalAmount = float.Parse(txtcarSupplierRent_totalAmount.Text);
                            bb.carSupplierRent_MonthlyAmount = float.Parse(txtcarSupplierRent_MonthlyAmount.Text);
                            bb.carSupplierRent_firstPaymentAmount = float.Parse(txtcarSupplierRent_firstPaymentAmount.Text);
                            bb.carSupplierRent_lastPaymentAmount = float.Parse(txtcarSupplierRent_lastPaymentAmount.Text);

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            //db.CarSupplierRents.AddObject(bb);
                            rowsAffected = db.SaveChanges();


                            // get the no ofMonths
                            int noOFMonths = int.Parse(txtNoOfmonths.Value.ToString());
                            // delete the remaining 
                            List<CarSupplierRentDetail> bb11 = (from c in db.CarSupplierRentDetails
                                                                where c.carSupplierRentID == bbID
                                                                && c.rd_mNo > noOFMonths
                                                                && c.rd_isMonthlyPayment == true
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
                                    db.CarSupplierRentDetails.DeleteObject(item);
                                    db.SaveChanges();
                                }
                            }

                            var bb22 = (from c in db.CarSupplierRentDetails
                                        where c.carSupplierRentID == bbID
                                        && c.rd_isMonthlyPayment == true
                                        select c).Max(cc => cc.rd_mNo);


                            var bb33 = (from c in db.CarSupplierRentDetails
                                        where c.carSupplierRentID == bbID
                                        && c.rd_isMonthlyPayment == true
                                        select c).Max(cc => cc.rd_fromDate);

                            for (int i = 0; i < int.Parse(txtNoOfmonths.Value.ToString()); i++)
                            {


                                // get unpaid rentLicencesDetails
                                CarSupplierRentDetail rld = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentID == bbID
                                                             && c.rd_mNo == i + 1
                                                             && c.rd_isMonthlyPayment == true
                                                             select c).FirstOrDefault();
                                if (rld != null && !rld.rd_isPaid.Value)
                                {
                                    rld.edited_Date = DateTime.Now;
                                    rld.editedBy = MainClass.userID;

                                    rld.rd_amount = float.Parse(txtcarSupplierRent_MonthlyAmount.Text);
                                    rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(i);
                                    rld.rd_mNo = i + 1;
                                    rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(i + 1).AddDays(-1);
                                    rld.carSupplierRentID = bb.carSupplierRentID;
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
                                        CarSupplierRentDetail rld = new CarSupplierRentDetail();
                                        rld.added_Date = DateTime.Now;
                                        rld.addedBy = MainClass.userID;
                                        rld.edited_Date = DateTime.Now;
                                        rld.editedBy = MainClass.userID;

                                        rld.rd_isPaid = false;
                                        rld.rd_amount = float.Parse(txtcarSupplierRent_MonthlyAmount.Text);
                                        rld.rd_fromDate = bb33.Value.AddMonths(i + 1);
                                        rld.rd_mNo = int.Parse(bb22.Value.ToString()) + i + 1;
                                        rld.rd_toDate = bb33.Value.AddMonths(i + 2).AddDays(-1);
                                        rld.carSupplierRentID = bb.carSupplierRentID;
                                        rld.rd_isFirstPayment = false;
                                        rld.rd_isLastPayment = false;
                                        rld.rd_isMonthlyPayment = true;

                                        db.CarSupplierRentDetails.AddObject(rld);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < noOFMonths; i++)
                                {
                                    CarSupplierRentDetail rld = new CarSupplierRentDetail();
                                    rld.added_Date = DateTime.Now;
                                    rld.addedBy = MainClass.userID;
                                    rld.edited_Date = DateTime.Now;
                                    rld.editedBy = MainClass.userID;

                                    rld.rd_isPaid = false;
                                    rld.rd_amount = float.Parse(txtcarSupplierRent_MonthlyAmount.Text);
                                    rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(i);
                                    rld.rd_mNo = i + 1;
                                    rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(i + 1).AddDays(-1);
                                    rld.carSupplierRentID = bb.carSupplierRentID;
                                    rld.rd_isFirstPayment = false;
                                    rld.rd_isLastPayment = false;
                                    rld.rd_isMonthlyPayment = true;

                                    db.CarSupplierRentDetails.AddObject(rld);
                                    db.SaveChanges();
                                }
                            }

                            if (float.Parse(txtcarSupplierRent_firstPaymentAmount.Text) > 0)
                            {

                                CarSupplierRentDetail rld = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentID == bbID
                                                             && c.rd_isFirstPayment == true
                                                             select c).FirstOrDefault();
                                if (rld != null)
                                {
                                    if (rld.rd_isPaid.Value && rld.rd_amount != float.Parse(txtcarSupplierRent_firstPaymentAmount.Text))
                                    {
                                        MessageBox.Show("لايمكنك تغيير مبلغ الدفعة الأولة لانه مدفوع");
                                        return;
                                    }
                                    else if (rld.rd_isPaid.Value && rld.rd_amount == float.Parse(txtcarSupplierRent_firstPaymentAmount.Text))
                                    {
                                    }
                                    else
                                    {
                                        rld.edited_Date = DateTime.Now;
                                        rld.editedBy = MainClass.userID;

                                        rld.rd_isPaid = false;
                                        rld.rd_amount = float.Parse(txtcarSupplierRent_firstPaymentAmount.Text);
                                        rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(-1);
                                        rld.rd_mNo = 0;
                                        rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddDays(-1);
                                        rld.carSupplierRentID = bb.carSupplierRentID;
                                        rld.rd_isFirstPayment = true;
                                        rld.rd_isLastPayment = false;
                                        rld.rd_isMonthlyPayment = false;
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    CarSupplierRentDetail rld1 = new CarSupplierRentDetail();
                                    rld1.added_Date = DateTime.Now;
                                    rld1.addedBy = MainClass.userID;
                                    rld1.edited_Date = DateTime.Now;
                                    rld1.editedBy = MainClass.userID;

                                    rld1.rd_isPaid = false;
                                    rld1.rd_amount = float.Parse(txtcarSupplierRent_firstPaymentAmount.Text);
                                    rld1.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(-1);
                                    rld1.rd_mNo = 0;
                                    rld1.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddDays(-1);
                                    rld1.carSupplierRentID = bb.carSupplierRentID;
                                    rld1.rd_isFirstPayment = true;
                                    rld1.rd_isLastPayment = false;
                                    rld1.rd_isMonthlyPayment = false;
                                    db.CarSupplierRentDetails.AddObject(rld1);
                                    db.SaveChanges();
                                }


                            }
                            else
                            {
                                // delete if any record overe there
                                CarSupplierRentDetail rld = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentID == bbID
                                                             && c.rd_isFirstPayment == true
                                                             select c).FirstOrDefault();
                                if (rld != null)
                                {
                                    if (rld.rd_isPaid.Value)
                                    {
                                        MessageBox.Show("لايمكنك تغيير مبلغ الدفعة الأولة لانه مدفوع");
                                        return;
                                    }
                                    else
                                    {
                                        db.CarSupplierRentDetails.DeleteObject(rld);
                                    }
                                }
                            }

                            if (float.Parse(txtcarSupplierRent_lastPaymentAmount.Text) > 0)
                            {
                                CarSupplierRentDetail rld = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentID == bbID
                                                             && c.rd_isLastPayment == true
                                                             select c).FirstOrDefault();
                                if (rld != null)
                                {
                                    if (rld.rd_isPaid.Value && rld.rd_amount != float.Parse(txtcarSupplierRent_lastPaymentAmount.Text))
                                    {
                                        MessageBox.Show("لايمكنك تغيير مبلغ الدفعة الأخيرة لانه مدفوع");
                                        return;
                                    }
                                    else if (rld.rd_isPaid.Value && rld.rd_amount == float.Parse(txtcarSupplierRent_lastPaymentAmount.Text))
                                    {
                                    }
                                    else
                                    {
                                        rld.edited_Date = DateTime.Now;
                                        rld.editedBy = MainClass.userID;

                                        rld.rd_isPaid = false;
                                        rld.rd_amount = float.Parse(txtcarSupplierRent_lastPaymentAmount.Text);
                                        rld.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(int.Parse(txtNoOfmonths.Value.ToString()));
                                        rld.rd_mNo = 1000;
                                        rld.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(int.Parse(txtNoOfmonths.Value.ToString()) + 1).AddDays(-1);
                                        rld.carSupplierRentID = bb.carSupplierRentID;
                                        rld.rd_isFirstPayment = false;
                                        rld.rd_isLastPayment = true;
                                        rld.rd_isMonthlyPayment = false;
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    CarSupplierRentDetail rld1 = new CarSupplierRentDetail();
                                    rld1.added_Date = DateTime.Now;
                                    rld1.addedBy = MainClass.userID;
                                    rld1.edited_Date = DateTime.Now;
                                    rld1.editedBy = MainClass.userID;

                                    rld1.rd_isPaid = false;
                                    rld1.rd_amount = float.Parse(txtcarSupplierRent_lastPaymentAmount.Text);
                                    rld1.rd_fromDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddMonths(-1);
                                    rld1.rd_mNo = 1000;
                                    rld1.rd_toDate = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).AddDays(-1);
                                    rld1.carSupplierRentID = bb.carSupplierRentID;
                                    rld1.rd_isFirstPayment = false;
                                    rld1.rd_isLastPayment = true;
                                    rld1.rd_isMonthlyPayment = false;
                                    db.CarSupplierRentDetails.AddObject(rld1);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                // delete if any record overe there
                                CarSupplierRentDetail rld = (from c in db.CarSupplierRentDetails
                                                             where c.carSupplierRentID == bbID
                                                             && c.rd_isLastPayment == true
                                                             select c).FirstOrDefault();
                                if (rld != null)
                                {
                                    if (rld.rd_isPaid.Value)
                                    {
                                        MessageBox.Show("لايمكنك تغيير مبلغ الدفعة الأخيرة لانه مدفوع");
                                        return;
                                    }
                                    else
                                    {
                                        db.CarSupplierRentDetails.DeleteObject(rld);
                                    }
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
                    position1 = lstCarBuy.Count - 1;
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
            frm.isCurrentbying = true;
            frm.ShowDialog();
            if (frm.found)
            {
                //carID = frm.carID;
                txtCarNo1.Text = frm.carNO;
                this.txtCarBrand.Text = frm.carName;
                //lblCarID.Text = frm.carID.ToString();

                CarSupplierRent cl = this.lstCarBuy.Find(c => c.carID == frm.carID);
                position1 = lstCarBuy.IndexOf(cl);
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
            if (position1 < lstCarBuy.Count - 1)
            {
                position1 += 1;
                bindData1();
            }
        }

        void btnLast_Click1(object sender, EventArgs e)
        {
            if (position1 < lstCarBuy.Count - 1)
            {
                position1 = lstCarBuy.Count - 1;
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
            txtcarSupplierRent_totalAmount.ReadOnly = true;

            txtCarNo1.ReadOnly = true;
            txtcarSupplierRent_MonthlyAmount.ReadOnly = true;
            this.txtrentCarNo.ReadOnly = true;
            //txtrentShopNo.Text = "";
            txtTo1.Enabled = false;
            txtcarSupplierRent_firstPaymentDate.Enabled = false;
            txtNoOfmonths.Enabled = false;
            btnFindCustomer1.Enabled = false;
            btnFindLicenses1.Enabled = false;
            txtcarSupplierRent_firstPaymentAmount.ReadOnly = true;
            txtcarSupplierRent_lastPaymentAmount.ReadOnly = true;


        }

        private void enableControls1()
        {
            mainbar1.disableButton();

           // txtDamanAmount1.ReadOnly = false;
            txtcarSupplierRent_MonthlyAmount.ReadOnly = false;
            txtrentCarNo.ReadOnly = false;
            //txtTo1.Enabled = true;
            txtNoOfmonths.Enabled = true;
            txtcarSupplierRent_firstPaymentDate.Enabled = true;
            txtcarSupplierRent_firstPaymentAmount.ReadOnly = false;
            txtcarSupplierRent_lastPaymentAmount.ReadOnly = false;
            txtcarSupplierRent_totalAmount.ReadOnly = false;
            btnFindCustomer1.Enabled = true;
            btnFindLicenses1.Enabled = true;

        }

        private void getData1()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCarBuy = (from c in db.CarSupplierRents orderby c.carSupplierRentID  select c).ToList();
            }
        }

        private void bindData1()
        {
            if (lstCarBuy.Count > 0)
            {
                supplierID = lstCarBuy[position1].carSuppliserID.Value;
                carID =  lstCarBuy[position1].carID.Value;
                txtcarSupplierRent_totalAmount.Text =  lstCarBuy[position1].carSupplierRent_totalAmount.Value.ToString();

                txtcarSupplierRent_firstPaymentDate.Text =  lstCarBuy[position1].carSupplierRent_firstPaymentDate.Value.ToShortDateString();
                txtcarSupplierRent_MonthlyAmount.Text = lstCarBuy[position1].carSupplierRent_MonthlyAmount.Value.ToString();
                this.txtrentCarNo.Text = lstCarBuy[position1].carRentNo;

                //txtrentShopNo.Text = "";
                txtTo1.Text = lstCarBuy[position1].carSupplierRent_to.Value.ToShortDateString();
                txtNoOfmonths.Value = lstCarBuy[position1].carSupplierRent_noOfMonths.Value;
                lblID1.Text = lstCarBuy[position1].carSupplierRentID.ToString();

                txtcarSupplierRent_firstPaymentAmount.Text = lstCarBuy[position1].carSupplierRent_firstPaymentAmount.Value.ToString();
                txtcarSupplierRent_lastPaymentAmount.Text = lstCarBuy[position1].carSupplierRent_lastPaymentAmount.Value.ToString();

                mainbar1.lblCount.Text = position1 + 1 + " / " + lstCarBuy.Count;

                bindCustomer1(supplierID);
                bindCar1(carID);

                           
            }
            else
            {
                supplierID = -1;
                carID = -1;

                txtCarName1.Text = "";
                txtCarBrand.Text = "";
                txtCustomerName1.Text = "";
                txtcarSupplierRent_totalAmount.Text = "0";
  
                txtcarSupplierRent_firstPaymentDate.Text = DateTime.Now.ToShortDateString();
                txtCarNo1.Text = "";
                txtcarSupplierRent_MonthlyAmount.Text = "0";
                txtrentCarNo.Text = "";
                //txtrentShopNo.Text = "";
                txtTo1.Text = DateTime.Now.ToShortDateString();
                txtNoOfmonths.Value = 0;
                lblID1.Text = "-1";
                txtcarSupplierRent_firstPaymentAmount.Text = "0";
                txtcarSupplierRent_lastPaymentAmount.Text = "0";
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
                var val2 = from c in db.CarSuplliers    
                           where c.carSuppliserID == cID
                           select c;
                CarSupllier cl2 = val2.FirstOrDefault();

                txtCustomerName1.Text = cl2.carSuppliser_Name;
            }
        }


        private void btnFindLicenses1_Click(object sender, EventArgs e)
        {
            CarsM.frmfindCars frm = new frmfindCars(false);
            //frm.isAllCar = true;
            if (change1 == "add")
            {
                frm.isCurrentBuyingCarNotInclude = true;
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
            frmfindCarsSupplier frm = new frmfindCarsSupplier();
            frm.ShowDialog();
            if (frm.found)
            {
                supplierID = frm.carSuppliserID;
                txtCustomerName1.Text = frm.carSuppliser_Name;
            }
        }

        private void txtRentalAmount1_TextChanged(object sender, EventArgs e)
        {
            if (change1 != "")
            {
                double ra = 0;
                if (double.TryParse(txtcarSupplierRent_MonthlyAmount.Text, out ra))
                {
                //    TimeSpan ts = DateTime.Parse(txtTo1.Text).Subtract(DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text));
                    
                //    int totalDays = ts.Days;
                //    if (ts.Days > 0)
                //    {
                //        double dayAmount =Math.Round((ra / 30) * ts.Days,2);
                //        //txtDamanAmount1.Text = dayAmount.ToString();
                //    }
                //    int noOfMonths = ((DateTime.Parse(txtTo1.Text).Year - DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).Year) * 12) + (DateTime.Parse(txtTo1.Text).Month - DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text).Month);
                //    txtcarSupplierRent_totalAmount.Text = (ra * noOfMonths).ToString();
                    getTotalAmount();
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
                    DateTime df = DateTime.Parse(txtcarSupplierRent_firstPaymentDate.Text);
                    txtTo1.Text = df.AddMonths(nn).AddDays(-1).ToShortDateString();

                    txtRentalAmount1_TextChanged(sender, e);
                    //getTotalAmount();
                }
            }
            getTotalAmount();
        }

        private void ChangeRentingAmount_Click(object sender, EventArgs e)
        {
            //if (lblID1.Text != "-1")
            //{
            //    frmChangeRentingAmountCar frm = new frmChangeRentingAmountCar();
            //    frm._CarCustomerRenteID = int.Parse(lblID1.Text);
            //    frm.ShowDialog();
            //}
        }

        private void txtcarSupplierRent_firstPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            if (change1 != "")
            {
                getTotalAmount();
            }
        }


        public double getTotalAmount()
        {
            double  total = 0;
            double ra = 0;
            if (double.TryParse(this.txtcarSupplierRent_MonthlyAmount.Text, out ra))
            {
                total += ra * double.Parse(txtNoOfmonths.Text);
            }

            if (double.TryParse(txtcarSupplierRent_firstPaymentAmount.Text, out ra))
            {
                total += ra;
            }

            if (double.TryParse(this.txtcarSupplierRent_lastPaymentAmount.Text, out ra))
            {
                total += ra;
            }

            txtcarSupplierRent_totalAmount.Text = total.ToString();
            return total;
        }

        private void txtcarSupplierRent_lastPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            getTotalAmount();
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

                    var val1 = (from c in db.CarSupplierRents
                                join cc in db.CarSupplierRentDetails on c.carSupplierRentID equals cc.carSupplierRentID
                                where c.carSupplierRentID == rentID1
                                select new
                                {
                                    cc.rd_amount

                                }).ToList();

                    foreach (var item in val1)
                    {
                        rAmount += item.rd_amount.Value;
                    }

                    var val2 = (from c in db.CarSupplierRentPayments
                                where c.carSupplierRentID == rentID1
                                select c).ToList();

                    foreach (var item in val2)
                    {
                        pAmount += item.carSupplierRentPayment_amount.Value;
                    }

                    if (rAmount - pAmount > 0)
                    {
                        MessageBox.Show("لا يمكنك انهاء الايجار , يجب دفع المبلغ المتبقي");
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("هل تريد انهاء الشراء ؟", "Confirmation Of Ending of renting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            CarSupplierRent bb = (from c in db.CarSupplierRents
                                                  where c.carSupplierRentID ==
                                                      rentID1
                                                  select c).FirstOrDefault();
                            bb.carSupplierRent_isactive = false;
                            int no = db.SaveChanges();
                            if (no > 0)
                            {
                                MessageBox.Show("تم انهاء الشراء بنجاح");
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
