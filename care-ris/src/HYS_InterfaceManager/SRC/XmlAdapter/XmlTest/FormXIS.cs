using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.XmlAdapter.Common;
using HYS.XmlAdapter.Common.Net;

namespace XmlTest
{
    public partial class FormXIS : Form
    {
        public FormXIS()
        {
            InitializeComponent();
        }

        private SocketEntity _server;

        private delegate void ServerTextHandler(string receiveData);
        private void _ServerReceiveText(string receiveData)
        {
            this.textBoxServerReceive.Text = receiveData;
            //return this.textBoxServerSend.Text;
        }
        public void ServerReceiveText(string receiveData)
        {
            ServerTextHandler dlg = new ServerTextHandler(_ServerReceiveText);
            this.Invoke(dlg, new object[] { receiveData });
            
            //object obj = this.Invoke(dlg, new object[] { receiveData });
            //if( obj != null ) sendData = obj.ToString();
            //sendData = "aaa";
        }

        private bool _server_OnRequest(string receiveData, ref string sendData)
        {
            receiveData = SocketHelper.DeleteEOF(receiveData, _server.Config.ReceiveEndSign);
            //this.textBoxServerReceive.Text = receiveData;
            //ServerReceiveText(receiveData);
            //ServerReceiveText(receiveData, ref sendData);
            //sendData = receiveData + "aaa";
            sendData = receiveData.Replace('#','$');
            return true;
        }

        private delegate void ExceptionLogHandler(Exception err);
        private void _SetExceptionLog(Exception err)
        {
            int index = this.listBoxLog.Items.Add(err);
            this.listBoxLog.SelectedIndex = index;
        }
        public void SetExceptionLog(Exception err)
        {
            ExceptionLogHandler dlg = new ExceptionLogHandler(_SetExceptionLog);
            this.Invoke(dlg, new object[] { err });
        }

        private void SocketEntity_OnError(object sender, EventArgs e)
        {
            SetExceptionLog(SocketLogMgt.LastError);
        }

        private void listBoxLog_DoubleClick(object sender, EventArgs e)
        {
            Exception err = this.listBoxLog.SelectedItem as Exception;
            if (err != null) MessageBox.Show(err.ToString());
        }

        private void buttonInboundSend_Click(object sender, EventArgs e)
        {
            string sendData = this.textBoxClientSend.Text;
            //SocketResult result = SocketHelper.SendData("127.0.0.1", (int)this.numericUpDownClientPort.Value, sendData);
            
            SocketConfig config = new SocketConfig();
            config.Port = (int)this.numericUpDownClientPort.Value;
            config.IPAddress = "127.0.0.1";
            config.SendEndSign = "</XMLRequestMessage>";
            config.ReceiveEndSign = "</XMLResponseMessage>";
            config.IncludeHeader = false;

            SocketResult result = SocketHelper.SendData(config, sendData);
            
            if (result.Type == SocketResultType.Success)
            {
                this.textBoxClientReceive.Text = result.ReceivedString;
            }
            else
            {
                //MessageBox.Show(result.ExceptionInfor, result.Type.ToString());
            }
            Application.DoEvents();
        }

        private void buttonServerStart_Click(object sender, EventArgs e)
        {
            //_server = SocketEntity.Create((int)this.numericUpDownServerPort.Value);

            SocketConfig config = new SocketConfig();
            config.Port = (int)this.numericUpDownServerPort.Value;
            config.ReceiveEndSign = "</XMLRequestMessage>";
            config.SendEndSign = "</XMLResponseMessage>";

            _server = SocketEntity.Create(config);
            if (_server == null) return;

            _server.OnRequest += new RequestEventHandler(_server_OnRequest);
            if (_server == null)
            {
                MessageBox.Show(SocketLogMgt.LastErrorInfor);
            }
            else if (_server.Start())
            {
                this.numericUpDownServerPort.Enabled = false;
            }
            else
            {
                MessageBox.Show(SocketLogMgt.LastErrorInfor);
            }
        }

        private void buttonServerStop_Click(object sender, EventArgs e)
        {
            if (_server == null || !_server.IsListening) return;
            if (_server.Stop())
            {
                this.numericUpDownServerPort.Enabled = true;
            }
            else
            {
                MessageBox.Show(SocketLogMgt.LastErrorInfor);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.listBoxLog.Items.Clear();
        }

        private void FormXIS_Load(object sender, EventArgs e)
        {
            SocketConfig config = new SocketConfig();
            this.numericUpDownClientPort.Value = this.numericUpDownServerPort.Value = config.Port;

            SocketLogMgt.OnError += new EventHandler(SocketEntity_OnError);
        }

        private void FormXIS_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonServerStop_Click(sender, EventArgs.Empty);
        }

        private void buttonSendMore_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                buttonInboundSend_Click(sender, e);
            }
        }

        //private string _requestHeader;
        //public string RequestHeader
        //{
        //    get
        //    {
        //        if (_requestHeader == null)
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            sb.AppendLine("POST /HYS-EC HTTP/1.1");
        //            sb.AppendLine("Content-Type: text/xml");
        //            sb.AppendLine("Content-Length: 1024");
        //            sb.AppendLine("SOAPAction: \"\"");
        //            sb.AppendLine("Host: localhost:" + this.numericUpDownClientPort.Value.ToString());
        //            sb.AppendLine("Pragma: no-cache");
        //            sb.AppendLine();

        //            _requestHeader = sb.ToString();
        //        }
        //        return _requestHeader;
        //    }
        //}

        private void buttonSendWithHeader_Click(object sender, EventArgs e)
        {
            SocketConfig config = new SocketConfig();
            config.Port = (int)this.numericUpDownClientPort.Value;
            config.IPAddress = "127.0.0.1";
            config.SendEndSign = "</XMLRequestMessage>";
            config.ReceiveEndSign = "</XMLResponseMessage>";

            string msg = this.textBoxClientSend.Text;
            SocketResult result = SocketHelper.SendData(config, msg);

            if (result.Type == SocketResultType.Success)
            {
                this.textBoxClientReceive.Text = result.ReceivedString;
            }
            else
            {
                //MessageBox.Show(result.ExceptionInfor, result.Type.ToString());
            }
            Application.DoEvents();
        }
    }
}