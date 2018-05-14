namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Forms
{
    partial class FormConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDisableMLLP = new System.Windows.Forms.CheckBox();
            this.checkBoxMultipleSession = new System.Windows.Forms.CheckBox();
            this.comboBoxCodePage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSuccessTemplate = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxProcessingType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonFailureTemplate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDispatch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxDisableMLLP);
            this.groupBox1.Controls.Add(this.checkBoxMultipleSession);
            this.groupBox1.Controls.Add(this.comboBoxCodePage);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 88);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HL7 Listener Setting";
            // 
            // checkBoxDisableMLLP
            // 
            this.checkBoxDisableMLLP.Location = new System.Drawing.Point(430, 59);
            this.checkBoxDisableMLLP.Name = "checkBoxDisableMLLP";
            this.checkBoxDisableMLLP.Size = new System.Drawing.Size(98, 19);
            this.checkBoxDisableMLLP.TabIndex = 29;
            this.checkBoxDisableMLLP.Text = "Disable MLLP";
            this.checkBoxDisableMLLP.UseVisualStyleBackColor = true;
            // 
            // checkBoxMultipleSession
            // 
            this.checkBoxMultipleSession.Location = new System.Drawing.Point(241, 59);
            this.checkBoxMultipleSession.Name = "checkBoxMultipleSession";
            this.checkBoxMultipleSession.Size = new System.Drawing.Size(179, 19);
            this.checkBoxMultipleSession.TabIndex = 28;
            this.checkBoxMultipleSession.Text = "Multiple session in a connection";
            this.checkBoxMultipleSession.UseVisualStyleBackColor = true;
            // 
            // comboBoxCodePage
            // 
            this.comboBoxCodePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodePage.FormattingEnabled = true;
            this.comboBoxCodePage.Location = new System.Drawing.Point(242, 29);
            this.comboBoxCodePage.MaxDropDownItems = 12;
            this.comboBoxCodePage.Name = "comboBoxCodePage";
            this.comboBoxCodePage.Size = new System.Drawing.Size(260, 21);
            this.comboBoxCodePage.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(149, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 19);
            this.label4.TabIndex = 26;
            this.label4.Text = "Character Set:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(53, 29);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownPort.TabIndex = 23;
            this.numericUpDownPort.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSuccessTemplate
            // 
            this.buttonSuccessTemplate.Location = new System.Drawing.Point(23, 165);
            this.buttonSuccessTemplate.Name = "buttonSuccessTemplate";
            this.buttonSuccessTemplate.Size = new System.Drawing.Size(187, 24);
            this.buttonSuccessTemplate.TabIndex = 24;
            this.buttonSuccessTemplate.Text = "Success Acknowledgement";
            this.buttonSuccessTemplate.UseVisualStyleBackColor = true;
            this.buttonSuccessTemplate.Click += new System.EventHandler(this.buttonSuccessTemplate_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(465, 333);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 25);
            this.buttonOK.TabIndex = 24;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(547, 333);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBoxProcessingType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonFailureTemplate);
            this.groupBox2.Controls.Add(this.buttonSuccessTemplate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonDispatch);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 212);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HL7 Message Processing";
            // 
            // comboBoxProcessingType
            // 
            this.comboBoxProcessingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessingType.FormattingEnabled = true;
            this.comboBoxProcessingType.Items.AddRange(new object[] {
            "Use raw HL7v2 text in the MLLP payload as message content",
            "Transform HL7v2 text in the MLLP payload to XML as message content",
            "Use raw XML in the MLLP payload as message content"});
            this.comboBoxProcessingType.Location = new System.Drawing.Point(23, 48);
            this.comboBoxProcessingType.MaxDropDownItems = 12;
            this.comboBoxProcessingType.Name = "comboBoxProcessingType";
            this.comboBoxProcessingType.Size = new System.Drawing.Size(394, 21);
            this.comboBoxProcessingType.TabIndex = 28;
            this.comboBoxProcessingType.SelectedIndexChanged += new System.EventHandler(this.comboBoxProcessingType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(20, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(570, 20);
            this.label5.TabIndex = 29;
            this.label5.Text = "The following rule determine how to transform incoming MLLP payload to incoming (" +
                "XML) message:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonFailureTemplate
            // 
            this.buttonFailureTemplate.Location = new System.Drawing.Point(230, 165);
            this.buttonFailureTemplate.Name = "buttonFailureTemplate";
            this.buttonFailureTemplate.Size = new System.Drawing.Size(187, 24);
            this.buttonFailureTemplate.TabIndex = 26;
            this.buttonFailureTemplate.Text = "Failure Acknowledgement";
            this.buttonFailureTemplate.UseVisualStyleBackColor = true;
            this.buttonFailureTemplate.Click += new System.EventHandler(this.buttonFailureTemplate_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(20, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(570, 29);
            this.label3.TabIndex = 28;
            this.label3.Text = "After dispatching incoming message to publish channel, the following templates ge" +
                "nerate outgoing acknowledgement:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDispatch
            // 
            this.buttonDispatch.Location = new System.Drawing.Point(23, 106);
            this.buttonDispatch.Name = "buttonDispatch";
            this.buttonDispatch.Size = new System.Drawing.Size(187, 24);
            this.buttonDispatch.TabIndex = 27;
            this.buttonDispatch.Text = "Message Dispatching Setting";
            this.buttonDispatch.UseVisualStyleBackColor = true;
            this.buttonDispatch.Click += new System.EventHandler(this.buttonDispatch_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(20, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(570, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "The following rule determine how to dispatch incoming message to publish channel " +
                "or request channel:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 369);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "FormConfig";
            this.Text = "HL7 Inbound Configuration";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSuccessTemplate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonDispatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonFailureTemplate;
        private System.Windows.Forms.ComboBox comboBoxCodePage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxMultipleSession;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxProcessingType;
        private System.Windows.Forms.CheckBox checkBoxDisableMLLP;
    }
}