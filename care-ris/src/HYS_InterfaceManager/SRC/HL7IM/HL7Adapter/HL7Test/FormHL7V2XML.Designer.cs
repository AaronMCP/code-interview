namespace HYS.MessageDevices.HL7Adapter.HL7Test
{
    partial class FormHL7V2XML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7V2XML));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHL7Msg = new System.Windows.Forms.TextBox();
            this.checkBoxHL7Msg = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxXmlMsg = new System.Windows.Forms.TextBox();
            this.textBoxTransHL7Msg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxXmlMsg = new System.Windows.Forms.CheckBox();
            this.checkBoxTransHL7Msg = new System.Windows.Forms.CheckBox();
            this.buttonHL7toXML = new System.Windows.Forms.Button();
            this.buttonXMLtoHL7 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownTimes = new System.Windows.Forms.NumericUpDown();
            this.labelPerformHL7toXML = new System.Windows.Forms.Label();
            this.labelPerformXMLtoHL7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transformer Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(21, 60);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(125, 21);
            this.comboBoxType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(18, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Original HL7v2 Message:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxHL7Msg
            // 
            this.textBoxHL7Msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHL7Msg.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHL7Msg.Location = new System.Drawing.Point(22, 124);
            this.textBoxHL7Msg.Multiline = true;
            this.textBoxHL7Msg.Name = "textBoxHL7Msg";
            this.textBoxHL7Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxHL7Msg.Size = new System.Drawing.Size(462, 91);
            this.textBoxHL7Msg.TabIndex = 3;
            this.textBoxHL7Msg.Text = resources.GetString("textBoxHL7Msg.Text");
            // 
            // checkBoxHL7Msg
            // 
            this.checkBoxHL7Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHL7Msg.AutoSize = true;
            this.checkBoxHL7Msg.Checked = true;
            this.checkBoxHL7Msg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHL7Msg.Location = new System.Drawing.Point(403, 101);
            this.checkBoxHL7Msg.Name = "checkBoxHL7Msg";
            this.checkBoxHL7Msg.Size = new System.Drawing.Size(81, 17);
            this.checkBoxHL7Msg.TabIndex = 4;
            this.checkBoxHL7Msg.Text = "Word Wrap";
            this.checkBoxHL7Msg.UseVisualStyleBackColor = true;
            this.checkBoxHL7Msg.CheckedChanged += new System.EventHandler(this.checkBoxHL7Msg_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "XML Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXmlMsg
            // 
            this.textBoxXmlMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXmlMsg.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXmlMsg.Location = new System.Drawing.Point(21, 250);
            this.textBoxXmlMsg.Multiline = true;
            this.textBoxXmlMsg.Name = "textBoxXmlMsg";
            this.textBoxXmlMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXmlMsg.Size = new System.Drawing.Size(463, 91);
            this.textBoxXmlMsg.TabIndex = 6;
            this.textBoxXmlMsg.Text = resources.GetString("textBoxXmlMsg.Text");
            // 
            // textBoxTransHL7Msg
            // 
            this.textBoxTransHL7Msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTransHL7Msg.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTransHL7Msg.Location = new System.Drawing.Point(22, 383);
            this.textBoxTransHL7Msg.Multiline = true;
            this.textBoxTransHL7Msg.Name = "textBoxTransHL7Msg";
            this.textBoxTransHL7Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTransHL7Msg.Size = new System.Drawing.Size(463, 91);
            this.textBoxTransHL7Msg.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(19, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Transformed HL7v2 Message:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxXmlMsg
            // 
            this.checkBoxXmlMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXmlMsg.AutoSize = true;
            this.checkBoxXmlMsg.Checked = true;
            this.checkBoxXmlMsg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXmlMsg.Location = new System.Drawing.Point(403, 230);
            this.checkBoxXmlMsg.Name = "checkBoxXmlMsg";
            this.checkBoxXmlMsg.Size = new System.Drawing.Size(81, 17);
            this.checkBoxXmlMsg.TabIndex = 9;
            this.checkBoxXmlMsg.Text = "Word Wrap";
            this.checkBoxXmlMsg.UseVisualStyleBackColor = true;
            this.checkBoxXmlMsg.CheckedChanged += new System.EventHandler(this.checkBoxXmlMsg_CheckedChanged);
            // 
            // checkBoxTransHL7Msg
            // 
            this.checkBoxTransHL7Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTransHL7Msg.AutoSize = true;
            this.checkBoxTransHL7Msg.Checked = true;
            this.checkBoxTransHL7Msg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTransHL7Msg.Location = new System.Drawing.Point(403, 363);
            this.checkBoxTransHL7Msg.Name = "checkBoxTransHL7Msg";
            this.checkBoxTransHL7Msg.Size = new System.Drawing.Size(81, 17);
            this.checkBoxTransHL7Msg.TabIndex = 10;
            this.checkBoxTransHL7Msg.Text = "Word Wrap";
            this.checkBoxTransHL7Msg.UseVisualStyleBackColor = true;
            this.checkBoxTransHL7Msg.CheckedChanged += new System.EventHandler(this.checkBoxTransHL7Msg_CheckedChanged);
            // 
            // buttonHL7toXML
            // 
            this.buttonHL7toXML.Location = new System.Drawing.Point(168, 25);
            this.buttonHL7toXML.Name = "buttonHL7toXML";
            this.buttonHL7toXML.Size = new System.Drawing.Size(94, 33);
            this.buttonHL7toXML.TabIndex = 11;
            this.buttonHL7toXML.Text = "HL7 -> XML";
            this.buttonHL7toXML.UseVisualStyleBackColor = true;
            this.buttonHL7toXML.Click += new System.EventHandler(this.buttonHL7toXML_Click);
            // 
            // buttonXMLtoHL7
            // 
            this.buttonXMLtoHL7.Location = new System.Drawing.Point(279, 25);
            this.buttonXMLtoHL7.Name = "buttonXMLtoHL7";
            this.buttonXMLtoHL7.Size = new System.Drawing.Size(94, 33);
            this.buttonXMLtoHL7.TabIndex = 12;
            this.buttonXMLtoHL7.Text = "XML -> HL7";
            this.buttonXMLtoHL7.UseVisualStyleBackColor = true;
            this.buttonXMLtoHL7.Click += new System.EventHandler(this.buttonXMLtoHL7_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(389, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 15;
            this.label5.Text = "Transform Times:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTimes
            // 
            this.numericUpDownTimes.Location = new System.Drawing.Point(392, 60);
            this.numericUpDownTimes.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTimes.Name = "numericUpDownTimes";
            this.numericUpDownTimes.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownTimes.TabIndex = 16;
            this.numericUpDownTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelPerformHL7toXML
            // 
            this.labelPerformHL7toXML.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPerformHL7toXML.Location = new System.Drawing.Point(168, 61);
            this.labelPerformHL7toXML.Name = "labelPerformHL7toXML";
            this.labelPerformHL7toXML.Size = new System.Drawing.Size(94, 21);
            this.labelPerformHL7toXML.TabIndex = 18;
            this.labelPerformHL7toXML.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPerformXMLtoHL7
            // 
            this.labelPerformXMLtoHL7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPerformXMLtoHL7.Location = new System.Drawing.Point(279, 61);
            this.labelPerformXMLtoHL7.Name = "labelPerformXMLtoHL7";
            this.labelPerformXMLtoHL7.Size = new System.Drawing.Size(94, 21);
            this.labelPerformXMLtoHL7.TabIndex = 19;
            this.labelPerformXMLtoHL7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormHL7V2XML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 495);
            this.Controls.Add(this.labelPerformXMLtoHL7);
            this.Controls.Add(this.labelPerformHL7toXML);
            this.Controls.Add(this.numericUpDownTimes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonXMLtoHL7);
            this.Controls.Add(this.buttonHL7toXML);
            this.Controls.Add(this.checkBoxTransHL7Msg);
            this.Controls.Add(this.checkBoxXmlMsg);
            this.Controls.Add(this.textBoxTransHL7Msg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxXmlMsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxHL7Msg);
            this.Controls.Add(this.textBoxHL7Msg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label1);
            this.Name = "FormHL7V2XML";
            this.Text = "HL7v2 <-> XML Transformation";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxHL7Msg;
        private System.Windows.Forms.CheckBox checkBoxHL7Msg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxXmlMsg;
        private System.Windows.Forms.TextBox textBoxTransHL7Msg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxXmlMsg;
        private System.Windows.Forms.CheckBox checkBoxTransHL7Msg;
        private System.Windows.Forms.Button buttonHL7toXML;
        private System.Windows.Forms.Button buttonXMLtoHL7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownTimes;
        private System.Windows.Forms.Label labelPerformHL7toXML;
        private System.Windows.Forms.Label labelPerformXMLtoHL7;
    }
}

