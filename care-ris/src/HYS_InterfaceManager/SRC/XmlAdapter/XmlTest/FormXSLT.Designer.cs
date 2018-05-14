namespace XmlTest
{
    partial class FormXSLT
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
            this.textBoxInXML = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOutXML = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonInput = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.textBoxGCXml = new System.Windows.Forms.TextBox();
            this.checkBoxGC = new System.Windows.Forms.CheckBox();
            this.checkBoxIn = new System.Windows.Forms.CheckBox();
            this.checkBoxOut = new System.Windows.Forms.CheckBox();
            this.checkBoxHTML = new System.Windows.Forms.CheckBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelIn = new System.Windows.Forms.Panel();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.panelOut = new System.Windows.Forms.Panel();
            this.panelMain.SuspendLayout();
            this.panelIn.SuspendLayout();
            this.panelOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxInXML
            // 
            this.textBoxInXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInXML.Location = new System.Drawing.Point(13, 32);
            this.textBoxInXML.Multiline = true;
            this.textBoxInXML.Name = "textBoxInXML";
            this.textBoxInXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInXML.Size = new System.Drawing.Size(383, 163);
            this.textBoxInXML.TabIndex = 0;
            this.textBoxInXML.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxInXML.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input XML:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ouput XML:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxOutXML
            // 
            this.textBoxOutXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutXML.Location = new System.Drawing.Point(13, 27);
            this.textBoxOutXML.Multiline = true;
            this.textBoxOutXML.Name = "textBoxOutXML";
            this.textBoxOutXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutXML.Size = new System.Drawing.Size(383, 150);
            this.textBoxOutXML.TabIndex = 2;
            this.textBoxOutXML.WordWrap = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(465, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "GC Gateway XML:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonInput
            // 
            this.buttonInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInput.Location = new System.Drawing.Point(430, 98);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(32, 52);
            this.buttonInput.TabIndex = 6;
            this.buttonInput.Text = ">>";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOutput.Location = new System.Drawing.Point(430, 302);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(32, 52);
            this.buttonOutput.TabIndex = 7;
            this.buttonOutput.Text = "<<";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // textBoxGCXml
            // 
            this.textBoxGCXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGCXml.Location = new System.Drawing.Point(468, 44);
            this.textBoxGCXml.Multiline = true;
            this.textBoxGCXml.Name = "textBoxGCXml";
            this.textBoxGCXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxGCXml.Size = new System.Drawing.Size(383, 369);
            this.textBoxGCXml.TabIndex = 8;
            // 
            // checkBoxGC
            // 
            this.checkBoxGC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxGC.AutoSize = true;
            this.checkBoxGC.Location = new System.Drawing.Point(815, 27);
            this.checkBoxGC.Name = "checkBoxGC";
            this.checkBoxGC.Size = new System.Drawing.Size(36, 17);
            this.checkBoxGC.TabIndex = 10;
            this.checkBoxGC.Text = "IE";
            this.checkBoxGC.UseVisualStyleBackColor = true;
            // 
            // checkBoxIn
            // 
            this.checkBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIn.AutoSize = true;
            this.checkBoxIn.Location = new System.Drawing.Point(360, 15);
            this.checkBoxIn.Name = "checkBoxIn";
            this.checkBoxIn.Size = new System.Drawing.Size(36, 17);
            this.checkBoxIn.TabIndex = 11;
            this.checkBoxIn.Text = "IE";
            this.checkBoxIn.UseVisualStyleBackColor = true;
            // 
            // checkBoxOut
            // 
            this.checkBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOut.AutoSize = true;
            this.checkBoxOut.Location = new System.Drawing.Point(360, 9);
            this.checkBoxOut.Name = "checkBoxOut";
            this.checkBoxOut.Size = new System.Drawing.Size(36, 17);
            this.checkBoxOut.TabIndex = 12;
            this.checkBoxOut.Text = "IE";
            this.checkBoxOut.UseVisualStyleBackColor = true;
            // 
            // checkBoxHTML
            // 
            this.checkBoxHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHTML.AutoSize = true;
            this.checkBoxHTML.Location = new System.Drawing.Point(753, 27);
            this.checkBoxHTML.Name = "checkBoxHTML";
            this.checkBoxHTML.Size = new System.Drawing.Size(56, 17);
            this.checkBoxHTML.TabIndex = 13;
            this.checkBoxHTML.Text = "HTML";
            this.checkBoxHTML.UseVisualStyleBackColor = true;
            this.checkBoxHTML.CheckedChanged += new System.EventHandler(this.checkBoxHTML_CheckedChanged);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.panelIn);
            this.panelMain.Controls.Add(this.splitterMain);
            this.panelMain.Controls.Add(this.panelOut);
            this.panelMain.Location = new System.Drawing.Point(12, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(412, 415);
            this.panelMain.TabIndex = 14;
            // 
            // panelIn
            // 
            this.panelIn.Controls.Add(this.textBoxInXML);
            this.panelIn.Controls.Add(this.label1);
            this.panelIn.Controls.Add(this.checkBoxIn);
            this.panelIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIn.Location = new System.Drawing.Point(0, 0);
            this.panelIn.Name = "panelIn";
            this.panelIn.Size = new System.Drawing.Size(412, 219);
            this.panelIn.TabIndex = 16;
            // 
            // splitterMain
            // 
            this.splitterMain.BackColor = System.Drawing.SystemColors.HighlightText;
            this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterMain.Location = new System.Drawing.Point(0, 219);
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(412, 3);
            this.splitterMain.TabIndex = 15;
            this.splitterMain.TabStop = false;
            // 
            // panelOut
            // 
            this.panelOut.Controls.Add(this.textBoxOutXML);
            this.panelOut.Controls.Add(this.checkBoxOut);
            this.panelOut.Controls.Add(this.label2);
            this.panelOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOut.Location = new System.Drawing.Point(0, 222);
            this.panelOut.Name = "panelOut";
            this.panelOut.Size = new System.Drawing.Size(412, 193);
            this.panelOut.TabIndex = 14;
            // 
            // FormXSLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 439);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.checkBoxHTML);
            this.Controls.Add(this.checkBoxGC);
            this.Controls.Add(this.textBoxGCXml);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonInput);
            this.Controls.Add(this.label3);
            this.Name = "FormXSLT";
            this.Text = "XSLT Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMain.ResumeLayout(false);
            this.panelIn.ResumeLayout(false);
            this.panelIn.PerformLayout();
            this.panelOut.ResumeLayout(false);
            this.panelOut.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInXML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOutXML;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.TextBox textBoxGCXml;
        private System.Windows.Forms.CheckBox checkBoxGC;
        private System.Windows.Forms.CheckBox checkBoxIn;
        private System.Windows.Forms.CheckBox checkBoxOut;
        private System.Windows.Forms.CheckBox checkBoxHTML;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelOut;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.Panel panelIn;
    }
}

