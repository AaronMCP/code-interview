namespace HYS.MessageDevices.SOAPAdapter.Test
{
    partial class FormSOAPServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSOAPServer));
            this.textBoxSrvURI = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSrvAction = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.checkBoxMsgRcvWrap = new System.Windows.Forms.CheckBox();
            this.checkBoxMsgSndWrap = new System.Windows.Forms.CheckBox();
            this.textBoxMsgRcv = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMsgSnd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxSrvURI
            // 
            this.textBoxSrvURI.Location = new System.Drawing.Point(116, 22);
            this.textBoxSrvURI.Name = "textBoxSrvURI";
            this.textBoxSrvURI.Size = new System.Drawing.Size(372, 20);
            this.textBoxSrvURI.TabIndex = 13;
            this.textBoxSrvURI.Text = "http://localhost:8080/RHISWS/PIXService";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "URI:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSrvAction
            // 
            this.textBoxSrvAction.Location = new System.Drawing.Point(116, 48);
            this.textBoxSrvAction.Name = "textBoxSrvAction";
            this.textBoxSrvAction.ReadOnly = true;
            this.textBoxSrvAction.Size = new System.Drawing.Size(372, 20);
            this.textBoxSrvAction.TabIndex = 16;
            this.textBoxSrvAction.Text = "Do not support filtering by SOAP Action currently.";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(18, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 19);
            this.label6.TabIndex = 15;
            this.label6.Text = "SOAP Action:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(506, 22);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(53, 46);
            this.buttonStart.TabIndex = 17;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(565, 22);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(53, 46);
            this.buttonStop.TabIndex = 18;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // checkBoxMsgRcvWrap
            // 
            this.checkBoxMsgRcvWrap.AutoSize = true;
            this.checkBoxMsgRcvWrap.Checked = true;
            this.checkBoxMsgRcvWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMsgRcvWrap.Location = new System.Drawing.Point(537, 88);
            this.checkBoxMsgRcvWrap.Name = "checkBoxMsgRcvWrap";
            this.checkBoxMsgRcvWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxMsgRcvWrap.TabIndex = 24;
            this.checkBoxMsgRcvWrap.Text = "Word Wrap";
            this.checkBoxMsgRcvWrap.UseVisualStyleBackColor = true;
            // 
            // checkBoxMsgSndWrap
            // 
            this.checkBoxMsgSndWrap.AutoSize = true;
            this.checkBoxMsgSndWrap.Checked = true;
            this.checkBoxMsgSndWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMsgSndWrap.Location = new System.Drawing.Point(537, 244);
            this.checkBoxMsgSndWrap.Name = "checkBoxMsgSndWrap";
            this.checkBoxMsgSndWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxMsgSndWrap.TabIndex = 23;
            this.checkBoxMsgSndWrap.Text = "Word Wrap";
            this.checkBoxMsgSndWrap.UseVisualStyleBackColor = true;
            // 
            // textBoxMsgRcv
            // 
            this.textBoxMsgRcv.Location = new System.Drawing.Point(21, 108);
            this.textBoxMsgRcv.Multiline = true;
            this.textBoxMsgRcv.Name = "textBoxMsgRcv";
            this.textBoxMsgRcv.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMsgRcv.Size = new System.Drawing.Size(597, 121);
            this.textBoxMsgRcv.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 19);
            this.label4.TabIndex = 21;
            this.label4.Text = "Receive Message:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMsgSnd
            // 
            this.textBoxMsgSnd.Location = new System.Drawing.Point(21, 267);
            this.textBoxMsgSnd.Multiline = true;
            this.textBoxMsgSnd.Name = "textBoxMsgSnd";
            this.textBoxMsgSnd.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMsgSnd.Size = new System.Drawing.Size(597, 125);
            this.textBoxMsgSnd.TabIndex = 20;
            this.textBoxMsgSnd.Text = resources.GetString("textBoxMsgSnd.Text");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 19);
            this.label3.TabIndex = 19;
            this.label3.Text = "Send Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSOAPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 412);
            this.Controls.Add(this.checkBoxMsgRcvWrap);
            this.Controls.Add(this.checkBoxMsgSndWrap);
            this.Controls.Add(this.textBoxMsgRcv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMsgSnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxSrvAction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxSrvURI);
            this.Controls.Add(this.label5);
            this.Name = "FormSOAPServer";
            this.Text = "SOAP Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSOAPServer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSrvURI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSrvAction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxMsgRcvWrap;
        private System.Windows.Forms.CheckBox checkBoxMsgSndWrap;
        private System.Windows.Forms.TextBox textBoxMsgRcv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMsgSnd;
        private System.Windows.Forms.Label label3;
    }
}