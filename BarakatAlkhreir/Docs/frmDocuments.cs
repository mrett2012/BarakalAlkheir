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
    public partial class frmDocuments : Telerik.WinControls.UI.RadForm
    {
        public string action = "";

        string change = "";
        int position = 0;
        List<Document> lstDocument = null;

        components.banner mainbar = new components.banner("frmDocuments");

        public int document_TypeID = -1;
        public int documentTypeDefID = -1;
        public int documentID = -1;
        public bool isExit = false;

        public frmDocuments()
        {
            InitializeComponent();
        }

        private void frmDocuments_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmDocuments_LoadData);
        }

        private async System.Threading.Tasks.Task frmDocuments_LoadData(object sender, EventArgs e)
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

                await PublicClass.bindNationalityAsync(cmbdocument_placeOfIssue);
                await PublicClass.bindDocumentDefAsync(cmbdocumentDefID, documentTypeDefID);

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
                
                Document lst = lstDocument.Find(c => c.documentID == documentID);
                if(lst != null)
                    position = lstDocument.IndexOf(lst);

                bindData();
                timer1.Start();

            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void btnAdd_Click(object sender, EventArgs e)
        {
            change = "add";
            enableControls();
            cmbdocument_placeOfIssue.SelectedIndex = 0;
            cmbdocumentDefID.SelectedIndex = 0;
            txtdocument_endDate.Text = DateTime.Now.ToShortDateString();
            txtdocument_No.Text = "";
            txtdocument_Remarks.Text = "";
            txtdocument_startDate.Text = DateTime.Now.ToShortDateString();
            txtdocument_Status.Text = "";
            txtdocument_Type.Text = "";
        }

        public void btnEdit_Click(object sender, EventArgs e)
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
                            Document bb = (from c in db.Documents
                                           where c.documentID ==
                                               bbID
                                           select c).FirstOrDefault();

                            db.Documents.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                position = 0;
                                documentID = -1;
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
                        Document bb = new Document();

                        bb.document_placeOfIssue = cmbdocument_placeOfIssue.Text;
                        bb.documentDefID = int.Parse(cmbdocumentDefID.SelectedValue.ToString());
                        bb.document_TypeID = document_TypeID;

                        DateTime ddd = DateTime.Now;
                        if (DateTime.TryParse(txtdocument_endDate.Text, out ddd))
                        {
                            bb.document_endDate = ddd;
                        }
                        if (DateTime.TryParse(txtdocument_startDate.Text, out ddd))
                        {
                            bb.document_startDate = ddd;

                        }

                        bb.document_No = txtdocument_No.Text;
                        bb.document_Remarks = txtdocument_Remarks.Text;

                        bb.document_Status = txtdocument_Status.Text;
                        bb.document_Type = txtdocument_Type.Text;

                        bb.document_text1 = "";
                        bb.document_text2 = "";
                        bb.document_text3 = "";
                        bb.document_text4 = "";
                        bb.document_text5 = "";


                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Documents.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            documentID = bb.documentID;
                        }
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        Document bb = (from c in db.Documents
                                       where c.documentID ==
                                           bbID
                                       select c).FirstOrDefault();

                        bb.document_placeOfIssue = cmbdocument_placeOfIssue.Text;
                        bb.documentDefID = int.Parse(cmbdocumentDefID.SelectedValue.ToString());
                        bb.document_TypeID = document_TypeID;

                        DateTime ddd = DateTime.Now;
                        if (DateTime.TryParse(txtdocument_endDate.Text, out ddd))
                        {
                            bb.document_endDate = ddd;
                        }
                        else
                        {
                            bb.document_endDate = null;
                        }
                        if (DateTime.TryParse(txtdocument_startDate.Text, out ddd))
                        {
                            bb.document_startDate = ddd;
                        }
                        else
                        {
                            bb.document_startDate = null;
                        }

                        bb.document_No = txtdocument_No.Text;
                        bb.document_Remarks = txtdocument_Remarks.Text;

                        bb.document_Status = txtdocument_Status.Text;
                        bb.document_Type = txtdocument_Type.Text;

                        bb.document_text1 = "";
                        bb.document_text2 = "";
                        bb.document_text3 = "";
                        bb.document_text4 = "";
                        bb.document_text5 = "";

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Documents.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstDocument.Count - 1;
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
            if (position < lstDocument.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstDocument.Count - 1)
            {
                position = lstDocument.Count - 1;
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

            cmbdocument_placeOfIssue.Enabled = false;
            cmbdocumentDefID.Enabled = false;
            txtdocument_endDate.Enabled = false;
            txtdocument_startDate.Enabled = false;

            txtdocument_No.ReadOnly = true;
            txtdocument_Remarks.ReadOnly = true;      
            txtdocument_Status.ReadOnly = true;
            txtdocument_Type.ReadOnly = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            cmbdocument_placeOfIssue.Enabled = true;
            cmbdocumentDefID.Enabled = true;
            txtdocument_endDate.Enabled = true;
            txtdocument_startDate.Enabled = true;

            txtdocument_No.ReadOnly = false;
            txtdocument_Remarks.ReadOnly = false;
            txtdocument_Status.ReadOnly = false;
            txtdocument_Type.ReadOnly = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocument = (from c in db.Documents
                               where c.document_TypeID == document_TypeID &&
                               c.documentDefID == documentTypeDefID &&
                               (c.DocumentDef.documentDef_Name != "جواز السفر" &&
                                         c.DocumentDef.documentDef_Name != "الجواز السفر" &&
                                         c.DocumentDef.documentDef_Name != "جواز سفر") &&
                                         (c.DocumentDef.documentDef_Name != "اقامة" &&
                                            c.DocumentDef.documentDef_Name != "إقامة" &&
                                            c.DocumentDef.documentDef_Name != "الاقامة" &&
                                       c.DocumentDef.documentDef_Name != "الإقامة" &&
                                            c.DocumentDef.documentDef_Name != "أقامة") &&
                                            (c.DocumentDef.documentDef_Name != "الرخصة" &&
                                         c.DocumentDef.documentDef_Name != "رخصة" &&
                                         c.DocumentDef.documentDef_Name != "الرخصة التجارية" &&
                                         c.DocumentDef.documentDef_Name != "رخصة تجارية")
                               select c).ToList();
            }
        }

        private void bindData()
        {
            if (lstDocument.Count > 0)
            {
                if (lstDocument[position].document_endDate.HasValue)
                {
                    txtdocument_endDate.Text = lstDocument[position].document_endDate.Value.ToShortDateString();
                }
                else
                {
                    txtdocument_endDate.Text = "";
                }

                txtdocument_No.Text = lstDocument[position].document_No;
                txtdocument_Remarks.Text = lstDocument[position].document_Remarks;
                if (lstDocument[position].document_startDate.HasValue)
                {
                    txtdocument_startDate.Text = lstDocument[position].document_startDate.Value.ToShortDateString();
                }
                else
                {
                    txtdocument_startDate.Text = "";
                }
                txtdocument_Status.Text = lstDocument[position].document_Status;
                txtdocument_Type.Text = lstDocument[position].document_Type;
                cmbdocument_placeOfIssue.Text = lstDocument[position].document_placeOfIssue;

                for (int i = 0; i < cmbdocumentDefID.Items.Count; i++)
                {
                    if (cmbdocumentDefID.Items[i].Value.ToString() == lstDocument[position].documentDefID.Value.ToString())
                    {
                        cmbdocumentDefID.SelectedIndex = i;
                    }
                }

                
                lblID.Text = lstDocument[position].documentID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstDocument.Count;
            }
            else
            {
                cmbdocument_placeOfIssue.SelectedIndex = 0;
                cmbdocumentDefID.SelectedIndex = 0;
                txtdocument_endDate.Text = DateTime.Now.ToShortDateString();
                txtdocument_No.Text = "";
                txtdocument_Remarks.Text = "";
                txtdocument_startDate.Text = DateTime.Now.ToShortDateString();
                txtdocument_Status.Text = "";
                txtdocument_Type.Text = "";

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (action == "add")
            {
                btnAdd_Click(sender, e);
            }
            else if (action == "edit")
            {
                btnEdit_Click(sender, e);
            }

            timer1.Stop();
        }


    }
}
