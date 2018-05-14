namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    partial class QueryResult
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.enumCmbbxRedundancy = new EnumComboBox.EnumComboBox();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblLUTStar = new System.Windows.Forms.Label();
            this.enumCmbbxTranslation = new EnumComboBox.EnumComboBox();
            this.txtTranslationValue = new System.Windows.Forms.TextBox();
            this.cmbbxResult = new System.Windows.Forms.ComboBox();
            this.lblTranslationValue = new System.Windows.Forms.Label();
            this.lblTranslationType = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTypeStar = new System.Windows.Forms.Label();
            this.lblFieldStar = new System.Windows.Forms.Label();
            this.enumCmbbxThirdPartyFieldType = new EnumComboBox.EnumComboBox();
            this.txtThirdPartyFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(293, 379);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(379, 379);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.enumCmbbxTable);
            this.groupBox2.Controls.Add(this.lblTable);
            this.groupBox2.Controls.Add(this.cmbbxGWField);
            this.groupBox2.Controls.Add(this.lblField);
            this.groupBox2.Location = new System.Drawing.Point(12, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GC Gateway Field";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(137, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(137, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.enumCmbbxTable.Location = new System.Drawing.Point(151, 19);
            this.enumCmbbxTable.Name = "enumCmbbxTable";
            this.enumCmbbxTable.Size = new System.Drawing.Size(266, 21);
            this.enumCmbbxTable.StartIndex = 0;
            this.enumCmbbxTable.TabIndex = 0;
            this.enumCmbbxTable.TheType = typeof(HYS.Common.Objects.Rule.GWDataDBTable);
            this.enumCmbbxTable.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTable_SelectedIndexChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(34, 23);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(65, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table Name";
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(152, 57);
            this.cmbbxGWField.MaxDropDownItems = 20;
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(265, 21);
            this.cmbbxGWField.TabIndex = 1;
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(34, 60);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(60, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field Name";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.enumCmbbxRedundancy);
            this.groupBox4.Controls.Add(this.lblRedundancy);
            this.groupBox4.Location = new System.Drawing.Point(12, 308);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 56);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Redundancy";
            // 
            // enumCmbbxRedundancy
            // 
            this.enumCmbbxRedundancy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxRedundancy.FormattingEnabled = true;
            this.enumCmbbxRedundancy.Items.AddRange(new object[] {
            "False",
            "True"});
            this.enumCmbbxRedundancy.Location = new System.Drawing.Point(152, 19);
            this.enumCmbbxRedundancy.Name = "enumCmbbxRedundancy";
            this.enumCmbbxRedundancy.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxRedundancy.StartIndex = 0;
            this.enumCmbbxRedundancy.TabIndex = 0;
            this.enumCmbbxRedundancy.TheType = typeof(HYS.SQLInboundAdapterObjects.EnumBool);
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(34, 22);
            this.lblRedundancy.Name = "lblRedundancy";
            this.lblRedundancy.Size = new System.Drawing.Size(102, 13);
            this.lblRedundancy.TabIndex = 17;
            this.lblRedundancy.Text = "Redundancy Check";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbbxResult);
            this.groupBox3.Controls.Add(this.lblLUTStar);
            this.groupBox3.Controls.Add(this.enumCmbbxTranslation);
            this.groupBox3.Controls.Add(this.txtTranslationValue);
            this.groupBox3.Controls.Add(this.lblTranslationValue);
            this.groupBox3.Controls.Add(this.lblTranslationType);
            this.groupBox3.Location = new System.Drawing.Point(12, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 92);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Translation";
            // 
            // lblLUTStar
            // 
            this.lblLUTStar.AutoSize = true;
            this.lblLUTStar.ForeColor = System.Drawing.Color.Red;
            this.lblLUTStar.Location = new System.Drawing.Point(140, 59);
            this.lblLUTStar.Name = "lblLUTStar";
            this.lblLUTStar.Size = new System.Drawing.Size(11, 13);
            this.lblLUTStar.TabIndex = 1;
            this.lblLUTStar.Text = "*";
            this.lblLUTStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLUTStar.Visible = false;
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
            this.enumCmbbxTranslation.Location = new System.Drawing.Point(154, 19);
            this.enumCmbbxTranslation.Name = "enumCmbbxTranslation";
            this.enumCmbbxTranslation.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTranslation.StartIndex = 0;
            this.enumCmbbxTranslation.TabIndex = 0;
            this.enumCmbbxTranslation.TheType = typeof(HYS.Common.Objects.Rule.TranslatingType);
            this.enumCmbbxTranslation.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTranslation_SelectedIndexChanged);
            // 
            // txtTranslationValue
            // 
            this.txtTranslationValue.Enabled = false;
            this.txtTranslationValue.Location = new System.Drawing.Point(154, 56);
            this.txtTranslationValue.Name = "txtTranslationValue";
            this.txtTranslationValue.Size = new System.Drawing.Size(263, 20);
            this.txtTranslationValue.TabIndex = 1;
            // 
            // cmbbxResult
            // 
            this.cmbbxResult.FormattingEnabled = true;
            this.cmbbxResult.Location = new System.Drawing.Point(154, 56);
            this.cmbbxResult.Name = "cmbbxResult";
            this.cmbbxResult.Size = new System.Drawing.Size(263, 21);
            this.cmbbxResult.TabIndex = 20;
            this.cmbbxResult.Visible = false;
            // 
            // lblTranslationValue
            // 
            this.lblTranslationValue.AutoSize = true;
            this.lblTranslationValue.Location = new System.Drawing.Point(34, 56);
            this.lblTranslationValue.Name = "lblTranslationValue";
            this.lblTranslationValue.Size = new System.Drawing.Size(89, 13);
            this.lblTranslationValue.TabIndex = 18;
            this.lblTranslationValue.Text = "Translation Value";
            // 
            // lblTranslationType
            // 
            this.lblTranslationType.AutoSize = true;
            this.lblTranslationType.Location = new System.Drawing.Point(34, 22);
            this.lblTranslationType.Name = "lblTranslationType";
            this.lblTranslationType.Size = new System.Drawing.Size(86, 13);
            this.lblTranslationType.TabIndex = 16;
            this.lblTranslationType.Text = "Translation Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTypeStar);
            this.groupBox1.Controls.Add(this.lblFieldStar);
            this.groupBox1.Controls.Add(this.enumCmbbxThirdPartyFieldType);
            this.groupBox1.Controls.Add(this.txtThirdPartyFieldName);
            this.groupBox1.Controls.Add(this.lblFieldType);
            this.groupBox1.Controls.Add(this.lblFieldName);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Third Party Field";
            // 
            // lblTypeStar
            // 
            this.lblTypeStar.AutoSize = true;
            this.lblTypeStar.ForeColor = System.Drawing.Color.Red;
            this.lblTypeStar.Location = new System.Drawing.Point(137, 66);
            this.lblTypeStar.Name = "lblTypeStar";
            this.lblTypeStar.Size = new System.Drawing.Size(11, 13);
            this.lblTypeStar.TabIndex = 1;
            this.lblTypeStar.Text = "*";
            this.lblTypeStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFieldStar
            // 
            this.lblFieldStar.AutoSize = true;
            this.lblFieldStar.ForeColor = System.Drawing.Color.Red;
            this.lblFieldStar.Location = new System.Drawing.Point(137, 25);
            this.lblFieldStar.Name = "lblFieldStar";
            this.lblFieldStar.Size = new System.Drawing.Size(11, 13);
            this.lblFieldStar.TabIndex = 0;
            this.lblFieldStar.Text = "*";
            this.lblFieldStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.enumCmbbxThirdPartyFieldType.Location = new System.Drawing.Point(151, 58);
            this.enumCmbbxThirdPartyFieldType.MaxDropDownItems = 20;
            this.enumCmbbxThirdPartyFieldType.Name = "enumCmbbxThirdPartyFieldType";
            this.enumCmbbxThirdPartyFieldType.Size = new System.Drawing.Size(266, 21);
            this.enumCmbbxThirdPartyFieldType.StartIndex = 0;
            this.enumCmbbxThirdPartyFieldType.TabIndex = 1;
            this.enumCmbbxThirdPartyFieldType.TheType = typeof(System.Data.OleDb.OleDbType);
            // 
            // txtThirdPartyFieldName
            // 
            this.txtThirdPartyFieldName.Location = new System.Drawing.Point(151, 22);
            this.txtThirdPartyFieldName.Name = "txtThirdPartyFieldName";
            this.txtThirdPartyFieldName.Size = new System.Drawing.Size(263, 20);
            this.txtThirdPartyFieldName.TabIndex = 0;
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(34, 61);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(56, 13);
            this.lblFieldType.TabIndex = 23;
            this.lblFieldType.Text = "Field Type";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(35, 25);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 22;
            this.lblFieldName.Text = "Field Name";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(12, 369);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "(*: Required)";
            // 
            // QueryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 410);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblRedundancy;
        private EnumComboBox.EnumComboBox enumCmbbxRedundancy;
        private System.Windows.Forms.GroupBox groupBox3;
        private EnumComboBox.EnumComboBox enumCmbbxTranslation;
        private System.Windows.Forms.TextBox txtTranslationValue;
        private System.Windows.Forms.ComboBox cmbbxResult;
        private System.Windows.Forms.Label lblTranslationValue;
        private System.Windows.Forms.Label lblTranslationType;
        private System.Windows.Forms.GroupBox groupBox1;
        private EnumComboBox.EnumComboBox enumCmbbxTable;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbbxGWField;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblTypeStar;
        private System.Windows.Forms.Label lblFieldStar;
        private EnumComboBox.EnumComboBox enumCmbbxThirdPartyFieldType;
        private System.Windows.Forms.TextBox txtThirdPartyFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLUTStar;
    }
}