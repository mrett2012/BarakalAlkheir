using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BarakalAlkheir
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDateFormat.ApplyToCurrentThread();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!PrepareDatabase()) return;
            Application.Run(new frmMain());
        }

        private static bool PrepareDatabase()
        {
            Exception failure = null;
            using (var status = new Form
            {
                Text = "بركات الخير",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ClientSize = new System.Drawing.Size(420, 100),
                ControlBox = false,
                RightToLeft = RightToLeft.Yes,
                RightToLeftLayout = true
            })
            {
                status.Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = "جارٍ تجهيز قاعدة البيانات، يرجى الانتظار..."
                });
                status.Controls.Add(new ProgressBar { Dock = DockStyle.Bottom, Style = ProgressBarStyle.Marquee });
                status.Shown += async (sender, e) =>
                {
                    try
                    {
                        await System.Threading.Tasks.Task.Run(() => Model.Migrations.DatabaseMigrationRunner.ApplyPending());
                    }
                    catch (Exception error) { failure = error; }
                    finally { status.Close(); }
                };
                status.ShowDialog();
            }
            if (failure == null) return true;
            MessageBox.Show("تعذر تجهيز قاعدة البيانات. لم يتم فتح البرنامج.\r\n" +
                "تحقق من الاتصال وصلاحيات تحديث قاعدة البيانات ثم أعد المحاولة.\r\n\r\n" +
                failure.GetBaseException().Message,
                "خطأ في قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}
