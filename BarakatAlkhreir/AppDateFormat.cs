using System;
using System.Globalization;
using System.Threading;

namespace BarakalAlkheir
{
    public static class AppDateFormat
    {
        // Change the application's full-date format here.
        public const string Pattern = "dd/MM/yyyy";

        private static readonly CultureInfo dateCulture = CreateDateCulture();

        public static CultureInfo Culture { get { return dateCulture; } }

        // Quote separators so native Windows controls do not replace them.
        public static string PickerFormat { get { return Pattern.Replace("/", "'/'"); } }
        public static string GridFormat { get { return "{0:" + Pattern + "}"; } }

        private static CultureInfo CreateDateCulture()
        {
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = Pattern;
            culture.DateTimeFormat.LongDatePattern = Pattern;
            culture.DateTimeFormat.FullDateTimePattern = Pattern + " " + culture.DateTimeFormat.LongTimePattern;
            return CultureInfo.ReadOnly(culture);
        }

        public static void ApplyToCurrentThread()
        {
            // .NET Framework 4.0: initialize the UI thread before creating any forms.
            // Call this at the entry point of any future application worker thread too.
            var culture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            var dates = (DateTimeFormatInfo)Culture.DateTimeFormat.Clone();
            dates.ShortTimePattern = culture.DateTimeFormat.ShortTimePattern;
            dates.LongTimePattern = culture.DateTimeFormat.LongTimePattern;
            dates.AMDesignator = culture.DateTimeFormat.AMDesignator;
            dates.PMDesignator = culture.DateTimeFormat.PMDesignator;
            dates.TimeSeparator = culture.DateTimeFormat.TimeSeparator;
            dates.FullDateTimePattern = Pattern + " " + dates.LongTimePattern;
            culture.DateTimeFormat = dates;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public static string Format(DateTime value)
        {
            return value.ToString(Pattern, Culture);
        }

        public static bool TryParseStoredDate(string value, out DateTime date)
        {
            // Older employee application dates were saved as year/month/day text.
            return DateTime.TryParseExact(value, new[] { Pattern, "yyyy / MM / dd", "yyyy/MM/dd" },
                Culture, DateTimeStyles.AllowWhiteSpaces, out date);
        }

        public static string FormatStoredDate(string value)
        {
            DateTime date;
            return TryParseStoredDate(value, out date) ? Format(date) : value;
        }
    }
}
