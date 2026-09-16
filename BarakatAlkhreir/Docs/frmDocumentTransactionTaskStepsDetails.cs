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

namespace BarakalAlkheir.Docs
{
    public partial class frmDocumentTransactionTaskSetpsDetails : Telerik.WinControls.UI.RadForm
    {
        public int _documentTransTaskDetailsID;
        public bool isLastTask = false;


        List<documentTransTaskDetail> lstdocumentTransTaskDetail = null;
        components.banner mainbar = new components.banner("frmDocumentTransactionTaskSetpsDetails");

        public frmDocumentTransactionTaskSetpsDetails()
        {
            InitializeComponent();
        }

        private void frmDocumentTransactionTaskSetpsDetails_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                this.Controls.Add(mainbar);

                mainbar.btnSave.Click += new EventHandler(btnSave_Click);
                mainbar.btnExit.Click += new EventHandler(btnExit_Click);

                mainbar.lblCount.Visible = false;
                mainbar.btnAdd.Visible = false;
                mainbar.btnEdit.Visible = false;
                mainbar.btnDelete.Visible = false;
                mainbar.btnCancel.Visible = false;
                mainbar.btnFind.Visible = false;
                mainbar.btnFirst.Visible = false;
                mainbar.btnPrevious.Visible = false;
                mainbar.btnNext.Visible = false;
                mainbar.btnLast.Visible = false;

                mainbar.btnSave.Enabled = true;

                PublicClass.bindMandoub(cmbemployeeID);
                cmbemployeeID.SelectedIndex = 0;

                getData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                documentTransTaskDetail val1 = (from c in db.documentTransTaskDetails
                                                where c.documentTransTaskDetailsID == _documentTransTaskDetailsID
                                                select c).FirstOrDefault();

                if (val1.documentTransSetp_endDate.HasValue)
                    txtdocumentTransSetp_endDate.Text = val1.documentTransSetp_endDate.Value.ToShortDateString();

                if (val1.documentTransSetp_startDate.HasValue)
                    txtdocumentTransSetp_startDate.Text = val1.documentTransSetp_startDate.Value.ToShortDateString();

                txtdocumentTransSetp_name.Text = val1.documentTransSetp_name;
                txtdocumentTransSetp_Period.Text = val1.documentTransSetp_Period.Value.ToString();
                txtdocumentTransSetp_Remarks.Text = val1.documentTransSetp_Remarks;
                chkdocumentTransSetp_isClose.Checked = val1.documentTransSetp_isClose.Value;

                for (int i = 0; i < cmbemployeeID.Items.Count; i++)
                {
                    if (cmbemployeeID.Items[i].Value.ToString() == val1.employeeID.ToString())
                    {
                        cmbemployeeID.SelectedIndex = i;
                        break;
                    }
                }
                
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    using (TransactionScope ts = new TransactionScope())
                    {

                        documentTransTaskDetail val1 = (from c in db.documentTransTaskDetails
                                                        where c.documentTransTaskDetailsID == _documentTransTaskDetailsID
                                                        select c).FirstOrDefault();


                        DateTime bb = DateTime.Now;
                        if (DateTime.TryParse(txtdocumentTransSetp_endDate.Text, out bb))
                        {
                            val1.documentTransSetp_endDate = bb;
                        }
                        if (DateTime.TryParse(txtdocumentTransSetp_startDate.Text, out bb))
                        {
                            val1.documentTransSetp_startDate = bb;
                        }

                        val1.documentTransSetp_name = txtdocumentTransSetp_name.Text;
                        val1.documentTransSetp_Period = int.Parse(txtdocumentTransSetp_Period.Text);
                        val1.documentTransSetp_Remarks = txtdocumentTransSetp_Remarks.Text;
                        val1.documentTransSetp_isClose = chkdocumentTransSetp_isClose.Checked;

                        if (cmbemployeeID.SelectedValue != null)
                            val1.employeeID = int.Parse(cmbemployeeID.SelectedValue.ToString());

                        db.SaveChanges();

                        int IDD = val1.documentTransTaskID.Value;
                        if (chkdocumentTransSetp_isClose.Checked && isLastTask)
                        {
                            // we must close the task for ever
                            DocumentTransactionTask val2 = (from c in db.DocumentTransactionTasks
                                                            where c.documentTransTaskID == IDD
                                                            select c).FirstOrDefault();
                            val2.documentTransTask_isClose = true;
                            db.SaveChanges();
                        }

                        db.AcceptAllChanges();
                        ts.Complete();
                        mainbar.btnSave.Enabled = false;
                        MessageBox.Show("تم الحفظ");

                    }
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

        void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
