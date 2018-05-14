using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler;
using System.IO;
using System.Collections;
using HYS.IM.Common.Logging;
using HYS.IM.Common.HL7v2.Encoding;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Forms
{
    public partial class FormServer : Form
    {
        private IServer _server;

        private void RefreshButtons()
        {
            if (_server != null && _server.IsListening)
            {
                this.numericUpDownPort.Enabled = false;
                this.buttonStart.Enabled = false;
                this.buttonStop.Enabled = true;
            }
            else
            {
                this.numericUpDownPort.Enabled = true;
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;
            }
        }

        private class SampleTemplate
        {
            public string FileName { get; set; }
            public string GetTemplate()
            {
                try
                {
                    return File.ReadAllText(FileName);
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            public SampleTemplate(string filename)
            {
                FileName = filename;
            }
            public override string ToString()
            {
                return Path.GetFileName(FileName);
            }

            public string GetIncomingMessageType()
            {
                string[] slist = Path.GetFileName(FileName).Split('.');
                if (slist.Length > 0) return slist[0];
                return "";
            }
            public string GetOutgoingMessageType()
            {
                string[] slist = Path.GetFileName(FileName).Split('.');
                if (slist.Length > 1) return slist[1];
                return "";
            }
        }

        private List<SampleTemplate> _templateList = new List<SampleTemplate>();

        private void LoadSampleTemplateList()
        {
            try
            {
                this.listBoxTemplate.Items.Clear();
                string path = Path.Combine(Application.StartupPath, "SampleTemplates");
                string[] flist = Directory.GetFiles(path);
                foreach (string fn in flist)
                {
                    SampleTemplate t = new SampleTemplate(fn);
                    this.listBoxTemplate.Items.Add(t);
                    _templateList.Add(t);
                }

                if (this.listBoxTemplate.Items.Count > 0)
                    this.listBoxTemplate.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                Program.Context.Log.Write(e);
            }
        }

        private class MessagePair
        {
            public string IncomingMessageType { get; set; }
            public string OutgoingMessageType { get; set; }

            public string IncomingMessageContent { get; set; }
            public string OutgoingMessageContent { get; set; }

            public SampleTemplate Template { get; set; }

            public TimeSpan ProcessingTime { get; set; }

            private MessagePair()
            {
            }

            public static MessagePair GetMessagePair(string recieveMessage, List<SampleTemplate> templateList)
            {
                DateTime dtBegin = DateTime.Now;

                MessagePair p = new MessagePair();
                p.IncomingMessageContent = recieveMessage;
                p.IncomingMessageType = HL7MessageParser.GetField(recieveMessage, "MSH", 9).Replace("^", "_");

                SampleTemplate matchTemplate = null;
                SampleTemplate commonTemplate = null;
                foreach (SampleTemplate t in templateList)
                {
                    string msgType = t.GetIncomingMessageType();
                    if (msgType == "ANY") commonTemplate = t;

                    if (p.IncomingMessageType == msgType)
                    {
                        matchTemplate = t;
                        break;
                    }
                }

                if (matchTemplate == null)
                    matchTemplate = commonTemplate;

                if (matchTemplate == null)
                {
                    Program.Context.Log.Write(LogType.Error,
                        string.Format("Cannot find match acknowledgment message template for message {0} \r\n{1}",
                        p.IncomingMessageType, p.IncomingMessageContent));
                    p.OutgoingMessageType = "AE";
                    p.OutgoingMessageContent = HL7MessageParser.FormatResponseMessage
                        (p.IncomingMessageContent, HL7MessageTemplates.ErrorResponse);
                }
                else
                {
                    p.Template = matchTemplate;
                    p.OutgoingMessageType = matchTemplate.GetOutgoingMessageType();
                    p.OutgoingMessageContent = HL7MessageParser.FormatResponseMessage
                            (p.IncomingMessageContent, matchTemplate.GetTemplate());
                }

                DateTime dtEnd = DateTime.Now;
                p.ProcessingTime = dtEnd.Subtract(dtBegin);

                return p;
            }
        }

        public FormServer()
        {
            InitializeComponent();

            //this.comboBoxTemplate.SelectedIndex = 1;
            this.textBoxSendTemplate.Text = Program.Context.ConfigMgr.Config.ReadHL7AckAATemplate();
            this.numericUpDownPort.Value = Program.Context.ConfigMgr.Config.SocketConfig.Port;

            LoadSampleTemplateList();
        }

        //private delegate string UpdateGUIHandler(string context);
        private delegate void UpdateGUIHandler2(MessagePair pair);

        private bool _server_OnRequest(string receiveData, ref string sendData)
        {
            //string incomingMessage = receiveData.Replace("\r", "\r\n");

            string incomingMessage = receiveData;
            if (Program.Context.ConfigMgr.Config.MessagePreprocessing.Enable)
                incomingMessage = Program.Context.ConfigMgr.Config.MessagePreprocessing.Replace(incomingMessage);
            incomingMessage = incomingMessage.Replace("\r", "\r\n");

            MessagePair pair = MessagePair.GetMessagePair(incomingMessage, _templateList);
            if (pair == null) return false;

            UpdateGUIHandler2 d = delegate(MessagePair p)
            {
                ListViewItem item = new ListViewItem(p.IncomingMessageType);
                item.SubItems.Add(p.OutgoingMessageType);
                item.SubItems.Add(p.ProcessingTime.TotalMilliseconds.ToString());
                item.Tag = p;

                this.listViewMessage.Items.Add(item);
                if (this.listViewMessage.Items.Count == 1)
                    this.listViewMessage.Items[0].Selected = true;
            };
            this.listViewMessage.Invoke(d, pair);

            string outgoingMessagse = pair.OutgoingMessageContent;
            sendData = outgoingMessagse.Replace("\r\n", "\r");
            return true;

            //UpdateGUIHandler d = delegate(string context)
            //{
            //    this.textBoxReceive.Text = context.Replace("\r", "\r\n");
            //    return this.textBoxSendTemplate.Text.Replace("\r\n", "\r");
            //};

            //string rspMessageTemplate = this.textBoxReceive.Invoke(d, receiveData) as string;
            //sendData = HL7MessageHelper.FormatResponseMessage(receiveData, rspMessageTemplate);
            //return true;
        }

        private void checkBoxReceiveWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxReceive.WordWrap = this.checkBoxReceiveWrap.Checked;
        }

        private void checkBoxSendWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSendTemplate.WordWrap = this.checkBoxSendWrap.Checked;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int port = (int)this.numericUpDownPort.Value;

            //_server = SocketServer.Create(port);
            Program.Context.ConfigMgr.Config.SocketConfig.Port = port;
            _server = SocketServer.Create(Program.Context.ConfigMgr.Config.SocketConfig);
            _server.OnRequest += new RequestEventHandler(_server_OnRequest);
            if (!_server.Start())
            {
                MessageBox.Show(this,
                    SocketLogMgt.LastErrorInfor,
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            RefreshButtons();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (_server == null) return;

            if (!_server.Stop())
            {
                MessageBox.Show(this,
                    SocketLogMgt.LastErrorInfor,
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            RefreshButtons();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string rqMessage = this.textBoxReceive.Text;
            string rspMessageTemplate = this.textBoxSendTemplate.Text;
            string rspMessage = HL7MessageParser.FormatResponseMessage(rqMessage, rspMessageTemplate);

            MessageBox.Show(this,
                rspMessage,
                this.Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_server != null && _server.IsListening)
            {
                buttonStop_Click(sender, e);
            }
        }

        private void listBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SampleTemplate msg = this.listBoxTemplate.SelectedItem as SampleTemplate;
            if (msg != null) this.textBoxSendTemplate.Text = msg.GetTemplate();
        }

        private void listViewMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewMessage.SelectedItems.Count < 1) return;
            MessagePair p = this.listViewMessage.SelectedItems[0].Tag as MessagePair;
            if (p != null)
            {
                this.textBoxReceive.Text = p.IncomingMessageContent;
                this.textBoxSend.Text = p.OutgoingMessageContent;
            }
        }
    }
}
