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
                licName = GView.CurrentRow.Cells[2].Value.ToString();
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
                licName = GView.CurrentRow.Cells[2].Value.ToString();
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

        private void GView_Click(object sender, EventArgs e)
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





    }
}
