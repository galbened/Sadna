namespace GUI
{
    partial class Home
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
            this.components = new System.ComponentModel.Container();
            this.HeadlineLabel = new System.Windows.Forms.Label();
            this.Addbutton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.userManagerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ForumName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userManagerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // HeadlineLabel
            // 
            this.HeadlineLabel.AutoSize = true;
            this.HeadlineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.HeadlineLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.HeadlineLabel.Location = new System.Drawing.Point(21, 9);
            this.HeadlineLabel.Name = "HeadlineLabel";
            this.HeadlineLabel.Size = new System.Drawing.Size(84, 25);
            this.HeadlineLabel.TabIndex = 0;
            this.HeadlineLabel.Text = "Forums";
            this.HeadlineLabel.UseMnemonic = false;
            // 
            // Addbutton
            // 
            this.Addbutton.Location = new System.Drawing.Point(38, 52);
            this.Addbutton.Name = "Addbutton";
            this.Addbutton.Size = new System.Drawing.Size(94, 31);
            this.Addbutton.TabIndex = 1;
            this.Addbutton.Text = "Add new forum";
            this.Addbutton.UseVisualStyleBackColor = true;
            this.Addbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.Red;
            this.RemoveButton.Location = new System.Drawing.Point(149, 53);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(112, 29);
            this.RemoveButton.TabIndex = 2;
            this.RemoveButton.Text = "Remove forum";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ForumName});
            this.dataGridView1.DataSource = this.userManagerBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(38, 145);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(222, 90);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // ForumName
            // 
            this.ForumName.HeaderText = "Forum name";
            this.ForumName.Name = "ForumName";
            // 
            // Home
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.Addbutton);
            this.Controls.Add(this.HeadlineLabel);
            this.Name = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userManagerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HeadlineLabel;
        private System.Windows.Forms.Button Addbutton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource userManagerBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ForumName;


    }
}