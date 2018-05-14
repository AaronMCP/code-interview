using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.SOAPAdapter.Test.Forms;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            FormSOAPClient frm = new FormSOAPClient();
            frm.ShowDialog();
        }

        private void buttonServer_Click(object sender, EventArgs e)
        {
            FormSOAPServer frm = new FormSOAPServer();
            frm.ShowDialog();
        }

        private void buttonXSLT_Click(object sender, EventArgs e)
        {
            FormXSLT frm = new FormXSLT();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormXSLTFile frm = new FormXSLTFile();
            frm.ShowDialog();
        }
    }
}
