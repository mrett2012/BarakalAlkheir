using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class CarSupllier
    {
        public CarSupllier()
        {
            CarSupplierRents = new HashSet<CarSupplierRent>();
        }

        public int carSuppliserID { get; set; }
        public string carSuppliser_Name { get; set; }
        public string carSuppliser_PersonInCharge { get; set; }
        public string carSuppliser_Contact { get; set; }
        public string carSuppliser_Address { get; set; }
        public string carSuppliser_remarks { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }

        public virtual ICollection<CarSupplierRent> CarSupplierRents { get; set; }
    }
}
