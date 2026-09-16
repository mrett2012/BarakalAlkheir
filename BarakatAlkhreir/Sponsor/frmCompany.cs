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

namespace BarakalAlkheir.Sponsor
{
    public partial class frmCompany : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;

        string change2 = "";
        int position2 = 0;

        Company Company = null;
        List<Company> lstCompany = null;

        CompanyAuthorization companyAuthorization = null;
        List<CompanyAuthorization> lstcompanyAuthorization = null;

        components.banner mainbar = new components.banner("CompanyPage");
        components.banner mainbar2 = new components.banner("AuthorizationPage");

        public frmCompany()
        {
            InitializeComponent();
        }

        private void frmCompanys_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                CompanyPage.Controls.Add(mainbar);


                mainbar2.Dock = DockStyle.Top;
                AuthorizationPage.Controls.Add(mainbar2);
                
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


                /////////////////////////////////////////////////////////////////

                mainbar2.btnAdd.Click += new EventHandler(btnAdd2_Click);
                mainbar2.btnEdit.Click += new EventHandler(btnEdit2_Click);
                mainbar2.btnDelete.Click += new EventHandler(btnDelete2_Click);
                mainbar2.btnCancel.Click += new EventHandler(btnCancel2_Click);
                mainbar2.btnSave.Click += new EventHandler(btnSave2_Click);

                mainbar2.btnFind.Visible = false;

                mainbar2.btnFirst.Click += new EventHandler(btnFirst2_Click);
                mainbar2.btnPrevious.Click += new EventHandler(btnPrevious2_Click);
                mainbar2.btnNext.Click += new EventHandler(btnNext2_Click);
                mainbar2.btnLast.Click += new EventHandler(btnLast2_Click);
                mainbar2.btnExit.Click += new EventHandler(btnExit2_Click);

                PublicClass.bindAuthorization(cmbAuthorizationID);
                cmbAuthorizationID.SelectedIndex = 0;

                disableControls();
                disableControls2();

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

            txtcomp_arabicName.Text = "";
            txtcomp_civilId.Text = "";
            txtcomp_contactNo.Text = "";
            txtcomp_Designation.Text = "";
            txtcomp_englishName.Text = "";
            txtcomp_fileNo.Text = "";
            txtcomp_nam1.Text = "";
            txtcomp_nam2.Text = "";
            txtcomp_nam3.Text = "";
            txtcomp_nam4.Text = "";
            txtcomp_nam5.Text = "";
            txtcomp_name1.Text = "";
            txtcomp_name2.Text = "";
            txtcomp_name3.Text = "";
            txtcomp_name4.Text = "";
            txtcomp_name5.Text = "";
            txtcomp_nationality.Text = "";
            txtcomp_nationalityNo.Text = "";
            txtcomp_ownerName.Text = "";
            txtcomp_remarks.Text = "";
            txtcomp_Salary.Text = "";
            txtcomp_uniqueNo.Text = "";
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
            if (lblID.Text != "-1")
            {
                DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {

                        int bbID = int.Parse(lblID.Text);
                        Company bb = (from c in db.Companies
                                   where c.companyID ==
                                       bbID
                                   select c).FirstOrDefault();

                        db.Companies.DeleteObject(bb);
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

        void btnCancel_Click(object sender, EventArgs e)
        {
            change = "";
            disableControls();
            bindData();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            int rowsAffected = 0;
            if (change == "add")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    Company bb = new Company();

                    bb.comp_arabicName = txtcomp_arabicName.Text ;
                    bb.comp_civilId = txtcomp_civilId.Text;
                    bb.comp_contactNo = txtcomp_contactNo.Text;
                    bb.comp_Designation = txtcomp_Designation.Text;
                    bb.comp_englishName = txtcomp_englishName.Text;
                    bb.comp_fileNo = txtcomp_fileNo.Text;
                    bb.comp_nam1 = txtcomp_nam1.Text;
                    bb.comp_nam2 = txtcomp_nam2.Text;
                    bb.comp_nam3 = txtcomp_nam3.Text;
                    bb.comp_nam4 = txtcomp_nam4.Text;
                    bb.comp_nam5 = txtcomp_nam5.Text;
                    bb.comp_name1 = txtcomp_name1.Text;
                    bb.comp_name2 = txtcomp_name2.Text;
                    bb.comp_name3 = txtcomp_name3.Text;
                    bb.comp_name4 = txtcomp_name4.Text;
                    bb.comp_name5 = txtcomp_name5.Text;
                    bb.comp_nationality = txtcomp_nationality.Text;
                    bb.comp_nationalityNo = txtcomp_nationalityNo.Text;
                    bb.comp_ownerName = txtcomp_ownerName.Text;
                    bb.comp_remarks = txtcomp_remarks.Text;
                    bb.comp_Salary = txtcomp_Salary.Text;
                    bb.comp_uniqueNo = txtcomp_uniqueNo.Text;

                    bb.added_Date = DateTime.Now;
                    bb.addedBy = MainClass.userID;

                    bb.edited_Date = DateTime.Now;
                    bb.editedBy = MainClass.userID;

                    db.Companies.AddObject(bb);
                    rowsAffected = db.SaveChanges();
                }
            }
            if (change == "edit")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int bbID = int.Parse(lblID.Text);
                    Company bb = (from c in db.Companies where c.companyID == 
                                   bbID select c).FirstOrDefault();

                    bb.comp_arabicName = txtcomp_arabicName.Text;
                    bb.comp_civilId = txtcomp_civilId.Text;
                    bb.comp_contactNo = txtcomp_contactNo.Text;
                    bb.comp_Designation = txtcomp_Designation.Text;
                    bb.comp_englishName = txtcomp_englishName.Text;
                    bb.comp_fileNo = txtcomp_fileNo.Text;
                    bb.comp_nam1 = txtcomp_nam1.Text;
                    bb.comp_nam2 = txtcomp_nam2.Text;
                    bb.comp_nam3 = txtcomp_nam3.Text;
                    bb.comp_nam4 = txtcomp_nam4.Text;
                    bb.comp_nam5 = txtcomp_nam5.Text;
                    bb.comp_name1 = txtcomp_name1.Text;
                    bb.comp_name2 = txtcomp_name2.Text;
                    bb.comp_name3 = txtcomp_name3.Text;
                    bb.comp_name4 = txtcomp_name4.Text;
                    bb.comp_name5 = txtcomp_name5.Text;
                    bb.comp_nationality = txtcomp_nationality.Text;
                    bb.comp_nationalityNo = txtcomp_nationalityNo.Text;
                    bb.comp_ownerName = txtcomp_ownerName.Text;
                    bb.comp_remarks = txtcomp_remarks.Text;
                    bb.comp_Salary = txtcomp_Salary.Text;
                    bb.comp_uniqueNo = txtcomp_uniqueNo.Text;

                    bb.edited_Date = DateTime.Now;
                    bb.editedBy = MainClass.userID;
                    
                    //db.Companys.AddObject(bb);
                    rowsAffected = db.SaveChanges();
                }
            }


            if (rowsAffected > 0)
            {
                disableControls();

                getData();
                if (change == "add")
                {
                    position = lstCompany.Count - 1;
                }
                change = "";
                
                bindData();
            }
        }

        void btnFind_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            frmFindCompany frm = new frmFindCompany();
            frm.ShowDialog();
            if (frm.found)
            {
                Company cl = this.lstCompany.Find(c => c.companyID == frm.cID);

                position = lstCompany.IndexOf(cl);
                bindData();
            }
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
            if (position <lstCompany.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstCompany.Count -1)
            {
                position = lstCompany.Count - 1;
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

            txtcomp_arabicName.ReadOnly = true;
            txtcomp_civilId.ReadOnly = true;
            txtcomp_contactNo.ReadOnly = true;
            txtcomp_Designation.ReadOnly = true;
            txtcomp_englishName.ReadOnly = true;
            txtcomp_fileNo.ReadOnly = true;
            txtcomp_nam1.ReadOnly = true;
            txtcomp_nam2.ReadOnly = true;
            txtcomp_nam3.ReadOnly = true;
            txtcomp_nam4.ReadOnly = true;
            txtcomp_nam5.ReadOnly = true;
            txtcomp_name1.ReadOnly = true;
            txtcomp_name2.ReadOnly = true;
            txtcomp_name3.ReadOnly = true;
            txtcomp_name4.ReadOnly = true;
            txtcomp_name5.ReadOnly = true;
            txtcomp_nationality.ReadOnly = true;
            txtcomp_nationalityNo.ReadOnly = true;
            txtcomp_ownerName.ReadOnly = true;
            txtcomp_remarks.ReadOnly = true;
            txtcomp_Salary.ReadOnly = true;
            txtcomp_uniqueNo.ReadOnly = true;


          
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtcomp_arabicName.ReadOnly = false;
            txtcomp_civilId.ReadOnly = false;
            txtcomp_contactNo.ReadOnly = false;
            txtcomp_Designation.ReadOnly = false;
            txtcomp_englishName.ReadOnly = false;
            txtcomp_fileNo.ReadOnly = false;
            txtcomp_nam1.ReadOnly = false;
            txtcomp_nam2.ReadOnly = false;
            txtcomp_nam3.ReadOnly = false;
            txtcomp_nam4.ReadOnly = false;
            txtcomp_nam5.ReadOnly = false;
            txtcomp_name1.ReadOnly = false;
            txtcomp_name2.ReadOnly = false;
            txtcomp_name3.ReadOnly = false;
            txtcomp_name4.ReadOnly = false;
            txtcomp_name5.ReadOnly = false;
            txtcomp_nationality.ReadOnly = false;
            txtcomp_nationalityNo.ReadOnly = false;
            txtcomp_ownerName.ReadOnly = false;
            txtcomp_remarks.ReadOnly = false;
            txtcomp_Salary.ReadOnly = false;
            txtcomp_uniqueNo.ReadOnly = false;

           
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCompany = (from c in db.Companies select c).ToList();
            }
        }

        private void bindData()
        {
            if (lstCompany.Count > 0)
            {

                txtcomp_arabicName.Text = lstCompany[position].comp_arabicName;
                txtcomp_civilId.Text = lstCompany[position].comp_civilId;
                txtcomp_contactNo.Text = lstCompany[position].comp_contactNo;
                txtcomp_Designation.Text = lstCompany[position].comp_Designation;
                txtcomp_englishName.Text = lstCompany[position].comp_englishName;
                txtcomp_fileNo.Text = lstCompany[position].comp_fileNo;
                txtcomp_nam1.Text = lstCompany[position].comp_nam1;
                txtcomp_nam2.Text = lstCompany[position].comp_nam2;
                txtcomp_nam3.Text = lstCompany[position].comp_nam3;
                txtcomp_nam4.Text = lstCompany[position].comp_nam4;
                txtcomp_nam5.Text = lstCompany[position].comp_nam5;
                txtcomp_name1.Text = lstCompany[position].comp_name1;
                txtcomp_name2.Text = lstCompany[position].comp_name2;
                txtcomp_name3.Text = lstCompany[position].comp_name3;
                txtcomp_name4.Text = lstCompany[position].comp_name4;
                txtcomp_name5.Text = lstCompany[position].comp_name5;
                txtcomp_nationality.Text = lstCompany[position].comp_nationality;
                txtcomp_nationalityNo.Text = lstCompany[position].comp_nationalityNo;
                txtcomp_ownerName.Text = lstCompany[position].comp_ownerName;
                txtcomp_remarks.Text = lstCompany[position].comp_remarks;
                txtcomp_Salary.Text = lstCompany[position].comp_Salary;
                txtcomp_uniqueNo.Text = lstCompany[position].comp_uniqueNo;

                lblID.Text = lstCompany[position].companyID.ToString();

                radPanel1.Text = "شركة :  "+lstCompany[position].comp_arabicName;

                mainbar.lblCount.Text = position + 1 + " / " + lstCompany.Count;
            }
            else
            {
                radPanel1.Text = "الشركات";
                txtcomp_arabicName.Text = "";
                txtcomp_civilId.Text = "";
                txtcomp_contactNo.Text = "";
                txtcomp_Designation.Text = "";
                txtcomp_englishName.Text = "";
                txtcomp_fileNo.Text = "";
                txtcomp_nam1.Text = "";
                txtcomp_nam2.Text = "";
                txtcomp_nam3.Text = "";
                txtcomp_nam4.Text = "";
                txtcomp_nam5.Text = "";
                txtcomp_name1.Text = "";
                txtcomp_name2.Text = "";
                txtcomp_name3.Text = "";
                txtcomp_name4.Text = "";
                txtcomp_name5.Text = "";
                txtcomp_nationality.Text = "";
                txtcomp_nationalityNo.Text = "";
                txtcomp_ownerName.Text = "";
                txtcomp_remarks.Text = "";
                txtcomp_Salary.Text = "";
                txtcomp_uniqueNo.Text = "";

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }

            getData2();
            bindData2();
        }



        //////////////////////////////////////////////////

        void btnAdd2_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                change2 = "add";
                enableControls2();
                cmbAuthorizationID.SelectedIndex = 0;
                chkemp_active.Checked = true;
            }
        }

        void btnEdit2_Click(object sender, EventArgs e)
        {
            if (lblID2.Text != "-1")
            {
                change2 = "edit";
                enableControls2();
            }
        }

        void btnDelete2_Click(object sender, EventArgs e)
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
                        CompanyAuthorization bb = (from c in db.CompanyAuthorizations
                                      where c.companyAuthorizationID ==
                                          bbID
                                      select c).FirstOrDefault();

                        db.CompanyAuthorizations.DeleteObject(bb);
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

        void btnCancel2_Click(object sender, EventArgs e)
        {
            change2 = "";
            disableControls2();
            bindData2();
        }

        void btnSave2_Click(object sender, EventArgs e)
        {
            int rowsAffected = 0;
            if (change2 == "add")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        CompanyAuthorization bb = new CompanyAuthorization();

                        bb.companyID = int.Parse(lblID.Text);
                        bb.authorizationID = int.Parse(cmbAuthorizationID.SelectedValue.ToString());
                        bb.auth_isCurrent = chkemp_active.Checked;
                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.CompanyAuthorizations.AddObject(bb);
                        rowsAffected = db.SaveChanges();

                        int bbID = int.Parse(lblID.Text);
                        if (rowsAffected > 0 && this.chkemp_active.Checked)
                        {
                            var val = from c in db.CompanyAuthorizations
                                      where c.authorizationID != bb.authorizationID
                                      && c.companyID == bbID
                                      select c;

                            foreach (var item in val)
                            {
                                item.auth_isCurrent = false;
                                db.SaveChanges();
                            }

                        }

                        ts.Complete();
                        db.AcceptAllChanges();
                    }
                }
            }
            if (change2 == "edit")
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        int bbID = int.Parse(lblID2.Text);
                        CompanyAuthorization bb = (from c in db.CompanyAuthorizations
                                                   where c.companyAuthorizationID ==
                                                       bbID
                                                   select c).FirstOrDefault();

                        bb.companyID = int.Parse(lblID.Text);
                        bb.authorizationID = int.Parse(cmbAuthorizationID.SelectedValue.ToString());
                        bb.auth_isCurrent = chkemp_active.Checked;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        rowsAffected = db.SaveChanges();

                        int bbID1 = int.Parse(lblID.Text);
                        if (rowsAffected > 0 && this.chkemp_active.Checked)
                        {
                            var val = from c in db.CompanyAuthorizations
                                      where c.authorizationID != bb.authorizationID
                                      && c.companyID == bbID1
                                      select c;

                            foreach (var item in val)
                            {
                                item.auth_isCurrent = false;
                                db.SaveChanges();
                            }

                        }

                        ts.Complete();
                        db.AcceptAllChanges();
                    }
                }
            }


            if (rowsAffected > 0)
            {
                disableControls2();

                getData2();
                if (change2 == "add")
                {
                    position2 = lstcompanyAuthorization.Count - 1;
                }
                change2 = "";

                bindData2();
            }
        }

        void btnFirst2_Click(object sender, EventArgs e)
        {
            if (position2 > 0)
            {
                position2 = 0;
                bindData2();
            }
        }

        void btnPrevious2_Click(object sender, EventArgs e)
        {
            if (position2 > 0)
            {
                position2 -= 1;
                bindData2();
            }
        }

        void btnNext2_Click(object sender, EventArgs e)
        {
            if (position2 < lstcompanyAuthorization.Count - 1)
            {
                position2 += 1;
                bindData2();
            }
        }

        void btnLast2_Click(object sender, EventArgs e)
        {
            if (position2 < lstcompanyAuthorization.Count - 1)
            {
                position2 = lstcompanyAuthorization.Count - 1;
                bindData2();
            }
        }

        void btnExit2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls2()
        {
            mainbar2.enableButtons();
            cmbAuthorizationID.Enabled = false;
            chkemp_active.Enabled = false;

            GView.Enabled = true;
        }

        private void enableControls2()
        {
            mainbar2.disableButton();

            cmbAuthorizationID.Enabled = true;
            chkemp_active.Enabled = true;

            GView.Enabled = false;
        }

        private void getData2()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int bbID = int.Parse(lblID.Text);
                lstcompanyAuthorization = (from c in db.CompanyAuthorizations.Include("AuthorizationPerson")
                              where c.companyID == bbID
                              select c).ToList();
               
                GView.DataSource = lstcompanyAuthorization;
            }
        }

        private void bindData2()
        {
            if (this.lstcompanyAuthorization.Count > 0)
            {

                for (int i = 0; i < cmbAuthorizationID.Items.Count; i++)
                {
                    if (cmbAuthorizationID.Items[i].Value.ToString() == lstcompanyAuthorization[position2].authorizationID.ToString())
                    {
                        cmbAuthorizationID.SelectedIndex = i;
                        break;
                    }
                }

                lblID2.Text = lstcompanyAuthorization[position2].companyAuthorizationID.ToString();

                
                mainbar2.lblCount.Text = position2 + 1 + " / " + lstcompanyAuthorization.Count;

                GView.Rows[position2].IsSelected = true;
            }
            else
            {
                cmbAuthorizationID.SelectedIndex = 0;

                lblID2.Text = "-1";

                mainbar2.lblCount.Text = " 0 / 0";

            }
        }



    }
}
