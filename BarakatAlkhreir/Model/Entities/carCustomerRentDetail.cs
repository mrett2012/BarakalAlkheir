using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class carCustomerRentDetail
    {
        public int carCustomerRentDetailsID { get; set; }
        public int? carCustomerRentID { get; set; }
        public DateTime? rd_fromDate { get; set; }
        public DateTime? rd_toDate { get; set; }
        public int? rd_mNo { get; set; }
        public float? rd_amount { get; set; }
        public bool? rd_isPaid { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual CarCustomerRent CarCustomerRent { get; set; }
    }
}
