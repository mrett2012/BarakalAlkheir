namespace BarakalAlkheir
{
    partial class frmlogin
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
            this.txtuserName = new Telerik.WinControls.UI.RadTextBox();
            this.txtpassword = new Telerik.WinControls.UI.RadTextBox();
            this.loginButton = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtuserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtuserName
            // 
            this.txtuserName.BackColor = System.Drawing.Color.White;
            this.txtuserName.BackgroundImage = global::BarakalAlkheir.Properties.Resources.login_but_bg;
            this.txtuserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtuserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtuserName.ForeColor = System.Drawing.Color.Teal;
            this.txtuserName.Location = new System.Drawing.Point(183, 158);
            this.txtuserName.Name = "txtuserName";
            this.txtuserName.Size = new System.Drawing.Size(150, 20);
            this.txtuserName.TabIndex = 0;
            this.txtuserName.TabStop = false;
            this.txtuserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtuserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuserName_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.BackColor = System.Drawing.Color.White;
            this.txtpassword.BackgroundImage = global::BarakalAlkheir.Properties.Resources.login_but_bg;
            this.txtpassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtpassword.ForeColor = System.Drawing.Color.Teal;
            this.txtpassword.Location = new System.Drawing.Point(183, 204);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '.';
            this.txtpassword.Size = new System.Drawing.Size(150, 20);
            this.txtpassword.TabIndex = 1;
            this.txtpassword.TabStop = false;
            this.txtpassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // loginButton
            // 
            this.loginButton.BackgroundImage = global::BarakalAlkheir.Properties.Resources.login_but1;
            this.loginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.loginButton.Image = global::BarakalAlkheir.Properties.Resources.user_icon;
            this.loginButton.ImageScalingSize = new System.Drawing.Size(35, 35);
            this.loginButton.Location = new System.Drawing.Point(180, 243);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(264, 38);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = " تسجيل دخول";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // frmlogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BarakalAlkheir.Properties.Resources.login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(477, 326);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtuserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmlogin";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الدخول";
            this.ThemeName = "ControlDefault";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmlogin_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtuserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtuserName;
        private Telerik.WinControls.UI.RadTextBox txtpassword;
        private Telerik.WinControls.UI.RadButton loginButton;
    }
}
