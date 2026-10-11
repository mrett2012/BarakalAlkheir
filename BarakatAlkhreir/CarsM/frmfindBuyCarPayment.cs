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

namespace BarakalAlkheir.CarsM
{
    public partial class frmfindBuyCarPayment : Telerik.WinControls.UI.RadForm
    {
        public bool found = false;
        public int licID = -1;
        public string licNO = "";
        public string licName = "";

        public frmfindBuyCarPayment()
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
                getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void getData(string _voucher, string _linO, string _LNo, string _Cname)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.CarSupplierRentPayments
                           join cc in db.CarSupplierRents on c.carSupplierRentID equals cc.carSupplierRentID
                           join ccc in db.Cars on cc.carID equals ccc.carID
                           where c.carSupplierRentPayment_VoucherNo.Contains(_voucher) &&
                           ccc.car_Brand.Contains(_Cname) &&
                           ccc.car_Name.Contains(_LNo) &&
                           ccc.car_No.Contains(_linO)
                           select new
                           {
                               c.carSupplierRentPaymentID,
                               c.carSupplierRentPayment_amount,
                               c.carSupplierRentPayment_VoucherNo,
                               ccc.car_No,
                               ccc.car_Name,
                               ccc.car_Brand

                           }).ToList();

                GView.Rows.Clear();
                foreach (var item in val)
                {
                    int r = GView.Rows.Count;
                    GView.Rows.Add(1);
                    GView.Rows[r].Cells[0].Value = item.carSupplierRentPaymentID.ToString();
                    GView.Rows[r].Cells[1].Value = item.carSupplierRentPayment_VoucherNo;
                    GView.Rows[r].Cells[2].Value = item.car_No;
                    GView.Rows[r].Cells[3].Value = item.car_Name;
                    GView.Rows[r].Cells[4].Value = item.car_Brand;
                    GView.Rows[r].Cells[5].Value = item.carSupplierRentPayment_amount.Value.ToString();
                }

            }// end of using
        }

        private void txtlicenseNo_TextChanged(object sender, EventArgs e)
        {
           getData(txtVoucherNo.Text,txtlicenseNo.Text,txtlicenseLinkCode.Text,txtcommercialName.Text);
        }

        private void txtcommercialName_TextChanged(object sender, EventArgs e)
        {
            getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
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

            getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
        }

        private void txtVoucherNo_TextChanged(object sender, EventArgs e)
        {
            getData(txtVoucherNo.Text, txtlicenseNo.Text, txtlicenseLinkCode.Text, txtcommercialName.Text);
            
        }





    }
}
