namespace BarakalAlkheir.CarsM
{
    partial class frmfindCars
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.txtcar_No = new Telerik.WinControls.UI.RadTextBox();
            this.txtcar_Name = new Telerik.WinControls.UI.RadTextBox();
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
            this.txtcar_Brand = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcar_No)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcar_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcar_Brand)).BeginInit();
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
            this.radPanel1.Text = "البحث عن سيارة";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(631, 54);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(65, 21);
            this.radLabel11.TabIndex = 6;
            this.radLabel11.Text = "رقم السيارة";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(631, 78);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(38, 21);
            this.radLabel5.TabIndex = 7;
            this.radLabel5.Text = "الاسم ";
            // 
            // txtcar_No
            // 
            this.txtcar_No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcar_No.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcar_No.Location = new System.Drawing.Point(347, 54);
            this.txtcar_No.Name = "txtcar_No";
            this.txtcar_No.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcar_No.Size = new System.Drawing.Size(276, 23);
            this.txtcar_No.TabIndex = 0;
            this.txtcar_No.TabStop = false;
            this.txtcar_No.TextChanged += new System.EventHandler(this.txtlicenseNo_TextChanged);
            // 
            // txtcar_Name
            // 
            this.txtcar_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcar_Name.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcar_Name.Location = new System.Drawing.Point(347, 78);
            this.txtcar_Name.Name = "txtcar_Name";
            this.txtcar_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcar_Name.Size = new System.Drawing.Size(276, 23);
            this.txtcar_Name.TabIndex = 1;
            this.txtcar_Name.TabStop = false;
            this.txtcar_Name.TextChanged += new System.EventHandler(this.txtcommercialName_TextChanged);
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
            gridViewTextBoxColumn1.FieldName = "carID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "gvcarID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "car_No";
            gridViewTextBoxColumn2.HeaderText = "رقم السيارة";
            gridViewTextBoxColumn2.Name = "gvcar_No";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 100;
            gridViewTextBoxColumn3.FieldName = "car_Name";
            gridViewTextBoxColumn3.HeaderText = "الاسم";
            gridViewTextBoxColumn3.Name = "gvcar_Name";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 150;
            gridViewTextBoxColumn4.FieldName = "car_Brand";
            gridViewTextBoxColumn4.HeaderText = "ماركة";
            gridViewTextBoxColumn4.Name = "GVcar_Brand";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 150;
            gridViewTextBoxColumn5.FieldName = "car_Model";
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "موديل";
            gridViewTextBoxColumn5.Name = "gvcar_Model";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 100;
            gridViewTextBoxColumn6.FieldName = "car_ChesseeNo";
            gridViewTextBoxColumn6.FormatString = "";
            gridViewTextBoxColumn6.HeaderText = "رقم المحرك";
            gridViewTextBoxColumn6.Name = "gvcar_ChesseeNo";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.Width = 150;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
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
            // radLabel21
            // 
            this.radLabel21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel21.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel21.Location = new System.Drawing.Point(631, 102);
            this.radLabel21.Name = "radLabel21";
            this.radLabel21.Size = new System.Drawing.Size(43, 21);
            this.radLabel21.TabIndex = 8;
            this.radLabel21.Text = "الماركة";
            // 
            // txtcar_Brand
            // 
            this.txtcar_Brand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcar_Brand.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcar_Brand.Location = new System.Drawing.Point(347, 102);
            this.txtcar_Brand.Name = "txtcar_Brand";
            this.txtcar_Brand.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcar_Brand.Size = new System.Drawing.Size(276, 23);
            this.txtcar_Brand.TabIndex = 2;
            this.txtcar_Brand.TabStop = false;
            this.txtcar_Brand.TextChanged += new System.EventHandler(this.txtlicenseLinkCode_TextChanged);
            // 
            // frmfindCars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 532);
            this.Controls.Add(this.radLabel21);
            this.Controls.Add(this.txtcar_Brand);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radLabel11);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.txtcar_No);
            this.Controls.Add(this.txtcar_Name);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmfindCars";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "find a Car";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmfindLicences_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcar_No)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcar_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcar_Brand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadTextBox txtcar_No;
        private Telerik.WinControls.UI.RadTextBox txtcar_Name;
        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadLabel radLabel21;
        private Telerik.WinControls.UI.RadTextBox txtcar_Brand;
    }
}
