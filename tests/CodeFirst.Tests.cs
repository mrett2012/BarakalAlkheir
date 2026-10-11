using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using BarakalAlkheir.Model;

internal static class CodeFirstTests
{
    private static readonly string[] PaymentColumns = { "rentLicPay_VouNo", "rentLicPay_PaymentDate", "rentLicPay_mandoubID", "payment_added_Date", "payment_edited_Date", "payment_addedBy", "payment_editedBy", "legacyPaymentID" };
    private static int assertions;
    private static void Check(bool condition, string message)
    {
        assertions++;
        if (!condition) throw new InvalidOperationException(message);
    }

    private static Dictionary<string, XElement> Tables(XDocument model)
    {
        var storage = model.Descendants().Single(e => e.Name.LocalName == "StorageModels").Elements().Single();
        var types = storage.Elements().Where(e => e.Name.LocalName == "EntityType").ToDictionary(e => (string)e.Attribute("Name"));
        return storage.Elements().Single(e => e.Name.LocalName == "EntityContainer").Elements()
            .Where(e => e.Name.LocalName == "EntitySet").ToDictionary(
                e => ((string)e.Attribute("Schema") ?? "dbo") + "." + ((string)e.Attribute("Table") ?? (string)e.Attribute("Name")),
                e => types[((string)e.Attribute("EntityType")).Split('.').Last()]);
    }

    private static string Attribute(XElement element, string name)
    {
        var attr = element.Attributes().SingleOrDefault(a => a.Name.LocalName == name);
        return attr == null ? null : attr.Value;
    }

    private static void Column(XElement expected, XElement actual, string table)
    {
        string name = Attribute(expected, "Name");
        string expectedType = Attribute(expected, "Type");
        string actualType = Attribute(actual, "Type");
        Check(expectedType.Replace("(max)", "") == actualType.Replace("(max)", ""), table + "." + name + " SQL type");
        Check((Attribute(expected, "Nullable") ?? "true") == (Attribute(actual, "Nullable") ?? "true"), table + "." + name + " nullability");
        Check((Attribute(expected, "StoreGeneratedPattern") ?? "None") == (Attribute(actual, "StoreGeneratedPattern") ?? "None"), table + "." + name + " identity");
        foreach (string facet in new[] { "MaxLength", "Precision", "Scale" })
        {
            string value = Attribute(expected, facet);
            if (value != null) Check(value == Attribute(actual, facet), table + "." + name + " " + facet);
        }
        if (expectedType.EndsWith("(max)", StringComparison.Ordinal))
            Check(actualType.EndsWith("(max)", StringComparison.Ordinal) || Attribute(actual, "MaxLength") == "Max", table + "." + name + " unbounded binary");
    }

    private static string[] Relationships(XDocument model)
    {
        var schema = model.Descendants().Single(e => e.Name.LocalName == "StorageModels").Elements().Single();
        var tableNames = Tables(model).ToDictionary(p => Attribute(p.Value, "Name"), p => p.Key);
        return schema.Elements().Where(e => e.Name.LocalName == "Association").Select(association => {
            var constraint = association.Elements().Single(e => e.Name.LocalName == "ReferentialConstraint");
            var ends = association.Elements().Where(e => e.Name.LocalName == "End").ToDictionary(e => Attribute(e, "Role"));
            var principal = constraint.Elements().Single(e => e.Name.LocalName == "Principal");
            var dependent = constraint.Elements().Single(e => e.Name.LocalName == "Dependent");
            var parent = ends[Attribute(principal, "Role")];
            var member = ends[Attribute(dependent, "Role")];
            Func<XElement, string> keys = e => string.Join(",", e.Elements().Select(p => Attribute(p, "Name")));
            var deletion = parent.Elements().SingleOrDefault(e => e.Name.LocalName == "OnDelete");
            return tableNames[Attribute(parent, "Type").Split('.').Last()] + "(" + keys(principal) + ")/" + Attribute(parent, "Multiplicity") + ":" +
                tableNames[Attribute(member, "Type").Split('.').Last()] + "(" + keys(dependent) + ")/" + Attribute(member, "Multiplicity") + ":" +
                (deletion == null ? "None" : Attribute(deletion, "Action"));
        }).OrderBy(s => s).ToArray();
    }

    private sealed class ReadOnlyCommands : DbCommandInterceptor
    {
        private static void RequireSelect(DbCommand command)
        {
            if (!command.CommandText.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Read-only check rejected a non-SELECT command.");
        }
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> context)
        { throw new InvalidOperationException("Read-only check rejected ExecuteNonQuery."); }
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> context)
        { RequireSelect(command); }
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> context)
        { RequireSelect(command); }
    }

    private static void ExistingDatabase(XDocument model, string configPath)
    {
        var configured = XDocument.Load(configPath).Root.Element("connectionStrings").Elements("add")
            .Single(e => (string)e.Attribute("name") == "BarakeitAlkheirEntities");
        Check((string)configured.Attribute("providerName") == "System.Data.SqlClient", "SQL Client configuration");
        string connectionString = (string)configured.Attribute("connectionString");
        var target = new SqlConnectionStringBuilder(connectionString);
        Check(target.InitialCatalog == "BarakeitAlkheir2", "expected read-only catalog");
        var expected = Tables(model);
        var actual = new Dictionary<string, Dictionary<string, string[]>>();
        using (var connection = new SqlConnection(connectionString))
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"SELECT s.name,t.name,c.name,ty.name,c.max_length,c.is_nullable,c.is_identity
FROM sys.tables t JOIN sys.schemas s ON s.schema_id=t.schema_id
JOIN sys.columns c ON c.object_id=t.object_id JOIN sys.types ty ON ty.user_type_id=c.system_type_id;";
            connection.Open();
            using (var reader = command.ExecuteReader()) while (reader.Read())
            {
                string table = reader.GetString(0) + "." + reader.GetString(1);
                if (!actual.ContainsKey(table)) actual[table] = new Dictionary<string, string[]>();
                actual[table][reader.GetString(2)] = new[] { reader.GetString(3), reader.GetInt16(4).ToString(), reader.GetBoolean(5).ToString(), reader.GetBoolean(6).ToString() };
            }
        }
        foreach (var table in expected)
        {
            Check(actual.ContainsKey(table.Key), "existing table " + table.Key);
            foreach (var column in table.Value.Elements().Where(e => e.Name.LocalName == "Property"))
            {
                string name = Attribute(column, "Name");
                Check(actual[table.Key].ContainsKey(name), "existing column " + table.Key + "." + name);
                var value = actual[table.Key][name];
                string type = Attribute(column, "Type");
                Check(value[0] == type.Replace("(max)", ""), "existing SQL type " + table.Key + "." + name);
                Check(bool.Parse(value[2]) == ((Attribute(column, "Nullable") ?? "true") == "true"), "existing nullability " + table.Key + "." + name);
                Check(bool.Parse(value[3]) == (Attribute(column, "StoreGeneratedPattern") == "Identity"), "existing identity " + table.Key + "." + name);
                string length = Attribute(column, "MaxLength");
                if (length != null)
                    Check(int.Parse(value[1]) == int.Parse(length) * (type.StartsWith("n", StringComparison.Ordinal) ? 2 : 1), "existing length " + table.Key + "." + name);
                if (type.EndsWith("(max)", StringComparison.Ordinal)) Check(value[1] == "-1", "existing unbounded binary " + table.Key + "." + name);
            }
        }
        var readOnly = new ReadOnlyCommands();
        DbInterception.Add(readOnly);
        try
        {
            using (var db = new BarakeitAlkheirEntities(connectionString))
            {
                Check(db.RentLicenses.Any(r => r.rentLicenseID == 266), "existing rental query");
                Check(db.RentLicensesDetails.Count(d => d.rentLicenseID == 266) == 91, "existing installment query");
                Check(db.Database.SqlQuery<int>("SELECT COUNT(*) FROM dbo.RentLicensesPaymentArchive_20261011 WHERE rentLicensePaymentID=11158").Single() == 1, "historical receipt retained even if archive-only");
                Check(db.RentLicensesDetails.Any(p => p.legacyPaymentID != null && p.rd_isPaid == true), "unambiguous merged receipt query");
                var receipt = db.RentLicensesDetails.Where(p => p.legacyPaymentID != null && p.rd_isPaid == true).OrderBy(p => p.rentLicencesDetailsID).First();
                Check(receipt.RentLicens != null && receipt.RentLicens.rentLicenseID == receipt.rentLicenseID, "lazy loading follows the existing receipt foreign key");
                Check(!db.RentLicensesDetails.Any(p => p.legacyPaymentID != null && p.rd_isPaid != true), "unpaid installments do not receive archived receipt metadata");
                Check(db.DocumentTransactionTasks.Select(d => DbFunctions.AddDays(d.documentTransTask_date, 1)).Take(1).ToList().Count <= 1, "EF6 date function translates");
            }
        }
        finally { DbInterception.Remove(readOnly); }
        Console.WriteLine("PASS: existing BarakeitAlkheir2 schema and EF6 SELECT queries; all writes blocked by interceptor.");
    }

    private static int Main(string[] args)
    {
        try
        {
            DbModelBuilder builder;
            // Explicit provider metadata builds the model without contacting SQL Server.
            using (var context = new BarakeitAlkheirEntities("Data Source=unused;Initial Catalog=ModelOnly;Integrated Security=True"))
            {
                builder = new DbModelBuilder();
                typeof(BarakeitAlkheirEntities).GetMethod("OnModelCreating", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(context, new object[] { builder });
                Check(context.Configuration.LazyLoadingEnabled, "lazy loading preserved");
                Check(context.Configuration.ProxyCreationEnabled, "navigation proxies enabled");
                Check(!context.Configuration.ValidateOnSaveEnabled, "legacy validation behavior preserved");
                Check(context.Database.Connection.State == System.Data.ConnectionState.Closed, "constructor does not open the connection");
            }
            var model = builder.Build(new DbProviderInfo("System.Data.SqlClient", "2008"));
            var buffer = new StringWriter();
            using (var writer = XmlWriter.Create(buffer)) EdmxWriter.WriteEdmx(model, writer);
            var actual = XDocument.Parse(buffer.ToString());
            var expected = XDocument.Load(args[0]);
            var before = Tables(expected);
            var after = Tables(actual);
            Check(before.Count == 63 && after.Count == 62, "62 tables after payment consolidation");
            var originalTables = before;
            before = before.Where(t => t.Key != "dbo.RentLicensesPayment").ToDictionary(t => t.Key, t => t.Value);
            Check(before.Keys.OrderBy(s => s).SequenceEqual(after.Keys.OrderBy(s => s)), "table/schema names preserved");
            int columns = 0;
            foreach (var table in before)
            {
                var previous = table.Value.Elements().Where(e => e.Name.LocalName == "Property").ToDictionary(e => Attribute(e, "Name"));
                var current = after[table.Key].Elements().Where(e => e.Name.LocalName == "Property").ToDictionary(e => Attribute(e, "Name"));
                Check(previous.Keys.OrderBy(s => s).SequenceEqual(current.Keys.Where(n => table.Key != "dbo.RentLicensesDetails" || !PaymentColumns.Contains(n)).OrderBy(s => s)), table.Key + " columns preserved");
                foreach (var column in previous) { Column(column.Value, current[column.Key], table.Key); columns++; }
                Func<XElement, string[]> keys = e => e.Elements().Single(p => p.Name.LocalName == "Key").Elements().Select(p => Attribute(p, "Name")).ToArray();
                Check(keys(table.Value).SequenceEqual(keys(after[table.Key])), table.Key + " primary key preserved");
            }
            Check(Relationships(expected).Where(r => !r.Contains(":dbo.RentLicensesPayment(")).SequenceEqual(Relationships(actual)), "all relationship keys, multiplicities and delete rules preserved");
            Check(typeof(BarakeitAlkheirEntities).GetProperties().Count(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)) == 62, "all context sets registered");
            Check(typeof(RentLicensesDetail).BaseType == typeof(object), "POCO entities replace EntityObject");
            Check(!typeof(BarakeitAlkheirEntities).Assembly.GetManifestResourceNames().Any(s => s.EndsWith(".csdl") || s.EndsWith(".ssdl") || s.EndsWith(".msl")), "no embedded database-first model");
            Check(!new BarakalAlkheir.Model.Migrations.Configuration().AutomaticMigrationsEnabled, "automatic migrations disabled");
            var baseline = new BarakalAlkheir.Model.Migrations.ExistingDatabaseBaseline();
            var snapshot = (System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata)baseline;
            Check(!string.IsNullOrEmpty(snapshot.Target), "migration model snapshot embedded");
            using (var compressed = new MemoryStream(Convert.FromBase64String(snapshot.Target)))
            using (var unzip = new GZipStream(compressed, CompressionMode.Decompress))
            {
                var baselineModel = XDocument.Load(unzip);
                var baselineTables = Tables(baselineModel);
                Check(baselineTables.Keys.OrderBy(s => s).SequenceEqual(originalTables.Keys.OrderBy(s => s)), "baseline preserves the original tables");
                Check(Relationships(baselineModel).SequenceEqual(Relationships(expected)), "baseline preserves original relationship snapshot");
            }
            var merged = new BarakalAlkheir.Model.Migrations.MergeLicencePaymentsIntoDetails();
            var mergedMetadata = (System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata)merged;
            using (var bytes = new MemoryStream(Convert.FromBase64String(mergedMetadata.Target)))
            using (var unzip = new GZipStream(bytes, CompressionMode.Decompress))
            {
                var migrated = XDocument.Load(unzip);
                Check(Tables(migrated).Keys.OrderBy(n => n).SequenceEqual(after.Keys.OrderBy(n => n)), "merge snapshot matches current tables");
                Check(Relationships(migrated).SequenceEqual(Relationships(actual)), "merge snapshot relationships match current model");
                var paymentColumns = after["dbo.RentLicensesDetails"].Elements().Where(e => e.Name.LocalName == "Property" && PaymentColumns.Contains(Attribute(e, "Name"))).ToArray();
                Check(paymentColumns.Length == PaymentColumns.Length, "all eight receipt metadata columns mapped");
                foreach (var column in paymentColumns) Column(Tables(migrated)["dbo.RentLicensesDetails"].Elements().Single(e => e.Name.LocalName == "Property" && Attribute(e,"Name") == Attribute(column,"Name")), column, "dbo.RentLicensesDetails");
            }
            baseline.Up();
            var operations = (System.Collections.IEnumerable)typeof(System.Data.Entity.Migrations.DbMigration)
                .GetProperty("Operations", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(baseline, null);
            Check(!operations.Cast<object>().Any(), "empty baseline does not modify business tables");
            Console.WriteLine("PASS: " + assertions + " mapping checks; 62 tables, " + (columns + PaymentColumns.Length) + " columns, 61 relationships; no database access.");
            if (args.Length > 1) ExistingDatabase(actual, args[1]);
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
    }
}
