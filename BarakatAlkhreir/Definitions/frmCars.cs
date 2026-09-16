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
    public partial class frmCars : Telerik.WinControls.UI.RadForm
    {
        string change = "";
        int position = 0;
        Car car = null;
        List<Car> lstCar = null;

        components.banner mainbar = new components.banner("pageCar");
        components.banner mainbarDocument = new components.banner("PageCarDocument");
        List<Document> lstDocument = null;
        int positionDocument = 0;
        public frmCars()
        {
            InitializeComponent();
        }

        private void disableAllPages()
        {
            this.pageCar.Enabled = false;
            this.PageCarDocument.Enabled = false;

        }

        private void enableAllPages()
        {
            pageCar.Enabled = true;
            PageCarDocument.Enabled = true;
        }

        private void frmCars_Load(object sender, EventArgs e)
        {
            try
            {
                mainbar.Dock = DockStyle.Top;
                pageCar.Controls.Add(mainbar);

                
                mainbarDocument.Dock = DockStyle.Top;
                PageCarDocument.Controls.Add(mainbarDocument);


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


                mainbarDocument.btnAdd.Click += new EventHandler(btnAdd_ClickDoc);
                mainbarDocument.btnEdit.Click += new EventHandler(btnEdit_ClickDoc);
                mainbarDocument.btnDelete.Click += new EventHandler(btnDelete_ClickDoc);

                mainbarDocument.btnFirst.Click += new EventHandler(btnFirst_ClickDoc);
                mainbarDocument.btnPrevious.Click += new EventHandler(btnPrevious_ClickDoc);
                mainbarDocument.btnNext.Click += new EventHandler(btnNext_ClickDoc);
                mainbarDocument.btnLast.Click += new EventHandler(btnLast_ClickDoc);
                mainbarDocument.btnExit.Click += new EventHandler(btnExit_ClickDoc);
                mainbarDocument.btnSave.Visible = false;
                mainbarDocument.btnCancel.Visible = false;
                mainbarDocument.btnFind.Visible = false;


                disableControls();

                getData();
                bindData();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getMaxEmpNo()
        {
            string temp = "";

            try
            {
                using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                {
                    var val = db.Cars.Where(cc => cc.car_fileNo != "" && cc.car_fileNo != null).ToList();
                    List<int> empNo1 = new List<int>();

                    foreach (var item in val)
                    {
                        int nn=0;
                        if (int.TryParse(item.car_fileNo, out nn))
                        {
                            empNo1.Add(int.Parse(item.car_fileNo));
                        }
                    }

                    empNo1.Sort();
                    lblmaxEmpNo.Text = empNo1[empNo1.Count - 1].ToString();
                }
            }
            catch
            {
            }

            //lblmaxEmpNo.Text = temp;
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            change = "add";
            enableControls();
            txtcar_Brand.Text = "";
            txtcar_ChesseeNo.Text = "";
            chkcar_isAvailable.Checked = true;
            txtcar_Model.Text = "";
            txtcar_Name.Text = "";
            txtcar_fileNo.Text = "";
            lblCarName.Text = "";
            txtcar_No.Text = "";
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.lblID.Text != "-1")
            {
                change = "edit";
                enableControls();
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lblID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(this.lblID.Text);
                            Car bb = (from c in db.Cars
                                      where c.carID ==
                                          bbID
                                      select c).FirstOrDefault();

                            db.Cars.DeleteObject(bb);
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
                        Car bb = new Car();

                        bb.car_Brand = txtcar_Brand.Text;
                        bb.car_ChesseeNo = txtcar_ChesseeNo.Text;
                        bb.car_isAvailable = chkcar_isAvailable.Checked;
                        bb.car_Model = txtcar_Model.Text;
                        bb.car_Name = txtcar_Name.Text;
                        bb.car_fileNo = txtcar_fileNo.Text;
                        bb.car_No = txtcar_No.Text;

                        bb.added_Date = DateTime.Now;
                        bb.addedBy = MainClass.userID;

                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        db.Cars.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }
                if (change == "edit")
                {
                    using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                    {
                        int bbID = int.Parse(this.lblID.Text);
                        Car bb = (from c in db.Cars
                                  where c.carID ==
                                      bbID
                                  select c).FirstOrDefault();

                        bb.car_Brand = txtcar_Brand.Text;
                        bb.car_ChesseeNo = txtcar_ChesseeNo.Text;
                        bb.car_isAvailable = chkcar_isAvailable.Checked;
                        bb.car_Model = txtcar_Model.Text;
                        bb.car_Name = txtcar_Name.Text;
                        bb.car_fileNo = txtcar_fileNo.Text;
                        bb.car_No = txtcar_No.Text;
                        bb.edited_Date = DateTime.Now;
                        bb.editedBy = MainClass.userID;

                        //db.Cars.AddObject(bb);
                        rowsAffected = db.SaveChanges();
                    }
                }


                if (rowsAffected > 0)
                {
                    disableControls();

                    getData();
                    if (change == "add")
                    {
                        position = lstCar.Count - 1;
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
            CarsM.frmfindCars frm = new CarsM.frmfindCars(false);
            frm.isAllCar = true;
            frm.ShowDialog();
            if (frm.found)
            {
                Car cl = this.lstCar.Find(c => c.carID == frm.carID);
                position = lstCar.IndexOf(cl);
                bindData();
            }
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
            if (position <lstCar.Count -1)
            {
                position += 1;
                bindData();
            }
        }

        void btnLast_Click(object sender, EventArgs e)
        {
            if (position <lstCar.Count -1)
            {
                position = lstCar.Count - 1;
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

            txtcar_Brand.ReadOnly = true;
            txtcar_ChesseeNo.ReadOnly = true;
            chkcar_isAvailable.Enabled = false;
            txtcar_Model.ReadOnly = true;
            txtcar_Name.ReadOnly = true;
            txtcar_fileNo.ReadOnly = true;
            txtcar_No.ReadOnly = true;


            GView.ReadOnly = false;
            GView.Enabled = true;
            enableAllPages();
        }

        private void enableControls()
        {
            mainbar.disableButton();

            txtcar_Brand.ReadOnly = false;
            txtcar_ChesseeNo.ReadOnly = false;
            chkcar_isAvailable.Enabled = true;
            txtcar_Model.ReadOnly = false;
            txtcar_Name.ReadOnly = false;
            txtcar_fileNo.ReadOnly = false;
            txtcar_No.ReadOnly = false;

            GView.ReadOnly = true;
            GView.Enabled = false;
            disableAllPages();
            pageCar.Enabled = true;
        }


        private void getData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstCar = (from c in db.Cars  select c).ToList();
                GView.DataSource = lstCar;
            }
        }

        private void bindData()
        {
            if (lstCar.Count > 0)
            {
                txtcar_Brand.Text = lstCar[position].car_Brand;
                txtcar_ChesseeNo.Text = lstCar[position].car_ChesseeNo;
                chkcar_isAvailable.Checked = lstCar[position].car_isAvailable.Value;
                txtcar_Model.Text = lstCar[position].car_Model;
                txtcar_Name.Text = lstCar[position].car_Name;
                txtcar_fileNo.Text = lstCar[position].car_fileNo;
                lblCarName.Text = lstCar[position].car_Name;
                txtcar_No.Text = lstCar[position].car_No;

                this.lblID.Text = lstCar[position].carID.ToString();

                mainbar.lblCount.Text = position + 1 + " / " + lstCar.Count;
                GView.Rows[position].IsSelected = true;
            }
            else
            {
                txtcar_Brand.Text = "";
                txtcar_ChesseeNo.Text = "";
                chkcar_isAvailable.Checked = true;
                txtcar_Model.Text = "";
                txtcar_Name.Text = "";
                txtcar_fileNo.Text = "";
                lblCarName.Text = "";
                txtcar_No.Text = "";

                this.lblID.Text = "-1";

                mainbar.lblCount.Text = " 0 / 0";
                GView.ClearSelection();
            }

            getMaxEmpNo();
            getDocData();
            bindDocData1();
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
                Car car = lstCar.Find(b => b.carID == bID);

                position = lstCar.IndexOf(car);
                bindData();
            }
        }

        

        // Start Document Process

        public void refreshDocument()
        {
            getDocData();
            bindDocData1();
        }
        void btnAdd_ClickDoc(object sender, EventArgs e)
        {
            if (lblID.Text != "-1")
            {
                BarakalAlkheir.Docs.frmDocuments frm = new Docs.frmDocuments();
                frm.documentTypeDefID = int.Parse(lbldocumentTypeDefID.Text);
                frm.document_TypeID = int.Parse(lblID.Text);
                frm.ShowDialog();
                //frm.btnAdd_Click(sender, e);
                refreshDocument();
                //getDocData();
                //bindDocData1();
            }
        }

        void btnEdit_ClickDoc(object sender, EventArgs e)
        {
            if (lblID.Text != "-1" && lblDocumentID.Text != "-1")
            {
                BarakalAlkheir.Docs.frmDocuments frm = new Docs.frmDocuments();
                frm.documentTypeDefID = int.Parse(lbldocumentTypeDefID.Text);
                frm.document_TypeID = int.Parse(lblID.Text);
                frm.documentID = int.Parse(lblDocumentID.Text);
                frm.action = "edit";
                frm.ShowDialog();
                //  frm.btnEdit_Click(sender, e);
                //getDocData();
                //bindDocData1();
                refreshDocument();
            }
        }

        void btnDelete_ClickDoc(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text != "-1")
                {
                    DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "Confirmation Of Deleting", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
                        {

                            int bbID = int.Parse(lblDocumentID.Text);
                            Document bb = (from c in db.Documents
                                           where c.documentID ==
                                               bbID
                                           select c).FirstOrDefault();

                            db.Documents.DeleteObject(bb);
                            int noOfAffectedRows = db.SaveChanges();
                            if (noOfAffectedRows > 0)
                            {
                                getDocData();
                                bindDocData1();
                                refreshDocument();
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

        void btnFirst_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument > 0)
            {
                positionDocument = 0;
                bindDocData1();
            }
        }

        void btnPrevious_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument > 0)
            {
                positionDocument -= 1;
                bindDocData1();
            }
        }

        void btnNext_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument < lstDocument.Count - 1)
            {
                positionDocument += 1;
                bindDocData1();
            }
        }

        void btnLast_ClickDoc(object sender, EventArgs e)
        {
            if (positionDocument < lstDocument.Count - 1)
            {
                positionDocument = lstDocument.Count - 1;
                bindDocData1();
            }
        }

        void btnExit_ClickDoc(object sender, EventArgs e)
        {
            Close();
        }


        private void getDocData()
        {
            using (BarakeitAlkheirEntities db = new BarakeitAlkheirEntities())
            {
                lstDocument = new List<Document>();
                lblDocumentID.Text = "-1";
                int carID = int.Parse(lblID.Text);

                var val = (from c in db.DocumentTypeDefinitions
                           where c.documentType_Code == lbldocumentType_Code.Text
                           select c).FirstOrDefault();

                if (val != null)
                    lbldocumentTypeDefID.Text = val.documentTypeDefID.ToString();


                lstDocument = (from c in db.Documents
                               where c.document_TypeID == carID &&
                               c.DocumentDef.documentTypeDefID == 4                        
                               select c).ToList();


                var val1 = (from c in db.Documents
                            where c.document_TypeID == carID &&
                               c.DocumentDef.documentTypeDefID == 4  
                            select new
                            {
                                c.documentID,
                                c.DocumentDef.documentDef_Name,
                                c.document_No,
                                c.document_startDate,
                                c.document_endDate
                            }).ToList();

                gvDocument.DataSource = val1;
            }
        }

        private void bindDocData1()
        {
            if (lstDocument.Count > 0)
            {
                if (lblDocumentID.Text == "-1")
                {
                    lblDocumentID.Text = lstDocument[0].documentID.ToString();
                    gvDocument.Rows[0].IsSelected = true;
                }
                else
                {
                    int IDD = int.Parse(lblDocumentID.Text);
                    Document dc = lstDocument.Find(cc => cc.documentID == IDD);

                    positionDocument = lstDocument.FindIndex(cc => cc.documentID == IDD);
                    gvDocument.Rows[positionDocument].IsSelected = true;
                }

                mainbarDocument.lblCount.Text = positionDocument + 1 + " / " + lstDocument.Count;

            }
            else
            {
                lblDocumentID.Text = "-1";
                mainbarDocument.lblCount.Text = " 0 / 0";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //refreshDocument();
        }

        private void gvDocument_Click(object sender, EventArgs e)
        {
            if (gvDocument.Rows.Count > 0)
            {
                lblDocumentID.Text = gvDocument.CurrentRow.Cells[0].Value.ToString();
            }
        }

        // end of document Process



    }
}
