using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.UI;

internal static class DocumentListLoadingTests
{
    [STAThread]
    private static int Main()
    {
        try
        {
            Control.CheckForIllegalCrossThreadCalls = true;
            Assembly app = typeof(BarakalAlkheir.frmMain).Assembly;
            Type lookup = app.GetType("BarakalAlkheir.Docs.DocumentListLookup", true);
            MethodInfo batch = lookup.GetMethod("Batches", BindingFlags.Static | BindingFlags.NonPublic);
            int[] ids = Enumerable.Range(1, 3001).Concat(new[] { 1, 2, -1 }).ToArray();
            var groups = ((IEnumerable<int[]>)batch.Invoke(null, new object[] { ids })).ToList();
            if (groups.Any(group => group.Length >= 2100)) throw new Exception("Lookup batch risks SQL Server's parameter limit.");
            if (!groups.SelectMany(group => group).OrderBy(id => id).SequenceEqual(Enumerable.Range(1, 3001)))
                throw new Exception("Batching lost, repeated, or added document IDs.");

            string[] names = { "frmEmployeeExpiryDocuments", "frmCompanyLicensesExpiryDocuments", "frmCarsExpiryDocuments",
                "frmTransactionDocumentsTasks", "frmTransactionDocumentsTasksNotPaid", "frmTransactionDocumentsTasksSteps" };
            foreach (string name in names)
            {
                using (Form form = (Form)Activator.CreateInstance(app.GetType("BarakalAlkheir.Docs." + name, true)))
                {
                    // Do not show the form or invoke its database-backed loading callback.
                    var grid = (RadGridView)form.GetType().GetField("GView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
                    grid.BeginUpdate();
                    try
                    {
                        for (int index = 0; index < 3; index++)
                        {
                            grid.Rows.AddNew();
                            grid.Rows[grid.Rows.Count - 1].Cells[0].Value = index;
                            // Telerik does not assign CurrentRow until EndUpdate. Style the added row directly.
                            grid.Rows[grid.Rows.Count - 1].Tag = true;
                        }
                    }
                    finally { grid.EndUpdate(); }
                    if (grid.Rows.Count != 3) throw new Exception(name + " lost grid rows.");
                    for (int index = 0; index < grid.Rows.Count; index++)
                        if (!object.Equals(grid.Rows[index].Tag, true) || Convert.ToInt32(grid.Rows[index].Cells[0].Value) != index)
                            throw new Exception(name + " styled or populated the wrong row.");
                }
            }
            string[] permissionNames = { "add", "edit", "delete", "print", "view", "search" };
            foreach (string permission in permissionNames)
            {
                using (var toolbar = new BarakalAlkheir.components.banner("test"))
                {
                    Type type = toolbar.GetType();
                    type.GetMethod("DisableAllSecurity", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(toolbar, null);
                    type.GetMethod("ApplySecurityEvents", BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(toolbar, new object[] { new List<string> { permission } });
                    if (toolbar.btnAdd.Visible != (permission == "add") ||
                        toolbar.btnEdit.Visible != (permission == "edit") ||
                        toolbar.btnDelete.Visible != (permission == "delete") ||
                        toolbar.btnPrint.Visible != (permission == "print") ||
                        toolbar.btnSave.Visible != (permission == "add" || permission == "edit") ||
                        toolbar.btnCancel.Visible != toolbar.btnSave.Visible ||
                        toolbar.btnFind.Visible != (permission != "add" && permission != "edit") ||
                        !toolbar.btnFirst.Visible || !toolbar.btnLast.Visible ||
                        !toolbar.btnNext.Visible || !toolbar.btnPrevious.Visible)
                        throw new Exception("Toolbar permission mapping changed: " + permission);
                }
            }
            Console.WriteLine("PASS: large-ID lookup batching, six document grids, and six toolbar permissions; no database access.");
            return 0;
        }
        catch (Exception exception) { Console.Error.WriteLine(exception); return 1; }
    }
}
