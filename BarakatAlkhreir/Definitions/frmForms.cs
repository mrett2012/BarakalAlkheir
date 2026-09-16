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
    public partial class frmForms : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        FormScrean FormScrean = null;
        List<FormScrean> lstFormScrean = null;

        components.banner mainbar = new components.banner("frmForms");

        public frmForms()
        {
            InitializeComponent();
        }

        private void frmFormScreans_Load(object sender, EventArgs e)
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
            txtform_IDName.Text = "";
            txtform_DisplayName.Text = "";
            txtform_Descriptions.Text = "";
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
                            FormScrean bb = (from c in db.FormScreans
                                             where c.formID ==
                                                 bbID
                                             select c).FirstOrDefault();

                            db.FormScreans.DeleteObject(bb);
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
                        FormScrean bb = new FormScrean();

                        bb.form_IDName = txtform_IDName.Text;
                        bb.form_DisplayName = txtform_DisplayName.Text;
                        bb.form_Descriptions = txtform_Descriptions.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.FormScreans.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        FormScrean bb = (from c in db.FormScreans
                                         where c.formID ==
                                             bbID
                                         select c).FirstOrDefault();

                        bb.form_IDName = txtform_IDName.Text;
                        bb.form_DisplayName = txtform_DisplayName.Text;
                        bb.form_Descriptions = txtform_Descriptions.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.FormScreans.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstFormScrean.Count - 1;
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
            if (position <lstFormScrean.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstFormScrean.Count -1)
            {
                position = lstFormScrean.Count - 1;
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
            txtform_Descriptions.ReadOnly = true;
            txtform_DisplayName.ReadOnly = true;
            txtform_IDName.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtform_Descriptions.ReadOnly = false;
            txtform_DisplayName.ReadOnly = false;
            txtform_IDName.ReadOnly = false;
            GView.ReadOnly = true;
            GView.Enabled = false;
        }


        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstFormScrean = (from c in db.FormScreans  select c).ToList();
                GView.DataSource = lstFormScrean;
            }
        }

        private void bindData()
        {
            if (lstFormScrean.Count > 0)
            {
                txtform_IDName.Text = lstFormScrean[position].form_IDName;
                txtform_DisplayName.Text = lstFormScrean[position].form_DisplayName;
                txtform_Descriptions.Text = lstFormScrean[position].form_Descriptions;
                lblID.Text = lstFormScrean[position].formID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstFormScrean.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtform_IDName.Text = "";
                txtform_DisplayName.Text = "";
                txtform_Descriptions.Text = "";
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
        }

        private void GView_Click(object sender, EventArgs e)
        {
            
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                FormScrean = lstFormScrean.Find(b => b.formID == bID);

                position = lstFormScrean.IndexOf(FormScrean);
                bindData();
            }
        }



    }
}
