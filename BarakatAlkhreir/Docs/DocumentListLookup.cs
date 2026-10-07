using System;
using System.Collections.Generic;
using System.Linq;
using BarakalAlkheir.Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace BarakalAlkheir.Docs
{
    internal sealed class DocumentListData<T>
    {
        public List<T> Rows;
        public DocumentListLookup Lookup;
    }

    internal sealed class DocumentListLookup
    {
        private readonly Dictionary<int, string> transactions = new Dictionary<int, string>();
        private readonly HashSet<int> remarks = new HashSet<int>();
        private readonly HashSet<int> rentals = new HashSet<int>();

        public string Transactions(int documentId)
        {
            string value;
            return transactions.TryGetValue(documentId, out value) ? value : string.Empty;
        }

        public bool HasRemarks(int licenseId) { return remarks.Contains(licenseId); }
        public bool HasRental(int licenseId) { return rentals.Contains(licenseId); }

        public static void ConfigureGrid(RadGridView grid)
        {
            grid.RowFormatting += (sender, args) =>
            {
                if (object.Equals(args.RowElement.RowInfo.Tag, true))
                    args.RowElement.DrawFill = true;
                else
                    args.RowElement.ResetValue(GridRowElement.DrawFillProperty, ValueResetFlags.Local);
            };
        }

        public static DocumentListData<T> Create<T>(BarakeitAlkheirEntities db, IEnumerable<T> rows,
            Func<T, int> documentId, Func<T, int> licenseId, bool openTasksOnly)
        {
            var result = new DocumentListData<T> { Rows = rows.ToList(), Lookup = new DocumentListLookup() };
            var lookup = result.Lookup;
            // Keep each IN predicate comfortably below SQL Server's parameter limit.
            foreach (int[] ids in Batches(result.Rows.Select(documentId)))
            {
                var names = (from task in db.DocumentTransactionTasks
                             join transaction in db.DocumentTransactions on task.documentTransID equals transaction.documentTransID
                             join definition in db.TransactionDefs on transaction.transDefID equals definition.transDefID
                             where task.documentID.HasValue && ids.Contains(task.documentID.Value)
                                 && (!openTasksOnly || task.documentTransTask_isClose == false)
                             orderby task.documentTransTaskID
                             select new { Id = task.documentID.Value, Name = definition.transDef_namea }).ToList();
                foreach (var name in names)
                    lookup.transactions[name.Id] = lookup.Transactions(name.Id) + "-" + name.Name;
            }

            if (licenseId != null)
            {
                DateTime now = DateTime.Now;
                foreach (int[] ids in Batches(result.Rows.Select(licenseId)))
                {
                    var remarkIds = (from remark in db.CompanyLicencesRemarks
                                     where remark.companyLicencesID.HasValue && ids.Contains(remark.companyLicencesID.Value)
                                         && remark.comLicenseRem_isActive == true
                                     select remark.companyLicencesID.Value).Distinct().ToList();
                    lookup.remarks.UnionWith(remarkIds);
                    var rentalIds = (from rent in db.RentLicenses
                                     join detail in db.RentLicensesDetails on rent.rentLicenseID equals detail.rentLicenseID
                                     where rent.companyLicencesID.HasValue && ids.Contains(rent.companyLicencesID.Value)
                                         && detail.rd_fromDate < now && detail.rd_isPaid == false
                                     select rent.companyLicencesID.Value).Distinct().ToList();
                    lookup.rentals.UnionWith(rentalIds);
                }
            }
            return result;
        }

        private static IEnumerable<int[]> Batches(IEnumerable<int> ids)
        {
            int[] uniqueIds = ids.Where(id => id >= 0).Distinct().ToArray();
            for (int index = 0; index < uniqueIds.Length; index += 500)
                yield return uniqueIds.Skip(index).Take(500).ToArray();
        }
    }
}
