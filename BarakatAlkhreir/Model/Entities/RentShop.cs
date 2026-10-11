using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class RentShop
    {
        public RentShop()
        {
            RentShopEnds = new HashSet<RentShopEnd>();
            RentShopPayments = new HashSet<RentShopPayment>();
        }

        public int rentShopID { get; set; }
        public string rentShopNo { get; set; }
        public int? customerID { get; set; }
        public int? licencesShopID { get; set; }
        public DateTime? rentShop_date { get; set; }
        public DateTime? rentShop_from { get; set; }
        public DateTime? rentShop_to { get; set; }
        public float? rentShop_RentalAmount { get; set; }
        public float? rentShop_DamanAmount { get; set; }
        public bool? rentShop_active { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual LicensesShop LicensesShop { get; set; }
        public virtual ICollection<RentShopEnd> RentShopEnds { get; set; }
        public virtual ICollection<RentShopPayment> RentShopPayments { get; set; }
    }
}
