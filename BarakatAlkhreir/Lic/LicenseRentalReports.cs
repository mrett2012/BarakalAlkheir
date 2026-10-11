using System;
using System.Collections.Generic;
using System.Linq;
using BarakalAlkheir.Model;

namespace BarakalAlkheir.Lic
{
    internal static class LicenseRentalReports
    {
        internal static Dictionary<int, decimal> PaidByInstalment(BarakeitAlkheirEntities db)
        {
            return db.RentLicensesDetails.Where(d => d.rd_isPaid == true)
                .Select(d => new { d.rentLicencesDetailsID, d.rd_amount }).ToList()
                .ToDictionary(d => d.rentLicencesDetailsID, d => LicenseRentalRules.Amount(d.rd_amount));
        }

        internal static decimal Paid(Dictionary<int, decimal> totals, int detailId)
        {
            decimal result;
            return totals.TryGetValue(detailId, out result) ? result : 0m;
        }

        internal static bool InPeriod(DateTime? date, bool instalment, DateTime? from, DateTime? to)
        {
            if (!date.HasValue) return !from.HasValue && !to.HasValue;
            var billing = instalment ? LicenseRentalRules.BillingDate(date.Value) : date.Value;
            var month = new DateTime(billing.Year, billing.Month, 1);
            return (!from.HasValue || month >= new DateTime(from.Value.Year, from.Value.Month, 1)) &&
                (!to.HasValue || month <= new DateTime(to.Value.Year, to.Value.Month, 1));
        }
    }
}
