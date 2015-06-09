namespace GUI
{
    partial class SuperAdminLogin
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
            this.HeadlineLabel = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HeadlineLabel
            // 
            this.HeadlineLabel.AutoSize = true;
            this.HeadlineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.HeadlineLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HeadlineLabel.Location = new System.Drawing.Point(70, 12);
            this.HeadlineLabel.Name = "HeadlineLabel";
            this.HeadlineLabel.Size = new System.Drawing.Size(135, 25);
            this.HeadlineLabel.TabIndex = 0;
            this.HeadlineLabel.Text = "Super Admin";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(67, 63);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.UserNameTextBox.TabIndex = 1;
            this.UserNameTextBox.Text = "user name";
            this.UserNameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(67, 107);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(137, 20);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.Text = "password";
            // 
            // AcceptButton
            // 
            this.AcceptButton.BackColor = System.Drawing.Color.Green;
            this.AcceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.AcceptButton.Location = new System.Drawing.Point(56, 163);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(185, 49);
            this.AcceptButton.TabIndex = 3;
            this.AcceptButton.Text = "I\'m super admin";
            this.AcceptButton.UseVisualStyleBackColor = false;
            this.AcceptButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SuperAdminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.HeadlineLabel);
            this.Name = "SuperAdminLogin";
            this.Text = "SuperAdminLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HeadlineLabel;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button AcceptButton;
    }
}