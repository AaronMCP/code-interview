namespace HYS.RdetAdapter.RdetOutboundAdapterConfiguration.Forms
{
    partial class FChannel
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
            this.lblExist = new System.Windows.Forms.Label();
            this.checkBoxStatus = new System.Windows.Forms.CheckBox();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.panelCriteria = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCriteriaDelete = new System.Windows.Forms.Button();
            this.btnCriteriaModify = new System.Windows.Forms.Button();
            this.btnCriteriaAdd = new System.Windows.Forms.Button();
            this.lstvCriteria = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.btnStatement = new System.Windows.Forms.Button();
            this.txtStatement = new System.Windows.Forms.TextBox();
            this.radiobtnDataset = new System.Windows.Forms.RadioButton();
            this.radiobtnStatement = new System.Windows.Forms.RadioButton();
            this.panelcontrol = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelResult = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblMapping = new System.Windows.Forms.Label();
            this.btnResultDelete = new System.Windows.Forms.Button();
            this.btnResultModify = new System.Windows.Forms.Button();
            this.btnResultAdd = new System.Windows.Forms.Button();
            this.lstvResult = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.panelGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelCriteria.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelcontrol.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGeneral
            // 
            this.panelGeneral.Controls.Add(this.groupBox1);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGeneral.Location = new System.Drawing.Point(0, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(792, 70);
            this.panelGeneral.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblExist);
            this.groupBox1.Controls.Add(this.checkBoxStatus);
            this.groupBox1.Controls.Add(this.txtChannelName);
            this.groupBox1.Controls.Add(this.lblChannel);
            this.groupBox1.Location = new System.Drawing.Point(15, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // lblExist
            // 
            this.lblExist.AutoSize = true;
            this.lblExist.ForeColor = System.Drawing.Color.Red;
            this.lblExist.Location = new System.Drawing.Point(127, 46);
            this.lblExist.Name = "lblExist";
            this.lblExist.Size = new System.Drawing.Size(86, 13);
            this.lblExist.TabIndex = 10;
            this.lblExist.Text = "Channel Existed!";
            this.lblExist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExist.Visible = false;
            // 
            // checkBoxStatus
            // 
            this.checkBoxStatus.AutoSize = true;
            this.checkBoxStatus.Location = new System.Drawing.Point(389, 22);
            this.checkBoxStatus.Name = "checkBoxStatus";
            this.checkBoxStatus.Size = new System.Drawing.Size(59, 17);
            this.checkBoxStatus.TabIndex = 2;
            this.checkBoxStatus.Text = "Enable";
            this.checkBoxStatus.UseVisualStyleBackColor = true;
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(129, 20);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(229, 20);
            this.txtChannelName.TabIndex = 1;
            this.txtChannelName.TextChanged += new System.EventHandler(this.txtChannelName_TextChanged);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(12, 23);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(77, 13);
            this.lblChannel.TabIndex = 0;
            this.lblChannel.Text = "Channel Name";
            // 
            // panelCriteria
            // 
            this.panelCriteria.Controls.Add(this.groupBox2);
            this.panelCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCriteria.Location = new System.Drawing.Point(0, 70);
            this.panelCriteria.Name = "panelCriteria";
            this.panelCriteria.Size = new System.Drawing.Size(792, 263);
            this.panelCriteria.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnCriteriaDelete);
            this.groupBox2.Controls.Add(this.btnCriteriaModify);
            this.groupBox2.Controls.Add(this.btnCriteriaAdd);
            this.groupBox2.Controls.Add(this.lstvCriteria);
            this.groupBox2.Controls.Add(this.btnStatement);
            this.groupBox2.Controls.Add(this.txtStatement);
            this.groupBox2.Controls.Add(this.radiobtnDataset);
            this.groupBox2.Controls.Add(this.radiobtnStatement);
            this.groupBox2.Location = new System.Drawing.Point(14, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(765, 257);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Access Mode";
            // 
            // btnCriteriaDelete
            // 
            this.btnCriteriaDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaDelete.Enabled = false;
            this.btnCriteriaDelete.Location = new System.Drawing.Point(675, 118);
            this.btnCriteriaDelete.Name = "btnCriteriaDelete";
            this.btnCriteriaDelete.Size = new System.Drawing.Size(73, 22);
            this.btnCriteriaDelete.TabIndex = 7;
            this.btnCriteriaDelete.Text = "Delete";
            this.btnCriteriaDelete.UseVisualStyleBackColor = true;
            this.btnCriteriaDelete.Click += new System.EventHandler(this.btnCriteriaDelete_Click);
            // 
            // btnCriteriaModify
            // 
            this.btnCriteriaModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaModify.Enabled = false;
            this.btnCriteriaModify.Location = new System.Drawing.Point(675, 85);
            this.btnCriteriaModify.Name = "btnCriteriaModify";
            this.btnCriteriaModify.Size = new System.Drawing.Size(73, 22);
            this.btnCriteriaModify.TabIndex = 6;
            this.btnCriteriaModify.Text = "Edit";
            this.btnCriteriaModify.UseVisualStyleBackColor = true;
            this.btnCriteriaModify.Click += new System.EventHandler(this.btnCriteriaModify_Click);
            // 
            // btnCriteriaAdd
            // 
            this.btnCriteriaAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaAdd.Enabled = false;
            this.btnCriteriaAdd.Location = new System.Drawing.Point(675, 53);
            this.btnCriteriaAdd.Name = "btnCriteriaAdd";
            this.btnCriteriaAdd.Size = new System.Drawing.Size(73, 22);
            this.btnCriteriaAdd.TabIndex = 5;
            this.btnCriteriaAdd.Text = "Add";
            this.btnCriteriaAdd.UseVisualStyleBackColor = true;
            this.btnCriteriaAdd.Click += new System.EventHandler(this.btnCriteriaAdd_Click);
            // 
            // lstvCriteria
            // 
            this.lstvCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvCriteria.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader12});
            this.lstvCriteria.Enabled = false;
            this.lstvCriteria.FullRowSelect = true;
            this.lstvCriteria.HideSelection = false;
            this.lstvCriteria.Location = new System.Drawing.Point(130, 53);
            this.lstvCriteria.Name = "lstvCriteria";
            this.lstvCriteria.Size = new System.Drawing.Size(539, 198);
            this.lstvCriteria.TabIndex = 4;
            this.lstvCriteria.UseCompatibleStateImageBehavior = false;
            this.lstvCriteria.View = System.Windows.Forms.View.Details;
            this.lstvCriteria.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvCriteria_ColumnClick);
            this.lstvCriteria.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvCriteria_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Table Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Field Name";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Operator";
            this.columnHeader4.Width = 77;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Value";
            this.columnHeader5.Width = 88;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Join Type";
            this.columnHeader12.Width = 120;
            // 
            // btnStatement
            // 
            this.btnStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatement.Location = new System.Drawing.Point(717, 14);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(31, 22);
            this.btnStatement.TabIndex = 3;
            this.btnStatement.Text = "...";
            this.btnStatement.UseVisualStyleBackColor = true;
            this.btnStatement.Click += new System.EventHandler(this.btnStatement_Click);
            // 
            // txtStatement
            // 
            this.txtStatement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatement.Location = new System.Drawing.Point(130, 16);
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.Size = new System.Drawing.Size(579, 20);
            this.txtStatement.TabIndex = 2;
            // 
            // radiobtnDataset
            // 
            this.radiobtnDataset.AutoSize = true;
            this.radiobtnDataset.Location = new System.Drawing.Point(15, 53);
            this.radiobtnDataset.Name = "radiobtnDataset";
            this.radiobtnDataset.Size = new System.Drawing.Size(103, 17);
            this.radiobtnDataset.TabIndex = 1;
            this.radiobtnDataset.Text = "Parameter Mode";
            this.radiobtnDataset.UseVisualStyleBackColor = true;
            // 
            // radiobtnStatement
            // 
            this.radiobtnStatement.AutoSize = true;
            this.radiobtnStatement.Checked = true;
            this.radiobtnStatement.Location = new System.Drawing.Point(15, 16);
            this.radiobtnStatement.Name = "radiobtnStatement";
            this.radiobtnStatement.Size = new System.Drawing.Size(103, 17);
            this.radiobtnStatement.TabIndex = 0;
            this.radiobtnStatement.TabStop = true;
            this.radiobtnStatement.Text = "Statement Mode";
            this.radiobtnStatement.UseVisualStyleBackColor = true;
            this.radiobtnStatement.CheckedChanged += new System.EventHandler(this.radiobtnStatement_CheckedChanged);
            // 
            // panelcontrol
            // 
            this.panelcontrol.Controls.Add(this.btnCancel);
            this.panelcontrol.Controls.Add(this.btnOK);
            this.panelcontrol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelcontrol.Location = new System.Drawing.Point(0, 616);
            this.panelcontrol.Name = "panelcontrol";
            this.panelcontrol.Size = new System.Drawing.Size(792, 51);
            this.panelcontrol.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(706, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 22);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(627, 16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 22);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelResult
            // 
            this.panelResult.Controls.Add(this.groupBox3);
            this.panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResult.Location = new System.Drawing.Point(0, 333);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(792, 283);
            this.panelResult.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblMapping);
            this.groupBox3.Controls.Add(this.btnResultDelete);
            this.groupBox3.Controls.Add(this.btnResultModify);
            this.groupBox3.Controls.Add(this.btnResultAdd);
            this.groupBox3.Controls.Add(this.lstvResult);
            this.groupBox3.Location = new System.Drawing.Point(14, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(765, 276);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Field Mapping";
            // 
            // lblMapping
            // 
            this.lblMapping.AutoSize = true;
            this.lblMapping.Location = new System.Drawing.Point(21, 22);
            this.lblMapping.Name = "lblMapping";
            this.lblMapping.Size = new System.Drawing.Size(67, 13);
            this.lblMapping.TabIndex = 12;
            this.lblMapping.Text = "Mapping List";
            // 
            // btnResultDelete
            // 
            this.btnResultDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultDelete.Enabled = false;
            this.btnResultDelete.Location = new System.Drawing.Point(675, 90);
            this.btnResultDelete.Name = "btnResultDelete";
            this.btnResultDelete.Size = new System.Drawing.Size(73, 22);
            this.btnResultDelete.TabIndex = 11;
            this.btnResultDelete.Text = "Delete";
            this.btnResultDelete.UseVisualStyleBackColor = true;
            this.btnResultDelete.Click += new System.EventHandler(this.btnResultDelete_Click);
            // 
            // btnResultModify
            // 
            this.btnResultModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultModify.Enabled = false;
            this.btnResultModify.Location = new System.Drawing.Point(675, 55);
            this.btnResultModify.Name = "btnResultModify";
            this.btnResultModify.Size = new System.Drawing.Size(73, 22);
            this.btnResultModify.TabIndex = 10;
            this.btnResultModify.Text = "Edit";
            this.btnResultModify.UseVisualStyleBackColor = true;
            this.btnResultModify.Click += new System.EventHandler(this.btnResultModify_Click);
            // 
            // btnResultAdd
            // 
            this.btnResultAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultAdd.Location = new System.Drawing.Point(675, 22);
            this.btnResultAdd.Name = "btnResultAdd";
            this.btnResultAdd.Size = new System.Drawing.Size(73, 22);
            this.btnResultAdd.TabIndex = 9;
            this.btnResultAdd.Text = "Add";
            this.btnResultAdd.UseVisualStyleBackColor = true;
            this.btnResultAdd.Click += new System.EventHandler(this.btnResultAdd_Click);
            // 
            // lstvResult
            // 
            this.lstvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lstvResult.FullRowSelect = true;
            this.lstvResult.HideSelection = false;
            this.lstvResult.Location = new System.Drawing.Point(130, 22);
            this.lstvResult.Name = "lstvResult";
            this.lstvResult.Size = new System.Drawing.Size(539, 248);
            this.lstvResult.TabIndex = 8;
            this.lstvResult.UseCompatibleStateImageBehavior = false;
            this.lstvResult.View = System.Windows.Forms.View.Details;
            this.lstvResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvResult_ColumnClick);
            this.lstvResult.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvResult_ItemSelectionChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#";
            this.columnHeader6.Width = 40;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "GC Gateway Field";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ThirdParty Field";
            this.columnHeader8.Width = 131;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ThirdParty Field Type";
            this.columnHeader9.Width = 160;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Translation";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Check Redundancy";
            this.columnHeader11.Width = 120;
            // 
            // FChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 667);
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.panelcontrol);
            this.Controls.Add(this.panelCriteria);
            this.Controls.Add(this.panelGeneral);
            this.Name = "FChannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Channel";
            this.panelGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelCriteria.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelcontrol.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelCriteria;
        private System.Windows.Forms.Panel panelcontrol;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxStatus;
        private System.Windows.Forms.TextBox txtChannelName;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.ListView lstvCriteria;
        private System.Windows.Forms.Button btnStatement;
        private System.Windows.Forms.TextBox txtStatement;
        private System.Windows.Forms.RadioButton radiobtnDataset;
        private System.Windows.Forms.RadioButton radiobtnStatement;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCriteriaDelete;
        private System.Windows.Forms.Button btnCriteriaModify;
        private System.Windows.Forms.Button btnCriteriaAdd;
        private System.Windows.Forms.Button btnResultDelete;
        private System.Windows.Forms.Button btnResultModify;
        private System.Windows.Forms.Button btnResultAdd;
        private System.Windows.Forms.ListView lstvResult;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Label lblExist;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Label lblMapping;
    }
}