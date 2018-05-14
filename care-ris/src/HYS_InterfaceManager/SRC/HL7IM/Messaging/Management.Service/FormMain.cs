using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.Messaging.Management.Service
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private EntityImpl _service = new EntityImpl();

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (_service.Start())
            {
                MessageBox.Show("Start success at: " + Program.ConfigMgt.Config.RemotingUrl);
            }
            else
            {
                MessageBox.Show("Start failed.");
            }
        }
    }
}