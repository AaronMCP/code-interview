namespace XmlTest
{
    partial class FormXIS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXIS));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSendWithHeader = new System.Windows.Forms.Button();
            this.buttonSendMore = new System.Windows.Forms.Button();
            this.buttonClientSend = new System.Windows.Forms.Button();
            this.textBoxClientReceive = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxClientSend = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownClientPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxServerSend = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxServerReceive = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonServerStop = new System.Windows.Forms.Button();
            this.buttonServerStart = new System.Windows.Forms.Button();
            this.numericUpDownServerPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxWorker = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClientPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSendWithHeader);
            this.groupBox1.Controls.Add(this.buttonSendMore);
            this.groupBox1.Controls.Add(this.buttonClientSend);
            this.groupBox1.Controls.Add(this.textBoxClientReceive);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxClientSend);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownClientPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 449);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client";
            // 
            // buttonSendWithHeader
            // 
            this.buttonSendWithHeader.Location = new System.Drawing.Point(110, 59);
            this.buttonSendWithHeader.Name = "buttonSendWithHeader";
            this.buttonSendWithHeader.Size = new System.Drawing.Size(120, 21);
            this.buttonSendWithHeader.TabIndex = 7;
            this.buttonSendWithHeader.Text = "Send with header";
            this.buttonSendWithHeader.UseVisualStyleBackColor = true;
            this.buttonSendWithHeader.Click += new System.EventHandler(this.buttonSendWithHeader_Click);
            // 
            // buttonSendMore
            // 
            this.buttonSendMore.Location = new System.Drawing.Point(236, 57);
            this.buttonSendMore.Name = "buttonSendMore";
            this.buttonSendMore.Size = new System.Drawing.Size(76, 21);
            this.buttonSendMore.TabIndex = 7;
            this.buttonSendMore.Text = "Send More";
            this.buttonSendMore.UseVisualStyleBackColor = true;
            this.buttonSendMore.Click += new System.EventHandler(this.buttonSendMore_Click);
            // 
            // buttonClientSend
            // 
            this.buttonClientSend.Location = new System.Drawing.Point(236, 30);
            this.buttonClientSend.Name = "buttonClientSend";
            this.buttonClientSend.Size = new System.Drawing.Size(76, 21);
            this.buttonClientSend.TabIndex = 6;
            this.buttonClientSend.Text = "Send";
            this.buttonClientSend.UseVisualStyleBackColor = true;
            this.buttonClientSend.Click += new System.EventHandler(this.buttonInboundSend_Click);
            // 
            // textBoxClientReceive
            // 
            this.textBoxClientReceive.Location = new System.Drawing.Point(21, 273);
            this.textBoxClientReceive.Multiline = true;
            this.textBoxClientReceive.Name = "textBoxClientReceive";
            this.textBoxClientReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxClientReceive.Size = new System.Drawing.Size(291, 154);
            this.textBoxClientReceive.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Receive Data:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxClientSend
            // 
            this.textBoxClientSend.Location = new System.Drawing.Point(22, 86);
            this.textBoxClientSend.Multiline = true;
            this.textBoxClientSend.Name = "textBoxClientSend";
            this.textBoxClientSend.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxClientSend.Size = new System.Drawing.Size(291, 154);
            this.textBoxClientSend.TabIndex = 3;
            this.textBoxClientSend.Text = resources.GetString("textBoxClientSend.Text");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Send Data:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownClientPort
            // 
            this.numericUpDownClientPort.Location = new System.Drawing.Point(110, 30);
            this.numericUpDownClientPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownClientPort.Name = "numericUpDownClientPort";
            this.numericUpDownClientPort.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownClientPort.TabIndex = 1;
            this.numericUpDownClientPort.Value = new decimal(new int[] {
            3200,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxServerSend);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxServerReceive);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonServerStop);
            this.groupBox2.Controls.Add(this.buttonServerStart);
            this.groupBox2.Controls.Add(this.numericUpDownServerPort);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(365, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 449);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // textBoxServerSend
            // 
            this.textBoxServerSend.Location = new System.Drawing.Point(26, 273);
            this.textBoxServerSend.Multiline = true;
            this.textBoxServerSend.Name = "textBoxServerSend";
            this.textBoxServerSend.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxServerSend.Size = new System.Drawing.Size(291, 154);
            this.textBoxServerSend.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(23, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Send Data:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxServerReceive
            // 
            this.textBoxServerReceive.Location = new System.Drawing.Point(26, 86);
            this.textBoxServerReceive.Multiline = true;
            this.textBoxServerReceive.Name = "textBoxServerReceive";
            this.textBoxServerReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxServerReceive.Size = new System.Drawing.Size(291, 154);
            this.textBoxServerReceive.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(23, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Receive Data:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonServerStop
            // 
            this.buttonServerStop.Location = new System.Drawing.Point(269, 32);
            this.buttonServerStop.Name = "buttonServerStop";
            this.buttonServerStop.Size = new System.Drawing.Size(49, 21);
            this.buttonServerStop.TabIndex = 8;
            this.buttonServerStop.Text = "Stop";
            this.buttonServerStop.UseVisualStyleBackColor = true;
            this.buttonServerStop.Click += new System.EventHandler(this.buttonServerStop_Click);
            // 
            // buttonServerStart
            // 
            this.buttonServerStart.Location = new System.Drawing.Point(214, 32);
            this.buttonServerStart.Name = "buttonServerStart";
            this.buttonServerStart.Size = new System.Drawing.Size(49, 21);
            this.buttonServerStart.TabIndex = 7;
            this.buttonServerStart.Text = "Start";
            this.buttonServerStart.UseVisualStyleBackColor = true;
            this.buttonServerStart.Click += new System.EventHandler(this.buttonServerStart_Click);
            // 
            // numericUpDownServerPort
            // 
            this.numericUpDownServerPort.Location = new System.Drawing.Point(114, 32);
            this.numericUpDownServerPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownServerPort.Name = "numericUpDownServerPort";
            this.numericUpDownServerPort.Size = new System.Drawing.Size(83, 20);
            this.numericUpDownServerPort.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Listen on Port:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(21, 46);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(428, 95);
            this.listBoxLog.TabIndex = 2;
            this.listBoxLog.DoubleClick += new System.EventHandler(this.listBoxLog_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxWorker);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.buttonClear);
            this.groupBox3.Controls.Add(this.listBoxLog);
            this.groupBox3.Location = new System.Drawing.Point(19, 475);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(683, 155);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // listBoxWorker
            // 
            this.listBoxWorker.FormattingEnabled = true;
            this.listBoxWorker.Location = new System.Drawing.Point(460, 46);
            this.listBoxWorker.Name = "listBoxWorker";
            this.listBoxWorker.Size = new System.Drawing.Size(204, 95);
            this.listBoxWorker.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(18, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Exception List";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(457, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Worker List";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(346, 19);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(103, 21);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // FormXIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 642);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormXIS";
            this.Text = "FormXIS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormXIS_FormClosing);
            this.Load += new System.EventHandler(this.FormXIS_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClientPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownClientPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxClientReceive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxClientSend;
        private System.Windows.Forms.Button buttonClientSend;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownServerPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxServerSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxServerReceive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonServerStop;
        private System.Windows.Forms.Button buttonServerStart;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBoxWorker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonSendMore;
        private System.Windows.Forms.Button buttonSendWithHeader;
    }
}