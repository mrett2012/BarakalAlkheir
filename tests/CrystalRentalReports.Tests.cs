using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

internal static class CrystalRentalReportsTests
{
    private static int checkedReports;
    private static void Inspect(ReportDocument report, string file)
    {
        foreach (Table table in report.Database.Tables)
            if (table.Name.IndexOf("RentLicensesPayment", StringComparison.OrdinalIgnoreCase) >= 0 ||
                table.Location.IndexOf("RentLicensesPayment", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new InvalidOperationException("Report depends directly on the retired payment table: " + file);
        foreach (FormulaFieldDefinition formula in report.DataDefinition.FormulaFields)
            if (formula.Text.IndexOf("RentLicensesPayment", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new InvalidOperationException("Report formula depends on the retired payment table: " + file);
    }

    private static DataSet Dataset(Type formType, string method)
    {
        using (var form = (Form)Activator.CreateInstance(formType))
        {
            formType.GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(form, null);
            var table = ((DataTable)formType.GetField("dt", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form)).Copy();
            var row = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                string name = column.ColumnName;
                int number;
                row[column] = name == "Amount" || name == "ten" ? "100.00" :
                    name == "16" ? "01/02/2025" : int.TryParse(name, out number) ? "1" :
                    name.EndsWith("ID", StringComparison.OrdinalIgnoreCase) ? "1" :
                    name == "ForDate" ? "02/2025" :
                    name.IndexOf("Date", StringComparison.OrdinalIgnoreCase) >= 0 || name == "rentLicense_from" || name == "rentLicense_to" || name == "seven" || name == "eight" ? "01/02/2025" :
                    name == "extra2" ? "مدفوع" : "TEST";
            }
            table.Rows.Add(row);
            var dataset = new DataSet(); dataset.Tables.Add(table); return dataset;
        }
    }

    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            string root = Path.GetFullPath(args[0]);
            string output = Path.GetFullPath(args[1]);
            Directory.CreateDirectory(output);
            var app = Assembly.Load("BarakalAlkheir");
            foreach (string folder in args.Length > 2 ? new string[0] : new[] { "hrReports", "otherReports", "CarReports" })
            foreach (string path in Directory.GetFiles(Path.Combine(root, folder), "*.rpt"))
            {
                using (var report = new ReportDocument())
                {
                    report.Load(path, OpenReportMethod.OpenReportByTempCopy);
                    Inspect(report, folder + "/" + Path.GetFileName(path));
                    foreach (ReportDocument subreport in report.Subreports) Inspect(subreport, Path.GetFileName(path));
                    checkedReports++;
                    report.Close();
                }
            }
            if (checkedReports > 0) Console.WriteLine("PASS: " + checkedReports + " report designs/subreports inspected; no direct retired payment-table dependencies.");
            var payments = Dataset(app.GetType("BarakalAlkheir.Lic.frmPaymentReport", true), "cTable");
            var rentals = Dataset(app.GetType("BarakalAlkheir.Lic.frmLicenseMainPage", true), "makePrintTable");
            var documents = Dataset(app.GetType("BarakalAlkheir.Docs.frmDocMainPage", true), "makePrintTable");
            foreach (string name in new[] { "PaymentReport", "PaymentReport1", "OtherPaymentReport", "NotPaymentReport", "DeservedPaymentReport", "rentLicence" })
            using (var report = new ReportDocument())
            {
                report.Load(Path.Combine(root, "otherReports", name + ".rpt"), OpenReportMethod.OpenReportByTempCopy);
                report.SetDataSource(name == "rentLicence" ? rentals : name == "OtherPaymentReport" ? documents : payments);
                report.SetParameterValue("title", "اختبار بيانات تجريبية - SYNTHETIC TEST");
                report.ExportToDisk(ExportFormatType.PortableDocFormat, Path.Combine(output, name + ".pdf"));
                report.Close();
                if (new FileInfo(Path.Combine(output, name + ".pdf")).Length == 0) throw new InvalidOperationException("Empty report export: " + name);
                Console.WriteLine("PASS: " + name + " binds the actual form dataset and exports synthetic PDF.");
            }
            return 0;
        }
        catch (Exception error)
        {
            // Report paths/types are diagnostic; never print embedded connection or credential details.
            Console.Error.WriteLine("Report verification stopped after " + checkedReports + " designs: " + error.GetType().Name);
            for (Exception cause = error.InnerException; cause != null; cause = cause.InnerException)
                Console.Error.WriteLine("Cause: " + cause.GetType().Name + (cause is FileNotFoundException ? " (" + ((FileNotFoundException)cause).FileName + ")" : ""));
            if (error is FormulaException) Console.Error.WriteLine(error.Message);
            return 1;
        }
    }
}
