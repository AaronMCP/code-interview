namespace Hys.PrintTemplateManager
{
    public partial class SetNodeNameForm
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
            this.txtBoxTemplateName = new Hys.CommonControls.CSTextBox();
            this.lblTemplateName = new Hys.CommonControls.CSLabel();
            this.btnOK = new Hys.CommonControls.CSButton();
            this.btnCancel = new Hys.CommonControls.CSButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxTemplateName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTemplateName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBoxTemplateName
            // 
            this.txtBoxTemplateName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxTemplateName.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F);
            this.txtBoxTemplateName.Location = new System.Drawing.Point(12, 40);
            this.txtBoxTemplateName.MaxLength = 128;
            this.txtBoxTemplateName.Name = "txtBoxTemplateName";
            this.txtBoxTemplateName.Size = new System.Drawing.Size(285, 21);
            this.txtBoxTemplateName.TabIndex = 0;
            this.txtBoxTemplateName.TabStop = false;
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.Location = new System.Drawing.Point(12, 16);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(75, 18);
            this.lblTemplateName.TabIndex = 1;
            this.lblTemplateName.Text = "NodeName";
            this.lblTemplateName.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(147, 72);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // SetNodeNameForm
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(309, 112);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTemplateName);
            this.Controls.Add(this.txtBoxTemplateName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SetNodeNameForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputName";
            this.Load += new System.EventHandler(this.SetNodeName_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxTemplateName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTemplateName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hys.CommonControls.CSTextBox txtBoxTemplateName;
        private Hys.CommonControls.CSLabel lblTemplateName;
        private Hys.CommonControls.CSButton btnOK;
        private Hys.CommonControls.CSButton btnCancel;
    }
}