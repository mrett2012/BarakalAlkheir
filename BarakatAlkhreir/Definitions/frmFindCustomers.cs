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

namespace BarakalAlkheir.Definitions
{
    public partial class frmFindCustomers : Telerik.WinControls.UI.RadForm
    {
        List<Customer> lstCompanyLicense = null;
        public bool found = false;
        public int cID = -1;
        public string cName = "";

        public frmFindCustomers()
        {
            InitializeComponent();
        }

        private void frmFindCustomers_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmFindCustomers_LoadData);
        }

        private async System.Threading.Tasks.Task frmFindCustomers_LoadData(object sender, EventArgs e)
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
                lstCompanyLicense = (from c in db.Customers where c.customer_active == true select c).ToList();

            }
        }

        private void bindData()
        {
            GView.DataSource = lstCompanyLicense;
        }

        private void txtcustomer_Name_TextChanged(object sender, EventArgs e)
        {
            if (lstCompanyLicense == null) return;
            List<Customer> dd = lstCompanyLicense.FindAll(c => (c.customer_Name ?? string.Empty).Contains(txtcustomer_Name.Text) == true);
            if (txtcustomer_Name.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtcustomer_CivilID_TextChanged(object sender, EventArgs e)
        {
            if (lstCompanyLicense == null) return;
            List<Customer> dd = lstCompanyLicense.FindAll(c => (c.customer_CivilID ?? string.Empty).Contains(txtcustomer_CivilID.Text) == true);
            if (txtcustomer_CivilID.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtcustomer_phoneNumber1_TextChanged(object sender, EventArgs e)
        {
            if (lstCompanyLicense == null) return;
            List<Customer> dd = lstCompanyLicense.FindAll(c => (c.customer_phoneNumber1 ?? string.Empty).Contains(txtcustomer_phoneNumber1.Text) == true);
            if (txtcustomer_phoneNumber1.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectCurrentCustomer();
        }

        private void SelectCurrentCustomer()
        {
            var row = GView.CurrentRow as Telerik.WinControls.UI.GridViewDataRowInfo;
            int selectedId;
            if (row == null || !int.TryParse(Convert.ToString(row.Cells[0].Value), out selectedId) || selectedId <= 0)
                return;
            cID = selectedId;
            cName = Convert.ToString(row.Cells[1].Value);
            found = true;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;
            SelectCurrentCustomer();
        }




    }
}
