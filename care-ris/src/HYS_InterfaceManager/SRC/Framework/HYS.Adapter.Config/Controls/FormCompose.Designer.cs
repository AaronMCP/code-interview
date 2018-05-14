namespace HYS.Adapter.Config.Controls
{
    partial class FormCompose
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
            this.groupBoxField = new System.Windows.Forms.GroupBox();
            this.buttonEditField = new System.Windows.Forms.Button();
            this.buttonDeleteField = new System.Windows.Forms.Button();
            this.buttonAddField = new System.Windows.Forms.Button();
            this.listViewFields = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.groupBoxRule = new System.Windows.Forms.GroupBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxField = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxField.SuspendLayout();
            this.groupBoxRule.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxField
            // 
            this.groupBoxField.Controls.Add(this.buttonEditField);
            this.groupBoxField.Controls.Add(this.buttonDeleteField);
            this.groupBoxField.Controls.Add(this.buttonAddField);
            this.groupBoxField.Controls.Add(this.listViewFields);
            this.groupBoxField.Location = new System.Drawing.Point(12, 13);
            this.groupBoxField.Name = "groupBoxField";
            this.groupBoxField.Size = new System.Drawing.Size(380, 173);
            this.groupBoxField.TabIndex = 0;
            this.groupBoxField.TabStop = false;
            this.groupBoxField.Text = "Source Fields";
            // 
            // buttonEditField
            // 
            this.buttonEditField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditField.Location = new System.Drawing.Point(293, 63);
            this.buttonEditField.Name = "buttonEditField";
            this.buttonEditField.Size = new System.Drawing.Size(66, 30);
            this.buttonEditField.TabIndex = 25;
            this.buttonEditField.Text = "Edit";
            this.buttonEditField.UseVisualStyleBackColor = true;
            this.buttonEditField.Click += new System.EventHandler(this.buttonEditField_Click);
            // 
            // buttonDeleteField
            // 
            this.buttonDeleteField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteField.Location = new System.Drawing.Point(293, 99);
            this.buttonDeleteField.Name = "buttonDeleteField";
            this.buttonDeleteField.Size = new System.Drawing.Size(66, 30);
            this.buttonDeleteField.TabIndex = 24;
            this.buttonDeleteField.Text = "Delete";
            this.buttonDeleteField.UseVisualStyleBackColor = true;
            this.buttonDeleteField.Click += new System.EventHandler(this.buttonDeleteField_Click);
            // 
            // buttonAddField
            // 
            this.buttonAddField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddField.Location = new System.Drawing.Point(293, 27);
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.Size = new System.Drawing.Size(66, 30);
            this.buttonAddField.TabIndex = 23;
            this.buttonAddField.Text = "Add";
            this.buttonAddField.UseVisualStyleBackColor = true;
            this.buttonAddField.Click += new System.EventHandler(this.buttonAddField_Click);
            // 
            // listViewFields
            // 
            this.listViewFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader5});
            this.listViewFields.FullRowSelect = true;
            this.listViewFields.HideSelection = false;
            this.listViewFields.Location = new System.Drawing.Point(19, 27);
            this.listViewFields.MultiSelect = false;
            this.listViewFields.Name = "listViewFields";
            this.listViewFields.Size = new System.Drawing.Size(262, 127);
            this.listViewFields.TabIndex = 22;
            this.listViewFields.UseCompatibleStateImageBehavior = false;
            this.listViewFields.View = System.Windows.Forms.View.Details;
            this.listViewFields.DoubleClick += new System.EventHandler(this.listViewFields_DoubleClick);
            this.listViewFields.SelectedIndexChanged += new System.EventHandler(this.listViewFields_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Label";
            this.columnHeader6.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "GC Gateway Field";
            this.columnHeader5.Width = 175;
            // 
            // groupBoxRule
            // 
            this.groupBoxRule.Controls.Add(this.textBoxDescription);
            this.groupBoxRule.Controls.Add(this.label2);
            this.groupBoxRule.Controls.Add(this.textBoxPattern);
            this.groupBoxRule.Controls.Add(this.label1);
            this.groupBoxRule.Controls.Add(this.comboBoxField);
            this.groupBoxRule.Controls.Add(this.label8);
            this.groupBoxRule.Location = new System.Drawing.Point(12, 192);
            this.groupBoxRule.Name = "groupBoxRule";
            this.groupBoxRule.Size = new System.Drawing.Size(380, 164);
            this.groupBoxRule.TabIndex = 1;
            this.groupBoxRule.TabStop = false;
            this.groupBoxRule.Text = "Composing Rule";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(116, 128);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(243, 20);
            this.textBoxDescription.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Description:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.AcceptsReturn = true;
            this.textBoxPattern.AcceptsTab = true;
            this.textBoxPattern.Location = new System.Drawing.Point(116, 52);
            this.textBoxPattern.Multiline = true;
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPattern.Size = new System.Drawing.Size(243, 70);
            this.textBoxPattern.TabIndex = 13;
            this.textBoxPattern.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Compose Pattern:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxField
            // 
            this.comboBoxField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxField.FormattingEnabled = true;
            this.comboBoxField.Location = new System.Drawing.Point(116, 25);
            this.comboBoxField.MaxDropDownItems = 30;
            this.comboBoxField.Name = "comboBoxField";
            this.comboBoxField.Size = new System.Drawing.Size(243, 21);
            this.comboBoxField.TabIndex = 11;
            this.comboBoxField.SelectedIndexChanged += new System.EventHandler(this.comboBoxField_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Target Field:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(212, 365);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 36;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(305, 365);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 35;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormCompose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 404);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxRule);
            this.Controls.Add(this.groupBoxField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCompose";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormCompose";
            this.groupBoxField.ResumeLayout(false);
            this.groupBoxRule.ResumeLayout(false);
            this.groupBoxRule.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxField;
        private System.Windows.Forms.GroupBox groupBoxRule;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listViewFields;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button buttonEditField;
        private System.Windows.Forms.Button buttonDeleteField;
        private System.Windows.Forms.Button buttonAddField;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
    }
}