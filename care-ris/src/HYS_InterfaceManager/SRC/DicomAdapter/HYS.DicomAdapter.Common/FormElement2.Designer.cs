namespace HYS.DicomAdapter.Common
{
    partial class FormElement2<T>
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
            this.checkBoxRedundancy = new System.Windows.Forms.CheckBox();
            this.groupBoxGateway = new System.Windows.Forms.GroupBox();
            this.comboBoxLUT = new System.Windows.Forms.ComboBox();
            this.checkBoxLUT = new System.Windows.Forms.CheckBox();
            this.textBoxFixValue = new System.Windows.Forms.TextBox();
            this.comboBoxField = new System.Windows.Forms.ComboBox();
            this.checkBoxFixValue = new System.Windows.Forms.CheckBox();
            this.comboBoxTable = new System.Windows.Forms.ComboBox();
            this.labelField = new System.Windows.Forms.Label();
            this.labelTable = new System.Windows.Forms.Label();
            this.groupBoxDicom = new System.Windows.Forms.GroupBox();
            this.textBoxListGroup = new System.Windows.Forms.TextBox();
            this.labelCatalog = new System.Windows.Forms.Label();
            this.textBoxElementNum = new System.Windows.Forms.TextBox();
            this.labelElement = new System.Windows.Forms.Label();
            this.textBoxGroupNum = new System.Windows.Forms.TextBox();
            this.labelGroup = new System.Windows.Forms.Label();
            this.comboBoxVR = new System.Windows.Forms.ComboBox();
            this.comboBoxTag = new System.Windows.Forms.ComboBox();
            this.labelVR = new System.Windows.Forms.Label();
            this.labelTag = new System.Windows.Forms.Label();
            this.labelGroupNum = new System.Windows.Forms.Label();
            this.labelElementNum = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDirection = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxGateway.SuspendLayout();
            this.groupBoxDicom.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxRedundancy
            // 
            this.checkBoxRedundancy.AutoSize = true;
            this.checkBoxRedundancy.Location = new System.Drawing.Point(26, 161);
            this.checkBoxRedundancy.Name = "checkBoxRedundancy";
            this.checkBoxRedundancy.Size = new System.Drawing.Size(121, 17);
            this.checkBoxRedundancy.TabIndex = 8;
            this.checkBoxRedundancy.Text = "Check Redundancy";
            this.checkBoxRedundancy.UseVisualStyleBackColor = true;
            // 
            // groupBoxGateway
            // 
            this.groupBoxGateway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGateway.Controls.Add(this.checkBoxRedundancy);
            this.groupBoxGateway.Controls.Add(this.comboBoxLUT);
            this.groupBoxGateway.Controls.Add(this.checkBoxLUT);
            this.groupBoxGateway.Controls.Add(this.textBoxFixValue);
            this.groupBoxGateway.Controls.Add(this.comboBoxField);
            this.groupBoxGateway.Controls.Add(this.checkBoxFixValue);
            this.groupBoxGateway.Controls.Add(this.comboBoxTable);
            this.groupBoxGateway.Controls.Add(this.labelField);
            this.groupBoxGateway.Controls.Add(this.labelTable);
            this.groupBoxGateway.Location = new System.Drawing.Point(337, 11);
            this.groupBoxGateway.Name = "groupBoxGateway";
            this.groupBoxGateway.Size = new System.Drawing.Size(318, 195);
            this.groupBoxGateway.TabIndex = 1;
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
            this.comboBoxLUT.TabIndex = 7;
            // 
            // checkBoxLUT
            // 
            this.checkBoxLUT.AutoSize = true;
            this.checkBoxLUT.Location = new System.Drawing.Point(26, 132);
            this.checkBoxLUT.Name = "checkBoxLUT";
            this.checkBoxLUT.Size = new System.Drawing.Size(95, 17);
            this.checkBoxLUT.TabIndex = 6;
            this.checkBoxLUT.Text = "Lookup Table:";
            this.checkBoxLUT.UseVisualStyleBackColor = true;
            // 
            // textBoxFixValue
            // 
            this.textBoxFixValue.Enabled = false;
            this.textBoxFixValue.Location = new System.Drawing.Point(123, 101);
            this.textBoxFixValue.Name = "textBoxFixValue";
            this.textBoxFixValue.Size = new System.Drawing.Size(164, 20);
            this.textBoxFixValue.TabIndex = 5;
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
            this.comboBoxField.TabIndex = 1;
            // 
            // checkBoxFixValue
            // 
            this.checkBoxFixValue.AutoSize = true;
            this.checkBoxFixValue.Location = new System.Drawing.Point(26, 104);
            this.checkBoxFixValue.Name = "checkBoxFixValue";
            this.checkBoxFixValue.Size = new System.Drawing.Size(72, 17);
            this.checkBoxFixValue.TabIndex = 4;
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
            this.comboBoxTable.TabIndex = 0;
            // 
            // labelField
            // 
            this.labelField.Location = new System.Drawing.Point(23, 59);
            this.labelField.Name = "labelField";
            this.labelField.Size = new System.Drawing.Size(88, 20);
            this.labelField.TabIndex = 2;
            this.labelField.Text = "Field:";
            this.labelField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTable
            // 
            this.labelTable.Location = new System.Drawing.Point(23, 32);
            this.labelTable.Name = "labelTable";
            this.labelTable.Size = new System.Drawing.Size(88, 20);
            this.labelTable.TabIndex = 0;
            this.labelTable.Text = "Table:";
            this.labelTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxDicom
            // 
            this.groupBoxDicom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDicom.Controls.Add(this.textBoxListGroup);
            this.groupBoxDicom.Controls.Add(this.labelCatalog);
            this.groupBoxDicom.Controls.Add(this.textBoxElementNum);
            this.groupBoxDicom.Controls.Add(this.labelElement);
            this.groupBoxDicom.Controls.Add(this.textBoxGroupNum);
            this.groupBoxDicom.Controls.Add(this.labelGroup);
            this.groupBoxDicom.Controls.Add(this.comboBoxVR);
            this.groupBoxDicom.Controls.Add(this.comboBoxTag);
            this.groupBoxDicom.Controls.Add(this.labelVR);
            this.groupBoxDicom.Controls.Add(this.labelTag);
            this.groupBoxDicom.Controls.Add(this.labelGroupNum);
            this.groupBoxDicom.Controls.Add(this.labelElementNum);
            this.groupBoxDicom.Location = new System.Drawing.Point(11, 11);
            this.groupBoxDicom.Name = "groupBoxDicom";
            this.groupBoxDicom.Size = new System.Drawing.Size(296, 195);
            this.groupBoxDicom.TabIndex = 0;
            this.groupBoxDicom.TabStop = false;
            this.groupBoxDicom.Text = "DICOM Element";
            // 
            // textBoxListGroup
            // 
            this.textBoxListGroup.Location = new System.Drawing.Point(85, 161);
            this.textBoxListGroup.Name = "textBoxListGroup";
            this.textBoxListGroup.Size = new System.Drawing.Size(190, 20);
            this.textBoxListGroup.TabIndex = 4;
            // 
            // labelCatalog
            // 
            this.labelCatalog.Location = new System.Drawing.Point(23, 161);
            this.labelCatalog.Name = "labelCatalog";
            this.labelCatalog.Size = new System.Drawing.Size(88, 20);
            this.labelCatalog.TabIndex = 12;
            this.labelCatalog.Text = "Belong To:";
            this.labelCatalog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxElementNum
            // 
            this.textBoxElementNum.Location = new System.Drawing.Point(190, 91);
            this.textBoxElementNum.MaxLength = 4;
            this.textBoxElementNum.Name = "textBoxElementNum";
            this.textBoxElementNum.Size = new System.Drawing.Size(84, 20);
            this.textBoxElementNum.TabIndex = 2;
            // 
            // labelElement
            // 
            this.labelElement.Location = new System.Drawing.Point(82, 91);
            this.labelElement.Name = "labelElement";
            this.labelElement.Size = new System.Drawing.Size(88, 20);
            this.labelElement.TabIndex = 9;
            this.labelElement.Text = "Element Number:";
            this.labelElement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxGroupNum
            // 
            this.textBoxGroupNum.Location = new System.Drawing.Point(190, 65);
            this.textBoxGroupNum.MaxLength = 4;
            this.textBoxGroupNum.Name = "textBoxGroupNum";
            this.textBoxGroupNum.Size = new System.Drawing.Size(84, 20);
            this.textBoxGroupNum.TabIndex = 1;
            // 
            // labelGroup
            // 
            this.labelGroup.Location = new System.Drawing.Point(82, 65);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(88, 20);
            this.labelGroup.TabIndex = 2;
            this.labelGroup.Text = "Group Number:";
            this.labelGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxVR
            // 
            this.comboBoxVR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVR.FormattingEnabled = true;
            this.comboBoxVR.Location = new System.Drawing.Point(85, 128);
            this.comboBoxVR.MaxDropDownItems = 30;
            this.comboBoxVR.Name = "comboBoxVR";
            this.comboBoxVR.Size = new System.Drawing.Size(190, 21);
            this.comboBoxVR.TabIndex = 3;
            // 
            // comboBoxTag
            // 
            this.comboBoxTag.FormattingEnabled = true;
            this.comboBoxTag.Location = new System.Drawing.Point(85, 31);
            this.comboBoxTag.MaxDropDownItems = 30;
            this.comboBoxTag.Name = "comboBoxTag";
            this.comboBoxTag.Size = new System.Drawing.Size(190, 21);
            this.comboBoxTag.TabIndex = 0;
            // 
            // labelVR
            // 
            this.labelVR.Location = new System.Drawing.Point(23, 127);
            this.labelVR.Name = "labelVR";
            this.labelVR.Size = new System.Drawing.Size(88, 20);
            this.labelVR.TabIndex = 2;
            this.labelVR.Text = "VR Type:";
            this.labelVR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTag
            // 
            this.labelTag.Location = new System.Drawing.Point(23, 30);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(88, 20);
            this.labelTag.TabIndex = 0;
            this.labelTag.Text = "Tag:";
            this.labelTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGroupNum
            // 
            this.labelGroupNum.Location = new System.Drawing.Point(171, 65);
            this.labelGroupNum.Name = "labelGroupNum";
            this.labelGroupNum.Size = new System.Drawing.Size(28, 20);
            this.labelGroupNum.TabIndex = 3;
            this.labelGroupNum.Text = "0x";
            this.labelGroupNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelElementNum
            // 
            this.labelElementNum.Location = new System.Drawing.Point(171, 91);
            this.labelElementNum.Name = "labelElementNum";
            this.labelElementNum.Size = new System.Drawing.Size(28, 20);
            this.labelElementNum.TabIndex = 11;
            this.labelElementNum.Text = "0x";
            this.labelElementNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(566, 221);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDirection
            // 
            this.labelDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirection.Location = new System.Drawing.Point(311, 89);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(26, 20);
            this.labelDirection.TabIndex = 35;
            this.labelDirection.Text = "-->";
            this.labelDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(464, 221);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormElement2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 258);
            this.Controls.Add(this.groupBoxGateway);
            this.Controls.Add(this.groupBoxDicom);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelDirection);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormElement2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormElement2";
            this.Load += new System.EventHandler(this.FormQCElement_Load);
            this.groupBoxGateway.ResumeLayout(false);
            this.groupBoxGateway.PerformLayout();
            this.groupBoxDicom.ResumeLayout(false);
            this.groupBoxDicom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckBox checkBoxRedundancy;
        public System.Windows.Forms.GroupBox groupBoxGateway;
        public System.Windows.Forms.ComboBox comboBoxLUT;
        public System.Windows.Forms.CheckBox checkBoxLUT;
        public System.Windows.Forms.TextBox textBoxFixValue;
        public System.Windows.Forms.ComboBox comboBoxField;
        public System.Windows.Forms.CheckBox checkBoxFixValue;
        public System.Windows.Forms.ComboBox comboBoxTable;
        public System.Windows.Forms.Label labelField;
        public System.Windows.Forms.Label labelTable;
        public System.Windows.Forms.GroupBox groupBoxDicom;
        public System.Windows.Forms.TextBox textBoxElementNum;
        public System.Windows.Forms.Label labelElement;
        public System.Windows.Forms.TextBox textBoxGroupNum;
        public System.Windows.Forms.Label labelGroup;
        public System.Windows.Forms.ComboBox comboBoxVR;
        public System.Windows.Forms.ComboBox comboBoxTag;
        public System.Windows.Forms.Label labelVR;
        public System.Windows.Forms.Label labelTag;
        public System.Windows.Forms.Label labelGroupNum;
        public System.Windows.Forms.Label labelElementNum;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelDirection;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.TextBox textBoxListGroup;
        public System.Windows.Forms.Label labelCatalog;
    }
}