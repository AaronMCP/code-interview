namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    partial class PassiveMode
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstvSP = new System.Windows.Forms.ListView();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.btnSPModify = new System.Windows.Forms.Button();
            this.btnSPAdd = new System.Windows.Forms.Button();
            this.btnSPDelete = new System.Windows.Forms.Button();
            this.lblSPSet = new System.Windows.Forms.Label();
            this.btDefault = new System.Windows.Forms.Button();
            this.btCopy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btDefault);
            this.groupBox1.Controls.Add(this.lstvSP);
            this.groupBox1.Controls.Add(this.btnSPModify);
            this.groupBox1.Controls.Add(this.btnSPAdd);
            this.groupBox1.Controls.Add(this.btCopy);
            this.groupBox1.Controls.Add(this.btnSPDelete);
            this.groupBox1.Controls.Add(this.lblSPSet);
            this.groupBox1.Location = new System.Drawing.Point(15, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 441);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GC Gateway Storage Procedures";
            // 
            // lstvSP
            // 
            this.lstvSP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvSP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14});
            this.lstvSP.FullRowSelect = true;
            this.lstvSP.HideSelection = false;
            this.lstvSP.Location = new System.Drawing.Point(140, 20);
            this.lstvSP.Name = "lstvSP";
            this.lstvSP.Size = new System.Drawing.Size(445, 407);
            this.lstvSP.TabIndex = 0;
            this.lstvSP.UseCompatibleStateImageBehavior = false;
            this.lstvSP.View = System.Windows.Forms.View.Details;
            this.lstvSP.DoubleClick += new System.EventHandler(this.lstvSP_DoubleClick);
            this.lstvSP.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvSP_ColumnClick);
            this.lstvSP.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvSP_ItemSelectionChanged);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "#";
            this.columnHeader13.Width = 25;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Storage Name";
            this.columnHeader14.Width = 355;
            // 
            // btnSPModify
            // 
            this.btnSPModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSPModify.Enabled = false;
            this.btnSPModify.Location = new System.Drawing.Point(591, 48);
            this.btnSPModify.Name = "btnSPModify";
            this.btnSPModify.Size = new System.Drawing.Size(70, 22);
            this.btnSPModify.TabIndex = 2;
            this.btnSPModify.Text = "Edit";
            this.btnSPModify.UseVisualStyleBackColor = true;
            this.btnSPModify.Click += new System.EventHandler(this.btnSPModefy_Click);
            // 
            // btnSPAdd
            // 
            this.btnSPAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSPAdd.Location = new System.Drawing.Point(591, 20);
            this.btnSPAdd.Name = "btnSPAdd";
            this.btnSPAdd.Size = new System.Drawing.Size(70, 22);
            this.btnSPAdd.TabIndex = 1;
            this.btnSPAdd.Text = "Add";
            this.btnSPAdd.UseVisualStyleBackColor = true;
            this.btnSPAdd.Click += new System.EventHandler(this.btnSPAdd_Click);
            // 
            // btnSPDelete
            // 
            this.btnSPDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSPDelete.Enabled = false;
            this.btnSPDelete.Location = new System.Drawing.Point(591, 104);
            this.btnSPDelete.Name = "btnSPDelete";
            this.btnSPDelete.Size = new System.Drawing.Size(70, 22);
            this.btnSPDelete.TabIndex = 3;
            this.btnSPDelete.Text = "Delete";
            this.btnSPDelete.UseVisualStyleBackColor = true;
            this.btnSPDelete.Click += new System.EventHandler(this.btnSPDelete_Click);
            // 
            // lblSPSet
            // 
            this.lblSPSet.AutoSize = true;
            this.lblSPSet.Location = new System.Drawing.Point(20, 20);
            this.lblSPSet.Name = "lblSPSet";
            this.lblSPSet.Size = new System.Drawing.Size(101, 13);
            this.lblSPSet.TabIndex = 81;
            this.lblSPSet.Text = "Storage Procedures";
            // 
            // btDefault
            // 
            this.btDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDefault.Location = new System.Drawing.Point(591, 132);
            this.btDefault.Name = "btDefault";
            this.btDefault.Size = new System.Drawing.Size(70, 22);
            this.btDefault.TabIndex = 82;
            this.btDefault.Text = "Default";
            this.btDefault.UseVisualStyleBackColor = true;
            this.btDefault.Click += new System.EventHandler(this.btDefault_Click);
            // 
            // btCopy
            // 
            this.btCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCopy.Enabled = false;
            this.btCopy.Location = new System.Drawing.Point(591, 76);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(70, 22);
            this.btCopy.TabIndex = 3;
            this.btCopy.Text = "Copy";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // PassiveMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PassiveMode";
            this.Size = new System.Drawing.Size(750, 450);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSPModify;
        private System.Windows.Forms.Button btnSPAdd;
        private System.Windows.Forms.Button btnSPDelete;
        private System.Windows.Forms.ListView lstvSP;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label lblSPSet;
        private System.Windows.Forms.Button btDefault;
        private System.Windows.Forms.Button btCopy;
    }
}
