namespace BarakalAlkheir.Definitions
{
    partial class frmTransactionDef
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
            this.GView = new Telerik.WinControls.UI.RadGridView();
            this.txttransDef_namea = new Telerik.WinControls.UI.RadTextBox();
            this.txttransDef_code = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.lblID = new Telerik.WinControls.UI.RadLabel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttransDef_namea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttransDef_code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
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
            this.GView.Location = new System.Drawing.Point(38, 155);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "transDefID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "nationalityID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVtransDefID";
            gridViewTextBoxColumn1.Width = 100;
            gridViewTextBoxColumn2.FieldName = "transDef_code";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "الكود";
            gridViewTextBoxColumn2.Name = "GVtransDef_code";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldName = "transDef_namea";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "الاجراء";
            gridViewTextBoxColumn3.Name = "GVtransDef_namea";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 400;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(714, 360);
            this.GView.TabIndex = 2;
            this.GView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GView_CellClick);
            this.GView.Click += new System.EventHandler(this.GView_Click);
            // 
            // txttransDef_namea
            // 
            this.txttransDef_namea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txttransDef_namea.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttransDef_namea.Location = new System.Drawing.Point(338, 38);
            this.txttransDef_namea.Name = "txttransDef_namea";
            this.txttransDef_namea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txttransDef_namea.Size = new System.Drawing.Size(311, 23);
            this.txttransDef_namea.TabIndex = 32;
            this.txttransDef_namea.TabStop = false;
            // 
            // txttransDef_code
            // 
            this.txttransDef_code.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txttransDef_code.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttransDef_code.Location = new System.Drawing.Point(338, 12);
            this.txttransDef_code.Name = "txttransDef_code";
            this.txttransDef_code.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txttransDef_code.Size = new System.Drawing.Size(311, 23);
            this.txttransDef_code.TabIndex = 31;
            this.txttransDef_code.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(655, 40);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(44, 21);
            this.radLabel15.TabIndex = 35;
            this.radLabel15.Text = "الاجراء";
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(663, 14);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(25, 21);
            this.radLabel11.TabIndex = 34;
            this.radLabel11.Text = "كود";
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.radLabel11);
            this.radPanel2.Controls.Add(this.txttransDef_namea);
            this.radPanel2.Controls.Add(this.txttransDef_code);
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Location = new System.Drawing.Point(38, 81);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(714, 75);
            this.radPanel2.TabIndex = 37;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(758, 47);
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
            this.radPanel1.Text = "أنواع الاجراءات";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmTransactionDef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 575);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmTransactionDef";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Definition";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmTransactionDef_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttransDef_namea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttransDef_code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
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
        private Telerik.WinControls.UI.RadTextBox txttransDef_namea;
        private Telerik.WinControls.UI.RadTextBox txttransDef_code;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel lblID;
    }
}
