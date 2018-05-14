namespace HYS.IM.Messaging.Composer
{
    partial class FormMInterfaceConfigPage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewSection = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSectionEdit = new System.Windows.Forms.Button();
            this.buttonSectionDelete = new System.Windows.Forms.Button();
            this.buttonSectionAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxConfigFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelSection = new System.Windows.Forms.Panel();
            this.panelSection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(17, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 2);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(112, 38);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(371, 20);
            this.textBoxDescription.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 62;
            this.label1.Text = "Page Description:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(112, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(371, 20);
            this.textBoxName.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 20);
            this.label6.TabIndex = 60;
            this.label6.Text = "Page Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Cutomized Configuration",
            "Message Box Retry Configuration",
            "Message Box Garbage Collection Configuration",
            "Message Box Retry And Garbage Collection Configuration"});
            this.comboBoxType.Location = new System.Drawing.Point(112, 80);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(297, 21);
            this.comboBoxType.TabIndex = 66;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "Page Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 30);
            this.label3.TabIndex = 67;
            this.label3.Text = "Configuration Sections:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewSection
            // 
            this.listViewSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.listViewSection.FullRowSelect = true;
            this.listViewSection.HideSelection = false;
            this.listViewSection.Location = new System.Drawing.Point(8, 4);
            this.listViewSection.MultiSelect = false;
            this.listViewSection.Name = "listViewSection";
            this.listViewSection.Size = new System.Drawing.Size(297, 87);
            this.listViewSection.TabIndex = 68;
            this.listViewSection.UseCompatibleStateImageBehavior = false;
            this.listViewSection.View = System.Windows.Forms.View.Details;
            this.listViewSection.SelectedIndexChanged += new System.EventHandler(this.listViewSection_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Section Name";
            this.columnHeader5.Width = 133;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            this.columnHeader6.Width = 127;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(19, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 2);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            // 
            // buttonSectionEdit
            // 
            this.buttonSectionEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSectionEdit.Location = new System.Drawing.Point(315, 35);
            this.buttonSectionEdit.Name = "buttonSectionEdit";
            this.buttonSectionEdit.Size = new System.Drawing.Size(64, 25);
            this.buttonSectionEdit.TabIndex = 72;
            this.buttonSectionEdit.Text = "Edit";
            this.buttonSectionEdit.UseVisualStyleBackColor = true;
            this.buttonSectionEdit.Click += new System.EventHandler(this.buttonSectionEdit_Click);
            // 
            // buttonSectionDelete
            // 
            this.buttonSectionDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSectionDelete.Location = new System.Drawing.Point(315, 66);
            this.buttonSectionDelete.Name = "buttonSectionDelete";
            this.buttonSectionDelete.Size = new System.Drawing.Size(64, 25);
            this.buttonSectionDelete.TabIndex = 71;
            this.buttonSectionDelete.Text = "Delete";
            this.buttonSectionDelete.UseVisualStyleBackColor = true;
            this.buttonSectionDelete.Click += new System.EventHandler(this.buttonSectionDelete_Click);
            // 
            // buttonSectionAdd
            // 
            this.buttonSectionAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSectionAdd.Location = new System.Drawing.Point(315, 4);
            this.buttonSectionAdd.Name = "buttonSectionAdd";
            this.buttonSectionAdd.Size = new System.Drawing.Size(64, 25);
            this.buttonSectionAdd.TabIndex = 70;
            this.buttonSectionAdd.Text = "Add";
            this.buttonSectionAdd.UseVisualStyleBackColor = true;
            this.buttonSectionAdd.Click += new System.EventHandler(this.buttonSectionAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(400, 257);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 25);
            this.buttonCancel.TabIndex = 74;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(308, 257);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 25);
            this.buttonOK.TabIndex = 73;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(419, 102);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(64, 25);
            this.buttonBrowse.TabIndex = 77;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxConfigFile
            // 
            this.textBoxConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfigFile.Location = new System.Drawing.Point(112, 107);
            this.textBoxConfigFile.Name = "textBoxConfigFile";
            this.textBoxConfigFile.Size = new System.Drawing.Size(297, 20);
            this.textBoxConfigFile.TabIndex = 76;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 75;
            this.label4.Text = "Configuration File:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSection
            // 
            this.panelSection.Controls.Add(this.buttonSectionEdit);
            this.panelSection.Controls.Add(this.buttonSectionDelete);
            this.panelSection.Controls.Add(this.buttonSectionAdd);
            this.panelSection.Controls.Add(this.listViewSection);
            this.panelSection.Location = new System.Drawing.Point(104, 135);
            this.panelSection.Name = "panelSection";
            this.panelSection.Size = new System.Drawing.Size(390, 104);
            this.panelSection.TabIndex = 78;
            // 
            // FormMInterfaceConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(505, 294);
            this.Controls.Add(this.panelSection);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxConfigFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMInterfaceConfigPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormMInterfaceConfig";
            this.panelSection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewSection;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSectionEdit;
        private System.Windows.Forms.Button buttonSectionDelete;
        private System.Windows.Forms.Button buttonSectionAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxConfigFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelSection;
    }
}