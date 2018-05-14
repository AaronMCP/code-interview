using System;
using System.Windows.Forms;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms
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
    }
}
