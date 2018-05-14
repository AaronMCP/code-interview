namespace HYS.DicomAdapter.MWLServer.Forms
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
            this.buttonSCP = new System.Windows.Forms.Button();
            this.buttonQC = new System.Windows.Forms.Button();
            this.buttonQR = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonService = new System.Windows.Forms.Button();
            this.buttonIDGeneration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSCP
            // 
            this.buttonSCP.Location = new System.Drawing.Point(22, 21);
            this.buttonSCP.Name = "buttonSCP";
            this.buttonSCP.Size = new System.Drawing.Size(122, 33);
            this.buttonSCP.TabIndex = 0;
            this.buttonSCP.Text = "SCP Setting";
            this.buttonSCP.UseVisualStyleBackColor = true;
            this.buttonSCP.Click += new System.EventHandler(this.buttonSCP_Click);
            // 
            // buttonQC
            // 
            this.buttonQC.Location = new System.Drawing.Point(164, 21);
            this.buttonQC.Name = "buttonQC";
            this.buttonQC.Size = new System.Drawing.Size(147, 33);
            this.buttonQC.TabIndex = 1;
            this.buttonQC.Text = "Query Criteria Setting";
            this.buttonQC.UseVisualStyleBackColor = true;
            this.buttonQC.Click += new System.EventHandler(this.buttonQC_Click);
            // 
            // buttonQR
            // 
            this.buttonQR.Location = new System.Drawing.Point(329, 21);
            this.buttonQR.Name = "buttonQR";
            this.buttonQR.Size = new System.Drawing.Size(147, 33);
            this.buttonQR.TabIndex = 2;
            this.buttonQR.Text = "Query Result Setting";
            this.buttonQR.UseVisualStyleBackColor = true;
            this.buttonQR.Click += new System.EventHandler(this.buttonQR_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(22, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 2);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(329, 82);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(61, 33);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(396, 82);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 33);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonService
            // 
            this.buttonService.Location = new System.Drawing.Point(22, 82);
            this.buttonService.Name = "buttonService";
            this.buttonService.Size = new System.Drawing.Size(122, 33);
            this.buttonService.TabIndex = 6;
            this.buttonService.Text = "SCP Test";
            this.buttonService.UseVisualStyleBackColor = true;
            this.buttonService.Click += new System.EventHandler(this.buttonService_Click);
            // 
            // buttonIDGeneration
            // 
            this.buttonIDGeneration.Location = new System.Drawing.Point(164, 82);
            this.buttonIDGeneration.Name = "buttonIDGeneration";
            this.buttonIDGeneration.Size = new System.Drawing.Size(147, 33);
            this.buttonIDGeneration.TabIndex = 15;
            this.buttonIDGeneration.Text = "ID Generation Test";
            this.buttonIDGeneration.UseVisualStyleBackColor = true;
            this.buttonIDGeneration.Click += new System.EventHandler(this.buttonIDGeneration_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 136);
            this.Controls.Add(this.buttonIDGeneration);
            this.Controls.Add(this.buttonService);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonQR);
            this.Controls.Add(this.buttonQC);
            this.Controls.Add(this.buttonSCP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Modality Worklist Adapter Setting";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSCP;
        private System.Windows.Forms.Button buttonQC;
        private System.Windows.Forms.Button buttonQR;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonService;
        private System.Windows.Forms.Button buttonIDGeneration;
    }
}