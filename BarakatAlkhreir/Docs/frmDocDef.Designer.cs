namespace BarakalAlkheir.Docs
{
    partial class frmDocDef
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
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.lblID = new Telerik.WinControls.UI.RadLabel();
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.txtdocumentDef_WarningDays = new Telerik.WinControls.UI.RadTextBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.cmbdocumentTypeDefID = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtdocumentDefCode = new Telerik.WinControls.UI.RadTextBox();
            this.txtdocumentDef_Name = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdocumentDef_WarningDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbdocumentTypeDefID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdocumentDefCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdocumentDef_Name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(624, 59);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(65, 21);
            this.radLabel11.TabIndex = 34;
            this.radLabel11.Text = "نوع الوثيقة";
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(744, 55);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 21);
            this.lblID.TabIndex = 46;
            this.lblID.Text = "-1";
            this.lblID.Visible = false;
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(27, 201);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "documentDefID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "documentDefID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVdocumentDefID";
            gridViewTextBoxColumn1.Width = 44;
            gridViewTextBoxColumn2.FieldName = "documentDefCode";
            gridViewTextBoxColumn2.HeaderText = "كود الوثيقة";
            gridViewTextBoxColumn2.Name = "gvdocumentDefCode";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 100;
            gridViewTextBoxColumn3.FieldName = "documentDef_Name";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "نوع الوتيقة";
            gridViewTextBoxColumn3.Name = "GVdocumentDef_Name";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 250;
            gridViewTextBoxColumn4.FieldName = "documentDef_WarningDays";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "فترة التنبيه";
            gridViewTextBoxColumn4.Name = "GVdocumentDef_WarningDays";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 100;
            gridViewTextBoxColumn5.FieldName = "documentType_Name";
            gridViewTextBoxColumn5.HeaderText = "المجموعة";
            gridViewTextBoxColumn5.Name = "GvdocumentType_Name";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 200;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(714, 344);
            this.GView.TabIndex = 2;
            this.GView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GView_CellClick);
            // 
            // txtdocumentDef_WarningDays
            // 
            this.txtdocumentDef_WarningDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdocumentDef_WarningDays.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdocumentDef_WarningDays.Location = new System.Drawing.Point(305, 84);
            this.txtdocumentDef_WarningDays.Name = "txtdocumentDef_WarningDays";
            this.txtdocumentDef_WarningDays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtdocumentDef_WarningDays.Size = new System.Drawing.Size(311, 23);
            this.txtdocumentDef_WarningDays.TabIndex = 3;
            this.txtdocumentDef_WarningDays.TabStop = false;
            this.txtdocumentDef_WarningDays.Leave += new System.EventHandler(this.txtdocumentDef_WarningDays_Leave);
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(27, 48);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(714, 38);
            this.radPanel1.TabIndex = 0;
            this.radPanel1.Text = "انواع الوثائق";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.cmbdocumentTypeDefID);
            this.radPanel2.Controls.Add(this.radLabel2);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.radLabel11);
            this.radPanel2.Controls.Add(this.txtdocumentDefCode);
            this.radPanel2.Controls.Add(this.txtdocumentDef_WarningDays);
            this.radPanel2.Controls.Add(this.txtdocumentDef_Name);
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Location = new System.Drawing.Point(27, 85);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(714, 117);
            this.radPanel2.TabIndex = 1;
            // 
            // cmbdocumentTypeDefID
            // 
            this.cmbdocumentTypeDefID.DropDownAnimationEnabled = true;
            this.cmbdocumentTypeDefID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbdocumentTypeDefID.Location = new System.Drawing.Point(305, 9);
            this.cmbdocumentTypeDefID.Name = "cmbdocumentTypeDefID";
            this.cmbdocumentTypeDefID.ShowImageInEditorArea = true;
            this.cmbdocumentTypeDefID.Size = new System.Drawing.Size(311, 23);
            this.cmbdocumentTypeDefID.TabIndex = 0;
            // 
            // radLabel2
            // 
            this.radLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(624, 9);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(57, 21);
            this.radLabel2.TabIndex = 41;
            this.radLabel2.Text = "المجموعة";
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(624, 34);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(63, 21);
            this.radLabel1.TabIndex = 34;
            this.radLabel1.Text = "كود الوثيقة";
            // 
            // txtdocumentDefCode
            // 
            this.txtdocumentDefCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdocumentDefCode.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdocumentDefCode.Location = new System.Drawing.Point(305, 34);
            this.txtdocumentDefCode.Name = "txtdocumentDefCode";
            this.txtdocumentDefCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtdocumentDefCode.Size = new System.Drawing.Size(311, 23);
            this.txtdocumentDefCode.TabIndex = 1;
            this.txtdocumentDefCode.TabStop = false;
            // 
            // txtdocumentDef_Name
            // 
            this.txtdocumentDef_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdocumentDef_Name.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdocumentDef_Name.Location = new System.Drawing.Point(305, 59);
            this.txtdocumentDef_Name.Name = "txtdocumentDef_Name";
            this.txtdocumentDef_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtdocumentDef_Name.Size = new System.Drawing.Size(311, 23);
            this.txtdocumentDef_Name.TabIndex = 2;
            this.txtdocumentDef_Name.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(626, 84);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(59, 21);
            this.radLabel15.TabIndex = 35;
            this.radLabel15.Text = "فترة التنيه";
            this.radLabel15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.AutoSize = true;
            this.radStatusStrip1.LayoutStyle = Telerik.WinControls.UI.RadStatusBarLayoutStyle.Stack;
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 578);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(779, 24);
            this.radStatusStrip1.TabIndex = 48;
            this.radStatusStrip1.Text = "radStatusStrip1";
            // 
            // frmDocDef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 602);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radStatusStrip1);
            this.Name = "frmDocDef";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doc Def";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmDocDef_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdocumentDef_WarningDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbdocumentTypeDefID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdocumentDefCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdocumentDef_Name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel lblID;
        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadTextBox txtdocumentDef_WarningDays;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadTextBox txtdocumentDef_Name;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadDropDownList cmbdocumentTypeDefID;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtdocumentDefCode;
    }
}
