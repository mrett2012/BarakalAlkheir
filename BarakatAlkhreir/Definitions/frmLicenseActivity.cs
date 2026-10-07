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

namespace BarakalAlkheir.Definitions
{
    public partial class frmLicenseActivity : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        
        LicenseActivity licenseActivity = null;
        List<LicenseActivity> lstLicenseActivity = null;

        components.banner mainbar = new components.banner("frmLicenseActivity");

        public frmLicenseActivity()
        {
            InitializeComponent();
        }

        private void frmLicenseActivities_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmLicenseActivities_LoadData);
        }

        private async System.Threading.Tasks.Task frmLicenseActivities_LoadData(object sender, EventArgs e)
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

                mainbar.btnFind.Visible = false;

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
            //txtLicenseActivity_Code.Text = "";
            txtLicenseActivity_namea.Text = "";
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
                            LicenseActivity bb = (from c in db.LicenseActivities
                                         where c.licenseActivityID ==
                                             bbID
                                         select c).FirstOrDefault();

                            db.LicenseActivities.DeleteObject(bb);
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
                        LicenseActivity bb = new LicenseActivity();

                        //bb.LicenseActivity_Code = txtLicenseActivity_Code.Text;
                        bb.Activity_namea = txtLicenseActivity_namea.Text;
                        bb.Activity_namee = "";

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.LicenseActivities.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        LicenseActivity bb = (from c in db.LicenseActivities
                                     where c.licenseActivityID ==
                                         bbID
                                     select c).FirstOrDefault();


                        bb.Activity_namea = txtLicenseActivity_namea.Text;
                        bb.Activity_namee = "";

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.LicenseActivities.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstLicenseActivity.Count - 1;
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
            if (position <lstLicenseActivity.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstLicenseActivity.Count -1)
            {
                position = lstLicenseActivity.Count - 1;
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
            txtLicenseActivity_namea.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtLicenseActivity_namea.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.LicenseActivities  select c).ToList();
                }
            }, rows =>
            {
                lstLicenseActivity = rows;
                GView.DataSource = lstLicenseActivity;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstLicenseActivity = (from c in db.LicenseActivities  select c).ToList();
                GView.DataSource = lstLicenseActivity;
            }
        }

        private void bindData()
        {
            if (lstLicenseActivity.Count > 0)
            {
                txtLicenseActivity_namea.Text = lstLicenseActivity[position].Activity_namea;
                lblID.Text = lstLicenseActivity[position].licenseActivityID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstLicenseActivity.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtLicenseActivity_namea.Text = "";
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
        }



        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                licenseActivity = lstLicenseActivity.Find(b => b.licenseActivityID == bID);

                position = lstLicenseActivity.IndexOf(licenseActivity);
                bindData();
            }
        }



    }
}
