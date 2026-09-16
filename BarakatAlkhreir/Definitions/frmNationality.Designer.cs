namespace BarakalAlkheir.Definitions
{
    partial class frmNationality
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
            this.txtnatio_namea = new Telerik.WinControls.UI.RadTextBox();
            this.txtnatio_Code = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.lblID = new Telerik.WinControls.UI.RadLabel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtnatio_namee = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnatio_namea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnatio_Code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnatio_namee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GView
            // 
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.GView.Location = new System.Drawing.Point(38, 179);
            // 
            // GView
            // 
            this.GView.MasterTemplate.AllowAddNewRow = false;
            this.GView.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.FieldName = "nationalityID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "nationalityID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "GVnationalityID";
            gridViewTextBoxColumn1.Width = 100;
            gridViewTextBoxColumn2.FieldName = "natio_Code";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "الكود";
            gridViewTextBoxColumn2.Name = "GVnatio_Code";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 100;
            gridViewTextBoxColumn3.FieldName = "natio_namea";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "الجنسيات عربي";
            gridViewTextBoxColumn3.Name = "GVnatio_namea";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 200;
            gridViewTextBoxColumn4.FieldName = "natio_namee";
            gridViewTextBoxColumn4.HeaderText = "الجنسيات انكليزي";
            gridViewTextBoxColumn4.Name = "GVnatio_namee";
            gridViewTextBoxColumn4.Width = 200;
            this.GView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GView.Size = new System.Drawing.Size(714, 344);
            this.GView.TabIndex = 2;
            this.GView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GView_CellClick);
            this.GView.Click += new System.EventHandler(this.GView_Click);
            // 
            // txtnatio_namea
            // 
            this.txtnatio_namea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnatio_namea.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnatio_namea.Location = new System.Drawing.Point(311, 38);
            this.txtnatio_namea.Name = "txtnatio_namea";
            this.txtnatio_namea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtnatio_namea.Size = new System.Drawing.Size(311, 23);
            this.txtnatio_namea.TabIndex = 32;
            this.txtnatio_namea.TabStop = false;
            // 
            // txtnatio_Code
            // 
            this.txtnatio_Code.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnatio_Code.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnatio_Code.Location = new System.Drawing.Point(311, 12);
            this.txtnatio_Code.Name = "txtnatio_Code";
            this.txtnatio_Code.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtnatio_Code.Size = new System.Drawing.Size(311, 23);
            this.txtnatio_Code.TabIndex = 31;
            this.txtnatio_Code.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(630, 40);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(70, 21);
            this.radLabel15.TabIndex = 35;
            this.radLabel15.Text = "الاسم عربي ";
            // 
            // radLabel11
            // 
            this.radLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.Location = new System.Drawing.Point(630, 14);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(25, 21);
            this.radLabel11.TabIndex = 34;
            this.radLabel11.Text = "كود";
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.radLabel11);
            this.radPanel2.Controls.Add(this.txtnatio_namee);
            this.radPanel2.Controls.Add(this.txtnatio_namea);
            this.radPanel2.Controls.Add(this.txtnatio_Code);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Location = new System.Drawing.Point(38, 81);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(714, 101);
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
            this.radPanel1.Text = "الجنسيات";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(630, 67);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(75, 21);
            this.radLabel1.TabIndex = 35;
            this.radLabel1.Text = "الاسم انكليزي";
            // 
            // txtnatio_namee
            // 
            this.txtnatio_namee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnatio_namee.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnatio_namee.Location = new System.Drawing.Point(311, 65);
            this.txtnatio_namee.Name = "txtnatio_namee";
            this.txtnatio_namee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtnatio_namee.Size = new System.Drawing.Size(311, 23);
            this.txtnatio_namee.TabIndex = 32;
            this.txtnatio_namee.TabStop = false;
            // 
            // frmNationality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 583);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.GView);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmNationality";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nationality";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmNationalitys_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnatio_namea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnatio_Code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnatio_namee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView GView;
        private Telerik.WinControls.UI.RadTextBox txtnatio_namea;
        private Telerik.WinControls.UI.RadTextBox txtnatio_Code;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel lblID;
        private Telerik.WinControls.UI.RadTextBox txtnatio_namee;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}
