namespace HYS.FileAdapter.FileOutboundAdapterConfiguration
{
    partial class FFileOutConfig
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
            this.btLoadDefault = new System.Windows.Forms.Button();
            this.pMain = new System.Windows.Forms.Panel();
            this.panelChannel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnChannelCopy = new System.Windows.Forms.Button();
            this.lblChannel = new System.Windows.Forms.Label();
            this.btnChannelAdd = new System.Windows.Forms.Button();
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
            this.btSelPreGWDBField = new System.Windows.Forms.Button();
            this.mtbTimerInterval = new System.Windows.Forms.NumericUpDown();
            this.tbFileDTFormat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btSelFilePath = new System.Windows.Forms.Button();
            this.tbFileSuffix = new System.Windows.Forms.TextBox();
            this.tbFilePrefix = new System.Windows.Forms.TextBox();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCodePage = new System.Windows.Forms.ComboBox();
            this.panelControl.SuspendLayout();
            this.pMain.SuspendLayout();
            this.panelChannel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelLUT.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelServerParameters.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtbTimerInterval)).BeginInit();
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
            // btLoadDefault
            // 
            this.btLoadDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoadDefault.Location = new System.Drawing.Point(638, 119);
            this.btLoadDefault.Name = "btLoadDefault";
            this.btLoadDefault.Size = new System.Drawing.Size(110, 22);
            this.btLoadDefault.TabIndex = 2;
            this.btLoadDefault.Text = "Load Default";
            this.btLoadDefault.UseVisualStyleBackColor = true;
            this.btLoadDefault.Click += new System.EventHandler(this.btLoadDefault_Click);
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
            this.panelChannel.Location = new System.Drawing.Point(0, 321);
            this.panelChannel.Name = "panelChannel";
            this.panelChannel.Size = new System.Drawing.Size(792, 312);
            this.panelChannel.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnChannelCopy);
            this.groupBox3.Controls.Add(this.lblChannel);
            this.groupBox3.Controls.Add(this.btnChannelAdd);
            this.groupBox3.Controls.Add(this.btnChannelDelete);
            this.groupBox3.Controls.Add(this.btnChannelModify);
            this.groupBox3.Controls.Add(this.lstvChannel);
            this.groupBox3.Location = new System.Drawing.Point(13, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(767, 304);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Third Party Data Outbound Channels";
            // 
            // btnChannelCopy
            // 
            this.btnChannelCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelCopy.Enabled = false;
            this.btnChannelCopy.Location = new System.Drawing.Point(675, 82);
            this.btnChannelCopy.Name = "btnChannelCopy";
            this.btnChannelCopy.Size = new System.Drawing.Size(73, 22);
            this.btnChannelCopy.TabIndex = 24;
            this.btnChannelCopy.Text = "Copy";
            this.btnChannelCopy.UseVisualStyleBackColor = true;
            this.btnChannelCopy.Click += new System.EventHandler(this.btnChannelCopy_Click);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(19, 20);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(101, 13);
            this.lblChannel.TabIndex = 23;
            this.lblChannel.Text = "Outbound Channels";
            // 
            // btnChannelAdd
            // 
            this.btnChannelAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelAdd.Location = new System.Drawing.Point(675, 26);
            this.btnChannelAdd.Name = "btnChannelAdd";
            this.btnChannelAdd.Size = new System.Drawing.Size(73, 22);
            this.btnChannelAdd.TabIndex = 4;
            this.btnChannelAdd.Text = "Add";
            this.btnChannelAdd.UseVisualStyleBackColor = true;
            this.btnChannelAdd.Click += new System.EventHandler(this.btnChannelAdd_Click);
            // 
            // btnChannelDelete
            // 
            this.btnChannelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelDelete.Enabled = false;
            this.btnChannelDelete.Location = new System.Drawing.Point(675, 110);
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
            this.btnChannelModify.Location = new System.Drawing.Point(675, 54);
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
            this.lstvChannel.Location = new System.Drawing.Point(141, 20);
            this.lstvChannel.Name = "lstvChannel";
            this.lstvChannel.Size = new System.Drawing.Size(528, 278);
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
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channel Name";
            this.columnHeader2.Width = 145;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Access Mode";
            this.columnHeader3.Width = 147;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Enable";
            this.columnHeader5.Width = 76;
            // 
            // panelLUT
            // 
            this.panelLUT.Controls.Add(this.groupBox2);
            this.panelLUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLUT.Location = new System.Drawing.Point(0, 191);
            this.panelLUT.Name = "panelLUT";
            this.panelLUT.Size = new System.Drawing.Size(792, 130);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(767, 109);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lookup Table";
            // 
            // lblLUT
            // 
            this.lblLUT.AutoSize = true;
            this.lblLUT.Location = new System.Drawing.Point(20, 19);
            this.lblLUT.Name = "lblLUT";
            this.lblLUT.Size = new System.Drawing.Size(89, 13);
            this.lblLUT.TabIndex = 23;
            this.lblLUT.Text = "Translation Table";
            // 
            // btnLUTAdd
            // 
            this.btnLUTAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLUTAdd.Location = new System.Drawing.Point(675, 19);
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
            this.btnLUTDelete.Location = new System.Drawing.Point(675, 75);
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
            this.btnLUTModify.Location = new System.Drawing.Point(675, 47);
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
            this.lstvLUT.Location = new System.Drawing.Point(141, 19);
            this.lstvLUT.Name = "lstvLUT";
            this.lstvLUT.Size = new System.Drawing.Size(528, 84);
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
            this.columnHeader6.Width = 24;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Table Name";
            this.columnHeader7.Width = 137;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Item Count";
            this.columnHeader8.Width = 151;
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
            this.panelServerParameters.Size = new System.Drawing.Size(792, 191);
            this.panelServerParameters.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbCodePage);
            this.groupBox1.Controls.Add(this.btSelPreGWDBField);
            this.groupBox1.Controls.Add(this.btLoadDefault);
            this.groupBox1.Controls.Add(this.mtbTimerInterval);
            this.groupBox1.Controls.Add(this.tbFileDTFormat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btSelFilePath);
            this.groupBox1.Controls.Add(this.tbFileSuffix);
            this.groupBox1.Controls.Add(this.tbFilePrefix);
            this.groupBox1.Controls.Add(this.tbFilePath);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 157);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Parameters";
            // 
            // btSelPreGWDBField
            // 
            this.btSelPreGWDBField.Location = new System.Drawing.Point(261, 60);
            this.btSelPreGWDBField.Name = "btSelPreGWDBField";
            this.btSelPreGWDBField.Size = new System.Drawing.Size(17, 18);
            this.btSelPreGWDBField.TabIndex = 38;
            this.btSelPreGWDBField.Text = ">>";
            this.btSelPreGWDBField.UseVisualStyleBackColor = true;
            this.btSelPreGWDBField.Click += new System.EventHandler(this.btSelPreGWDBField_Click);
            // 
            // mtbTimerInterval
            // 
            this.mtbTimerInterval.Location = new System.Drawing.Point(138, 33);
            this.mtbTimerInterval.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.mtbTimerInterval.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.mtbTimerInterval.Name = "mtbTimerInterval";
            this.mtbTimerInterval.Size = new System.Drawing.Size(140, 20);
            this.mtbTimerInterval.TabIndex = 37;
            this.mtbTimerInterval.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // tbFileDTFormat
            // 
            this.tbFileDTFormat.Location = new System.Drawing.Point(430, 63);
            this.tbFileDTFormat.Name = "tbFileDTFormat";
            this.tbFileDTFormat.Size = new System.Drawing.Size(238, 20);
            this.tbFileDTFormat.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "File Name DateTime Format";
            // 
            // btSelFilePath
            // 
            this.btSelFilePath.BackgroundImage = global::HYS.FileAdapter.FileOutboundAdapterConfiguration.Properties.Resources.folder;
            this.btSelFilePath.Location = new System.Drawing.Point(650, 34);
            this.btSelFilePath.Name = "btSelFilePath";
            this.btSelFilePath.Size = new System.Drawing.Size(18, 17);
            this.btSelFilePath.TabIndex = 31;
            this.btSelFilePath.Text = "...";
            this.btSelFilePath.UseVisualStyleBackColor = true;
            this.btSelFilePath.Click += new System.EventHandler(this.btSelFilePath_Click);
            // 
            // tbFileSuffix
            // 
            this.tbFileSuffix.Location = new System.Drawing.Point(138, 88);
            this.tbFileSuffix.Name = "tbFileSuffix";
            this.tbFileSuffix.Size = new System.Drawing.Size(140, 20);
            this.tbFileSuffix.TabIndex = 29;
            this.tbFileSuffix.Text = ".ini";
            // 
            // tbFilePrefix
            // 
            this.tbFilePrefix.Location = new System.Drawing.Point(137, 59);
            this.tbFilePrefix.Name = "tbFilePrefix";
            this.tbFilePrefix.Size = new System.Drawing.Size(123, 20);
            this.tbFilePrefix.TabIndex = 29;
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(430, 33);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(239, 20);
            this.tbFilePath.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "File Name Extension";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "File Name Prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Destination Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Timer Interval(ms)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Encoding";
            // 
            // cbCodePage
            // 
            this.cbCodePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCodePage.Location = new System.Drawing.Point(138, 122);
            this.cbCodePage.Name = "cbCodePage";
            this.cbCodePage.Size = new System.Drawing.Size(322, 21);
            this.cbCodePage.TabIndex = 44;
            // 
            // FFileOutConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 666);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.panelControl);
            this.Name = "FFileOutConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Destination";
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
            ((System.ComponentModel.ISupportInitialize)(this.mtbTimerInterval)).EndInit();
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
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblLUT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btSelFilePath;
        private System.Windows.Forms.TextBox tbFileSuffix;
        private System.Windows.Forms.TextBox tbFilePrefix;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileDTFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.NumericUpDown mtbTimerInterval;
        private System.Windows.Forms.Button btnChannelCopy;
        private System.Windows.Forms.Button btLoadDefault;
        private System.Windows.Forms.Button btSelPreGWDBField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCodePage;
    }
}

