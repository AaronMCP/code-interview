namespace Common.LicenseEditor
{
    partial class LicenseEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLicenseFile = new System.Windows.Forms.TextBox();
            this.buttonBrowns = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDeviceName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxEnabled = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "License File:";
            // 
            // textBoxLicenseFile
            // 
            this.textBoxLicenseFile.Location = new System.Drawing.Point(107, 21);
            this.textBoxLicenseFile.Name = "textBoxLicenseFile";
            this.textBoxLicenseFile.Size = new System.Drawing.Size(352, 20);
            this.textBoxLicenseFile.TabIndex = 1;
            // 
            // buttonBrowns
            // 
            this.buttonBrowns.Location = new System.Drawing.Point(465, 20);
            this.buttonBrowns.Name = "buttonBrowns";
            this.buttonBrowns.Size = new System.Drawing.Size(39, 23);
            this.buttonBrowns.TabIndex = 2;
            this.buttonBrowns.Text = "...";
            this.buttonBrowns.UseVisualStyleBackColor = true;
            this.buttonBrowns.Click += new System.EventHandler(this.buttonBrowns_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Device Name:";
            // 
            // comboBoxDeviceName
            // 
            this.comboBoxDeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDeviceName.FormattingEnabled = true;
            this.comboBoxDeviceName.Items.AddRange(new object[] {
            "HL7_SENDER",
            "HL7_RECEIVER"});
            this.comboBoxDeviceName.Location = new System.Drawing.Point(105, 31);
            this.comboBoxDeviceName.Name = "comboBoxDeviceName";
            this.comboBoxDeviceName.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDeviceName.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxEnabled);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxDeviceName);
            this.groupBox1.Location = new System.Drawing.Point(38, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 77);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lisence Info";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Enabled:";
            // 
            // comboBoxEnabled
            // 
            this.comboBoxEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnabled.FormattingEnabled = true;
            this.comboBoxEnabled.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comboBoxEnabled.Location = new System.Drawing.Point(317, 31);
            this.comboBoxEnabled.Name = "comboBoxEnabled";
            this.comboBoxEnabled.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEnabled.TabIndex = 11;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(355, 158);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(61, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(433, 158);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(61, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LicenseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 205);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonBrowns);
            this.Controls.Add(this.textBoxLicenseFile);
            this.Controls.Add(this.label1);
            this.Name = "LicenseEditor";
            this.Text = "HL7 Gateway License Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLicenseFile;
        private System.Windows.Forms.Button buttonBrowns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDeviceName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxEnabled;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}

