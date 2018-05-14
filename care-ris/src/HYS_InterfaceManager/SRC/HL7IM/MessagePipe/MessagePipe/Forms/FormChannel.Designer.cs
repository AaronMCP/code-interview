namespace HYS.MessageDevices.MessagePipe.Forms
{
    partial class FormChannel
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
            this.label2 = new System.Windows.Forms.Label();
            this.tBoxChannelName = new System.Windows.Forms.TextBox();
            this.tBoxChannelDescription = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxChnType = new System.Windows.Forms.ComboBox();
            this.buttonChnSetting = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckBoxChannelEnable = new System.Windows.Forms.CheckBox();
            this.gBoxSetting = new System.Windows.Forms.GroupBox();
            this.rtBoxSetting = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gBoxSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Channel Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Channel Description:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tBoxChannelName
            // 
            this.tBoxChannelName.Location = new System.Drawing.Point(164, 19);
            this.tBoxChannelName.Name = "tBoxChannelName";
            this.tBoxChannelName.Size = new System.Drawing.Size(155, 20);
            this.tBoxChannelName.TabIndex = 9;
            // 
            // tBoxChannelDescription
            // 
            this.tBoxChannelDescription.Location = new System.Drawing.Point(164, 45);
            this.tBoxChannelDescription.Name = "tBoxChannelDescription";
            this.tBoxChannelDescription.Size = new System.Drawing.Size(155, 20);
            this.tBoxChannelDescription.TabIndex = 10;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(243, 284);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 25);
            this.buttonOK.TabIndex = 22;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(325, 284);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(30, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Channel Type:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxChnType
            // 
            this.comboBoxChnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChnType.FormattingEnabled = true;
            this.comboBoxChnType.Location = new System.Drawing.Point(164, 77);
            this.comboBoxChnType.Name = "comboBoxChnType";
            this.comboBoxChnType.Size = new System.Drawing.Size(154, 21);
            this.comboBoxChnType.TabIndex = 24;
            this.comboBoxChnType.SelectedIndexChanged += new System.EventHandler(this.comboBoxChnType_SelectedIndexChanged);
            // 
            // buttonChnSetting
            // 
            this.buttonChnSetting.Location = new System.Drawing.Point(313, 16);
            this.buttonChnSetting.Name = "buttonChnSetting";
            this.buttonChnSetting.Size = new System.Drawing.Size(94, 24);
            this.buttonChnSetting.TabIndex = 25;
            this.buttonChnSetting.Text = "Setting";
            this.buttonChnSetting.UseVisualStyleBackColor = true;
            this.buttonChnSetting.Click += new System.EventHandler(this.buttonChnSetting_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gBoxSetting);
            this.splitContainer1.Size = new System.Drawing.Size(413, 278);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckBoxChannelEnable);
            this.groupBox1.Controls.Add(this.tBoxChannelName);
            this.groupBox1.Controls.Add(this.comboBoxChnType);
            this.groupBox1.Controls.Add(this.tBoxChannelDescription);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 151);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generic Channel Configuration";
            // 
            // ckBoxChannelEnable
            // 
            this.ckBoxChannelEnable.AutoSize = true;
            this.ckBoxChannelEnable.Checked = true;
            this.ckBoxChannelEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxChannelEnable.Location = new System.Drawing.Point(33, 110);
            this.ckBoxChannelEnable.Name = "ckBoxChannelEnable";
            this.ckBoxChannelEnable.Size = new System.Drawing.Size(101, 17);
            this.ckBoxChannelEnable.TabIndex = 25;
            this.ckBoxChannelEnable.Text = "Enable Channel";
            this.ckBoxChannelEnable.UseVisualStyleBackColor = true;
            // 
            // gBoxSetting
            // 
            this.gBoxSetting.Controls.Add(this.rtBoxSetting);
            this.gBoxSetting.Controls.Add(this.buttonChnSetting);
            this.gBoxSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBoxSetting.Location = new System.Drawing.Point(0, 0);
            this.gBoxSetting.Name = "gBoxSetting";
            this.gBoxSetting.Size = new System.Drawing.Size(413, 123);
            this.gBoxSetting.TabIndex = 26;
            this.gBoxSetting.TabStop = false;
            this.gBoxSetting.Text = "Message Processing Configuration";
            // 
            // rtBoxSetting
            // 
            this.rtBoxSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtBoxSetting.Location = new System.Drawing.Point(3, 16);
            this.rtBoxSetting.Name = "rtBoxSetting";
            this.rtBoxSetting.ReadOnly = true;
            this.rtBoxSetting.Size = new System.Drawing.Size(304, 104);
            this.rtBoxSetting.TabIndex = 26;
            this.rtBoxSetting.Text = "";
            // 
            // FormChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 321);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "FormChannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Channel Configuration";
            this.Load += new System.EventHandler(this.FormChannel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gBoxSetting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBoxChannelName;
        private System.Windows.Forms.TextBox tBoxChannelDescription;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxChnType;
        private System.Windows.Forms.Button buttonChnSetting;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckBoxChannelEnable;
        private System.Windows.Forms.GroupBox gBoxSetting;
        private System.Windows.Forms.RichTextBox rtBoxSetting;
    }
}