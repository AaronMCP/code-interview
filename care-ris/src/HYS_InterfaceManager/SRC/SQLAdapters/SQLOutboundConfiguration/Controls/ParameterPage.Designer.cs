namespace HYS.SQLOutboundAdapterConfiguration.Controls
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCrireria = new System.Windows.Forms.Label();
            this.btnCriteriaModify = new System.Windows.Forms.Button();
            this.btnCriteriaAdd = new System.Windows.Forms.Button();
            this.btnCriteriaDelete = new System.Windows.Forms.Button();
            this.lstvInParameter = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnResultAdd = new System.Windows.Forms.Button();
            this.btnResultDelete = new System.Windows.Forms.Button();
            this.btnResultModify = new System.Windows.Forms.Button();
            this.lstvOutParameter = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCrireria);
            this.panel1.Controls.Add(this.btnCriteriaModify);
            this.panel1.Controls.Add(this.btnCriteriaAdd);
            this.panel1.Controls.Add(this.btnCriteriaDelete);
            this.panel1.Controls.Add(this.lstvInParameter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 174);
            this.panel1.TabIndex = 0;
            // 
            // lblCrireria
            // 
            this.lblCrireria.AutoSize = true;
            this.lblCrireria.Location = new System.Drawing.Point(34, 16);
            this.lblCrireria.Name = "lblCrireria";
            this.lblCrireria.Size = new System.Drawing.Size(82, 13);
            this.lblCrireria.TabIndex = 98;
            this.lblCrireria.Text = "Input Parameter";
            // 
            // btnCriteriaModify
            // 
            this.btnCriteriaModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaModify.Enabled = false;
            this.btnCriteriaModify.Location = new System.Drawing.Point(669, 44);
            this.btnCriteriaModify.Name = "btnCriteriaModify";
            this.btnCriteriaModify.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaModify.TabIndex = 2;
            this.btnCriteriaModify.Text = "Edit";
            this.btnCriteriaModify.UseVisualStyleBackColor = true;
            this.btnCriteriaModify.Click += new System.EventHandler(this.btnInParameterModify_Click);
            // 
            // btnCriteriaAdd
            // 
            this.btnCriteriaAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaAdd.Location = new System.Drawing.Point(669, 16);
            this.btnCriteriaAdd.Name = "btnCriteriaAdd";
            this.btnCriteriaAdd.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaAdd.TabIndex = 1;
            this.btnCriteriaAdd.Text = "Add";
            this.btnCriteriaAdd.UseVisualStyleBackColor = true;
            this.btnCriteriaAdd.Click += new System.EventHandler(this.btnInParameterAdd_Click);
            // 
            // btnCriteriaDelete
            // 
            this.btnCriteriaDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaDelete.Enabled = false;
            this.btnCriteriaDelete.Location = new System.Drawing.Point(669, 72);
            this.btnCriteriaDelete.Name = "btnCriteriaDelete";
            this.btnCriteriaDelete.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaDelete.TabIndex = 3;
            this.btnCriteriaDelete.Text = "Delete";
            this.btnCriteriaDelete.UseVisualStyleBackColor = true;
            this.btnCriteriaDelete.Click += new System.EventHandler(this.btnInParameterDelete_Click);
            // 
            // lstvInParameter
            // 
            this.lstvInParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvInParameter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader3});
            this.lstvInParameter.FullRowSelect = true;
            this.lstvInParameter.HideSelection = false;
            this.lstvInParameter.Location = new System.Drawing.Point(162, 16);
            this.lstvInParameter.Name = "lstvInParameter";
            this.lstvInParameter.Size = new System.Drawing.Size(501, 151);
            this.lstvInParameter.TabIndex = 0;
            this.lstvInParameter.UseCompatibleStateImageBehavior = false;
            this.lstvInParameter.View = System.Windows.Forms.View.Details;
            this.lstvInParameter.DoubleClick += new System.EventHandler(this.lstvInParameter_DoubleClick);
            this.lstvInParameter.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvInParameter_ColumnClick);
            this.lstvInParameter.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvInParameter_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Parameter";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "GC Gateway Field";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Translation";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Operator";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Join Type";
            this.columnHeader3.Width = 75;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblResult);
            this.panel2.Controls.Add(this.btnResultAdd);
            this.panel2.Controls.Add(this.btnResultDelete);
            this.panel2.Controls.Add(this.btnResultModify);
            this.panel2.Controls.Add(this.lstvOutParameter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 174);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(750, 226);
            this.panel2.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(34, 13);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(100, 13);
            this.lblResult.TabIndex = 99;
            this.lblResult.Text = "Result Set Mapping";
            // 
            // btnResultAdd
            // 
            this.btnResultAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultAdd.Location = new System.Drawing.Point(669, 7);
            this.btnResultAdd.Name = "btnResultAdd";
            this.btnResultAdd.Size = new System.Drawing.Size(70, 22);
            this.btnResultAdd.TabIndex = 1;
            this.btnResultAdd.Text = "Add";
            this.btnResultAdd.UseVisualStyleBackColor = true;
            this.btnResultAdd.Click += new System.EventHandler(this.btnOutParameterAdd_Click);
            // 
            // btnResultDelete
            // 
            this.btnResultDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultDelete.Enabled = false;
            this.btnResultDelete.Location = new System.Drawing.Point(669, 63);
            this.btnResultDelete.Name = "btnResultDelete";
            this.btnResultDelete.Size = new System.Drawing.Size(70, 22);
            this.btnResultDelete.TabIndex = 3;
            this.btnResultDelete.Text = "Delete";
            this.btnResultDelete.UseVisualStyleBackColor = true;
            this.btnResultDelete.Click += new System.EventHandler(this.btnOutParameterDelete_Click);
            // 
            // btnResultModify
            // 
            this.btnResultModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultModify.Enabled = false;
            this.btnResultModify.Location = new System.Drawing.Point(669, 35);
            this.btnResultModify.Name = "btnResultModify";
            this.btnResultModify.Size = new System.Drawing.Size(70, 22);
            this.btnResultModify.TabIndex = 2;
            this.btnResultModify.Text = "Edit";
            this.btnResultModify.UseVisualStyleBackColor = true;
            this.btnResultModify.Click += new System.EventHandler(this.btnOutParameterModify_Click);
            // 
            // lstvOutParameter
            // 
            this.lstvOutParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvOutParameter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader10,
            this.columnHeader11});
            this.lstvOutParameter.FullRowSelect = true;
            this.lstvOutParameter.HideSelection = false;
            this.lstvOutParameter.Location = new System.Drawing.Point(162, 7);
            this.lstvOutParameter.Name = "lstvOutParameter";
            this.lstvOutParameter.Size = new System.Drawing.Size(501, 210);
            this.lstvOutParameter.TabIndex = 0;
            this.lstvOutParameter.UseCompatibleStateImageBehavior = false;
            this.lstvOutParameter.View = System.Windows.Forms.View.Details;
            this.lstvOutParameter.DoubleClick += new System.EventHandler(this.lstvOutParameter_DoubleClick);
            this.lstvOutParameter.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvOutParameter_ColumnClick);
            this.lstvOutParameter.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvOutParameter_ItemSelectionChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "#";
            this.columnHeader7.Width = 23;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Third Party Field";
            this.columnHeader8.Width = 143;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "GC Gateway Field";
            this.columnHeader10.Width = 144;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Translation";
            this.columnHeader11.Width = 115;
            // 
            // ParameterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ParameterPage";
            this.Size = new System.Drawing.Size(750, 400);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCriteriaModify;
        private System.Windows.Forms.Button btnCriteriaAdd;
        private System.Windows.Forms.Button btnCriteriaDelete;
        private System.Windows.Forms.ListView lstvInParameter;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblCrireria;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnResultAdd;
        private System.Windows.Forms.Button btnResultDelete;
        private System.Windows.Forms.Button btnResultModify;
        private System.Windows.Forms.ListView lstvOutParameter;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;

    }
}
