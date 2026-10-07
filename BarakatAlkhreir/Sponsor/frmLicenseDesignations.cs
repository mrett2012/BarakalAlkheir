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

namespace BarakalAlkheir.Sponsor
{
    public partial class frmLicenseDesignations : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        CompanyLicence CompanyLicence = null;
        List<CompanyLicence> lstCompanyLicence = null;
        List<CompanyLicenseDesignation> lstCompanyLicenseDesignation = null;

        components.banner mainbar = new components.banner("frmLicenseDesignations");
        int noOfeditSeats = 0;


        public frmLicenseDesignations()
        {
            InitializeComponent();
        }

        private void frmLicenseDesignations_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmLicenseDesignations_LoadData);
        }

        private async System.Threading.Tasks.Task frmLicenseDesignations_LoadData(object sender, EventArgs e)
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
                await PublicClass.bindDesignationsAsync(cmbDesignations);

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
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
            int Rseats = int.Parse(lblRemainingSeats.Text);
            if (Rseats > 0)
            {
                change = "add";
                enableControls();

                txtnoOfSeats.Text = "";
                cmbDesignations.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("ليس هناك اماكن متاحة للاضافة","Error Input",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                change = "edit";
                noOfeditSeats = int.Parse(txtnoOfSeats.Text);
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
                            CompanyLicenseDesignation bb = (from c in db.CompanyLicenseDesignations
                                                            where c.companyLicencesDesignationID ==
                                                                bbID
                                                            select c).FirstOrDefault();

                            db.CompanyLicenseDesignations.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                //position = 0;
                                lblID.Text = "-1";
                                bindGridView();
                                bindLicenseDesignation();
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
            bindLicenseDesignation();
        }

        private bool inputValidation()
        {
            bool temp = false;
            string msg = "";
            int NewSeats;
            if (int.TryParse(txtnoOfSeats.Text, out NewSeats))
            {
                int Rseates=int.Parse(lblRemainingSeats.Text);
                
                if (change == "add")
                {
                    
                    if (NewSeats > Rseates)
                    {
                        msg += "لايمكنك اضافة عدد وظائف اكبر من المتاح \n";
                    }
                }
                if (change == "edit")
                {
                    

                    if (NewSeats > Rseates + noOfeditSeats)
                    {
                        msg += "لايمكنك اضافة عدد وظائف اكبر من المتاح \n";
                    }
                }
            }
            else
            {
                msg += "الرقم الذي ادخلته غير صحيح\n";
            }

            if (msg.Length > 0)
            {
                MessageBox.Show(msg, "Error Input", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                temp = true;
            }

            return temp;
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
                        using (TransactionScope ts = new TransactionScope())
                        {
                            CompanyLicenseDesignation bb = new CompanyLicenseDesignation();

                            bb.designationID = int.Parse(this.cmbDesignations.SelectedValue.ToString());
                            bb.noOfSeats = int.Parse(txtnoOfSeats.Text);
                            bb.companyLicencesID = lstCompanyLicence[position].companyLicencesID;

                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.CompanyLicenseDesignations.AddObject(bb);
                            rowsAffected = db.SaveChanges();
                            lblID.Text = bb.companyLicencesDesignationID.ToString();

                            ts.Complete();
                            db.AcceptAllChanges();
                        }
                    }
                }
                if (change == "edit" && inputValidation())
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            int bbID = int.Parse(lblID.Text);
                            CompanyLicenseDesignation bb = (from c in db.CompanyLicenseDesignations
                                                            where c.companyLicencesDesignationID ==
                                                                bbID
                                                            select c).FirstOrDefault();

                            bb.designationID = int.Parse(this.cmbDesignations.SelectedValue.ToString());
                            bb.noOfSeats = int.Parse(txtnoOfSeats.Text);
                            bb.companyLicencesID = lstCompanyLicence[position].companyLicencesID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            rowsAffected = db.SaveChanges();

                            ts.Complete();
                            db.AcceptAllChanges();

                        }
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();
                    bindGridView();
                    bindLicenseDesignation();
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
            frmfindLicences frm = new frmfindLicences(false,true);
            frm.ShowDialog();
            if (frm.found)
            {
                CompanyLicence cl = lstCompanyLicence.Find(c => c.companyLicencesID == frm.licID);
                position = lstCompanyLicence.IndexOf(cl);
                bindData();
            }
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
            if (position < lstCompanyLicence.Count - 1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position < lstCompanyLicence.Count - 1)
            {
                position = lstCompanyLicence.Count - 1;
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


            txtcommercialName.ReadOnly = true;
            txtlicenseNo.ReadOnly = true;
            txtNoOfDeservedSeats.ReadOnly = true;
            txtNoOfUsedSeats.ReadOnly = true;

            cmbDesignations.Enabled = false;
            txtnoOfSeats.ReadOnly = true;
         
            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            cmbDesignations.Enabled = true;
            txtnoOfSeats.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCompanyLicence = (from c in db.CompanyLicences select c).ToList();
            }
        }

        private void bindData()
        {
            if (lstCompanyLicence.Count > 0)
            {
                txtcommercialName.Text = lstCompanyLicence[position].commercialName;
                txtlicenseNo.Text = lstCompanyLicence[position].licenseNo;
                txtNoOfDeservedSeats.Text = lstCompanyLicence[position].noOfSeats.Value.ToString();
                mainbar.lblCount.Text = position + 1 + " / " + lstCompanyLicence.Count;                       
            }
            else
            {
                txtcommercialName.Text = "";
                txtlicenseNo.Text = "";
                txtNoOfDeservedSeats.Text = "0";
                txtNoOfUsedSeats.Text = "0";
                txtnoOfSeats.Text = "0";
                cmbDesignations.SelectedIndex = 0;
                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
            lblID.Text = "-1";
            bindGridView();
            bindLicenseDesignation();
        }


        private void bindGridView()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int CLID = lstCompanyLicence[position].companyLicencesID;
                var val = from c in db.CompanyLicenseDesignations
                          where c.companyLicencesID == CLID
                          select new
                          {
                              c.companyLicencesDesignationID,
                              c.Designation.desig_namea,
                              c.noOfSeats
                          };
                GView.DataSource = val;
            }
        }

        private void bindLicenseDesignation()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                int CLID = lstCompanyLicence[position].companyLicencesID;

                lstCompanyLicenseDesignation = (from c in db.CompanyLicenseDesignations
                                                where c.companyLicencesID == CLID
                                                select c).ToList();

                

                if (lblID.Text == "-1")
                {
                    if (lstCompanyLicenseDesignation.Count > 0)
                    {
                        txtnoOfSeats.Text = lstCompanyLicenseDesignation[0].noOfSeats.Value.ToString();
                        for (int i = 0; i < cmbDesignations.Items.Count; i++)
                        {
                            if (cmbDesignations.Items[i].Value.ToString() == lstCompanyLicenseDesignation[0].designationID.Value.ToString())
                            {
                                cmbDesignations.SelectedIndex = i;
                                break;
                            }
                        }
                        GView.Rows[0].IsSelected = true;
                        lblID.Text = lstCompanyLicenseDesignation[0].companyLicencesDesignationID.ToString();
                    }
                    else
                    {
                        txtnoOfSeats.Text = "0";
                        cmbDesignations.SelectedIndex = 0;
                        lblID.Text = "-1";
                    }
                }
                else
                {
                    int cldID = int.Parse(lblID.Text);
                    CompanyLicenseDesignation cld = lstCompanyLicenseDesignation.Find(c => c.companyLicencesDesignationID == cldID);
                    txtnoOfSeats.Text = cld.noOfSeats.Value.ToString();

                    for (int i = 0; i < cmbDesignations.Items.Count; i++)
                    {
                        if (cmbDesignations.Items[i].Value.ToString() == cld.designationID.Value.ToString())
                        {
                            cmbDesignations.SelectedIndex = i;
                            break;
                        }
                    }
                    
                }

                int noOfSeats = 0;
                foreach (var item in lstCompanyLicenseDesignation)
                {
                    noOfSeats += item.noOfSeats.Value;
                }
                txtNoOfUsedSeats.Text = noOfSeats.ToString();

                int used = int.Parse(txtNoOfDeservedSeats.Text);
                lblRemainingSeats.Text = (used - noOfSeats).ToString();
                
            }
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                lblID.Text = bID.ToString();
                bindLicenseDesignation();

            }
        }




    }
}
