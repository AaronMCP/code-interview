namespace HYS.Common.HL7ObjectsTest
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
            this.buttonGenerateXML = new System.Windows.Forms.Button();
            this.tabControlRcvMsg = new System.Windows.Forms.TabControl();
            this.tabPageRcvMsgPlain = new System.Windows.Forms.TabPage();
            this.textBoxRcvMsg = new System.Windows.Forms.TextBox();
            this.tabPageRcvMsgTree = new System.Windows.Forms.TabPage();
            this.webBrowserRcvMsg = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControlRcvMsg.SuspendLayout();
            this.tabPageRcvMsgPlain.SuspendLayout();
            this.tabPageRcvMsgTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenerateXML
            // 
            this.buttonGenerateXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateXML.Location = new System.Drawing.Point(558, 12);
            this.buttonGenerateXML.Name = "buttonGenerateXML";
            this.buttonGenerateXML.Size = new System.Drawing.Size(128, 38);
            this.buttonGenerateXML.TabIndex = 0;
            this.buttonGenerateXML.Text = "Generate HL7 XML";
            this.buttonGenerateXML.UseVisualStyleBackColor = true;
            this.buttonGenerateXML.Click += new System.EventHandler(this.buttonGenerateXML_Click);
            // 
            // tabControlRcvMsg
            // 
            this.tabControlRcvMsg.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlRcvMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRcvMsg.Controls.Add(this.tabPageRcvMsgPlain);
            this.tabControlRcvMsg.Controls.Add(this.tabPageRcvMsgTree);
            this.tabControlRcvMsg.Location = new System.Drawing.Point(12, 12);
            this.tabControlRcvMsg.Multiline = true;
            this.tabControlRcvMsg.Name = "tabControlRcvMsg";
            this.tabControlRcvMsg.SelectedIndex = 0;
            this.tabControlRcvMsg.Size = new System.Drawing.Size(526, 304);
            this.tabControlRcvMsg.TabIndex = 66;
            // 
            // tabPageRcvMsgPlain
            // 
            this.tabPageRcvMsgPlain.Controls.Add(this.textBoxRcvMsg);
            this.tabPageRcvMsgPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageRcvMsgPlain.Name = "tabPageRcvMsgPlain";
            this.tabPageRcvMsgPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRcvMsgPlain.Size = new System.Drawing.Size(499, 296);
            this.tabPageRcvMsgPlain.TabIndex = 0;
            this.tabPageRcvMsgPlain.Text = "Plain Text";
            this.tabPageRcvMsgPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxRcvMsg
            // 
            this.textBoxRcvMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRcvMsg.Location = new System.Drawing.Point(3, 3);
            this.textBoxRcvMsg.Multiline = true;
            this.textBoxRcvMsg.Name = "textBoxRcvMsg";
            this.textBoxRcvMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRcvMsg.Size = new System.Drawing.Size(493, 290);
            this.textBoxRcvMsg.TabIndex = 18;
            // 
            // tabPageRcvMsgTree
            // 
            this.tabPageRcvMsgTree.Controls.Add(this.webBrowserRcvMsg);
            this.tabPageRcvMsgTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageRcvMsgTree.Name = "tabPageRcvMsgTree";
            this.tabPageRcvMsgTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRcvMsgTree.Size = new System.Drawing.Size(382, 234);
            this.tabPageRcvMsgTree.TabIndex = 1;
            this.tabPageRcvMsgTree.Text = "Tree View";
            this.tabPageRcvMsgTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserRcvMsg
            // 
            this.webBrowserRcvMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserRcvMsg.Location = new System.Drawing.Point(3, 3);
            this.webBrowserRcvMsg.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserRcvMsg.Name = "webBrowserRcvMsg";
            this.webBrowserRcvMsg.Size = new System.Drawing.Size(376, 228);
            this.webBrowserRcvMsg.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(558, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 38);
            this.button1.TabIndex = 67;
            this.button1.Text = "Generate HL7 Object";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 337);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControlRcvMsg);
            this.Controls.Add(this.buttonGenerateXML);
            this.Name = "FormMain";
            this.Text = "HL7 Objects Test";
            this.tabControlRcvMsg.ResumeLayout(false);
            this.tabPageRcvMsgPlain.ResumeLayout(false);
            this.tabPageRcvMsgPlain.PerformLayout();
            this.tabPageRcvMsgTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerateXML;
        private System.Windows.Forms.TabControl tabControlRcvMsg;
        private System.Windows.Forms.TabPage tabPageRcvMsgPlain;
        private System.Windows.Forms.TextBox textBoxRcvMsg;
        private System.Windows.Forms.TabPage tabPageRcvMsgTree;
        private System.Windows.Forms.WebBrowser webBrowserRcvMsg;
        private System.Windows.Forms.Button button1;
    }
}

