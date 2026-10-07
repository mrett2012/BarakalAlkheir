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
    public partial class frmFindLicencesRemarks : Telerik.WinControls.UI.RadForm
    {
        List<CompanyLicencesRemark> lstCompanyLicenseRe = null;
        public int ID = -1;
        public bool found = false;

        public frmFindLicencesRemarks()
        {
            InitializeComponent();
        }

        private void btnFindLicenses_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, false);
            frm.isMainLicencesInclude = false;
            frm.ShowDialog();
            if (frm.found)
            {
                lblcompanyLicencesID.Text = frm.licID.ToString();
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var cc = (from c in db.CompanyLicences
                              where c.companyLicencesID == frm.licID
                              select c).ToList();
                    if (cc.Count > 0)
                    {
                        txtcommercialName.Text = cc[0].commercialName;
                        txtlicenseUniqueNo.Text = cc[0].licenseUniqueNo;
                        getData();
                        bindData();
                    }
                }
            }
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {

                var val = (from c in db.CompanyLicencesRemarks.Include("CompanyLicence")
                                       select c).ToList();

                if (txtcomLicenseRemID.Text.Length > 0)
                {
                    int ID = 0;
                    if (int.TryParse(txtcomLicenseRemID.Text, out ID))
                    {
                        val = val.Where(cc => cc.comLicenseRemID == ID).ToList();
                    }
                }

                if (lblcompanyLicencesID.Text != "-1")
                {
                    int ID = 0;
                    if (int.TryParse(lblcompanyLicencesID.Text, out ID))
                    {
                        val = val.Where(cc => cc.companyLicencesID == ID).ToList();
                    }
                }

                lstCompanyLicenseRe = val.ToList();
            }
        }

        private void bindData()
        {
            GView.DataSource = lstCompanyLicenseRe;
        }

        private void frmFindLicencesRemarks_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmFindLicencesRemarks_LoadData);
        }

        private void frmFindLicencesRemarks_LoadData(object sender, EventArgs e)
        {
            try
            {
                getData();
                bindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                ID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                found = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtcomLicenseRemID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                getData();
                bindData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


    }
}
