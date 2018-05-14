namespace OutboundDBInstall
{
    partial class FConfigMergeMappings
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Index", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Patient", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Order", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Report", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "PatientID",
            "<==",
            "Patient.ID"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            this.labWizard = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.listViewMappings = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.btAllCheck = new System.Windows.Forms.Button();
            this.btAllNoCheck = new System.Windows.Forms.Button();
            this.buttonEditMapping = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labWizard
            // 
            this.labWizard.AutoSize = true;
            this.labWizard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labWizard.Location = new System.Drawing.Point(30, 17);
            this.labWizard.Name = "labWizard";
            this.labWizard.Size = new System.Drawing.Size(280, 13);
            this.labWizard.TabIndex = 8;
            this.labWizard.Text = "Step 2: Please configure merging field mappings";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(577, 512);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(496, 512);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 5;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 118;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Inbound Field";
            this.columnHeader3.Width = 217;
            // 
            // listViewMappings
            // 
            this.listViewMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMappings.CheckBoxes = true;
            this.listViewMappings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.listViewMappings.FullRowSelect = true;
            listViewGroup1.Header = "Index";
            listViewGroup1.Name = "grpIndex";
            listViewGroup2.Header = "Patient";
            listViewGroup2.Name = "grpPatient";
            listViewGroup3.Header = "Order";
            listViewGroup3.Name = "grpOrder";
            listViewGroup4.Header = "Report";
            listViewGroup4.Name = "grpReport";
            this.listViewMappings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            listViewItem1.Group = listViewGroup2;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup3;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup4;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.Group = listViewGroup1;
            listViewItem4.StateImageIndex = 0;
            this.listViewMappings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listViewMappings.Location = new System.Drawing.Point(33, 47);
            this.listViewMappings.MultiSelect = false;
            this.listViewMappings.Name = "listViewMappings";
            this.listViewMappings.Size = new System.Drawing.Size(618, 459);
            this.listViewMappings.TabIndex = 3;
            this.listViewMappings.UseCompatibleStateImageBehavior = false;
            this.listViewMappings.View = System.Windows.Forms.View.Details;
            this.listViewMappings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewMappings_ItemCheck);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Outbound Field";
            this.columnHeader2.Width = 217;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            this.columnHeader4.Width = 40;
            // 
            // btAllCheck
            // 
            this.btAllCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAllCheck.Location = new System.Drawing.Point(34, 512);
            this.btAllCheck.Name = "btAllCheck";
            this.btAllCheck.Size = new System.Drawing.Size(75, 23);
            this.btAllCheck.TabIndex = 4;
            this.btAllCheck.Text = "Select All";
            this.btAllCheck.UseVisualStyleBackColor = true;
            this.btAllCheck.Click += new System.EventHandler(this.btAllCheck_Click);
            // 
            // btAllNoCheck
            // 
            this.btAllNoCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAllNoCheck.Location = new System.Drawing.Point(115, 512);
            this.btAllNoCheck.Name = "btAllNoCheck";
            this.btAllNoCheck.Size = new System.Drawing.Size(86, 23);
            this.btAllNoCheck.TabIndex = 7;
            this.btAllNoCheck.Text = "Unselect All";
            this.btAllNoCheck.UseVisualStyleBackColor = true;
            this.btAllNoCheck.Click += new System.EventHandler(this.btAllNoCheck_Click);
            // 
            // buttonEditMapping
            // 
            this.buttonEditMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditMapping.Location = new System.Drawing.Point(207, 512);
            this.buttonEditMapping.Name = "buttonEditMapping";
            this.buttonEditMapping.Size = new System.Drawing.Size(86, 23);
            this.buttonEditMapping.TabIndex = 9;
            this.buttonEditMapping.Text = "Edit Mapping";
            this.buttonEditMapping.UseVisualStyleBackColor = true;
            this.buttonEditMapping.Click += new System.EventHandler(this.buttonEditMapping_Click);
            // 
            // FConfigMergeMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 558);
            this.Controls.Add(this.buttonEditMapping);
            this.Controls.Add(this.labWizard);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAllNoCheck);
            this.Controls.Add(this.btAllCheck);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.listViewMappings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FConfigMergeMappings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure Merging Field Mappings";
            this.DoubleClick += new System.EventHandler(this.FConfigMergeMappings_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labWizard;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listViewMappings;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btAllCheck;
        private System.Windows.Forms.Button btAllNoCheck;
        private System.Windows.Forms.Button buttonEditMapping;
    }
}