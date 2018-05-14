using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM
{
    public partial class FormDBScript : Form
    {
        public FormDBScript(string script)
        {
            InitializeComponent();
            this.textBox1.Text = _script = script;
        }

        private bool _clear;
        public bool Clear
        {
            get { return _clear; }
        }

        private string _script;
        public string Script
        {
            get { return _script; }
        }

        private string GenerateScript()
        {
            string soucestr= this.textBox1.Text;

            decimal repeat = this.numericUpDownRepeat.Value;
            decimal afrom = this.numericUpDownAFrom.Value;
            decimal bfrom = this.numericUpDownCFrom.Value;
            decimal cfrom = this.numericUpDownCFrom.Value;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.textBoxBegin.Text);
            for (decimal i = 0; i < repeat; i++)
            {
                string str = soucestr;
                if (str.IndexOf("{A}") >= 0) str = str.Replace("{A}", ((int)(afrom++)).ToString(this.textBoxFormatA.Text));
                if (str.IndexOf("{B}") >= 0) str = str.Replace("{B}", ((int)(bfrom++)).ToString(this.textBoxFormatB.Text));
                if (str.IndexOf("{C}") >= 0) str = str.Replace("{C}", ((int)(cfrom++)).ToString(this.textBoxFormatC.Text));
                sb.Append(str);
            }
            sb.AppendLine(this.textBoxEnd.Text);
            return sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _script = GenerateScript();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBoxWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.WordWrap = this.checkBoxWrap.Checked;
        }

        private void checkBoxClear_CheckedChanged(object sender, EventArgs e)
        {
            _clear = this.checkBoxClear.Checked;
        }
    }
}