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

namespace BarakalAlkheir.emp
{
    public partial class frmFindEmployees : Telerik.WinControls.UI.RadForm
    {
        List<Employee> lstEmployee = null;
        List<Employee> findEmployee = null;
        public bool found = false;
        public int empID = -1;
        public string empNO = "";
        public string empName = "";

        bool isLoaded = false;
        public frmFindEmployees()
        {
            InitializeComponent();
        }

        private void frmFindEmployees_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmFindEmployees_LoadData);
        }

        private async System.Threading.Tasks.Task frmFindEmployees_LoadData(object sender, EventArgs e)
        {
            try
            {
                await PublicClass.bindNationalityAsync(cmbnationalityID);
                await PublicClass.bindGendersAsync(cmbGender);
                cmbGender.SelectedIndex = 0;
                cmbnationalityID.SelectedIndex = 0;

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
                bindData();
                isLoaded = true;
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
                lstEmployee = (from c in db.Employees.Include("Nationality").Include("Gender") select c).ToList();
                findEmployee = lstEmployee;
            }
        }

        private void bindData()
        {
            //lstEmployee[0].emp_isMandoub
            GView.DataSource = findEmployee;
        }


        private void searchData()
        {
            if (isLoaded)
            {
                findEmployee = lstEmployee;
                findEmployee = findEmployee.Where(c => c.emp_No.Contains(txtemp_No.Text) == true).ToList();
                findEmployee = findEmployee.Where(c => c.emp_civilID.Contains(txtemp_civilID.Text) == true).ToList();
                findEmployee = findEmployee.Where(c => c.emp_namefullar.Contains(txtemp_namefullar.Text) == true).ToList();
                if (cmbGender.SelectedIndex != 0)
                {
                    int gID = int.Parse(cmbGender.SelectedValue.ToString());
                    findEmployee = findEmployee.Where(c => c.genderID == gID).ToList();
                }
                if (cmbnationalityID.SelectedIndex != 0)
                {
                    int nID = int.Parse(cmbnationalityID.SelectedValue.ToString());
                    findEmployee = findEmployee.Where(c => c.nationalityID == nID).ToList();
                }
                if (chkemp_active.Checked)
                {
                    findEmployee = findEmployee.Where(c => c.emp_active == chkemp_active.Checked).ToList();
                }
                if (chkemp_isMandoub.Checked)
                {
                    findEmployee = findEmployee.Where(c => c.emp_isMandoub == chkemp_isMandoub.Checked).ToList();
                }
                if (chkemp_Notactive.Checked)
                {
                    findEmployee = findEmployee.Where(c => c.emp_active == false).ToList();
                }

                bindData();
            }
        }

        private void txtemp_No_TextChanged(object sender, EventArgs e)
        {
            searchData();
        }

        private void txtemp_civilID_TextChanged(object sender, EventArgs e)
        {
            searchData();
        }

        private void txtemp_namefullar_TextChanged(object sender, EventArgs e)
        {
            searchData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbGender_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            searchData();
        }

        private void cmbnationalityID_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            searchData();
        }

        private void chkemp_isMandoub_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            searchData();
        }

        private void chkemp_active_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            searchData();
        }

        private void chkemp_Notactive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            searchData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                findtheData();
            }
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                findtheData();
            }
        }

        private void findtheData()
        {
            empID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
            empNO = GView.CurrentRow.Cells[1].Value.ToString();
            empName = GView.CurrentRow.Cells[2].Value.ToString();
            found = true;
            Close();
        }


    }
}
