using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms
{
    public partial class FormBatch : Form
    {
        private readonly string _initialFolder;
        private readonly string _serverIP;
        private readonly int _serverPort;

        public FormBatch(string folder, string ip, int port)
        {
            InitializeComponent();
            LoadClientType();
            
            _initialFolder = folder;
            _serverIP = ip;
            _serverPort = port;

            this.textBoxPath.Text = _initialFolder;
        }

        private void ReloadFiles()
        {
            string folder = this.textBoxPath.Text.Trim();

            this.listViewMessage.Items.Clear();

            if (!Directory.Exists(folder))
            {
                MessageBox.Show(this,
                    "Cannot find the folder containing the HL7 message files.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string[] flist = Directory.GetFiles(folder);

                SortedList<string, string> list = new SortedList<string, string>();
                foreach (string f in flist)
                {
                    string fn = Path.GetFileName(f);
                    list.Add(fn, f);
                }

                foreach (KeyValuePair<string, string> p in list)
                {
                    string fn = p.Key;
                    ListViewItem i = new ListViewItem(fn);
                    i.SubItems.Add("");
                    i.SubItems.Add("");
                    i.SubItems.Add("");
                    i.SubItems.Add("");
                    i.Tag = p.Value;
                    this.listViewMessage.Items.Add(i);
                }
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this,
                    "Error when loading file into the list. \r\n\r\n" + err.Message,
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void BrowseFolder()
        {
            string folder = this.textBoxPath.Text.Trim();
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (Directory.Exists(folder)) dlg.SelectedPath = folder;
            dlg.Description = "Please select a folder containing the HL7 message files for sending.";
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            this.textBoxPath.Text = dlg.SelectedPath;
        }
        private void LoadClientType()
        {
            this.comboBoxType.Items.Clear();
            foreach (string str in SocketClientFactory.SocketClientTypeRegistry) this.comboBoxType.Items.Add(str);
            if (this.comboBoxType.Items.Count > 0) this.comboBoxType.SelectedIndex = 0;
        }

        private IClient _client;
        private void OpenSocket()
        {
            if (_client != null) return;

            SocketClientConfig cfg = new SocketClientConfig();
            cfg.SocketClientType = this.comboBoxType.SelectedItem as string;
            cfg.IPAddress = _serverIP;
            cfg.Port = _serverPort;

            _client = SocketClientFactory.Create(cfg);

            if (_client != null && _client.Open())
            {
                this.buttonOpen.Enabled = false;
                this.buttonSend.Enabled = true;
                this.buttonClose.Enabled = true;
                this.comboBoxType.Enabled = false;
            }
            else
            {
                this.buttonOpen.Enabled = true;
                this.buttonSend.Enabled = false;
                this.buttonClose.Enabled = false;
                this.comboBoxType.Enabled = true;

                MessageBox.Show(this, "Open socket failed. \r\n\r\n"
                    + SocketLogMgt.LastErrorInfor, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool SendMessages(IClient cln)
        {
            if (cln == null) return false;

            if (this.listViewMessage.Items.Count < 1)
            {
                MessageBox.Show(this,
                    "There are no HL7 message files in the list. Please click \"Reload Files\" to load the list first.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            string folder = this.textBoxPath.Text.Trim();
            string ackFolder = Path.Combine(folder, "ack");

            try
            {
                if (!Directory.Exists(ackFolder))
                    Directory.CreateDirectory(ackFolder);

                foreach (ListViewItem i in this.listViewMessage.Items)
                {
                    string fn = i.Text;
                    string f = i.Tag as string;

                    string msg = "";
                    using (StreamReader sr = File.OpenText(f))
                    {
                        msg = sr.ReadToEnd();
                    }
                    msg = msg.Replace("\r\n", "\r");

                    DateTime dtBegin = DateTime.Now;
                    SocketResult result = cln.SendData(msg);
                    DateTime dtEnd = DateTime.Now;

                    if (result.Type == SocketResultType.Success)
                    {
                        string ackfn = fn + ".ack.txt";
                        string ackf = Path.Combine(ackFolder, ackfn);
                        using (StreamWriter sw = File.CreateText(ackf))
                        {
                            sw.Write(result.ReceivedString);
                        }

                        i.SubItems[1].Text = ackfn;
                    }
                    else
                    {
                        i.SubItems[1].Text = result.Type.ToString() + ":" + result.ExceptionInfor;
                        break;
                    }

                    TimeSpan procTime = dtEnd.Subtract(dtBegin);
                    i.SubItems[2].Text = procTime.TotalMilliseconds.ToString();
                    i.SubItems[3].Text = dtBegin.ToString(LogHelper.DateTimeFomat);
                    i.SubItems[4].Text = dtEnd.ToString(LogHelper.DateTimeFomat);

                    Application.DoEvents();
                }

                return true;
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this,
                    "Error when sending HL7 message. \r\n\r\n" + err.Message,
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
        }
        private void CloseSocket()
        {
            if (_client == null) return;

            if (_client.Close())
            {
                this.buttonOpen.Enabled = true;
                this.buttonSend.Enabled = false;
                this.buttonClose.Enabled = false;
                this.comboBoxType.Enabled = true;

                _client = null;
            }
            else
            {
                this.buttonOpen.Enabled = false;
                this.buttonSend.Enabled = true;
                this.buttonClose.Enabled = true;
                this.comboBoxType.Enabled = false;

                MessageBox.Show(this, "Close socket failed. \r\n\r\n"
                    + SocketLogMgt.LastErrorInfor, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void AutoSend()
        {
            decimal count = this.numericUpDownAutoSendTimes.Value;
            if (count < 1) return;

            SocketClientConfig cfg = new SocketClientConfig();
            cfg.SocketClientType = this.comboBoxType.SelectedItem as string;
            cfg.IPAddress = _serverIP;
            cfg.Port = _serverPort;

            IClient cln = SocketClientFactory.Create(cfg);

            for (decimal i = 1; i <= count; i++)
            {
                this.labelTimesCounter.Text = string.Format("{0}/{1}", i, count);

                if (cln == null || !cln.Open())
                {
                    MessageBox.Show(this, "Open socket failed. \r\n\r\n"
                        + SocketLogMgt.LastErrorInfor, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }

                if (!SendMessages(cln)) break;

                if (!cln.Close())
                {
                    MessageBox.Show(this, "Close socket failed. \r\n\r\n"
                        + SocketLogMgt.LastErrorInfor, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }
        private void buttonReload_Click(object sender, EventArgs e)
        {
            ReloadFiles();
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessages(_client);
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenSocket();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            CloseSocket();
        }
        private void buttonAutoSend_Click(object sender, EventArgs e)
        {
            AutoSend();
        }  
        private void FormBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSocket();
        }
    }
}
