using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XmlTest
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
            frm.ShowDialog(this);
        }

        private void buttonTreeList_Click(object sender, EventArgs e)
        {
            FormList frm = new FormList();
            frm.ShowDialog(this);
        }

        private void buttonInbound_Click(object sender, EventArgs e)
        {
            FormTransform frm = new FormTransform();
            frm.ShowDialog(this);
        }

        private void buttonSocket_Click(object sender, EventArgs e)
        {
            FormXIS frm = new FormXIS();
            frm.ShowDialog(this);
        }

        private void buttonCoding_Click(object sender, EventArgs e)
        {
            FormCoding frm = new FormCoding();
            frm.ShowDialog(this);
        }

        private void buttonFileDetector_Click(object sender, EventArgs e)
        {
            FormDirectory frm = new FormDirectory();
            frm.ShowDialog(this);
        }
    }
}