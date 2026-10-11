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
    public partial class frmfindLicences1 : Telerik.WinControls.UI.RadForm
    {
        List<CompanyLicence> lstCompanyLicense = null;
        public bool found = false;
        public int licID = -1;
        public string licNO = "";
        public string licName = "";
         

        public bool isCurrentRentedLicences = false;
        public bool isMainLicencesInclude = false;
        public bool isCurrentRentedLicencesInclude = true;

        public bool isAllLicences = false;

        public frmfindLicences1(bool _isCurrentRentedLicences, bool _isMainLicencesInclude)
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
                lstCompanyLicense = (from c in db.CompanyLicences
                                     join d in db.RentLicenses
                                         on c.companyLicencesID equals d.companyLicencesID
                                     where c.isActive == true && d.rentLicense_active==true
                                     select c).ToList();
            }
        }

        private void bindData()
        {
            GView.DataSource = lstCompanyLicense;
        }

        private void txtlicenseAutomatedNumber_TextChanged(object sender, EventArgs e)
        {
            if (lstCompanyLicense == null) return;
            string number = txtlicenseAutomatedNumber.Text.Trim();
            GView.DataSource = number.Length == 0 ? lstCompanyLicense :
                lstCompanyLicense.FindAll(c => (c.licenseAutomatedNumber ?? string.Empty).Contains(number));
        }

        private void txtlicenseNo_TextChanged(object sender, EventArgs e)
       {
            if (lstCompanyLicense == null) return;
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => (c.licenseNo ?? string.Empty).Contains(txtlicenseNo.Text)==true);
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
            if (lstCompanyLicense == null) return;
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => (c.commercialName ?? string.Empty).Contains(txtcommercialName.Text) == true);
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
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;
            SelectCurrentLicence();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectCurrentLicence();
        }

        private void SelectCurrentLicence()
        {
            var row = GView.CurrentRow as Telerik.WinControls.UI.GridViewDataRowInfo;
            int selectedId;
            if (row == null || !int.TryParse(Convert.ToString(row.Cells[0].Value), out selectedId) || selectedId <= 0)
                return;
            licID = selectedId;
            licNO = Convert.ToString(row.Cells[1].Value);
            licName = Convert.ToString(row.Cells[2].Value);
            found = true;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtlicenseLinkCode_TextChanged(object sender, EventArgs e)
        {
            if (lstCompanyLicense == null) return;
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => (c.licenseLinkCode ?? string.Empty).Contains(txtlicenseLinkCode.Text) == true);
            if (txtlicenseLinkCode.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void GView_Click(object sender, EventArgs e)
        {

        }

        private void txtlicensecivilID_TextChanged(object sender, EventArgs e)
        {
            if (lstCompanyLicense == null) return;
            List<CompanyLicence> dd = lstCompanyLicense.FindAll(c => (c.licensecivilID ?? string.Empty).Contains(this.txtlicensecivilID.Text) == true);
            if (txtlicensecivilID.Text.Length > 0)
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
