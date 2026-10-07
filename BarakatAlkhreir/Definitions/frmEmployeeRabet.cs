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
    public partial class frmEmployeeRabet : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        EmployeeRabet EmployeeRabet = null;
        List<EmployeeRabet> lstEmployeeRabet = null;

        components.banner mainbar = new components.banner("frmEmployeeRabet");

        public frmEmployeeRabet()
        {
            InitializeComponent();
        }

        private void frmEmployeeRabets_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmEmployeeRabets_LoadData);
        }

        private async System.Threading.Tasks.Task frmEmployeeRabets_LoadData(object sender, EventArgs e)
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
            txtrabet_namea.Text = "";
            txtrabet_description.Text = "";
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
                            EmployeeRabet bb = (from c in db.EmployeeRabets
                                              where c.rabetID ==
                                                  bbID
                                              select c).FirstOrDefault();

                            db.EmployeeRabets.DeleteObject(bb);
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
                        EmployeeRabet bb = new EmployeeRabet();

                        bb.rabet_namea = txtrabet_namea.Text;
                        bb.rabet_description = txtrabet_description.Text;
        

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.EmployeeRabets.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        EmployeeRabet bb = (from c in db.EmployeeRabets
                                            where c.rabetID ==
                                              bbID
                                          select c).FirstOrDefault();

                        bb.rabet_namea = txtrabet_namea.Text;
                        bb.rabet_description = txtrabet_description.Text;
               

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.EmployeeRabets.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstEmployeeRabet.Count - 1;
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
            if (position <lstEmployeeRabet.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstEmployeeRabet.Count -1)
            {
                position = lstEmployeeRabet.Count - 1;
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
            txtrabet_description.ReadOnly = true;
            txtrabet_namea.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtrabet_description.ReadOnly = false;
            txtrabet_namea.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }


        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.EmployeeRabets  select c).ToList();
                }
            }, rows =>
            {
                lstEmployeeRabet = rows;
                GView.DataSource = lstEmployeeRabet;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstEmployeeRabet = (from c in db.EmployeeRabets  select c).ToList();
                GView.DataSource = lstEmployeeRabet;
            }
        }

        private void bindData()
        {
            if (lstEmployeeRabet.Count > 0)
            {
                txtrabet_namea.Text = lstEmployeeRabet[position].rabet_namea;
                txtrabet_description.Text = lstEmployeeRabet[position].rabet_description;
                lblID.Text = lstEmployeeRabet[position].rabetID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstEmployeeRabet.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtrabet_namea.Text = "";
                txtrabet_description.Text = "";
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
                EmployeeRabet = lstEmployeeRabet.Find(b => b.rabetID == bID);

                position = lstEmployeeRabet.IndexOf(EmployeeRabet);
                bindData();
            }
        }



    }
}
