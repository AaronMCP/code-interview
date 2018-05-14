namespace HYS.DicomAdapter.MWLServer.Forms
{
    partial class FormQCAdvance
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
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonSingleByte = new System.Windows.Forms.RadioButton();
            this.radioButtonIdeographic = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonBackward = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.checkedListBoxComponents = new System.Windows.Forms.CheckedListBox();
            this.radioButtonPhonetic = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonUpCriteria = new System.Windows.Forms.Button();
            this.buttonDownCriteria = new System.Windows.Forms.Button();
            this.buttonDeleteCriteria = new System.Windows.Forms.Button();
            this.buttonEditCriteria = new System.Windows.Forms.Button();
            this.buttonAddCriteria = new System.Windows.Forms.Button();
            this.listViewCriteria = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(510, 36);
            this.label4.TabIndex = 26;
            this.label4.Text = "Please select the compoents to be used as query criteria and arrange the order of" +
                " these components:";
            // 
            // radioButtonSingleByte
            // 
            this.radioButtonSingleByte.AutoSize = true;
            this.radioButtonSingleByte.Checked = true;
            this.radioButtonSingleByte.Location = new System.Drawing.Point(53, 62);
            this.radioButtonSingleByte.Name = "radioButtonSingleByte";
            this.radioButtonSingleByte.Size = new System.Drawing.Size(296, 17);
            this.radioButtonSingleByte.TabIndex = 27;
            this.radioButtonSingleByte.TabStop = true;
            this.radioButtonSingleByte.Text = "Single-byte Character Representation (English Character).";
            this.radioButtonSingleByte.UseVisualStyleBackColor = true;
            // 
            // radioButtonIdeographic
            // 
            this.radioButtonIdeographic.AutoSize = true;
            this.radioButtonIdeographic.Location = new System.Drawing.Point(52, 85);
            this.radioButtonIdeographic.Name = "radioButtonIdeographic";
            this.radioButtonIdeographic.Size = new System.Drawing.Size(255, 17);
            this.radioButtonIdeographic.TabIndex = 26;
            this.radioButtonIdeographic.Text = "Ideographic Representation (Chinese Character).";
            this.radioButtonIdeographic.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(465, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "Please select which compoents group to be used as query criteria:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonBackward);
            this.groupBox2.Controls.Add(this.buttonForward);
            this.groupBox2.Controls.Add(this.checkedListBoxComponents);
            this.groupBox2.Controls.Add(this.radioButtonPhonetic);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.radioButtonSingleByte);
            this.groupBox2.Controls.Add(this.radioButtonIdeographic);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(535, 277);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Person Name Parsing Rule";
            // 
            // buttonBackward
            // 
            this.buttonBackward.Location = new System.Drawing.Point(295, 195);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(136, 24);
            this.buttonBackward.TabIndex = 34;
            this.buttonBackward.Text = "Move Backward";
            this.buttonBackward.UseVisualStyleBackColor = true;
            this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(295, 165);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(136, 24);
            this.buttonForward.TabIndex = 33;
            this.buttonForward.Text = "Move Forward";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // checkedListBoxComponents
            // 
            this.checkedListBoxComponents.FormattingEnabled = true;
            this.checkedListBoxComponents.Items.AddRange(new object[] {
            "Family Name",
            "Given Name",
            "Middle Name",
            "Prefix",
            "Suffix"});
            this.checkedListBoxComponents.Location = new System.Drawing.Point(53, 165);
            this.checkedListBoxComponents.Name = "checkedListBoxComponents";
            this.checkedListBoxComponents.Size = new System.Drawing.Size(224, 94);
            this.checkedListBoxComponents.TabIndex = 32;
            this.checkedListBoxComponents.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxComponents_SelectedIndexChanged);
            this.checkedListBoxComponents.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxComponents_ItemCheck);
            // 
            // radioButtonPhonetic
            // 
            this.radioButtonPhonetic.AutoSize = true;
            this.radioButtonPhonetic.Location = new System.Drawing.Point(52, 108);
            this.radioButtonPhonetic.Name = "radioButtonPhonetic";
            this.radioButtonPhonetic.Size = new System.Drawing.Size(145, 17);
            this.radioButtonPhonetic.TabIndex = 31;
            this.radioButtonPhonetic.Text = "Phonetic Representation.";
            this.radioButtonPhonetic.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(458, 552);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 26;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(356, 552);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 25;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonDefault
            // 
            this.buttonDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDefault.Location = new System.Drawing.Point(12, 552);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(93, 27);
            this.buttonDefault.TabIndex = 34;
            this.buttonDefault.Text = "Default";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonUpCriteria);
            this.groupBox1.Controls.Add(this.buttonDownCriteria);
            this.groupBox1.Controls.Add(this.buttonDeleteCriteria);
            this.groupBox1.Controls.Add(this.buttonEditCriteria);
            this.groupBox1.Controls.Add(this.buttonAddCriteria);
            this.groupBox1.Controls.Add(this.listViewCriteria);
            this.groupBox1.Controls.Add(this.comboBoxOperator);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 295);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 244);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional Query Criteria";
            // 
            // buttonUpCriteria
            // 
            this.buttonUpCriteria.Location = new System.Drawing.Point(431, 165);
            this.buttonUpCriteria.Name = "buttonUpCriteria";
            this.buttonUpCriteria.Size = new System.Drawing.Size(80, 27);
            this.buttonUpCriteria.TabIndex = 42;
            this.buttonUpCriteria.Text = "Move Up";
            this.buttonUpCriteria.UseVisualStyleBackColor = true;
            this.buttonUpCriteria.Click += new System.EventHandler(this.buttonUpCriteria_Click);
            // 
            // buttonDownCriteria
            // 
            this.buttonDownCriteria.Location = new System.Drawing.Point(431, 198);
            this.buttonDownCriteria.Name = "buttonDownCriteria";
            this.buttonDownCriteria.Size = new System.Drawing.Size(80, 27);
            this.buttonDownCriteria.TabIndex = 41;
            this.buttonDownCriteria.Text = "Move Down";
            this.buttonDownCriteria.UseVisualStyleBackColor = true;
            this.buttonDownCriteria.Click += new System.EventHandler(this.buttonDownCriteria_Click);
            // 
            // buttonDeleteCriteria
            // 
            this.buttonDeleteCriteria.Location = new System.Drawing.Point(431, 132);
            this.buttonDeleteCriteria.Name = "buttonDeleteCriteria";
            this.buttonDeleteCriteria.Size = new System.Drawing.Size(80, 27);
            this.buttonDeleteCriteria.TabIndex = 40;
            this.buttonDeleteCriteria.Text = "Delete";
            this.buttonDeleteCriteria.UseVisualStyleBackColor = true;
            this.buttonDeleteCriteria.Click += new System.EventHandler(this.buttonDeleteCriteria_Click);
            // 
            // buttonEditCriteria
            // 
            this.buttonEditCriteria.Location = new System.Drawing.Point(430, 99);
            this.buttonEditCriteria.Name = "buttonEditCriteria";
            this.buttonEditCriteria.Size = new System.Drawing.Size(80, 27);
            this.buttonEditCriteria.TabIndex = 39;
            this.buttonEditCriteria.Text = "Edit";
            this.buttonEditCriteria.UseVisualStyleBackColor = true;
            this.buttonEditCriteria.Click += new System.EventHandler(this.buttonEditCriteria_Click);
            // 
            // buttonAddCriteria
            // 
            this.buttonAddCriteria.Location = new System.Drawing.Point(430, 66);
            this.buttonAddCriteria.Name = "buttonAddCriteria";
            this.buttonAddCriteria.Size = new System.Drawing.Size(80, 27);
            this.buttonAddCriteria.TabIndex = 38;
            this.buttonAddCriteria.Text = "Add";
            this.buttonAddCriteria.UseVisualStyleBackColor = true;
            this.buttonAddCriteria.Click += new System.EventHandler(this.buttonAddCriteria_Click);
            // 
            // listViewCriteria
            // 
            this.listViewCriteria.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewCriteria.FullRowSelect = true;
            this.listViewCriteria.HideSelection = false;
            this.listViewCriteria.Location = new System.Drawing.Point(22, 66);
            this.listViewCriteria.MultiSelect = false;
            this.listViewCriteria.Name = "listViewCriteria";
            this.listViewCriteria.Size = new System.Drawing.Size(393, 159);
            this.listViewCriteria.TabIndex = 37;
            this.listViewCriteria.UseCompatibleStateImageBehavior = false;
            this.listViewCriteria.View = System.Windows.Forms.View.Details;
            this.listViewCriteria.SelectedIndexChanged += new System.EventHandler(this.listViewCriteria_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "GC Gateway Field";
            this.columnHeader1.Width = 148;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Operator ";
            this.columnHeader2.Width = 87;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Fix Value";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Join Type";
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(430, 26);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(80, 21);
            this.comboBoxOperator.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 21);
            this.label1.TabIndex = 35;
            this.label1.Text = "The logical result of the following criteria will join C-FIND-RQ criteria with op" +
                "erator:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormQCAdvance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 591);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQCAdvance";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "C-FIND-RQ Advance Setting";
            this.Load += new System.EventHandler(this.FormQCAdvance_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonSingleByte;
        private System.Windows.Forms.RadioButton radioButtonIdeographic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton radioButtonPhonetic;
        private System.Windows.Forms.CheckedListBox checkedListBoxComponents;
        private System.Windows.Forms.Button buttonBackward;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.ListView listViewCriteria;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonDeleteCriteria;
        private System.Windows.Forms.Button buttonEditCriteria;
        private System.Windows.Forms.Button buttonAddCriteria;
        private System.Windows.Forms.Button buttonDownCriteria;
        private System.Windows.Forms.Button buttonUpCriteria;
    }
}