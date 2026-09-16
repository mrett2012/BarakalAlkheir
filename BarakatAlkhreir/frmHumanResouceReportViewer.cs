using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace BarakalAlkheir
{
    public partial class frmHumanResouceReportViewer : Form
    {
        public DataSet ds = null;
        public string printReport = "";
        public string rtype2 = "";

        public string from = "";
        public string to = "";

        public string header = "";
        // tagayoub Parameters
        public string tagayobDate = "";
        public string Ttelephone = "";
        public string TconfessName = "";
        public string Trelation = "";
        public string TcivilNo = "";

        // ekhtar Sakan Parameter
        public string Sname1 = "";
        public string Scivil1 = "";
        public string Sname2 = "";
        public string Scivil2 = "";
        public string Sname3 = "";
        public string Scivil3 = "";
        public string Sname4 = "";
        public string Scivil4 = "";
        public string Sname5 = "";
        public string Scivil5 = "";
        public string Sname6 = "";
        public string Scivil6 = "";
        public string Sname7 = "";
        public string Scivil7 = "";
        public string Sname8 = "";
        public string Scivil8 = "";
        public string Sname9 = "";
        public string Scivil9 = "";
        public string Sname10 = "";
        public string Scivil10 = "";

        // cure Application Parameter
        public string Cclinic = "";
        public string Creason = "";
        public string Cdate = "";

        // mandoub Application parameters
        public string MAtype = "";

        public string MAfile1 = "";
        public string MAname1 = "";

        public string MAfile2 = "";
        public string MAname2 = "";


        public string MAfile3 = "";
        public string MAname3 = "";

        public string MAfile4 = "";
        public string MAname4 = "";

        public string MAfile5 = "";
        public string MAname5 = "";

        public string MAfile6 = "";
        public string MAname6 = "";

        public string MAfile7 = "";
        public string MAname7 = "";

        public string MAfile8 = "";
        public string MAname8 = "";


        // mandoub Back Parameters
        public string Mcompany1 = "";
        public string Mcompany2 = "";
        public string Mcompany3 = "";
        public string Mcompany4 = "";
        public string Mcompany5 = "";
        public string Mcompany6 = "";

        public string Maddress1 = "";
        public string Maddress2 = "";
        public string Maddress3 = "";
        public string Maddress4 = "";
        public string Maddress5 = "";
        public string Maddress6 = "";

        public string Munique1 = "";
        public string Munique2 = "";
        public string Munique3 = "";
        public string Munique4 = "";
        public string Munique5 = "";
        public string Munique6 = "";

        public string Mtel1 = "";
        public string Mtel2 = "";
        public string Mtel3 = "";
        public string Mtel4 = "";
        public string Mtel5 = "";
        public string Mtel6 = "";



        /// <summary>
        /// stop work paramater
        /// </summary>

        public string dateOfStopWork = "";
        public string employeeStopWork = "";
        public string employeeWork = "";

        public string reportFullAdress = "";

        public string title = "";

        public frmHumanResouceReportViewer()
        {
            InitializeComponent();
        }

        private void frmHumanResouceReportViewer_Load(object sender, EventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            try
            {
                if (printReport == "passport")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/HRPassports.rpt");
                    rd.SetDataSource(ds);

                }
                else if (printReport == "passportN")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/JawasatFront.rpt");
                    rd.SetDataSource(ds);
                }

                else if (printReport == "migration")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/HRMigration1.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "migration1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/HRMigration11.rpt");
                    rd.SetDataSource(ds);
                }

                else if (printReport == "Payment")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/PaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Payment1")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/PaymentReport1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "OtherPayment")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/OtherPaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "NotPayment")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/NotPaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "DeservedPayment")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/DeservedPaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "employeeReport")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/EmployeeReport.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "migration2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/HRMigration2.rpt");
                    rd.SetDataSource(ds);
                    
                }
                else if (printReport == "bpassport")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/backPassport.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "bpassportN")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/JawazatBack.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "bmigration")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/backMigration1.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "visitVisa")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/VisitVisa.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "workContract")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/WorkContract.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "workContractFront2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/WorkContract4Front.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "workContractBack2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/WorkContract4Back.rpt");
                   // rd.SetDataSource(ds);
                }
                else if (printReport == "workContract1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/EmploymentContract.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "workContract2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/EmploymentContractFull.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "getIndemity")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/admitOfRecievingIndemity.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "finalRep")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/finalLeave.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "stopWork")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/warninOfStopWorking.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("dateOfStopWork",dateOfStopWork );
                    rd.SetParameterValue("employeeStopWork",employeeStopWork );
                    rd.SetParameterValue("employeeWork", employeeWork);
                    rd.SetParameterValue("reportFullAdress", reportFullAdress);

                }
                else if (printReport == "issueHealth")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/issueHealthCertificate.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "renewHealth")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/renewHealthCertificate.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "employeebysponsor")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");
                   
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ ⁄·Ï ﬂ›«·… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsor00")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ ·Ì” ⁄·Ï ﬂ›«·… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsorE")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsorE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee Sponsored By : ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsor1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Ì⁄„·Ê‰ ·œÏ  ");
                    rd.SetParameterValue("rtype2", rtype2);
                    
                }
                else if (printReport == "employeebysponsor111")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor111.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Ì⁄„·Ê‰ ·œÏ  ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsor111N")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor111N.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Ì⁄„·Ê‰ ·œÏ  ");
                    rd.SetParameterValue("rtype2", rtype2);
                }

                else if (printReport == "employeebysponsorE1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsorE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee Work For : ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsor2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰  —ﬂÊ «·Œœ„…  ");
                    rd.SetParameterValue("rtype2", rtype2);
                }

                else if (printReport == "employeebysponsorleft")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesLeftTheCompany.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰  —ﬂÊ «·Œœ„…  ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsorleft1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesLeftTheCompany1.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰  —ﬂÊ «·Œœ„…  ");
                    rd.SetParameterValue("rtype2", rtype2);
                    rd.SetParameterValue("from", from);
                    rd.SetParameterValue("to", to);
                }
                else if (printReport == "employeebysponsorE2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsorE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee Left the company :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeebysponsorLeft2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesLeftThecompanyE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee Left the company :");
                    rd.SetParameterValue("rtype2", rtype2);
                }

                else if (printReport == "employeenotsponsor")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ ·Ì”Ê ⁄·Ï ﬂ›«·… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorE")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsorE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee not Sponsored by : ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsor1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·—Œ’ «· Ã«—Ì… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorE1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsorE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee By Commercial lisence No :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsor2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsor.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·⁄ﬁÊœ «·ÕﬂÊ„Ì… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorE2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesBySponsorE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee By Government Contract No: ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorN")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·Ã‰”Ì… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorN1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·ÊŸÌ›… ");
                    rd.SetParameterValue("rtype2", rtype2);
                }                else if (printReport == "employeenotsponsorN3")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·ﬁ”„ ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorN2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·„” ÊÏ «· ⁄·Ì„Ì ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorNE")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChangeE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee by nationality :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorNE3")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChangeE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee by Division :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorNE1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChangeE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee by Occupation :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorNE2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChangeE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee by Education :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorNE")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChangeE.rpt");

                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "Employee By Nationality :");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorD")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·ﬁ”„ ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorDD")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·„”„Ï «·ÊŸÌ›Ì ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "employeenotsponsorE")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeesByChange.rpt");
                    
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("rtype", "„ÊŸ›Ê‰ Õ”» «·„” ÊÏ «· ⁄·Ì„Ì ");
                    rd.SetParameterValue("rtype2", rtype2);
                }
                else if (printReport == "passportRenew")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/PassportRenew.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("header", header);
                }
                else if (printReport == "residencyRenew")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/ResidencyRenew.rpt");             
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("header", header);
                }
                else if (printReport == "passportInOut")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/passportInOut.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "comLicense1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/CommercialLicense1.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "comLicense11")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/CommercialLicense11.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "comLicense2")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/CommercialLicense2.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "comLicense3")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/CommercialLicense3.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allCar.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allInsusCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allInsusCar.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allOverAllCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allOverAllCar.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allMediaCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allMediaCar.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allHealthCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allHealthCar.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allInsusCarEx")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allInsusCarEx.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allOverAllCarEx")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allOverAllCarEx.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allMediaCarEx")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allMediaCarEx.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allHealthCarEx")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allHealthCarEx.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "allInsusCarEx1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allInsusCarEx1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("from", from);
                    rd.SetParameterValue("to", to);
                }
                else if (printReport == "allOverAllCarEx1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allOverAllCarEx1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("from", from);
                    rd.SetParameterValue("to", to);
                }
                else if (printReport == "allMediaCarEx1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allMediaCarEx1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("from", from);
                    rd.SetParameterValue("to", to);
                }
                else if (printReport == "allHealthCarEx1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/allHealthCarEx1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("from", from);
                    rd.SetParameterValue("to", to);
                }
                else if (printReport == "photo")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/photo.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "employeeFinance")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/employeeByFinanciall1.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "ekhtarTagayob")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/ekhtarTagayob.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("tagayobDate", tagayobDate);
                }
                else if (printReport == "EkrarSakanFront")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/ekrarSakan.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "EkrarSakanBack")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/ekrarSakanBack.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("Sname1",Sname1 );
                    rd.SetParameterValue("Scivil1",Scivil1 );

                    rd.SetParameterValue("Sname2", Sname2);
                    rd.SetParameterValue("Scivil2", Scivil2);

                    rd.SetParameterValue("Sname3", Sname3);
                    rd.SetParameterValue("Scivil3", Scivil3);

                    rd.SetParameterValue("Sname4", Sname4);
                    rd.SetParameterValue("Scivil4", Scivil4);

                    rd.SetParameterValue("Sname5", Sname5);
                    rd.SetParameterValue("Scivil5", Scivil5);

                    rd.SetParameterValue("Sname6", Sname6);
                    rd.SetParameterValue("Scivil6", Scivil6);

                    rd.SetParameterValue("Sname7", Sname7);
                    rd.SetParameterValue("Scivil7", Scivil7);

                    rd.SetParameterValue("Sname8", Sname8);
                    rd.SetParameterValue("Scivil8", Scivil8);

                    rd.SetParameterValue("Sname9", Sname9);
                    rd.SetParameterValue("Scivil9", Scivil9);

                    rd.SetParameterValue("Sname10", Sname10);
                    rd.SetParameterValue("Scivil10", Scivil10);
  
                }
                else if (printReport == "cureApp")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/Talab3elaj.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("Cclinic", Cclinic);
                    rd.SetParameterValue("Creason", Creason);
                    rd.SetParameterValue("Cdate", Cdate);
                }
                else if (printReport == "mandoubApp")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/ManDoubApp.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("MAtype", MAtype);
                    rd.SetParameterValue("MAfile1", MAfile1);
                    rd.SetParameterValue("MAname1", MAname1);

                    rd.SetParameterValue("MAfile2", MAfile2);
                    rd.SetParameterValue("MAname2", MAname2);

                    rd.SetParameterValue("MAfile3", MAfile3);
                    rd.SetParameterValue("MAname3", MAname3);

                    rd.SetParameterValue("MAfile4", MAfile4);
                    rd.SetParameterValue("MAname4", MAname4);

                    rd.SetParameterValue("MAfile5", MAfile5);
                    rd.SetParameterValue("MAname5", MAname5);

                    rd.SetParameterValue("MAfile6", MAfile6);
                    rd.SetParameterValue("MAname6", MAname6);

                    rd.SetParameterValue("MAfile7", MAfile7);
                    rd.SetParameterValue("MAname7", MAname7);

                    rd.SetParameterValue("MAfile8", MAfile8);
                    rd.SetParameterValue("MAname8", MAname8);

                }
                else if (printReport == "mandoubBackFront")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/HaweyiaMandoub.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "notHealth")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/notHealth.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "passportin")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/PassportIn.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "passportout")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/PassportOut.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "resignApp")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/resignApp.rpt");
                    rd.SetDataSource(ds);
                }

                else if (printReport == "mandoubBackBack")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/HaweyiaMandoubBack.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("Mcompany1", Mcompany1);
                    rd.SetParameterValue("Maddress1", Maddress1);
                    rd.SetParameterValue("Munique1", Munique1);
                    rd.SetParameterValue("Mtel1", Mtel1);

                    rd.SetParameterValue("Mcompany2", Mcompany2);
                    rd.SetParameterValue("Maddress2", Maddress2);
                    rd.SetParameterValue("Munique2", Munique2);
                    rd.SetParameterValue("Mtel2", Mtel2);

                    rd.SetParameterValue("Mcompany3", Mcompany3);
                    rd.SetParameterValue("Maddress3", Maddress3);
                    rd.SetParameterValue("Munique3", Munique3);
                    rd.SetParameterValue("Mtel3", Mtel3);

                    rd.SetParameterValue("Mcompany4", Mcompany4);
                    rd.SetParameterValue("Maddress4", Maddress4);
                    rd.SetParameterValue("Munique4", Munique4);
                    rd.SetParameterValue("Mtel4", Mtel4);

                    rd.SetParameterValue("Mcompany5", Mcompany5);
                    rd.SetParameterValue("Maddress5", Maddress5);
                    rd.SetParameterValue("Munique5", Munique5);
                    rd.SetParameterValue("Mtel5", Mtel5);

                    rd.SetParameterValue("Mcompany6", Mcompany6);
                    rd.SetParameterValue("Maddress6", Maddress6);
                    rd.SetParameterValue("Munique6", Munique6);
                    rd.SetParameterValue("Mtel6", Mtel6);




                }
                else if (printReport == "ta3ahod")
                {
                    rd.Load(Application.StartupPath + "/../../OtherReport/Maba7esHoroub.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("Ttelephone",Ttelephone);
                    rd.SetParameterValue("TconfessName",TconfessName);
                    rd.SetParameterValue("Trelation",Trelation);
                    rd.SetParameterValue("TcivilNo",TcivilNo);
                }
                else if (printReport == "healthDate")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/healthExpiry.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "healthDate11")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/healthExpiry11.rpt");
                    rd.SetDataSource(ds);
                }
                else if (printReport == "healthDate1")
                {
                    rd.Load(Application.StartupPath + "/../../hrReports/healthExpiry1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("from", from);
                    rd.SetParameterValue("to", to);
                }
                else if (printReport == "rentLicence")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/rentLicence.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Licence")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/Licence.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "LicenceFilter")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/LicenceFilter.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "LicenceSeat")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/LicenceSeat.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "LicenceSeatLeft")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/LicenceSeatLeft.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "rentCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/rentCar.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "buyCar")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/buyCar.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Car")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/Car.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Paymentc")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/PaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Paymentcc1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/PaymentReportcc1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Paymentc1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/PaymentReport1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "Paymentc11")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/PaymentReport11.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "NotPaymentc")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/NotPaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "NotPaymentc1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/NotPaymentReport1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "DeservedPaymentc")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/DeservedPaymentReport.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "DeservedPaymentc1")
                {
                    rd.Load(Application.StartupPath + "/../../CarReports/DeservedPaymentReport1.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "employeeLicences")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/employeeLicences.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "employeeTransaction")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/employeeTransaction.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "employeeExpiryDocument")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/employeeExpiryDocument.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "employeeLicencesRemarks")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/employeeLicencesRemarks.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                else if (printReport == "CustomersL")
                {
                    rd.Load(Application.StartupPath + "/../../otherReports/CustomersL.rpt");
                    rd.SetDataSource(ds);
                    rd.SetParameterValue("title", title);
                }
                crystalReportViewer1.ReportSource = rd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}