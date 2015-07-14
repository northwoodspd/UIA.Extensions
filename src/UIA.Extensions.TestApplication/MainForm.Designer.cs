namespace UIA.Extensions.TestApplication
{
    partial class MainForm
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
            this.basicPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fakeCombo = new System.Windows.Forms.Label();
            this.toggleRowButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.updateHeaders = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.howManyToAdd = new System.Windows.Forms.TextBox();
            this.addRowButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.basicPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // basicPanel
            // 
            this.basicPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basicPanel.Controls.Add(this.groupBox1);
            this.basicPanel.Controls.Add(this.toggleRowButton);
            this.basicPanel.Controls.Add(this.pictureBox1);
            this.basicPanel.Controls.Add(this.updateHeaders);
            this.basicPanel.Controls.Add(this.deleteButton);
            this.basicPanel.Controls.Add(this.numericUpDown);
            this.basicPanel.Controls.Add(this.howManyToAdd);
            this.basicPanel.Controls.Add(this.addRowButton);
            this.basicPanel.Controls.Add(this.dataGridView);
            this.basicPanel.Controls.Add(this.monthCalendar);
            this.basicPanel.Location = new System.Drawing.Point(12, 12);
            this.basicPanel.Name = "basicPanel";
            this.basicPanel.Size = new System.Drawing.Size(613, 339);
            this.basicPanel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fakeCombo);
            this.groupBox1.Location = new System.Drawing.Point(269, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 44);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fake Combo";
            // 
            // fakeCombo
            // 
            this.fakeCombo.AutoSize = true;
            this.fakeCombo.Location = new System.Drawing.Point(6, 16);
            this.fakeCombo.Name = "fakeCombo";
            this.fakeCombo.Size = new System.Drawing.Size(34, 13);
            this.fakeCombo.TabIndex = 0;
            this.fakeCombo.Text = "herpa";
            // 
            // toggleRowButton
            // 
            this.toggleRowButton.Location = new System.Drawing.Point(269, 207);
            this.toggleRowButton.Name = "toggleRowButton";
            this.toggleRowButton.Size = new System.Drawing.Size(75, 23);
            this.toggleRowButton.TabIndex = 8;
            this.toggleRowButton.Text = "Toggle Row";
            this.toggleRowButton.UseVisualStyleBackColor = true;
            this.toggleRowButton.Click += new System.EventHandler(this.toggleRowButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UIA.Extensions.TestApplication.Properties.Resources.MrT;
            this.pictureBox1.Location = new System.Drawing.Point(9, 221);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // updateHeaders
            // 
            this.updateHeaders.Location = new System.Drawing.Point(409, 177);
            this.updateHeaders.Name = "updateHeaders";
            this.updateHeaders.Size = new System.Drawing.Size(111, 23);
            this.updateHeaders.TabIndex = 6;
            this.updateHeaders.Text = "Update Headers";
            this.updateHeaders.UseVisualStyleBackColor = true;
            this.updateHeaders.Click += new System.EventHandler(this.updateHeaders_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(535, 177);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete Last";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(9, 183);
            this.numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown.TabIndex = 4;
            // 
            // howManyToAdd
            // 
            this.howManyToAdd.Location = new System.Drawing.Point(350, 179);
            this.howManyToAdd.Name = "howManyToAdd";
            this.howManyToAdd.Size = new System.Drawing.Size(45, 20);
            this.howManyToAdd.TabIndex = 3;
            this.howManyToAdd.Text = "1";
            // 
            // addRowButton
            // 
            this.addRowButton.Location = new System.Drawing.Point(269, 177);
            this.addRowButton.Name = "addRowButton";
            this.addRowButton.Size = new System.Drawing.Size(75, 23);
            this.addRowButton.TabIndex = 2;
            this.addRowButton.Text = "Add Row";
            this.addRowButton.UseVisualStyleBackColor = true;
            this.addRowButton.Click += new System.EventHandler(this.addRowButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(269, 9);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(341, 162);
            this.dataGridView.TabIndex = 1;
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(9, 9);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 341);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(637, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 363);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.basicPanel);
            this.Name = "MainForm";
            this.Text = "UIA.Fluent Test Application";
            this.basicPanel.ResumeLayout(false);
            this.basicPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel basicPanel;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button addRowButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox howManyToAdd;
        private System.Windows.Forms.NumericUpDown numericUpDown;
		private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button updateHeaders;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button toggleRowButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label fakeCombo;
    }
}

