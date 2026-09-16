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
    public partial class frmDesignations : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        Designation Designation = null;
        List<Designation> lstDesignation = null;

        components.banner mainbar = new components.banner("frmDesignations");

        public frmDesignations()
        {
            InitializeComponent();
        }

        private void frmDesignations_Load(object sender, EventArgs e)
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
            txtdesig_Code.Text = "";
            txtdesig_namea.Text = "";
            txtdesig_namee.Text = "";
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
                            Designation bb = (from c in db.Designations
                                              where c.designationID ==
                                                  bbID
                                              select c).FirstOrDefault();

                            db.Designations.DeleteObject(bb);
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

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                change = "edit";
                enableControls();
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
                        Designation bb = new Designation();

                        bb.desig_Code = txtdesig_Code.Text;
                        bb.desig_namea = txtdesig_namea.Text;
                        bb.desig_namee = txtdesig_namee.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Designations.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        Designation bb = (from c in db.Designations
                                          where c.designationID ==
                                              bbID
                                          select c).FirstOrDefault();

                        bb.desig_Code = txtdesig_Code.Text;
                        bb.desig_namea = txtdesig_namea.Text;
                        bb.desig_namee = txtdesig_namee.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Designations.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstDesignation.Count - 1;
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
            if (position <lstDesignation.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstDesignation.Count -1)
            {
                position = lstDesignation.Count - 1;
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
            txtdesig_namea.ReadOnly = true;
            txtdesig_namee.ReadOnly = true;
            txtdesig_Code.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtdesig_namea.ReadOnly = false;
            txtdesig_namee.ReadOnly = false;
            txtdesig_Code.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }


        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDesignation = (from c in db.Designations  select c).ToList();
                GView.DataSource = lstDesignation;
            }
        }

        private void bindData()
        {
            if (lstDesignation.Count > 0)
            {
                txtdesig_Code.Text = lstDesignation[position].desig_Code;
                txtdesig_namea.Text = lstDesignation[position].desig_namea;
                txtdesig_namee.Text = lstDesignation[position].desig_namee;
                lblID.Text = lstDesignation[position].designationID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstDesignation.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtdesig_Code.Text = "";
                txtdesig_namea.Text = "";
                txtdesig_namee.Text = "";
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
                Designation = lstDesignation.Find(b => b.designationID == bID);

                position = lstDesignation.IndexOf(Designation);
                bindData();
            }
        }



    }
}
