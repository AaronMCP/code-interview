namespace HYS.FileAdapter.FileOutboundAdapterConfiguration.UIControls
{
    partial class FFileField
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSelFileFields = new System.Windows.Forms.Button();
            this.tbTemplate = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbFileDTFormat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFileSuffix = new System.Windows.Forms.TextBox();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.tbFilePrefix = new System.Windows.Forms.TextBox();
            this.btSelFilePath = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btSelFileFields);
            this.groupBox2.Controls.Add(this.tbTemplate);
            this.groupBox2.Location = new System.Drawing.Point(13, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 181);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Field  Editor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Edit File Field";
            // 
            // btSelFileFields
            // 
            this.btSelFileFields.Location = new System.Drawing.Point(287, 14);
            this.btSelFileFields.Name = "btSelFileFields";
            this.btSelFileFields.Size = new System.Drawing.Size(159, 23);
            this.btSelFileFields.TabIndex = 43;
            this.btSelFileFields.Text = "Select GCGateway Field";
            this.btSelFileFields.UseVisualStyleBackColor = true;
            this.btSelFileFields.Click += new System.EventHandler(this.btSelFileFields_Click);
            // 
            // tbTemplate
            // 
            this.tbTemplate.Location = new System.Drawing.Point(11, 47);
            this.tbTemplate.Multiline = true;
            this.tbTemplate.Name = "tbTemplate";
            this.tbTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTemplate.Size = new System.Drawing.Size(435, 124);
            this.tbTemplate.TabIndex = 42;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbFileDTFormat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btSelFilePath);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbFileSuffix);
            this.groupBox1.Controls.Add(this.tbFilePath);
            this.groupBox1.Controls.Add(this.tbFilePrefix);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 122);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination File Name Setup";
            // 
            // tbFileDTFormat
            // 
            this.tbFileDTFormat.Location = new System.Drawing.Point(154, 83);
            this.tbFileDTFormat.Name = "tbFileDTFormat";
            this.tbFileDTFormat.Size = new System.Drawing.Size(256, 20);
            this.tbFileDTFormat.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Folder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "DateTime Format";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Prefix";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Extension";
            // 
            // tbFileSuffix
            // 
            this.tbFileSuffix.Location = new System.Drawing.Point(333, 23);
            this.tbFileSuffix.Name = "tbFileSuffix";
            this.tbFileSuffix.Size = new System.Drawing.Size(77, 20);
            this.tbFileSuffix.TabIndex = 42;
            this.tbFileSuffix.Text = ".txt";
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(154, 53);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(256, 20);
            this.tbFilePath.TabIndex = 40;
            // 
            // tbFilePrefix
            // 
            this.tbFilePrefix.Location = new System.Drawing.Point(154, 20);
            this.tbFilePrefix.Name = "tbFilePrefix";
            this.tbFilePrefix.Size = new System.Drawing.Size(61, 20);
            this.tbFilePrefix.TabIndex = 41;
            // 
            // btSelFilePath
            // 
            this.btSelFilePath.BackgroundImage = global::HYS.FileAdapter.FileOutboundAdapterConfiguration.Properties.Resources.folder;
            this.btSelFilePath.Location = new System.Drawing.Point(392, 53);
            this.btSelFilePath.Name = "btSelFilePath";
            this.btSelFilePath.Size = new System.Drawing.Size(18, 17);
            this.btSelFilePath.TabIndex = 43;
            this.btSelFilePath.Text = "...";
            this.btSelFilePath.UseVisualStyleBackColor = true;
            this.btSelFilePath.Click += new System.EventHandler(this.btSelFilePath_Click);
            // 
            // FFileField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FFileField";
            this.Size = new System.Drawing.Size(464, 380);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbTemplate;
        private System.Windows.Forms.Button btSelFileFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbFileDTFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btSelFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFileSuffix;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.TextBox tbFilePrefix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
