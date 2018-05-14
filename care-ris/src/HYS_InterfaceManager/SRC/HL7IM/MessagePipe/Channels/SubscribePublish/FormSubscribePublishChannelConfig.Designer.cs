namespace HYS.MessageDevices.MessagePipe.Channels.SubscribePublish
{
    partial class FormSubscribePublishChannelConfig
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.lViewProcessor = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.btnEntry = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtBoxEntryCriteria = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEditProcessor = new System.Windows.Forms.Button();
            this.btnDeleteProcessor = new System.Windows.Forms.Button();
            this.btnAddProcessor = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(355, 249);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 29);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(270, 249);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 29);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // lViewProcessor
            // 
            this.lViewProcessor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lViewProcessor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lViewProcessor.FullRowSelect = true;
            this.lViewProcessor.HideSelection = false;
            this.lViewProcessor.Location = new System.Drawing.Point(3, 16);
            this.lViewProcessor.MultiSelect = false;
            this.lViewProcessor.Name = "lViewProcessor";
            this.lViewProcessor.Size = new System.Drawing.Size(273, 95);
            this.lViewProcessor.TabIndex = 7;
            this.lViewProcessor.UseCompatibleStateImageBehavior = false;
            this.lViewProcessor.View = System.Windows.Forms.View.Details;
            this.lViewProcessor.SelectedIndexChanged += new System.EventHandler(this.lViewProcessor_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "DeviceName";
            this.columnHeader3.Width = 83;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 81;
            // 
            // btnEntry
            // 
            this.btnEntry.Location = new System.Drawing.Point(299, 19);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(128, 29);
            this.btnEntry.TabIndex = 8;
            this.btnEntry.Text = "Entry Criteria";
            this.btnEntry.UseVisualStyleBackColor = true;
            this.btnEntry.Click += new System.EventHandler(this.buttonEntry_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(439, 243);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.rtBoxEntryCriteria);
            this.groupBox1.Controls.Add(this.btnEntry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Configuration";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(299, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Publish Message Type";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtBoxEntryCriteria
            // 
            this.rtBoxEntryCriteria.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtBoxEntryCriteria.Location = new System.Drawing.Point(3, 16);
            this.rtBoxEntryCriteria.Name = "rtBoxEntryCriteria";
            this.rtBoxEntryCriteria.ReadOnly = true;
            this.rtBoxEntryCriteria.Size = new System.Drawing.Size(273, 106);
            this.rtBoxEntryCriteria.TabIndex = 9;
            this.rtBoxEntryCriteria.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEditProcessor);
            this.groupBox2.Controls.Add(this.btnDeleteProcessor);
            this.groupBox2.Controls.Add(this.btnAddProcessor);
            this.groupBox2.Controls.Add(this.lViewProcessor);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 114);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Processor Configuration";
            // 
            // btnEditProcessor
            // 
            this.btnEditProcessor.Location = new System.Drawing.Point(299, 77);
            this.btnEditProcessor.Name = "btnEditProcessor";
            this.btnEditProcessor.Size = new System.Drawing.Size(134, 23);
            this.btnEditProcessor.TabIndex = 10;
            this.btnEditProcessor.Text = "Edit Processor";
            this.btnEditProcessor.UseVisualStyleBackColor = true;
            this.btnEditProcessor.Click += new System.EventHandler(this.btnEditProcessor_Click);
            // 
            // btnDeleteProcessor
            // 
            this.btnDeleteProcessor.Location = new System.Drawing.Point(299, 48);
            this.btnDeleteProcessor.Name = "btnDeleteProcessor";
            this.btnDeleteProcessor.Size = new System.Drawing.Size(134, 23);
            this.btnDeleteProcessor.TabIndex = 9;
            this.btnDeleteProcessor.Text = "Delete Processor";
            this.btnDeleteProcessor.UseVisualStyleBackColor = true;
            this.btnDeleteProcessor.Click += new System.EventHandler(this.btnDeleteProcessor_Click);
            // 
            // btnAddProcessor
            // 
            this.btnAddProcessor.Location = new System.Drawing.Point(299, 19);
            this.btnAddProcessor.Name = "btnAddProcessor";
            this.btnAddProcessor.Size = new System.Drawing.Size(134, 23);
            this.btnAddProcessor.TabIndex = 8;
            this.btnAddProcessor.Text = "Add Processor";
            this.btnAddProcessor.UseVisualStyleBackColor = true;
            this.btnAddProcessor.Click += new System.EventHandler(this.btnAddProcessor_Click);
            // 
            // FormSubscribePublishChannelConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 290);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormSubscribePublishChannelConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "One Way Channel Setting";
            this.Load += new System.EventHandler(this.FormSubscribePublishChannelConfig_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListView lViewProcessor;
        private System.Windows.Forms.Button btnEntry;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtBoxEntryCriteria;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEditProcessor;
        private System.Windows.Forms.Button btnDeleteProcessor;
        private System.Windows.Forms.Button btnAddProcessor;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button1;
    }
}