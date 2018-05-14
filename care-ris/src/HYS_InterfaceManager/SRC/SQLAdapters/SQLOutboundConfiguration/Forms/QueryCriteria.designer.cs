namespace HYS.SQLOutboundAdapterConfiguration.Forms
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.enumCmbbxOperator = new EnumComboBox.EnumComboBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.enumCmbbxTable = new EnumComboBox.EnumComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.cmbbxGWField = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.btnOK.Location = new System.Drawing.Point(296, 269);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(382, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.enumCmbbxOperator);
            this.groupBox3.Controls.Add(this.lblOperator);
            this.groupBox3.Controls.Add(this.txtValue);
            this.groupBox3.Controls.Add(this.lblValue);
            this.groupBox3.Controls.Add(this.enumCmbbxTable);
            this.groupBox3.Controls.Add(this.lblTable);
            this.groupBox3.Controls.Add(this.lblField);
            this.groupBox3.Controls.Add(this.cmbbxGWField);
            this.groupBox3.Location = new System.Drawing.Point(13, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 173);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Filter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(136, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(136, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(136, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(136, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.enumCmbbxOperator.Location = new System.Drawing.Point(151, 97);
            this.enumCmbbxOperator.Name = "enumCmbbxOperator";
            this.enumCmbbxOperator.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxOperator.StartIndex = 0;
            this.enumCmbbxOperator.TabIndex = 2;
            this.enumCmbbxOperator.TheType = typeof(HYS.Common.Objects.Rule.QueryCriteriaOperator);
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(48, 101);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(48, 13);
            this.lblOperator.TabIndex = 22;
            this.lblOperator.Text = "Operator";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(150, 136);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(264, 20);
            this.txtValue.TabIndex = 3;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(48, 140);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 20;
            this.lblValue.Text = "Value";
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
            this.enumCmbbxTable.Location = new System.Drawing.Point(150, 20);
            this.enumCmbbxTable.Name = "enumCmbbxTable";
            this.enumCmbbxTable.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxTable.StartIndex = 0;
            this.enumCmbbxTable.TabIndex = 0;
            this.enumCmbbxTable.TheType = typeof(HYS.Common.Objects.Rule.GWDataDBTable);
            this.enumCmbbxTable.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxTable_SelectedIndexChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(48, 24);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(65, 13);
            this.lblTable.TabIndex = 17;
            this.lblTable.Text = "Table Name";
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(48, 62);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(60, 13);
            this.lblField.TabIndex = 16;
            this.lblField.Text = "Field Name";
            // 
            // cmbbxGWField
            // 
            this.cmbbxGWField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGWField.FormattingEnabled = true;
            this.cmbbxGWField.Location = new System.Drawing.Point(150, 58);
            this.cmbbxGWField.MaxDropDownItems = 20;
            this.cmbbxGWField.Name = "cmbbxGWField";
            this.cmbbxGWField.Size = new System.Drawing.Size(265, 21);
            this.cmbbxGWField.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.enumCmbbxJoin);
            this.groupBox2.Controls.Add(this.lblJoin);
            this.groupBox2.Location = new System.Drawing.Point(13, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 68);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Join Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(136, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enumCmbbxJoin
            // 
            this.enumCmbbxJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxJoin.FormattingEnabled = true;
            this.enumCmbbxJoin.Items.AddRange(new object[] {
            "And",
            "Or"});
            this.enumCmbbxJoin.Location = new System.Drawing.Point(150, 28);
            this.enumCmbbxJoin.Name = "enumCmbbxJoin";
            this.enumCmbbxJoin.Size = new System.Drawing.Size(265, 21);
            this.enumCmbbxJoin.StartIndex = 1;
            this.enumCmbbxJoin.TabIndex = 0;
            this.enumCmbbxJoin.TheType = typeof(HYS.Common.Objects.Rule.QueryCriteriaType);
            // 
            // lblJoin
            // 
            this.lblJoin.AutoSize = true;
            this.lblJoin.Location = new System.Drawing.Point(48, 31);
            this.lblJoin.Name = "lblJoin";
            this.lblJoin.Size = new System.Drawing.Size(62, 13);
            this.lblJoin.TabIndex = 17;
            this.lblJoin.Text = "Type Name";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(16, 257);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(66, 13);
            this.lblNote.TabIndex = 21;
            this.lblNote.Text = "(*: Required)";
            // 
            // QueryCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 297);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryCriteria";
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
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbbxGWField;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblJoin;
        private EnumComboBox.EnumComboBox enumCmbbxJoin;
        private EnumComboBox.EnumComboBox enumCmbbxTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private EnumComboBox.EnumComboBox enumCmbbxOperator;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}