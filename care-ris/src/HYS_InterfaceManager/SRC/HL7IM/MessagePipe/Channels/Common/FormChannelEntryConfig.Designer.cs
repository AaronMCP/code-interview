namespace HYS.MessageDevices.MessagePipe.Channels.Common
{
    partial class FormChannelEntryConfig
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancl = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tBoxMsgScheme = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tBoxMsgCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmBoxCheckMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tBoxRE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tBoxXPathPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBoxXPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(250, 234);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 29);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancl
            // 
            this.buttonCancl.Location = new System.Drawing.Point(337, 234);
            this.buttonCancl.Name = "buttonCancl";
            this.buttonCancl.Size = new System.Drawing.Size(72, 29);
            this.buttonCancl.TabIndex = 7;
            this.buttonCancl.Text = "Cancel";
            this.buttonCancl.UseVisualStyleBackColor = true;
            this.buttonCancl.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(421, 228);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tBoxMsgScheme);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tBoxMsgCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmBoxCheckMode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message Type";
            // 
            // tBoxMsgScheme
            // 
            this.tBoxMsgScheme.Location = new System.Drawing.Point(296, 56);
            this.tBoxMsgScheme.Name = "tBoxMsgScheme";
            this.tBoxMsgScheme.Size = new System.Drawing.Size(92, 20);
            this.tBoxMsgScheme.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Code Scheme:";
            // 
            // tBoxMsgCode
            // 
            this.tBoxMsgCode.Location = new System.Drawing.Point(100, 56);
            this.tBoxMsgCode.Name = "tBoxMsgCode";
            this.tBoxMsgCode.Size = new System.Drawing.Size(92, 20);
            this.tBoxMsgCode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Message Code:";
            // 
            // cmBoxCheckMode
            // 
            this.cmBoxCheckMode.FormattingEnabled = true;
            this.cmBoxCheckMode.Location = new System.Drawing.Point(100, 20);
            this.cmBoxCheckMode.Name = "cmBoxCheckMode";
            this.cmBoxCheckMode.Size = new System.Drawing.Size(277, 21);
            this.cmBoxCheckMode.TabIndex = 1;
            this.cmBoxCheckMode.SelectedIndexChanged += new System.EventHandler(this.cmBoxCheckMode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Check Mode:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBoxRE);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tBoxXPathPrefix);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tBoxXPath);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 133);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XPath Configuration";
            // 
            // tBoxRE
            // 
            this.tBoxRE.Location = new System.Drawing.Point(130, 92);
            this.tBoxRE.Name = "tBoxRE";
            this.tBoxRE.Size = new System.Drawing.Size(285, 20);
            this.tBoxRE.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Regular Expression:";
            // 
            // tBoxXPathPrefix
            // 
            this.tBoxXPathPrefix.Location = new System.Drawing.Point(130, 56);
            this.tBoxXPathPrefix.Name = "tBoxXPathPrefix";
            this.tBoxXPathPrefix.Size = new System.Drawing.Size(285, 20);
            this.tBoxXPathPrefix.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "XPath Prefix:";
            // 
            // tBoxXPath
            // 
            this.tBoxXPath.Location = new System.Drawing.Point(130, 20);
            this.tBoxXPath.Name = "tBoxXPath";
            this.tBoxXPath.Size = new System.Drawing.Size(285, 20);
            this.tBoxXPath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "XPath:";
            // 
            // FormChannelEntryConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 275);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonCancl);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormChannelEntryConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Channel Entry Criteria";
            this.Load += new System.EventHandler(this.FormChannelEntryConfig_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBoxMsgCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmBoxCheckMode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tBoxRE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tBoxXPathPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBoxXPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBoxMsgScheme;
        private System.Windows.Forms.Label label6;
    }
}