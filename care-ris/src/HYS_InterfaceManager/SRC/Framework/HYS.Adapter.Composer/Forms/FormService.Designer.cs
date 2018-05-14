namespace HYS.Adapter.Composer.Forms
{
    partial class FormService
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
            this.propertyGridConfig = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelDevice = new System.Windows.Forms.Label();
            this.buttonTestDB = new System.Windows.Forms.Button();
            this.propertyGridGC = new System.Windows.Forms.PropertyGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.propertyGridGCTime = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGridConfig
            // 
            this.propertyGridConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridConfig.Location = new System.Drawing.Point(15, 138);
            this.propertyGridConfig.Name = "propertyGridConfig";
            this.propertyGridConfig.Size = new System.Drawing.Size(679, 109);
            this.propertyGridConfig.TabIndex = 42;
            this.propertyGridConfig.ToolbarVisible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(15, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 2);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 21);
            this.label1.TabIndex = 40;
            this.label1.Text = "Adapter Service Config File content";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(347, 45);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(134, 32);
            this.buttonSave.TabIndex = 39;
            this.buttonSave.Text = "Save/Create Config";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(226, 44);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(100, 33);
            this.buttonLoad.TabIndex = 38;
            this.buttonLoad.Text = "Load Config";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLocation.Location = new System.Drawing.Point(225, 19);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(469, 20);
            this.textBoxLocation.TabIndex = 37;
            // 
            // labelDevice
            // 
            this.labelDevice.Location = new System.Drawing.Point(12, 19);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(182, 21);
            this.labelDevice.TabIndex = 36;
            this.labelDevice.Text = "Adapter Service Config File location:";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonTestDB
            // 
            this.buttonTestDB.Location = new System.Drawing.Point(498, 45);
            this.buttonTestDB.Name = "buttonTestDB";
            this.buttonTestDB.Size = new System.Drawing.Size(196, 32);
            this.buttonTestDB.TabIndex = 43;
            this.buttonTestDB.Text = "Test DB Connection";
            this.buttonTestDB.UseVisualStyleBackColor = true;
            this.buttonTestDB.Click += new System.EventHandler(this.buttonTestDB_Click);
            // 
            // propertyGridGC
            // 
            this.propertyGridGC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridGC.Location = new System.Drawing.Point(15, 285);
            this.propertyGridGC.Name = "propertyGridGC";
            this.propertyGridGC.Size = new System.Drawing.Size(409, 193);
            this.propertyGridGC.TabIndex = 44;
            this.propertyGridGC.ToolbarVisible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(12, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 21);
            this.label2.TabIndex = 45;
            this.label2.Text = "Garbage collection rule";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // propertyGridGCTime
            // 
            this.propertyGridGCTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridGCTime.Location = new System.Drawing.Point(430, 285);
            this.propertyGridGCTime.Name = "propertyGridGCTime";
            this.propertyGridGCTime.Size = new System.Drawing.Size(261, 193);
            this.propertyGridGCTime.TabIndex = 46;
            this.propertyGridGCTime.ToolbarVisible = false;
            // 
            // FormService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 490);
            this.Controls.Add(this.propertyGridGCTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.propertyGridGC);
            this.Controls.Add(this.buttonTestDB);
            this.Controls.Add(this.propertyGridConfig);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.labelDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormService";
            this.Text = "Adapter Service Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.Button buttonTestDB;
        private System.Windows.Forms.PropertyGrid propertyGridGC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid propertyGridGCTime;
    }
}