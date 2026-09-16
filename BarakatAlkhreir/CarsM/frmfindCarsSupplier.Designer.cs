namespace BarakalAlkheir.CarsM
{
    partial class frmfindCarsSupplier
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.txtcarSuppliser_Name = new Telerik.WinControls.UI.RadTextBox();
            this.txtcarSuppliser_PersonInCharge = new Telerik.WinControls.UI.RadTextBox();
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.txtcarSuppliser_Contact = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_PersonInCharge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Contact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
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
            this.radPanel1.Size = new System.Drawing.Size(734, 38);
            this.radPanel1.TabIndex = 9;
            this.radPanel1.Text = "البحث عن شركة السيارات";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtcarSuppliser_Name
            // 
            this.txtcarSuppliser_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_Name.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_Name.Location = new System.Drawing.Point(347, 54);
            this.txtcarSuppliser_Name.Name = "txtcarSuppliser_Name";
            this.txtcarSuppliser_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_Name.Size = new System.Drawing.Size(276, 23);
            this.txtcarSuppliser_Name.TabIndex = 0;
            this.txtcarSuppliser_Name.TabStop = false;
            this.txtcarSuppliser_Name.TextChanged += new System.EventHandler(this.txtlicenseNo_TextChanged);
            // 
            // txtcarSuppliser_PersonInCharge
            // 
            this.txtcarSuppliser_PersonInCharge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_PersonInCharge.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_PersonInCharge.Location = new System.Drawing.Point(347, 78);
            this.txtcarSuppliser_PersonInCharge.Name = "txtcarSuppliser_PersonInCharge";
            this.txtcarSuppliser_PersonInCharge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_PersonInCharge.Size = new System.Drawing.Size(276, 23);
            this.txtcarSuppliser_PersonInCharge.TabIndex = 1;
            this.txtcarSuppliser_PersonInCharge.TabStop = false;
            this.txtcarSuppliser_PersonInCharge.TextChanged += new System.EventHandler(this.txtcommercialName_TextChanged);
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(23, 134);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "carSuppliserID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "gvcarSuppliserID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "carSuppliser_Name";
            gridViewTextBoxColumn2.HeaderText = "الاسم";
            gridViewTextBoxColumn2.Name = "gvcarSuppliser_Name";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 250;
            gridViewTextBoxColumn3.FieldName = "carSuppliser_PersonInCharge";
            gridViewTextBoxColumn3.HeaderText = "المسئول";
            gridViewTextBoxColumn3.Name = "GVcarSuppliser_PersonInCharge";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 200;
            gridViewTextBoxColumn4.FieldName = "carSuppliser_Contact";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "الهاتف";
            gridViewTextBoxColumn4.Name = "gvcarSuppliser_Contact";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 150;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(685, 336);
            this.GView.TabIndex = 3;
            this.GView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GView_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(578, 484);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(130, 30);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(430, 484);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "اغلاق";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtcarSuppliser_Contact
            // 
            this.txtcarSuppliser_Contact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_Contact.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_Contact.Location = new System.Drawing.Point(347, 102);
            this.txtcarSuppliser_Contact.Name = "txtcarSuppliser_Contact";
            this.txtcarSuppliser_Contact.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_Contact.Size = new System.Drawing.Size(276, 23);
            this.txtcarSuppliser_Contact.TabIndex = 2;
            this.txtcarSuppliser_Contact.TabStop = false;
            this.txtcarSuppliser_Contact.TextChanged += new System.EventHandler(this.txtlicenseLinkCode_TextChanged);
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(638, 54);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(43, 21);
            this.radLabel11.TabIndex = 13;
            this.radLabel11.Text = "الشركة";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(640, 106);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(41, 21);
            this.radLabel5.TabIndex = 11;
            this.radLabel5.Text = "الهاتف";
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(633, 80);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(48, 21);
            this.radLabel15.TabIndex = 12;
            this.radLabel15.Text = "المسئول";
            // 
            // frmfindCarsSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 532);
            this.Controls.Add(this.radLabel11);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.radLabel15);
            this.Controls.Add(this.txtcarSuppliser_Contact);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.txtcarSuppliser_Name);
            this.Controls.Add(this.txtcarSuppliser_PersonInCharge);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmfindCarsSupplier";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "find a Car Supplier";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmfindLicences_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_PersonInCharge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Contact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_Name;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_PersonInCharge;
        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_Contact;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel15;
    }
}
