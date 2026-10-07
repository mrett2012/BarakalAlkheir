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
    public partial class frmDocGroupTypeDef : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        DocumentTypeDefinition DocumentTypeDefinition = null;
        List<DocumentTypeDefinition> lstDocumentTypeDefinition = null;

        components.banner mainbar = new components.banner("frmDocGroupTypeDef");

        public frmDocGroupTypeDef()
        {
            InitializeComponent();
        }

        private void frmDocGroupTypeDef_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmDocGroupTypeDef_LoadData);
        }

        private async System.Threading.Tasks.Task frmDocGroupTypeDef_LoadData(object sender, EventArgs e)
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
            txtdocumentType_Code.Text = "";
            txtdocumentType_Name.Text = "";

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
                            DocumentTypeDefinition bb = (from c in db.DocumentTypeDefinitions
                                                         where c.documentTypeDefID ==
                                                             bbID
                                                         select c).FirstOrDefault();

                            db.DocumentTypeDefinitions.DeleteObject(bb);
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
                        DocumentTypeDefinition bb = new DocumentTypeDefinition();
                        bb.documentType_Code = txtdocumentType_Code.Text;
                        bb.documentType_Name = txtdocumentType_Name.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.DocumentTypeDefinitions.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        DocumentTypeDefinition bb = (from c in db.DocumentTypeDefinitions
                                                     where c.documentTypeDefID ==
                                                         bbID
                                                     select c).FirstOrDefault();
                        bb.documentType_Code = txtdocumentType_Code.Text;
                        bb.documentType_Name = txtdocumentType_Name.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.DocumentTypeDefinitions.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstDocumentTypeDefinition.Count - 1;
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
            if (position < lstDocumentTypeDefinition.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstDocumentTypeDefinition.Count - 1)
            {
                position = lstDocumentTypeDefinition.Count - 1;
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

            txtdocumentType_Name.ReadOnly = true;
            txtdocumentType_Code.ReadOnly = true;
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtdocumentType_Name.ReadOnly = false;
            txtdocumentType_Code.ReadOnly = false;
            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.DocumentTypeDefinitions select c).ToList();
                }
            }, rows =>
            {
                lstDocumentTypeDefinition = rows;
                GView.DataSource = lstDocumentTypeDefinition;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocumentTypeDefinition = (from c in db.DocumentTypeDefinitions select c).ToList();
                GView.DataSource = lstDocumentTypeDefinition;
            }
        }

        private void bindData()
        {
            if (lstDocumentTypeDefinition.Count > 0)
            {
                txtdocumentType_Code.Text = lstDocumentTypeDefinition[position].documentType_Code;
                txtdocumentType_Name.Text = lstDocumentTypeDefinition[position].documentType_Name;

                lblID.Text = lstDocumentTypeDefinition[position].documentTypeDefID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstDocumentTypeDefinition.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtdocumentType_Code.Text = "";
                txtdocumentType_Name.Text = "";
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
                DocumentTypeDefinition = lstDocumentTypeDefinition.Find(b => b.documentTypeDefID == bID);

                position = lstDocumentTypeDefinition.IndexOf(DocumentTypeDefinition);
                bindData();
            }
        }
 
    }
}
