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
    public partial class frmDocDef : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        DocumentDef DocumentDef = null;
        List<DocumentDef> lstDocumentDef = null;

        components.banner mainbar = new components.banner("frmDocDef");

        public frmDocDef()
        {
            InitializeComponent();
        }


        private void frmDocDef_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmDocDef_LoadData);
        }

        private async System.Threading.Tasks.Task frmDocDef_LoadData(object sender, EventArgs e)
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

                await PublicClass.bindDocumentTypeDefAsync(cmbdocumentTypeDefID);
                getData();
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
            txtdocumentDef_Name.Text = "";
            txtdocumentDefCode.Text = "";
            txtdocumentDef_WarningDays.Text = "0";
            cmbdocumentTypeDefID.SelectedIndex = 0;

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
                            DocumentDef bb = (from c in db.DocumentDefs
                                              where c.documentDefID ==
                                                             bbID
                                              select c).FirstOrDefault();

                            db.DocumentDefs.Remove(bb);
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
                        DocumentDef bb = new DocumentDef();
                        bb.documentDef_Name = txtdocumentDef_Name.Text;
                        bb.documentDef_WarningDays = int.Parse(txtdocumentDef_WarningDays.Text);
                        bb.documentTypeDefID = int.Parse(cmbdocumentTypeDefID.SelectedValue.ToString());
                        bb.documentDefCode = txtdocumentDefCode.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.DocumentDefs.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        DocumentDef bb = (from c in db.DocumentDefs
                                          where c.documentDefID ==
                                                         bbID
                                          select c).FirstOrDefault();

                        bb.documentDef_Name = txtdocumentDef_Name.Text;
                        bb.documentDef_WarningDays = int.Parse(txtdocumentDef_WarningDays.Text);
                        bb.documentTypeDefID = int.Parse(cmbdocumentTypeDefID.SelectedValue.ToString());
                        bb.documentDefCode = txtdocumentDefCode.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.DocumentDefs.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstDocumentDef.Count - 1;
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
            if (position < lstDocumentDef.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstDocumentDef.Count - 1)
            {
                position = lstDocumentDef.Count - 1;
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

            txtdocumentDef_WarningDays.ReadOnly = true;
            txtdocumentDef_Name.ReadOnly = true;
            txtdocumentDefCode.ReadOnly = true;
            cmbdocumentTypeDefID.Enabled = false;

            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtdocumentDef_WarningDays.ReadOnly = false;
            txtdocumentDef_Name.ReadOnly = false;
            txtdocumentDefCode.ReadOnly = false;
            cmbdocumentTypeDefID.Enabled = true;
            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocumentDef = (from c in db.DocumentDefs select c).ToList();

                var val = from c in db.DocumentDefs
                          select new
                          {
                              c.documentDefID,
                              c.documentDefCode,
                              c.documentDef_Name,
                              c.documentDef_WarningDays,
                              c.DocumentTypeDefinition.documentType_Name
                          };
                GView.DataSource = val.ToList();
            }
        }

        private void bindData()
        {
            if (lstDocumentDef.Count > 0)
            {
                txtdocumentDef_Name.Text = lstDocumentDef[position].documentDef_Name;
                txtdocumentDefCode.Text = lstDocumentDef[position].documentDefCode;
                txtdocumentDef_WarningDays.Text = lstDocumentDef[position].documentDef_WarningDays.Value.ToString();


                for (int i = 0; i < cmbdocumentTypeDefID.Items.Count; i++)
                {
                    if (cmbdocumentTypeDefID.Items[i].Value.ToString() == lstDocumentDef[position].documentTypeDefID.Value.ToString())
                    {
                        cmbdocumentTypeDefID.SelectedIndex = i;
                        break;
                    }
                }

                lblID.Text = lstDocumentDef[position].documentDefID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstDocumentDef.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtdocumentDef_Name.Text = "";
                txtdocumentDefCode.Text = "";
                txtdocumentDef_WarningDays.Text = "0";
                cmbdocumentTypeDefID.SelectedIndex = 0;
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                DocumentDef = lstDocumentDef.Find(b => b.documentDefID == bID);

                position = lstDocumentDef.IndexOf(DocumentDef);
                bindData();
            }
        }

        private void txtdocumentDef_WarningDays_Leave(object sender, EventArgs e)
        {
            if (change.Length > 0)
            {
                int r =0;
                if (!int.TryParse(txtdocumentDef_WarningDays.Text, out r))
                {
                    txtdocumentDef_WarningDays.Text = "0";
                }
            }
        }



    }
}
