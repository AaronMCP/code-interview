using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HYS.IM.Forms
{
    public partial class FormRegExpTest : Form
    {
        public FormRegExpTest(string exp, string rep)
        {
            InitializeComponent();

            Expression = exp;
            Replacement = rep;

            this.textBoxRegExpression.Text = Expression;
            this.textBoxReplacement.Text = Replacement;
        }

        public string Expression;
        public string Replacement;
        private void Test()
        {
            string str = this.textBoxSource.Text;
            string exp = this.textBoxRegExpression.Text;
            string rep = this.textBoxReplacement.Text;

            string res = "";

            try
            {
                res = Regex.Replace(str, exp, rep);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, "Test failed.\r\n\r\n" + err.Message, "Test",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.textBoxTarget.Text = res;
            this.textBoxTarget.SelectAll();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Expression = this.textBoxRegExpression.Text;
            Replacement = this.textBoxReplacement.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
}