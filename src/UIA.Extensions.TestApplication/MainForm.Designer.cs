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
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.howManyToAdd = new System.Windows.Forms.TextBox();
            this.addRowButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.deleteButton = new System.Windows.Forms.Button();
            this.basicPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // basicPanel
            // 
            this.basicPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basicPanel.Controls.Add(this.deleteButton);
            this.basicPanel.Controls.Add(this.numericUpDown);
            this.basicPanel.Controls.Add(this.howManyToAdd);
            this.basicPanel.Controls.Add(this.addRowButton);
            this.basicPanel.Controls.Add(this.dataGridView);
            this.basicPanel.Controls.Add(this.monthCalendar);
            this.basicPanel.Location = new System.Drawing.Point(12, 12);
            this.basicPanel.Name = "basicPanel";
            this.basicPanel.Size = new System.Drawing.Size(613, 262);
            this.basicPanel.TabIndex = 0;
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
            this.dataGridView.Size = new System.Drawing.Size(341, 162);
            this.dataGridView.TabIndex = 1;
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(9, 9);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 0;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 286);
            this.Controls.Add(this.basicPanel);
            this.Name = "MainForm";
            this.Text = "UIA.Fluent Test Application";
            this.basicPanel.ResumeLayout(false);
            this.basicPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel basicPanel;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button addRowButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox howManyToAdd;
        private System.Windows.Forms.NumericUpDown numericUpDown;
		private System.Windows.Forms.Button deleteButton;
    }
}

