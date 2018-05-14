namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Forms
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
            this.textBoxURI = new System.Windows.Forms.TextBox();
            this.buttonAdvance = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonRcvDispatch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRcvXsltExt = new System.Windows.Forms.Button();
            this.buttonRcvXslt = new System.Windows.Forms.Button();
            this.radioButtonRcvXslt = new System.Windows.Forms.RadioButton();
            this.radioButtonRcvDirect = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSndFailure = new System.Windows.Forms.Button();
            this.buttonSndSuccess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSndXsltExt = new System.Windows.Forms.Button();
            this.buttonSndXslt = new System.Windows.Forms.Button();
            this.radioButtonSndXslt = new System.Windows.Forms.RadioButton();
            this.radioButtonSndDirect = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(468, 332);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 25);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxURI
            // 
            this.textBoxURI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURI.Location = new System.Drawing.Point(123, 24);
            this.textBoxURI.Name = "textBoxURI";
            this.textBoxURI.Size = new System.Drawing.Size(347, 20);
            this.textBoxURI.TabIndex = 12;
            // 
            // buttonAdvance
            // 
            this.buttonAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvance.Location = new System.Drawing.Point(486, 19);
            this.buttonAdvance.Name = "buttonAdvance";
            this.buttonAdvance.Size = new System.Drawing.Size(99, 25);
            this.buttonAdvance.TabIndex = 14;
            this.buttonAdvance.Text = "Advance Setting";
            this.buttonAdvance.UseVisualStyleBackColor = true;
            this.buttonAdvance.Click += new System.EventHandler(this.buttonAdvance_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "SOAP Server URI:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxURI);
            this.groupBox1.Controls.Add(this.buttonAdvance);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 60);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SOAP Server Endpoint";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(550, 332);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.buttonRcvDispatch);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.buttonRcvXsltExt);
            this.groupBox3.Controls.Add(this.buttonRcvXslt);
            this.groupBox3.Controls.Add(this.radioButtonRcvXslt);
            this.groupBox3.Controls.Add(this.radioButtonRcvDirect);
            this.groupBox3.Location = new System.Drawing.Point(12, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 239);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Requesting Message Processing";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(15, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 34);
            this.label5.TabIndex = 24;
            this.label5.Text = "Determine how to dispatch the incoming message in order to get corresponding resp" +
                "onse:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonRcvDispatch
            // 
            this.buttonRcvDispatch.Location = new System.Drawing.Point(37, 198);
            this.buttonRcvDispatch.Name = "buttonRcvDispatch";
            this.buttonRcvDispatch.Size = new System.Drawing.Size(228, 25);
            this.buttonRcvDispatch.TabIndex = 23;
            this.buttonRcvDispatch.Text = "Message Dispatching Setting";
            this.buttonRcvDispatch.UseVisualStyleBackColor = true;
            this.buttonRcvDispatch.Click += new System.EventHandler(this.buttonRcvDispatch_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(15, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 24);
            this.label4.TabIndex = 19;
            this.label4.Text = "Process requesting SOAP envelope by:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonRcvXsltExt
            // 
            this.buttonRcvXsltExt.Location = new System.Drawing.Point(154, 129);
            this.buttonRcvXsltExt.Name = "buttonRcvXsltExt";
            this.buttonRcvXsltExt.Size = new System.Drawing.Size(111, 25);
            this.buttonRcvXsltExt.TabIndex = 17;
            this.buttonRcvXsltExt.Text = "XSLT Extension";
            this.buttonRcvXsltExt.UseVisualStyleBackColor = true;
            this.buttonRcvXsltExt.Click += new System.EventHandler(this.buttonRcvXsltExt_Click);
            // 
            // buttonRcvXslt
            // 
            this.buttonRcvXslt.Location = new System.Drawing.Point(37, 129);
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
            this.radioButtonRcvXslt.Location = new System.Drawing.Point(18, 93);
            this.radioButtonRcvXslt.Name = "radioButtonRcvXslt";
            this.radioButtonRcvXslt.Size = new System.Drawing.Size(255, 32);
            this.radioButtonRcvXslt.TabIndex = 1;
            this.radioButtonRcvXslt.TabStop = true;
            this.radioButtonRcvXslt.Text = "Using XSLT script to transform the requesting SOAP envelope to incoming message.";
            this.radioButtonRcvXslt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvXslt.UseVisualStyleBackColor = true;
            // 
            // radioButtonRcvDirect
            // 
            this.radioButtonRcvDirect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRcvDirect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvDirect.Checked = true;
            this.radioButtonRcvDirect.Location = new System.Drawing.Point(18, 54);
            this.radioButtonRcvDirect.Name = "radioButtonRcvDirect";
            this.radioButtonRcvDirect.Size = new System.Drawing.Size(255, 33);
            this.radioButtonRcvDirect.TabIndex = 0;
            this.radioButtonRcvDirect.TabStop = true;
            this.radioButtonRcvDirect.Text = "Using the requesting SOAP envelope as the body content of incoming message direct" +
                "ly.";
            this.radioButtonRcvDirect.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRcvDirect.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonSndFailure);
            this.groupBox2.Controls.Add(this.buttonSndSuccess);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonSndXsltExt);
            this.groupBox2.Controls.Add(this.buttonSndXslt);
            this.groupBox2.Controls.Add(this.radioButtonSndXslt);
            this.groupBox2.Controls.Add(this.radioButtonSndDirect);
            this.groupBox2.Location = new System.Drawing.Point(313, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(313, 239);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Responsing Message Generation";
            // 
            // buttonSndFailure
            // 
            this.buttonSndFailure.Location = new System.Drawing.Point(155, 198);
            this.buttonSndFailure.Name = "buttonSndFailure";
            this.buttonSndFailure.Size = new System.Drawing.Size(111, 25);
            this.buttonSndFailure.TabIndex = 24;
            this.buttonSndFailure.Text = "Failure Response";
            this.buttonSndFailure.UseVisualStyleBackColor = true;
            this.buttonSndFailure.Click += new System.EventHandler(this.buttonSndFailure_Click);
            // 
            // buttonSndSuccess
            // 
            this.buttonSndSuccess.Location = new System.Drawing.Point(38, 198);
            this.buttonSndSuccess.Name = "buttonSndSuccess";
            this.buttonSndSuccess.Size = new System.Drawing.Size(111, 25);
            this.buttonSndSuccess.TabIndex = 23;
            this.buttonSndSuccess.Text = "Success Response";
            this.buttonSndSuccess.UseVisualStyleBackColor = true;
            this.buttonSndSuccess.Click += new System.EventHandler(this.buttonSndSuccess_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(16, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 34);
            this.label1.TabIndex = 23;
            this.label1.Text = "After dispatching incoming message to publish channel, use the following template" +
                "s to generate response:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(15, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 24);
            this.label3.TabIndex = 18;
            this.label3.Text = "Generate responsing SOAP envelope by:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSndXsltExt
            // 
            this.buttonSndXsltExt.Location = new System.Drawing.Point(155, 129);
            this.buttonSndXsltExt.Name = "buttonSndXsltExt";
            this.buttonSndXsltExt.Size = new System.Drawing.Size(111, 25);
            this.buttonSndXsltExt.TabIndex = 17;
            this.buttonSndXsltExt.Text = "XSLT Extension";
            this.buttonSndXsltExt.UseVisualStyleBackColor = true;
            this.buttonSndXsltExt.Click += new System.EventHandler(this.buttonSndXsltExt_Click);
            // 
            // buttonSndXslt
            // 
            this.buttonSndXslt.Location = new System.Drawing.Point(38, 129);
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
            this.radioButtonSndXslt.Location = new System.Drawing.Point(19, 93);
            this.radioButtonSndXslt.Name = "radioButtonSndXslt";
            this.radioButtonSndXslt.Size = new System.Drawing.Size(270, 32);
            this.radioButtonSndXslt.TabIndex = 1;
            this.radioButtonSndXslt.TabStop = true;
            this.radioButtonSndXslt.Text = "Use XSLT script to transform outgoing message to the responsing SOAP envelope.";
            this.radioButtonSndXslt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndXslt.UseVisualStyleBackColor = true;
            // 
            // radioButtonSndDirect
            // 
            this.radioButtonSndDirect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonSndDirect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndDirect.Checked = true;
            this.radioButtonSndDirect.Location = new System.Drawing.Point(19, 54);
            this.radioButtonSndDirect.Name = "radioButtonSndDirect";
            this.radioButtonSndDirect.Size = new System.Drawing.Size(270, 33);
            this.radioButtonSndDirect.TabIndex = 0;
            this.radioButtonSndDirect.TabStop = true;
            this.radioButtonSndDirect.Text = "Use the body content of outgoing message as the responsing SOAP envelope directly" +
                ".";
            this.radioButtonSndDirect.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSndDirect.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 369);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.MinimumSize = new System.Drawing.Size(646, 403);
            this.Name = "FormConfig";
            this.Text = "SOAP Server Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxURI;
        private System.Windows.Forms.Button buttonAdvance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRcvXsltExt;
        private System.Windows.Forms.Button buttonRcvXslt;
        private System.Windows.Forms.RadioButton radioButtonRcvXslt;
        private System.Windows.Forms.RadioButton radioButtonRcvDirect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSndXsltExt;
        private System.Windows.Forms.Button buttonSndXslt;
        private System.Windows.Forms.RadioButton radioButtonSndXslt;
        private System.Windows.Forms.RadioButton radioButtonSndDirect;
        private System.Windows.Forms.Button buttonRcvDispatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSndSuccess;
        private System.Windows.Forms.Button buttonSndFailure;
        private System.Windows.Forms.Label label5;
    }
}