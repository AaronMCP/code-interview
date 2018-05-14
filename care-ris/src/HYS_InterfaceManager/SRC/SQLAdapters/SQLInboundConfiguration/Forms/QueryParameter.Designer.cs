namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    partial class QueryParameter
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.enumCmbbxRedundancy = new EnumComboBox.EnumComboBox();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblLUTStar = new System.Windows.Forms.Label();
            this.enumCmbbxTranslation = new EnumComboBox.EnumComboBox();
            this.lblTranslationValue = new System.Windows.Forms.Label();
            this.lblTranslationType = new System.Windows.Forms.Label();
            this.cmbbxResult = new System.Windows.Forms.ComboBox();
            this.txtTranslationValue = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.lblParameterStar = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(295, 350);
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
            this.btnCancel.Location = new System.Drawing.Point(381, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.enumCmbbxTable);
            this.groupBox1.Controls.Add(this.lblTable);
            this.groupBox1.Controls.Add(this.cmbbxGWField);
            this.groupBox1.Controls.Add(this.lblField);
            this.groupBox1.Location = new System.Drawing.Point(12, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 97);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GCGateway Field";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(138, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(138, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.enumCmbbxTable.Location = new System.Drawing.Point(151, 22);
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
            this.lblTable.Location = new System.Drawing.Point(37, 26);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(65, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table Name";
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(152, 58);
            this.cmbbxGWField.MaxDropDownItems = 20;
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(263, 21);
            this.cmbbxGWField.TabIndex = 1;
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(37, 61);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(60, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field Name";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.enumCmbbxRedundancy);
            this.groupBox4.Controls.Add(this.lblRedundancy);
            this.groupBox4.Location = new System.Drawing.Point(13, 284);
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
            this.enumCmbbxRedundancy.Location = new System.Drawing.Point(150, 19);
            this.enumCmbbxRedundancy.Name = "enumCmbbxRedundancy";
            this.enumCmbbxRedundancy.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxRedundancy.StartIndex = 0;
            this.enumCmbbxRedundancy.TabIndex = 0;
            this.enumCmbbxRedundancy.TheType = typeof(HYS.SQLInboundAdapterObjects.EnumBool);
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(37, 23);
            this.lblRedundancy.Name = "lblRedundancy";
            this.lblRedundancy.Size = new System.Drawing.Size(102, 13);
            this.lblRedundancy.TabIndex = 17;
            this.lblRedundancy.Text = "Redundancy Check";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblLUTStar);
            this.groupBox2.Controls.Add(this.enumCmbbxTranslation);
            this.groupBox2.Controls.Add(this.lblTranslationValue);
            this.groupBox2.Controls.Add(this.lblTranslationType);
            this.groupBox2.Controls.Add(this.cmbbxResult);
            this.groupBox2.Controls.Add(this.txtTranslationValue);
            this.groupBox2.Location = new System.Drawing.Point(12, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 98);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Translation";
            // 
            // lblLUTStar
            // 
            this.lblLUTStar.AutoSize = true;
            this.lblLUTStar.ForeColor = System.Drawing.Color.Red;
            this.lblLUTStar.Location = new System.Drawing.Point(139, 62);
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
            this.enumCmbbxTranslation.Location = new System.Drawing.Point(151, 22);
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
            this.lblTranslationValue.Location = new System.Drawing.Point(39, 62);
            this.lblTranslationValue.Name = "lblTranslationValue";
            this.lblTranslationValue.Size = new System.Drawing.Size(89, 13);
            this.lblTranslationValue.TabIndex = 18;
            this.lblTranslationValue.Text = "Translation Value";
            // 
            // lblTranslationType
            // 
            this.lblTranslationType.AutoSize = true;
            this.lblTranslationType.Location = new System.Drawing.Point(37, 25);
            this.lblTranslationType.Name = "lblTranslationType";
            this.lblTranslationType.Size = new System.Drawing.Size(86, 13);
            this.lblTranslationType.TabIndex = 16;
            this.lblTranslationType.Text = "Translation Type";
            // 
            // cmbbxResult
            // 
            this.cmbbxResult.FormattingEnabled = true;
            this.cmbbxResult.Location = new System.Drawing.Point(150, 59);
            this.cmbbxResult.Name = "cmbbxResult";
            this.cmbbxResult.Size = new System.Drawing.Size(263, 21);
            this.cmbbxResult.TabIndex = 1;
            this.cmbbxResult.Visible = false;
            // 
            // txtTranslationValue
            // 
            this.txtTranslationValue.Enabled = false;
            this.txtTranslationValue.Location = new System.Drawing.Point(150, 59);
            this.txtTranslationValue.Name = "txtTranslationValue";
            this.txtTranslationValue.Size = new System.Drawing.Size(263, 20);
            this.txtTranslationValue.TabIndex = 22;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtParameter);
            this.groupBox3.Controls.Add(this.lblParameterStar);
            this.groupBox3.Controls.Add(this.lblFieldName);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 59);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parameter";
            // 
            // txtParameter
            // 
            this.txtParameter.Location = new System.Drawing.Point(152, 21);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(263, 20);
            this.txtParameter.TabIndex = 0;
            // 
            // lblParameterStar
            // 
            this.lblParameterStar.AutoSize = true;
            this.lblParameterStar.ForeColor = System.Drawing.Color.Red;
            this.lblParameterStar.Location = new System.Drawing.Point(139, 24);
            this.lblParameterStar.Name = "lblParameterStar";
            this.lblParameterStar.Size = new System.Drawing.Size(11, 13);
            this.lblParameterStar.TabIndex = 31;
            this.lblParameterStar.Text = "*";
            this.lblParameterStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(38, 23);
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
            this.lblNote.Location = new System.Drawing.Point(10, 343);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 24;
            this.lblNote.Text = "(*: Required)";
            // 
            // QueryParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 381);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryParameter";
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
        private System.Windows.Forms.Label lblTranslationValue;
        private System.Windows.Forms.Label lblTranslationType;
        private EnumComboBox.EnumComboBox enumCmbbxTable;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbbxGWField;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblLUTStar;
        private System.Windows.Forms.Label lblParameterStar;
    }
}