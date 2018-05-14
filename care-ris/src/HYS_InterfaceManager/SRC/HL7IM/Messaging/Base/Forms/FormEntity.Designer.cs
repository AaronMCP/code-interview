namespace HYS.IM.Messaging.Base.Forms
{
    partial class FormEntity<A, E>
        where A : EntryAttribute
        where E : IEntry
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
            this.labelDesc = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxClassName = new System.Windows.Forms.ComboBox();
            this.textBoxEntityDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxEntityName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxEntityID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelStep1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxRelativePath = new System.Windows.Forms.CheckBox();
            this.buttonConfigBrowse = new System.Windows.Forms.Button();
            this.textBoxConfigPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAssemblyBrowse = new System.Windows.Forms.Button();
            this.textBoxAssemblyLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelStep2 = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            this.panelStep1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelStep2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(450, 370);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 27);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(367, 370);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(74, 27);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "Next";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelDesc
            // 
            this.labelDesc.Location = new System.Drawing.Point(21, 29);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(101, 20);
            this.labelDesc.TabIndex = 7;
            this.labelDesc.Text = "Class Name:";
            this.labelDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBoxClassName);
            this.groupBox2.Controls.Add(this.labelDesc);
            this.groupBox2.Controls.Add(this.textBoxEntityDescription);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxEntityName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxEntityID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(7, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(508, 164);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entity Information";
            // 
            // comboBoxClassName
            // 
            this.comboBoxClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassName.FormattingEnabled = true;
            this.comboBoxClassName.Location = new System.Drawing.Point(119, 28);
            this.comboBoxClassName.Name = "comboBoxClassName";
            this.comboBoxClassName.Size = new System.Drawing.Size(373, 21);
            this.comboBoxClassName.TabIndex = 16;
            this.comboBoxClassName.SelectedIndexChanged += new System.EventHandler(this.comboBoxClassName_SelectedIndexChanged);
            // 
            // textBoxEntityDescription
            // 
            this.textBoxEntityDescription.Location = new System.Drawing.Point(119, 109);
            this.textBoxEntityDescription.Multiline = true;
            this.textBoxEntityDescription.Name = "textBoxEntityDescription";
            this.textBoxEntityDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEntityDescription.Size = new System.Drawing.Size(373, 41);
            this.textBoxEntityDescription.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(21, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Entity Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxEntityName
            // 
            this.textBoxEntityName.Location = new System.Drawing.Point(119, 83);
            this.textBoxEntityName.Name = "textBoxEntityName";
            this.textBoxEntityName.Size = new System.Drawing.Size(373, 20);
            this.textBoxEntityName.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Entity Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxEntityID
            // 
            this.textBoxEntityID.Location = new System.Drawing.Point(119, 57);
            this.textBoxEntityID.Name = "textBoxEntityID";
            this.textBoxEntityID.ReadOnly = true;
            this.textBoxEntityID.Size = new System.Drawing.Size(373, 20);
            this.textBoxEntityID.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(21, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Entity ID:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStep1
            // 
            this.panelStep1.Controls.Add(this.groupBox1);
            this.panelStep1.Location = new System.Drawing.Point(9, 1);
            this.panelStep1.Name = "panelStep1";
            this.panelStep1.Size = new System.Drawing.Size(522, 179);
            this.panelStep1.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxRelativePath);
            this.groupBox1.Controls.Add(this.buttonConfigBrowse);
            this.groupBox1.Controls.Add(this.textBoxConfigPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonAssemblyBrowse);
            this.groupBox1.Controls.Add(this.textBoxAssemblyLocation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 157);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Assembly Confiugration";
            // 
            // checkBoxRelativePath
            // 
            this.checkBoxRelativePath.AutoSize = true;
            this.checkBoxRelativePath.Checked = true;
            this.checkBoxRelativePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelativePath.Location = new System.Drawing.Point(22, 129);
            this.checkBoxRelativePath.Name = "checkBoxRelativePath";
            this.checkBoxRelativePath.Size = new System.Drawing.Size(284, 17);
            this.checkBoxRelativePath.TabIndex = 15;
            this.checkBoxRelativePath.Text = "Use relative path when browsing and selecting pathes.";
            this.checkBoxRelativePath.UseVisualStyleBackColor = true;
            // 
            // buttonConfigBrowse
            // 
            this.buttonConfigBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfigBrowse.Location = new System.Drawing.Point(419, 89);
            this.buttonConfigBrowse.Name = "buttonConfigBrowse";
            this.buttonConfigBrowse.Size = new System.Drawing.Size(70, 28);
            this.buttonConfigBrowse.TabIndex = 13;
            this.buttonConfigBrowse.Text = "Browse";
            this.buttonConfigBrowse.UseVisualStyleBackColor = true;
            this.buttonConfigBrowse.Click += new System.EventHandler(this.buttonConfigBrowse_Click);
            // 
            // textBoxConfigPath
            // 
            this.textBoxConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfigPath.Location = new System.Drawing.Point(23, 97);
            this.textBoxConfigPath.Name = "textBoxConfigPath";
            this.textBoxConfigPath.Size = new System.Drawing.Size(378, 20);
            this.textBoxConfigPath.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Configuration File Path:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAssemblyBrowse
            // 
            this.buttonAssemblyBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAssemblyBrowse.Location = new System.Drawing.Point(419, 44);
            this.buttonAssemblyBrowse.Name = "buttonAssemblyBrowse";
            this.buttonAssemblyBrowse.Size = new System.Drawing.Size(70, 28);
            this.buttonAssemblyBrowse.TabIndex = 6;
            this.buttonAssemblyBrowse.Text = "Browse";
            this.buttonAssemblyBrowse.UseVisualStyleBackColor = true;
            this.buttonAssemblyBrowse.Click += new System.EventHandler(this.buttonAssemblyBrowse_Click);
            // 
            // textBoxAssemblyLocation
            // 
            this.textBoxAssemblyLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssemblyLocation.Location = new System.Drawing.Point(23, 52);
            this.textBoxAssemblyLocation.Name = "textBoxAssemblyLocation";
            this.textBoxAssemblyLocation.Size = new System.Drawing.Size(378, 20);
            this.textBoxAssemblyLocation.TabIndex = 1;
            this.textBoxAssemblyLocation.TextChanged += new System.EventHandler(this.textBoxAssemblyLocation_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Assembly Location:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStep2
            // 
            this.panelStep2.Controls.Add(this.groupBox2);
            this.panelStep2.Location = new System.Drawing.Point(9, 180);
            this.panelStep2.Name = "panelStep2";
            this.panelStep2.Size = new System.Drawing.Size(522, 179);
            this.panelStep2.TabIndex = 18;
            this.panelStep2.Visible = false;
            // 
            // FormEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 409);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panelStep2);
            this.Controls.Add(this.panelStep1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEntity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Message Entity";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelStep1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelStep2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxEntityName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxEntityID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEntityDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxClassName;
        private System.Windows.Forms.Panel panelStep1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonConfigBrowse;
        private System.Windows.Forms.TextBox textBoxConfigPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAssemblyBrowse;
        private System.Windows.Forms.TextBox textBoxAssemblyLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelStep2;
        private System.Windows.Forms.CheckBox checkBoxRelativePath;
    }
}