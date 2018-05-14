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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
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
