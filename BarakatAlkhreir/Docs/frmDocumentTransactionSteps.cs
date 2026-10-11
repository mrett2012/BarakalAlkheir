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
    public partial class frmDocumentTransactionSteps : Telerik.WinControls.UI.RadForm
    {

        public int _documentTransID = -1;
        public string transDef_namea = "";
        public string documentDef_Name = "";

        string change = "";
        int position = 0;
        DocumentTransactionStep DocumentTransactionStep = null;
        List<DocumentTransactionStep> lstDocumentTransactionStep = null;

        components.banner mainbar = new components.banner("frmDocumentTransactionSteps");

        public frmDocumentTransactionSteps()
        {
            InitializeComponent();
        }

        private void frmDocumentTransactionSteps_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmDocumentTransactionSteps_LoadData);
        }

        private async System.Threading.Tasks.Task frmDocumentTransactionSteps_LoadData(object sender, EventArgs e)
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

                txtdocumentDef_Name.Text = documentDef_Name;
                txttransDef_namea.Text = transDef_namea;


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
            txtdocumentTransSetp_name.Text = "";
            txtdocumentTransSetp_Period.Text = "";
            txtdocumentTransSetp_pCode.Text = "";
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
                            DocumentTransactionStep bb = (from c in db.DocumentTransactionSteps
                                                          where c.documentTransSetpID == bbID
                                                          select c).FirstOrDefault();

                            db.DocumentTransactionSteps.Remove(bb);
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
                        DocumentTransactionStep bb = new DocumentTransactionStep();

                        bb.documentTransSetp_name = txtdocumentTransSetp_name.Text;
                        bb.documentTransSetp_Period = int.Parse(txtdocumentTransSetp_Period.Text);
                        bb.documentTransSetp_pCode = txtdocumentTransSetp_pCode.Text;
                        bb.documentTransSetp_Order = 1;
                        bb.documentTransID = _documentTransID;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.DocumentTransactionSteps.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit" && inputValidation())
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        DocumentTransactionStep bb = (from c in db.DocumentTransactionSteps
                                                      where c.documentTransSetpID == bbID
                                                      select c).FirstOrDefault();

                        bb.documentTransSetp_name = txtdocumentTransSetp_name.Text;
                        bb.documentTransSetp_Period = int.Parse(txtdocumentTransSetp_Period.Text);
                        bb.documentTransSetp_pCode = txtdocumentTransSetp_pCode.Text;
                        bb.documentTransSetp_Order = 1;
                        bb.documentTransID = _documentTransID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.DocumentTransactionSteps.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstDocumentTransactionStep.Count - 1;
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

            int b = 0;
            if (!int.TryParse(txtdocumentTransSetp_Period.Text, out b))
            {
                msg += "عدد الأيام غير صحيح\n";
            }
            if (txtdocumentTransSetp_name.Text.Length < 4)
            {
                msg += " تفاصيل المرحلة غير صحيح\n";
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
            if (position < lstDocumentTransactionStep.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstDocumentTransactionStep.Count - 1)
            {
                position = lstDocumentTransactionStep.Count - 1;
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
            txtdocumentTransSetp_name.ReadOnly = true;
            txtdocumentTransSetp_Period.ReadOnly = true;
            txtdocumentTransSetp_pCode.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            txtdocumentTransSetp_name.ReadOnly = false;
            txtdocumentTransSetp_Period.ReadOnly = false;
            txtdocumentTransSetp_pCode.ReadOnly = false;
            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (var db = new BarakeitAlkheirEntities()) { return (from c in db.DocumentTransactionSteps where c.documentTransID == _documentTransID select c).ToList(); }
            }, rows =>
            {
                lstDocumentTransactionStep = rows;

                GView.DataSource = lstDocumentTransactionStep;
                //lstDocumentTransactionStepStep[0].TransactionDef.transDef_namea

            });
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocumentTransactionStep = (from c in db.DocumentTransactionSteps where c.documentTransID == _documentTransID select c).ToList();
                GView.DataSource = lstDocumentTransactionStep;
                //lstDocumentTransactionStepStep[0].TransactionDef.transDef_namea
            }
        }

        private void bindData()
        {
            if (lstDocumentTransactionStep.Count > 0)
            {
                this.txtdocumentTransSetp_name.Text = lstDocumentTransactionStep[position].documentTransSetp_name.ToString();
                this.txtdocumentTransSetp_Period.Text = lstDocumentTransactionStep[position].documentTransSetp_Period.Value.ToString();
                txtdocumentTransSetp_pCode.Text = lstDocumentTransactionStep[position].documentTransSetp_pCode;
                lblID.Text = lstDocumentTransactionStep[position].documentTransSetpID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstDocumentTransactionStep.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtdocumentTransSetp_name.Text = "";
                txtdocumentTransSetp_Period.Text = "";
                txtdocumentTransSetp_pCode.Text = "";
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
        }


        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    int rowIndex = e.RowIndex;
            //    int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
            //    DocumentTransactionStep = lstDocumentTransactionStep.Find(b => b.documentTransSetpID == bID);

            //    position = lstDocumentTransactionStep.IndexOf(DocumentTransactionStep);
            //    bindData();
            //}
        }

        private void GView_Click(object sender, EventArgs e)
        {
            //int rowIndex = e.RowIndex;
            if (GView.CurrentRow.Cells[0].Value != null)
            {
                int bID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                DocumentTransactionStep = lstDocumentTransactionStep.Find(b => b.documentTransSetpID == bID);

                position = lstDocumentTransactionStep.IndexOf(DocumentTransactionStep);
                bindData();
            }
        }



    }
}
