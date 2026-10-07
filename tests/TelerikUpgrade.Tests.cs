using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BarakalAlkheir;
using Telerik.WinControls.UI;

internal static class TelerikUpgradeTests
{
    private static readonly Version ExpectedVersion = new Version(2026, 3, 812, 48);

    [STAThread]
    private static int Main()
    {
        try
        {
            AppDateFormat.ApplyToCurrentThread();
            Application.EnableVisualStyles();
            Type[] types = {
                typeof(frmMain), typeof(frmlogin),
                typeof(BarakalAlkheir.Definitions.frmBanks),
                typeof(BarakalAlkheir.Docs.frmTransactionDocumentsTasksSteps)
            };
            foreach (Type type in types)
            {
                // Do not show forms: their Load handlers access the database.
                using (Form form = (Form)Activator.CreateInstance(type))
                {
                    if (form.Controls.Count == 0) throw new Exception(type.Name + " has no controls.");
                    Console.WriteLine("PASS: constructed " + type.Name);
                }
            }

            using (var form = new BarakalAlkheir.Docs.frmTransactionDocumentsTasksSteps())
            {
                var grid = (RadGridView)form.GetType().GetField("GView",
                    BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
                if (grid.Columns.Count != 14) throw new Exception("Task grid columns changed.");
                // Render the actual grid independently, with synthetic data only.
                grid.Parent.Controls.Remove(grid);
                grid.Dock = DockStyle.None;
                grid.Size = new Size(1100, 260);
                grid.Rows.AddNew();
                grid.Rows[0].Cells[4].Value = "الإقامة";
                grid.Rows[0].Cells[5].Value = "TEST-001";
                grid.Rows[0].Cells[6].Value = "موظف تجريبي";
                grid.Rows[0].Cells[7].Value = "شركة تجريبية";
                grid.Rows[0].Cells[8].Value = null;
                grid.Rows[0].Cells[9].Value = null;
                grid.Rows[0].Cells[10].Value = "تجديد";
                grid.Rows[0].Cells[13].Value = 1;
                grid.Rows.AddNew();
                grid.Rows[1].Cells[4].Value = "جواز السفر";
                grid.Rows[1].Cells[5].Value = "TEST-002";
                grid.Rows[1].Cells[8].Value = "07/10/2026";
                grid.Rows[1].Cells[9].Value = 0;
                grid.CreateControl();
                using (var bitmap = new Bitmap(grid.Width, grid.Height))
                {
                    grid.DrawToBitmap(bitmap, new Rectangle(Point.Empty, grid.Size));
                    bitmap.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "telerik-task-grid.png"));
                }
                grid.Dispose();
                Console.WriteLine("PASS: task grid rendering with Arabic and empty expiry cells.");
            }

            int checkedAssemblies = 0;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyName name = assembly.GetName();
                if (name.Name.StartsWith("Telerik.WinControls") || name.Name == "TelerikCommon" || name.Name == "TelerikData")
                {
                    if (name.Version != ExpectedVersion)
                        throw new Exception("Unexpected Telerik assembly: " + name.FullName);
                    checkedAssemblies++;
                }
            }
            if (checkedAssemblies < 4) throw new Exception("Core Telerik assemblies were not exercised.");
            Console.WriteLine("PASS: " + checkedAssemblies + " loaded Telerik assemblies match " + ExpectedVersion + ".");
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception);
            return 1;
        }
    }
}
