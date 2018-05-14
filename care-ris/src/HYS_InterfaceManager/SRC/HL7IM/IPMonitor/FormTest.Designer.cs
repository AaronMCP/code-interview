namespace HYS.IM.IPMonitor
{
    partial class FormTest
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
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.buttonGetAdapters = new System.Windows.Forms.Button();
            this.checkBoxWordWrap = new System.Windows.Forms.CheckBox();
            this.buttonSetIPAddress = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxSubnet = new System.Windows.Forms.TextBox();
            this.lsubnet = new System.Windows.Forms.Label();
            this.textBoxDNS = new System.Windows.Forms.TextBox();
            this.lDNS = new System.Windows.Forms.Label();
            this.textBoxGateway = new System.Windows.Forms.TextBox();
            this.lGateway = new System.Windows.Forms.Label();
            this.textBoxServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btServiceName = new System.Windows.Forms.Button();
            this.btCableStatus = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMain
            // 
            this.textBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMain.Location = new System.Drawing.Point(12, 12);
            this.textBoxMain.Multiline = true;
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMain.Size = new System.Drawing.Size(685, 240);
            this.textBoxMain.TabIndex = 0;
            this.textBoxMain.WordWrap = false;
            // 
            // buttonGetAdapters
            // 
            this.buttonGetAdapters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetAdapters.Location = new System.Drawing.Point(703, 12);
            this.buttonGetAdapters.Name = "buttonGetAdapters";
            this.buttonGetAdapters.Size = new System.Drawing.Size(122, 217);
            this.buttonGetAdapters.TabIndex = 4;
            this.buttonGetAdapters.Text = "Get All IPEnabled  Network Adapters\' Information";
            this.buttonGetAdapters.UseVisualStyleBackColor = true;
            this.buttonGetAdapters.Click += new System.EventHandler(this.buttonGetAdapters_Click);
            // 
            // checkBoxWordWrap
            // 
            this.checkBoxWordWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxWordWrap.AutoSize = true;
            this.checkBoxWordWrap.Location = new System.Drawing.Point(703, 235);
            this.checkBoxWordWrap.Name = "checkBoxWordWrap";
            this.checkBoxWordWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxWordWrap.TabIndex = 5;
            this.checkBoxWordWrap.Text = "Word Wrap";
            this.checkBoxWordWrap.UseVisualStyleBackColor = true;
            this.checkBoxWordWrap.CheckedChanged += new System.EventHandler(this.checkBoxWordWrap_CheckedChanged);
            // 
            // buttonSetIPAddress
            // 
            this.buttonSetIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetIPAddress.Location = new System.Drawing.Point(703, 267);
            this.buttonSetIPAddress.Name = "buttonSetIPAddress";
            this.buttonSetIPAddress.Size = new System.Drawing.Size(122, 128);
            this.buttonSetIPAddress.TabIndex = 6;
            this.buttonSetIPAddress.Text = "Set Network Adapter IP Address";
            this.buttonSetIPAddress.UseVisualStyleBackColor = true;
            this.buttonSetIPAddress.Click += new System.EventHandler(this.buttonSetIPAddress_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(154, 268);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(543, 20);
            this.textBoxName.TabIndex = 7;
            this.textBoxName.Text = " [00000007] Intel(R) 82579LM Gigabit Network Connection";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Network Adapter Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "IP Address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIP.Location = new System.Drawing.Point(154, 295);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(543, 20);
            this.textBoxIP.TabIndex = 10;
            this.textBoxIP.Text = "192.168.0.88";
            // 
            // textBoxSubnet
            // 
            this.textBoxSubnet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubnet.Location = new System.Drawing.Point(154, 321);
            this.textBoxSubnet.Name = "textBoxSubnet";
            this.textBoxSubnet.Size = new System.Drawing.Size(543, 20);
            this.textBoxSubnet.TabIndex = 12;
            this.textBoxSubnet.Text = "255.255.255.0";
            // 
            // lsubnet
            // 
            this.lsubnet.Location = new System.Drawing.Point(9, 320);
            this.lsubnet.Name = "lsubnet";
            this.lsubnet.Size = new System.Drawing.Size(139, 18);
            this.lsubnet.TabIndex = 11;
            this.lsubnet.Text = "Subnet Mask:";
            this.lsubnet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDNS
            // 
            this.textBoxDNS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDNS.Location = new System.Drawing.Point(154, 375);
            this.textBoxDNS.Name = "textBoxDNS";
            this.textBoxDNS.Size = new System.Drawing.Size(543, 20);
            this.textBoxDNS.TabIndex = 14;
            this.textBoxDNS.Text = "192.168.0.1";
            // 
            // lDNS
            // 
            this.lDNS.Location = new System.Drawing.Point(9, 374);
            this.lDNS.Name = "lDNS";
            this.lDNS.Size = new System.Drawing.Size(139, 18);
            this.lDNS.TabIndex = 13;
            this.lDNS.Text = "DNS Server:";
            this.lDNS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxGateway
            // 
            this.textBoxGateway.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGateway.Location = new System.Drawing.Point(154, 349);
            this.textBoxGateway.Name = "textBoxGateway";
            this.textBoxGateway.Size = new System.Drawing.Size(543, 20);
            this.textBoxGateway.TabIndex = 16;
            this.textBoxGateway.Text = "192.168.0.1";
            // 
            // lGateway
            // 
            this.lGateway.Location = new System.Drawing.Point(9, 348);
            this.lGateway.Name = "lGateway";
            this.lGateway.Size = new System.Drawing.Size(139, 18);
            this.lGateway.TabIndex = 15;
            this.lGateway.Text = "Gateway:";
            this.lGateway.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxServiceName
            // 
            this.textBoxServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServiceName.Location = new System.Drawing.Point(154, 417);
            this.textBoxServiceName.Name = "textBoxServiceName";
            this.textBoxServiceName.ReadOnly = true;
            this.textBoxServiceName.Size = new System.Drawing.Size(496, 20);
            this.textBoxServiceName.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Monitored Service Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btServiceName
            // 
            this.btServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btServiceName.Location = new System.Drawing.Point(703, 406);
            this.btServiceName.Name = "btServiceName";
            this.btServiceName.Size = new System.Drawing.Size(127, 47);
            this.btServiceName.TabIndex = 19;
            this.btServiceName.Text = "Get Service\'s status";
            this.btServiceName.UseVisualStyleBackColor = true;
            this.btServiceName.Click += new System.EventHandler(this.btServiceName_Click);
            // 
            // btCableStatus
            // 
            this.btCableStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCableStatus.Location = new System.Drawing.Point(12, 467);
            this.btCableStatus.Name = "btCableStatus";
            this.btCableStatus.Size = new System.Drawing.Size(818, 45);
            this.btCableStatus.TabIndex = 22;
            this.btCableStatus.Text = "Check if Network Adapter is Disconnected(input \"Network Adapter Name\"  above firs" +
                "t)";
            this.btCableStatus.UseVisualStyleBackColor = true;
            this.btCableStatus.Click += new System.EventHandler(this.btCableStatus_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(656, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 524);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btCableStatus);
            this.Controls.Add(this.btServiceName);
            this.Controls.Add(this.textBoxServiceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxGateway);
            this.Controls.Add(this.lGateway);
            this.Controls.Add(this.textBoxDNS);
            this.Controls.Add(this.lDNS);
            this.Controls.Add(this.textBoxSubnet);
            this.Controls.Add(this.lsubnet);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonSetIPAddress);
            this.Controls.Add(this.checkBoxWordWrap);
            this.Controls.Add(this.buttonGetAdapters);
            this.Controls.Add(this.textBoxMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTest";
            this.Text = "IP Monitor Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMain;
        private System.Windows.Forms.Button buttonGetAdapters;
        private System.Windows.Forms.CheckBox checkBoxWordWrap;
        private System.Windows.Forms.Button buttonSetIPAddress;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxSubnet;
        private System.Windows.Forms.Label lsubnet;
        private System.Windows.Forms.TextBox textBoxDNS;
        private System.Windows.Forms.Label lDNS;
        private System.Windows.Forms.TextBox textBoxGateway;
        private System.Windows.Forms.Label lGateway;
        private System.Windows.Forms.TextBox textBoxServiceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btServiceName;
        private System.Windows.Forms.Button btCableStatus;
        private System.Windows.Forms.Button button1;
    }
}