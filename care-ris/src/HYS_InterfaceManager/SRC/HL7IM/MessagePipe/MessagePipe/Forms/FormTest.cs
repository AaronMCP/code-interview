using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Controler;
using HYS.MessageDevices.MessagePipe.Base;
using System.IO;
using HYS.Common.Xml;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Forms
{
    public partial class FormTest : Form
    {
        private VirtualLogger _logger;
        private VirtualMessageSender _sender;
        private MessagePipeControler _controler;

        private XmlTabControlControler _ctlRcvIn;
        private XmlTabControlControler _ctlRcvOut;
        private XmlTabControlControler _ctlSndOut;
        private XmlTabControlControler _ctlSndIn;
        private FileComboBoxControler _ctrlSampleMsg;

        private class VirtualMessageSender : ISender
        {
            private FormTest _form;
            public VirtualMessageSender(FormTest frm)
            {
                _form = frm;
            }

            #region ISender Members

            public bool NotifyMessagePublish(HYS.Messaging.Objects.Message message)
            {
                _form._ctlSndOut.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + message.ToXMLString());
                return true;
            }

            public bool NotifyMessageRequest(HYS.Messaging.Objects.Message request, out HYS.Messaging.Objects.Message response)
            {
                _form._ctlSndOut.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + request.ToXMLString());
                response = XObjectManager.CreateObject<HYS.Messaging.Objects.Message>(_form._ctlSndIn.GetXmlString());
                return response != null;
            }

            #endregion
        }

        private class VirtualLogger : ILog
        {
            private FormTest _form;
            public VirtualLogger(FormTest frm)
            {
                _form = frm;
            }

            private void UpdateTestBox(string text)
            {
                _form.textBoxLog.Text += text + "\r\n";
                _form.textBoxLog.SelectionStart = _form.textBoxLog.Text.Length - 1;
                _form.textBoxLog.ScrollToCaret();
            }

            #region ILog Members

            public void Write(string msg)
            {
                Program.Log.Write(msg);
                Write(LogType.Debug, msg);
            }

            public void Write(LogType type, string msg)
            {
                Program.Log.Write(type, msg);
                UpdateTestBox("[" + type.ToString() + "] " + msg);
            }

            public void Write(Exception err)
            {
                Program.Log.Write(err);
                UpdateTestBox(err.ToString());
            }

            public void DumpToFile(string folder, string filename, XObject message)
            {
                Program.Log.DumpToFile(folder, filename, message);
            }

            public bool DumpData
            {
                get { return false; }
            }

            #endregion
        }

        public FormTest()
        {
            InitializeComponent();

            _logger = new VirtualLogger(this);
            _sender = new VirtualMessageSender(this);
            _controler = new MessagePipeControler(_sender, _logger);
            _controler.InitializeChannels();

            _ctlRcvIn = new XmlTabControlControler(tabControlRevIn,
                tabPageRevInPlain, textBoxRevIn, tabPageRevInTree, webBrowserRevIn);
            _ctlRcvOut = new XmlTabControlControler(tabControlRevOut,
                tabPageRevOutPlain, textBoxRevOut, tabPageRevOutTree, webBrowserRevOut);
            _ctlSndOut = new XmlTabControlControler(tabControlSndOut,
                tabPageSndOutPlain, textBoxSndOut, tabPageSndOutTree, webBrowserSndOut);
            _ctlSndIn = new XmlTabControlControler(tabControlSndIn,
               tabPageSndInPlain, textBoxSndIn, tabPageSndInTree, webBrowserSndIn);

            _ctrlSampleMsg = new FileComboBoxControler(this.comboBoxMsg,
                Path.Combine(Application.StartupPath, "SampleMessages"), true);
            _ctrlSampleMsg.ItemSelected += delegate(FileComboBoxItem item)
            {
                _ctlRcvIn.Open(item.FileContent);
            };
            _ctrlSampleMsg.SelectTheFirstItem();
        }

        private void buttonDispatchFromSub_Click(object sender, EventArgs e)
        {
            string msgXml = _ctlRcvIn.GetXmlString();
            HYS.Messaging.Objects.Message msg = XObjectManager.CreateObject<HYS.Messaging.Objects.Message>(msgXml);

            DateTime dtBegin = DateTime.Now;
            bool res = _controler.DispatchSubscriberMessage(msg);
            DateTime dtEnd = DateTime.Now;
            TimeSpan ts = dtEnd.Subtract(dtBegin);
            this.labelReqPerform.Text = ts.TotalMilliseconds.ToString() + "ms";

            MessageBox.Show("result: " + res.ToString());
        }

        private void buttonDispatchFromRsp_Click(object sender, EventArgs e)
        {
            string msgXml = _ctlRcvIn.GetXmlString();
            HYS.Messaging.Objects.Message rsp = null;
            HYS.Messaging.Objects.Message req = XObjectManager.CreateObject<HYS.Messaging.Objects.Message>(msgXml);

            DateTime dtBegin = DateTime.Now;
            bool res = _controler.DispatchResponserMessage(req, out rsp);
            DateTime dtEnd = DateTime.Now;
            TimeSpan ts = dtEnd.Subtract(dtBegin);
            this.labelRspPerform.Text = ts.TotalMilliseconds.ToString() + "ms";

            if(!res) MessageBox.Show("result: " + res.ToString());
            if (rsp != null) _ctlRcvOut.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + rsp.ToXMLString());
        }

        private void FormTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controler.UninitializeChannels();
        }
    }
}

