namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    partial class StatementPage
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
            this.txtSPStatement = new System.Windows.Forms.RichTextBox();
            this.lblIsChanged = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSPStatement
            // 
            this.txtSPStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSPStatement.Location = new System.Drawing.Point(37, 15);
            this.txtSPStatement.Name = "txtSPStatement";
            this.txtSPStatement.Size = new System.Drawing.Size(602, 176);
            this.txtSPStatement.TabIndex = 0;
            this.txtSPStatement.Text = "";
            this.txtSPStatement.TextChanged += new System.EventHandler(this.txtSPStatement_TextChanged);
            // 
            // lblIsChanged
            // 
            this.lblIsChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIsChanged.AutoSize = true;
            this.lblIsChanged.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblIsChanged.ForeColor = System.Drawing.Color.Red;
            this.lblIsChanged.Location = new System.Drawing.Point(469, 0);
            this.lblIsChanged.Name = "lblIsChanged";
            this.lblIsChanged.Size = new System.Drawing.Size(170, 13);
            this.lblIsChanged.TabIndex = 89;
            this.lblIsChanged.Text = "Script has been modified manually!";
            this.lblIsChanged.Visible = false;
            // 
            // StatementPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblIsChanged);
            this.Controls.Add(this.txtSPStatement);
            this.Name = "StatementPage";
            this.Size = new System.Drawing.Size(650, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtSPStatement;
        private System.Windows.Forms.Label lblIsChanged;
    }
}
