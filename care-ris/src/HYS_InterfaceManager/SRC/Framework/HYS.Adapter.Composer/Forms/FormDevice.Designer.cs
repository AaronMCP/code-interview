namespace HYS.Adapter.Composer.Forms
{
    partial class FormDevice
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelDevice = new System.Windows.Forms.Label();
            this.propertyGridHeader = new System.Windows.Forms.PropertyGrid();
            this.buttonDefaultInbound = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonFileCheck = new System.Windows.Forms.Button();
            this.listBoxFile = new System.Windows.Forms.ListBox();
            this.buttonFileDelete = new System.Windows.Forms.Button();
            this.buttonFileAdd = new System.Windows.Forms.Button();
            this.propertyGridFile = new System.Windows.Forms.PropertyGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonCommandCheck = new System.Windows.Forms.Button();
            this.listBoxCommand = new System.Windows.Forms.ListBox();
            this.buttonCommandDelete = new System.Windows.Forms.Button();
            this.buttonCommandAdd = new System.Windows.Forms.Button();
            this.propertyGridCommand = new System.Windows.Forms.PropertyGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDefaultOutbound = new System.Windows.Forms.Button();
            this.buttonDefaultBidirectional = new System.Windows.Forms.Button();
            this.buttonDeviceName = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 29;
            this.label2.Text = "1. Header";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(13, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 2);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "DeviceDir file content";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(236, 44);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(126, 32);
            this.buttonSave.TabIndex = 26;
            this.buttonSave.Text = "Save/Create DeviceDir";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(136, 43);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(94, 33);
            this.buttonLoad.TabIndex = 25;
            this.buttonLoad.Text = "Load DeviceDir";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(933, 17);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(26, 21);
            this.buttonBrowse.TabIndex = 24;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLocation.Location = new System.Drawing.Point(136, 17);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(780, 20);
            this.textBoxLocation.TabIndex = 23;
            // 
            // labelDevice
            // 
            this.labelDevice.Location = new System.Drawing.Point(10, 17);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(120, 21);
            this.labelDevice.TabIndex = 22;
            this.labelDevice.Text = "DeviceDir location:";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // propertyGridHeader
            // 
            this.propertyGridHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridHeader.Location = new System.Drawing.Point(14, 122);
            this.propertyGridHeader.Name = "propertyGridHeader";
            this.propertyGridHeader.Size = new System.Drawing.Size(945, 298);
            this.propertyGridHeader.TabIndex = 21;
            this.propertyGridHeader.ToolbarVisible = false;
            // 
            // buttonDefaultInbound
            // 
            this.buttonDefaultInbound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefaultInbound.Location = new System.Drawing.Point(368, 44);
            this.buttonDefaultInbound.Name = "buttonDefaultInbound";
            this.buttonDefaultInbound.Size = new System.Drawing.Size(160, 44);
            this.buttonDefaultInbound.TabIndex = 40;
            this.buttonDefaultInbound.Text = "Set default inbound DeviceDir (for .NET adapter)";
            this.buttonDefaultInbound.UseVisualStyleBackColor = true;
            this.buttonDefaultInbound.Click += new System.EventHandler(this.buttonDefaultInbound_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(14, 426);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(946, 323);
            this.tabControl1.TabIndex = 41;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonFileCheck);
            this.tabPage1.Controls.Add(this.listBoxFile);
            this.tabPage1.Controls.Add(this.buttonFileDelete);
            this.tabPage1.Controls.Add(this.buttonFileAdd);
            this.tabPage1.Controls.Add(this.propertyGridFile);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(938, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonFileCheck
            // 
            this.buttonFileCheck.Location = new System.Drawing.Point(311, 8);
            this.buttonFileCheck.Name = "buttonFileCheck";
            this.buttonFileCheck.Size = new System.Drawing.Size(81, 21);
            this.buttonFileCheck.TabIndex = 44;
            this.buttonFileCheck.Text = "Check";
            this.buttonFileCheck.UseVisualStyleBackColor = true;
            this.buttonFileCheck.Click += new System.EventHandler(this.buttonFileCheck_Click);
            // 
            // listBoxFile
            // 
            this.listBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxFile.FormattingEnabled = true;
            this.listBoxFile.Location = new System.Drawing.Point(6, 35);
            this.listBoxFile.Name = "listBoxFile";
            this.listBoxFile.Size = new System.Drawing.Size(386, 251);
            this.listBoxFile.TabIndex = 43;
            this.listBoxFile.SelectedIndexChanged += new System.EventHandler(this.listBoxFile_SelectedIndexChanged);
            // 
            // buttonFileDelete
            // 
            this.buttonFileDelete.Location = new System.Drawing.Point(249, 8);
            this.buttonFileDelete.Name = "buttonFileDelete";
            this.buttonFileDelete.Size = new System.Drawing.Size(56, 21);
            this.buttonFileDelete.TabIndex = 42;
            this.buttonFileDelete.Text = "Delete";
            this.buttonFileDelete.UseVisualStyleBackColor = true;
            this.buttonFileDelete.Click += new System.EventHandler(this.buttonFileDelete_Click);
            // 
            // buttonFileAdd
            // 
            this.buttonFileAdd.Location = new System.Drawing.Point(205, 8);
            this.buttonFileAdd.Name = "buttonFileAdd";
            this.buttonFileAdd.Size = new System.Drawing.Size(38, 21);
            this.buttonFileAdd.TabIndex = 41;
            this.buttonFileAdd.Text = "Add";
            this.buttonFileAdd.UseVisualStyleBackColor = true;
            this.buttonFileAdd.Click += new System.EventHandler(this.buttonFileAdd_Click);
            // 
            // propertyGridFile
            // 
            this.propertyGridFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridFile.Location = new System.Drawing.Point(398, 8);
            this.propertyGridFile.Name = "propertyGridFile";
            this.propertyGridFile.Size = new System.Drawing.Size(534, 278);
            this.propertyGridFile.TabIndex = 40;
            this.propertyGridFile.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridFile_PropertyValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 21);
            this.label3.TabIndex = 39;
            this.label3.Text = "2. Files";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonCommandCheck);
            this.tabPage2.Controls.Add(this.listBoxCommand);
            this.tabPage2.Controls.Add(this.buttonCommandDelete);
            this.tabPage2.Controls.Add(this.buttonCommandAdd);
            this.tabPage2.Controls.Add(this.propertyGridCommand);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(938, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Command List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonCommandCheck
            // 
            this.buttonCommandCheck.Location = new System.Drawing.Point(311, 8);
            this.buttonCommandCheck.Name = "buttonCommandCheck";
            this.buttonCommandCheck.Size = new System.Drawing.Size(81, 21);
            this.buttonCommandCheck.TabIndex = 45;
            this.buttonCommandCheck.Text = "Check";
            this.buttonCommandCheck.UseVisualStyleBackColor = true;
            this.buttonCommandCheck.Click += new System.EventHandler(this.buttonCommandCheck_Click);
            // 
            // listBoxCommand
            // 
            this.listBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxCommand.FormattingEnabled = true;
            this.listBoxCommand.Location = new System.Drawing.Point(6, 35);
            this.listBoxCommand.Name = "listBoxCommand";
            this.listBoxCommand.Size = new System.Drawing.Size(386, 251);
            this.listBoxCommand.TabIndex = 44;
            this.listBoxCommand.SelectedIndexChanged += new System.EventHandler(this.listBoxCommand_SelectedIndexChanged);
            // 
            // buttonCommandDelete
            // 
            this.buttonCommandDelete.Location = new System.Drawing.Point(249, 8);
            this.buttonCommandDelete.Name = "buttonCommandDelete";
            this.buttonCommandDelete.Size = new System.Drawing.Size(56, 21);
            this.buttonCommandDelete.TabIndex = 43;
            this.buttonCommandDelete.Text = "Delete";
            this.buttonCommandDelete.UseVisualStyleBackColor = true;
            this.buttonCommandDelete.Click += new System.EventHandler(this.buttonCommandDelete_Click);
            // 
            // buttonCommandAdd
            // 
            this.buttonCommandAdd.Location = new System.Drawing.Point(205, 8);
            this.buttonCommandAdd.Name = "buttonCommandAdd";
            this.buttonCommandAdd.Size = new System.Drawing.Size(38, 21);
            this.buttonCommandAdd.TabIndex = 42;
            this.buttonCommandAdd.Text = "Add";
            this.buttonCommandAdd.UseVisualStyleBackColor = true;
            this.buttonCommandAdd.Click += new System.EventHandler(this.buttonCommandAdd_Click);
            // 
            // propertyGridCommand
            // 
            this.propertyGridCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridCommand.Location = new System.Drawing.Point(398, 8);
            this.propertyGridCommand.Name = "propertyGridCommand";
            this.propertyGridCommand.Size = new System.Drawing.Size(456, 278);
            this.propertyGridCommand.TabIndex = 41;
            this.propertyGridCommand.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridCommand_PropertyValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 21);
            this.label4.TabIndex = 40;
            this.label4.Text = "3. Commands";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDefaultOutbound
            // 
            this.buttonDefaultOutbound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefaultOutbound.Location = new System.Drawing.Point(534, 44);
            this.buttonDefaultOutbound.Name = "buttonDefaultOutbound";
            this.buttonDefaultOutbound.Size = new System.Drawing.Size(167, 44);
            this.buttonDefaultOutbound.TabIndex = 42;
            this.buttonDefaultOutbound.Text = "Set default outbound DeviceDir (for .NET adapter)";
            this.buttonDefaultOutbound.UseVisualStyleBackColor = true;
            this.buttonDefaultOutbound.Click += new System.EventHandler(this.buttonDefaultOutbound_Click);
            // 
            // buttonDefaultBidirectional
            // 
            this.buttonDefaultBidirectional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefaultBidirectional.Location = new System.Drawing.Point(707, 44);
            this.buttonDefaultBidirectional.Name = "buttonDefaultBidirectional";
            this.buttonDefaultBidirectional.Size = new System.Drawing.Size(158, 44);
            this.buttonDefaultBidirectional.TabIndex = 43;
            this.buttonDefaultBidirectional.Text = "Set default bidirectional DeviceDir (for .NET adapter)";
            this.buttonDefaultBidirectional.UseVisualStyleBackColor = true;
            this.buttonDefaultBidirectional.Click += new System.EventHandler(this.buttonDefaultBidirectional_Click);
            // 
            // buttonDeviceName
            // 
            this.buttonDeviceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeviceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeviceName.Location = new System.Drawing.Point(871, 44);
            this.buttonDeviceName.Name = "buttonDeviceName";
            this.buttonDeviceName.Size = new System.Drawing.Size(85, 44);
            this.buttonDeviceName.TabIndex = 44;
            this.buttonDeviceName.Text = "Licensed Device Name";
            this.buttonDeviceName.UseVisualStyleBackColor = true;
            this.buttonDeviceName.Click += new System.EventHandler(this.buttonDeviceName_Click);
            // 
            // FormDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 770);
            this.Controls.Add(this.buttonDeviceName);
            this.Controls.Add(this.buttonDefaultBidirectional);
            this.Controls.Add(this.buttonDefaultOutbound);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonDefaultInbound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.propertyGridHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDevice";
            this.Text = "Device Editor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.PropertyGrid propertyGridHeader;
        private System.Windows.Forms.Button buttonDefaultInbound;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox listBoxFile;
        private System.Windows.Forms.Button buttonFileDelete;
        private System.Windows.Forms.Button buttonFileAdd;
        private System.Windows.Forms.PropertyGrid propertyGridFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBoxCommand;
        private System.Windows.Forms.Button buttonCommandDelete;
        private System.Windows.Forms.Button buttonCommandAdd;
        private System.Windows.Forms.PropertyGrid propertyGridCommand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDefaultOutbound;
        private System.Windows.Forms.Button buttonDefaultBidirectional;
        private System.Windows.Forms.Button buttonFileCheck;
        private System.Windows.Forms.Button buttonCommandCheck;
        private System.Windows.Forms.Button buttonDeviceName;
    }
}

