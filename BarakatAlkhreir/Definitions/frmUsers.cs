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

namespace BarakalAlkheir.Definitions
{
    public partial class frmUsers : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        User User = null;
        List<User> lstUser = null;

        components.banner mainbar = new components.banner("frmUsers");

        public frmUsers()
        {
            InitializeComponent();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            BarakalAlkheir.components.FormLoading.Attach(this, this.frmUsers_LoadData);
        }

        private async System.Threading.Tasks.Task frmUsers_LoadData(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                await mainbar.AddToAsync(this);

                mainbar.btnAdd.Click += new EventHandler(btnAdd_Click);
                mainbar.btnEdit.Click += new EventHandler(btnEdit_Click);
                mainbar.btnDelete.Click += new EventHandler(btnDelete_Click);
                mainbar.btnCancel.Click += new EventHandler(btnCancel_Click);
                mainbar.btnSave.Click += new EventHandler(btnSave_Click);
                mainbar.btnFind.Click += new EventHandler(btnFind_Click);
                mainbar.btnFirst.Click += new EventHandler(btnFirst_Click);
                mainbar.btnPrevious.Click += new EventHandler(btnPrevious_Click);
                mainbar.btnNext.Click += new EventHandler(btnNext_Click);
                mainbar.btnLast.Click += new EventHandler(btnLast_Click);
                mainbar.btnExit.Click += new EventHandler(btnExit_Click);

                disableControls();
                await PublicClass.bindUserGroupAsync(cmbuserGroupID);

                await getDataAsync();
                bindData();
                
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            change = "add";
            enableControls();

            txtretypepassword.Text = "";
            chkisActive.Checked = true;
            chkisSuperUser.Checked = false;
            txtpassword.Text = "";
            txtuserName.Text = "";
            txtdisplayName.Text = "";
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                change = "edit";
                enableControls();
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟",
                        "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(lblID.Text);
                            User bb = (from c in db.Users
                                       where c.userID ==
                                           bbID
                                       select c).FirstOrDefault();

                            db.Users.Remove(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                position = 0;
                                getData();
                                bindData();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            change = "";
            disableControls();
            bindData();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                if (change == "add")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        User bb = new User();

                        bb.userGroupID = int.Parse(cmbuserGroupID.SelectedValue.ToString());
                        bb.isActive = chkisActive.Checked;
                        bb.isSuperUser = chkisSuperUser.Checked;
                        bb.password = txtpassword.Text;
                        bb.userName = txtuserName.Text;
                        bb.displayName = txtdisplayName.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Users.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(lblID.Text);
                        User bb = (from c in db.Users
                                   where c.userID ==
                                       bbID
                                   select c).FirstOrDefault();

                        bb.userGroupID = int.Parse(cmbuserGroupID.SelectedValue.ToString());
                        bb.isActive = chkisActive.Checked;
                        bb.isSuperUser = chkisSuperUser.Checked;
                        bb.password = txtpassword.Text;
                        bb.userName = txtuserName.Text;
                        bb.displayName = txtdisplayName.Text;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Users.Add(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstUser.Count - 1;
                    }
                    change = "";

                    bindData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnFind_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void btnFirst_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position = 0;
                bindData();
            }
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position -= 1;
                bindData();
            }
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            if (position <lstUser.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstUser.Count -1)
            {
                position = lstUser.Count - 1;
                bindData();
            }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void disableControls()
        {
            mainbar.enableButtons();

            txtretypepassword.ReadOnly = true;
            chkisActive.Enabled = false;
            txtpassword.ReadOnly = true;
            txtuserName.ReadOnly = true;
            txtdisplayName.ReadOnly = true;

            cmbuserGroupID.Enabled = false;
            chkisSuperUser.Enabled = false;

            GView.ReadOnly = false;
            GView.Enabled = true;
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtretypepassword.ReadOnly = false;
            chkisActive.Enabled = true;
            txtpassword.ReadOnly = false;
            txtuserName.ReadOnly = false;
            txtdisplayName.ReadOnly = false;

            cmbuserGroupID.Enabled = true;
            chkisSuperUser.Enabled = true;

            GView.ReadOnly = true;
            GView.Enabled = false;
        }

        private System.Threading.Tasks.Task getDataAsync()
        {
            return BarakalAlkheir.components.FormLoading.LoadAsync(this, () =>
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    return (from c in db.Users  select c).ToList();
                }
            }, rows =>
            {
                lstUser = rows;
                GView.DataSource = lstUser;
            });
        }
        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstUser = (from c in db.Users  select c).ToList();
                GView.DataSource = lstUser;
            }
        }

        private void bindData()
        {
            if (lstUser.Count > 0)
            {
                txtretypepassword.Text = lstUser[position].password;
                chkisActive.Checked = lstUser[position].isActive.Value;
                chkisSuperUser.Checked = lstUser[position].isSuperUser.Value;
                txtpassword.Text = lstUser[position].password;
                txtuserName.Text = lstUser[position].userName;
                txtdisplayName.Text = lstUser[position].displayName;
                lblID.Text = lstUser[position].userID.ToString();

                for (int i = 0; i < cmbuserGroupID.Items.Count; i++)
                {
                    if (cmbuserGroupID.Items[i].Value.ToString() == lstUser[position].userGroupID.ToString())
                    {
                        cmbuserGroupID.SelectedIndex = i;
                        break;
                    }
                }

                mainbar.lblCount.Text = position + 1 + " / " + lstUser.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                cmbuserGroupID.SelectedIndex = -1;

                txtretypepassword.Text = "";
                chkisActive.Checked = true;
                chkisActive.Checked = false;
                txtpassword.Text = "";
                txtuserName.Text = "";
                txtdisplayName.Text = "";

                lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }
        }




        private void GView_Click(object sender, EventArgs e)
        {
            
        }

        private void GView_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int bID = int.Parse(GView.Rows[rowIndex].Cells[0].Value.ToString());
                User User = lstUser.Find(b => b.userID == bID);

                position = lstUser.IndexOf(User);
                bindData();
            }
        }



    }
}
