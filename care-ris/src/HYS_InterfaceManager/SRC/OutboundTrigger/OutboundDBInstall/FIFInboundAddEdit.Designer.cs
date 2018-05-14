namespace OutboundDBInstall
{
    partial class FIFInboundAddEdit
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
            this.btCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxRedundancyChecking = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gBoxfilter = new System.Windows.Forms.GroupBox();
            this.lViewFilter = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEditFilter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbInboundInterface = new System.Windows.Forms.ComboBox();
            this.clbEventTypeList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeViewMatchCriteria = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btSelMergeFields = new System.Windows.Forms.Button();
            this.ckbIsMerging = new System.Windows.Forms.CheckBox();
            this.btSelPKFields = new System.Windows.Forms.Button();
            this.lvMergeFields = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gBoxfilter.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(628, 27);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxRedundancyChecking);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btCancel);
            this.panel2.Controls.Add(this.btOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 525);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 62);
            this.panel2.TabIndex = 2;
            // 
            // checkBoxRedundancyChecking
            // 
            this.checkBoxRedundancyChecking.Location = new System.Drawing.Point(40, 27);
            this.checkBoxRedundancyChecking.Name = "checkBoxRedundancyChecking";
            this.checkBoxRedundancyChecking.Size = new System.Drawing.Size(198, 21);
            this.checkBoxRedundancyChecking.TabIndex = 20;
            this.checkBoxRedundancyChecking.Text = "Check Redundancy";
            this.checkBoxRedundancyChecking.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 2);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(535, 27);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gBoxfilter);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.cbInboundInterface);
            this.panel1.Controls.Add(this.clbEventTypeList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 587);
            this.panel1.TabIndex = 1;
            // 
            // gBoxfilter
            // 
            this.gBoxfilter.Controls.Add(this.lViewFilter);
            this.gBoxfilter.Controls.Add(this.label5);
            this.gBoxfilter.Controls.Add(this.btnEditFilter);
            this.gBoxfilter.Location = new System.Drawing.Point(329, 73);
            this.gBoxfilter.Name = "gBoxfilter";
            this.gBoxfilter.Size = new System.Drawing.Size(364, 155);
            this.gBoxfilter.TabIndex = 18;
            this.gBoxfilter.TabStop = false;
            // 
            // lViewFilter
            // 
            this.lViewFilter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9});
            this.lViewFilter.FullRowSelect = true;
            this.lViewFilter.Location = new System.Drawing.Point(24, 57);
            this.lViewFilter.MultiSelect = false;
            this.lViewFilter.Name = "lViewFilter";
            this.lViewFilter.Size = new System.Drawing.Size(333, 85);
            this.lViewFilter.TabIndex = 17;
            this.lViewFilter.UseCompatibleStateImageBehavior = false;
            this.lViewFilter.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "No.";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Filter";
            this.columnHeader9.Width = 257;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Other Filter Fields";
            // 
            // btnEditFilter
            // 
            this.btnEditFilter.Location = new System.Drawing.Point(283, 22);
            this.btnEditFilter.Name = "btnEditFilter";
            this.btnEditFilter.Size = new System.Drawing.Size(75, 23);
            this.btnEditFilter.TabIndex = 15;
            this.btnEditFilter.Text = "Edit";
            this.btnEditFilter.UseVisualStyleBackColor = true;
            this.btnEditFilter.Click += new System.EventHandler(this.btnEditFilter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(146, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 2);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // cbInboundInterface
            // 
            this.cbInboundInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInboundInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInboundInterface.FormattingEnabled = true;
            this.cbInboundInterface.Location = new System.Drawing.Point(146, 34);
            this.cbInboundInterface.Name = "cbInboundInterface";
            this.cbInboundInterface.Size = new System.Drawing.Size(549, 21);
            this.cbInboundInterface.TabIndex = 12;
            this.cbInboundInterface.SelectedIndexChanged += new System.EventHandler(this.cbInboundInterface_SelectedIndexChanged);
            // 
            // clbEventTypeList
            // 
            this.clbEventTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clbEventTypeList.FormattingEnabled = true;
            this.clbEventTypeList.Location = new System.Drawing.Point(40, 100);
            this.clbEventTypeList.Name = "clbEventTypeList";
            this.clbEventTypeList.Size = new System.Drawing.Size(265, 409);
            this.clbEventTypeList.TabIndex = 9;
            this.clbEventTypeList.SelectedIndexChanged += new System.EventHandler(this.clbEventTypeList_SelectedIndexChanged);
            this.clbEventTypeList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbEventTypeList_ItemCheck);
            this.clbEventTypeList.Click += new System.EventHandler(this.clbEventTypeList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Inbound Interface ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Accept Event Type";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeViewMatchCriteria);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btSelMergeFields);
            this.groupBox3.Controls.Add(this.ckbIsMerging);
            this.groupBox3.Controls.Add(this.btSelPKFields);
            this.groupBox3.Controls.Add(this.lvMergeFields);
            this.groupBox3.Location = new System.Drawing.Point(329, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 290);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            // 
            // treeViewMatchCriteria
            // 
            this.treeViewMatchCriteria.Location = new System.Drawing.Point(25, 51);
            this.treeViewMatchCriteria.Name = "treeViewMatchCriteria";
            this.treeViewMatchCriteria.Size = new System.Drawing.Size(332, 95);
            this.treeViewMatchCriteria.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Merge Criteria";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Merge Field Mappings";
            // 
            // btSelMergeFields
            // 
            this.btSelMergeFields.Location = new System.Drawing.Point(282, 161);
            this.btSelMergeFields.Name = "btSelMergeFields";
            this.btSelMergeFields.Size = new System.Drawing.Size(75, 20);
            this.btSelMergeFields.TabIndex = 15;
            this.btSelMergeFields.Text = "Edit";
            this.btSelMergeFields.UseVisualStyleBackColor = true;
            this.btSelMergeFields.Click += new System.EventHandler(this.btSelMergeFields_Click);
            // 
            // ckbIsMerging
            // 
            this.ckbIsMerging.AutoSize = true;
            this.ckbIsMerging.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbIsMerging.Location = new System.Drawing.Point(25, -2);
            this.ckbIsMerging.Name = "ckbIsMerging";
            this.ckbIsMerging.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbIsMerging.Size = new System.Drawing.Size(56, 17);
            this.ckbIsMerging.TabIndex = 11;
            this.ckbIsMerging.Text = "Merge";
            this.ckbIsMerging.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbIsMerging.UseVisualStyleBackColor = true;
            this.ckbIsMerging.Click += new System.EventHandler(this.ckbIsMerging_Click);
            // 
            // btSelPKFields
            // 
            this.btSelPKFields.Location = new System.Drawing.Point(283, 22);
            this.btSelPKFields.Name = "btSelPKFields";
            this.btSelPKFields.Size = new System.Drawing.Size(75, 23);
            this.btSelPKFields.TabIndex = 15;
            this.btSelPKFields.Text = "Edit";
            this.btSelPKFields.UseVisualStyleBackColor = true;
            this.btSelPKFields.Click += new System.EventHandler(this.btSelPKFields_Click);
            // 
            // lvMergeFields
            // 
            this.lvMergeFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvMergeFields.HideSelection = false;
            this.lvMergeFields.Location = new System.Drawing.Point(24, 192);
            this.lvMergeFields.Name = "lvMergeFields";
            this.lvMergeFields.Size = new System.Drawing.Size(334, 93);
            this.lvMergeFields.TabIndex = 14;
            this.lvMergeFields.UseCompatibleStateImageBehavior = false;
            this.lvMergeFields.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "No.";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Mapping";
            this.columnHeader6.Width = 260;
            // 
            // FIFInboundAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 587);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FIFInboundAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIFInboundAddEdit";
            this.Load += new System.EventHandler(this.FIFInboundAddEdit_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gBoxfilter.ResumeLayout(false);
            this.gBoxfilter.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbIsMerging;
        private System.Windows.Forms.CheckedListBox clbEventTypeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbInboundInterface;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvMergeFields;
        private System.Windows.Forms.Button btSelMergeFields;
        private System.Windows.Forms.Button btSelPKFields;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxRedundancyChecking;
        private System.Windows.Forms.GroupBox gBoxfilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditFilter;
        private System.Windows.Forms.ListView lViewFilter;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.TreeView treeViewMatchCriteria;
    }
}