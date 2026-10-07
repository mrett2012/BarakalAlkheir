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
using BarakalAlkheir.emp;
using BarakalAlkheir.Sponsor;
using System.Transactions;


namespace BarakalAlkheir.emp
{
    public partial class frmEmployees : Telerik.WinControls.UI.RadForm
    {
        string change1 = "";
        int position1 = 0;
        List<Employee> lstEmployee = null;
        components.banner mainbaremployee = new components.banner("pageEmployee");
        components.banner mainbarAddress = new components.banner("pageAddress");
        components.banner mainbaroldSponsor = new components.banner("pageOldSponsor");

        components.banner mainbarUpdatePrint = new components.banner("pageUpdatePrint");
        components.banner mainbarSponsor = new components.banner("pageSponsor");

        components.banner mainbarPassport = new components.banner("pagePassport");


        components.banner mainbarsSalary = new components.banner("pageSalary");
        components.banner mainbarWorkContract = new components.banner("pageWorkContract");
        components.banner mainbarDependent = new components.banner("pageDependent");
        components.banner mainbarLicenseShop = new components.banner("PageShop");
        components.banner mainbarDocument = new components.banner("PageEmployeeDocument");


        string changeAddress = "";
        string changeOldSponsor = "";
        string changeUpdatePrint="";

        string changeSponsor = "";
        string changePassport = "";

        string changeSalary = "";
        string changeWorkContract = "";
        string changeDependent = "";

        string changeShop = "";

        List<EmployeeSponsor> sponsor = null;
        List<EmployeePassport> passport = null;
        List<EmployeeSalary> lstsalary = null;
        List<EmployeeWorkContract> lstworkContract = null;
        List<Dependent> lstDependent = null;
        List<Document> lstDocument = null;

        List<EmployeeLicenseShop> lstEmployeeLicenseShop = null;
        int positionDependent = 0;
        int positionDocument = 0;

        public int _employeeID = -1;

        public frmEmployees()
        {
            InitializeComponent();
        }

        private void radPageView1_PageRemoving(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void getMaxEmpNo()
        {
            string temp = "";

            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = db.Employees.ToList();
                    List<int> empNo1 = new List<int>();

                    foreach (var item in val)
                    {
                        empNo1.Add(int.Parse(item.emp_No));
                    }

                    empNo1.Sort();
                    lblmaxEmpNo.Text = empNo1[empNo1.Count - 1].ToString();
                }
            }
            catch
            {
            }

            //lblmaxEmpNo.Text = temp;
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmEmployees_LoadData);
        }

        private async System.Threading.Tasks.Task frmEmployees_LoadData(object sender, EventArgs e)
        {
            try
            {

                mainbaremployee.Dock = DockStyle.Top;
                await mainbaremployee.AddToAsync(pageEmployee);
                
                mainbarAddress.Dock = DockStyle.Top;
                await mainbarAddress.AddToAsync(pageAddress);

                mainbaroldSponsor.Dock = DockStyle.Top;
                await mainbaroldSponsor.AddToAsync(pageOldSponsor);


                this.mainbarUpdatePrint.Dock = DockStyle.Top;
                await mainbarUpdatePrint.AddToAsync(pageUpdatePrint);

                mainbarSponsor.Dock = DockStyle.Top;
                await mainbarSponsor.AddToAsync(pageSponsor);

                mainbarPassport.Dock = DockStyle.Top;
                await mainbarPassport.AddToAsync(PagePassport);

                mainbarsSalary.Dock = DockStyle.Top;
                await mainbarsSalary.AddToAsync(pageSalary);

                mainbarWorkContract.Dock = DockStyle.Top;
                await mainbarWorkContract.AddToAsync(pageWorkContract);

                mainbarLicenseShop.Dock = DockStyle.Top;
                await mainbarLicenseShop.AddToAsync(PageShop);


                mainbarDependent.Dock = DockStyle.Top;
                await mainbarDependent.AddToAsync(pageDependent);
                
                mainbarDocument.Dock = DockStyle.Top;
                await mainbarDocument.AddToAsync(PageEmployeeDocument);

                mainbaremployee.btnAdd.Click += new EventHandler(btnAdd_Click1);
                mainbaremployee.btnEdit.Click += new EventHandler(btnEdit_Click1);
                mainbaremployee.btnDelete.Click += new EventHandler(btnDelete_Click1);
                mainbaremployee.btnCancel.Click += new EventHandler(btnCancel_Click1);
                mainbaremployee.btnSave.Click += new EventHandler(btnSave_Click1);
                mainbaremployee.btnFind.Click += new EventHandler(btnFind_Click1);
                mainbaremployee.btnFirst.Click += new EventHandler(btnFirst_Click1);
                mainbaremployee.btnPrevious.Click += new EventHandler(btnPrevious_Click1);
                mainbaremployee.btnNext.Click += new EventHandler(btnNext_Click1);
                mainbaremployee.btnLast.Click += new EventHandler(btnLast_Click1);
                mainbaremployee.btnExit.Click += new EventHandler(btnExit_Click1);
                //mainbaremployee.btnFind.Visible = false;


                mainbarAddress.btnEdit.Click += new EventHandler(btnEdit_Click2);
                mainbarAddress.btnCancel.Click += new EventHandler(btnCancel_Click2);
                mainbarAddress.btnSave.Click += new EventHandler(btnSave_Click2);
                mainbarAddress.btnExit.Click += new EventHandler(btnExit_Click2);

                mainbarAddress.btnAdd.Visible = false;
                mainbarAddress.btnDelete.Visible = false;
                mainbarAddress.btnFind.Visible = false;
                mainbarAddress.btnFirst.Visible = false;
                mainbarAddress.btnLast.Visible = false;
                mainbarAddress.btnNext.Visible = false;
                mainbarAddress.btnPrevious.Visible = false;
                mainbarAddress.btnPrint.Visible = false;
                mainbarAddress.lblCount.Visible = false;


                mainbaroldSponsor.btnEdit.Click += new EventHandler(btnEdit_ClickOldSponsor);
                mainbaroldSponsor.btnCancel.Click += new EventHandler(btnCancel_ClickOldSponsor);
                mainbaroldSponsor.btnSave.Click += new EventHandler(btnSave_ClickOldSponsor);
                mainbaroldSponsor.btnExit.Click += new EventHandler(btnExit_ClickOldSponsor);

                mainbaroldSponsor.btnAdd.Visible = false;
                mainbaroldSponsor.btnDelete.Visible = false;
                mainbaroldSponsor.btnFind.Visible = false;
                mainbaroldSponsor.btnFirst.Visible = false;
                mainbaroldSponsor.btnLast.Visible = false;
                mainbaroldSponsor.btnNext.Visible = false;
                mainbaroldSponsor.btnPrevious.Visible = false;
                mainbaroldSponsor.btnPrint.Visible = false;
                mainbaroldSponsor.lblCount.Visible = false;




                this.mainbarUpdatePrint.btnEdit.Click += new EventHandler(btnEdit_Click222);
                mainbarUpdatePrint.btnCancel.Click += new EventHandler(btnCancel_Click222);
                mainbarUpdatePrint.btnSave.Click += new EventHandler(btnSave_Click222);
                mainbarUpdatePrint.btnExit.Click += new EventHandler(btnExit_Click222);

                mainbarUpdatePrint.btnAdd.Visible = false;
                mainbarUpdatePrint.btnDelete.Visible = false;
                mainbarUpdatePrint.btnFind.Visible = false;
                mainbarUpdatePrint.btnFirst.Visible = false;
                mainbarUpdatePrint.btnLast.Visible = false;
                mainbarUpdatePrint.btnNext.Visible = false;
                mainbarUpdatePrint.btnPrevious.Visible = false;
                mainbarUpdatePrint.btnPrint.Visible = false;
                mainbarUpdatePrint.lblCount.Visible = false;



                mainbarSponsor.btnEdit.Click += new EventHandler(btnEdit_Click3);
                mainbarSponsor.btnCancel.Click += new EventHandler(btnCancel_Click3);
                mainbarSponsor.btnSave.Click += new EventHandler(btnSave_Click3);
                mainbarSponsor.btnExit.Click += new EventHandler(btnExit_Click3);
                mainbarSponsor.btnAdd.Click += new EventHandler(btnAdd_Click3);
                mainbarSponsor.btnDelete.Click += new EventHandler(btnDelete_Click3);


                mainbarPassport.btnEdit.Click += new EventHandler(btnEdit_ClickPassport);
                mainbarPassport.btnCancel.Click += new EventHandler(btnCancel_ClickPassport);
                mainbarPassport.btnSave.Click += new EventHandler(btnSave_ClickPassport);
                mainbarPassport.btnExit.Click += new EventHandler(btnExit_ClickPassport);
                mainbarPassport.btnAdd.Click += new EventHandler(btnAdd_ClickPassport);
                mainbarPassport.btnDelete.Click += new EventHandler(btnDelete_ClickPassport);


                //mainbarSponsor.btnAdd.Visible = false;
                //mainbarSponsor.btnDelete.Visible = false;
                mainbarSponsor.btnFind.Visible = false;
                mainbarSponsor.btnFirst.Visible = false;
                mainbarSponsor.btnLast.Visible = false;
                mainbarSponsor.btnNext.Visible = false;
                mainbarSponsor.btnPrevious.Visible = false;
                mainbarSponsor.btnPrint.Visible = false;
                mainbarSponsor.lblCount.Visible = false;

                mainbarPassport.btnFind.Visible = false;
                mainbarPassport.btnFirst.Visible = false;
                mainbarPassport.btnLast.Visible = false;
                mainbarPassport.btnNext.Visible = false;
                mainbarPassport.btnPrevious.Visible = false;
                mainbarPassport.btnPrint.Visible = false;
                mainbarPassport.lblCount.Visible = false;


                mainbarsSalary.btnEdit.Click += new EventHandler(btnEdit_ClickS);
                mainbarsSalary.btnCancel.Click += new EventHandler(btnCancel_ClickS);
                mainbarsSalary.btnSave.Click += new EventHandler(btnSave_ClickS);
                mainbarsSalary.btnExit.Click += new EventHandler(btnExit_ClickS);

                mainbarsSalary.btnAdd.Click += new EventHandler(btnAdd_ClickS);
                mainbarsSalary.btnDelete.Click += new EventHandler(btnDelete_ClickS);
                //mainbarsSalary.btnAdd.Visible = false;
                //mainbarsSalary.btnDelete.Visible = false;

                mainbarsSalary.btnFind.Visible = false;
                mainbarsSalary.btnFirst.Visible = false;
                mainbarsSalary.btnLast.Visible = false;
                mainbarsSalary.btnNext.Visible = false;
                mainbarsSalary.btnPrevious.Visible = false;
                mainbarsSalary.btnPrint.Visible = false;
                mainbarsSalary.lblCount.Visible = false;


                mainbarWorkContract.btnEdit.Click += new EventHandler(btnEdit_ClickW);
                mainbarWorkContract.btnCancel.Click += new EventHandler(btnCancel_ClickW);
                mainbarWorkContract.btnSave.Click += new EventHandler(btnSave_ClickW);
                mainbarWorkContract.btnExit.Click += new EventHandler(btnExit_ClickW);

                mainbarWorkContract.btnAdd.Click += new EventHandler(btnAdd_ClickW);
                mainbarWorkContract.btnDelete.Click += new EventHandler(btnDelete_ClickW);

                //mainbarWorkContract.btnAdd.Visible = false;
                //mainbarWorkContract.btnDelete.Visible = false;

                mainbarWorkContract.btnFind.Visible = false;
                mainbarWorkContract.btnFirst.Visible = false;
                mainbarWorkContract.btnLast.Visible = false;
                mainbarWorkContract.btnNext.Visible = false;
                mainbarWorkContract.btnPrevious.Visible = false;
                mainbarWorkContract.btnPrint.Visible = false;
                mainbarWorkContract.lblCount.Visible = false;


                mainbarLicenseShop.btnEdit.Click += new EventHandler(btnEdit_ClickSH);
                mainbarLicenseShop.btnCancel.Click += new EventHandler(btnCancel_ClickSH);
                mainbarLicenseShop.btnSave.Click += new EventHandler(btnSave_ClickSH);
                mainbarLicenseShop.btnExit.Click += new EventHandler(btnExit_ClickSH);

                mainbarLicenseShop.btnAdd.Click += new EventHandler(btnAdd_ClickSH);
                mainbarLicenseShop.btnDelete.Click += new EventHandler(btnDelete_ClickSH);

                //mainbarLicenseShop.btnAdd.Visible = false;
                //mainbarLicenseShop.btnDelete.Visible = false;
                mainbarLicenseShop.btnFind.Visible = false;
                mainbarLicenseShop.btnFirst.Visible = false;
                mainbarLicenseShop.btnLast.Visible = false;
                mainbarLicenseShop.btnNext.Visible = false;
                mainbarLicenseShop.btnPrevious.Visible = false;
                mainbarLicenseShop.btnPrint.Visible = false;
                mainbarLicenseShop.lblCount.Visible = false;


                mainbarDependent.btnAdd.Click += new EventHandler(btnAdd_ClickD);
                mainbarDependent.btnEdit.Click += new EventHandler(btnEdit_ClickD);
                mainbarDependent.btnDelete.Click += new EventHandler(btnDelete_ClickD);
                mainbarDependent.btnCancel.Click += new EventHandler(btnCancel_ClickD);
                mainbarDependent.btnSave.Click += new EventHandler(btnSave_ClickD);
                mainbarDependent.btnFirst.Click += new EventHandler(btnFirst_ClickD);
                mainbarDependent.btnPrevious.Click += new EventHandler(btnPrevious_ClickD);
                mainbarDependent.btnNext.Click += new EventHandler(btnNext_ClickD);
                mainbarDependent.btnLast.Click += new EventHandler(btnLast_ClickD);
                mainbarDependent.btnExit.Click += new EventHandler(btnExit_ClickD);
                mainbarDependent.btnFind.Visible = false;

                mainbarDocument.btnAdd.Click += new EventHandler(btnAdd_ClickDoc);
                mainbarDocument.btnEdit.Click += new EventHandler(btnEdit_ClickDoc);
                mainbarDocument.btnDelete.Click += new EventHandler(btnDelete_ClickDoc);

                mainbarDocument.btnFirst.Click += new EventHandler(btnFirst_ClickDoc);
                mainbarDocument.btnPrevious.Click += new EventHandler(btnPrevious_ClickDoc);
                mainbarDocument.btnNext.Click += new EventHandler(btnNext_ClickDoc);
                mainbarDocument.btnLast.Click += new EventHandler(btnLast_ClickDoc);
                mainbarDocument.btnExit.Click += new EventHandler(btnExit_ClickDoc);
                mainbarDocument.btnSave.Visible = false;
                mainbarDocument.btnCancel.Visible = false;
                mainbarDocument.btnFind.Visible = false;
               

                disableControls1();
                disableAddressControls2();
                disableAddressControls222();
                disableAddressControlsOldSponsor();

                // load data into employees
                await PublicClass.bindEducationsAsync(cmbeducationID);
                await PublicClass.bindGendersAsync(cmbGender);
                await PublicClass.bindEmployeeStatusAsync(cmbEmploeeStatus);
                await PublicClass.bindNationalityAsync(cmbnationality_placeOfBirthID);
                await PublicClass.bindNationalityAsync(cmbnationalityID);
                await PublicClass.bindMaritalStatusAsync(cmbmaritalID);
                await PublicClass.bindReligionsAsync(cmbreligionID);
                await PublicClass.bindCompanyAsync(cmbcompanyID);
                await PublicClass.bindDesignationsAsync(cmbdesignationID);
                await PublicClass.bindBanksAsync(cmbbankID);

                await PublicClass.bindGendersAsync(txtdepend_gender);
                await PublicClass.bindNationalityAsync(txtdepend_nationality);
                await PublicClass.bindReligionsAsync(txtdepend_religion);
                await PublicClass.bindNationalityAsync(cmbpass_placeOfIssue);

                await PublicClass.bindGovernorateAsync(txtemployeeSponsor_Shou2oun);
                txtemployeeSponsor_Shou2oun.SelectedIndex = 0;

                await PublicClass.bindGovernorateAsync(txtemployeeSponsor_Jawazat);
                txtemployeeSponsor_Jawazat.SelectedIndex = 0;

                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData1);
                if (_employeeID != -1)
                {
                    position1 = lstEmployee.FindIndex(cc => cc.employeeID == _employeeID);
                }
                bindData1();


                disableAddressControls3();
                disableSalaryControlsS();
                disableWCControlsS();
                disableDepControls1();
                disableSHControlsS();
                disablePassportControls3();

                //txtemployeeSponsor_Shou2oun.Enabled = false;
                


                //getData3();
                //bindSponsorData3();

                //getDataS();
                //bindSalaryDataS();

                //getDataWC();
                //bindWCData();

                //getDepData();
                //bindDepData1();

                //getDataSH();
                //bindSHData();

                //getDocData();
                //bindDocData1();

                pageEmployee.Select();
               // timer1.Start();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void disableAllPages()
        {
            pageAddress.Enabled = false;
            pageUpdatePrint.Enabled = false;
            pageDependent.Enabled = false;
            pageEmployee.Enabled = false;
            pageSalary.Enabled = false;
            pageSponsor.Enabled = false;
            PageShop.Enabled = false;
            pageWorkContract.Enabled = false;
            PageEmployeeDocument.Enabled = false;
            pageWorkContract.Enabled = false;
            PagePassport.Enabled = false;
            pageOldSponsor.Enabled = false;
        }

        private void enableAllPages()
        {
            pageAddress.Enabled = true;
            pageUpdatePrint.Enabled = true;
            pageDependent.Enabled = true;
            pageEmployee.Enabled = true;
            pageSalary.Enabled = true;
            pageSponsor.Enabled = true;
            PageShop.Enabled = true;
            pageWorkContract.Enabled = true;
            PageEmployeeDocument.Enabled = true;
            pageWorkContract.Enabled = true;
            PagePassport.Enabled = true;
            pageOldSponsor.Enabled = true;
        }

        // start employee data 
        void btnAdd_Click1(object sender, EventArgs e)
        {
            change1 = "add";
            enableControls1();

            txtemp_civilID.Text = "";
            txtemp_day.Text = "";
            txtemp_month.Text = "";
            txtemp_namea1.Text = "";
            txtemp_namea2.Text = "";
            txtemp_namea3.Text = "";
            txtemp_namea4.Text = "";
            txtemp_namea5.Text = "";
            txtemp_namee1.Text = "";
            txtemp_namee2.Text = "";
            txtemp_namee3.Text = "";
            txtemp_namee4.Text = "";
            txtemp_namee5.Text = "";
            txtemp_namefullar.Text = "";
            txtemp_namefullen.Text = "";
            txtemp_No.Text = "";
            txtemp_year.Text = "";

            cmbeducationID.SelectedIndex = 0;
            cmbemp_bloodType.SelectedIndex = 0;
            cmbGender.SelectedIndex = 0;
            cmbEmploeeStatus.SelectedIndex = 0;
            cmbmaritalID.SelectedIndex = 0;
            cmbnationality_placeOfBirthID.SelectedIndex = 0;
            cmbnationalityID.SelectedIndex = 0;
            cmbreligionID.SelectedIndex = 0;
            chkemp_active.Checked = true;
            chkemp_isMandoub.Checked = false;

            //// clear address data
            txtMohafaza.Text = "";
            txtJada.Text = "";
            txtQuet3a.Text = "";
            txtShare3.Text = "";
            txtBuilduingType.Text = "";
            txtfloor.Text = "";
            txtappartment.Text = "";
            txtpobox.Text = "";
            txtpoboxOffice.Text = "";
            txtemail.Text = "";
            txthomeNo.Text = "";
            txtphoneNo.Text = "";
            txtfulladdress.Text = "";

        }

        void btnEdit_Click1(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                change1 = "edit";
                enableControls1();
            }
        }

        void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(lblEmployeeID.Text);
                            Employee bb = (from c in db.Employees
                                           where c.employeeID ==
                                               bbID
                                           select c).FirstOrDefault();

                            db.Employees.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                position1 = 0;
                                getData1();
                                bindData1();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnCancel_Click1(object sender, EventArgs e)
        {
            change1 = "";
            disableControls1();
            bindData1();
        }

        void btnSave_Click1(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                if (change1 == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        Employee bb = new Employee();

                        bb.emp_civilID = txtemp_civilID.Text;
                        bb.emp_day = txtemp_day.Text;
                        bb.emp_month = txtemp_month.Text;
                        bb.emp_namea1 = txtemp_namea1.Text;
                        bb.emp_namea2 = txtemp_namea2.Text;
                        bb.emp_namea3 = txtemp_namea3.Text;
                        bb.emp_namea4 = txtemp_namea4.Text;
                        bb.emp_namea5 = txtemp_namea5.Text;
                        bb.emp_namee1 = txtemp_namee1.Text;
                        bb.emp_namee2 = txtemp_namee2.Text;
                        bb.emp_namee3 = txtemp_namee3.Text;
                        bb.emp_namee4 = txtemp_namee4.Text;
                        bb.emp_namee5 = txtemp_namee5.Text;
                        bb.emp_namefullar = txtemp_namefullar.Text;
                        bb.emp_namefullen = txtemp_namefullen.Text;
                        bb.emp_No = txtemp_No.Text;
                        bb.emp_fileNo = txtemp_No.Text;
                        bb.emp_year = txtemp_year.Text;

                        bb.educationID = int.Parse(cmbeducationID.SelectedValue.ToString());
                        bb.emp_bloodType = cmbemp_bloodType.Text;
                        bb.genderID = int.Parse(cmbGender.SelectedValue.ToString());
                        bb.employeeStatusID = int.Parse(cmbEmploeeStatus.SelectedValue.ToString());
                        bb.maritalID = int.Parse(cmbmaritalID.SelectedValue.ToString());
                        bb.nationality_placeOfBirthID = int.Parse(cmbnationality_placeOfBirthID.SelectedValue.ToString());
                        bb.nationalityID = int.Parse(cmbnationalityID.SelectedValue.ToString());
                        bb.religionID = int.Parse(cmbreligionID.SelectedValue.ToString());

                        bb.departmentID = -1;
                        bb.companyID = -1;

                        bb.emp_active = chkemp_active.Checked;
                        bb.emp_isMandoub = chkemp_isMandoub.Checked;

                        bb.Mohafaza = "";
                        bb.Jada = "";
                        bb.Quet3a = "";
                        bb.Share3 = "";
                        bb.BuilduingType = "";
                        bb.floor = "";
                        bb.appartment = "";
                        bb.pobox = "";
                        bb.poboxOffice = "";
                        bb.email = "";
                        bb.homeNo = "";
                        bb.phoneNo = "";
                        bb.fulladdress = "";

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change1 == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblEmployeeID.Text);
                        Employee bb = (from c in db.Employees
                                       where c.employeeID ==
                                           bbID
                                       select c).FirstOrDefault();

                        bb.emp_civilID = txtemp_civilID.Text;
                        bb.emp_day = txtemp_day.Text;
                        bb.emp_month = txtemp_month.Text;
                        bb.emp_namea1 = txtemp_namea1.Text;
                        bb.emp_namea2 = txtemp_namea2.Text;
                        bb.emp_namea3 = txtemp_namea3.Text;
                        bb.emp_namea4 = txtemp_namea4.Text;
                        bb.emp_namea5 = txtemp_namea5.Text;
                        bb.emp_namee1 = txtemp_namee1.Text;
                        bb.emp_namee2 = txtemp_namee2.Text;
                        bb.emp_namee3 = txtemp_namee3.Text;
                        bb.emp_namee4 = txtemp_namee4.Text;
                        bb.emp_namee5 = txtemp_namee5.Text;
                        bb.emp_namefullar = txtemp_namefullar.Text;
                        bb.emp_namefullen = txtemp_namefullen.Text;
                        bb.emp_No = txtemp_No.Text;
                        bb.emp_year = txtemp_year.Text;

                        bb.educationID = int.Parse(cmbeducationID.SelectedValue.ToString());
                        bb.emp_bloodType = cmbemp_bloodType.Text;
                        bb.genderID = int.Parse(cmbGender.SelectedValue.ToString());
                        bb.employeeStatusID = int.Parse(cmbEmploeeStatus.SelectedValue.ToString());
                        bb.maritalID = int.Parse(cmbmaritalID.SelectedValue.ToString());
                        bb.nationality_placeOfBirthID = int.Parse(cmbnationality_placeOfBirthID.SelectedValue.ToString());
                        bb.nationalityID = int.Parse(cmbnationalityID.SelectedValue.ToString());
                        bb.religionID = int.Parse(cmbreligionID.SelectedValue.ToString());
                        bb.emp_active = chkemp_active.Checked;
                        bb.emp_isMandoub = chkemp_isMandoub.Checked;


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls1();

                    getData1();
                    if (change1 == "add")
                    {
                        position1 = lstEmployee.Count - 1;
                    }
                    change1 = "";

                    bindData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnFind_Click1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            frmFindEmployees frm = new frmFindEmployees();
            frm.ShowDialog();
            if (frm.found)
            {
                position1 = lstEmployee.FindIndex(c => c.employeeID == frm.empID);
                bindData1();
            }
        }

        void btnFirst_Click1(object sender, EventArgs e)
        {
            if (position1 > 0)
            {
                position1 = 0;
                bindData1();
            }
        }

        void btnPrevious_Click1(object sender, EventArgs e)
        {
            if (position1 > 0)
            {
                position1 -= 1;
                bindData1();
            }
        }

        void btnNext_Click1(object sender, EventArgs e)
        {
            if (position1 < lstEmployee.Count - 1)
            {
                position1 += 1;
                bindData1();
            }
        }

        void btnLast_Click1(object sender, EventArgs e)
        {
            if (position1 < lstEmployee.Count - 1)
            {
                position1 = lstEmployee.Count - 1;
                bindData1();
            }
        }

        void btnExit_Click1(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls1()
        {

            mainbaremployee.enableButtons();

            txtemp_civilID.ReadOnly = true;
            txtemp_day.ReadOnly = true;
            txtemp_month.ReadOnly = true;
            txtemp_namea1.ReadOnly = true;
            txtemp_namea2.ReadOnly = true;
            txtemp_namea3.ReadOnly = true;
            txtemp_namea4.ReadOnly = true;
            txtemp_namea5.ReadOnly = true;
            txtemp_namee1.ReadOnly = true;
            txtemp_namee2.ReadOnly = true;
            txtemp_namee3.ReadOnly = true;
            txtemp_namee4.ReadOnly = true;
            txtemp_namee5.ReadOnly = true;
            txtemp_namefullar.ReadOnly = true;
            txtemp_namefullen.ReadOnly = true;
            txtemp_No.ReadOnly = true;
            txtemp_year.ReadOnly = true;

            cmbeducationID.Enabled = false;
            cmbemp_bloodType.Enabled = false;
            cmbGender.Enabled = false;
            cmbEmploeeStatus.Enabled = false;
            cmbmaritalID.Enabled = false;
            cmbnationality_placeOfBirthID.Enabled = false;
            cmbnationalityID.Enabled = false;
            cmbreligionID.Enabled = false;

            chkemp_active.Enabled = false;
            chkemp_isMandoub.Enabled = false;
           
            enableAllPages();
        }

        private void enableControls1()
        {
            mainbaremployee.disableButton();

            txtemp_civilID.ReadOnly = false;
            txtemp_day.ReadOnly = false;
            txtemp_month.ReadOnly = false;
            txtemp_namea1.ReadOnly = false;
            txtemp_namea2.ReadOnly = false;
            txtemp_namea3.ReadOnly = false;
            txtemp_namea4.ReadOnly = false;
            txtemp_namea5.ReadOnly = false;
            txtemp_namee1.ReadOnly = false;
            txtemp_namee2.ReadOnly = false;
            txtemp_namee3.ReadOnly = false;
            txtemp_namee4.ReadOnly = false;
            txtemp_namee5.ReadOnly = false;
            //txtemp_namefullar.ReadOnly = true;
            //txtemp_namefullen.ReadOnly = true;
            txtemp_No.ReadOnly = false;
            txtemp_year.ReadOnly = false;

            cmbeducationID.Enabled = true;
            cmbemp_bloodType.Enabled = true;
            cmbGender.Enabled = true;
            cmbEmploeeStatus.Enabled = true;
            cmbmaritalID.Enabled = true;
            cmbnationality_placeOfBirthID.Enabled = true;
            cmbnationalityID.Enabled = true;
            cmbreligionID.Enabled = true;
            chkemp_active.Enabled = true;
            chkemp_isMandoub.Enabled = true;
            disableAllPages();
            pageEmployee.Enabled = true;
        }

        private void getData1()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstEmployee = (from c in db.Employees select c).ToList();
            }
        }

        private void bindData1()
        {
            disableAllPrinting();

            if (lstEmployee.Count > 0)
            {
                cmbeducationID.SelectedIndex = 0;
                cmbGender.SelectedIndex = 0;
                cmbEmploeeStatus.SelectedIndex = 0;
                cmbmaritalID.SelectedIndex = 0;
                cmbnationality_placeOfBirthID.SelectedIndex = 0;
                cmbnationalityID.SelectedIndex = 0;
                cmbreligionID.SelectedIndex = 0;
                
                txtemp_civilID.Text = lstEmployee[position1].emp_civilID;
                txtemp_day.Text = lstEmployee[position1].emp_day;
                txtemp_month.Text = lstEmployee[position1].emp_month;
                txtemp_namea1.Text = lstEmployee[position1].emp_namea1;
                txtemp_namea2.Text = lstEmployee[position1].emp_namea2;
                txtemp_namea3.Text = lstEmployee[position1].emp_namea3;
                txtemp_namea4.Text = lstEmployee[position1].emp_namea4;
                txtemp_namea5.Text = lstEmployee[position1].emp_namea5;
                txtemp_namee1.Text = lstEmployee[position1].emp_namee1;
                txtemp_namee2.Text = lstEmployee[position1].emp_namee2;
                txtemp_namee3.Text = lstEmployee[position1].emp_namee3;
                txtemp_namee4.Text = lstEmployee[position1].emp_namee4;
                txtemp_namee5.Text = lstEmployee[position1].emp_namee5;
                txtemp_namefullar.Text = lstEmployee[position1].emp_namefullar;
                lblEmployeeName1.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName2.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName3.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName4.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName5.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName6.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName7.Text = lstEmployee[position1].emp_namefullar;
                //lblEmployeeName8.Text = lstEmployee[position1].emp_namefullar;
                txtemp_namefullen.Text = lstEmployee[position1].emp_namefullen;
                txtemp_No.Text = lstEmployee[position1].emp_No;
                txtemp_year.Text = lstEmployee[position1].emp_year;


                chkemp_active.Checked = lstEmployee[position1].emp_active.Value;
                chkemp_isMandoub.Checked = lstEmployee[position1].emp_isMandoub.Value;

                cmbemp_bloodType.Text = lstEmployee[position1].emp_bloodType;

                for (int i = 0; i < cmbeducationID.Items.Count; i++)
                {
                    if (cmbeducationID.Items[i].Value.ToString() == lstEmployee[position1].educationID.Value.ToString())
                    {
                        cmbeducationID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbGender.Items.Count; i++)
                {
                    if (cmbGender.Items[i].Value.ToString() == lstEmployee[position1].genderID.Value.ToString())
                    {
                        cmbGender.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < this.cmbEmploeeStatus.Items.Count; i++)
                {
                    if (cmbEmploeeStatus.Items[i].Value.ToString() == lstEmployee[position1].employeeStatusID.Value.ToString())
                    {
                        cmbEmploeeStatus.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbmaritalID.Items.Count; i++)
                {
                    if (cmbmaritalID.Items[i].Value.ToString() == lstEmployee[position1].maritalID.Value.ToString())
                    {
                        cmbmaritalID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbnationality_placeOfBirthID.Items.Count; i++)
                {
                    if (cmbnationality_placeOfBirthID.Items[i].Value.ToString() == lstEmployee[position1].nationality_placeOfBirthID.Value.ToString())
                    {
                        cmbnationality_placeOfBirthID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbnationalityID.Items.Count; i++)
                {
                    if (cmbnationalityID.Items[i].Value.ToString() == lstEmployee[position1].nationalityID.Value.ToString())
                    {
                        cmbnationalityID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbreligionID.Items.Count; i++)
                {
                    if (cmbreligionID.Items[i].Value.ToString() == lstEmployee[position1].religionID.Value.ToString())
                    {
                        cmbreligionID.SelectedIndex = i;
                        break;
                    }
                }

                
                lblEmployeeID.Text = lstEmployee[position1].employeeID.ToString();
                mainbaremployee.lblCount.Text = position1 + 1 + " / " + lstEmployee.Count;

                getPrintingData(lstEmployee[position1].employeeID);
                bindAddressData2();
                bindAddressData222();
                bindAddressDataOldSponsor();
            }
            else
            {
                txtemp_civilID.Text = "";
                txtemp_day.Text = "";
                txtemp_month.Text = "";
                txtemp_namea1.Text = "";
                txtemp_namea2.Text = "";
                txtemp_namea3.Text = "";
                txtemp_namea4.Text = "";
                txtemp_namea5.Text = "";
                txtemp_namee1.Text = "";
                txtemp_namee2.Text = "";
                txtemp_namee3.Text = "";
                txtemp_namee4.Text = "";
                txtemp_namee5.Text = "";
                txtemp_namefullar.Text = "";
                txtemp_namefullen.Text = "";
                lblEmployeeName1.Text = "";
                //lblEmployeeName2.Text = "";
                //lblEmployeeName3.Text = "";
                //lblEmployeeName4.Text = "";
                //lblEmployeeName5.Text = "";
                //lblEmployeeName6.Text = "";
                //lblEmployeeName7.Text = "";
                //lblEmployeeName8.Text = "";
                txtemp_No.Text = "";
                txtemp_year.Text = "";

                cmbeducationID.SelectedIndex = 0;
                cmbemp_bloodType.SelectedIndex = 0;
                cmbGender.SelectedIndex = 0;
                cmbEmploeeStatus.SelectedIndex = 0;
                cmbmaritalID.SelectedIndex = 0;
                cmbEmploeeStatus.SelectedIndex = 0;
                cmbnationality_placeOfBirthID.SelectedIndex = 0;
                cmbnationalityID.SelectedIndex = 0;
                cmbreligionID.SelectedIndex = 0;
                chkemp_active.Checked = true;
                chkemp_isMandoub.Checked = false;

                lblEmployeeID.Text = "-1";
                mainbaremployee.lblCount.Text = " 0 / 0";
            }

            getPassport3();
            bindPassportData3();

            getData3();
            bindSponsorData3();

            getDataS();
            bindSalaryDataS();

            getDataWC();
            bindWCData();

            getDepData();
            bindDepData1();

            getDataSH();
            bindSHData();

            getDocData();
            bindDocData1();
            getMaxEmpNo();
        }

        private void txtemp_namea1_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullar.Text = fullArabicName();
        }

        private void txtemp_namea4_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullar.Text = fullArabicName();
        }

        private void txtemp_namea3_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullar.Text = fullArabicName();
        }

        private void txtemp_namea2_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullar.Text = fullArabicName();
        }

        private void txtemp_namea5_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullar.Text = fullArabicName();
        }

        private string fullArabicName()
        {
            return txtemp_namea1.Text + ' ' + txtemp_namea2.Text +' '+
                    txtemp_namea3.Text + ' ' + txtemp_namea4.Text + ' ' + txtemp_namea5.Text;
        }

        private string fullEnglishName()
        {
            return txtemp_namee1.Text + ' ' + txtemp_namee2.Text +' '+
                    txtemp_namee3.Text + ' ' + txtemp_namee4.Text + ' ' + txtemp_namee5.Text;
        }

        private void txtemp_namee1_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullen.Text = fullEnglishName();
        }

        private void txtemp_namee2_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullen.Text = fullEnglishName();
        }

        private void txtemp_namee3_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullen.Text = fullEnglishName();
        }

        private void txtemp_namee4_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullen.Text = fullEnglishName();
        }

        private void txtemp_namee5_TextChanged(object sender, EventArgs e)
        {
            txtemp_namefullen.Text = fullEnglishName();
        }

        // end of employee data


        // start employee address

        void btnEdit_Click2(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                changeAddress = "edit";
                enableAddressControls2();
            }
        }

        void btnCancel_Click2(object sender, EventArgs e)
        {
            changeAddress = "";
            disableAddressControls2();
            bindAddressData2();
        }

        void btnSave_Click2(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeAddress == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblEmployeeID.Text);
                        Employee bb = (from c in db.Employees
                                       where c.employeeID ==
                                           bbID
                                       select c).FirstOrDefault();

                        bb.Mohafaza = txtMohafaza.Text;
                        bb.Jada = txtJada.Text;
                        bb.Quet3a = txtQuet3a.Text;
                        bb.Share3 = txtShare3.Text;
                        bb.BuilduingType = txtBuilduingType.Text;
                        bb.floor = txtfloor.Text;
                        bb.appartment = txtappartment.Text;
                        bb.pobox = txtpobox.Text;
                        bb.poboxOffice = txtpoboxOffice.Text;
                        bb.email = txtemail.Text;
                        bb.homeNo = txthomeNo.Text;
                        bb.phoneNo = txtphoneNo.Text;
                        bb.fulladdress = txtfulladdress.Text;


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    changeAddress = "";
                    disableAddressControls2();
                    getData1();
                    bindAddressData2();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_Click2(object sender, EventArgs e)
        {
            Close();
        }

        private void disableAddressControls2()
        {

            mainbarAddress.enableButtons();

            txtMohafaza.ReadOnly = true;
            txtJada.ReadOnly = true;
            txtQuet3a.ReadOnly = true;
            txtShare3.ReadOnly = true;
            txtBuilduingType.ReadOnly = true;
            txtfloor.ReadOnly = true;
            txtappartment.ReadOnly = true;
            txtpobox.ReadOnly = true;
            txtpoboxOffice.ReadOnly = true;
            txtemail.ReadOnly = true;
            txthomeNo.ReadOnly = true;
            txtphoneNo.ReadOnly = true;
            txtfulladdress.ReadOnly = true;
            enableAllPages();
        }

        private void enableAddressControls2()
        {
            mainbarAddress.disableButton();

            txtMohafaza.ReadOnly = false;
            txtJada.ReadOnly = false;
            txtQuet3a.ReadOnly = false;
            txtShare3.ReadOnly = false;
            txtBuilduingType.ReadOnly = false;
            txtfloor.ReadOnly = false;
            txtappartment.ReadOnly = false;
            txtpobox.ReadOnly = false;
            txtpoboxOffice.ReadOnly = false;
            txtemail.ReadOnly = false;
            txthomeNo.ReadOnly = false;
            txtphoneNo.ReadOnly = false;
            txtfulladdress.ReadOnly = false;
            disableAllPages();
            pageAddress.Enabled = true;
        }

        private void bindAddressData2()
        {
            if (lstEmployee.Count > 0)
            {
                txtMohafaza.Text = lstEmployee[position1].Mohafaza;
                txtJada.Text = lstEmployee[position1].Jada;
                txtQuet3a.Text = lstEmployee[position1].Quet3a;
                txtShare3.Text = lstEmployee[position1].Share3;
                txtBuilduingType.Text = lstEmployee[position1].BuilduingType;
                txtfloor.Text = lstEmployee[position1].floor;
                txtappartment.Text = lstEmployee[position1].appartment;
                txtpobox.Text = lstEmployee[position1].pobox;
                txtpoboxOffice.Text = lstEmployee[position1].poboxOffice;
                txtemail.Text = lstEmployee[position1].email;
                txthomeNo.Text = lstEmployee[position1].homeNo;
                txtphoneNo.Text = lstEmployee[position1].phoneNo;
                txtfulladdress.Text = lstEmployee[position1].fulladdress;
            }
            else
            {
                txtMohafaza.Text = "";
                txtJada.Text = "";
                txtQuet3a.Text = "";
                txtShare3.Text = "";
                txtBuilduingType.Text = "";
                txtfloor.Text = "";
                txtappartment.Text = "";
                txtpobox.Text = "";
                txtpoboxOffice.Text = "";
                txtemail.Text = "";
                txthomeNo.Text = "";
                txtphoneNo.Text = "";
                txtfulladdress.Text = "";
            }
        }


        // end of employee address


        // start employee OldSponsor

        void btnEdit_ClickOldSponsor(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                changeOldSponsor = "edit";
                enableAddressControlsOldSponsor();
            }
        }

        void btnCancel_ClickOldSponsor(object sender, EventArgs e)
        {
            changeOldSponsor = "";
            disableAddressControlsOldSponsor();
            bindAddressDataOldSponsor();
        }

        void btnSave_ClickOldSponsor(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeOldSponsor == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblEmployeeID.Text);
                        Employee bb = (from c in db.Employees
                                       where c.employeeID ==
                                           bbID
                                       select c).FirstOrDefault();


                        bb.emp_OldNoOfYears = this.txtOldNoOfYears.Text;
                        bb.oldoldAuthName = this.txtOldOldAuth.Text;
                        bb.oldoldCommercialName = this.txtOldOldCommercialName.Text;
                        bb.oldoldFileNo = this.txtOldOldFileNo.Text;
                        bb.oldoldGoveContract = this.txtOldOldGoveComtract.Text;
                        bb.oldoldLicenceNO = this.txtOldOldLicenceNo.Text;
                        bb.oldoladOwnerName = this.txtOldOldSponsorName.Text;
                        bb.emp_OldDesignation = this.txtOldSponsorDesignation.Text;
                        bb.emp_OldSalary = this.txtOldSponsorSalary.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    changeOldSponsor = "";
                    disableAddressControlsOldSponsor();
                    getData1();
                    bindAddressDataOldSponsor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_ClickOldSponsor(object sender, EventArgs e)
        {
            Close();
        }

        private void disableAddressControlsOldSponsor()
        {

            this.mainbaroldSponsor.enableButtons();

            this.txtOldNoOfYears.Enabled = false;
            this.txtOldOldAuth.Enabled = false;
            this.txtOldOldCommercialName.Enabled = false;
            this.txtOldOldFileNo.Enabled = false;
            this.txtOldOldGoveComtract.Enabled = false;
            this.txtOldOldLicenceNo.Enabled = false;
            this.txtOldOldSponsorName.Enabled = false;
            this.txtOldSponsorDesignation.Enabled = false;
            this.txtOldSponsorSalary.Enabled = false;

            enableAllPages();
        }

        private void enableAddressControlsOldSponsor()
        {
            this.mainbaroldSponsor.disableButton();

            this.txtOldNoOfYears.Enabled = true;
            this.txtOldOldAuth.Enabled = true;
            this.txtOldOldCommercialName.Enabled = true;
            this.txtOldOldFileNo.Enabled = true;
            this.txtOldOldGoveComtract.Enabled = true;
            this.txtOldOldLicenceNo.Enabled = true;
            this.txtOldOldSponsorName.Enabled = true;
            this.txtOldSponsorDesignation.Enabled = true;
            this.txtOldSponsorSalary.Enabled = true;

            disableAllPages();
            this.pageOldSponsor.Enabled = true;
        }

        private void bindAddressDataOldSponsor()
        {
            if (lstEmployee.Count > 0)
            {
                this.txtOldNoOfYears.Text = lstEmployee[position1].emp_OldNoOfYears;
                this.txtOldOldAuth.Text = lstEmployee[position1].oldoldAuthName;
                this.txtOldOldCommercialName.Text = lstEmployee[position1].oldoldCommercialName;
                this.txtOldOldFileNo.Text = lstEmployee[position1].oldoldFileNo;
                this.txtOldOldGoveComtract.Text = lstEmployee[position1].oldoldGoveContract;
                this.txtOldOldLicenceNo.Text = lstEmployee[position1].oldoldLicenceNO;
                this.txtOldOldSponsorName.Text = lstEmployee[position1].oldoladOwnerName;
                this.txtOldSponsorDesignation.Text = lstEmployee[position1].emp_OldDesignation;
                this.txtOldSponsorSalary.Text = lstEmployee[position1].emp_OldSalary;
            }
            else
            {
                this.txtOldNoOfYears.Text = "";
                this.txtOldOldAuth.Text = "";
                this.txtOldOldCommercialName.Text = "";
                this.txtOldOldFileNo.Text = "";
                this.txtOldOldGoveComtract.Text = "";
                this.txtOldOldLicenceNo.Text = "";
                this.txtOldOldSponsorName.Text = "";
                this.txtOldSponsorDesignation.Text = "";
                this.txtOldSponsorSalary.Text = "";
      
                
            }
        }


        // end of employee address




        // start employee address

        void btnEdit_Click222(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                changeUpdatePrint = "edit";
                enableAddressControls222();
            }
        }

        void btnCancel_Click222(object sender, EventArgs e)
        {
            changeUpdatePrint = "";
            disableAddressControls222();
            bindAddressData222();
        }

        void btnSave_Click222(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeUpdatePrint == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblEmployeeID.Text);
                        Employee bb = (from c in db.Employees
                                       where c.employeeID ==
                                           bbID
                                       select c).FirstOrDefault();

                            string r1 = "";
                            string r2 = "";
                            string r3 = "";
                            string r4 = "";
                            string r5 = "";
                            string r6 = "";
                            string r7 = "";
                            string r8 = "";
                            string r9 = "";
                            string r10 = "";
                            string r11 = "";
                            string r12 = "";
                            string r13 = "";
                            string r14 = "";
                            string r15 = "";
                            string r16 = "";
                            string r17 = "";
                            string r18 = "";
                            string r19 = "";
                            string r20 = "";
                            string r21 = "";
                            string r22 = "";
                            string r23 = "";
                            string r24 = "";
                            string r25 = "";
                            string r26 = "";
                            string r27 = "";
                            string r28 = "";

                            if (raaa1.Checked)
                            {
                                r1 = "X";
                            }
                            if (raaa2.Checked)
                            {
                                r2 = "X";
                            }
                            if (raaa3.Checked)
                            {
                                r3 = "X";
                            }

                            r4 = cmbType1.Text;
                            r5 = cmbType2.Text;
                            r6 = cmbType3.Text;
                            r7 = cmdMohahaza.Text;
                            r8 = cmbActionType.Text;

                            //if (choose1.Checked)
                            //{
                            //    r1 = "X";
                            //}
                            //if (choose2.Checked)
                            //{
                            //    r2 = "X";
                            //}
                            //if (choose3.Text.Length > 0)
                            //{
                            //    r3 = choose3.Text;
                            //}
                            

                            //if (choose4.Checked)
                            //{
                            //    r4 = "X";
                            //}
                            //if (choose5.Checked)
                            //{
                            //    r5 = "X";
                            //}
                            //if (choose6.Checked)
                            //{
                            //    r6 = "X";
                            //}
                            
                            //if (choose7.Checked)
                            //{
                            //    r7 = "X";
                            //}
                            //if (choose8.Checked)
                            //{
                            //    r8 = "X";
                            //}
                            if (choose9.Checked)
                            {
                                r9 = "X";
                            }
                            if (choose10.Checked)
                            {
                                r10 = "X";
                            }
                            if (choose11.Checked)
                            {
                                r11 = "X";
                            } if (choose12.Checked)
                            {
                                r12 = "X";
                            }
                            if (choose13.Checked)
                            {
                                r13 = "X";
                            } if (choose14.Checked)
                            {
                                r14 = "X";
                            }
                            if (choose15.Checked)
                            {
                                r15 = "X";
                            }
                            if (choose16.Checked)
                            {
                                r16 = "X";
                            }
                            if (choose17.Checked)
                            {
                                r17 = "X";
                            }
                            if (choose18.Checked)
                            {
                                r18 = "X";
                            }
                            if (choose19.Checked)
                            {
                                r19 = "X";
                            } if (choose20.Checked)
                            {
                                r20 = "X";
                            } if (choose21.Checked)
                            {
                                r21 = "X";
                            } if (choose22.Checked)
                            {
                                r22 = "X";
                            }
                            if (choose23.Checked)
                            {
                                r23 = "X";
                            } if (choose24.Checked)
                            {
                                r24 = "X";
                            }
                            if (choose25.Checked)
                            {
                                r25 = "X";
                            }
                            if (choose26.Checked)
                            {
                                r26 = "X";
                            }
                            if (choose27.Checked)
                            {
                                r27 = "X";
                            }
                            //if (choose28.Checked)
                            //{
                            //    r28 = "X";
                            //}


                        bb.emp_appDate = AppDateFormat.Format(txtAppDate.Value);
                        bb.emp_appType = txtAppType.Text;
                        bb.emp_res1 = r1;
                        bb.emp_res2 = r2;
                        bb.emp_res3 = r3;
                        bb.emp_res4 = r4;
                        bb.emp_res5 = r5;
                        bb.emp_res6 = r6;
                        bb.emp_res7 = r7;
                        bb.emp_res8 = r8;
                        bb.emp_res9 = r9;
                        bb.emp_res10 = r10;

                        bb.emp_res11 = r11;
                        bb.emp_res12 = r12;
                        bb.emp_res13 = r13;
                        bb.emp_res14 = r14;
                        bb.emp_res15 = r15;
                        bb.emp_res16 = r16;
                        bb.emp_res17 = r17;
                        bb.emp_res18 = r18;
                        bb.emp_res19 = r19;
                        bb.emp_res20 = r20;

                        bb.emp_res21 = r21;
                        bb.emp_res22 = r22;
                        bb.emp_res23 = r23;
                        bb.emp_res24 = r24;
                        bb.emp_res25 = r25;
                        bb.emp_res26 = r26;
                        bb.emp_res27 = r27;


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    changeAddress = "";
                    disableAddressControls222();
                    getData1();
                    bindAddressData222();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_Click222(object sender, EventArgs e)
        {
            Close();
        }

        private void disableAddressControls222()
        {

            mainbarUpdatePrint.enableButtons();
            panelUpdate.Enabled = false;
            enableAllPages();
        }

        private void enableAddressControls222()
        {
            disableAllPages();
            this.mainbarUpdatePrint.disableButton();
            panelUpdate.Enabled = true;
            mainbarUpdatePrint.Enabled = true;
            pageUpdatePrint.Enabled = true;
        }

        private void bindAddressData222()
        {
            if (lstEmployee.Count > 0)
            {
                DateTime applicationDate;
                if (AppDateFormat.TryParseStoredDate(lstEmployee[position1].emp_appDate, out applicationDate))
                    txtAppDate.Value = applicationDate;
                else
                    txtAppDate.Text = lstEmployee[position1].emp_appDate;
                txtAppType.Text = lstEmployee[position1].emp_appType;
                choose3.Text = lstEmployee[position1].emp_res3;

                cmbType1.Text = lstEmployee[position1].emp_res4;
                cmbType2.Text = lstEmployee[position1].emp_res5;
                cmbType3.Text = lstEmployee[position1].emp_res6;

                cmdMohahaza.Text = lstEmployee[position1].emp_res7;
                cmbActionType.Text= lstEmployee[position1].emp_res8;


                if (lstEmployee[position1].emp_res1 == "X")
                {
                    choose1.Checked = true;
                    raaa1.Checked = true;
                }
                if (lstEmployee[position1].emp_res2 == "X")
                {
                    choose2.Checked = true;
                    raaa2.Checked = true;
                }
                if (lstEmployee[position1].emp_res3 == "X")
                {
                  //  choose2.Checked = true;
                    raaa3.Checked = true;
                }
                //if (lstEmployee[position1].emp_res4 == "X")
                //{
                //    choose4.Checked = true;
                //}
                //if (lstEmployee[position1].emp_res5 == "X")
                //{
                //    choose5.Checked = true;
                //}
                //if (lstEmployee[position1].emp_res6 == "X")
                //{
                //    choose6.Checked = true;
                //}


                //if (lstEmployee[position1].emp_res7 == "X")
                //{
                //    choose7.Checked = true;
                //}
                //if (lstEmployee[position1].emp_res8 == "X")
                //{
                //    choose8.Checked = true;
                //}
                if (lstEmployee[position1].emp_res9 == "X")
                {
                    choose9.Checked = true;
                }
                if (lstEmployee[position1].emp_res10 == "X")
                {
                    choose10.Checked = true;
                }
                if (lstEmployee[position1].emp_res11 == "X")
                {
                    choose11.Checked = true;
                }
                if (lstEmployee[position1].emp_res12 == "X")
                {
                    choose12.Checked = true;
                }
                if (lstEmployee[position1].emp_res13 == "X")
                {
                    choose13.Checked = true;
                }
                if (lstEmployee[position1].emp_res14 == "X")
                {
                    choose14.Checked = true;
                }
                if (lstEmployee[position1].emp_res15 == "X")
                {
                    choose15.Checked = true;
                }
                if (lstEmployee[position1].emp_res16 == "X")
                {
                    choose16.Checked = true;
                }
                if (lstEmployee[position1].emp_res17 == "X")
                {
                    choose17.Checked = true;
                }
                if (lstEmployee[position1].emp_res18 == "X")
                {
                    choose18.Checked = true;
                }
                if (lstEmployee[position1].emp_res19 == "X")
                {
                    choose19.Checked = true;
                }
                if (lstEmployee[position1].emp_res20 == "X")
                {
                    choose20.Checked = true;
                }
                if (lstEmployee[position1].emp_res21 == "X")
                {
                    choose21.Checked = true;
                }
                if (lstEmployee[position1].emp_res22 == "X")
                {
                    choose22.Checked = true;
                }
                if (lstEmployee[position1].emp_res23 == "X")
                {
                    choose23.Checked = true;
                }
                if (lstEmployee[position1].emp_res24 == "X")
                {
                    choose24.Checked = true;
                }
                if (lstEmployee[position1].emp_res25 == "X")
                {
                    choose25.Checked = true;
                }
                if (lstEmployee[position1].emp_res26 == "X")
                {
                    choose26.Checked = true;
                }
                if (lstEmployee[position1].emp_res27 == "X")
                {
                    choose27.Checked = true;
                }
            }
            else
            {
                
            }
        }


        // end of employee address
 

        // start of employee Sponsor


        private void btnFindLicenses_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, false);
            frm.isMainLicencesInclude = false;
            frm.ShowDialog();
            if (frm.found)
            {
                int IDD = frm.licID;
                lblcompanyLicencesID.Text = frm.licID.ToString();
                txtlicenseNo.Text = frm.licNO;
                txtcommercialName.Text = frm.licName;

                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.CompanyLicences
                               where c.companyLicencesID == IDD
                               select c).ToList();
                    if (val != null)
                    {
                        txtcommercialName.Text = val[0].commercialName;

                        for (int i = 0; i < cmbcompanyID.Items.Count; i++)
                        {
                            if (cmbcompanyID.Items[i].Value.ToString() == val[0].companyID.Value.ToString())
                            {
                                cmbcompanyID.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }


            }

        }

        void btnEdit_Click3(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (sponsor.Count > 0)
                {
                    changeSponsor = "edit";
                    enableAddressControls3();
                }
            }
        }
        
        void btnAdd_Click3(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (sponsor.Count == 0)
                {
                    if (int.Parse(txtNoOfSeatLeft.Text) > 0)
                    {
                        changeSponsor = "add";
                        enableAddressControls3();
                    }
                    else
                    {
                        MessageBox.Show("لا يمكنك اضافة كفيل , يجب زيادة الايجار ");
                    }
                }

            }
        }

        void btnDelete_Click3(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    if (lblSponsorID.Text != "-1")
                    {
                        DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                            {
                                using (TransactionScope ts = new TransactionScope())
                                {
                                    int IDDD = int.Parse(lblEmployeeID.Text);
                                    int bbID = int.Parse(lblSponsorID.Text);

                                    Document bb1 = (from c in db.Documents.Include("DocumentDef")
                                                    where
                                                        c.document_TypeID == IDDD &&
                                                        c.documentDefID == 2
                                                    select c).FirstOrDefault();

                                    if (bb1 != null)
                                    {
                                        db.Documents.DeleteObject(bb1);
                                        db.SaveChanges();
                                    }


                                    EmployeeSponsor bb = (from c in db.EmployeeSponsors
                                                          where c.employeeSponsorID ==
                                                              bbID
                                                          select c).FirstOrDefault();

                                    db.EmployeeSponsors.DeleteObject(bb);
                                    db.SaveChanges();


                                    

                                    db.AcceptAllChanges();
                                    ts.Complete();

                                }
                                bindData1();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnCancel_Click3(object sender, EventArgs e)
        {
            changeSponsor = "";
            disableAddressControls3();
            bindSponsorData3();
        }

        void btnSave_Click3(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeSponsor == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            EmployeeSponsor bb = new EmployeeSponsor();
                            Document bb1 = new Document();

                            bb.companyID = int.Parse(cmbcompanyID.SelectedValue.ToString());
                            bb.designationID = int.Parse(cmbdesignationID.SelectedValue.ToString());

                            bb.companyLicencesID = int.Parse(lblcompanyLicencesID.Text);
                            bb.employeeID = int.Parse(lblEmployeeID.Text);
                            //bb.employeeSponsor_entryDate = DateTime.Parse(txtemployeeSponsor_entryDate.Text);
                            bb.employeeSponsor_Jawazat = txtemployeeSponsor_Jawazat.Text;
                            bb.employeeSponsor_salary = txtemployeeSponsor_salary.Text;
                            bb.employeeSponsor_residencyNo = txtemployeeSponsor_ResidencyNo.Text;

                            DateTime ddd = DateTime.Now;
                            if (DateTime.TryParse(txtemployeeSponsor_entryDate.Text, out ddd))
                            {
                                bb.employeeSponsor_entryDate = ddd;
                            }
                            if (DateTime.TryParse(txtemployeeSponsor_ResidencyIssueDate.Text, out ddd))
                            {
                                bb.employeeSponsor_residencyStartDate = ddd;
                                bb1.document_startDate = ddd;
                            }
                            if (DateTime.TryParse(txtemployeeSponsor_ResidencyExpiryDate.Text, out ddd))
                            {
                                bb.employeeSponsor_residencyEndDate = ddd;
                                bb1.document_endDate = ddd;
                            }

                            bb.employeeSponsor_Shou2oun = txtemployeeSponsor_Shou2oun.Text;
                            bb.employeeSponsor_useDuration = txtemployeeSponsor_useDuration.Text;
                            bb.employeeSponsor_WorkPermitNo = txtemployeeSponsor_WorkPermitNo.Text;

                            bb1.document_No = txtemployeeSponsor_ResidencyNo.Text;
                            bb1.document_text1 = "";
                            bb1.document_text2 = "";
                            bb1.document_text3 = "";
                            bb1.document_text4 = "";
                            bb1.document_text5 = "";


                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            db.EmployeeSponsors.AddObject(bb);
                            rowsAffected = db.SaveChanges();

                            //// we must get the id of the document
                            //var val = from c in db.DocumentDefs
                            //          where c.documentDefID == 2 &&
                            //          (c.documentDef_Name == "اقامة" ||
                            //          c.documentDef_Name == "إقامة" ||
                            //          c.documentDef_Name == "الاقامة" ||
                            //          c.documentDef_Name == "الإقامة" ||
                            //          c.documentDef_Name == "أقامة")
                            //          select c;
                            //if (val.ToList().Count > 0)
                            //{
                            //    bb1.documentDefID = val.ToList()[0].documentDefID;
                            //}
                            bb1.documentDefID = 2;
                            bb1.document_placeOfIssue = "";
                            bb1.document_TypeID = int.Parse(lblEmployeeID.Text);

                            bb1.document_Remarks = txtemployeeSponsor_useDuration.Text;

                            bb1.document_Status = "";
                            bb1.document_Type = "عادي";

                            bb1.added_Date = DateTime.Now;
                            bb1.addedBy = MainClass.userID;

                            bb1.edited_Date = DateTime.Now;
                            bb1.editedBy = MainClass.userID;

                            db.Documents.AddObject(bb1);
                            rowsAffected = db.SaveChanges();


                            lblSponsorID.Text = bb.employeeSponsorID.ToString();
                            db.AcceptAllChanges();
                            ts.Complete();
                        }
                    }
                }
                else if (changeSponsor == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblSponsorID.Text);
                        EmployeeSponsor bb = (from c in db.EmployeeSponsors
                                              where c.employeeSponsorID ==
                                                  bbID
                                              select c).FirstOrDefault();

                        int IDDD = int.Parse(lblEmployeeID.Text);

                        // we must get the id of the document
                        //var val = from c in db.DocumentDefs
                        //          where c.documentTypeDefID == 2 &&
                        //          (c.documentDef_Name == "اقامة" ||
                        //          c.documentDef_Name == "إقامة" ||
                        //          c.documentDef_Name == "الاقامة" ||
                        //          c.documentDef_Name == "الإقامة" ||
                        //          c.documentDef_Name == "أقامة")
                        //          select c;

                        Document bb1 = (from c in db.Documents.Include("DocumentDef")
                                        where
                                            c.document_TypeID == IDDD &&
                                            c.documentDefID ==2 
                                        select c).FirstOrDefault();


                        using (TransactionScope ts = new TransactionScope())
                        {
                            bb.companyID = int.Parse(cmbcompanyID.SelectedValue.ToString());
                            bb.designationID = int.Parse(cmbdesignationID.SelectedValue.ToString());

                            bb.companyLicencesID = int.Parse(lblcompanyLicencesID.Text);
                            bb.employeeID = int.Parse(lblEmployeeID.Text);
                            //bb.employeeSponsor_entryDate = DateTime.Parse(txtemployeeSponsor_entryDate.Text);
                            bb.employeeSponsor_Jawazat = txtemployeeSponsor_Jawazat.Text;
                            bb.employeeSponsor_salary = txtemployeeSponsor_salary.Text;
                            bb.employeeSponsor_residencyNo = txtemployeeSponsor_ResidencyNo.Text;

                            DateTime ddd = DateTime.Now;
                            if (DateTime.TryParse(txtemployeeSponsor_entryDate.Text, out ddd))
                            {
                                bb.employeeSponsor_entryDate = ddd;
                            }
                            if (DateTime.TryParse(txtemployeeSponsor_ResidencyIssueDate.Text, out ddd))
                            {
                                bb.employeeSponsor_residencyStartDate = ddd;
                                bb1.document_startDate = ddd;
                            }
                            if (DateTime.TryParse(txtemployeeSponsor_ResidencyExpiryDate.Text, out ddd))
                            {
                                bb.employeeSponsor_residencyEndDate = ddd;
                                bb1.document_endDate = ddd;
                            }
                            bb1.documentDefID = 2;
                            bb.employeeSponsor_Shou2oun = txtemployeeSponsor_Shou2oun.Text;
                            bb.employeeSponsor_useDuration = txtemployeeSponsor_useDuration.Text;
                            bb.employeeSponsor_WorkPermitNo = txtemployeeSponsor_WorkPermitNo.Text;

                            bb1.document_No = txtemployeeSponsor_ResidencyNo.Text;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            //db.EmployeeSponsors.AddObject(bb);
                            rowsAffected = db.SaveChanges();

                            // we must get the id of the document
                            //var val = from c in db.DocumentDefs
                            //          where c.documentDef_Name == "اقامة" ||
                            //          c.documentDef_Name == "إقامة" ||
                            //          c.documentDef_Name == "أقامة"
                            //          select c;
                            //if (val.ToList().Count > 0)
                            //{
                            //    bb1.documentDefID = val.ToList()[0].documentDefID;
                            //}

                            bb1.document_placeOfIssue = "";
                            //bb1.document_TypeID = int.Parse(lblEmployeeID.Text);

                            bb1.document_Remarks = txtemployeeSponsor_useDuration.Text;

                            bb1.document_Status = "";
                            bb1.document_Type = "عادي";



                            bb1.edited_Date = DateTime.Now;
                            bb1.editedBy = MainClass.userID;

                            //db.Documents.AddObject(bb1);
                            rowsAffected = db.SaveChanges();


                            lblSponsorID.Text = bb.employeeSponsorID.ToString();
                            db.AcceptAllChanges();
                            ts.Complete();
                        }
                    }
                }


                if (rowsAffected > 0)
                {
                    changeSponsor = "";
                    disableAddressControls3();
                    getData3();
                    bindSponsorData3();
                    bindSHData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_Click3(object sender, EventArgs e)
        {
            Close();
        }

        private void disableAddressControls3()
        {

            mainbarSponsor.enableButtons();
            enableAllPages();

            btnFindLicenses.Enabled = false;
            btnFindCompany.Enabled = false;

            cmbcompanyID.Enabled = false;
            cmbdesignationID.Enabled = false;
            txtemployeeSponsor_entryDate.ReadOnly = true;

            txtlicenseNo.ReadOnly = true;
            txtcommercialName.ReadOnly = true;
            txtemployeeSponsor_Jawazat.Enabled = false;
            txtemployeeSponsor_salary.ReadOnly = true;

            txtemployeeSponsor_ResidencyExpiryDate.ReadOnly = true;
            txtemployeeSponsor_ResidencyIssueDate.ReadOnly = true;
            txtemployeeSponsor_ResidencyNo.ReadOnly = true;

            txtemployeeSponsor_Shou2oun.Enabled = false;
            txtemployeeSponsor_useDuration.ReadOnly = true;
            cmbemployeeSponsor_useDuration.Enabled = false;
            txtemployeeSponsor_WorkPermitNo.ReadOnly = true;

            
        }

        private void enableAddressControls3()
        {
            mainbarSponsor.disableButton();

            btnFindLicenses.Enabled = true;
            btnFindCompany.Enabled = true;
            cmbcompanyID.Enabled = true;
            cmbdesignationID.Enabled = true;
            txtemployeeSponsor_entryDate.ReadOnly = false;

            txtlicenseNo.ReadOnly = false;
            txtcommercialName.ReadOnly = false;
            txtemployeeSponsor_Jawazat.Enabled = true;
            txtemployeeSponsor_salary.ReadOnly = false;

            txtemployeeSponsor_ResidencyNo.ReadOnly = false;
            txtemployeeSponsor_ResidencyIssueDate.ReadOnly = false;
            txtemployeeSponsor_ResidencyExpiryDate.ReadOnly = false;

            txtemployeeSponsor_Shou2oun.Enabled = true;
            txtemployeeSponsor_useDuration.ReadOnly = false;
            cmbemployeeSponsor_useDuration.Enabled = true;
            txtemployeeSponsor_WorkPermitNo.ReadOnly = false;

            disableAllPages();
            pageSponsor.Enabled = true;
        }

        private void getData3()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                sponsor = new List<EmployeeSponsor>();
                int empID = int.Parse(lblEmployeeID.Text);
                sponsor = (from c in db.EmployeeSponsors 
                           where c.employeeID == empID  select c).ToList();
            }
        }

        private void bindSponsorData3()
        {
            if (sponsor.Count > 0)
            {
                if (sponsor[0].employeeSponsor_entryDate.HasValue)
                    txtemployeeSponsor_entryDate.Text = sponsor[0].employeeSponsor_entryDate.Value.ToShortDateString();
                else
                    txtemployeeSponsor_entryDate.Text = "";

                if (sponsor[0].employeeSponsor_residencyEndDate.HasValue)
                    txtemployeeSponsor_ResidencyExpiryDate.Text = sponsor[0].employeeSponsor_residencyEndDate.Value.ToShortDateString();
                else
                    txtemployeeSponsor_ResidencyExpiryDate.Text = "";

                if (sponsor[0].employeeSponsor_residencyStartDate.HasValue)
                    txtemployeeSponsor_ResidencyIssueDate.Text = sponsor[0].employeeSponsor_residencyStartDate.Value.ToShortDateString();
                else
                    txtemployeeSponsor_ResidencyIssueDate.Text = "";

                
                txtemployeeSponsor_salary.Text = sponsor[0].employeeSponsor_salary;
                txtemployeeSponsor_salary1.Text = sponsor[0].employeeSponsor_salary;
                txtemployeeSponsor_ResidencyNo.Text = sponsor[0].employeeSponsor_residencyNo;

                

                txtemployeeSponsor_useDuration.Text = sponsor[0].employeeSponsor_useDuration;
                cmbemployeeSponsor_useDuration.Text = sponsor[0].employeeSponsor_useDuration;
                txtemployeeSponsor_WorkPermitNo.Text = sponsor[0].employeeSponsor_WorkPermitNo;

                txtemployeeSponsor_Shou2oun.Text = sponsor[0].employeeSponsor_Shou2oun;
                txtemployeeSponsor_Jawazat.Text = sponsor[0].employeeSponsor_Jawazat;
                //for (int i = 0; i < txtemployeeSponsor_Shou2oun.Items.Count; i++)
                //{
                //    if (txtemployeeSponsor_Shou2oun.Items[i].Text.ToString() == sponsor[0].employeeSponsor_Shou2oun)
                //    {
                //        txtemployeeSponsor_Shou2oun.SelectedIndex = i;
                //        break;
                //    }
                //}
                int clID = sponsor[0].companyLicencesID.Value;
                int ccmbID = -1;
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var vvaal = (from c in db.CompanyLicences
                               where c.companyLicencesID == clID
                               select c).ToList();
                    if (vvaal != null)
                        ccmbID = vvaal[0].companyID.Value;

                }
                for (int i = 0; i < cmbcompanyID.Items.Count; i++)
                {
                    if (cmbcompanyID.Items[i].Value.ToString() == ccmbID.ToString())
                    {
                        cmbcompanyID.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbdesignationID.Items.Count; i++)
                {
                    if (cmbdesignationID.Items[i].Value.ToString() == sponsor[0].designationID.Value.ToString())
                    {
                        cmbdesignationID.SelectedIndex = i;
                        break;
                    }
                }

                lblcompanyLicencesID.Text = sponsor[0].companyLicencesID.Value.ToString();
                lblSponsorID.Text = sponsor[0].employeeSponsorID.ToString();
                bindLicenseData(sponsor[0].companyLicencesID.Value);

            }
            else
            {
                lblSponsorID.Text = "-1";
                lblcompanyLicencesID.Text = "-1";

                cmbcompanyID.SelectedIndex = 0;
                cmbdesignationID.SelectedIndex = 0;

                txtlicenseNo.Text = "";
                txtcommercialName.Text = "";
                txtemployeeSponsor_entryDate.Text ="";
                txtemployeeSponsor_Jawazat.Text = "";
                txtemployeeSponsor_salary.Text = "";
                txtemployeeSponsor_ResidencyNo.Text = "";
                txtemployeeSponsor_ResidencyIssueDate.Text = "";
                txtemployeeSponsor_ResidencyExpiryDate.Text = "";
                txtemployeeSponsor_salary1.Text = "";
                txtemployeeSponsor_Shou2oun.Text = "";
                txtemployeeSponsor_useDuration.Text = "";
                cmbemployeeSponsor_useDuration.SelectedText = "";
                txtemployeeSponsor_WorkPermitNo.Text = "";


            }
        }

        private void bindLicenseData(int _lid)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.CompanyLicences
                          where c.companyLicencesID == _lid
                          select c).ToList();
                if (val.Count > 0)
                {
                    txtcommercialName.Text = val[0].commercialName;
                    txtlicenseNo.Text = val[0].licenseNo;
                }
                else
                {
                    txtcommercialName.Text = "";
                    txtlicenseNo.Text = "";
                }
            }
        }

        // end of of employee sponsor

        // START of Salary Part

        void btnEdit_ClickS(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (lstsalary.Count > 0)
                {
                    changeSalary = "edit";
                    enableSalaryControlsS();
                }
            }
        }

        void btnAdd_ClickS(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (lstsalary.Count == 0)
                {
                    changeSalary = "add";
                    enableSalaryControlsS();
                }

            }
        }


        void btnCancel_ClickS(object sender, EventArgs e)
        {
            changeSalary = "";
            disableSalaryControlsS();
            bindSalaryDataS();
        }

        void btnDelete_ClickS(object sender, EventArgs e)
        {
            try
            {

                if (lblEmployeeID.Text != "-1")
                {
                    if (lblSalaryID.Text != "-1")
                    {
                        DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                            {

                                int bbID = int.Parse(lblSalaryID.Text);
                                EmployeeSalary bb = (from c in db.EmployeeSalaries
                                                     where c.employeeSalaryID ==
                                                         bbID
                                                     select c).FirstOrDefault();

                                db.EmployeeSalaries.DeleteObject(bb);
                                int noOfAffectedRows = db.SaveChanges();
                                if (noOfAffectedRows > 0)
                                {
                                    bindData1();
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnSave_ClickS(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeSalary == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        EmployeeSalary bb = new EmployeeSalary();

                        bb.bankID = int.Parse(cmbbankID.SelectedValue.ToString());
                        bb.accountNo = txtaccountNo.Text;
                        bb.branchName = txtbranchName.Text;
                        bb.VoucherNo = txtVoucherNo.Text;
                        bb.employeeID = int.Parse(lblEmployeeID.Text);
                        bb.paidFromSalary = float.Parse(txtpaidFromSalary.Text);


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.EmployeeSalaries.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                        lblSalaryID.Text = bb.employeeSalaryID.ToString();
                    }
                }
                else if (changeSalary == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblSalaryID.Text);
                        EmployeeSalary bb = (from c in db.EmployeeSalaries
                                             where c.employeeSalaryID ==
                                                 bbID
                                             select c).FirstOrDefault();

                        bb.bankID = int.Parse(cmbbankID.SelectedValue.ToString());
                        bb.accountNo = txtaccountNo.Text;
                        bb.branchName = txtbranchName.Text;
                        bb.VoucherNo = txtVoucherNo.Text;
                        bb.employeeID = int.Parse(lblEmployeeID.Text);
                        bb.paidFromSalary = float.Parse(txtpaidFromSalary.Text);


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    changeSalary = "";
                    disableSalaryControlsS();
                    getDataS();
                    bindSalaryDataS();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_ClickS(object sender, EventArgs e)
        {
            Close();
        }

        private void disableSalaryControlsS()
        {

            mainbarsSalary.enableButtons();
            enableAllPages();

            cmbbankID.Enabled=false;
            txtaccountNo.ReadOnly=true;
            txtbranchName.ReadOnly=true;
            txtVoucherNo.ReadOnly = true;
            txtpaidFromSalary.ReadOnly = true;
            txtemployeeSponsor_salary1.ReadOnly = true;

        }

        private void enableSalaryControlsS()
        {
            mainbarsSalary.disableButton();

            cmbbankID.Enabled = true;
            txtaccountNo.ReadOnly = false;
            txtbranchName.ReadOnly = false;
            txtVoucherNo.ReadOnly = false;
            txtpaidFromSalary.ReadOnly = false;

            disableAllPages();
            this.pageSalary.Enabled = true;
        }

        private void getDataS()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstsalary = new List<EmployeeSalary>();
                int empID = int.Parse(lblEmployeeID.Text);
                lstsalary = (from c in db.EmployeeSalaries
                           where c.employeeID == empID 
                           select c).ToList();
            }
        }

        private void bindSalaryDataS()
        {
            if (lstsalary.Count > 0)
            {

                txtaccountNo.Text = lstsalary[0].accountNo;
                txtbranchName.Text = lstsalary[0].branchName;
                txtVoucherNo.Text = lstsalary[0].VoucherNo;
                txtpaidFromSalary.Text = lstsalary[0].paidFromSalary.Value.ToString();

                for (int i = 0; i < this.cmbbankID.Items.Count; i++)
                {
                    if (cmbbankID.Items[i].Value.ToString() ==this.lstsalary[0].bankID.Value.ToString())
                    {
                        cmbbankID.SelectedIndex = i;
                        break;
                    }
                }

                lblSalaryID.Text = lstsalary[0].employeeSalaryID.ToString();
       
            }
            else
            {
                txtaccountNo.Text = "";
                txtbranchName.Text = "";
                txtVoucherNo.Text = "";
                txtpaidFromSalary.Text = "";
                cmbbankID.SelectedIndex = 0;
                lblSalaryID.Text = "-1";
            }
        }


        /// end of salary 
       

        // start work Contract 

        void btnEdit_ClickW(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (lstworkContract.Count > 0)
                {
                    changeWorkContract = "edit";
                    enableWCControlsS();
                }
            }
        }

        void btnAdd_ClickW(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")           {
                if (lstworkContract.Count == 0)
                {
                    changeWorkContract = "add";
                    enableWCControlsS();
                }

            }
        }


        void btnCancel_ClickW(object sender, EventArgs e)
        {
            changeWorkContract = "";
            disableWCControlsS();
            bindWCData();
        }


        void btnDelete_ClickW(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    if (lblWorkContractID.Text != "-1")
                    {
                        DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                            {

                                int bbID = int.Parse(this.lblWorkContractID.Text);
                                EmployeeWorkContract bb = (from c in db.EmployeeWorkContracts
                                                           where c.workContractID ==
                                                               bbID
                                                           select c).FirstOrDefault();

                                db.EmployeeWorkContracts.DeleteObject(bb);
                                int noOfAffectedRows = db.SaveChanges();
                                if (noOfAffectedRows > 0)
                                {
                                    bindData1();
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnSave_ClickW(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeWorkContract == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        EmployeeWorkContract bb = new EmployeeWorkContract();

                        bb.wc_date = DateTime.Parse(txtwc_date.Text);
                        bb.wc_duration = txtwc_duration.Text;
                        bb.wc_durationType = txtwc_durationType.Text;
                        bb.wc_payEvery = txtwc_payEvery.Text;
                        bb.wc_startingContract = DateTime.Parse(txtwc_startingContract.Text);
                        bb.employeeID = int.Parse(lblEmployeeID.Text);

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.EmployeeWorkContracts.AddObject(bb);
                        rowsAffected = db.SaveChanges();

                        lblWorkContractID.Text = bb.workContractID.ToString();
                    }
                }
                else if (changeWorkContract == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(this.lblWorkContractID.Text);
                        EmployeeWorkContract bb = (from c in db.EmployeeWorkContracts
                                                   where c.workContractID ==
                                                       bbID
                                                   select c).FirstOrDefault();

                        bb.wc_date = DateTime.Parse(txtwc_date.Text);
                        bb.wc_duration = txtwc_duration.Text;
                        bb.wc_durationType = txtwc_durationType.Text;
                        bb.wc_payEvery = txtwc_payEvery.Text;
                        bb.wc_startingContract = DateTime.Parse(txtwc_startingContract.Text);


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    changeWorkContract = "";
                    disableWCControlsS();
                    getDataWC();
                    bindWCData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_ClickW(object sender, EventArgs e)
        {
            Close();
        }

        private void disableWCControlsS()
        {

            mainbarWorkContract.enableButtons();
            enableAllPages();

            txtwc_date.Enabled = false;
            txtwc_duration.ReadOnly = true;
            txtwc_durationType.ReadOnly = true;
            txtwc_payEvery.ReadOnly = true;
            txtwc_startingContract.Enabled = false;


        }

        private void enableWCControlsS()
        {
            mainbarWorkContract.disableButton();

            txtwc_date.Enabled = true;
            txtwc_duration.ReadOnly = false;
            txtwc_durationType.ReadOnly = false;
            txtwc_payEvery.ReadOnly = false;
            txtwc_startingContract.Enabled = true;

            disableAllPages();
            this.pageWorkContract.Enabled = true;
        }

        private void getDataWC()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstworkContract = new List<EmployeeWorkContract>();
                int empID = int.Parse(lblEmployeeID.Text);
                lstworkContract = (from c in db.EmployeeWorkContracts
                             where c.employeeID == empID 
                             select c).ToList();
            }
        }

        private void bindWCData()
        {
            if (lstworkContract.Count > 0)
            {
                txtwc_date.Text = lstworkContract[0].wc_date.Value.ToShortDateString();
                txtwc_duration.Text = lstworkContract[0].wc_duration;
                txtwc_durationType.Text = lstworkContract[0].wc_durationType;
                txtwc_payEvery.Text = lstworkContract[0].wc_payEvery;
                txtwc_startingContract.Text = lstworkContract[0].wc_startingContract.Value.ToShortDateString(); ;
                lblWorkContractID.Text = lstworkContract[0].workContractID.ToString();
            }
            else
            {
                txtwc_date.Text = DateTime.Now.ToShortDateString();
                txtwc_duration.Text = "";
                txtwc_durationType.Text = "";
                txtwc_payEvery.Text = "";
                txtwc_startingContract.Text = DateTime.Now.ToShortDateString();
                lblWorkContractID.Text ="-1";

            }
        }

        // end work Contract


        // start of dependent

        void btnAdd_ClickD(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                changeDependent = "add";
                enableDepControls1();

                txtdepend_aName.Text = "";
                txtdepend_birthday.Text = "";
                txtdepend_eName.Text = "";
                txtdepend_gender.Text = "";
                txtdepend_nationality.Text = "";
                txtdepend_relation.Text = "";
                txtdepend_religion.Text = "";
            }
        }

        void btnEdit_ClickD(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1" && lblDependentID.Text != "-1")
            {
                changeDependent = "edit";
                enableDepControls1();
            }
        }

        void btnDelete_ClickD(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(lblDependentID.Text);
                            Dependent bb = (from c in db.Dependents
                                            where c.dependentsID ==
                                                bbID
                                            select c).FirstOrDefault();

                            db.Dependents.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                positionDependent = 0;
                                getDepData();
                                bindDepData1();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnCancel_ClickD(object sender, EventArgs e)
        {
            changeDependent = "";
            disableDepControls1();
            bindDepData1();
        }

        void btnSave_ClickD(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                if (changeDependent == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        Dependent bb = new Dependent();

                        bb.depend_aName = txtdepend_aName.Text;
                        bb.depend_birthday = txtdepend_birthday.Text;
                        bb.depend_eName = txtdepend_eName.Text;
                        bb.depend_gender = txtdepend_gender.Text;
                        bb.depend_nationality = txtdepend_nationality.Text;
                        bb.depend_relation = txtdepend_relation.Text;
                        bb.depend_religion = txtdepend_religion.Text;
                        bb.employeeID = int.Parse(lblEmployeeID.Text);

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Dependents.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (changeDependent == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(this.lblDependentID.Text);
                        Dependent bb = (from c in db.Dependents
                                        where c.dependentsID ==
                                            bbID
                                        select c).FirstOrDefault();

                        bb.depend_aName = txtdepend_aName.Text;
                        bb.depend_birthday = txtdepend_birthday.Text;
                        bb.depend_eName = txtdepend_eName.Text;
                        bb.depend_gender = txtdepend_gender.Text;
                        bb.depend_nationality = txtdepend_nationality.Text;
                        bb.depend_relation = txtdepend_relation.Text;
                        bb.depend_religion = txtdepend_religion.Text;


                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Employees.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableDepControls1();

                    getDepData();
                    if (changeDependent == "add")
                    {
                        positionDependent = lstDependent.Count - 1;
                    }
                    changeDependent = "";

                    bindDepData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnFind_ClickD(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void btnFirst_ClickD(object sender, EventArgs e)
        {
            if (positionDependent > 0)
            {
                positionDependent = 0;
                bindDepData1();
            }
        }

        void btnPrevious_ClickD(object sender, EventArgs e)
        {
            if (positionDependent > 0)
            {
                positionDependent -= 1;
                bindDepData1();
            }
        }

        void btnNext_ClickD(object sender, EventArgs e)
        {
            if (positionDependent < lstDependent.Count - 1)
            {
                positionDependent += 1;
                bindDepData1();
            }
        }

        void btnLast_ClickD(object sender, EventArgs e)
        {
            if (positionDependent < lstDependent.Count - 1)
            {
                positionDependent = lstDependent.Count - 1;
                bindDepData1();
            }
        }

        void btnExit_ClickD(object sender, EventArgs e)
        {
            Close();
        }

        private void disableDepControls1()
        {

            mainbarDependent.enableButtons();

            txtdepend_aName.ReadOnly = true;
            txtdepend_birthday.ReadOnly = true;
            txtdepend_eName.ReadOnly = true;
            txtdepend_gender.Enabled = false;
            txtdepend_nationality.Enabled = false;
            txtdepend_relation.ReadOnly = true;
            txtdepend_religion.Enabled = false;


            enableAllPages();
        }

        private void enableDepControls1()
        {
            mainbarDependent.disableButton();

            txtdepend_aName.ReadOnly = false;
            txtdepend_birthday.ReadOnly = false;
            txtdepend_eName.ReadOnly = false;
            txtdepend_gender.Enabled = true;
            txtdepend_nationality.Enabled = true;
            txtdepend_relation.ReadOnly = false;
            txtdepend_religion.Enabled = true;

            disableAllPages();
            this.pageDependent.Enabled = true;
        }

        private void getDepData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDependent = new List<Dependent>();
                int empID = int.Parse(lblEmployeeID.Text);
                lstDependent = (from c in db.Dependents 
                                where c.employeeID == empID  select c).ToList();
                gView.DataSource = lstDependent;
            }
        }

        private void bindDepData1()
        {

            
            if (lstDependent.Count > 0)
            {

                txtdepend_aName.Text = lstDependent[positionDependent].depend_aName;
                txtdepend_birthday.Text = lstDependent[positionDependent].depend_birthday;
                txtdepend_eName.Text = lstDependent[positionDependent].depend_eName;
                txtdepend_gender.Text = lstDependent[positionDependent].depend_gender;
                txtdepend_nationality.Text = lstDependent[positionDependent].depend_nationality;
                txtdepend_relation.Text = lstDependent[positionDependent].depend_relation;
                txtdepend_religion.Text = lstDependent[positionDependent].depend_religion;
                
                this.lblDependentID.Text = lstDependent[positionDependent].dependentsID.ToString();
                mainbarDependent.lblCount.Text = positionDependent + 1 + " / " + lstDependent.Count;

                //bindAddressData2();

            }
            else
            {
                txtdepend_aName.Text = "";
                txtdepend_birthday.Text = "";
                txtdepend_eName.Text = "";
                txtdepend_gender.Text = "";
                txtdepend_nationality.Text = "";
                txtdepend_relation.Text = "";
                txtdepend_religion.Text = "";

                lblDependentID.Text = "-1";

                mainbarDependent.lblCount.Text = " 0 / 0";
            }
        }
        // end of depenedent


        // start Shop

        void btnEdit_ClickSH(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (lstEmployeeLicenseShop.Count > 0)
                {
                    changeShop = "edit";
                    enableSHControlsS();
                }
            }
        }

        void btnAdd_ClickSH(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (lstEmployeeLicenseShop.Count == 0)
                {
                    txtEmployeeLink.Text = "";
                    changeShop = "add";
                    enableSHControlsS();
                }

            }
        }

        void btnDelete_ClickSH(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    if (lblemployeeLicShopID.Text != "-1")
                    {
                        DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                            {

                                int bbID = int.Parse(lblemployeeLicShopID.Text);
                                EmployeeLicenseShop bb = (from c in db.EmployeeLicenseShops
                                                          where c.employeeLicShopID ==
                                                              bbID
                                                          select c).FirstOrDefault();

                                db.EmployeeLicenseShops.DeleteObject(bb);
                                int noOfAffectedRows = db.SaveChanges();
                                if (noOfAffectedRows > 0)
                                {
                                    bindData1();
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnCancel_ClickSH(object sender, EventArgs e)
        {
            changeShop = "";
            disableSHControlsS();
            bindSHData();
        }

        void btnSave_ClickSH(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changeShop == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {

                        EmployeeLicenseShop bb = new EmployeeLicenseShop();

                        bb.employeeID = int.Parse(lblEmployeeID.Text);
                        bb.licencesShopID = int.Parse(lblLicenseShopID.Text);
                        bb.carID = int.Parse(lblCarID.Text);
                        bb.employeeLink = txtEmployeeLink.Text;
                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.EmployeeLicenseShops.AddObject(bb);
                        rowsAffected = db.SaveChanges();

                        this.lblemployeeLicShopID.Text = bb.employeeLicShopID.ToString();
                    }
                }
                else if (changeShop == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(this.lblemployeeLicShopID.Text);
                        EmployeeLicenseShop bb = (from c in db.EmployeeLicenseShops
                                                  where c.employeeLicShopID == bbID
                                                  select c).FirstOrDefault();

                        bb.employeeID = int.Parse(lblEmployeeID.Text);
                        bb.licencesShopID = int.Parse(lblLicenseShopID.Text);
                        if (txtcar_No.Text.Length == 0 || txtcar_No.Text.Length == 0)
                        {
                            lblCarID.Text = "-1";
                        }
                        bb.carID = int.Parse(lblCarID.Text);
                        bb.employeeLink = txtEmployeeLink.Text;
                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    changeShop = "";
                    disableSHControlsS();
                    getDataSH();
                    bindSHData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_ClickSH(object sender, EventArgs e)
        {
            Close();
        }

        private void disableSHControlsS()
        {
            
            mainbarLicenseShop.enableButtons();
            enableAllPages();

            txtShopCommercialName.ReadOnly = true;
            txtShopNo.ReadOnly = true;

            btnFindShop.Enabled = false;
            txtEmployeeLink.Enabled = false;
            txtcar_No.Enabled = false;
            txtcar_Name.Enabled = false;
            btnFindCar.Enabled = false;

        }

        private void enableSHControlsS()
        {
            mainbarLicenseShop.disableButton();

            btnFindShop.Enabled = true;
            txtEmployeeLink.Enabled = true;
            txtcar_No.Enabled = true;
            txtcar_Name.Enabled = true;
            btnFindCar.Enabled = true;

            disableAllPages();
            this.PageShop.Enabled = true;
        }

        private void getDataSH()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstEmployeeLicenseShop = new List<EmployeeLicenseShop>();
                int empID = int.Parse(lblEmployeeID.Text);
                lstEmployeeLicenseShop = (from c in db.EmployeeLicenseShops
                                   where c.employeeID == empID 
                                   select c).ToList();
            }
        }

        private void bindSHData()
        {
            if (lstEmployeeLicenseShop.Count > 0)
            {
                
                lblLicenseShopID.Text = lstEmployeeLicenseShop[0].licencesShopID.Value.ToString();
                lblCarID.Text = lstEmployeeLicenseShop[0].carID.Value.ToString();
                lblemployeeLicShopID.Text = lstEmployeeLicenseShop[0].employeeLicShopID.ToString();
                txtEmployeeLink.Text = lstEmployeeLicenseShop[0].employeeLink;
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    int IDD = lstEmployeeLicenseShop[0].licencesShopID.Value;
                    var val = (from c in db.LicensesShops
                               where c.licencesShopID == IDD
                               select c).FirstOrDefault();
                    
                    txtShopNo.Text = val.shop_Code;
                    txtShopCommercialName.Text =val.shop_Name;
                    txtEmployeeLink.Text = val.CompanyLicence.licenseLinkCode;

                    int cID = lstEmployeeLicenseShop[0].carID.Value;
                    var vcar = (from c in db.Cars
                                where c.carID == cID
                                select c).FirstOrDefault();
                    if(vcar!=null)
                    {
                        txtcar_No.Text = vcar.car_No;
                        txtcar_Name.Text = vcar.car_Name;
                        lblCarID.Text = vcar.carID.ToString();
                    }else
                    {
                        txtcar_No.Text = "";
                        txtcar_Name.Text = "";
                        lblCarID.Text = "-1";
                    }
                    
                }
                txtNoOfSeatForShop.Text = MainFile.NoOfSeatForShop(lstEmployeeLicenseShop[0].licencesShopID.Value).ToString();
                txtNoOfSeatTaken.Text = MainFile.NoOfSeatTaken(lstEmployeeLicenseShop[0].licencesShopID.Value).ToString();
                txtNoOfSeatLeft.Text = (int.Parse(txtNoOfSeatForShop.Text) - int.Parse(txtNoOfSeatTaken.Text)).ToString(); 
            }
            else
            {
                txtNoOfSeatForShop.Text = "0";
                txtNoOfSeatTaken.Text = "0";
                txtNoOfSeatLeft.Text = "0";

                txtShopNo.Text = "";
                txtShopCommercialName.Text = "";
                lblLicenseShopID.Text = "-1";
                lblCarID.Text = "-1";
                lblemployeeLicShopID.Text = "-1";
                txtEmployeeLink.Text = "";
            }
        }

        private void btnFindShop_Click(object sender, EventArgs e)
        {
            Sponsor.frmfindLicences frm = new Sponsor.frmfindLicences(false, false);
            frm.ShowDialog();
            if (frm.found)
            {
                int ID2 = frm.licID;
                txtShopNo.Text = frm.licNO;
                txtEmployeeLink.Text = frm.licenseLinkCode;
                //txtShopCommercialName.Text = frm.licName;
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = (from c in db.LicensesShops
                              where c.companyLicencesID == ID2
                              select new { c.licencesShopID }).ToList();
                    //MessageBox.Show(ID2 + "  == " + val[0].licencesShopID.ToString());

                    lblLicenseShopID.Text = val[0].licencesShopID.ToString();
                }
            }
        }

        // end Shop




        // start employee document


        public void refreshDocument()
        {
            getDocData();
            bindDocData1();
        }
        void btnAdd_ClickDoc(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                BarakalAlkheir.Docs.frmDocuments frm = new Docs.frmDocuments();
                frm.documentTypeDefID = int.Parse(lbldocumentTypeDefID.Text);
                frm.document_TypeID = int.Parse(lblEmployeeID.Text);
                frm.ShowDialog();
                //frm.btnAdd_Click(sender, e);
                refreshDocument();
                //getDocData();
                //bindDocData1();
            }
        }

        void btnEdit_ClickDoc(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1" && lblDocumentID.Text != "-1")
            {
                BarakalAlkheir.Docs.frmDocuments frm = new Docs.frmDocuments();
                frm.documentTypeDefID = int.Parse(lbldocumentTypeDefID.Text);
                frm.document_TypeID = int.Parse(lblEmployeeID.Text);
                frm.documentID = int.Parse(lblDocumentID.Text);
                frm.action = "edit";
                frm.ShowDialog();
              //  frm.btnEdit_Click(sender, e);
                //getDocData();
                //bindDocData1();
                refreshDocument();
            }
        }

        void btnDelete_ClickDoc(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(lblDocumentID.Text);
                            Document bb = (from c in db.Documents
                                           where c.documentID ==
                                               bbID
                                           select c).FirstOrDefault();

                            db.Documents.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                getDocData();
                                bindDocData1();
                                refreshDocument();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnFirst_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument > 0)
            {
                positionDocument = 0;
                bindDocData1();
            }
        }

        void btnPrevious_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument > 0)
            {
                positionDocument -= 1;
                bindDocData1();
            }
        }

        void btnNext_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument < lstDocument.Count - 1)
            {
                positionDocument += 1;
                bindDocData1();
            }
        }

        void btnLast_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument < lstDocument.Count - 1)
            {
                positionDocument = lstDocument.Count - 1;
                bindDocData1();
            }
        }

        void btnExit_ClickDoc(object sender, EventArgs e)
        {
            Close();
        }


        bool action = false;
        bool addExcemption = false;

        private void getDocData()
        {
            if (!MainClass.isSuperUser)
            {
                if (MainFile.isDeleteSecurity("btnCancelDocumentTask"))
                {
                    this.gvDocument.Columns["gvCancel"].IsVisible = true;
                }
                else
                {
                    this.gvDocument.Columns["gvCancel"].IsVisible = false;
                }

                if (MainFile.isAddSecurity("btnAddExceptionAction"))
                {
                    addExcemption = true;
                }
                else
                {
                    addExcemption = false;
                }
                if (MainFile.isAddSecurity("btnAddDocumentTask"))
                {
                    action = true;
                }
                else
                {
                    action = false;
                }


            }
            else
            {
                action = true;
                addExcemption = true;
            }


            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocument = new List<Document>();
                lblDocumentID.Text = "-1";
                int empID = int.Parse(lblEmployeeID.Text);
                int companyLicencesID = -1;

                var vv1 = (from c in db.EmployeeLicenseShops
                          where c.employeeID == empID
                          select c).ToList();
                if (vv1.Count > 0)
                {
                    companyLicencesID = vv1[0].LicensesShop.companyLicencesID.Value;
                }




                var val = (from c in db.DocumentTypeDefinitions
                           where c.documentType_Code == lbldocumentType_Code.Text
                           select c).FirstOrDefault();

                if (val != null)
                    lbldocumentTypeDefID.Text = val.documentTypeDefID.ToString();


                lstDocument = (from c in db.Documents
                               where c.document_TypeID == empID &&
                               c.DocumentDef.documentTypeDefID ==1
                               select c).ToList();

                gvDocument.Rows.Clear();
                foreach (var item in lstDocument)
                {
                    gvDocument.Rows.AddNew();
                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[0].Value = item.documentTypeDefID;
                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[1].Value = item.documentDefID;
                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[2].Value = item.companyLicencesID;
                    gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[0].Value = item.documentID;
                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[4].Value = item.employeeID;

                    gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[1].Value = item.DocumentDef.documentDef_Name;
                    gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[2].Value = item.document_No;

                    if (item.document_startDate.HasValue)
                    {
                        gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[3].Value = item.document_startDate.Value.ToShortDateString();
                    }
                    else
                    {
                        gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[3].Value = "";
                    }

                    if (item.document_endDate.HasValue)
                    {
                        gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[4].Value = item.document_endDate.Value.ToShortDateString();
                    }
                    else
                    {
                        gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[4].Value = "";
                    }

                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[4].Value = item.commercialName;
                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[9].Value = item.document_endDate.Value.ToShortDateString();
                    //gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[10].Value = item.document_endDate.Value.Subtract(DateTime.Now).Days;
                    gvDocument.Rows[gvDocument.Rows.Count - 1].Cells[5].Value = getDocumentTransaction(item.documentID);
                    if (isHasRemarks(companyLicencesID))
                    {
                       // gvDocument.CurrentView.CurrentRow.DrawFill = true;
                        //GView.CurrentView.CurrentRow.BackColor = Color.Red;
                        gvDocument.Rows[gvDocument.Rows.Count - 1].Cells["gvRemarks"].Value = "ملاحظة ";
                    }

                    if (isHasRental(companyLicencesID))
                    {
                       // gvDocument.CurrentView.CurrentRow.DrawFill = true;
                        //GView.CurrentView.CurrentRow.BackColor = Color.Cyan;
                        gvDocument.Rows[gvDocument.Rows.Count - 1].Cells["gvRemarks"].Value += " ايجار ";
                    }

                }

                //var val1 = (from c in db.Documents
                //            where c.document_TypeID == empID &&
                //            c.DocumentDef.documentTypeDefID == 1
                //            select new
                //            {
                //                c.documentID,
                //                c.DocumentDef.documentDef_Name,
                //                c.document_No,
                //                c.document_startDate,
                //                c.document_endDate
                //            }).ToList();

                //gvDocument.DataSource = val1;
            }
        }

        private string getDocumentTransaction(int docID)
        {
            string temp = "";

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.DocumentTransactionTasks
                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                           where c.documentID == docID &&
                           c.documentTransTask_isClose ==false
                           select new
                           {
                               c2.transDef_namea
                           }).ToList();

                foreach (var item in val)
                {
                    temp += "-" + item.transDef_namea;
                }
            }

            return temp;
        }

        private bool isHasRental(int _companyLicencesID)
        {
            bool temp = false;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.RentLicenses
                           where (c.companyLicencesID == _companyLicencesID)
                           join d in db.RentLicensesDetails on c.rentLicenseID equals d.rentLicenseID
                           where d.rd_fromDate < DateTime.Now
                           && d.rd_isPaid == false
                           select c).ToList();
                if (val.Count > 0)
                {
                    temp = true;
                }

            }
            return temp;
        }

        private bool isHasRemarks(int lID)
        {
            bool temp = false;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.CompanyLicencesRemarks
                           where c.companyLicencesID == lID &&
                           c.comLicenseRem_isActive == true
                           select c).ToList();
                if (val.Count > 0)
                {
                    return true;
                }
            }

            return temp;
        }

        private bool isDocumentTransactionStepsStart(int docID)
        {
            bool temp = false;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.DocumentTransactionTasks
                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                           join c3 in db.documentTransTaskDetails on c.documentTransTaskID equals c3.documentTransTaskID
                           where c.documentID == docID &&
                           c3.documentTransSetp_isClose == true
                           select new
                           {
                               c2.transDef_namea
                           }).ToList();

                if (val.Count > 0)
                {
                    temp = true;
                }
            }

            return temp;
        }

        private bool isDocumentTransactionPaid(int docID)
        {
            bool temp = false;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.DocumentTransactionTasks
                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                           join c3 in db.DocumentTransactionTasksPayments on c.documentTransTaskID equals c3.documentTransTaskID
                           where c.documentID == docID
                           select new
                           {
                               c2.transDef_namea
                           }).ToList();

                if (val.Count > 0)
                {
                    temp = true;
                }
            }

            return temp;
        }


        bool isPressed = false;

        private void bindDocData1()
        {           
            if (lstDocument.Count > 0)
            {
                if (lblDocumentID.Text == "-1")
                {
                    lblDocumentID.Text = lstDocument[0].documentID.ToString();
                    gvDocument.Rows[0].IsSelected = true;
                }
                else
                {
                    int IDD = int.Parse(lblDocumentID.Text);
                    Document dc = lstDocument.Find(cc => cc.documentID == IDD);

                    positionDocument = lstDocument.FindIndex(cc => cc.documentID == IDD);
                    gvDocument.Rows[positionDocument].IsSelected = true;
                }

                mainbarDocument.lblCount.Text = positionDocument + 1 + " / " + lstDocument.Count;
                
            }
            else
            {
                lblDocumentID.Text = "-1";
                mainbarDocument.lblCount.Text = " 0 / 0";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //refreshDocument();
        }

        private void gvDocument_Click(object sender, EventArgs e)
        {
            if (gvDocument.Rows.Count > 0)
            {
                lblDocumentID.Text = gvDocument.CurrentRow.Cells[0].Value.ToString();
            }

            try
            {
                isPressed = false;
                if (this.gvDocument.Rows.Count > 0)
                {
                    if (gvDocument.CurrentColumn.Name == "gvCancel")
                    {
                        // TODO: check the security 
                        // check if task has been initialise
                        // check if any of the task details has been close
                        // TODO: delete the task and the task details
                        // Check Transaction Task Payment
                        int docID = int.Parse(gvDocument.CurrentRow.Cells["gvdocumentID"].Value.ToString());
                        if (!isDocumentTransactionStepsStart(docID))
                        {
                            if (!isDocumentTransactionPaid(docID))
                            {
                                if (gvDocument.CurrentRow.Cells["gvAction"].Value.ToString().Length > 0)
                                {
                                    DialogResult dr = MessageBox.Show("هل تريد حذف الاجراء", "Confirmation of Deletion", MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                                    {
                                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                                        {
                                            using (TransactionScope ts = new TransactionScope())
                                            {
                                                var val = (from c in db.DocumentTransactionTasks
                                                           join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                                                           join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                                                           join c3 in db.documentTransTaskDetails on c.documentTransTaskID equals c3.documentTransTaskID
                                                           where c.documentID == docID
                                                           select new
                                                           {
                                                               c3.documentTransTaskDetailsID
                                                           }).ToList();


                                                foreach (var item in val)
                                                {

                                                    int dID = item.documentTransTaskDetailsID;
                                                    documentTransTaskDetail dd = (from cc3 in db.documentTransTaskDetails
                                                                                  where cc3.documentTransTaskDetailsID == dID
                                                                                  select cc3).FirstOrDefault();
                                                    if (dd != null)
                                                        db.documentTransTaskDetails.DeleteObject(dd);
                                                    db.SaveChanges();
                                                }

                                                var val1 = (from c in db.DocumentTransactionTasks
                                                            join c1 in db.DocumentTransactions on c.documentTransID equals c1.documentTransID
                                                            join c2 in db.TransactionDefs on c1.transDefID equals c2.transDefID
                                                            join c3 in db.DocumentTransactionTasksPayments on c.documentTransTaskID equals c3.documentTransTaskID
                                                            where c.documentID == docID
                                                            select new
                                                            {
                                                                c3.DocumentTransactionTasksPaymentID
                                                            }).ToList();

                                                foreach (var item in val1)
                                                {

                                                    int pID = item.DocumentTransactionTasksPaymentID;
                                                    DocumentTransactionTasksPayment dd = (from cc2 in db.DocumentTransactionTasksPayments
                                                                                          where cc2.DocumentTransactionTasksPaymentID == pID
                                                                                          select cc2).FirstOrDefault();
                                                    if (dd != null)
                                                        db.DocumentTransactionTasksPayments.DeleteObject(dd);
                                                    db.SaveChanges();
                                                }


                                                DocumentTransactionTask dtt = (from cc1 in db.DocumentTransactionTasks
                                                                               where cc1.documentID == docID
                                                                               select cc1).SingleOrDefault();
                                                if (dtt != null)
                                                    db.DocumentTransactionTasks.DeleteObject(dtt);
                                                db.SaveChanges();

                                                db.AcceptAllChanges();
                                                ts.Complete();

                                                MessageBox.Show("تم الغاء العملية بنجاح");

                                            }
                                        }

                                        gvDocument.Rows.Clear();
                                        getDocData();
                                        bindDocData1();
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("Doesn't have task");
                                }
                            } // end of if paid
                            else
                            {
                                MessageBox.Show("تم دفع مبلغ الاجرائ ولا يمكن الغاء العملية");
                            }
                        }// end of document
                        else
                        {
                            MessageBox.Show("مراحل الاجراء بدأت , ولا يمكن الغاءها");
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radMaskedEditBox1_Click(object sender, EventArgs e)
        {

        }

        private void radMaskedEditBox4_Click(object sender, EventArgs e)
        {

        }

        private void radLabel73_Click(object sender, EventArgs e)
        {

        }

        private void radLabel74_Click(object sender, EventArgs e)
        {

        }

        private void radLabel82_Click(object sender, EventArgs e)
        {

        }

        private void radTextBox10_TextChanged(object sender, EventArgs e)
        {

        }





        // end of Document

        // end employee document
        // start of employee Passport

        void btnEdit_ClickPassport(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (passport.Count > 0)
                {
                    changePassport = "edit";
                    enablePassportControls3();
                }
            }
        }

        void btnAdd_ClickPassport(object sender, EventArgs e)
        {
            if (lblEmployeeID.Text != "-1")
            {
                if (passport.Count == 0)
                {
                    changePassport = "add";
                    enablePassportControls3();
                }

            }
        }

        void btnDelete_ClickPassport(object sender, EventArgs e)
        {
            try
            {
                if (lblEmployeeID.Text != "-1")
                {
                    if (lblpassportID.Text != "-1")
                    {
                        DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                            {
                                int noOfAffectedRows = 0;

                                using (TransactionScope ts = new TransactionScope())
                                {
                                    int bbID = int.Parse(lblpassportID.Text);
                                    EmployeePassport bb = (from c in db.EmployeePassports
                                                           where c.passportID ==
                                                               bbID
                                                           select c).FirstOrDefault();

                                    db.EmployeePassports.DeleteObject(bb);
                                    db.SaveChanges();

                                    int IDDD = int.Parse(lblEmployeeID.Text);
                                    Document bb1 = (from c in db.Documents
                                                    where
                                                        c.document_TypeID == IDDD &&
                                                        c.documentDefID==1
                                                    select c).FirstOrDefault();

                                    if (bb1 != null)
                                        db.Documents.DeleteObject(bb1);

                                    db.SaveChanges();
                                    ts.Complete();

                                }


                                bindData1();

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnCancel_ClickPassport(object sender, EventArgs e)
        {
            changePassport = "";
            disablePassportControls3();
            bindPassportData3();
        }

        void btnSave_ClickPassport(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;

                if (changePassport == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            EmployeePassport bb = new EmployeePassport();
                            Document bb1 = new Document();

                            bb.employeeID = int.Parse(lblEmployeeID.Text);
                            bb.pass_no = txtpass_no.Text;
                            bb.pass_placeOfIssue = cmbpass_placeOfIssue.Text;
                            bb.pass_type = cmbpass_type.Text;

                            DateTime ddd = DateTime.Now;
                            if (DateTime.TryParse(txtpass_issueDate.Text, out ddd))
                            {
                                bb.pass_issueDate = ddd;
                                bb1.document_startDate = ddd;
                            }
                            if (DateTime.TryParse(txtpass_expiryDate.Text, out ddd))
                            {
                                bb.pass_expiryDate = ddd;
                                bb1.document_endDate = ddd;
                            }


                            bb.pass_InUsed = true;

                            bb.added_Date = DateTime.Now;
                            bb.addedBy = MainClass.userID;

                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            

                            db.EmployeePassports.AddObject(bb);
                            rowsAffected = db.SaveChanges();
                            lblpassportID.Text = bb.passportID.ToString();

                            //// we must get the id of the document
                            //var val = from c in db.DocumentDefs
                            //          where c.documentDef_Name == "جواز السفر" ||
                            //          c.documentDef_Name == "الجواز السفر" ||
                            //          c.documentDef_Name == "جواز سفر"
                            //          select c;
                            //if (val.ToList().Count > 0)
                            //{
                            //    bb1.documentDefID = val.ToList()[0].documentDefID;
                            //}

                            bb1.documentDefID = 1;
                            bb1.document_No = txtpass_no.Text;
                            bb1.document_text1 = "";
                            bb1.document_text2 = "";
                            bb1.document_text3 = "";
                            bb1.document_text4 = "";
                            bb1.document_text5 = "";

                            bb1.document_placeOfIssue = cmbpass_placeOfIssue.Text;
                            bb1.document_TypeID = int.Parse(lblEmployeeID.Text);

                            bb1.document_Remarks = txtemployeeSponsor_useDuration.Text;

                            bb1.document_Status = "";
                            bb1.document_Type = cmbpass_type.Text;

                            bb1.added_Date = DateTime.Now;
                            bb1.addedBy = MainClass.userID;

                            bb1.edited_Date = DateTime.Now;
                            bb1.editedBy = MainClass.userID;

                            db.Documents.AddObject(bb1);
                            db.SaveChanges();

                            db.AcceptAllChanges();
                            ts.Complete();

                        }
                    }
                }
                else if (changePassport == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {

                            int bbID = int.Parse(lblpassportID.Text);
                            EmployeePassport bb = (from c in db.EmployeePassports
                                                   where c.passportID ==
                                                       bbID
                                                   select c).FirstOrDefault();

                            int IDDD = int.Parse(lblEmployeeID.Text);
                            Document bb1 = (from c in db.Documents
                                            where
                                                c.document_TypeID == IDDD &&
                                                c.documentDefID==1
                                            select c).FirstOrDefault();

                        using (TransactionScope ts = new TransactionScope())
                        {


                            bb.pass_no = txtpass_no.Text;
                            bb.pass_placeOfIssue = cmbpass_placeOfIssue.Text;
                            bb.pass_type = cmbpass_type.Text;

                            DateTime ddd = DateTime.Now;
                            if (DateTime.TryParse(txtpass_issueDate.Text, out ddd))
                            {
                                bb.pass_issueDate = ddd;
                                bb1.document_startDate = ddd;
                            }
                            else
                            {
                                bb.pass_issueDate = null;
                                bb1.document_startDate = null;
                            }
                            if (DateTime.TryParse(txtpass_expiryDate.Text, out ddd))
                            {
                                bb.pass_expiryDate = ddd;
                                bb1.document_endDate = ddd;
                            }
                            else
                            {
                                bb.pass_expiryDate = null;
                                bb1.document_endDate = null;
                            }

                            bb1.documentDefID = 1;
                            bb.edited_Date = DateTime.Now;
                            bb.editedBy = MainClass.userID;

                            //db.Employees.AddObject(bb);
                            rowsAffected = db.SaveChanges();


                            bb1.document_No = txtpass_no.Text;
                            bb1.document_text1 = "";
                            bb1.document_text2 = "";
                            bb1.document_text3 = "";
                            bb1.document_text4 = "";
                            bb1.document_text5 = "";

                            bb1.document_placeOfIssue = cmbpass_placeOfIssue.Text;
                            bb1.document_TypeID = int.Parse(lblEmployeeID.Text);

                            bb1.document_Remarks = txtemployeeSponsor_useDuration.Text;

                            bb1.document_Status = "";
                            bb1.document_Type = cmbpass_type.Text;


                            bb1.edited_Date = DateTime.Now;
                            bb1.editedBy = MainClass.userID;

                            db.SaveChanges();

                            db.AcceptAllChanges();
                            ts.Complete();



                        }
                    }
                }


                if (rowsAffected > 0)
                {
                    changePassport = "";
                    disablePassportControls3();
                    getPassport3();
                    bindPassportData3();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnExit_ClickPassport(object sender, EventArgs e)
        {
            Close();
        }


        private void disablePassportControls3()
        {

            mainbarPassport.enableButtons();
            enableAllPages();

            txtpass_expiryDate.ReadOnly = true;
            txtpass_issueDate.ReadOnly = true;
            txtpass_no.ReadOnly = true;

            cmbpass_type.Enabled = false;
            cmbpass_placeOfIssue.Enabled = false;


        }

        private void enablePassportControls3()
        {
            mainbarPassport.disableButton();

            txtpass_expiryDate.ReadOnly = false;
            txtpass_issueDate.ReadOnly = false;
            txtpass_no.ReadOnly = false;

            cmbpass_type.Enabled = true;
            cmbpass_placeOfIssue.Enabled = true;

            disableAllPages();
            PagePassport.Enabled = true;
        }

        private void getPassport3()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                passport = new List<EmployeePassport>();
                int empID = int.Parse(lblEmployeeID.Text);
                passport = (from c in db.EmployeePassports
                           where c.employeeID == empID
                           select c).ToList();
            }
        }

        private void bindPassportData3()
        {
            if (passport.Count > 0)
            {
                if (passport[0].pass_issueDate.HasValue)
                    this.txtpass_issueDate.Text = passport[0].pass_issueDate.Value.ToShortDateString();
                else
                    txtpass_issueDate.Text = "";

                if (passport[0].pass_expiryDate.HasValue)
                    this.txtpass_expiryDate.Text = passport[0].pass_expiryDate.Value.ToShortDateString();
                else
                    txtpass_expiryDate.Text = "";



                this.txtpass_no.Text = passport[0].pass_no;
                this.cmbpass_placeOfIssue.Text = passport[0].pass_placeOfIssue;
                this.cmbpass_type.Text = passport[0].pass_type;

                lblpassportID.Text = passport[0].passportID.ToString();
           

            }
            else
            {
                lblpassportID.Text = "-1";

                txtpass_no.Text = "";
                cmbpass_placeOfIssue.Text = "";
                cmbpass_type.Text = "";

                txtpass_issueDate.Text = "";
                txtpass_expiryDate.Text = "";

            }
        }

        private void btnFindCompany_Click(object sender, EventArgs e)
        {
            try
            {
                frmFindCompany frm = new frmFindCompany();
                frm.ShowDialog();
                if (frm.found)
                {
                    for (int i = 0; i < cmbcompanyID.Items.Count; i++)
                    {
                        if (cmbcompanyID.Items[i].Value.ToString() == frm.cID.ToString())
                        {
                            cmbcompanyID.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        DataTable printTable = null;
        private void makePrintData()
        {
            printTable = new DataTable("employeePrintTable");

            // Main employees Data
            printTable.Columns.Add("emp_ID", System.Type.GetType("System.Int16"));
            printTable.Columns.Add("emp_fileNo");
            printTable.Columns.Add("emp_employeeNo");
            printTable.Columns.Add("emp_nam1");
            printTable.Columns.Add("emp_nam2");
            printTable.Columns.Add("emp_nam3");
            printTable.Columns.Add("emp_nam4");
            printTable.Columns.Add("emp_nam5");
            printTable.Columns.Add("emp_name1");
            printTable.Columns.Add("emp_name2");
            printTable.Columns.Add("emp_name3");
            printTable.Columns.Add("emp_name4");
            printTable.Columns.Add("emp_name5");
            printTable.Columns.Add("emp_FullArabicName");
            printTable.Columns.Add("emp_FullEnglishName");
            printTable.Columns.Add("emp_civilID");
            printTable.Columns.Add("emp_gender");
            printTable.Columns.Add("emp_nationality");
            printTable.Columns.Add("emp_nationalitye");
            printTable.Columns.Add("emp_day");
            printTable.Columns.Add("emp_month");
            printTable.Columns.Add("emp_year");
            printTable.Columns.Add("emp_placeOfBirth");
            printTable.Columns.Add("emp_religion");
            printTable.Columns.Add("emp_maritalStatus");
            printTable.Columns.Add("emp_bloodType");
            printTable.Columns.Add("emp_educational");
            printTable.Columns.Add("emp_designation");
            
            printTable.Columns.Add("emp_division");
            printTable.Columns.Add("emp_CompanyWorkWith");
            printTable.Columns.Add("emp_GovContractNo");
            printTable.Columns.Add("emp_appointmentDate");

            printTable.Columns.Add("emp_oldNoOFYears");
            printTable.Columns.Add("emp_oldSalary");
            printTable.Columns.Add("emp_oldDesignation");
            printTable.Columns.Add("emp_oldGovContractNo");

            // address of the employee
            printTable.Columns.Add("emp_governate");
            printTable.Columns.Add("emp_block");
            printTable.Columns.Add("emp_jada");
            printTable.Columns.Add("emp_buildingType");
            printTable.Columns.Add("emp_lotNo");
            printTable.Columns.Add("emp_region");
            printTable.Columns.Add("emp_street");
            printTable.Columns.Add("emp_buildinNo");
            printTable.Columns.Add("emp_floorNo");
            printTable.Columns.Add("emp_flatNo");
            printTable.Columns.Add("emp_entrance");
            printTable.Columns.Add("emp_POBOX");
            printTable.Columns.Add("emp_PostalCode");
            printTable.Columns.Add("emp_PostOfficeName");
            printTable.Columns.Add("emp_telephone");

            // address of the employee
            printTable.Columns.Add("emp_governate1");
            printTable.Columns.Add("emp_block1");
            printTable.Columns.Add("emp_jada1");
            printTable.Columns.Add("emp_buildingType1");
            printTable.Columns.Add("emp_lotNo1");
            printTable.Columns.Add("emp_region1");
            printTable.Columns.Add("emp_street1");
            printTable.Columns.Add("emp_buildinNo1");
            printTable.Columns.Add("emp_floorNo1");
            printTable.Columns.Add("emp_flatNo1");
            printTable.Columns.Add("emp_entrance1");
            printTable.Columns.Add("emp_POBOX1");
            printTable.Columns.Add("emp_PostalCode1");
            printTable.Columns.Add("emp_PostOfficeName1");
            printTable.Columns.Add("emp_telephone1");

            printTable.Columns.Add("ArabicMoney");
            printTable.Columns.Add("EnglishMoney");

            printTable.Columns.Add("homeAddress");
            printTable.Columns.Add("workAdress");

            // sponsor of the employee
            printTable.Columns.Add("sponsor_nam1");
            printTable.Columns.Add("sponsor_nam2");
            printTable.Columns.Add("sponsor_nam3");
            printTable.Columns.Add("sponsor_nam4");
            printTable.Columns.Add("sponsor_nam5");
            printTable.Columns.Add("sponsor_name1");
            printTable.Columns.Add("sponsor_name2");
            printTable.Columns.Add("sponsor_name3");
            printTable.Columns.Add("sponsor_name4");
            printTable.Columns.Add("sponsor_name5");
            printTable.Columns.Add("sponsor_ArabicName");
            printTable.Columns.Add("sponsor_EnglishName");
            printTable.Columns.Add("sponsor_FileNo");
            printTable.Columns.Add("sponsor_UniqueNo");
            printTable.Columns.Add("sponsor_OwnerName");
            printTable.Columns.Add("sponsor_OwnerNationality");
            printTable.Columns.Add("sponsor_OwnerNationalityNo");
            printTable.Columns.Add("sponsor_CivilID");
            printTable.Columns.Add("sponsor_licenceNo");

            // sponsor address
            printTable.Columns.Add("sponsor_governate");
            printTable.Columns.Add("sponsor_area");
            printTable.Columns.Add("sponsor_jada");
            printTable.Columns.Add("sponsor_region");
            printTable.Columns.Add("sponsor_street");
            printTable.Columns.Add("sponsor_buildingType");
            printTable.Columns.Add("sponsor_lotNo");
            printTable.Columns.Add("sponsor_buildingNo");
            printTable.Columns.Add("sponsor_floorno");
            printTable.Columns.Add("sponsor_appartmentNo");
            printTable.Columns.Add("sponsor_entrance");
            printTable.Columns.Add("sponsor_pbox");
            printTable.Columns.Add("sponsor_postalCode");
            printTable.Columns.Add("sponsor_tel1");

            printTable.Columns.Add("work_condition1");
            printTable.Columns.Add("work_condition2");
            printTable.Columns.Add("work_condition3");
            printTable.Columns.Add("work_condition4");
            printTable.Columns.Add("work_condition5");


            printTable.Columns.Add("Auth_nam1");
            printTable.Columns.Add("Auth_nam2");
            printTable.Columns.Add("Auth_nam3");
            printTable.Columns.Add("Auth_nam4");
            printTable.Columns.Add("Auth_nam5");
            printTable.Columns.Add("Auth_name1");
            printTable.Columns.Add("Auth_name2");
            printTable.Columns.Add("Auth_name3");
            printTable.Columns.Add("Auth_name4");
            printTable.Columns.Add("Auth_name5");
            printTable.Columns.Add("Auth_ArabicName");
            printTable.Columns.Add("Auth_EnglishName");
            printTable.Columns.Add("Auth_CicilID");
            printTable.Columns.Add("Auth_Designation");
            printTable.Columns.Add("Auth_nationality");

            // residency of the employee
            printTable.Columns.Add("res_no");
            printTable.Columns.Add("res_no1");
            printTable.Columns.Add("res_no2");
            printTable.Columns.Add("res_no3");
            printTable.Columns.Add("res_no4");
            printTable.Columns.Add("res_no5");
            printTable.Columns.Add("res_no6");
            printTable.Columns.Add("res_no7");
            printTable.Columns.Add("res_no8");
            printTable.Columns.Add("res_no9");
            printTable.Columns.Add("res_type");
            printTable.Columns.Add("res_IssueDate");
            printTable.Columns.Add("res_ExpiryDate");
            printTable.Columns.Add("res_EntryDate");
            printTable.Columns.Add("res_Salary");
            printTable.Columns.Add("res_designation");
            printTable.Columns.Add("res_designatione");
            printTable.Columns.Add("res_hirePeriod");
            printTable.Columns.Add("res_workPermitNo");

            printTable.Columns.Add("res_passGovernate");
            printTable.Columns.Add("res_resiGovernate");

            // oldSponsor
            printTable.Columns.Add("oldsponsor_nam1");
            printTable.Columns.Add("oldsponsor_nam2");
            printTable.Columns.Add("oldsponsor_nam3");
            printTable.Columns.Add("oldsponsor_nam4");
            printTable.Columns.Add("oldsponsor_nam5");
            printTable.Columns.Add("oldsponsor_name1");
            printTable.Columns.Add("oldsponsor_name2");
            printTable.Columns.Add("oldsponsor_name3");
            printTable.Columns.Add("oldsponsor_name4");
            printTable.Columns.Add("oldsponsor_name5");
            printTable.Columns.Add("oldsponsor_ArabicName");
            printTable.Columns.Add("oldsponsor_EnglishName");
            printTable.Columns.Add("oldsponsor_FileNo");
            printTable.Columns.Add("oldsponsor_UniqueNo");
            printTable.Columns.Add("oldsponsor_OwnerName");
            printTable.Columns.Add("oldsponsor_OwnerNationality");
            printTable.Columns.Add("oldsponsor_OwnerNationalityNo");
            printTable.Columns.Add("oldsponsor_CivilID");
            printTable.Columns.Add("oldsponsor_licenceNo");
            printTable.Columns.Add("oldAuth_nam1");
            printTable.Columns.Add("oldAuth_nam2");
            printTable.Columns.Add("oldAuth_nam3");
            printTable.Columns.Add("oldAuth_nam4");
            printTable.Columns.Add("oldAuth_nam5");
            printTable.Columns.Add("oldAuth_name1");
            printTable.Columns.Add("oldAuth_name2");
            printTable.Columns.Add("oldAuth_name3");
            printTable.Columns.Add("oldAuth_name4");
            printTable.Columns.Add("oldAuth_name5");
            printTable.Columns.Add("oldAuth_ArabicName");
            printTable.Columns.Add("oldAuth_EnglishName");
            printTable.Columns.Add("oldAuth_CicilID");
            printTable.Columns.Add("oldAuth_Designation");
            printTable.Columns.Add("oldAuth_nationality");

            // passport Information
            printTable.Columns.Add("pass_no");
            printTable.Columns.Add("pass_IssueDate");
            printTable.Columns.Add("pass_ExpiryDate");
            printTable.Columns.Add("pass_PlaceOfIssue");
            printTable.Columns.Add("pass_Tpye");

            // passport dependency
            printTable.Columns.Add("dep_no");

            printTable.Columns.Add("dep_aname1");
            printTable.Columns.Add("dep_ename1");
            printTable.Columns.Add("dep_birthDay1");
            printTable.Columns.Add("dep_gender1");
            printTable.Columns.Add("dep_nationality1");
            printTable.Columns.Add("dep_relaion1");
            printTable.Columns.Add("dep_religion1");

            printTable.Columns.Add("dep_aname2");
            printTable.Columns.Add("dep_ename2");
            printTable.Columns.Add("dep_birthDay2");
            printTable.Columns.Add("dep_gender2");
            printTable.Columns.Add("dep_nationality2");
            printTable.Columns.Add("dep_relaion2");
            printTable.Columns.Add("dep_religion2");

            printTable.Columns.Add("dep_aname3");
            printTable.Columns.Add("dep_ename3");
            printTable.Columns.Add("dep_birthDay3");
            printTable.Columns.Add("dep_gender3");
            printTable.Columns.Add("dep_nationality3");
            printTable.Columns.Add("dep_relaion3");
            printTable.Columns.Add("dep_religion3");

            printTable.Columns.Add("dep_aname4");
            printTable.Columns.Add("dep_ename4");
            printTable.Columns.Add("dep_birthDay4");
            printTable.Columns.Add("dep_gender4");
            printTable.Columns.Add("dep_nationality4");
            printTable.Columns.Add("dep_relaion4");
            printTable.Columns.Add("dep_religion4");

            printTable.Columns.Add("dep_aname5");
            printTable.Columns.Add("dep_ename5");
            printTable.Columns.Add("dep_birthDay5");
            printTable.Columns.Add("dep_gender5");
            printTable.Columns.Add("dep_nationality5");
            printTable.Columns.Add("dep_relaion5");
            printTable.Columns.Add("dep_religion5");

            printTable.Columns.Add("dep_aname6");
            printTable.Columns.Add("dep_ename6");
            printTable.Columns.Add("dep_birthDay6");
            printTable.Columns.Add("dep_gender6");
            printTable.Columns.Add("dep_nationality6");
            printTable.Columns.Add("dep_relaion6");
            printTable.Columns.Add("dep_religion6");

            // work contract
            printTable.Columns.Add("workContract_date");
            printTable.Columns.Add("workContract_Startdate");
            printTable.Columns.Add("workContract_TypeDuration");
            printTable.Columns.Add("workContract_Duration");
            printTable.Columns.Add("workContract_PayDuration");


            //print Type
            printTable.Columns.Add("emp_appDate");
            printTable.Columns.Add("emp_appType");
            printTable.Columns.Add("choose1");
            printTable.Columns.Add("choose2");
            printTable.Columns.Add("choose3");
            printTable.Columns.Add("choose4");
            printTable.Columns.Add("choose5");
            printTable.Columns.Add("choose6");
            printTable.Columns.Add("choose7");
            printTable.Columns.Add("choose8");

            printTable.Columns.Add("choose9");
            printTable.Columns.Add("choose10");
            printTable.Columns.Add("choose11");
            printTable.Columns.Add("choose12");
            printTable.Columns.Add("choose13");
            printTable.Columns.Add("choose14");
            printTable.Columns.Add("choose15");
            printTable.Columns.Add("choose16");

            printTable.Columns.Add("choose17");
            printTable.Columns.Add("choose18");
            printTable.Columns.Add("choose19");
            printTable.Columns.Add("choose20");
            printTable.Columns.Add("choose21");
            printTable.Columns.Add("choose22");
            printTable.Columns.Add("choose23");
            printTable.Columns.Add("choose24");

            printTable.Columns.Add("choose25");
            printTable.Columns.Add("choose26");
            printTable.Columns.Add("choose27");
            printTable.Columns.Add("choose28");

            printTable.Columns.Add("radio1");
            printTable.Columns.Add("radio2");
            printTable.Columns.Add("radio3");
            printTable.Columns.Add("radio4");
            printTable.Columns.Add("radio5");
            printTable.Columns.Add("radio6");
            printTable.Columns.Add("radio7");
            printTable.Columns.Add("radio8");


            printTable.Columns.Add("healthRenew");
            printTable.Columns.Add("healthExpiry");
            printTable.Columns.Add("healthNO");
            printTable.Columns.Add("noOFLeave");
            printTable.Columns.Add("healthDate");



        }

        private void loadPrintData(int _employeeID)
        {

            makePrintData();

            if (_employeeID != -1)
            {
               
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {

                    Employee ee = (from c in db.Employees
                                           where c.employeeID == _employeeID
                                           select c).FirstOrDefault();

                    DataRow dr = printTable.NewRow();


                    // Main employees Data
                    dr["emp_ID"] = ee.employeeID;
                    dr["emp_fileNo"] = ee.emp_fileNo;
                    dr["emp_employeeNo"] = ee.emp_No;
                    dr["emp_nam1"] = ee.emp_namea1;
                    dr["emp_nam2"] = ee.emp_namea2;
                    dr["emp_nam3"] = ee.emp_namea3;
                    dr["emp_nam4"] = ee.emp_namea4;
                    dr["emp_nam5"] = ee.emp_namea5;
                    dr["emp_name1"] = ee.emp_namee1;
                    dr["emp_name2"] = ee.emp_namee2;
                    dr["emp_name3"] = ee.emp_namee3;
                    dr["emp_name4"] = ee.emp_namee4;
                    dr["emp_name5"] = ee.emp_namee5;
                    dr["emp_FullArabicName"] = ee.emp_namefullar;
                    dr["emp_FullEnglishName"] = ee.emp_namefullen;
                    dr["emp_civilID"] = ee.emp_civilID;

                    dr["emp_day"] = ee.emp_day;
                    dr["emp_month"] = ee.emp_month;
                    dr["emp_year"] = ee.emp_year;

                    // validation
                    dr["emp_gender"] = ee.Gender.gender_namea;

                    // validation
                    dr["emp_nationality"] = ee.Nationality.natio_namea;
                    dr["emp_nationalitye"] = ee.Nationality.natio_namee;

                    dr["emp_placeOfBirth"] = ee.Nationality1.natio_namea;
                    dr["emp_religion"] = ee.Religion.religion_namea;
                    dr["emp_maritalStatus"] = ee.MaritalStatu.marital_namea;

                    dr["emp_bloodType"] = ee.emp_bloodType;
                    dr["emp_educational"] = ee.Education.edu_namea;

                    dr["emp_designation"] = "";
                    dr["emp_division"] = "";

                    dr["emp_CompanyWorkWith"] = "";

                    dr["emp_GovContractNo"] = ee.oldoldGoveContract;
                    dr["emp_appointmentDate"] = "";
                    dr["emp_oldSalary"] = ee.emp_OldSalary;
                    dr["emp_oldNoOFYears"] = ee.emp_OldNoOfYears;
                    dr["emp_oldDesignation"] = ee.emp_OldDesignation;
                    dr["emp_oldGovContractNo"] = ee.oldoldGoveContract;
                    dr["oldsponsor_ArabicName"] = ee.oldoldCommercialName;
                    dr["oldsponsor_FileNo"] = ee.oldoldFileNo;
                    dr["oldsponsor_UniqueNo"] = "";
                    dr["oldsponsor_OwnerName"] = ee.oldoladOwnerName;
                    dr["oldsponsor_licenceNo"] = ee.oldoldLicenceNO;
                    dr["oldAuth_ArabicName"] = ee.oldoldAuthName;


                    // address of the employee
                    dr["emp_governate"] = ee.Mohafaza;
                    dr["emp_block"] = ee.Quet3a;
                    dr["emp_jada"] = ee.Jada;
                    dr["emp_buildingType"] = ee.BuilduingType;
                    dr["emp_lotNo"] = "";
                    dr["emp_region"] = "";
                    dr["emp_street"] = ee.Share3;
                    dr["emp_buildinNo"] = "";
                    dr["emp_floorNo"] =ee.floor;
                    dr["emp_flatNo"] = ee.appartment;
                    dr["emp_entrance"] = ee.appartment;
                    dr["emp_POBOX"] = ee.pobox;
                    dr["emp_PostalCode"] = ee.poboxOffice;
                    dr["emp_PostOfficeName"] = "";
                    dr["emp_telephone"] = ee.phoneNo;


                    // address of the employee
                    //dr["emp_governate1"] = "";
                    //dr["emp_block1"] = "";
                    //dr["emp_jada1"] = "";
                    //dr["emp_buildingType1"] = "";
                    //dr["emp_lotNo1"] = "";
                    //dr["emp_region1"] = "";
                    //dr["emp_street1"] = "";
                    //dr["emp_buildinNo1"] = "";
                    //dr["emp_floorNo1"] = "";
                    //dr["emp_flatNo1"] = "";
                    //dr["emp_entrance1"] = "";
                    //dr["emp_POBOX1"] = "";
                    //dr["emp_PostalCode1"] = "";
                    //dr["emp_PostOfficeName1"] = "";
                    //dr["emp_telephone1"] = "";


                    //print Type
                    dr["emp_appDate"] = AppDateFormat.FormatStoredDate(ee.emp_appDate);
                    dr["emp_appType"] = ee.emp_appType;
                    dr["choose1"] = ee.emp_res1;
                    dr["choose2"] = ee.emp_res2;
                    dr["choose3"] = ee.emp_res3;
                    dr["choose4"] = ee.emp_res4;
                    dr["choose5"] = ee.emp_res5;
                    dr["choose6"] = ee.emp_res6;

                    dr["choose7"] = ee.emp_res7;
                    dr["choose8"] = ee.emp_res8;
                    dr["choose9"] = ee.emp_res9;
                    dr["choose10"] = ee.emp_res11;

                    dr["choose11"] = ee.emp_res11;
                    dr["choose12"] = ee.emp_res12;
                    dr["choose13"] = ee.emp_res13;
                    dr["choose14"] = ee.emp_res14;
                    dr["choose15"] = ee.emp_res15;
                    dr["choose16"] = ee.emp_res16;
                    dr["choose17"] = ee.emp_res17;
                    dr["choose18"] = ee.emp_res18;

                    dr["choose19"] = ee.emp_res19;
                    dr["choose20"] = ee.emp_res20;

                    dr["choose21"] = ee.emp_res21;
                    dr["choose22"] = ee.emp_res22;
                    dr["choose23"] = ee.emp_res23;
                    dr["choose24"] = ee.emp_res24;
                    dr["choose25"] = ee.emp_res25;
                    dr["choose26"] = ee.emp_res26;
                    dr["choose27"] = ee.emp_res27;
                    dr["choose28"] = ee.emp_res28;



                    EmployeeSponsor es = (from c in db.EmployeeSponsors 
                                          where c.employeeID == _employeeID select c).FirstOrDefault();

                    if (es != null)
                    {
                        dr["sponsor_nam1"] = es.CompanyLicence.Company.comp_nam1;
                        dr["sponsor_nam2"] = es.CompanyLicence.Company.comp_nam2;
                        dr["sponsor_nam3"] = es.CompanyLicence.Company.comp_nam3;
                        dr["sponsor_nam4"] = es.CompanyLicence.Company.comp_nam4;
                        dr["sponsor_nam5"] = es.CompanyLicence.Company.comp_nam5;
                        dr["sponsor_name1"] = es.CompanyLicence.Company.comp_name1;
                        dr["sponsor_name2"] = es.CompanyLicence.Company.comp_name2;
                        dr["sponsor_name3"] = es.CompanyLicence.Company.comp_name3;
                        dr["sponsor_name4"] = es.CompanyLicence.Company.comp_name4;
                        dr["sponsor_name5"] = es.CompanyLicence.Company.comp_name5;

                        dr["sponsor_OwnerNationality"] = es.CompanyLicence.Company.comp_nationality;
                        dr["sponsor_OwnerNationalityNo"] = es.CompanyLicence.Company.comp_nationalityNo;
                        dr["sponsor_UniqueNo"] = es.CompanyLicence.Company.comp_uniqueNo;

                        dr["Auth_nam1"] = "";
                        dr["Auth_nam2"] = "";
                        dr["Auth_nam3"] = "";
                        dr["Auth_nam4"] = "";
                        dr["Auth_nam5"] = "";
                        dr["Auth_name1"] = "";
                        dr["Auth_name2"] = "";
                        dr["Auth_name3"] = "";
                        dr["Auth_name4"] = "";
                        dr["Auth_name5"] = "";


                        dr["sponsor_ArabicName"] = es.CompanyLicence.commercialName;
                        dr["sponsor_EnglishName"] = es.CompanyLicence.Company.comp_englishName;
                        dr["sponsor_FileNo"] = es.CompanyLicence.Company.comp_fileNo;

                        dr["sponsor_OwnerName"] = es.CompanyLicence.Company.comp_arabicName;
                        if (es.CompanyLicence.licenseUniqueNo.Length > 0)
                        {
                            dr["sponsor_UniqueNo"] = es.CompanyLicence.licenseUniqueNo;
                        }
                        else
                        {
                            dr["sponsor_UniqueNo"] = es.CompanyLicence.Company.comp_uniqueNo;
                        }

                        dr["sponsor_CivilID"] = es.CompanyLicence.Company.comp_fileNo;
                        dr["sponsor_licenceNo"] = es.CompanyLicence.licenseNo;


                        dr["sponsor_governate"] = es.CompanyLicence.Mohafaza;
                        dr["sponsor_area"] = es.CompanyLicence.Quet3a;
                        dr["sponsor_jada"] = "";
                        dr["sponsor_region"] = es.CompanyLicence.Mantaqa;
                        dr["sponsor_street"] = es.CompanyLicence.Share3;
                        dr["sponsor_buildingType"] = es.CompanyLicence.BuilduingType;
                        dr["sponsor_lotNo"] = "";
                        dr["sponsor_buildingNo"] = "";
                        dr["sponsor_floorno"] = es.CompanyLicence.floor;
                        dr["sponsor_appartmentNo"] = es.CompanyLicence.appartment;
                        dr["sponsor_entrance"] = es.CompanyLicence.appartment;
                        dr["sponsor_pbox"] = es.CompanyLicence.pobox;
                        dr["sponsor_postalCode"] = es.CompanyLicence.poboxOffice;
                        dr["sponsor_tel1"] = es.CompanyLicence.contactNo;

                        dr["work_condition1"] = "";
                        dr["work_condition2"] = "";
                        dr["work_condition3"] = "";
                        dr["work_condition4"] = "";
                        dr["work_condition5"] = es.CompanyLicence.fulladdress;


                        CompanyAuthorization cau = (from caut in db.CompanyAuthorizations
                                                    where caut.companyID == es.CompanyLicence.companyID
                                                    && caut.auth_isCurrent == true
                                                    select caut).FirstOrDefault();

                        dr["Auth_ArabicName"] = "";
                        dr["Auth_EnglishName"] = "";
                        dr["Auth_CicilID"] = "";
                        dr["Auth_Designation"] = "";
                        dr["Auth_nationality"] = "";

                        if(cau != null)
                        {
                            dr["Auth_ArabicName"] = cau.AuthorizationPerson.fauth_ullArabicName;
                            dr["Auth_EnglishName"] = cau.AuthorizationPerson.fauth_ullEnglishName;
                            dr["Auth_CicilID"] = cau.AuthorizationPerson.auth_CivilID;
                        }

                        



                        dr["oldsponsor_nam1"] = "";
                        dr["oldsponsor_nam2"] = "";
                        dr["oldsponsor_nam3"] = "";
                        dr["oldsponsor_nam4"] = "";
                        dr["oldsponsor_nam5"] = "";
                        dr["oldsponsor_name1"] = "";
                        dr["oldsponsor_name2"] = "";
                        dr["oldsponsor_name3"] = "";
                        dr["oldsponsor_name4"] = "";
                        dr["oldsponsor_name5"] = "";

                        dr["oldsponsor_EnglishName"] = "";

                        dr["oldsponsor_OwnerNationality"] = "";
                        dr["oldsponsor_OwnerNationalityNo"] = "";
                        dr["oldsponsor_CivilID"] = "";
                        //dr["oldsponsor_ArabicName"] = "";
                        //dr["oldsponsor_FileNo"] = "";
                        dr["oldsponsor_UniqueNo"] = "";
                        //dr["oldsponsor_OwnerName"] = "";
                        //dr["oldsponsor_licenceNo"] = "";
                        //dr["oldAuth_ArabicName"] = "";

                        dr["oldAuth_nam1"] = "";
                        dr["oldAuth_nam2"] = "";
                        dr["oldAuth_nam3"] = "";
                        dr["oldAuth_nam4"] = "";
                        dr["oldAuth_nam5"] = "";
                        dr["oldAuth_name1"] = "";
                        dr["oldAuth_name2"] = "";
                        dr["oldAuth_name3"] = "";
                        dr["oldAuth_name4"] = "";
                        dr["oldAuth_name5"] = "";

                        dr["oldAuth_EnglishName"] = "";
                        dr["oldAuth_CicilID"] = "";
                        dr["oldAuth_Designation"] = "";
                        dr["oldAuth_nationality"] = "";




                        // residency of the employee
                        string res_noP = es.employeeSponsor_residencyNo;
                        dr["res_no"] = res_noP;
                        if (res_noP.Length == 9)
                        {
                            dr["res_no1"] = res_noP.Substring(0, 1);
                            dr["res_no2"] = res_noP.Substring(1, 1);
                            dr["res_no3"] = res_noP.Substring(2, 1);
                            dr["res_no4"] = res_noP.Substring(3, 1);
                            dr["res_no5"] = res_noP.Substring(4, 1);
                            dr["res_no6"] = res_noP.Substring(5, 1);
                            dr["res_no7"] = res_noP.Substring(6, 1);
                            dr["res_no8"] = res_noP.Substring(7, 1);
                            dr["res_no9"] = res_noP.Substring(8, 1);
                        }
                        else
                        {
                            dr["res_no1"] = "";
                            dr["res_no2"] = "";
                            dr["res_no3"] = "";
                            dr["res_no4"] = "";
                            dr["res_no5"] = "";
                            dr["res_no6"] = "";
                            dr["res_no7"] = "";
                            dr["res_no8"] = "";
                            dr["res_no9"] = "";
                        }
                        dr["res_type"] = "";


                        dr["ArabicMoney"] = "";
                        dr["EnglishMoney"] = "";

                        dr["res_IssueDate"] = "";
                        dr["res_ExpiryDate"] = "";
                        dr["res_EntryDate"] = "";
                        if (es.employeeSponsor_residencyStartDate.HasValue)
                        {
                            dr["res_IssueDate"] = es.employeeSponsor_residencyStartDate.Value.ToShortDateString();
                        }

                        if (es.employeeSponsor_residencyEndDate.HasValue)
                        {
                            dr["res_ExpiryDate"] = es.employeeSponsor_residencyEndDate.Value.ToShortDateString();
                        }
                        if (es.employeeSponsor_entryDate.HasValue)
                        {
                            dr["res_EntryDate"] = AppDateFormat.Format(es.employeeSponsor_entryDate.Value);
                        }

                        dr["res_Salary"] = es.employeeSponsor_salary;

                        dr["ArabicMoney"] = "";
                        dr["EnglishMoney"] = "";

                        dr["res_designation"] = es.Designation.desig_namea;
                        dr["res_designatione"] = es.Designation.desig_namee;
                        dr["res_hirePeriod"] = es.employeeSponsor_useDuration;
                        dr["res_workPermitNo"] = es.employeeSponsor_WorkPermitNo;


                        dr["res_passGovernate"] = es.employeeSponsor_Jawazat;
                        dr["res_resiGovernate"] = es.employeeSponsor_Shou2oun;

                        dr["work_condition1"] = es.CompanyLicence.Mohafaza;
                        dr["work_condition2"] = es.CompanyLicence.Mohafaza;
                    }


                        EmployeePassport ep = (from c in db.EmployeePassports
                                               where c.employeeID == _employeeID
                                               && c.pass_InUsed == true
                                               select c).FirstOrDefault();
                        if (ep != null)
                        {
                            // passport Information
                            dr["pass_no"] = ep.pass_no;
                            dr["pass_PlaceOfIssue"] = ep.pass_placeOfIssue;
                            dr["pass_Tpye"] = ep.pass_type;
                            dr["pass_IssueDate"] = "";
                            dr["pass_ExpiryDate"] = "";
                            if (ep.pass_issueDate.HasValue)
                            {
                                dr["pass_IssueDate"] = ep.pass_issueDate.Value.ToShortDateString();
                            }
                            if (ep.pass_expiryDate.HasValue)
                            {
                                dr["pass_ExpiryDate"] = ep.pass_expiryDate.Value.ToShortDateString();
                            }
                        }



                    // employee dependency

                        List<Dependent> lstdep = (from c in db.Dependents
                                                  where c.employeeID == _employeeID
                                                  select c).ToList();

                        dr["dep_no"] = lstdep.Count.ToString();
                        if (lstdep.Count > 0)
                        {

                            dr["dep_aname1"] = lstdep[0].depend_aName;
                            dr["dep_ename1"] = lstdep[0].depend_eName;
                            dr["dep_birthDay1"] = lstdep[0].depend_birthday;
                            dr["dep_gender1"] = lstdep[0].depend_gender;
                            dr["dep_nationality1"] = lstdep[0].depend_nationality;
                            dr["dep_relaion1"] = lstdep[0].depend_relation;
                            dr["dep_religion1"] = lstdep[0].depend_religion;
                        }

                        if (lstdep.Count > 1)
                        {

                            dr["dep_aname2"] = lstdep[1].depend_aName;
                            dr["dep_ename2"] = lstdep[1].depend_eName;
                            dr["dep_birthDay2"] = lstdep[1].depend_birthday;
                            dr["dep_gender2"] = lstdep[1].depend_gender;
                            dr["dep_nationality2"] = lstdep[1].depend_nationality;
                            dr["dep_relaion2"] = lstdep[1].depend_relation;
                            dr["dep_religion2"] = lstdep[1].depend_religion;
                        }

                        if (lstdep.Count > 2)
                        {

                            dr["dep_aname3"] = lstdep[2].depend_aName;
                            dr["dep_ename3"] = lstdep[2].depend_eName;
                            dr["dep_birthDay3"] = lstdep[2].depend_birthday;
                            dr["dep_gender3"] = lstdep[2].depend_gender;
                            dr["dep_nationality3"] = lstdep[2].depend_nationality;
                            dr["dep_relaion3"] = lstdep[2].depend_relation;
                            dr["dep_religion3"] = lstdep[2].depend_religion;
                        }

                        if (lstdep.Count > 3)
                        {

                            dr["dep_aname4"] = lstdep[3].depend_aName;
                            dr["dep_ename4"] = lstdep[3].depend_eName;
                            dr["dep_birthDay4"] = lstdep[3].depend_birthday;
                            dr["dep_gender4"] = lstdep[3].depend_gender;
                            dr["dep_nationality4"] = lstdep[3].depend_nationality;
                            dr["dep_relaion4"] = lstdep[3].depend_relation;
                            dr["dep_religion4"] = lstdep[3].depend_religion;
                        }

                        if (lstdep.Count > 4)
                        {

                            dr["dep_aname5"] = lstdep[4].depend_aName;
                            dr["dep_ename5"] = lstdep[4].depend_eName;
                            dr["dep_birthDay5"] = lstdep[4].depend_birthday;
                            dr["dep_gender5"] = lstdep[4].depend_gender;
                            dr["dep_nationality5"] = lstdep[4].depend_nationality;
                            dr["dep_relaion5"] = lstdep[4].depend_relation;
                            dr["dep_religion5"] = lstdep[4].depend_religion;
                        }

                        if (lstdep.Count > 5)
                        {

                            dr["dep_aname6"] = lstdep[5].depend_aName;
                            dr["dep_ename6"] = lstdep[5].depend_eName;
                            dr["dep_birthDay6"] = lstdep[5].depend_birthday;
                            dr["dep_gender6"] = lstdep[5].depend_gender;
                            dr["dep_nationality6"] = lstdep[5].depend_nationality;
                            dr["dep_relaion6"] = lstdep[5].depend_relation;
                            dr["dep_religion6"] = lstdep[5].depend_religion;
                        }


                    /// employee contract
                    EmployeeWorkContract ewc = (from c in db.EmployeeWorkContracts
                                                where c.employeeID == _employeeID
                                                select c).FirstOrDefault();

                    if (ewc != null)
                    {
                        dr["workContract_date"] = "";
                        dr["workContract_Startdate"] = "";

                        if (ewc.wc_date.HasValue)
                        {
                            dr["workContract_date"] = ewc.wc_date.Value.ToShortDateString();
                        }
                        if (ewc.wc_startingContract.HasValue)
                        {
                            dr["workContract_Startdate"] = ewc.wc_startingContract.Value.ToShortDateString();
                        }

                        dr["workContract_TypeDuration"] = ewc.wc_durationType;
                        dr["workContract_Duration"] = ewc.wc_duration;
                        dr["workContract_PayDuration"] = ewc.wc_payEvery;
                    }


                    


                   dr["healthRenew"] = "";
                    
                        dr["healthExpiry"] = "";
                    

                    dr["homeAddress"] = "";
                    dr["workAdress"] = "";
                    dr["healthNO"] = "";
                    dr["noOFLeave"] = "";

                    dr["healthDate"] = "";

                    printTable.Rows.Add(dr);

                }
            }


        }
        private void btnResidencyApplicationForm_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                //ds.WriteXmlSchema(Application.StartupPath + "/../../xml/PrintEmployee.xsd");
                //MessageBox.Show("Test it good !");
                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "passport";
                rv.ds = ds;
                rv.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnResidencyApplicationFormBack_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "bpassport";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnTransferWorkingPermit_Click(object sender, EventArgs e)
        {
            try
            {

                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "migration";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnInternalSecurity_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "migration2";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnTransferWorkingPermitBack_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "bmigration";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnContractReport_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

               // ds.WriteXmlSchema("employeeData.xsd");
                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "workContract";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            try
            {

                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "photo";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnReceiningIndemity1_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "getIndemity";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnMokhalasa_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "finalRep";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnNotWork_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "stopWork";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEmployeeReport_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "employeeReport";
                rv.ds = ds;
                rv.ShowDialog();
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AllEmployeePage_SelectedPageChanged(object sender, EventArgs e)
        {
            if (AllEmployeePage.SelectedPage == pageSponsor)
            {
                
            }
        }


        // end of of employee Passport

        private void disableAllPrinting()
        {
            btnResidencyApplicationForm.Enabled = false;
            btnResidencyApplicationFormBack.Enabled = false;

            btnResidencyApplicationFormN.Enabled = false;
            btnResidencyApplicationFormBackN.Enabled = false;
            btnPhoto.Enabled = false;

            btnTransferWorkingPermit.Enabled = false;
            btnTransferWorkingPermit1.Enabled = false;
            btnInternalSecurity.Enabled = false;
            btnTransferWorkingPermitBack.Enabled = false;
            btnContractReport.Enabled = false;
            btnContractReportFront2.Enabled = false;
            btnContractReportBack2.Enabled = false;
        }

        private void enablePrinting(string _code)
        {
            if (_code == "01" || _code == "02")
            {
                //btnResidencyApplicationForm.Enabled = true;
                //btnResidencyApplicationFormBack.Enabled = true;
                //btnPhoto.Enabled = true;
         
                btnResidencyApplicationForm.Enabled = true;
                btnResidencyApplicationFormBack.Enabled = true;

                btnResidencyApplicationFormN.Enabled = true;
                btnResidencyApplicationFormBackN.Enabled = true;

                btnTransferWorkingPermit.Enabled = true;
                btnTransferWorkingPermit1.Enabled = true;
                btnInternalSecurity.Enabled = true;
                btnTransferWorkingPermitBack.Enabled = true;
                btnContractReport.Enabled = true;
                btnContractReportFront2.Enabled = true;
                btnContractReportBack2.Enabled = true;
                btnPhoto.Enabled = true;
            }
        }

        private void getPrintingData(int _employeeID)
        {
            disableAllPrinting();
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c3 in db.DocumentTypeDefinitions
                           join c4 in db.DocumentDefs
                           on c3.documentTypeDefID equals c4.documentTypeDefID
                           join c1 in db.Documents
                           on c4.documentDefID equals c1.documentDefID
                           join cc in db.DocumentTransactionTasks on c1.documentID equals cc.documentID
                           join cc1 in db.DocumentTransactions on cc.documentTransID equals cc1.documentTransID
                           join c2 in db.Employees
                           on c1.document_TypeID equals c2.employeeID
                           where c3.documentType_Code == "01" &&
                           cc.documentTransTask_Approved == true &&
                           cc.documentTransTask_isClose == false
                           where c2.employeeID == _employeeID
                           select new
                           {
                               cc.documentTransTaskID

                           }).ToList();

                foreach (var item in val)
                {
                    int IDD = item.documentTransTaskID;

                    var val2 = (from c in db.documentTransTaskDetails
                                where c.documentTransTaskID == IDD && 
                                c.documentTransSetp_isClose == false
                                select c).ToList();

                    if (val2.Count > 0)
                    {
                        enablePrinting(val2[0].documentTransSetp_pCode);
                    }
                }
                

            }


        }

        private void gvDocument_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == System.Windows.Forms.MouseButtons.Right && action)
                {
                    //KeyPressEventArgs ee = new KeyPressEventArgs(
                    //GView_KeyDown(sender, KeyPressEventArgs.Empty);

                    if (gvDocument.CurrentRow.Cells["gvRemarks"].Value != null &&
                        gvDocument.CurrentRow.Cells["gvRemarks"].Value.ToString().Length > 1 && isPressed == false)
                    {
                        MessageBox.Show("الرخصة فيها مشكلة أو عليها اجار محل");
                    }
                    //else if (GView.CurrentView.CurrentRow.BackColor == Color.Cyan && isPressed == false)
                    //{
                    //    MessageBox.Show("الرخصة عليها ايجار ");
                    //}
                    else if (addExcemption || (action && isPressed == false))
                    {
                        int IDD = int.Parse(gvDocument.CurrentRow.Cells[0].Value.ToString());

                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                            var val = (from c in db.DocumentTransactions
                                       join c1 in db.DocumentDefs on c.documentDefID equals c1.documentDefID
                                       join c2 in db.TransactionDefs on c.transDefID equals c2.transDefID
                                       join c3 in db.Documents on c1.documentDefID equals c3.documentDefID
                                       where c3.documentID == IDD
                                       select new
                                       {
                                           c2.transDef_namea,
                                           c.documentTrans_Amount,
                                           c.documentTransID,
                                           c3.documentID
                                       }).ToList();

                            ContextMenu m = new ContextMenu();
                            foreach (var item in val)
                            {
                                MenuItem i1 = new MenuItem(item.transDef_namea);
                                i1.Name = item.documentTransID.ToString() + "," + item.documentID;
                                i1.Click += new EventHandler(i1_Click);
                                m.MenuItems.Add(i1);
                            }

                            m.Show(gvDocument, new Point(e.X, e.Y));
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void i1_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem m = (MenuItem)sender;
                string name = m.Text;
                char[] delimiters = new char[] { ',' };
                int IDD = int.Parse(m.Name.Split(delimiters, 2)[0]);
                int DID = int.Parse(m.Name.Split(delimiters, 2)[1]);

                DialogResult dr = MessageBox.Show("هل تريد " + name + " ?", "Confirmation Of Action", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {

                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            var val1 = (from c in db.DocumentTransactionTasks
                                        where c.documentTransID == IDD &&
                                        c.documentID == DID
                                        select c).ToList();
                            if (val1.Count > 0)
                            {
                                MessageBox.Show("العملية موجودة");
                            }
                            else
                            {
                                DocumentTransactionTask dd = new DocumentTransactionTask();
                                dd.documentTransID = IDD;
                                dd.documentID = DID;
                                dd.documentTransTask_Approved = true;
                                dd.documentTransTask_isPaid = false;
                                dd.documentTransTask_ApprovedDate = DateTime.Now;
                                dd.documentTransTask_date = DateTime.Now;
                                dd.documentTransTask_isClose = false;
                                dd.documentTransTask_NoOfDays = 0;
                                dd.edited_Date = DateTime.Now;
                                dd.editedBy = MainClass.userID;

                                db.DocumentTransactionTasks.AddObject(dd);
                                int rowsAffected = db.SaveChanges();

                                if (rowsAffected > 0 && isPressed && this.gvDocument.CurrentRow.Cells["gvRemarks"].Value != null &&
                                    gvDocument.CurrentRow.Cells["gvRemarks"].Value.ToString().Length > 1)
                                {
                                    int _documentTransTaskID = dd.documentTransTaskID;
                                    DocumentTransactionTask val11 = (from c in db.DocumentTransactionTasks
                                                                     where c.documentTransTaskID == _documentTransTaskID
                                                                     select c).FirstOrDefault();
                                    val11.documentTransTask_Approved = true;
                                    val11.documentTransTask_ApprovedDate = DateTime.Now;
                                    db.SaveChanges();
                                    // we must ensure that the task has at least one task
                                    // asssign the tasks 
                                    // implement Transaction Task Details

                                    //var val2 = (from c in db.DocumentTransactionSteps
                                    //            where c.documentTransID == val11.documentTransID
                                    //            select c).ToList();
                                    //if (val2.Count > 0)
                                    //{
                                    //    // proceed
                                    //    foreach (var item in val2)
                                    //    {
                                    //        documentTransTaskDetail ass = new documentTransTaskDetail();
                                    //        ass.employeeID = -1;
                                    //        ass.documentTransTaskID = _documentTransTaskID;
                                    //        ass.documentTransSetp_startDate = DateTime.Now;
                                    //        ass.documentTransSetp_name = item.documentTransSetp_name;
                                    //        ass.documentTransSetp_Period = item.documentTransSetp_Period;
                                    //        ass.documentTransSetp_pCode = item.documentTransSetp_pCode;
                                    //        ass.documentTransSetp_Remarks = "";
                                    //        ass.documentTransSetp_isClose = false;
                                    //        ass.added_Date = DateTime.Now;
                                    //        ass.addedBy = MainClass.userID;
                                    //        ass.edited_Date = DateTime.Now;
                                    //        ass.editedBy = MainClass.userID;

                                    //        db.documentTransTaskDetails.AddObject(ass);
                                    //        db.SaveChanges();
                                    //    }


                                    //    DocumentTransactionTask val3 = (from c in db.DocumentTransactionTasks
                                    //                                    where c.documentTransTaskID == _documentTransTaskID
                                    //                                    select c).FirstOrDefault();
                                    //    val3.documentTransTask_isPaid = false;
                                    //    val3.documentTransTask_Approved = true;
                                    //    val3.documentTransTask_ApprovedDate = DateTime.Now;
                                    //    val3.edited_Date = DateTime.Now;
                                    //    val3.editedBy = MainClass.userID;

                                    //    db.SaveChanges();


                                    //    MessageBox.Show("تم تحديد المراحل");

                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("يجب ان يكون هناك مرحلة واحدة على الأقل");
                                    //    ts.Dispose();
                                    //}

                                }
                                else
                                {

                                }

                                db.AcceptAllChanges();
                                ts.Complete();
                                MessageBox.Show("تم ترحيل العملية بنجاح");
                            }
                            // we must check if the order already there for that documents
                            // we must add record to document transaction tasks
                            //MessageBox.Show(name);

                        }

                    }

                    this.gvDocument.Rows.Clear();
                    getDocData();
                    bindDocData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void gvDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
            {
                isPressed = true;
            }
        }

        private void gvDocument_KeyUp(object sender, KeyEventArgs e)
        {
            isPressed = false;
        }

        private void btnTransferWorkingPermit1_Click(object sender, EventArgs e)
        {
            try
            {

                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "migration1";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbemployeeSponsor_salary_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                DateTime d;
                if(DateTime.TryParse(txtemployeeSponsor_ResidencyIssueDate.Text, out d))
                {
                    if (cmbemployeeSponsor_useDuration.SelectedIndex != -1)
                        txtemployeeSponsor_ResidencyExpiryDate.Text = d.AddYears(cmbemployeeSponsor_useDuration.SelectedIndex).AddDays(-1).ToShortDateString();
                    else
                        txtemployeeSponsor_ResidencyExpiryDate.Text = d.ToShortDateString();
                }

                txtemployeeSponsor_useDuration.Text = cmbemployeeSponsor_useDuration.SelectedText;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                //ds.WriteXmlSchema(Application.StartupPath + "/../../xml/PrintEmployee.xsd");
                //MessageBox.Show("Test it good !");
                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "passportN";
                rv.ds = ds;
                rv.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "bpassportN";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnContractReportFront2_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

               //  ds.WriteXmlSchema("employeeData.xsd");
                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "workContractFront2";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnContractReportBack2_Click(object sender, EventArgs e)
        {
            try
            {
                makePrintData();
                printTable.Clear();

                loadPrintData(int.Parse(lblEmployeeID.Text));

                DataSet ds = new DataSet();
                ds.Tables.Add(printTable);

                // ds.WriteXmlSchema("employeeData.xsd");
                frmHumanResouceReportViewer rv = new frmHumanResouceReportViewer();
                rv.printReport = "workContractBack2";
                rv.ds = ds;
                rv.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            CarsM.frmfindCars frm = new CarsM.frmfindCars(false);
            frm.isAllCar = true;
            frm.ShowDialog();
            if (frm.found)
            {
                txtcar_No.Text = frm.carNO;
                txtcar_Name.Text = frm.carName;
                lblCarID.Text = frm.carID.ToString(); ;
            }
        }



    }
}
