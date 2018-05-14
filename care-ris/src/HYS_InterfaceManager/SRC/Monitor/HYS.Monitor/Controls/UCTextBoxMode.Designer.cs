namespace HYS.Adapter.Monitor.Controls
{
    partial class UCTextBoxMode
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
            this.txtStatement = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtStatement
            // 
            this.txtStatement.AcceptsTab = true;
            this.txtStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatement.Location = new System.Drawing.Point(12, 3);
            this.txtStatement.Multiline = true;
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatement.Size = new System.Drawing.Size(496, 297);
            this.txtStatement.TabIndex = 0;
            this.txtStatement.TextChanged += new System.EventHandler(this.txtStatement_TextChanged);
            // 
            // UCTextBoxMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStatement);
            this.Name = "UCTextBoxMode";
            this.Size = new System.Drawing.Size(520, 310);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatement;
    }
}
