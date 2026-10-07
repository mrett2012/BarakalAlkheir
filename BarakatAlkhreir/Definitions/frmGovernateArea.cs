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
namespace BarakalAlkheir.Definitions
{
    public partial class frmGovernateArea : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        Governorate Governorate = null;
        List<Governorate> lstGovernorate = null;
        List<GovernorateArea> lstGovernorateArea = null;

        components.banner mainbar = new components.banner("frmGovernateArea");

        public frmGovernateArea()
        {
            InitializeComponent();
        }

        private void frmGovernateArea_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmGovernateArea_LoadData);
        }

        private async System.Threading.Tasks.Task frmGovernateArea_LoadData(object sender, EventArgs e)
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

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
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

            txgoverarea_namea.Text = "";
            txtgoverarea_Code.Text = "";
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
                            GovernorateArea bb = (from c in db.GovernorateAreas
                                                  where c.goverareaID ==
                                                      bbID
                                                  select c).FirstOrDefault();

                            db.GovernorateAreas.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                position = 0;
                                lblID.Text = "-1";
                                bindGridView();
                                bindGovernateArea();
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
            bindGovernateArea();
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
                            GovernorateArea bb = new GovernorateArea();

                            bb.goverarea_Code = txtgoverarea_Code.Text;
                            bb.goverarea_namea = txgoverarea_namea.Text;
                            bb.goverarea_namee = "";
                            bb.governorateID = lstGovernorate[position].governorateID;

                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.GovernorateAreas.AddObject(bb);
                            rowsAffected = db.SaveChanges();
                            lblID.Text = bb.goverareaID.ToString();

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
                            GovernorateArea bb = (from c in db.GovernorateAreas
                                                  where c.goverareaID ==
                                                      bbID
                                                  select c).FirstOrDefault();

                            bb.goverarea_Code = txtgoverarea_Code.Text;
                            bb.goverarea_namea = txgoverarea_namea.Text;
                            bb.goverarea_namee = "";
                            bb.governorateID = lstGovernorate[position].governorateID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            rowsAffected = db.SaveChanges();

                            ts.Complete();
                            db.AcceptAllChanges();

                        }
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();
                    bindGridView();
                    bindGovernateArea();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnFind_Click(object sender, EventArgs e)
        {
            ////throw new NotImplementedException();
            //frmfindLicences frm = new frmfindLicences(false, true);
            //frm.ShowDialog();
            //if (frm.found)
            //{
            //    Governorate cl = lstGovernorate.Find(c => c.GovernoratesID == frm.licID);
            //    position = lstGovernorate.IndexOf(cl);
            //    bindData();
            //}
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
            if (position < lstGovernorate.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstGovernorate.Count - 1)
            {
                position = lstGovernorate.Count - 1;
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



            txtGovernateName.ReadOnly = true;


            txtgoverarea_Code.ReadOnly = true;
            txgoverarea_namea.ReadOnly = true;

            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtgoverarea_Code.ReadOnly = false;
            txgoverarea_namea.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstGovernorate = (from c in db.Governorates select c).ToList();
            }
        }

        private void bindData()
        {
            if (lstGovernorate.Count > 0)
            {
                txtGovernateName.Text = lstGovernorate[position].gover_namea;
                mainbar.lblCount.Text = position + 1 + " / " + lstGovernorate.Count;
            }
            else
            {
       
                txtGovernateName.Text = "";

                txgoverarea_namea.Text = "0";
                txtgoverarea_Code.Text = "";
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
            lblID.Text = "-1";
            bindGridView();
            bindGovernateArea();
        }


        private void bindGridView()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int CLID = lstGovernorate[position].governorateID;
                var val = from c in db.GovernorateAreas
                          where c.governorateID == CLID
                          select new
                          {
                              c.goverareaID,
                              c.goverarea_Code,
                              c.goverarea_namea
                          };
                GView.DataSource = val;
            }
        }

        private void bindGovernateArea()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int CLID = lstGovernorate[position].governorateID;

                lstGovernorateArea = (from c in db.GovernorateAreas
                                                where c.governorateID == CLID
                                                select c).ToList();



                if (lblID.Text == "-1")
                {
                    if (lstGovernorateArea.Count > 0)
                    {
                        txgoverarea_namea.Text = lstGovernorateArea[0].goverarea_namea;
                        txtgoverarea_Code.Text =  lstGovernorateArea[0].goverarea_Code;
                        GView.Rows[0].IsSelected = true;
                        lblID.Text = lstGovernorateArea[0].goverareaID.ToString();
                    }
                    else
                    {
                        txgoverarea_namea.Text = "0";
                        txtgoverarea_Code.Text = "";
                        lblID.Text = "-1";
                    }
                }
                else
                {
                    int cldID = int.Parse(lblID.Text);
                    GovernorateArea cld = lstGovernorateArea.Find(c => c.goverareaID == cldID);
                    txgoverarea_namea.Text = cld.goverarea_namea;
                    txtgoverarea_Code.Text = cld.goverarea_Code;

                }

            }
        }


        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                lblID.Text = bID.ToString();
                bindGovernateArea();

            }
        }

    }
}
