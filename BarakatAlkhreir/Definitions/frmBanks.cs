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
    public partial class frmBanks : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        Bank bank = null;
        List<Bank> lstBank = null;

        components.banner mainbar = new components.banner("frmBanks");

        public frmBanks()
        {
            InitializeComponent();
        }

        private void frmBanks_Load(object sender, EventArgs e)
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
            txtCode.Text = "";
            txtBankNamea.Text = "";
            txtAccNo.Text = "";
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
                            Bank bb = (from c in db.Banks
                                       where c.bankID ==
                                           bbID
                                       select c).FirstOrDefault();

                            db.Banks.DeleteObject(bb);
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
                        Bank bb = new Bank();
                        bb.bank_Code = txtCode.Text;
                        bb.bank_namea = txtBankNamea.Text;
                        bb.bank_namee = "";
                        bb.bank_accNo = txtAccNo.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Banks.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        Bank bb = (from c in db.Banks
                                   where c.bankID ==
                                       bbID
                                   select c).FirstOrDefault();
                        bb.bank_Code = txtCode.Text;
                        bb.bank_namea = txtBankNamea.Text;
                        bb.bank_namee = "";
                        bb.bank_accNo = txtAccNo.Text;
                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Banks.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstBank.Count - 1;
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
            if (position <lstBank.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstBank.Count -1)
            {
                position = lstBank.Count - 1;
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
            txtAccNo.ReadOnly = true;
            txtBankNamea.ReadOnly = true;
            txtCode.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtAccNo.ReadOnly = false;
            txtBankNamea.ReadOnly = false;
            txtCode.ReadOnly = false;
            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstBank = (from c in db.Banks select c).ToList();
                GView.DataSource = lstBank;
            }
        }

        private void bindData()
        {
            if (lstBank.Count > 0)
            {
                txtCode.Text = lstBank[position].bank_Code;
                txtBankNamea.Text = lstBank[position].bank_namea;
                txtAccNo.Text = lstBank[position].bank_accNo;
                lblID.Text = lstBank[position].bankID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstBank.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtCode.Text = "";
                txtBankNamea.Text = "";
                txtAccNo.Text = "";
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
                bank = lstBank.Find(b => b.bankID == bID);

                position = lstBank.IndexOf(bank);
                bindData();
            }
        }



    }
}
