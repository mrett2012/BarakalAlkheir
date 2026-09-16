namespace BarakalAlkheir.Definitions
{
    partial class frmCarSuppliers
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
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.txtcarSuppliser_PersonInCharge = new Telerik.WinControls.UI.RadTextBox();
            this.txtcarSuppliser_Name = new Telerik.WinControls.UI.RadTextBox();
            this.txtcarSuppliser_Contact = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.lblID = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtcarSuppliser_remarks = new Telerik.WinControls.UI.RadTextBox();
            this.txtcarSuppliser_Address = new Telerik.WinControls.UI.RadTextBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_PersonInCharge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Contact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_remarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(38, 230);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "carSuppliserID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "carSuppliserID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVcarSuppliserID";
            gridViewTextBoxColumn1.Width = 44;
            gridViewTextBoxColumn2.FieldName = "carSuppliser_Name";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "الشركة";
            gridViewTextBoxColumn2.Name = "GVcarSuppliser_Name";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 250;
            gridViewTextBoxColumn3.FieldName = "carSuppliser_PersonInCharge";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "المسئول";
            gridViewTextBoxColumn3.Name = "GVcarSuppliser_PersonInCharge";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 250;
            gridViewTextBoxColumn4.FieldName = "carSuppliser_Contact";
            gridViewTextBoxColumn4.HeaderText = "الهاتف";
            gridViewTextBoxColumn4.Name = "GVcarSuppliser_Contact";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 180;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(714, 321);
            this.GView.TabIndex = 2;
            this.GView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GView_CellClick);
            this.GView.Click += new System.EventHandler(this.GView_Click);
            // 
            // txtcarSuppliser_PersonInCharge
            // 
            this.txtcarSuppliser_PersonInCharge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_PersonInCharge.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_PersonInCharge.Location = new System.Drawing.Point(273, 38);
            this.txtcarSuppliser_PersonInCharge.Name = "txtcarSuppliser_PersonInCharge";
            this.txtcarSuppliser_PersonInCharge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_PersonInCharge.Size = new System.Drawing.Size(349, 23);
            this.txtcarSuppliser_PersonInCharge.TabIndex = 1;
            this.txtcarSuppliser_PersonInCharge.TabStop = false;
            // 
            // txtcarSuppliser_Name
            // 
            this.txtcarSuppliser_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_Name.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_Name.Location = new System.Drawing.Point(273, 12);
            this.txtcarSuppliser_Name.Name = "txtcarSuppliser_Name";
            this.txtcarSuppliser_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_Name.Size = new System.Drawing.Size(349, 23);
            this.txtcarSuppliser_Name.TabIndex = 0;
            this.txtcarSuppliser_Name.TabStop = false;
            // 
            // txtcarSuppliser_Contact
            // 
            this.txtcarSuppliser_Contact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_Contact.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_Contact.Location = new System.Drawing.Point(273, 64);
            this.txtcarSuppliser_Contact.Name = "txtcarSuppliser_Contact";
            this.txtcarSuppliser_Contact.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_Contact.Size = new System.Drawing.Size(349, 23);
            this.txtcarSuppliser_Contact.TabIndex = 2;
            this.txtcarSuppliser_Contact.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(634, 38);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(48, 21);
            this.radLabel15.TabIndex = 9;
            this.radLabel15.Text = "المسئول";
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(639, 12);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(43, 21);
            this.radLabel11.TabIndex = 10;
            this.radLabel11.Text = "الشركة";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(641, 64);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(41, 21);
            this.radLabel5.TabIndex = 8;
            this.radLabel5.Text = "الهاتف";
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.lblID);
            this.radPanel2.Controls.Add(this.radLabel11);
            this.radPanel2.Controls.Add(this.txtcarSuppliser_PersonInCharge);
            this.radPanel2.Controls.Add(this.radLabel2);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.radLabel5);
            this.radPanel2.Controls.Add(this.txtcarSuppliser_remarks);
            this.radPanel2.Controls.Add(this.txtcarSuppliser_Name);
            this.radPanel2.Controls.Add(this.txtcarSuppliser_Address);
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Controls.Add(this.txtcarSuppliser_Contact);
            this.radPanel2.Location = new System.Drawing.Point(38, 81);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(714, 150);
            this.radPanel2.TabIndex = 1;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(88, 12);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 21);
            this.lblID.TabIndex = 5;
            this.lblID.Text = "-1";
            this.lblID.Visible = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(630, 118);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(52, 21);
            this.radLabel2.TabIndex = 6;
            this.radLabel2.Text = "ملاحظات";
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(639, 91);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(43, 21);
            this.radLabel1.TabIndex = 7;
            this.radLabel1.Text = "العنوان";
            // 
            // txtcarSuppliser_remarks
            // 
            this.txtcarSuppliser_remarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_remarks.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_remarks.Location = new System.Drawing.Point(42, 118);
            this.txtcarSuppliser_remarks.Name = "txtcarSuppliser_remarks";
            this.txtcarSuppliser_remarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_remarks.Size = new System.Drawing.Size(580, 23);
            this.txtcarSuppliser_remarks.TabIndex = 4;
            this.txtcarSuppliser_remarks.TabStop = false;
            // 
            // txtcarSuppliser_Address
            // 
            this.txtcarSuppliser_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcarSuppliser_Address.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcarSuppliser_Address.Location = new System.Drawing.Point(42, 91);
            this.txtcarSuppliser_Address.Name = "txtcarSuppliser_Address";
            this.txtcarSuppliser_Address.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcarSuppliser_Address.Size = new System.Drawing.Size(580, 23);
            this.txtcarSuppliser_Address.TabIndex = 3;
            this.txtcarSuppliser_Address.TabStop = false;
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(38, 44);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(714, 38);
            this.radPanel1.TabIndex = 0;
            this.radPanel1.Text = "شركات السيارات";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCarSuppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 593);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmCarSuppliers";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Car Suppliers";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmCarSuplliers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_PersonInCharge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Contact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_remarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcarSuppliser_Address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_PersonInCharge;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_Name;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_Contact;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel lblID;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_Address;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtcarSuppliser_remarks;
    }
}
