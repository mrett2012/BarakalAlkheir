using System;
using System.Collections.Generic;

namespace BarakalAlkheir.Model
{
    public partial class Car
    {
        public Car()
        {
            CarCustomerRents = new HashSet<CarCustomerRent>();
            CarSupplierRents = new HashSet<CarSupplierRent>();
        }

        public int carID { get; set; }
        public string car_No { get; set; }
        public string car_Name { get; set; }
        public string car_Model { get; set; }
        public string car_Brand { get; set; }
        public string car_ChesseeNo { get; set; }
        public bool? car_isAvailable { get; set; }
        public DateTime? added_Date { get; set; }
        public DateTime? edited_Date { get; set; }
        public int? addedBy { get; set; }
        public int? editedBy { get; set; }
        public string car_fileNo { get; set; }

        public virtual ICollection<CarCustomerRent> CarCustomerRents { get; set; }
        public virtual ICollection<CarSupplierRent> CarSupplierRents { get; set; }
    }
}
