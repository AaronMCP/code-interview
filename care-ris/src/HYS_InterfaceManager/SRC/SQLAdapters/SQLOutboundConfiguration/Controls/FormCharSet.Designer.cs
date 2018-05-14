namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    partial class FormCharSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCharSet));
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxCharSet = new System.Windows.Forms.ListBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // listBoxCharSet
            // 
            this.listBoxCharSet.FormattingEnabled = true;
            this.listBoxCharSet.Location = new System.Drawing.Point(21, 77);
            this.listBoxCharSet.Name = "listBoxCharSet";
            this.listBoxCharSet.Size = new System.Drawing.Size(298, 173);
            this.listBoxCharSet.TabIndex = 2;
            this.listBoxCharSet.DoubleClick += new System.EventHandler(this.listBoxCharSet_DoubleClick);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(21, 262);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(298, 30);
            this.buttonCopy.TabIndex = 3;
            this.buttonCopy.Text = "Copy Code Page Number To Clipboard and Close Dialog";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // FormCharSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 304);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.listBoxCharSet);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCharSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Character Set Reference";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxCharSet;
        private System.Windows.Forms.Button buttonCopy;
    }
}