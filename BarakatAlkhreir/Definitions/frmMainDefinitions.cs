using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BarakalAlkheir.Definitions
{
    public partial class frmMainDefinitions : Telerik.WinControls.UI.RadForm
    {
        public frmMainDefinitions()
        {
            InitializeComponent();
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            frmBanks frm = new frmBanks();
            frm.ShowDialog();
        }

        private void btnDesignations_Click(object sender, EventArgs e)
        {
            frmDesignations frm = new frmDesignations();
            frm.ShowDialog();
        }

        private void btnEducations_Click(object sender, EventArgs e)
        {
            frmEducations frm = new frmEducations();
            frm.ShowDialog();
        }

        private void btnGender_Click(object sender, EventArgs e)
        {
            frmGender frm = new frmGender();
            frm.ShowDialog();
        }

        private void btnMaritalStatus_Click(object sender, EventArgs e)
        {
            frmMaritalStatus frm = new frmMaritalStatus();
            frm.ShowDialog();
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {

            frmNationality frm = new frmNationality();
            frm.ShowDialog();
        }

        private void btnReligions_Click(object sender, EventArgs e)
        {
         
                frmReligions frm = new frmReligions();
                frm.ShowDialog();
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDocumentTypeDefinitions_Click(object sender, EventArgs e)
        {
            Docs.frmDocGroupTypeDef frm = new Docs.frmDocGroupTypeDef();
            frm.ShowDialog();
        }

        private void btnDocDef_Click(object sender, EventArgs e)
        {
            Docs.frmDocDef frm = new Docs.frmDocDef();
            frm.ShowDialog();
        }

        private void btnGovernorate_Click(object sender, EventArgs e)
        {
            frmGovernorate frm = new frmGovernorate();
            frm.ShowDialog();
        }

        private void btnGovernorateArea_Click(object sender, EventArgs e)
        {
            frmGovernateArea frm = new frmGovernateArea();
            frm.ShowDialog();
        }

        private void btnEmployeeStatus_Click(object sender, EventArgs e)
        {
            frmEmployeeStatusDef frm = new frmEmployeeStatusDef();
            frm.ShowDialog();
        }


        private void btnForms_Click(object sender, EventArgs e)
        {
            frmForms frm = new frmForms();
            frm.ShowDialog();
        }

        private void radPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFormEvents_Click(object sender, EventArgs e)
        {
            frmFormEvents frm = new frmFormEvents();
            frm.ShowDialog();
        }

        private void btnUsersPermission_Click(object sender, EventArgs e)
        {
            frmUsersFormEvents frm = new frmUsersFormEvents();
            frm.ShowDialog();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            frmTransactionDef frm = new frmTransactionDef();
            frm.ShowDialog();
        }

        private void btnTransactionCost_Click(object sender, EventArgs e)
        {
           Docs.frmDocumentTransactions frm = new Docs.frmDocumentTransactions();
            frm.ShowDialog();
        }

        private void btnEmployeeRawabet_Click(object sender, EventArgs e)
        {
            frmEmployeeRabet frm = new frmEmployeeRabet();
            frm.ShowDialog();
        }

    }
}
