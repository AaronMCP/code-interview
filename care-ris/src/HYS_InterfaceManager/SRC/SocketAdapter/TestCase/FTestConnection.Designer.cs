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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTestConnection));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.cbsCodePage = new System.Windows.Forms.ComboBox();
            this.tbListenIP = new System.Windows.Forms.ComboBox();
            this.tbListentPort = new System.Windows.Forms.TextBox();
            this.tbsPackage4 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbsPackage3 = new System.Windows.Forms.TextBox();
            this.tbsPackage2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbsPackage1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbsClear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.tbcPackage1 = new System.Windows.Forms.TextBox();
            this.cbcCodePage = new System.Windows.Forms.ComboBox();
            this.tbServerIP = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbcPackage4 = new System.Windows.Forms.TextBox();
            this.tbcPackage3 = new System.Windows.Forms.TextBox();
            this.tbcPackage2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bSend4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbcClear = new System.Windows.Forms.Button();
            this.bSend1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.cbsCodePage);
            this.panel1.Controls.Add(this.tbListenIP);
            this.panel1.Controls.Add(this.tbListentPort);
            this.panel1.Controls.Add(this.tbsPackage4);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tbsPackage3);
            this.panel1.Controls.Add(this.tbsPackage2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbsPackage1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.tbsClear);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(29, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 489);
            this.panel1.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 45;
            this.label16.Text = "CodePage";
            // 
            // cbsCodePage
            // 
            this.cbsCodePage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbsCodePage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbsCodePage.FormattingEnabled = true;
            this.cbsCodePage.Location = new System.Drawing.Point(72, -1);
            this.cbsCodePage.Name = "cbsCodePage";
            this.cbsCodePage.Size = new System.Drawing.Size(196, 21);
            this.cbsCodePage.TabIndex = 44;
            this.cbsCodePage.SelectedIndexChanged += new System.EventHandler(this.cbcCodePage_SelectedIndexChanged);
            // 
            // tbListenIP
            // 
            this.tbListenIP.FormattingEnabled = true;
            this.tbListenIP.Items.AddRange(new object[] {
            "127.0.0.1",
            "150.245.176.146",
            "150.245.176.148",
            "150.245.176.56"});
            this.tbListenIP.Location = new System.Drawing.Point(72, 32);
            this.tbListenIP.Name = "tbListenIP";
            this.tbListenIP.Size = new System.Drawing.Size(129, 21);
            this.tbListenIP.TabIndex = 12;
            this.tbListenIP.Text = "127.0.0.1";
            // 
            // tbListentPort
            // 
            this.tbListentPort.Location = new System.Drawing.Point(283, 33);
            this.tbListentPort.Name = "tbListentPort";
            this.tbListentPort.Size = new System.Drawing.Size(50, 20);
            this.tbListentPort.TabIndex = 8;
            this.tbListentPort.Text = "7000";
            // 
            // tbsPackage4
            // 
            this.tbsPackage4.Location = new System.Drawing.Point(3, 425);
            this.tbsPackage4.Multiline = true;
            this.tbsPackage4.Name = "tbsPackage4";
            this.tbsPackage4.Size = new System.Drawing.Size(411, 56);
            this.tbsPackage4.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(277, 348);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Package3";
            // 
            // tbsPackage3
            // 
            this.tbsPackage3.Location = new System.Drawing.Point(7, 363);
            this.tbsPackage3.Multiline = true;
            this.tbsPackage3.Name = "tbsPackage3";
            this.tbsPackage3.Size = new System.Drawing.Size(407, 44);
            this.tbsPackage3.TabIndex = 6;
            this.tbsPackage3.Text = "><<PacketType=3%Source=150.245.178.95|7000%Destination=150.245.178.95|6000><Comma" +
                "nd come from=150.245.178.95|6000><CommandGUID=60bc2f4d25b1485eba40d638951ce1ff%R" +
                "esult=5>";
            // 
            // tbsPackage2
            // 
            this.tbsPackage2.Location = new System.Drawing.Point(7, 287);
            this.tbsPackage2.Multiline = true;
            this.tbsPackage2.Name = "tbsPackage2";
            this.tbsPackage2.Size = new System.Drawing.Size(407, 58);
            this.tbsPackage2.TabIndex = 6;
            this.tbsPackage2.Text = "><<PacketType=3%Source=150.245.178.95|7000%Destination=150.245.178.95|6000><Comma" +
                "ndGUID=edadafe2d4054a758b52d638951ce1ff%Send result=1>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(291, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Server";
            // 
            // tbsPackage1
            // 
            this.tbsPackage1.Location = new System.Drawing.Point(7, 111);
            this.tbsPackage1.Multiline = true;
            this.tbsPackage1.Name = "tbsPackage1";
            this.tbsPackage1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbsPackage1.Size = new System.Drawing.Size(407, 140);
            this.tbsPackage1.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(277, 410);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Package4";
            // 
            // tbsClear
            // 
            this.tbsClear.Location = new System.Drawing.Point(94, 76);
            this.tbsClear.Name = "tbsClear";
            this.tbsClear.Size = new System.Drawing.Size(75, 23);
            this.tbsClear.TabIndex = 1;
            this.tbsClear.Text = "Clear";
            this.tbsClear.UseVisualStyleBackColor = true;
            this.tbsClear.Click += new System.EventHandler(this.tbsClear_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "ListenIP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(277, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Package2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(223, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "ListenPort";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(277, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Package1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Package1";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.tbcPackage1);
            this.panel2.Controls.Add(this.cbcCodePage);
            this.panel2.Controls.Add(this.tbServerIP);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.tbServerPort);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.tbcPackage4);
            this.panel2.Controls.Add(this.tbcPackage3);
            this.panel2.Controls.Add(this.tbcPackage2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.bSend4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbcClear);
            this.panel2.Controls.Add(this.bSend1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(454, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 489);
            this.panel2.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "CodePage";
            // 
            // tbcPackage1
            // 
            this.tbcPackage1.Location = new System.Drawing.Point(3, 111);
            this.tbcPackage1.Multiline = true;
            this.tbcPackage1.Name = "tbcPackage1";
            this.tbcPackage1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbcPackage1.Size = new System.Drawing.Size(407, 140);
            this.tbcPackage1.TabIndex = 12;
            this.tbcPackage1.Text = resources.GetString("tbcPackage1.Text");
            // 
            // cbcCodePage
            // 
            this.cbcCodePage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbcCodePage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbcCodePage.FormattingEnabled = true;
            this.cbcCodePage.Location = new System.Drawing.Point(67, 3);
            this.cbcCodePage.Name = "cbcCodePage";
            this.cbcCodePage.Size = new System.Drawing.Size(196, 21);
            this.cbcCodePage.TabIndex = 42;
            this.cbcCodePage.SelectedIndexChanged += new System.EventHandler(this.cbcCodePage_SelectedIndexChanged);
            // 
            // tbServerIP
            // 
            this.tbServerIP.FormattingEnabled = true;
            this.tbServerIP.Items.AddRange(new object[] {
            "127.0.0.1",
            "150.245.176.146",
            "150.245.176.148",
            "150.245.176.56"});
            this.tbServerIP.Location = new System.Drawing.Point(58, 30);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(99, 21);
            this.tbServerIP.TabIndex = 11;
            this.tbServerIP.Text = "127.0.0.1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "ServerIP";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(268, 29);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(57, 20);
            this.tbServerPort.TabIndex = 10;
            this.tbServerPort.Text = "6000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(163, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "ServerPort";
            // 
            // tbcPackage4
            // 
            this.tbcPackage4.Location = new System.Drawing.Point(-1, 425);
            this.tbcPackage4.Multiline = true;
            this.tbcPackage4.Name = "tbcPackage4";
            this.tbcPackage4.Size = new System.Drawing.Size(403, 56);
            this.tbcPackage4.TabIndex = 6;
            this.tbcPackage4.Text = "<><PacketType=4%Source=127.0.0.1|3000%Destination=127.0.0.1|6000><CommandGUID=F75" +
                "2286D-E139-4A67-ADEB-80D2775A4BC4%From=127.0.0.1|3000>";
            // 
            // tbcPackage3
            // 
            this.tbcPackage3.Location = new System.Drawing.Point(3, 363);
            this.tbcPackage3.Multiline = true;
            this.tbcPackage3.Name = "tbcPackage3";
            this.tbcPackage3.Size = new System.Drawing.Size(403, 44);
            this.tbcPackage3.TabIndex = 6;
            // 
            // tbcPackage2
            // 
            this.tbcPackage2.Location = new System.Drawing.Point(3, 287);
            this.tbcPackage2.Multiline = true;
            this.tbcPackage2.Name = "tbcPackage2";
            this.tbcPackage2.Size = new System.Drawing.Size(403, 58);
            this.tbcPackage2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Package3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(269, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Package4";
            // 
            // bSend4
            // 
            this.bSend4.Location = new System.Drawing.Point(209, 76);
            this.bSend4.Name = "bSend4";
            this.bSend4.Size = new System.Drawing.Size(54, 23);
            this.bSend4.TabIndex = 5;
            this.bSend4.Text = "Send4";
            this.bSend4.UseVisualStyleBackColor = true;
            this.bSend4.Click += new System.EventHandler(this.bSend4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Package2";
            // 
            // tbcClear
            // 
            this.tbcClear.Location = new System.Drawing.Point(85, 76);
            this.tbcClear.Name = "tbcClear";
            this.tbcClear.Size = new System.Drawing.Size(57, 23);
            this.tbcClear.TabIndex = 5;
            this.tbcClear.Text = "Clear";
            this.tbcClear.UseVisualStyleBackColor = true;
            this.tbcClear.Click += new System.EventHandler(this.tbcClear_Click);
            // 
            // bSend1
            // 
            this.bSend1.Location = new System.Drawing.Point(148, 76);
            this.bSend1.Name = "bSend1";
            this.bSend1.Size = new System.Drawing.Size(57, 23);
            this.bSend1.TabIndex = 5;
            this.bSend1.Text = "Send1";
            this.bSend1.UseVisualStyleBackColor = true;
            this.bSend1.Click += new System.EventHandler(this.Send1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(282, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Client";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 23);
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
            // FTestConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 527);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bSend4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbcPackage4;
        private System.Windows.Forms.TextBox tbcPackage3;
        private System.Windows.Forms.TextBox tbcPackage2;
        private System.Windows.Forms.TextBox tbsPackage4;
        private System.Windows.Forms.TextBox tbsPackage3;
        private System.Windows.Forms.TextBox tbsPackage2;
        private System.Windows.Forms.TextBox tbsPackage1;
        private System.Windows.Forms.Button tbsClear;
        private System.Windows.Forms.Button tbcClear;
        private System.Windows.Forms.TextBox tbListentPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox tbServerIP;
        private System.Windows.Forms.ComboBox tbListenIP;
        private System.Windows.Forms.TextBox tbcPackage1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbcCodePage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbsCodePage;
    }
}