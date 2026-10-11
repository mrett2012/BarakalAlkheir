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
        decimal[] scheduledTotals = new decimal[12];
        decimal[] paidTotals = new decimal[12];
        bool isloaded = false;

        public frmRentingPaymentDetails()
        {
            InitializeComponent();
        }

        private void frmRentingPaymentDetails_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmRentingPaymentDetails_LoadData);
        }

        private void frmRentingPaymentDetails_LoadData(object sender, EventArgs e)
        {
            txtDate.Value = DateTime.Today;
            isloaded = true;
            btnPrint_Click(sender, e);
        }

       


        private void getData()
        {
            GView.Rows.Clear();
            scheduledTotals = new decimal[12];
            paidTotals = new decimal[12];
            int year = txtDate.Value.Year;
            DateTime first = LicenseRentalRules.PeriodStart(new DateTime(year, 1, 1));
            DateTime end = LicenseRentalRules.PeriodEndExclusive(new DateTime(year, 12, 1));
            using (var db = new BarakeitAlkheirEntities())
            {
                var details = db.RentLicensesDetails.Where(d => d.rd_fromDate >= first && d.rd_fromDate < end).ToList();
                var receipts = details.Where(d => d.rd_isPaid == true).ToList();
                var ids = details.Where(d => d.rentLicenseID.HasValue).Select(d => d.rentLicenseID.Value).Distinct().ToList();
                // Historical rentals remain visible in their billing year after closure.
                var rentals = (from r in db.RentLicenses
                               join c in db.CompanyLicences on r.companyLicencesID equals c.companyLicencesID
                               where ids.Contains(r.rentLicenseID)
                               orderby r.rentLicenseID
                               select new { r.rentLicenseID, c.licenseNo, c.commercialName, r.rentLicense_from, r.rentLicense_to }).ToList();
                foreach (var rental in rentals)
                {
                    var scheduled = new decimal[12];
                    var paid = new decimal[12];
                    foreach (var detail in details.Where(d => d.rentLicenseID == rental.rentLicenseID))
                        scheduled[LicenseRentalRules.BillingDate(detail.rd_fromDate.Value).Month - 1] += LicenseRentalRules.Amount(detail.rd_amount ?? 0);
                    foreach (var receipt in receipts.Where(p => p.rentLicenseID == rental.rentLicenseID))
                        paid[LicenseRentalRules.BillingDate(receipt.rd_fromDate.Value).Month - 1] += LicenseRentalRules.Amount(receipt.rd_amount ?? 0);
                    int row = GView.Rows.Add();
                    GView.Rows[row].Cells[0].Value = rental.rentLicenseID;
                    GView.Rows[row].Cells[1].Value = rental.licenseNo;
                    GView.Rows[row].Cells[2].Value = rental.commercialName;
                    GView.Rows[row].Cells[3].Value = rental.rentLicense_from.HasValue ? LicenseRentalRules.FormatDate(rental.rentLicense_from.Value) : "";
                    GView.Rows[row].Cells[4].Value = rental.rentLicense_to.HasValue ? LicenseRentalRules.FormatDate(rental.rentLicense_to.Value) : "";
                    for (int month = 0; month < 12; month++)
                    {
                        scheduledTotals[month] += scheduled[month];
                        paidTotals[month] += paid[month];
                        if (scheduled[month] == 0 && paid[month] == 0) continue;
                        var cell = GView.Rows[row].Cells[month + 5];
                        cell.Value = scheduled[month];
                        cell.Style.BackColor = paid[month] == scheduled[month] ? Color.Aqua : (paid[month] == 0 ? Color.Red : Color.Orange);
                        cell.ToolTipText = "المدفوع: " + paid[month] + "، المتبقي: " + (scheduled[month] - paid[month]);
                    }
                    GView.Rows[row].Cells[17].Value = scheduled.Sum();
                    GView.Rows[row].Cells[18].Value = paid.Sum();
                    GView.Rows[row].Cells[19].Value = scheduled.Sum() - paid.Sum();
                }
            }
        }


        private void getSummaryData()
        {
            if (GView.Rows.Count == 0) return;
            int row = GView.Rows.Count;
            GView.Rows.Add(3);
            GView.Rows[row].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
            GView.Rows[row + 1].DefaultCellStyle.BackColor = Color.DodgerBlue;
            GView.Rows[row + 2].DefaultCellStyle.BackColor = Color.Aqua;
            GView.Rows[row].Cells[2].Value = "المجموع الكلي";
            GView.Rows[row + 1].Cells[2].Value = "المدفوع";
            GView.Rows[row + 2].Cells[2].Value = "المتبقي";
            for (int month = 0; month < 12; month++)
            {
                GView.Rows[row].Cells[month + 5].Value = scheduledTotals[month];
                GView.Rows[row + 1].Cells[month + 5].Value = paidTotals[month];
                GView.Rows[row + 2].Cells[month + 5].Value = scheduledTotals[month] - paidTotals[month];
            }
            GView.Rows[row + 2].Cells[17].Value = scheduledTotals.Sum();
            GView.Rows[row + 2].Cells[18].Value = paidTotals.Sum();
            GView.Rows[row + 2].Cells[19].Value = scheduledTotals.Sum() - paidTotals.Sum();
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!isloaded) return;
            try { getData(); getSummaryData(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
