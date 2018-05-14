namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    partial class ActiveMode
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
            this.components = new System.ComponentModel.Container();
            this.panelConnection = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelDB = new System.Windows.Forms.Panel();
            this.chkOracleDriver = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxProvider = new System.Windows.Forms.TextBox();
            this.llblDetail = new System.Windows.Forms.LinkLabel();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.lblDB = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.panelFile = new System.Windows.Forms.Panel();
            this.linkLabelCharSet = new System.Windows.Forms.LinkLabel();
            this.textBoxIndexFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxIndexFile = new System.Windows.Forms.CheckBox();
            this.textBoxFileExtension = new System.Windows.Forms.TextBox();
            this.labelFilePattern = new System.Windows.Forms.Label();
            this.textBoxDataFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFileConnString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.radioButtonDB = new System.Windows.Forms.RadioButton();
            this.btnTest = new System.Windows.Forms.Button();
            this.panelChannels = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblTimeInterval = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.btnChannelCopy = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstvChannel = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripConnStr = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSQLServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOracle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSybase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCSVX64 = new System.Windows.Forms.ToolStripMenuItem();
            this.oracleX64ConnectionStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelConnection.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelDB.SuspendLayout();
            this.panelFile.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelChannels.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.contextMenuStripConnStr.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelConnection
            // 
            this.panelConnection.Controls.Add(this.groupBox2);
            this.panelConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConnection.Location = new System.Drawing.Point(0, 0);
            this.panelConnection.Name = "panelConnection";
            this.panelConnection.Size = new System.Drawing.Size(750, 236);
            this.panelConnection.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panelDB);
            this.groupBox2.Controls.Add(this.panelFile);
            this.groupBox2.Controls.Add(this.buttonDefault);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.btnTest);
            this.groupBox2.Location = new System.Drawing.Point(15, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 224);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Third Party OLEDB Data Souce Connection";
            // 
            // panelDB
            // 
            this.panelDB.Controls.Add(this.chkOracleDriver);
            this.panelDB.Controls.Add(this.label1);
            this.panelDB.Controls.Add(this.textBoxProvider);
            this.panelDB.Controls.Add(this.llblDetail);
            this.panelDB.Controls.Add(this.lblServer);
            this.panelDB.Controls.Add(this.txtServer);
            this.panelDB.Controls.Add(this.txtDB);
            this.panelDB.Controls.Add(this.lblDB);
            this.panelDB.Controls.Add(this.txtUser);
            this.panelDB.Controls.Add(this.lblUser);
            this.panelDB.Controls.Add(this.txtPassword);
            this.panelDB.Controls.Add(this.lblPassword);
            this.panelDB.Location = new System.Drawing.Point(13, 44);
            this.panelDB.Name = "panelDB";
            this.panelDB.Size = new System.Drawing.Size(710, 147);
            this.panelDB.TabIndex = 114;
            // 
            // chkOracleDriver
            // 
            this.chkOracleDriver.AutoSize = true;
            this.chkOracleDriver.Location = new System.Drawing.Point(585, 106);
            this.chkOracleDriver.Name = "chkOracleDriver";
            this.chkOracleDriver.Size = new System.Drawing.Size(110, 17);
            this.chkOracleDriver.TabIndex = 114;
            this.chkOracleDriver.Text = "Oracle X64 Driver";
            this.chkOracleDriver.UseVisualStyleBackColor = true;
            this.chkOracleDriver.CheckedChanged += new System.EventHandler(this.chkOracleDriver_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "OLEDB Provider";
            // 
            // textBoxProvider
            // 
            this.textBoxProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProvider.Location = new System.Drawing.Point(127, 13);
            this.textBoxProvider.Name = "textBoxProvider";
            this.textBoxProvider.Size = new System.Drawing.Size(447, 20);
            this.textBoxProvider.TabIndex = 112;
            // 
            // llblDetail
            // 
            this.llblDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llblDetail.AutoSize = true;
            this.llblDetail.Location = new System.Drawing.Point(580, 127);
            this.llblDetail.Name = "llblDetail";
            this.llblDetail.Size = new System.Drawing.Size(102, 13);
            this.llblDetail.TabIndex = 4;
            this.llblDetail.TabStop = true;
            this.llblDetail.Text = "Show more details...";
            this.llblDetail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblDetail_LinkClicked);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(7, 39);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(67, 13);
            this.lblServer.TabIndex = 103;
            this.lblServer.Text = "Data Source";
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.Location = new System.Drawing.Point(127, 39);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(447, 20);
            this.txtServer.TabIndex = 0;
            // 
            // txtDB
            // 
            this.txtDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDB.Location = new System.Drawing.Point(127, 66);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(447, 20);
            this.txtDB.TabIndex = 1;
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(7, 66);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(53, 13);
            this.lblDB.TabIndex = 105;
            this.lblDB.Text = "Database";
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.Location = new System.Drawing.Point(127, 93);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(447, 20);
            this.txtUser.TabIndex = 2;
            this.txtUser.Text = "                                       ";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(7, 93);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(43, 13);
            this.lblUser.TabIndex = 107;
            this.lblUser.Text = "User ID";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(127, 120);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(447, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(7, 120);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 109;
            this.lblPassword.Text = "Password";
            // 
            // panelFile
            // 
            this.panelFile.Controls.Add(this.linkLabelCharSet);
            this.panelFile.Controls.Add(this.textBoxIndexFolder);
            this.panelFile.Controls.Add(this.label5);
            this.panelFile.Controls.Add(this.checkBoxIndexFile);
            this.panelFile.Controls.Add(this.textBoxFileExtension);
            this.panelFile.Controls.Add(this.labelFilePattern);
            this.panelFile.Controls.Add(this.textBoxDataFolder);
            this.panelFile.Controls.Add(this.label3);
            this.panelFile.Controls.Add(this.textBoxFileConnString);
            this.panelFile.Controls.Add(this.label2);
            this.panelFile.Location = new System.Drawing.Point(6, 57);
            this.panelFile.Name = "panelFile";
            this.panelFile.Size = new System.Drawing.Size(711, 147);
            this.panelFile.TabIndex = 116;
            // 
            // linkLabelCharSet
            // 
            this.linkLabelCharSet.Location = new System.Drawing.Point(632, 48);
            this.linkLabelCharSet.Name = "linkLabelCharSet";
            this.linkLabelCharSet.Size = new System.Drawing.Size(77, 26);
            this.linkLabelCharSet.TabIndex = 127;
            this.linkLabelCharSet.TabStop = true;
            this.linkLabelCharSet.Text = "Character Set Reference";
            this.linkLabelCharSet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCharSet_LinkClicked);
            // 
            // textBoxIndexFolder
            // 
            this.textBoxIndexFolder.Location = new System.Drawing.Point(476, 107);
            this.textBoxIndexFolder.Name = "textBoxIndexFolder";
            this.textBoxIndexFolder.Size = new System.Drawing.Size(152, 20);
            this.textBoxIndexFolder.TabIndex = 122;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 121;
            this.label5.Text = "Index File Folder Path:";
            // 
            // checkBoxIndexFile
            // 
            this.checkBoxIndexFile.AutoSize = true;
            this.checkBoxIndexFile.Location = new System.Drawing.Point(127, 110);
            this.checkBoxIndexFile.Name = "checkBoxIndexFile";
            this.checkBoxIndexFile.Size = new System.Drawing.Size(213, 17);
            this.checkBoxIndexFile.TabIndex = 120;
            this.checkBoxIndexFile.Text = "Create Index File after Writing Data File.";
            this.checkBoxIndexFile.UseVisualStyleBackColor = true;
            this.checkBoxIndexFile.CheckedChanged += new System.EventHandler(this.checkBoxIndexFile_CheckedChanged);
            // 
            // textBoxFileExtension
            // 
            this.textBoxFileExtension.Location = new System.Drawing.Point(476, 81);
            this.textBoxFileExtension.Name = "textBoxFileExtension";
            this.textBoxFileExtension.Size = new System.Drawing.Size(152, 20);
            this.textBoxFileExtension.TabIndex = 119;
            // 
            // labelFilePattern
            // 
            this.labelFilePattern.AutoSize = true;
            this.labelFilePattern.Location = new System.Drawing.Point(350, 84);
            this.labelFilePattern.Name = "labelFilePattern";
            this.labelFilePattern.Size = new System.Drawing.Size(106, 13);
            this.labelFilePattern.TabIndex = 118;
            this.labelFilePattern.Text = "File Name Extension:";
            // 
            // textBoxDataFolder
            // 
            this.textBoxDataFolder.Location = new System.Drawing.Point(127, 81);
            this.textBoxDataFolder.Name = "textBoxDataFolder";
            this.textBoxDataFolder.Size = new System.Drawing.Size(203, 20);
            this.textBoxDataFolder.TabIndex = 117;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 116;
            this.label3.Text = "Data File Folder Path:";
            // 
            // textBoxFileConnString
            // 
            this.textBoxFileConnString.Location = new System.Drawing.Point(127, 10);
            this.textBoxFileConnString.Multiline = true;
            this.textBoxFileConnString.Name = "textBoxFileConnString";
            this.textBoxFileConnString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFileConnString.Size = new System.Drawing.Size(501, 64);
            this.textBoxFileConnString.TabIndex = 115;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 114;
            this.label2.Text = "Connection String:";
            // 
            // buttonDefault
            // 
            this.buttonDefault.Location = new System.Drawing.Point(292, 193);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(214, 23);
            this.buttonDefault.TabIndex = 114;
            this.buttonDefault.Text = "Load Default Connection String";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButtonFile);
            this.panel2.Controls.Add(this.radioButtonDB);
            this.panel2.Location = new System.Drawing.Point(13, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(698, 26);
            this.panel2.TabIndex = 115;
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.AutoSize = true;
            this.radioButtonFile.Location = new System.Drawing.Point(221, 6);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(176, 17);
            this.radioButtonFile.TabIndex = 1;
            this.radioButtonFile.TabStop = true;
            this.radioButtonFile.Text = "Connect to Third Party Data File";
            this.radioButtonFile.UseVisualStyleBackColor = true;
            this.radioButtonFile.CheckedChanged += new System.EventHandler(this.radioButtonFile_CheckedChanged);
            // 
            // radioButtonDB
            // 
            this.radioButtonDB.AutoSize = true;
            this.radioButtonDB.Location = new System.Drawing.Point(7, 6);
            this.radioButtonDB.Name = "radioButtonDB";
            this.radioButtonDB.Size = new System.Drawing.Size(180, 17);
            this.radioButtonDB.TabIndex = 0;
            this.radioButtonDB.TabStop = true;
            this.radioButtonDB.Text = "Connect to Third Party Database";
            this.radioButtonDB.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(140, 193);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(126, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // panelChannels
            // 
            this.panelChannels.Controls.Add(this.groupBox3);
            this.panelChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChannels.Location = new System.Drawing.Point(0, 236);
            this.panelChannels.Name = "panelChannels";
            this.panelChannels.Size = new System.Drawing.Size(750, 214);
            this.panelChannels.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblChannel);
            this.groupBox3.Controls.Add(this.lblTimeInterval);
            this.groupBox3.Controls.Add(this.numericUpDown);
            this.groupBox3.Controls.Add(this.btnChannelCopy);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnModify);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Controls.Add(this.lstvChannel);
            this.groupBox3.Location = new System.Drawing.Point(15, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(723, 209);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Third Party Data Outnbound Channels";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(362, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 106;
            this.label4.Text = "ms";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(20, 61);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(101, 13);
            this.lblChannel.TabIndex = 105;
            this.lblChannel.Text = "Outbound Channels";
            // 
            // lblTimeInterval
            // 
            this.lblTimeInterval.AutoSize = true;
            this.lblTimeInterval.Location = new System.Drawing.Point(20, 27);
            this.lblTimeInterval.Name = "lblTimeInterval";
            this.lblTimeInterval.Size = new System.Drawing.Size(80, 13);
            this.lblTimeInterval.TabIndex = 104;
            this.lblTimeInterval.Text = "Access Interval";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(127, 25);
            this.numericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(229, 20);
            this.numericUpDown.TabIndex = 0;
            this.numericUpDown.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // btnChannelCopy
            // 
            this.btnChannelCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelCopy.Enabled = false;
            this.btnChannelCopy.Location = new System.Drawing.Point(591, 117);
            this.btnChannelCopy.Name = "btnChannelCopy";
            this.btnChannelCopy.Size = new System.Drawing.Size(70, 22);
            this.btnChannelCopy.TabIndex = 4;
            this.btnChannelCopy.Text = "Copy";
            this.btnChannelCopy.UseVisualStyleBackColor = true;
            this.btnChannelCopy.Click += new System.EventHandler(this.btnChannelCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(591, 145);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 22);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.Enabled = false;
            this.btnModify.Location = new System.Drawing.Point(591, 89);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(70, 22);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = "Edit";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(591, 61);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 22);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstvChannel
            // 
            this.lstvChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvChannel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5});
            this.lstvChannel.FullRowSelect = true;
            this.lstvChannel.HideSelection = false;
            this.lstvChannel.Location = new System.Drawing.Point(127, 61);
            this.lstvChannel.Name = "lstvChannel";
            this.lstvChannel.Size = new System.Drawing.Size(458, 134);
            this.lstvChannel.TabIndex = 1;
            this.lstvChannel.UseCompatibleStateImageBehavior = false;
            this.lstvChannel.View = System.Windows.Forms.View.Details;
            this.lstvChannel.DoubleClick += new System.EventHandler(this.lstvChannel_DoubleClick);
            this.lstvChannel.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvChannel_ColumnClick);
            this.lstvChannel.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvChannel_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 23;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channel Name";
            this.columnHeader2.Width = 143;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Query Mode To Third Party";
            this.columnHeader3.Width = 181;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            this.columnHeader5.Width = 84;
            // 
            // contextMenuStripConnStr
            // 
            this.contextMenuStripConnStr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCSV,
            this.toolStripMenuItemSQLServer,
            this.toolStripMenuItemOracle,
            this.oracleX64ConnectionStringToolStripMenuItem,
            this.toolStripMenuItemSybase,
            this.toolStripMenuItemCSVX64});
            this.contextMenuStripConnStr.Name = "contextMenuStripConnStr";
            this.contextMenuStripConnStr.Size = new System.Drawing.Size(284, 158);
            // 
            // toolStripMenuItemCSV
            // 
            this.toolStripMenuItemCSV.Name = "toolStripMenuItemCSV";
            this.toolStripMenuItemCSV.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItemCSV.Text = "CSV File Connection String";
            this.toolStripMenuItemCSV.Click += new System.EventHandler(this.toolStripMenuItemCSV_Click);
            // 
            // toolStripMenuItemSQLServer
            // 
            this.toolStripMenuItemSQLServer.Name = "toolStripMenuItemSQLServer";
            this.toolStripMenuItemSQLServer.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItemSQLServer.Text = "SQL Server Connection String";
            this.toolStripMenuItemSQLServer.Click += new System.EventHandler(this.toolStripMenuItemSQLServer_Click);
            // 
            // toolStripMenuItemOracle
            // 
            this.toolStripMenuItemOracle.Name = "toolStripMenuItemOracle";
            this.toolStripMenuItemOracle.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItemOracle.Text = "Oracle Connection String";
            this.toolStripMenuItemOracle.Click += new System.EventHandler(this.toolStripMenuItemOracle_Click);
            // 
            // toolStripMenuItemSybase
            // 
            this.toolStripMenuItemSybase.Name = "toolStripMenuItemSybase";
            this.toolStripMenuItemSybase.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItemSybase.Text = "Sybase Connection String";
            this.toolStripMenuItemSybase.Click += new System.EventHandler(this.toolStripMenuItemSybase_Click);
            // 
            // toolStripMenuItemCSVX64
            // 
            this.toolStripMenuItemCSVX64.Name = "toolStripMenuItemCSVX64";
            this.toolStripMenuItemCSVX64.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItemCSVX64.Text = "CSV File Connection String For X64 Platform";
            this.toolStripMenuItemCSVX64.Click += new System.EventHandler(this.toolStripMenuItemCSVX64_Click);
            // 
            // oracleX64ConnectionStringToolStripMenuItem
            // 
            this.oracleX64ConnectionStringToolStripMenuItem.Name = "oracleX64ConnectionStringToolStripMenuItem";
            this.oracleX64ConnectionStringToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.oracleX64ConnectionStringToolStripMenuItem.Text = "Oracle X64 Connection String";
            this.oracleX64ConnectionStringToolStripMenuItem.Click += new System.EventHandler(this.oracleX64ConnectionStringToolStripMenuItem_Click);
            // 
            // ActiveMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelChannels);
            this.Controls.Add(this.panelConnection);
            this.Name = "ActiveMode";
            this.Size = new System.Drawing.Size(750, 450);
            this.panelConnection.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panelDB.ResumeLayout(false);
            this.panelDB.PerformLayout();
            this.panelFile.ResumeLayout(false);
            this.panelFile.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelChannels.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.contextMenuStripConnStr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelConnection;
        private System.Windows.Forms.Panel panelChannels;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lstvChannel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblTimeInterval;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Button btnChannelCopy;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelFile;
        private System.Windows.Forms.TextBox textBoxIndexFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxIndexFile;
        private System.Windows.Forms.TextBox textBoxFileExtension;
        private System.Windows.Forms.Label labelFilePattern;
        private System.Windows.Forms.TextBox textBoxDataFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFileConnString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProvider;
        private System.Windows.Forms.LinkLabel llblDetail;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.RadioButton radioButtonDB;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripConnStr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCSV;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSQLServer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOracle;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSybase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelCharSet;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCSVX64;
        private System.Windows.Forms.CheckBox chkOracleDriver;
        private System.Windows.Forms.ToolStripMenuItem oracleX64ConnectionStringToolStripMenuItem;
    }
}
