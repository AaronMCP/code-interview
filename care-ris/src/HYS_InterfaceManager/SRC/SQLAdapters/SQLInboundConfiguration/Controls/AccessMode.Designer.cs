namespace HYS.SQLInboundAdapterConfiguration.Controls
{
    partial class AccessMode
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
            this.panelChannels = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFilePathGet = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblms = new System.Windows.Forms.Label();
            this.lblTimeInterval = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panelChannels.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChannels
            // 
            this.panelChannels.Controls.Add(this.groupBox3);
            this.panelChannels.Location = new System.Drawing.Point(0, 0);
            this.panelChannels.Name = "panelChannels";
            this.panelChannels.Size = new System.Drawing.Size(750, 455);
            this.panelChannels.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnFilePathGet);
            this.groupBox3.Controls.Add(this.txtFilePath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblms);
            this.groupBox3.Controls.Add(this.lblTimeInterval);
            this.groupBox3.Controls.Add(this.numericUpDown);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox3.Location = new System.Drawing.Point(15, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(723, 103);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Third Party Application Channels";
            // 
            // btnFilePathGet
            // 
            this.btnFilePathGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilePathGet.Location = new System.Drawing.Point(393, 60);
            this.btnFilePathGet.Name = "btnFilePathGet";
            this.btnFilePathGet.Size = new System.Drawing.Size(36, 23);
            this.btnFilePathGet.TabIndex = 114;
            this.btnFilePathGet.Text = "...";
            this.btnFilePathGet.UseVisualStyleBackColor = true;
            this.btnFilePathGet.Click += new System.EventHandler(this.btnFilePathGet_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(140, 62);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(250, 20);
            this.txtFilePath.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 112;
            this.label1.Text = "File";
            // 
            // lblms
            // 
            this.lblms.AutoSize = true;
            this.lblms.Location = new System.Drawing.Point(390, 28);
            this.lblms.Name = "lblms";
            this.lblms.Size = new System.Drawing.Size(20, 13);
            this.lblms.TabIndex = 111;
            this.lblms.Text = "ms";
            // 
            // lblTimeInterval
            // 
            this.lblTimeInterval.AutoSize = true;
            this.lblTimeInterval.Location = new System.Drawing.Point(20, 27);
            this.lblTimeInterval.Name = "lblTimeInterval";
            this.lblTimeInterval.Size = new System.Drawing.Size(80, 13);
            this.lblTimeInterval.TabIndex = 104;
            this.lblTimeInterval.Text = "Access Interval";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(140, 25);
            this.numericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(250, 20);
            this.numericUpDown.TabIndex = 0;
            this.numericUpDown.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // AccessMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelChannels);
            this.Name = "AccessMode";
            this.Size = new System.Drawing.Size(750, 455);
            this.panelChannels.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChannels;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTimeInterval;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Label lblms;
        private System.Windows.Forms.Button btnFilePathGet;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
    }
}
