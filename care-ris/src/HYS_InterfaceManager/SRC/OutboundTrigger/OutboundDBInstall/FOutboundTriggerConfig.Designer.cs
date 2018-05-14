namespace OutboundDBInstall
{
    partial class FOutboundTriggerConfig
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
            this.pMain = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pSQLButtons = new System.Windows.Forms.Panel();
            this.btCancelModify = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.labHijack = new System.Windows.Forms.Label();
            this.pConfigurationButtons = new System.Windows.Forms.Panel();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btModify = new System.Windows.Forms.Button();
            this.rbSQLView = new System.Windows.Forms.RadioButton();
            this.rbConfigurationView = new System.Windows.Forms.RadioButton();
            this.pSetting = new System.Windows.Forms.Panel();
            this.tcScript = new System.Windows.Forms.TabControl();
            this.tpInstallTriggerScript = new System.Windows.Forms.TabPage();
            this.tbInstallTrigger = new System.Windows.Forms.RichTextBox();
            this.tpUninstallTriggerScript = new System.Windows.Forms.TabPage();
            this.tbUninstallTrigger = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.InboundInterface = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcceptEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TriggerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbOutboundDesc = new System.Windows.Forms.TextBox();
            this.tbOutboundName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.pMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pSQLButtons.SuspendLayout();
            this.pConfigurationButtons.SuspendLayout();
            this.pSetting.SuspendLayout();
            this.tcScript.SuspendLayout();
            this.tpInstallTriggerScript.SuspendLayout();
            this.tpUninstallTriggerScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.groupBox2);
            this.pMain.Controls.Add(this.groupBox1);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(646, 390);
            this.pMain.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pSQLButtons);
            this.groupBox2.Controls.Add(this.labHijack);
            this.groupBox2.Controls.Add(this.pConfigurationButtons);
            this.groupBox2.Controls.Add(this.rbSQLView);
            this.groupBox2.Controls.Add(this.rbConfigurationView);
            this.groupBox2.Controls.Add(this.pSetting);
            this.groupBox2.Location = new System.Drawing.Point(24, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 300);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Outbound Trigger Setting";
            // 
            // pSQLButtons
            // 
            this.pSQLButtons.Controls.Add(this.btCancelModify);
            this.pSQLButtons.Controls.Add(this.btApply);
            this.pSQLButtons.Location = new System.Drawing.Point(356, 264);
            this.pSQLButtons.Name = "pSQLButtons";
            this.pSQLButtons.Size = new System.Drawing.Size(221, 36);
            this.pSQLButtons.TabIndex = 8;
            // 
            // btCancelModify
            // 
            this.btCancelModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancelModify.Location = new System.Drawing.Point(99, 9);
            this.btCancelModify.Name = "btCancelModify";
            this.btCancelModify.Size = new System.Drawing.Size(95, 21);
            this.btCancelModify.TabIndex = 3;
            this.btCancelModify.Text = "Cancel";
            this.btCancelModify.UseVisualStyleBackColor = true;
            this.btCancelModify.Click += new System.EventHandler(this.btCancelModify_Click);
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(8, 9);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(85, 21);
            this.btApply.TabIndex = 3;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // labHijack
            // 
            this.labHijack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labHijack.ForeColor = System.Drawing.Color.Red;
            this.labHijack.Location = new System.Drawing.Point(370, 27);
            this.labHijack.Name = "labHijack";
            this.labHijack.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labHijack.Size = new System.Drawing.Size(204, 12);
            this.labHijack.TabIndex = 6;
            this.labHijack.Text = "Script Was Modified!";
            this.labHijack.Visible = false;
            // 
            // pConfigurationButtons
            // 
            this.pConfigurationButtons.Controls.Add(this.btRemove);
            this.pConfigurationButtons.Controls.Add(this.btAdd);
            this.pConfigurationButtons.Controls.Add(this.btModify);
            this.pConfigurationButtons.Location = new System.Drawing.Point(62, 264);
            this.pConfigurationButtons.Name = "pConfigurationButtons";
            this.pConfigurationButtons.Size = new System.Drawing.Size(262, 33);
            this.pConfigurationButtons.TabIndex = 7;
            // 
            // btRemove
            // 
            this.btRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRemove.Enabled = false;
            this.btRemove.Location = new System.Drawing.Point(160, 7);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 21);
            this.btRemove.TabIndex = 3;
            this.btRemove.Text = "Delete";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Location = new System.Drawing.Point(-2, 7);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 21);
            this.btAdd.TabIndex = 1;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btModify
            // 
            this.btModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btModify.Enabled = false;
            this.btModify.Location = new System.Drawing.Point(79, 7);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(75, 21);
            this.btModify.TabIndex = 2;
            this.btModify.Text = "Edit";
            this.btModify.UseVisualStyleBackColor = true;
            this.btModify.Click += new System.EventHandler(this.btModify_Click);
            // 
            // rbSQLView
            // 
            this.rbSQLView.AutoSize = true;
            this.rbSQLView.Location = new System.Drawing.Point(200, 27);
            this.rbSQLView.Name = "rbSQLView";
            this.rbSQLView.Size = new System.Drawing.Size(71, 16);
            this.rbSQLView.TabIndex = 1;
            this.rbSQLView.Text = "SQL View";
            this.rbSQLView.UseVisualStyleBackColor = true;
            // 
            // rbConfigurationView
            // 
            this.rbConfigurationView.AutoSize = true;
            this.rbConfigurationView.Checked = true;
            this.rbConfigurationView.Location = new System.Drawing.Point(72, 27);
            this.rbConfigurationView.Name = "rbConfigurationView";
            this.rbConfigurationView.Size = new System.Drawing.Size(131, 16);
            this.rbConfigurationView.TabIndex = 1;
            this.rbConfigurationView.TabStop = true;
            this.rbConfigurationView.Text = "Configuration View";
            this.rbConfigurationView.UseVisualStyleBackColor = true;
            this.rbConfigurationView.CheckedChanged += new System.EventHandler(this.rbConfigurationView_CheckedChanged);
            // 
            // pSetting
            // 
            this.pSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pSetting.Controls.Add(this.tcScript);
            this.pSetting.Controls.Add(this.dataGridView1);
            this.pSetting.Location = new System.Drawing.Point(63, 56);
            this.pSetting.Name = "pSetting";
            this.pSetting.Size = new System.Drawing.Size(521, 208);
            this.pSetting.TabIndex = 2;
            // 
            // tcScript
            // 
            this.tcScript.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcScript.Controls.Add(this.tpInstallTriggerScript);
            this.tcScript.Controls.Add(this.tpUninstallTriggerScript);
            this.tcScript.Location = new System.Drawing.Point(300, 3);
            this.tcScript.Name = "tcScript";
            this.tcScript.SelectedIndex = 0;
            this.tcScript.Size = new System.Drawing.Size(218, 171);
            this.tcScript.TabIndex = 0;
            // 
            // tpInstallTriggerScript
            // 
            this.tpInstallTriggerScript.Controls.Add(this.tbInstallTrigger);
            this.tpInstallTriggerScript.Location = new System.Drawing.Point(4, 25);
            this.tpInstallTriggerScript.Name = "tpInstallTriggerScript";
            this.tpInstallTriggerScript.Padding = new System.Windows.Forms.Padding(3);
            this.tpInstallTriggerScript.Size = new System.Drawing.Size(210, 142);
            this.tpInstallTriggerScript.TabIndex = 0;
            this.tpInstallTriggerScript.Text = "Install Trigger Script";
            this.tpInstallTriggerScript.UseVisualStyleBackColor = true;
            // 
            // tbInstallTrigger
            // 
            this.tbInstallTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInstallTrigger.Location = new System.Drawing.Point(3, 3);
            this.tbInstallTrigger.Name = "tbInstallTrigger";
            this.tbInstallTrigger.Size = new System.Drawing.Size(204, 136);
            this.tbInstallTrigger.TabIndex = 0;
            this.tbInstallTrigger.Text = "";
            this.tbInstallTrigger.TextChanged += new System.EventHandler(this.tbInstallTrigger_TextChanged);
            // 
            // tpUninstallTriggerScript
            // 
            this.tpUninstallTriggerScript.Controls.Add(this.tbUninstallTrigger);
            this.tpUninstallTriggerScript.Location = new System.Drawing.Point(4, 25);
            this.tpUninstallTriggerScript.Name = "tpUninstallTriggerScript";
            this.tpUninstallTriggerScript.Padding = new System.Windows.Forms.Padding(3);
            this.tpUninstallTriggerScript.Size = new System.Drawing.Size(210, 142);
            this.tpUninstallTriggerScript.TabIndex = 1;
            this.tpUninstallTriggerScript.Text = "UninstallTriggerScript";
            this.tpUninstallTriggerScript.UseVisualStyleBackColor = true;
            // 
            // tbUninstallTrigger
            // 
            this.tbUninstallTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUninstallTrigger.Location = new System.Drawing.Point(3, 3);
            this.tbUninstallTrigger.Name = "tbUninstallTrigger";
            this.tbUninstallTrigger.Size = new System.Drawing.Size(204, 136);
            this.tbUninstallTrigger.TabIndex = 1;
            this.tbUninstallTrigger.Text = "";
            this.tbUninstallTrigger.TextChanged += new System.EventHandler(this.tbUninstallTrigger_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InboundInterface,
            this.AcceptEventType,
            this.TriggerName});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(258, 144);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // InboundInterface
            // 
            this.InboundInterface.HeaderText = "Inbound Interface";
            this.InboundInterface.Name = "InboundInterface";
            // 
            // AcceptEventType
            // 
            this.AcceptEventType.HeaderText = "Accecpt EventType";
            this.AcceptEventType.Name = "AcceptEventType";
            // 
            // TriggerName
            // 
            this.TriggerName.HeaderText = "Trigger Name";
            this.TriggerName.Name = "TriggerName";
            this.TriggerName.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbOutboundDesc);
            this.groupBox1.Controls.Add(this.tbOutboundName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(533, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(101, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Info";
            this.groupBox1.Visible = false;
            // 
            // tbOutboundDesc
            // 
            this.tbOutboundDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutboundDesc.Enabled = false;
            this.tbOutboundDesc.Location = new System.Drawing.Point(72, 42);
            this.tbOutboundDesc.Name = "tbOutboundDesc";
            this.tbOutboundDesc.Size = new System.Drawing.Size(3, 21);
            this.tbOutboundDesc.TabIndex = 8;
            // 
            // tbOutboundName
            // 
            this.tbOutboundName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutboundName.Enabled = false;
            this.tbOutboundName.Location = new System.Drawing.Point(72, 17);
            this.tbOutboundName.Name = "tbOutboundName";
            this.tbOutboundName.Size = new System.Drawing.Size(3, 21);
            this.tbOutboundName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Desc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btCancel);
            this.panel2.Controls.Add(this.btOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 346);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(646, 44);
            this.panel2.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(533, 12);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 21);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(436, 12);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 21);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // FOutboundTriggerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 390);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pMain);
            this.Name = "FOutboundTriggerConfig";
            this.Text = "FOutboundTriggerConfig";
            this.Load += new System.EventHandler(this.FOutboundTriggerConfig_Load);
            this.ClientSizeChanged += new System.EventHandler(this.FOutboundTriggerConfig_ClientSizeChanged);
            this.pMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pSQLButtons.ResumeLayout(false);
            this.pConfigurationButtons.ResumeLayout(false);
            this.pSetting.ResumeLayout(false);
            this.tcScript.ResumeLayout(false);
            this.tpInstallTriggerScript.ResumeLayout(false);
            this.tpUninstallTriggerScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbOutboundDesc;
        private System.Windows.Forms.TextBox tbOutboundName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbSQLView;
        private System.Windows.Forms.RadioButton rbConfigurationView;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel pSetting;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TabControl tcScript;
        private System.Windows.Forms.TabPage tpInstallTriggerScript;
        private System.Windows.Forms.TabPage tpUninstallTriggerScript;
        private System.Windows.Forms.Label labHijack;
        private System.Windows.Forms.Panel pSQLButtons;
        private System.Windows.Forms.Button btCancelModify;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Panel pConfigurationButtons;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btModify;
        private System.Windows.Forms.RichTextBox tbInstallTrigger;
        private System.Windows.Forms.RichTextBox tbUninstallTrigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn InboundInterface;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcceptEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TriggerName;
    }
}