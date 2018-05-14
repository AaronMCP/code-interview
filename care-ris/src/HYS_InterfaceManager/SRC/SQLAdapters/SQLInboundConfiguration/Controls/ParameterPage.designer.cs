namespace HYS.SQLInboundAdapterConfiguration.Controls
{
    partial class ParameterPage
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
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.lstvParameter = new System.Windows.Forms.ListView();
            this.btnResultModify = new System.Windows.Forms.Button();
            this.btnResultDelete = new System.Windows.Forms.Button();
            this.btnResultAdd = new System.Windows.Forms.Button();
            this.lblSPName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "#";
            this.columnHeader7.Width = 31;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Input Parameters";
            this.columnHeader8.Width = 165;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "GCGateway Field";
            this.columnHeader10.Width = 165;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Translation";
            this.columnHeader11.Width = 120;
            // 
            // lstvParameter
            // 
            this.lstvParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvParameter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader10,
            this.columnHeader11});
            this.lstvParameter.FullRowSelect = true;
            this.lstvParameter.HideSelection = false;
            this.lstvParameter.Location = new System.Drawing.Point(166, 16);
            this.lstvParameter.Name = "lstvParameter";
            this.lstvParameter.Size = new System.Drawing.Size(397, 175);
            this.lstvParameter.TabIndex = 0;
            this.lstvParameter.UseCompatibleStateImageBehavior = false;
            this.lstvParameter.View = System.Windows.Forms.View.Details;
            this.lstvParameter.DoubleClick += new System.EventHandler(this.lstvParameter_DoubleClick);
            this.lstvParameter.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvParameter_ColumnClick);
            this.lstvParameter.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvParameter_ItemSelectionChanged);
            // 
            // btnResultModify
            // 
            this.btnResultModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultModify.Enabled = false;
            this.btnResultModify.Location = new System.Drawing.Point(569, 47);
            this.btnResultModify.Name = "btnResultModify";
            this.btnResultModify.Size = new System.Drawing.Size(69, 24);
            this.btnResultModify.TabIndex = 2;
            this.btnResultModify.Text = "Edit";
            this.btnResultModify.UseVisualStyleBackColor = true;
            this.btnResultModify.Click += new System.EventHandler(this.btnParameterModify_Click);
            // 
            // btnResultDelete
            // 
            this.btnResultDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultDelete.Enabled = false;
            this.btnResultDelete.Location = new System.Drawing.Point(569, 77);
            this.btnResultDelete.Name = "btnResultDelete";
            this.btnResultDelete.Size = new System.Drawing.Size(69, 23);
            this.btnResultDelete.TabIndex = 3;
            this.btnResultDelete.Text = "Delete";
            this.btnResultDelete.UseVisualStyleBackColor = true;
            this.btnResultDelete.Click += new System.EventHandler(this.btnParameterDelete_Click);
            // 
            // btnResultAdd
            // 
            this.btnResultAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultAdd.Location = new System.Drawing.Point(569, 16);
            this.btnResultAdd.Name = "btnResultAdd";
            this.btnResultAdd.Size = new System.Drawing.Size(69, 24);
            this.btnResultAdd.TabIndex = 1;
            this.btnResultAdd.Text = "Add";
            this.btnResultAdd.UseVisualStyleBackColor = true;
            this.btnResultAdd.Click += new System.EventHandler(this.btnParameterAdd_Click);
            // 
            // lblSPName
            // 
            this.lblSPName.AutoSize = true;
            this.lblSPName.Location = new System.Drawing.Point(33, 16);
            this.lblSPName.Margin = new System.Windows.Forms.Padding(0);
            this.lblSPName.Name = "lblSPName";
            this.lblSPName.Size = new System.Drawing.Size(82, 13);
            this.lblSPName.TabIndex = 90;
            this.lblSPName.Text = "Input Parameter";
            // 
            // ParameterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSPName);
            this.Controls.Add(this.btnResultAdd);
            this.Controls.Add(this.btnResultDelete);
            this.Controls.Add(this.btnResultModify);
            this.Controls.Add(this.lstvParameter);
            this.Name = "ParameterPage";
            this.Size = new System.Drawing.Size(650, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ListView lstvParameter;
        private System.Windows.Forms.Button btnResultModify;
        private System.Windows.Forms.Button btnResultDelete;
        private System.Windows.Forms.Button btnResultAdd;
        private System.Windows.Forms.Label lblSPName;
    }
}
