using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Model;
using System.Transactions;

namespace BarakalAlkheir.Sponsor
{
    public partial class frmCompanyLicences : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        CompanyLicence CompanyLicence = null;
        List<CompanyLicence> lstCompanyLicence = null;
        components.banner mainbar = new components.banner("pageLicences");
        components.banner mainbarDocument = new components.banner("pageLicencesDocuments");

        public int _companyLicencesID = -1;
        List<Document> lstDocument = null;
        int positionDocument = 0;

        public frmCompanyLicences()
        {
            InitializeComponent();
        }


        private void frmCompanyLicences_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                pageLicences.Controls.Add(mainbar);
                
                mainbarDocument.Dock = DockStyle.Top;
                pageLicencesDocuments.Controls.Add(mainbarDocument);

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

                mainbarDocument.btnAdd.Click += new EventHandler(btnAdd_ClickDoc);
                mainbarDocument.btnEdit.Click += new EventHandler(btnEdit_ClickDoc);
                mainbarDocument.btnDelete.Click += new EventHandler(btnDelete_ClickDoc);

                mainbarDocument.btnFirst.Click += new EventHandler(btnFirst_ClickDoc);
                mainbarDocument.btnPrevious.Click += new EventHandler(btnPrevious_ClickDoc);
                mainbarDocument.btnNext.Click += new EventHandler(btnNext_ClickDoc);
                mainbarDocument.btnLast.Click += new EventHandler(btnLast_ClickDoc);
                mainbarDocument.btnExit.Click += new EventHandler(btnExit_ClickDoc);
                mainbarDocument.btnSave.Visible = false;
                mainbarDocument.btnCancel.Visible = false;
                mainbarDocument.btnFind.Visible = false;



                disableControls();
                PublicClass.bindCompany(cmbcompanyID);
                PublicClass.bindMandoub(cmbMandoub);
                PublicClass.bindLicenseActivity(cmbLicenseActivity);
                PublicClass.bindGovernorate(txtMohafaza);
                PublicClass.bindGovernorateArea(txtMantaqa, -1);
                txtMantaqa.SelectedIndex = 0;

                getData();
                
                if (_companyLicencesID != -1)
                {
                    position = lstCompanyLicence.FindIndex(cc => cc.companyLicencesID == _companyLicencesID);
                }
                bindData();
                
            }
                catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void btnAdd_Click(object sender, EventArgs e)
        {
            change = "add";
            enableControls();

            txtappartment.Text = "";
            txtBuilduingType.Text = "";
            txtcommercialName.Text = "";
            lblcommercialName.Text = "";
            txtcontactNo.Text = "";
            txtendDate.Text = DateTime.Now.ToShortDateString();
            txtfloor.Text = "";
            txtfulladdress.Text = "";
            txtlicenseUniqueNo.Text = "";
            txtlicenseLinkCode.Text = "";
            txtlicensecivilID.Text = "";
            txtlicenseNo.Text = "";
            //txtMantaqa.Text = "";
            //txtMohafaza.Text = "";
            txtnoOfSeats.Text = "0";
            txtpobox.Text = "";
            txtpoboxOffice.Text = "";
            txtQuet3a.Text = "";
            txtShare3.Text = "";
            txtstartDate.Text =DateTime.Now.ToShortDateString();
            chkisActive.Checked = true;
            chkisFack.Checked = false;
            chkisClose.Checked = false;
            chkisMainLicences.Checked = false;
            cmbMandoub.SelectedIndex = 0;
            cmbLicenseActivity.SelectedIndex = 0;
            cmbcompanyID.SelectedIndex = 0;
            txtMohafaza.SelectedIndex = 0;
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
                    DialogResult dr = MessageBox.Show("هل تريد ال   حذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            using (TransactionScope ts = new TransactionScope())
                            {
                                int bbID = int.Parse(lblID.Text);

                                int IDDD = int.Parse(this.lblID.Text);
                                Document bb1 = (from c in db.Documents
                                                where
                                                    c.document_TypeID == IDDD &&
                                                    c.documentDefID ==6
                                                select c).FirstOrDefault();

                                if (bb1 != null)
                                {
                                    db.Documents.DeleteObject(bb1);
                                    db.SaveChanges();
                                }


                                LicensesShop lss = (from lcc in db.LicensesShops
                                                    where lcc.companyLicencesID == IDDD
                                                    select lcc).FirstOrDefault();
                                if (lss != null)
                                {
                                    db.LicensesShops.DeleteObject(lss);
                                    db.SaveChanges();
                                }

                                CompanyLicence bb = (from c in db.CompanyLicences
                                                     where c.companyLicencesID ==
                                                         bbID
                                                     select c).FirstOrDefault();

                                db.CompanyLicences.DeleteObject(bb);
                                db.SaveChanges();
                               


                                

                                db.AcceptAllChanges();
                                ts.Complete();

                            }

                            position = 0;
                            getData();
                            bindData();
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
                        using (TransactionScope ts = new TransactionScope())
                        {
                            CompanyLicence bb = new CompanyLicence();

                            bb.companyID = int.Parse(cmbcompanyID.SelectedValue.ToString());
                            bb.mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());
                            bb.licenseActivityID = int.Parse(this.cmbLicenseActivity.SelectedValue.ToString());

                            bb.governorateID = int.Parse(txtMohafaza.SelectedValue.ToString());
                            bb.goverareaID = int.Parse(txtMantaqa.SelectedValue.ToString());



                            bb.appartment = txtappartment.Text;
                            bb.BuilduingType = txtBuilduingType.Text;
                            bb.commercialName = txtcommercialName.Text;
                            bb.contactNo = txtcontactNo.Text;
                            bb.endDate = DateTime.Parse(txtendDate.Text);
                            bb.floor = txtfloor.Text;
                            bb.fulladdress = txtfulladdress.Text;
                            bb.licenseUniqueNo = txtlicenseUniqueNo.Text;
                            bb.licenseLinkCode = txtlicenseLinkCode.Text;
                            bb.licensecivilID = txtlicensecivilID.Text;
                            bb.licenseNo = txtlicenseNo.Text;
                            bb.Mantaqa = txtMantaqa.Text;
                            bb.Mohafaza = txtMohafaza.Text;
                            bb.noOfSeats = int.Parse(txtnoOfSeats.Text);
                            bb.pobox = txtpobox.Text;
                            bb.poboxOffice = txtpoboxOffice.Text;
                            bb.Quet3a = txtQuet3a.Text;
                            bb.Share3 = txtShare3.Text;
                            bb.startDate = DateTime.Parse(txtstartDate.Text);

                            bb.isActive = chkisActive.Checked;
                            bb.isFack = chkisFack.Checked;
                            bb.isClose = chkisClose.Checked;
                            bb.isMainLicences = chkisMainLicences.Checked;


                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.CompanyLicences.AddObject(bb);
                            rowsAffected = db.SaveChanges();

                            lblID.Text = bb.companyLicencesID.ToString();

                            if (rowsAffected > 0)
                            {
                                LicensesShop shop = new LicensesShop();
                                shop.licencesShopID = bb.companyLicencesID;
                                shop.companyLicencesID = bb.companyLicencesID;
                                shop.shop_isCurrent = true;
                                shop.shop_Code = txtlicenseNo.Text;
                                shop.shop_Name = txtcommercialName.Text;
                                shop.shop_contact1 = "";
                                shop.shop_contact2 = "";

                                shop.added_Date = DateTime.Now;
                                shop.addedBy = MainClass.userID;

                                shop.edited_Date = DateTime.Now;
                                shop.editedBy = MainClass.userID;

                                db.LicensesShops.AddObject(shop);
                                db.SaveChanges();

                                Document bb1 = new Document();
                                bb1.document_startDate = DateTime.Parse(txtstartDate.Text);
                                bb1.document_endDate = DateTime.Parse(txtendDate.Text);

                                // we must get the id of the document
                                //var val = from c in db.DocumentDefs
                                //          where
                                //          c.documentDefID == 2 &&
                                //          (c.documentDef_Name == "الرخصة" ||
                                //             c.documentDef_Name == "الرخصة التجارية" ||
                                //             c.documentDef_Name == "الرخصة التجارية" ||
                                //             c.documentDef_Name == "رخصة تجارية")
                                //          select c;
                                //if (val.ToList().Count > 0)
                                //{
                                //    bb1.documentDefID = val.ToList()[0].documentDefID;
                                //}

                                bb1.documentDefID = 6;
                                bb1.document_No = txtlicenseNo.Text;
                                bb1.document_text1 = "";
                                bb1.document_text2 = "";
                                bb1.document_text3 = "";
                                bb1.document_text4 = "";
                                bb1.document_text5 = "";

                                bb1.document_placeOfIssue = txtMohafaza.Text;
                                bb1.document_TypeID = int.Parse(this.lblID.Text);

                                bb1.document_Remarks = txtcommercialName.Text;

                                bb1.document_Status = "";
                                bb1.document_Type = "الرخصة التجارية";

                                bb1.added_Date = DateTime.Now;
                                bb1.addedBy = MainClass.userID;

                                bb1.edited_Date = DateTime.Now;
                                bb1.editedBy = MainClass.userID;

                                db.Documents.AddObject(bb1);
                                db.SaveChanges();

                            }
                            db.AcceptAllChanges();
                            ts.Complete();

                        }
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            int bbID = int.Parse(lblID.Text);
                            CompanyLicence bb = (from c in db.CompanyLicences
                                                 where c.companyLicencesID ==
                                                     bbID
                                                 select c).FirstOrDefault();

                            bb.companyID = int.Parse(cmbcompanyID.SelectedValue.ToString());
                            bb.mandoubID = int.Parse(cmbMandoub.SelectedValue.ToString());
                            bb.licenseActivityID = int.Parse(cmbLicenseActivity.SelectedValue.ToString());
                            bb.governorateID = int.Parse(txtMohafaza.SelectedValue.ToString());
                            bb.goverareaID = int.Parse(txtMantaqa.SelectedValue.ToString());



                            bb.appartment = txtappartment.Text;
                            bb.BuilduingType = txtBuilduingType.Text;
                            bb.commercialName = txtcommercialName.Text;
                            bb.contactNo = txtcontactNo.Text;
                            bb.endDate = DateTime.Parse(txtendDate.Text);
                            bb.floor = txtfloor.Text;
                            bb.fulladdress = txtfulladdress.Text;
                            bb.licenseUniqueNo = txtlicenseUniqueNo.Text;
                            bb.licenseLinkCode = txtlicenseLinkCode.Text;
                            bb.licensecivilID = txtlicensecivilID.Text;
                            bb.licenseNo = txtlicenseNo.Text;
                            bb.Mantaqa = txtMantaqa.Text;
                            bb.Mohafaza = txtMohafaza.Text;
                            bb.noOfSeats = int.Parse(txtnoOfSeats.Text);
                            bb.pobox = txtpobox.Text;
                            bb.poboxOffice = txtpoboxOffice.Text;
                            bb.Quet3a = txtQuet3a.Text;
                            bb.Share3 = txtShare3.Text;
                            bb.startDate = DateTime.Parse(txtstartDate.Text);

                            bb.isActive = chkisActive.Checked;
                            bb.isFack = chkisFack.Checked;
                            bb.isClose = chkisClose.Checked;
                            bb.isMainLicences = chkisMainLicences.Checked;



                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            rowsAffected = db.SaveChanges();

                            if (rowsAffected > 0)
                            {
                                LicensesShop shop = (from c in db.LicensesShops
                                                     where c.companyLicencesID == bb.companyLicencesID
                                                     select c).FirstOrDefault();

                                shop.licencesShopID = bb.companyLicencesID;
                                shop.shop_Code = txtlicenseNo.Text;
                                shop.shop_Name = txtcommercialName.Text;
                                shop.edited_Date = DateTime.Now;
                                shop.editedBy = MainClass.userID;

                                db.SaveChanges();

                                // we must get the id of the document
                                //int ddIDD = -1;
                                //var val = from c in db.DocumentDefs
                                //          where
                                //          c.documentDefID == 6 &&
                                //          (c.documentDef_Name == "الرخصة" ||
                                //             c.documentDef_Name == "الرخصة التجارية" ||
                                //             c.documentDef_Name == "الرخصة التجارية" ||
                                //             c.documentDef_Name == "رخصة تجارية")
                                //          select c;
                                //if (val.ToList().Count > 0)
                                //{
                                //    ddIDD = val.ToList()[0].documentDefID;
                                //}

                                int IDDD = int.Parse(this.lblID.Text);
                                Document bb1 = (from c in db.Documents
                                                where
                                                    c.document_TypeID == IDDD &&
                                                    c.documentDefID == 6
                                                select c).FirstOrDefault();


                                bb1.document_startDate = DateTime.Parse(txtstartDate.Text);
                                bb1.document_endDate = DateTime.Parse(txtendDate.Text);
                                bb1.documentDefID = 6;
                                bb1.document_No = txtlicenseNo.Text;
                                bb1.document_text1 = "";
                                bb1.document_text2 = "";
                                bb1.document_text3 = "";
                                bb1.document_text4 = "";
                                bb1.document_text5 = "";

                                bb1.document_placeOfIssue = txtMohafaza.Text;
                                bb1.document_TypeID = int.Parse(this.lblID.Text);

                                bb1.document_Remarks = txtcommercialName.Text;

                                bb1.document_Status = "";
                                bb1.document_Type = "الرخصة التجارية";

                                bb1.edited_Date = DateTime.Now;
                                bb1.editedBy = MainClass.userID;

                                db.SaveChanges();
                            }

                            db.AcceptAllChanges();
                            ts.Complete();

                        }
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstCompanyLicence.Count - 1;
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
            frmfindLicences frm = new frmfindLicences(false,true);
            frm.isAllLicences = true;
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

            txtappartment.ReadOnly = true;
            txtBuilduingType.ReadOnly = true;
            txtcommercialName.ReadOnly = true;
            txtcontactNo.ReadOnly = true;
            
            txtfloor.ReadOnly = true;
            txtfulladdress.ReadOnly = true;
            txtlicenseUniqueNo.ReadOnly = true;
            txtlicenseLinkCode.ReadOnly = true;
            txtlicensecivilID.ReadOnly = true;
            txtlicenseNo.ReadOnly = true;
            txtMantaqa.Enabled = false;
            txtMohafaza.Enabled = false;
            txtnoOfSeats.ReadOnly = true;
            txtpobox.ReadOnly = true;
            txtpoboxOffice.ReadOnly = true;
            txtQuet3a.ReadOnly = true;
            txtShare3.ReadOnly = true;

            txtendDate.Enabled = false;
            txtstartDate.Enabled = false;

            cmbcompanyID.Enabled = false;
            btnFindCompany.Enabled = false;

            cmbMandoub.Enabled = false;
            cmbLicenseActivity.Enabled = false;
            chkisActive.Enabled = false;
            chkisFack.Enabled = false;
            chkisClose.Enabled = false;
            chkisMainLicences.Enabled = false;


            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtappartment.ReadOnly = false;
            txtBuilduingType.ReadOnly = false;
            txtcommercialName.ReadOnly = false;
            txtcontactNo.ReadOnly = false;

            txtfloor.ReadOnly = false;
            txtfulladdress.ReadOnly = false;
            txtlicenseUniqueNo.ReadOnly = false;
            txtlicenseLinkCode.ReadOnly = false;
            txtlicensecivilID.ReadOnly = false;
            txtlicenseNo.ReadOnly = false;
            txtMantaqa.Enabled = true;
            txtMohafaza.Enabled = true;
            txtnoOfSeats.ReadOnly = false;
            txtpobox.ReadOnly = false;
            txtpoboxOffice.ReadOnly = false;
            txtQuet3a.ReadOnly = false;
            txtShare3.ReadOnly = false;

            txtendDate.Enabled = true;
            txtstartDate.Enabled = true;

            cmbcompanyID.Enabled = true;
            btnFindCompany.Enabled = true;
            cmbMandoub.Enabled = true;
            cmbLicenseActivity.Enabled = true;
            chkisActive.Enabled = true;
            chkisFack.Enabled = true;
            chkisClose.Enabled = true;
            chkisMainLicences.Enabled = true;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCompanyLicence = (from c in db.CompanyLicences  select c).ToList();
                GView.DataSource = lstCompanyLicence;
            }
        }

        private void bindData()
        {
            if (lstCompanyLicence.Count > 0)
            {
                txtappartment.Text = lstCompanyLicence[position].appartment;
                txtBuilduingType.Text = lstCompanyLicence[position].BuilduingType;
                txtcommercialName.Text = lstCompanyLicence[position].commercialName;
                lblcommercialName.Text = lstCompanyLicence[position].commercialName;
                txtcontactNo.Text = lstCompanyLicence[position].contactNo;
                txtendDate.Text = lstCompanyLicence[position].endDate.Value.ToShortDateString();
                txtfloor.Text = lstCompanyLicence[position].floor;
                txtfulladdress.Text = lstCompanyLicence[position].fulladdress;
                txtlicenseUniqueNo.Text = lstCompanyLicence[position].licenseUniqueNo;
                txtlicenseLinkCode.Text = lstCompanyLicence[position].licenseLinkCode;
                txtlicensecivilID.Text = lstCompanyLicence[position].licensecivilID;
                txtlicenseNo.Text = lstCompanyLicence[position].licenseNo;
                txtMantaqa.Text = lstCompanyLicence[position].Mantaqa;
                txtMohafaza.Text = lstCompanyLicence[position].Mohafaza;
                txtnoOfSeats.Text = lstCompanyLicence[position].noOfSeats.Value.ToString();
                txtpobox.Text = lstCompanyLicence[position].pobox;
                txtpoboxOffice.Text = lstCompanyLicence[position].poboxOffice;
                txtQuet3a.Text = lstCompanyLicence[position].Quet3a;
                txtShare3.Text = lstCompanyLicence[position].Share3;
                txtstartDate.Text = lstCompanyLicence[position].startDate.Value.ToShortDateString();

                chkisActive.Checked = lstCompanyLicence[position].isActive.Value;
                chkisFack.Checked = lstCompanyLicence[position].isFack.Value;
                chkisClose.Checked = lstCompanyLicence[position].isClose.Value;
                chkisMainLicences.Checked = lstCompanyLicence[position].isMainLicences.Value;


                lblID.Text = lstCompanyLicence[position].companyLicencesID.ToString();

                for (int i = 0; i < cmbcompanyID.Items.Count; i++)
                {
                    if (cmbcompanyID.Items[i].Value.ToString() == lstCompanyLicence[position].companyID.Value.ToString())
                    {
                        cmbcompanyID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbMandoub.Items.Count; i++)
                {
                    if (cmbMandoub.Items[i].Value.ToString() == lstCompanyLicence[position].mandoubID.Value.ToString())
                    {
                        cmbMandoub.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < this.cmbLicenseActivity.Items.Count; i++)
                {
                    if (cmbLicenseActivity.Items[i].Value.ToString() == lstCompanyLicence[position].licenseActivityID.Value.ToString())
                    {
                        cmbLicenseActivity.SelectedIndex = i;
                        break;
                    }
                }


                for (int i = 0; i < txtMohafaza.Items.Count; i++)
                {
                    if (txtMohafaza.Items[i].Value.ToString() == lstCompanyLicence[position].governorateID.Value.ToString())
                    {
                        txtMohafaza.SelectedIndex = i;
                        break;
                    }
                }

                int _governorateID = lstCompanyLicence[position].governorateID.Value;
                PublicClass.bindGovernorateArea(txtMantaqa, _governorateID);
                txtMantaqa.SelectedIndex = 0;

                for (int i = 0; i < txtMantaqa.Items.Count; i++)
                {
                    if (txtMantaqa.Items[i].Value.ToString() == lstCompanyLicence[position].goverareaID.Value.ToString())
                    {
                        txtMantaqa.SelectedIndex = i;
                        break;
                    }
                }

                mainbar.lblCount.Text = position + 1 + " / " + lstCompanyLicence.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtappartment.Text = "";
                txtBuilduingType.Text = "";
                txtcommercialName.Text = "";
                lblcommercialName.Text = "";
                txtcontactNo.Text = "";
                txtendDate.Text = "";
                txtfloor.Text = "";
                txtfulladdress.Text = "";
                txtlicenseUniqueNo.Text = "";
                txtlicenseLinkCode.Text = "";
                txtlicensecivilID.Text = "";
                txtlicenseNo.Text = "";
                txtMantaqa.Text = "";
                txtMohafaza.Text = "";
                txtnoOfSeats.Text = "";
                txtpobox.Text = "";
                txtpoboxOffice.Text = "";
                txtQuet3a.Text = "";
                txtShare3.Text = "";
                txtstartDate.Text = "";

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }

            getDocData();
            bindDocData1();
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                CompanyLicence cl = this.lstCompanyLicence.Find(b => b.companyLicencesID == bID);

                position = lstCompanyLicence.IndexOf(cl);
                bindData();
            }
        }

        private void chkisClose_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkisClose.Checked)
            {
                chkisActive.Checked = false;
            }
        }

        private void txtMohafaza_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (change != "")
            {
                int _governorateID = int.Parse(txtMohafaza.SelectedValue.ToString());
                PublicClass.bindGovernorateArea(txtMantaqa, _governorateID);
                txtMantaqa.SelectedIndex = 0;
            }
        }

        private void btnFindCompany_Click(object sender, EventArgs e)
        {
            try
            {
                frmFindCompany frm = new frmFindCompany();
                frm.ShowDialog();
                if (frm.found)
                {
                    for (int i = 0; i < cmbcompanyID.Items.Count; i++)
                    {
                        if (cmbcompanyID.Items[i].Value.ToString() == frm.cID.ToString())
                        {
                            cmbcompanyID.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radPageView1_PageRemoving(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {
            e.Cancel = true;
        }



        // start employee document


        public void refreshDocument()
        {
            getDocData();
            bindDocData1();
        }
        void btnAdd_ClickDoc(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                BarakalAlkheir.Docs.frmDocuments frm = new Docs.frmDocuments();
                frm.documentTypeDefID = int.Parse(lbldocumentTypeDefID.Text);
                frm.document_TypeID = int.Parse(lblID.Text);
                frm.ShowDialog();
                //frm.btnAdd_Click(sender, e);
                refreshDocument();
                //getDocData();
                //bindDocData1();
            }
        }

        void btnEdit_ClickDoc(object sender, EventArgs e)
        {
            if (lblID.Text != "-1" && lblDocumentID.Text != "-1")
            {
                BarakalAlkheir.Docs.frmDocuments frm = new Docs.frmDocuments();
                frm.documentTypeDefID = int.Parse(lbldocumentTypeDefID.Text);
                frm.document_TypeID = int.Parse(lblID.Text);
                frm.documentID = int.Parse(lblDocumentID.Text);
                frm.action = "edit";
                frm.ShowDialog();
                //  frm.btnEdit_Click(sender, e);
                //getDocData();
                //bindDocData1();
                refreshDocument();
            }
        }

        void btnDelete_ClickDoc(object sender, EventArgs e)
        {
            try
            {
                if (this.lblID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(lblDocumentID.Text);
                            Document bb = (from c in db.Documents
                                           where c.documentID ==
                                               bbID
                                           select c).FirstOrDefault();

                            db.Documents.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                getDocData();
                                bindDocData1();
                                refreshDocument();
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

        void btnFirst_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument > 0)
            {
                positionDocument = 0;
                bindDocData1();
            }
        }

        void btnPrevious_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument > 0)
            {
                positionDocument -= 1;
                bindDocData1();
            }
        }

        void btnNext_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument < lstDocument.Count - 1)
            {
                positionDocument += 1;
                bindDocData1();
            }
        }

        void btnLast_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument < lstDocument.Count - 1)
            {
                positionDocument = lstDocument.Count - 1;
                bindDocData1();
            }
        }

        void btnExit_ClickDoc(object sender, EventArgs e)
        {
            Close();
        }


        private void getDocData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocument = new List<Document>();
                lblDocumentID.Text = "-1";
                int licID = int.Parse(lblID.Text);

                var val = (from c in db.DocumentTypeDefinitions
                           where c.documentType_Code == lbldocumentType_Code.Text
                           select c).FirstOrDefault();

                if (val != null)
                    lbldocumentTypeDefID.Text = val.documentTypeDefID.ToString();


                lstDocument = (from c in db.Documents
                               where c.document_TypeID == licID &&
                               c.DocumentDef.documentTypeDefID == 2 &&
                               (c.DocumentDef.documentDef_Name != "الرخصة" &&
                                         c.DocumentDef.documentDef_Name != "رخصة" &&
                                         c.DocumentDef.documentDef_Name != "الرخصة التجارية" &&
                                         c.DocumentDef.documentDef_Name != "رخصة تجارية")
                               select c).ToList();


                var val1 = (from c in db.Documents
                            where c.document_TypeID == licID &&
                            c.DocumentDef.documentTypeDefID ==2 &&
                            (c.DocumentDef.documentDef_Name != "الرخصة" &&
                                         c.DocumentDef.documentDef_Name != "رخصة" &&
                                         c.DocumentDef.documentDef_Name != "الرخصة التجارية" &&
                                         c.DocumentDef.documentDef_Name != "رخصة تجارية")
                            select new
                            {
                                c.documentID,
                                c.DocumentDef.documentDef_Name,
                                c.document_No,
                                c.document_startDate,
                                c.document_endDate
                            }).ToList();

                gvDocument.DataSource = val1;
            }
        }

        private void bindDocData1()
        {
            if (lstDocument.Count > 0)
            {
                if (lblDocumentID.Text == "-1")
                {
                    lblDocumentID.Text = lstDocument[0].documentID.ToString();
                    gvDocument.Rows[0].IsSelected = true;
                }
                else
                {
                    int IDD = int.Parse(lblDocumentID.Text);
                    Document dc = lstDocument.Find(cc => cc.documentID == IDD);

                    positionDocument = lstDocument.FindIndex(cc => cc.documentID == IDD);
                    gvDocument.Rows[positionDocument].IsSelected = true;
                }

                mainbarDocument.lblCount.Text = positionDocument + 1 + " / " + lstDocument.Count;

            }
            else
            {
                lblDocumentID.Text = "-1";
                mainbarDocument.lblCount.Text = " 0 / 0";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //refreshDocument();
        }

        private void gvDocument_Click(object sender, EventArgs e)
        {
            if (gvDocument.Rows.Count > 0)
            {
                lblDocumentID.Text = gvDocument.CurrentRow.Cells[0].Value.ToString();
            }
        }

   

        // end of document



    }
}
