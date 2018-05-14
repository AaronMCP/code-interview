using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    public partial class FormXSLT : Form
    {
        public FormXSLT()
        {
            InitializeComponent();
        }

        private void buttonTransform_Click(object sender, EventArgs e)
        {
            string source = this.textBoxSource.Text.Trim();
            string xslt = this.textBoxXSLT.Text.Trim();
            string target = "";

            XMLTransformer transformer = XMLTransformer.CreateFromString(xslt);

            DateTime dtBegin = DateTime.Now;
            bool res = (transformer != null && transformer.TransformString(source, ref target));
            DateTime dtEnd = DateTime.Now;
            TimeSpan sp = dtEnd.Subtract(dtBegin);
            this.labelPerform.Text = sp.TotalMilliseconds.ToString() + "ms";

            if (res)
            {
                this.textBoxTarget.Text = target;
            }
            else
            {
                MessageBox.Show(this, XMLTransformer.LastErrorInfor);
            }
        }

        private void checkBoxSource_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSource.WordWrap = this.checkBoxSource.Checked;
        }

        private void checkBoxXSLT_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSource.WordWrap = this.checkBoxXSLT.Checked;
        }

        private void checkBoxTarget_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxTarget.WordWrap = this.checkBoxTarget.Checked;
        }
    }
}
