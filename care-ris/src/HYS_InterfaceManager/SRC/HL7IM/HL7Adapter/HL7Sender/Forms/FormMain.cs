using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms
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
            FormClient frm = new FormClient();
            frm.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormMultiThreadClient frm = new FormMultiThreadClient();
            frm.ShowDialog(this);
        }
    }
}
