namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    partial class Channel
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
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxStatus = new System.Windows.Forms.CheckBox();
            this.lblExist = new System.Windows.Forms.Label();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.lblChannelName = new System.Windows.Forms.Label();
            this.panelCriteria = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCriteria = new System.Windows.Forms.Label();
            this.btnCriteriaModify = new System.Windows.Forms.Button();
            this.btnCriteriaAdd = new System.Windows.Forms.Button();
            this.btnCriteriaDelete = new System.Windows.Forms.Button();
            this.lstvCriteria = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.lblModeName = new System.Windows.Forms.Label();
            this.lblModeType = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelMapping = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lstvResult = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRD = new System.Windows.Forms.ColumnHeader();
            this.btnResultDelete = new System.Windows.Forms.Button();
            this.btnResultModify = new System.Windows.Forms.Button();
            this.btnResultAdd = new System.Windows.Forms.Button();
            this.panelQueryMode = new System.Windows.Forms.Panel();
            this.txtModeName = new System.Windows.Forms.TextBox();
            this.enumCmbbxOperationType = new EnumComboBox.EnumComboBox();
            this.panelGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelCriteria.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMapping.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel.SuspendLayout();
            this.panelQueryMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGeneral
            // 
            this.panelGeneral.Controls.Add(this.groupBox1);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGeneral.Location = new System.Drawing.Point(0, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(812, 73);
            this.panelGeneral.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxStatus);
            this.groupBox1.Controls.Add(this.lblExist);
            this.groupBox1.Controls.Add(this.txtChannelName);
            this.groupBox1.Controls.Add(this.lblChannelName);
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel Information";
            // 
            // checkBoxStatus
            // 
            this.checkBoxStatus.AutoSize = true;
            this.checkBoxStatus.Checked = true;
            this.checkBoxStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStatus.Location = new System.Drawing.Point(487, 26);
            this.checkBoxStatus.Name = "checkBoxStatus";
            this.checkBoxStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxStatus.Size = new System.Drawing.Size(59, 17);
            this.checkBoxStatus.TabIndex = 59;
            this.checkBoxStatus.Text = "Enable";
            this.checkBoxStatus.UseVisualStyleBackColor = true;
            this.checkBoxStatus.CheckedChanged += new System.EventHandler(this.checkBoxStatus_CheckedChanged);
            // 
            // lblExist
            // 
            this.lblExist.AutoSize = true;
            this.lblExist.ForeColor = System.Drawing.Color.Red;
            this.lblExist.Location = new System.Drawing.Point(154, 47);
            this.lblExist.Name = "lblExist";
            this.lblExist.Size = new System.Drawing.Size(75, 13);
            this.lblExist.TabIndex = 57;
            this.lblExist.Text = "Name Existed!";
            this.lblExist.Visible = false;
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(157, 24);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(295, 20);
            this.txtChannelName.TabIndex = 0;
            this.txtChannelName.TextChanged += new System.EventHandler(this.txtChannelName_TextChanged);
            // 
            // lblChannelName
            // 
            this.lblChannelName.AutoSize = true;
            this.lblChannelName.Location = new System.Drawing.Point(24, 27);
            this.lblChannelName.Name = "lblChannelName";
            this.lblChannelName.Size = new System.Drawing.Size(77, 13);
            this.lblChannelName.TabIndex = 54;
            this.lblChannelName.Text = "Channel Name";
            // 
            // panelCriteria
            // 
            this.panelCriteria.Controls.Add(this.groupBox2);
            this.panelCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCriteria.Location = new System.Drawing.Point(0, 73);
            this.panelCriteria.Name = "panelCriteria";
            this.panelCriteria.Size = new System.Drawing.Size(812, 161);
            this.panelCriteria.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblCriteria);
            this.groupBox2.Controls.Add(this.btnCriteriaModify);
            this.groupBox2.Controls.Add(this.btnCriteriaAdd);
            this.groupBox2.Controls.Add(this.btnCriteriaDelete);
            this.groupBox2.Controls.Add(this.lstvCriteria);
            this.groupBox2.Location = new System.Drawing.Point(15, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(785, 148);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GC Gateway Query Filter";
            // 
            // lblCriteria
            // 
            this.lblCriteria.AutoSize = true;
            this.lblCriteria.Location = new System.Drawing.Point(24, 22);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(60, 13);
            this.lblCriteria.TabIndex = 55;
            this.lblCriteria.Text = "Query Filter";
            // 
            // btnCriteriaModify
            // 
            this.btnCriteriaModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaModify.Enabled = false;
            this.btnCriteriaModify.Location = new System.Drawing.Point(624, 50);
            this.btnCriteriaModify.Name = "btnCriteriaModify";
            this.btnCriteriaModify.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaModify.TabIndex = 2;
            this.btnCriteriaModify.Text = "Edit";
            this.btnCriteriaModify.UseVisualStyleBackColor = true;
            this.btnCriteriaModify.Click += new System.EventHandler(this.btnCriteriaModify_Click);
            // 
            // btnCriteriaAdd
            // 
            this.btnCriteriaAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaAdd.Location = new System.Drawing.Point(624, 22);
            this.btnCriteriaAdd.Name = "btnCriteriaAdd";
            this.btnCriteriaAdd.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaAdd.TabIndex = 1;
            this.btnCriteriaAdd.Text = "Add";
            this.btnCriteriaAdd.UseVisualStyleBackColor = true;
            this.btnCriteriaAdd.Click += new System.EventHandler(this.btnCriteriaAdd_Click);
            // 
            // btnCriteriaDelete
            // 
            this.btnCriteriaDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaDelete.Enabled = false;
            this.btnCriteriaDelete.Location = new System.Drawing.Point(624, 78);
            this.btnCriteriaDelete.Name = "btnCriteriaDelete";
            this.btnCriteriaDelete.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaDelete.TabIndex = 3;
            this.btnCriteriaDelete.Text = "Delete";
            this.btnCriteriaDelete.UseVisualStyleBackColor = true;
            this.btnCriteriaDelete.Click += new System.EventHandler(this.btnCriteriaDelete_Click);
            // 
            // lstvCriteria
            // 
            this.lstvCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvCriteria.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lstvCriteria.FullRowSelect = true;
            this.lstvCriteria.HideSelection = false;
            this.lstvCriteria.Location = new System.Drawing.Point(157, 22);
            this.lstvCriteria.Name = "lstvCriteria";
            this.lstvCriteria.Size = new System.Drawing.Size(461, 115);
            this.lstvCriteria.TabIndex = 0;
            this.lstvCriteria.UseCompatibleStateImageBehavior = false;
            this.lstvCriteria.View = System.Windows.Forms.View.Details;
            this.lstvCriteria.DoubleClick += new System.EventHandler(this.lstvCriteria_DoubleClick);
            this.lstvCriteria.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvCriteria_ColumnClick);
            this.lstvCriteria.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvCriteria_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 23;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "GC Gateway Field";
            this.columnHeader2.Width = 152;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Operator";
            this.columnHeader4.Width = 91;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Value";
            this.columnHeader5.Width = 104;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Join Type";
            this.columnHeader6.Width = 86;
            // 
            // lblModeName
            // 
            this.lblModeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblModeName.Location = new System.Drawing.Point(21, 42);
            this.lblModeName.Name = "lblModeName";
            this.lblModeName.Size = new System.Drawing.Size(127, 13);
            this.lblModeName.TabIndex = 56;
            this.lblModeName.Text = "Storage Procedure Name";
            // 
            // lblModeType
            // 
            this.lblModeType.AutoSize = true;
            this.lblModeType.Location = new System.Drawing.Point(21, 10);
            this.lblModeType.Name = "lblModeType";
            this.lblModeType.Size = new System.Drawing.Size(72, 13);
            this.lblModeType.TabIndex = 55;
            this.lblModeType.Text = "Access Mode";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 613);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(812, 40);
            this.panelBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(717, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(631, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelMapping
            // 
            this.panelMapping.Controls.Add(this.groupBox3);
            this.panelMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMapping.Location = new System.Drawing.Point(0, 234);
            this.panelMapping.Name = "panelMapping";
            this.panelMapping.Size = new System.Drawing.Size(812, 379);
            this.panelMapping.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.panel);
            this.groupBox3.Controls.Add(this.panelQueryMode);
            this.groupBox3.Location = new System.Drawing.Point(15, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(785, 368);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Third Party Data Operation";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lblResult);
            this.panel.Controls.Add(this.lstvResult);
            this.panel.Controls.Add(this.btnResultDelete);
            this.panel.Controls.Add(this.btnResultModify);
            this.panel.Controls.Add(this.btnResultAdd);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 83);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(779, 282);
            this.panel.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(21, 10);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(99, 13);
            this.lblResult.TabIndex = 55;
            this.lblResult.Text = "Parameter Mapping";
            // 
            // lstvResult
            // 
            this.lstvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeaderRD});
            this.lstvResult.FullRowSelect = true;
            this.lstvResult.HideSelection = false;
            this.lstvResult.Location = new System.Drawing.Point(154, 10);
            this.lstvResult.Name = "lstvResult";
            this.lstvResult.Size = new System.Drawing.Size(536, 262);
            this.lstvResult.TabIndex = 0;
            this.lstvResult.UseCompatibleStateImageBehavior = false;
            this.lstvResult.View = System.Windows.Forms.View.Details;
            this.lstvResult.DoubleClick += new System.EventHandler(this.lstvResult_DoubleClick);
            this.lstvResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvResult_ColumnClick);
            this.lstvResult.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvResult_ItemSelectionChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "#";
            this.columnHeader7.Width = 24;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Parameter Name";
            this.columnHeader8.Width = 96;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Parameter Type";
            this.columnHeader9.Width = 88;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "GC Gateway Field";
            this.columnHeader10.Width = 125;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Translation";
            this.columnHeader11.Width = 87;
            // 
            // columnHeaderRD
            // 
            this.columnHeaderRD.Text = "Check Redundancy";
            this.columnHeaderRD.Width = 0;
            // 
            // btnResultDelete
            // 
            this.btnResultDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultDelete.Enabled = false;
            this.btnResultDelete.Location = new System.Drawing.Point(696, 66);
            this.btnResultDelete.Name = "btnResultDelete";
            this.btnResultDelete.Size = new System.Drawing.Size(70, 22);
            this.btnResultDelete.TabIndex = 3;
            this.btnResultDelete.Text = "Delete";
            this.btnResultDelete.UseVisualStyleBackColor = true;
            this.btnResultDelete.Click += new System.EventHandler(this.btnResultDelete_Click);
            // 
            // btnResultModify
            // 
            this.btnResultModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultModify.Enabled = false;
            this.btnResultModify.Location = new System.Drawing.Point(696, 38);
            this.btnResultModify.Name = "btnResultModify";
            this.btnResultModify.Size = new System.Drawing.Size(70, 22);
            this.btnResultModify.TabIndex = 2;
            this.btnResultModify.Text = "Edit";
            this.btnResultModify.UseVisualStyleBackColor = true;
            this.btnResultModify.Click += new System.EventHandler(this.btnResultModify_Click);
            // 
            // btnResultAdd
            // 
            this.btnResultAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultAdd.Location = new System.Drawing.Point(696, 10);
            this.btnResultAdd.Name = "btnResultAdd";
            this.btnResultAdd.Size = new System.Drawing.Size(70, 22);
            this.btnResultAdd.TabIndex = 1;
            this.btnResultAdd.Text = "Add";
            this.btnResultAdd.UseVisualStyleBackColor = true;
            this.btnResultAdd.Click += new System.EventHandler(this.btnResultAdd_Click);
            // 
            // panelQueryMode
            // 
            this.panelQueryMode.Controls.Add(this.txtModeName);
            this.panelQueryMode.Controls.Add(this.lblModeType);
            this.panelQueryMode.Controls.Add(this.enumCmbbxOperationType);
            this.panelQueryMode.Controls.Add(this.lblModeName);
            this.panelQueryMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQueryMode.Location = new System.Drawing.Point(3, 16);
            this.panelQueryMode.Name = "panelQueryMode";
            this.panelQueryMode.Size = new System.Drawing.Size(779, 67);
            this.panelQueryMode.TabIndex = 0;
            // 
            // txtModeName
            // 
            this.txtModeName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModeName.Location = new System.Drawing.Point(154, 39);
            this.txtModeName.Name = "txtModeName";
            this.txtModeName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtModeName.Size = new System.Drawing.Size(539, 20);
            this.txtModeName.TabIndex = 1;
            this.txtModeName.WordWrap = false;
            // 
            // enumCmbbxOperationType
            // 
            this.enumCmbbxOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxOperationType.FormattingEnabled = true;
            this.enumCmbbxOperationType.Items.AddRange(new object[] {
            "StorageProcedure",
            "Table",
            "SQLStatement"});
            this.enumCmbbxOperationType.Location = new System.Drawing.Point(154, 7);
            this.enumCmbbxOperationType.Name = "enumCmbbxOperationType";
            this.enumCmbbxOperationType.Size = new System.Drawing.Size(294, 21);
            this.enumCmbbxOperationType.StartIndex = 0;
            this.enumCmbbxOperationType.TabIndex = 0;
            this.enumCmbbxOperationType.TheType = typeof(HYS.SQLOutboundAdapterObjects.ThrPartyDBOperationType);
            this.enumCmbbxOperationType.SelectedIndexChanged += new System.EventHandler(this.enumCmbbxOperationType_SelectedIndexChanged);
            // 
            // Channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 653);
            this.Controls.Add(this.panelMapping);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelCriteria);
            this.Controls.Add(this.panelGeneral);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(820, 680);
            this.Name = "Channel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panelGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelCriteria.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelMapping.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.panelQueryMode.ResumeLayout(false);
            this.panelQueryMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Panel panelCriteria;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelMapping;
        private System.Windows.Forms.Button btnResultDelete;
        private System.Windows.Forms.Button btnResultAdd;
        private System.Windows.Forms.Button btnResultModify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblExist;
        private System.Windows.Forms.TextBox txtChannelName;
        private System.Windows.Forms.Label lblChannelName;
        private System.Windows.Forms.GroupBox groupBox2;
        private EnumComboBox.EnumComboBox enumCmbbxOperationType;
        private System.Windows.Forms.Label lblModeName;
        private System.Windows.Forms.Label lblModeType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lstvResult;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeaderRD;
        private System.Windows.Forms.CheckBox checkBoxStatus;
        private System.Windows.Forms.ListView lstvCriteria;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnCriteriaModify;
        private System.Windows.Forms.Button btnCriteriaAdd;
        private System.Windows.Forms.Button btnCriteriaDelete;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panelQueryMode;
        private System.Windows.Forms.Label lblCriteria;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox txtModeName;

        public EnumComboBox.EnumComboBox EnumCmbbxOperationType {
            get { return enumCmbbxOperationType; }
        }
    }
}