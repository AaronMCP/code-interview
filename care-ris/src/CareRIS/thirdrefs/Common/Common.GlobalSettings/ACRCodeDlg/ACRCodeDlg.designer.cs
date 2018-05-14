namespace Kodak.GCRIS.Client.Oam.ACRCode
{
    partial class ACRCodeDlg
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBoxACRCode = new System.Windows.Forms.TextBox();
            this.txtBoxPathology = new System.Windows.Forms.TextBox();
            this.txtBoxAnatomy = new System.Windows.Forms.TextBox();
            this.lblACRCode = new System.Windows.Forms.Label();
            this.lblPothology = new System.Windows.Forms.Label();
            this.lblAnatomy = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.treeViewAnatomy = new System.Windows.Forms.TreeView();
            this.treeViewPathology = new System.Windows.Forms.TreeView();
            this.REditBoxResult = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 111);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnOK);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.txtBoxACRCode);
            this.splitContainer1.Panel1.Controls.Add(this.txtBoxPathology);
            this.splitContainer1.Panel1.Controls.Add(this.txtBoxAnatomy);
            this.splitContainer1.Panel1.Controls.Add(this.lblACRCode);
            this.splitContainer1.Panel1.Controls.Add(this.lblPothology);
            this.splitContainer1.Panel1.Controls.Add(this.lblAnatomy);
            this.splitContainer1.Panel1.Controls.Add(this.lblLocation);
            this.splitContainer1.Panel1.Controls.Add(this.treeViewAnatomy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeViewPathology);
            this.splitContainer1.Size = new System.Drawing.Size(716, 519);
            this.splitContainer1.SplitterDistance = 272;
            this.splitContainer1.TabIndex = 21;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 493);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(135, 493);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(53, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(10, 493);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search Location";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtBoxACRCode
            // 
            this.txtBoxACRCode.Location = new System.Drawing.Point(84, 455);
            this.txtBoxACRCode.Name = "txtBoxACRCode";
            this.txtBoxACRCode.Size = new System.Drawing.Size(174, 20);
            this.txtBoxACRCode.TabIndex = 7;
            // 
            // txtBoxPathology
            // 
            this.txtBoxPathology.Location = new System.Drawing.Point(84, 429);
            this.txtBoxPathology.Name = "txtBoxPathology";
            this.txtBoxPathology.Size = new System.Drawing.Size(174, 20);
            this.txtBoxPathology.TabIndex = 6;
            // 
            // txtBoxAnatomy
            // 
            this.txtBoxAnatomy.Location = new System.Drawing.Point(84, 401);
            this.txtBoxAnatomy.Name = "txtBoxAnatomy";
            this.txtBoxAnatomy.Size = new System.Drawing.Size(174, 20);
            this.txtBoxAnatomy.TabIndex = 5;
            // 
            // lblACRCode
            // 
            this.lblACRCode.AutoSize = true;
            this.lblACRCode.Location = new System.Drawing.Point(7, 456);
            this.lblACRCode.Name = "lblACRCode";
            this.lblACRCode.Size = new System.Drawing.Size(54, 13);
            this.lblACRCode.TabIndex = 4;
            this.lblACRCode.Text = "ACRCode";
            // 
            // lblPothology
            // 
            this.lblPothology.AutoSize = true;
            this.lblPothology.Location = new System.Drawing.Point(7, 429);
            this.lblPothology.Name = "lblPothology";
            this.lblPothology.Size = new System.Drawing.Size(54, 13);
            this.lblPothology.TabIndex = 3;
            this.lblPothology.Text = "Pathology";
            // 
            // lblAnatomy
            // 
            this.lblAnatomy.AutoSize = true;
            this.lblAnatomy.Location = new System.Drawing.Point(7, 401);
            this.lblAnatomy.Name = "lblAnatomy";
            this.lblAnatomy.Size = new System.Drawing.Size(48, 13);
            this.lblAnatomy.TabIndex = 2;
            this.lblAnatomy.Text = "Anatomy";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(7, 374);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(48, 13);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location";
            // 
            // treeViewAnatomy
            // 
            this.treeViewAnatomy.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeViewAnatomy.Location = new System.Drawing.Point(0, 0);
            this.treeViewAnatomy.Name = "treeViewAnatomy";
            this.treeViewAnatomy.Size = new System.Drawing.Size(272, 359);
            this.treeViewAnatomy.TabIndex = 0;
            this.treeViewAnatomy.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAnatomy_AfterSelect);
            // 
            // treeViewPathology
            // 
            this.treeViewPathology.Location = new System.Drawing.Point(0, 0);
            this.treeViewPathology.Name = "treeViewPathology";
            this.treeViewPathology.Size = new System.Drawing.Size(439, 520);
            this.treeViewPathology.TabIndex = 0;
            this.treeViewPathology.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPathology_AfterSelect);
            // 
            // REditBoxResult
            // 
            this.REditBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.REditBoxResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.REditBoxResult.Location = new System.Drawing.Point(12, 12);
            this.REditBoxResult.Name = "REditBoxResult";
            this.REditBoxResult.Size = new System.Drawing.Size(716, 93);
            this.REditBoxResult.TabIndex = 20;
            this.REditBoxResult.Text = "";
            // 
            // ACRCodeDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(739, 642);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.REditBoxResult);
            this.Name = "ACRCodeDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACRCodeDlg";
            this.Load += new System.EventHandler(this.ACRCodeDlg_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox REditBoxResult;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TreeView treeViewAnatomy;
        private System.Windows.Forms.TreeView treeViewPathology;
        private System.Windows.Forms.TextBox txtBoxACRCode;
        private System.Windows.Forms.TextBox txtBoxPathology;
        private System.Windows.Forms.TextBox txtBoxAnatomy;
        private System.Windows.Forms.Label lblACRCode;
        private System.Windows.Forms.Label lblPothology;
        private System.Windows.Forms.Label lblAnatomy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSearch;
    }
}