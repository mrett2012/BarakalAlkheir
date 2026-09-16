using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BarakalAlkheir.Sponsor
{
    public partial class fromCompanyMain : Telerik.WinControls.UI.RadForm
    {
        public fromCompanyMain()
        {
            InitializeComponent();
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            frmCompany frm = new frmCompany();
            frm.ShowDialog();
        }

        private void btnCompanyLicences_Click(object sender, EventArgs e)
        {
            frmCompanyLicences frm = new frmCompanyLicences();
            frm.ShowDialog();
        }

        private void btnDesignations_Click(object sender, EventArgs e)
        {
            frmCompanyAuthorization frm = new frmCompanyAuthorization();
            frm.ShowDialog();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            frmLicenseDesignations frm = new frmLicenseDesignations();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            BarakalAlkheir.Definitions.frmLicenseActivity frm = new Definitions.frmLicenseActivity();
            frm.ShowDialog();
        }




    }
}
