namespace HYS.DicomAdapter.Common
{
    partial class FormSCP
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
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAETitle = new System.Windows.Forms.TextBox();
            this.labelAETitle = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPDULength = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownTimeOut = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableAETitleChecking = new System.Windows.Forms.CheckBox();
            this.panelList = new System.Windows.Forms.Panel();
            this.listViewModality = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonEcho = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.radioButtonNotAllowAll = new System.Windows.Forms.RadioButton();
            this.radioButtonAllowAll = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPDULength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeOut)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxAETitle);
            this.groupBox1.Controls.Add(this.labelAETitle);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Entity";
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(118, 67);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(170, 20);
            this.numericUpDownPort.TabIndex = 3;
            this.numericUpDownPort.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxAETitle
            // 
            this.textBoxAETitle.Location = new System.Drawing.Point(118, 30);
            this.textBoxAETitle.Name = "textBoxAETitle";
            this.textBoxAETitle.Size = new System.Drawing.Size(171, 20);
            this.textBoxAETitle.TabIndex = 1;
            // 
            // labelAETitle
            // 
            this.labelAETitle.Location = new System.Drawing.Point(27, 30);
            this.labelAETitle.Name = "labelAETitle";
            this.labelAETitle.Size = new System.Drawing.Size(88, 20);
            this.labelAETitle.TabIndex = 0;
            this.labelAETitle.Text = "AE Title:";
            this.labelAETitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDownPDULength);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericUpDownTimeOut);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(349, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 111);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DICOM Communication";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(332, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "KB";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(332, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "ms";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownPDULength
            // 
            this.numericUpDownPDULength.Location = new System.Drawing.Point(176, 30);
            this.numericUpDownPDULength.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownPDULength.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownPDULength.Name = "numericUpDownPDULength";
            this.numericUpDownPDULength.Size = new System.Drawing.Size(150, 20);
            this.numericUpDownPDULength.TabIndex = 5;
            this.numericUpDownPDULength.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(27, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "PDU Max Length:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTimeOut
            // 
            this.numericUpDownTimeOut.Location = new System.Drawing.Point(176, 65);
            this.numericUpDownTimeOut.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownTimeOut.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTimeOut.Name = "numericUpDownTimeOut";
            this.numericUpDownTimeOut.Size = new System.Drawing.Size(150, 20);
            this.numericUpDownTimeOut.TabIndex = 3;
            this.numericUpDownTimeOut.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Association Time Out:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBoxEnableAETitleChecking);
            this.groupBox3.Controls.Add(this.panelList);
            this.groupBox3.Controls.Add(this.radioButtonNotAllowAll);
            this.groupBox3.Controls.Add(this.radioButtonAllowAll);
            this.groupBox3.Location = new System.Drawing.Point(13, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(732, 365);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Access Control";
            // 
            // checkBoxEnableAETitleChecking
            // 
            this.checkBoxEnableAETitleChecking.AutoSize = true;
            this.checkBoxEnableAETitleChecking.Location = new System.Drawing.Point(32, 322);
            this.checkBoxEnableAETitleChecking.Name = "checkBoxEnableAETitleChecking";
            this.checkBoxEnableAETitleChecking.Size = new System.Drawing.Size(185, 17);
            this.checkBoxEnableAETitleChecking.TabIndex = 8;
            this.checkBoxEnableAETitleChecking.Text = "Enabled Called AE Title Checking";
            this.checkBoxEnableAETitleChecking.UseVisualStyleBackColor = true;
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelList.Controls.Add(this.listViewModality);
            this.panelList.Controls.Add(this.buttonAdd);
            this.panelList.Controls.Add(this.buttonEdit);
            this.panelList.Controls.Add(this.buttonEcho);
            this.panelList.Controls.Add(this.buttonDelete);
            this.panelList.Enabled = false;
            this.panelList.Location = new System.Drawing.Point(11, 63);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(696, 243);
            this.panelList.TabIndex = 7;
            // 
            // listViewModality
            // 
            this.listViewModality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewModality.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5});
            this.listViewModality.FullRowSelect = true;
            this.listViewModality.HideSelection = false;
            this.listViewModality.Location = new System.Drawing.Point(20, 10);
            this.listViewModality.MultiSelect = false;
            this.listViewModality.Name = "listViewModality";
            this.listViewModality.Size = new System.Drawing.Size(558, 226);
            this.listViewModality.TabIndex = 0;
            this.listViewModality.UseCompatibleStateImageBehavior = false;
            this.listViewModality.View = System.Windows.Forms.View.Details;
            this.listViewModality.SelectedIndexChanged += new System.EventHandler(this.listViewModality_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "AETitle";
            this.columnHeader2.Width = 130;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IP Address";
            this.columnHeader3.Width = 138;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Description ";
            this.columnHeader5.Width = 185;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(595, 10);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(87, 27);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(596, 43);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(87, 27);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonEcho
            // 
            this.buttonEcho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEcho.Location = new System.Drawing.Point(595, 209);
            this.buttonEcho.Name = "buttonEcho";
            this.buttonEcho.Size = new System.Drawing.Size(87, 27);
            this.buttonEcho.TabIndex = 4;
            this.buttonEcho.Text = "Echo";
            this.buttonEcho.UseVisualStyleBackColor = true;
            this.buttonEcho.Visible = false;
            this.buttonEcho.Click += new System.EventHandler(this.buttonEcho_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(596, 76);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(87, 27);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // radioButtonNotAllowAll
            // 
            this.radioButtonNotAllowAll.Location = new System.Drawing.Point(32, 26);
            this.radioButtonNotAllowAll.Name = "radioButtonNotAllowAll";
            this.radioButtonNotAllowAll.Size = new System.Drawing.Size(210, 31);
            this.radioButtonNotAllowAll.TabIndex = 6;
            this.radioButtonNotAllowAll.TabStop = true;
            this.radioButtonNotAllowAll.Text = "Only Allow Modalities In Following List.";
            this.radioButtonNotAllowAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllowAll
            // 
            this.radioButtonAllowAll.Checked = true;
            this.radioButtonAllowAll.Location = new System.Drawing.Point(290, 26);
            this.radioButtonAllowAll.Name = "radioButtonAllowAll";
            this.radioButtonAllowAll.Size = new System.Drawing.Size(362, 31);
            this.radioButtonAllowAll.TabIndex = 5;
            this.radioButtonAllowAll.TabStop = true;
            this.radioButtonAllowAll.Text = "Allow Any Modality To Access.";
            this.radioButtonAllowAll.UseVisualStyleBackColor = true;
            this.radioButtonAllowAll.CheckedChanged += new System.EventHandler(this.radioButtonAllowAll_CheckedChanged);
            // 
            // FormSCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 511);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormSCP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SCP Setting";
            this.Load += new System.EventHandler(this.FormModality_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormModality_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPDULength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeOut)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelAETitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAETitle;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPDULength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listViewModality;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonEcho;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.RadioButton radioButtonNotAllowAll;
        private System.Windows.Forms.RadioButton radioButtonAllowAll;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.CheckBox checkBoxEnableAETitleChecking;
    }
}

