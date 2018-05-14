namespace HYS.FileAdapter.FileOutboundAdapterConfiguration.Forms
{
    partial class FQueryResult
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckbSaveContentToFile = new System.Windows.Forms.CheckBox();
            this.tbSectionName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThirdPartyFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.gbTranslation = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTranslationType = new System.Windows.Forms.Label();
            this.cmbbxResult = new System.Windows.Forms.ComboBox();
            this.txtTranslationValue = new System.Windows.Forms.TextBox();
            this.gbRedundancy = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.gbGateway = new System.Windows.Forms.GroupBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.pContain = new System.Windows.Forms.Panel();
            this.pSimpField = new System.Windows.Forms.Panel();
            this.enumCmbbxRedundancy = new EnumComboBox.EnumComboBox();
            this.enumCmbbxTranslation = new EnumComboBox.EnumComboBox();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.enumCmbbxThirdPartyFieldType = new EnumComboBox.EnumComboBox();
            this.groupBox3.SuspendLayout();
            this.gbTranslation.SuspendLayout();
            this.gbRedundancy.SuspendLayout();
            this.gbGateway.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(338, 460);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 22);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(400, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(12, 445);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "(*: Required)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckbSaveContentToFile);
            this.groupBox3.Controls.Add(this.tbSectionName);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.enumCmbbxThirdPartyFieldType);
            this.groupBox3.Controls.Add(this.txtThirdPartyFieldName);
            this.groupBox3.Controls.Add(this.lblFieldName);
            this.groupBox3.Controls.Add(this.lblFieldType);
            this.groupBox3.Location = new System.Drawing.Point(10, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 109);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thrid Party ";
            // 
            // ckbSaveContentToFile
            // 
            this.ckbSaveContentToFile.AutoSize = true;
            this.ckbSaveContentToFile.Location = new System.Drawing.Point(151, 86);
            this.ckbSaveContentToFile.Name = "ckbSaveContentToFile";
            this.ckbSaveContentToFile.Size = new System.Drawing.Size(126, 17);
            this.ckbSaveContentToFile.TabIndex = 31;
            this.ckbSaveContentToFile.Text = "Save Content To File";
            this.ckbSaveContentToFile.UseVisualStyleBackColor = true;
            this.ckbSaveContentToFile.CheckedChanged += new System.EventHandler(this.ckbSaveContentToFile_CheckedChanged);
            // 
            // tbSectionName
            // 
            this.tbSectionName.Location = new System.Drawing.Point(152, 18);
            this.tbSectionName.Name = "tbSectionName";
            this.tbSectionName.Size = new System.Drawing.Size(263, 20);
            this.tbSectionName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Section Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(135, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(135, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtThirdPartyFieldName
            // 
            this.txtThirdPartyFieldName.Location = new System.Drawing.Point(152, 55);
            this.txtThirdPartyFieldName.Name = "txtThirdPartyFieldName";
            this.txtThirdPartyFieldName.Size = new System.Drawing.Size(262, 20);
            this.txtThirdPartyFieldName.TabIndex = 3;
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(33, 58);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 22;
            this.lblFieldName.Text = "Field Name";
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(334, 39);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(56, 13);
            this.lblFieldType.TabIndex = 23;
            this.lblFieldType.Text = "Field Type";
            this.lblFieldType.Visible = false;
            // 
            // gbTranslation
            // 
            this.gbTranslation.Controls.Add(this.enumCmbbxTranslation);
            this.gbTranslation.Controls.Add(this.lblResult);
            this.gbTranslation.Controls.Add(this.lblTranslationType);
            this.gbTranslation.Controls.Add(this.cmbbxResult);
            this.gbTranslation.Controls.Add(this.txtTranslationValue);
            this.gbTranslation.Location = new System.Drawing.Point(10, 238);
            this.gbTranslation.Name = "gbTranslation";
            this.gbTranslation.Size = new System.Drawing.Size(454, 109);
            this.gbTranslation.TabIndex = 25;
            this.gbTranslation.TabStop = false;
            this.gbTranslation.Text = "Translation";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(33, 69);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(33, 13);
            this.lblResult.TabIndex = 18;
            this.lblResult.Text = "None";
            // 
            // lblTranslationType
            // 
            this.lblTranslationType.AutoSize = true;
            this.lblTranslationType.Location = new System.Drawing.Point(32, 32);
            this.lblTranslationType.Name = "lblTranslationType";
            this.lblTranslationType.Size = new System.Drawing.Size(86, 13);
            this.lblTranslationType.TabIndex = 16;
            this.lblTranslationType.Text = "Translation Type";
            // 
            // cmbbxResult
            // 
            this.cmbbxResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxResult.FormattingEnabled = true;
            this.cmbbxResult.Location = new System.Drawing.Point(151, 66);
            this.cmbbxResult.Name = "cmbbxResult";
            this.cmbbxResult.Size = new System.Drawing.Size(264, 21);
            this.cmbbxResult.TabIndex = 5;
            this.cmbbxResult.Visible = false;
            // 
            // txtTranslationValue
            // 
            this.txtTranslationValue.Enabled = false;
            this.txtTranslationValue.Location = new System.Drawing.Point(151, 66);
            this.txtTranslationValue.Name = "txtTranslationValue";
            this.txtTranslationValue.Size = new System.Drawing.Size(263, 20);
            this.txtTranslationValue.TabIndex = 22;
            // 
            // gbRedundancy
            // 
            this.gbRedundancy.Controls.Add(this.label3);
            this.gbRedundancy.Controls.Add(this.enumCmbbxRedundancy);
            this.gbRedundancy.Controls.Add(this.lblRedundancy);
            this.gbRedundancy.Location = new System.Drawing.Point(10, 353);
            this.gbRedundancy.Name = "gbRedundancy";
            this.gbRedundancy.Size = new System.Drawing.Size(454, 70);
            this.gbRedundancy.TabIndex = 24;
            this.gbRedundancy.TabStop = false;
            this.gbRedundancy.Text = "Redundancy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(137, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(33, 31);
            this.lblRedundancy.Name = "lblRedundancy";
            this.lblRedundancy.Size = new System.Drawing.Size(102, 13);
            this.lblRedundancy.TabIndex = 17;
            this.lblRedundancy.Text = "Redundancy Check";
            // 
            // gbGateway
            // 
            this.gbGateway.Controls.Add(this.enumCmbbxTable);
            this.gbGateway.Controls.Add(this.lblTable);
            this.gbGateway.Controls.Add(this.cmbbxGWField);
            this.gbGateway.Controls.Add(this.lblField);
            this.gbGateway.Location = new System.Drawing.Point(10, 127);
            this.gbGateway.Name = "gbGateway";
            this.gbGateway.Size = new System.Drawing.Size(449, 105);
            this.gbGateway.TabIndex = 23;
            this.gbGateway.TabStop = false;
            this.gbGateway.Text = "GC Gateway ";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(32, 27);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(34, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table";
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(152, 63);
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(263, 21);
            this.cmbbxGWField.TabIndex = 1;
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(33, 66);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(29, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field";
            // 
            // pContain
            // 
            this.pContain.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pContain.Location = new System.Drawing.Point(-5, 112);
            this.pContain.Name = "pContain";
            this.pContain.Size = new System.Drawing.Size(476, 330);
            this.pContain.TabIndex = 18;
            // 
            // pSimpField
            // 
            this.pSimpField.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pSimpField.Location = new System.Drawing.Point(2, 118);
            this.pSimpField.Name = "pSimpField";
            this.pSimpField.Size = new System.Drawing.Size(462, 320);
            this.pSimpField.TabIndex = 27;
            // 
            // enumCmbbxRedundancy
            // 
            this.enumCmbbxRedundancy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxRedundancy.FormattingEnabled = true;
            this.enumCmbbxRedundancy.Items.AddRange(new object[] {
            "False",
            "True"});
            this.enumCmbbxRedundancy.Location = new System.Drawing.Point(151, 28);
            this.enumCmbbxRedundancy.Name = "enumCmbbxRedundancy";
            this.enumCmbbxRedundancy.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxRedundancy.TabIndex = 6;
            this.enumCmbbxRedundancy.TheType = typeof(HYS.FileAdapter.Configuration.EnumBool);
            // 
            // enumCmbbxTranslation
            // 
            this.enumCmbbxTranslation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxTranslation.FormattingEnabled = true;
            this.enumCmbbxTranslation.Items.AddRange(new object[] {
            "None",
            "FixValue",
            "DefaultValue",
            "LookUpTable",
            "LookUpTableReverse"});
            this.enumCmbbxTranslation.Location = new System.Drawing.Point(151, 29);
            this.enumCmbbxTranslation.Name = "enumCmbbxTranslation";
            this.enumCmbbxTranslation.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTranslation.TabIndex = 4;
            this.enumCmbbxTranslation.TheType = typeof(HYS.Common.Objects.Rule.TranslatingType);
            this.enumCmbbxTranslation.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTranslation_SelectedIndexChanged);
            // 
            // enumCmbbxTable
            // 
            this.enumCmbbxTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxTable.FormattingEnabled = true;
            this.enumCmbbxTable.Items.AddRange(new object[] {
            "None",
            "Index",
            "Patient",
            "Order",
            "Report"});
            this.enumCmbbxTable.Location = new System.Drawing.Point(151, 24);
            this.enumCmbbxTable.Name = "enumCmbbxTable";
            this.enumCmbbxTable.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTable.TabIndex = 0;
            this.enumCmbbxTable.TheType = typeof(HYS.Common.Objects.Rule.GWDataDBTable);
            this.enumCmbbxTable.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTable_SelectedIndexChanged);
            // 
            // enumCmbbxThirdPartyFieldType
            // 
            this.enumCmbbxThirdPartyFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxThirdPartyFieldType.FormattingEnabled = true;
            this.enumCmbbxThirdPartyFieldType.Items.AddRange(new object[] {
            "BigInt",
            "Binary",
            "Boolean",
            "BSTR",
            "Char",
            "Currency",
            "Date",
            "DBDate",
            "DBTime",
            "DBTimeStamp",
            "Decimal",
            "Double",
            "Empty",
            "Error",
            "Filetime",
            "Guid",
            "IDispatch",
            "Integer",
            "IUnknown",
            "LongVarBinary",
            "LongVarChar",
            "LongVarWChar",
            "Numeric",
            "PropVariant",
            "Single",
            "SmallInt",
            "TinyInt",
            "UnsignedBigInt",
            "UnsignedInt",
            "UnsignedSmallInt",
            "UnsignedTinyInt",
            "VarBinary",
            "VarChar",
            "Variant",
            "VarNumeric",
            "VarWChar",
            "WChar"});
            this.enumCmbbxThirdPartyFieldType.Location = new System.Drawing.Point(396, 28);
            this.enumCmbbxThirdPartyFieldType.Name = "enumCmbbxThirdPartyFieldType";
            this.enumCmbbxThirdPartyFieldType.Size = new System.Drawing.Size(47, 21);
            this.enumCmbbxThirdPartyFieldType.TabIndex = 27;
            this.enumCmbbxThirdPartyFieldType.TheType = typeof(System.Data.OleDb.OleDbType);
            this.enumCmbbxThirdPartyFieldType.Visible = false;
            // 
            // FQueryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 489);
            this.Controls.Add(this.gbRedundancy);
            this.Controls.Add(this.gbTranslation);
            this.Controls.Add(this.gbGateway);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pSimpField);
            this.Controls.Add(this.pContain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FQueryResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbTranslation.ResumeLayout(false);
            this.gbTranslation.PerformLayout();
            this.gbRedundancy.ResumeLayout(false);
            this.gbRedundancy.PerformLayout();
            this.gbGateway.ResumeLayout(false);
            this.gbGateway.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EnumComboBox.EnumComboBox enumCmbbxThirdPartyFieldType;
        private System.Windows.Forms.TextBox txtThirdPartyFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.GroupBox gbTranslation;
        private EnumComboBox.EnumComboBox enumCmbbxTranslation;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTranslationType;
        private System.Windows.Forms.ComboBox cmbbxResult;
        private System.Windows.Forms.TextBox txtTranslationValue;
        private System.Windows.Forms.GroupBox gbRedundancy;
        private System.Windows.Forms.Label label3;
        private EnumComboBox.EnumComboBox enumCmbbxRedundancy;
        private System.Windows.Forms.Label lblRedundancy;
        private System.Windows.Forms.GroupBox gbGateway;
        private EnumComboBox.EnumComboBox enumCmbbxTable;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cmbbxGWField;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.TextBox tbSectionName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckbSaveContentToFile;
        private System.Windows.Forms.Panel pContain;
        private System.Windows.Forms.Panel pSimpField;
    }
}