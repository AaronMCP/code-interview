namespace HYS.SQLOutboundAdapterConfiguration.Forms
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
            this.lblFieldStar = new System.Windows.Forms.Label();
            this.lblTableStar = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.enumCmbbxFieldType = new EnumComboBox.EnumComboBox();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.groupBoxRD = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.enumCmbbxRedundancy = new EnumComboBox.EnumComboBox();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbbxResult = new System.Windows.Forms.ComboBox();
            this.lblLUTStar = new System.Windows.Forms.Label();
            this.enumCmbbxTranslation = new EnumComboBox.EnumComboBox();
            this.lblTranslationValue = new System.Windows.Forms.Label();
            this.lblTranslationType = new System.Windows.Forms.Label();
            this.txtTranslationValue = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBoxRD.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(295, 389);
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
            this.btnCancel.Location = new System.Drawing.Point(381, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFieldStar);
            this.groupBox2.Controls.Add(this.lblTableStar);
            this.groupBox2.Controls.Add(this.lblTable);
            this.groupBox2.Controls.Add(this.enumCmbbxTable);
            this.groupBox2.Controls.Add(this.cmbbxGWField);
            this.groupBox2.Controls.Add(this.lblField);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 99);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GC Gateway Field";
            // 
            // lblFieldStar
            // 
            this.lblFieldStar.AutoSize = true;
            this.lblFieldStar.ForeColor = System.Drawing.Color.Red;
            this.lblFieldStar.Location = new System.Drawing.Point(145, 66);
            this.lblFieldStar.Name = "lblFieldStar";
            this.lblFieldStar.Size = new System.Drawing.Size(11, 13);
            this.lblFieldStar.TabIndex = 23;
            this.lblFieldStar.Text = "*";
            this.lblFieldStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTableStar
            // 
            this.lblTableStar.AutoSize = true;
            this.lblTableStar.ForeColor = System.Drawing.Color.Red;
            this.lblTableStar.Location = new System.Drawing.Point(145, 27);
            this.lblTableStar.Name = "lblTableStar";
            this.lblTableStar.Size = new System.Drawing.Size(11, 13);
            this.lblTableStar.TabIndex = 22;
            this.lblTableStar.Text = "*";
            this.lblTableStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(35, 30);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(65, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table Name";
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
            this.enumCmbbxTable.Location = new System.Drawing.Point(158, 27);
            this.enumCmbbxTable.Name = "enumCmbbxTable";
            this.enumCmbbxTable.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTable.StartIndex = 0;
            this.enumCmbbxTable.TabIndex = 0;
            this.enumCmbbxTable.TheType = typeof(HYS.Common.Objects.Rule.GWDataDBTable);
            this.enumCmbbxTable.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTable_SelectedIndexChanged);
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(158, 63);
            this.cmbbxGWField.MaxDropDownItems = 20;
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(263, 21);
            this.cmbbxGWField.TabIndex = 1;
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(35, 66);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(60, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(145, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(145, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enumCmbbxFieldType
            // 
            this.enumCmbbxFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxFieldType.FormattingEnabled = true;
            this.enumCmbbxFieldType.Items.AddRange(new object[] {
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
            this.enumCmbbxFieldType.Location = new System.Drawing.Point(158, 57);
            this.enumCmbbxFieldType.MaxDropDownItems = 20;
            this.enumCmbbxFieldType.Name = "enumCmbbxFieldType";
            this.enumCmbbxFieldType.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxFieldType.StartIndex = 0;
            this.enumCmbbxFieldType.TabIndex = 1;
            this.enumCmbbxFieldType.TheType = typeof(System.Data.OleDb.OleDbType);
            // 
            // txtParameter
            // 
            this.txtParameter.Location = new System.Drawing.Point(158, 23);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(263, 20);
            this.txtParameter.TabIndex = 0;
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(35, 60);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(82, 13);
            this.lblFieldType.TabIndex = 11;
            this.lblFieldType.Text = "Parameter Type";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(35, 26);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(86, 13);
            this.lblFieldName.TabIndex = 10;
            this.lblFieldName.Text = "Parameter Name";
            // 
            // groupBoxRD
            // 
            this.groupBoxRD.Controls.Add(this.label3);
            this.groupBoxRD.Controls.Add(this.enumCmbbxRedundancy);
            this.groupBoxRD.Controls.Add(this.lblRedundancy);
            this.groupBoxRD.Location = new System.Drawing.Point(12, 310);
            this.groupBoxRD.Name = "groupBoxRD";
            this.groupBoxRD.Size = new System.Drawing.Size(449, 65);
            this.groupBoxRD.TabIndex = 3;
            this.groupBoxRD.TabStop = false;
            this.groupBoxRD.Text = "Redundancy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(145, 29);
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
            this.enumCmbbxRedundancy.Location = new System.Drawing.Point(158, 26);
            this.enumCmbbxRedundancy.Name = "enumCmbbxRedundancy";
            this.enumCmbbxRedundancy.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxRedundancy.StartIndex = 0;
            this.enumCmbbxRedundancy.TabIndex = 0;
            this.enumCmbbxRedundancy.TheType = typeof(HYS.SQLOutboundAdapterObjects.EnumBool);
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(35, 29);
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
            this.groupBox3.Controls.Add(this.lblTranslationValue);
            this.groupBox3.Controls.Add(this.lblTranslationType);
            this.groupBox3.Controls.Add(this.txtTranslationValue);
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 96);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Translation";
            // 
            // cmbbxResult
            // 
            this.cmbbxResult.FormattingEnabled = true;
            this.cmbbxResult.Location = new System.Drawing.Point(158, 60);
            this.cmbbxResult.Name = "cmbbxResult";
            this.cmbbxResult.Size = new System.Drawing.Size(263, 21);
            this.cmbbxResult.TabIndex = 1;
            this.cmbbxResult.Visible = false;
            // 
            // lblLUTStar
            // 
            this.lblLUTStar.AutoSize = true;
            this.lblLUTStar.ForeColor = System.Drawing.Color.Red;
            this.lblLUTStar.Location = new System.Drawing.Point(145, 63);
            this.lblLUTStar.Name = "lblLUTStar";
            this.lblLUTStar.Size = new System.Drawing.Size(11, 13);
            this.lblLUTStar.TabIndex = 24;
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
            this.enumCmbbxTranslation.Location = new System.Drawing.Point(158, 23);
            this.enumCmbbxTranslation.Name = "enumCmbbxTranslation";
            this.enumCmbbxTranslation.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTranslation.StartIndex = 0;
            this.enumCmbbxTranslation.TabIndex = 0;
            this.enumCmbbxTranslation.TheType = typeof(HYS.Common.Objects.Rule.TranslatingType);
            this.enumCmbbxTranslation.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTranslation_SelectedIndexChanged);
            // 
            // lblTranslationValue
            // 
            this.lblTranslationValue.AutoSize = true;
            this.lblTranslationValue.Location = new System.Drawing.Point(35, 63);
            this.lblTranslationValue.Name = "lblTranslationValue";
            this.lblTranslationValue.Size = new System.Drawing.Size(89, 13);
            this.lblTranslationValue.TabIndex = 18;
            this.lblTranslationValue.Text = "Translation Value";
            // 
            // lblTranslationType
            // 
            this.lblTranslationType.AutoSize = true;
            this.lblTranslationType.Location = new System.Drawing.Point(35, 25);
            this.lblTranslationType.Name = "lblTranslationType";
            this.lblTranslationType.Size = new System.Drawing.Size(86, 13);
            this.lblTranslationType.TabIndex = 16;
            this.lblTranslationType.Text = "Translation Type";
            // 
            // txtTranslationValue
            // 
            this.txtTranslationValue.Enabled = false;
            this.txtTranslationValue.Location = new System.Drawing.Point(158, 60);
            this.txtTranslationValue.Name = "txtTranslationValue";
            this.txtTranslationValue.Size = new System.Drawing.Size(263, 20);
            this.txtTranslationValue.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enumCmbbxFieldType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtParameter);
            this.groupBox1.Controls.Add(this.lblFieldType);
            this.groupBox1.Controls.Add(this.lblFieldName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(12, 378);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "(*: Required)";
            // 
            // QueryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 417);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxRD);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxRD.ResumeLayout(false);
            this.groupBoxRD.PerformLayout();
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
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.GroupBox groupBoxRD;
        private System.Windows.Forms.Label lblRedundancy;
        private EnumComboBox.EnumComboBox enumCmbbxFieldType;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblFieldStar;
        private System.Windows.Forms.Label lblTableStar;
        private System.Windows.Forms.Label lblLUTStar;
        private System.Windows.Forms.Label label3;
    }
}