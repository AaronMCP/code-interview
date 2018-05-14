namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    partial class QueryOutParameter
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
            this.lblTableStar = new System.Windows.Forms.Label();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblFieldStar = new System.Windows.Forms.Label();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblLUTStar = new System.Windows.Forms.Label();
            this.enumCmbbxTranslation = new EnumComboBox.EnumComboBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTranslationType = new System.Windows.Forms.Label();
            this.txtTranslationValue = new System.Windows.Forms.TextBox();
            this.cmbbxResult = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(295, 310);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(381, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTableStar);
            this.groupBox2.Controls.Add(this.enumCmbbxTable);
            this.groupBox2.Controls.Add(this.lblTable);
            this.groupBox2.Controls.Add(this.lblFieldStar);
            this.groupBox2.Controls.Add(this.cmbbxGWField);
            this.groupBox2.Controls.Add(this.lblField);
            this.groupBox2.Location = new System.Drawing.Point(12, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 103);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GCGateway Field";
            // 
            // lblTableStar
            // 
            this.lblTableStar.AutoSize = true;
            this.lblTableStar.ForeColor = System.Drawing.Color.Red;
            this.lblTableStar.Location = new System.Drawing.Point(139, 29);
            this.lblTableStar.Name = "lblTableStar";
            this.lblTableStar.Size = new System.Drawing.Size(11, 13);
            this.lblTableStar.TabIndex = 32;
            this.lblTableStar.Text = "*";
            this.lblTableStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.enumCmbbxTable.Location = new System.Drawing.Point(152, 25);
            this.enumCmbbxTable.Name = "enumCmbbxTable";
            this.enumCmbbxTable.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTable.StartIndex = 0;
            this.enumCmbbxTable.TabIndex = 0;
            this.enumCmbbxTable.TheType = typeof(HYS.Common.Objects.Rule.GWDataDBTable);
            this.enumCmbbxTable.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTable_SelectedIndexChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(38, 29);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(65, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table Name";
            // 
            // lblFieldStar
            // 
            this.lblFieldStar.AutoSize = true;
            this.lblFieldStar.ForeColor = System.Drawing.Color.Red;
            this.lblFieldStar.Location = new System.Drawing.Point(139, 64);
            this.lblFieldStar.Name = "lblFieldStar";
            this.lblFieldStar.Size = new System.Drawing.Size(11, 13);
            this.lblFieldStar.TabIndex = 31;
            this.lblFieldStar.Text = "*";
            this.lblFieldStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(153, 61);
            this.cmbbxGWField.MaxDropDownItems = 20;
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(263, 21);
            this.cmbbxGWField.TabIndex = 1;
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(38, 64);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(60, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(139, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbbxResult);
            this.groupBox3.Controls.Add(this.lblLUTStar);
            this.groupBox3.Controls.Add(this.enumCmbbxTranslation);
            this.groupBox3.Controls.Add(this.lblResult);
            this.groupBox3.Controls.Add(this.lblTranslationType);
            this.groupBox3.Controls.Add(this.txtTranslationValue);
            this.groupBox3.Location = new System.Drawing.Point(12, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 99);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Translation";
            // 
            // lblLUTStar
            // 
            this.lblLUTStar.AutoSize = true;
            this.lblLUTStar.ForeColor = System.Drawing.Color.Red;
            this.lblLUTStar.Location = new System.Drawing.Point(140, 64);
            this.lblLUTStar.Name = "lblLUTStar";
            this.lblLUTStar.Size = new System.Drawing.Size(11, 13);
            this.lblLUTStar.TabIndex = 33;
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
            this.enumCmbbxTranslation.Location = new System.Drawing.Point(152, 24);
            this.enumCmbbxTranslation.Name = "enumCmbbxTranslation";
            this.enumCmbbxTranslation.Size = new System.Drawing.Size(263, 21);
            this.enumCmbbxTranslation.StartIndex = 0;
            this.enumCmbbxTranslation.TabIndex = 0;
            this.enumCmbbxTranslation.TheType = typeof(HYS.Common.Objects.Rule.TranslatingType);
            this.enumCmbbxTranslation.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTranslation_SelectedIndexChanged);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(38, 64);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(89, 13);
            this.lblResult.TabIndex = 18;
            this.lblResult.Text = "Translation Value";
            // 
            // lblTranslationType
            // 
            this.lblTranslationType.AutoSize = true;
            this.lblTranslationType.Location = new System.Drawing.Point(38, 27);
            this.lblTranslationType.Name = "lblTranslationType";
            this.lblTranslationType.Size = new System.Drawing.Size(86, 13);
            this.lblTranslationType.TabIndex = 16;
            this.lblTranslationType.Text = "Translation Type";
            // 
            // txtTranslationValue
            // 
            this.txtTranslationValue.Enabled = false;
            this.txtTranslationValue.Location = new System.Drawing.Point(151, 61);
            this.txtTranslationValue.Name = "txtTranslationValue";
            this.txtTranslationValue.Size = new System.Drawing.Size(263, 20);
            this.txtTranslationValue.TabIndex = 1;
            // 
            // cmbbxResult
            // 
            this.cmbbxResult.FormattingEnabled = true;
            this.cmbbxResult.Location = new System.Drawing.Point(151, 61);
            this.cmbbxResult.Name = "cmbbxResult";
            this.cmbbxResult.Size = new System.Drawing.Size(263, 21);
            this.cmbbxResult.TabIndex = 20;
            this.cmbbxResult.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtParameter);
            this.groupBox1.Controls.Add(this.lblFieldName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // txtParameter
            // 
            this.txtParameter.Location = new System.Drawing.Point(152, 29);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(263, 20);
            this.txtParameter.TabIndex = 0;
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(38, 31);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(86, 13);
            this.lblFieldName.TabIndex = 22;
            this.lblFieldName.Text = "Parameter Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(81, 433);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(9, 298);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 24;
            this.lblNote.Text = "(*: Required)";
            // 
            // QueryOutParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 339);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryOutParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private EnumComboBox.EnumComboBox enumCmbbxTranslation;
        private System.Windows.Forms.TextBox txtTranslationValue;
        private System.Windows.Forms.ComboBox cmbbxResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTranslationType;
        private EnumComboBox.EnumComboBox enumCmbbxTable;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbbxGWField;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblLUTStar;
        private System.Windows.Forms.Label lblFieldStar;
        private System.Windows.Forms.Label lblTableStar;
    }
}