namespace HYS.XmlAdapter.Outbound.Forms
{
    partial class FormConfig
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
            this.buttonAdd = new System.Windows.Forms.Button();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.listViewMessage = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.groupBoxXIS = new System.Windows.Forms.GroupBox();
            this.radioButtonSocket = new System.Windows.Forms.RadioButton();
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.textBoxTargetDeviceName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSourceDeviceName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxIPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxMergePrimaryKey = new System.Windows.Forms.ComboBox();
            this.checkBoxMerge = new System.Windows.Forms.CheckBox();
            this.groupBoxSocket = new System.Windows.Forms.GroupBox();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.textBoxSuffix = new System.Windows.Forms.TextBox();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.textBoxTargetDir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonBrowseSource = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.groupBoxXIS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxSocket.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(648, 34);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(87, 27);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "GC Gateway Event Type";
            this.columnHeader4.Width = 198;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "HL7 Event Type";
            this.columnHeader3.Width = 92;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(649, 67);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(87, 27);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(649, 133);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(87, 27);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonCopy);
            this.groupBox1.Controls.Add(this.listViewMessage);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.buttonEdit);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Location = new System.Drawing.Point(12, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 331);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message Mapping";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(649, 100);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(87, 27);
            this.buttonCopy.TabIndex = 8;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // listViewMessage
            // 
            this.listViewMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMessage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewMessage.FullRowSelect = true;
            this.listViewMessage.HideSelection = false;
            this.listViewMessage.Location = new System.Drawing.Point(29, 34);
            this.listViewMessage.MultiSelect = false;
            this.listViewMessage.Name = "listViewMessage";
            this.listViewMessage.Size = new System.Drawing.Size(600, 277);
            this.listViewMessage.TabIndex = 4;
            this.listViewMessage.UseCompatibleStateImageBehavior = false;
            this.listViewMessage.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "XIS Event Type";
            this.columnHeader2.Width = 252;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(106, 56);
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
            this.numericUpDownPort.Size = new System.Drawing.Size(99, 20);
            this.numericUpDownPort.TabIndex = 5;
            this.numericUpDownPort.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBoxXIS
            // 
            this.groupBoxXIS.Controls.Add(this.radioButtonSocket);
            this.groupBoxXIS.Controls.Add(this.radioButtonFile);
            this.groupBoxXIS.Location = new System.Drawing.Point(13, 12);
            this.groupBoxXIS.Name = "groupBoxXIS";
            this.groupBoxXIS.Size = new System.Drawing.Size(232, 99);
            this.groupBoxXIS.TabIndex = 2;
            this.groupBoxXIS.TabStop = false;
            this.groupBoxXIS.Text = "XIM Client Setting";
            // 
            // radioButtonSocket
            // 
            this.radioButtonSocket.Location = new System.Drawing.Point(26, 30);
            this.radioButtonSocket.Name = "radioButtonSocket";
            this.radioButtonSocket.Size = new System.Drawing.Size(200, 24);
            this.radioButtonSocket.TabIndex = 18;
            this.radioButtonSocket.TabStop = true;
            this.radioButtonSocket.Text = "Send Outbound Data With Socket.";
            this.radioButtonSocket.UseVisualStyleBackColor = true;
            this.radioButtonSocket.CheckedChanged += new System.EventHandler(this.radioButtonSocket_CheckedChanged);
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.Location = new System.Drawing.Point(26, 60);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(200, 24);
            this.radioButtonFile.TabIndex = 17;
            this.radioButtonFile.TabStop = true;
            this.radioButtonFile.Text = "Send Outbound Data To File.";
            this.radioButtonFile.UseVisualStyleBackColor = true;
            // 
            // textBoxTargetDeviceName
            // 
            this.textBoxTargetDeviceName.Location = new System.Drawing.Point(365, 56);
            this.textBoxTargetDeviceName.Name = "textBoxTargetDeviceName";
            this.textBoxTargetDeviceName.Size = new System.Drawing.Size(132, 20);
            this.textBoxTargetDeviceName.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(232, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Target Device Name:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSourceDeviceName
            // 
            this.textBoxSourceDeviceName.Location = new System.Drawing.Point(365, 30);
            this.textBoxSourceDeviceName.Name = "textBoxSourceDeviceName";
            this.textBoxSourceDeviceName.Size = new System.Drawing.Size(132, 20);
            this.textBoxSourceDeviceName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(232, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Originating Device Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxIPAddress
            // 
            this.textBoxIPAddress.Location = new System.Drawing.Point(106, 30);
            this.textBoxIPAddress.Name = "textBoxIPAddress";
            this.textBoxIPAddress.Size = new System.Drawing.Size(99, 20);
            this.textBoxIPAddress.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server IP:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(308, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "ms";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(158, 24);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(144, 20);
            this.numericUpDownInterval.TabIndex = 11;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Data Checking Interval:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBoxMergePrimaryKey);
            this.groupBox2.Controls.Add(this.numericUpDownInterval);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.checkBoxMerge);
            this.groupBox2.Location = new System.Drawing.Point(12, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 94);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Process";
            // 
            // comboBoxMergePrimaryKey
            // 
            this.comboBoxMergePrimaryKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMergePrimaryKey.Enabled = false;
            this.comboBoxMergePrimaryKey.FormattingEnabled = true;
            this.comboBoxMergePrimaryKey.Items.AddRange(new object[] {
            "Patient ID",
            "Accession Number",
            "Study Instance UID"});
            this.comboBoxMergePrimaryKey.Location = new System.Drawing.Point(523, 55);
            this.comboBoxMergePrimaryKey.Name = "comboBoxMergePrimaryKey";
            this.comboBoxMergePrimaryKey.Size = new System.Drawing.Size(212, 21);
            this.comboBoxMergePrimaryKey.TabIndex = 2;
            // 
            // checkBoxMerge
            // 
            this.checkBoxMerge.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxMerge.Location = new System.Drawing.Point(28, 57);
            this.checkBoxMerge.Name = "checkBoxMerge";
            this.checkBoxMerge.Size = new System.Drawing.Size(489, 24);
            this.checkBoxMerge.TabIndex = 0;
            this.checkBoxMerge.Text = "Merge Multiple SCHEDULED_PROCEDURE_STEP Items Into One XIM Document, According To" +
                "";
            this.checkBoxMerge.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxMerge.UseVisualStyleBackColor = true;
            this.checkBoxMerge.CheckedChanged += new System.EventHandler(this.checkBoxMerge_CheckedChanged);
            // 
            // groupBoxSocket
            // 
            this.groupBoxSocket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSocket.Controls.Add(this.textBoxTargetDeviceName);
            this.groupBoxSocket.Controls.Add(this.textBoxSourceDeviceName);
            this.groupBoxSocket.Controls.Add(this.textBoxIPAddress);
            this.groupBoxSocket.Controls.Add(this.numericUpDownPort);
            this.groupBoxSocket.Controls.Add(this.label7);
            this.groupBoxSocket.Controls.Add(this.label1);
            this.groupBoxSocket.Controls.Add(this.label6);
            this.groupBoxSocket.Controls.Add(this.label2);
            this.groupBoxSocket.Location = new System.Drawing.Point(251, 12);
            this.groupBoxSocket.Name = "groupBoxSocket";
            this.groupBoxSocket.Size = new System.Drawing.Size(516, 99);
            this.groupBoxSocket.TabIndex = 5;
            this.groupBoxSocket.TabStop = false;
            this.groupBoxSocket.Text = "Socket Setting";
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFile.Controls.Add(this.textBoxSuffix);
            this.groupBoxFile.Controls.Add(this.textBoxPrefix);
            this.groupBoxFile.Controls.Add(this.textBoxTargetDir);
            this.groupBoxFile.Controls.Add(this.label5);
            this.groupBoxFile.Controls.Add(this.buttonBrowseSource);
            this.groupBoxFile.Controls.Add(this.label8);
            this.groupBoxFile.Controls.Add(this.label10);
            this.groupBoxFile.Location = new System.Drawing.Point(251, 12);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(516, 99);
            this.groupBoxFile.TabIndex = 6;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Setting";
            // 
            // textBoxSuffix
            // 
            this.textBoxSuffix.Location = new System.Drawing.Point(326, 56);
            this.textBoxSuffix.Name = "textBoxSuffix";
            this.textBoxSuffix.Size = new System.Drawing.Size(136, 20);
            this.textBoxSuffix.TabIndex = 19;
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(127, 56);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(136, 20);
            this.textBoxPrefix.TabIndex = 17;
            // 
            // textBoxTargetDir
            // 
            this.textBoxTargetDir.Location = new System.Drawing.Point(127, 30);
            this.textBoxTargetDir.Name = "textBoxTargetDir";
            this.textBoxTargetDir.Size = new System.Drawing.Size(335, 20);
            this.textBoxTargetDir.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(278, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Suffix:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonBrowseSource
            // 
            this.buttonBrowseSource.Location = new System.Drawing.Point(468, 30);
            this.buttonBrowseSource.Name = "buttonBrowseSource";
            this.buttonBrowseSource.Size = new System.Drawing.Size(26, 20);
            this.buttonBrowseSource.TabIndex = 16;
            this.buttonBrowseSource.Text = "...";
            this.buttonBrowseSource.UseVisualStyleBackColor = true;
            this.buttonBrowseSource.Click += new System.EventHandler(this.buttonBrowseSource_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(26, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "File Name Prefix:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(26, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 20);
            this.label10.TabIndex = 6;
            this.label10.Text = "Target Directory:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 560);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxSocket);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxXIS);
            this.Name = "FormConfig";
            this.Text = "XIM Outbound Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfig_FormClosing);
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.groupBoxXIS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBoxSocket.ResumeLayout(false);
            this.groupBoxSocket.PerformLayout();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewMessage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.GroupBox groupBoxXIS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIPAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxMerge;
        private System.Windows.Forms.ComboBox comboBoxMergePrimaryKey;
        private System.Windows.Forms.TextBox textBoxTargetDeviceName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSourceDeviceName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxSocket;
        private System.Windows.Forms.RadioButton radioButtonSocket;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.TextBox textBoxTargetDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonBrowseSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.TextBox textBoxSuffix;
        private System.Windows.Forms.Button buttonCopy;
    }
}