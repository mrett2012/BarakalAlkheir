using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using BarakalAlkheir.Model;

namespace BarakalAlkheir.CarsM
{
    public partial class frmfindCars : Telerik.WinControls.UI.RadForm
    {
        List<Car> lstCar = null;
        public bool found = false;

        public int carID = -1;
        public string carNO = "";
        public string carName = "";
         

        public bool isCurrentRentedCar = false;
        public bool isCurrentRentedCarInclude = false;

        public bool isCurrentBuyingCarNotInclude = false;

        public bool isCurrentbying= false;

        public bool isAllCar = false;

        public frmfindCars(bool _isCurrentRentedCar)
        {
            InitializeComponent();
        }

        private void frmfindLicences_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmfindLicences_LoadData);
        }

        private async System.Threading.Tasks.Task frmfindLicences_LoadData(object sender, EventArgs e)
        {
            try
            {
                await BarakalAlkheir.components.FormLoading.ReadAsync(this, getData);
                bindData();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                if (isAllCar)
                {
                    lstCar = (from c in db.Cars
                                         select c).ToList();
                }
                else if (isCurrentbying)
                {
                    lstCar = (from c in db.Cars
                              join d in db.CarSupplierRents
                                  on c.carID equals d.carID
                              where c.car_isAvailable == true && d.carSupplierRent_isactive == true
                              select c).ToList();
                }
                else if (isCurrentBuyingCarNotInclude)
                {
                    lstCar = (from c in db.Cars
                              where !(from cc in db.Cars
                                      join d in db.CarSupplierRents
                                      on cc.carID equals d.carID
                                      select cc.carID).Contains(c.carID)
                                    && c.car_isAvailable == true
                              select c).ToList();
                }

                else if (isCurrentRentedCar)
                {

                    lstCar = (from c in db.Cars
                              join d in db.CarCustomerRents
                             on c.carID equals d.carID
                              where c.car_isAvailable == true && d.carCustomerRent_active == true
                              select c).ToList();

                }
                else
                {
                    if (isCurrentRentedCarInclude)
                    {
                        lstCar = (from c in db.Cars
                                  where c.car_isAvailable == true
                                  select c).ToList();
                    }
                    else
                    {

                        lstCar = (from c in db.Cars
                                  where !(from cc in db.Cars
                                          join d in db.CarCustomerRents
                                          on cc.carID equals d.carID
                                          where d.carCustomerRent_active == true
                                          && cc.carID==c.carID
                                          select cc.carID).Contains(c.carID)
                                        && c.car_isAvailable == true
                                  select c).ToList();
                    }

                }
                
            }
        }

        private void bindData()
        {
            GView.DataSource = lstCar;
        }

        private void txtlicenseNo_TextChanged(object sender, EventArgs e)
       {
            List<Car> dd = lstCar.FindAll(c => c.car_No.Contains(txtcar_No.Text)==true);
            if (txtcar_No.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtcommercialName_TextChanged(object sender, EventArgs e)
        {
            List<Car> dd = lstCar.FindAll(c => c.car_Name.Contains(txtcar_Name.Text) == true);
            if (txtcar_Name.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void GView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                carID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                carNO = GView.CurrentRow.Cells[1].Value.ToString();
                carName = GView.CurrentRow.Cells[3].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                carID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                carNO = GView.CurrentRow.Cells[1].Value.ToString();
                carName = GView.CurrentRow.Cells[3].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtlicenseLinkCode_TextChanged(object sender, EventArgs e)
        {
            List<Car> dd = lstCar.FindAll(c => c.car_Brand.Contains(txtcar_Brand.Text) == true);
            if (txtcar_Brand.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }





    }
}
