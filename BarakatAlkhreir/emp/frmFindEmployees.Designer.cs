namespace BarakalAlkheir.emp
{
    partial class frmFindEmployees
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.txtemp_No = new Telerik.WinControls.UI.RadTextBox();
            this.txtemp_civilID = new Telerik.WinControls.UI.RadTextBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.txtemp_namefullar = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.cmbnationalityID = new Telerik.WinControls.UI.RadDropDownList();
            this.cmbGender = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel22 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
            this.chkemp_active = new Telerik.WinControls.UI.RadCheckBox();
            this.chkemp_isMandoub = new Telerik.WinControls.UI.RadCheckBox();
            this.chkemp_Notactive = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemp_No)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemp_civilID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemp_namefullar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbnationalityID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkemp_active)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkemp_isMandoub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkemp_Notactive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(552, 520);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "اغلاق";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(700, 520);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(130, 30);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(9, 145);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "employeeID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "gvemployeeID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "emp_No";
            gridViewTextBoxColumn2.HeaderText = "رقم الموظف";
            gridViewTextBoxColumn2.Name = "gvemp_No";
            gridViewTextBoxColumn2.Width = 70;
            gridViewTextBoxColumn3.FieldName = "emp_civilID";
            gridViewTextBoxColumn3.HeaderText = "الرقم المدني";
            gridViewTextBoxColumn3.Name = "gvemp_civilID";
            gridViewTextBoxColumn3.Width = 120;
            gridViewTextBoxColumn4.FieldName = "emp_namefullar";
            gridViewTextBoxColumn4.HeaderText = "الاسم";
            gridViewTextBoxColumn4.Name = "gvemp_namefullar";
            gridViewTextBoxColumn4.Width = 200;
            gridViewTextBoxColumn5.FieldName = "Nationality.natio_namea";
            gridViewTextBoxColumn5.HeaderText = "الجنسية";
            gridViewTextBoxColumn5.Name = "gvNatiolity";
            gridViewTextBoxColumn5.Width = 100;
            gridViewTextBoxColumn6.FieldName = "Gender.gender_namea";
            gridViewTextBoxColumn6.HeaderText = "الجنس";
            gridViewTextBoxColumn6.Name = "gvGender";
            gridViewTextBoxColumn6.Width = 100;
            gridViewCheckBoxColumn1.FieldName = "emp_isMandoub";
            gridViewCheckBoxColumn1.HeaderText = "مندوب";
            gridViewCheckBoxColumn1.Name = "gvIsMandoub";
            gridViewCheckBoxColumn2.FieldName = "emp_active";
            gridViewCheckBoxColumn2.HeaderText = "مفعل";
            gridViewCheckBoxColumn2.Name = "gvemp_active";
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewCheckBoxColumn1,
            gridViewCheckBoxColumn2});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(821, 361);
            this.GView.TabIndex = 7;
            this.GView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GView_KeyDown);
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(753, 50);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(69, 21);
            this.radLabel11.TabIndex = 10;
            this.radLabel11.Text = "رقم الموظف";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(753, 76);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(67, 21);
            this.radLabel5.TabIndex = 11;
            this.radLabel5.Text = "الرقم المدني";
            // 
            // txtemp_No
            // 
            this.txtemp_No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtemp_No.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtemp_No.Location = new System.Drawing.Point(492, 48);
            this.txtemp_No.Name = "txtemp_No";
            this.txtemp_No.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtemp_No.Size = new System.Drawing.Size(253, 23);
            this.txtemp_No.TabIndex = 0;
            this.txtemp_No.TabStop = false;
            this.txtemp_No.TextChanged += new System.EventHandler(this.txtemp_No_TextChanged);
            // 
            // txtemp_civilID
            // 
            this.txtemp_civilID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtemp_civilID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtemp_civilID.Location = new System.Drawing.Point(492, 74);
            this.txtemp_civilID.Name = "txtemp_civilID";
            this.txtemp_civilID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtemp_civilID.Size = new System.Drawing.Size(253, 23);
            this.txtemp_civilID.TabIndex = 1;
            this.txtemp_civilID.TabStop = false;
            this.txtemp_civilID.TextChanged += new System.EventHandler(this.txtemp_civilID_TextChanged);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(856, 38);
            this.radPanel1.TabIndex = 15;
            this.radPanel1.Text = "البحث عن موظف";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtemp_namefullar
            // 
            this.txtemp_namefullar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtemp_namefullar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtemp_namefullar.Location = new System.Drawing.Point(492, 100);
            this.txtemp_namefullar.Name = "txtemp_namefullar";
            this.txtemp_namefullar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtemp_namefullar.Size = new System.Drawing.Size(253, 23);
            this.txtemp_namefullar.TabIndex = 2;
            this.txtemp_namefullar.TabStop = false;
            this.txtemp_namefullar.TextChanged += new System.EventHandler(this.txtemp_namefullar_TextChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(753, 102);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(33, 21);
            this.radLabel1.TabIndex = 12;
            this.radLabel1.Text = "الاسم";
            // 
            // cmbnationalityID
            // 
            this.cmbnationalityID.DropDownAnimationEnabled = true;
            this.cmbnationalityID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbnationalityID.Location = new System.Drawing.Point(77, 73);
            this.cmbnationalityID.Name = "cmbnationalityID";
            this.cmbnationalityID.ShowImageInEditorArea = true;
            this.cmbnationalityID.Size = new System.Drawing.Size(241, 23);
            this.cmbnationalityID.TabIndex = 4;
            this.cmbnationalityID.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbnationalityID_SelectedIndexChanged);
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownAnimationEnabled = true;
            this.cmbGender.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbGender.Location = new System.Drawing.Point(77, 47);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.ShowImageInEditorArea = true;
            this.cmbGender.Size = new System.Drawing.Size(241, 23);
            this.cmbGender.TabIndex = 3;
            this.cmbGender.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbGender_SelectedIndexChanged);
            // 
            // radLabel22
            // 
            this.radLabel22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel22.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel22.Location = new System.Drawing.Point(325, 76);
            this.radLabel22.Name = "radLabel22";
            this.radLabel22.Size = new System.Drawing.Size(47, 21);
            this.radLabel22.TabIndex = 14;
            this.radLabel22.Text = "الجنسية";
            // 
            // radLabel21
            // 
            this.radLabel21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel21.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel21.Location = new System.Drawing.Point(325, 50);
            this.radLabel21.Name = "radLabel21";
            this.radLabel21.Size = new System.Drawing.Size(41, 21);
            this.radLabel21.TabIndex = 13;
            this.radLabel21.Text = "الجنس";
            // 
            // chkemp_active
            // 
            this.chkemp_active.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkemp_active.Location = new System.Drawing.Point(177, 103);
            this.chkemp_active.Name = "chkemp_active";
            this.chkemp_active.Size = new System.Drawing.Size(45, 21);
            this.chkemp_active.TabIndex = 5;
            this.chkemp_active.Text = "مفعل";
            this.chkemp_active.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkemp_active_ToggleStateChanged);
            // 
            // chkemp_isMandoub
            // 
            this.chkemp_isMandoub.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkemp_isMandoub.Location = new System.Drawing.Point(263, 103);
            this.chkemp_isMandoub.Name = "chkemp_isMandoub";
            this.chkemp_isMandoub.Size = new System.Drawing.Size(54, 21);
            this.chkemp_isMandoub.TabIndex = 6;
            this.chkemp_isMandoub.Text = "مندوب";
            this.chkemp_isMandoub.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkemp_isMandoub_ToggleStateChanged);
            // 
            // chkemp_Notactive
            // 
            this.chkemp_Notactive.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkemp_Notactive.Location = new System.Drawing.Point(77, 103);
            this.chkemp_Notactive.Name = "chkemp_Notactive";
            this.chkemp_Notactive.Size = new System.Drawing.Size(69, 21);
            this.chkemp_Notactive.TabIndex = 5;
            this.chkemp_Notactive.Text = "غير مفعل";
            this.chkemp_Notactive.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkemp_Notactive_ToggleStateChanged);
            // 
            // frmFindEmployees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 559);
            this.Controls.Add(this.chkemp_Notactive);
            this.Controls.Add(this.chkemp_active);
            this.Controls.Add(this.chkemp_isMandoub);
            this.Controls.Add(this.cmbnationalityID);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.radLabel22);
            this.Controls.Add(this.radLabel21);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radLabel11);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.txtemp_namefullar);
            this.Controls.Add(this.txtemp_No);
            this.Controls.Add(this.txtemp_civilID);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmFindEmployees";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Employees";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmFindEmployees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemp_No)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemp_civilID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemp_namefullar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbnationalityID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkemp_active)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkemp_isMandoub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkemp_Notactive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadTextBox txtemp_No;
        private Telerik.WinControls.UI.RadTextBox txtemp_civilID;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadTextBox txtemp_namefullar;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList cmbnationalityID;
        private Telerik.WinControls.UI.RadDropDownList cmbGender;
        private Telerik.WinControls.UI.RadLabel radLabel22;
        private Telerik.WinControls.UI.RadLabel radLabel21;
        private Telerik.WinControls.UI.RadCheckBox chkemp_active;
        private Telerik.WinControls.UI.RadCheckBox chkemp_isMandoub;
        private Telerik.WinControls.UI.RadCheckBox chkemp_Notactive;
    }
}
