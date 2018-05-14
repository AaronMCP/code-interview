namespace HYS.MessageDevices.SOAPAdapter.Test
{
    partial class FormMain
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
            this.buttonXSLT = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonXSLT
            // 
            this.buttonXSLT.Location = new System.Drawing.Point(12, 12);
            this.buttonXSLT.Name = "buttonXSLT";
            this.buttonXSLT.Size = new System.Drawing.Size(148, 31);
            this.buttonXSLT.TabIndex = 2;
            this.buttonXSLT.Text = "XSLT Test";
            this.buttonXSLT.UseVisualStyleBackColor = true;
            this.buttonXSLT.Click += new System.EventHandler(this.buttonXSLT_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "XSLT Test 2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 96);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonXSLT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "SOAP Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonXSLT;
        private System.Windows.Forms.Button button1;
    }
}