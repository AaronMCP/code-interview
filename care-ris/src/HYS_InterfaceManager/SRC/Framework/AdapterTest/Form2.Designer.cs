namespace AdapterTest
{
    partial class Form2
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
            this.buttonReadDog = new System.Windows.Forms.Button();
            this.propertyGridDog = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // buttonReadDog
            // 
            this.buttonReadDog.Location = new System.Drawing.Point(417, 21);
            this.buttonReadDog.Name = "buttonReadDog";
            this.buttonReadDog.Size = new System.Drawing.Size(121, 29);
            this.buttonReadDog.TabIndex = 0;
            this.buttonReadDog.Text = "Read Dog";
            this.buttonReadDog.UseVisualStyleBackColor = true;
            this.buttonReadDog.Click += new System.EventHandler(this.buttonReadDog_Click);
            // 
            // propertyGridDog
            // 
            this.propertyGridDog.HelpVisible = false;
            this.propertyGridDog.Location = new System.Drawing.Point(21, 21);
            this.propertyGridDog.Name = "propertyGridDog";
            this.propertyGridDog.Size = new System.Drawing.Size(381, 119);
            this.propertyGridDog.TabIndex = 1;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 266);
            this.Controls.Add(this.propertyGridDog);
            this.Controls.Add(this.buttonReadDog);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReadDog;
        private System.Windows.Forms.PropertyGrid propertyGridDog;
    }
}