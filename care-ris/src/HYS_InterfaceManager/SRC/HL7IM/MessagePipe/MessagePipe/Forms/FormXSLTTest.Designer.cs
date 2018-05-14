namespace HYS.MessageDevices.MessagePipe.Processors.XSLT
{
    partial class FormXSLTTest
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
            this.rtBoxOrig = new System.Windows.Forms.RichTextBox();
            this.btnTransform = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtBoxOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtBoxOrig
            // 
            this.rtBoxOrig.Location = new System.Drawing.Point(12, 42);
            this.rtBoxOrig.Name = "rtBoxOrig";
            this.rtBoxOrig.Size = new System.Drawing.Size(164, 234);
            this.rtBoxOrig.TabIndex = 0;
            this.rtBoxOrig.Text = "";
            // 
            // btnTransform
            // 
            this.btnTransform.Location = new System.Drawing.Point(204, 42);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(75, 23);
            this.btnTransform.TabIndex = 1;
            this.btnTransform.Text = "Transform";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(158, 294);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "D:\\XDSGW_1.1\\RHIS_PIX\\Entities\\SHL7SND_MSGPIPE_NOTIFY\\XSLT\\DICOM2HL7_XSLT\\CFIND2O" +
                "RR.xsl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "XSLT File Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Outputl Message";
            // 
            // rtBoxOutput
            // 
            this.rtBoxOutput.Location = new System.Drawing.Point(343, 42);
            this.rtBoxOutput.Name = "rtBoxOutput";
            this.rtBoxOutput.Size = new System.Drawing.Size(164, 234);
            this.rtBoxOutput.TabIndex = 5;
            this.rtBoxOutput.Text = "";
            // 
            // FormXSLTTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 342);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtBoxOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnTransform);
            this.Controls.Add(this.rtBoxOrig);
            this.Name = "FormXSLTTest";
            this.Text = "FormXSLTTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtBoxOrig;
        private System.Windows.Forms.Button btnTransform;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtBoxOutput;
    }
}