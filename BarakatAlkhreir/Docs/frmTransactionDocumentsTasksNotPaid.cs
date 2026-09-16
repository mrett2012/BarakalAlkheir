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
    public partial class frmTransactionDocumentsTasksNotPaid : Telerik.WinControls.UI.RadForm
    {
        public frmTransactionDocumentsTasksNotPaid()
        {
            InitializeComponent();
        }

        private void frmEmployeeExpiryDocuments_Load(object sender, EventArgs e)
        {
            try
            {
                getData1();
                getData();
                getData2();
                
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

                var val = from c3 in db.DocumentTypeDefinitions
                          join c4 in db.DocumentDefs
                          on c3.documentTypeDefID equals c4.documentTypeDefID
                          join c1 in db.Documents
                          on c4.documentDefID equals c1.documentDefID
                          join c6 in db.CompanyLicences
                          on c1.document_TypeID equals c6.companyLicencesID
                          join cc in db.DocumentTransactionTasks on c1.documentID equals cc.documentID
                          join cc1 in db.DocumentTransactions on cc.documentTransID equals cc1.documentTransID
                          where c3.documentType_Code == "02" &&
                          c1.document_endDate != null &&
                          c6.isFack == false &&
                          c6.isActive == true &&
                          cc.documentTransTask_Approved ==true &&
                          cc.documentTransTask_isPaid==false
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
                              cc.documentTransTaskID,
                              c6.commercialName,
                              noOfDays = c4.documentDef_WarningDays.Value,
                              cc1.documentTrans_Amount,
                              cc1.documentTransID
                          };

                var val1 = val.ToList();
                
                foreach (var item in val1)
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
                    GView.Rows[GView.Rows.Count - 1].Cells[8].Value = item.document_endDate.Value.ToShortDateString();
                    GView.Rows[GView.Rows.Count - 1].Cells[9].Value = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    GView.Rows[GView.Rows.Count - 1].Cells[10].Value = getDocumentTransaction(item.documentID);
                    GView.Rows[GView.Rows.Count - 1].Cells[11].Value = item.documentTrans_Amount.Value.ToString();
                    GView.Rows[GView.Rows.Count - 1].Cells[13].Value = item.documentTransTaskID.ToString();
                    if (isHasRemarks(item.companyLicencesID))
                    {
                        GView.CurrentView.CurrentRow.DrawFill = true;
                        //GView.CurrentView.CurrentRow.BackColor = Color.Red;
                        
                    }
                    if (isHasRental(item.companyLicencesID))
                    {
                        GView.CurrentView.CurrentRow.DrawFill = true;
                        //GView.CurrentView.CurrentRow.BackColor = Color.Cyan;
                        
                    }

                }
            }


        }

        private bool isHasRental(int _companyLicencesID)
        {
            bool temp = false;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.RentLicenses
                           where (c.companyLicencesID == _companyLicencesID)
                           join d in db.RentLicensesDetails on c.rentLicenseID equals d.rentLicenseID
                           where d.rd_fromDate < DateTime.Now
                           && d.rd_isPaid == false
                           select c).ToList();
                if (val.Count > 0)
                {
                    temp = true;
                }

            }
            return temp;
        }

        private void getData1()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = from c3 in db.DocumentTypeDefinitions
                          join c4 in db.DocumentDefs
                          on c3.documentTypeDefID equals c4.documentTypeDefID
                          join c1 in db.Documents
                          on c4.documentDefID equals c1.documentDefID
                          join c2 in db.Employees
                          on c1.document_TypeID equals c2.employeeID
                          //join cc11 in db.EmployeeSponsors on c2.employeeID equals cc11.employeeID
                          //join c6 in db.CompanyLicences
                          //on cc11.companyLicencesID equals c6.companyLicencesID
                          join c5 in db.EmployeeLicenseShops on c2.employeeID equals c5.employeeID
                          join cc5 in db.LicensesShops on c5.licencesShopID equals cc5.licencesShopID
                          join c6 in db.CompanyLicences on cc5.companyLicencesID equals c6.companyLicencesID
                          join cc in db.DocumentTransactionTasks on c1.documentID equals cc.documentID
                          join cc1 in db.DocumentTransactions on cc.documentTransID equals cc1.documentTransID
                          where c3.documentType_Code == "01" &&
                          c1.document_endDate != null &&
                          cc.documentTransTask_Approved == true &&
                          cc.documentTransTask_isPaid == false 
                          orderby c1.document_endDate  
                          select new
                          {
                              c3.documentTypeDefID,
                              c4.documentDefID,
                              c6.companyLicencesID,
                              c1.documentID,
                              c2.employeeID,
                              c3.documentType_Name,
                              cc.documentTransTaskID,
                              c4.documentDef_Name,

                              c1.document_endDate,

                              c2.emp_namefullar,
                              c2.emp_No,

                              c6.commercialName,
                              noOfDays = c4.documentDef_WarningDays.Value,
                              cc1.documentTrans_Amount,
                              cc1.documentTransID
                          };
                //BindingSource bs = new BindingSource();
                //bs.Add(val.ToList().Where(p => p.document_endDate <= DateTime.Now.AddDays(p.noOfDays)));
                var val1 = val.ToList();
                //GView1.DataSource = val1.ToList();

                foreach (var item in val1)
                {
                    GView.Rows.AddNew();
                    GView.Rows[GView.Rows.Count - 1].Cells[0].Value = item.documentTypeDefID;
                    GView.Rows[GView.Rows.Count - 1].Cells[1].Value = item.documentDefID;
                    GView.Rows[GView.Rows.Count - 1].Cells[2].Value = item.companyLicencesID;
                    GView.Rows[GView.Rows.Count - 1].Cells[3].Value = item.documentID;
                    //GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.employeeID;

                    GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.documentDef_Name;
                    GView.Rows[GView.Rows.Count - 1].Cells[5].Value = item.emp_No;
                    GView.Rows[GView.Rows.Count - 1].Cells[6].Value = item.emp_namefullar;
                    GView.Rows[GView.Rows.Count - 1].Cells[7].Value = item.commercialName;
                    GView.Rows[GView.Rows.Count - 1].Cells[8].Value = item.document_endDate.Value.ToShortDateString();
                    GView.Rows[GView.Rows.Count - 1].Cells[9].Value = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    GView.Rows[GView.Rows.Count - 1].Cells[10].Value = getDocumentTransaction(item.documentID);
                    GView.Rows[GView.Rows.Count - 1].Cells[11].Value = item.documentTrans_Amount.Value.ToString();
                    GView.Rows[GView.Rows.Count - 1].Cells[13].Value = item.documentTransTaskID.ToString();
                    if (isHasRemarks(item.companyLicencesID))
                    {
                        GView.CurrentView.CurrentRow.DrawFill = true;
                        //GView.CurrentView.CurrentRow.BackColor = Color.Red;

                    }
                    if (isHasRental(item.companyLicencesID))
                    {
                        GView.CurrentView.CurrentRow.DrawFill = true;
                        //GView.CurrentView.CurrentRow.BackColor = Color.Cyan;

                    }
                }
            }


        }

        private void getData2()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = from c3 in db.DocumentTypeDefinitions
                          join c4 in db.DocumentDefs
                          on c3.documentTypeDefID equals c4.documentTypeDefID
                          join c1 in db.Documents
                          on c4.documentDefID equals c1.documentDefID
                          join c6 in db.Cars
                          on c1.document_TypeID equals c6.carID
                          join cc in db.DocumentTransactionTasks on c1.documentID equals cc.documentID
                          join cc1 in db.DocumentTransactions on cc.documentTransID equals cc1.documentTransID
                          where c3.documentType_Code == "04" &&
                          c1.document_endDate != null &&
                          cc.documentTransTask_Approved == true &&
                          cc.documentTransTask_isPaid == false &&
                          cc.documentTransTask_isClose == false
                          orderby c1.document_endDate
                          select new
                          {
                              c3.documentTypeDefID,
                              c4.documentDefID,
                              c6.carID,
                              c1.documentID,
                              //c2.employeeID,
                              c3.documentType_Name,
                              c4.documentDef_Name,
                              c1.document_endDate,
                              c6.car_No,
                              c6.car_Name,

                              c6.car_Model,
                              noOfDays = c4.documentDef_WarningDays.Value,
                              cc.documentTransTaskID,
                              cc1.documentTrans_Amount,
                              cc1.documentTransID
                          };

                var val1 = val.ToList().Where(p => p.document_endDate <= DateTime.Now.AddDays(p.noOfDays));


                foreach (var item in val1)
                {
                    GView.Rows.AddNew();
                    GView.Rows[GView.Rows.Count - 1].Cells[0].Value = item.documentTypeDefID;
                    GView.Rows[GView.Rows.Count - 1].Cells[1].Value = item.documentDefID;
                    GView.Rows[GView.Rows.Count - 1].Cells[2].Value = item.carID;
                    GView.Rows[GView.Rows.Count - 1].Cells[3].Value = item.documentID;
                    //GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.employeeID;

                    GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.documentDef_Name;
                    GView.Rows[GView.Rows.Count - 1].Cells[5].Value = item.car_No;
                    GView.Rows[GView.Rows.Count - 1].Cells[6].Value = item.car_Model;
                    GView.Rows[GView.Rows.Count - 1].Cells[7].Value = item.car_Name;
                    GView.Rows[GView.Rows.Count - 1].Cells[8].Value = item.document_endDate.Value.ToShortDateString();
                    GView.Rows[GView.Rows.Count - 1].Cells[9].Value = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    GView.Rows[GView.Rows.Count - 1].Cells[10].Value = getDocumentTransaction(item.documentID);

                    GView.Rows[GView.Rows.Count - 1].Cells[11].Value = item.documentTrans_Amount.Value.ToString();
                    GView.Rows[GView.Rows.Count - 1].Cells[13].Value = item.documentTransTaskID.ToString();
                    //if (isHasRemarks(item.companyLicencesID))
                    //{
                    //    GView.CurrentView.CurrentRow.DrawFill = true;
                    //    GView.CurrentView.CurrentRow.BackColor = Color.Red;
                    //    GView.Rows[GView.Rows.Count - 1].Cells["gvRemarks"].Value = "ملاحظة";
                    //}

                    //if (isHasRental(item.companyLicencesID))
                    //{
                    //    GView.CurrentView.CurrentRow.DrawFill = true;
                    //    GView.CurrentView.CurrentRow.BackColor = Color.Cyan;
                    //    GView.Rows[GView.Rows.Count - 1].Cells["gvRemarks"].Value += "  ايجار";

                    //}
                }
            }


        }


        private string getDocumentTransaction(int docID)
        {
            string temp = "";

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.DocumentTransactionTasks
                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                           where c.documentID == docID
                           select new
                           {
                               c2.transDef_namea
                           }).ToList();

                foreach (var item in val)
                {
                    temp +="-"+ item.transDef_namea ;
                }
            }

            return temp;
        }

        private bool isHasRemarks(int lID)
        {
            bool temp = false;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.CompanyLicencesRemarks
                           where c.companyLicencesID == lID &&
                           c.comLicenseRem_isActive == true
                           select c).ToList();
                if (val.Count > 0)
                {
                    return true;
                }
            }

            return temp;
        }

        private void GView_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    if (GView.CurrentView.CurrentRow.BackColor == Color.Red)
            //    {
            //        MessageBox.Show("الرخصة فيها مشكلة");
            //    }
            //    else
            //    {
            //        int IDD = int.Parse(GView.CurrentRow.Cells[3].Value.ToString());

            //        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            //        {
            //            var val = (from c in db.DocumentTransactions
            //                       join c1 in db.DocumentDefs on c.documentDefID equals c1.documentDefID
            //                       join c2 in db.TransactionDefs on c.transDefID equals c2.transDefID
            //                       join c3 in db.Documents on c1.documentDefID equals c3.documentDefID
            //                       where c3.documentID == IDD
            //                       select new
            //                       {
            //                           c2.transDef_namea,
            //                           c.documentTrans_Amount,
            //                           c.documentTransID,
            //                           c3.documentID
            //                       }).ToList();

            //            ContextMenu m = new ContextMenu();
            //            foreach (var item in val)
            //            {
            //                MenuItem i1 = new MenuItem(item.transDef_namea);
            //                i1.Name = item.documentTransID.ToString() + "," + item.documentID;
            //                i1.Click += new EventHandler(i1_Click);
            //                m.MenuItems.Add(i1);
            //            }

            //            m.Show(GView, new Point(e.X, e.Y));
            //        }


            //    }
            //}
        }

        void i1_Click(object sender, EventArgs e)
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
                                        c.documentID == DID
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
                                dd.documentTransTask_Approved = false;
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

                                db.AcceptAllChanges();
                                ts.Complete();

                            }
                            // we must check if the order already there for that documents
                            // we must add record to document transaction tasks
                            //MessageBox.Show(name);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void GView_Click(object sender, EventArgs e)
        {
            try
            {
                if (GView.CurrentCell != null)
                {
                    if (GView.CurrentCell.ColumnInfo.Name == "GVCommand")
                    {
                        int _documentTransTaskID = int.Parse(GView.CurrentRow.Cells["gvdocumentTransTaskID"].Value.ToString());
                        int _GVAmount = int.Parse(GView.CurrentRow.Cells["GVAmount"].Value.ToString());

                        Docs.frmDocumentTransactionTaskPayment frm = new frmDocumentTransactionTaskPayment();
                        frm._documentTransTaskID = _documentTransTaskID;
                        frm._documentTrans_Amount = _GVAmount;
                        frm.isAlreadyApproved = true;
                        frm.ShowDialog();
                        GView.Rows.Clear();
                        frmEmployeeExpiryDocuments_Load(sender, e);
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
 