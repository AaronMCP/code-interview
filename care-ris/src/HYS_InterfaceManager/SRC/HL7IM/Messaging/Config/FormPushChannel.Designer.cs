namespace HYS.IM.Messaging.Config
{
    partial class FormPushChannel
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSubscriber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPublisher = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEntityStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxProtocolType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBoxMessageType = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelMessageType = new System.Windows.Forms.Panel();
            this.panelMessageContent = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxRegExp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxXPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxRoutingType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAdvance = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelMessageType.SuspendLayout();
            this.panelMessageContent.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(495, 399);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 25);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(403, 399);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 25);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Message Type List:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSubscriber
            // 
            this.textBoxSubscriber.Location = new System.Drawing.Point(125, 54);
            this.textBoxSubscriber.Name = "textBoxSubscriber";
            this.textBoxSubscriber.ReadOnly = true;
            this.textBoxSubscriber.Size = new System.Drawing.Size(412, 20);
            this.textBoxSubscriber.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxPublisher);
            this.groupBox1.Controls.Add(this.textBoxSubscriber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelEntityStatus);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 95);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Routing Source and Destination";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(540, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxPublisher
            // 
            this.comboBoxPublisher.FormattingEnabled = true;
            this.comboBoxPublisher.Location = new System.Drawing.Point(125, 27);
            this.comboBoxPublisher.Name = "comboBoxPublisher";
            this.comboBoxPublisher.Size = new System.Drawing.Size(412, 21);
            this.comboBoxPublisher.TabIndex = 0;
            this.comboBoxPublisher.SelectedIndexChanged += new System.EventHandler(this.comboBoxPublisher_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Subscriber:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEntityStatus
            // 
            this.labelEntityStatus.Location = new System.Drawing.Point(18, 28);
            this.labelEntityStatus.Name = "labelEntityStatus";
            this.labelEntityStatus.Size = new System.Drawing.Size(101, 20);
            this.labelEntityStatus.TabIndex = 24;
            this.labelEntityStatus.Text = "Publisher:";
            this.labelEntityStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(345, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 19);
            this.label5.TabIndex = 37;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxProtocolType
            // 
            this.comboBoxProtocolType.FormattingEnabled = true;
            this.comboBoxProtocolType.Items.AddRange(new object[] {
            "LPC  (inner process call back)",
            "MSMQ  (message queue)",
            "RPC on Local Machine  (named pipe)",
            "RPC on LAN/Intranet  (tcp/ip)",
            "RPC on WAN/Internet  (soap)"});
            this.comboBoxProtocolType.Location = new System.Drawing.Point(125, 27);
            this.comboBoxProtocolType.Name = "comboBoxProtocolType";
            this.comboBoxProtocolType.Size = new System.Drawing.Size(214, 21);
            this.comboBoxProtocolType.TabIndex = 0;
            this.comboBoxProtocolType.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocolType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 21);
            this.label3.TabIndex = 33;
            this.label3.Text = "Protocol Type:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkedListBoxMessageType
            // 
            this.checkedListBoxMessageType.CheckOnClick = true;
            this.checkedListBoxMessageType.FormattingEnabled = true;
            this.checkedListBoxMessageType.Location = new System.Drawing.Point(108, 8);
            this.checkedListBoxMessageType.Name = "checkedListBoxMessageType";
            this.checkedListBoxMessageType.Size = new System.Drawing.Size(412, 124);
            this.checkedListBoxMessageType.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelMessageType);
            this.groupBox2.Controls.Add(this.panelMessageContent);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.comboBoxRoutingType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 204);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Routing Contract";
            // 
            // panelMessageType
            // 
            this.panelMessageType.Controls.Add(this.checkedListBoxMessageType);
            this.panelMessageType.Controls.Add(this.label2);
            this.panelMessageType.Location = new System.Drawing.Point(17, 54);
            this.panelMessageType.Name = "panelMessageType";
            this.panelMessageType.Size = new System.Drawing.Size(551, 137);
            this.panelMessageType.TabIndex = 40;
            // 
            // panelMessageContent
            // 
            this.panelMessageContent.Controls.Add(this.label14);
            this.panelMessageContent.Controls.Add(this.label12);
            this.panelMessageContent.Controls.Add(this.textBoxRegExp);
            this.panelMessageContent.Controls.Add(this.label11);
            this.panelMessageContent.Controls.Add(this.label9);
            this.panelMessageContent.Controls.Add(this.textBoxPrefix);
            this.panelMessageContent.Controls.Add(this.label10);
            this.panelMessageContent.Controls.Add(this.label8);
            this.panelMessageContent.Controls.Add(this.textBoxXPath);
            this.panelMessageContent.Controls.Add(this.label7);
            this.panelMessageContent.Location = new System.Drawing.Point(17, 54);
            this.panelMessageContent.Name = "panelMessageContent";
            this.panelMessageContent.Size = new System.Drawing.Size(540, 148);
            this.panelMessageContent.TabIndex = 41;
            this.panelMessageContent.Visible = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(523, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 20);
            this.label14.TabIndex = 47;
            this.label14.Text = "*";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(108, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(412, 20);
            this.label12.TabIndex = 39;
            this.label12.Text = "Example: value1|value2|value3|...";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxRegExp
            // 
            this.textBoxRegExp.Location = new System.Drawing.Point(108, 100);
            this.textBoxRegExp.Name = "textBoxRegExp";
            this.textBoxRegExp.Size = new System.Drawing.Size(412, 20);
            this.textBoxRegExp.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 45;
            this.label11.Text = "Regular Expression:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(108, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(412, 20);
            this.label9.TabIndex = 44;
            this.label9.Text = "Example: a|www.a.org|b|www.b.org";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(108, 54);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(412, 20);
            this.textBoxPrefix.TabIndex = 43;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(1, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 42;
            this.label10.Text = "Prefix Definition:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(108, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(412, 20);
            this.label8.TabIndex = 41;
            this.label8.Text = "Example: /Message/Body/a:Root/b:Element";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXPath
            // 
            this.textBoxXPath.Location = new System.Drawing.Point(108, 8);
            this.textBoxXPath.Name = "textBoxXPath";
            this.textBoxXPath.Size = new System.Drawing.Size(412, 20);
            this.textBoxXPath.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "XPath:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(345, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 20);
            this.label13.TabIndex = 42;
            this.label13.Text = "*";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxRoutingType
            // 
            this.comboBoxRoutingType.FormattingEnabled = true;
            this.comboBoxRoutingType.Location = new System.Drawing.Point(125, 29);
            this.comboBoxRoutingType.Name = "comboBoxRoutingType";
            this.comboBoxRoutingType.Size = new System.Drawing.Size(214, 21);
            this.comboBoxRoutingType.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(18, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Routing Type:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAdvance);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comboBoxProtocolType);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 321);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 65);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Routing Protocol";
            // 
            // buttonAdvance
            // 
            this.buttonAdvance.Location = new System.Drawing.Point(391, 22);
            this.buttonAdvance.Name = "buttonAdvance";
            this.buttonAdvance.Size = new System.Drawing.Size(146, 28);
            this.buttonAdvance.TabIndex = 38;
            this.buttonAdvance.Text = "Advance Setting";
            this.buttonAdvance.UseVisualStyleBackColor = true;
            this.buttonAdvance.Visible = false;
            this.buttonAdvance.Click += new System.EventHandler(this.buttonAdvance_Click);
            // 
            // FormPushChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(595, 436);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPushChannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormPushChannel";
            this.Load += new System.EventHandler(this.FormPushChannel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panelMessageType.ResumeLayout(false);
            this.panelMessageContent.ResumeLayout(false);
            this.panelMessageContent.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSubscriber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelEntityStatus;
        private System.Windows.Forms.CheckedListBox checkedListBoxMessageType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxProtocolType;
        private System.Windows.Forms.ComboBox comboBoxPublisher;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxRoutingType;
        private System.Windows.Forms.Panel panelMessageType;
        private System.Windows.Forms.Panel panelMessageContent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxXPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxRegExp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonAdvance;
    }
}