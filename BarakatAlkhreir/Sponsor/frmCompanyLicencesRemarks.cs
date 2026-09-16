using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Model;
using System.Transactions;

namespace BarakalAlkheir.Sponsor
{
    public partial class frmCompanyLicencesRemarks : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;

        List<CompanyLicencesRemark> lstCompanyLicencesRemark = null;
        CompanyLicencesRemark companyLicenceRemark = null;
        components.banner mainbar = new components.banner("frmCompanyLicencesRemarks");

        public int _comLicenseRemID = -1;

        public frmCompanyLicencesRemarks()
        {
            InitializeComponent();
        }


        private void frmCompanyLicences_Load(object sender, EventArgs e)
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
                mainbar.btnFind.Click += new EventHandler(btnFind_Click);
                mainbar.btnFirst.Click += new EventHandler(btnFirst_Click);
                mainbar.btnPrevious.Click += new EventHandler(btnPrevious_Click);
                mainbar.btnNext.Click += new EventHandler(btnNext_Click);
                mainbar.btnLast.Click += new EventHandler(btnLast_Click);
                mainbar.btnExit.Click += new EventHandler(btnExit_Click);

                disableControls();
                getData();

                if (_comLicenseRemID != -1)
                {
                    position = this.lstCompanyLicencesRemark.FindIndex(cc => cc.comLicenseRemID == _comLicenseRemID);
                }
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

            txtcomLicenseRem_Action.Text = "";
            txtcomLicenseRem_Date.Text = "";
            txtcomLicenseRem_Remarks.Text = "";
            chkcomLicenseRem_isActive.Checked = false;

            txtcommercialName.Text = "";
            txtlicenseNo.Text = "";
            txtlicenseUniqueNo.Text = "";
            lblcompanyLicencesID.Text = "-1";
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
                            int ID = int.Parse(lblID.Text);
                            CompanyLicencesRemark cc = (from c in db.CompanyLicencesRemarks
                                                        where c.comLicenseRemID == ID
                                                        select c).FirstOrDefault();
                            db.DeleteObject(cc);
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
                        int cID = int.Parse(lblcompanyLicencesID.Text);

                        CompanyLicencesRemark bb = new CompanyLicencesRemark();
                        bb.companyLicencesID = cID;
                        bb.comLicenseRem_Action = txtcomLicenseRem_Action.Text;
                        bb.comLicenseRem_isActive = !chkcomLicenseRem_isActive.Checked;
                        bb.comLicenseRem_Remarks = txtcomLicenseRem_Remarks.Text;

                        DateTime ddd = DateTime.Now;
                        if (DateTime.TryParse(txtcomLicenseRem_Date.Text, out ddd))
                        {
                            bb.comLicenseRem_Date = ddd;
                        }


                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.CompanyLicencesRemarks.AddObject(bb);
                        rowsAffected = db.SaveChanges();


                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {

                        int bbID = int.Parse(lblID.Text);

                        CompanyLicencesRemark bb = (from c in db.CompanyLicencesRemarks
                                                    where c.comLicenseRemID == bbID
                                                    select c).FirstOrDefault();

                        int cID = int.Parse(lblcompanyLicencesID.Text);

                        bb.companyLicencesID = cID;
                        bb.comLicenseRem_Action = txtcomLicenseRem_Action.Text;
                        bb.comLicenseRem_isActive = !chkcomLicenseRem_isActive.Checked;
                        bb.comLicenseRem_Remarks = txtcomLicenseRem_Remarks.Text;

                        DateTime ddd = DateTime.Now;
                        if (DateTime.TryParse(txtcomLicenseRem_Date.Text, out ddd))
                        {
                            bb.comLicenseRem_Date = ddd;
                        }

                        rowsAffected = db.SaveChanges();



                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = this.lstCompanyLicencesRemark.Count - 1;
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
            try
            {
                frmFindLicencesRemarks frm = new frmFindLicencesRemarks();
                frm.ShowDialog();
                if (frm.found)
                {
                   position =  lstCompanyLicencesRemark.FindIndex(cc => cc.comLicenseRemID == frm.ID);
                }

                bindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (position < lstCompanyLicencesRemark.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstCompanyLicencesRemark.Count - 1)
            {
                position = lstCompanyLicencesRemark.Count - 1;
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

            btnFindLicenses.Enabled = false;
            txtcomLicenseRem_Action.Enabled = false;
            txtcomLicenseRem_Date.Enabled = false;
            txtcomLicenseRem_Remarks.Enabled = false;
            chkcomLicenseRem_isActive.Enabled = false;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            btnFindLicenses.Enabled = true;
            txtcomLicenseRem_Action.Enabled = true;
            txtcomLicenseRem_Date.Enabled = true;
            txtcomLicenseRem_Remarks.Enabled = true;
            chkcomLicenseRem_isActive.Enabled = true;

        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCompanyLicencesRemark = (from c in db.CompanyLicencesRemarks  select c).ToList();
            }
        }

        private void bindData()
        {
            if (lstCompanyLicencesRemark.Count > 0)
            {

                txtcomLicenseRem_Action.Text = lstCompanyLicencesRemark[position].comLicenseRem_Action;
                if (lstCompanyLicencesRemark[position].comLicenseRem_Date.HasValue)
                {
                    txtcomLicenseRem_Date.Text = lstCompanyLicencesRemark[position].comLicenseRem_Date.Value.ToShortDateString();
                }
                else
                {
                    txtcomLicenseRem_Date.Text = "";
                }

                txtcomLicenseRem_Remarks.Text = lstCompanyLicencesRemark[position].comLicenseRem_Remarks;
                chkcomLicenseRem_isActive.Checked = !lstCompanyLicencesRemark[position].comLicenseRem_isActive.Value;

                lblcompanyLicencesID.Text = lstCompanyLicencesRemark[position].companyLicencesID.ToString();
                lblID.Text = lstCompanyLicencesRemark[position].comLicenseRemID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstCompanyLicencesRemark.Count;

                int IDD = lstCompanyLicencesRemark[position].companyLicencesID.Value;
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var cc = (from c in db.CompanyLicences
                              where c.companyLicencesID == IDD
                              select c).ToList();
                    if (cc.Count > 0)
                    {
                        txtcommercialName.Text = cc[0].commercialName;
                        txtlicenseNo.Text = cc[0].licenseNo;
                        txtlicenseUniqueNo.Text = cc[0].licenseUniqueNo;
                    }
                }
            }
            else
            {

                txtcomLicenseRem_Action.Text = "";
                txtcomLicenseRem_Date.Text = "";
                txtcomLicenseRem_Remarks.Text = "";
                chkcomLicenseRem_isActive.Checked = false;

                txtcommercialName.Text = "" ;
                txtlicenseNo.Text = "";
                txtlicenseUniqueNo.Text = "";
                lblcompanyLicencesID.Text = "-1";
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }
        }





        private void btnFindLicenses_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, false);
            frm.isMainLicencesInclude = false;
            frm.ShowDialog();
            if (frm.found)
            {
                lblcompanyLicencesID.Text = frm.licID.ToString();

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var cc = (from c in db.CompanyLicences
                             where c.companyLicencesID == frm.licID
                             select c).ToList();
                    if (cc.Count > 0)
                    {
                        txtcommercialName.Text = cc[0].commercialName;
                        txtlicenseNo.Text = cc[0].licenseNo;
                        txtlicenseUniqueNo.Text = cc[0].licenseUniqueNo;
                    }
                }
            }

        }




    }
}
