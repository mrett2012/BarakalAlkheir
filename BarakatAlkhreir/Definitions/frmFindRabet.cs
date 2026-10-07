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
    public partial class frmFindRabet : Telerik.WinControls.UI.RadForm
    {
        List<EmployeeRabet> lstCompanyLicense = null;
        public bool found = false;
        public int cID = -1;
        public string cName = "";

        public frmFindRabet()
        {
            InitializeComponent();
        }

        private void frmFindEmployeeRabets_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmFindEmployeeRabets_LoadData);
        }

        private async System.Threading.Tasks.Task frmFindEmployeeRabets_LoadData(object sender, EventArgs e)
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
                lstCompanyLicense = (from c in db.EmployeeRabets orderby c.rabet_namea select c).ToList();

            }
        }

        private void bindData()
        {
            GView.DataSource = lstCompanyLicense;
        }

        private void txtEmployeeRabet_Name_TextChanged(object sender, EventArgs e)
        {
            List<EmployeeRabet> dd = lstCompanyLicense.FindAll(c => c.rabet_namea.Contains(txtrabet_namea.Text) == true);
            if (txtrabet_namea.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtEmployeeRabet_CivilID_TextChanged(object sender, EventArgs e)
        {
            List<EmployeeRabet> dd = lstCompanyLicense.FindAll(c => c.rabet_description.Contains(txtrabet_description.Text) == true);
            if (txtrabet_description.Text.Length > 0)
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
