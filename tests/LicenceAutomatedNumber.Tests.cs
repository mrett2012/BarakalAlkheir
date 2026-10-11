using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BarakalAlkheir.Model;
using BarakalAlkheir.Sponsor;
using Telerik.WinControls.UI;

internal static class LicenceAutomatedNumberTests
{
    private static int checks;
    private static readonly BindingFlags Private = BindingFlags.Instance | BindingFlags.NonPublic;
    private static object Field(object target, string name) { return target.GetType().GetField(name, Private).GetValue(target); }
    private static void Check(bool condition, string message) { checks++; if (!condition) throw new Exception(message); }

    private static void Render(Form form, string loadMethod)
    {
        // Disable the database-loading handler. Render real controls using synthetic values only.
        form.Load -= (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), form, form.GetType().GetMethod(loadMethod, Private));
        form.ShowInTaskbar = false;
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(-32000, -32000);
        form.Show();
        if (form is frmCompanyLicences)
        {
            var pages = (RadPageView)Field(form, "radPageView1");
            pages.SelectedPage = (RadPageViewPage)Field(form, "pageLicences");
            pages.SelectedPage.Visible = true;
        }
        Application.DoEvents();
        var number = (RadTextBox)Field(form, "txtlicenseAutomatedNumber");
        var label = (RadLabel)Field(form, "lbllicenseAutomatedNumber");
        Check(number.Visible && label.Visible, form.Name + " automated number visible");
        Check(number.MaxLength == 50 && label.Text == "الرقم الآلي", form.Name + " Arabic label and length");
        Check(number.Parent.ClientRectangle.Contains(number.Bounds), form.Name + " field inside parent");
        Check(label.Parent.ClientRectangle.Contains(label.Bounds), form.Name + " label inside parent");
        foreach (Control sibling in number.Parent.Controls)
            if (sibling != number && sibling != label && sibling.Visible)
                Check(!number.Bounds.IntersectsWith(sibling.Bounds) && !label.Bounds.IntersectsWith(sibling.Bounds), form.Name + " field does not overlap " + sibling.Name);
        using (var bitmap = new Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size));
            bitmap.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, form.Name + "-automated-number.png"));
        }
        form.Hide();
    }

    private static void Search(Form form)
    {
        var number = (RadTextBox)Field(form, "txtlicenseAutomatedNumber");
        var grid = (RadGridView)Field(form, "GView");
        number.Text = "001234"; // Input before asynchronous data is available must be safe.
        var rows = new List<CompanyLicence> {
            new CompanyLicence { companyLicencesID=11, licenseNo="LIC-11", licenseAutomatedNumber="001234", licenseLinkCode="LINK-11", commercialName="شركة تجريبية" },
            new CompanyLicence { companyLicencesID=12, licenseNo="LIC-12", licenseAutomatedNumber=null },
            new CompanyLicence { companyLicencesID=13, licenseNo="LIC-13", licenseAutomatedNumber="009876" }
        };
        form.GetType().GetField("lstCompanyLicense", Private).SetValue(form, rows);
        form.GetType().GetMethod("bindData", Private).Invoke(form, null);
        number.Text = " 012 ";
        Check(grid.RowCount == 1, form.Name + " partial search trims input and handles nulls");
        Check(Convert.ToString(grid.Rows[0].Cells["gvlicenseAutomatedNumber"].Value) == "001234", form.Name + " result displays leading zeros");
        number.Text = "missing";
        Check(grid.RowCount == 0, form.Name + " no matches");
        form.GetType().GetMethod("btnOK_Click", Private).Invoke(form, new object[] { null, EventArgs.Empty });
        Check(!(bool)form.GetType().GetField("found").GetValue(form), form.Name + " empty result cannot select");
        number.Text = "";
        Check(grid.RowCount == 3, form.Name + " clearing search restores eligible list");
        number.Text = "001234";
        grid.CurrentRow = grid.Rows[0];
        form.GetType().GetMethod("btnOK_Click", Private).Invoke(form, new object[] { null, EventArgs.Empty });
        Check((int)form.GetType().GetField("licID").GetValue(form) == 11, form.Name + " selected licence ID preserved");
        Check((string)form.GetType().GetField("licNO").GetValue(form) == "LIC-11", form.Name + " licence number remains distinct from automated number");
    }

    [STAThread]
    private static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            using (var form = new frmCompanyLicences())
            {
                var number = (RadTextBox)Field(form, "txtlicenseAutomatedNumber");
                Check(number.ReadOnly, "master field starts read-only");
                form.GetType().GetMethod("enableControls", Private).Invoke(form, null);
                Check(!number.ReadOnly, "master field enabled for add/edit");
                number.Text = "001234";
                form.GetType().GetMethod("disableControls", Private).Invoke(form, null);
                Check(number.ReadOnly, "master field read-only outside add/edit");
                var banner = (BarakalAlkheir.components.banner)Field(form, "mainbar");
                banner.Dock = DockStyle.Top;
                ((Control)Field(form, "pageLicences")).Controls.Add(banner);
                Render(form, "frmCompanyLicences_Load");
            }
            using (var form = new frmfindLicences(false, true))
            {
                Render(form, "frmfindLicences_Load");
                Search(form);
            }
            using (var form = new frmfindLicences1(true, true))
            {
                Render(form, "frmfindLicences_Load");
                Search(form);
            }
            using (var form = new BarakalAlkheir.Lic.frmfindLicencesPayment())
                Render(form, "frmfindLicences_Load");
            Console.WriteLine("PASS: " + checks + " licence automated-number control, search, selection and layout checks; synthetic data, no database access.");
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
    }
}
