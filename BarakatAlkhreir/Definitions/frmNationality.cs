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
    public partial class frmNationality : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        Nationality Nationality = null;
        List<Nationality> lstNationality = null;

        components.banner mainbar = new components.banner("frmNationality");

        public frmNationality()
        {
            InitializeComponent();
        }

        private void frmNationalitys_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmNationalitys_LoadData);
        }

        private async System.Threading.Tasks.Task frmNationalitys_LoadData(object sender, EventArgs e)
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
            txtnatio_Code.Text = "";
            txtnatio_namea.Text = "";
            txtnatio_namee.Text = "";
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
                            Nationality bb = (from c in db.Nationalities
                                              where c.nationalityID ==
                                                  bbID
                                              select c).FirstOrDefault();

                            db.Nationalities.Remove(bb);
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
                        Nationality bb = new Nationality();

                        bb.natio_Code = txtnatio_Code.Text;
                        bb.natio_namea = txtnatio_namea.Text;
                        bb.natio_namee = txtnatio_namee.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Nationalities.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        Nationality bb = (from c in db.Nationalities
                                          where c.nationalityID ==
                                              bbID
                                          select c).FirstOrDefault();

                        bb.natio_Code = txtnatio_Code.Text;
                        bb.natio_namea = txtnatio_namea.Text;
                        bb.natio_namee = txtnatio_namee.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Nationalitys.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstNationality.Count - 1;
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
            if (position <lstNationality.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstNationality.Count -1)
            {
                position = lstNationality.Count - 1;
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
            txtnatio_namea.ReadOnly = true;
            txtnatio_namee.ReadOnly = true;
            txtnatio_Code.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtnatio_namea.ReadOnly = false;
            txtnatio_namee.ReadOnly = false;
            txtnatio_Code.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }


        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.Nationalities  select c).ToList();
                }
            }, rows =>
            {
                lstNationality = rows;
                GView.DataSource = lstNationality;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstNationality = (from c in db.Nationalities  select c).ToList();
                GView.DataSource = lstNationality;
            }
        }

        private void bindData()
        {
            if (lstNationality.Count > 0)
            {
                txtnatio_Code.Text = lstNationality[position].natio_Code;
                txtnatio_namea.Text = lstNationality[position].natio_namea;
                txtnatio_namee.Text = lstNationality[position].natio_namee;
                lblID.Text = lstNationality[position].nationalityID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstNationality.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtnatio_Code.Text = "";
                txtnatio_namea.Text = "";
                txtnatio_namee.Text = "";
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
                Nationality = lstNationality.Find(b => b.nationalityID == bID);

                position = lstNationality.IndexOf(Nationality);
                bindData();
            }
        }



    }
}
