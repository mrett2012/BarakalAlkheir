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
using System.Collections;

namespace BarakalAlkheir.Lic
{
    public partial class frmfindLicencesPayment : Telerik.WinControls.UI.RadForm
    {
        public bool found = false;
        public int licID = -1;
        public string licNO = "";
        public string licName = "";

        public frmfindLicencesPayment()
        {
            InitializeComponent();
        }

        private void frmfindLicences_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmfindLicences_LoadData);
        }

        private void frmfindLicences_LoadData(object sender, EventArgs e)
        {
            try
            {
                //getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void getData(string _voucher, string _linO, string _LNo, string _Cname)
        {
            bool filterByVoucher = !string.IsNullOrEmpty(_voucher);
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.RentLicensesDetails.Where(d => d.rd_isPaid == true)
                           join cc in db.RentLicenses on c.rentLicenseID equals cc.rentLicenseID
                           join ccc in db.CompanyLicences on cc.companyLicencesID equals ccc.companyLicencesID
                           where (!filterByVoucher || c.rentLicPay_VouNo.Contains(_voucher)) &&
                           ccc.commercialName.Contains(_Cname) &&
                           ccc.licenseLinkCode.Contains(_LNo) &&
                           ccc.licenseNo.Contains(_linO)
                           orderby c.rd_fromDate
                           select new
                           {
                               c.rentLicencesDetailsID,
                               c.rd_amount,
                               c.rentLicPay_VouNo,
                               rd_fromDate = c.rd_fromDate,
                               ccc.licenseNo,
                               ccc.licenseLinkCode,
                               ccc.commercialName

                           }).ToList();

                GView.Rows.Clear();
                foreach (var item in val)
                {
                    int r = GView.Rows.Count;
                    GView.Rows.Add(1);
                    GView.Rows[r].Cells[0].Value = item.rentLicencesDetailsID.ToString();
                    GView.Rows[r].Cells[1].Value = item.rentLicPay_VouNo;
                    GView.Rows[r].Cells[2].Value = item.licenseLinkCode;
                    GView.Rows[r].Cells[3].Value = item.licenseNo;
                    GView.Rows[r].Cells[4].Value = item.rd_fromDate.HasValue ? LicenseRentalRules.FormatDate(LicenseRentalRules.BillingDate(item.rd_fromDate.Value)) : "";
                    
                    GView.Rows[r].Cells[5].Value = item.rd_amount.Value.ToString();
                    GView.Rows[r].Cells[6].Value = item.commercialName;
                    
                }

            }// end of using
        }

        private void txtlicenseNo_TextChanged(object sender, EventArgs e)
        {
           //getData(txtVoucherNo.Text,txtlicenseNo.Text,txtlicenseLinkCode.Text,txtcommercialName.Text);
        }

        private void txtcommercialName_TextChanged(object sender, EventArgs e)
        {
            //getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                licID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                found = true;
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                licID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
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

            //getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
        }

        private void txtVoucherNo_TextChanged(object sender, EventArgs e)
        {
           // getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtVoucherNo.Text.Length>1 ||
                txtlicenseNo.Text.Length > 1 ||
                txtlicenseLinkCode.Text.Length > 1 ||
                txtcommercialName.Text.Length > 1)
            getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
        }





    }
}
