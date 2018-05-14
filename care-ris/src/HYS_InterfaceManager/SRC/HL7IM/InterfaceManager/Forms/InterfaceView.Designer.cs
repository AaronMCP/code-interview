namespace HYS.HL7IM.Manager.Forms
{
    partial class InterfaceView
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Inbound Interfaces", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Outbound Interfaces", System.Windows.Forms.HorizontalAlignment.Left);
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panelMain = new System.Windows.Forms.Panel();
            this.listViewInterface = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripInterface = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain.SuspendLayout();
            this.contextMenuStripInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Interface Path";
            this.columnHeader5.Width = 280;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type ";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Interface Name ";
            this.columnHeader1.Width = 180;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listViewInterface);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(790, 498);
            this.panelMain.TabIndex = 1;
            // 
            // listViewInterface
            // 
            this.listViewInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewInterface.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader2});
            this.listViewInterface.FullRowSelect = true;
            listViewGroup1.Header = "Inbound Interfaces";
            listViewGroup1.Name = "Inbound Interfaces";
            listViewGroup2.Header = "Outbound Interfaces";
            listViewGroup2.Name = "Outbound Interfaces";
            this.listViewInterface.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listViewInterface.HideSelection = false;
            this.listViewInterface.Location = new System.Drawing.Point(4, 4);
            this.listViewInterface.Margin = new System.Windows.Forms.Padding(4);
            this.listViewInterface.MultiSelect = false;
            this.listViewInterface.Name = "listViewInterface";
            this.listViewInterface.ShowGroups = false;
            this.listViewInterface.Size = new System.Drawing.Size(786, 496);
            this.listViewInterface.TabIndex = 0;
            this.listViewInterface.UseCompatibleStateImageBehavior = false;
            this.listViewInterface.View = System.Windows.Forms.View.Details;
            this.listViewInterface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewInterface_MouseClick);
            this.listViewInterface.SelectedIndexChanged += new System.EventHandler(this.listViewInterface_SelectedIndexChanged);
            this.listViewInterface.DoubleClick += new System.EventHandler(this.listViewInterface_DoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Status";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Install Date";
            this.columnHeader2.Width = 100;
            // 
            // contextMenuStripInterface
            // 
            this.contextMenuStripInterface.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseFolderToolStripMenuItem,
            this.managementToolStripMenuItem,
            this.uninstallToolStripMenuItem});
            this.contextMenuStripInterface.Name = "contextMenuStripInterface";
            this.contextMenuStripInterface.Size = new System.Drawing.Size(143, 70);
            // 
            // browseFolderToolStripMenuItem
            // 
            this.browseFolderToolStripMenuItem.Name = "browseFolderToolStripMenuItem";
            this.browseFolderToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.browseFolderToolStripMenuItem.Text = "Browse Folder";
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.managementToolStripMenuItem.Text = "Manage";
            // 
            // uninstallToolStripMenuItem
            // 
            this.uninstallToolStripMenuItem.Name = "uninstallToolStripMenuItem";
            this.uninstallToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.uninstallToolStripMenuItem.Text = "Uninstall";
            // 
            // InterfaceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InterfaceView";
            this.Size = new System.Drawing.Size(790, 498);
            this.panelMain.ResumeLayout(false);
            this.contextMenuStripInterface.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ListView listViewInterface;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInterface;
        private System.Windows.Forms.ToolStripMenuItem browseFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uninstallToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
    }
}
