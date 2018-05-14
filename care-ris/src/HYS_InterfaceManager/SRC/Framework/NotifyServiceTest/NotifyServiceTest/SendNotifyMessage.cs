using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;

namespace NotifyServiceTest
{
    public partial class SendNotifyMessage : Form
    {
        public SendNotifyMessage()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            NotifyAdapterServerClient client = new NotifyAdapterServerClient("StatusNotifier");
            client.NotifyStatusChanged(int.Parse(textBoxInterfaceID.Text), int.Parse(textBoxStatus.Text));            
        }
    }
}
