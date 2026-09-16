namespace BarakalAlkheir.Definitions
{
    partial class frmFindCustomers
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
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.txtcustomer_Name = new Telerik.WinControls.UI.RadTextBox();
            this.txtcustomer_CivilID = new Telerik.WinControls.UI.RadTextBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.txtcustomer_phoneNumber1 = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_CivilID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_phoneNumber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(417, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 30);
            this.btnCancel.TabIndex = 49;
            this.btnCancel.Text = "اغلاق";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(565, 461);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(130, 30);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(16, 136);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "customerID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "gvcustomerID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "customer_Name";
            gridViewTextBoxColumn2.HeaderText = "الإسم";
            gridViewTextBoxColumn2.Name = "gvcustomer_Name";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 250;
            gridViewTextBoxColumn3.FieldName = "customer_CivilID";
            gridViewTextBoxColumn3.HeaderText = "الرقم المدني";
            gridViewTextBoxColumn3.Name = "GVcustomer_CivilID";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 250;
            gridViewTextBoxColumn4.FieldName = "customer_phoneNumber1";
            gridViewTextBoxColumn4.HeaderText = "الهاتف";
            gridViewTextBoxColumn4.Name = "gvcustomer_phoneNumber1";
            gridViewTextBoxColumn4.Width = 150;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(678, 319);
            this.GView.TabIndex = 48;
            this.GView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GView_KeyDown);
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(652, 54);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(33, 21);
            this.radLabel11.TabIndex = 46;
            this.radLabel11.Text = "الاسم";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(618, 78);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(67, 21);
            this.radLabel5.TabIndex = 47;
            this.radLabel5.Text = "الرقم المدني";
            // 
            // txtcustomer_Name
            // 
            this.txtcustomer_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_Name.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_Name.Location = new System.Drawing.Point(334, 54);
            this.txtcustomer_Name.Name = "txtcustomer_Name";
            this.txtcustomer_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_Name.Size = new System.Drawing.Size(276, 23);
            this.txtcustomer_Name.TabIndex = 44;
            this.txtcustomer_Name.TabStop = false;
            this.txtcustomer_Name.TextChanged += new System.EventHandler(this.txtcustomer_Name_TextChanged);
            // 
            // txtcustomer_CivilID
            // 
            this.txtcustomer_CivilID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_CivilID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_CivilID.Location = new System.Drawing.Point(334, 78);
            this.txtcustomer_CivilID.Name = "txtcustomer_CivilID";
            this.txtcustomer_CivilID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_CivilID.Size = new System.Drawing.Size(276, 23);
            this.txtcustomer_CivilID.TabIndex = 45;
            this.txtcustomer_CivilID.TabStop = false;
            this.txtcustomer_CivilID.TextChanged += new System.EventHandler(this.txtcustomer_CivilID_TextChanged);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(707, 38);
            this.radPanel1.TabIndex = 43;
            this.radPanel1.Text = "البحث عن عميل";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtcustomer_phoneNumber1
            // 
            this.txtcustomer_phoneNumber1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcustomer_phoneNumber1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustomer_phoneNumber1.Location = new System.Drawing.Point(334, 102);
            this.txtcustomer_phoneNumber1.Name = "txtcustomer_phoneNumber1";
            this.txtcustomer_phoneNumber1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtcustomer_phoneNumber1.Size = new System.Drawing.Size(276, 23);
            this.txtcustomer_phoneNumber1.TabIndex = 45;
            this.txtcustomer_phoneNumber1.TabStop = false;
            this.txtcustomer_phoneNumber1.TextChanged += new System.EventHandler(this.txtcustomer_phoneNumber1_TextChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(644, 102);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(41, 21);
            this.radLabel1.TabIndex = 47;
            this.radLabel1.Text = "الهاتف";
            // 
            // frmFindCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 503);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radLabel11);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.txtcustomer_phoneNumber1);
            this.Controls.Add(this.txtcustomer_Name);
            this.Controls.Add(this.txtcustomer_CivilID);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmFindCustomers";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Customers";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmFindCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_CivilID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustomer_phoneNumber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
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
        private Telerik.WinControls.UI.RadTextBox txtcustomer_Name;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_CivilID;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadTextBox txtcustomer_phoneNumber1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}
