using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using BarakalAlkheir.Model;

namespace BarakalAlkheir.Sponsor
{
    public partial class frmfindLicences : Telerik.WinControls.UI.RadForm
    {
        List<CompanyLicence> lstCompanyLicense = null;
        public bool found = false;
        public int licID = -1;
        public string licNO = "";
        public string licName = "";
        public string licenseLinkCode = "";
         

        public bool isCurrentRentedLicences = false;
        public bool isMainLicencesInclude = false;
        public bool isCurrentRentedLicencesInclude = true;

        public bool isAllLicences = false;

        public frmfindLicences(bool _isCurrentRentedLicences, bool _isMainLicencesInclude)
        {
            InitializeComponent();
        }

        private void frmfindLicences_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmfindLicences_LoadData);
        }

        private async System.Threading.Tasks.Task frmfindLicences_LoadData(object sender, EventArgs e)
        {
            try
            {
                await PublicClass.bindGovernorateAsync(txtMohafaza);
                await PublicClass.bindGovernorateAreaAsync(txtMantaqa, -1);
                //txtMantaqa.SelectedIndex = 0;

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
                bindData();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                if (isAllLicences)
                {
                    lstCompanyLicense = (from c in db.CompanyLicences
                                         select c).ToList();
                }
                else if (isCurrentRentedLicences)
                {
                    if (isMainLicencesInclude)
                    {
                        lstCompanyLicense = (from c in db.CompanyLicences
                                             join d in db.RentLicenses
                                                 on c.companyLicencesID equals d.companyLicencesID
                                             where c.isActive == true && d.rentLicense_active == true
                                             select c).ToList();
                    }
                    else
                    {
                        lstCompanyLicense = (from c in db.CompanyLicences
                                             join d in db.RentLicenses
                                                 on c.companyLicencesID equals d.companyLicencesID
                                             where c.isActive == true && d.rentLicense_active == true
                                             && c.isMainLicences == false
                                             select c).ToList();
                    }
                }
                else
                {
                    if (isMainLicencesInclude)
                    {
                        //lstCompanyLicense = (from c in db.CompanyLicences where c.isActive == true select c).ToList();

                        if (isCurrentRentedLicencesInclude)
                        {
                            lstCompanyLicense = (from c in db.CompanyLicences
                                                 where c.isActive == true
                                                 && c.isMainLicences == true
                                                 select c).ToList();
                        }
                        else
                        {

                            lstCompanyLicense = (from c in db.CompanyLicences
                                                 where !(from cc in db.CompanyLicences
                                                         join d in db.RentLicenses
                                                              on cc.companyLicencesID equals d.companyLicencesID
                                                         where d.rentLicense_active == true
                                                         select cc.companyLicencesID).Contains(c.companyLicencesID)
                                                       && c.isActive == true
                                                 && c.isMainLicences == true
                                                 select c).ToList();
                        }
                    }
                    else
                    {
                        if (isCurrentRentedLicencesInclude)
                        {
                            lstCompanyLicense = (from c in db.CompanyLicences
                                                 where c.isActive == true
                                                 && c.isMainLicences == false
                                                 select c).ToList();
                        }
                        else
                        {

                            lstCompanyLicense = (from c in db.CompanyLicences
                                                 where !(from cc in db.CompanyLicences
                                                    join d in db.RentLicenses
                                                         on cc.companyLicencesID equals d.companyLicencesID
                                                        where d.rentLicense_active == true select cc.companyLicencesID).Contains(c.companyLicencesID)
                                                       && c.isActive == true
                                                 && c.isMainLicences == false
                                                 select c).ToList();
                        }
                    }
                }
                
            }
        }

        private void bindData()
        {
            GView.DataSource = lstCompanyLicense;
        }

        private void txtlicenseNo_TextChanged(object sender, EventArgs e)
       {
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.licenseNo.Contains(txtlicenseNo.Text)==true);
            if (txtlicenseNo.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtcommercialName_TextChanged(object sender, EventArgs e)
        {
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.commercialName.Contains(txtcommercialName.Text) == true);
            if (txtcommercialName.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                licID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                licNO = GView.CurrentRow.Cells[1].Value.ToString();
                licName = GView.CurrentRow.Cells[3].Value.ToString();
                licenseLinkCode = GView.CurrentRow.Cells[3].Value.ToString();

                found = true;
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                licID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                licNO = GView.CurrentRow.Cells[1].Value.ToString();
                licName = GView.CurrentRow.Cells[3].Value.ToString();
                licenseLinkCode = GView.CurrentRow.Cells[2].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtlicenseLinkCode_TextChanged(object sender, EventArgs e)
        {
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.licenseLinkCode.Contains(txtlicenseLinkCode.Text) == true);
            if (txtlicenseLinkCode.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtlicensecivilID_TextChanged(object sender, EventArgs e)
        {
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.licensecivilID.Contains(this.txtlicensecivilID.Text) == true);
            if (txtlicensecivilID.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtMohafaza_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            int _governorateID = int.Parse(txtMohafaza.SelectedValue.ToString());
            PublicClass.bindGovernorateArea(txtMantaqa, _governorateID);
            txtMantaqa.SelectedIndex = 0;

            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.Mohafaza.Contains(this.txtMohafaza.Text) == true);
            if (txtMohafaza.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtMantaqa_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.Mantaqa.Contains(this.txtMantaqa.Text) == true);
            if (txtMantaqa.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtlicenseUniqueNo_TextChanged(object sender, EventArgs e)
        {
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => c.licenseUniqueNo.Contains(this.txtlicenseUniqueNo.Text) == true);
            if (txtlicenseUniqueNo.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }





    }
}
