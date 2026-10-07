using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using BarakalAlkheir;
using BarakalAlkheir.Lic;
using BarakalAlkheir.Model;

internal static class LicenceRentalTests
{
    private static int assertions;
    private static string provider;
    private static string entity;
    private static LicenseRentalWorkflow workflow;

    private static void Check(bool condition, string description)
    {
        assertions++;
        if (!condition) throw new Exception("FAIL: " + description);
    }

    private static void Reject(Action action, string description)
    {
        bool rejected = false;
        try { action(); } catch (InvalidOperationException) { rejected = true; }
        Check(rejected, description);
    }

    private static BarakeitAlkheirEntities Db() { return new BarakeitAlkheirEntities(entity); }
    private static object Sql(string text)
    {
        using (var connection = new SqlConnection(provider))
        using (var command = new SqlCommand(text, connection))
        {
            connection.Open();
            command.CommandTimeout = 30;
            return command.ExecuteScalar();
        }
    }
    private static DateTime? Version(int id) { using (var db = Db()) return db.RentLicenses.Single(r => r.rentLicenseID == id).edited_Date; }
    private static RentLicensesDetail[] Details(int id) { using (var db = Db()) return db.RentLicensesDetails.Where(d => d.rentLicenseID == id).OrderBy(d => d.rd_mNo).ToArray(); }
    private static RentLicensesPayment Receipt(int id) { using (var db = Db()) return db.RentLicensesPayments.Single(p => p.rentLicensePaymentID == id); }
    private static int Pay(int id)
    {
        var detail = Details(id).First(d => d.rd_isPaid == false);
        return workflow.SavePayment(-1, id, detail.rentLicencesDetailsID, LicenseRentalRules.Amount(detail.rd_amount), "TEST", DateTime.Today, 1, null);
    }

    private static void PureRules()
    {
        foreach (string name in new[] { "en-US", "en-GB", "ar-KW" })
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(name);
            Check(LicenseRentalRules.ParsePositiveAmount(125.125m.ToString()) == 125.125m, "amount culture " + name);
            Check(LicenseRentalRules.BillingDate(new DateTime(2026, 9, 16)) == new DateTime(2026, 10, 15), "payment date culture " + name);
            Reject(() => LicenseRentalRules.ParsePositiveAmount("0"), "zero charge " + name);
            Reject(() => LicenseRentalRules.ParsePositiveAmount("-1"), "negative charge " + name);
        }
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var start = new DateTime(2025, 1, 31);
        Check(LicenseRentalRules.InstalmentStart(start, 3) == new DateTime(2025, 3, 31), "month-end extension anchored to contract");
        Check(LicenseRentalRules.InstalmentEnd(start, 2).AddDays(1) == LicenseRentalRules.InstalmentStart(start, 3), "no overlap");
        Check(LicenseRentalRules.InstalmentStart(new DateTime(2024, 1, 31), 2).Day == 29, "leap year");
        Check(LicenseRentalRules.BillingDate(new DateTime(2025, 12, 11)) == new DateTime(2026, 1, 15), "year rollover");
        Check(LicenseRentalRules.BillingDate(new DateTime(2025, 5, 10)).Month == 5, "cutoff day 10");
        Check(LicenseRentalRules.BillingDate(new DateTime(2025, 5, 11)).Month == 6, "cutoff day 11");
        Check(LicenseRentalReports.InPeriod(new DateTime(2025, 4, 11), true, new DateTime(2025, 5, 1), new DateTime(2025, 5, 1)), "report April 11 belongs to May");
        Check(!LicenseRentalReports.InPeriod(new DateTime(2025, 5, 11), true, new DateTime(2025, 5, 1), new DateTime(2025, 5, 1)), "report May 11 excluded from May");
        Reject(() => LicenseRentalRules.InstalmentStart(start, 0), "zero month");
        Reject(() => LicenseRentalRules.InstalmentEnd(start, 1001), "maximum duration");
        Check(Enumerable.Repeat(LicenseRentalRules.Amount(0.1f), 10).Sum() == 1m, "decimal total avoids binary float accumulation");
    }

    private static void DateFormats()
    {
        CultureInfo original = Thread.CurrentThread.CurrentCulture;
        try
        {
            foreach (string name in new[] { "en-US", "en-GB", "ar-KW", "ar-SA", "th-TH" })
            {
                var culture = new CultureInfo(name, false);
                culture.DateTimeFormat.DateSeparator = "-";
                culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                Thread.CurrentThread.CurrentCulture = culture;
                Check(LicenseRentalRules.FormatDate(new DateTime(2026, 4, 5)) == "05/04/2026", "unambiguous day/month order " + name);
                Check(LicenseRentalRules.FormatDate(new DateTime(2026, 9, 16)) == "16/09/2026", "Gregorian date and literal slash " + name);
                Check(LicenseRentalRules.FormatDate(new DateTime(2024, 2, 29)) == "29/02/2024", "leap date display " + name);
                foreach (Type type in new[] { typeof(frmRentALicense), typeof(frmRentLicensePayment), typeof(frmChangeRentingAmount),
                    typeof(frmPaymentReport), typeof(frmLicenseMainPage), typeof(frmRentingPaymentDetails) })
                using (var form = (System.Windows.Forms.Form)Activator.CreateInstance(type))
                {
                    foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                    {
                        var native = field.GetValue(form) as System.Windows.Forms.DateTimePicker;
                        var telerik = field.GetValue(form) as Telerik.WinControls.UI.RadDateTimePicker;
                        if (native == null && telerik == null) continue;
                        string format = native != null ? native.CustomFormat : telerik.CustomFormat;
                        foreach (DateTime date in new[] { new DateTime(2026, 4, 5), new DateTime(2026, 9, 16), new DateTime(2024, 2, 29) })
                        {
                            string actual;
                            if (native != null) { IntPtr handle = native.Handle; native.Value = date; actual = native.Text; }
                            else { IntPtr handle = telerik.Handle; telerik.Value = date; actual = telerik.Text; }
                            string expected = date.ToString(format, CultureInfo.InvariantCulture);
                            Check(actual == expected, "date control " + type.Name + "." + field.Name + " " + name + " expected " + expected + " got " + actual);
                        }
                    }
                }
            }
        }
        finally { Thread.CurrentThread.CurrentCulture = original; }
    }

    private static void Integration(string fixture, string output, string constraints)
    {
        Sql(File.ReadAllText(fixture));
        Sql("INSERT dbo.CompanyLicences(isActive,isMainLicences,licenseNo,commercialName) VALUES(1,0,N'TEST-1',N'Test licence');" +
            "INSERT dbo.CompanyLicences(isActive,isMainLicences,licenseNo,commercialName) VALUES(1,0,N'TEST-2',N'Test licence 2');" +
            "INSERT dbo.Customers(customer_Name) VALUES(N'Test customer'); INSERT dbo.Employees(emp_isMandoub,emp_namefullar) VALUES(1,N'Test collector');");
        MainClass.userID = 1;
        MainClass.userGroupID = 1;
        MainClass.isSuperUser = true;
        workflow = new LicenseRentalWorkflow(entity);
        DateTime start = new DateTime(2025, 1, 31);
        int rental = workflow.SaveRental(-1, 1, 1, "ORIGINAL", start, 2, 1, 100m, null);
        Check(Details(rental).Length == 2, "create two instalments");
        Reject(() => workflow.SaveRental(-1, 1, 1, "DUPLICATE", start, 2, 1, 100m, null), "duplicate active licence");
        Reject(() => workflow.SaveRental(-1, 2, 1, "ZERO", start, 2, 1, 0m, null), "zero rate rejected transactionally");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicenses")) == 1, "rejected create rolled back");
        workflow.SaveRental(rental, 1, 1, "ORIGINAL", start, 3, 1, 0m, Version(rental));
        Check(Details(rental)[2].rd_fromDate == new DateTime(2025, 3, 31), "database extension date");
        int firstReceipt = Pay(rental);
        var first = Details(rental)[0];
        Reject(() => workflow.SavePayment(-1, rental, first.rentLicencesDetailsID, 100m, "DUP", DateTime.Today, 1, null), "duplicate receipt rejected");
        workflow.ChangeRate(rental, new DateTime(2025, 2, 28), 150m, Version(rental));
        var changed = Details(rental);
        Check(changed[0].rd_amount == 100f && changed[1].rd_amount == 150f && changed[2].rd_amount == 150f, "rate change preserves paid month");
        workflow.SaveRental(rental, 1, 1, "ORIGINAL", start, 4, 2, 0m, Version(rental));
        Check(Details(rental).Select(d => d.rd_amount).SequenceEqual(new float?[] { 100, 150, 150, 150 }), "edit and extension preserve scheduled rates");
        using (var db = Db()) Check(db.RentLicenses.Single(r => r.rentLicenseID == rental).rentLicense_DamanAmount == 550f, "contract total follows schedule");
        Reject(() => workflow.SaveRental(rental, 2, 1, "MOVE", start, 4, 1, 0m, Version(rental)), "paid rental cannot change licence");
        var next = Details(rental)[1];
        Reject(() => workflow.SavePayment(-1, rental, next.rentLicencesDetailsID, 100m, "STALE", DateTime.Today, 1, null), "stale charge rejected");
        Reject(() => workflow.SavePayment(-1, rental, next.rentLicencesDetailsID, 150m, "COLLECTOR", DateTime.Today, -1, null), "collector required");
        var receipt = Receipt(firstReceipt);
        workflow.SavePayment(firstReceipt, rental, first.rentLicencesDetailsID, 100m, "EDITED", new DateTime(2025, 2, 20), 1, receipt.edited_Date);
        Reject(() => workflow.SavePayment(firstReceipt, rental, first.rentLicencesDetailsID, 100m, "STALE EDIT", DateTime.Today, 1, receipt.edited_Date), "stale receipt edit rejected");
        receipt = Receipt(firstReceipt);
        Check(receipt.rentLicPay_VouNo == "EDITED" && receipt.rentLicenseID == rental && receipt.rentLicencesDetailsID == first.rentLicencesDetailsID, "receipt edit retains links");
        Check(Details(rental)[0].rd_isPaid == true, "paid flag saved");
        Reject(() => workflow.CloseRental(rental), "unpaid rental cannot close");
        workflow.DeletePayment(firstReceipt, rental, receipt.edited_Date);
        Check(Details(rental)[0].rd_isPaid == false, "delete unsets paid flag");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPayment")) == 0, "delete removes receipt");

        Sql("CREATE TRIGGER dbo.LicenceTestRejectReceipt ON dbo.RentLicensesPayment AFTER INSERT AS BEGIN RAISERROR('TEST ROLLBACK',16,1); ROLLBACK TRANSACTION; END");
        bool failed = false;
        try { Pay(rental); } catch { failed = true; }
        Check(failed && Details(rental)[0].rd_isPaid == false && Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPayment")) == 0, "receipt and paid flag roll back together");
        Sql("DROP TRIGGER dbo.LicenceTestRejectReceipt");

        int successes = 0, conflicts = 0;
        var gate = new ManualResetEvent(false);
        var errors = new System.Collections.Generic.List<Exception>();
        ThreadStart payOnce = () => {
            gate.WaitOne();
            try { workflow.SavePayment(-1, rental, first.rentLicencesDetailsID, 100m, "RACE", DateTime.Today, 1, null); Interlocked.Increment(ref successes); }
            catch (RentalChangedException) { Interlocked.Increment(ref conflicts); }
            catch (Exception ex) { lock (errors) errors.Add(ex); }
        };
        var a = new Thread(payOnce); var b = new Thread(payOnce); a.Start(); b.Start(); gate.Set(); a.Join(); b.Join();
        if (errors.Count > 0) throw errors[0];
        Check(successes == 1 && conflicts == 1, "concurrent payment has exactly one winner");
        workflow.SaveRental(rental, 1, 1, "ORIGINAL", start, 2, 2, 0m, Version(rental));
        Check(Details(rental).Length == 2, "shortening removes only unpaid tail");
        Pay(rental);
        Reject(() => workflow.SaveRental(rental, 1, 1, "SHORT", start, 1, 2, 0m, Version(rental)), "shortening across paid month rejected");

        int historicalReceipt;
        using (var db = Db()) historicalReceipt = db.RentLicensesPayments.First(p => p.rentLicenseID == rental).rentLicensePaymentID;
        // Deliberately corrupt only the disposable fixture to exercise fail-closed paths.
        Sql("INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate) SELECT rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate FROM dbo.RentLicensesPayment WHERE rentLicensePaymentID=" + historicalReceipt);
        Reject(() => workflow.CloseRental(rental), "duplicate cannot conceal unpaid balance");
        Reject(() => workflow.DeletePayment(historicalReceipt, rental, Receipt(historicalReceipt).edited_Date), "ambiguous duplicate deletion blocked");
        Sql("DELETE dbo.RentLicensesPayment WHERE rentLicensePaymentID=(SELECT MAX(rentLicensePaymentID) FROM dbo.RentLicensesPayment)");
        int crossRental = workflow.SaveRental(-1, 2, 1, "CROSS", start, 1, 1, 100m, null);
        Sql("UPDATE dbo.RentLicensesPayment SET rentLicenseID=" + crossRental + " WHERE rentLicensePaymentID=" + historicalReceipt);
        Reject(() => workflow.CloseRental(rental), "cross-rental receipt blocks source settlement");
        Reject(() => Pay(crossRental), "cross-rental receipt blocks destination payment");
        using (var db = Db()) Check(LicenseRentalReports.PaidByInstalment(db).Values.Sum() == 150m, "misallocated receipt does not settle another rental");
        Sql("UPDATE dbo.RentLicensesPayment SET rentLicenseID=" + rental + " WHERE rentLicensePaymentID=" + historicalReceipt);
        workflow.DeleteRental(crossRental);
        workflow.CloseRental(rental);
        Reject(() => workflow.DeletePayment(historicalReceipt, rental, Receipt(historicalReceipt).edited_Date), "closed rental is financially read-only");
        int replacement = workflow.SaveRental(-1, 1, 1, "REPLACEMENT", new DateTime(2025, 5, 1), 1, 1, 200m, null);
        Check(replacement != rental, "licence can be re-rented after closure");
        Reject(() => workflow.SavePayment(historicalReceipt, replacement, first.rentLicencesDetailsID, 100m, "MOVE", DateTime.Today, 1, null), "receipt cannot move to replacement");
        int other = workflow.SaveRental(-1, 2, 1, "OTHER", new DateTime(2025, 5, 1), 1, 1, 200m, null);
        MainClass.isSuperUser = false;
        Reject(() => workflow.CloseRental(other), "operation permission enforced without toolbar");
        MainClass.isSuperUser = true;
        workflow.DeleteRental(other);
        int precise = workflow.SaveRental(-1, 2, 1, "PRECISION", start, 12, 1, 100.1234m, null);
        Check(Details(precise).Length == 12, "valid charges are not rejected because REAL contract summary rounds");
        DateTime? oldVersion = Version(precise);
        workflow.SaveRental(precise, 2, 1, "PRECISION EDIT", start, 12, 2, 0m, oldVersion);
        Reject(() => workflow.SaveRental(precise, 2, 1, "STALE RENTAL", start, 12, 3, 0m, oldVersion), "stale rental edit rejected");
        Reject(() => workflow.ChangeRate(precise, start.AddYears(2), 200m, Version(precise)), "rate date must affect an unpaid instalment");
        workflow.DeleteRental(precise);
        using (var db = Db()) {
            var totals = LicenseRentalReports.PaidByInstalment(db);
            Check(totals.Values.Sum() == 250m, "receipt and schedule reporting totals reconcile");
        }
        FormChecks(output, historicalReceipt, rental);
        CorrectionChecks(output, constraints, historicalReceipt);
        Sql(File.ReadAllText(constraints));
        Sql(File.ReadAllText(constraints));
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.foreign_keys WHERE name='FK_LicenceReceipt_InstalmentRental' AND is_disabled=0 AND is_not_trusted=0")) == 1, "validated composite FK installed idempotently");
        bool duplicateBlocked = false;
        try { Sql("INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate) SELECT rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate FROM dbo.RentLicensesPayment WHERE rentLicensePaymentID=" + historicalReceipt); }
        catch (SqlException) { duplicateBlocked = true; }
        Check(duplicateBlocked, "database prevents duplicate receipts");
        bool wrongRentalBlocked = false;
        try { Sql("UPDATE dbo.RentLicensesPayment SET rentLicenseID=" + replacement + " WHERE rentLicensePaymentID=" + historicalReceipt); }
        catch (SqlException) { wrongRentalBlocked = true; }
        Check(wrongRentalBlocked, "composite FK prevents cross-rental receipt links");
        string rollback = Path.Combine(Path.GetDirectoryName(constraints), "licence-rental-integrity-rollback.sql");
        Sql(File.ReadAllText(rollback));
        Sql(File.ReadAllText(rollback));
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.foreign_keys WHERE name='FK_LicenceReceipt_InstalmentRental'")) == 0, "constraint rollback is idempotent");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPayment")) == 2, "constraint rollback preserves receipts");
    }

    private static void FormChecks(string output, int receiptId, int rentalId)
    {
        string config = Path.Combine(output, "LicenceRental.Ui.config");
        new XDocument(new XElement("configuration", new XElement("connectionStrings", new XElement("add",
            new XAttribute("name", "BarakeitAlkheirEntities"), new XAttribute("connectionString", entity),
            new XAttribute("providerName", "System.Data.EntityClient"))))).Save(config);
        var domain = AppDomain.CreateDomain("LicenceRental UI tests", null,
            new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory, ConfigurationFile = config });
        try {
            var smoke = (LicenceRentalFormSmoke)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(LicenceRentalFormSmoke).FullName);
            Check(smoke.Run(receiptId, rentalId, output), "historical receipt and Arabic form rendering");
        }
        finally { AppDomain.Unload(domain); File.Delete(config); }
    }

    private static void CorrectionChecks(string output, string constraints, int receiptId)
    {
        string script = Path.Combine(Path.GetDirectoryName(constraints), "Invoke-LicenceRentalCorrection.ps1");
        string config = Path.Combine(output, "LicenceRental.Correction.config");
        string manifest = Path.Combine(output, "correction-test.json");
        string package = Path.Combine(output, "correction-test.clixml");
        new XDocument(new XElement("configuration", new XElement("connectionStrings", new XElement("add",
            new XAttribute("name", "BarakeitAlkheirEntities"), new XAttribute("connectionString", entity))))).Save(config);
        string database = new SqlConnectionStringBuilder(provider).InitialCatalog;
        Func<string, bool> run = mode => {
            var start = new System.Diagnostics.ProcessStartInfo("powershell.exe",
                "-NoProfile -ExecutionPolicy Bypass -File \"" + script + "\" -Mode " + mode +
                " -ConfigPath \"" + config + "\" -ExpectedDatabase " + database + " -PackagePath \"" + package +
                "\" -ManifestPath \"" + manifest + "\" -AuditUserId 1") {
                    UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true
                };
            using (var process = System.Diagnostics.Process.Start(start)) {
                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode != 0 && mode != "Apply") Console.Error.WriteLine(stdout + stderr);
                return process.ExitCode == 0;
            }
        };
        try {
            File.Delete(package);
            string original = Receipt(receiptId).rentLicPay_VouNo;
            File.WriteAllText(manifest, "[{\"table\":\"RentLicensesPayment\",\"id\":" + receiptId + ",\"operation\":\"update\",\"reason\":\"Synthetic test\",\"changes\":{\"rentLicPay_VouNo\":\"CORRECTED\"}}]");
            Check(run("Preview") && Receipt(receiptId).rentLicPay_VouNo == original, "correction preview is read-only");
            Check(run("Apply") && Receipt(receiptId).rentLicPay_VouNo == "CORRECTED", "reviewed correction applied");
            Check(!run("Apply"), "stale correction package rejected");
            Check(run("Rollback") && Receipt(receiptId).rentLicPay_VouNo == original, "correction rollback restores original values");
            File.Delete(package);
            int secondId = Convert.ToInt32(Sql("SELECT MAX(rentLicensePaymentID) FROM dbo.RentLicensesPayment WHERE rentLicensePaymentID<>" + receiptId));
            File.WriteAllText(manifest, "[{\"table\":\"RentLicensesPayment\",\"id\":" + receiptId + ",\"operation\":\"update\",\"reason\":\"Atomic batch test\",\"changes\":{\"rentLicPay_VouNo\":\"BATCH\"}},{\"table\":\"RentLicensesPayment\",\"id\":" + secondId + ",\"operation\":\"update\",\"reason\":\"Stale second row\",\"changes\":{\"rentLicPay_VouNo\":\"BATCH2\"}}]");
            Check(run("Preview"), "batch preview created");
            Sql("UPDATE dbo.RentLicensesPayment SET editedBy=999 WHERE rentLicensePaymentID=" + secondId);
            Check(!run("Apply") && Receipt(receiptId).rentLicPay_VouNo == original, "stale second row rolls back first correction");
            Sql("UPDATE dbo.RentLicensesPayment SET editedBy=1 WHERE rentLicensePaymentID=" + secondId);
            File.Delete(package);
            File.WriteAllText(manifest, "[{\"table\":\"RentLicensesPayment\",\"id\":" + receiptId + ",\"operation\":\"delete\",\"reason\":\"Synthetic deletion rollback test\"}]");
            Check(run("Preview") && run("Apply"), "reviewed deletion applied");
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPayment WHERE rentLicensePaymentID=" + receiptId)) == 0, "deleted receipt absent");
            Check(run("Rollback") && Receipt(receiptId).rentLicPay_VouNo == original, "deleted receipt restored with original identity");
        }
        finally { File.Delete(config); File.Delete(manifest); File.Delete(package); }
    }

    [STAThread]
    private static int Main(string[] args)
    {
        string createdDatabase = null;
        SqlConnectionStringBuilder master = null;
        try {
            PureRules();
            DateFormats();
            if (args.Length == 4) {
                var configuration = XDocument.Load(args[0]);
                string configured = configuration.Root.Element("connectionStrings").Elements("add").Single(e => (string)e.Attribute("name") == "BarakeitAlkheirEntities").Attribute("connectionString").Value;
                var entityBuilder = new System.Data.EntityClient.EntityConnectionStringBuilder(configured);
                master = new SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString) { InitialCatalog = "master", ConnectTimeout = 5 };
                string name = "BarakatLicenceTests_" + Guid.NewGuid().ToString("N");
                using (var connection = new SqlConnection(master.ConnectionString)) {
                    connection.Open();
                    using (var command = new SqlCommand("CREATE DATABASE [" + name + "]", connection)) command.ExecuteNonQuery();
                }
                createdDatabase = name;
                provider = new SqlConnectionStringBuilder(master.ConnectionString) { InitialCatalog = name }.ConnectionString;
                entityBuilder.ProviderConnectionString = provider;
                entity = entityBuilder.ConnectionString;
                Integration(args[1], args[2], args[3]);
            }
            Console.WriteLine("PASS: " + assertions + " assertions" + (createdDatabase == null ? " (rules and date controls; no database)" : " (rules, disposable SQL database, concurrent writes and form smoke checks)"));
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex.ToString()); return 1; }
        finally {
            SqlConnection.ClearAllPools();
            if (createdDatabase != null) {
                using (var connection = new SqlConnection(master.ConnectionString)) {
                    connection.Open();
                    using (var command = new SqlCommand("ALTER DATABASE [" + createdDatabase + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + createdDatabase + "]", connection)) command.ExecuteNonQuery();
                }
                Console.WriteLine("Disposable test database removed.");
            }
        }
    }
}

public sealed class LicenceRentalFormSmoke : MarshalByRefObject
{
    private static void ShowOffscreen(System.Windows.Forms.Form form)
    {
        form.ShowInTaskbar = false;
        form.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        form.Location = new System.Drawing.Point(-20000, -20000);
        form.Show();
        System.Windows.Forms.Application.DoEvents();
    }
    private static object Field(object form, string name) { return form.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form); }
    private static void Call(object form, string name, Type[] types, params object[] args) {
        form.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic, null, types, null).Invoke(form, args);
    }
    private static void Click(object form, string name) { Call(form, name, new[] { typeof(object), typeof(EventArgs) }, form, EventArgs.Empty); }
    public bool Run(int receiptId, int rentalId, string output)
    {
        MainClass.userID = 1; MainClass.userGroupID = 1; MainClass.isSuperUser = true;
        System.Windows.Forms.Application.EnableVisualStyles();
        using (var form = new frmRentLicensePayment()) {
            ShowOffscreen(form);
            PublicClass.bindMandoub((Telerik.WinControls.UI.RadDropDownList)Field(form, "cmbMandoub"));
            Call(form, "getTotalRow", Type.EmptyTypes);
            Call(form, "getData", new[] { typeof(int) }, receiptId);
            Call(form, "bindData", Type.EmptyTypes);
            if ((int)Field(form, "rentID") != rentalId || ((System.Windows.Forms.Control)Field(form, "txtrentLicenseNo")).Text != "ORIGINAL") return false;
            var toolbar = (BarakalAlkheir.components.banner)Field(form, "mainbar");
            if (toolbar.btnEdit.Enabled || toolbar.btnDelete.Enabled) return false;
            using (var image = new System.Drawing.Bitmap(form.Width, form.Height)) {
                form.DrawToBitmap(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height));
                image.Save(Path.Combine(output, "receipt-form.png"));
            }
            Click(form, "btnAdd_Click");
            if ((int)Field(form, "rentID") != -1 || (int)Field(form, "_rentLicencesDetailsID") != -1) return false;
            Click(form, "btnCancel_Click");
            if ((int)Field(form, "rentID") != rentalId || toolbar.btnEdit.Enabled) return false;
        }
        using (var form = new frmRentALicense()) {
            ShowOffscreen(form);
            Click(form, "btnLast_Click1");
            Click(form, "btnEdit_Click1");
            if (((System.Windows.Forms.Control)Field(form, "txtDateFrom1")).Enabled ||
                !((Telerik.WinControls.UI.RadTextBox)Field(form, "txtRentalAmount1")).ReadOnly) return false;
            ((System.Windows.Forms.NumericUpDown)Field(form, "txtNoOfmonths")).Value = 2;
            if (((System.Windows.Forms.Control)Field(form, "txtDamanAmount1")).Text != "400") return false;
            Click(form, "btnCancel_Click1");
            Click(form, "btnAdd_Click1");
            ((System.Windows.Forms.NumericUpDown)Field(form, "txtNoOfmonths")).Value = 2;
            ((System.Windows.Forms.DateTimePicker)Field(form, "txtDateFrom1")).Value = new DateTime(2025, 1, 31);
            if (((System.Windows.Forms.DateTimePicker)Field(form, "txtTo1")).Value.Date != new DateTime(2025, 3, 30)) return false;
            Click(form, "btnCancel_Click1");
            using (var image = new System.Drawing.Bitmap(form.Width, form.Height)) {
                form.DrawToBitmap(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height));
                image.Save(Path.Combine(output, "rental-form.png"));
            }
        }
        using (var form = new frmChangeRentingAmount()) {
            form._rentLicenseID = rentalId;
            ShowOffscreen(form);
            Click(form, "btnEdit_Click1");
            if ((string)Field(form, "change1") != "") return false;
        }
        using (var form = new frmPaymentReport()) {
            if (!((System.Windows.Forms.Control)Field(form, "chkShowOldPayment")).Enabled) return false;
        }
        using (var form = new frmRentingPaymentDetails()) {
            ShowOffscreen(form);
            ((Telerik.WinControls.UI.RadDateTimePicker)Field(form, "txtDate")).Value = new DateTime(2025, 1, 1);
            Call(form, "getData", Type.EmptyTypes);
            Call(form, "getSummaryData", Type.EmptyTypes);
            var grid = (System.Windows.Forms.DataGridView)Field(form, "GView");
            if (grid.Rows.Count != 5 || Convert.ToDecimal(grid.Rows[grid.Rows.Count - 1].Cells[18].Value) != 250m) return false;
            using (var image = new System.Drawing.Bitmap(form.Width, form.Height)) {
                form.DrawToBitmap(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height));
                image.Save(Path.Combine(output, "annual-form.png"));
            }
        }
        return true;
    }
}
