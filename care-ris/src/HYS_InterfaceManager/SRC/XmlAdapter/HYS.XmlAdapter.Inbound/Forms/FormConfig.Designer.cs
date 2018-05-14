namespace HYS.XmlAdapter.Inbound.Forms
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
            this.groupBoxXIS = new System.Windows.Forms.GroupBox();
            this.radioButtonSocket = new System.Windows.Forms.RadioButton();
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.listViewMessage = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.groupBoxSocket = new System.Windows.Forms.GroupBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSouceDeviceName = new System.Windows.Forms.TextBox();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.radioButtonMove = new System.Windows.Forms.RadioButton();
            this.radioButtonDelete = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonBrowseSource = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownTimerInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSourceDir = new System.Windows.Forms.TextBox();
            this.groupBoxXIS.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxSocket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.groupBoxFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxXIS
            // 
            this.groupBoxXIS.Controls.Add(this.radioButtonSocket);
            this.groupBoxXIS.Controls.Add(this.radioButtonFile);
            this.groupBoxXIS.Location = new System.Drawing.Point(13, 12);
            this.groupBoxXIS.Name = "groupBoxXIS";
            this.groupBoxXIS.Size = new System.Drawing.Size(218, 119);
            this.groupBoxXIS.TabIndex = 0;
            this.groupBoxXIS.TabStop = false;
            this.groupBoxXIS.Text = "XIM Server Setting";
            // 
            // radioButtonSocket
            // 
            this.radioButtonSocket.Location = new System.Drawing.Point(25, 32);
            this.radioButtonSocket.Name = "radioButtonSocket";
            this.radioButtonSocket.Size = new System.Drawing.Size(177, 24);
            this.radioButtonSocket.TabIndex = 16;
            this.radioButtonSocket.TabStop = true;
            this.radioButtonSocket.Text = "Get Inbound Data From Socket.";
            this.radioButtonSocket.UseVisualStyleBackColor = true;
            this.radioButtonSocket.CheckedChanged += new System.EventHandler(this.radioButtonSocket_CheckedChanged);
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.Location = new System.Drawing.Point(25, 62);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(177, 24);
            this.radioButtonFile.TabIndex = 15;
            this.radioButtonFile.TabStop = true;
            this.radioButtonFile.Text = "Get Inbound Data From File.";
            this.radioButtonFile.UseVisualStyleBackColor = true;
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
            this.groupBox1.Location = new System.Drawing.Point(12, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 356);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message Mapping";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(649, 101);
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
            this.listViewMessage.Size = new System.Drawing.Size(600, 302);
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
            // columnHeader3
            // 
            this.columnHeader3.Text = "HL7 Event Type";
            this.columnHeader3.Width = 92;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "GC Gateway Event Type";
            this.columnHeader4.Width = 198;
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
            this.buttonDelete.Location = new System.Drawing.Point(648, 135);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(87, 27);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // groupBoxSocket
            // 
            this.groupBoxSocket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSocket.Controls.Add(this.numericUpDownPort);
            this.groupBoxSocket.Controls.Add(this.label1);
            this.groupBoxSocket.Controls.Add(this.label2);
            this.groupBoxSocket.Controls.Add(this.textBoxSouceDeviceName);
            this.groupBoxSocket.Location = new System.Drawing.Point(237, 12);
            this.groupBoxSocket.Name = "groupBoxSocket";
            this.groupBoxSocket.Size = new System.Drawing.Size(531, 119);
            this.groupBoxSocket.TabIndex = 17;
            this.groupBoxSocket.TabStop = false;
            this.groupBoxSocket.Text = "Socket Setting";
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(161, 33);
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
            this.numericUpDownPort.Size = new System.Drawing.Size(163, 20);
            this.numericUpDownPort.TabIndex = 9;
            this.numericUpDownPort.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Listening on Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Originating Device Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSouceDeviceName
            // 
            this.textBoxSouceDeviceName.Location = new System.Drawing.Point(161, 61);
            this.textBoxSouceDeviceName.Name = "textBoxSouceDeviceName";
            this.textBoxSouceDeviceName.Size = new System.Drawing.Size(163, 20);
            this.textBoxSouceDeviceName.TabIndex = 11;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFile.Controls.Add(this.radioButtonMove);
            this.groupBoxFile.Controls.Add(this.radioButtonDelete);
            this.groupBoxFile.Controls.Add(this.label7);
            this.groupBoxFile.Controls.Add(this.buttonBrowseSource);
            this.groupBoxFile.Controls.Add(this.label6);
            this.groupBoxFile.Controls.Add(this.numericUpDownTimerInterval);
            this.groupBoxFile.Controls.Add(this.label3);
            this.groupBoxFile.Controls.Add(this.label4);
            this.groupBoxFile.Controls.Add(this.textBoxSourceDir);
            this.groupBoxFile.Location = new System.Drawing.Point(237, 12);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(531, 119);
            this.groupBoxFile.TabIndex = 18;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Setting";
            // 
            // radioButtonMove
            // 
            this.radioButtonMove.Location = new System.Drawing.Point(195, 84);
            this.radioButtonMove.Name = "radioButtonMove";
            this.radioButtonMove.Size = new System.Drawing.Size(237, 24);
            this.radioButtonMove.TabIndex = 18;
            this.radioButtonMove.TabStop = true;
            this.radioButtonMove.Text = "Move To Backup Folder.";
            this.radioButtonMove.UseVisualStyleBackColor = true;
            // 
            // radioButtonDelete
            // 
            this.radioButtonDelete.Location = new System.Drawing.Point(129, 84);
            this.radioButtonDelete.Name = "radioButtonDelete";
            this.radioButtonDelete.Size = new System.Drawing.Size(70, 24);
            this.radioButtonDelete.TabIndex = 17;
            this.radioButtonDelete.TabStop = true;
            this.radioButtonDelete.Text = "Delete";
            this.radioButtonDelete.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Succeeded File:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonBrowseSource
            // 
            this.buttonBrowseSource.Location = new System.Drawing.Point(491, 30);
            this.buttonBrowseSource.Name = "buttonBrowseSource";
            this.buttonBrowseSource.Size = new System.Drawing.Size(26, 20);
            this.buttonBrowseSource.TabIndex = 15;
            this.buttonBrowseSource.Text = "...";
            this.buttonBrowseSource.UseVisualStyleBackColor = true;
            this.buttonBrowseSource.Click += new System.EventHandler(this.buttonBrowseSource_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(300, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "ms";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTimerInterval
            // 
            this.numericUpDownTimerInterval.Location = new System.Drawing.Point(129, 57);
            this.numericUpDownTimerInterval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownTimerInterval.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownTimerInterval.Name = "numericUpDownTimerInterval";
            this.numericUpDownTimerInterval.Size = new System.Drawing.Size(163, 20);
            this.numericUpDownTimerInterval.TabIndex = 9;
            this.numericUpDownTimerInterval.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Check Interval:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Source Directory:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSourceDir
            // 
            this.textBoxSourceDir.Location = new System.Drawing.Point(129, 30);
            this.textBoxSourceDir.Name = "textBoxSourceDir";
            this.textBoxSourceDir.Size = new System.Drawing.Size(356, 20);
            this.textBoxSourceDir.TabIndex = 11;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 505);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxSocket);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxXIS);
            this.Name = "FormConfig";
            this.Text = "XIM Inbound Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfig_FormClosing);
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBoxXIS.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxSocket.ResumeLayout(false);
            this.groupBoxSocket.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxXIS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewMessage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.RadioButton radioButtonSocket;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.GroupBox groupBoxSocket;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSouceDeviceName;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.Button buttonBrowseSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownTimerInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSourceDir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButtonMove;
        private System.Windows.Forms.RadioButton radioButtonDelete;
        private System.Windows.Forms.Button buttonCopy;
    }
}