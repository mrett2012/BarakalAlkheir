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

namespace BarakalAlkheir.Docs
{
    public partial class frmDocumentTransactions : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        DocumentTransaction DocumentTransaction = null;
        List<DocumentTransaction> lstDocumentTransaction = null;

        components.banner mainbar = new components.banner("frmDocumentTransactions");

        public frmDocumentTransactions()
        {
            InitializeComponent();
        }

        private void frmDocumentTransaction_Load(object sender, EventArgs e)
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

                mainbar.btnFind.Visible = false;

                PublicClass.bindDocumentDef(cmbdocumentDefID);
                cmbdocumentDefID.SelectedIndex = 0;

                PublicClass.bindTransactionDef(cmbtransDefID);
                cmbtransDefID.SelectedIndex = 0;


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
            txtdocumentTrans_Amount.Text = "";
            txtdocumentTrans_Description.Text = "";
            cmbtransDefID.SelectedIndex = 0;
            cmbdocumentDefID.SelectedIndex = 0;
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
                            DocumentTransaction bb = (from c in db.DocumentTransactions
                                                      where c.documentTransID ==
                                           bbID
                                                      select c).FirstOrDefault();

                            db.DocumentTransactions.DeleteObject(bb);
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
                if (change == "add" && inputValidation())
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        DocumentTransaction bb = new DocumentTransaction();

                        bb.documentTrans_Amount = float.Parse(txtdocumentTrans_Amount.Text);
                        bb.documentTrans_Description = txtdocumentTrans_Description.Text;
                        bb.documentDefID = int.Parse(cmbdocumentDefID.SelectedValue.ToString());
                        bb.transDefID = int.Parse(cmbtransDefID.SelectedValue.ToString());

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.DocumentTransactions.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit" && inputValidation())
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        DocumentTransaction bb = (from c in db.DocumentTransactions
                                                  where c.documentTransID ==
                                       bbID
                                                  select c).FirstOrDefault();

                        bb.documentTrans_Amount = float.Parse(txtdocumentTrans_Amount.Text);
                        bb.documentTrans_Description = txtdocumentTrans_Description.Text;
                        bb.documentDefID = int.Parse(cmbdocumentDefID.SelectedValue.ToString());
                        bb.transDefID = int.Parse(cmbtransDefID.SelectedValue.ToString());

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.DocumentTransactions.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstDocumentTransaction.Count - 1;
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

        private bool inputValidation()
        {
            bool temp = false;
            string msg = "";

            double b = 0;
            if (!double.TryParse(txtdocumentTrans_Amount.Text, out b))
            {
                msg += "المبلغ غير صحيح\n";
            }
            if (this.cmbdocumentDefID.SelectedIndex < 1)
            {
                msg += " يجب اختيار من أنواع الوثائق\n";
            }

            if (cmbtransDefID.SelectedIndex < 1)
            {
                msg += " يجب اختيار من الاجرائات\n";
            }

            if (msg.Length > 0)
            {
                MessageBox.Show(msg, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                temp = true;
            }

            return temp;
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
            if (position <lstDocumentTransaction.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstDocumentTransaction.Count -1)
            {
                position = lstDocumentTransaction.Count - 1;
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
            txtdocumentTrans_Description.ReadOnly = true;
            txtdocumentTrans_Amount.ReadOnly = true;
            cmbdocumentDefID.Enabled = false;
            cmbtransDefID.Enabled = false;

            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtdocumentTrans_Description.ReadOnly = false;
            txtdocumentTrans_Amount.ReadOnly = false;

            cmbdocumentDefID.Enabled = true;
            cmbtransDefID.Enabled = true;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocumentTransaction = (from c in db.DocumentTransactions.Include("DocumentDef").Include("TransactionDef") select c).ToList();
                GView.DataSource = lstDocumentTransaction;
                //lstDocumentTransaction[0].TransactionDef.transDef_namea
            }
        }

        private void bindData()
        {
            if (lstDocumentTransaction.Count > 0)
            {
                txtdocumentTrans_Amount.Text = lstDocumentTransaction[position].documentTrans_Amount.Value.ToString();
                txtdocumentTrans_Description.Text = lstDocumentTransaction[position].documentTrans_Description;
                lblID.Text = lstDocumentTransaction[position].documentTransID.ToString();

                for (int i = 0; i < cmbtransDefID.Items.Count; i++)
                {
                    if (cmbtransDefID.Items[i].Value.ToString() == lstDocumentTransaction[position].transDefID.ToString())
                    {
                        cmbtransDefID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < this.cmbdocumentDefID.Items.Count; i++)
                {
                    if (cmbdocumentDefID.Items[i].Value.ToString() == lstDocumentTransaction[position].documentDefID.ToString())
                    {
                        cmbdocumentDefID.SelectedIndex = i;
                        break;
                    }
                }



                mainbar.lblCount.Text = position + 1 + " / " + lstDocumentTransaction.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtdocumentTrans_Amount.Text = "";
                txtdocumentTrans_Description.Text = "";
                lblID.Text = "-1";
                cmbtransDefID.SelectedIndex = 0;
                cmbdocumentDefID.SelectedIndex = 0;

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
        }

        private void GView_Click(object sender, EventArgs e)
        {
            if (GView.CurrentCell.ColumnIndex == 5)
            {
                frmDocumentTransactionSteps frm = new frmDocumentTransactionSteps();
                frm._documentTransID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                frm.documentDef_Name = GView.CurrentRow.Cells[1].Value.ToString();
                frm.transDef_namea = GView.CurrentRow.Cells[2].Value.ToString();
                frm.ShowDialog();
            }
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                DocumentTransaction = lstDocumentTransaction.Find(b => b.documentTransID == bID);

                position = lstDocumentTransaction.IndexOf(DocumentTransaction);
                bindData();
            }
        }





    }
}
