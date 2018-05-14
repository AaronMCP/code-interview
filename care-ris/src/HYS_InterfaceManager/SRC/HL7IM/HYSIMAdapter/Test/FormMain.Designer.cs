namespace HYS.MessageDevices.CSBAdapter.Test
{
    partial class FormMain
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
            this.buttonReadPatientMsg = new System.Windows.Forms.Button();
            this.dataGridViewDS = new System.Windows.Forms.DataGridView();
            this.buttonReadOrderMessage = new System.Windows.Forms.Button();
            this.tabControlHL7Msg = new System.Windows.Forms.TabControl();
            this.tabPageHL7MsgPlain = new System.Windows.Forms.TabPage();
            this.textBoxHL7Msg = new System.Windows.Forms.TextBox();
            this.tabPageHL7MsgTree = new System.Windows.Forms.TabPage();
            this.webBrowserHL7Msg = new System.Windows.Forms.WebBrowser();
            this.comboBoxSampleHL7Message = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxXSLT = new System.Windows.Forms.TextBox();
            this.buttonTransform = new System.Windows.Forms.Button();
            this.tabControlCSBMsg = new System.Windows.Forms.TabControl();
            this.tabPageCSBMsgPlain = new System.Windows.Forms.TabPage();
            this.textBoxCSBMsg = new System.Windows.Forms.TextBox();
            this.tabPageCSBMsgTree = new System.Windows.Forms.TabPage();
            this.webBrowserCSBMsg = new System.Windows.Forms.WebBrowser();
            this.buttonGenDS = new System.Windows.Forms.Button();
            this.labelDSName = new System.Windows.Forms.Label();
            this.buttonReadReportMessage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDS)).BeginInit();
            this.tabControlHL7Msg.SuspendLayout();
            this.tabPageHL7MsgPlain.SuspendLayout();
            this.tabPageHL7MsgTree.SuspendLayout();
            this.tabControlCSBMsg.SuspendLayout();
            this.tabPageCSBMsgPlain.SuspendLayout();
            this.tabPageCSBMsgTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReadPatientMsg
            // 
            this.buttonReadPatientMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReadPatientMsg.Location = new System.Drawing.Point(799, 377);
            this.buttonReadPatientMsg.Name = "buttonReadPatientMsg";
            this.buttonReadPatientMsg.Size = new System.Drawing.Size(179, 28);
            this.buttonReadPatientMsg.TabIndex = 0;
            this.buttonReadPatientMsg.Text = "Read Sample Patient Message";
            this.buttonReadPatientMsg.UseVisualStyleBackColor = true;
            this.buttonReadPatientMsg.Click += new System.EventHandler(this.buttonReadPatientMsg_Click);
            // 
            // dataGridViewDS
            // 
            this.dataGridViewDS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDS.Location = new System.Drawing.Point(22, 377);
            this.dataGridViewDS.Name = "dataGridViewDS";
            this.dataGridViewDS.Size = new System.Drawing.Size(762, 145);
            this.dataGridViewDS.TabIndex = 1;
            // 
            // buttonReadOrderMessage
            // 
            this.buttonReadOrderMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReadOrderMessage.Location = new System.Drawing.Point(799, 408);
            this.buttonReadOrderMessage.Name = "buttonReadOrderMessage";
            this.buttonReadOrderMessage.Size = new System.Drawing.Size(179, 28);
            this.buttonReadOrderMessage.TabIndex = 2;
            this.buttonReadOrderMessage.Text = "Read Sample Order Message";
            this.buttonReadOrderMessage.UseVisualStyleBackColor = true;
            this.buttonReadOrderMessage.Click += new System.EventHandler(this.buttonReadOrderMessage_Click);
            // 
            // tabControlHL7Msg
            // 
            this.tabControlHL7Msg.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlHL7Msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlHL7Msg.Controls.Add(this.tabPageHL7MsgPlain);
            this.tabControlHL7Msg.Controls.Add(this.tabPageHL7MsgTree);
            this.tabControlHL7Msg.Location = new System.Drawing.Point(22, 71);
            this.tabControlHL7Msg.Multiline = true;
            this.tabControlHL7Msg.Name = "tabControlHL7Msg";
            this.tabControlHL7Msg.SelectedIndex = 0;
            this.tabControlHL7Msg.Size = new System.Drawing.Size(457, 293);
            this.tabControlHL7Msg.TabIndex = 27;
            // 
            // tabPageHL7MsgPlain
            // 
            this.tabPageHL7MsgPlain.Controls.Add(this.textBoxHL7Msg);
            this.tabPageHL7MsgPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageHL7MsgPlain.Name = "tabPageHL7MsgPlain";
            this.tabPageHL7MsgPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHL7MsgPlain.Size = new System.Drawing.Size(430, 285);
            this.tabPageHL7MsgPlain.TabIndex = 0;
            this.tabPageHL7MsgPlain.Text = "Plain Text";
            this.tabPageHL7MsgPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxHL7Msg
            // 
            this.textBoxHL7Msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxHL7Msg.Location = new System.Drawing.Point(3, 3);
            this.textBoxHL7Msg.Multiline = true;
            this.textBoxHL7Msg.Name = "textBoxHL7Msg";
            this.textBoxHL7Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxHL7Msg.Size = new System.Drawing.Size(424, 279);
            this.textBoxHL7Msg.TabIndex = 18;
            // 
            // tabPageHL7MsgTree
            // 
            this.tabPageHL7MsgTree.Controls.Add(this.webBrowserHL7Msg);
            this.tabPageHL7MsgTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageHL7MsgTree.Name = "tabPageHL7MsgTree";
            this.tabPageHL7MsgTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHL7MsgTree.Size = new System.Drawing.Size(430, 285);
            this.tabPageHL7MsgTree.TabIndex = 1;
            this.tabPageHL7MsgTree.Text = "Tree View";
            this.tabPageHL7MsgTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserHL7Msg
            // 
            this.webBrowserHL7Msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserHL7Msg.Location = new System.Drawing.Point(3, 3);
            this.webBrowserHL7Msg.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHL7Msg.Name = "webBrowserHL7Msg";
            this.webBrowserHL7Msg.Size = new System.Drawing.Size(424, 279);
            this.webBrowserHL7Msg.TabIndex = 0;
            // 
            // comboBoxSampleHL7Message
            // 
            this.comboBoxSampleHL7Message.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleHL7Message.FormattingEnabled = true;
            this.comboBoxSampleHL7Message.Location = new System.Drawing.Point(22, 35);
            this.comboBoxSampleHL7Message.MaxDropDownItems = 20;
            this.comboBoxSampleHL7Message.Name = "comboBoxSampleHL7Message";
            this.comboBoxSampleHL7Message.Size = new System.Drawing.Size(431, 21);
            this.comboBoxSampleHL7Message.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(385, 19);
            this.label7.TabIndex = 35;
            this.label7.Text = "Please Select a Sample XDSGW Message containing HKHA HL7 XML:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(494, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 19);
            this.label1.TabIndex = 37;
            this.label1.Text = "XSLT File to Transform XDSGW Message from HKHA HL7 XML to CSB DataSet XML:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXSLT
            // 
            this.textBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLT.Location = new System.Drawing.Point(497, 35);
            this.textBoxXSLT.Name = "textBoxXSLT";
            this.textBoxXSLT.Size = new System.Drawing.Size(376, 20);
            this.textBoxXSLT.TabIndex = 38;
            this.textBoxXSLT.Text = "XSLTFiles\\HK_HA\\Receiver\\main.xsl";
            // 
            // buttonTransform
            // 
            this.buttonTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTransform.Location = new System.Drawing.Point(891, 27);
            this.buttonTransform.Name = "buttonTransform";
            this.buttonTransform.Size = new System.Drawing.Size(87, 28);
            this.buttonTransform.TabIndex = 39;
            this.buttonTransform.Text = "Transform";
            this.buttonTransform.UseVisualStyleBackColor = true;
            this.buttonTransform.Click += new System.EventHandler(this.buttonTransform_Click);
            // 
            // tabControlCSBMsg
            // 
            this.tabControlCSBMsg.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlCSBMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCSBMsg.Controls.Add(this.tabPageCSBMsgPlain);
            this.tabControlCSBMsg.Controls.Add(this.tabPageCSBMsgTree);
            this.tabControlCSBMsg.Location = new System.Drawing.Point(497, 71);
            this.tabControlCSBMsg.Multiline = true;
            this.tabControlCSBMsg.Name = "tabControlCSBMsg";
            this.tabControlCSBMsg.SelectedIndex = 0;
            this.tabControlCSBMsg.Size = new System.Drawing.Size(481, 293);
            this.tabControlCSBMsg.TabIndex = 40;
            // 
            // tabPageCSBMsgPlain
            // 
            this.tabPageCSBMsgPlain.Controls.Add(this.textBoxCSBMsg);
            this.tabPageCSBMsgPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageCSBMsgPlain.Name = "tabPageCSBMsgPlain";
            this.tabPageCSBMsgPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSBMsgPlain.Size = new System.Drawing.Size(454, 285);
            this.tabPageCSBMsgPlain.TabIndex = 0;
            this.tabPageCSBMsgPlain.Text = "Plain Text";
            this.tabPageCSBMsgPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxCSBMsg
            // 
            this.textBoxCSBMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCSBMsg.Location = new System.Drawing.Point(3, 3);
            this.textBoxCSBMsg.Multiline = true;
            this.textBoxCSBMsg.Name = "textBoxCSBMsg";
            this.textBoxCSBMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCSBMsg.Size = new System.Drawing.Size(448, 279);
            this.textBoxCSBMsg.TabIndex = 18;
            // 
            // tabPageCSBMsgTree
            // 
            this.tabPageCSBMsgTree.Controls.Add(this.webBrowserCSBMsg);
            this.tabPageCSBMsgTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageCSBMsgTree.Name = "tabPageCSBMsgTree";
            this.tabPageCSBMsgTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSBMsgTree.Size = new System.Drawing.Size(454, 285);
            this.tabPageCSBMsgTree.TabIndex = 1;
            this.tabPageCSBMsgTree.Text = "Tree View";
            this.tabPageCSBMsgTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserCSBMsg
            // 
            this.webBrowserCSBMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserCSBMsg.Location = new System.Drawing.Point(3, 3);
            this.webBrowserCSBMsg.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserCSBMsg.Name = "webBrowserCSBMsg";
            this.webBrowserCSBMsg.Size = new System.Drawing.Size(448, 279);
            this.webBrowserCSBMsg.TabIndex = 0;
            // 
            // buttonGenDS
            // 
            this.buttonGenDS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenDS.Location = new System.Drawing.Point(799, 494);
            this.buttonGenDS.Name = "buttonGenDS";
            this.buttonGenDS.Size = new System.Drawing.Size(179, 28);
            this.buttonGenDS.TabIndex = 41;
            this.buttonGenDS.Text = "Generate DataSet";
            this.buttonGenDS.UseVisualStyleBackColor = true;
            this.buttonGenDS.Click += new System.EventHandler(this.buttonGenDS_Click);
            // 
            // labelDSName
            // 
            this.labelDSName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDSName.Location = new System.Drawing.Point(802, 473);
            this.labelDSName.Name = "labelDSName";
            this.labelDSName.Size = new System.Drawing.Size(176, 21);
            this.labelDSName.TabIndex = 42;
            this.labelDSName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReadReportMessage
            // 
            this.buttonReadReportMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReadReportMessage.Location = new System.Drawing.Point(799, 439);
            this.buttonReadReportMessage.Name = "buttonReadReportMessage";
            this.buttonReadReportMessage.Size = new System.Drawing.Size(179, 28);
            this.buttonReadReportMessage.TabIndex = 43;
            this.buttonReadReportMessage.Text = "Read Sample Report Message";
            this.buttonReadReportMessage.UseVisualStyleBackColor = true;
            this.buttonReadReportMessage.Click += new System.EventHandler(this.buttonReadReportMessage_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 544);
            this.Controls.Add(this.buttonReadReportMessage);
            this.Controls.Add(this.labelDSName);
            this.Controls.Add(this.buttonGenDS);
            this.Controls.Add(this.tabControlCSBMsg);
            this.Controls.Add(this.buttonTransform);
            this.Controls.Add(this.textBoxXSLT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSampleHL7Message);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tabControlHL7Msg);
            this.Controls.Add(this.buttonReadOrderMessage);
            this.Controls.Add(this.dataGridViewDS);
            this.Controls.Add(this.buttonReadPatientMsg);
            this.Name = "FormMain";
            this.Text = "CS Broker Adapter Testing";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDS)).EndInit();
            this.tabControlHL7Msg.ResumeLayout(false);
            this.tabPageHL7MsgPlain.ResumeLayout(false);
            this.tabPageHL7MsgPlain.PerformLayout();
            this.tabPageHL7MsgTree.ResumeLayout(false);
            this.tabControlCSBMsg.ResumeLayout(false);
            this.tabPageCSBMsgPlain.ResumeLayout(false);
            this.tabPageCSBMsgPlain.PerformLayout();
            this.tabPageCSBMsgTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReadPatientMsg;
        private System.Windows.Forms.DataGridView dataGridViewDS;
        private System.Windows.Forms.Button buttonReadOrderMessage;
        private System.Windows.Forms.TabControl tabControlHL7Msg;
        private System.Windows.Forms.TabPage tabPageHL7MsgPlain;
        private System.Windows.Forms.TextBox textBoxHL7Msg;
        private System.Windows.Forms.TabPage tabPageHL7MsgTree;
        private System.Windows.Forms.WebBrowser webBrowserHL7Msg;
        private System.Windows.Forms.ComboBox comboBoxSampleHL7Message;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxXSLT;
        private System.Windows.Forms.Button buttonTransform;
        private System.Windows.Forms.TabControl tabControlCSBMsg;
        private System.Windows.Forms.TabPage tabPageCSBMsgPlain;
        private System.Windows.Forms.TextBox textBoxCSBMsg;
        private System.Windows.Forms.TabPage tabPageCSBMsgTree;
        private System.Windows.Forms.WebBrowser webBrowserCSBMsg;
        private System.Windows.Forms.Button buttonGenDS;
        private System.Windows.Forms.Label labelDSName;
        private System.Windows.Forms.Button buttonReadReportMessage;
    }
}

