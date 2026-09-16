namespace BarakalAlkheir.Definitions
{
    partial class frmUsersFormEvents
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.gview = new System.Windows.Forms.DataGridView();
            this.cmbuserGroupID = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.gvFormID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbuserGroupID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.radPanel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel1.Location = new System.Drawing.Point(50, 46);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPanel1.Size = new System.Drawing.Size(654, 38);
            this.radPanel1.TabIndex = 40;
            this.radPanel1.Text = "صلاحيات المستخدم";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radPanel2
            // 
            this.radPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel2.Controls.Add(this.gview);
            this.radPanel2.Controls.Add(this.cmbuserGroupID);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.radPanel2.Location = new System.Drawing.Point(50, 83);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(654, 420);
            this.radPanel2.TabIndex = 41;
            // 
            // gview
            // 
            this.gview.AllowUserToAddRows = false;
            this.gview.AllowUserToDeleteRows = false;
            this.gview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gvFormID,
            this.GVDisplayName});
            this.gview.Location = new System.Drawing.Point(3, 50);
            this.gview.Name = "gview";
            this.gview.ReadOnly = true;
            this.gview.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gview.Size = new System.Drawing.Size(630, 356);
            this.gview.TabIndex = 15;
            // 
            // cmbuserGroupID
            // 
            this.cmbuserGroupID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbuserGroupID.DropDownAnimationEnabled = true;
            this.cmbuserGroupID.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cmbuserGroupID.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbuserGroupID.Location = new System.Drawing.Point(220, 13);
            this.cmbuserGroupID.Name = "cmbuserGroupID";
            this.cmbuserGroupID.ShowImageInEditorArea = true;
            this.cmbuserGroupID.Size = new System.Drawing.Size(286, 23);
            this.cmbuserGroupID.TabIndex = 14;
            this.cmbuserGroupID.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbuserGroupID_SelectedIndexChanged);
            ((Telerik.WinControls.UI.RadDropDownListElement)(this.cmbuserGroupID.GetChildAt(0))).DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(512, 15);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(121, 21);
            this.radLabel1.TabIndex = 13;
            this.radLabel1.Text = "المجموعة المستخدمين";
            // 
            // gvFormID
            // 
            this.gvFormID.HeaderText = "FID";
            this.gvFormID.Name = "gvFormID";
            this.gvFormID.ReadOnly = true;
            this.gvFormID.Visible = false;
            this.gvFormID.Width = 50;
            // 
            // GVDisplayName
            // 
            this.GVDisplayName.HeaderText = "الشاشة";
            this.GVDisplayName.Name = "GVDisplayName";
            this.GVDisplayName.ReadOnly = true;
            this.GVDisplayName.Width = 250;
            // 
            // frmUsersFormEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 541);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radPanel2);
            this.Name = "frmUsersFormEvents";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Users Form Events";
            this.ThemeName = "ControlDefault";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmUsersFormEvents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbuserGroupID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadDropDownList cmbuserGroupID;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.DataGridView gview;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvFormID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVDisplayName;
    }
}
