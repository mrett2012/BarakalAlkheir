using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Model;
using System.Transactions;

namespace BarakalAlkheir.Sponsor
{
    public partial class frmCompanyAuthorization : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        AuthorizationPerson AuthorizationPerson = null;
        List<AuthorizationPerson> lstAuthorizationPerson = null;

        components.banner mainbar = new components.banner("frmAuthorizationPerson");


        public frmCompanyAuthorization()
        {
            InitializeComponent();
        }

        private void frmCompanyAuthorizationAuthorization_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmCompanyAuthorizationAuthorization_LoadData);
        }

        private async System.Threading.Tasks.Task frmCompanyAuthorizationAuthorization_LoadData(object sender, EventArgs e)
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

                await getDataAsync();
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
            enableControls();

            txtfauth_ullArabicName.Text = "";
            txtauth_CivilID.Text = "";
            txtauth_ContactNo.Text = "";
            txtauth_Designation.Text = "";
            txtfauth_ullEnglishName.Text = "";
            txtauth_namea1.Text = "";
            txtauth_namea2.Text = "";
            txtauth_namea3.Text = "";
            txtauth_namea4.Text = "";
            txtauth_namea5.Text = "";
            txtauth_namee1.Text = "";
            txtauth_namee2.Text = "";
            txtauth_namee3.Text = "";
            txtauth_namee4.Text = "";
            txtauth_namee5.Text = "";
            txtauth_remarks.Text = "";

            chkisActive.Checked = true;
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

                            int bbID = int.Parse(lblID.Text);
                            AuthorizationPerson bb = (from c in db.AuthorizationPersons
                                                      where c.authorizationID ==
                                                          bbID
                                                      select c).FirstOrDefault();

                            db.AuthorizationPersons.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                position = 0;
                                getData();
                                bindData();
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
                if (change == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            AuthorizationPerson bb = new AuthorizationPerson();

                            bb.fauth_ullArabicName = txtfauth_ullArabicName.Text;
                            bb.auth_CivilID = txtauth_CivilID.Text;
                            bb.auth_ContactNo = txtauth_ContactNo.Text;
                            bb.auth_Designation = txtauth_Designation.Text;
                            bb.fauth_ullEnglishName = txtfauth_ullEnglishName.Text;
                            bb.auth_namea1 = txtauth_namea1.Text;
                            bb.auth_namea2 = txtauth_namea2.Text;
                            bb.auth_namea3 = txtauth_namea3.Text;
                            bb.auth_namea4 = txtauth_namea4.Text;
                            bb.auth_namea5 = txtauth_namea5.Text;
                            bb.auth_namee1 = txtauth_namee1.Text;
                            bb.auth_namee2 = txtauth_namee2.Text;
                            bb.auth_namee3 = txtauth_namee3.Text;
                            bb.auth_namee4 = txtauth_namee4.Text;
                            bb.auth_namee5 = txtauth_namee5.Text;
                            bb.auth_remarks = txtauth_remarks.Text;
                            bb.auth_isCurrent = chkisActive.Checked;


                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.AuthorizationPersons.AddObject(bb);
                            rowsAffected = db.SaveChanges();

                            //if (rowsAffected > 0 && chkisActive.Checked)
                            //{
                            //    var val = from c in db.AuthorizationPersons
                            //              where c.AuthorizationPersonID != bb.AuthorizationPersonID
                            //              select c;

                            //    foreach (var item in val)
                            //    {
                            //        item.auth_isCurrent = false;
                            //        db.SaveChanges();
                            //    }

                            //}

                            ts.Complete();
                            db.AcceptAllChanges();

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
                            AuthorizationPerson bb = (from c in db.AuthorizationPersons
                                                      where c.authorizationID ==
                                                          bbID
                                                      select c).FirstOrDefault();

                            bb.fauth_ullArabicName = txtfauth_ullArabicName.Text;
                            bb.auth_CivilID = txtauth_CivilID.Text;
                            bb.auth_ContactNo = txtauth_ContactNo.Text;
                            bb.auth_Designation = txtauth_Designation.Text;
                            bb.fauth_ullEnglishName = txtfauth_ullEnglishName.Text;
                            bb.auth_namea1 = txtauth_namea1.Text;
                            bb.auth_namea2 = txtauth_namea2.Text;
                            bb.auth_namea3 = txtauth_namea3.Text;
                            bb.auth_namea4 = txtauth_namea4.Text;
                            bb.auth_namea5 = txtauth_namea5.Text;
                            bb.auth_namee1 = txtauth_namee1.Text;
                            bb.auth_namee2 = txtauth_namee2.Text;
                            bb.auth_namee3 = txtauth_namee3.Text;
                            bb.auth_namee4 = txtauth_namee4.Text;
                            bb.auth_namee5 = txtauth_namee5.Text;
                            bb.auth_remarks = txtauth_remarks.Text;
                            bb.auth_isCurrent = chkisActive.Checked;


                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            rowsAffected = db.SaveChanges();

                            //if (rowsAffected > 0 && chkisActive.Checked)
                            //{
                            //    var val = from c in db.AuthorizationPersons
                            //              where c.AuthorizationPersonID != bb.AuthorizationPersonID
                            //              select c;

                            //    foreach (var item in val)
                            //    {
                            //        item.auth_isCurrent = false;
                            //        db.SaveChanges();
                            //    }

                            //}


                            ts.Complete();
                            db.AcceptAllChanges();
                        }
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstAuthorizationPerson.Count - 1;
                        GView.Rows[position].IsSelected = true;
                    }
                    change = "";

                    bindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnFind_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void btnFirst_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position = 0;
                bindData();
            }
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position -= 1;
                bindData();
            }
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            if (position < lstAuthorizationPerson.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstAuthorizationPerson.Count - 1)
            {
                position = lstAuthorizationPerson.Count - 1;
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

            txtfauth_ullArabicName.ReadOnly = true;
            txtauth_CivilID.ReadOnly = true;
            txtauth_ContactNo.ReadOnly = true;
            txtauth_Designation.ReadOnly = true;
            txtfauth_ullEnglishName.ReadOnly = true;

            txtauth_namea1.ReadOnly = true;
            txtauth_namea2.ReadOnly = true;
            txtauth_namea3.ReadOnly = true;
            txtauth_namea4.ReadOnly = true;
            txtauth_namea5.ReadOnly = true;
            txtauth_namee1.ReadOnly = true;
            txtauth_namee2.ReadOnly = true;
            txtauth_namee3.ReadOnly = true;
            txtauth_namee4.ReadOnly = true;
            txtauth_namee5.ReadOnly = true;

            txtauth_remarks.ReadOnly = true;

            //cmbcompanyID.Enabled = false;
            chkisActive.Enabled = false;


        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtfauth_ullArabicName.ReadOnly = false;
            txtauth_CivilID.ReadOnly = false;
            txtauth_ContactNo.ReadOnly = false;
            txtauth_Designation.ReadOnly = false;
            txtfauth_ullEnglishName.ReadOnly = false;

            txtauth_namea1.ReadOnly = false;
            txtauth_namea2.ReadOnly = false;
            txtauth_namea3.ReadOnly = false;
            txtauth_namea4.ReadOnly = false;
            txtauth_namea5.ReadOnly = false;
            txtauth_namee1.ReadOnly = false;
            txtauth_namee2.ReadOnly = false;
            txtauth_namee3.ReadOnly = false;
            txtauth_namee4.ReadOnly = false;
            txtauth_namee5.ReadOnly = false;

            txtauth_remarks.ReadOnly = false;

            //cmbcompanyID.Enabled = true;
            chkisActive.Enabled = true;


        }


        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.AuthorizationPersons  select c).ToList();
                }
            }, rows =>
            {
                lstAuthorizationPerson = rows;
                GView.DataSource = lstAuthorizationPerson;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstAuthorizationPerson = (from c in db.AuthorizationPersons  select c).ToList();
                GView.DataSource = lstAuthorizationPerson;
            }
        }

        private void bindData()
        {
            if (lstAuthorizationPerson.Count > 0)
            {

                txtfauth_ullArabicName.Text = lstAuthorizationPerson[position].fauth_ullArabicName;
                txtauth_CivilID.Text = lstAuthorizationPerson[position].auth_CivilID;
                txtauth_ContactNo.Text = lstAuthorizationPerson[position].auth_ContactNo;
                txtauth_Designation.Text = lstAuthorizationPerson[position].auth_Designation;
                txtfauth_ullEnglishName.Text = lstAuthorizationPerson[position].fauth_ullEnglishName;
                txtauth_namea1.Text = lstAuthorizationPerson[position].auth_namea1;
                txtauth_namea2.Text = lstAuthorizationPerson[position].auth_namea2;
                txtauth_namea3.Text = lstAuthorizationPerson[position].auth_namea3;
                txtauth_namea4.Text = lstAuthorizationPerson[position].auth_namea4;
                txtauth_namea5.Text = lstAuthorizationPerson[position].auth_namea5;
                txtauth_namee1.Text = lstAuthorizationPerson[position].auth_namee1;
                txtauth_namee2.Text = lstAuthorizationPerson[position].auth_namee2;
                txtauth_namee3.Text = lstAuthorizationPerson[position].auth_namee3;
                txtauth_namee4.Text = lstAuthorizationPerson[position].auth_namee4;
                txtauth_namee5.Text = lstAuthorizationPerson[position].auth_namee5;

                txtauth_remarks.Text = lstAuthorizationPerson[position].auth_remarks;

                chkisActive.Checked = lstAuthorizationPerson[position].auth_isCurrent.Value;
                lblID.Text = lstAuthorizationPerson[position].authorizationID.ToString();

                //for (int i = 0; i < cmbcompanyID.Items.Count; i++)
                //{
                //    if (cmbcompanyID.Items[i].Value.ToString() == lstAuthorizationPerson[position].companyID.Value.ToString())
                //    {
                //        cmbcompanyID.SelectedIndex =i;
                //        break;
                //    }
                //}

                mainbar.lblCount.Text = position + 1 + " / " + lstAuthorizationPerson.Count;
            }
            else
            {
                txtfauth_ullArabicName.Text = "";
                txtauth_CivilID.Text = "";
                txtauth_ContactNo.Text = "";
                txtauth_Designation.Text = "";
                txtfauth_ullEnglishName.Text = "";

                txtauth_namea1.Text = "";
                txtauth_namea2.Text = "";
                txtauth_namea3.Text = "";
                txtauth_namea4.Text = "";
                txtauth_namea5.Text = "";
                txtauth_namee1.Text = "";
                txtauth_namee2.Text = "";
                txtauth_namee3.Text = "";
                txtauth_namee4.Text = "";
                txtauth_namee5.Text = "";
                txtauth_remarks.Text = "";

                //cmbcompanyID.SelectedIndex = 0;
                
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                AuthorizationPerson cl = this.lstAuthorizationPerson.Find(b => b.authorizationID == bID);

                position = lstAuthorizationPerson.IndexOf(cl);
                bindData();
            }
        }

    }
}
