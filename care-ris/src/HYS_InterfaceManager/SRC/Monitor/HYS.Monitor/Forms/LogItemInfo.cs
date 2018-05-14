using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYS.Adapter.Monitor
{
    public partial class LogItemInfo : Form
    {
        public LogItemInfo()
        {
            InitializeComponent();
            checkBoxWordWrap.Checked = true;
        }

        public void ShowInfo(ListViewItem item) {
            txtDateTime.Text = item.SubItems[1].Text;
            txtSeverity.Text = item.SubItems[2].Text;
            txtAssembly.Text = item.SubItems[3].Text;
            txtModule.Text = item.SubItems[4].Text;
            txtInfo.Text = item.SubItems[5].Text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            txtInfo.WordWrap = checkBoxWordWrap.Checked;
        }
    }
}