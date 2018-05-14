namespace HYS.IM.Messaging.Composer
{
    partial class FormMInterface
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
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageHost = new System.Windows.Forms.TabPage();
            this.buttonHostDelete = new System.Windows.Forms.Button();
            this.listViewHost = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.buttonHostAdd = new System.Windows.Forms.Button();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.buttonConfigEdit = new System.Windows.Forms.Button();
            this.buttonConfigDelete = new System.Windows.Forms.Button();
            this.listViewConfig = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.buttonConfigAdd = new System.Windows.Forms.Button();
            this.tabPageMonitor = new System.Windows.Forms.TabPage();
            this.buttonMonitorEdit = new System.Windows.Forms.Button();
            this.buttonMonitorDelete = new System.Windows.Forms.Button();
            this.listViewMonitor = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.buttonMonitorAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageHost.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.tabPageMonitor.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(135, 44);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(321, 20);
            this.textBoxDescription.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 46;
            this.label1.Text = "Interface Description:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(135, 18);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(321, 20);
            this.textBoxName.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 20);
            this.label6.TabIndex = 44;
            this.label6.Text = "Interface Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageHost);
            this.tabControlMain.Controls.Add(this.tabPageConfig);
            this.tabControlMain.Controls.Add(this.tabPageMonitor);
            this.tabControlMain.Location = new System.Drawing.Point(20, 85);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(448, 214);
            this.tabControlMain.TabIndex = 48;
            // 
            // tabPageHost
            // 
            this.tabPageHost.Controls.Add(this.buttonHostDelete);
            this.tabPageHost.Controls.Add(this.listViewHost);
            this.tabPageHost.Controls.Add(this.buttonHostAdd);
            this.tabPageHost.Location = new System.Drawing.Point(4, 22);
            this.tabPageHost.Name = "tabPageHost";
            this.tabPageHost.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHost.Size = new System.Drawing.Size(440, 188);
            this.tabPageHost.TabIndex = 0;
            this.tabPageHost.Text = "Hosts";
            this.tabPageHost.UseVisualStyleBackColor = true;
            // 
            // buttonHostDelete
            // 
            this.buttonHostDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHostDelete.Location = new System.Drawing.Point(368, 37);
            this.buttonHostDelete.Name = "buttonHostDelete";
            this.buttonHostDelete.Size = new System.Drawing.Size(64, 25);
            this.buttonHostDelete.TabIndex = 52;
            this.buttonHostDelete.Text = "Delete";
            this.buttonHostDelete.UseVisualStyleBackColor = true;
            this.buttonHostDelete.Click += new System.EventHandler(this.buttonHostDelete_Click);
            // 
            // listViewHost
            // 
            this.listViewHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHost.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.listViewHost.FullRowSelect = true;
            this.listViewHost.HideSelection = false;
            this.listViewHost.Location = new System.Drawing.Point(6, 6);
            this.listViewHost.MultiSelect = false;
            this.listViewHost.Name = "listViewHost";
            this.listViewHost.Size = new System.Drawing.Size(356, 176);
            this.listViewHost.TabIndex = 40;
            this.listViewHost.UseCompatibleStateImageBehavior = false;
            this.listViewHost.View = System.Windows.Forms.View.Details;
            this.listViewHost.SelectedIndexChanged += new System.EventHandler(this.listViewHost_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Host Name ";
            this.columnHeader5.Width = 137;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description ";
            this.columnHeader6.Width = 184;
            // 
            // buttonHostAdd
            // 
            this.buttonHostAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHostAdd.Location = new System.Drawing.Point(368, 6);
            this.buttonHostAdd.Name = "buttonHostAdd";
            this.buttonHostAdd.Size = new System.Drawing.Size(64, 25);
            this.buttonHostAdd.TabIndex = 51;
            this.buttonHostAdd.Text = "Add";
            this.buttonHostAdd.UseVisualStyleBackColor = true;
            this.buttonHostAdd.Click += new System.EventHandler(this.buttonHostAdd_Click);
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.buttonConfigEdit);
            this.tabPageConfig.Controls.Add(this.buttonConfigDelete);
            this.tabPageConfig.Controls.Add(this.listViewConfig);
            this.tabPageConfig.Controls.Add(this.buttonConfigAdd);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(440, 188);
            this.tabPageConfig.TabIndex = 1;
            this.tabPageConfig.Text = "Configuration Pages";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // buttonConfigEdit
            // 
            this.buttonConfigEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfigEdit.Location = new System.Drawing.Point(368, 37);
            this.buttonConfigEdit.Name = "buttonConfigEdit";
            this.buttonConfigEdit.Size = new System.Drawing.Size(64, 25);
            this.buttonConfigEdit.TabIndex = 56;
            this.buttonConfigEdit.Text = "Edit";
            this.buttonConfigEdit.UseVisualStyleBackColor = true;
            this.buttonConfigEdit.Click += new System.EventHandler(this.buttonConfigEdit_Click);
            // 
            // buttonConfigDelete
            // 
            this.buttonConfigDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfigDelete.Location = new System.Drawing.Point(368, 68);
            this.buttonConfigDelete.Name = "buttonConfigDelete";
            this.buttonConfigDelete.Size = new System.Drawing.Size(64, 25);
            this.buttonConfigDelete.TabIndex = 55;
            this.buttonConfigDelete.Text = "Delete";
            this.buttonConfigDelete.UseVisualStyleBackColor = true;
            this.buttonConfigDelete.Click += new System.EventHandler(this.buttonConfigDelete_Click);
            // 
            // listViewConfig
            // 
            this.listViewConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewConfig.FullRowSelect = true;
            this.listViewConfig.HideSelection = false;
            this.listViewConfig.Location = new System.Drawing.Point(6, 6);
            this.listViewConfig.MultiSelect = false;
            this.listViewConfig.Name = "listViewConfig";
            this.listViewConfig.Size = new System.Drawing.Size(356, 176);
            this.listViewConfig.TabIndex = 53;
            this.listViewConfig.UseCompatibleStateImageBehavior = false;
            this.listViewConfig.View = System.Windows.Forms.View.Details;
            this.listViewConfig.SelectedIndexChanged += new System.EventHandler(this.listViewConfig_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Page Name";
            this.columnHeader1.Width = 137;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 184;
            // 
            // buttonConfigAdd
            // 
            this.buttonConfigAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfigAdd.Location = new System.Drawing.Point(368, 6);
            this.buttonConfigAdd.Name = "buttonConfigAdd";
            this.buttonConfigAdd.Size = new System.Drawing.Size(64, 25);
            this.buttonConfigAdd.TabIndex = 54;
            this.buttonConfigAdd.Text = "Add";
            this.buttonConfigAdd.UseVisualStyleBackColor = true;
            this.buttonConfigAdd.Click += new System.EventHandler(this.buttonConfigAdd_Click);
            // 
            // tabPageMonitor
            // 
            this.tabPageMonitor.Controls.Add(this.buttonMonitorEdit);
            this.tabPageMonitor.Controls.Add(this.buttonMonitorDelete);
            this.tabPageMonitor.Controls.Add(this.listViewMonitor);
            this.tabPageMonitor.Controls.Add(this.buttonMonitorAdd);
            this.tabPageMonitor.Location = new System.Drawing.Point(4, 22);
            this.tabPageMonitor.Name = "tabPageMonitor";
            this.tabPageMonitor.Size = new System.Drawing.Size(440, 188);
            this.tabPageMonitor.TabIndex = 2;
            this.tabPageMonitor.Text = "Monitor Pages";
            this.tabPageMonitor.UseVisualStyleBackColor = true;
            // 
            // buttonMonitorEdit
            // 
            this.buttonMonitorEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMonitorEdit.Location = new System.Drawing.Point(368, 37);
            this.buttonMonitorEdit.Name = "buttonMonitorEdit";
            this.buttonMonitorEdit.Size = new System.Drawing.Size(64, 25);
            this.buttonMonitorEdit.TabIndex = 60;
            this.buttonMonitorEdit.Text = "Edit";
            this.buttonMonitorEdit.UseVisualStyleBackColor = true;
            this.buttonMonitorEdit.Click += new System.EventHandler(this.buttonMonitorEdit_Click);
            // 
            // buttonMonitorDelete
            // 
            this.buttonMonitorDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMonitorDelete.Location = new System.Drawing.Point(368, 68);
            this.buttonMonitorDelete.Name = "buttonMonitorDelete";
            this.buttonMonitorDelete.Size = new System.Drawing.Size(64, 25);
            this.buttonMonitorDelete.TabIndex = 59;
            this.buttonMonitorDelete.Text = "Delete";
            this.buttonMonitorDelete.UseVisualStyleBackColor = true;
            this.buttonMonitorDelete.Click += new System.EventHandler(this.buttonMonitorDelete_Click);
            // 
            // listViewMonitor
            // 
            this.listViewMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMonitor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewMonitor.FullRowSelect = true;
            this.listViewMonitor.HideSelection = false;
            this.listViewMonitor.Location = new System.Drawing.Point(6, 6);
            this.listViewMonitor.MultiSelect = false;
            this.listViewMonitor.Name = "listViewMonitor";
            this.listViewMonitor.Size = new System.Drawing.Size(356, 176);
            this.listViewMonitor.TabIndex = 57;
            this.listViewMonitor.UseCompatibleStateImageBehavior = false;
            this.listViewMonitor.View = System.Windows.Forms.View.Details;
            this.listViewMonitor.SelectedIndexChanged += new System.EventHandler(this.listViewMonitor_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Page Name";
            this.columnHeader3.Width = 137;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 184;
            // 
            // buttonMonitorAdd
            // 
            this.buttonMonitorAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMonitorAdd.Location = new System.Drawing.Point(368, 6);
            this.buttonMonitorAdd.Name = "buttonMonitorAdd";
            this.buttonMonitorAdd.Size = new System.Drawing.Size(64, 25);
            this.buttonMonitorAdd.TabIndex = 58;
            this.buttonMonitorAdd.Text = "Add";
            this.buttonMonitorAdd.UseVisualStyleBackColor = true;
            this.buttonMonitorAdd.Click += new System.EventHandler(this.buttonMonitorAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(382, 316);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 25);
            this.buttonCancel.TabIndex = 50;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(290, 316);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 25);
            this.buttonOK.TabIndex = 49;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormMInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(480, 353);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label6);
            this.Name = "FormMInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormMInterfaceConfig";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageHost.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageMonitor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageHost;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TabPage tabPageMonitor;
        private System.Windows.Forms.ListView listViewHost;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button buttonHostDelete;
        private System.Windows.Forms.Button buttonHostAdd;
        private System.Windows.Forms.Button buttonConfigDelete;
        private System.Windows.Forms.ListView listViewConfig;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonConfigAdd;
        private System.Windows.Forms.Button buttonConfigEdit;
        private System.Windows.Forms.Button buttonMonitorEdit;
        private System.Windows.Forms.Button buttonMonitorDelete;
        private System.Windows.Forms.ListView listViewMonitor;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonMonitorAdd;
    }
}