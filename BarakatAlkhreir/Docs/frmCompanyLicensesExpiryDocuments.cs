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
    public partial class frmCompanyLicensesExpiryDocuments : Telerik.WinControls.UI.RadForm
    {
        bool action = false;
        bool addExcemption = false;

        public frmCompanyLicensesExpiryDocuments()
        {
            InitializeComponent();
            DocumentListLookup.ConfigureGrid(GView);
        }

        private void frmEmployeeExpiryDocuments_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmEmployeeExpiryDocuments_LoadData);
        }

        private async System.Threading.Tasks.Task frmEmployeeExpiryDocuments_LoadData(object sender, EventArgs e)
        {
            try
            {
                GView.Rows.Clear();
                await getDataAsync();
                if (!MainClass.isSuperUser)
                {
                    if ((await BarakalAlkheir.components.FormLoading.ReadAsync(this, () => MainFile.isDeleteSecurity("btnCancelDocumentTask"))))
                    {
                        GView.Columns["gvCancel"].IsVisible = true;
                    }
                    else
                    {
                        GView.Columns["gvCancel"].IsVisible = false;
                    }

                    if ((await BarakalAlkheir.components.FormLoading.ReadAsync(this, () => MainFile.isAddSecurity("btnAddExceptionAction"))))
                    {
                        addExcemption = true;
                    }
                    else
                    {
                        addExcemption = false;
                    }
                    if ((await BarakalAlkheir.components.FormLoading.ReadAsync(this, () => MainFile.isAddSecurity("btnAddDocumentTask"))))
                    {
                        action = true;
                    }
                    else
                    {
                        action = false;
                    }


                }
                else
                {
                    action = true;
                    addExcemption = true;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getData()
        {
            // Preserve synchronous refreshes used by existing write handlers.
            getDataAsync(false).GetAwaiter().GetResult();
        }

        private async System.Threading.Tasks.Task getDataAsync(bool background = true)
        {
            await BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = from c3 in db.DocumentTypeDefinitions
                              join c4 in db.DocumentDefs
                              on c3.documentTypeDefID equals c4.documentTypeDefID
                              join c1 in db.Documents
                              on c4.documentDefID equals c1.documentDefID
                              join c6 in db.CompanyLicences
                              on c1.document_TypeID equals c6.companyLicencesID
                              where c3.documentType_Code == "02" &&
                              c1.document_endDate != null &&
                              c6.isFack == false &&
                              c6.isActive == true
                              orderby c1.document_endDate
                              select new
                              {
                                  c3.documentTypeDefID,
                                  c4.documentDefID,
                                  c6.companyLicencesID,
                                  c1.documentID,
                                  //c2.employeeID,
                                  c3.documentType_Name,
                                  c4.documentDef_Name,
                                  c1.document_endDate,
                                  c6.licenseUniqueNo,
                                  c6.licenseNo,

                                  c6.commercialName,
                                  noOfDays = (c4.documentDef_WarningDays ?? 0)
                              };

                    DateTime now = DateTime.Now;
                    var val1 = val.Where(p => p.document_endDate <= System.Data.Objects.EntityFunctions.AddDays(now, p.noOfDays)).ToList();
                    return DocumentListLookup.Create(db, val1, item => item.documentID, item => item.companyLicencesID, true);
                }
            }, loaded =>
            {
                GView.BeginUpdate();
                try
                {
                    foreach (var item in loaded.Rows)
                    {
                        GView.Rows.AddNew();
                        GView.Rows[GView.Rows.Count - 1].Cells[0].Value = item.documentTypeDefID;
                        GView.Rows[GView.Rows.Count - 1].Cells[1].Value = item.documentDefID;
                        GView.Rows[GView.Rows.Count - 1].Cells[2].Value = item.companyLicencesID;
                        GView.Rows[GView.Rows.Count - 1].Cells[3].Value = item.documentID;
                        //GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.employeeID;

                        GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.documentDef_Name;
                        GView.Rows[GView.Rows.Count - 1].Cells[5].Value = item.licenseNo;
                        GView.Rows[GView.Rows.Count - 1].Cells[6].Value = item.licenseUniqueNo;
                        GView.Rows[GView.Rows.Count - 1].Cells[7].Value = item.commercialName;
                        GView.Rows[GView.Rows.Count - 1].Cells[8].Value = item.document_endDate.HasValue ? item.document_endDate.Value.ToShortDateString() : string.Empty;
                        GView.Rows[GView.Rows.Count - 1].Cells[9].Value = item.document_endDate.HasValue ? (object)item.document_endDate.Value.Subtract(DateTime.Now).Days : null;
                        GView.Rows[GView.Rows.Count - 1].Cells[10].Value = loaded.Lookup.Transactions(item.documentID);
                        if (loaded.Lookup.HasRemarks(item.companyLicencesID))
                        {
                            GView.Rows[GView.Rows.Count - 1].Tag = true;
                            //GView.CurrentView.CurrentRow.BackColor = Color.Red;
                            GView.Rows[GView.Rows.Count - 1].Cells["gvRemarks"].Value = "ملاحظة";
                        }

                        if (loaded.Lookup.HasRental(item.companyLicencesID))
                        {
                            GView.Rows[GView.Rows.Count - 1].Tag = true;
                            //GView.CurrentView.CurrentRow.BackColor = Color.Cyan;
                            GView.Rows[GView.Rows.Count - 1].Cells["gvRemarks"].Value += "  ايجار";

                        }
                    }
                }
                finally { GView.EndUpdate(); }
            }, background);
        }








        bool isPressed = false;
        private void GView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == System.Windows.Forms.MouseButtons.Right && action)
                {
                    if (GView.CurrentRow.Cells["gvRemarks"].Value != null && 
                        GView.CurrentRow.Cells["gvRemarks"].Value.ToString().Length > 1 && isPressed == false)
                    {
                        MessageBox.Show("الرخصة فيها مشكلة أو عليها اجار محل");
                    }
                    //else if (GView.CurrentView.CurrentRow.BackColor == Color.Cyan && isPressed == false)
                    //{
                    //    MessageBox.Show("الرخصة عليها ايجار");
                    //}
                    else if (addExcemption || (action && isPressed == false))
                    {
                        int IDD = int.Parse(GView.CurrentRow.Cells[3].Value.ToString());

                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            var val = (from c in db.DocumentTransactions
                                       join c1 in db.DocumentDefs on c.documentDefID equals c1.documentDefID
                                       join c2 in db.TransactionDefs on c.transDefID equals c2.transDefID
                                       join c3 in db.Documents on c1.documentDefID equals c3.documentDefID
                                       where c3.documentID == IDD
                                       select new
                                       {
                                           c2.transDef_namea,
                                           c.documentTrans_Amount,
                                           c.documentTransID,
                                           c3.documentID
                                       }).ToList();

                            ContextMenu m = new ContextMenu();
                            foreach (var item in val)
                            {
                                MenuItem i1 = new MenuItem(item.transDef_namea);
                                i1.Name = item.documentTransID.ToString() + "," + item.documentID;
                                i1.Click += new EventHandler(i1_Click);
                                m.MenuItems.Add(i1);
                            }

                            m.Show(GView, new Point(e.X, e.Y));
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async void i1_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem m = (MenuItem)sender;
                string name = m.Text;
                char[] delimiters = new char[] { ',' };
                int IDD = int.Parse(m.Name.Split(delimiters, 2)[0]);
                int DID = int.Parse(m.Name.Split(delimiters, 2)[1]);

                DialogResult dr = MessageBox.Show("هل تريد " + name + " ?", "Confirmation Of Action", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {

                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            var val1 = (from c in db.DocumentTransactionTasks
                                        where c.documentTransID == IDD &&
                                        c.documentID == DID &&
                                        c.documentTransTask_isClose == false
                                        select c).ToList();
                            if (val1.Count > 0)
                            {
                                MessageBox.Show("العملية موجودة");
                            }
                            else
                            {
                                DocumentTransactionTask dd = new DocumentTransactionTask();
                                dd.documentTransID = IDD;
                                dd.documentID = DID;
                                dd.documentTransTask_Approved = true;
                                dd.documentTransTask_isPaid = false;
                                dd.documentTransTask_ApprovedDate = DateTime.Now;
                                dd.documentTransTask_date = DateTime.Now;
                                dd.documentTransTask_isClose = false;
                                dd.documentTransTask_NoOfDays = 0;
                                dd.added_Date = DateTime.Now;
                                dd.addedBy = MainClass.userID;
                                dd.edited_Date = DateTime.Now;
                                dd.editedBy = MainClass.userID;

                                db.DocumentTransactionTasks.AddObject(dd);

                                int rowsAffected = db.SaveChanges();

                                if (rowsAffected > 0 && isPressed && GView.CurrentRow.Cells["gvRemarks"].Value != null &&
                                    GView.CurrentRow.Cells["gvRemarks"].Value.ToString().Length > 1)
                                {
                                    int _documentTransTaskID = dd.documentTransTaskID;
                                    DocumentTransactionTask val11 = (from c in db.DocumentTransactionTasks
                                                                     where c.documentTransTaskID == _documentTransTaskID
                                                                     select c).FirstOrDefault();
                                    val11.documentTransTask_Approved = true;
                                    val11.documentTransTask_ApprovedDate = DateTime.Now;
                                    db.SaveChanges();
                                    // we must ensure that the task has at least one task
                                    // asssign the tasks 
                                    // implement Transaction Task Details

                                    //var val2 = (from c in db.DocumentTransactionSteps
                                    //            where c.documentTransID == val11.documentTransID
                                    //            select c).ToList();
                                    //if (val2.Count > 0)
                                    //{
                                    //    // proceed
                                    //    foreach (var item in val2)
                                    //    {
                                    //        documentTransTaskDetail ass = new documentTransTaskDetail();
                                    //        ass.employeeID = -1;
                                    //        ass.documentTransTaskID = _documentTransTaskID;
                                    //        ass.documentTransSetp_startDate = DateTime.Now;
                                    //        ass.documentTransSetp_name = item.documentTransSetp_name;
                                    //        ass.documentTransSetp_Period = item.documentTransSetp_Period;
                                    //        ass.documentTransSetp_pCode = item.documentTransSetp_pCode;
                                    //        ass.documentTransSetp_Remarks = "";
                                    //        ass.documentTransSetp_isClose = false;
                                    //        ass.added_Date = DateTime.Now;
                                    //        ass.addedBy = MainClass.userID;
                                    //        ass.edited_Date = DateTime.Now;
                                    //        ass.editedBy = MainClass.userID;

                                    //        db.documentTransTaskDetails.AddObject(ass);
                                    //        db.SaveChanges();
                                    //    }


                                    //    DocumentTransactionTask val3 = (from c in db.DocumentTransactionTasks
                                    //                                    where c.documentTransTaskID == _documentTransTaskID
                                    //                                    select c).FirstOrDefault();
                                    //    val3.documentTransTask_isPaid = false;
                                    //    val3.documentTransTask_Approved = true;
                                    //    val3.documentTransTask_ApprovedDate = DateTime.Now;
                                    //    val3.edited_Date = DateTime.Now;
                                    //    val3.editedBy = MainClass.userID;

                                    //    db.SaveChanges();

                                    //    isPressed = false;
                                    //    MessageBox.Show("تم تحديد المراحل");
                                    //    MessageBox.Show("تم ترحيل العملية بنجاح");

                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("يجب ان يكون هناك مرحلة واحدة على الأقل");
                                    //    ts.Dispose();
                                    //    isPressed = false;
                                    //    return;
                                    //}

                                }

                                db.AcceptAllChanges();

                                ts.Complete();
                                //MessageBox.Show(ts.ToString());
                            }
                            // we must check if the order already there for that documents
                            // we must add record to document transaction tasks
                            //MessageBox.Show(name);

                        }

                    }

                    GView.Rows.Clear();

                    await BarakalAlkheir.components.FormLoading.RunAsync(this, () => frmEmployeeExpiryDocuments_LoadData(sender, e));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
            {
                isPressed = true;
            }
        }

        private void GView_KeyUp(object sender, KeyEventArgs e)
        {
            isPressed = false;
        }

        private bool isDocumentTransactionStepsStart(int docID)
        {
            bool temp = false;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.DocumentTransactionTasks
                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                           join c3 in db.documentTransTaskDetails on c.documentTransTaskID equals c3.documentTransTaskID
                           where c.documentID == docID &&
                           c3.documentTransSetp_isClose == true
                           select new
                           {
                               c2.transDef_namea
                           }).ToList();

                if (val.Count > 0)
                {
                    temp = true;
                }
            }

            return temp;
        }

        private bool isDocumentTransactionPaid(int docID)
        {
            bool temp = false;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.DocumentTransactionTasks
                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                           join c3 in db.DocumentTransactionTasksPayments on c.documentTransTaskID equals c3.documentTransTaskID
                           where c.documentID == docID
                           select new
                           {
                               c2.transDef_namea
                           }).ToList();

                if (val.Count > 0)
                {
                    temp = true;
                }
            }

            return temp;
        }

        private void GView_Click(object sender, EventArgs e)
        {
            try
            {
                isPressed = false;
                if (GView.Rows.Count > 0)
                {
                    if (GView.CurrentColumn.Name == "gvCancel")
                    {
                        // TODO: check the security 
                        // check if task has been initialise
                        // check if any of the task details has been close
                        // TODO: delete the task and the task details
                        // Check Transaction Task Payment
                        int docID = int.Parse(GView.CurrentRow.Cells["gvdocumentID"].Value.ToString());
                        if (!isDocumentTransactionStepsStart(docID))
                        {
                            if (!isDocumentTransactionPaid(docID))
                            {
                                if (GView.CurrentRow.Cells["gvActionTaken"].Value.ToString().Length > 0)
                                {
                                    DialogResult dr = MessageBox.Show("هل تريد حذف الاجراء", "Confirmation of Deletion", MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                                    {
                                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                                        {
                                            using (TransactionScope ts = new TransactionScope())
                                            {
                                                var val = (from c in db.DocumentTransactionTasks
                                                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                                                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                                                           join c3 in db.documentTransTaskDetails on c.documentTransTaskID equals c3.documentTransTaskID
                                                           where c.documentID == docID
                                                           select new
                                                           {
                                                               c3.documentTransTaskDetailsID
                                                           }).ToList();


                                                foreach (var item in val)
                                                {

                                                    int dID = item.documentTransTaskDetailsID;
                                                    documentTransTaskDetail dd = (from cc3 in db.documentTransTaskDetails
                                                                                  where cc3.documentTransTaskDetailsID == dID
                                                                                  select cc3).FirstOrDefault();
                                                    if (dd != null)
                                                        db.documentTransTaskDetails.DeleteObject(dd);
                                                    db.SaveChanges();
                                                }

                                                var val1 = (from c in db.DocumentTransactionTasks
                                                            join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                                                            join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                                                            join c3 in db.DocumentTransactionTasksPayments on c.documentTransTaskID equals c3.documentTransTaskID
                                                            where c.documentID == docID
                                                            select new
                                                            {
                                                                c3.DocumentTransactionTasksPaymentID
                                                            }).ToList();

                                                foreach (var item in val1)
                                                {

                                                    int pID = item.DocumentTransactionTasksPaymentID;
                                                    DocumentTransactionTasksPayment dd = (from cc2 in db.DocumentTransactionTasksPayments
                                                                                          where cc2.DocumentTransactionTasksPaymentID == pID
                                                                                          select cc2).FirstOrDefault();
                                                    if (dd != null)
                                                        db.DocumentTransactionTasksPayments.DeleteObject(dd);
                                                    db.SaveChanges();
                                                }


                                                DocumentTransactionTask dtt = (from cc1 in db.DocumentTransactionTasks
                                                                               where cc1.documentID == docID
                                                                               select cc1).SingleOrDefault();
                                                if (dtt != null)
                                                    db.DocumentTransactionTasks.DeleteObject(dtt);
                                                db.SaveChanges();

                                                db.AcceptAllChanges();
                                                ts.Complete();

                                                MessageBox.Show("تم الغاء العملية بنجاح");

                                            }
                                        }

                                        GView.Rows.Clear();
                                        getData();
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("Doesn't have task");
                                }
                            } // end of if paid
                            else
                            {
                                MessageBox.Show("تم دفع مبلغ الاجرائ ولا يمكن الغاء العملية");
                            }
                        }// end of document
                        else
                        {
                            MessageBox.Show("مراحل الاجراء بدأت , ولا يمكن الغاءها");
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
 