namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Forms
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxURI = new System.Windows.Forms.TextBox();
            this.buttonAdvance = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonNavigate = new System.Windows.Forms.Button();
            this.textBoxAction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSndXsltExt = new System.Windows.Forms.Button();
            this.buttonSndXslt = new System.Windows.Forms.Button();
            this.radioButtonSndXslt = new System.Windows.Forms.RadioButton();
            this.radioButtonSndDirect = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRcvXsltExt = new System.Windows.Forms.Button();
            this.buttonRcvXslt = new System.Windows.Forms.Button();
            this.radioButtonRcvXslt = new System.Windows.Forms.RadioButton();
            this.radioButtonRcvDirect = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(467, 318);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 25);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(549, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "SOAP Server URI:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxURI
            // 
            this.textBoxURI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURI.Location = new System.Drawing.Point(119, 30);
            this.textBoxURI.Name = "textBoxURI";
            this.textBoxURI.Size = new System.Drawing.Size(346, 20);
            this.textBoxURI.TabIndex = 12;
            // 
            // buttonAdvance
            // 
            this.buttonAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvance.Location = new System.Drawing.Point(481, 56);
            this.buttonAdvance.Name = "buttonAdvance";
            this.buttonAdvance.Size = new System.Drawing.Size(101, 25);
            this.buttonAdvance.TabIndex = 14;
            this.buttonAdvance.Text = "Advance Setting";
            this.buttonAdvance.UseVisualStyleBackColor = true;
            this.buttonAdvance.Click += new System.EventHandler(this.buttonAdvance_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonNavigate);
            this.groupBox1.Controls.Add(this.textBoxAction);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxURI);
            this.groupBox1.Controls.Add(this.buttonAdvance);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 97);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SOAP Client Endpoint";
            // 
            // buttonNavigate
            // 
            this.buttonNavigate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNavigate.Location = new System.Drawing.Point(481, 25);
            this.buttonNavigate.Name = "buttonNavigate";
            this.buttonNavigate.Size = new System.Drawing.Size(101, 25);
            this.buttonNavigate.TabIndex = 20;
            this.buttonNavigate.Text = "View In Browser";
            this.buttonNavigate.UseVisualStyleBackColor = true;
            this.buttonNavigate.Click += new System.EventHandler(this.buttonNavigate_Click);
            // 
            // textBoxAction
            // 
            this.textBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAction.Location = new System.Drawing.Point(119, 56);
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.Size = new System.Drawing.Size(346, 20);
            this.textBoxAction.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "SOAP Action:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonSndXsltExt);
            this.groupBox2.Controls.Add(this.buttonSndXslt);
            this.groupBox2.Controls.Add(this.radioButtonSndXslt);
            this.groupBox2.Controls.Add(this.radioButtonSndDirect);
            this.groupBox2.Location = new System.Drawing.Point(12, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 191);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Requesting Message Generation";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(15, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 23);
            this.label3.TabIndex = 18;
            this.label3.Text = "Generate requesting SOAP envelope by:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSndXsltExt
            // 
            this.buttonSndXsltExt.Location = new System.Drawing.Point(153, 148);
            this.buttonSndXsltExt.Name = "buttonSndXsltExt";
            this.buttonSndXsltExt.Size = new System.Drawing.Size(111, 25);
            this.buttonSndXsltExt.TabIndex = 17;
            this.buttonSndXsltExt.Text = "XSLT Extension";
            this.buttonSndXsltExt.UseVisualStyleBackColor = true;
            this.buttonSndXsltExt.Click += new System.EventHandler(this.buttonSndXsltExt_Click);
            // 
            // buttonSndXslt
            // 
            this.buttonSndXslt.Location = new System.Drawing.Point(36, 148);
            this.buttonSndXslt.Name = "buttonSndXslt";
            this.buttonSndXslt.Size = new System.Drawing.Size(111, 25);
            this.buttonSndXslt.TabIndex = 17;
            this.buttonSndXslt.Text = "Edit XSLT File";
            this.buttonSndXslt.UseVisualStyleBackColor = true;
            this.buttonSndXslt.Click += new System.EventHandler(this.buttonSndXslt_Click);
            // 
            // radioButtonSndXslt
            // 
            this.radioButtonSndXslt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonSndXslt.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndXslt.Location = new System.Drawing.Point(18, 110);
            this.radioButtonSndXslt.Name = "radioButtonSndXslt";
            this.radioButtonSndXslt.Size = new System.Drawing.Size(270, 32);
            this.radioButtonSndXslt.TabIndex = 1;
            this.radioButtonSndXslt.TabStop = true;
            this.radioButtonSndXslt.Text = "Using XSLT script to transform outgoing message to the requesting SOAP envelope.";
            this.radioButtonSndXslt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndXslt.UseVisualStyleBackColor = true;
            // 
            // radioButtonSndDirect
            // 
            this.radioButtonSndDirect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonSndDirect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndDirect.Checked = true;
            this.radioButtonSndDirect.Location = new System.Drawing.Point(18, 60);
            this.radioButtonSndDirect.Name = "radioButtonSndDirect";
            this.radioButtonSndDirect.Size = new System.Drawing.Size(270, 33);
            this.radioButtonSndDirect.TabIndex = 0;
            this.radioButtonSndDirect.TabStop = true;
            this.radioButtonSndDirect.Text = "Using the body content of outgoing message as the requesting SOAP envelope direct" +
                "ly.";
            this.radioButtonSndDirect.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndDirect.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.buttonRcvXsltExt);
            this.groupBox3.Controls.Add(this.buttonRcvXslt);
            this.groupBox3.Controls.Add(this.radioButtonRcvXslt);
            this.groupBox3.Controls.Add(this.radioButtonRcvDirect);
            this.groupBox3.Location = new System.Drawing.Point(329, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 191);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Responsing Message Processing";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "Process responsing SOAP envelope by:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonRcvXsltExt
            // 
            this.buttonRcvXsltExt.Location = new System.Drawing.Point(154, 148);
            this.buttonRcvXsltExt.Name = "buttonRcvXsltExt";
            this.buttonRcvXsltExt.Size = new System.Drawing.Size(111, 25);
            this.buttonRcvXsltExt.TabIndex = 17;
            this.buttonRcvXsltExt.Text = "XSLT Extension";
            this.buttonRcvXsltExt.UseVisualStyleBackColor = true;
            this.buttonRcvXsltExt.Click += new System.EventHandler(this.buttonRcvXsltExt_Click);
            // 
            // buttonRcvXslt
            // 
            this.buttonRcvXslt.Location = new System.Drawing.Point(37, 148);
            this.buttonRcvXslt.Name = "buttonRcvXslt";
            this.buttonRcvXslt.Size = new System.Drawing.Size(111, 25);
            this.buttonRcvXslt.TabIndex = 17;
            this.buttonRcvXslt.Text = "Edit XSLT File";
            this.buttonRcvXslt.UseVisualStyleBackColor = true;
            this.buttonRcvXslt.Click += new System.EventHandler(this.buttonRcvXslt_Click);
            // 
            // radioButtonRcvXslt
            // 
            this.radioButtonRcvXslt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRcvXslt.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvXslt.Location = new System.Drawing.Point(18, 110);
            this.radioButtonRcvXslt.Name = "radioButtonRcvXslt";
            this.radioButtonRcvXslt.Size = new System.Drawing.Size(260, 32);
            this.radioButtonRcvXslt.TabIndex = 1;
            this.radioButtonRcvXslt.TabStop = true;
            this.radioButtonRcvXslt.Text = "Using XSLT script to transform the responsing SOAP envelope to incoming message.";
            this.radioButtonRcvXslt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvXslt.UseVisualStyleBackColor = true;
            // 
            // radioButtonRcvDirect
            // 
            this.radioButtonRcvDirect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRcvDirect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvDirect.Checked = true;
            this.radioButtonRcvDirect.Location = new System.Drawing.Point(18, 60);
            this.radioButtonRcvDirect.Name = "radioButtonRcvDirect";
            this.radioButtonRcvDirect.Size = new System.Drawing.Size(260, 33);
            this.radioButtonRcvDirect.TabIndex = 0;
            this.radioButtonRcvDirect.TabStop = true;
            this.radioButtonRcvDirect.Text = "Using the responsing SOAP envelope as the body content of incoming message direct" +
                "ly.";
            this.radioButtonRcvDirect.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvDirect.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 355);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.MinimumSize = new System.Drawing.Size(645, 378);
            this.Name = "FormConfig";
            this.Text = "SOAP Client Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxURI;
        private System.Windows.Forms.Button buttonAdvance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonSndDirect;
        private System.Windows.Forms.RadioButton radioButtonSndXslt;
        private System.Windows.Forms.Button buttonSndXslt;
        private System.Windows.Forms.Button buttonSndXsltExt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonRcvXsltExt;
        private System.Windows.Forms.Button buttonRcvXslt;
        private System.Windows.Forms.RadioButton radioButtonRcvXslt;
        private System.Windows.Forms.RadioButton radioButtonRcvDirect;
        private System.Windows.Forms.TextBox textBoxAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonNavigate;
    }
}