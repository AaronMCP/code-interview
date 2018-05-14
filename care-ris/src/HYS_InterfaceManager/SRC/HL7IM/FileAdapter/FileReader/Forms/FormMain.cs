using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Forms
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
