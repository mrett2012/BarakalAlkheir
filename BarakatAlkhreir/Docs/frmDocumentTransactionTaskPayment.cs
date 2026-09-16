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
    public partial class frmDocumentTransactionTaskPayment : Telerik.WinControls.UI.RadForm
    {
        public int _documentTransTaskID;
        public double _documentTrans_Amount;
        public bool paid = false;
        public bool isAlreadyApproved = false;

        List<DocumentTransactionTasksPayment> lstDocumentTransactionTasksPayment = null;
        components.banner mainbar = new components.banner("frmDocumentTransactionTaskPayment");



        public frmDocumentTransactionTaskPayment()
        {
            InitializeComponent();
        }

        private void frmDocumentTransactionTaskPayment_Load(object sender, EventArgs e)
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

                txtvoucherAmount.Text = _documentTrans_Amount.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                if (isAlreadyApproved == false)
                {

                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            DocumentTransactionTasksPayment bb = new DocumentTransactionTasksPayment();
                            bb.documentTransTaskID = _documentTransTaskID;
                            bb.voucherNo = txtvoucherNo.Text;
                            bb.voucherAmount = float.Parse(txtvoucherAmount.Text);

                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.DocumentTransactionTasksPayments.AddObject(bb);
                            rowsAffected = db.SaveChanges();
                            if (rowsAffected > 0)
                            {
                                DocumentTransactionTask val1 = (from c in db.DocumentTransactionTasks
                                                                where c.documentTransTaskID == _documentTransTaskID
                                                                select c).FirstOrDefault();
                                val1.documentTransTask_Approved = true;
                                val1.documentTransTask_ApprovedDate = DateTime.Now;
                                db.SaveChanges();
                                // we must ensure that the task has at least one task
                                // asssign the tasks 
                                // implement Transaction Task Details

                                var val2 = (from c in db.DocumentTransactionSteps
                                            where c.documentTransID == val1.documentTransID
                                            select c).ToList();
                                if (val2.Count > 0)
                                {
                                    // proceed
                                    foreach (var item in val2)
                                    {
                                        documentTransTaskDetail ass = new documentTransTaskDetail();
                                        ass.employeeID = -1;
                                        ass.documentTransTaskID = _documentTransTaskID;
                                        ass.documentTransSetp_startDate = DateTime.Now;
                                        ass.documentTransSetp_name = item.documentTransSetp_name;
                                        ass.documentTransSetp_Period = item.documentTransSetp_Period;
                                        ass.documentTransSetp_pCode = item.documentTransSetp_pCode;
                                        ass.documentTransSetp_Remarks = "";
                                        ass.documentTransSetp_isClose = false;
                                        ass.added_Date = DateTime.Now;
                                        ass.addedBy = MainClass.userID;
                                        ass.edited_Date = DateTime.Now;
                                        ass.editedBy = MainClass.userID;

                                        db.documentTransTaskDetails.AddObject(ass);
                                        db.SaveChanges();
                                    }


                                    DocumentTransactionTask val3 = (from c in db.DocumentTransactionTasks
                                                                    where c.documentTransTaskID == _documentTransTaskID
                                                                    select c).FirstOrDefault();
                                    val3.documentTransTask_isPaid = true;
                                    val3.documentTransTask_Approved = true;
                                    val3.documentTransTask_ApprovedDate = DateTime.Now;
                                    val3.edited_Date = DateTime.Now;
                                    val3.editedBy = MainClass.userID;

                                    db.SaveChanges();

                                    db.AcceptAllChanges();
                                    ts.Complete();
                                    MessageBox.Show("تم تجديد المراحل");
                                    mainbar.btnSave.Enabled = false;
                                    paid = true;
                                }
                                else
                                {
                                    MessageBox.Show("يجب ان يكون هناك مرحلة واحدة على الأقل");
                                    ts.Dispose();
                                }




                            }
                        }
                    }
                }
                else
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            DocumentTransactionTasksPayment bb = new DocumentTransactionTasksPayment();
                            bb.documentTransTaskID = _documentTransTaskID;
                            bb.voucherNo = txtvoucherNo.Text;
                            bb.voucherAmount = float.Parse(txtvoucherAmount.Text);

                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.DocumentTransactionTasksPayments.AddObject(bb);
                            rowsAffected = db.SaveChanges();
                            if (rowsAffected > 0)
                            {
                                DocumentTransactionTask val1 = (from c in db.DocumentTransactionTasks
                                                                where c.documentTransTaskID == _documentTransTaskID
                                                                select c).FirstOrDefault();
                                val1.documentTransTask_isPaid = true;
                                val1.edited_Date = DateTime.Now;
                                db.SaveChanges();
                                db.AcceptAllChanges();
                                ts.Complete();
                                mainbar.btnSave.Enabled = false;

                            }
                            else
                            {
                                ts.Dispose();
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
