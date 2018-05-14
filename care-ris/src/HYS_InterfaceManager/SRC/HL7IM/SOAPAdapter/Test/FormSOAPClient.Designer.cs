namespace HYS.MessageDevices.SOAPAdapter.Test.Forms
{
    partial class FormSOAPClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSOAPClient));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxValueSnd = new System.Windows.Forms.TextBox();
            this.textBoxValueRcv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonValueCall = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMsgSnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMsgRcv = new System.Windows.Forms.TextBox();
            this.buttonMsgCall = new System.Windows.Forms.Button();
            this.textBoxMsgURI = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxMsgAction = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxMsgSndWrap = new System.Windows.Forms.CheckBox();
            this.checkBoxMsgRcvWrap = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Send Value:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueSnd
            // 
            this.textBoxValueSnd.Location = new System.Drawing.Point(94, 17);
            this.textBoxValueSnd.Name = "textBoxValueSnd";
            this.textBoxValueSnd.Size = new System.Drawing.Size(390, 20);
            this.textBoxValueSnd.TabIndex = 1;
            this.textBoxValueSnd.Text = "test";
            // 
            // textBoxValueRcv
            // 
            this.textBoxValueRcv.Location = new System.Drawing.Point(94, 43);
            this.textBoxValueRcv.Name = "textBoxValueRcv";
            this.textBoxValueRcv.Size = new System.Drawing.Size(390, 20);
            this.textBoxValueRcv.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Receive Value:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonValueCall
            // 
            this.buttonValueCall.Location = new System.Drawing.Point(502, 17);
            this.buttonValueCall.Name = "buttonValueCall";
            this.buttonValueCall.Size = new System.Drawing.Size(117, 46);
            this.buttonValueCall.TabIndex = 4;
            this.buttonValueCall.Text = "Call Web Service";
            this.buttonValueCall.UseVisualStyleBackColor = true;
            this.buttonValueCall.Click += new System.EventHandler(this.buttonValueCall_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Send Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsgSnd
            // 
            this.textBoxMsgSnd.Location = new System.Drawing.Point(17, 172);
            this.textBoxMsgSnd.Multiline = true;
            this.textBoxMsgSnd.Name = "textBoxMsgSnd";
            this.textBoxMsgSnd.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMsgSnd.Size = new System.Drawing.Size(602, 125);
            this.textBoxMsgSnd.TabIndex = 6;
            this.textBoxMsgSnd.Text = resources.GetString("textBoxMsgSnd.Text");
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Receive Message:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsgRcv
            // 
            this.textBoxMsgRcv.Location = new System.Drawing.Point(17, 331);
            this.textBoxMsgRcv.Multiline = true;
            this.textBoxMsgRcv.Name = "textBoxMsgRcv";
            this.textBoxMsgRcv.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMsgRcv.Size = new System.Drawing.Size(602, 125);
            this.textBoxMsgRcv.TabIndex = 8;
            // 
            // buttonMsgCall
            // 
            this.buttonMsgCall.Location = new System.Drawing.Point(502, 95);
            this.buttonMsgCall.Name = "buttonMsgCall";
            this.buttonMsgCall.Size = new System.Drawing.Size(117, 46);
            this.buttonMsgCall.TabIndex = 9;
            this.buttonMsgCall.Text = "Call Web Service";
            this.buttonMsgCall.UseVisualStyleBackColor = true;
            this.buttonMsgCall.Click += new System.EventHandler(this.buttonMsgCall_Click);
            // 
            // textBoxMsgURI
            // 
            this.textBoxMsgURI.Location = new System.Drawing.Point(94, 95);
            this.textBoxMsgURI.Name = "textBoxMsgURI";
            this.textBoxMsgURI.Size = new System.Drawing.Size(390, 20);
            this.textBoxMsgURI.TabIndex = 11;
            this.textBoxMsgURI.Text = "http://cnshw6zmrs1x/RHISWS/PIXService.asmx";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "URI:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(14, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 2);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // textBoxMsgAction
            // 
            this.textBoxMsgAction.Location = new System.Drawing.Point(94, 121);
            this.textBoxMsgAction.Name = "textBoxMsgAction";
            this.textBoxMsgAction.Size = new System.Drawing.Size(390, 20);
            this.textBoxMsgAction.TabIndex = 14;
            this.textBoxMsgAction.Text = "http://www.carestreamhealth.com/MessageCom";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "SOAP Action:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxMsgSndWrap
            // 
            this.checkBoxMsgSndWrap.AutoSize = true;
            this.checkBoxMsgSndWrap.Checked = true;
            this.checkBoxMsgSndWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMsgSndWrap.Location = new System.Drawing.Point(538, 152);
            this.checkBoxMsgSndWrap.Name = "checkBoxMsgSndWrap";
            this.checkBoxMsgSndWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxMsgSndWrap.TabIndex = 15;
            this.checkBoxMsgSndWrap.Text = "Word Wrap";
            this.checkBoxMsgSndWrap.UseVisualStyleBackColor = true;
            this.checkBoxMsgSndWrap.CheckedChanged += new System.EventHandler(this.checkBoxMsgSndWrap_CheckedChanged);
            // 
            // checkBoxMsgRcvWrap
            // 
            this.checkBoxMsgRcvWrap.AutoSize = true;
            this.checkBoxMsgRcvWrap.Checked = true;
            this.checkBoxMsgRcvWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMsgRcvWrap.Location = new System.Drawing.Point(538, 311);
            this.checkBoxMsgRcvWrap.Name = "checkBoxMsgRcvWrap";
            this.checkBoxMsgRcvWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxMsgRcvWrap.TabIndex = 16;
            this.checkBoxMsgRcvWrap.Text = "Word Wrap";
            this.checkBoxMsgRcvWrap.UseVisualStyleBackColor = true;
            this.checkBoxMsgRcvWrap.CheckedChanged += new System.EventHandler(this.checkBoxMsgRcvWrap_CheckedChanged);
            // 
            // FormSOAPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 475);
            this.Controls.Add(this.checkBoxMsgRcvWrap);
            this.Controls.Add(this.checkBoxMsgSndWrap);
            this.Controls.Add(this.textBoxMsgAction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxMsgURI);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonMsgCall);
            this.Controls.Add(this.textBoxMsgRcv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMsgSnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonValueCall);
            this.Controls.Add(this.textBoxValueRcv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxValueSnd);
            this.Controls.Add(this.label1);
            this.Name = "FormSOAPClient";
            this.Text = "SOAP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxValueSnd;
        private System.Windows.Forms.TextBox textBoxValueRcv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonValueCall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMsgSnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMsgRcv;
        private System.Windows.Forms.Button buttonMsgCall;
        private System.Windows.Forms.TextBox textBoxMsgURI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxMsgAction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxMsgSndWrap;
        private System.Windows.Forms.CheckBox checkBoxMsgRcvWrap;
    }
}

