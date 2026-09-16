using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using BarakalAlkheir.Model;
using System.Transactions;

namespace BarakalAlkheir.Definitions
{
    public partial class frmUsersFormEvents : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        components.banner mainbar = new components.banner("frmUsersFormEvents");
        List<UserGroupEvent> lstUserGroupEvent = null;

        bool isLoadData = false;

        public frmUsersFormEvents()
        {
            InitializeComponent();
        }

        private void frmUsersFormEvents_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                this.Controls.Add(mainbar);

                
                mainbar.btnEdit.Click += new EventHandler(btnEdit_Click);            
                mainbar.btnCancel.Click += new EventHandler(btnCancel_Click);
                mainbar.btnSave.Click += new EventHandler(btnSave_Click);
                mainbar.btnExit.Click += new EventHandler(btnExit_Click);
                mainbar.btnDelete.Click += new EventHandler(btnDelete_Click);

                mainbar.btnAdd.Visible = false;
                //mainbar.btnDelete.Visible = false;
                mainbar.btnFirst.Visible = false;
                mainbar.btnPrevious.Visible = false;
                mainbar.btnNext.Visible = false;
                mainbar.btnLast.Visible = false;
                mainbar.btnFind.Visible = false;
                mainbar.lblCount.Visible = false;

                gview.ReadOnly = false;
                gview.Columns[0].ReadOnly = true;
                gview.Columns[1].ReadOnly = true;

                disableControls();
                PublicClass.bindUserGroup(cmbuserGroupID);
                cmbuserGroupID.SelectedIndex = 0;

                loadScreens();
                loadEvents();
                DisableScreenEvent();
                loadScreenEvent();

                bindUserGroupEvents();
                isLoadData = true;                
                //getData();
                //bindData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int ugID = int.Parse(cmbuserGroupID.SelectedValue.ToString());
                if (ugID != -1)
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        // delete add data related to that group

                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            using (TransactionScope scop = new TransactionScope())
                            {
                                // delete add data related to that group
                                var vv = (from cc in db.UserGroupEvents
                                          where cc.userGroupID == ugID
                                          select cc).ToList();
                                foreach (var item in vv)
                                {
                                    db.UserGroupEvents.DeleteObject(item);
                                    db.SaveChanges();
                                }

                                db.AcceptAllChanges();
                                scop.Complete();
                            }



                        }

                        if (isLoadData)
                        {
                            getUserGroupEvents(int.Parse(cmbuserGroupID.SelectedValue.ToString()));
                            bindUserGroupEvents();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void loadScreens()
        {
            gview.Rows.Clear();
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormScreans
                          orderby c.form_DisplayName
                          select new
                          {
                              c.form_DisplayName,
                              c.formID
                          }).ToList();
                if (val.Count > 0)
                {
                    foreach (var item in val)
                    {
                        gview.Rows.Add(1);
                        int row = gview.Rows.Count - 1;
                        gview.Rows[row].Cells[0].Value = item.formID.ToString();
                        gview.Rows[row].Cells[1].Value = item.form_DisplayName;
                    }
                }
            }
        }

        private void loadEvents()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                

                var val = (from c in db.EventsDefinitions
                           orderby c.event_DisplayName
                          select new
                          {
                              c.eventID,
                              c.event_DisplayName
                          }).ToList();
                
                if (val.Count > 0)
                {
                    foreach (var item in val)
                    {
                        int col = gview.Columns.Count;
                        gview.Columns.Add(item.eventID + col + 1 + "", "EID");
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        chk.HeaderText = item.event_DisplayName;
                        chk.Name = item.eventID + col + 2 + "";
                        gview.Columns.Add(chk);
                        gview.Columns[col].Width = 50;
                        gview.Columns[col].Visible = false;
                        gview.Columns[col+1].Width = 50;

                        if (gview.Rows.Count > 0)
                        {
                            for (int i = 0; i < gview.Rows.Count; i++)
                            {
                                gview.Rows[i].Cells[col].Value = item.eventID.ToString();
                            }
                        }
                    }
                }


            }
        }

        private void enableCell(DataGridViewCell dc, bool enabled)
        {
            //toggle read-only state
            dc.ReadOnly = !enabled;
            if (enabled)
            {
                //restore cell style to the default value
                dc.Style.BackColor = dc.OwningColumn.DefaultCellStyle.BackColor;
                dc.Style.ForeColor = dc.OwningColumn.DefaultCellStyle.ForeColor;
            }
            else
            {
                //gray out the cell
                dc.Style.BackColor = Color.Red;
                dc.Style.ForeColor = Color.DarkGray;
            }
        }

        private void DisableScreenEvent()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormEvents
                           select new
                           {
                               c.eventID,
                               c.formID
                           }).ToList();

                foreach (var item in val)
                {
                    for (int i = 0; i < gview.Rows.Count; i++)
                    {
                        int fID = int.Parse(gview.Rows[i].Cells[0].Value.ToString());
                        if (gview.Columns.Count > 2)
                        {
                            for (int j = 1; j < gview.Columns.Count; j++)
                            {
                                if (j % 2 == 0)
                                {    
                                        enableCell(gview.Rows[i].Cells[j + 1], false);    
                                        
                                }
                            }// end of for (j)
                        }// end of (if)
                    }// end of for (i)
                }// end of (foreach)



            }
        }

        private void loadScreenEvent()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var val = (from c in db.FormEvents
                           select new
                           {
                               c.eventID,
                               c.formID
                           }).ToList();

                foreach (var item in val)
                {
                    for (int i = 0; i < gview.Rows.Count; i++)
                    {
                        int fID = int.Parse(gview.Rows[i].Cells[0].Value.ToString());
                        if (gview.Columns.Count > 2)
                        {
                            for (int j = 1; j < gview.Columns.Count; j++)
                            {
                                if (j % 2 == 0)
                                {
                                    int eID = int.Parse(gview.Rows[i].Cells[j].Value.ToString());
                                    if (eID == item.eventID && fID == item.formID)
                                    {
                                        enableCell(gview.Rows[i].Cells[j+1], true);
                                    }
                                }
                            }// end of for (j)
                        }// end of (if)
                    }// end of for (i)
                }// end of (foreach)



            }
        }

        private void clearDataGridValue()
        {
            for (int i = 0; i < gview.Rows.Count; i++)
            {
                for (int j = 1; j < gview.Columns.Count; j++)
                {
                    if (j % 2 == 0)
                    {
                        gview.Rows[i].Cells[j + 1].Value = false.ToString();
                    }
                }
            }
        }

        private void getUserGroupEvents(int ugID)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstUserGroupEvent = (from c in db.UserGroupEvents
                                     where c.userGroupID == ugID
                                     select c).ToList();
                db.UserGroupEvents.MergeOption = System.Data.Objects.MergeOption.NoTracking;

            }
        }

        private void bindUserGroupEvents()
        {
            clearDataGridValue();
            if (lstUserGroupEvent != null)
            {
                foreach (var item in lstUserGroupEvent)
                {
                    int feID = item.formEventID.Value;
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        var val = (from c in db.FormEvents
                                   where c.formEventID == feID
                                   select new { c.formID, c.eventID }).ToList();
                        if (val.Count > 0)
                        {
                            for (int i = 0; i < gview.Rows.Count; i++)
                            {
                                int fID = int.Parse(gview.Rows[i].Cells[0].Value.ToString());

                                for (int j = 1; j < gview.Columns.Count; j++)
                                {
                                    if (j % 2 == 0)
                                    {
                                        int eID = int.Parse(gview.Rows[i].Cells[j].Value.ToString());
                                        if (val[0].eventID == eID && val[0].formID == fID)
                                        {
                                            gview.Rows[i].Cells[j + 1].Value = true.ToString();
                                        }
                                    }
                                }
                            }
                        }

                    }

                }
            }
        }
        

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (cmbuserGroupID.SelectedIndex > 0)
            {
                change = "edit";
                enableControls();
            }
           
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
                if (cmbuserGroupID.SelectedIndex > 0)
                {
                    if (change == "edit")
                    {
                        int ugID = int.Parse(cmbuserGroupID.SelectedValue.ToString());
                        // delete add data related to that group

                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {
                           
                            using (TransactionScope scop = new TransactionScope())
                            {
                                // delete add data related to that group
                                var vv = (from cc in db.UserGroupEvents
                                          where cc.userGroupID == ugID
                                          select cc).ToList();
                                foreach (var item in vv)
                                {
                                    db.UserGroupEvents.DeleteObject(item);
                                    db.SaveChanges();
                                }

                                for (int i = 0; i < gview.Rows.Count; i++)
                                {
                                    int fID = int.Parse(gview.Rows[i].Cells[0].Value.ToString());
                                    for (int j = 1; j < gview.Columns.Count; j++)
                                    {
                                        if (j % 2 == 0)
                                        {
                                            int eID = int.Parse(gview.Rows[i].Cells[j].Value.ToString());
                                            if (gview.Rows[i].Cells[j + 1].Value != null)
                                            {
                                                bool isEnable;
                                                if (bool.TryParse(gview.Rows[i].Cells[j + 1].Value.ToString(), out isEnable))
                                                {
                                                    // add new records
                                                    if (isEnable)
                                                    {
                                                        var v = (from c in db.FormEvents
                                                                 where c.eventID == eID &&
                                                                 c.formID == fID
                                                                 select c).FirstOrDefault();
                                                        int feID = v.formEventID;

                                                        UserGroupEvent uu = new UserGroupEvent();
                                                        uu.userGroupID = ugID;
                                                        uu.formEventID = feID;
                                                        uu.isEnable = true;
                                                        uu.added_Date = DateTime.Now;
                                                        uu.edited_Date = DateTime.Now;

                                                        uu.addedBy = MainClass.userID;
                                                        uu.editedBy = MainClass.userID;

                                                        db.UserGroupEvents.AddObject(uu);
                                                        db.SaveChanges();
                                                    }
                                                }
                                              
                                            }
                                            
                                        }
                                    }
                                }
                                db.AcceptAllChanges();
                                scop.Complete();
                                disableControls();
                            }// end of scope
                        }
                    }
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
            cmbuserGroupID.Enabled = true;
            gview.Enabled = false;
        }

        private void enableControls()
        {
            mainbar.disableButton();
            cmbuserGroupID.Enabled = false;
            gview.Enabled = true;
        }

        private void cmbuserGroupID_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (isLoadData)
            {
                getUserGroupEvents(int.Parse(cmbuserGroupID.SelectedValue.ToString()));
                bindUserGroupEvents();
            }
        }

       
    }
}
