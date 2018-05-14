namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration
{
    partial class FSocketInConfiguration
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
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pMain = new System.Windows.Forms.Panel();
            this.panelChannel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.btnChannelAdd = new System.Windows.Forms.Button();
            this.btnChannelCopy = new System.Windows.Forms.Button();
            this.btnChannelDelete = new System.Windows.Forms.Button();
            this.btnChannelModify = new System.Windows.Forms.Button();
            this.lstvChannel = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.panelLUT = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblLUT = new System.Windows.Forms.Label();
            this.btnLUTAdd = new System.Windows.Forms.Button();
            this.btnLUTDelete = new System.Windows.Forms.Button();
            this.btnLUTModify = new System.Windows.Forms.Button();
            this.lstvLUT = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.panelServerParameters = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCodePage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceiveTimeout = new System.Windows.Forms.TextBox();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.txtSendTimeout = new System.Windows.Forms.TextBox();
            this.lblSendTimeout = new System.Windows.Forms.Label();
            this.txtTryCount = new System.Windows.Forms.TextBox();
            this.lblTryCount = new System.Windows.Forms.Label();
            this.txtConnectTimeout = new System.Windows.Forms.TextBox();
            this.lblConnectTimeout = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblListenPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblListenIP = new System.Windows.Forms.Label();
            this.panelControl.SuspendLayout();
            this.pMain.SuspendLayout();
            this.panelChannel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelLUT.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelServerParameters.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 633);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(792, 33);
            this.panelControl.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(707, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(628, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 22);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.panelChannel);
            this.pMain.Controls.Add(this.panelLUT);
            this.pMain.Controls.Add(this.panelServerParameters);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(792, 633);
            this.pMain.TabIndex = 4;
            // 
            // panelChannel
            // 
            this.panelChannel.Controls.Add(this.groupBox3);
            this.panelChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChannel.Location = new System.Drawing.Point(0, 362);
            this.panelChannel.Name = "panelChannel";
            this.panelChannel.Size = new System.Drawing.Size(792, 271);
            this.panelChannel.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblChannel);
            this.groupBox3.Controls.Add(this.btnChannelAdd);
            this.groupBox3.Controls.Add(this.btnChannelCopy);
            this.groupBox3.Controls.Add(this.btnChannelDelete);
            this.groupBox3.Controls.Add(this.btnChannelModify);
            this.groupBox3.Controls.Add(this.lstvChannel);
            this.groupBox3.Location = new System.Drawing.Point(13, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(767, 263);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channel";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(19, 19);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(93, 13);
            this.lblChannel.TabIndex = 24;
            this.lblChannel.Text = "Inbound Channels";
            // 
            // btnChannelAdd
            // 
            this.btnChannelAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelAdd.Location = new System.Drawing.Point(675, 19);
            this.btnChannelAdd.Name = "btnChannelAdd";
            this.btnChannelAdd.Size = new System.Drawing.Size(73, 22);
            this.btnChannelAdd.TabIndex = 4;
            this.btnChannelAdd.Text = "Add";
            this.btnChannelAdd.UseVisualStyleBackColor = true;
            this.btnChannelAdd.Click += new System.EventHandler(this.btnChannelAdd_Click);
            // 
            // btnChannelCopy
            // 
            this.btnChannelCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelCopy.Enabled = false;
            this.btnChannelCopy.Location = new System.Drawing.Point(675, 75);
            this.btnChannelCopy.Name = "btnChannelCopy";
            this.btnChannelCopy.Size = new System.Drawing.Size(73, 22);
            this.btnChannelCopy.TabIndex = 3;
            this.btnChannelCopy.Text = "Copy";
            this.btnChannelCopy.UseVisualStyleBackColor = true;
            this.btnChannelCopy.Click += new System.EventHandler(this.btnChannelCopy_Click);
            // 
            // btnChannelDelete
            // 
            this.btnChannelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelDelete.Enabled = false;
            this.btnChannelDelete.Location = new System.Drawing.Point(675, 103);
            this.btnChannelDelete.Name = "btnChannelDelete";
            this.btnChannelDelete.Size = new System.Drawing.Size(73, 22);
            this.btnChannelDelete.TabIndex = 3;
            this.btnChannelDelete.Text = "Delete";
            this.btnChannelDelete.UseVisualStyleBackColor = true;
            this.btnChannelDelete.Click += new System.EventHandler(this.btnChannelDelete_Click);
            // 
            // btnChannelModify
            // 
            this.btnChannelModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelModify.Enabled = false;
            this.btnChannelModify.Location = new System.Drawing.Point(675, 47);
            this.btnChannelModify.Name = "btnChannelModify";
            this.btnChannelModify.Size = new System.Drawing.Size(73, 22);
            this.btnChannelModify.TabIndex = 2;
            this.btnChannelModify.Text = "Edit";
            this.btnChannelModify.UseVisualStyleBackColor = true;
            this.btnChannelModify.Click += new System.EventHandler(this.btnChannelModify_Click);
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
            this.lstvChannel.Location = new System.Drawing.Point(138, 19);
            this.lstvChannel.MultiSelect = false;
            this.lstvChannel.Name = "lstvChannel";
            this.lstvChannel.Size = new System.Drawing.Size(531, 203);
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
            this.columnHeader1.Width = 54;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channel Name";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Access Mode";
            this.columnHeader3.Width = 153;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Enable";
            this.columnHeader5.Width = 90;
            // 
            // panelLUT
            // 
            this.panelLUT.Controls.Add(this.groupBox2);
            this.panelLUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLUT.Location = new System.Drawing.Point(0, 153);
            this.panelLUT.Name = "panelLUT";
            this.panelLUT.Size = new System.Drawing.Size(792, 209);
            this.panelLUT.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblLUT);
            this.groupBox2.Controls.Add(this.btnLUTAdd);
            this.groupBox2.Controls.Add(this.btnLUTDelete);
            this.groupBox2.Controls.Add(this.btnLUTModify);
            this.groupBox2.Controls.Add(this.lstvLUT);
            this.groupBox2.Location = new System.Drawing.Point(13, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(767, 189);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lookup Table";
            // 
            // lblLUT
            // 
            this.lblLUT.AutoSize = true;
            this.lblLUT.Location = new System.Drawing.Point(19, 22);
            this.lblLUT.Name = "lblLUT";
            this.lblLUT.Size = new System.Drawing.Size(94, 13);
            this.lblLUT.TabIndex = 24;
            this.lblLUT.Text = "Translation Tables";
            // 
            // btnLUTAdd
            // 
            this.btnLUTAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLUTAdd.Location = new System.Drawing.Point(675, 22);
            this.btnLUTAdd.Name = "btnLUTAdd";
            this.btnLUTAdd.Size = new System.Drawing.Size(73, 22);
            this.btnLUTAdd.TabIndex = 7;
            this.btnLUTAdd.Text = "Add";
            this.btnLUTAdd.UseVisualStyleBackColor = true;
            this.btnLUTAdd.Click += new System.EventHandler(this.btnLUTAdd_Click);
            // 
            // btnLUTDelete
            // 
            this.btnLUTDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLUTDelete.Enabled = false;
            this.btnLUTDelete.Location = new System.Drawing.Point(675, 78);
            this.btnLUTDelete.Name = "btnLUTDelete";
            this.btnLUTDelete.Size = new System.Drawing.Size(73, 22);
            this.btnLUTDelete.TabIndex = 6;
            this.btnLUTDelete.Text = "Delete";
            this.btnLUTDelete.UseVisualStyleBackColor = true;
            this.btnLUTDelete.Click += new System.EventHandler(this.btnLUTDelete_Click);
            // 
            // btnLUTModify
            // 
            this.btnLUTModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLUTModify.Enabled = false;
            this.btnLUTModify.Location = new System.Drawing.Point(675, 50);
            this.btnLUTModify.Name = "btnLUTModify";
            this.btnLUTModify.Size = new System.Drawing.Size(73, 22);
            this.btnLUTModify.TabIndex = 5;
            this.btnLUTModify.Text = "Edit";
            this.btnLUTModify.UseVisualStyleBackColor = true;
            this.btnLUTModify.Click += new System.EventHandler(this.btnLUTModify_Click);
            // 
            // lstvLUT
            // 
            this.lstvLUT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvLUT.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lstvLUT.FullRowSelect = true;
            this.lstvLUT.HideSelection = false;
            this.lstvLUT.Location = new System.Drawing.Point(138, 22);
            this.lstvLUT.MultiSelect = false;
            this.lstvLUT.Name = "lstvLUT";
            this.lstvLUT.Size = new System.Drawing.Size(531, 161);
            this.lstvLUT.TabIndex = 2;
            this.lstvLUT.UseCompatibleStateImageBehavior = false;
            this.lstvLUT.View = System.Windows.Forms.View.Details;
            this.lstvLUT.DoubleClick += new System.EventHandler(this.lstvLUT_DoubleClick);
            this.lstvLUT.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvLUT_ColumnClick);
            this.lstvLUT.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvLUT_ItemSelectionChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#";
            this.columnHeader6.Width = 59;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Table Name";
            this.columnHeader7.Width = 298;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Item Count";
            this.columnHeader8.Width = 108;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Table Source";
            this.columnHeader9.Width = 0;
            // 
            // panelServerParameters
            // 
            this.panelServerParameters.Controls.Add(this.groupBox1);
            this.panelServerParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelServerParameters.Location = new System.Drawing.Point(0, 0);
            this.panelServerParameters.Name = "panelServerParameters";
            this.panelServerParameters.Size = new System.Drawing.Size(792, 153);
            this.panelServerParameters.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbCodePage);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtReceiveTimeout);
            this.groupBox1.Controls.Add(this.lblReceiveTimeout);
            this.groupBox1.Controls.Add(this.txtSendTimeout);
            this.groupBox1.Controls.Add(this.lblSendTimeout);
            this.groupBox1.Controls.Add(this.txtTryCount);
            this.groupBox1.Controls.Add(this.lblTryCount);
            this.groupBox1.Controls.Add(this.txtConnectTimeout);
            this.groupBox1.Controls.Add(this.lblConnectTimeout);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.lblListenPort);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.lblListenIP);
            this.groupBox1.Location = new System.Drawing.Point(13, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Paramenters";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Encoding";
            // 
            // cbCodePage
            // 
            this.cbCodePage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCodePage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCodePage.FormattingEnabled = true;
            this.cbCodePage.Location = new System.Drawing.Point(138, 123);
            this.cbCodePage.Name = "cbCodePage";
            this.cbCodePage.Size = new System.Drawing.Size(322, 21);
            this.cbCodePage.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(466, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(466, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(466, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "time(s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(721, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "ms";
            this.label2.Visible = false;
            // 
            // txtReceiveTimeout
            // 
            this.txtReceiveTimeout.Location = new System.Drawing.Point(138, 92);
            this.txtReceiveTimeout.Name = "txtReceiveTimeout";
            this.txtReceiveTimeout.Size = new System.Drawing.Size(322, 20);
            this.txtReceiveTimeout.TabIndex = 23;
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.AutoSize = true;
            this.lblReceiveTimeout.Location = new System.Drawing.Point(19, 94);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(88, 13);
            this.lblReceiveTimeout.TabIndex = 22;
            this.lblReceiveTimeout.Text = "Receive Timeout";
            // 
            // txtSendTimeout
            // 
            this.txtSendTimeout.Location = new System.Drawing.Point(138, 66);
            this.txtSendTimeout.Name = "txtSendTimeout";
            this.txtSendTimeout.Size = new System.Drawing.Size(322, 20);
            this.txtSendTimeout.TabIndex = 21;
            // 
            // lblSendTimeout
            // 
            this.lblSendTimeout.AutoSize = true;
            this.lblSendTimeout.Location = new System.Drawing.Point(19, 68);
            this.lblSendTimeout.Name = "lblSendTimeout";
            this.lblSendTimeout.Size = new System.Drawing.Size(73, 13);
            this.lblSendTimeout.TabIndex = 20;
            this.lblSendTimeout.Text = "Send Timeout";
            // 
            // txtTryCount
            // 
            this.txtTryCount.Location = new System.Drawing.Point(138, 40);
            this.txtTryCount.Name = "txtTryCount";
            this.txtTryCount.Size = new System.Drawing.Size(322, 20);
            this.txtTryCount.TabIndex = 19;
            // 
            // lblTryCount
            // 
            this.lblTryCount.AutoSize = true;
            this.lblTryCount.Location = new System.Drawing.Point(19, 42);
            this.lblTryCount.Name = "lblTryCount";
            this.lblTryCount.Size = new System.Drawing.Size(96, 13);
            this.lblTryCount.TabIndex = 18;
            this.lblTryCount.Text = "Connect Try Times";
            // 
            // txtConnectTimeout
            // 
            this.txtConnectTimeout.Location = new System.Drawing.Point(647, 39);
            this.txtConnectTimeout.Name = "txtConnectTimeout";
            this.txtConnectTimeout.Size = new System.Drawing.Size(68, 20);
            this.txtConnectTimeout.TabIndex = 17;
            this.txtConnectTimeout.Visible = false;
            // 
            // lblConnectTimeout
            // 
            this.lblConnectTimeout.AutoSize = true;
            this.lblConnectTimeout.Location = new System.Drawing.Point(553, 42);
            this.lblConnectTimeout.Name = "lblConnectTimeout";
            this.lblConnectTimeout.Size = new System.Drawing.Size(88, 13);
            this.lblConnectTimeout.TabIndex = 16;
            this.lblConnectTimeout.Text = "Connect Timeout";
            this.lblConnectTimeout.Visible = false;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(526, 9);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(203, 20);
            this.txtIP.TabIndex = 15;
            this.txtIP.Visible = false;
            // 
            // lblListenPort
            // 
            this.lblListenPort.AutoSize = true;
            this.lblListenPort.Location = new System.Drawing.Point(19, 19);
            this.lblListenPort.Name = "lblListenPort";
            this.lblListenPort.Size = new System.Drawing.Size(57, 13);
            this.lblListenPort.TabIndex = 14;
            this.lblListenPort.Text = "Listen Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(138, 14);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(108, 20);
            this.txtPort.TabIndex = 13;
            // 
            // lblListenIP
            // 
            this.lblListenIP.AutoSize = true;
            this.lblListenIP.Location = new System.Drawing.Point(472, 16);
            this.lblListenIP.Name = "lblListenIP";
            this.lblListenIP.Size = new System.Drawing.Size(48, 13);
            this.lblListenIP.TabIndex = 12;
            this.lblListenIP.Text = "Listen IP";
            this.lblListenIP.Visible = false;
            // 
            // FSocketInConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 666);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.panelControl);
            this.Name = "FSocketInConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Source";
            this.panelControl.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.panelChannel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelLUT.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelServerParameters.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel panelChannel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnChannelAdd;
        private System.Windows.Forms.Button btnChannelDelete;
        private System.Windows.Forms.Button btnChannelModify;
        private System.Windows.Forms.ListView lstvChannel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel panelLUT;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLUTAdd;
        private System.Windows.Forms.Button btnLUTDelete;
        private System.Windows.Forms.Button btnLUTModify;
        private System.Windows.Forms.ListView lstvLUT;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Panel panelServerParameters;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtReceiveTimeout;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.TextBox txtSendTimeout;
        private System.Windows.Forms.Label lblSendTimeout;
        private System.Windows.Forms.TextBox txtTryCount;
        private System.Windows.Forms.Label lblTryCount;
        private System.Windows.Forms.TextBox txtConnectTimeout;
        private System.Windows.Forms.Label lblConnectTimeout;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblListenPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblListenIP;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLUT;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCodePage;
        private System.Windows.Forms.Button btnChannelCopy;
    }
}