using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Processors.XSLT;

namespace HYS.MessageDevices.MessagePipe.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormConfig frm = new FormConfig();
            frm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormTest frm = new FormTest();
            frm.ShowDialog(this);
        }

        private void btnXSLTTest_Click(object sender, EventArgs e)
        {
            FormXSLTTest frmXSLT = new FormXSLTTest();
            frmXSLT.ShowDialog();
        }
    }
}
