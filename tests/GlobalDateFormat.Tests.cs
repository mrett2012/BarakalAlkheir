using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BarakalAlkheir;
using Telerik.WinControls.UI;

internal static class GlobalDateFormatTests
{
    private static int assertions;
    private static int pickerCases;
    private static int gridCases;
    private static int maskedDateCases;
    private static readonly DateTime[] Dates = {
        new DateTime(2026, 4, 5), new DateTime(2026, 9, 16), new DateTime(2024, 2, 29)
    };
    private static readonly string[] ExpectedDates = { "05/04/2026", "16/09/2026", "29/02/2024" };

    private static void Check(bool condition, string description)
    {
        assertions++;
        if (!condition) throw new Exception("FAIL: " + description);
    }

    private static void CultureRules(string name)
    {
        // Simulate conflicting Windows regional overrides, including a non-Gregorian calendar.
        var machineCulture = new CultureInfo(name, false);
        machineCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
        machineCulture.DateTimeFormat.DateSeparator = "-";
        machineCulture.NumberFormat.NumberDecimalSeparator = ",";
        machineCulture.NumberFormat.NumberGroupSeparator = "_";
        var uiCulture = CultureInfo.GetCultureInfo(name);
        Thread.CurrentThread.CurrentCulture = machineCulture;
        Thread.CurrentThread.CurrentUICulture = uiCulture;
        string numberBefore = 12345.67m.ToString("N2");

        // Explicit date formatting must also work before startup configures the UI thread.
        for (int i = 0; i < Dates.Length; i++)
            Check(AppDateFormat.Format(Dates[i]) == ExpectedDates[i], "explicit date before startup " + name);

        AppDateFormat.ApplyToCurrentThread();
        CultureInfo current = Thread.CurrentThread.CurrentCulture;
        Check(current.Name == machineCulture.Name, "regional culture name preserved " + name);
        Check(Object.ReferenceEquals(Thread.CurrentThread.CurrentUICulture, uiCulture), "UI language preserved " + name);
        Check(current.NumberFormat.NumberDecimalSeparator == ",", "decimal separator preserved " + name);
        Check(current.NumberFormat.NumberGroupSeparator == "_", "group separator preserved " + name);
        Check(12345.67m.ToString("N2") == numberBefore, "numeric display preserved " + name);
        Check(decimal.Parse("12345,67") == 12345.67m, "numeric parsing preserved " + name);
        Check(machineCulture.DateTimeFormat.ShortDatePattern == "yyyy-MM-dd", "original culture not mutated " + name);
        Check(machineCulture.DateTimeFormat.DateSeparator == "-", "original separator not mutated " + name);
        Check(current.DateTimeFormat.Calendar is GregorianCalendar, "Gregorian application calendar " + name);

        for (int i = 0; i < Dates.Length; i++)
        {
            Check(Dates[i].ToShortDateString() == ExpectedDates[i], "standard short date " + name);
            Check(Dates[i].ToString("d") == ExpectedDates[i], "standard date format token " + name);
            Check(DateTime.Parse(ExpectedDates[i]) == Dates[i], "day/month parsing " + name);
        }
        Check(DateTime.Parse("31/12/2026") == new DateTime(2026, 12, 31), "end-year parsing " + name);
        DateTime storedDate;
        foreach (string legacy in new[] { "2026 / 04 / 05", "2026/04/05", "05/04/2026" })
        {
            Check(AppDateFormat.TryParseStoredDate(legacy, out storedDate) && storedDate == Dates[0], "legacy stored date parsing " + name);
            Check(AppDateFormat.FormatStoredDate(legacy) == ExpectedDates[0], "legacy stored date display " + name);
        }
        Check(!AppDateFormat.TryParseStoredDate("31/02/2026", out storedDate), "impossible stored date rejected " + name);
        Check(AppDateFormat.FormatStoredDate("unrecognized date") == "unrecognized date", "unknown stored text preserved " + name);
        Check(AppDateFormat.FormatStoredDate(null) == null, "null stored text preserved " + name);
        Check(AppDateFormat.FormatStoredDate("") == "", "empty stored text preserved " + name);
        AppDateFormat.ApplyToCurrentThread();
        Check(Dates[0].ToShortDateString() == ExpectedDates[0], "reapplying startup setting is stable " + name);
        Check(12345.67m.ToString("N2") == numberBefore, "numeric display after reapplying " + name);
    }

    private static void DateControls(Form form, string cultureName)
    {
        // Constructors only initialize controls. Never show a form or run its database-backed Load event.
        foreach (FieldInfo field in form.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            var masked = field.GetValue(form) as RadMaskedEditBox;
            if (masked != null && masked.Mask == "00/00/0000")
            {
                IntPtr handle = masked.Handle;
                for (int i = 0; i < Dates.Length; i++)
                {
                    masked.Text = ExpectedDates[i];
                    string maskLabel = form.GetType().FullName + "." + field.Name + " " + cultureName;
                    Check(masked.Text == ExpectedDates[i], "masked date retains slash text " + maskLabel);
                    Check(DateTime.Parse(masked.Text) == Dates[i], "masked date parsing " + maskLabel);
                    maskedDateCases++;
                }
            }
            var native = field.GetValue(form) as DateTimePicker;
            var telerik = field.GetValue(form) as RadDateTimePicker;
            if (native == null && telerik == null) continue;
            string label = form.GetType().FullName + "." + field.Name + " " + cultureName;
            string format = native != null ? native.CustomFormat : telerik.CustomFormat;
            bool periodOnly = !String.IsNullOrEmpty(format) && format.IndexOf('d') < 0;
            DateTimePickerFormat mode = native != null ? native.Format : telerik.Format;
            Check(mode == DateTimePickerFormat.Custom, "explicit picker format " + label);
            if (!periodOnly)
                Check(format == AppDateFormat.PickerFormat, "full-date picker uses global setting " + label);

            for (int i = 0; i < Dates.Length; i++)
            {
                string actual;
                DateTime value;
                if (native != null)
                {
                    IntPtr handle = native.Handle;
                    native.Value = Dates[i];
                    actual = native.Text;
                    value = native.Value;
                }
                else
                {
                    IntPtr handle = telerik.Handle;
                    telerik.Value = Dates[i];
                    actual = telerik.Text;
                    value = telerik.Value;
                }
                // Existing month/year filters retain their precision while using the Gregorian calendar.
                string expected = periodOnly ? Dates[i].ToString(format, CultureInfo.InvariantCulture) : ExpectedDates[i];
                // Native month names follow Windows UI localization; date calculations read Value.
                if (native == null || !periodOnly || format.IndexOf("MMM", StringComparison.Ordinal) < 0)
                    Check(actual == expected, "picker text " + label + " expected " + expected + " got " + actual);
                else
                    Check(!String.IsNullOrEmpty(actual), "native month filter has text " + label);
                Check(value.Date == Dates[i], "picker retains date value " + label);
                pickerCases++;
            }
        }

        foreach (FieldInfo field in form.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            var grid = field.GetValue(form) as RadGridView;
            if (grid == null) continue;
            foreach (GridViewDataColumn column in grid.Columns)
            {
                string format = column.FormatString;
                if (String.IsNullOrEmpty(format) || format.IndexOf('d') < 0) continue;
                string label = form.GetType().FullName + "." + field.Name + "." + column.Name + " " + cultureName;
                Check(format == AppDateFormat.GridFormat, "grid date uses global setting " + label);
                for (int i = 0; i < Dates.Length; i++)
                {
                    string actual = String.Format(column.FormatInfo, format, Dates[i]);
                    Check(actual == ExpectedDates[i], "grid column date formatting " + label);
                    gridCases++;
                }
            }
        }
    }

    private static void Forms(string cultureName)
    {
        Type[] types = {
            typeof(BarakalAlkheir.Lic.frmRentALicense),
            typeof(BarakalAlkheir.Lic.frmRentLicensePayment),
            typeof(BarakalAlkheir.Lic.frmChangeRentingAmount),
            typeof(BarakalAlkheir.Lic.frmPaymentReport),
            typeof(BarakalAlkheir.Lic.frmLicenseMainPage),
            typeof(BarakalAlkheir.Lic.frmRentingPaymentDetails),
            typeof(BarakalAlkheir.CarsM.frmRentACar),
            typeof(BarakalAlkheir.CarsM.frmRentCarPayment),
            typeof(BarakalAlkheir.CarsM.frmChangeRentingAmountCar),
            typeof(BarakalAlkheir.CarsM.frmPaymentReport),
            typeof(BarakalAlkheir.CarsM.frmCarMainPage),
            typeof(BarakalAlkheir.CarsM.frmRentingPaymentCarDetails),
            typeof(BarakalAlkheir.CarsM.frmBuyACar),
            typeof(BarakalAlkheir.CarsM.frmBuyCarPayment),
            typeof(BarakalAlkheir.CarsM.frmBuyingPaymentCarDetails),
            typeof(BarakalAlkheir.Sponsor.frmCompanyLicences),
            typeof(BarakalAlkheir.Sponsor.frmCompanyLicencesRemarks),
            typeof(BarakalAlkheir.emp.frmEmployees),
            typeof(BarakalAlkheir.emp.frmEmployeeMain),
            typeof(BarakalAlkheir.Definitions.frmCustomers),
            typeof(BarakalAlkheir.Definitions.frmCars),
            typeof(BarakalAlkheir.Docs.frmDocuments),
            typeof(BarakalAlkheir.Docs.frmDocMainPage),
            typeof(BarakalAlkheir.Docs.frmCompanyLicensesExpiryDocuments),
            typeof(BarakalAlkheir.Docs.frmDocumentTransactionTaskSetpsDetails)
        };
        foreach (Type type in types)
            using (var form = (Form)Activator.CreateInstance(type)) DateControls(form, cultureName);
    }

    [STAThread]
    private static int Main()
    {
        CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
        CultureInfo originalUiCulture = Thread.CurrentThread.CurrentUICulture;
        try
        {
            Check(AppDateFormat.Pattern == "dd/MM/yyyy", "one global day/month/year pattern");
            Check(AppDateFormat.Culture.IsReadOnly, "shared date culture is immutable");
            Check(AppDateFormat.Culture.DateTimeFormat.IsReadOnly, "shared date rules are immutable");
            foreach (string culture in new[] { "en-US", "en-GB", "ar-KW", "ar-SA", "th-TH" })
            {
                CultureRules(culture);
                Forms(culture);
                Console.WriteLine("PASS: date formatting and controls under " + culture);
            }
            Check(pickerCases > 0, "picker cases executed");
            Check(gridCases > 0, "grid cases executed");
            Check(maskedDateCases > 0, "masked date cases executed");
            Console.WriteLine("PASS: " + assertions + " checks; " + pickerCases + " picker date cases; " + gridCases + " grid format cases; " + maskedDateCases + " masked date cases. No forms shown or database operations performed.");
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception);
            return 1;
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
            Thread.CurrentThread.CurrentUICulture = originalUiCulture;
        }
    }
}
