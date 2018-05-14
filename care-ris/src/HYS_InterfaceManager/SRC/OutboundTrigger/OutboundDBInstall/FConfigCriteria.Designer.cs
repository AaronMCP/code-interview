namespace OutboundDBInstall
{
    partial class FConfigCriteria
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOutField = new System.Windows.Forms.ComboBox();
            this.comboBoxOutTable = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxInField = new System.Windows.Forms.ComboBox();
            this.comboBoxInTable = new System.Windows.Forms.ComboBox();
            this.textBoxFixValue = new System.Windows.Forms.TextBox();
            this.radioButtonInboundField = new System.Windows.Forms.RadioButton();
            this.radioButtonFixValue = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxLogicOperator = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxOutField);
            this.groupBox1.Controls.Add(this.comboBoxOutTable);
            this.groupBox1.Location = new System.Drawing.Point(31, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Outbound Field";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(314, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Field:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Table:";
            // 
            // comboBoxOutField
            // 
            this.comboBoxOutField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutField.FormattingEnabled = true;
            this.comboBoxOutField.Location = new System.Drawing.Point(92, 63);
            this.comboBoxOutField.Name = "comboBoxOutField";
            this.comboBoxOutField.Size = new System.Drawing.Size(218, 21);
            this.comboBoxOutField.TabIndex = 1;
            // 
            // comboBoxOutTable
            // 
            this.comboBoxOutTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutTable.FormattingEnabled = true;
            this.comboBoxOutTable.Location = new System.Drawing.Point(92, 27);
            this.comboBoxOutTable.Name = "comboBoxOutTable";
            this.comboBoxOutTable.Size = new System.Drawing.Size(218, 21);
            this.comboBoxOutTable.TabIndex = 0;
            this.comboBoxOutTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxOutTable_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBoxInField);
            this.groupBox2.Controls.Add(this.comboBoxInTable);
            this.groupBox2.Controls.Add(this.textBoxFixValue);
            this.groupBox2.Controls.Add(this.radioButtonInboundField);
            this.groupBox2.Controls.Add(this.radioButtonFixValue);
            this.groupBox2.Location = new System.Drawing.Point(31, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 172);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Match Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Field:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Table:";
            // 
            // comboBoxInField
            // 
            this.comboBoxInField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInField.FormattingEnabled = true;
            this.comboBoxInField.Location = new System.Drawing.Point(124, 129);
            this.comboBoxInField.Name = "comboBoxInField";
            this.comboBoxInField.Size = new System.Drawing.Size(186, 21);
            this.comboBoxInField.TabIndex = 5;
            // 
            // comboBoxInTable
            // 
            this.comboBoxInTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInTable.FormattingEnabled = true;
            this.comboBoxInTable.Location = new System.Drawing.Point(124, 93);
            this.comboBoxInTable.Name = "comboBoxInTable";
            this.comboBoxInTable.Size = new System.Drawing.Size(186, 21);
            this.comboBoxInTable.TabIndex = 4;
            this.comboBoxInTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxInTable_SelectedIndexChanged);
            // 
            // textBoxFixValue
            // 
            this.textBoxFixValue.Enabled = false;
            this.textBoxFixValue.Location = new System.Drawing.Point(124, 35);
            this.textBoxFixValue.Name = "textBoxFixValue";
            this.textBoxFixValue.Size = new System.Drawing.Size(186, 20);
            this.textBoxFixValue.TabIndex = 2;
            // 
            // radioButtonInboundField
            // 
            this.radioButtonInboundField.AutoSize = true;
            this.radioButtonInboundField.Checked = true;
            this.radioButtonInboundField.Location = new System.Drawing.Point(36, 70);
            this.radioButtonInboundField.Name = "radioButtonInboundField";
            this.radioButtonInboundField.Size = new System.Drawing.Size(89, 17);
            this.radioButtonInboundField.TabIndex = 1;
            this.radioButtonInboundField.TabStop = true;
            this.radioButtonInboundField.Text = "Inbound Field";
            this.radioButtonInboundField.UseVisualStyleBackColor = true;
            this.radioButtonInboundField.CheckedChanged += new System.EventHandler(this.radioButtonInboundField_CheckedChanged);
            // 
            // radioButtonFixValue
            // 
            this.radioButtonFixValue.AutoSize = true;
            this.radioButtonFixValue.Location = new System.Drawing.Point(36, 36);
            this.radioButtonFixValue.Name = "radioButtonFixValue";
            this.radioButtonFixValue.Size = new System.Drawing.Size(68, 17);
            this.radioButtonFixValue.TabIndex = 0;
            this.radioButtonFixValue.Text = "Fix Value";
            this.radioButtonFixValue.UseVisualStyleBackColor = true;
            this.radioButtonFixValue.CheckedChanged += new System.EventHandler(this.radioButtonFixValue_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Logic Operator:";
            // 
            // comboBoxLogicOperator
            // 
            this.comboBoxLogicOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogicOperator.FormattingEnabled = true;
            this.comboBoxLogicOperator.Location = new System.Drawing.Point(123, 141);
            this.comboBoxLogicOperator.Name = "comboBoxLogicOperator";
            this.comboBoxLogicOperator.Size = new System.Drawing.Size(218, 21);
            this.comboBoxLogicOperator.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(223, 374);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(304, 374);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FConfigCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 419);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxLogicOperator);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FConfigCriteria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Criteria";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOutField;
        private System.Windows.Forms.ComboBox comboBoxOutTable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonInboundField;
        private System.Windows.Forms.RadioButton radioButtonFixValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxInField;
        private System.Windows.Forms.ComboBox comboBoxInTable;
        private System.Windows.Forms.TextBox textBoxFixValue;
        private System.Windows.Forms.ComboBox comboBoxLogicOperator;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label6;
    }
}