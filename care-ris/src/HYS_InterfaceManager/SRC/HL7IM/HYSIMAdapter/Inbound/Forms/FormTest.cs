using System;
using System.Windows.Forms;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Adapters;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Controler;
using MSG = HYS.IM.Messaging.Objects;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms
{
    public partial class FormTest : Form
    {
        private CSBrokerInboundControler _controler;

        public FormTest()
        {
            InitializeComponent();
            EntityImpl publisher = new EntityImpl();
            publisher.Context.PreLoading();
            publisher.OnMessagePublish += new HYS.IM.Messaging.Objects.PublishModel.MessagePublishHandler(publisher_OnMessagePublish);
            _controler = new CSBrokerInboundControler(publisher);
        }

        bool publisher_OnMessagePublish(HYS.IM.Messaging.Objects.Message message)
        {
            AddMessageToTheList(message);
            return true;
        }

        private delegate void AddMessageToTheListHandler(MSG.Message msg);
        internal void AddMessageToTheList(MSG.Message msg)
        {
            AddMessageToTheListHandler dlg = delegate(MSG.Message m)
            {
                this.listBoxMsg.Items.Add(m);
            };
            this.listBoxMsg.Invoke(dlg, msg);
        }

        private void FormTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controler.StopTimer();
        }
        private void listBoxMsg_DoubleClick(object sender, EventArgs e)
        {
            MSG.Message msg = this.listBoxMsg.SelectedItem as MSG.Message;
            MessageBox.Show(msg.ToXMLString());
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            _controler.StartTimer();

            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            _controler.StopTimer();

            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
        }
    }
}
