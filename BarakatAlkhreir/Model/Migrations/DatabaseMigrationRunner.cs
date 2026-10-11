using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace BarakalAlkheir.Model.Migrations
{
    public static class DatabaseMigrationRunner
    {
        public static void ApplyPending(string connectionString = null)
        {
            using (var db = connectionString == null ? new BarakeitAlkheirEntities() : new BarakeitAlkheirEntities(connectionString))
            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                // Opening the configured database first prevents an empty database being created
                // by the adoption baseline, which intentionally contains no CreateTable calls.
                string targetConnectionString = connection.ConnectionString;
                connection.Open();
                bool locked = false;
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        // Separate from LicenceRentals: each EF migration acquires that lock
                        // on its own connection. This session lock serializes client startups.
                        command.CommandTimeout = 130;
                        command.CommandText = @"DECLARE @result int;
EXEC @result=sys.sp_getapplock @Resource=N'BarakalAlkheir.SchemaMigrations',
 @LockMode='Exclusive',@LockOwner='Session',@LockTimeout=120000;
SELECT @result;";
                        if (Convert.ToInt32(command.ExecuteScalar()) < 0)
                            throw new InvalidOperationException("تعذر انتظار تحديث قاعدة البيانات من جهاز آخر. يرجى إعادة المحاولة.");
                        locked = true;
                        command.CommandText = @"SELECT CASE WHEN OBJECT_ID('dbo.RentLicensesDetails','U') IS NOT NULL
 AND OBJECT_ID('dbo.CompanyLicences','U') IS NOT NULL AND OBJECT_ID('dbo.Users','U') IS NOT NULL
 THEN 1 ELSE 0 END;";
                        if (Convert.ToInt32(command.ExecuteScalar()) != 1)
                            throw new InvalidOperationException("قاعدة البيانات المحددة ليست قاعدة بركات الخير مهيأة. تحقق من إعدادات الاتصال أو استعد نسخة احتياطية قبل التشغيل.");
                    }

                    var configuration = new Configuration
                    {
                        TargetDatabase = new DbConnectionInfo(targetConnectionString, "System.Data.SqlClient"),
                        CommandTimeout = 300
                    };
                    var migrator = new DbMigrator(configuration);
                    var local = migrator.GetLocalMigrations().ToArray();
                    if (migrator.GetDatabaseMigrations().Except(local).Any())
                        throw new InvalidOperationException("قاعدة البيانات تستخدم إصداراً أحدث. يرجى تثبيت أحدث إصدار من البرنامج.");
                    if (migrator.GetPendingMigrations().Any())
                        migrator.Update();

                    // History alone cannot prove that a manually modified database is usable.
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"SELECT COUNT(*) FROM sys.columns
WHERE object_id=OBJECT_ID('dbo.RentLicensesDetails') AND name IN
 ('rentLicPay_VouNo','rentLicPay_PaymentDate','rentLicPay_mandoubID','payment_added_Date',
  'payment_edited_Date','payment_addedBy','payment_editedBy','legacyPaymentID');";
                        int paymentColumnCount = Convert.ToInt32(command.ExecuteScalar());
                        command.CommandText = "SELECT COL_LENGTH('dbo.CompanyLicences','licenseAutomatedNumber');";
                        object automatedNumberLength = command.ExecuteScalar();
                        if (paymentColumnCount != 8 || automatedNumberLength == DBNull.Value || Convert.ToInt32(automatedNumberLength) != 100)
                            throw new InvalidOperationException("سجل تحديث قاعدة البيانات لا يطابق الأعمدة الموجودة. يلزم إصلاح قاعدة البيانات قبل التشغيل.");
                    }
                }
                finally
                {
                    if (locked && connection.State == ConnectionState.Open)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"EXEC sys.sp_releaseapplock
 @Resource=N'BarakalAlkheir.SchemaMigrations',@LockOwner='Session';";
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
