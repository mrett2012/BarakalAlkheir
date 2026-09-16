using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Definitions;
using BarakalAlkheir.Sponsor;
using BarakalAlkheir.Model;
using System.Linq;

namespace BarakalAlkheir
{
    public partial class frmMain : Telerik.WinControls.UI.RadForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.mainForm = this;

                frmlogin frm = new frmlogin();
                frm.ShowDialog();
                
   
            }
            catch (Exception ex)
            {
                
            }
        }

        

        private void MenuMainDefinitions_Click(object sender, EventArgs e)
        {

        }

        private void MenuBank_Click(object sender, EventArgs e)
        {
            if (MainClass.frmbanks == null || MainClass.frmbanks.IsDisposed)
            {
                MainClass.frmbanks = new frmBanks();
                MainClass.frmbanks.MdiParent = MainClass.mainForm;
               // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmbanks.Show();
            }
            else
            {
                MainClass.frmbanks.Activate();
            }
        }

        private void radMenuItem4_Click(object sender, EventArgs e)
        {
            if (MainClass.frmCars == null || MainClass.frmCars.IsDisposed)
            {
                MainClass.frmCars = new frmCars();
                MainClass.frmCars.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmCars.Show();
            }
            else
            {
                MainClass.frmCars.Activate();
            }
        }

        private void radMenuItem5_Click(object sender, EventArgs e)
        {
            if (MainClass.frmCarsupplier == null || MainClass.frmCarsupplier.IsDisposed)
            {
                MainClass.frmCarsupplier = new frmCarSuppliers();
                MainClass.frmCarsupplier.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmCarsupplier.Show();
            }
            else
            {
                MainClass.frmCarsupplier.Activate();
            }
        }

        private void MenuCompanyInfo_Click(object sender, EventArgs e)
        {
            if (MainClass.frmCompany == null || MainClass.frmCompany.IsDisposed)
            {
                MainClass.frmCompany = new Sponsor.frmCompany();
                MainClass.frmCompany.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmCompany.Show();
            }
            else
            {
                MainClass.frmCompany.Activate();
            }
        }

        private void radMenuItem17_Click(object sender, EventArgs e)
        {
            if (MainClass.frmCustomers == null || MainClass.frmCustomers.IsDisposed)
            {
                MainClass.frmCustomers = new frmCustomers();
                MainClass.frmCustomers.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmCustomers.Show();
            }
            else
            {
                MainClass.frmCustomers.Activate();
            }
        }

        private void radMenuItem18_Click(object sender, EventArgs e)
        {
            if (MainClass.frmDesignations == null || MainClass.frmDesignations.IsDisposed)
            {
                MainClass.frmDesignations = new frmDesignations();
                MainClass.frmDesignations.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmDesignations.Show();
            }
            else
            {
                MainClass.frmDesignations.Activate();
            }
        }

        private void radMenuItem19_Click(object sender, EventArgs e)
        {
            if (MainClass.frmEducation == null || MainClass.frmEducation.IsDisposed)
            {
                MainClass.frmEducation = new frmEducations();
                MainClass.frmEducation.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmEducation.Show();
            }
            else
            {
                MainClass.frmEducation.Activate();
            }
        }

        private void radMenuItem20_Click(object sender, EventArgs e)
        {
            if (MainClass.frmGender == null || MainClass.frmGender.IsDisposed)
            {
                MainClass.frmGender = new frmGender();
                MainClass.frmGender.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmGender.Show();
            }
            else
            {
                MainClass.frmGender.Activate();
            }
        }

        private void radMenuItem21_Click(object sender, EventArgs e)
        {
            if (MainClass.frmMaritalStatus == null || MainClass.frmMaritalStatus.IsDisposed)
            {
                MainClass.frmMaritalStatus = new frmMaritalStatus();
                MainClass.frmMaritalStatus.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmMaritalStatus.Show();
            }
            else
            {
                MainClass.frmMaritalStatus.Activate();
            }
        }

        private void radMenuItem22_Click(object sender, EventArgs e)
        {
            if (MainClass.frmNationality == null || MainClass.frmNationality.IsDisposed)
            {
                MainClass.frmNationality = new frmNationality();
                MainClass.frmNationality.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmNationality.Show();
            }
            else
            {
                MainClass.frmNationality.Activate();
            }
        }

        private void radMenuItem23_Click(object sender, EventArgs e)
        {
            if (MainClass.frmReligions == null || MainClass.frmReligions.IsDisposed)
            {
                MainClass.frmReligions = new frmReligions();
                MainClass.frmReligions.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmReligions.Show();
            }
            else
            {
                MainClass.frmReligions.Activate();
            }
        }

        private void radMenuItem12_Click(object sender, EventArgs e)
        {
            if (MainClass.frmUsergroup == null || MainClass.frmUsergroup.IsDisposed)
            {
                MainClass.frmUsergroup = new frmUserGroup();
                MainClass.frmUsergroup.MdiParent = MainClass.mainForm;
                // MainClass.frmbanks.WindowState = FormWindowState.Maximized;
                MainClass.frmUsergroup.Show();
            }
            else
            {
                MainClass.frmUsergroup.Activate();
            }
        }

        private void radMenuItem11_Click(object sender, EventArgs e)
        {
            if (MainClass.frmuser == null || MainClass.frmuser.IsDisposed)
            {
                MainClass.frmuser = new frmUsers();
                MainClass.frmuser.MdiParent = MainClass.mainForm;
                
                MainClass.frmuser.Show();
                
            }
            else
            {
                MainClass.frmuser.Activate();
            }
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (MainClass.frmForms == null || MainClass.frmForms.IsDisposed)
            {
                MainClass.frmForms = new frmForms();
                MainClass.frmForms.MdiParent = MainClass.mainForm;
                
                MainClass.frmForms.Show();
                MainClass.frmForms.WindowState = FormWindowState.Maximized;
            }
            else
            {
                MainClass.frmForms.Activate();
            }
        }


        private void btnMainDefinitions_Click(object sender, EventArgs e)
        {
            if (MainClass.frmMainDefinitions == null || MainClass.frmMainDefinitions.IsDisposed)
            {
                MainClass.frmMainDefinitions = new frmMainDefinitions();
                MainClass.frmMainDefinitions.MdiParent = MainClass.mainForm;
                
                MainClass.frmMainDefinitions.Show();
                MainClass.frmMainDefinitions.WindowState = FormWindowState.Maximized;
            }
            else
            {
                MainClass.frmMainDefinitions.Activate();
            }
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            if (MainClass.frmCompanyMain == null || MainClass.frmCompanyMain.IsDisposed)
            {
                MainClass.frmCompanyMain = new Sponsor.fromCompanyMain();
                MainClass.frmCompanyMain.MdiParent = MainClass.mainForm;
               
                MainClass.frmCompanyMain.Show();
                //MainClass.frmCompanyMain.WindowState = FormWindowState.Maximized;
            }
            else
            {
                MainClass.frmCompanyMain.Activate();
            }
        }


        private void mnuBank_Click(object sender, EventArgs e)
        {
            frmBanks frm = new frmBanks();
            frm.ShowDialog();
        }

        private void mnuMaritalStatus_Click(object sender, EventArgs e)
        {
            frmMaritalStatus frm = new frmMaritalStatus();
            frm.ShowDialog();
        }

        private void mnuNationality_Click(object sender, EventArgs e)
        {
            frmNationality frm = new frmNationality();
            frm.ShowDialog();
        }

        private void mnuDesignations_Click(object sender, EventArgs e)
        {
            frmDesignations frm = new frmDesignations();
            frm.ShowDialog();
        }

        private void mnuEducations_Click(object sender, EventArgs e)
        {
            frmEducations frm = new frmEducations();
            frm.ShowDialog();
        }

        private void mnuGender_Click(object sender, EventArgs e)
        {
            frmGender frm = new frmGender();
            frm.ShowDialog();
        }

        private void mnuReligion_Click(object sender, EventArgs e)
        {
            frmReligions frm = new frmReligions();
            frm.ShowDialog();
        }

        private void mnuCompany_Click(object sender, EventArgs e)
        {
            frmCompany frm = new frmCompany();
            frm.ShowDialog();
        }

        private void mnuCompanyLicences_Click(object sender, EventArgs e)
        {
            frmCompanyLicences frm = new frmCompanyLicences();
            frm.ShowDialog();
        }

        private void mnuAuthorization_Click(object sender, EventArgs e)
        {
            frmCompanyAuthorization frm = new frmCompanyAuthorization();
            frm.ShowDialog();
        }

        private void mnuLicenseDesignations_Click(object sender, EventArgs e)
        {
            frmLicenseDesignations frm = new frmLicenseDesignations();
            frm.ShowDialog();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            Lic.frmLicenseMainPage frm = new Lic.frmLicenseMainPage();
            frm.ShowDialog();
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            if (MainClass.frmEmplooyeeMain == null || MainClass.frmEmplooyeeMain.IsDisposed)
            {
                MainClass.frmEmplooyeeMain = new emp.frmEmployeeMain();
                MainClass.frmEmplooyeeMain.MdiParent = MainClass.mainForm;

                MainClass.frmEmplooyeeMain.Show();
                MainClass.frmEmplooyeeMain.WindowState = FormWindowState.Maximized;
            }
            else
            {
                MainClass.frmEmplooyeeMain.Activate();
            }
        }





        private void btnDocMainPage_Click(object sender, EventArgs e)
        {
            Docs.frmDocMainPage frm = new Docs.frmDocMainPage();
            frm.ShowDialog();
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            CarsM.frmCarMainPage frm = new CarsM.frmCarMainPage();
            frm.ShowDialog();
        }

        private void radMenuItem14_Click(object sender, EventArgs e)
        {
            Close();
        }






    }
}
