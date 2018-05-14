namespace HYS.IM.Messaging.Config
{
    partial class FormDuplexProcess
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
            this.groupBoxXSLT = new System.Windows.Forms.GroupBox();
            this.buttonBrowseResponse = new System.Windows.Forms.Button();
            this.textBoxXSLTResponse = new System.Windows.Forms.TextBox();
            this.checkBoxXSLTResponse = new System.Windows.Forms.CheckBox();
            this.checkBoxRelativePath = new System.Windows.Forms.CheckBox();
            this.buttonBrowseRequest = new System.Windows.Forms.Button();
            this.textBoxXSLTRequest = new System.Windows.Forms.TextBox();
            this.checkBoxXSLTRequest = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxXSLT.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxXSLT
            // 
            this.groupBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxXSLT.Controls.Add(this.buttonBrowseResponse);
            this.groupBoxXSLT.Controls.Add(this.textBoxXSLTResponse);
            this.groupBoxXSLT.Controls.Add(this.checkBoxXSLTResponse);
            this.groupBoxXSLT.Controls.Add(this.checkBoxRelativePath);
            this.groupBoxXSLT.Controls.Add(this.buttonBrowseRequest);
            this.groupBoxXSLT.Controls.Add(this.textBoxXSLTRequest);
            this.groupBoxXSLT.Controls.Add(this.checkBoxXSLTRequest);
            this.groupBoxXSLT.Location = new System.Drawing.Point(12, 12);
            this.groupBoxXSLT.Name = "groupBoxXSLT";
            this.groupBoxXSLT.Size = new System.Drawing.Size(471, 188);
            this.groupBoxXSLT.TabIndex = 19;
            this.groupBoxXSLT.TabStop = false;
            this.groupBoxXSLT.Text = "Message Transformation";
            // 
            // buttonBrowseResponse
            // 
            this.buttonBrowseResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseResponse.Location = new System.Drawing.Point(386, 115);
            this.buttonBrowseResponse.Name = "buttonBrowseResponse";
            this.buttonBrowseResponse.Size = new System.Drawing.Size(68, 25);
            this.buttonBrowseResponse.TabIndex = 22;
            this.buttonBrowseResponse.Text = "Browse";
            this.buttonBrowseResponse.UseVisualStyleBackColor = true;
            this.buttonBrowseResponse.Click += new System.EventHandler(this.buttonBrowseResponse_Click);
            // 
            // textBoxXSLTResponse
            // 
            this.textBoxXSLTResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLTResponse.Location = new System.Drawing.Point(15, 120);
            this.textBoxXSLTResponse.Name = "textBoxXSLTResponse";
            this.textBoxXSLTResponse.Size = new System.Drawing.Size(356, 20);
            this.textBoxXSLTResponse.TabIndex = 21;
            // 
            // checkBoxXSLTResponse
            // 
            this.checkBoxXSLTResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLTResponse.Location = new System.Drawing.Point(14, 87);
            this.checkBoxXSLTResponse.Name = "checkBoxXSLTResponse";
            this.checkBoxXSLTResponse.Size = new System.Drawing.Size(439, 27);
            this.checkBoxXSLTResponse.TabIndex = 20;
            this.checkBoxXSLTResponse.Text = "Use the following XSLT file to transform responsing message before {0}.";
            this.checkBoxXSLTResponse.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelativePath
            // 
            this.checkBoxRelativePath.AutoSize = true;
            this.checkBoxRelativePath.Checked = true;
            this.checkBoxRelativePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelativePath.Location = new System.Drawing.Point(15, 155);
            this.checkBoxRelativePath.Name = "checkBoxRelativePath";
            this.checkBoxRelativePath.Size = new System.Drawing.Size(284, 17);
            this.checkBoxRelativePath.TabIndex = 19;
            this.checkBoxRelativePath.Text = "Use relative path when browsing and selecting pathes.";
            this.checkBoxRelativePath.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseRequest
            // 
            this.buttonBrowseRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseRequest.Location = new System.Drawing.Point(386, 56);
            this.buttonBrowseRequest.Name = "buttonBrowseRequest";
            this.buttonBrowseRequest.Size = new System.Drawing.Size(68, 25);
            this.buttonBrowseRequest.TabIndex = 19;
            this.buttonBrowseRequest.Text = "Browse";
            this.buttonBrowseRequest.UseVisualStyleBackColor = true;
            this.buttonBrowseRequest.Click += new System.EventHandler(this.buttonBrowseRequest_Click);
            // 
            // textBoxXSLTRequest
            // 
            this.textBoxXSLTRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLTRequest.Location = new System.Drawing.Point(15, 61);
            this.textBoxXSLTRequest.Name = "textBoxXSLTRequest";
            this.textBoxXSLTRequest.Size = new System.Drawing.Size(356, 20);
            this.textBoxXSLTRequest.TabIndex = 1;
            // 
            // checkBoxXSLTRequest
            // 
            this.checkBoxXSLTRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLTRequest.Location = new System.Drawing.Point(15, 28);
            this.checkBoxXSLTRequest.Name = "checkBoxXSLTRequest";
            this.checkBoxXSLTRequest.Size = new System.Drawing.Size(438, 26);
            this.checkBoxXSLTRequest.TabIndex = 0;
            this.checkBoxXSLTRequest.Text = "Use the following XSLT file to transform requesting message before {0}.";
            this.checkBoxXSLTRequest.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(398, 213);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 25);
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(306, 213);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 25);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormDuplexProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 250);
            this.Controls.Add(this.groupBoxXSLT);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDuplexProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Processing Setting";
            this.groupBoxXSLT.ResumeLayout(false);
            this.groupBoxXSLT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxXSLT;
        private System.Windows.Forms.CheckBox checkBoxRelativePath;
        private System.Windows.Forms.Button buttonBrowseRequest;
        private System.Windows.Forms.TextBox textBoxXSLTRequest;
        private System.Windows.Forms.CheckBox checkBoxXSLTRequest;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonBrowseResponse;
        private System.Windows.Forms.TextBox textBoxXSLTResponse;
        private System.Windows.Forms.CheckBox checkBoxXSLTResponse;
    }
}