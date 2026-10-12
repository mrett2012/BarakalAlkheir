using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using BarakalAlkheir.Model;

// Every write targets a newly-created, uniquely-named disposable database.
// The application's configured catalog is never opened by this harness.
internal static class ReceiptAllocationRecoveryTests
{
    private const string Migration = "202610121200000_RestoreLicenceReceiptAllocations";
    private const string Archive = "dbo.RentLicensesPaymentArchive_20261011";
    private const string Evidence = "dbo.LicenceReceiptRecovery_20261012";
    private const string OriginalColumns = "rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy";
    private static readonly HashSet<int> Recovered = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 26, 35, 36, 37, 41 };
    private static readonly string[] Cases = {
        "cross-rental stale ID and provably wrong date", "wrong-month stale ID and provably wrong date",
        "missing detail ID and missing actual date", "billing cutoff day 10", "billing cutoff day 11",
        "December cutoff rolls into next year", "correct existing actual date", "unpaid remains unpaid",
        "different receipt amount", "duplicate same-amount receipts", "duplicate different-amount receipt",
        "duplicate paid installments", "duplicate paid and unpaid installments", "existing voucher",
        "already allocated target", "source receipt allocated elsewhere", "edited after consolidation",
        "added after consolidation", "unprovable existing actual date", "conflicting old linked dates",
        "null among old linked dates", "zero charge", "negative charge", "null receipt amount",
        "no archived receipt", "source actual date is null", "missing schedule date", "missing receipt billing date",
        "same month in different year", "existing payment added date", "existing payment edited date",
        "existing payment added user", "existing payment edited user", "existing collector",
        "correct date despite ambiguous old links", "null schedule audit dates", "multiple old links with one agreed date",
        "day 10 rejects next-month receipt", "day 11 rejects same-month receipt", "null rental identity",
        "receipt with null optional metadata", "audit exactly at consolidation"
    };
    private static int checks;
    private static string connectionString;

    private static void Check(bool condition, string message)
    {
        checks++;
        if (!condition) throw new InvalidOperationException(message);
    }

    private static object Sql(string sql, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddRange(parameters);
            command.CommandTimeout = 120;
            connection.Open();
            return command.ExecuteScalar();
        }
    }

    private static DbMigrator Migrator()
    {
        return new DbMigrator(new BarakalAlkheir.Model.Migrations.Configuration {
            TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient") });
    }

    private static void CreateSchema()
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
                    (string)schema.Attribute("ProviderManifestToken"),
                    new System.Data.Entity.Core.Metadata.Edm.StoreItemCollection(new[] { reader })));
        }
    }

    private static void AddRental(int id)
    {
        Sql(@"SET IDENTITY_INSERT dbo.RentLicenses ON;
INSERT dbo.RentLicenses(rentLicenseID,rentLicense_from,rentLicense_to,rentLicense_noOfMonths,rentLicense_active)
VALUES(@id,'20250101','20261231',24,1);
SET IDENTITY_INSERT dbo.RentLicenses OFF;", new SqlParameter("@id", id));
    }

    private static void AddDetail(int id, int rental, bool paid)
    {
        Sql(@"SET IDENTITY_INSERT dbo.RentLicensesDetails ON;
INSERT dbo.RentLicensesDetails(rentLicencesDetailsID,rentLicenseID,rd_fromDate,rd_toDate,rd_mNo,rd_amount,rd_isPaid,added_Date,edited_Date,addedBy,editedBy)
VALUES(@id,@rental,'20250101','20250131',1,40,@paid,'20180101','20180201',2,3);
SET IDENTITY_INSERT dbo.RentLicensesDetails OFF;",
            new SqlParameter("@id", id), new SqlParameter("@rental", rental), new SqlParameter("@paid", paid));
    }

    private static void AddReceipt(int id, int rental, int staleDetail, string billingDate, string actualDate)
    {
        Sql(@"SET IDENTITY_INSERT dbo.RentLicensesPayment ON;
INSERT dbo.RentLicensesPayment(rentLicensePaymentID,rentLicenseID,rentLicencesDetailsID,rentLicPay_VouNo,rentLicPay_Amount,rentLicPay_ForDate,rentLicPay_PaymentDate,rentLicPay_mandoubID,added_Date,edited_Date,addedBy,editedBy)
VALUES(@id,@rental,@detail,@voucher,40,@billing,@actual,7,'20190101','20190201',4,5);
SET IDENTITY_INSERT dbo.RentLicensesPayment OFF;",
            new SqlParameter("@id", id), new SqlParameter("@rental", rental), new SqlParameter("@detail", staleDetail),
            new SqlParameter("@voucher", "TEST-" + id),
            new SqlParameter("@billing", billingDate == null ? (object)DBNull.Value : DateTime.ParseExact(billingDate, "yyyyMMdd", null)),
            new SqlParameter("@actual", actualDate == null ? (object)DBNull.Value : DateTime.ParseExact(actualDate, "yyyyMMdd", null)));
    }

    private static void Fixture()
    {
        CreateSchema();
        for (int id = 1; id <= Cases.Length; id++)
        {
            AddRental(id);
            AddDetail(id, id, id != 8);
            if (id != 25) AddReceipt(1000 + id, id, 99999, "20250115", "20250201");
        }
        AddRental(901);
        AddReceipt(9001, 901, 1, "20200615", "20200620");
        AddReceipt(9002, 2, 2, "20250215", "20250320");
        AddReceipt(9003, 901, 20, "20200715", "20200620");
        AddReceipt(9004, 901, 20, "20200815", "20200720");
        AddReceipt(9005, 901, 21, "20200915", "20200620");
        AddReceipt(9006, 901, 21, "20201015", null);
        AddReceipt(9007, 901, 35, "20201115", "20200620");
        AddReceipt(9008, 901, 35, "20201215", "20200720");
        AddReceipt(9009, 901, 37, "20210115", "20200620");
        AddReceipt(9010, 901, 37, "20210215", "20200620");
        AddReceipt(9011, 10, 99999, "20250120", "20250201");
        AddReceipt(9012, 11, 99999, "20250120", "20250201");
        AddDetail(501, 12, true);
        AddDetail(502, 13, false);
        AddDetail(503, 901, true);
        Sql(@"UPDATE dbo.RentLicensesDetails SET rd_fromDate='20250110' WHERE rentLicencesDetailsID IN(4,38);
UPDATE dbo.RentLicensesDetails SET rd_fromDate='20250111' WHERE rentLicencesDetailsID IN(5,39);
UPDATE dbo.RentLicensesDetails SET rd_fromDate='20251211',rd_toDate='20260110',rd_mNo=12 WHERE rentLicencesDetailsID=6;
UPDATE dbo.RentLicensesPayment SET rentLicPay_ForDate='20250215' WHERE rentLicensePaymentID IN(1005,1038);
UPDATE dbo.RentLicensesPayment SET rentLicPay_ForDate='20260115' WHERE rentLicensePaymentID=1006;
UPDATE dbo.RentLicensesPayment SET rentLicPay_Amount=45 WHERE rentLicensePaymentID IN(1009,9012);
UPDATE dbo.RentLicensesDetails SET rd_amount=0 WHERE rentLicencesDetailsID=22;
UPDATE dbo.RentLicensesPayment SET rentLicPay_Amount=0 WHERE rentLicensePaymentID=1022;
UPDATE dbo.RentLicensesDetails SET rd_amount=-40 WHERE rentLicencesDetailsID=23;
UPDATE dbo.RentLicensesPayment SET rentLicPay_Amount=-40 WHERE rentLicensePaymentID=1023;
UPDATE dbo.RentLicensesPayment SET rentLicPay_Amount=NULL WHERE rentLicensePaymentID=1024;
UPDATE dbo.RentLicensesPayment SET rentLicPay_PaymentDate=NULL WHERE rentLicensePaymentID=1026;
UPDATE dbo.RentLicensesDetails SET rd_fromDate=NULL WHERE rentLicencesDetailsID=27;
UPDATE dbo.RentLicensesPayment SET rentLicPay_ForDate=NULL WHERE rentLicensePaymentID=1028;
UPDATE dbo.RentLicensesPayment SET rentLicPay_ForDate='20240115' WHERE rentLicensePaymentID=1029;
UPDATE dbo.RentLicensesDetails SET rentLicenseID=NULL WHERE rentLicencesDetailsID=40;
UPDATE dbo.RentLicensesDetails SET added_Date=NULL,edited_Date=NULL WHERE rentLicencesDetailsID=36;
UPDATE dbo.RentLicensesPayment SET rentLicPay_VouNo=NULL,rentLicPay_PaymentDate=NULL,rentLicPay_mandoubID=NULL,
 added_Date=NULL,edited_Date=NULL,addedBy=NULL,editedBy=NULL WHERE rentLicensePaymentID=1041;
ALTER TABLE dbo.RentLicensesDetails ADD RecoveryTestMarker nvarchar(40) NULL;");
        Sql("UPDATE dbo.RentLicensesDetails SET RecoveryTestMarker=N'Preserve custom column';");
        var migrator = Migrator();
        migrator.Update("AddLicenceAutomatedNumber");
        Check(migrator.GetDatabaseMigrations().Count() == 4, "fixture applies all four preceding migrations");
        Check(Convert.ToDateTime(Sql("SELECT rentLicPay_PaymentDate FROM dbo.RentLicensesDetails WHERE rentLicencesDetailsID=1")) == new DateTime(2020, 6, 20),
            "fixture reproduces old cross-rental ID-only date recovery");
        Check(Convert.ToDateTime(Sql("SELECT rentLicPay_PaymentDate FROM dbo.RentLicensesDetails WHERE rentLicencesDetailsID=2")) == new DateTime(2025, 3, 20),
            "fixture reproduces old wrong-month ID-only date recovery");
        Sql(@"UPDATE dbo.RentLicensesDetails SET rentLicPay_PaymentDate='20250201' WHERE rentLicencesDetailsID IN(7,35);
UPDATE dbo.RentLicensesDetails SET rentLicPay_VouNo=N'NEW-VOUCHER' WHERE rentLicencesDetailsID=14;
UPDATE dbo.RentLicensesDetails SET legacyPaymentID=1015 WHERE rentLicencesDetailsID=15;
UPDATE dbo.RentLicensesDetails SET legacyPaymentID=1016 WHERE rentLicencesDetailsID=503;
UPDATE dbo.RentLicensesDetails SET edited_Date=DATEADD(day,1,GETDATE()) WHERE rentLicencesDetailsID=17;
UPDATE dbo.RentLicensesDetails SET added_Date=DATEADD(day,1,GETDATE()) WHERE rentLicencesDetailsID=18;
UPDATE dbo.RentLicensesDetails SET rentLicPay_PaymentDate='20200620' WHERE rentLicencesDetailsID IN(19,20,21);
UPDATE dbo.RentLicensesDetails SET payment_added_Date='20260101' WHERE rentLicencesDetailsID=30;
UPDATE dbo.RentLicensesDetails SET payment_edited_Date='20260101' WHERE rentLicencesDetailsID=31;
UPDATE dbo.RentLicensesDetails SET payment_addedBy=4 WHERE rentLicencesDetailsID=32;
UPDATE dbo.RentLicensesDetails SET payment_editedBy=5 WHERE rentLicencesDetailsID=33;
UPDATE dbo.RentLicensesDetails SET rentLicPay_mandoubID=7 WHERE rentLicencesDetailsID=34;
UPDATE dbo.RentLicensesDetails SET edited_Date=(SELECT create_date FROM sys.objects WHERE object_id=OBJECT_ID('dbo.RentLicensesPaymentArchive_20261011')) WHERE rentLicencesDetailsID=42;
SELECT * INTO dbo.TestBeforeRecovery FROM dbo.RentLicensesDetails;");
    }

    private static string Rows(string table)
    {
        string key = table == Archive ? "rentLicensePaymentID" : "rentLicencesDetailsID";
        return Convert.ToString(Sql("SELECT * FROM " + table + " ORDER BY " + key + " FOR XML RAW"));
    }

    private static string DetailColumns()
    {
        return Convert.ToString(Sql(@"SELECT STUFF((SELECT ','+QUOTENAME(name) FROM sys.columns
WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') ORDER BY column_id FOR XML PATH('')),1,1,'')"));
    }

    private static void VerifyReports()
    {
        var reports = typeof(BarakeitAlkheirEntities).Assembly.GetType("BarakalAlkheir.Lic.LicenseRentalReports", true);
        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
        using (var db = new BarakeitAlkheirEntities(connectionString))
        {
            var totals = (Dictionary<int, decimal>)reports.GetMethod("PaidByInstalment", flags).Invoke(null, new object[] { db });
            foreach (var detail in db.RentLicensesDetails.ToList())
            {
                bool paid = detail.rd_isPaid == true;
                Check(totals.ContainsKey(detail.rentLicencesDetailsID) == paid, "report paid membership follows flag for " + detail.rentLicencesDetailsID);
                decimal expected = paid ? Convert.ToDecimal(detail.rd_amount.Value) : 0m;
                decimal actual = (decimal)reports.GetMethod("Paid", flags).Invoke(null, new object[] { totals, detail.rentLicencesDetailsID });
                Check(actual == expected, "report paid amount follows schedule for " + detail.rentLicencesDetailsID);
            }
            Check(totals.ContainsKey(25), "paid installment with no receipt remains paid in report");
            Check(!totals.ContainsKey(8), "unpaid installment with archived receipt remains unpaid in report");
        }
        var inPeriod = reports.GetMethod("InPeriod", flags);
        Check((bool)inPeriod.Invoke(null, new object[] { new DateTime(2025, 1, 10), true, new DateTime(2025, 1, 1), new DateTime(2025, 1, 31) }), "report day 10 bills January");
        Check((bool)inPeriod.Invoke(null, new object[] { new DateTime(2025, 1, 11), true, new DateTime(2025, 2, 1), new DateTime(2025, 2, 28) }), "report day 11 bills February");
        Check(!(bool)inPeriod.Invoke(null, new object[] { new DateTime(2025, 1, 11), true, new DateTime(2025, 1, 1), new DateTime(2025, 1, 31) }), "report excludes following billing month");
        Check((bool)inPeriod.Invoke(null, new object[] { new DateTime(2025, 12, 11), true, new DateTime(2026, 1, 1), new DateTime(2026, 1, 31) }), "report cutoff preserves billing year rollover");
    }

    private static void VerifyRecovery()
    {
        Fixture();
        string archive = Rows(Archive);
        VerifyReports();
        var migrator = Migrator();
        migrator.Update(Migration);
        Check(migrator.GetDatabaseMigrations().Contains(Migration), "recovery recorded in EF migration history");
        for (int id = 1; id <= Cases.Length; id++)
        {
            bool recovered = Recovered.Contains(id);
            int evidenceCount = Convert.ToInt32(Sql("SELECT COUNT(*) FROM " + Evidence + " WHERE rentLicencesDetailsID=@id", new SqlParameter("@id", id)));
            Check(evidenceCount == (recovered ? 1 : 0), Cases[id - 1]);
            if (recovered)
            {
                Check(Convert.ToInt32(Sql("SELECT legacyPaymentID FROM dbo.RentLicensesDetails WHERE rentLicencesDetailsID=@id", new SqlParameter("@id", id))) == 1000 + id,
                    Cases[id - 1] + ": correct receipt allocated");
                Check(Convert.ToInt32(Sql("SELECT RecoveryPaymentID FROM " + Evidence + " WHERE rentLicencesDetailsID=@id", new SqlParameter("@id", id))) == 1000 + id,
                    Cases[id - 1] + ": receipt identity recorded in evidence");
            }
            Console.WriteLine("PASS: " + Cases[id - 1]);
        }
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM " + Evidence)) == Recovered.Count, "only expected rows recovered");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM " + Evidence + " WHERE RecoveredAt IS NULL OR RecoveredAt>DATEADD(minute,1,GETUTCDATE())")) == 0, "recovery time recorded");
        Check(Rows(Archive) == archive, "every original archived receipt and column preserved");
        string columns = DetailColumns();
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM (SELECT " + OriginalColumns + ",RecoveryTestMarker FROM dbo.TestBeforeRecovery EXCEPT SELECT " + OriginalColumns + ",RecoveryTestMarker FROM dbo.RentLicensesDetails) differences")) == 0,
            "all schedule amounts, dates, ownership, audit, flags and custom values preserved");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.RentLicensesDetails")) == Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.TestBeforeRecovery")), "no installments added or removed");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM (SELECT " + columns + " FROM dbo.TestBeforeRecovery WHERE rentLicencesDetailsID IN(SELECT rentLicencesDetailsID FROM " + Evidence + ") EXCEPT SELECT " + columns + " FROM " + Evidence + ") differences")) == 0,
            "durable evidence holds each complete original row, including unknown custom columns");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM (SELECT " + columns + " FROM dbo.TestBeforeRecovery WHERE rentLicencesDetailsID NOT IN(SELECT rentLicencesDetailsID FROM " + Evidence + ") EXCEPT SELECT " + columns + " FROM dbo.RentLicensesDetails) differences")) == 0,
            "all skipped rows unchanged in every column");
        Check(Convert.ToInt32(Sql(@"SELECT COUNT(*) FROM (
SELECT e.rentLicencesDetailsID,p.rentLicensePaymentID,p.rentLicPay_VouNo,p.rentLicPay_PaymentDate,p.rentLicPay_mandoubID,p.added_Date,p.edited_Date,p.addedBy,p.editedBy
FROM dbo.LicenceReceiptRecovery_20261012 e JOIN dbo.RentLicensesPaymentArchive_20261011 p ON p.rentLicensePaymentID=e.RecoveryPaymentID
EXCEPT SELECT rentLicencesDetailsID,legacyPaymentID,rentLicPay_VouNo,rentLicPay_PaymentDate,rentLicPay_mandoubID,payment_added_Date,payment_edited_Date,payment_addedBy,payment_editedBy
FROM dbo.RentLicensesDetails) differences")) == 0, "recovered receipt metadata matches correct archive row exactly");
        VerifyReports();
        string details = Rows("dbo.RentLicensesDetails");
        string evidence = Rows(Evidence);
        migrator.Update(Migration);
        Check(Rows("dbo.RentLicensesDetails") == details && Rows(Evidence) == evidence && Rows(Archive) == archive,
            "repeat migration preserves details, original archive and recovery evidence");
        bool rejected = false;
        try { migrator.Update("AddLicenceAutomatedNumber"); } catch (NotSupportedException) { rejected = true; }
        Check(rejected, "automatic downgrade refuses to overwrite subsequent financial changes");
        Check(Rows("dbo.RentLicensesDetails") == details, "rejected downgrade preserves recovered rows");
    }

    private static void VerifyTriggerFailure()
    {
        Fixture();
        string details = Rows("dbo.RentLicensesDetails");
        string archive = Rows(Archive);
        // A database DDL trigger fails after the before-value table has been created,
        // proving transaction rollback beyond the migration's DML-trigger preflight guard.
        Sql(@"CREATE TRIGGER RejectRecoveryEvidenceAlter ON DATABASE FOR ALTER_TABLE AS
BEGIN
 IF EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]','nvarchar(128)')=N'LicenceReceiptRecovery_20261012'
 BEGIN RAISERROR('TEST: reject evidence primary key',16,1); ROLLBACK TRANSACTION; END;
END;");
        bool rejected = false;
        try { Migrator().Update(Migration); } catch (SqlException) { rejected = true; }
        Check(rejected, "failed evidence trigger rejects migration");
        Check(Rows("dbo.RentLicensesDetails") == details && Rows(Archive) == archive, "failed migration preserves every schedule and archive value");
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM sys.tables WHERE name='LicenceReceiptRecovery_20261012'")) == 0, "failed migration rolls back created recovery table");
        Check(Migrator().GetPendingMigrations().Contains(Migration), "failed migration remains pending");
        Sql("DROP TRIGGER RejectRecoveryEvidenceAlter ON DATABASE;");
        Sql(@"CREATE TRIGGER RejectRecoveryInstallmentUpdate ON dbo.RentLicensesDetails AFTER UPDATE AS
BEGIN RAISERROR('TEST: reject installment update',16,1); ROLLBACK TRANSACTION; END;");
        rejected = false;
        try { Migrator().Update(Migration); } catch (SqlException) { rejected = true; }
        Check(rejected, "enabled installment trigger rejected before writing");
        Check(Rows("dbo.RentLicensesDetails") == details && Rows(Archive) == archive, "trigger preflight preserves details and archive");
        Sql("DROP TRIGGER dbo.RejectRecoveryInstallmentUpdate;");
        Migrator().Update(Migration);
        Check(Convert.ToInt32(Sql("SELECT COUNT(*) FROM " + Evidence)) == Recovered.Count, "retry after trigger removal recovers expected rows once");
        Console.WriteLine("PASS: failed-trigger rollback, trigger preflight, and retry");
    }

    private static int Main(string[] args)
    {
        try
        {
            var entry = XDocument.Load(args[0]).Root.Element("connectionStrings").Elements("add")
                .Single(e => (string)e.Attribute("name") == "BarakeitAlkheirEntities");
            var master = new SqlConnectionStringBuilder((string)entry.Attribute("connectionString")) { InitialCatalog = "master" };
            foreach (Action run in new Action[] { VerifyRecovery, VerifyTriggerFailure })
            {
                string database = "BarakatReceiptRecoveryTests_" + Guid.NewGuid().ToString("N");
                connectionString = master.ConnectionString;
                Sql("CREATE DATABASE [" + database + "]");
                try
                {
                    connectionString = new SqlConnectionStringBuilder(master.ConnectionString) { InitialCatalog = database }.ConnectionString;
                    run();
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                    connectionString = master.ConnectionString;
                    Sql("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + database + "]");
                }
            }
            Console.WriteLine("PASS: " + checks + " SQL Server recovery/report assertions; disposable databases removed; customer catalogs unchanged.");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine("Receipt recovery verification failed: " + error.GetType().Name + " / " + error.Message + "\n" + error.StackTrace);
            return 1;
        }
    }
}
