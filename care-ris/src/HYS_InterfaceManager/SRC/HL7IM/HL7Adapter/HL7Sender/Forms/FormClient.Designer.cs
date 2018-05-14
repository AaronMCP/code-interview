namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms
{
    partial class FormClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClient));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.checkBoxSendWrap = new System.Windows.Forms.CheckBox();
            this.checkBoxReceiveWrap = new System.Windows.Forms.CheckBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxSample = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSegment = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSetValue = new System.Windows.Forms.Button();
            this.buttonGetValue = new System.Windows.Forms.Button();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownField = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelProcessingTime = new System.Windows.Forms.Label();
            this.buttonSendBatch = new System.Windows.Forms.Button();
            this.checkBoxReplace0D = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownField)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(238, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sending Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSend
            // 
            this.textBoxSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSend.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSend.Location = new System.Drawing.Point(241, 38);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSend.Size = new System.Drawing.Size(597, 159);
            this.textBoxSend.TabIndex = 1;
            this.textBoxSend.Text = resources.GetString("textBoxSend.Text");
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReceive.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceive.Location = new System.Drawing.Point(241, 233);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReceive.Size = new System.Drawing.Size(597, 104);
            this.textBoxReceive.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(238, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Receiving Message";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(860, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server IP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIP.Location = new System.Drawing.Point(860, 38);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(90, 20);
            this.textBoxIP.TabIndex = 5;
            this.textBoxIP.Text = "10.112.12.204";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(860, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Server Port";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(860, 159);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(90, 38);
            this.buttonSend.TabIndex = 8;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // checkBoxSendWrap
            // 
            this.checkBoxSendWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSendWrap.AutoSize = true;
            this.checkBoxSendWrap.Checked = true;
            this.checkBoxSendWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSendWrap.Location = new System.Drawing.Point(758, 18);
            this.checkBoxSendWrap.Name = "checkBoxSendWrap";
            this.checkBoxSendWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxSendWrap.TabIndex = 9;
            this.checkBoxSendWrap.Text = "Word Wrap";
            this.checkBoxSendWrap.UseVisualStyleBackColor = true;
            this.checkBoxSendWrap.CheckedChanged += new System.EventHandler(this.checkBoxSendWrap_CheckedChanged);
            // 
            // checkBoxReceiveWrap
            // 
            this.checkBoxReceiveWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxReceiveWrap.AutoSize = true;
            this.checkBoxReceiveWrap.Checked = true;
            this.checkBoxReceiveWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReceiveWrap.Location = new System.Drawing.Point(758, 213);
            this.checkBoxReceiveWrap.Name = "checkBoxReceiveWrap";
            this.checkBoxReceiveWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxReceiveWrap.TabIndex = 10;
            this.checkBoxReceiveWrap.Text = "Word Wrap";
            this.checkBoxReceiveWrap.UseVisualStyleBackColor = true;
            this.checkBoxReceiveWrap.CheckedChanged += new System.EventHandler(this.checkBoxReceiveWrap_CheckedChanged);
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPort.Location = new System.Drawing.Point(860, 93);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownPort.TabIndex = 11;
            this.numericUpDownPort.Value = new decimal(new int[] {
            1234,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Sample Messages";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxSample
            // 
            this.listBoxSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxSample.FormattingEnabled = true;
            this.listBoxSample.Location = new System.Drawing.Point(16, 38);
            this.listBoxSample.Name = "listBoxSample";
            this.listBoxSample.Size = new System.Drawing.Size(204, 160);
            this.listBoxSample.TabIndex = 13;
            this.listBoxSample.SelectedIndexChanged += new System.EventHandler(this.listBoxSample_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(7, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Segment:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSegment
            // 
            this.textBoxSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSegment.Location = new System.Drawing.Point(68, 11);
            this.textBoxSegment.Name = "textBoxSegment";
            this.textBoxSegment.Size = new System.Drawing.Size(39, 20);
            this.textBoxSegment.TabIndex = 15;
            this.textBoxSegment.Text = "MSH";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.buttonSetValue);
            this.panel1.Controls.Add(this.buttonGetValue);
            this.panel1.Controls.Add(this.textBoxValue);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.numericUpDownField);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBoxSegment);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(17, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 103);
            this.panel1.TabIndex = 16;
            // 
            // buttonSetValue
            // 
            this.buttonSetValue.Location = new System.Drawing.Point(105, 63);
            this.buttonSetValue.Name = "buttonSetValue";
            this.buttonSetValue.Size = new System.Drawing.Size(84, 28);
            this.buttonSetValue.TabIndex = 21;
            this.buttonSetValue.Text = "Set Value";
            this.buttonSetValue.UseVisualStyleBackColor = true;
            this.buttonSetValue.Click += new System.EventHandler(this.buttonSetValue_Click);
            // 
            // buttonGetValue
            // 
            this.buttonGetValue.Location = new System.Drawing.Point(10, 63);
            this.buttonGetValue.Name = "buttonGetValue";
            this.buttonGetValue.Size = new System.Drawing.Size(89, 28);
            this.buttonGetValue.TabIndex = 20;
            this.buttonGetValue.Text = "Get Value";
            this.buttonGetValue.UseVisualStyleBackColor = true;
            this.buttonGetValue.Click += new System.EventHandler(this.buttonGetValue_Click);
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(68, 37);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(121, 20);
            this.textBoxValue.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Location = new System.Drawing.Point(7, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Field Value:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownField
            // 
            this.numericUpDownField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownField.Location = new System.Drawing.Point(146, 11);
            this.numericUpDownField.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownField.Name = "numericUpDownField";
            this.numericUpDownField.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownField.TabIndex = 17;
            this.numericUpDownField.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(114, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Field:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Location = new System.Drawing.Point(14, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(205, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Parse The Sending Message";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProcessingTime
            // 
            this.labelProcessingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProcessingTime.Location = new System.Drawing.Point(362, 210);
            this.labelProcessingTime.Name = "labelProcessingTime";
            this.labelProcessingTime.Size = new System.Drawing.Size(374, 20);
            this.labelProcessingTime.TabIndex = 18;
            this.labelProcessingTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSendBatch
            // 
            this.buttonSendBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSendBatch.Location = new System.Drawing.Point(860, 299);
            this.buttonSendBatch.Name = "buttonSendBatch";
            this.buttonSendBatch.Size = new System.Drawing.Size(90, 38);
            this.buttonSendBatch.TabIndex = 19;
            this.buttonSendBatch.Text = "Send Batch";
            this.buttonSendBatch.UseVisualStyleBackColor = true;
            this.buttonSendBatch.Click += new System.EventHandler(this.buttonSendBatch_Click);
            // 
            // checkBoxReplace0D
            // 
            this.checkBoxReplace0D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxReplace0D.AutoSize = true;
            this.checkBoxReplace0D.Checked = true;
            this.checkBoxReplace0D.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReplace0D.Location = new System.Drawing.Point(538, 18);
            this.checkBoxReplace0D.Name = "checkBoxReplace0D";
            this.checkBoxReplace0D.Size = new System.Drawing.Size(198, 17);
            this.checkBoxReplace0D.TabIndex = 20;
            this.checkBoxReplace0D.Text = "Replace 0D0A to 0D before sending";
            this.checkBoxReplace0D.UseVisualStyleBackColor = true;
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 354);
            this.Controls.Add(this.checkBoxReplace0D);
            this.Controls.Add(this.buttonSendBatch);
            this.Controls.Add(this.labelProcessingTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBoxSample);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownPort);
            this.Controls.Add(this.checkBoxReceiveWrap);
            this.Controls.Add(this.checkBoxSendWrap);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxReceive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.label1);
            this.Name = "FormClient";
            this.Text = "HL7 Client";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.CheckBox checkBoxSendWrap;
        private System.Windows.Forms.CheckBox checkBoxReceiveWrap;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxSample;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSegment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button buttonSetValue;
        private System.Windows.Forms.Button buttonGetValue;
        private System.Windows.Forms.Label labelProcessingTime;
        private System.Windows.Forms.Button buttonSendBatch;
        private System.Windows.Forms.CheckBox checkBoxReplace0D;
    }
}

