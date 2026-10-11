using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BarakalAlkheir.Model;

internal static class PaymentMergeMigrationTests
{
    private static int checks;
    private static string connectionString;
    private static string scriptPath;
    private static void Script()
    {
        string script = File.ReadAllText(scriptPath);
        // The disposable connection has already selected its database; omit the reviewed production USE header.
        Sql(script.Substring(script.IndexOf("SET NOCOUNT ON;", StringComparison.Ordinal)));
    }
    private static void Check(bool condition, string name) { checks++; if (!condition) throw new InvalidOperationException(name); }
    private static object Sql(string sql)
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(sql, connection)) { connection.Open(); command.CommandTimeout = 120; return command.ExecuteScalar(); }
    }
    private static DbMigrator Migrator()
    {
        return new DbMigrator(new BarakalAlkheir.Model.Migrations.Configuration {
            TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient") });
    }
    private static void Fixture(bool applyBaseline = true)
    {
        using (var configured = new BarakeitAlkheirEntities(connectionString)) { }
        Database.SetInitializer<DbContext>(null);
        var baseline = (IMigrationMetadata)new BarakalAlkheir.Model.Migrations.ExistingDatabaseBaseline();
        using (var bytes = new MemoryStream(Convert.FromBase64String(baseline.Target)))
        using (var unzip = new GZipStream(bytes, CompressionMode.Decompress))
        {
            var schema = XDocument.Load(unzip).Descendants().Single(e => e.Name.LocalName == "StorageModels").Elements().Single();
            using (var reader = schema.CreateReader())
                Sql(System.Data.Entity.SqlServer.SqlProviderServices.Instance.CreateDatabaseScript(
                    (string)schema.Attribute("ProviderManifestToken"), new System.Data.Entity.Core.Metadata.Edm.StoreItemCollection(new[] { reader })));
        }
        Sql(@"INSERT dbo.RentLicenses(rentLicense_from,rentLicense_to,rentLicense_noOfMonths,rentLicense_active) VALUES('20250101','20250131',1,1),('20250101','20250131',1,0);
INSERT dbo.RentLicensesDetails(rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy)
VALUES(1,'20250101','20250131',1,100,1,'20180101','20180201',2,3);
SET IDENTITY_INSERT dbo.RentLicensesPayment ON;
INSERT dbo.RentLicensesPayment(rentLicensePaymentID,rentLicenseID,rentLicencesDetailsID,rentLicPay_VouNo,rentLicPay_Amount,rentLicPay_ForDate,rentLicPay_PaymentDate,added_Date,edited_Date,addedBy,editedBy)
VALUES(42,1,1,N'TEST',100,'20250115','20250201','20190101','20190201',4,5);
SET IDENTITY_INSERT dbo.RentLicensesPayment OFF;");
        if (applyBaseline) Migrator().Update("ExistingDatabaseBaseline");
    }
    private static void Run(string scenario, string mutation)
    {
        Fixture();
        if (mutation != null)
            foreach (string batch in mutation.Split(new[] { "\nGO\n" }, StringSplitOptions.RemoveEmptyEntries)) Sql(batch);
        var migrator = Migrator();
        bool succeeds = scenario != "null-paid-flag" && scenario != "receipt-trigger" && scenario != "sql-dependency"
            && scenario != "installment-trigger" && scenario != "existing-archive" && scenario != "incoming-foreign-key";
        int originalReceiptCount = Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPayment"));
        bool copiesMetadata = scenario == "valid" || scenario == "script-valid" || scenario == "duplicate-one-match"
            || scenario == "script-duplicate-one-match" || scenario == "billing-cutoff" || scenario == "partial-metadata";
        if (succeeds)
        {
            if (scenario.StartsWith("script-", StringComparison.Ordinal)) Script(); else migrator.Update("MergeLicencePaymentsIntoDetails");
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.tables WHERE name='RentLicensesPayment'")) == 0, "payment table removed");
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPaymentArchive_20261011")) == originalReceiptCount, "every original receipt retained in archive");
            if (scenario == "conflicting-single")
                Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPaymentArchive_20261011 WHERE rentLicensePaymentID=42 AND rentLicenseID=2 AND rentLicPay_Amount=40 AND rentLicPay_ForDate='20190815'")) == 1, "conflicting original values retained");
            if (scenario == "custom-receipt-column")
                Check(Convert.ToString(Sql("SELECT CustomReference FROM dbo.RentLicensesPaymentArchive_20261011 WHERE rentLicensePaymentID=42")) == "KEEP", "custom original column retained");
            using (var db = new BarakeitAlkheirEntities(connectionString))
            {
                var d = db.RentLicensesDetails.Single();
                Check(d.rentLicenseID == 1 && d.rd_amount == 100 && d.rd_isPaid == (scenario != "unpaid-with-receipt"), "schedule identity/charge/state remain authoritative");
                if (copiesMetadata)
                {
                    Check(d.legacyPaymentID == 42 && d.rentLicPay_VouNo == (scenario == "partial-metadata" ? null : "TEST")
                        && d.rentLicPay_PaymentDate == (scenario == "partial-metadata" ? (DateTime?)null : new DateTime(2025,2,1)), "unique matching receipt metadata copied");
                    Check(d.payment_added_Date == new DateTime(2019,1,1) && d.payment_edited_Date == new DateTime(2019,2,1) && d.payment_addedBy == 4 && d.payment_editedBy == 5, "payment audit preserved separately");
                }
                else
                    Check(d.legacyPaymentID == null && d.rentLicPay_VouNo == null && d.rentLicPay_PaymentDate == null && d.rentLicPay_mandoubID == null
                        && d.payment_added_Date == null && d.payment_edited_Date == null && d.payment_addedBy == null && d.payment_editedBy == null, "ambiguous or unmatched metadata stays blank");
                Check(d.added_Date == new DateTime(2018,1,1) && d.edited_Date == new DateTime(2018,2,1) && d.addedBy == 2 && d.editedBy == 3, "schedule audit preserved");
                Check(d.RentLicens.rentLicenseID == 1, "merged lazy loading follows installment owner");
                Check(d.rd_fromDate == (scenario == "invalid-target" ? (DateTime?)null : new DateTime(2025,1,scenario == "billing-cutoff" || scenario == "wrong-billing-month" ? 11 : 1)), "installment date preserved");
            }
            Check(!migrator.GetPendingMigrations().Any(m => m.EndsWith("_MergeLicencePaymentsIntoDetails")), "migration history updated");
            if (scenario.StartsWith("script-", StringComparison.Ordinal)) Script(); else migrator.Update("MergeLicencePaymentsIntoDetails");
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPaymentArchive_20261011")) == originalReceiptCount, "repeat update does not duplicate archive");
            bool downgradeRejected = false;
            try { migrator.Update("ExistingDatabaseBaseline"); } catch (NotSupportedException) { downgradeRejected = true; }
            Check(downgradeRejected, "automatic downgrade refuses payment loss");
            VerifyDateRecovery(scenario);
        }
        else
        {
            bool rejected = false;
            try { if (scenario.StartsWith("script-", StringComparison.Ordinal)) Script(); else migrator.Update("MergeLicencePaymentsIntoDetails"); } catch (SqlException) { rejected = true; }
            Check(rejected, scenario + " rejected");
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.tables WHERE name='RentLicensesPayment'")) == 1, scenario + " retains original table");
            Check(Sql("SELECT COL_LENGTH('dbo.RentLicensesDetails','rentLicPay_VouNo')") == DBNull.Value, scenario + " adds no columns");
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.tables WHERE name='RentLicensesPaymentArchive_20261011'")) == (scenario == "existing-archive" ? 1 : 0), scenario + " preserves archive state");
            Check(migrator.GetPendingMigrations().Any(m => m.EndsWith("_MergeLicencePaymentsIntoDetails")), scenario + " keeps migration pending");
        }
        Console.WriteLine("PASS: migration scenario " + scenario);
    }
    private static void VerifyDateRecovery(string scenario)
    {
        const string unchanged = "SELECT rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy,rentLicPay_VouNo,rentLicPay_mandoubID,payment_added_Date,payment_edited_Date,payment_addedBy,payment_editedBy,legacyPaymentID FROM dbo.RentLicensesDetails FOR XML RAW";
        string before = Convert.ToString(Sql(unchanged));
        string archive = Convert.ToString(Sql("SELECT * FROM dbo.RentLicensesPaymentArchive_20261011 ORDER BY rentLicensePaymentID FOR XML RAW"));
        bool hasDate = scenario != "duplicate-no-match" && scenario != "duplicate-many-matches"
            && scenario != "duplicate-conflicting-dates" && scenario != "orphan" && scenario != "paid-without-receipt"
            && scenario != "unpaid-with-receipt" && scenario != "partial-metadata";
        if (scenario == "valid") Sql("UPDATE dbo.RentLicensesDetails SET rentLicPay_PaymentDate='20300101'");
        var migrator = Migrator();
        migrator.Update("RestoreArchivedLicencePaymentDates");
        object date = Sql("SELECT rentLicPay_PaymentDate FROM dbo.RentLicensesDetails");
        Check(hasDate ? date.Equals(scenario == "valid" ? new DateTime(2030,1,1) : new DateTime(2025,2,1)) : date == DBNull.Value,
            "date recovery uses only unambiguous linked archive dates and preserves existing dates: " + scenario);
        Check(Convert.ToString(Sql(unchanged)) == before, "date recovery preserves every other installment field");
        Check(Convert.ToString(Sql("SELECT * FROM dbo.RentLicensesPaymentArchive_20261011 ORDER BY rentLicensePaymentID FOR XML RAW")) == archive, "date recovery preserves archive");
        Check(!migrator.GetPendingMigrations().Any(), "date recovery migration recorded");
        migrator.Update("RestoreArchivedLicencePaymentDates");
        Check(Sql("SELECT rentLicPay_PaymentDate FROM dbo.RentLicensesDetails").Equals(date), "date recovery repeat is harmless");
        bool rejected = false;
        try { migrator.Update("MergeLicencePaymentsIntoDetails"); } catch (NotSupportedException) { rejected = true; }
        Check(rejected, "date recovery downgrade refuses to erase historical dates");
    }
    private static int Main(string[] args)
    {
        var entry = XDocument.Load(args[0]).Root.Element("connectionStrings").Elements("add").Single(e => (string)e.Attribute("name") == "BarakeitAlkheirEntities");
        var master = new SqlConnectionStringBuilder((string)entry.Attribute("connectionString")) { InitialCatalog = "master" };
        scriptPath = args[1];
        string database = null;
        try
        {
            var cases = new[] {
                new[] { "valid", (string)null },
                new[] { "script-valid", (string)null },
                new[] { "conflicting-single", "UPDATE dbo.RentLicensesPayment SET rentLicenseID=2,rentLicPay_Amount=40,rentLicPay_ForDate='20190815'" },
                new[] { "duplicate-one-match", "INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID) VALUES(1,1)" },
                new[] { "script-duplicate-one-match", "INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID) VALUES(1,1)" },
                new[] { "duplicate-no-match", "UPDATE dbo.RentLicensesPayment SET rentLicenseID=2; INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID) VALUES(2,1)" },
                new[] { "duplicate-many-matches", "INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate,rentLicPay_VouNo) VALUES(1,1,100,'20250115',N'OTHER')" },
                new[] { "duplicate-identical", "INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate,rentLicPay_VouNo,rentLicPay_PaymentDate) VALUES(1,1,100,'20250115',N'TEST','20250201')" },
                new[] { "duplicate-conflicting-dates", "INSERT dbo.RentLicensesPayment(rentLicenseID,rentLicencesDetailsID,rentLicPay_Amount,rentLicPay_ForDate,rentLicPay_VouNo,rentLicPay_PaymentDate) VALUES(1,1,100,'20250115',N'OTHER','20250301')" },
                new[] { "orphan", "UPDATE dbo.RentLicensesPayment SET rentLicencesDetailsID=999" },
                new[] { "paid-without-receipt", "DELETE dbo.RentLicensesPayment" },
                new[] { "unpaid-with-receipt", "UPDATE dbo.RentLicensesDetails SET rd_isPaid=0" },
                new[] { "invalid-target", "UPDATE dbo.RentLicensesDetails SET rd_fromDate=NULL" },
                new[] { "billing-cutoff", "UPDATE dbo.RentLicensesDetails SET rd_fromDate='20250111'; UPDATE dbo.RentLicensesPayment SET rentLicPay_ForDate='20250220'" },
                new[] { "wrong-billing-month", "UPDATE dbo.RentLicensesDetails SET rd_fromDate='20250111'" },
                new[] { "partial-metadata", "UPDATE dbo.RentLicensesPayment SET rentLicPay_VouNo=NULL,rentLicPay_PaymentDate=NULL" },
                new[] { "custom-receipt-column", "ALTER TABLE dbo.RentLicensesPayment ADD CustomReference nvarchar(20)\nGO\nUPDATE dbo.RentLicensesPayment SET CustomReference=N'KEEP',rentLicenseID=2" },
                new[] { "null-paid-flag", "UPDATE dbo.RentLicensesDetails SET rd_isPaid=NULL" },
                new[] { "receipt-trigger", "CREATE TRIGGER dbo.TestReceiptTrigger ON dbo.RentLicensesPayment AFTER INSERT AS BEGIN SET NOCOUNT ON; END" },
                new[] { "sql-dependency", "CREATE VIEW dbo.TestReceiptView AS SELECT rentLicensePaymentID FROM dbo.RentLicensesPayment" },
                new[] { "installment-trigger", "CREATE TRIGGER dbo.TestInstallmentTrigger ON dbo.RentLicensesDetails AFTER UPDATE AS BEGIN SET NOCOUNT ON; UPDATE d SET rd_amount=999 FROM dbo.RentLicensesDetails d JOIN inserted i ON i.rentLicencesDetailsID=d.rentLicencesDetailsID; END" },
                new[] { "existing-archive", "CREATE TABLE dbo.RentLicensesPaymentArchive_20261011(ID int)" },
                new[] { "incoming-foreign-key", "CREATE TABLE dbo.TestReceiptReference(ID int REFERENCES dbo.RentLicensesPayment(rentLicensePaymentID))" }
            };
            foreach (var item in cases)
            {
                database = "BarakatMergeTests_" + Guid.NewGuid().ToString("N");
                connectionString = master.ConnectionString;
                Sql("CREATE DATABASE [" + database + "]");
                try {
                    connectionString = new SqlConnectionStringBuilder(master.ConnectionString) { InitialCatalog = database }.ConnectionString;
                    Run(item[0], item[1]);
                }
                finally {
                    SqlConnection.ClearAllPools(); connectionString = master.ConnectionString;
                    Sql("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + database + "]");
                    database = null;
                }
            }
            foreach (string scenario in new[] { "first-start", "current", "concurrent", "blocked-retry", "empty", "newer-database", "missing-column" })
            {
                database = "BarakatStartupTests_" + Guid.NewGuid().ToString("N");
                connectionString = master.ConnectionString;
                Sql("CREATE DATABASE [" + database + "]");
                try
                {
                    connectionString = new SqlConnectionStringBuilder(master.ConnectionString) { InitialCatalog = database }.ConnectionString;
                    RunStartup(scenario);
                }
                finally
                {
                    SqlConnection.ClearAllPools(); connectionString = master.ConnectionString;
                    Sql("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + database + "]");
                    database = null;
                }
            }
            Console.WriteLine("PASS: " + checks + " migration assertions; disposable databases removed; customer database unchanged.");
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine("Migration verification failed: " + error.GetType().Name + " / " + error.Message + "\n" + error.StackTrace); return 1; }
    }

    private static void RunStartup(string scenario)
    {
        if (scenario == "empty")
        {
            bool rejected = false;
            try { BarakalAlkheir.Model.Migrations.DatabaseMigrationRunner.ApplyPending(connectionString); }
            catch (InvalidOperationException) { rejected = true; }
            Check(rejected && Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.tables")) == 0, "empty database rejected without recording a false baseline");
            return;
        }
        Fixture(false);
        if (scenario == "current" || scenario == "newer-database" || scenario == "missing-column")
            Migrator().Update();
        if (scenario == "newer-database")
            Sql("INSERT dbo.__MigrationHistory(MigrationId,ContextKey,Model,ProductVersion) SELECT '209901010000000_Future',ContextKey,Model,ProductVersion FROM dbo.__MigrationHistory WHERE MigrationId LIKE '%RestoreArchivedLicencePaymentDates'");
        if (scenario == "missing-column")
            Sql("ALTER TABLE dbo.RentLicensesDetails DROP CONSTRAINT CK_LicenceInstalment_PaymentState; ALTER TABLE dbo.RentLicensesDetails DROP COLUMN rentLicPay_VouNo;");
        if (scenario == "blocked-retry")
            Sql("UPDATE dbo.RentLicensesDetails SET rd_isPaid=NULL");
        if (scenario == "blocked-retry" || scenario == "newer-database" || scenario == "missing-column")
        {
            bool rejected = false;
            try { BarakalAlkheir.Model.Migrations.DatabaseMigrationRunner.ApplyPending(connectionString); }
            catch (Exception error)
            {
                if (!(error is InvalidOperationException) && !(error is SqlException)) throw;
                rejected = true;
            }
            Check(rejected, "startup stops for " + scenario);
            if (scenario != "blocked-retry") return;
            Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPayment")) == 1, "failed startup preserves original receipts");
            Check(Sql("SELECT COL_LENGTH('dbo.RentLicensesDetails','rentLicPay_VouNo')") == DBNull.Value, "failed migration rolls back new columns");
            Sql("UPDATE dbo.RentLicensesDetails SET rd_isPaid=1");
        }
        if (scenario == "concurrent")
        {
            var errors = new System.Collections.Generic.List<Exception>();
            using (var start = new System.Threading.ManualResetEvent(false))
            {
                System.Threading.ThreadStart run = () => {
                    start.WaitOne();
                    try { BarakalAlkheir.Model.Migrations.DatabaseMigrationRunner.ApplyPending(connectionString); }
                    catch (Exception error) { lock (errors) errors.Add(error); }
                };
                var first = new System.Threading.Thread(run); var second = new System.Threading.Thread(run);
                first.Start(); second.Start(); start.Set(); first.Join(); second.Join();
            }
            if (errors.Count != 0) throw errors[0];
            Check(errors.Count == 0, "simultaneous client startups both succeed");
        }
        else BarakalAlkheir.Model.Migrations.DatabaseMigrationRunner.ApplyPending(connectionString);
        Check(!Migrator().GetPendingMigrations().Any(), "startup applies all explicit migrations: " + scenario);
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.__MigrationHistory")) == 3, "startup records each migration once");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesPaymentArchive_20261011")) == 1, "startup preserves receipt archive");
        using (var db = new BarakeitAlkheirEntities(connectionString))
            Check(db.RentLicensesDetails.Single().rentLicPay_VouNo == "TEST", "EF payment page query succeeds after startup migration");
        BarakalAlkheir.Model.Migrations.DatabaseMigrationRunner.ApplyPending(connectionString);
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.__MigrationHistory")) == 3, "repeat startup skips applied migrations");
        Console.WriteLine("PASS: application startup migration scenario " + scenario);
    }
}
