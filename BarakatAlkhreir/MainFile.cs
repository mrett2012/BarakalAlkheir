using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarakalAlkheir.Model;

namespace BarakalAlkheir
{
    public class MainFile
    {

        public static bool isVisibleSecurity(string _form_IDName)
        {
            bool temp = false;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormScreans
                           join d in db.FormEvents
                           on c.formID equals d.formID
                           join dd in db.EventsDefinitions
                           on d.eventID equals dd.eventID
                           join e in db.UserGroupEvents
                           on d.formEventID equals e.formEventID
                           where e.userGroupID == MainClass.userGroupID &&
                           c.form_IDName == _form_IDName
                           select new
                           {
                               dd.event_Name
                           }).ToList();

                if (val.Count > 0)
                {
                    foreach (var item in val)
                    {
                        if (item.event_Name == "view")
                        {
                            temp = true;
                        }


                    }
                }


            }

            return temp;
        }

        public static bool isDeleteSecurity(string _form_IDName)
        {
            bool temp = false;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormScreans
                           join d in db.FormEvents
                           on c.formID equals d.formID
                           join dd in db.EventsDefinitions
                           on d.eventID equals dd.eventID
                           join e in db.UserGroupEvents
                           on d.formEventID equals e.formEventID
                           where e.userGroupID == MainClass.userGroupID &&
                           c.form_IDName == _form_IDName
                           select new
                           {
                               dd.event_Name
                           }).ToList();

                if (val.Count > 0)
                {
                    foreach (var item in val)
                    {
                        if (item.event_Name == "delete")
                        {
                            temp = true;
                        }


                    }
                }


            }

            return temp;
        }

        public static bool isAddSecurity(string _form_IDName)
        {
            bool temp = false;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormScreans
                           join d in db.FormEvents
                           on c.formID equals d.formID
                           join dd in db.EventsDefinitions
                           on d.eventID equals dd.eventID
                           join e in db.UserGroupEvents
                           on d.formEventID equals e.formEventID
                           where e.userGroupID == MainClass.userGroupID &&
                           c.form_IDName == _form_IDName
                           select new
                           {
                               dd.event_Name
                           }).ToList();

                if (val.Count > 0)
                {
                    foreach (var item in val)
                    {
                        if (item.event_Name == "add")
                        {
                            temp = true;
                        }


                    }
                }


            }

            return temp;
        }


        public static int NoOfSeatForShop(int _LicShopID)
        {
            int temp = 0;
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.RentLicenses
                           where c.companyLicencesID == _LicShopID
                           && c.rentLicense_active == true
                           select c).ToList();
                if (val.Count > 0)
                {
                    temp = val[0].rentLicense_NoOfSeats.Value;
                }
            }

            return temp;
        }

        public static int NoOfSeatTaken(int _LicShopID)
        {
            int temp = 0;

            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.EmployeeSponsors
                           join cc in db.Employees on c.employeeID equals cc.employeeID
                           join ccc in db.EmployeeLicenseShops on cc.employeeID equals ccc.employeeID
                           where ccc.licencesShopID == _LicShopID && cc.emp_active == true
                           select c).ToList();
                temp = val.Count;
            }

            return temp;
        }


    }
}
