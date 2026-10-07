namespace BarakalAlkheir.Definitions
{
    partial class frmCustomers
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
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.txtcustomer_CivilID = new Telerik.WinControls.UI.RadTextBox();
            this.txtcustomer_Name = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.txtcustomer_BirthDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.cmbcustomer_nationalityID = new Telerik.WinControls.UI.RadDropDownList();
            this.chkcustomer_active = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtcustomer_Address = new Telerik.WinControls.UI.RadTextBox();
            this.txtcustomer_phoneNumber2 = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtcustomer_phoneNumber1 = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lblID = new Telerik.WinControls.UI.RadLabel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_CivilID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_BirthDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbcustomer_nationalityID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcustomer_active)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_Address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_phoneNumber2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_phoneNumber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(47, 220);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "customerID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "customerID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVcustomerID";
            gridViewTextBoxColumn1.Width = 44;
            gridViewTextBoxColumn2.FieldName = "customer_Name";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "الاسم";
            gridViewTextBoxColumn2.Name = "GVcustomer_Name";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 240;
            gridViewTextBoxColumn3.FieldName = "customer_CivilID";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "رقم المدني";
            gridViewTextBoxColumn3.Name = "GVcustomer_CivilID";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 150;
            gridViewTextBoxColumn4.FieldName = "customer_phoneNumber1";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "رقم الهاتف";
            gridViewTextBoxColumn4.Name = "GVcustomer_phoneNumber1";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 150;
            gridViewTextBoxColumn5.FieldName = "customer_phoneNumber2";
            gridViewTextBoxColumn5.HeaderText = "رقم الفاكس";
            gridViewTextBoxColumn5.Name = "GVcustomer_phoneNumber2";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 150;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(739, 364);
            this.GView.TabIndex = 1;
            this.GView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GView_CellClick);
            this.GView.Click += new System.EventHandler(this.GView_Click);
            // 
            // txtcustomer_CivilID
            // 
            this.txtcustomer_CivilID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_CivilID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_CivilID.Location = new System.Drawing.Point(422, 38);
            this.txtcustomer_CivilID.Name = "txtcustomer_CivilID";
            this.txtcustomer_CivilID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_CivilID.Size = new System.Drawing.Size(225, 23);
            this.txtcustomer_CivilID.TabIndex = 1;
            this.txtcustomer_CivilID.TabStop = false;
            // 
            // txtcustomer_Name
            // 
            this.txtcustomer_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_Name.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_Name.Location = new System.Drawing.Point(422, 12);
            this.txtcustomer_Name.Name = "txtcustomer_Name";
            this.txtcustomer_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_Name.Size = new System.Drawing.Size(225, 23);
            this.txtcustomer_Name.TabIndex = 0;
            this.txtcustomer_Name.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(652, 38);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(61, 21);
            this.radLabel15.TabIndex = 9;
            this.radLabel15.Text = "رقم المدني";
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(680, 12);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(33, 21);
            this.radLabel11.TabIndex = 8;
            this.radLabel11.Text = "الاسم";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(666, 64);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(47, 21);
            this.radLabel5.TabIndex = 10;
            this.radLabel5.Text = "الجنسية";
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.txtcustomer_BirthDate);
            this.radPanel2.Controls.Add(this.cmbcustomer_nationalityID);
            this.radPanel2.Controls.Add(this.chkcustomer_active);
            this.radPanel2.Controls.Add(this.radLabel3);
            this.radPanel2.Controls.Add(this.txtcustomer_Address);
            this.radPanel2.Controls.Add(this.txtcustomer_phoneNumber2);
            this.radPanel2.Controls.Add(this.radLabel11);
            this.radPanel2.Controls.Add(this.radLabel2);
            this.radPanel2.Controls.Add(this.txtcustomer_CivilID);
            this.radPanel2.Controls.Add(this.txtcustomer_phoneNumber1);
            this.radPanel2.Controls.Add(this.radLabel4);
            this.radPanel2.Controls.Add(this.radLabel5);
            this.radPanel2.Controls.Add(this.radLabel6);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.txtcustomer_Name);
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Location = new System.Drawing.Point(47, 81);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(739, 140);
            this.radPanel2.TabIndex = 0;
            // 
            // txtcustomer_BirthDate
            // 
            this.txtcustomer_BirthDate.Culture = AppDateFormat.Culture;
            this.txtcustomer_BirthDate.CustomFormat = AppDateFormat.PickerFormat;
            this.txtcustomer_BirthDate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtcustomer_BirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtcustomer_BirthDate.Location = new System.Drawing.Point(73, 12);
            this.txtcustomer_BirthDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.txtcustomer_BirthDate.MinDate = new System.DateTime(((long)(0)));
            this.txtcustomer_BirthDate.Name = "txtcustomer_BirthDate";
            this.txtcustomer_BirthDate.NullableValue = new System.DateTime(2012, 5, 14, 14, 11, 59, 186);
            this.txtcustomer_BirthDate.NullDate = new System.DateTime(((long)(0)));
            this.txtcustomer_BirthDate.Size = new System.Drawing.Size(225, 23);
            this.txtcustomer_BirthDate.TabIndex = 3;
            this.txtcustomer_BirthDate.TabStop = false;
            this.txtcustomer_BirthDate.Text = "14/05/2012";
            this.txtcustomer_BirthDate.Value = new System.DateTime(2012, 5, 14, 14, 11, 59, 186);
            this.txtcustomer_BirthDate.Leave += new System.EventHandler(this.txtcustomer_BirthDate_Leave);
            // 
            // cmbcustomer_nationalityID
            // 
            this.cmbcustomer_nationalityID.DropDownAnimationEnabled = true;
            this.cmbcustomer_nationalityID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbcustomer_nationalityID.Location = new System.Drawing.Point(420, 64);
            this.cmbcustomer_nationalityID.Name = "cmbcustomer_nationalityID";
            this.cmbcustomer_nationalityID.ShowImageInEditorArea = true;
            this.cmbcustomer_nationalityID.Size = new System.Drawing.Size(227, 23);
            this.cmbcustomer_nationalityID.TabIndex = 2;
            // 
            // chkcustomer_active
            // 
            this.chkcustomer_active.Location = new System.Drawing.Point(628, 119);
            this.chkcustomer_active.Name = "chkcustomer_active";
            this.chkcustomer_active.Size = new System.Drawing.Size(15, 15);
            this.chkcustomer_active.TabIndex = 7;
            // 
            // radLabel3
            // 
            this.radLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(310, 38);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(62, 21);
            this.radLabel3.TabIndex = 12;
            this.radLabel3.Text = "رقم الهاتف";
            // 
            // txtcustomer_Address
            // 
            this.txtcustomer_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_Address.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_Address.Location = new System.Drawing.Point(72, 90);
            this.txtcustomer_Address.Name = "txtcustomer_Address";
            this.txtcustomer_Address.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_Address.Size = new System.Drawing.Size(575, 23);
            this.txtcustomer_Address.TabIndex = 6;
            this.txtcustomer_Address.TabStop = false;
            // 
            // txtcustomer_phoneNumber2
            // 
            this.txtcustomer_phoneNumber2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_phoneNumber2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_phoneNumber2.Location = new System.Drawing.Point(73, 64);
            this.txtcustomer_phoneNumber2.Name = "txtcustomer_phoneNumber2";
            this.txtcustomer_phoneNumber2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_phoneNumber2.Size = new System.Drawing.Size(225, 23);
            this.txtcustomer_phoneNumber2.TabIndex = 5;
            this.txtcustomer_phoneNumber2.TabStop = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(682, 116);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(31, 21);
            this.radLabel2.TabIndex = 15;
            this.radLabel2.Text = "مفعل";
            // 
            // txtcustomer_phoneNumber1
            // 
            this.txtcustomer_phoneNumber1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_phoneNumber1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_phoneNumber1.Location = new System.Drawing.Point(73, 38);
            this.txtcustomer_phoneNumber1.Name = "txtcustomer_phoneNumber1";
            this.txtcustomer_phoneNumber1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_phoneNumber1.Size = new System.Drawing.Size(225, 23);
            this.txtcustomer_phoneNumber1.TabIndex = 4;
            this.txtcustomer_phoneNumber1.TabStop = false;
            // 
            // radLabel4
            // 
            this.radLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(302, 12);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(70, 21);
            this.radLabel4.TabIndex = 11;
            this.radLabel4.Text = "تاريخ الميلاد";
            // 
            // radLabel6
            // 
            this.radLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.Location = new System.Drawing.Point(670, 91);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(43, 21);
            this.radLabel6.TabIndex = 14;
            this.radLabel6.Text = "العتوان";
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(308, 64);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(64, 21);
            this.radLabel1.TabIndex = 13;
            this.radLabel1.Text = "رقم الفاكس";
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(792, 44);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 21);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "-1";
            this.lblID.Visible = false;
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(47, 44);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(739, 38);
            this.radPanel1.TabIndex = 2;
            this.radPanel1.Text = "العملاء";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 615);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmCustomers";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customers";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_CivilID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_BirthDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbcustomer_nationalityID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcustomer_active)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_Address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_phoneNumber2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_phoneNumber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_CivilID;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_Name;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel lblID;
        private Telerik.WinControls.UI.RadCheckBox chkcustomer_active;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_phoneNumber2;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_phoneNumber1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_Address;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadDropDownList cmbcustomer_nationalityID;
        private Telerik.WinControls.UI.RadDateTimePicker txtcustomer_BirthDate;
    }
}
