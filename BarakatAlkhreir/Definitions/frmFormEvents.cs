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
using System.Transactions;

namespace BarakalAlkheir.Definitions
{
    public partial class frmFormEvents : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        components.banner mainbar = new components.banner("frmFormEvents");
        bool islstLoad = false;

        public frmFormEvents()
        {
            InitializeComponent();
        }

        private void frmFormEvents_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                this.Controls.Add(mainbar);

                mainbar.btnAdd.Visible = false;
                mainbar.btnDelete.Visible = false;
                mainbar.btnFind.Visible = false;
                mainbar.btnFirst.Visible = false;
                mainbar.btnPrevious.Visible = false;
                mainbar.btnNext.Visible = false;
                mainbar.btnLast.Visible = false;
                mainbar.lblCount.Visible = false;

                mainbar.btnEdit.Click += new EventHandler(btnEdit_Click);
                mainbar.btnCancel.Click += new EventHandler(btnCancel_Click);
                mainbar.btnSave.Click += new EventHandler(btnSave_Click);
                mainbar.btnExit.Click += new EventHandler(btnExit_Click);


                disableControls();

                bindDefaultGrid();
                LoadForms();
                islstLoad = true;
                if (lstData.Items.Count > 0)
                {
                    lstData.SetSelected(0, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }



        void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls()
        {
            mainbar.enableButtons();
            lstData.Enabled = true;
            Gview.ReadOnly = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            lstData.Enabled = false;
            Gview.ReadOnly = false;
            Gview.Columns[2].ReadOnly = true;
        }


        void btnEdit_Click(object sender, EventArgs e)
        {
            change = "edit";
            enableControls();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            change = "";
            disableControls();

        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (change == "edit")
                {
                    int fID = int.Parse(lstData.SelectedValue.ToString());

                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {


                        using (TransactionScope scop = new TransactionScope())
                        {
                            for (int i = 0; i < Gview.Rows.Count; i++)
                            {
                                int eventID = int.Parse(Gview.Rows[i].Cells[1].Value.ToString());
                                int formEventID = int.Parse(Gview.Rows[i].Cells[0].Value.ToString());
                                bool isSelected = bool.Parse(Gview.Rows[i].Cells[3].Value.ToString());

                                if (isSelected && formEventID == -1)
                                {
                                    // add record
                                    FormEvent bb = new FormEvent();
                                    bb.formID = fID;
                                    bb.eventID = eventID;

                                    bb.added_Date = DateTime.Now;
                                    bb.addedBy = MainClass.userID;
                                    bb.edited_Date = DateTime.Now;
                                    bb.editedBy = MainClass.userID;

                                    db.FormEvents.AddObject(bb);
                                    db.SaveChanges();
                                }
                                else if (!isSelected && formEventID != -1)
                                {
                                    // edit record

                                    FormEvent bb = (from c in db.FormEvents
                                                    where c.formID == fID
                                                    && c.eventID == eventID
                                                    select c).FirstOrDefault();
                                    if (bb != null)
                                    {
                                        bb.formID = fID;
                                        bb.eventID = eventID;

                                        bb.edited_Date = DateTime.Now;
                                        bb.editedBy = MainClass.userID;

                                        db.FormEvents.DeleteObject(bb);
                                        db.SaveChanges();
                                    }
                                }
                            }

                            db.AcceptAllChanges();
                            scop.Complete();
                        }


                    }
                    change = "";
                    disableControls();
                    bindFormEvent(fID);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadForms()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = from c in db.FormScreans
                          orderby c.form_DisplayName
                          select new
                          {
                              c.formID,
                              c.form_DisplayName
                          };

                lstData.DataSource = val.ToList();
                lstData.DisplayMember = "form_DisplayName";
                lstData.ValueMember = "formID";
               
            }
        }

        private void bindDefaultGrid()
        {
            Gview.Rows.Clear();
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.EventsDefinitions
                           orderby c.event_DisplayName
                           select c).ToList();
                foreach (var item in val)
                {
                    Gview.Rows.Add(1);
                    int row = Gview.Rows.Count - 1;

                    Gview.Rows[row].Cells[0].Value = "-1";
                    Gview.Rows[row].Cells[1].Value = item.eventID;
                    Gview.Rows[row].Cells[2].Value = item.event_DisplayName;
                }
            }
        }

        private void resetGridView()
        {
            for (int i = 0; i < Gview.Rows.Count; i++)
            {
                Gview.Rows[i].Cells[0].Value = "-1";
                Gview.Rows[i].Cells[3].Value = false.ToString();
            }
        }

        private void bindFormEvent(int _formID)
        {
            resetGridView();
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormEvents
                           where c.formID == _formID
                           select c).ToList();
                if (val.Count > 0)
                {
                    foreach (var item in val)
                    {
                        for (int i = 0; i < Gview.Rows.Count; i++)
                        {
                            if (item.eventID.Value.ToString() == Gview.Rows[i].Cells[1].Value.ToString())
                            {
                                Gview.Rows[i].Cells[0].Value = item.formEventID.ToString();
                                Gview.Rows[i].Cells[3].Value = true.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void lstData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (islstLoad)
            {
                if (lstData.SelectedIndex != -1 && lstData.Items.Count > 0)
                {
                    

                    int fID = int.Parse(lstData.SelectedValue.ToString());
                    bindFormEvent(fID);
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        var val = (from c in db.FormScreans
                                   where c.formID == fID
                                   select new
                                   {
                                       c.form_DisplayName
                                   }).ToList();
                        if (val.Count > 0)
                        {
                            lblFormName.Text = val[0].form_DisplayName;
                        }
                    }
                }
            }
            else
            {
                lblFormName.Text = "";
            }
        }




    }
}
