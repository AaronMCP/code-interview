namespace HYS.IM.Config
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Inbound Adapters", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Outbound Adapters", System.Windows.Forms.HorizontalAlignment.Left);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonServiceLog = new System.Windows.Forms.Button();
            this.buttonServiceConfig = new System.Windows.Forms.Button();
            this.buttonServiceStop = new System.Windows.Forms.Button();
            this.buttonServiceStart = new System.Windows.Forms.Button();
            this.panelDiagram = new System.Windows.Forms.Panel();
            this.labelAdapter22 = new System.Windows.Forms.Label();
            this.labelAdapter21 = new System.Windows.Forms.Label();
            this.labelAdapter13 = new System.Windows.Forms.Label();
            this.labelAdapter12 = new System.Windows.Forms.Label();
            this.labelApdater11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelInterfaceName = new System.Windows.Forms.Label();
            this.tabPageAdapter = new System.Windows.Forms.TabPage();
            this.panelAdapter = new System.Windows.Forms.Panel();
            this.listViewAdpaters = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.buttonAdapterConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.timerLoading = new System.Windows.Forms.Timer(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlMain.SuspendLayout();
            this.tabPageService.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.panelDiagram.SuspendLayout();
            this.tabPageAdapter.SuspendLayout();
            this.panelAdapter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageService);
            this.tabControlMain.Controls.Add(this.tabPageAdapter);
            this.tabControlMain.Location = new System.Drawing.Point(13, 15);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(451, 296);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageService
            // 
            this.tabPageService.Controls.Add(this.panelLegend);
            this.tabPageService.Controls.Add(this.buttonServiceLog);
            this.tabPageService.Controls.Add(this.buttonServiceConfig);
            this.tabPageService.Controls.Add(this.buttonServiceStop);
            this.tabPageService.Controls.Add(this.buttonServiceStart);
            this.tabPageService.Controls.Add(this.panelDiagram);
            this.tabPageService.Controls.Add(this.groupBox1);
            this.tabPageService.Controls.Add(this.labelInterfaceName);
            this.tabPageService.Location = new System.Drawing.Point(4, 22);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageService.Size = new System.Drawing.Size(443, 270);
            this.tabPageService.TabIndex = 0;
            this.tabPageService.Text = "Service";
            this.tabPageService.UseVisualStyleBackColor = true;
            // 
            // panelLegend
            // 
            this.panelLegend.BackColor = System.Drawing.SystemColors.Control;
            this.panelLegend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLegend.BackgroundImage")));
            this.panelLegend.Controls.Add(this.label2);
            this.panelLegend.Controls.Add(this.label3);
            this.panelLegend.Location = new System.Drawing.Point(17, 188);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(298, 48);
            this.panelLegend.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(177, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Publish Channel";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(177, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "Request Channel";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonServiceLog
            // 
            this.buttonServiceLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServiceLog.Location = new System.Drawing.Point(334, 157);
            this.buttonServiceLog.Name = "buttonServiceLog";
            this.buttonServiceLog.Size = new System.Drawing.Size(91, 27);
            this.buttonServiceLog.TabIndex = 14;
            this.buttonServiceLog.Text = "View Log";
            this.buttonServiceLog.UseVisualStyleBackColor = true;
            this.buttonServiceLog.Click += new System.EventHandler(this.buttonServiceLog_Click);
            // 
            // buttonServiceConfig
            // 
            this.buttonServiceConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServiceConfig.Enabled = false;
            this.buttonServiceConfig.Location = new System.Drawing.Point(334, 124);
            this.buttonServiceConfig.Name = "buttonServiceConfig";
            this.buttonServiceConfig.Size = new System.Drawing.Size(91, 27);
            this.buttonServiceConfig.TabIndex = 13;
            this.buttonServiceConfig.Text = "Configure";
            this.buttonServiceConfig.UseVisualStyleBackColor = true;
            this.buttonServiceConfig.Click += new System.EventHandler(this.buttonServiceConfig_Click);
            // 
            // buttonServiceStop
            // 
            this.buttonServiceStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServiceStop.Enabled = false;
            this.buttonServiceStop.Location = new System.Drawing.Point(334, 91);
            this.buttonServiceStop.Name = "buttonServiceStop";
            this.buttonServiceStop.Size = new System.Drawing.Size(91, 27);
            this.buttonServiceStop.TabIndex = 12;
            this.buttonServiceStop.Text = "Stop";
            this.buttonServiceStop.UseVisualStyleBackColor = true;
            this.buttonServiceStop.Click += new System.EventHandler(this.buttonServiceStop_Click);
            // 
            // buttonServiceStart
            // 
            this.buttonServiceStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServiceStart.Enabled = false;
            this.buttonServiceStart.Location = new System.Drawing.Point(334, 58);
            this.buttonServiceStart.Name = "buttonServiceStart";
            this.buttonServiceStart.Size = new System.Drawing.Size(91, 27);
            this.buttonServiceStart.TabIndex = 2;
            this.buttonServiceStart.Text = "Start";
            this.buttonServiceStart.UseVisualStyleBackColor = true;
            this.buttonServiceStart.Click += new System.EventHandler(this.buttonServiceStart_Click);
            // 
            // panelDiagram
            // 
            this.panelDiagram.BackColor = System.Drawing.SystemColors.Control;
            this.panelDiagram.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelDiagram.BackgroundImage")));
            this.panelDiagram.Controls.Add(this.labelAdapter22);
            this.panelDiagram.Controls.Add(this.labelAdapter21);
            this.panelDiagram.Controls.Add(this.labelAdapter13);
            this.panelDiagram.Controls.Add(this.labelAdapter12);
            this.panelDiagram.Controls.Add(this.labelApdater11);
            this.panelDiagram.Location = new System.Drawing.Point(17, 58);
            this.panelDiagram.Name = "panelDiagram";
            this.panelDiagram.Size = new System.Drawing.Size(298, 130);
            this.panelDiagram.TabIndex = 11;
            // 
            // labelAdapter22
            // 
            this.labelAdapter22.BackColor = System.Drawing.Color.Transparent;
            this.labelAdapter22.Location = new System.Drawing.Point(177, 79);
            this.labelAdapter22.Name = "labelAdapter22";
            this.labelAdapter22.Size = new System.Drawing.Size(95, 21);
            this.labelAdapter22.TabIndex = 4;
            this.labelAdapter22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAdapter21
            // 
            this.labelAdapter21.BackColor = System.Drawing.Color.Transparent;
            this.labelAdapter21.Location = new System.Drawing.Point(177, 37);
            this.labelAdapter21.Name = "labelAdapter21";
            this.labelAdapter21.Size = new System.Drawing.Size(95, 21);
            this.labelAdapter21.TabIndex = 3;
            this.labelAdapter21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAdapter13
            // 
            this.labelAdapter13.BackColor = System.Drawing.Color.Transparent;
            this.labelAdapter13.Location = new System.Drawing.Point(32, 93);
            this.labelAdapter13.Name = "labelAdapter13";
            this.labelAdapter13.Size = new System.Drawing.Size(95, 21);
            this.labelAdapter13.TabIndex = 2;
            this.labelAdapter13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAdapter12
            // 
            this.labelAdapter12.BackColor = System.Drawing.Color.Transparent;
            this.labelAdapter12.Location = new System.Drawing.Point(32, 56);
            this.labelAdapter12.Name = "labelAdapter12";
            this.labelAdapter12.Size = new System.Drawing.Size(95, 21);
            this.labelAdapter12.TabIndex = 1;
            this.labelAdapter12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelApdater11
            // 
            this.labelApdater11.BackColor = System.Drawing.Color.Transparent;
            this.labelApdater11.Location = new System.Drawing.Point(32, 18);
            this.labelApdater11.Name = "labelApdater11";
            this.labelApdater11.Size = new System.Drawing.Size(95, 21);
            this.labelApdater11.TabIndex = 0;
            this.labelApdater11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(15, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 2);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // labelInterfaceName
            // 
            this.labelInterfaceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInterfaceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterfaceName.Location = new System.Drawing.Point(16, 15);
            this.labelInterfaceName.Name = "labelInterfaceName";
            this.labelInterfaceName.Size = new System.Drawing.Size(410, 20);
            this.labelInterfaceName.TabIndex = 9;
            this.labelInterfaceName.Text = "Loading configuration...";
            this.labelInterfaceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageAdapter
            // 
            this.tabPageAdapter.Controls.Add(this.panelAdapter);
            this.tabPageAdapter.Controls.Add(this.label1);
            this.tabPageAdapter.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdapter.Name = "tabPageAdapter";
            this.tabPageAdapter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdapter.Size = new System.Drawing.Size(443, 270);
            this.tabPageAdapter.TabIndex = 1;
            this.tabPageAdapter.Text = "Adapter";
            this.tabPageAdapter.UseVisualStyleBackColor = true;
            // 
            // panelAdapter
            // 
            this.panelAdapter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAdapter.Controls.Add(this.listViewAdpaters);
            this.panelAdapter.Controls.Add(this.buttonAdapterConfig);
            this.panelAdapter.Enabled = false;
            this.panelAdapter.Location = new System.Drawing.Point(6, 36);
            this.panelAdapter.Name = "panelAdapter";
            this.panelAdapter.Size = new System.Drawing.Size(436, 223);
            this.panelAdapter.TabIndex = 14;
            // 
            // listViewAdpaters
            // 
            this.listViewAdpaters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAdpaters.CheckBoxes = true;
            this.listViewAdpaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewAdpaters.FullRowSelect = true;
            listViewGroup1.Header = "Inbound Adapters";
            listViewGroup1.Name = "listViewGroupIn";
            listViewGroup2.Header = "Outbound Adapters";
            listViewGroup2.Name = "listViewGroupOut";
            this.listViewAdpaters.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listViewAdpaters.HideSelection = false;
            this.listViewAdpaters.Location = new System.Drawing.Point(13, 7);
            this.listViewAdpaters.MultiSelect = false;
            this.listViewAdpaters.Name = "listViewAdpaters";
            this.listViewAdpaters.Size = new System.Drawing.Size(306, 208);
            this.listViewAdpaters.TabIndex = 13;
            this.listViewAdpaters.UseCompatibleStateImageBehavior = false;
            this.listViewAdpaters.View = System.Windows.Forms.View.Details;
            this.listViewAdpaters.SelectedIndexChanged += new System.EventHandler(this.listViewAdpaters_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Enable";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name ";
            this.columnHeader2.Width = 83;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            this.columnHeader3.Width = 173;
            // 
            // buttonAdapterConfig
            // 
            this.buttonAdapterConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdapterConfig.Location = new System.Drawing.Point(329, 7);
            this.buttonAdapterConfig.Name = "buttonAdapterConfig";
            this.buttonAdapterConfig.Size = new System.Drawing.Size(91, 27);
            this.buttonAdapterConfig.TabIndex = 11;
            this.buttonAdapterConfig.Text = "Configure";
            this.buttonAdapterConfig.UseVisualStyleBackColor = true;
            this.buttonAdapterConfig.Click += new System.EventHandler(this.buttonAdapterConfig_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Please use the following list to enable/disable/configure adapters in this interf" +
                "ace:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(372, 320);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(91, 27);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // timerLoading
            // 
            this.timerLoading.Interval = 500;
            this.timerLoading.Tick += new System.EventHandler(this.timerLoading_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 363);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(487, 397);
            this.Name = "FormMain";
            this.Text = "HL7 Gateway Configuration";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageService.ResumeLayout(false);
            this.panelLegend.ResumeLayout(false);
            this.panelDiagram.ResumeLayout(false);
            this.tabPageAdapter.ResumeLayout(false);
            this.panelAdapter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageService;
        private System.Windows.Forms.TabPage tabPageAdapter;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelInterfaceName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelDiagram;
        private System.Windows.Forms.Button buttonServiceStart;
        private System.Windows.Forms.Button buttonServiceStop;
        private System.Windows.Forms.Button buttonServiceConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAdapterConfig;
        private System.Windows.Forms.ListView listViewAdpaters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label labelApdater11;
        private System.Windows.Forms.Label labelAdapter22;
        private System.Windows.Forms.Label labelAdapter21;
        private System.Windows.Forms.Label labelAdapter13;
        private System.Windows.Forms.Label labelAdapter12;
        private System.Windows.Forms.Panel panelAdapter;
        private System.Windows.Forms.Timer timerLoading;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.Button buttonServiceLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelLegend;
    }
}

