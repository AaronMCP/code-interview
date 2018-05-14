namespace HYS.DicomAdapter.Common
{
    partial class FormSCU
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
            this.groupBoxDICOM = new System.Windows.Forms.GroupBox();
            this.buttonEcho = new System.Windows.Forms.Button();
            this.textBoxSCUAET = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownSCPPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSCPIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSCPAET = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxTCP = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPDULength = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownTimeOut = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBoxTimer = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownTimer = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxCharacterSet = new System.Windows.Forms.GroupBox();
            this.comboBoxCharacterSet = new System.Windows.Forms.ComboBox();
            this.labelCharacterSet = new System.Windows.Forms.Label();
            this.checkBoxSendCharacterSetTag = new System.Windows.Forms.CheckBox();
            this.groupBoxDICOM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSCPPort)).BeginInit();
            this.groupBoxTCP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPDULength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeOut)).BeginInit();
            this.groupBoxTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimer)).BeginInit();
            this.groupBoxCharacterSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDICOM
            // 
            this.groupBoxDICOM.Controls.Add(this.buttonEcho);
            this.groupBoxDICOM.Controls.Add(this.textBoxSCUAET);
            this.groupBoxDICOM.Controls.Add(this.label6);
            this.groupBoxDICOM.Controls.Add(this.numericUpDownSCPPort);
            this.groupBoxDICOM.Controls.Add(this.label3);
            this.groupBoxDICOM.Controls.Add(this.textBoxSCPIP);
            this.groupBoxDICOM.Controls.Add(this.label1);
            this.groupBoxDICOM.Controls.Add(this.textBoxSCPAET);
            this.groupBoxDICOM.Controls.Add(this.label2);
            this.groupBoxDICOM.Location = new System.Drawing.Point(13, 12);
            this.groupBoxDICOM.Name = "groupBoxDICOM";
            this.groupBoxDICOM.Size = new System.Drawing.Size(375, 145);
            this.groupBoxDICOM.TabIndex = 0;
            this.groupBoxDICOM.TabStop = false;
            this.groupBoxDICOM.Text = "Application Entity";
            // 
            // buttonEcho
            // 
            this.buttonEcho.Location = new System.Drawing.Point(300, 100);
            this.buttonEcho.Name = "buttonEcho";
            this.buttonEcho.Size = new System.Drawing.Size(59, 26);
            this.buttonEcho.TabIndex = 79;
            this.buttonEcho.Text = "Validate";
            this.buttonEcho.UseVisualStyleBackColor = true;
            this.buttonEcho.Click += new System.EventHandler(this.buttonEcho_Click);
            // 
            // textBoxSCUAET
            // 
            this.textBoxSCUAET.Location = new System.Drawing.Point(136, 106);
            this.textBoxSCUAET.Name = "textBoxSCUAET";
            this.textBoxSCUAET.Size = new System.Drawing.Size(155, 20);
            this.textBoxSCUAET.TabIndex = 3;
            this.textBoxSCUAET.Text = "HYSIM_WLSCU";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Calling AE Title:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownSCPPort
            // 
            this.numericUpDownSCPPort.Location = new System.Drawing.Point(136, 54);
            this.numericUpDownSCPPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownSCPPort.Name = "numericUpDownSCPPort";
            this.numericUpDownSCPPort.Size = new System.Drawing.Size(155, 20);
            this.numericUpDownSCPPort.TabIndex = 1;
            this.numericUpDownSCPPort.Value = new decimal(new int[] {
            5678,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Called Port:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSCPIP
            // 
            this.textBoxSCPIP.Location = new System.Drawing.Point(136, 28);
            this.textBoxSCPIP.Name = "textBoxSCPIP";
            this.textBoxSCPIP.Size = new System.Drawing.Size(155, 20);
            this.textBoxSCPIP.TabIndex = 0;
            this.textBoxSCPIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Called IP Address:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSCPAET
            // 
            this.textBoxSCPAET.Location = new System.Drawing.Point(136, 80);
            this.textBoxSCPAET.Name = "textBoxSCPAET";
            this.textBoxSCPAET.Size = new System.Drawing.Size(155, 20);
            this.textBoxSCPAET.TabIndex = 2;
            this.textBoxSCPAET.Text = "JDICOM_WLSCP";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Called AE Title:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxTCP
            // 
            this.groupBoxTCP.Controls.Add(this.label5);
            this.groupBoxTCP.Controls.Add(this.label4);
            this.groupBoxTCP.Controls.Add(this.numericUpDownPDULength);
            this.groupBoxTCP.Controls.Add(this.label7);
            this.groupBoxTCP.Controls.Add(this.numericUpDownTimeOut);
            this.groupBoxTCP.Controls.Add(this.label8);
            this.groupBoxTCP.Location = new System.Drawing.Point(13, 163);
            this.groupBoxTCP.Name = "groupBoxTCP";
            this.groupBoxTCP.Size = new System.Drawing.Size(375, 91);
            this.groupBoxTCP.TabIndex = 1;
            this.groupBoxTCP.TabStop = false;
            this.groupBoxTCP.Text = "DICOM Communication";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(297, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "KB";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(297, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "ms";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownPDULength
            // 
            this.numericUpDownPDULength.Location = new System.Drawing.Point(136, 27);
            this.numericUpDownPDULength.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownPDULength.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownPDULength.Name = "numericUpDownPDULength";
            this.numericUpDownPDULength.Size = new System.Drawing.Size(155, 20);
            this.numericUpDownPDULength.TabIndex = 0;
            this.numericUpDownPDULength.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(21, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "PDU Max Length:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTimeOut
            // 
            this.numericUpDownTimeOut.Location = new System.Drawing.Point(136, 53);
            this.numericUpDownTimeOut.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownTimeOut.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTimeOut.Name = "numericUpDownTimeOut";
            this.numericUpDownTimeOut.Size = new System.Drawing.Size(155, 20);
            this.numericUpDownTimeOut.TabIndex = 1;
            this.numericUpDownTimeOut.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(21, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Association Time Out:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxTimer
            // 
            this.groupBoxTimer.Controls.Add(this.label10);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimer);
            this.groupBoxTimer.Controls.Add(this.label9);
            this.groupBoxTimer.Location = new System.Drawing.Point(402, 12);
            this.groupBoxTimer.Name = "groupBoxTimer";
            this.groupBoxTimer.Size = new System.Drawing.Size(336, 53);
            this.groupBoxTimer.TabIndex = 2;
            this.groupBoxTimer.TabStop = false;
            this.groupBoxTimer.Text = "Timer Control";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(304, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 20);
            this.label10.TabIndex = 72;
            this.label10.Text = "ms";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTimer
            // 
            this.numericUpDownTimer.Location = new System.Drawing.Point(203, 21);
            this.numericUpDownTimer.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownTimer.Name = "numericUpDownTimer";
            this.numericUpDownTimer.Size = new System.Drawing.Size(95, 20);
            this.numericUpDownTimer.TabIndex = 0;
            this.numericUpDownTimer.Value = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(198, 20);
            this.label9.TabIndex = 72;
            this.label9.Text = "Invoke DICOM Association in Every:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxCharacterSet
            // 
            this.groupBoxCharacterSet.Controls.Add(this.checkBoxSendCharacterSetTag);
            this.groupBoxCharacterSet.Controls.Add(this.comboBoxCharacterSet);
            this.groupBoxCharacterSet.Controls.Add(this.labelCharacterSet);
            this.groupBoxCharacterSet.Location = new System.Drawing.Point(402, 71);
            this.groupBoxCharacterSet.Name = "groupBoxCharacterSet";
            this.groupBoxCharacterSet.Size = new System.Drawing.Size(336, 106);
            this.groupBoxCharacterSet.TabIndex = 3;
            this.groupBoxCharacterSet.TabStop = false;
            this.groupBoxCharacterSet.Text = "Character Set";
            // 
            // comboBoxCharacterSet
            // 
            this.comboBoxCharacterSet.FormattingEnabled = true;
            this.comboBoxCharacterSet.Items.AddRange(new object[] {
            "GB18030",
            "ISO_IR 6",
            "ISO_IR 192",
            "ISO_IR 100",
            "ISO_IR 101",
            "ISO_IR 109",
            "ISO_IR 110",
            "ISO_IR 148",
            "ISO_IR 144",
            "ISO_IR 127",
            "ISO_IR 126",
            "ISO_IR 138",
            "ISO 2022 IR 13",
            "ISO 2022 IR 87",
            "ISO 2022 IR 149",
            "ISO 2022 IR 13\\ISO 2022 IR 87"});
            this.comboBoxCharacterSet.Location = new System.Drawing.Point(15, 46);
            this.comboBoxCharacterSet.MaxDropDownItems = 12;
            this.comboBoxCharacterSet.Name = "comboBoxCharacterSet";
            this.comboBoxCharacterSet.Size = new System.Drawing.Size(283, 21);
            this.comboBoxCharacterSet.TabIndex = 0;
            // 
            // labelCharacterSet
            // 
            this.labelCharacterSet.Location = new System.Drawing.Point(12, 22);
            this.labelCharacterSet.Name = "labelCharacterSet";
            this.labelCharacterSet.Size = new System.Drawing.Size(307, 18);
            this.labelCharacterSet.TabIndex = 26;
            this.labelCharacterSet.Text = "Use the following character set to encode DICOM data:";
            this.labelCharacterSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxSendCharacterSetTag
            // 
            this.checkBoxSendCharacterSetTag.AutoSize = true;
            this.checkBoxSendCharacterSetTag.Location = new System.Drawing.Point(15, 73);
            this.checkBoxSendCharacterSetTag.Name = "checkBoxSendCharacterSetTag";
            this.checkBoxSendCharacterSetTag.Size = new System.Drawing.Size(272, 17);
            this.checkBoxSendCharacterSetTag.TabIndex = 27;
            this.checkBoxSendCharacterSetTag.Text = "Ensure sending character set tag in DICOM request.";
            this.checkBoxSendCharacterSetTag.UseVisualStyleBackColor = true;
            // 
            // FormSCU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 314);
            this.Controls.Add(this.groupBoxCharacterSet);
            this.Controls.Add(this.groupBoxTimer);
            this.Controls.Add(this.groupBoxTCP);
            this.Controls.Add(this.groupBoxDICOM);
            this.Name = "FormSCU";
            this.Text = "SCU Setting";
            this.Load += new System.EventHandler(this.FormSCU_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSCU_FormClosing);
            this.groupBoxDICOM.ResumeLayout(false);
            this.groupBoxDICOM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSCPPort)).EndInit();
            this.groupBoxTCP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPDULength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeOut)).EndInit();
            this.groupBoxTimer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimer)).EndInit();
            this.groupBoxCharacterSet.ResumeLayout(false);
            this.groupBoxCharacterSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDICOM;
        private System.Windows.Forms.TextBox textBoxSCUAET;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownSCPPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSCPIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSCPAET;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxTCP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownPDULength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeOut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonEcho;
        private System.Windows.Forms.GroupBox groupBoxTimer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownTimer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBoxCharacterSet;
        private System.Windows.Forms.ComboBox comboBoxCharacterSet;
        private System.Windows.Forms.Label labelCharacterSet;
        private System.Windows.Forms.CheckBox checkBoxSendCharacterSetTag;
    }
}