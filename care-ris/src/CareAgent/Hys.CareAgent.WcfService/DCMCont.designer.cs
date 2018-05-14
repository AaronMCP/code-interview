namespace Hys.CareAgent.WcfService
{
    partial class DCMCont
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
                CloseControl();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DCMCont));
            this.DicomViewer = new AxDICOMVIEWERLib.AxDicomViewer();
            this.plPage = new System.Windows.Forms.Panel();
            this.m_panelPageControl = new System.Windows.Forms.Panel();
            this.m_textboxActivePage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_btnFirstPage = new RibbonMenuButton();
            this.m_textboxTotalPages = new System.Windows.Forms.TextBox();
            this.m_btnPrePage = new RibbonMenuButton();
            this.m_btnLastPage = new RibbonMenuButton();
            this.m_btnNextPage = new RibbonMenuButton();
            ((System.ComponentModel.ISupportInitialize)(this.DicomViewer)).BeginInit();
            this.plPage.SuspendLayout();
            this.m_panelPageControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // DicomViewer
            // 
            this.DicomViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DicomViewer.Enabled = true;
            this.DicomViewer.Location = new System.Drawing.Point(0, 0);
            this.DicomViewer.Name = "DicomViewer";
            this.DicomViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("DicomViewer.OcxState")));
            this.DicomViewer.Size = new System.Drawing.Size(485, 494);
            this.DicomViewer.TabIndex = 0;
            // 
            // plPage
            // 
            this.plPage.Controls.Add(this.m_panelPageControl);
            this.plPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plPage.Location = new System.Drawing.Point(0, 0);
            this.plPage.Name = "plPage";
            this.plPage.Size = new System.Drawing.Size(485, 42);
            this.plPage.TabIndex = 1;
            // 
            // m_panelPageControl
            // 
            this.m_panelPageControl.Controls.Add(this.m_textboxActivePage);
            this.m_panelPageControl.Controls.Add(this.label9);
            this.m_panelPageControl.Controls.Add(this.m_btnFirstPage);
            this.m_panelPageControl.Controls.Add(this.m_textboxTotalPages);
            this.m_panelPageControl.Controls.Add(this.m_btnPrePage);
            this.m_panelPageControl.Controls.Add(this.m_btnLastPage);
            this.m_panelPageControl.Controls.Add(this.m_btnNextPage);
            this.m_panelPageControl.Location = new System.Drawing.Point(96, 7);
            this.m_panelPageControl.Name = "m_panelPageControl";
            this.m_panelPageControl.Size = new System.Drawing.Size(226, 28);
            this.m_panelPageControl.TabIndex = 24;
            // 
            // m_textboxActivePage
            // 
            this.m_textboxActivePage.Location = new System.Drawing.Point(62, 4);
            this.m_textboxActivePage.Name = "m_textboxActivePage";
            this.m_textboxActivePage.Size = new System.Drawing.Size(40, 20);
            this.m_textboxActivePage.TabIndex = 22;
            this.m_textboxActivePage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_textboxActivePage_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "/";
            // 
            // m_btnFirstPage
            // 
            this.m_btnFirstPage.Arrow = RibbonMenuButton.e_arrow.None;
            this.m_btnFirstPage.BackColor = System.Drawing.Color.Transparent;
            this.m_btnFirstPage.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.m_btnFirstPage.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.m_btnFirstPage.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.m_btnFirstPage.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.m_btnFirstPage.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btnFirstPage.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.m_btnFirstPage.FadingSpeed = 35;
            this.m_btnFirstPage.FlatAppearance.BorderSize = 0;
            this.m_btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnFirstPage.GroupPos = RibbonMenuButton.e_groupPos.None;
            this.m_btnFirstPage.Image = global::Hys.CareAgent.WcfService.Properties.Resources.FIRST_PAGE;
            this.m_btnFirstPage.ImageLocation = RibbonMenuButton.e_imagelocation.Top;
            this.m_btnFirstPage.ImageOffset = 0;
            this.m_btnFirstPage.IsPressed = false;
            this.m_btnFirstPage.KeepPress = false;
            this.m_btnFirstPage.Location = new System.Drawing.Point(3, 1);
            this.m_btnFirstPage.MaxImageSize = new System.Drawing.Point(0, 0);
            this.m_btnFirstPage.MenuPos = new System.Drawing.Point(0, 0);
            this.m_btnFirstPage.Name = "m_btnFirstPage";
            this.m_btnFirstPage.Radius = 6;
            this.m_btnFirstPage.ShowBase = RibbonMenuButton.e_showbase.No;
            this.m_btnFirstPage.Size = new System.Drawing.Size(25, 25);
            this.m_btnFirstPage.SplitButton = RibbonMenuButton.e_splitbutton.No;
            this.m_btnFirstPage.SplitDistance = 0;
            this.m_btnFirstPage.TabIndex = 15;
            this.m_btnFirstPage.Title = "";
            this.m_btnFirstPage.UseVisualStyleBackColor = true;
            this.m_btnFirstPage.Click += new System.EventHandler(this.m_btnFirstPage_Click);
            // 
            // m_textboxTotalPages
            // 
            this.m_textboxTotalPages.Enabled = false;
            this.m_textboxTotalPages.Location = new System.Drawing.Point(118, 4);
            this.m_textboxTotalPages.MaxLength = 5;
            this.m_textboxTotalPages.Name = "m_textboxTotalPages";
            this.m_textboxTotalPages.Size = new System.Drawing.Size(40, 20);
            this.m_textboxTotalPages.TabIndex = 19;
            // 
            // m_btnPrePage
            // 
            this.m_btnPrePage.Arrow = RibbonMenuButton.e_arrow.None;
            this.m_btnPrePage.BackColor = System.Drawing.Color.Transparent;
            this.m_btnPrePage.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.m_btnPrePage.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.m_btnPrePage.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.m_btnPrePage.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.m_btnPrePage.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btnPrePage.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.m_btnPrePage.FadingSpeed = 35;
            this.m_btnPrePage.FlatAppearance.BorderSize = 0;
            this.m_btnPrePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPrePage.GroupPos = RibbonMenuButton.e_groupPos.None;
            this.m_btnPrePage.Image = global::Hys.CareAgent.WcfService.Properties.Resources.PRE_PAGE;
            this.m_btnPrePage.ImageLocation = RibbonMenuButton.e_imagelocation.Top;
            this.m_btnPrePage.ImageOffset = 0;
            this.m_btnPrePage.IsPressed = false;
            this.m_btnPrePage.KeepPress = false;
            this.m_btnPrePage.Location = new System.Drawing.Point(31, 1);
            this.m_btnPrePage.MaxImageSize = new System.Drawing.Point(0, 0);
            this.m_btnPrePage.MenuPos = new System.Drawing.Point(0, 0);
            this.m_btnPrePage.Name = "m_btnPrePage";
            this.m_btnPrePage.Radius = 6;
            this.m_btnPrePage.ShowBase = RibbonMenuButton.e_showbase.No;
            this.m_btnPrePage.Size = new System.Drawing.Size(25, 25);
            this.m_btnPrePage.SplitButton = RibbonMenuButton.e_splitbutton.No;
            this.m_btnPrePage.SplitDistance = 0;
            this.m_btnPrePage.TabIndex = 16;
            this.m_btnPrePage.Title = "";
            this.m_btnPrePage.UseVisualStyleBackColor = true;
            this.m_btnPrePage.Click += new System.EventHandler(this.m_btnPrePage_Click);
            // 
            // m_btnLastPage
            // 
            this.m_btnLastPage.Arrow = RibbonMenuButton.e_arrow.None;
            this.m_btnLastPage.BackColor = System.Drawing.Color.Transparent;
            this.m_btnLastPage.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.m_btnLastPage.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.m_btnLastPage.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.m_btnLastPage.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.m_btnLastPage.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btnLastPage.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.m_btnLastPage.FadingSpeed = 35;
            this.m_btnLastPage.FlatAppearance.BorderSize = 0;
            this.m_btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnLastPage.GroupPos = RibbonMenuButton.e_groupPos.None;
            this.m_btnLastPage.Image = global::Hys.CareAgent.WcfService.Properties.Resources.LAST_PAGE;
            this.m_btnLastPage.ImageLocation = RibbonMenuButton.e_imagelocation.Top;
            this.m_btnLastPage.ImageOffset = 0;
            this.m_btnLastPage.IsPressed = false;
            this.m_btnLastPage.KeepPress = false;
            this.m_btnLastPage.Location = new System.Drawing.Point(189, 1);
            this.m_btnLastPage.MaxImageSize = new System.Drawing.Point(0, 0);
            this.m_btnLastPage.MenuPos = new System.Drawing.Point(0, 0);
            this.m_btnLastPage.Name = "m_btnLastPage";
            this.m_btnLastPage.Radius = 6;
            this.m_btnLastPage.ShowBase = RibbonMenuButton.e_showbase.No;
            this.m_btnLastPage.Size = new System.Drawing.Size(25, 25);
            this.m_btnLastPage.SplitButton = RibbonMenuButton.e_splitbutton.No;
            this.m_btnLastPage.SplitDistance = 0;
            this.m_btnLastPage.TabIndex = 18;
            this.m_btnLastPage.Title = "";
            this.m_btnLastPage.UseVisualStyleBackColor = true;
            this.m_btnLastPage.Click += new System.EventHandler(this.m_btnLastPage_Click);
            // 
            // m_btnNextPage
            // 
            this.m_btnNextPage.Arrow = RibbonMenuButton.e_arrow.None;
            this.m_btnNextPage.BackColor = System.Drawing.Color.Transparent;
            this.m_btnNextPage.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.m_btnNextPage.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.m_btnNextPage.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.m_btnNextPage.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.m_btnNextPage.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btnNextPage.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.m_btnNextPage.FadingSpeed = 35;
            this.m_btnNextPage.FlatAppearance.BorderSize = 0;
            this.m_btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnNextPage.GroupPos = RibbonMenuButton.e_groupPos.None;
            this.m_btnNextPage.Image = global::Hys.CareAgent.WcfService.Properties.Resources.NEXT_PAGE;
            this.m_btnNextPage.ImageLocation = RibbonMenuButton.e_imagelocation.Top;
            this.m_btnNextPage.ImageOffset = 0;
            this.m_btnNextPage.IsPressed = false;
            this.m_btnNextPage.KeepPress = false;
            this.m_btnNextPage.Location = new System.Drawing.Point(161, 1);
            this.m_btnNextPage.MaxImageSize = new System.Drawing.Point(0, 0);
            this.m_btnNextPage.MenuPos = new System.Drawing.Point(0, 0);
            this.m_btnNextPage.Name = "m_btnNextPage";
            this.m_btnNextPage.Radius = 6;
            this.m_btnNextPage.ShowBase = RibbonMenuButton.e_showbase.No;
            this.m_btnNextPage.Size = new System.Drawing.Size(25, 25);
            this.m_btnNextPage.SplitButton = RibbonMenuButton.e_splitbutton.No;
            this.m_btnNextPage.SplitDistance = 0;
            this.m_btnNextPage.TabIndex = 17;
            this.m_btnNextPage.Title = "";
            this.m_btnNextPage.UseVisualStyleBackColor = true;
            this.m_btnNextPage.Click += new System.EventHandler(this.m_btnNextPage_Click);
            // 
            // DCMCont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            
            this.Controls.Add(this.DicomViewer);
            this.Controls.Add(this.plPage);
            this.Name = "DCMCont";
            this.Size = new System.Drawing.Size(485, 494);
            this.Resize += new System.EventHandler(this.DCMCont_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DicomViewer)).EndInit();
            this.plPage.ResumeLayout(false);
            this.m_panelPageControl.ResumeLayout(false);
            this.m_panelPageControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxDICOMVIEWERLib.AxDicomViewer DicomViewer;
        private System.Windows.Forms.Panel plPage;
        private System.Windows.Forms.Panel m_panelPageControl;
        private System.Windows.Forms.TextBox m_textboxActivePage;
        private System.Windows.Forms.Label label9;
        private RibbonMenuButton m_btnFirstPage;
        private System.Windows.Forms.TextBox m_textboxTotalPages;
        private RibbonMenuButton m_btnPrePage;
        private RibbonMenuButton m_btnLastPage;
        private RibbonMenuButton m_btnNextPage;


    }
}
