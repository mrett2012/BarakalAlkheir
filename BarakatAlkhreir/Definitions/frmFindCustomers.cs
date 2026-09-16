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
            List<Customer> dd = lstCompanyLicense.FindAll(c => c.customer_Name.Contains(txtcustomer_Name.Text) == true);
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
            List<Customer> dd = lstCompanyLicense.FindAll(c => c.customer_CivilID.Contains(txtcustomer_CivilID.Text) == true);
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
            List<Customer> dd = lstCompanyLicense.FindAll(c => c.customer_phoneNumber1.Contains(txtcustomer_phoneNumber1.Text) == true);
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
            if (GView.SelectedRows.Count > 0)
            {
                cID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                cName = GView.CurrentRow.Cells[1].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                cName = GView.CurrentRow.Cells[1].Value.ToString();
                found = true;
                Close();
            }
        }




    }
}
