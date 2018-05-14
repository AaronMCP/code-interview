namespace HYS.IM.Forms
{
    partial class DeviceView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelList = new System.Windows.Forms.Panel();
            this.labelTitle1 = new System.Windows.Forms.Label();
            this.listViewDevice = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageFile = new System.Windows.Forms.TabPage();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.tabPageCommand = new System.Windows.Forms.TabPage();
            this.listViewCommand = new System.Windows.Forms.ListView();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelDirection = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle2 = new System.Windows.Forms.Label();
            this.contextMenuStripDevice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.browseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateInterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageFile.SuspendLayout();
            this.tabPageCommand.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStripDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelList);
            this.panelMain.Controls.Add(this.splitterMain);
            this.panelMain.Controls.Add(this.panelDetail);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(848, 425);
            this.panelMain.TabIndex = 0;
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.labelTitle1);
            this.panelList.Controls.Add(this.listViewDevice);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelList.Location = new System.Drawing.Point(0, 0);
            this.panelList.Margin = new System.Windows.Forms.Padding(4);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(848, 153);
            this.panelList.TabIndex = 2;
            // 
            // labelTitle1
            // 
            this.labelTitle1.BackColor = System.Drawing.Color.DarkGray;
            this.labelTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle1.Location = new System.Drawing.Point(0, 0);
            this.labelTitle1.Name = "labelTitle1";
            this.labelTitle1.Size = new System.Drawing.Size(848, 23);
            this.labelTitle1.TabIndex = 1;
            this.labelTitle1.Text = "Device List";
            this.labelTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewDevice
            // 
            this.listViewDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDevice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewDevice.FullRowSelect = true;
            this.listViewDevice.HideSelection = false;
            this.listViewDevice.Location = new System.Drawing.Point(0, 27);
            this.listViewDevice.Margin = new System.Windows.Forms.Padding(4);
            this.listViewDevice.MultiSelect = false;
            this.listViewDevice.Name = "listViewDevice";
            this.listViewDevice.Size = new System.Drawing.Size(847, 126);
            this.listViewDevice.TabIndex = 0;
            this.listViewDevice.UseCompatibleStateImageBehavior = false;
            this.listViewDevice.View = System.Windows.Forms.View.Details;
            this.listViewDevice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewDevice_MouseClick);
            this.listViewDevice.SelectedIndexChanged += new System.EventHandler(this.listViewDevice_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Device ID";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Device Name";
            this.columnHeader2.Width = 173;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Direction";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 121;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Device Path";
            this.columnHeader5.Width = 235;
            // 
            // splitterMain
            // 
            this.splitterMain.BackColor = System.Drawing.SystemColors.Control;
            this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterMain.Location = new System.Drawing.Point(0, 153);
            this.splitterMain.Margin = new System.Windows.Forms.Padding(4);
            this.splitterMain.MinSize = 32;
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(848, 3);
            this.splitterMain.TabIndex = 1;
            this.splitterMain.TabStop = false;
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = System.Drawing.SystemColors.Control;
            this.panelDetail.Controls.Add(this.tabControl1);
            this.panelDetail.Controls.Add(this.groupBox1);
            this.panelDetail.Controls.Add(this.labelTitle2);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDetail.Location = new System.Drawing.Point(0, 156);
            this.panelDetail.Margin = new System.Windows.Forms.Padding(4);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(848, 269);
            this.panelDetail.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageFile);
            this.tabControl1.Controls.Add(this.tabPageCommand);
            this.tabControl1.Location = new System.Drawing.Point(324, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 217);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageFile
            // 
            this.tabPageFile.Controls.Add(this.listViewFile);
            this.tabPageFile.Location = new System.Drawing.Point(4, 25);
            this.tabPageFile.Name = "tabPageFile";
            this.tabPageFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFile.Size = new System.Drawing.Size(504, 188);
            this.tabPageFile.TabIndex = 0;
            this.tabPageFile.Text = "File List";
            this.tabPageFile.UseVisualStyleBackColor = true;
            // 
            // listViewFile
            // 
            this.listViewFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFile.FullRowSelect = true;
            this.listViewFile.Location = new System.Drawing.Point(3, 3);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(498, 182);
            this.listViewFile.TabIndex = 0;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#";
            this.columnHeader6.Width = 27;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Name";
            this.columnHeader7.Width = 129;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Type";
            this.columnHeader8.Width = 71;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Description";
            this.columnHeader9.Width = 133;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Backupable";
            this.columnHeader10.Width = 85;
            // 
            // tabPageCommand
            // 
            this.tabPageCommand.Controls.Add(this.listViewCommand);
            this.tabPageCommand.Location = new System.Drawing.Point(4, 25);
            this.tabPageCommand.Name = "tabPageCommand";
            this.tabPageCommand.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommand.Size = new System.Drawing.Size(504, 188);
            this.tabPageCommand.TabIndex = 1;
            this.tabPageCommand.Text = "Command List";
            this.tabPageCommand.UseVisualStyleBackColor = true;
            // 
            // listViewCommand
            // 
            this.listViewCommand.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader13,
            this.columnHeader12,
            this.columnHeader15,
            this.columnHeader14});
            this.listViewCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCommand.FullRowSelect = true;
            this.listViewCommand.Location = new System.Drawing.Point(3, 3);
            this.listViewCommand.MultiSelect = false;
            this.listViewCommand.Name = "listViewCommand";
            this.listViewCommand.Size = new System.Drawing.Size(498, 182);
            this.listViewCommand.TabIndex = 1;
            this.listViewCommand.UseCompatibleStateImageBehavior = false;
            this.listViewCommand.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "#";
            this.columnHeader11.Width = 27;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Type ";
            this.columnHeader13.Width = 71;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Command";
            this.columnHeader12.Width = 123;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Argument";
            this.columnHeader15.Width = 85;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Description ";
            this.columnHeader14.Width = 145;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.labelDescription);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.labelDirection);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.labelType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelVersion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 226);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDescription.BackColor = System.Drawing.SystemColors.Info;
            this.labelDescription.Location = new System.Drawing.Point(103, 140);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(168, 73);
            this.labelDescription.TabIndex = 9;
            this.labelDescription.Text = "  ";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 19);
            this.label10.TabIndex = 8;
            this.label10.Text = "Description:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDirection
            // 
            this.labelDirection.BackColor = System.Drawing.SystemColors.Info;
            this.labelDirection.Location = new System.Drawing.Point(103, 112);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(168, 19);
            this.labelDirection.TabIndex = 7;
            this.labelDirection.Text = "  ";
            this.labelDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Direction:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelType
            // 
            this.labelType.BackColor = System.Drawing.SystemColors.Info;
            this.labelType.Location = new System.Drawing.Point(103, 83);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(168, 19);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "  ";
            this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 19);
            this.label6.TabIndex = 4;
            this.label6.Text = "Type:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.SystemColors.Info;
            this.labelVersion.Location = new System.Drawing.Point(103, 54);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(168, 19);
            this.labelVersion.TabIndex = 3;
            this.labelVersion.Text = "  ";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Version:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.SystemColors.Info;
            this.labelName.Location = new System.Drawing.Point(103, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(168, 19);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "  ";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTitle2
            // 
            this.labelTitle2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle2.BackColor = System.Drawing.Color.DarkGray;
            this.labelTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle2.Location = new System.Drawing.Point(0, 4);
            this.labelTitle2.Name = "labelTitle2";
            this.labelTitle2.Size = new System.Drawing.Size(848, 23);
            this.labelTitle2.TabIndex = 2;
            this.labelTitle2.Text = "Device Information";
            this.labelTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStripDevice
            // 
            this.contextMenuStripDevice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshListToolStripMenuItem,
            this.toolStripMenuItem1,
            this.updateInterfaceToolStripMenuItem,
            this.browseFolderToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripDevice.Name = "contextMenuStripDevice";
            this.contextMenuStripDevice.Size = new System.Drawing.Size(158, 120);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.refreshListToolStripMenuItem.Text = "Refresh List";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // browseFolderToolStripMenuItem
            // 
            this.browseFolderToolStripMenuItem.Name = "browseFolderToolStripMenuItem";
            this.browseFolderToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.browseFolderToolStripMenuItem.Text = "Browse Folder";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // updateInterfaceToolStripMenuItem
            // 
            this.updateInterfaceToolStripMenuItem.Name = "updateInterfaceToolStripMenuItem";
            this.updateInterfaceToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.updateInterfaceToolStripMenuItem.Text = "Update Interface";
            // 
            // DeviceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DeviceView";
            this.Size = new System.Drawing.Size(848, 425);
            this.panelMain.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageFile.ResumeLayout(false);
            this.tabPageCommand.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStripDevice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.ListView listViewDevice;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label labelTitle1;
        private System.Windows.Forms.Label labelTitle2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageFile;
        private System.Windows.Forms.TabPage tabPageCommand;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ListView listViewCommand;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDevice;
        private System.Windows.Forms.ToolStripMenuItem refreshListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem browseFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateInterfaceToolStripMenuItem;
    }
}
