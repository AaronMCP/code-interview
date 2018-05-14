namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    partial class QueryCriteria
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxGetFromSP = new System.Windows.Forms.CheckBox();
            this.checkBoxNull = new System.Windows.Forms.CheckBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.enumCmbbxThirdPartyFieldType = new EnumComboBox.EnumComboBox();
            this.txtThirdpartyFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(296, 191);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(382, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxGetFromSP);
            this.groupBox3.Controls.Add(this.checkBoxNull);
            this.groupBox3.Controls.Add(this.txtValue);
            this.groupBox3.Controls.Add(this.lblValue);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.enumCmbbxThirdPartyFieldType);
            this.groupBox3.Controls.Add(this.txtThirdpartyFieldName);
            this.groupBox3.Controls.Add(this.lblFieldType);
            this.groupBox3.Controls.Add(this.lblFieldName);
            this.groupBox3.Location = new System.Drawing.Point(13, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 174);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parameter";
            // 
            // checkBoxGetFromSP
            // 
            this.checkBoxGetFromSP.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxGetFromSP.Location = new System.Drawing.Point(205, 120);
            this.checkBoxGetFromSP.Name = "checkBoxGetFromSP";
            this.checkBoxGetFromSP.Size = new System.Drawing.Size(218, 48);
            this.checkBoxGetFromSP.TabIndex = 32;
            this.checkBoxGetFromSP.Text = "Get From Storage Procedure Result Set (Please click \"Advance\" button on the Add/E" +
                "dit Channel dialog for details)";
            this.checkBoxGetFromSP.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxGetFromSP.UseVisualStyleBackColor = true;
            this.checkBoxGetFromSP.CheckedChanged += new System.EventHandler(this.checkBoxGetFromSP_CheckedChanged);
            // 
            // checkBoxNull
            // 
            this.checkBoxNull.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxNull.Location = new System.Drawing.Point(148, 120);
            this.checkBoxNull.Name = "checkBoxNull";
            this.checkBoxNull.Size = new System.Drawing.Size(56, 20);
            this.checkBoxNull.TabIndex = 31;
            this.checkBoxNull.Text = "Null";
            this.checkBoxNull.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxNull.UseVisualStyleBackColor = true;
            this.checkBoxNull.CheckedChanged += new System.EventHandler(this.checkBoxNull_CheckedChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(148, 94);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(265, 20);
            this.txtValue.TabIndex = 2;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(31, 97);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 27;
            this.lblValue.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(132, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(132, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 25;
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
            this.enumCmbbxThirdPartyFieldType.Location = new System.Drawing.Point(148, 57);
            this.enumCmbbxThirdPartyFieldType.MaxDropDownItems = 20;
            this.enumCmbbxThirdPartyFieldType.Name = "enumCmbbxThirdPartyFieldType";
            this.enumCmbbxThirdPartyFieldType.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxThirdPartyFieldType.StartIndex = 0;
            this.enumCmbbxThirdPartyFieldType.TabIndex = 1;
            this.enumCmbbxThirdPartyFieldType.TheType = typeof(System.Data.OleDb.OleDbType);
            // 
            // txtThirdpartyFieldName
            // 
            this.txtThirdpartyFieldName.Location = new System.Drawing.Point(148, 23);
            this.txtThirdpartyFieldName.Name = "txtThirdpartyFieldName";
            this.txtThirdpartyFieldName.Size = new System.Drawing.Size(265, 20);
            this.txtThirdpartyFieldName.TabIndex = 0;
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(31, 60);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(82, 13);
            this.lblFieldType.TabIndex = 20;
            this.lblFieldType.Text = "Parameter Type";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(31, 27);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(86, 13);
            this.lblFieldName.TabIndex = 19;
            this.lblFieldName.Text = "Parameter Name";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(10, 181);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 31;
            this.lblNote.Text = "(*: Required)";
            // 
            // QueryCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 220);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryCriteria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EnumComboBox.EnumComboBox enumCmbbxThirdPartyFieldType;
        private System.Windows.Forms.TextBox txtThirdpartyFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.CheckBox checkBoxNull;
        private System.Windows.Forms.CheckBox checkBoxGetFromSP;
    }
}