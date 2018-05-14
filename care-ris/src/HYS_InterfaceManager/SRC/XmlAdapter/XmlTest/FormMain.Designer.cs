namespace XmlTest
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
            this.buttonTreeList = new System.Windows.Forms.Button();
            this.buttonInbound = new System.Windows.Forms.Button();
            this.buttonSocket = new System.Windows.Forms.Button();
            this.buttonCoding = new System.Windows.Forms.Button();
            this.buttonFileDetector = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonXSLT
            // 
            this.buttonXSLT.Location = new System.Drawing.Point(12, 12);
            this.buttonXSLT.Name = "buttonXSLT";
            this.buttonXSLT.Size = new System.Drawing.Size(186, 31);
            this.buttonXSLT.TabIndex = 0;
            this.buttonXSLT.Text = "XSLT Prototype";
            this.buttonXSLT.UseVisualStyleBackColor = true;
            this.buttonXSLT.Click += new System.EventHandler(this.buttonXSLT_Click);
            // 
            // buttonTreeList
            // 
            this.buttonTreeList.Location = new System.Drawing.Point(12, 49);
            this.buttonTreeList.Name = "buttonTreeList";
            this.buttonTreeList.Size = new System.Drawing.Size(186, 31);
            this.buttonTreeList.TabIndex = 1;
            this.buttonTreeList.Text = "Tree List Prototype";
            this.buttonTreeList.UseVisualStyleBackColor = true;
            this.buttonTreeList.Click += new System.EventHandler(this.buttonTreeList_Click);
            // 
            // buttonInbound
            // 
            this.buttonInbound.Location = new System.Drawing.Point(12, 86);
            this.buttonInbound.Name = "buttonInbound";
            this.buttonInbound.Size = new System.Drawing.Size(186, 31);
            this.buttonInbound.TabIndex = 2;
            this.buttonInbound.Text = "In-Out Data Transform";
            this.buttonInbound.UseVisualStyleBackColor = true;
            this.buttonInbound.Click += new System.EventHandler(this.buttonInbound_Click);
            // 
            // buttonSocket
            // 
            this.buttonSocket.Location = new System.Drawing.Point(12, 123);
            this.buttonSocket.Name = "buttonSocket";
            this.buttonSocket.Size = new System.Drawing.Size(186, 31);
            this.buttonSocket.TabIndex = 3;
            this.buttonSocket.Text = "Socket Prototype";
            this.buttonSocket.UseVisualStyleBackColor = true;
            this.buttonSocket.Click += new System.EventHandler(this.buttonSocket_Click);
            // 
            // buttonCoding
            // 
            this.buttonCoding.Location = new System.Drawing.Point(12, 160);
            this.buttonCoding.Name = "buttonCoding";
            this.buttonCoding.Size = new System.Drawing.Size(186, 31);
            this.buttonCoding.TabIndex = 4;
            this.buttonCoding.Text = "UTF-8 Decoding";
            this.buttonCoding.UseVisualStyleBackColor = true;
            this.buttonCoding.Click += new System.EventHandler(this.buttonCoding_Click);
            // 
            // buttonFileDetector
            // 
            this.buttonFileDetector.Location = new System.Drawing.Point(12, 197);
            this.buttonFileDetector.Name = "buttonFileDetector";
            this.buttonFileDetector.Size = new System.Drawing.Size(186, 31);
            this.buttonFileDetector.TabIndex = 5;
            this.buttonFileDetector.Text = "File Detector";
            this.buttonFileDetector.UseVisualStyleBackColor = true;
            this.buttonFileDetector.Click += new System.EventHandler(this.buttonFileDetector_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 242);
            this.Controls.Add(this.buttonFileDetector);
            this.Controls.Add(this.buttonCoding);
            this.Controls.Add(this.buttonSocket);
            this.Controls.Add(this.buttonInbound);
            this.Controls.Add(this.buttonTreeList);
            this.Controls.Add(this.buttonXSLT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonXSLT;
        private System.Windows.Forms.Button buttonTreeList;
        private System.Windows.Forms.Button buttonInbound;
        private System.Windows.Forms.Button buttonSocket;
        private System.Windows.Forms.Button buttonCoding;
        private System.Windows.Forms.Button buttonFileDetector;
    }
}