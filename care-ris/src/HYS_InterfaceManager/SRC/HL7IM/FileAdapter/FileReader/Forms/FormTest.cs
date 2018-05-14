using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MSG = HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Controler;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Forms
{
    public partial class FormTest : Form
    {
        private FileReaderControler _controler;

        public FormTest()
        {
            InitializeComponent();
            _controler = new FileReaderControler(new MessagePublisherToGUI(this));
        }

        private class MessagePublisherToGUI : IMessagePublisher
        {
            private FormTest _frm;
            public MessagePublisherToGUI(FormTest frm)
            {
                _frm = frm;
            }

            #region IMessagePublisher Members

            public bool NotifyMessagePublish(MSG.Message message)
            {
                _frm.AddMessageToTheList(message);
                return true;
            }

            public ProgramContext Context
            {
                get
                {
                    return Program.Context;
                }
            }

            #endregion
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
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            _controler.StopTimer();
        }
    }
}
