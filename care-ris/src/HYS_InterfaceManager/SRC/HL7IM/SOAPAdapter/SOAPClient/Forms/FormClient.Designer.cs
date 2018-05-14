namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Forms
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
            this.textBoxAction = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxURI = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSndMsg = new System.Windows.Forms.TextBox();
            this.tabControlSndMsg = new System.Windows.Forms.TabControl();
            this.tabPageSndMsgPlain = new System.Windows.Forms.TabPage();
            this.tabPageSndMsgTree = new System.Windows.Forms.TabPage();
            this.webBrowserSndMsg = new System.Windows.Forms.WebBrowser();
            this.tabControlSndSoap = new System.Windows.Forms.TabControl();
            this.tabPageSndSoapPlain = new System.Windows.Forms.TabPage();
            this.textBoxSndSoap = new System.Windows.Forms.TextBox();
            this.tabPageSndSoapTree = new System.Windows.Forms.TabPage();
            this.webBrowserSndSoap = new System.Windows.Forms.WebBrowser();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControlRcvSoap = new System.Windows.Forms.TabControl();
            this.tabPageRcvSoapPlain = new System.Windows.Forms.TabPage();
            this.textBoxRcvSoap = new System.Windows.Forms.TextBox();
            this.tabPageRcvSoapTree = new System.Windows.Forms.TabPage();
            this.webBrowserRcvSoap = new System.Windows.Forms.WebBrowser();
            this.tabControlRcvMsg = new System.Windows.Forms.TabControl();
            this.tabPageRcvMsgPlain = new System.Windows.Forms.TabPage();
            this.textBoxRcvMsg = new System.Windows.Forms.TextBox();
            this.tabPageRcvMsgTree = new System.Windows.Forms.TabPage();
            this.webBrowserRcvMsg = new System.Windows.Forms.WebBrowser();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxSampleMessage = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSndTranPerform = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.labelSndSoapPerform = new System.Windows.Forms.Label();
            this.labelRcvTranPerform = new System.Windows.Forms.Label();
            this.labelRunPerform = new System.Windows.Forms.Label();
            this.linkLabelSndTransform = new System.Windows.Forms.LinkLabel();
            this.linkLabelSndSoap = new System.Windows.Forms.LinkLabel();
            this.linkLabelRcvTransform = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownTimes = new System.Windows.Forms.NumericUpDown();
            this.tabControlSndMsg.SuspendLayout();
            this.tabPageSndMsgPlain.SuspendLayout();
            this.tabPageSndMsgTree.SuspendLayout();
            this.tabControlSndSoap.SuspendLayout();
            this.tabPageSndSoapPlain.SuspendLayout();
            this.tabPageSndSoapTree.SuspendLayout();
            this.tabControlRcvSoap.SuspendLayout();
            this.tabPageRcvSoapPlain.SuspendLayout();
            this.tabPageRcvSoapTree.SuspendLayout();
            this.tabControlRcvMsg.SuspendLayout();
            this.tabPageRcvMsgPlain.SuspendLayout();
            this.tabPageRcvMsgTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxAction
            // 
            this.textBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAction.Location = new System.Drawing.Point(432, 38);
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.Size = new System.Drawing.Size(265, 20);
            this.textBoxAction.TabIndex = 25;
            this.textBoxAction.Text = "http://www.carestreamhealth.com/MessageCom";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(322, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 19);
            this.label6.TabIndex = 24;
            this.label6.Text = "SOAP Action:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxURI
            // 
            this.textBoxURI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURI.Location = new System.Drawing.Point(432, 12);
            this.textBoxURI.Name = "textBoxURI";
            this.textBoxURI.Size = new System.Drawing.Size(265, 20);
            this.textBoxURI.TabIndex = 23;
            this.textBoxURI.Text = "http://cnshw6zmrs1x/RHISWS/PIXService.asmx";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(322, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "SOAP Service URI:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSndMsg
            // 
            this.textBoxSndMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSndMsg.Location = new System.Drawing.Point(3, 3);
            this.textBoxSndMsg.Multiline = true;
            this.textBoxSndMsg.Name = "textBoxSndMsg";
            this.textBoxSndMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSndMsg.Size = new System.Drawing.Size(351, 181);
            this.textBoxSndMsg.TabIndex = 18;
            // 
            // tabControlSndMsg
            // 
            this.tabControlSndMsg.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlSndMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSndMsg.Controls.Add(this.tabPageSndMsgPlain);
            this.tabControlSndMsg.Controls.Add(this.tabPageSndMsgTree);
            this.tabControlSndMsg.Location = new System.Drawing.Point(22, 103);
            this.tabControlSndMsg.Multiline = true;
            this.tabControlSndMsg.Name = "tabControlSndMsg";
            this.tabControlSndMsg.SelectedIndex = 0;
            this.tabControlSndMsg.Size = new System.Drawing.Size(384, 195);
            this.tabControlSndMsg.TabIndex = 26;
            // 
            // tabPageSndMsgPlain
            // 
            this.tabPageSndMsgPlain.Controls.Add(this.textBoxSndMsg);
            this.tabPageSndMsgPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageSndMsgPlain.Name = "tabPageSndMsgPlain";
            this.tabPageSndMsgPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSndMsgPlain.Size = new System.Drawing.Size(357, 187);
            this.tabPageSndMsgPlain.TabIndex = 0;
            this.tabPageSndMsgPlain.Text = "Plain Text";
            this.tabPageSndMsgPlain.UseVisualStyleBackColor = true;
            // 
            // tabPageSndMsgTree
            // 
            this.tabPageSndMsgTree.Controls.Add(this.webBrowserSndMsg);
            this.tabPageSndMsgTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageSndMsgTree.Name = "tabPageSndMsgTree";
            this.tabPageSndMsgTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSndMsgTree.Size = new System.Drawing.Size(357, 187);
            this.tabPageSndMsgTree.TabIndex = 1;
            this.tabPageSndMsgTree.Text = "Tree View";
            this.tabPageSndMsgTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserSndMsg
            // 
            this.webBrowserSndMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserSndMsg.Location = new System.Drawing.Point(3, 3);
            this.webBrowserSndMsg.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserSndMsg.Name = "webBrowserSndMsg";
            this.webBrowserSndMsg.Size = new System.Drawing.Size(351, 181);
            this.webBrowserSndMsg.TabIndex = 0;
            // 
            // tabControlSndSoap
            // 
            this.tabControlSndSoap.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlSndSoap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSndSoap.Controls.Add(this.tabPageSndSoapPlain);
            this.tabControlSndSoap.Controls.Add(this.tabPageSndSoapTree);
            this.tabControlSndSoap.Location = new System.Drawing.Point(22, 326);
            this.tabControlSndSoap.Multiline = true;
            this.tabControlSndSoap.Name = "tabControlSndSoap";
            this.tabControlSndSoap.SelectedIndex = 0;
            this.tabControlSndSoap.Size = new System.Drawing.Size(384, 143);
            this.tabControlSndSoap.TabIndex = 27;
            // 
            // tabPageSndSoapPlain
            // 
            this.tabPageSndSoapPlain.Controls.Add(this.textBoxSndSoap);
            this.tabPageSndSoapPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageSndSoapPlain.Name = "tabPageSndSoapPlain";
            this.tabPageSndSoapPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSndSoapPlain.Size = new System.Drawing.Size(357, 135);
            this.tabPageSndSoapPlain.TabIndex = 0;
            this.tabPageSndSoapPlain.Text = "Plain Text";
            this.tabPageSndSoapPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxSndSoap
            // 
            this.textBoxSndSoap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSndSoap.Location = new System.Drawing.Point(3, 3);
            this.textBoxSndSoap.Multiline = true;
            this.textBoxSndSoap.Name = "textBoxSndSoap";
            this.textBoxSndSoap.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSndSoap.Size = new System.Drawing.Size(351, 129);
            this.textBoxSndSoap.TabIndex = 18;
            this.textBoxSndSoap.Text = resources.GetString("textBoxSndSoap.Text");
            // 
            // tabPageSndSoapTree
            // 
            this.tabPageSndSoapTree.Controls.Add(this.webBrowserSndSoap);
            this.tabPageSndSoapTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageSndSoapTree.Name = "tabPageSndSoapTree";
            this.tabPageSndSoapTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSndSoapTree.Size = new System.Drawing.Size(357, 135);
            this.tabPageSndSoapTree.TabIndex = 1;
            this.tabPageSndSoapTree.Text = "Tree View";
            this.tabPageSndSoapTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserSndSoap
            // 
            this.webBrowserSndSoap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserSndSoap.Location = new System.Drawing.Point(3, 3);
            this.webBrowserSndSoap.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserSndSoap.Name = "webBrowserSndSoap";
            this.webBrowserSndSoap.Size = new System.Drawing.Size(351, 129);
            this.webBrowserSndSoap.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(422, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 19);
            this.label2.TabIndex = 32;
            this.label2.Text = "The following XDS Gateway message will be responsed to the requester.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControlRcvSoap
            // 
            this.tabControlRcvSoap.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlRcvSoap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRcvSoap.Controls.Add(this.tabPageRcvSoapPlain);
            this.tabControlRcvSoap.Controls.Add(this.tabPageRcvSoapTree);
            this.tabControlRcvSoap.Location = new System.Drawing.Point(425, 326);
            this.tabControlRcvSoap.Multiline = true;
            this.tabControlRcvSoap.Name = "tabControlRcvSoap";
            this.tabControlRcvSoap.SelectedIndex = 0;
            this.tabControlRcvSoap.Size = new System.Drawing.Size(347, 143);
            this.tabControlRcvSoap.TabIndex = 31;
            // 
            // tabPageRcvSoapPlain
            // 
            this.tabPageRcvSoapPlain.Controls.Add(this.textBoxRcvSoap);
            this.tabPageRcvSoapPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageRcvSoapPlain.Name = "tabPageRcvSoapPlain";
            this.tabPageRcvSoapPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRcvSoapPlain.Size = new System.Drawing.Size(320, 135);
            this.tabPageRcvSoapPlain.TabIndex = 0;
            this.tabPageRcvSoapPlain.Text = "Plain Text";
            this.tabPageRcvSoapPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxRcvSoap
            // 
            this.textBoxRcvSoap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRcvSoap.Location = new System.Drawing.Point(3, 3);
            this.textBoxRcvSoap.Multiline = true;
            this.textBoxRcvSoap.Name = "textBoxRcvSoap";
            this.textBoxRcvSoap.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRcvSoap.Size = new System.Drawing.Size(314, 129);
            this.textBoxRcvSoap.TabIndex = 18;
            this.textBoxRcvSoap.Text = resources.GetString("textBoxRcvSoap.Text");
            // 
            // tabPageRcvSoapTree
            // 
            this.tabPageRcvSoapTree.Controls.Add(this.webBrowserRcvSoap);
            this.tabPageRcvSoapTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageRcvSoapTree.Name = "tabPageRcvSoapTree";
            this.tabPageRcvSoapTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRcvSoapTree.Size = new System.Drawing.Size(320, 135);
            this.tabPageRcvSoapTree.TabIndex = 1;
            this.tabPageRcvSoapTree.Text = "Tree View";
            this.tabPageRcvSoapTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserRcvSoap
            // 
            this.webBrowserRcvSoap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserRcvSoap.Location = new System.Drawing.Point(3, 3);
            this.webBrowserRcvSoap.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserRcvSoap.Name = "webBrowserRcvSoap";
            this.webBrowserRcvSoap.Size = new System.Drawing.Size(314, 129);
            this.webBrowserRcvSoap.TabIndex = 0;
            // 
            // tabControlRcvMsg
            // 
            this.tabControlRcvMsg.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlRcvMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRcvMsg.Controls.Add(this.tabPageRcvMsgPlain);
            this.tabControlRcvMsg.Controls.Add(this.tabPageRcvMsgTree);
            this.tabControlRcvMsg.Location = new System.Drawing.Point(425, 102);
            this.tabControlRcvMsg.Multiline = true;
            this.tabControlRcvMsg.Name = "tabControlRcvMsg";
            this.tabControlRcvMsg.SelectedIndex = 0;
            this.tabControlRcvMsg.Size = new System.Drawing.Size(347, 195);
            this.tabControlRcvMsg.TabIndex = 30;
            // 
            // tabPageRcvMsgPlain
            // 
            this.tabPageRcvMsgPlain.Controls.Add(this.textBoxRcvMsg);
            this.tabPageRcvMsgPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageRcvMsgPlain.Name = "tabPageRcvMsgPlain";
            this.tabPageRcvMsgPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRcvMsgPlain.Size = new System.Drawing.Size(320, 187);
            this.tabPageRcvMsgPlain.TabIndex = 0;
            this.tabPageRcvMsgPlain.Text = "Plain Text";
            this.tabPageRcvMsgPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxRcvMsg
            // 
            this.textBoxRcvMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRcvMsg.Location = new System.Drawing.Point(3, 3);
            this.textBoxRcvMsg.Multiline = true;
            this.textBoxRcvMsg.Name = "textBoxRcvMsg";
            this.textBoxRcvMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRcvMsg.Size = new System.Drawing.Size(314, 181);
            this.textBoxRcvMsg.TabIndex = 18;
            // 
            // tabPageRcvMsgTree
            // 
            this.tabPageRcvMsgTree.Controls.Add(this.webBrowserRcvMsg);
            this.tabPageRcvMsgTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageRcvMsgTree.Name = "tabPageRcvMsgTree";
            this.tabPageRcvMsgTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRcvMsgTree.Size = new System.Drawing.Size(320, 187);
            this.tabPageRcvMsgTree.TabIndex = 1;
            this.tabPageRcvMsgTree.Text = "Tree View";
            this.tabPageRcvMsgTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserRcvMsg
            // 
            this.webBrowserRcvMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserRcvMsg.Location = new System.Drawing.Point(3, 3);
            this.webBrowserRcvMsg.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserRcvMsg.Name = "webBrowserRcvMsg";
            this.webBrowserRcvMsg.Size = new System.Drawing.Size(314, 181);
            this.webBrowserRcvMsg.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(23, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(264, 19);
            this.label7.TabIndex = 33;
            this.label7.Text = "Please Select a Sample XDS Gateway Message:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxSampleMessage
            // 
            this.comboBoxSampleMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleMessage.FormattingEnabled = true;
            this.comboBoxSampleMessage.Location = new System.Drawing.Point(29, 38);
            this.comboBoxSampleMessage.MaxDropDownItems = 20;
            this.comboBoxSampleMessage.Name = "comboBoxSampleMessage";
            this.comboBoxSampleMessage.Size = new System.Drawing.Size(245, 21);
            this.comboBoxSampleMessage.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(24, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 2);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // labelSndTranPerform
            // 
            this.labelSndTranPerform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSndTranPerform.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSndTranPerform.Location = new System.Drawing.Point(74, 485);
            this.labelSndTranPerform.Name = "labelSndTranPerform";
            this.labelSndTranPerform.Size = new System.Drawing.Size(132, 19);
            this.labelSndTranPerform.TabIndex = 39;
            this.labelSndTranPerform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Location = new System.Drawing.Point(713, 38);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(61, 21);
            this.buttonRun.TabIndex = 40;
            this.buttonRun.Text = "Invokes";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // labelSndSoapPerform
            // 
            this.labelSndSoapPerform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSndSoapPerform.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSndSoapPerform.Location = new System.Drawing.Point(255, 485);
            this.labelSndSoapPerform.Name = "labelSndSoapPerform";
            this.labelSndSoapPerform.Size = new System.Drawing.Size(125, 19);
            this.labelSndSoapPerform.TabIndex = 41;
            this.labelSndSoapPerform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRcvTranPerform
            // 
            this.labelRcvTranPerform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRcvTranPerform.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelRcvTranPerform.Location = new System.Drawing.Point(432, 485);
            this.labelRcvTranPerform.Name = "labelRcvTranPerform";
            this.labelRcvTranPerform.Size = new System.Drawing.Size(124, 19);
            this.labelRcvTranPerform.TabIndex = 42;
            this.labelRcvTranPerform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRunPerform
            // 
            this.labelRunPerform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRunPerform.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelRunPerform.Location = new System.Drawing.Point(608, 485);
            this.labelRunPerform.Name = "labelRunPerform";
            this.labelRunPerform.Size = new System.Drawing.Size(141, 19);
            this.labelRunPerform.TabIndex = 43;
            this.labelRunPerform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabelSndTransform
            // 
            this.linkLabelSndTransform.Location = new System.Drawing.Point(26, 80);
            this.linkLabelSndTransform.Name = "linkLabelSndTransform";
            this.linkLabelSndTransform.Size = new System.Drawing.Size(380, 19);
            this.linkLabelSndTransform.TabIndex = 44;
            this.linkLabelSndTransform.TabStop = true;
            this.linkLabelSndTransform.Text = "Step 1: Transform the following XDS Gateway message to SOAP message.";
            this.linkLabelSndTransform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelSndTransform.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSndTransform_LinkClicked);
            // 
            // linkLabelSndSoap
            // 
            this.linkLabelSndSoap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelSndSoap.Location = new System.Drawing.Point(26, 304);
            this.linkLabelSndSoap.Name = "linkLabelSndSoap";
            this.linkLabelSndSoap.Size = new System.Drawing.Size(380, 19);
            this.linkLabelSndSoap.TabIndex = 45;
            this.linkLabelSndSoap.TabStop = true;
            this.linkLabelSndSoap.Text = "Step 2: Send the following SOAP request to remote server.";
            this.linkLabelSndSoap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelSndSoap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSndSoap_LinkClicked);
            // 
            // linkLabelRcvTransform
            // 
            this.linkLabelRcvTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelRcvTransform.Location = new System.Drawing.Point(422, 304);
            this.linkLabelRcvTransform.Name = "linkLabelRcvTransform";
            this.linkLabelRcvTransform.Size = new System.Drawing.Size(362, 19);
            this.linkLabelRcvTransform.TabIndex = 46;
            this.linkLabelRcvTransform.TabStop = true;
            this.linkLabelRcvTransform.Text = "Step 3: Transform the following SOAP response to XDS Gateway message.";
            this.linkLabelRcvTransform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelRcvTransform.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRcvTransform_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(26, 485);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 47;
            this.label1.Text = "Step 1:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(212, 485);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 48;
            this.label3.Text = "Step 2:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(388, 485);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 19);
            this.label4.TabIndex = 49;
            this.label4.Text = "Step 3:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(564, 485);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 19);
            this.label8.TabIndex = 50;
            this.label8.Text = "Invoke:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTimes
            // 
            this.numericUpDownTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTimes.Location = new System.Drawing.Point(713, 12);
            this.numericUpDownTimes.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTimes.Name = "numericUpDownTimes";
            this.numericUpDownTimes.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownTimes.TabIndex = 51;
            this.numericUpDownTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 513);
            this.Controls.Add(this.numericUpDownTimes);
            this.Controls.Add(this.labelRunPerform);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelRcvTranPerform);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSndSoapPerform);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSndTranPerform);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabelRcvTransform);
            this.Controls.Add(this.linkLabelSndSoap);
            this.Controls.Add(this.linkLabelSndTransform);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxAction);
            this.Controls.Add(this.textBoxURI);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxSampleMessage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControlRcvSoap);
            this.Controls.Add(this.tabControlRcvMsg);
            this.Controls.Add(this.tabControlSndSoap);
            this.Controls.Add(this.tabControlSndMsg);
            this.Controls.Add(this.label6);
            this.MinimumSize = new System.Drawing.Size(805, 547);
            this.Name = "FormClient";
            this.Text = "SOAP Client";
            this.tabControlSndMsg.ResumeLayout(false);
            this.tabPageSndMsgPlain.ResumeLayout(false);
            this.tabPageSndMsgPlain.PerformLayout();
            this.tabPageSndMsgTree.ResumeLayout(false);
            this.tabControlSndSoap.ResumeLayout(false);
            this.tabPageSndSoapPlain.ResumeLayout(false);
            this.tabPageSndSoapPlain.PerformLayout();
            this.tabPageSndSoapTree.ResumeLayout(false);
            this.tabControlRcvSoap.ResumeLayout(false);
            this.tabPageRcvSoapPlain.ResumeLayout(false);
            this.tabPageRcvSoapPlain.PerformLayout();
            this.tabPageRcvSoapTree.ResumeLayout(false);
            this.tabControlRcvMsg.ResumeLayout(false);
            this.tabPageRcvMsgPlain.ResumeLayout(false);
            this.tabPageRcvMsgPlain.PerformLayout();
            this.tabPageRcvMsgTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxURI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSndMsg;
        private System.Windows.Forms.TabControl tabControlSndMsg;
        private System.Windows.Forms.TabPage tabPageSndMsgPlain;
        private System.Windows.Forms.TabPage tabPageSndMsgTree;
        private System.Windows.Forms.WebBrowser webBrowserSndMsg;
        private System.Windows.Forms.TabControl tabControlSndSoap;
        private System.Windows.Forms.TabPage tabPageSndSoapPlain;
        private System.Windows.Forms.TextBox textBoxSndSoap;
        private System.Windows.Forms.TabPage tabPageSndSoapTree;
        private System.Windows.Forms.WebBrowser webBrowserSndSoap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControlRcvSoap;
        private System.Windows.Forms.TabPage tabPageRcvSoapPlain;
        private System.Windows.Forms.TextBox textBoxRcvSoap;
        private System.Windows.Forms.TabPage tabPageRcvSoapTree;
        private System.Windows.Forms.WebBrowser webBrowserRcvSoap;
        private System.Windows.Forms.TabControl tabControlRcvMsg;
        private System.Windows.Forms.TabPage tabPageRcvMsgPlain;
        private System.Windows.Forms.TextBox textBoxRcvMsg;
        private System.Windows.Forms.TabPage tabPageRcvMsgTree;
        private System.Windows.Forms.WebBrowser webBrowserRcvMsg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxSampleMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSndTranPerform;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label labelSndSoapPerform;
        private System.Windows.Forms.Label labelRcvTranPerform;
        private System.Windows.Forms.Label labelRunPerform;
        private System.Windows.Forms.LinkLabel linkLabelSndTransform;
        private System.Windows.Forms.LinkLabel linkLabelSndSoap;
        private System.Windows.Forms.LinkLabel linkLabelRcvTransform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownTimes;
    }
}