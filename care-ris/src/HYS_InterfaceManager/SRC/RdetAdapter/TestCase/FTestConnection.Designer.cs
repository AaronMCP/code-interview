namespace TestCase
{
    partial class FTestConnection
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.slbRespNewPatient = new System.Windows.Forms.ListBox();
            this.slbNewPatient = new System.Windows.Forms.ListBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbsPackage4 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbsPackage3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbsClear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.clbRespNewPatient = new System.Windows.Forms.ListBox();
            this.clbNewPatient = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tbcClear = new System.Windows.Forms.Button();
            this.bSend1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rbNewPatient = new System.Windows.Forms.RadioButton();
            this.rbGetScannerStatus = new System.Windows.Forms.RadioButton();
            this.rbGetLocale = new System.Windows.Forms.RadioButton();
            this.rbGetBodyParts = new System.Windows.Forms.RadioButton();
            this.rbGetProjections = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.slbRespNewPatient);
            this.panel1.Controls.Add(this.slbNewPatient);
            this.panel1.Controls.Add(this.tbPort);
            this.panel1.Controls.Add(this.tbsPackage4);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tbsPackage3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.tbsClear);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(29, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 361);
            this.panel1.TabIndex = 0;
            // 
            // slbRespNewPatient
            // 
            this.slbRespNewPatient.FormattingEnabled = true;
            this.slbRespNewPatient.Items.AddRange(new object[] {
            "ErrorCode=0"});
            this.slbRespNewPatient.Location = new System.Drawing.Point(4, 163);
            this.slbRespNewPatient.Name = "slbRespNewPatient";
            this.slbRespNewPatient.Size = new System.Drawing.Size(322, 56);
            this.slbRespNewPatient.TabIndex = 10;
            // 
            // slbNewPatient
            // 
            this.slbNewPatient.FormattingEnabled = true;
            this.slbNewPatient.Location = new System.Drawing.Point(4, 60);
            this.slbNewPatient.Name = "slbNewPatient";
            this.slbNewPatient.Size = new System.Drawing.Size(322, 82);
            this.slbNewPatient.TabIndex = 9;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(223, 22);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(41, 20);
            this.tbPort.TabIndex = 8;
            this.tbPort.Text = "58431";
            // 
            // tbsPackage4
            // 
            this.tbsPackage4.Location = new System.Drawing.Point(-1, 300);
            this.tbsPackage4.Multiline = true;
            this.tbsPackage4.Name = "tbsPackage4";
            this.tbsPackage4.Size = new System.Drawing.Size(330, 56);
            this.tbsPackage4.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(273, 223);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Package3";
            // 
            // tbsPackage3
            // 
            this.tbsPackage3.Location = new System.Drawing.Point(3, 238);
            this.tbsPackage3.Multiline = true;
            this.tbsPackage3.Name = "tbsPackage3";
            this.tbsPackage3.Size = new System.Drawing.Size(326, 44);
            this.tbsPackage3.TabIndex = 6;
            this.tbsPackage3.Text = "><<PacketType=3%Source=150.245.178.95|7000%Destination=150.245.178.95|6000><Comma" +
                "nd come from=150.245.178.95|6000><CommandGUID=60bc2f4d25b1485eba40d638951ce1ff%R" +
                "esult=5>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(291, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Server";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(273, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Package4";
            // 
            // tbsClear
            // 
            this.tbsClear.Location = new System.Drawing.Point(87, 23);
            this.tbsClear.Name = "tbsClear";
            this.tbsClear.Size = new System.Drawing.Size(75, 23);
            this.tbsClear.TabIndex = 1;
            this.tbsClear.Text = "Clear";
            this.tbsClear.UseVisualStyleBackColor = true;
            this.tbsClear.Click += new System.EventHandler(this.tbsClear_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(273, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Package2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(191, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(273, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Package1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Send";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbGetProjections);
            this.panel2.Controls.Add(this.rbGetBodyParts);
            this.panel2.Controls.Add(this.rbGetLocale);
            this.panel2.Controls.Add(this.rbGetScannerStatus);
            this.panel2.Controls.Add(this.rbNewPatient);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.tbServerIP);
            this.panel2.Controls.Add(this.tbServerPort);
            this.panel2.Controls.Add(this.clbRespNewPatient);
            this.panel2.Controls.Add(this.clbNewPatient);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.tbcClear);
            this.panel2.Controls.Add(this.bSend1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(385, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(416, 365);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Receive";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(150, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "ServerIP";
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(57, 3);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(87, 20);
            this.tbServerIP.TabIndex = 13;
            this.tbServerIP.Text = "172.16.3.218";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(182, 5);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(41, 20);
            this.tbServerPort.TabIndex = 12;
            this.tbServerPort.Text = "58431";
            // 
            // clbRespNewPatient
            // 
            this.clbRespNewPatient.FormattingEnabled = true;
            this.clbRespNewPatient.Location = new System.Drawing.Point(3, 266);
            this.clbRespNewPatient.Name = "clbRespNewPatient";
            this.clbRespNewPatient.Size = new System.Drawing.Size(383, 69);
            this.clbRespNewPatient.TabIndex = 11;
            // 
            // clbNewPatient
            // 
            this.clbNewPatient.FormattingEnabled = true;
            this.clbNewPatient.Items.AddRange(new object[] {
            "Command=GetScannerStauts"});
            this.clbNewPatient.Location = new System.Drawing.Point(1, 166);
            this.clbNewPatient.Name = "clbNewPatient";
            this.clbNewPatient.Size = new System.Drawing.Size(383, 82);
            this.clbNewPatient.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(216, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(57, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // tbcClear
            // 
            this.tbcClear.Location = new System.Drawing.Point(153, 32);
            this.tbcClear.Name = "tbcClear";
            this.tbcClear.Size = new System.Drawing.Size(57, 23);
            this.tbcClear.TabIndex = 5;
            this.tbcClear.Text = "Clear";
            this.tbcClear.UseVisualStyleBackColor = true;
            this.tbcClear.Click += new System.EventHandler(this.tbcClear_Click);
            // 
            // bSend1
            // 
            this.bSend1.Location = new System.Drawing.Point(72, 32);
            this.bSend1.Name = "bSend1";
            this.bSend1.Size = new System.Drawing.Size(73, 23);
            this.bSend1.TabIndex = 5;
            this.bSend1.Text = "SendCommand";
            this.bSend1.UseVisualStyleBackColor = true;
            this.bSend1.Click += new System.EventHandler(this.Send1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(339, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Client";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rbNewPatient
            // 
            this.rbNewPatient.AutoSize = true;
            this.rbNewPatient.Location = new System.Drawing.Point(21, 64);
            this.rbNewPatient.Name = "rbNewPatient";
            this.rbNewPatient.Size = new System.Drawing.Size(80, 17);
            this.rbNewPatient.TabIndex = 17;
            this.rbNewPatient.Text = "NewPatient";
            this.rbNewPatient.UseVisualStyleBackColor = true;
            // 
            // rbGetScannerStatus
            // 
            this.rbGetScannerStatus.AutoSize = true;
            this.rbGetScannerStatus.Checked = true;
            this.rbGetScannerStatus.Location = new System.Drawing.Point(21, 87);
            this.rbGetScannerStatus.Name = "rbGetScannerStatus";
            this.rbGetScannerStatus.Size = new System.Drawing.Size(112, 17);
            this.rbGetScannerStatus.TabIndex = 17;
            this.rbGetScannerStatus.TabStop = true;
            this.rbGetScannerStatus.Text = "GetScannerStatus";
            this.rbGetScannerStatus.UseVisualStyleBackColor = true;
            // 
            // rbGetLocale
            // 
            this.rbGetLocale.AutoSize = true;
            this.rbGetLocale.Location = new System.Drawing.Point(21, 110);
            this.rbGetLocale.Name = "rbGetLocale";
            this.rbGetLocale.Size = new System.Drawing.Size(74, 17);
            this.rbGetLocale.TabIndex = 17;
            this.rbGetLocale.Text = "GetLocale";
            this.rbGetLocale.UseVisualStyleBackColor = true;
            // 
            // rbGetBodyParts
            // 
            this.rbGetBodyParts.AutoSize = true;
            this.rbGetBodyParts.Location = new System.Drawing.Point(153, 75);
            this.rbGetBodyParts.Name = "rbGetBodyParts";
            this.rbGetBodyParts.Size = new System.Drawing.Size(90, 17);
            this.rbGetBodyParts.TabIndex = 17;
            this.rbGetBodyParts.Text = "GetBodyParts";
            this.rbGetBodyParts.UseVisualStyleBackColor = true;
            // 
            // rbGetProjections
            // 
            this.rbGetProjections.AutoSize = true;
            this.rbGetProjections.Location = new System.Drawing.Point(153, 98);
            this.rbGetProjections.Name = "rbGetProjections";
            this.rbGetProjections.Size = new System.Drawing.Size(94, 17);
            this.rbGetProjections.TabIndex = 17;
            this.rbGetProjections.Text = "GetProjections";
            this.rbGetProjections.UseVisualStyleBackColor = true;
            // 
            // FTestConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 380);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FTestConnection";
            this.Text = "FTestConnection";
            this.Load += new System.EventHandler(this.FTestConnection_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button bSend1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbsPackage4;
        private System.Windows.Forms.TextBox tbsPackage3;
        private System.Windows.Forms.Button tbsClear;
        private System.Windows.Forms.Button tbcClear;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox clbNewPatient;
        private System.Windows.Forms.ListBox slbNewPatient;
        private System.Windows.Forms.ListBox slbRespNewPatient;
        private System.Windows.Forms.ListBox clbRespNewPatient;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton rbGetLocale;
        private System.Windows.Forms.RadioButton rbGetScannerStatus;
        private System.Windows.Forms.RadioButton rbNewPatient;
        private System.Windows.Forms.RadioButton rbGetBodyParts;
        private System.Windows.Forms.RadioButton rbGetProjections;
    }
}