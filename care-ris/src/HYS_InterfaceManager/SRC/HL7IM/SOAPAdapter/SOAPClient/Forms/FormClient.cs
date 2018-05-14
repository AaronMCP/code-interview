using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler;
using HYS.Common.Xml;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Forms
{
    public partial class FormClient : Form
    {
        private SOAPClientControler _controler;
        private XmlTabControlControler _ctrlSndMsg;
        private XmlTabControlControler _ctrlSndSoap;
        private XmlTabControlControler _ctrlRcvSoap;
        private XmlTabControlControler _ctrlRcvMsg;
        private FileComboBoxControler _ctrlSampleMsg;

        public FormClient()
        {
            InitializeComponent();

            _controler = new SOAPClientControler(Program.Context);

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

            _ctrlSampleMsg = new FileComboBoxControler(this.comboBoxSampleMessage,
                Path.Combine(Application.StartupPath, "SampleMessages"), true);
            _ctrlSampleMsg.ItemSelected += delegate(FileComboBoxItem item)
            {
                _ctrlSndMsg.Open(item.FileContent);
            };
            _ctrlSampleMsg.SelectTheFirstItem();

            LoadSetting();
        }

        private void LoadSetting()
        {
            XMLTransformer.ClearTransformerCache();

            this.textBoxURI.Text = Program.Context.ConfigMgr.Config.SOAPServiceURI;
            this.textBoxAction.Text = Program.Context.ConfigMgr.Config.SOAPAction;
        }
        private void SndTransform()
        {
            string xdsgwMsg = _ctrlSndMsg.GetXmlString();
            string soapMsg = "";

            HYS.IM.Messaging.Objects.Message msg = 
                XObjectManager.CreateObject<HYS.IM.Messaging.Objects.Message>(xdsgwMsg);

            if(msg==null)
            {
                MessageBox.Show(this,
                    "Deserialize XDS Gateway message failed. \r\n" +
                    "Please check whether the XML string conforms to XDS Gateway message schema.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DateTime dtB = DateTime.Now;
            bool res = _controler.GenerateSOAPMessage(msg, out soapMsg);
            DateTime dtE = DateTime.Now;

            TimeSpan ts = dtE.Subtract(dtB);
            this.labelSndTranPerform.Text = ts.TotalMilliseconds.ToString() + " ms";

            if (res)
            {
                // for better display in web browser control
                _ctrlSndSoap.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + soapMsg);
            }
            else
            {
                MessageBox.Show(this,
                    "Transform error. Please see log file for details.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void SndSoapMessage()
        {
            string uri = this.textBoxURI.Text;
            string action = this.textBoxAction.Text;

            string sndMsg = _ctrlSndSoap.GetXmlString();
            string rcvMsg = "";

            DateTime dtB = DateTime.Now;
            SOAPSender s = new SOAPSender(
                Program.Context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath(), 
                Program.Context.Log);
            bool res = s.SendMessage(
                this.textBoxURI.Text.Trim(),    // Program.Context.ConfigMgr.Config.SOAPServiceURI, 
                this.textBoxAction.Text.Trim(), //Program.Context.ConfigMgr.Config.SOAPAction, 
                sndMsg, ref rcvMsg);
            DateTime dtE = DateTime.Now;

            TimeSpan ts = dtE.Subtract(dtB);
            this.labelSndSoapPerform.Text = ts.TotalMilliseconds.ToString() + " ms";

            if (res)
            {
                // for better display in web browser control
                _ctrlRcvSoap.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + rcvMsg);
            }
            else
            {
                MessageBox.Show(this,
                    "Send SOAP message error. Please see log file for details.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void RcvTransform()
        {
            string soapMsg = _ctrlRcvSoap.GetXmlString();
            HYS.IM.Messaging.Objects.Message xdsgwMsg = null;

            DateTime dtB = DateTime.Now;
            bool res = _controler.GenerateXDSGWMessage(soapMsg, out xdsgwMsg);
            DateTime dtE = DateTime.Now;

            TimeSpan ts = dtE.Subtract(dtB);
            this.labelRcvTranPerform.Text = ts.TotalMilliseconds.ToString() + " ms";

            if (res)
            {
                // for better display in web browser control
                _ctrlRcvMsg.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + xdsgwMsg.ToXMLString());
            }
            else
            {
                MessageBox.Show(this,
                    "Transform error. Please see log file for details.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void RunAll(int times)
        {
            if (times < 1) return;

            FileComboBoxItem i = _ctrlSampleMsg.GetSelectedItem();
            if (i == null) return;

            string sndSoap = "";
            string rcvSoap = "";
            HYS.IM.Messaging.Objects.Message rcvMsg = null;
            HYS.IM.Messaging.Objects.Message sndMsg =
                XObjectManager.CreateObject<HYS.IM.Messaging.Objects.Message>(i.FileContent);

            if (sndMsg == null)
            {
                MessageBox.Show(this,
                    "Deserialize XDS Gateway message failed. \r\n" +
                    "Please check whether the XML string conforms to XDS Gateway message schema.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string uri = this.textBoxURI.Text;
            string action = this.textBoxAction.Text;
            SOAPSender s = new SOAPSender(
                Program.Context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath(),
                Program.Context.Log);

            bool res = false;
            List<TimeSpan> tsList = new List<TimeSpan>();
            for (int t = 0; t < times; t++)
            {
                DateTime dtB = DateTime.Now;
                if (_controler.GenerateSOAPMessage(sndMsg, out sndSoap))
                {
                    if (s.SendMessage(
                        this.textBoxURI.Text.Trim(),    // Program.Context.ConfigMgr.Config.SOAPServiceURI, 
                        this.textBoxAction.Text.Trim(), //Program.Context.ConfigMgr.Config.SOAPAction, 
                        sndSoap, ref rcvSoap))
                    {
                        res = _controler.GenerateXDSGWMessage(rcvSoap, out rcvMsg);
                        if (!res) break;
                    }
                    else
                    {
                        res = false;
                        break;
                    }
                }
                else
                {
                    res = false;
                    break;
                }
                DateTime dtE = DateTime.Now;
                TimeSpan ts = dtE.Subtract(dtB);
                tsList.Add(ts);
            }

            if (res)
            {
                double totalMs = 0;
                foreach (TimeSpan ts in tsList) totalMs += ts.TotalMilliseconds;
                this.labelRunPerform.Text = (double)(totalMs / (double)times) + " ms";

                _ctrlSndMsg.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + sndMsg.ToXMLString());
                _ctrlSndSoap.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + sndSoap);
                _ctrlRcvSoap.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + rcvSoap);
                _ctrlRcvMsg.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + rcvMsg.ToXMLString());
            }
            else
            {
                MessageBox.Show(this,
                    "Transform or send message error. Please see log file for details.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            RunAll((int)this.numericUpDownTimes.Value);
        }
        private void linkLabelSndTransform_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SndTransform();
        }
        private void linkLabelSndSoap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SndSoapMessage();
        }
        private void linkLabelRcvTransform_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RcvTransform();
        }
    }
}
