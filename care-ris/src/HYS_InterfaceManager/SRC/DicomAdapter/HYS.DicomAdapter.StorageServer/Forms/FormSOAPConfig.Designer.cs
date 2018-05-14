namespace HYS.DicomAdapter.StorageServer.Forms
{
    partial class FormSOAPConfig
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
            this.groupBoxSOAP = new System.Windows.Forms.GroupBox();
            this.textBoxSOAPAction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSoapRsp = new System.Windows.Forms.Button();
            this.buttonSoapReq = new System.Windows.Forms.Button();
            this.checkBoxXMLasString = new System.Windows.Forms.CheckBox();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonBrowseD2S = new System.Windows.Forms.Button();
            this.buttonBrowseS2D = new System.Windows.Forms.Button();
            this.textBoxXSLTd2s = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxXSLTs2d = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxTransform = new System.Windows.Forms.GroupBox();
            this.groupBoxSOAP.SuspendLayout();
            this.groupBoxTransform.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSOAP
            // 
            this.groupBoxSOAP.Controls.Add(this.textBoxSOAPAction);
            this.groupBoxSOAP.Controls.Add(this.label1);
            this.groupBoxSOAP.Controls.Add(this.buttonSoapRsp);
            this.groupBoxSOAP.Controls.Add(this.buttonSoapReq);
            this.groupBoxSOAP.Controls.Add(this.checkBoxXMLasString);
            this.groupBoxSOAP.Controls.Add(this.textBoxURL);
            this.groupBoxSOAP.Controls.Add(this.label4);
            this.groupBoxSOAP.Location = new System.Drawing.Point(23, 20);
            this.groupBoxSOAP.Name = "groupBoxSOAP";
            this.groupBoxSOAP.Size = new System.Drawing.Size(551, 155);
            this.groupBoxSOAP.TabIndex = 2;
            this.groupBoxSOAP.TabStop = false;
            this.groupBoxSOAP.Text = "SOAP Setting";
            // 
            // textBoxSOAPAction
            // 
            this.textBoxSOAPAction.Location = new System.Drawing.Point(108, 54);
            this.textBoxSOAPAction.Name = "textBoxSOAPAction";
            this.textBoxSOAPAction.Size = new System.Drawing.Size(380, 20);
            this.textBoxSOAPAction.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 76;
            this.label1.Text = "SOAP Action:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSoapRsp
            // 
            this.buttonSoapRsp.Location = new System.Drawing.Point(306, 104);
            this.buttonSoapRsp.Name = "buttonSoapRsp";
            this.buttonSoapRsp.Size = new System.Drawing.Size(182, 28);
            this.buttonSoapRsp.TabIndex = 75;
            this.buttonSoapRsp.Text = "Response Message Processing";
            this.buttonSoapRsp.UseVisualStyleBackColor = true;
            this.buttonSoapRsp.Click += new System.EventHandler(this.buttonSoapRsp_Click);
            // 
            // buttonSoapReq
            // 
            this.buttonSoapReq.Location = new System.Drawing.Point(108, 104);
            this.buttonSoapReq.Name = "buttonSoapReq";
            this.buttonSoapReq.Size = new System.Drawing.Size(192, 28);
            this.buttonSoapReq.TabIndex = 74;
            this.buttonSoapReq.Text = "Request Message Processing";
            this.buttonSoapReq.UseVisualStyleBackColor = true;
            this.buttonSoapReq.Click += new System.EventHandler(this.buttonSoapReq_Click);
            // 
            // checkBoxXMLasString
            // 
            this.checkBoxXMLasString.AutoSize = true;
            this.checkBoxXMLasString.Checked = true;
            this.checkBoxXMLasString.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXMLasString.Location = new System.Drawing.Point(108, 81);
            this.checkBoxXMLasString.Name = "checkBoxXMLasString";
            this.checkBoxXMLasString.Size = new System.Drawing.Size(192, 17);
            this.checkBoxXMLasString.TabIndex = 72;
            this.checkBoxXMLasString.Text = "Enable SOAP Message Processing";
            this.checkBoxXMLasString.UseVisualStyleBackColor = true;
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(108, 25);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(380, 20);
            this.textBoxURL.TabIndex = 69;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
            this.label4.TabIndex = 69;
            this.label4.Text = "Server URI:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonBrowseD2S
            // 
            this.buttonBrowseD2S.Location = new System.Drawing.Point(455, 29);
            this.buttonBrowseD2S.Name = "buttonBrowseD2S";
            this.buttonBrowseD2S.Size = new System.Drawing.Size(33, 20);
            this.buttonBrowseD2S.TabIndex = 72;
            this.buttonBrowseD2S.Text = "...";
            this.buttonBrowseD2S.UseVisualStyleBackColor = true;
            this.buttonBrowseD2S.Click += new System.EventHandler(this.buttonBrowseD2S_Click);
            // 
            // buttonBrowseS2D
            // 
            this.buttonBrowseS2D.Location = new System.Drawing.Point(455, 56);
            this.buttonBrowseS2D.Name = "buttonBrowseS2D";
            this.buttonBrowseS2D.Size = new System.Drawing.Size(33, 20);
            this.buttonBrowseS2D.TabIndex = 71;
            this.buttonBrowseS2D.Text = "...";
            this.buttonBrowseS2D.UseVisualStyleBackColor = true;
            this.buttonBrowseS2D.Click += new System.EventHandler(this.buttonBrowseS2D_Click);
            // 
            // textBoxXSLTd2s
            // 
            this.textBoxXSLTd2s.Location = new System.Drawing.Point(167, 29);
            this.textBoxXSLTd2s.Name = "textBoxXSLTd2s";
            this.textBoxXSLTd2s.Size = new System.Drawing.Size(282, 20);
            this.textBoxXSLTd2s.TabIndex = 73;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 20);
            this.label7.TabIndex = 72;
            this.label7.Text = "XSLT File (DICOM->SOAP) :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXSLTs2d
            // 
            this.textBoxXSLTs2d.Location = new System.Drawing.Point(167, 56);
            this.textBoxXSLTs2d.Name = "textBoxXSLTs2d";
            this.textBoxXSLTs2d.Size = new System.Drawing.Size(282, 20);
            this.textBoxXSLTs2d.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 20);
            this.label5.TabIndex = 70;
            this.label5.Text = "XSLT File (SOAP->DICOM) :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(501, 295);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(73, 28);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(422, 295);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(73, 28);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxTransform
            // 
            this.groupBoxTransform.Controls.Add(this.textBoxXSLTs2d);
            this.groupBoxTransform.Controls.Add(this.buttonBrowseD2S);
            this.groupBoxTransform.Controls.Add(this.label5);
            this.groupBoxTransform.Controls.Add(this.buttonBrowseS2D);
            this.groupBoxTransform.Controls.Add(this.label7);
            this.groupBoxTransform.Controls.Add(this.textBoxXSLTd2s);
            this.groupBoxTransform.Location = new System.Drawing.Point(23, 185);
            this.groupBoxTransform.Name = "groupBoxTransform";
            this.groupBoxTransform.Size = new System.Drawing.Size(551, 99);
            this.groupBoxTransform.TabIndex = 4;
            this.groupBoxTransform.TabStop = false;
            this.groupBoxTransform.Text = "SOAP - DICOM Transformation";
            // 
            // FormSOAPConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 345);
            this.Controls.Add(this.groupBoxTransform);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxSOAP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSOAPConfig";
            this.Text = "SOAP Client Configuration";
            this.groupBoxSOAP.ResumeLayout(false);
            this.groupBoxSOAP.PerformLayout();
            this.groupBoxTransform.ResumeLayout(false);
            this.groupBoxTransform.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSOAP;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxXSLTs2d;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxXSLTd2s;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonBrowseS2D;
        private System.Windows.Forms.Button buttonBrowseD2S;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxXMLasString;
        private System.Windows.Forms.GroupBox groupBoxTransform;
        private System.Windows.Forms.Button buttonSoapReq;
        private System.Windows.Forms.Button buttonSoapRsp;
        private System.Windows.Forms.TextBox textBoxSOAPAction;
        private System.Windows.Forms.Label label1;
    }
}