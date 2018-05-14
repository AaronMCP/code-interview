namespace HYS.IM.Forms
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interfaceViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupByDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regExpMgtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookUpTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.licenseViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDeviceView = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInterfaceView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBrowseFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGroupByType = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGroupByDirection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHighGroup = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelView = new System.Windows.Forms.Panel();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.panelTool = new System.Windows.Forms.Panel();
            this.timerStartup = new System.Windows.Forms.Timer(this.components);
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuMain.Size = new System.Drawing.Size(816, 35);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(93, 31);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(259, 32);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(256, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(259, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceViewToolStripMenuItem,
            this.interfaceViewToolStripMenuItem,
            this.toolStripMenuItem1,
            this.groupByDirectionToolStripMenuItem,
            this.groupByTypeToolStripMenuItem,
            this.toolStripSeparator4,
            this.refreshToolStripMenuItem,
            this.browseFolderToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(70, 31);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // deviceViewToolStripMenuItem
            // 
            this.deviceViewToolStripMenuItem.Name = "deviceViewToolStripMenuItem";
            this.deviceViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.deviceViewToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.deviceViewToolStripMenuItem.Text = "Device View";
            this.deviceViewToolStripMenuItem.Click += new System.EventHandler(this.deviceViewToolStripMenuItem_Click);
            // 
            // interfaceViewToolStripMenuItem
            // 
            this.interfaceViewToolStripMenuItem.Name = "interfaceViewToolStripMenuItem";
            this.interfaceViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.interfaceViewToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.interfaceViewToolStripMenuItem.Text = "Interface View";
            this.interfaceViewToolStripMenuItem.Click += new System.EventHandler(this.interfaceViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(270, 6);
            // 
            // groupByDirectionToolStripMenuItem
            // 
            this.groupByDirectionToolStripMenuItem.Name = "groupByDirectionToolStripMenuItem";
            this.groupByDirectionToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.groupByDirectionToolStripMenuItem.Text = "Group By Direction";
            this.groupByDirectionToolStripMenuItem.Click += new System.EventHandler(this.groupByDirectionToolStripMenuItem_Click);
            // 
            // groupByTypeToolStripMenuItem
            // 
            this.groupByTypeToolStripMenuItem.Name = "groupByTypeToolStripMenuItem";
            this.groupByTypeToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.groupByTypeToolStripMenuItem.Text = "Group By Type";
            this.groupByTypeToolStripMenuItem.Click += new System.EventHandler(this.groupByTypeToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(270, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // browseFolderToolStripMenuItem
            // 
            this.browseFolderToolStripMenuItem.Name = "browseFolderToolStripMenuItem";
            this.browseFolderToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.browseFolderToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.browseFolderToolStripMenuItem.Text = "Browse Folder";
            this.browseFolderToolStripMenuItem.Click += new System.EventHandler(this.browseFolderToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regExpMgtToolStripMenuItem,
            this.lookUpTableToolStripMenuItem,
            this.systemConfigurationToolStripMenuItem,
            this.toolStripSeparator2,
            this.licenseViewerToolStripMenuItem,
            this.logViewerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(75, 31);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // regExpMgtToolStripMenuItem
            // 
            this.regExpMgtToolStripMenuItem.Name = "regExpMgtToolStripMenuItem";
            this.regExpMgtToolStripMenuItem.Size = new System.Drawing.Size(403, 32);
            this.regExpMgtToolStripMenuItem.Text = "Regular Expression Management";
            this.regExpMgtToolStripMenuItem.Click += new System.EventHandler(this.regExpMgtToolStripMenuItem_Click);
            // 
            // lookUpTableToolStripMenuItem
            // 
            this.lookUpTableToolStripMenuItem.Name = "lookUpTableToolStripMenuItem";
            this.lookUpTableToolStripMenuItem.Size = new System.Drawing.Size(403, 32);
            this.lookUpTableToolStripMenuItem.Text = "Look Up Table Management";
            this.lookUpTableToolStripMenuItem.Click += new System.EventHandler(this.lookUpTableToolStripMenuItem_Click);
            // 
            // systemConfigurationToolStripMenuItem
            // 
            this.systemConfigurationToolStripMenuItem.Name = "systemConfigurationToolStripMenuItem";
            this.systemConfigurationToolStripMenuItem.Size = new System.Drawing.Size(403, 32);
            this.systemConfigurationToolStripMenuItem.Text = "System Configuration";
            this.systemConfigurationToolStripMenuItem.Click += new System.EventHandler(this.systemConfigurationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(400, 6);
            // 
            // licenseViewerToolStripMenuItem
            // 
            this.licenseViewerToolStripMenuItem.Name = "licenseViewerToolStripMenuItem";
            this.licenseViewerToolStripMenuItem.Size = new System.Drawing.Size(403, 32);
            this.licenseViewerToolStripMenuItem.Text = "License Viewer";
            this.licenseViewerToolStripMenuItem.Click += new System.EventHandler(this.licenseViewerToolStripMenuItem_Click);
            // 
            // logViewerToolStripMenuItem
            // 
            this.logViewerToolStripMenuItem.Name = "logViewerToolStripMenuItem";
            this.logViewerToolStripMenuItem.Size = new System.Drawing.Size(403, 32);
            this.logViewerToolStripMenuItem.Text = "Log Viewer";
            this.logViewerToolStripMenuItem.Click += new System.EventHandler(this.logViewerToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpDocumentToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 31);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpDocumentToolStripMenuItem
            // 
            this.helpDocumentToolStripMenuItem.Name = "helpDocumentToolStripMenuItem";
            this.helpDocumentToolStripMenuItem.Size = new System.Drawing.Size(239, 32);
            this.helpDocumentToolStripMenuItem.Text = "Help Document";
            this.helpDocumentToolStripMenuItem.Visible = false;
            this.helpDocumentToolStripMenuItem.Click += new System.EventHandler(this.helpDocumentToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(236, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(239, 32);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusMain
            // 
            this.statusMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusMain.Location = new System.Drawing.Point(0, 523);
            this.statusMain.Name = "statusMain";
            this.statusMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusMain.Size = new System.Drawing.Size(816, 32);
            this.statusMain.TabIndex = 1;
            this.statusMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(398, 27);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(398, 27);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "device list";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDeviceView,
            this.toolStripButtonInterfaceView,
            this.toolStripSeparator1,
            this.toolStripButtonRefresh,
            this.toolStripButtonBrowseFolder,
            this.toolStripSeparator3,
            this.toolStripButtonGroupByType,
            this.toolStripButtonGroupByDirection,
            this.toolStripButtonHighGroup});
            this.toolStripMain.Location = new System.Drawing.Point(0, 35);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripMain.Size = new System.Drawing.Size(816, 34);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStripMain";
            // 
            // toolStripButtonDeviceView
            // 
            this.toolStripButtonDeviceView.Image = global::HYS.IM.Properties.Resources.device;
            this.toolStripButtonDeviceView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeviceView.Name = "toolStripButtonDeviceView";
            this.toolStripButtonDeviceView.Size = new System.Drawing.Size(151, 31);
            this.toolStripButtonDeviceView.Text = "Device View";
            this.toolStripButtonDeviceView.Click += new System.EventHandler(this.toolStripButtonDeviceView_Click);
            // 
            // toolStripButtonInterfaceView
            // 
            this.toolStripButtonInterfaceView.Image = global::HYS.IM.Properties.Resources._interface;
            this.toolStripButtonInterfaceView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInterfaceView.Name = "toolStripButtonInterfaceView";
            this.toolStripButtonInterfaceView.Size = new System.Drawing.Size(171, 31);
            this.toolStripButtonInterfaceView.Text = "Interface View";
            this.toolStripButtonInterfaceView.Click += new System.EventHandler(this.toolStripButtonInterfaceView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = global::HYS.IM.Properties.Resources.refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(107, 31);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefreshList_Click);
            // 
            // toolStripButtonBrowseFolder
            // 
            this.toolStripButtonBrowseFolder.Image = global::HYS.IM.Properties.Resources.folder;
            this.toolStripButtonBrowseFolder.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.toolStripButtonBrowseFolder.Name = "toolStripButtonBrowseFolder";
            this.toolStripButtonBrowseFolder.Size = new System.Drawing.Size(173, 31);
            this.toolStripButtonBrowseFolder.Text = "Browse Folder";
            this.toolStripButtonBrowseFolder.Click += new System.EventHandler(this.toolStripButtonBrowseFolder_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripButtonGroupByType
            // 
            this.toolStripButtonGroupByType.Image = global::HYS.IM.Properties.Resources.GroupType;
            this.toolStripButtonGroupByType.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.toolStripButtonGroupByType.Name = "toolStripButtonGroupByType";
            this.toolStripButtonGroupByType.Size = new System.Drawing.Size(179, 31);
            this.toolStripButtonGroupByType.Text = "Group By Type";
            this.toolStripButtonGroupByType.Click += new System.EventHandler(this.toolStripButtonGroupByType_Click);
            // 
            // toolStripButtonGroupByDirection
            // 
            this.toolStripButtonGroupByDirection.Image = global::HYS.IM.Properties.Resources.GroupDirection;
            this.toolStripButtonGroupByDirection.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.toolStripButtonGroupByDirection.Name = "toolStripButtonGroupByDirection";
            this.toolStripButtonGroupByDirection.Size = new System.Drawing.Size(219, 31);
            this.toolStripButtonGroupByDirection.Text = "Group By Direction";
            this.toolStripButtonGroupByDirection.Click += new System.EventHandler(this.toolStripButtonGroupByDirection_Click);
            // 
            // toolStripButtonHighGroup
            // 
            this.toolStripButtonHighGroup.Image = global::HYS.IM.Properties.Resources.GroupHide;
            this.toolStripButtonHighGroup.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.toolStripButtonHighGroup.Name = "toolStripButtonHighGroup";
            this.toolStripButtonHighGroup.Size = new System.Drawing.Size(147, 31);
            this.toolStripButtonHighGroup.Text = "Hide Group";
            this.toolStripButtonHighGroup.Click += new System.EventHandler(this.toolStripButtonHideGroup_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.Desktop;
            this.panelMain.Controls.Add(this.panelView);
            this.panelMain.Controls.Add(this.splitterMain);
            this.panelMain.Controls.Add(this.panelTool);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 69);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(816, 454);
            this.panelMain.TabIndex = 3;
            // 
            // panelView
            // 
            this.panelView.BackColor = System.Drawing.SystemColors.Control;
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(192, 0);
            this.panelView.Margin = new System.Windows.Forms.Padding(4);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(624, 454);
            this.panelView.TabIndex = 2;
            // 
            // splitterMain
            // 
            this.splitterMain.BackColor = System.Drawing.SystemColors.Control;
            this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterMain.Location = new System.Drawing.Point(189, 0);
            this.splitterMain.Margin = new System.Windows.Forms.Padding(4);
            this.splitterMain.MinSize = 170;
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(3, 454);
            this.splitterMain.TabIndex = 1;
            this.splitterMain.TabStop = false;
            // 
            // panelTool
            // 
            this.panelTool.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTool.Location = new System.Drawing.Point(0, 0);
            this.panelTool.Margin = new System.Windows.Forms.Padding(4);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(189, 454);
            this.panelTool.TabIndex = 0;
            // 
            // timerStartup
            // 
            this.timerStartup.Tick += new System.EventHandler(this.timerStartup_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 555);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Interface Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.Panel panelTool;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeviceView;
        private System.Windows.Forms.ToolStripButton toolStripButtonInterfaceView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonHighGroup;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButtonGroupByDirection;
        private System.Windows.Forms.ToolStripButton toolStripButtonGroupByType;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interfaceViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem groupByDirectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton toolStripButtonBrowseFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem browseFolderToolStripMenuItem;
        private System.Windows.Forms.Timer timerStartup;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem lookUpTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem licenseViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regExpMgtToolStripMenuItem;
    }
}