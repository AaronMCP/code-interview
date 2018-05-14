namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration.Forms
{
    partial class FQueryCriteria
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.enumCmbbxOperator = new EnumComboBox.EnumComboBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.enumCmbbxThirdPartyFieldType = new EnumComboBox.EnumComboBox();
            this.txtThirdpartyFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.enumCmbbxJoin = new EnumComboBox.EnumComboBox();
            this.lblJoin = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(345, 236);
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
            this.btnCancel.Location = new System.Drawing.Point(407, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 22);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.enumCmbbxOperator);
            this.groupBox3.Controls.Add(this.lblOperator);
            this.groupBox3.Controls.Add(this.txtValue);
            this.groupBox3.Controls.Add(this.lblValue);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.enumCmbbxThirdPartyFieldType);
            this.groupBox3.Controls.Add(this.txtThirdpartyFieldName);
            this.groupBox3.Controls.Add(this.lblFieldType);
            this.groupBox3.Controls.Add(this.lblFieldName);
            this.groupBox3.Location = new System.Drawing.Point(13, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 136);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Source";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(123, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(123, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enumCmbbxOperator
            // 
            this.enumCmbbxOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxOperator.FormattingEnabled = true;
            this.enumCmbbxOperator.Items.AddRange(new object[] {
            "Equal",
            "NotEqual",
            "LargerThan",
            "EqualLargerThan",
            "SmallerThan",
            "EqualSmallerThan",
            "Like"});
            this.enumCmbbxOperator.Location = new System.Drawing.Point(139, 58);
            this.enumCmbbxOperator.Name = "enumCmbbxOperator";
            this.enumCmbbxOperator.Size = new System.Drawing.Size(264, 21);
            this.enumCmbbxOperator.StartIndex = 0;
            this.enumCmbbxOperator.TabIndex = 31;
            this.enumCmbbxOperator.TheType = typeof(HYS.Common.Objects.Rule.QueryCriteriaOperator);
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(36, 61);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(48, 13);
            this.lblOperator.TabIndex = 30;
            this.lblOperator.Text = "Operator";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(138, 97);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(265, 20);
            this.txtValue.TabIndex = 28;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(36, 100);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 27;
            this.lblValue.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(123, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(123, 28);
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
            this.enumCmbbxThirdPartyFieldType.Location = new System.Drawing.Point(139, 58);
            this.enumCmbbxThirdPartyFieldType.MaxDropDownItems = 20;
            this.enumCmbbxThirdPartyFieldType.Name = "enumCmbbxThirdPartyFieldType";
            this.enumCmbbxThirdPartyFieldType.Size = new System.Drawing.Size(264, 21);
            this.enumCmbbxThirdPartyFieldType.StartIndex = 0;
            this.enumCmbbxThirdPartyFieldType.TabIndex = 24;
            this.enumCmbbxThirdPartyFieldType.TheType = typeof(System.Data.OleDb.OleDbType);
            this.enumCmbbxThirdPartyFieldType.Visible = false;
            // 
            // txtThirdpartyFieldName
            // 
            this.txtThirdpartyFieldName.Location = new System.Drawing.Point(138, 19);
            this.txtThirdpartyFieldName.Name = "txtThirdpartyFieldName";
            this.txtThirdpartyFieldName.Size = new System.Drawing.Size(265, 20);
            this.txtThirdpartyFieldName.TabIndex = 21;
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(36, 61);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(56, 13);
            this.lblFieldType.TabIndex = 20;
            this.lblFieldType.Text = "Field Type";
            this.lblFieldType.Visible = false;
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(36, 23);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 19;
            this.lblFieldName.Text = "Field Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.enumCmbbxJoin);
            this.groupBox2.Controls.Add(this.lblJoin);
            this.groupBox2.Location = new System.Drawing.Point(13, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 75);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Join Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(123, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enumCmbbxJoin
            // 
            this.enumCmbbxJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxJoin.FormattingEnabled = true;
            this.enumCmbbxJoin.Items.AddRange(new object[] {
            "And",
            "Or"});
            this.enumCmbbxJoin.Location = new System.Drawing.Point(138, 32);
            this.enumCmbbxJoin.Name = "enumCmbbxJoin";
            this.enumCmbbxJoin.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxJoin.StartIndex = 1;
            this.enumCmbbxJoin.TabIndex = 19;
            this.enumCmbbxJoin.TheType = typeof(HYS.Common.Objects.Rule.QueryCriteriaType);
            // 
            // lblJoin
            // 
            this.lblJoin.AutoSize = true;
            this.lblJoin.Location = new System.Drawing.Point(36, 34);
            this.lblJoin.Name = "lblJoin";
            this.lblJoin.Size = new System.Drawing.Size(62, 13);
            this.lblJoin.TabIndex = 17;
            this.lblJoin.Text = "Type Name";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(12, 236);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 21;
            this.lblNote.Text = "(*: Required)";
            // 
            // FQueryCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 270);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FQueryCriteria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblJoin;
        private EnumComboBox.EnumComboBox enumCmbbxJoin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EnumComboBox.EnumComboBox enumCmbbxThirdPartyFieldType;
        private System.Windows.Forms.TextBox txtThirdpartyFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private EnumComboBox.EnumComboBox enumCmbbxOperator;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}