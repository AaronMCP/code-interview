namespace HYS.MessageDevices.MessagePipe.Channels.RequestResponse
{
    partial class FormRequestResponseChannelConfig
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
            this.buttonEntry = new System.Windows.Forms.Button();
            this.lViewResponseProcessor = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtBoxEntryCriteria = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEditRequestProcessor = new System.Windows.Forms.Button();
            this.btnDeleteRequestProcessor = new System.Windows.Forms.Button();
            this.btnAddRequestProcessor = new System.Windows.Forms.Button();
            this.lViewRequestProcessor = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEditResponseProcessor = new System.Windows.Forms.Button();
            this.btnDeleteResponseProcessor = new System.Windows.Forms.Button();
            this.btnAddResponseProcessor = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEntry
            // 
            this.buttonEntry.Location = new System.Drawing.Point(325, 19);
            this.buttonEntry.Name = "buttonEntry";
            this.buttonEntry.Size = new System.Drawing.Size(136, 29);
            this.buttonEntry.TabIndex = 13;
            this.buttonEntry.Text = "Entry Criteria";
            this.buttonEntry.UseVisualStyleBackColor = true;
            this.buttonEntry.Click += new System.EventHandler(this.buttonEntry_Click);
            // 
            // lViewResponseProcessor
            // 
            this.lViewResponseProcessor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader8});
            this.lViewResponseProcessor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lViewResponseProcessor.FullRowSelect = true;
            this.lViewResponseProcessor.HideSelection = false;
            this.lViewResponseProcessor.Location = new System.Drawing.Point(3, 16);
            this.lViewResponseProcessor.MultiSelect = false;
            this.lViewResponseProcessor.Name = "lViewResponseProcessor";
            this.lViewResponseProcessor.Size = new System.Drawing.Size(304, 99);
            this.lViewResponseProcessor.TabIndex = 12;
            this.lViewResponseProcessor.UseCompatibleStateImageBehavior = false;
            this.lViewResponseProcessor.View = System.Windows.Forms.View.Details;
            this.lViewResponseProcessor.SelectedIndexChanged += new System.EventHandler(this.lViewResponseProcessor_SelectedIndexChanged);
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
            this.columnHeader3.Text = "Device Name";
            this.columnHeader3.Width = 93;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Description";
            this.columnHeader8.Width = 106;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(399, 368);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 29);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(308, 368);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 29);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(473, 362);
            this.splitContainer1.SplitterDistance = 121;
            this.splitContainer1.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.rtBoxEntryCriteria);
            this.groupBox1.Controls.Add(this.buttonEntry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 121);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entry Configuration";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(325, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 29);
            this.button1.TabIndex = 15;
            this.button1.Text = "Request Message Types";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtBoxEntryCriteria
            // 
            this.rtBoxEntryCriteria.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtBoxEntryCriteria.Location = new System.Drawing.Point(3, 16);
            this.rtBoxEntryCriteria.Name = "rtBoxEntryCriteria";
            this.rtBoxEntryCriteria.Size = new System.Drawing.Size(304, 102);
            this.rtBoxEntryCriteria.TabIndex = 14;
            this.rtBoxEntryCriteria.Text = "";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(473, 237);
            this.splitContainer2.SplitterDistance = 115;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEditRequestProcessor);
            this.groupBox3.Controls.Add(this.btnDeleteRequestProcessor);
            this.groupBox3.Controls.Add(this.btnAddRequestProcessor);
            this.groupBox3.Controls.Add(this.lViewRequestProcessor);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(473, 115);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Request Processor Configuration";
            // 
            // btnEditRequestProcessor
            // 
            this.btnEditRequestProcessor.Location = new System.Drawing.Point(339, 77);
            this.btnEditRequestProcessor.Name = "btnEditRequestProcessor";
            this.btnEditRequestProcessor.Size = new System.Drawing.Size(105, 23);
            this.btnEditRequestProcessor.TabIndex = 17;
            this.btnEditRequestProcessor.Text = "Edit";
            this.btnEditRequestProcessor.UseVisualStyleBackColor = true;
            this.btnEditRequestProcessor.Click += new System.EventHandler(this.btnEditRequestProcessor_Click);
            // 
            // btnDeleteRequestProcessor
            // 
            this.btnDeleteRequestProcessor.Location = new System.Drawing.Point(339, 48);
            this.btnDeleteRequestProcessor.Name = "btnDeleteRequestProcessor";
            this.btnDeleteRequestProcessor.Size = new System.Drawing.Size(105, 23);
            this.btnDeleteRequestProcessor.TabIndex = 16;
            this.btnDeleteRequestProcessor.Text = "Delete";
            this.btnDeleteRequestProcessor.UseVisualStyleBackColor = true;
            this.btnDeleteRequestProcessor.Click += new System.EventHandler(this.btnDeleteRequestProcessor_Click);
            // 
            // btnAddRequestProcessor
            // 
            this.btnAddRequestProcessor.Location = new System.Drawing.Point(339, 19);
            this.btnAddRequestProcessor.Name = "btnAddRequestProcessor";
            this.btnAddRequestProcessor.Size = new System.Drawing.Size(105, 23);
            this.btnAddRequestProcessor.TabIndex = 15;
            this.btnAddRequestProcessor.Text = "Add";
            this.btnAddRequestProcessor.UseVisualStyleBackColor = true;
            this.btnAddRequestProcessor.Click += new System.EventHandler(this.btnAddRequestProcessor_Click);
            // 
            // lViewRequestProcessor
            // 
            this.lViewRequestProcessor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lViewRequestProcessor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lViewRequestProcessor.FullRowSelect = true;
            this.lViewRequestProcessor.HideSelection = false;
            this.lViewRequestProcessor.Location = new System.Drawing.Point(3, 16);
            this.lViewRequestProcessor.MultiSelect = false;
            this.lViewRequestProcessor.Name = "lViewRequestProcessor";
            this.lViewRequestProcessor.Size = new System.Drawing.Size(304, 96);
            this.lViewRequestProcessor.TabIndex = 0;
            this.lViewRequestProcessor.UseCompatibleStateImageBehavior = false;
            this.lViewRequestProcessor.View = System.Windows.Forms.View.Details;
            this.lViewRequestProcessor.SelectedIndexChanged += new System.EventHandler(this.lViewRequestProcessor_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "No";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Device Name";
            this.columnHeader6.Width = 98;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Descriprion";
            this.columnHeader7.Width = 76;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEditResponseProcessor);
            this.groupBox2.Controls.Add(this.btnDeleteResponseProcessor);
            this.groupBox2.Controls.Add(this.btnAddResponseProcessor);
            this.groupBox2.Controls.Add(this.lViewResponseProcessor);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 118);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Response Processor Configuration";
            // 
            // btnEditResponseProcessor
            // 
            this.btnEditResponseProcessor.Location = new System.Drawing.Point(339, 77);
            this.btnEditResponseProcessor.Name = "btnEditResponseProcessor";
            this.btnEditResponseProcessor.Size = new System.Drawing.Size(105, 23);
            this.btnEditResponseProcessor.TabIndex = 20;
            this.btnEditResponseProcessor.Text = "Edit";
            this.btnEditResponseProcessor.UseVisualStyleBackColor = true;
            this.btnEditResponseProcessor.Click += new System.EventHandler(this.btnEditResponseProcessor_Click);
            // 
            // btnDeleteResponseProcessor
            // 
            this.btnDeleteResponseProcessor.Location = new System.Drawing.Point(339, 48);
            this.btnDeleteResponseProcessor.Name = "btnDeleteResponseProcessor";
            this.btnDeleteResponseProcessor.Size = new System.Drawing.Size(105, 23);
            this.btnDeleteResponseProcessor.TabIndex = 19;
            this.btnDeleteResponseProcessor.Text = "Delete";
            this.btnDeleteResponseProcessor.UseVisualStyleBackColor = true;
            this.btnDeleteResponseProcessor.Click += new System.EventHandler(this.btnDeleteResponseProcessor_Click);
            // 
            // btnAddResponseProcessor
            // 
            this.btnAddResponseProcessor.Location = new System.Drawing.Point(339, 19);
            this.btnAddResponseProcessor.Name = "btnAddResponseProcessor";
            this.btnAddResponseProcessor.Size = new System.Drawing.Size(105, 23);
            this.btnAddResponseProcessor.TabIndex = 18;
            this.btnAddResponseProcessor.Text = "Add";
            this.btnAddResponseProcessor.UseVisualStyleBackColor = true;
            this.btnAddResponseProcessor.Click += new System.EventHandler(this.btnAddResponseProcessor_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(325, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 29);
            this.button2.TabIndex = 16;
            this.button2.Text = "Response Message Type";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormRequestResponseChannelConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 409);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormRequestResponseChannelConfig";
            this.Text = "Duplex Channel Setting";
            this.Load += new System.EventHandler(this.FormRequestResponseChannelConfig_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEntry;
        private System.Windows.Forms.ListView lViewResponseProcessor;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lViewRequestProcessor;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.RichTextBox rtBoxEntryCriteria;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnDeleteRequestProcessor;
        private System.Windows.Forms.Button btnAddRequestProcessor;
        private System.Windows.Forms.Button btnEditRequestProcessor;
        private System.Windows.Forms.Button btnEditResponseProcessor;
        private System.Windows.Forms.Button btnDeleteResponseProcessor;
        private System.Windows.Forms.Button btnAddResponseProcessor;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}