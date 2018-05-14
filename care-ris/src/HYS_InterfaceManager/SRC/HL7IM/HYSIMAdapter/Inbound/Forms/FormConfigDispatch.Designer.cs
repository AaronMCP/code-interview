namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms
{
    partial class FormConfigDispatch
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
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxField = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonRequest = new System.Windows.Forms.RadioButton();
            this.radioButtonPublish = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxValueResponser = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxValueSubscriber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonCustom.Location = new System.Drawing.Point(25, 78);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(529, 27);
            this.radioButtonCustom.TabIndex = 35;
            this.radioButtonCustom.TabStop = true;
            this.radioButtonCustom.Text = "The value of the following database field determines where the cs broker message " +
                "would be dispatched.";
            this.radioButtonCustom.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxField);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxTable);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonCustom);
            this.groupBox1.Controls.Add(this.radioButtonRequest);
            this.groupBox1.Controls.Add(this.radioButtonPublish);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxValueResponser);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxValueSubscriber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 365);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message Dispatching Criteria";
            // 
            // comboBoxField
            // 
            this.comboBoxField.FormattingEnabled = true;
            this.comboBoxField.Location = new System.Drawing.Point(323, 111);
            this.comboBoxField.Name = "comboBoxField";
            this.comboBoxField.Size = new System.Drawing.Size(121, 21);
            this.comboBoxField.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Field:";
            // 
            // comboBoxTable
            // 
            this.comboBoxTable.FormattingEnabled = true;
            this.comboBoxTable.Location = new System.Drawing.Point(134, 111);
            this.comboBoxTable.Name = "comboBoxTable";
            this.comboBoxTable.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTable.TabIndex = 37;
            this.comboBoxTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Database table:";
            // 
            // radioButtonRequest
            // 
            this.radioButtonRequest.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRequest.Location = new System.Drawing.Point(25, 53);
            this.radioButtonRequest.Name = "radioButtonRequest";
            this.radioButtonRequest.Size = new System.Drawing.Size(490, 19);
            this.radioButtonRequest.TabIndex = 34;
            this.radioButtonRequest.TabStop = true;
            this.radioButtonRequest.Text = "Dispatch all cs broker message to request channel.";
            this.radioButtonRequest.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRequest.UseVisualStyleBackColor = true;
            // 
            // radioButtonPublish
            // 
            this.radioButtonPublish.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonPublish.Checked = true;
            this.radioButtonPublish.Location = new System.Drawing.Point(25, 28);
            this.radioButtonPublish.Name = "radioButtonPublish";
            this.radioButtonPublish.Size = new System.Drawing.Size(490, 19);
            this.radioButtonPublish.TabIndex = 33;
            this.radioButtonPublish.TabStop = true;
            this.radioButtonPublish.Text = "Dispatch all cs broker message to publish channel.";
            this.radioButtonPublish.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonPublish.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(137, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(358, 20);
            this.label8.TabIndex = 32;
            this.label8.Text = "Example: value1|value2|value3|...";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueResponser
            // 
            this.textBoxValueResponser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueResponser.Location = new System.Drawing.Point(137, 282);
            this.textBoxValueResponser.Name = "textBoxValueResponser";
            this.textBoxValueResponser.Size = new System.Drawing.Size(411, 20);
            this.textBoxValueResponser.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(53, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(495, 33);
            this.label9.TabIndex = 30;
            this.label9.Text = "The value of database field of cs borker message matches the following regular ex" +
                "pression will be dispatched to request channel:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(137, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(370, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "Example: value1|value2|value3|...";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueSubscriber
            // 
            this.textBoxValueSubscriber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueSubscriber.Location = new System.Drawing.Point(137, 193);
            this.textBoxValueSubscriber.Name = "textBoxValueSubscriber";
            this.textBoxValueSubscriber.Size = new System.Drawing.Size(411, 20);
            this.textBoxValueSubscriber.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(50, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(498, 33);
            this.label6.TabIndex = 21;
            this.label6.Text = "The value of database field of cs borker message matches the following regular ex" +
                "pression will be dispatched to publish channel:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(423, 393);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 25);
            this.buttonOK.TabIndex = 22;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(505, 393);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormConfigDispatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(591, 428);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigDispatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Dispatching Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonRequest;
        private System.Windows.Forms.RadioButton radioButtonPublish;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxValueResponser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxValueSubscriber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTable;
        private System.Windows.Forms.Label label1;
    }
}