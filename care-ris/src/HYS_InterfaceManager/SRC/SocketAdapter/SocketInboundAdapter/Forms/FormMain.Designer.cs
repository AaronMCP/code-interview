namespace HYS.SocketAdapter.SocketInboundAdapter
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
            this.button1 = new System.Windows.Forms.Button();
            this.btBuildEmptyConfiguration = new System.Windows.Forms.Button();
            this.btBuildDefaultConfiguration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btBuildEmptyConfiguration
            // 
            this.btBuildEmptyConfiguration.Location = new System.Drawing.Point(203, 261);
            this.btBuildEmptyConfiguration.Name = "btBuildEmptyConfiguration";
            this.btBuildEmptyConfiguration.Size = new System.Drawing.Size(195, 23);
            this.btBuildEmptyConfiguration.TabIndex = 3;
            this.btBuildEmptyConfiguration.Text = "BuildEmptyConfiguration";
            this.btBuildEmptyConfiguration.UseVisualStyleBackColor = true;
            this.btBuildEmptyConfiguration.Click += new System.EventHandler(this.btBuildEmptyConfiguration_Click);
            // 
            // btBuildDefaultConfiguration
            // 
            this.btBuildDefaultConfiguration.Location = new System.Drawing.Point(203, 203);
            this.btBuildDefaultConfiguration.Name = "btBuildDefaultConfiguration";
            this.btBuildDefaultConfiguration.Size = new System.Drawing.Size(195, 23);
            this.btBuildDefaultConfiguration.TabIndex = 2;
            this.btBuildDefaultConfiguration.Text = "BuildDefaultConfiguration";
            this.btBuildDefaultConfiguration.UseVisualStyleBackColor = true;
            this.btBuildDefaultConfiguration.Click += new System.EventHandler(this.btBuildDefaultConfiguration_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 319);
            this.Controls.Add(this.btBuildEmptyConfiguration);
            this.Controls.Add(this.btBuildDefaultConfiguration);
            this.Controls.Add(this.button1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btBuildEmptyConfiguration;
        private System.Windows.Forms.Button btBuildDefaultConfiguration;
    }
}