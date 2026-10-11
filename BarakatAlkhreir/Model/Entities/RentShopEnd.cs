using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class RentShopEnd
    {
        public int rentShopEndID { get; set; }
        public int? rentShopID { get; set; }
        public DateTime? rentShopEnd_Date { get; set; }
        public string rentShopEnd_reasons { get; set; }
        public bool? rentShopEnd_isActive { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual RentShop RentShop { get; set; }
    }
}
