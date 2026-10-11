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
    public partial class frmCarSuppliers : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        CarSupllier CarSupllier = null;
        List<CarSupllier> lstCarSupllier = null;

        components.banner mainbar = new components.banner("frmCarSuppliers");

        public frmCarSuppliers()
        {
            InitializeComponent();
        }

        private void frmCarSuplliers_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmCarSuplliers_LoadData);
        }

        private async System.Threading.Tasks.Task frmCarSuplliers_LoadData(object sender, EventArgs e)
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

            txtcarSuppliser_Address.Text = "";
            txtcarSuppliser_Contact.Text = "";
            txtcarSuppliser_Name.Text = "";
            txtcarSuppliser_PersonInCharge.Text = "";
            txtcarSuppliser_remarks.Text = "";
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
                            CarSupllier bb = (from c in db.CarSuplliers
                                              where c.carSuppliserID ==
                                                  bbID
                                              select c).FirstOrDefault();

                            db.CarSuplliers.Remove(bb);
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
                        CarSupllier bb = new CarSupllier();

                        bb.carSuppliser_Address = txtcarSuppliser_Address.Text;
                        bb.carSuppliser_Contact = txtcarSuppliser_Contact.Text;
                        bb.carSuppliser_Name = txtcarSuppliser_Name.Text;
                        bb.carSuppliser_PersonInCharge = txtcarSuppliser_PersonInCharge.Text;
                        bb.carSuppliser_remarks = txtcarSuppliser_remarks.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.CarSuplliers.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        CarSupllier bb = (from c in db.CarSuplliers
                                          where c.carSuppliserID ==
                                              bbID
                                          select c).FirstOrDefault();

                        bb.carSuppliser_Address = txtcarSuppliser_Address.Text;
                        bb.carSuppliser_Contact = txtcarSuppliser_Contact.Text;
                        bb.carSuppliser_Name = txtcarSuppliser_Name.Text;
                        bb.carSuppliser_PersonInCharge = txtcarSuppliser_PersonInCharge.Text;
                        bb.carSuppliser_remarks = txtcarSuppliser_remarks.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.CarSuplliers.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstCarSupllier.Count - 1;
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
            CarsM.frmfindCarsSupplier frm = new CarsM.frmfindCarsSupplier();
            frm.ShowDialog();
            if (frm.found)
            {
                CarSupllier cl = this.lstCarSupllier.Find(c => c.carSuppliserID == frm.carSuppliserID);
                position = lstCarSupllier.IndexOf(cl);
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
            if (position <lstCarSupllier.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstCarSupllier.Count -1)
            {
                position = lstCarSupllier.Count - 1;
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

            txtcarSuppliser_Address.ReadOnly = true;
            txtcarSuppliser_Contact.ReadOnly = true;
            txtcarSuppliser_Name.ReadOnly = true;
            txtcarSuppliser_PersonInCharge.ReadOnly = true;
            txtcarSuppliser_remarks.ReadOnly = true;


            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtcarSuppliser_Address.ReadOnly = false;
            txtcarSuppliser_Contact.ReadOnly = false;
            txtcarSuppliser_Name.ReadOnly = false;
            txtcarSuppliser_PersonInCharge.ReadOnly = false;
            txtcarSuppliser_remarks.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }


        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.CarSuplliers  select c).ToList();
                }
            }, rows =>
            {
                lstCarSupllier = rows;
                GView.DataSource = lstCarSupllier;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCarSupllier = (from c in db.CarSuplliers  select c).ToList();
                GView.DataSource = lstCarSupllier;
            }
        }

        private void bindData()
        {
            if (lstCarSupllier.Count > 0)
            {

                txtcarSuppliser_Address.Text = lstCarSupllier[position].carSuppliser_Address;
                txtcarSuppliser_remarks.Text = lstCarSupllier[position].carSuppliser_remarks;

                txtcarSuppliser_Name.Text = lstCarSupllier[position].carSuppliser_Name;
                txtcarSuppliser_PersonInCharge.Text = lstCarSupllier[position].carSuppliser_PersonInCharge;
                txtcarSuppliser_Contact.Text = lstCarSupllier[position].carSuppliser_Contact;


                lblID.Text = lstCarSupllier[position].carSuppliserID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstCarSupllier.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtcarSuppliser_Name.Text = "";
                txtcarSuppliser_PersonInCharge.Text = "";
                txtcarSuppliser_Contact.Text = "";
                txtcarSuppliser_Address.Text = "";
                txtcarSuppliser_remarks.Text = "";

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
                CarSupllier = lstCarSupllier.Find(b => b.carSuppliserID == bID);

                position = lstCarSupllier.IndexOf(CarSupllier);
                bindData();
            }
        }



    }
}
