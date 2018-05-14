namespace Hys.CareAgent.WcfService
{
    partial class FormDcmInfo
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
            if (m_dcmcontViewer != null)
            {
                m_dcmcontViewer.Dispose();
                m_dcmcontViewer = null;
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
            this.m_dcmcontViewer = new Hys.CareAgent.WcfService.DCMCont();
            this.SuspendLayout();
            // 
            // m_dcmcontViewer
            // 
            this.m_dcmcontViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dcmcontViewer.EMRKeyName = null;
            this.m_dcmcontViewer.Location = new System.Drawing.Point(0, 0);
            this.m_dcmcontViewer.Name = "m_dcmcontViewer";
            this.m_dcmcontViewer.Size = new System.Drawing.Size(714, 638);
            this.m_dcmcontViewer.TabIndex = 0;
            // 
            // FormDcmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 638);
            this.Controls.Add(this.m_dcmcontViewer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDcmInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看DICOM影像";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private DCMCont m_dcmcontViewer;
    }
}