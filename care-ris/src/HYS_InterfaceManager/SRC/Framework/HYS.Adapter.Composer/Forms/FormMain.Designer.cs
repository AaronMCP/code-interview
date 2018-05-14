namespace HYS.Adapter.Composer.Forms
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDevice = new System.Windows.Forms.TabPage();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.buttonIMParam = new System.Windows.Forms.Button();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.tabPageMonitor = new System.Windows.Forms.TabPage();
            this.buttonChineseCodingTest = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageDevice);
            this.tabControlMain.Controls.Add(this.tabPageScript);
            this.tabControlMain.Controls.Add(this.tabPageService);
            this.tabControlMain.Controls.Add(this.tabPageConfig);
            this.tabControlMain.Controls.Add(this.tabPageMonitor);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(992, 716);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageDevice
            // 
            this.tabPageDevice.Location = new System.Drawing.Point(4, 22);
            this.tabPageDevice.Name = "tabPageDevice";
            this.tabPageDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDevice.Size = new System.Drawing.Size(984, 690);
            this.tabPageDevice.TabIndex = 0;
            this.tabPageDevice.Text = "Device Index File";
            this.tabPageDevice.UseVisualStyleBackColor = true;
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.buttonChineseCodingTest);
            this.tabPageScript.Controls.Add(this.buttonIMParam);
            this.tabPageScript.Location = new System.Drawing.Point(4, 22);
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.Size = new System.Drawing.Size(984, 690);
            this.tabPageScript.TabIndex = 4;
            this.tabPageScript.Text = "Script";
            this.tabPageScript.UseVisualStyleBackColor = true;
            // 
            // buttonIMParam
            // 
            this.buttonIMParam.Location = new System.Drawing.Point(34, 29);
            this.buttonIMParam.Name = "buttonIMParam";
            this.buttonIMParam.Size = new System.Drawing.Size(190, 21);
            this.buttonIMParam.TabIndex = 47;
            this.buttonIMParam.Text = "Get IM Parameters";
            this.buttonIMParam.UseVisualStyleBackColor = true;
            this.buttonIMParam.Click += new System.EventHandler(this.buttonIMParam_Click);
            // 
            // tabPageService
            // 
            this.tabPageService.Location = new System.Drawing.Point(4, 22);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageService.Size = new System.Drawing.Size(984, 690);
            this.tabPageService.TabIndex = 1;
            this.tabPageService.Text = "Windows Service";
            this.tabPageService.UseVisualStyleBackColor = true;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Size = new System.Drawing.Size(984, 690);
            this.tabPageConfig.TabIndex = 2;
            this.tabPageConfig.Text = "Configuration Tool";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageMonitor
            // 
            this.tabPageMonitor.Location = new System.Drawing.Point(4, 22);
            this.tabPageMonitor.Name = "tabPageMonitor";
            this.tabPageMonitor.Size = new System.Drawing.Size(984, 690);
            this.tabPageMonitor.TabIndex = 3;
            this.tabPageMonitor.Text = "Monitor Tool";
            this.tabPageMonitor.UseVisualStyleBackColor = true;
            // 
            // buttonChineseCodingTest
            // 
            this.buttonChineseCodingTest.Location = new System.Drawing.Point(34, 69);
            this.buttonChineseCodingTest.Name = "buttonChineseCodingTest";
            this.buttonChineseCodingTest.Size = new System.Drawing.Size(190, 21);
            this.buttonChineseCodingTest.TabIndex = 48;
            this.buttonChineseCodingTest.Text = "Chinese Coding Test";
            this.buttonChineseCodingTest.UseVisualStyleBackColor = true;
            this.buttonChineseCodingTest.Click += new System.EventHandler(this.buttonChineseCodingTest_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 716);
            this.Controls.Add(this.tabControlMain);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.Text = "GC Gateway Adapter Composer (for internal use only)";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageDevice;
        private System.Windows.Forms.TabPage tabPageService;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageMonitor;
        private System.Windows.Forms.TabPage tabPageScript;
        private System.Windows.Forms.Button buttonIMParam;
        private System.Windows.Forms.Button buttonChineseCodingTest;
    }
}