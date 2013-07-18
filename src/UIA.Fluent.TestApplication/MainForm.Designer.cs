namespace UIA.Fluent.TestApplication
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
            this.SuspendLayout();
            // 
            // basicPanel
            // 
            this.basicPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basicPanel.Location = new System.Drawing.Point(12, 12);
            this.basicPanel.Name = "basicPanel";
            this.basicPanel.Size = new System.Drawing.Size(589, 288);
            this.basicPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 312);
            this.Controls.Add(this.basicPanel);
            this.Name = "MainForm";
            this.Text = "UIA.Fluent Test Application";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel basicPanel;
    }
}

