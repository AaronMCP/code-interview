using System;
using System.Windows.Forms;
using HYS.DicomAdapter.Common;

namespace HYS.DicomAdapter.StorageServer.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonSCP_Click(object sender, EventArgs e)
        {
            FormSCP frm = new FormSCP(Program.ConfigMgt, Program.Log);
            frm.Text = "Storage SCP Setting";
            frm.ShowDialog(this);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Program.ConfigMgt.Save();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStorage_Click(object sender, EventArgs e)
        {
            FormStorage frm = new FormStorage();
            frm.ShowDialog(this);
        }

        private void buttonMapping_Click(object sender, EventArgs e)
        {
            FormMapping frm = new FormMapping();
            frm.ShowDialog(this);
        }

        private void buttonService_Click(object sender, EventArgs e)
        {
            FormService frm = new FormService();
            frm.ShowDialog(this);
        }

        private void buttonSOAPConfig_Click(object sender, EventArgs e)
        {
            FormSOAPConfig frm = new FormSOAPConfig();
            frm.ShowDialog(this);
        }
    }
}