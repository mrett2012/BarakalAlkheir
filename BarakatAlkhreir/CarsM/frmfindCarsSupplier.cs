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

namespace BarakalAlkheir.CarsM
{
    public partial class frmfindCarsSupplier : Telerik.WinControls.UI.RadForm
    {
        List<CarSupllier> lstCarSupllier = null;
        public bool found = false;

        public int carSuppliserID = -1;
        public string carSuppliser_PersonInCharge = "";
        public string carSuppliser_Name = "";
         

        public frmfindCarsSupplier()
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
                
                    lstCarSupllier = (from c in db.CarSuplliers
                                         select c).ToList();               
            }
        }

        private void bindData()
        {
            GView.DataSource = lstCarSupllier;
        }

        private void txtlicenseNo_TextChanged(object sender, EventArgs e)
       {
            List<CarSupllier> dd = lstCarSupllier.FindAll(c => c.carSuppliser_Name.Contains(txtcarSuppliser_Name.Text)==true);
            if (txtcarSuppliser_Name.Text.Length > 0)
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
            List<CarSupllier> dd = lstCarSupllier.FindAll(c => c.carSuppliser_PersonInCharge.Contains(this.txtcarSuppliser_PersonInCharge.Text) == true);
            if (this.txtcarSuppliser_PersonInCharge.Text.Length > 0)
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
                carSuppliserID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                carSuppliser_PersonInCharge = GView.CurrentRow.Cells[2].Value.ToString();
                carSuppliser_Name = GView.CurrentRow.Cells[1].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                carSuppliserID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                carSuppliser_PersonInCharge = GView.CurrentRow.Cells[2].Value.ToString();
                carSuppliser_Name = GView.CurrentRow.Cells[1].Value.ToString();
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
            List<CarSupllier> dd = lstCarSupllier.FindAll(c => c.carSuppliser_Contact.Contains(txtcarSuppliser_Contact.Text) == true);
            if (txtcarSuppliser_Contact.Text.Length > 0)
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
