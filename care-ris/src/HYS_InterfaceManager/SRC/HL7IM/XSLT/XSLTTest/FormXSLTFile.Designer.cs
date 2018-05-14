namespace HYS.MessageDevices.SOAPAdapter.Test
{
    partial class FormXSLTFile
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
            this.buttonTransform = new System.Windows.Forms.Button();
            this.textBoxXSLT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonTransform
            // 
            this.buttonTransform.Location = new System.Drawing.Point(534, 12);
            this.buttonTransform.Name = "buttonTransform";
            this.buttonTransform.Size = new System.Drawing.Size(85, 46);
            this.buttonTransform.TabIndex = 9;
            this.buttonTransform.Text = "Transform";
            this.buttonTransform.UseVisualStyleBackColor = true;
            this.buttonTransform.Click += new System.EventHandler(this.buttonTransform_Click);
            // 
            // textBoxXSLT
            // 
            this.textBoxXSLT.Location = new System.Drawing.Point(94, 38);
            this.textBoxXSLT.Name = "textBoxXSLT";
            this.textBoxXSLT.Size = new System.Drawing.Size(425, 20);
            this.textBoxXSLT.TabIndex = 8;
            this.textBoxXSLT.Text = "Samples\\main.xsl";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "XSLT File:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(94, 12);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.Size = new System.Drawing.Size(425, 20);
            this.textBoxSource.TabIndex = 6;
            this.textBoxSource.Text = "Samples\\GENERIC_NOTIFICATION_6eeaf439-6261-40f7-844c-c18bca6d5005.xml";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Source File:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormXSLTFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 82);
            this.Controls.Add(this.buttonTransform);
            this.Controls.Add(this.textBoxXSLT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.label1);
            this.Name = "FormXSLTFile";
            this.Text = "FormXSLTFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTransform;
        private System.Windows.Forms.TextBox textBoxXSLT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label label1;
    }
}