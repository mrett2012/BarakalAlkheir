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

namespace BarakalAlkheir.Sponsor
{
    public partial class frmFindCompany : Telerik.WinControls.UI.RadForm
    {
        List<Company> lstCompany = null;
        public bool found = false;
        public int cID = -1;
        public string cName = "";
        public string cFileNo = "";
        public string cUniqueNo = "";

        public frmFindCompany()
        {
            InitializeComponent();
        }

        private void frmFindCompany_Load(object sender, EventArgs e)
        {
            try
            {
          
                getData();
                bindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                    lstCompany = (from c in db.Companies
                                      orderby c.comp_arabicName
                                         select c).ToList();
            }
        }

        private void bindData()
        {
            GView.DataSource = lstCompany;
        }

        private void txtcommercialName_TextChanged(object sender, EventArgs e)
        {
            List<Company> dd = lstCompany.FindAll(c => c.comp_arabicName.Contains(txtName.Text) == true);
            if (this.txtName.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtFileNo_TextChanged(object sender, EventArgs e)
        {
            List<Company> dd = lstCompany.FindAll(c => c.comp_fileNo.Contains(this.txtFileNo.Text) == true);
            if (this.txtFileNo.Text.Length > 0)
            {
                GView.DataSource = dd;
            }
            else
            {
                bindData();
            }
        }

        private void txtUniqueNo_TextChanged(object sender, EventArgs e)
        {
            List<Company> dd = lstCompany.FindAll(c => c.comp_uniqueNo.Contains(this.txtUniqueNo.Text) == true);
            if (this.txtUniqueNo.Text.Length > 0)
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
                cID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                cName = GView.CurrentRow.Cells[1].Value.ToString();
                cFileNo = GView.CurrentRow.Cells[2].Value.ToString();
                cUniqueNo= GView.CurrentRow.Cells[3].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GView.SelectedRows.Count > 0)
            {
                cID = int.Parse(GView.CurrentRow.Cells[0].Value.ToString());
                cName = GView.CurrentRow.Cells[1].Value.ToString();
                cFileNo = GView.CurrentRow.Cells[2].Value.ToString();
                cUniqueNo = GView.CurrentRow.Cells[3].Value.ToString();
                found = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
