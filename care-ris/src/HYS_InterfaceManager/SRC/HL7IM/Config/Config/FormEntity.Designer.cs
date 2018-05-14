namespace HYS.IM.Config
{
    partial class FormEntity
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.tabPageTransform = new System.Windows.Forms.TabPage();
            this.panelDuplexTransform = new System.Windows.Forms.Panel();
            this.buttonBrowseResponse = new System.Windows.Forms.Button();
            this.textBoxXSLTResponse = new System.Windows.Forms.TextBox();
            this.checkBoxXSLTResponse = new System.Windows.Forms.CheckBox();
            this.buttonBrowseRequest = new System.Windows.Forms.Button();
            this.textBoxXSLTRequest = new System.Windows.Forms.TextBox();
            this.checkBoxXSLTRequest = new System.Windows.Forms.CheckBox();
            this.checkBoxRelativePath = new System.Windows.Forms.CheckBox();
            this.panelOneWayTransform = new System.Windows.Forms.Panel();
            this.checkBoxXSLT = new System.Windows.Forms.CheckBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxXSLT = new System.Windows.Forms.TextBox();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageTransform.SuspendLayout();
            this.panelDuplexTransform.SuspendLayout();
            this.panelOneWayTransform.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(442, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 28);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(356, 308);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 28);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(3, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(492, 247);
            this.panelMain.TabIndex = 8;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageTransform);
            this.tabControlMain.Location = new System.Drawing.Point(15, 16);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(506, 279);
            this.tabControlMain.TabIndex = 9;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.panelMain);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(498, 253);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "...";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // tabPageTransform
            // 
            this.tabPageTransform.Controls.Add(this.panelDuplexTransform);
            this.tabPageTransform.Controls.Add(this.checkBoxRelativePath);
            this.tabPageTransform.Controls.Add(this.panelOneWayTransform);
            this.tabPageTransform.Location = new System.Drawing.Point(4, 22);
            this.tabPageTransform.Name = "tabPageTransform";
            this.tabPageTransform.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTransform.Size = new System.Drawing.Size(498, 253);
            this.tabPageTransform.TabIndex = 1;
            this.tabPageTransform.Text = "Message Transformation";
            this.tabPageTransform.UseVisualStyleBackColor = true;
            // 
            // panelDuplexTransform
            // 
            this.panelDuplexTransform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDuplexTransform.Controls.Add(this.buttonBrowseResponse);
            this.panelDuplexTransform.Controls.Add(this.textBoxXSLTResponse);
            this.panelDuplexTransform.Controls.Add(this.checkBoxXSLTResponse);
            this.panelDuplexTransform.Controls.Add(this.buttonBrowseRequest);
            this.panelDuplexTransform.Controls.Add(this.textBoxXSLTRequest);
            this.panelDuplexTransform.Controls.Add(this.checkBoxXSLTRequest);
            this.panelDuplexTransform.Location = new System.Drawing.Point(9, 84);
            this.panelDuplexTransform.Name = "panelDuplexTransform";
            this.panelDuplexTransform.Size = new System.Drawing.Size(489, 127);
            this.panelDuplexTransform.TabIndex = 20;
            // 
            // buttonBrowseResponse
            // 
            this.buttonBrowseResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseResponse.Location = new System.Drawing.Point(398, 94);
            this.buttonBrowseResponse.Name = "buttonBrowseResponse";
            this.buttonBrowseResponse.Size = new System.Drawing.Size(68, 25);
            this.buttonBrowseResponse.TabIndex = 28;
            this.buttonBrowseResponse.Text = "Browse";
            this.buttonBrowseResponse.UseVisualStyleBackColor = true;
            this.buttonBrowseResponse.Click += new System.EventHandler(this.buttonBrowseResponse_Click);
            // 
            // textBoxXSLTResponse
            // 
            this.textBoxXSLTResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLTResponse.Location = new System.Drawing.Point(12, 99);
            this.textBoxXSLTResponse.Name = "textBoxXSLTResponse";
            this.textBoxXSLTResponse.Size = new System.Drawing.Size(368, 20);
            this.textBoxXSLTResponse.TabIndex = 27;
            // 
            // checkBoxXSLTResponse
            // 
            this.checkBoxXSLTResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLTResponse.Location = new System.Drawing.Point(12, 66);
            this.checkBoxXSLTResponse.Name = "checkBoxXSLTResponse";
            this.checkBoxXSLTResponse.Size = new System.Drawing.Size(474, 27);
            this.checkBoxXSLTResponse.TabIndex = 26;
            this.checkBoxXSLTResponse.Text = "Use the following XSLT file to transform responsing message before {0}.";
            this.checkBoxXSLTResponse.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseRequest
            // 
            this.buttonBrowseRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseRequest.Location = new System.Drawing.Point(398, 37);
            this.buttonBrowseRequest.Name = "buttonBrowseRequest";
            this.buttonBrowseRequest.Size = new System.Drawing.Size(68, 25);
            this.buttonBrowseRequest.TabIndex = 25;
            this.buttonBrowseRequest.Text = "Browse";
            this.buttonBrowseRequest.UseVisualStyleBackColor = true;
            this.buttonBrowseRequest.Click += new System.EventHandler(this.buttonBrowseRequest_Click);
            // 
            // textBoxXSLTRequest
            // 
            this.textBoxXSLTRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLTRequest.Location = new System.Drawing.Point(12, 42);
            this.textBoxXSLTRequest.Name = "textBoxXSLTRequest";
            this.textBoxXSLTRequest.Size = new System.Drawing.Size(368, 20);
            this.textBoxXSLTRequest.TabIndex = 24;
            // 
            // checkBoxXSLTRequest
            // 
            this.checkBoxXSLTRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLTRequest.Location = new System.Drawing.Point(12, 7);
            this.checkBoxXSLTRequest.Name = "checkBoxXSLTRequest";
            this.checkBoxXSLTRequest.Size = new System.Drawing.Size(474, 26);
            this.checkBoxXSLTRequest.TabIndex = 23;
            this.checkBoxXSLTRequest.Text = "Use the following XSLT file to transform requesting message before {0}.";
            this.checkBoxXSLTRequest.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelativePath
            // 
            this.checkBoxRelativePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxRelativePath.AutoSize = true;
            this.checkBoxRelativePath.Checked = true;
            this.checkBoxRelativePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelativePath.Location = new System.Drawing.Point(21, 217);
            this.checkBoxRelativePath.Name = "checkBoxRelativePath";
            this.checkBoxRelativePath.Size = new System.Drawing.Size(284, 17);
            this.checkBoxRelativePath.TabIndex = 19;
            this.checkBoxRelativePath.Text = "Use relative path when browsing and selecting pathes.";
            this.checkBoxRelativePath.UseVisualStyleBackColor = true;
            // 
            // panelOneWayTransform
            // 
            this.panelOneWayTransform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOneWayTransform.Controls.Add(this.checkBoxXSLT);
            this.panelOneWayTransform.Controls.Add(this.buttonBrowse);
            this.panelOneWayTransform.Controls.Add(this.textBoxXSLT);
            this.panelOneWayTransform.Location = new System.Drawing.Point(9, 12);
            this.panelOneWayTransform.Name = "panelOneWayTransform";
            this.panelOneWayTransform.Size = new System.Drawing.Size(489, 73);
            this.panelOneWayTransform.TabIndex = 2;
            // 
            // checkBoxXSLT
            // 
            this.checkBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLT.Location = new System.Drawing.Point(12, 13);
            this.checkBoxXSLT.Name = "checkBoxXSLT";
            this.checkBoxXSLT.Size = new System.Drawing.Size(474, 26);
            this.checkBoxXSLT.TabIndex = 0;
            this.checkBoxXSLT.Text = "Use the following XSLT file to transform message before {0}.";
            this.checkBoxXSLT.UseVisualStyleBackColor = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(398, 41);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(68, 25);
            this.buttonBrowse.TabIndex = 19;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxXSLT
            // 
            this.textBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLT.Location = new System.Drawing.Point(12, 46);
            this.textBoxXSLT.Name = "textBoxXSLT";
            this.textBoxXSLT.Size = new System.Drawing.Size(368, 20);
            this.textBoxXSLT.TabIndex = 1;
            // 
            // FormEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 348);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormEntity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormEntity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEntity_FormClosing);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageTransform.ResumeLayout(false);
            this.tabPageTransform.PerformLayout();
            this.panelDuplexTransform.ResumeLayout(false);
            this.panelDuplexTransform.PerformLayout();
            this.panelOneWayTransform.ResumeLayout(false);
            this.panelOneWayTransform.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageTransform;
        private System.Windows.Forms.Panel panelDuplexTransform;
        private System.Windows.Forms.CheckBox checkBoxRelativePath;
        private System.Windows.Forms.Panel panelOneWayTransform;
        private System.Windows.Forms.CheckBox checkBoxXSLT;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxXSLT;
        private System.Windows.Forms.Button buttonBrowseResponse;
        private System.Windows.Forms.TextBox textBoxXSLTResponse;
        private System.Windows.Forms.CheckBox checkBoxXSLTResponse;
        private System.Windows.Forms.Button buttonBrowseRequest;
        private System.Windows.Forms.TextBox textBoxXSLTRequest;
        private System.Windows.Forms.CheckBox checkBoxXSLTRequest;
    }
}