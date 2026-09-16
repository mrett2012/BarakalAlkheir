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

namespace BarakalAlkheir
{
    public partial class frmlogin : Telerik.WinControls.UI.RadForm
    {
        public bool loged = false;

        public frmlogin()
        {
            InitializeComponent();
        }

        private void frmlogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!loged)
                MainClass.mainForm.Close();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                var users = (from c in db.Users
                             where c.userName == txtuserName.Text &&
                             c.password == txtpassword.Text
                             select c).ToList();
                if (users.Count > 0)
                {
                    MainClass.userID = users[0].userID;
                    MainClass.displayName = users[0].displayName;
                    MainClass.userName = users[0].userName;
                    MainClass.isSuperUser = users[0].isSuperUser.Value;
                    MainClass.userGroupID = users[0].userGroupID.Value;
                    loged = true;
                    Close();
                }
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                loginButton_Click(sender, e);
            }
        }

        private void txtuserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                loginButton_Click(sender, e);
            }
        }


    }
}
