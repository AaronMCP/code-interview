namespace HYS.IM.IPMonitor
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
            this.components = new System.ComponentModel.Container();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabIP = new System.Windows.Forms.TabPage();
            this.btnService = new System.Windows.Forms.Button();
            this.textBoxServiceName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxTimeout = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboAdapterName = new System.Windows.Forms.ComboBox();
            this.textBoxInterval = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxDNSPrivate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxGatewayPrivate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSubnetPrivate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIPPrivate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDNS = new System.Windows.Forms.TextBox();
            this.lDNS = new System.Windows.Forms.Label();
            this.cbDNS = new System.Windows.Forms.CheckBox();
            this.textBoxGateway = new System.Windows.Forms.TextBox();
            this.lGateway = new System.Windows.Forms.Label();
            this.textBoxSubnet = new System.Windows.Forms.TextBox();
            this.lsubnet = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGateway = new System.Windows.Forms.CheckBox();
            this.tabFlag = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btAddService = new System.Windows.Forms.Button();
            this.listServices = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btdeleteValidation = new System.Windows.Forms.Button();
            this.listValidations = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btAddValidation = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbValidations = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.tbFlagInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cboFlagAdapterName = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbIPEnable = new System.Windows.Forms.CheckBox();
            this.cbFlagEnable = new System.Windows.Forms.CheckBox();
            this.tabControlMain.SuspendLayout();
            this.tabIP.SuspendLayout();
            this.tabFlag.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlMain.Controls.Add(this.tabIP);
            this.tabControlMain.Controls.Add(this.tabFlag);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(600, 564);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabIP
            // 
            this.tabIP.Controls.Add(this.cbIPEnable);
            this.tabIP.Controls.Add(this.btnService);
            this.tabIP.Controls.Add(this.textBoxServiceName);
            this.tabIP.Controls.Add(this.label10);
            this.tabIP.Controls.Add(this.textBoxTimeout);
            this.tabIP.Controls.Add(this.label9);
            this.tabIP.Controls.Add(this.cboAdapterName);
            this.tabIP.Controls.Add(this.textBoxInterval);
            this.tabIP.Controls.Add(this.label8);
            this.tabIP.Controls.Add(this.textBoxDNSPrivate);
            this.tabIP.Controls.Add(this.label6);
            this.tabIP.Controls.Add(this.textBoxGatewayPrivate);
            this.tabIP.Controls.Add(this.label5);
            this.tabIP.Controls.Add(this.textBoxSubnetPrivate);
            this.tabIP.Controls.Add(this.label3);
            this.tabIP.Controls.Add(this.textBoxIPPrivate);
            this.tabIP.Controls.Add(this.label4);
            this.tabIP.Controls.Add(this.textBoxDNS);
            this.tabIP.Controls.Add(this.lDNS);
            this.tabIP.Controls.Add(this.cbDNS);
            this.tabIP.Controls.Add(this.textBoxGateway);
            this.tabIP.Controls.Add(this.lGateway);
            this.tabIP.Controls.Add(this.textBoxSubnet);
            this.tabIP.Controls.Add(this.lsubnet);
            this.tabIP.Controls.Add(this.textBoxIP);
            this.tabIP.Controls.Add(this.label2);
            this.tabIP.Controls.Add(this.label1);
            this.tabIP.Controls.Add(this.cbGateway);
            this.tabIP.Location = new System.Drawing.Point(4, 22);
            this.tabIP.Name = "tabIP";
            this.tabIP.Padding = new System.Windows.Forms.Padding(3);
            this.tabIP.Size = new System.Drawing.Size(592, 538);
            this.tabIP.TabIndex = 0;
            this.tabIP.Text = "IPSwitcher Configuration";
            this.tabIP.UseVisualStyleBackColor = true;
            // 
            // btnService
            // 
            this.btnService.Location = new System.Drawing.Point(555, 157);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(34, 27);
            this.btnService.TabIndex = 40;
            this.btnService.Text = "...";
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // textBoxServiceName
            // 
            this.textBoxServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServiceName.Location = new System.Drawing.Point(159, 161);
            this.textBoxServiceName.Name = "textBoxServiceName";
            this.textBoxServiceName.ReadOnly = true;
            this.textBoxServiceName.Size = new System.Drawing.Size(390, 20);
            this.textBoxServiceName.TabIndex = 39;
            this.textBoxServiceName.Text = "CSH FlagService";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(15, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 18);
            this.label10.TabIndex = 38;
            this.label10.Text = "Monitored Service Name:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTimeout.Location = new System.Drawing.Point(159, 112);
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(390, 20);
            this.textBoxTimeout.TabIndex = 37;
            this.textBoxTimeout.Text = "5";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(14, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 18);
            this.label9.TabIndex = 36;
            this.label9.Text = "Timeout for Ping IP (Unit:s):";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboAdapterName
            // 
            this.cboAdapterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAdapterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAdapterName.FormattingEnabled = true;
            this.cboAdapterName.Location = new System.Drawing.Point(159, 218);
            this.cboAdapterName.Name = "cboAdapterName";
            this.cboAdapterName.Size = new System.Drawing.Size(390, 21);
            this.cboAdapterName.TabIndex = 34;
            this.cboAdapterName.SelectedIndexChanged += new System.EventHandler(this.cboAdapterName_SelectedIndexChanged);
            // 
            // textBoxInterval
            // 
            this.textBoxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInterval.Location = new System.Drawing.Point(159, 86);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new System.Drawing.Size(390, 20);
            this.textBoxInterval.TabIndex = 33;
            this.textBoxInterval.Text = "3000";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(14, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 18);
            this.label8.TabIndex = 32;
            this.label8.Text = "Timer Interval (Unit:ms):";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDNSPrivate
            // 
            this.textBoxDNSPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDNSPrivate.Enabled = false;
            this.textBoxDNSPrivate.Location = new System.Drawing.Point(159, 323);
            this.textBoxDNSPrivate.Name = "textBoxDNSPrivate";
            this.textBoxDNSPrivate.Size = new System.Drawing.Size(390, 20);
            this.textBoxDNSPrivate.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 18);
            this.label6.TabIndex = 30;
            this.label6.Text = "Private DNS Server:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxGatewayPrivate
            // 
            this.textBoxGatewayPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGatewayPrivate.Enabled = false;
            this.textBoxGatewayPrivate.Location = new System.Drawing.Point(159, 297);
            this.textBoxGatewayPrivate.Name = "textBoxGatewayPrivate";
            this.textBoxGatewayPrivate.Size = new System.Drawing.Size(390, 20);
            this.textBoxGatewayPrivate.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 18);
            this.label5.TabIndex = 28;
            this.label5.Text = "Private Gateway:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSubnetPrivate
            // 
            this.textBoxSubnetPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubnetPrivate.Location = new System.Drawing.Point(159, 271);
            this.textBoxSubnetPrivate.Name = "textBoxSubnetPrivate";
            this.textBoxSubnetPrivate.Size = new System.Drawing.Size(390, 20);
            this.textBoxSubnetPrivate.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "Private Subnet Mask:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxIPPrivate
            // 
            this.textBoxIPPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIPPrivate.Location = new System.Drawing.Point(159, 245);
            this.textBoxIPPrivate.Name = "textBoxIPPrivate";
            this.textBoxIPPrivate.Size = new System.Drawing.Size(390, 20);
            this.textBoxIPPrivate.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 18);
            this.label4.TabIndex = 24;
            this.label4.Text = "Private IP Address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDNS
            // 
            this.textBoxDNS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDNS.Enabled = false;
            this.textBoxDNS.Location = new System.Drawing.Point(159, 427);
            this.textBoxDNS.Name = "textBoxDNS";
            this.textBoxDNS.Size = new System.Drawing.Size(390, 20);
            this.textBoxDNS.TabIndex = 23;
            // 
            // lDNS
            // 
            this.lDNS.Location = new System.Drawing.Point(14, 426);
            this.lDNS.Name = "lDNS";
            this.lDNS.Size = new System.Drawing.Size(139, 18);
            this.lDNS.TabIndex = 22;
            this.lDNS.Text = "Public DNS Server:";
            this.lDNS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDNS
            // 
            this.cbDNS.AutoSize = true;
            this.cbDNS.Location = new System.Drawing.Point(15, 63);
            this.cbDNS.Name = "cbDNS";
            this.cbDNS.Size = new System.Drawing.Size(158, 17);
            this.cbDNS.TabIndex = 21;
            this.cbDNS.Text = "Allow changing DNS Server";
            this.cbDNS.UseVisualStyleBackColor = true;
            this.cbDNS.CheckedChanged += new System.EventHandler(this.cbDNS_CheckedChanged);
            // 
            // textBoxGateway
            // 
            this.textBoxGateway.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGateway.Enabled = false;
            this.textBoxGateway.Location = new System.Drawing.Point(159, 401);
            this.textBoxGateway.Name = "textBoxGateway";
            this.textBoxGateway.Size = new System.Drawing.Size(390, 20);
            this.textBoxGateway.TabIndex = 20;
            // 
            // lGateway
            // 
            this.lGateway.Location = new System.Drawing.Point(14, 400);
            this.lGateway.Name = "lGateway";
            this.lGateway.Size = new System.Drawing.Size(139, 18);
            this.lGateway.TabIndex = 19;
            this.lGateway.Text = "Public Gateway:";
            this.lGateway.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSubnet
            // 
            this.textBoxSubnet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubnet.Location = new System.Drawing.Point(159, 375);
            this.textBoxSubnet.Name = "textBoxSubnet";
            this.textBoxSubnet.Size = new System.Drawing.Size(390, 20);
            this.textBoxSubnet.TabIndex = 18;
            // 
            // lsubnet
            // 
            this.lsubnet.Location = new System.Drawing.Point(14, 374);
            this.lsubnet.Name = "lsubnet";
            this.lsubnet.Size = new System.Drawing.Size(139, 18);
            this.lsubnet.TabIndex = 17;
            this.lsubnet.Text = "Public Subnet Mask:";
            this.lsubnet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIP.Location = new System.Drawing.Point(159, 349);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(390, 20);
            this.textBoxIP.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "Public IP Address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Network Adapter Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbGateway
            // 
            this.cbGateway.AutoSize = true;
            this.cbGateway.Location = new System.Drawing.Point(15, 40);
            this.cbGateway.Name = "cbGateway";
            this.cbGateway.Size = new System.Drawing.Size(143, 17);
            this.cbGateway.TabIndex = 1;
            this.cbGateway.Text = "Allow changing Gateway";
            this.cbGateway.UseVisualStyleBackColor = true;
            this.cbGateway.CheckedChanged += new System.EventHandler(this.cbGateway_CheckedChanged);
            // 
            // tabFlag
            // 
            this.tabFlag.Controls.Add(this.cbFlagEnable);
            this.tabFlag.Controls.Add(this.cboFlagAdapterName);
            this.tabFlag.Controls.Add(this.label13);
            this.tabFlag.Controls.Add(this.groupBox2);
            this.tabFlag.Controls.Add(this.tbFlagInterval);
            this.tabFlag.Controls.Add(this.label7);
            this.tabFlag.Location = new System.Drawing.Point(4, 22);
            this.tabFlag.Name = "tabFlag";
            this.tabFlag.Padding = new System.Windows.Forms.Padding(3);
            this.tabFlag.Size = new System.Drawing.Size(592, 538);
            this.tabFlag.TabIndex = 0;
            this.tabFlag.Text = "FlagService Configuration";
            this.tabFlag.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btAddService);
            this.groupBox2.Controls.Add(this.listServices);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.btDelete);
            this.groupBox2.Location = new System.Drawing.Point(15, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 433);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monitered Services";
            // 
            // btAddService
            // 
            this.btAddService.Location = new System.Drawing.Point(6, 19);
            this.btAddService.Name = "btAddService";
            this.btAddService.Size = new System.Drawing.Size(153, 30);
            this.btAddService.TabIndex = 2;
            this.btAddService.Text = "Add Service";
            this.btAddService.UseVisualStyleBackColor = true;
            this.btAddService.Click += new System.EventHandler(this.btAddService_Click);
            // 
            // listServices
            // 
            this.listServices.FormattingEnabled = true;
            this.listServices.Location = new System.Drawing.Point(4, 55);
            this.listServices.Name = "listServices";
            this.listServices.Size = new System.Drawing.Size(324, 368);
            this.listServices.TabIndex = 0;
            this.listServices.SelectedIndexChanged += new System.EventHandler(this.listServices_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btdeleteValidation);
            this.groupBox1.Controls.Add(this.listValidations);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btAddValidation);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cbValidations);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Location = new System.Drawing.Point(334, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 374);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Validations";
            // 
            // btdeleteValidation
            // 
            this.btdeleteValidation.Enabled = false;
            this.btdeleteValidation.Location = new System.Drawing.Point(114, 180);
            this.btdeleteValidation.Name = "btdeleteValidation";
            this.btdeleteValidation.Size = new System.Drawing.Size(94, 30);
            this.btdeleteValidation.TabIndex = 10;
            this.btdeleteValidation.Text = "Delete";
            this.btdeleteValidation.UseVisualStyleBackColor = true;
            this.btdeleteValidation.Click += new System.EventHandler(this.btdeleteValidation_Click);
            // 
            // listValidations
            // 
            this.listValidations.FormattingEnabled = true;
            this.listValidations.Location = new System.Drawing.Point(6, 222);
            this.listValidations.Name = "listValidations";
            this.listValidations.Size = new System.Drawing.Size(200, 147);
            this.listValidations.TabIndex = 3;
            this.listValidations.SelectedIndexChanged += new System.EventHandler(this.listValidations_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Description:";
            // 
            // btAddValidation
            // 
            this.btAddValidation.Enabled = false;
            this.btAddValidation.Location = new System.Drawing.Point(6, 180);
            this.btAddValidation.Name = "btAddValidation";
            this.btAddValidation.Size = new System.Drawing.Size(102, 30);
            this.btAddValidation.TabIndex = 6;
            this.btAddValidation.Text = "Add";
            this.btAddValidation.UseVisualStyleBackColor = true;
            this.btAddValidation.Click += new System.EventHandler(this.btAddValidation_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Validation:";
            // 
            // cbValidations
            // 
            this.cbValidations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValidations.FormattingEnabled = true;
            this.cbValidations.Location = new System.Drawing.Point(6, 46);
            this.cbValidations.Name = "cbValidations";
            this.cbValidations.Size = new System.Drawing.Size(200, 21);
            this.cbValidations.TabIndex = 4;
            this.cbValidations.SelectedIndexChanged += new System.EventHandler(this.cbValidations_SelectedIndexChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDescription.Location = new System.Drawing.Point(6, 94);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(200, 82);
            this.lblDescription.TabIndex = 7;
            // 
            // btDelete
            // 
            this.btDelete.Enabled = false;
            this.btDelete.Location = new System.Drawing.Point(175, 19);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(153, 30);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "Delete Service";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // tbFlagInterval
            // 
            this.tbFlagInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFlagInterval.Location = new System.Drawing.Point(157, 40);
            this.tbFlagInterval.Name = "tbFlagInterval";
            this.tbFlagInterval.Size = new System.Drawing.Size(412, 20);
            this.tbFlagInterval.TabIndex = 36;
            this.tbFlagInterval.Text = "3000";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 18);
            this.label7.TabIndex = 35;
            this.label7.Text = "Timer Interval (Unit:ms):";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(535, 578);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 27);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(452, 578);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(77, 27);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cboFlagAdapterName
            // 
            this.cboFlagAdapterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFlagAdapterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFlagAdapterName.FormattingEnabled = true;
            this.cboFlagAdapterName.Location = new System.Drawing.Point(157, 66);
            this.cboFlagAdapterName.Name = "cboFlagAdapterName";
            this.cboFlagAdapterName.Size = new System.Drawing.Size(412, 21);
            this.cboFlagAdapterName.TabIndex = 39;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(12, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(139, 18);
            this.label13.TabIndex = 38;
            this.label13.Text = "Network Adapter Name:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbIPEnable
            // 
            this.cbIPEnable.AutoSize = true;
            this.cbIPEnable.Location = new System.Drawing.Point(15, 17);
            this.cbIPEnable.Name = "cbIPEnable";
            this.cbIPEnable.Size = new System.Drawing.Size(347, 17);
            this.cbIPEnable.TabIndex = 41;
            this.cbIPEnable.Text = "Enable IP Service ( to implement virtual IP in high available solution).";
            this.cbIPEnable.UseVisualStyleBackColor = true;
            // 
            // cbFlagEnable
            // 
            this.cbFlagEnable.AutoSize = true;
            this.cbFlagEnable.Location = new System.Drawing.Point(15, 17);
            this.cbFlagEnable.Name = "cbFlagEnable";
            this.cbFlagEnable.Size = new System.Drawing.Size(342, 17);
            this.cbFlagEnable.TabIndex = 42;
            this.cbFlagEnable.Text = "Enable Flag Service ( to monitor services in high available solution).";
            this.cbFlagEnable.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 611);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.Text = "IP Monitor Service Configuration";
            this.tabControlMain.ResumeLayout(false);
            this.tabIP.ResumeLayout(false);
            this.tabIP.PerformLayout();
            this.tabFlag.ResumeLayout(false);
            this.tabFlag.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabIP;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox cbGateway;
        private System.Windows.Forms.TextBox textBoxSubnet;
        private System.Windows.Forms.Label lsubnet;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbDNS;
        private System.Windows.Forms.TextBox textBoxGateway;
        private System.Windows.Forms.Label lGateway;
        private System.Windows.Forms.TextBox textBoxDNS;
        private System.Windows.Forms.Label lDNS;
        private System.Windows.Forms.TextBox textBoxDNSPrivate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxGatewayPrivate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSubnetPrivate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIPPrivate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBoxInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboAdapterName;
        private System.Windows.Forms.TextBox textBoxTimeout;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabFlag;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btAddValidation;
        private System.Windows.Forms.ComboBox cbValidations;
        private System.Windows.Forms.ListBox listValidations;
        private System.Windows.Forms.Button btAddService;
        private System.Windows.Forms.ListBox listServices;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btdeleteValidation;
        private System.Windows.Forms.TextBox tbFlagInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.TextBox textBoxServiceName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboFlagAdapterName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbIPEnable;
        private System.Windows.Forms.CheckBox cbFlagEnable;
    }
}