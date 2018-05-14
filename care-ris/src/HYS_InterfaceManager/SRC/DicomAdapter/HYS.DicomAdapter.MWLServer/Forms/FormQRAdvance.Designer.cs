namespace HYS.DicomAdapter.MWLServer.Forms
{
    partial class FormQRAdvance
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
            this.checkedListBoxMerge = new System.Windows.Forms.CheckedListBox();
            this.groupBoxSplittingRule = new System.Windows.Forms.GroupBox();
            this.textBoxSeperator = new System.Windows.Forms.TextBox();
            this.checkedListBoxSplit = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelMerge = new System.Windows.Forms.Panel();
            this.radioButtonMergerDisable = new System.Windows.Forms.RadioButton();
            this.radioButtonMergerEnable = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonSpliterDisable = new System.Windows.Forms.RadioButton();
            this.radioButtonSpliterEnable = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxCharacterSet = new System.Windows.Forms.GroupBox();
            this.comboBoxCharacterSet = new System.Windows.Forms.ComboBox();
            this.labelCharacterSet = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panelMerge.SuspendLayout();
            this.groupBoxCharacterSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBoxMerge
            // 
            this.checkedListBoxMerge.CheckOnClick = true;
            this.checkedListBoxMerge.Enabled = false;
            this.checkedListBoxMerge.FormattingEnabled = true;
            this.checkedListBoxMerge.Items.AddRange(new object[] {
            "Study Instance UID (0020,000D)",
            "Accession Number (0008,0050)",
            "Patient ID (0010,0020)"});
            this.checkedListBoxMerge.Location = new System.Drawing.Point(22, 58);
            this.checkedListBoxMerge.Name = "checkedListBoxMerge";
            this.checkedListBoxMerge.Size = new System.Drawing.Size(442, 64);
            this.checkedListBoxMerge.TabIndex = 1;
            this.checkedListBoxMerge.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxMerge_ItemCheck);
            // 
            // groupBoxSplittingRule
            // 
            this.groupBoxSplittingRule.Location = new System.Drawing.Point(72, 113);
            this.groupBoxSplittingRule.Name = "groupBoxSplittingRule";
            this.groupBoxSplittingRule.Size = new System.Drawing.Size(445, 2);
            this.groupBoxSplittingRule.TabIndex = 20;
            this.groupBoxSplittingRule.TabStop = false;
            // 
            // textBoxSeperator
            // 
            this.textBoxSeperator.Enabled = false;
            this.textBoxSeperator.Location = new System.Drawing.Point(280, 132);
            this.textBoxSeperator.MaxLength = 1;
            this.textBoxSeperator.Name = "textBoxSeperator";
            this.textBoxSeperator.Size = new System.Drawing.Size(17, 20);
            this.textBoxSeperator.TabIndex = 2;
            this.textBoxSeperator.Text = "^";
            // 
            // checkedListBoxSplit
            // 
            this.checkedListBoxSplit.CheckOnClick = true;
            this.checkedListBoxSplit.Enabled = false;
            this.checkedListBoxSplit.FormattingEnabled = true;
            this.checkedListBoxSplit.Items.AddRange(new object[] {
            "Code Value (0008,0100) In Scheduled Protocol Code Sequence (0040,0008)",
            "Code Value (0008,0100) In Requested Procedure Code Sequence (0032,1064)"});
            this.checkedListBoxSplit.Location = new System.Drawing.Point(75, 165);
            this.checkedListBoxSplit.Name = "checkedListBoxSplit";
            this.checkedListBoxSplit.Size = new System.Drawing.Size(442, 64);
            this.checkedListBoxSplit.TabIndex = 1;
            this.checkedListBoxSplit.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxSplit_ItemCheck);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(72, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(445, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Multiple Protocol Codes Are Seperated By           In The Following Element.";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(474, 508);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(372, 508);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 22;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelMerge);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.radioButtonSpliterDisable);
            this.groupBox2.Controls.Add(this.textBoxSeperator);
            this.groupBox2.Controls.Add(this.radioButtonSpliterEnable);
            this.groupBox2.Controls.Add(this.groupBoxSplittingRule);
            this.groupBox2.Controls.Add(this.checkedListBoxSplit);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 418);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Protocol Code Handling Rule";
            // 
            // panelMerge
            // 
            this.panelMerge.Controls.Add(this.radioButtonMergerDisable);
            this.panelMerge.Controls.Add(this.radioButtonMergerEnable);
            this.panelMerge.Controls.Add(this.checkedListBoxMerge);
            this.panelMerge.Location = new System.Drawing.Point(53, 274);
            this.panelMerge.Name = "panelMerge";
            this.panelMerge.Size = new System.Drawing.Size(473, 131);
            this.panelMerge.TabIndex = 30;
            // 
            // radioButtonMergerDisable
            // 
            this.radioButtonMergerDisable.AutoSize = true;
            this.radioButtonMergerDisable.Checked = true;
            this.radioButtonMergerDisable.Location = new System.Drawing.Point(3, 3);
            this.radioButtonMergerDisable.Name = "radioButtonMergerDisable";
            this.radioButtonMergerDisable.Size = new System.Drawing.Size(186, 17);
            this.radioButtonMergerDisable.TabIndex = 28;
            this.radioButtonMergerDisable.TabStop = true;
            this.radioButtonMergerDisable.Text = "Create Multiple DICOM Data Sets.";
            this.radioButtonMergerDisable.UseVisualStyleBackColor = true;
            // 
            // radioButtonMergerEnable
            // 
            this.radioButtonMergerEnable.AutoSize = true;
            this.radioButtonMergerEnable.Location = new System.Drawing.Point(3, 26);
            this.radioButtonMergerEnable.Name = "radioButtonMergerEnable";
            this.radioButtonMergerEnable.Size = new System.Drawing.Size(479, 17);
            this.radioButtonMergerEnable.TabIndex = 29;
            this.radioButtonMergerEnable.Text = "Create Multiple Scheduled Protocol Code Sequence Items According To The Following" +
                " Element.";
            this.radioButtonMergerEnable.UseVisualStyleBackColor = true;
            this.radioButtonMergerEnable.CheckedChanged += new System.EventHandler(this.radioButtonMergerEnable_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(465, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "Please select how to provide mulitple Protocol Codes in DICOM Modality Work List:" +
                "";
            // 
            // radioButtonSpliterDisable
            // 
            this.radioButtonSpliterDisable.AutoSize = true;
            this.radioButtonSpliterDisable.Checked = true;
            this.radioButtonSpliterDisable.Location = new System.Drawing.Point(53, 62);
            this.radioButtonSpliterDisable.Name = "radioButtonSpliterDisable";
            this.radioButtonSpliterDisable.Size = new System.Drawing.Size(303, 17);
            this.radioButtonSpliterDisable.TabIndex = 27;
            this.radioButtonSpliterDisable.TabStop = true;
            this.radioButtonSpliterDisable.Text = "Multiple Protocol Codes Are Contained In Multiple Records.";
            this.radioButtonSpliterDisable.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpliterEnable
            // 
            this.radioButtonSpliterEnable.AutoSize = true;
            this.radioButtonSpliterEnable.Location = new System.Drawing.Point(52, 85);
            this.radioButtonSpliterEnable.Name = "radioButtonSpliterEnable";
            this.radioButtonSpliterEnable.Size = new System.Drawing.Size(301, 17);
            this.radioButtonSpliterEnable.TabIndex = 26;
            this.radioButtonSpliterEnable.Text = "Multiple Protocol Codes Are Contained In A Single Record.";
            this.radioButtonSpliterEnable.UseVisualStyleBackColor = true;
            this.radioButtonSpliterEnable.CheckedChanged += new System.EventHandler(this.radioButtonSpliterEnable_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(465, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "Please select how to identify mulitple Protocol Codes in GC Gateway data set:";
            // 
            // groupBoxCharacterSet
            // 
            this.groupBoxCharacterSet.Controls.Add(this.comboBoxCharacterSet);
            this.groupBoxCharacterSet.Controls.Add(this.labelCharacterSet);
            this.groupBoxCharacterSet.Location = new System.Drawing.Point(12, 436);
            this.groupBoxCharacterSet.Name = "groupBoxCharacterSet";
            this.groupBoxCharacterSet.Size = new System.Drawing.Size(550, 66);
            this.groupBoxCharacterSet.TabIndex = 25;
            this.groupBoxCharacterSet.TabStop = false;
            this.groupBoxCharacterSet.Text = "Character Set";
            // 
            // comboBoxCharacterSet
            // 
            this.comboBoxCharacterSet.FormattingEnabled = true;
            this.comboBoxCharacterSet.Items.AddRange(new object[] {
            "GB18030",
            "ISO_IR 6",
            "ISO_IR 192",
            "ISO_IR 100",
            "ISO_IR 101",
            "ISO_IR 109",
            "ISO_IR 110",
            "ISO_IR 148",
            "ISO_IR 144",
            "ISO_IR 127",
            "ISO_IR 126",
            "ISO_IR 138",
            "ISO 2022 IR 13",
            "ISO 2022 IR 87",
            "ISO 2022 IR 149",
            "ISO 2022 IR 13\\ISO 2022 IR 87"});
            this.comboBoxCharacterSet.Location = new System.Drawing.Point(117, 27);
            this.comboBoxCharacterSet.MaxDropDownItems = 12;
            this.comboBoxCharacterSet.Name = "comboBoxCharacterSet";
            this.comboBoxCharacterSet.Size = new System.Drawing.Size(399, 21);
            this.comboBoxCharacterSet.TabIndex = 27;
            // 
            // labelCharacterSet
            // 
            this.labelCharacterSet.Location = new System.Drawing.Point(19, 27);
            this.labelCharacterSet.Name = "labelCharacterSet";
            this.labelCharacterSet.Size = new System.Drawing.Size(134, 18);
            this.labelCharacterSet.TabIndex = 26;
            this.labelCharacterSet.Text = "Character Set:";
            this.labelCharacterSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormQRAdvance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 547);
            this.Controls.Add(this.groupBoxCharacterSet);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQRAdvance";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "C-FIND-RSP Advance Setting";
            this.Load += new System.EventHandler(this.FormQRAdvance_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelMerge.ResumeLayout(false);
            this.panelMerge.PerformLayout();
            this.groupBoxCharacterSet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSplittingRule;
        private System.Windows.Forms.CheckedListBox checkedListBoxSplit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSeperator;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckedListBox checkedListBoxMerge;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonSpliterEnable;
        private System.Windows.Forms.RadioButton radioButtonSpliterDisable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonMergerEnable;
        private System.Windows.Forms.RadioButton radioButtonMergerDisable;
        private System.Windows.Forms.Panel panelMerge;
        private System.Windows.Forms.GroupBox groupBoxCharacterSet;
        private System.Windows.Forms.Label labelCharacterSet;
        private System.Windows.Forms.ComboBox comboBoxCharacterSet;
    }
}