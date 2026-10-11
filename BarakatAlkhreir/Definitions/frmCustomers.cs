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
    public partial class frmCustomers : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        Customer Customer = null;
        List<Customer> lstCustomer = null;

        components.banner mainbar = new components.banner("frmCustomers");

        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmCustomers_LoadData);
        }

        private async System.Threading.Tasks.Task frmCustomers_LoadData(object sender, EventArgs e)
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

                await PublicClass.bindNationalityAsync(cmbcustomer_nationalityID);

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
            chkcustomer_active.Checked = true;
            cmbcustomer_nationalityID.SelectedIndex = 0;
            txtcustomer_Address.Text = "";
            txtcustomer_BirthDate.Text = DateTime.Now.ToShortDateString();
            txtcustomer_CivilID.Text = "";
            txtcustomer_Name.Text = "";
            txtcustomer_phoneNumber1.Text = "";
            txtcustomer_phoneNumber2.Text = "";
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
                            Customer bb = (from c in db.Customers
                                           where c.customerID ==
                                               bbID
                                           select c).FirstOrDefault();

                            db.Customers.Remove(bb);
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
                        Customer bb = new Customer();

                        bb.customer_nationalityID = int.Parse(cmbcustomer_nationalityID.SelectedValue.ToString());
                        bb.customer_Address = txtcustomer_Address.Text;
                        bb.customer_BirthDate = DateTime.Parse(txtcustomer_BirthDate.Text);
                        bb.customer_CivilID = txtcustomer_CivilID.Text;
                        bb.customer_Name = txtcustomer_Name.Text;
                        bb.customer_phoneNumber1 = txtcustomer_phoneNumber1.Text;
                        bb.customer_phoneNumber2 = txtcustomer_phoneNumber2.Text;
                        bb.customer_active = chkcustomer_active.Checked;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Customers.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        Customer bb = (from c in db.Customers
                                       where c.customerID ==
                                           bbID
                                       select c).FirstOrDefault();

                        bb.customer_nationalityID = int.Parse(cmbcustomer_nationalityID.SelectedValue.ToString());
                        bb.customer_Address = txtcustomer_Address.Text;
                        bb.customer_BirthDate = DateTime.Parse(txtcustomer_BirthDate.Text);
                        bb.customer_CivilID = txtcustomer_CivilID.Text;
                        bb.customer_Name = txtcustomer_Name.Text;
                        bb.customer_phoneNumber1 = txtcustomer_phoneNumber1.Text;
                        bb.customer_phoneNumber2 = txtcustomer_phoneNumber2.Text;
                        bb.customer_active = chkcustomer_active.Checked;
                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Customers.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstCustomer.Count - 1;
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
            frmFindCustomers frm = new frmFindCustomers();
            frm.ShowDialog();
            if (frm.found)
            {
                Customer cl = this.lstCustomer.Find(c => c.customerID == frm.cID);
                position = lstCustomer.IndexOf(cl);
                bindData();
            }
        }

        void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                if (position > 0)
                {
                    position = 0;
                    bindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (position > 0)
                {
                    position -= 1;
                    bindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (position < lstCustomer.Count - 1)
                {
                    position += 1;
                    bindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                if (position < lstCustomer.Count - 1)
                {
                    position = lstCustomer.Count - 1;
                    bindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void disableControls()
        {
            mainbar.enableButtons();

            cmbcustomer_nationalityID.Enabled = false;
            txtcustomer_BirthDate.Enabled = false;
            chkcustomer_active.Enabled = false;

            txtcustomer_Address.ReadOnly = true;            
            txtcustomer_CivilID.ReadOnly = true;
            txtcustomer_Name.ReadOnly = true;
            txtcustomer_phoneNumber1.ReadOnly = true;
            txtcustomer_phoneNumber2.ReadOnly = true;

            

            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            cmbcustomer_nationalityID.Enabled = true;
            txtcustomer_BirthDate.Enabled = true;
            chkcustomer_active.Enabled = true;

            txtcustomer_Address.ReadOnly = false;
            txtcustomer_CivilID.ReadOnly = false;
            txtcustomer_Name.ReadOnly = false;
            txtcustomer_phoneNumber1.ReadOnly = false;
            txtcustomer_phoneNumber2.ReadOnly = false;


            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.Customers  select c).ToList();
                }
            }, rows =>
            {
                lstCustomer = rows;
                GView.DataSource = lstCustomer;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCustomer = (from c in db.Customers  select c).ToList();
                GView.DataSource = lstCustomer;
            }
        }

        private void bindData()
        {
            if (lstCustomer.Count > 0)
            {
                txtcustomer_Address.Text = lstCustomer[position].customer_Address;
                txtcustomer_BirthDate.Text = lstCustomer[position].customer_BirthDate.Value.ToShortDateString();
                txtcustomer_CivilID.Text = lstCustomer[position].customer_CivilID;
                txtcustomer_Name.Text = lstCustomer[position].customer_Name;
                txtcustomer_phoneNumber1.Text = lstCustomer[position].customer_phoneNumber1;
                txtcustomer_phoneNumber2.Text = lstCustomer[position].customer_phoneNumber2;
                chkcustomer_active.Checked = lstCustomer[position].customer_active.Value;

                for (int i = 0; i < cmbcustomer_nationalityID.Items.Count; i++)
                {
                    if (cmbcustomer_nationalityID.Items[i].Value.ToString() == lstCustomer[position].customer_nationalityID.ToString())
                    {
                        cmbcustomer_nationalityID.SelectedIndex = i;
                        break;
                    }
                }


                lblID.Text = lstCustomer[position].customerID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstCustomer.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                cmbcustomer_nationalityID.SelectedIndex = 0;
                chkcustomer_active.Checked = true;
                txtcustomer_Address.Text = "";
                txtcustomer_BirthDate.Text = DateTime.Now.ToShortDateString();
                txtcustomer_CivilID.Text = "";
                txtcustomer_Name.Text = "";
                txtcustomer_phoneNumber1.Text = "";
                txtcustomer_phoneNumber2.Text = "";

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
                Customer Customer = lstCustomer.Find(b => b.customerID == bID);

                position = lstCustomer.IndexOf(Customer);
                bindData();
            }
        }

        private void txtcustomer_BirthDate_Leave(object sender, EventArgs e)
        {
            //DateTime rr;
            //if (!DateTime.TryParse(txtcustomer_BirthDate.Text, out rr))
            //{
            //    txtcustomer_BirthDate.Text = DateTime.Now.ToShortDateString();
            //}
        }


    }
}
