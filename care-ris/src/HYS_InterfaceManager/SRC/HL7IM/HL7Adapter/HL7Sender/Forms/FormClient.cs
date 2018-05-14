using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.Common.HL7v2.Encoding;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();

            this.textBoxIP.Text = Program.Context.ConfigMgr.Config.SocketConfig.IPAddress;
            this.numericUpDownPort.Value = Program.Context.ConfigMgr.Config.SocketConfig.Port;

            LoadSampleMessageList();
        }

        private class SampleMessage
        {
            public string FileName { get; set; }
            public string GetMessage()
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
            public SampleMessage(string filename)
            {
                FileName = filename;
            }
            public override string ToString()
            {
                return Path.GetFileName(FileName);
            }
        }

        private void LoadSampleMessageList()
        {
            try
            {
                this.listBoxSample.Items.Clear();
                string path = Path.Combine(Program.Context.AppArgument.ConfigFilePath, "SampleMessages");
                string[] flist = Directory.GetFiles(path);
                foreach (string fn in flist)
                {
                    this.listBoxSample.Items.Add(new SampleMessage(fn));
                }
            }
            catch (Exception e)
            {
                Program.Context.Log.Write(e);
            }
        }

        private delegate void UpdateGUIHandler(string content);

        private void checkBoxSendWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSend.WordWrap = this.checkBoxSendWrap.Checked;
        }

        private void checkBoxReceiveWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxReceive.WordWrap = this.checkBoxReceiveWrap.Checked;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string strIP = this.textBoxIP.Text;
            int numPort = (int)this.numericUpDownPort.Value;
            string strSend = this.textBoxSend.Text;
            string strReceive = string.Empty;

            if (this.checkBoxReplace0D.Checked)
            {
                strSend = strSend.Replace("\r\n", "\r");    // replace ODOA with OD
            }

            SocketClient client = new SocketClient();
            SocketClientConfig clientCfg = new SocketClientConfig();
            clientCfg.SendTimeout = Program.Context.ConfigMgr.Config.SocketConfig.SendTimeout;
            clientCfg.ReceiveTimeout = Program.Context.ConfigMgr.Config.SocketConfig.ReceiveTimeout;
            clientCfg.ReceiveResponseBufferSizeKB = Program.Context.ConfigMgr.Config.SocketConfig.ReceiveResponseBufferSizeKB;
            clientCfg.CodePageCode = Program.Context.ConfigMgr.Config.SocketConfig.CodePageCode;
            clientCfg.CodePageName = Program.Context.ConfigMgr.Config.SocketConfig.CodePageName;
            clientCfg.IPAddress = strIP;
            clientCfg.Port = numPort;

            DateTime dtBegin = DateTime.Now;
            SocketResult result = client.SendData(clientCfg, strSend);
            DateTime dtEnd = DateTime.Now;

            TimeSpan procTime = dtEnd.Subtract(dtBegin);
            this.labelProcessingTime.Text = string.Format("in {0} ms.", procTime.TotalMilliseconds.ToString());

            if (result.Type == SocketResultType.Success)
            {
                UpdateGUIHandler d = delegate(string content)
                {
                    this.textBoxReceive.Text = content.Replace("\r", "\r\n");
                };
                this.textBoxReceive.Invoke(d, result.ReceivedString);
            }
            else
            {
                MessageBox.Show(this,
                        string.Format("Result: {0}\r\n\r\nInformation:{1}",
                            result.Type.ToString(),
                            result.ExceptionInfor),
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void listBoxSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            SampleMessage msg = this.listBoxSample.SelectedItem as SampleMessage;
            if (msg != null) this.textBoxSend.Text = msg.GetMessage();
        }

        private void buttonGetValue_Click(object sender, EventArgs e)
        {
            string hl7msg = this.textBoxSend.Text;
            string segment = this.textBoxSegment.Text;
            int field = (int)this.numericUpDownField.Value;
            string value = HL7MessageParser.GetField(hl7msg, segment, field);
            //string value = HL7MessageParser.GetSegment(hl7msg, segment);
            this.textBoxValue.Text = value;

            MessageBox.Show(this,
                value,
                this.Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void buttonSetValue_Click(object sender, EventArgs e)
        {
            string oldhl7msg = this.textBoxSend.Text;
            string segment = this.textBoxSegment.Text;
            int field = (int)this.numericUpDownField.Value;
            string value = this.textBoxValue.Text;
            string newhl7msg = HL7MessageParser.SetField(oldhl7msg, segment, field, value);
            //string newhl7msg = HL7MessageParser.SetSegment(oldhl7msg, segment, value);

            MessageBox.Show(this,
                newhl7msg,
                this.Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void buttonSendBatch_Click(object sender, EventArgs e)
        {
            FormBatch frm = new FormBatch(
                Path.Combine(Program.Context.AppArgument.ConfigFilePath, "SampleMessages"),
                this.textBoxIP.Text.Trim(),
                (int)this.numericUpDownPort.Value);
            frm.ShowDialog(this);
        }
    }
}
