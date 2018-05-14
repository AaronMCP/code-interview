namespace HYS.XmlAdapter.Common.Forms
{
    partial class FormField
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
            this.groupBoxGateway = new System.Windows.Forms.GroupBox();
            this.comboBoxLUT = new System.Windows.Forms.ComboBox();
            this.checkBoxLUT = new System.Windows.Forms.CheckBox();
            this.textBoxFixValue = new System.Windows.Forms.TextBox();
            this.comboBoxField = new System.Windows.Forms.ComboBox();
            this.checkBoxFixValue = new System.Windows.Forms.CheckBox();
            this.comboBoxTable = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxRedundancy = new System.Windows.Forms.CheckBox();
            this.groupBoxGateway.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGateway
            // 
            this.groupBoxGateway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGateway.Controls.Add(this.comboBoxLUT);
            this.groupBoxGateway.Controls.Add(this.checkBoxLUT);
            this.groupBoxGateway.Controls.Add(this.textBoxFixValue);
            this.groupBoxGateway.Controls.Add(this.comboBoxField);
            this.groupBoxGateway.Controls.Add(this.checkBoxFixValue);
            this.groupBoxGateway.Controls.Add(this.comboBoxTable);
            this.groupBoxGateway.Controls.Add(this.label8);
            this.groupBoxGateway.Controls.Add(this.label9);
            this.groupBoxGateway.Location = new System.Drawing.Point(15, 12);
            this.groupBoxGateway.Name = "groupBoxGateway";
            this.groupBoxGateway.Size = new System.Drawing.Size(312, 167);
            this.groupBoxGateway.TabIndex = 29;
            this.groupBoxGateway.TabStop = false;
            this.groupBoxGateway.Text = "GC Gateway Field";
            // 
            // comboBoxLUT
            // 
            this.comboBoxLUT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLUT.Enabled = false;
            this.comboBoxLUT.FormattingEnabled = true;
            this.comboBoxLUT.Location = new System.Drawing.Point(123, 128);
            this.comboBoxLUT.MaxDropDownItems = 30;
            this.comboBoxLUT.Name = "comboBoxLUT";
            this.comboBoxLUT.Size = new System.Drawing.Size(165, 21);
            this.comboBoxLUT.TabIndex = 14;
            // 
            // checkBoxLUT
            // 
            this.checkBoxLUT.AutoSize = true;
            this.checkBoxLUT.Location = new System.Drawing.Point(26, 132);
            this.checkBoxLUT.Name = "checkBoxLUT";
            this.checkBoxLUT.Size = new System.Drawing.Size(95, 17);
            this.checkBoxLUT.TabIndex = 14;
            this.checkBoxLUT.Text = "Lookup Table:";
            this.checkBoxLUT.UseVisualStyleBackColor = true;
            // 
            // textBoxFixValue
            // 
            this.textBoxFixValue.Enabled = false;
            this.textBoxFixValue.Location = new System.Drawing.Point(123, 101);
            this.textBoxFixValue.Name = "textBoxFixValue";
            this.textBoxFixValue.Size = new System.Drawing.Size(164, 20);
            this.textBoxFixValue.TabIndex = 13;
            // 
            // comboBoxField
            // 
            this.comboBoxField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxField.Enabled = false;
            this.comboBoxField.FormattingEnabled = true;
            this.comboBoxField.Location = new System.Drawing.Point(123, 58);
            this.comboBoxField.MaxDropDownItems = 30;
            this.comboBoxField.Name = "comboBoxField";
            this.comboBoxField.Size = new System.Drawing.Size(165, 21);
            this.comboBoxField.TabIndex = 7;
            // 
            // checkBoxFixValue
            // 
            this.checkBoxFixValue.AutoSize = true;
            this.checkBoxFixValue.Location = new System.Drawing.Point(26, 104);
            this.checkBoxFixValue.Name = "checkBoxFixValue";
            this.checkBoxFixValue.Size = new System.Drawing.Size(72, 17);
            this.checkBoxFixValue.TabIndex = 12;
            this.checkBoxFixValue.Text = "Fix Value:";
            this.checkBoxFixValue.UseVisualStyleBackColor = true;
            // 
            // comboBoxTable
            // 
            this.comboBoxTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTable.FormattingEnabled = true;
            this.comboBoxTable.Location = new System.Drawing.Point(123, 31);
            this.comboBoxTable.MaxDropDownItems = 30;
            this.comboBoxTable.Name = "comboBoxTable";
            this.comboBoxTable.Size = new System.Drawing.Size(165, 21);
            this.comboBoxTable.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(23, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Field:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(23, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Table:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(147, 192);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(240, 192);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxRedundancy
            // 
            this.checkBoxRedundancy.AutoSize = true;
            this.checkBoxRedundancy.Location = new System.Drawing.Point(15, 198);
            this.checkBoxRedundancy.Name = "checkBoxRedundancy";
            this.checkBoxRedundancy.Size = new System.Drawing.Size(121, 17);
            this.checkBoxRedundancy.TabIndex = 32;
            this.checkBoxRedundancy.Text = "Check Redundancy";
            this.checkBoxRedundancy.UseVisualStyleBackColor = true;
            // 
            // FormField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 231);
            this.Controls.Add(this.checkBoxRedundancy);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxGateway);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormField";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GC Gateway Field";
            this.Load += new System.EventHandler(this.FormField_Load);
            this.groupBoxGateway.ResumeLayout(false);
            this.groupBoxGateway.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGateway;
        private System.Windows.Forms.ComboBox comboBoxLUT;
        private System.Windows.Forms.CheckBox checkBoxLUT;
        private System.Windows.Forms.TextBox textBoxFixValue;
        private System.Windows.Forms.ComboBox comboBoxField;
        private System.Windows.Forms.CheckBox checkBoxFixValue;
        private System.Windows.Forms.ComboBox comboBoxTable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxRedundancy;
    }
}