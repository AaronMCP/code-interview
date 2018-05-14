namespace HYS.IM.Messaging.Config
{
    partial class FormOneWayProcess
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
            this.checkBoxRelativePath = new System.Windows.Forms.CheckBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxXSLT = new System.Windows.Forms.TextBox();
            this.checkBoxXSLT = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxXSLT.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxXSLT
            // 
            this.groupBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxXSLT.Controls.Add(this.checkBoxRelativePath);
            this.groupBoxXSLT.Controls.Add(this.buttonBrowse);
            this.groupBoxXSLT.Controls.Add(this.textBoxXSLT);
            this.groupBoxXSLT.Controls.Add(this.checkBoxXSLT);
            this.groupBoxXSLT.Location = new System.Drawing.Point(12, 12);
            this.groupBoxXSLT.Name = "groupBoxXSLT";
            this.groupBoxXSLT.Size = new System.Drawing.Size(471, 126);
            this.groupBoxXSLT.TabIndex = 0;
            this.groupBoxXSLT.TabStop = false;
            this.groupBoxXSLT.Text = "Message Transformation";
            // 
            // checkBoxRelativePath
            // 
            this.checkBoxRelativePath.AutoSize = true;
            this.checkBoxRelativePath.Checked = true;
            this.checkBoxRelativePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelativePath.Location = new System.Drawing.Point(15, 87);
            this.checkBoxRelativePath.Name = "checkBoxRelativePath";
            this.checkBoxRelativePath.Size = new System.Drawing.Size(284, 17);
            this.checkBoxRelativePath.TabIndex = 19;
            this.checkBoxRelativePath.Text = "Use relative path when browsing and selecting pathes.";
            this.checkBoxRelativePath.UseVisualStyleBackColor = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(385, 56);
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
            this.textBoxXSLT.Location = new System.Drawing.Point(15, 61);
            this.textBoxXSLT.Name = "textBoxXSLT";
            this.textBoxXSLT.Size = new System.Drawing.Size(356, 20);
            this.textBoxXSLT.TabIndex = 1;
            // 
            // checkBoxXSLT
            // 
            this.checkBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLT.Location = new System.Drawing.Point(15, 28);
            this.checkBoxXSLT.Name = "checkBoxXSLT";
            this.checkBoxXSLT.Size = new System.Drawing.Size(438, 26);
            this.checkBoxXSLT.TabIndex = 0;
            this.checkBoxXSLT.Text = "Use the following XSLT file to transform message before {0}.";
            this.checkBoxXSLT.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(398, 153);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 25);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(306, 153);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 25);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormOneWayProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 190);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxXSLT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOneWayProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Processing Setting";
            this.groupBoxXSLT.ResumeLayout(false);
            this.groupBoxXSLT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxXSLT;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxXSLT;
        private System.Windows.Forms.TextBox textBoxXSLT;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.CheckBox checkBoxRelativePath;
    }
}