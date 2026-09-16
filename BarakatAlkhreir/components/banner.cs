using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using BarakalAlkheir.Model;

namespace BarakalAlkheir.components
{
    public partial class banner : UserControl
    {
        string form_IDName = "";

        public banner(string _form_IDName)
        {
            InitializeComponent();
            form_IDName = _form_IDName;
        }

        public void enableButtons()
        {
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;

            btnEdit.Enabled = true;
            
            btnFirst.Enabled = true;
            btnLast.Enabled = true;
            btnNext.Enabled = true;
            btnPrevious.Enabled = true;

            btnFind.Enabled = true;
            btnPrint.Enabled = true;

            btnCancel.Enabled = false;
            btnSave.Enabled = false;

            
        }

        public void disableButton()
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            
            btnFirst.Enabled = false;
            btnLast.Enabled = false;
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;

            btnCancel.Enabled = true;
            btnSave.Enabled = true;

            btnFind.Enabled = false;
            btnPrint.Enabled = false;
        }

        private void banner_Load(object sender, EventArgs e)
        {
            if (!MainClass.isSuperUser)
            {
                DisableAllSecurity();
                ApplySecurity(form_IDName);
            }
        }


        private void DisableAllSecurity()
        {
            btnAdd.Visible = false;
            btnDelete.Visible = false;
            btnEdit.Visible = false;

            btnFirst.Visible = false;
            btnLast.Visible = false;
            btnNext.Visible = false;
            btnPrevious.Visible = false;

            btnCancel.Visible = false;
            btnSave.Visible = false;

            btnFind.Visible = false;
            btnPrint.Visible = false;
        }

        private void showNavigation()
        {
            btnFirst.Visible = true;
            btnLast.Visible = true;
            btnNext.Visible = true;
            btnPrevious.Visible = true;
        }

        private void ApplySecurity(string _form_IDName)
        {
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
                            btnAdd.Visible = true;
                            btnSave.Visible = true;
                            btnCancel.Visible = true;
                            showNavigation();
                        }
                        else if (item.event_Name == "edit")
                        {
                            btnEdit.Visible = true;
                            btnSave.Visible = true;
                            btnCancel.Visible = true;
                            showNavigation();
                        }
                        else if (item.event_Name == "delete")
                        {
                            this.btnDelete.Visible = true;
                            btnFind.Visible = true;
                            showNavigation();
                        }
                        else if (item.event_Name == "print")
                        {
                            this.btnPrint.Visible = true;
                            btnFind.Visible = true;
                            showNavigation();
                        }
                        else if (item.event_Name == "view")
                        {
                            // TODO: Check different Senario for Viewing
                            btnFind.Visible = true;
                            showNavigation();
                        }
                        else if (item.event_Name == "search")
                        {
                            this.btnFind.Visible = true;
                            showNavigation();
                        }

                    }
                }

            }
        }



    }
}
