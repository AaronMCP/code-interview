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
    public partial class FormXSLTFile : Form
    {
        public FormXSLTFile()
        {
            InitializeComponent();
        }

        private void buttonTransform_Click(object sender, EventArgs e)
        {
            string srcFile = this.textBoxSource.Text.Trim();
            string xslFile = this.textBoxXSLT.Text.Trim();
            string tarFile = string.Format("{0}.transformed.xml", srcFile);

            XMLTransformer transformer = XMLTransformer.CreateFromFile(xslFile);
            if (transformer.TransformFile(srcFile, tarFile)) MessageBox.Show("Success");
            else MessageBox.Show(this, XMLTransformer.LastErrorInfor);
        }
    }
}
