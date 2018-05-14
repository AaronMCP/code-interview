namespace HYS.DicomAdapter.StorageServer.Forms
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
            this.buttonService = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonMapping = new System.Windows.Forms.Button();
            this.buttonStorage = new System.Windows.Forms.Button();
            this.buttonSCP = new System.Windows.Forms.Button();
            this.buttonSOAPConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonService
            // 
            this.buttonService.Location = new System.Drawing.Point(20, 78);
            this.buttonService.Name = "buttonService";
            this.buttonService.Size = new System.Drawing.Size(122, 33);
            this.buttonService.TabIndex = 14;
            this.buttonService.Text = "SCP Test";
            this.buttonService.UseVisualStyleBackColor = true;
            this.buttonService.Click += new System.EventHandler(this.buttonService_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(394, 78);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 33);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(327, 78);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(61, 33);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(20, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 2);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // buttonMapping
            // 
            this.buttonMapping.Location = new System.Drawing.Point(442, 17);
            this.buttonMapping.Name = "buttonMapping";
            this.buttonMapping.Size = new System.Drawing.Size(147, 33);
            this.buttonMapping.TabIndex = 10;
            this.buttonMapping.Text = "Mapping Setting";
            this.buttonMapping.UseVisualStyleBackColor = true;
            this.buttonMapping.Click += new System.EventHandler(this.buttonMapping_Click);
            // 
            // buttonStorage
            // 
            this.buttonStorage.Location = new System.Drawing.Point(287, 17);
            this.buttonStorage.Name = "buttonStorage";
            this.buttonStorage.Size = new System.Drawing.Size(147, 33);
            this.buttonStorage.TabIndex = 9;
            this.buttonStorage.Text = "Storage Service Setting";
            this.buttonStorage.UseVisualStyleBackColor = true;
            this.buttonStorage.Click += new System.EventHandler(this.buttonStorage_Click);
            // 
            // buttonSCP
            // 
            this.buttonSCP.Location = new System.Drawing.Point(20, 17);
            this.buttonSCP.Name = "buttonSCP";
            this.buttonSCP.Size = new System.Drawing.Size(122, 33);
            this.buttonSCP.TabIndex = 8;
            this.buttonSCP.Text = "SCP Setting";
            this.buttonSCP.UseVisualStyleBackColor = true;
            this.buttonSCP.Click += new System.EventHandler(this.buttonSCP_Click);
            // 
            // buttonSOAPConfig
            // 
            this.buttonSOAPConfig.Location = new System.Drawing.Point(155, 17);
            this.buttonSOAPConfig.Name = "buttonSOAPConfig";
            this.buttonSOAPConfig.Size = new System.Drawing.Size(123, 33);
            this.buttonSOAPConfig.TabIndex = 15;
            this.buttonSOAPConfig.Text = "SOAP Config";
            this.buttonSOAPConfig.UseVisualStyleBackColor = true;
            this.buttonSOAPConfig.Click += new System.EventHandler(this.buttonSOAPConfig_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 129);
            this.Controls.Add(this.buttonSOAPConfig);
            this.Controls.Add(this.buttonService);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonMapping);
            this.Controls.Add(this.buttonStorage);
            this.Controls.Add(this.buttonSCP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Storage Adapter Setting";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonService;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonMapping;
        private System.Windows.Forms.Button buttonStorage;
        private System.Windows.Forms.Button buttonSCP;
        private System.Windows.Forms.Button buttonSOAPConfig;
    }
}

