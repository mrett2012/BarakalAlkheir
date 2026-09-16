namespace BarakalAlkheir.Definitions
{
    partial class frmFormEvents
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.lstData = new System.Windows.Forms.ListBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.gvChoose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gvEvenNAme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gveventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbformEventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gview = new System.Windows.Forms.DataGridView();
            this.lblFormName = new Telerik.WinControls.UI.RadLabel();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFormName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(331, 13);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(235, 30);
            label2.TabIndex = 44;
            label2.Text = "الشاشات";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            label5.Location = new System.Drawing.Point(53, 13);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(250, 30);
            label5.TabIndex = 43;
            label5.Text = "اجراء الشاشات";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.lblFormName);
            this.radPanel2.Controls.Add(this.Gview);
            this.radPanel2.Controls.Add(label2);
            this.radPanel2.Controls.Add(this.lstData);
            this.radPanel2.Controls.Add(label5);
            this.radPanel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel2.Location = new System.Drawing.Point(19, 85);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(619, 420);
            this.radPanel2.TabIndex = 39;
            // 
            // lstData
            // 
            this.lstData.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lstData.FormattingEnabled = true;
            this.lstData.ItemHeight = 18;
            this.lstData.Location = new System.Drawing.Point(331, 42);
            this.lstData.Name = "lstData";
            this.lstData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lstData.Size = new System.Drawing.Size(235, 364);
            this.lstData.TabIndex = 45;
            this.lstData.SelectedIndexChanged += new System.EventHandler(this.lstData_SelectedIndexChanged);
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(19, 48);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(619, 38);
            this.radPanel1.TabIndex = 38;
            this.radPanel1.Text = "اجراءات الشاشات";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gvChoose
            // 
            this.gvChoose.HeaderText = "اختيار";
            this.gvChoose.Name = "gvChoose";
            this.gvChoose.ReadOnly = true;
            // 
            // gvEvenNAme
            // 
            this.gvEvenNAme.HeaderText = "الاجراء";
            this.gvEvenNAme.Name = "gvEvenNAme";
            this.gvEvenNAme.ReadOnly = true;
            // 
            // gveventID
            // 
            this.gveventID.HeaderText = "eventID";
            this.gveventID.Name = "gveventID";
            this.gveventID.ReadOnly = true;
            this.gveventID.Visible = false;
            this.gveventID.Width = 50;
            // 
            // gbformEventID
            // 
            this.gbformEventID.HeaderText = "formEventID";
            this.gbformEventID.Name = "gbformEventID";
            this.gbformEventID.ReadOnly = true;
            this.gbformEventID.Visible = false;
            this.gbformEventID.Width = 50;
            // 
            // Gview
            // 
            this.Gview.AllowUserToAddRows = false;
            this.Gview.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Gview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gbformEventID,
            this.gveventID,
            this.gvEvenNAme,
            this.gvChoose});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Gview.DefaultCellStyle = dataGridViewCellStyle2;
            this.Gview.Location = new System.Drawing.Point(53, 41);
            this.Gview.Name = "Gview";
            this.Gview.ReadOnly = true;
            this.Gview.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Gview.Size = new System.Drawing.Size(250, 365);
            this.Gview.TabIndex = 46;
            // 
            // lblFormName
            // 
            this.lblFormName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormName.AutoSize = false;
            this.lblFormName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormName.ForeColor = System.Drawing.Color.Red;
            this.lblFormName.Location = new System.Drawing.Point(55, 17);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(160, 21);
            this.lblFormName.TabIndex = 40;
            this.lblFormName.Text = "كود المهنة";
            // 
            // frmFormEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 540);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radPanel1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmFormEvents";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Events";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmFormEvents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFormName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.ListBox lstData;
        private System.Windows.Forms.DataGridView Gview;
        private System.Windows.Forms.DataGridViewTextBoxColumn gbformEventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gveventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvEvenNAme;
        private System.Windows.Forms.DataGridViewCheckBoxColumn gvChoose;
        private Telerik.WinControls.UI.RadLabel lblFormName;
    }
}
