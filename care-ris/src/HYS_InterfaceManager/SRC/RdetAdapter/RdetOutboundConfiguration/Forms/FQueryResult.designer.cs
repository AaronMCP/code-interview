namespace HYS.RdetAdapter.RdetOutboundAdapterConfiguration.Forms
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.enumCmbbxRedundancy = new EnumComboBox.EnumComboBox();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.enumCmbbxTranslation = new EnumComboBox.EnumComboBox();
            this.txtTranslationValue = new System.Windows.Forms.TextBox();
            this.cmbbxResult = new System.Windows.Forms.ComboBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTranslationType = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.enumCmbbxThirdPartyFieldType = new EnumComboBox.EnumComboBox();
            this.txtThirdPartyFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(342, 397);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 22);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(404, 397);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 22);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enumCmbbxTable);
            this.groupBox1.Controls.Add(this.lblTable);
            this.groupBox1.Controls.Add(this.cmbbxGWField);
            this.groupBox1.Controls.Add(this.lblField);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 99);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Source";
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
            this.enumCmbbxTable.Location = new System.Drawing.Point(152, 27);
            this.enumCmbbxTable.Name = "enumCmbbxTable";
            this.enumCmbbxTable.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTable.TabIndex = 20;
            this.enumCmbbxTable.TheType = typeof(HYS.Common.Objects.Rule.GWDataDBTable);
            this.enumCmbbxTable.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTable_SelectedIndexChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(39, 30);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(34, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table";
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(152, 59);
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(263, 21);
            this.cmbbxGWField.TabIndex = 14;
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(38, 62);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(29, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.enumCmbbxRedundancy);
            this.groupBox4.Controls.Add(this.lblRedundancy);
            this.groupBox4.Location = new System.Drawing.Point(13, 317);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 65);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Redundancy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(135, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enumCmbbxRedundancy
            // 
            this.enumCmbbxRedundancy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxRedundancy.FormattingEnabled = true;
            this.enumCmbbxRedundancy.Items.AddRange(new object[] {
            "False",
            "True"});
            this.enumCmbbxRedundancy.Location = new System.Drawing.Point(152, 30);
            this.enumCmbbxRedundancy.Name = "enumCmbbxRedundancy";
            this.enumCmbbxRedundancy.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxRedundancy.TabIndex = 19;
            this.enumCmbbxRedundancy.TheType = typeof(HYS.RdetAdapter.Configuration.EnumBool);
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(39, 30);
            this.lblRedundancy.Name = "lblRedundancy";
            this.lblRedundancy.Size = new System.Drawing.Size(76, 13);
            this.lblRedundancy.TabIndex = 17;
            this.lblRedundancy.Text = "IsRedundancy";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.enumCmbbxTranslation);
            this.groupBox2.Controls.Add(this.txtTranslationValue);
            this.groupBox2.Controls.Add(this.cmbbxResult);
            this.groupBox2.Controls.Add(this.lblResult);
            this.groupBox2.Controls.Add(this.lblTranslationType);
            this.groupBox2.Location = new System.Drawing.Point(13, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 94);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Translation";
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
            this.enumCmbbxTranslation.Location = new System.Drawing.Point(151, 21);
            this.enumCmbbxTranslation.Name = "enumCmbbxTranslation";
            this.enumCmbbxTranslation.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTranslation.TabIndex = 20;
            this.enumCmbbxTranslation.TheType = typeof(HYS.Common.Objects.Rule.TranslatingType);
            this.enumCmbbxTranslation.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTranslation_SelectedIndexChanged);
            // 
            // txtTranslationValue
            // 
            this.txtTranslationValue.Enabled = false;
            this.txtTranslationValue.Location = new System.Drawing.Point(151, 53);
            this.txtTranslationValue.Name = "txtTranslationValue";
            this.txtTranslationValue.Size = new System.Drawing.Size(263, 20);
            this.txtTranslationValue.TabIndex = 22;
            // 
            // cmbbxResult
            // 
            this.cmbbxResult.FormattingEnabled = true;
            this.cmbbxResult.Location = new System.Drawing.Point(151, 53);
            this.cmbbxResult.Name = "cmbbxResult";
            this.cmbbxResult.Size = new System.Drawing.Size(264, 21);
            this.cmbbxResult.TabIndex = 20;
            this.cmbbxResult.Visible = false;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(38, 57);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(33, 13);
            this.lblResult.TabIndex = 18;
            this.lblResult.Text = "None";
            // 
            // lblTranslationType
            // 
            this.lblTranslationType.AutoSize = true;
            this.lblTranslationType.Location = new System.Drawing.Point(37, 24);
            this.lblTranslationType.Name = "lblTranslationType";
            this.lblTranslationType.Size = new System.Drawing.Size(86, 13);
            this.lblTranslationType.TabIndex = 16;
            this.lblTranslationType.Text = "Translation Type";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(14, 399);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "(*: Required)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.enumCmbbxThirdPartyFieldType);
            this.groupBox3.Controls.Add(this.txtThirdPartyFieldName);
            this.groupBox3.Controls.Add(this.lblFieldType);
            this.groupBox3.Controls.Add(this.lblFieldName);
            this.groupBox3.Location = new System.Drawing.Point(13, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 94);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Destination";
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
            this.label1.Location = new System.Drawing.Point(135, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.enumCmbbxThirdPartyFieldType.Location = new System.Drawing.Point(152, 54);
            this.enumCmbbxThirdPartyFieldType.Name = "enumCmbbxThirdPartyFieldType";
            this.enumCmbbxThirdPartyFieldType.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxThirdPartyFieldType.TabIndex = 27;
            this.enumCmbbxThirdPartyFieldType.TheType = typeof(System.Data.OleDb.OleDbType);
            // 
            // txtThirdPartyFieldName
            // 
            this.txtThirdPartyFieldName.Location = new System.Drawing.Point(152, 21);
            this.txtThirdPartyFieldName.Name = "txtThirdPartyFieldName";
            this.txtThirdPartyFieldName.Size = new System.Drawing.Size(263, 20);
            this.txtThirdPartyFieldName.TabIndex = 24;
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(38, 58);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(56, 13);
            this.lblFieldType.TabIndex = 23;
            this.lblFieldType.Text = "Field Type";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(38, 24);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 22;
            this.lblFieldName.Text = "Field Name";
            // 
            // FQueryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 436);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(479, 633);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(479, 400);
            this.Name = "FQueryResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblRedundancy;
        private EnumComboBox.EnumComboBox enumCmbbxRedundancy;
        private System.Windows.Forms.GroupBox groupBox2;
        private EnumComboBox.EnumComboBox enumCmbbxTranslation;
        private System.Windows.Forms.TextBox txtTranslationValue;
        private System.Windows.Forms.ComboBox cmbbxResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTranslationType;
        private EnumComboBox.EnumComboBox enumCmbbxTable;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbbxGWField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EnumComboBox.EnumComboBox enumCmbbxThirdPartyFieldType;
        private System.Windows.Forms.TextBox txtThirdPartyFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
    }
}