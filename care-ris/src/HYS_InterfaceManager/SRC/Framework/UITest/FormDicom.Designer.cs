namespace UITest
{
    partial class FormDicom
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
            this.buttonXPath = new System.Windows.Forms.Button();
            this.buttonLoadSchemaFile = new System.Windows.Forms.Button();
            this.buttonLoadSchema = new System.Windows.Forms.Button();
            this.buttonDump = new System.Windows.Forms.Button();
            this.checkBoxAutoExpand = new System.Windows.Forms.CheckBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAddChild = new System.Windows.Forms.Button();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.buttonAddRoot = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // buttonXPath
            // 
            this.buttonXPath.Location = new System.Drawing.Point(292, 29);
            this.buttonXPath.Name = "buttonXPath";
            this.buttonXPath.Size = new System.Drawing.Size(90, 32);
            this.buttonXPath.TabIndex = 23;
            this.buttonXPath.Text = "Get DPath";
            this.buttonXPath.UseVisualStyleBackColor = true;
            // 
            // buttonLoadSchemaFile
            // 
            this.buttonLoadSchemaFile.Location = new System.Drawing.Point(141, 29);
            this.buttonLoadSchemaFile.Name = "buttonLoadSchemaFile";
            this.buttonLoadSchemaFile.Size = new System.Drawing.Size(145, 32);
            this.buttonLoadSchemaFile.TabIndex = 22;
            this.buttonLoadSchemaFile.Text = "Load Schema From File";
            this.buttonLoadSchemaFile.UseVisualStyleBackColor = true;
            // 
            // buttonLoadSchema
            // 
            this.buttonLoadSchema.Location = new System.Drawing.Point(30, 29);
            this.buttonLoadSchema.Name = "buttonLoadSchema";
            this.buttonLoadSchema.Size = new System.Drawing.Size(105, 32);
            this.buttonLoadSchema.TabIndex = 21;
            this.buttonLoadSchema.Text = "Load Schema";
            this.buttonLoadSchema.UseVisualStyleBackColor = true;
            // 
            // buttonDump
            // 
            this.buttonDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDump.Location = new System.Drawing.Point(432, 311);
            this.buttonDump.Name = "buttonDump";
            this.buttonDump.Size = new System.Drawing.Size(80, 32);
            this.buttonDump.TabIndex = 20;
            this.buttonDump.Text = "Dump";
            this.buttonDump.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoExpand
            // 
            this.checkBoxAutoExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoExpand.AutoSize = true;
            this.checkBoxAutoExpand.Location = new System.Drawing.Point(432, 252);
            this.checkBoxAutoExpand.Name = "checkBoxAutoExpand";
            this.checkBoxAutoExpand.Size = new System.Drawing.Size(87, 17);
            this.checkBoxAutoExpand.TabIndex = 19;
            this.checkBoxAutoExpand.Text = "Auto Expand";
            this.checkBoxAutoExpand.UseVisualStyleBackColor = true;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(96, 323);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(286, 20);
            this.textBoxValue.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(27, 326);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Input Box:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 39;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(432, 202);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(80, 32);
            this.buttonClear.TabIndex = 16;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(432, 164);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(80, 32);
            this.buttonRemove.TabIndex = 15;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAddChild
            // 
            this.buttonAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddChild.Location = new System.Drawing.Point(432, 126);
            this.buttonAddChild.Name = "buttonAddChild";
            this.buttonAddChild.Size = new System.Drawing.Size(80, 32);
            this.buttonAddChild.TabIndex = 14;
            this.buttonAddChild.Text = "Add Child";
            this.buttonAddChild.UseVisualStyleBackColor = true;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 211;
            // 
            // buttonAddRoot
            // 
            this.buttonAddRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRoot.Location = new System.Drawing.Point(432, 88);
            this.buttonAddRoot.Name = "buttonAddRoot";
            this.buttonAddRoot.Size = new System.Drawing.Size(80, 32);
            this.buttonAddRoot.TabIndex = 13;
            this.buttonAddRoot.Text = "Add Root";
            this.buttonAddRoot.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(30, 88);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(352, 209);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // FormDicom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 378);
            this.Controls.Add(this.buttonXPath);
            this.Controls.Add(this.buttonLoadSchemaFile);
            this.Controls.Add(this.buttonLoadSchema);
            this.Controls.Add(this.buttonDump);
            this.Controls.Add(this.checkBoxAutoExpand);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAddChild);
            this.Controls.Add(this.buttonAddRoot);
            this.Controls.Add(this.listView1);
            this.Name = "FormDicom";
            this.Text = "FormDicom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonXPath;
        private System.Windows.Forms.Button buttonLoadSchemaFile;
        private System.Windows.Forms.Button buttonLoadSchema;
        private System.Windows.Forms.Button buttonDump;
        private System.Windows.Forms.CheckBox checkBoxAutoExpand;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAddChild;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonAddRoot;
        private System.Windows.Forms.ListView listView1;
    }
}