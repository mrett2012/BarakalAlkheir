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
using Microsoft.Office.Interop.Excel;


namespace BarakalAlkheir.Lic
{
    public partial class frmRentingPaymentDetails : Telerik.WinControls.UI.RadForm
    {
        DateTime firstDay = DateTime.Parse("01/01/" + DateTime.Now.Year);
        DateTime lastDay = DateTime.Parse("31/12/" + DateTime.Now.Year);
        bool isloaded = false;

        public frmRentingPaymentDetails()
        {
            InitializeComponent();
        }

        private void frmRentingPaymentDetails_Load(object sender, EventArgs e)
        {
            try
            {
                txtDate.Text = DateTime.Now.ToShortDateString();
                firstDay = DateTime.Parse("01/01/" + txtDate.Text);
                lastDay = DateTime.Parse("31/12/" + txtDate.Text);
                getData();
                getSummaryData();
                isloaded = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       


        private bool isPaidAmount(int _rentLicenseID, DateTime _dfrom)
        {
            bool temp = false;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                DateTime _dTo = _dfrom.AddMonths(1);
                var val2 = (from c in db.RentLicensesPayments
                           where c.rentLicenseID == _rentLicenseID &&
                           c.rentLicPay_ForDate.Value >= _dfrom &&
                           c.rentLicPay_ForDate.Value < _dTo
                           select c).ToList();
                if (val2.Count > 0)
                    temp = true;
            }

            return temp;
        }

        private void getData()
        {
            GView.Rows.Clear();
            int yeeaar = int.Parse(txtDate.Text);
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.CompanyLicences
                          join d in db.RentLicenses on c.companyLicencesID equals d.companyLicencesID
                          where (d.rentLicense_active==true)      
                           select new
                           {
                               d.rentLicenseID,
                               c.licenseNo,
                               c.commercialName,
                               d.rentLicense_from,
                               d.rentLicense_to,
                               d.rentLicense_RentalAmount,
                               d.rentLicense_DamanAmount
                           }).ToList();

                foreach (var item in val)
                {
                    GView.Rows.Add(1);
                    int row = GView.Rows.Count -1;


                    int LRID =  item.rentLicenseID;


                    GView.Rows[row].Cells[0].Value = item.rentLicenseID;
                    GView.Rows[row].Cells[1].Value = item.licenseNo;
                    GView.Rows[row].Cells[2].Value = item.commercialName;
                    GView.Rows[row].Cells[3].Value = item.rentLicense_from.Value.ToShortDateString();
                    GView.Rows[row].Cells[4].Value = item.rentLicense_to.Value.ToShortDateString();
                    
                    
                    int rIDD = item.rentLicenseID;
                    var val1 = (from c in db.RentLicensesDetails
                                where c.rentLicenseID == rIDD
                                select c).ToList();


                    //DateTime dfrom = item.rentLicense_from.Value;
                    //DateTime dto = item.rentLicense_to.Value;
                    float rentingAmount = 0;
                    float rentingPaidAmount = 0;
                    float rentingRemainingAmount = 0;

                    foreach (var vv in val1)
                    {


                        if (vv.rd_fromDate.Value.Month == 1 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 12 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar - 1)
                        {
                            GView.Rows[row].Cells[5].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[5].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[5].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
    
                        }
                        else if (vv.rd_fromDate.Value.Month == 2 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 1 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[6].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[6].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[6].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 3 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 2 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[7].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[7].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[7].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 4 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 3 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[8].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[8].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[8].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 5 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 4 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[9].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[9].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[9].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 6 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 5 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[10].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[10].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[10].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 7 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 6 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[11].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[11].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[11].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 8 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 7 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[12].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[12].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[12].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 9 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 8 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[13].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[13].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[13].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 10 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 9 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[14].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[14].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[14].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 11 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 10 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[15].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[15].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[15].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }
                        else if (vv.rd_fromDate.Value.Month == 12 && vv.rd_fromDate.Value.Day <= MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar ||
                            vv.rd_fromDate.Value.Month == 11 && vv.rd_fromDate.Value.Day > MainClass.StartRentDay && vv.rd_fromDate.Value.Year == yeeaar)
                        {
                            GView.Rows[row].Cells[16].Style.BackColor = Color.Red;
                            GView.Rows[row].Cells[16].Value = vv.rd_amount.Value;
                            rentingAmount += vv.rd_amount.Value;
                            if (vv.rd_isPaid.Value)
                            {
                                GView.Rows[row].Cells[16].Style.BackColor = Color.Aqua;
                                rentingPaidAmount += vv.rd_amount.Value;
                            }
                        }


                        GView.Rows[row].Cells[17].Value = rentingAmount.ToString();
                        GView.Rows[row].Cells[18].Value = rentingPaidAmount.ToString();
                        GView.Rows[row].Cells[19].Value = (rentingAmount - rentingPaidAmount).ToString();
                    }
                    



                }

            }
        }


        private void getSummaryData()
        {
            if (GView.Rows.Count > 0)
            {
                int nOfRows = GView.Rows.Count;

                double oneA = 0;
                double twoA = 0;
                double threeA = 0;
                double fourA = 0;
                double fiveA = 0;
                double sixA = 0;
                double sevenA = 0;
                double eightA = 0;
                double nineA = 0;
                double tenA = 0;
                double elevenA = 0;
                double twelveA = 0;

                double oneP = 0;
                double twoP = 0;
                double threeP = 0;
                double fourP = 0;
                double fiveP = 0;
                double sixP = 0;
                double sevenP = 0;
                double eightP = 0;
                double nineP = 0;
                double tenP = 0;
                double elevenP = 0;
                double twelveP = 0;


                double ee1 = 0;
                double ee2 = 0;
                double ee3 = 0;

                for (int i = 0; i < GView.Rows.Count; i++)
                {
                    if (GView.Rows[i].Cells[5].Value != null)
                    {
                        oneA += double.Parse(GView.Rows[i].Cells[5].Value.ToString());
                        if (GView.Rows[i].Cells[5].Style.BackColor == Color.Aqua)
                        {
                            oneP += double.Parse(GView.Rows[i].Cells[5].Value.ToString());
                        }
                    }

                    if (GView.Rows[i].Cells[6].Value != null)
                    {
                        twoA += double.Parse(GView.Rows[i].Cells[6].Value.ToString());
                        if (GView.Rows[i].Cells[6].Style.BackColor == Color.Aqua)
                        {
                            twoP += double.Parse(GView.Rows[i].Cells[6].Value.ToString());
                        }
                    }

                    if (GView.Rows[i].Cells[7].Value != null)
                    {
                        threeA += double.Parse(GView.Rows[i].Cells[7].Value.ToString());
                        if (GView.Rows[i].Cells[7].Style.BackColor == Color.Aqua)
                        {
                            threeP += double.Parse(GView.Rows[i].Cells[7].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[8].Value != null)
                    {
                        fourA += double.Parse(GView.Rows[i].Cells[8].Value.ToString());
                        if (GView.Rows[i].Cells[8].Style.BackColor == Color.Aqua)
                        {
                            fourP += double.Parse(GView.Rows[i].Cells[8].Value.ToString());
                        }
                    } 
                    if (GView.Rows[i].Cells[9].Value != null)
                    {
                        fiveA += double.Parse(GView.Rows[i].Cells[9].Value.ToString());
                        if (GView.Rows[i].Cells[9].Style.BackColor == Color.Aqua)
                        {
                            fiveP += double.Parse(GView.Rows[i].Cells[9].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[10].Value != null)
                    {
                        sixA += double.Parse(GView.Rows[i].Cells[10].Value.ToString());
                        if (GView.Rows[i].Cells[10].Style.BackColor == Color.Aqua)
                        {
                            sixP += double.Parse(GView.Rows[i].Cells[10].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[11].Value != null)
                    {
                        sevenA += double.Parse(GView.Rows[i].Cells[11].Value.ToString());
                        if (GView.Rows[i].Cells[11].Style.BackColor == Color.Aqua)
                        {
                            sevenP += double.Parse(GView.Rows[i].Cells[11].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[12].Value != null)
                    {
                        eightA += double.Parse(GView.Rows[i].Cells[12].Value.ToString());
                        if (GView.Rows[i].Cells[12].Style.BackColor == Color.Aqua)
                        {
                            eightP += double.Parse(GView.Rows[i].Cells[12].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[13].Value != null)
                    {
                        nineA += double.Parse(GView.Rows[i].Cells[13].Value.ToString());
                        if (GView.Rows[i].Cells[13].Style.BackColor == Color.Aqua)
                        {
                            nineP += double.Parse(GView.Rows[i].Cells[13].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[14].Value != null)
                    {
                        tenA += double.Parse(GView.Rows[i].Cells[14].Value.ToString());
                        if (GView.Rows[i].Cells[14].Style.BackColor == Color.Aqua)
                        {
                            tenP += double.Parse(GView.Rows[i].Cells[14].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[15].Value != null)
                    {
                        elevenA += double.Parse(GView.Rows[i].Cells[15].Value.ToString());
                        if (GView.Rows[i].Cells[15].Style.BackColor == Color.Aqua)
                        {
                            elevenP += double.Parse(GView.Rows[i].Cells[15].Value.ToString());
                        }
                    }
                    if (GView.Rows[i].Cells[16].Value != null)
                    {
                        twelveA += double.Parse(GView.Rows[i].Cells[16].Value.ToString());
                        if (GView.Rows[i].Cells[16].Style.BackColor == Color.Aqua)
                        {
                            twelveP += double.Parse(GView.Rows[i].Cells[16].Value.ToString());
                        }
                    }

                    if (GView.Rows[i].Cells[17].Value != null)
                    {
                        ee1 += double.Parse(GView.Rows[i].Cells[17].Value.ToString());
                    }
                    if (GView.Rows[i].Cells[18].Value != null)
                    {
                        ee2 += double.Parse(GView.Rows[i].Cells[18].Value.ToString());
                    }
                    if (GView.Rows[i].Cells[19].Value != null)
                    {
                        ee3 += double.Parse(GView.Rows[i].Cells[19].Value.ToString());
                    }
                }

                GView.Rows.Add(3);
                GView.Rows[nOfRows].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                GView.Rows[nOfRows+1].DefaultCellStyle.BackColor = Color.DodgerBlue;
                GView.Rows[nOfRows+2].DefaultCellStyle.BackColor = Color.Aqua;

                GView.Rows[nOfRows].Cells[2].Value = "المجموع الكلي ";
                GView.Rows[nOfRows + 1].Cells[2].Value = "المدفوع ";
                GView.Rows[nOfRows + 2].Cells[2].Value = "المتبقي";



                GView.Rows[nOfRows].Cells[5].Value = oneA.ToString();
                GView.Rows[nOfRows + 1].Cells[5].Value = oneP.ToString();
                GView.Rows[nOfRows + 2].Cells[5].Value = (oneA - oneP).ToString();

                GView.Rows[nOfRows].Cells[6].Value = twoA.ToString();
                GView.Rows[nOfRows + 1].Cells[6].Value = twoP.ToString();
                GView.Rows[nOfRows + 2].Cells[6].Value = (twoA - twoP).ToString();

                GView.Rows[nOfRows].Cells[7].Value = threeA.ToString();
                GView.Rows[nOfRows + 1].Cells[7].Value = threeP.ToString();
                GView.Rows[nOfRows + 2].Cells[7].Value = (threeA - threeP).ToString();

                GView.Rows[nOfRows].Cells[8].Value = fourA.ToString();
                GView.Rows[nOfRows + 1].Cells[8].Value = fourP.ToString();
                GView.Rows[nOfRows + 2].Cells[8].Value = (fourA - fourP).ToString();

                GView.Rows[nOfRows].Cells[9].Value = fiveA.ToString();
                GView.Rows[nOfRows + 1].Cells[9].Value = fiveP.ToString();
                GView.Rows[nOfRows + 2].Cells[9].Value = (fiveA - fiveP).ToString();

                GView.Rows[nOfRows].Cells[10].Value = sixA.ToString();
                GView.Rows[nOfRows + 1].Cells[10].Value = sixP.ToString();
                GView.Rows[nOfRows + 2].Cells[10].Value = (sixA - sixP).ToString();

                GView.Rows[nOfRows].Cells[11].Value = sevenA.ToString();
                GView.Rows[nOfRows + 1].Cells[11].Value = sevenP.ToString();
                GView.Rows[nOfRows + 2].Cells[11].Value = (sevenA - sevenP).ToString();

                GView.Rows[nOfRows].Cells[12].Value = eightA.ToString();
                GView.Rows[nOfRows + 1].Cells[12].Value = eightP.ToString();
                GView.Rows[nOfRows + 2].Cells[12].Value = (eightA - eightP).ToString();

                GView.Rows[nOfRows].Cells[13].Value = nineA.ToString();
                GView.Rows[nOfRows + 1].Cells[13].Value = nineP.ToString();
                GView.Rows[nOfRows + 2].Cells[13].Value = (nineA - nineP).ToString();

                GView.Rows[nOfRows].Cells[14].Value = tenA.ToString();
                GView.Rows[nOfRows + 1].Cells[14].Value = tenP.ToString();
                GView.Rows[nOfRows + 2].Cells[14].Value = (tenA - tenP).ToString();


                GView.Rows[nOfRows].Cells[15].Value = elevenA.ToString();
                GView.Rows[nOfRows + 1].Cells[15].Value = elevenP.ToString();
                GView.Rows[nOfRows + 2].Cells[15].Value = (elevenA - elevenP).ToString();

                GView.Rows[nOfRows].Cells[16].Value = twelveA.ToString();
                GView.Rows[nOfRows + 1].Cells[16].Value = twelveP.ToString();
                GView.Rows[nOfRows + 2].Cells[16].Value = (twelveA - twelveP).ToString();

                GView.Rows[nOfRows + 2].Cells[17].Value = ee1.ToString();
                GView.Rows[nOfRows + 2].Cells[18].Value = ee2.ToString();
                GView.Rows[nOfRows + 2].Cells[19].Value = ee3.ToString();

            }
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (isloaded)
            {
                //txtDate.Text = DateTime.Now.ToShortDateString();
                firstDay = DateTime.Parse("01/01/" + txtDate.Text);
                lastDay = DateTime.Parse("31/12/" + txtDate.Text);
                //GView.Rows.Clear();
                getData();
                getSummaryData();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application wapp;
            Microsoft.Office.Interop.Excel.Worksheet wsheet;
            Microsoft.Office.Interop.Excel.Workbook wbook;
            wapp = new Microsoft.Office.Interop.Excel.Application();
            wapp.Visible = false;
            wbook = wapp.Workbooks.Add(true);
            wsheet = (Worksheet)wbook.ActiveSheet;
            try
            {
                int iX;
                int iY;

                for (int i = 0; i < this.GView.Columns.Count; i++)
                {
                    wsheet.Cells[1, i + 1] = this.GView.Columns[i].HeaderText;
                    wsheet.Columns.Font.Bold = true;


                }
                for (int i = 0; i < this.GView.Rows.Count; i++)
                {
                    DataGridViewRow row = this.GView.Rows[i];

                    for (int j = 0; j < row.Cells.Count; j++)
                    {

                        DataGridViewCell cell = row.Cells[j];
                        try
                        {

                            wsheet.Cells[i + 2, j + 1] = (cell.Value == null) ? "" : cell.Value.ToString();


                            if (cell.Style.BackColor == Color.Empty)
                            {
                                wsheet.Cells[i + 2, j + 1].Interior.Color = Color.White;
                            }
                            else
                            {
                                wsheet.Cells[i + 2, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(cell.Style.BackColor);
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    if (i == GView.Rows.Count - 1)
                    {
                        wsheet.Rows[i + 2].Interior.Color = Color.Yellow;
                    }
                    if (i == GView.Rows.Count - 2)
                    {
                        wsheet.Rows[i + 2].Interior.Color = Color.Violet;
                    }

                    if (i == GView.Rows.Count - 3)
                    {
                        wsheet.Rows[i + 2].Interior.Color = Color.Tomato;
                    }
                }
                wapp.Visible = true;
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }


    }
}
