using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler;
using System.IO;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Forms
{
    public partial class FormServer : Form
    {
        private SOAPReceiver _receiver;
        private SOAPServerControler _controler;

        private XmlTabControlControler _ctrlSndMsg;
        private XmlTabControlControler _ctrlSndSoap;
        private XmlTabControlControler _ctrlRcvSoap;
        private XmlTabControlControler _ctrlRcvMsg;

        public FormServer()
        {
            InitializeComponent();

            _ctrlSndMsg = new XmlTabControlControler(this.tabControlSndMsg,
                this.tabPageSndMsgPlain, this.textBoxSndMsg,
                this.tabPageSndMsgTree, this.webBrowserSndMsg);
            _ctrlSndSoap = new XmlTabControlControler(this.tabControlSndSoap,
                this.tabPageSndSoapPlain, this.textBoxSndSoap,
                this.tabPageSndSoapTree, this.webBrowserSndSoap);
            _ctrlRcvSoap = new XmlTabControlControler(this.tabControlRcvSoap,
                this.tabPageRcvSoapPlain, this.textBoxRcvSoap,
                this.tabPageRcvSoapTree, this.webBrowserRcvSoap);
            _ctrlRcvMsg = new XmlTabControlControler(this.tabControlRcvMsg,
                this.tabPageRcvMsgPlain, this.textBoxRcvMsg,
                this.tabPageRcvMsgTree, this.webBrowserRcvMsg);

            LoadSetting();
        }

        private void LoadSetting()
        {
            //_receiver = new SOAPReceiver(Path.Combine(Application.StartupPath, "HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.exe.config"), Program.Log);
            _receiver = new SOAPReceiver(Program.Context.ConfigMgr.Config, Program.Context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath(), Program.Context.Log, true);
            _receiver.OnMessageReceived += new ReceiveSOAPMessageHandler(SOAPReceiver_OnMessageReceived);
            _controler = new SOAPServerControler(Program.Context);

            XMLTransformer.ClearTransformerCache();

            this.textBoxURI.Text = Program.Context.ConfigMgr.Config.SOAPServiceURI;
        }
        private void StopService()
        {
            if (_receiver.Stop())
            {
                this.textBoxURI.ReadOnly = false;
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;
            }
            else
            {
                MessageBox.Show(this,
                    "Stop SOAP reciever error. Please see log file for details.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void StartService()
        {
            string uri = this.textBoxURI.Text;
            if (_receiver.Start(uri))
            {
                this.textBoxURI.ReadOnly = true;
                this.buttonStart.Enabled = false;
                this.buttonStop.Enabled = true;
            }
            else
            {
                MessageBox.Show(this,
                    "Start SOAP reciever error. Please see log file for details.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void DisplaySession()
        {
            if (this.listViewMessage.SelectedItems.Count < 1) return;
            SOAPReceiverSession session = this.listViewMessage.SelectedItems[0].Tag as SOAPReceiverSession;
            if (session == null) return;

            _ctrlRcvSoap.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + session.IncomingSOAPEnvelope);
            _ctrlRcvMsg.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + session.IncomingMessageXml);
            _ctrlSndMsg.Open((session.OutgoingMessage == null) ? "" : "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + session.OutgoingMessage.ToXMLString());
            _ctrlSndSoap.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + session.OutgoingSOAPEnvelope);
        }
        private void UpdateList(SOAPReceiverSession session)
        {
            ListViewItem item = new ListViewItem(session.SessionID.ToString());
            item.SubItems.Add(session.ThreadID.ToString());
            item.SubItems.Add(session.GetTimeSpanMS());
            item.SubItems.Add(session.IncomingMessageDispatchingKey);
            item.SubItems.Add(session.GetStatusLog());
            item.Tag = session;
            this.listViewMessage.Items.Add(item);
            if (this.listViewMessage.Items.Count == 1) this.listViewMessage.Items[0].Selected = true;
        }
        private delegate void UpdateListHandler(SOAPReceiverSession session);

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopService();
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartService();
        }
        private bool SOAPReceiver_OnMessageReceived(SOAPReceiverSession session)
        {
            if (_controler.ProcessSoapSession(session, MessageDispatchModel.Test, null))
            {
                this.Invoke(
                    new UpdateListHandler(delegate(SOAPReceiverSession s) { UpdateList(s); }), 
                    new object[] { session });
                return true;
            }
            else
            {
                return false;
            }
        }
        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.textBoxURI.ReadOnly) StopService();
        }
        private void listViewMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySession();
        }
    }
}
