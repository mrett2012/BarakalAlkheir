namespace BarakalAlkheir.CarsM
{
    partial class frmBuyingPaymentCarDetails
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
            this.GView = new System.Windows.Forms.DataGridView();
            this.GVID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVlicenseNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVjan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvFeb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvmarch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvApril = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvJune = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVJuly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvAug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVSep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVOct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVNov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVDec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvRentingPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvAmountRemaining = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.txtDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.GView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GView
            // 
            this.GView.AllowUserToAddRows = false;
            this.GView.AllowUserToDeleteRows = false;
            this.GView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GVID,
            this.GVlicenseNO,
            this.gvName,
            this.gvFrom,
            this.gvTo,
            this.GVjan,
            this.gvFeb,
            this.gvmarch,
            this.gvApril,
            this.GvMay,
            this.GvJune,
            this.GVJuly,
            this.GvAug,
            this.GVSep,
            this.GVOct,
            this.GVNov,
            this.GVDec,
            this.GVTotal,
            this.gvRentingPaid,
            this.gvAmountRemaining});
            this.GView.Location = new System.Drawing.Point(2, 31);
            this.GView.Name = "GView";
            this.GView.ReadOnly = true;
            this.GView.Size = new System.Drawing.Size(1118, 339);
            this.GView.TabIndex = 1;
            // 
            // GVID
            // 
            this.GVID.HeaderText = "ID";
            this.GVID.Name = "GVID";
            this.GVID.ReadOnly = true;
            this.GVID.Visible = false;
            // 
            // GVlicenseNO
            // 
            this.GVlicenseNO.HeaderText = "رقم";
            this.GVlicenseNO.Name = "GVlicenseNO";
            this.GVlicenseNO.ReadOnly = true;
            this.GVlicenseNO.Width = 70;
            // 
            // gvName
            // 
            this.gvName.HeaderText = "اسم ";
            this.gvName.Name = "gvName";
            this.gvName.ReadOnly = true;
            this.gvName.Width = 150;
            // 
            // gvFrom
            // 
            this.gvFrom.HeaderText = "من";
            this.gvFrom.Name = "gvFrom";
            this.gvFrom.ReadOnly = true;
            this.gvFrom.Width = 80;
            // 
            // gvTo
            // 
            this.gvTo.HeaderText = "الى";
            this.gvTo.Name = "gvTo";
            this.gvTo.ReadOnly = true;
            this.gvTo.Width = 80;
            // 
            // GVjan
            // 
            this.GVjan.HeaderText = "1";
            this.GVjan.Name = "GVjan";
            this.GVjan.ReadOnly = true;
            this.GVjan.Width = 45;
            // 
            // gvFeb
            // 
            this.gvFeb.HeaderText = "2";
            this.gvFeb.Name = "gvFeb";
            this.gvFeb.ReadOnly = true;
            this.gvFeb.Width = 45;
            // 
            // gvmarch
            // 
            this.gvmarch.HeaderText = "3";
            this.gvmarch.Name = "gvmarch";
            this.gvmarch.ReadOnly = true;
            this.gvmarch.Width = 45;
            // 
            // gvApril
            // 
            this.gvApril.HeaderText = "4";
            this.gvApril.Name = "gvApril";
            this.gvApril.ReadOnly = true;
            this.gvApril.Width = 45;
            // 
            // GvMay
            // 
            this.GvMay.HeaderText = "5";
            this.GvMay.Name = "GvMay";
            this.GvMay.ReadOnly = true;
            this.GvMay.Width = 45;
            // 
            // GvJune
            // 
            this.GvJune.HeaderText = "6";
            this.GvJune.Name = "GvJune";
            this.GvJune.ReadOnly = true;
            this.GvJune.Width = 45;
            // 
            // GVJuly
            // 
            this.GVJuly.HeaderText = "7";
            this.GVJuly.Name = "GVJuly";
            this.GVJuly.ReadOnly = true;
            this.GVJuly.Width = 45;
            // 
            // GvAug
            // 
            this.GvAug.HeaderText = "8";
            this.GvAug.Name = "GvAug";
            this.GvAug.ReadOnly = true;
            this.GvAug.Width = 45;
            // 
            // GVSep
            // 
            this.GVSep.HeaderText = "9";
            this.GVSep.Name = "GVSep";
            this.GVSep.ReadOnly = true;
            this.GVSep.Width = 45;
            // 
            // GVOct
            // 
            this.GVOct.HeaderText = "10";
            this.GVOct.Name = "GVOct";
            this.GVOct.ReadOnly = true;
            this.GVOct.Width = 45;
            // 
            // GVNov
            // 
            this.GVNov.HeaderText = "11";
            this.GVNov.Name = "GVNov";
            this.GVNov.ReadOnly = true;
            this.GVNov.Width = 45;
            // 
            // GVDec
            // 
            this.GVDec.HeaderText = "12";
            this.GVDec.Name = "GVDec";
            this.GVDec.ReadOnly = true;
            this.GVDec.Width = 45;
            // 
            // GVTotal
            // 
            this.GVTotal.HeaderText = "الاجمالي";
            this.GVTotal.Name = "GVTotal";
            this.GVTotal.ReadOnly = true;
            this.GVTotal.Width = 70;
            // 
            // gvRentingPaid
            // 
            this.gvRentingPaid.HeaderText = "المدفوع";
            this.gvRentingPaid.Name = "gvRentingPaid";
            this.gvRentingPaid.ReadOnly = true;
            this.gvRentingPaid.Width = 70;
            // 
            // gvAmountRemaining
            // 
            this.gvAmountRemaining.HeaderText = "المتبقي";
            this.gvAmountRemaining.Name = "gvAmountRemaining";
            this.gvAmountRemaining.ReadOnly = true;
            this.gvAmountRemaining.Width = 70;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(83, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 24);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "اظهار";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtDate
            // 
            this.txtDate.CustomFormat = "yyyy";
            this.txtDate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.Location = new System.Drawing.Point(2, 4);
            this.txtDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.txtDate.MinDate = new System.DateTime(((long)(0)));
            this.txtDate.Name = "txtDate";
            this.txtDate.NullableValue = new System.DateTime(2012, 5, 31, 16, 24, 21, 281);
            this.txtDate.NullDate = new System.DateTime(((long)(0)));
            this.txtDate.Size = new System.Drawing.Size(75, 23);
            this.txtDate.TabIndex = 4;
            this.txtDate.TabStop = false;
            this.txtDate.Text = "2012";
            this.txtDate.Value = new System.DateTime(2012, 5, 31, 16, 24, 21, 281);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(214, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(87, 24);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "طباعة";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmBuyingPaymentCarDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 428);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.GView);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmBuyingPaymentCarDetails";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Buying Payment Details";
            this.ThemeName = "ControlDefault";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRentingPaymentDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GView;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVlicenseNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVjan;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvFeb;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvmarch;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvApril;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvJune;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVJuly;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvAug;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVSep;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVOct;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVNov;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVDec;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvRentingPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvAmountRemaining;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadDateTimePicker txtDate;
        private Telerik.WinControls.UI.RadButton btnExport;

    }
}
