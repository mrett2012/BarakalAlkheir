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
    public partial class frmDocMainPage : Telerik.WinControls.UI.RadForm
    {
        public frmDocMainPage()
        {
            InitializeComponent();
        }

        private void btnEmployeeExpiryDocument_Click(object sender, EventArgs e)
        {
            Docs.frmEmployeeExpiryDocuments frm = new Docs.frmEmployeeExpiryDocuments();
            frm.ShowDialog();
        }

        private void btnCompanyLicensesExpiryDocument_Click(object sender, EventArgs e)
        {
            Docs.frmCompanyLicensesExpiryDocuments frm = new Docs.frmCompanyLicensesExpiryDocuments();
            frm.ShowDialog();
        }

        private void btnTransactionTasks_Click(object sender, EventArgs e)
        {
            Docs.frmTransactionDocumentsTasks frm = new Docs.frmTransactionDocumentsTasks();
            frm.ShowDialog();
        }

        private void btnTransactionFollowSteps_Click(object sender, EventArgs e)
        {
            Docs.frmTransactionDocumentsTasksSteps frm = new Docs.frmTransactionDocumentsTasksSteps();
            frm.ShowDialog();
        }

        private void btnDocumentTransactionMustPay_Click(object sender, EventArgs e)
        {
            Docs.frmTransactionDocumentsTasksNotPaid frm = new frmTransactionDocumentsTasksNotPaid();
            frm.ShowDialog();
        }

        private void frmDocMainPage_Load(object sender, EventArgs e)
        {
            try
            {
                txtDate1.Text = DateTime.Now.ToShortDateString();
                txtdatefrom.Text = DateTime.Now.ToShortDateString();
                txtdateto.Text = DateTime.Now.ToShortDateString();

                //txtDate2.Text = DateTime.Now.ToShortDateString();
                PublicClass.bindDocumentTypeDef(cmbDocumentTypeDefinition);
                cmbDocumentTypeDefinition.SelectedIndex = 0;
                
                if (!MainClass.isSuperUser)
                {
                    if (MainFile.isVisibleSecurity("frmTransactionDocumentsTasks"))
                    {
                        btnTransactionTasks.Enabled = true;
                    }
                    else
                    {
                        btnTransactionTasks.Enabled = false;
                    }

                    if (MainFile.isVisibleSecurity("frmTransactionDocumentsTasksSteps"))
                    {
                        btnTransactionFollowSteps.Enabled = true;
                    }
                    else
                    {
                        btnTransactionFollowSteps.Enabled = false;
                    }
                    if (MainFile.isVisibleSecurity("frmEmployeeExpiryDocuments"))
                    {
                        btnEmployeeExpiryDocument.Enabled = true;
                    }
                    else
                    {
                        btnEmployeeExpiryDocument.Enabled = false;
                    }
                    if (MainFile.isVisibleSecurity("frmCompanyLicensesExpiryDocuments"))
                    {
                        btnCompanyLicensesExpiryDocument.Enabled = true;
                    }
                    else
                    {
                        btnCompanyLicensesExpiryDocument.Enabled = false;
                    }
                    if (MainFile.isVisibleSecurity("frmTransactionDocumentsTasksNotPaid"))
                    {
                        btnDocumentTransactionMustPay.Enabled = true;
                    }
                    else
                    {
                        btnDocumentTransactionMustPay.Enabled = false;
                    }

                    if (MainFile.isVisibleSecurity("frmCarsExpiryDocuments"))
                    {
                        btnCarDocumentExpiryDate.Enabled = true;
                    }
                    else
                    {
                        btnCarDocumentExpiryDate.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCarDocumentExpiryDate_Click(object sender, EventArgs e)
        {
            frmCarsExpiryDocuments frm = new frmCarsExpiryDocuments();
            frm.ShowDialog();
        }

        private void cmbDocumentTypeDefinition_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                int _documentTypeDefID = int.Parse(cmbDocumentTypeDefinition.SelectedValue.ToString());
                PublicClass.bindDocumentDef(cmbDocumentDef, _documentTypeDefID);
                cmbDocumentDef.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int _documentTypeDefID = int.Parse(cmbDocumentTypeDefinition.SelectedValue.ToString());
                if (_documentTypeDefID == 1)
                {
                    int _documentDefID = int.Parse(cmbDocumentDef.SelectedValue.ToString());
                    // employee Report
                    printEmployeeReport(_documentDefID);
                }
                else if (_documentTypeDefID == 2)
                {
                    // company licences Expiry Report
                }
                else if (_documentTypeDefID == 4)
                {
                    // Car Expiry Report
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printEmployeeReport(int _documentDefID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c3 in db.DocumentTypeDefinitions
                          join c4 in db.DocumentDefs
                          on c3.documentTypeDefID equals c4.documentTypeDefID
                          join c1 in db.Documents
                          on c4.documentDefID equals c1.documentDefID
                          join c2 in db.Employees
                          on c1.document_TypeID equals c2.employeeID
                          join c5 in db.EmployeeLicenseShops on c2.employeeID equals c5.employeeID into jj
                          from c5 in jj.DefaultIfEmpty()
                          join cc5 in db.LicensesShops on c5.licencesShopID equals cc5.licencesShopID into jj1
                          from cc5 in jj1.DefaultIfEmpty()
                          join c6 in db.CompanyLicences on cc5.companyLicencesID equals c6.companyLicencesID into jj2
                          from c6 in jj2.DefaultIfEmpty()
                          where c3.documentType_Code == "01" &&
                          c4.documentDefID == _documentDefID &&
                          c1.document_endDate != null &&
                          c2.emp_active == true
                           orderby c1.document_endDate
                          select new
                          {
                              c3.documentTypeDefID,
                              c4.documentDefID,
                              companyLicencesID = c6 != null ? c6.companyLicencesID : -1,
                              c1.documentID,
                              c2.employeeID,
                              c3.documentType_Name,
                              c4.documentDef_Name,
                              c1.document_endDate,
                              c2.emp_namefullar,
                              c2.emp_No,
                              c6.commercialName,
                              c5.employeeLink,
                              c6.licenseNo,
                              noOfDays = c4.documentDef_WarningDays.Value
                          }).ToList();

                var val1 = val.ToList().Where(p => p.document_endDate <= DateTime.Parse(txtDate1.Text));               


                //foreach (var item in val1)
                //{
                //    GView.Rows.AddNew();
                //    GView.Rows[GView.Rows.Count - 1].Cells[0].Value = item.documentTypeDefID;
                //    GView.Rows[GView.Rows.Count - 1].Cells[1].Value = item.documentDefID;
                //    GView.Rows[GView.Rows.Count - 1].Cells[2].Value = item.companyLicencesID;
                //    GView.Rows[GView.Rows.Count - 1].Cells[3].Value = item.documentID;
                //    GView.Rows[GView.Rows.Count - 1].Cells[4].Value = item.employeeID;

                //    GView.Rows[GView.Rows.Count - 1].Cells[5].Value = item.documentDef_Name;
                //    GView.Rows[GView.Rows.Count - 1].Cells[6].Value = item.emp_No;
                //    GView.Rows[GView.Rows.Count - 1].Cells[7].Value = item.emp_namefullar;
                //    GView.Rows[GView.Rows.Count - 1].Cells[8].Value = item.commercialName;
                //    GView.Rows[GView.Rows.Count - 1].Cells[9].Value = item.document_endDate.Value.ToShortDateString();
                //    GView.Rows[GView.Rows.Count - 1].Cells[10].Value = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                //    GView.Rows[GView.Rows.Count - 1].Cells[11].Value = getDocumentTransaction(item.documentID);


                //}
                makePrintTable();
                foreach (var item in val1)
                {
                    //if (item.employeeID == 3067)
                    //{
                    //    MessageBox.Show("");
                    //}
                    DataRow dr = dt.NewRow();

                    dr["one"] = item.employeeID;
                    dr["two"] = item.emp_No;
                    dr["three"] = item.emp_namefullar;
                    dr["four"] = item.licenseNo;
                    dr["five"] = item.employeeLink;
                    dr["six"] = item.commercialName;
                    dr["seven"] = item.document_endDate.Value.ToShortDateString();
                    dr["eight"] = this.cmbDocumentDef.Text;
                    dr["nine"] = "";
                    dr["ten"] = "";

                    dr["13"] = getDocumentTransaction(item.documentID);
                    
                    if (isHasRemarks(item.companyLicencesID))
                    {
                        //GView.CurrentView.CurrentRow.BackColor = Color.Red;
                        dr["12"] = "ملاحظة ";
                    }

                    if (isHasRental(item.companyLicencesID))
                    {
                        //GView.CurrentView.CurrentRow.BackColor = Color.Cyan;
                        dr["12"] += " ايجار ";
                    }

                   // MessageBox.Show(item.employeeID.ToString());

                    var vvv = (from xx in db.EmployeeSponsors.Include("CompanyLicence")
                               where xx.employeeID == item.employeeID
                               select xx).ToList();

                    if (vvv.Count > 0 && vvv[0].CompanyLicence!= null)
                    {
                        
                        dr["14"] = "";
                        dr["15"] = vvv[0].CompanyLicence.licenseNo;
                        dr["16"] = vvv[0].CompanyLicence.commercialName;
                    }
                    else
                    {
                        dr["14"] = "";
                        dr["15"] = "";
                        dr["16"] = "";
                    }

                    dr["17"] = "";
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";

                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("Licencesemployee.xsd");
                //MessageBox.Show("dde");

                    frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                    rv.printReport = "employeeExpiryDocument";
                    rv.ds = ds;
                    rv.title = "انتهاء " + cmbDocumentDef.Text + " الموظفين  : " + " حتى : " +
                        txtDate1.Text;
                    rv.ShowDialog(); 
                
                 


            }
        }

        DataTable dt = null;
        private void makePrintTable()
        {
            dt = new DataTable();
            dt.Columns.Add("one");
            dt.Columns.Add("two");
            dt.Columns.Add("three");
            dt.Columns.Add("four");
            dt.Columns.Add("five");
            dt.Columns.Add("six");
            dt.Columns.Add("seven");
            dt.Columns.Add("eight");
            dt.Columns.Add("nine");
            dt.Columns.Add("ten");

            dt.Columns.Add("11");
            dt.Columns.Add("12");
            dt.Columns.Add("13");
            dt.Columns.Add("14");
            dt.Columns.Add("15");
            dt.Columns.Add("16");
            dt.Columns.Add("17");
            dt.Columns.Add("18");
            dt.Columns.Add("19");
            dt.Columns.Add("20");

            dt.Columns.Add("21");
            dt.Columns.Add("22");
            dt.Columns.Add("23");
            dt.Columns.Add("24");
            dt.Columns.Add("25");

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
                    temp = item.transDef_namea;
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

        private void btnMandoubPayment_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintTable();
                getData1();
                getData();
                getData2();

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //ds.WriteXmlSchema("OtherPayment.xsd");
                //MessageBox.Show("OtherPayment !!!");

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "OtherPayment";
                rv.ds = ds;
                rv.title = "دفوعات المندوب من : " + txtdatefrom.Text + " الموظفين  : " + " حتى : " +
                    txtdateto.Text;
                rv.ShowDialog(); 

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
                          cc.documentTransTask_isPaid == true
                          orderby c1.document_endDate
                          select new
                          {
                              c3.documentTypeDefID,
                              c4.documentDefID,
                              c6.companyLicencesID,
                              c1.documentID,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().voucherNo,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().voucherAmount,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().edited_Date,
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

                var val1 = val.ToList().Where(s=>s.edited_Date.Value>=DateTime.Parse(txtdatefrom.Text) && s.edited_Date.Value <=DateTime.Parse(txtdateto.Text));

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
                    GView.Rows[GView.Rows.Count - 1].Cells[12].Value = item.documentTransTaskID.ToString();

                    GView.Rows[GView.Rows.Count - 1].Cells[13].Value = item.voucherAmount.Value.ToString();
                    GView.Rows[GView.Rows.Count - 1].Cells[14].Value = item.voucherNo.ToString();


                    DataRow dr = dt.NewRow();

                    dr["one"] = item.documentTypeDefID;
                    dr["two"] = item.documentDefID;
                    dr["three"] = item.companyLicencesID;
                    dr["four"] = item.documentID;
                    dr["five"] = item.documentDef_Name;
                    dr["six"] = item.licenseNo;
                    dr["seven"] = item.licenseUniqueNo;
                    dr["eight"] = item.commercialName;
                    dr["nine"] = item.document_endDate.Value.ToShortDateString();
                    dr["ten"] = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    dr["11"] = getDocumentTransaction(item.documentID);
                    dr["12"] = item.documentTrans_Amount.Value.ToString();
                    dr["13"] = item.documentTransTaskID.ToString();
                    dr["14"] = item.voucherAmount.Value.ToString();
                    dr["15"] = item.voucherNo.ToString();
                    dr["16"] = item.edited_Date.Value.ToShortDateString();
                    dr["17"] = "";
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";
                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);
                }
            }


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
                          cc.documentTransTask_isPaid == true
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
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().voucherNo,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().voucherAmount,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().edited_Date,
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
                //var val1 = val.ToList();
                var val1 = val.ToList().Where(s => s.edited_Date.Value >= DateTime.Parse(txtdatefrom.Text) && s.edited_Date.Value <= DateTime.Parse(txtdateto.Text));
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
                    GView.Rows[GView.Rows.Count - 1].Cells[12].Value = item.documentTransTaskID.ToString();

                    GView.Rows[GView.Rows.Count - 1].Cells[13].Value = item.voucherAmount.Value.ToString();
                    GView.Rows[GView.Rows.Count - 1].Cells[14].Value = item.voucherNo.ToString();

                    DataRow dr = dt.NewRow();

                    dr["one"] = item.documentTypeDefID;
                    dr["two"] = item.documentDefID;
                    dr["three"] = item.companyLicencesID;
                    dr["four"] = item.documentID;
                    dr["five"] = item.documentDef_Name;
                    dr["six"] = item.emp_No;
                    dr["seven"] = item.emp_namefullar;
                    dr["eight"] = item.commercialName;
                    dr["nine"] = item.document_endDate.Value.ToShortDateString();
                    dr["ten"] = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    dr["11"] = getDocumentTransaction(item.documentID);
                    dr["12"] = item.documentTrans_Amount.Value.ToString();
                    dr["13"] = item.documentTransTaskID.ToString();
                    dr["14"] = item.voucherAmount.Value.ToString();
                    dr["15"] = item.voucherNo.ToString();
                    dr["16"] = item.edited_Date.Value.ToShortDateString();
                    dr["17"] = "";
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";
                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);

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
                          cc.documentTransTask_isPaid == true 
                          orderby c1.document_endDate
                          select new
                          {
                              c3.documentTypeDefID,
                              c4.documentDefID,
                              c6.carID,
                              c1.documentID,
                              //c2.employeeID,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().voucherNo,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().voucherAmount,
                              cc.DocumentTransactionTasksPayments.FirstOrDefault().edited_Date,
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

                //var val1 = val.ToList().Where(p => p.document_endDate <= DateTime.Now.AddDays(p.noOfDays));
                var val1 = val.ToList().Where(s => s.edited_Date.Value >= DateTime.Parse(txtdatefrom.Text) && s.edited_Date.Value <= DateTime.Parse(txtdateto.Text));

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
                    GView.Rows[GView.Rows.Count - 1].Cells[12].Value = item.documentTransTaskID.ToString();

                    GView.Rows[GView.Rows.Count - 1].Cells[13].Value = item.voucherAmount.Value.ToString();
                    GView.Rows[GView.Rows.Count - 1].Cells[14].Value = item.voucherNo.ToString();

                    DataRow dr = dt.NewRow();

                    dr["one"] = item.documentTypeDefID;
                    dr["two"] = item.documentDefID;
                    dr["three"] = item.carID;
                    dr["four"] = item.documentID;
                    dr["five"] = item.documentDef_Name;
                    dr["six"] = item.car_No;
                    dr["seven"] = item.car_Model;
                    dr["eight"] = item.car_Name;
                    dr["nine"] = item.document_endDate.Value.ToShortDateString();
                    dr["ten"] = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    dr["11"] = getDocumentTransaction(item.documentID);
                    dr["12"] = item.documentTrans_Amount.Value.ToString();
                    dr["13"] = item.documentTransTaskID.ToString();
                    dr["14"] = item.voucherAmount.Value.ToString();
                    dr["15"] = item.voucherNo.ToString();
                    dr["16"] = item.edited_Date.Value.ToShortDateString();
                    dr["17"] = "";
                    dr["18"] = "";
                    dr["19"] = "";
                    dr["20"] = "";
                    dr["21"] = "";
                    dr["22"] = "";
                    dr["23"] = "";
                    dr["24"] = "";
                    dr["25"] = "";

                    dt.Rows.Add(dr);

                }
            }


        }





    }
}
