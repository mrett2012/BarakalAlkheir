namespace BarakalAlkheir.Definitions
{
    partial class frmForms
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
            this.txtform_DisplayName = new Telerik.WinControls.UI.RadTextBox();
            this.txtform_IDName = new Telerik.WinControls.UI.RadTextBox();
            this.txtform_Descriptions = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.lblID = new Telerik.WinControls.UI.RadLabel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtform_DisplayName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtform_IDName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtform_Descriptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
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
            this.GView.Location = new System.Drawing.Point(38, 180);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "formID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "formID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVformID";
            gridViewTextBoxColumn1.Width = 44;
            gridViewTextBoxColumn2.FieldName = "form_IDName";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "كود الشاشة";
            gridViewTextBoxColumn2.Name = "GVform_IDName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldName = "form_DisplayName";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "اسم الشاشة";
            gridViewTextBoxColumn3.Name = "GVform_DisplayName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 300;
            gridViewTextBoxColumn4.FieldName = "form_Descriptions";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "ملاحظات";
            gridViewTextBoxColumn4.Name = "GVform_Descriptions";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 200;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(714, 348);
            this.GView.TabIndex = 2;
            this.GView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GView_CellClick);
            this.GView.Click += new System.EventHandler(this.GView_Click);
            // 
            // txtform_DisplayName
            // 
            this.txtform_DisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtform_DisplayName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtform_DisplayName.Location = new System.Drawing.Point(311, 38);
            this.txtform_DisplayName.Name = "txtform_DisplayName";
            this.txtform_DisplayName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtform_DisplayName.Size = new System.Drawing.Size(311, 23);
            this.txtform_DisplayName.TabIndex = 32;
            this.txtform_DisplayName.TabStop = false;
            // 
            // txtform_IDName
            // 
            this.txtform_IDName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtform_IDName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtform_IDName.Location = new System.Drawing.Point(311, 12);
            this.txtform_IDName.Name = "txtform_IDName";
            this.txtform_IDName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtform_IDName.Size = new System.Drawing.Size(311, 23);
            this.txtform_IDName.TabIndex = 31;
            this.txtform_IDName.TabStop = false;
            // 
            // txtform_Descriptions
            // 
            this.txtform_Descriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtform_Descriptions.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtform_Descriptions.Location = new System.Drawing.Point(311, 64);
            this.txtform_Descriptions.Name = "txtform_Descriptions";
            this.txtform_Descriptions.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtform_Descriptions.Size = new System.Drawing.Size(311, 23);
            this.txtform_Descriptions.TabIndex = 33;
            this.txtform_Descriptions.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(625, 39);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(66, 21);
            this.radLabel15.TabIndex = 35;
            this.radLabel15.Text = "اسم الشاشة";
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(626, 13);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(65, 21);
            this.radLabel11.TabIndex = 34;
            this.radLabel11.Text = "كود الشاشة";
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(639, 65);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(52, 21);
            this.radLabel5.TabIndex = 36;
            this.radLabel5.Text = "ملاحظات";
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.radLabel11);
            this.radPanel2.Controls.Add(this.txtform_DisplayName);
            this.radPanel2.Controls.Add(this.radLabel5);
            this.radPanel2.Controls.Add(this.txtform_IDName);
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Controls.Add(this.txtform_Descriptions);
            this.radPanel2.Location = new System.Drawing.Point(38, 81);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(714, 100);
            this.radPanel2.TabIndex = 37;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(755, 51);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 21);
            this.lblID.TabIndex = 37;
            this.lblID.Text = "-1";
            this.lblID.Visible = false;
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
            this.radPanel1.TabIndex = 1;
            this.radPanel1.Text = "الشاشات";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 563);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmForms";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forms";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmFormScreans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtform_DisplayName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtform_IDName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtform_Descriptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadTextBox txtform_DisplayName;
        private Telerik.WinControls.UI.RadTextBox txtform_IDName;
        private Telerik.WinControls.UI.RadTextBox txtform_Descriptions;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel lblID;
    }
}
