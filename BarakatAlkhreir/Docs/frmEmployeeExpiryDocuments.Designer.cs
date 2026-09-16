namespace BarakalAlkheir.Docs
{
    partial class frmEmployeeExpiryDocuments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn2 = new Telerik.WinControls.UI.GridViewCommandColumn();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.GView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(941, 38);
            this.radPanel1.TabIndex = 1;
            this.radPanel1.Text = "وثائق الموظفين الواجب تجديدها";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GView
            // 
            this.GView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(0, 38);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn14.FieldName = "documentTypeDefID";
            gridViewTextBoxColumn14.FormatString = "";
            gridViewTextBoxColumn14.HeaderText = "documentTypeDefID";
            gridViewTextBoxColumn14.IsVisible = false;
            gridViewTextBoxColumn14.Name = "GVdocumentTypeDefID";
            gridViewTextBoxColumn14.ReadOnly = true;
            gridViewTextBoxColumn14.Width = 44;
            gridViewTextBoxColumn15.FieldName = "documentDefID";
            gridViewTextBoxColumn15.HeaderText = "documentDefID";
            gridViewTextBoxColumn15.IsVisible = false;
            gridViewTextBoxColumn15.Name = "gvdocumentDefID";
            gridViewTextBoxColumn15.ReadOnly = true;
            gridViewTextBoxColumn16.FieldName = "companyLicencesID";
            gridViewTextBoxColumn16.HeaderText = "companyLicencesID";
            gridViewTextBoxColumn16.IsVisible = false;
            gridViewTextBoxColumn16.Name = "gvcompanyLicencesID";
            gridViewTextBoxColumn16.ReadOnly = true;
            gridViewTextBoxColumn17.FieldName = "documentID";
            gridViewTextBoxColumn17.HeaderText = "documentID";
            gridViewTextBoxColumn17.IsVisible = false;
            gridViewTextBoxColumn17.Name = "gvdocumentID";
            gridViewTextBoxColumn17.ReadOnly = true;
            gridViewTextBoxColumn18.FieldName = "employeeID";
            gridViewTextBoxColumn18.HeaderText = "employeeID";
            gridViewTextBoxColumn18.IsVisible = false;
            gridViewTextBoxColumn18.Name = "gvemployeeID";
            gridViewTextBoxColumn18.ReadOnly = true;
            gridViewTextBoxColumn19.FieldName = "documentDef_Name";
            gridViewTextBoxColumn19.HeaderText = "نو ع الوثيقة";
            gridViewTextBoxColumn19.Name = "gvdocumentDef_Name";
            gridViewTextBoxColumn19.ReadOnly = true;
            gridViewTextBoxColumn19.Width = 100;
            gridViewTextBoxColumn20.FieldName = "emp_No";
            gridViewTextBoxColumn20.HeaderText = "رقم الموظف";
            gridViewTextBoxColumn20.Name = "gvemp_No";
            gridViewTextBoxColumn20.ReadOnly = true;
            gridViewTextBoxColumn20.Width = 70;
            gridViewTextBoxColumn21.FieldName = "emp_namefullar";
            gridViewTextBoxColumn21.HeaderText = "اسم الموظف";
            gridViewTextBoxColumn21.Name = "gvemp_namefullar";
            gridViewTextBoxColumn21.ReadOnly = true;
            gridViewTextBoxColumn21.Width = 180;
            gridViewTextBoxColumn22.FieldName = "commercialName";
            gridViewTextBoxColumn22.HeaderText = "اسم مكان العمل";
            gridViewTextBoxColumn22.Name = "gvcommercialName";
            gridViewTextBoxColumn22.ReadOnly = true;
            gridViewTextBoxColumn22.Width = 280;
            gridViewTextBoxColumn23.FieldName = "document_endDate";
            gridViewTextBoxColumn23.FormatString = "{0:dd/MM/yyyy}";
            gridViewTextBoxColumn23.HeaderText = "تاريخ الانتهاء";
            gridViewTextBoxColumn23.Name = "gvdocument_endDate";
            gridViewTextBoxColumn23.ReadOnly = true;
            gridViewTextBoxColumn23.Width = 100;
            gridViewTextBoxColumn24.FieldName = "noOfDays";
            gridViewTextBoxColumn24.HeaderText = "المتبقي";
            gridViewTextBoxColumn24.Name = "gvnoOfDays";
            gridViewTextBoxColumn24.ReadOnly = true;
            gridViewTextBoxColumn24.Width = 100;
            gridViewTextBoxColumn25.HeaderText = "الاجراء المتخذ";
            gridViewTextBoxColumn25.Name = "gvActionTaken";
            gridViewTextBoxColumn25.Width = 100;
            gridViewTextBoxColumn26.HeaderText = "ملاحظات";
            gridViewTextBoxColumn26.Name = "gvRemarks";
            gridViewTextBoxColumn26.Width = 150;
            gridViewCommandColumn2.DefaultText = "إلغاء";
            gridViewCommandColumn2.HeaderText = "إلغاء";
            gridViewCommandColumn2.Name = "gvCancel";
            gridViewCommandColumn2.UseDefaultText = true;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn14,
            gridViewTextBoxColumn15,
            gridViewTextBoxColumn16,
            gridViewTextBoxColumn17,
            gridViewTextBoxColumn18,
            gridViewTextBoxColumn19,
            gridViewTextBoxColumn20,
            gridViewTextBoxColumn21,
            gridViewTextBoxColumn22,
            gridViewTextBoxColumn23,
            gridViewTextBoxColumn24,
            gridViewTextBoxColumn25,
            gridViewTextBoxColumn26,
            gridViewCommandColumn2});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(941, 446);
            this.GView.TabIndex = 3;
            this.GView.Click += new System.EventHandler(this.GView_Click);
            this.GView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GView_KeyDown);
            this.GView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GView_KeyUp);
            this.GView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GView_MouseDown);
            // 
            // frmEmployeeExpiryDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 484);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "frmEmployeeExpiryDocuments";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Expiry Documents";
            this.ThemeName = "ControlDefault";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEmployeeExpiryDocuments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadGridView GView;
    }
}
