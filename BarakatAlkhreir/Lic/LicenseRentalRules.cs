using System;
using System.Globalization;

namespace BarakalAlkheir.Lic
{
    // Calendar and money rules shared by entry screens, transactions and reports.
    internal static class LicenseRentalRules
    {
        internal const int CutoffDay = 10;

        internal static string FormatDate(DateTime value)
        {
            return AppDateFormat.Format(value);
        }

        internal static DateTime InstalmentStart(DateTime start, int month)
        {
            if (month < 1 || month > 1000)
                throw new InvalidOperationException("عدد الأشهر يجب أن يكون بين 1 و 1000");
            return start.Date.AddMonths(month - 1);
        }

        internal static DateTime InstalmentEnd(DateTime start, int month)
        {
            InstalmentStart(start, month);
            return start.Date.AddMonths(month).AddDays(-1);
        }

        internal static DateTime BillingDate(DateTime instalmentStart)
        {
            return new DateTime(instalmentStart.Year, instalmentStart.Month, 15)
                .AddMonths(instalmentStart.Day > CutoffDay ? 1 : 0);
        }

        internal static DateTime PeriodStart(DateTime month)
        {
            return new DateTime(month.Year, month.Month, CutoffDay).AddMonths(-1).AddDays(1);
        }

        internal static DateTime PeriodEndExclusive(DateTime month)
        {
            return new DateTime(month.Year, month.Month, CutoffDay).AddDays(1);
        }

        internal static decimal ParsePositiveAmount(string text)
        {
            decimal value;
            if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value) || value <= 0)
                throw new InvalidOperationException("يجب إدخال مبلغ صحيح أكبر من صفر");
            ToStorageAmount(value);
            return value;
        }

        internal static decimal Amount(float? value)
        {
            if (!value.HasValue || float.IsNaN(value.Value) || float.IsInfinity(value.Value))
                throw new InvalidOperationException("يوجد مبلغ غير صحيح. يرجى مراجعة بيانات العقد");
            return Convert.ToDecimal(value.Value);
        }

        internal static float ToStorageAmount(decimal value)
        {
            float stored = (float)value;
            if (float.IsInfinity(stored) || Convert.ToDecimal(stored) != value)
                throw new InvalidOperationException("المبلغ يتجاوز دقة التخزين المتاحة");
            return stored;
        }

        internal static void ValidateText(string value)
        {
            if (value != null && value.Length > 50)
                throw new InvalidOperationException("الرقم يجب ألا يتجاوز 50 حرفاً");
        }
    }
}
