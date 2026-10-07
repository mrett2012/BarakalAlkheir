namespace BarakalAlkheir.Docs
{
    partial class frmTransactionDocumentsTasks
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
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
            this.radPanel1.Size = new System.Drawing.Size(1071, 38);
            this.radPanel1.TabIndex = 1;
            this.radPanel1.Text = "اجراءات الوثائق الواجبة";
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
            gridViewTextBoxColumn1.FieldName = "documentTypeDefID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "documentTypeDefID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVdocumentTypeDefID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 44;
            gridViewTextBoxColumn2.FieldName = "documentDefID";
            gridViewTextBoxColumn2.HeaderText = "documentDefID";
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "gvdocumentDefID";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn3.FieldName = "companyLicencesID";
            gridViewTextBoxColumn3.HeaderText = "companyLicencesID";
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "gvcompanyLicencesID";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "documentID";
            gridViewTextBoxColumn4.HeaderText = "documentID";
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "gvdocumentID";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn5.FieldName = "documentDef_Name";
            gridViewTextBoxColumn5.HeaderText = "نو ع الوثيقة";
            gridViewTextBoxColumn5.Name = "gvdocumentDef_Name";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 150;
            gridViewTextBoxColumn6.FieldName = "licenseNo";
            gridViewTextBoxColumn6.HeaderText = "رقم الرخصة";
            gridViewTextBoxColumn6.Name = "gvlicenseNo";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.Width = 100;
            gridViewTextBoxColumn7.FieldName = "licenseUniqueNo";
            gridViewTextBoxColumn7.HeaderText = "رقم الموحد";
            gridViewTextBoxColumn7.Name = "gvlicenseUniqueNo";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.Width = 100;
            gridViewTextBoxColumn8.FieldName = "commercialName";
            gridViewTextBoxColumn8.HeaderText = "اسم الرخصة";
            gridViewTextBoxColumn8.Name = "gvcommercialName";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn8.Width = 300;
            gridViewTextBoxColumn9.FieldName = "document_endDate";
            gridViewTextBoxColumn9.FormatString = AppDateFormat.GridFormat;
            gridViewTextBoxColumn9.FormatInfo = AppDateFormat.Culture;
            gridViewTextBoxColumn9.HeaderText = "تاريخ الانتهاء";
            gridViewTextBoxColumn9.Name = "gvdocument_endDate";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn9.Width = 100;
            gridViewTextBoxColumn10.FieldName = "noOfDays";
            gridViewTextBoxColumn10.HeaderText = "المتبقي";
            gridViewTextBoxColumn10.Name = "gvnoOfDays";
            gridViewTextBoxColumn10.ReadOnly = true;
            gridViewTextBoxColumn10.Width = 100;
            gridViewTextBoxColumn11.HeaderText = "الاجراء المتخذ";
            gridViewTextBoxColumn11.Name = "gvActionTaken";
            gridViewTextBoxColumn11.Width = 100;
            gridViewTextBoxColumn12.HeaderText = "المبلغ";
            gridViewTextBoxColumn12.Name = "GVAmount";
            gridViewCommandColumn1.DefaultText = "استلام الامر";
            gridViewCommandColumn1.HeaderText = "ادفع ؟";
            gridViewCommandColumn1.Name = "GVCommand";
            gridViewCommandColumn1.UseDefaultText = true;
            gridViewCommandColumn1.Width = 150;
            gridViewTextBoxColumn13.FieldName = "documentTransTaskID";
            gridViewTextBoxColumn13.HeaderText = "documentTransTaskID";
            gridViewTextBoxColumn13.IsVisible = false;
            gridViewTextBoxColumn13.Name = "gvdocumentTransTaskID";
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12,
            gridViewCommandColumn1,
            gridViewTextBoxColumn13});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(1071, 451);
            this.GView.TabIndex = 3;
            this.GView.Click += new System.EventHandler(this.GView_Click);
            this.GView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GView_MouseDown);
            // 
            // frmTransactionDocumentsTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 489);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "frmTransactionDocumentsTasks";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documents Transaction Tasks";
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
