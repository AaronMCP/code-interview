using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Common.HL7v2.MLLP;
using System.Threading;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms
{
    public partial class FormMultiThreadClient : Form
    {
        MulitThreadClient _client = new MulitThreadClient();
        public FormMultiThreadClient()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _client.Start(textBoxIP.Text.Trim(),
                (int)numericUpDownPort.Value,
                textBoxSend.Text.Trim(),
                checkBoxDumpResponse.Checked,
                (int)numericUpDownThreadCount.Value, numericUpDownTimerInterval.Value);

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _client.Stop();

            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void FormMultiThreadClient_Load(object sender, EventArgs e)
        {
            textBoxIP.Text = Program.Context.ConfigMgr.Config.SocketConfig.IPAddress;
            numericUpDownPort.Value = Program.Context.ConfigMgr.Config.SocketConfig.Port;

            this.textBoxSend.WordWrap = this.checkBoxSendWrap.Checked;
        }

        private void FormMultiThreadClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.Stop();
        }

        private void checkBoxSendWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSend.WordWrap = this.checkBoxSendWrap.Checked;
        }
    }

    internal class MulitThreadClient
    {
        private string _strIPAddr;
        private int _iPort;
        private int _iMaxThreadCount;
        private string _strSendMsg;
        private int _currentNumber;
        private bool _bDumpResponse;

        private System.Timers.Timer _timer = null;

        public MulitThreadClient()
        {

        }

        public void Start(string strIPAddr, int iPort, string strMsg, bool bDumpResponse, int iMaxThreadCount, Decimal dInterval)
        {
            if (_timer != null)
            {
                return;
            }

            _strIPAddr = strIPAddr;
            _iPort = iPort;
            _iMaxThreadCount = iMaxThreadCount;
            _strSendMsg = strMsg;
            _bDumpResponse = bDumpResponse;

            _timer = new System.Timers.Timer();
            _timer.Interval = (double)dInterval;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();

            WriteStartLog();
        }

        private void WriteStartLog()
        {
            Program.Context.Log.Write("Multi-Thread HL7 Sender is started.");
            Program.Context.Log.Write("-------------------Args---------------------------");
            Program.Context.Log.Write("\tIP:" + _strIPAddr + ",Port:" + _iPort);
            Program.Context.Log.Write("\tMax thread count :" + _iMaxThreadCount + ",Dump Response:" + _bDumpResponse.ToString());
            Program.Context.Log.Write("--------------------------------------------------");
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            while (_currentNumber < _iMaxThreadCount)
            {
                lock (this)
                {
                    _currentNumber++;
                }

                Program.Context.Log.Write(LogType.Information, "Current thread number:" + _currentNumber.ToString());
                Thread t = new Thread(SenderMessage);
                t.Start();
            }
        }

        private void SenderMessage()
        {
            Program.Context.Log.Write(LogType.Information, string.Format("Thread {0} is start...", Thread.CurrentThread.ManagedThreadId));

            string strSend = _strSendMsg;

            strSend = strSend.Replace("\r\n", "\r");

            SocketClient client = new SocketClient();
            SocketClientConfig clientCfg = new SocketClientConfig();
            clientCfg.SendTimeout = Program.Context.ConfigMgr.Config.SocketConfig.SendTimeout;
            clientCfg.ReceiveTimeout = Program.Context.ConfigMgr.Config.SocketConfig.ReceiveTimeout;
            clientCfg.ReceiveResponseBufferSizeKB = Program.Context.ConfigMgr.Config.SocketConfig.ReceiveResponseBufferSizeKB;
            clientCfg.CodePageCode = Program.Context.ConfigMgr.Config.SocketConfig.CodePageCode;
            clientCfg.CodePageName = Program.Context.ConfigMgr.Config.SocketConfig.CodePageName;
            clientCfg.IPAddress = _strIPAddr;
            clientCfg.Port = _iPort;

            //Thread.Sleep(100000);
            DateTime dtBegin = DateTime.Now;
            SocketResult result = client.SendData(clientCfg, strSend);
            DateTime dtEnd = DateTime.Now;

            TimeSpan procTime = dtEnd.Subtract(dtBegin);

            if (result.Type == SocketResultType.Success)
            {
                Program.Context.Log.Write(string.Format("Thread {0} send HL7 message success", Thread.CurrentThread.ManagedThreadId));
                if (_bDumpResponse)
                {
                    Program.Context.Log.Write(LogType.Information, result.ReceivedString);
                }
            }
            else
            {
                Program.Context.Log.Write(string.Format("Thread {0} send HL7 message failed.", Thread.CurrentThread.ManagedThreadId));
            }

            lock (this)
            {
                _currentNumber--;
            }

            Program.Context.Log.Write(LogType.Information, string.Format("Thread {0} is end", Thread.CurrentThread.ManagedThreadId));
            Program.Context.Log.Write(LogType.Information, string.Format("-----------Thread {0} is sumary -----------------", Thread.CurrentThread.ManagedThreadId));
            Program.Context.Log.Write(LogType.Information, "\tStart at " + dtBegin.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Program.Context.Log.Write(LogType.Information, "\tEnd at " + dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            Program.Context.Log.Write(LogType.Information, "\tProcess Time :  " + procTime.TotalMilliseconds);
            Program.Context.Log.Write(LogType.Information, "--------------------------------------------------------");

        }

        public void Stop()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Elapsed -= new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Stop();
            _timer.Close();
            _timer = null;
        }

    }
}
