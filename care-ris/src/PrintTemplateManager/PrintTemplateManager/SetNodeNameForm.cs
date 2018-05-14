using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Hys.CommonControls;

namespace Hys.PrintTemplateManager
{
    partial class SetNodeNameForm : CSForm
    {
        public SetNodeNameForm()
        {
            InitializeComponent();
        }
        public string TemplateName
        {
            get
            {
                return txtBoxTemplateName.Text.Trim();
            }
            set
            {
                if (value != null && value != "")
                {
                    txtBoxTemplateName.Text = value;
                }
            }
        }
        private void SetNodeName_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxTemplateName.Text.Trim() != "")
            {
                if (Regex.IsMatch(txtBoxTemplateName.Text, "[,|'\"]"))
                {
                    MessageBox.Show("Invalid charactor(, | ' \")");
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please input template name!");
            }
        }
    }
}