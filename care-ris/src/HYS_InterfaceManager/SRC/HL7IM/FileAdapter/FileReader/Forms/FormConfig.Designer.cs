namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Forms
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label10;
            this.textBoxINFolder = new System.Windows.Forms.TextBox();
            this.textBoxDisposeFolder = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxProcessingType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFailureTemplate = new System.Windows.Forms.Button();
            this.buttonSuccessTemplate = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxDispose = new System.Windows.Forms.ComboBox();
            this.buttonDispatch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.textBoxFileExtension = new System.Windows.Forms.TextBox();
            this.comboBoxCodePage = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(22, 27);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 20);
            label2.TabIndex = 25;
            label2.Text = "File Folder:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label5.Location = new System.Drawing.Point(20, 74);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(578, 19);
            label5.TabIndex = 28;
            label5.Text = "After dispatching incoming message to publish channel, how to dispose the source " +
                "message file:";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label6.Location = new System.Drawing.Point(9, 448);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(246, 29);
            label6.TabIndex = 26;
            label6.Text = "The following rule determine how to dispatch incoming message to subscriber or re" +
                "sponser entity:";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label6.Visible = false;
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(22, 62);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(89, 19);
            label7.TabIndex = 26;
            label7.Text = "Character Set:";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(382, 65);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(74, 13);
            label4.TabIndex = 34;
            label4.Text = "Timer Interval:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(382, 31);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(75, 13);
            label8.TabIndex = 36;
            label8.Text = "File Extension:";
            // 
            // label10
            // 
            label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label10.Location = new System.Drawing.Point(21, 121);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(578, 23);
            label10.TabIndex = 40;
            label10.Text = "After dispatching incoming message to publish channel, output the result to follo" +
                "wing folder:";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxINFolder
            // 
            this.textBoxINFolder.Location = new System.Drawing.Point(101, 27);
            this.textBoxINFolder.Name = "textBoxINFolder";
            this.textBoxINFolder.Size = new System.Drawing.Size(258, 20);
            this.textBoxINFolder.TabIndex = 24;
            // 
            // textBoxDisposeFolder
            // 
            this.textBoxDisposeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDisposeFolder.Location = new System.Drawing.Point(23, 145);
            this.textBoxDisposeFolder.Name = "textBoxDisposeFolder";
            this.textBoxDisposeFolder.Size = new System.Drawing.Size(561, 20);
            this.textBoxDisposeFolder.TabIndex = 27;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(478, 386);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(67, 25);
            this.buttonOK.TabIndex = 29;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(563, 386);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(67, 25);
            this.buttonCancel.TabIndex = 28;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBoxProcessingType);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(label10);
            this.groupBox2.Controls.Add(this.buttonFailureTemplate);
            this.groupBox2.Controls.Add(this.buttonSuccessTemplate);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.comboBoxDispose);
            this.groupBox2.Controls.Add(label5);
            this.groupBox2.Controls.Add(this.textBoxDisposeFolder);
            this.groupBox2.Location = new System.Drawing.Point(16, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 248);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Message Processing";
            // 
            // comboBoxProcessingType
            // 
            this.comboBoxProcessingType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProcessingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessingType.FormattingEnabled = true;
            this.comboBoxProcessingType.Items.AddRange(new object[] {
            "Use raw HL7v2 text in the file content as message content",
            "Transform HL7v2 text in the file content to XML as message content",
            "Use raw XML in the file content as message content"});
            this.comboBoxProcessingType.Location = new System.Drawing.Point(25, 48);
            this.comboBoxProcessingType.MaxDropDownItems = 12;
            this.comboBoxProcessingType.Name = "comboBoxProcessingType";
            this.comboBoxProcessingType.Size = new System.Drawing.Size(559, 21);
            this.comboBoxProcessingType.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "The following rule determine how to transform inputting file content to incoming " +
                "(XML) message:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonFailureTemplate
            // 
            this.buttonFailureTemplate.Location = new System.Drawing.Point(208, 200);
            this.buttonFailureTemplate.Name = "buttonFailureTemplate";
            this.buttonFailureTemplate.Size = new System.Drawing.Size(178, 24);
            this.buttonFailureTemplate.TabIndex = 38;
            this.buttonFailureTemplate.Text = "Failure Acknowledgement";
            this.buttonFailureTemplate.UseVisualStyleBackColor = true;
            this.buttonFailureTemplate.Click += new System.EventHandler(this.buttonFailureTemplate_Click);
            // 
            // buttonSuccessTemplate
            // 
            this.buttonSuccessTemplate.Location = new System.Drawing.Point(24, 200);
            this.buttonSuccessTemplate.Name = "buttonSuccessTemplate";
            this.buttonSuccessTemplate.Size = new System.Drawing.Size(178, 24);
            this.buttonSuccessTemplate.TabIndex = 37;
            this.buttonSuccessTemplate.Text = "Success Acknowledgement";
            this.buttonSuccessTemplate.UseVisualStyleBackColor = true;
            this.buttonSuccessTemplate.Click += new System.EventHandler(this.buttonSuccessTemplate_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(20, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(577, 31);
            this.label9.TabIndex = 39;
            this.label9.Text = "After dispatching incoming message to publish channel, the following templates ge" +
                "nerate outgoing acknowledgement:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxDispose
            // 
            this.comboBoxDispose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDispose.FormattingEnabled = true;
            this.comboBoxDispose.Location = new System.Drawing.Point(23, 94);
            this.comboBoxDispose.Name = "comboBoxDispose";
            this.comboBoxDispose.Size = new System.Drawing.Size(118, 21);
            this.comboBoxDispose.TabIndex = 34;
            // 
            // buttonDispatch
            // 
            this.buttonDispatch.Location = new System.Drawing.Point(227, 450);
            this.buttonDispatch.Name = "buttonDispatch";
            this.buttonDispatch.Size = new System.Drawing.Size(187, 24);
            this.buttonDispatch.TabIndex = 27;
            this.buttonDispatch.Text = "Message Dispatching Setting";
            this.buttonDispatch.UseVisualStyleBackColor = true;
            this.buttonDispatch.Visible = false;
            this.buttonDispatch.Click += new System.EventHandler(this.buttonDispatch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxTimeInterval);
            this.groupBox1.Controls.Add(this.textBoxFileExtension);
            this.groupBox1.Controls.Add(label8);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(this.comboBoxCodePage);
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.textBoxINFolder);
            this.groupBox1.Location = new System.Drawing.Point(17, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 101);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Setting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "ms";
            // 
            // textBoxTimeInterval
            // 
            this.textBoxTimeInterval.Location = new System.Drawing.Point(463, 61);
            this.textBoxTimeInterval.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.textBoxTimeInterval.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxTimeInterval.Name = "textBoxTimeInterval";
            this.textBoxTimeInterval.Size = new System.Drawing.Size(103, 20);
            this.textBoxTimeInterval.TabIndex = 38;
            this.textBoxTimeInterval.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // textBoxFileExtension
            // 
            this.textBoxFileExtension.Location = new System.Drawing.Point(463, 27);
            this.textBoxFileExtension.Name = "textBoxFileExtension";
            this.textBoxFileExtension.Size = new System.Drawing.Size(103, 20);
            this.textBoxFileExtension.TabIndex = 37;
            // 
            // comboBoxCodePage
            // 
            this.comboBoxCodePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodePage.FormattingEnabled = true;
            this.comboBoxCodePage.Location = new System.Drawing.Point(101, 61);
            this.comboBoxCodePage.MaxDropDownItems = 12;
            this.comboBoxCodePage.Name = "comboBoxCodePage";
            this.comboBoxCodePage.Size = new System.Drawing.Size(258, 21);
            this.comboBoxCodePage.TabIndex = 27;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 430);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(label6);
            this.Controls.Add(this.buttonDispatch);
            this.MinimumSize = new System.Drawing.Size(441, 220);
            this.Name = "FormConfig";
            this.Text = "File Reader Configuration";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxINFolder;
        private System.Windows.Forms.TextBox textBoxDisposeFolder;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDispatch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCodePage;
        private System.Windows.Forms.ComboBox comboBoxDispose;
        private System.Windows.Forms.TextBox textBoxFileExtension;
        private System.Windows.Forms.Button buttonFailureTemplate;
        private System.Windows.Forms.Button buttonSuccessTemplate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxProcessingType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown textBoxTimeInterval;
        private System.Windows.Forms.Label label3;
    }
}