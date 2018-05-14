namespace HYS.MessageDevices.HL7Adapter.HL7Test
{
    partial class FormXMLTOXML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXMLTOXML));
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSourceXml = new System.Windows.Forms.TextBox();
            this.checkBoxHL7Msg = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxXmlMsg = new System.Windows.Forms.TextBox();
            this.checkBoxXmlMsg = new System.Windows.Forms.CheckBox();
            this.buttonTransform = new System.Windows.Forms.Button();
            this.textBoxXSLTPath = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Source XML";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSourceXml
            // 
            this.textBoxSourceXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSourceXml.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSourceXml.Location = new System.Drawing.Point(21, 34);
            this.textBoxSourceXml.Multiline = true;
            this.textBoxSourceXml.Name = "textBoxSourceXml";
            this.textBoxSourceXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSourceXml.Size = new System.Drawing.Size(711, 219);
            this.textBoxSourceXml.TabIndex = 3;
            // 
            // checkBoxHL7Msg
            // 
            this.checkBoxHL7Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHL7Msg.AutoSize = true;
            this.checkBoxHL7Msg.Checked = true;
            this.checkBoxHL7Msg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHL7Msg.Location = new System.Drawing.Point(651, 11);
            this.checkBoxHL7Msg.Name = "checkBoxHL7Msg";
            this.checkBoxHL7Msg.Size = new System.Drawing.Size(81, 17);
            this.checkBoxHL7Msg.TabIndex = 4;
            this.checkBoxHL7Msg.Text = "Word Wrap";
            this.checkBoxHL7Msg.UseVisualStyleBackColor = true;
            this.checkBoxHL7Msg.CheckedChanged += new System.EventHandler(this.checkBoxHL7Msg_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "XDS Message XML:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXmlMsg
            // 
            this.textBoxXmlMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXmlMsg.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXmlMsg.Location = new System.Drawing.Point(21, 377);
            this.textBoxXmlMsg.Multiline = true;
            this.textBoxXmlMsg.Name = "textBoxXmlMsg";
            this.textBoxXmlMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXmlMsg.Size = new System.Drawing.Size(712, 220);
            this.textBoxXmlMsg.TabIndex = 6;
            this.textBoxXmlMsg.Text = resources.GetString("textBoxXmlMsg.Text");
            // 
            // checkBoxXmlMsg
            // 
            this.checkBoxXmlMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXmlMsg.AutoSize = true;
            this.checkBoxXmlMsg.Checked = true;
            this.checkBoxXmlMsg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXmlMsg.Location = new System.Drawing.Point(652, 353);
            this.checkBoxXmlMsg.Name = "checkBoxXmlMsg";
            this.checkBoxXmlMsg.Size = new System.Drawing.Size(81, 17);
            this.checkBoxXmlMsg.TabIndex = 9;
            this.checkBoxXmlMsg.Text = "Word Wrap";
            this.checkBoxXmlMsg.UseVisualStyleBackColor = true;
            this.checkBoxXmlMsg.CheckedChanged += new System.EventHandler(this.checkBoxXmlMsg_CheckedChanged);
            // 
            // buttonTransform
            // 
            this.buttonTransform.Location = new System.Drawing.Point(20, 304);
            this.buttonTransform.Name = "buttonTransform";
            this.buttonTransform.Size = new System.Drawing.Size(712, 33);
            this.buttonTransform.TabIndex = 11;
            this.buttonTransform.Text = "Transform";
            this.buttonTransform.UseVisualStyleBackColor = true;
            this.buttonTransform.Click += new System.EventHandler(this.buttonHL7toXML_Click);
            // 
            // textBoxXSLTPath
            // 
            this.textBoxXSLTPath.Location = new System.Drawing.Point(86, 267);
            this.textBoxXSLTPath.Name = "textBoxXSLTPath";
            this.textBoxXSLTPath.Size = new System.Drawing.Size(598, 20);
            this.textBoxXSLTPath.TabIndex = 12;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(690, 265);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(42, 23);
            this.buttonSelect.TabIndex = 13;
            this.buttonSelect.Text = "...";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "XSLT path:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormXMLTOXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 619);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.textBoxXSLTPath);
            this.Controls.Add(this.buttonTransform);
            this.Controls.Add(this.checkBoxXmlMsg);
            this.Controls.Add(this.textBoxXmlMsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxHL7Msg);
            this.Controls.Add(this.textBoxSourceXml);
            this.Controls.Add(this.label2);
            this.Name = "FormXMLTOXML";
            this.Text = "XML <-> XML Transformation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSourceXml;
        private System.Windows.Forms.CheckBox checkBoxHL7Msg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxXmlMsg;
        private System.Windows.Forms.CheckBox checkBoxXmlMsg;
        private System.Windows.Forms.Button buttonTransform;
        private System.Windows.Forms.TextBox textBoxXSLTPath;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Label label1;
    }
}

