using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.Command;


namespace TestCase
{
    public partial class FPressureTest : Form
    {
        public FPressureTest()
        {
            InitializeComponent();
        }


        ServerSocket _ServerSocket = null;
        ClientSocket _ClientSocket = null;

        Logging _ServerLog = new Logging("Server");
        Logging _ClientLog = new Logging("Client");

        private void btServerStart_Click(object sender, EventArgs e)
        {
            if (btServerStart.Text == "ServerStart")
            {
                _ServerSocket = new ServerSocket("", 7000, _ServerLog);
                _ServerSocket.OnClientDataReceived = ClientDataReceivedStrEvent;

                _ServerSocket.Start();
                btServerStart.Text = "ServerEnd";
            }
            else
            {
                _ServerSocket.Stop();
                _ServerSocket = null;
                btServerStart.Text = "ServerStart";
            }
        }

        private void btClientSend_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(this.textBox1.Text);
            for(int i=0;i<count;i++)
            {
                _ClientSocket = new ClientSocket(_ClientLog);
                _ClientSocket.Connect("127.0.0.1", 7000, _ClientLog);
                try
                {
                    string sMsg = "No:" + i.ToString() + "<><PacketType=1%Souce=150.245.176.146|1033%Destination=127.0.0.1|6000><Command type=8%CommandGUID=919c7150002e4294a33bd638951ce1ff><Paramname=AccessionNumber%ParamValue=><Paramname=ModalityName%ParamValue=James><Paramname=OperatorName%ParamValue=><Paramname=PatientID%ParamValue=U100203303><Paramname=Performed_enddt%ParamValue=2005-07-29 10:16:14><Paramname=Performed_startdt%ParamValue=2005-07-29 10:16:14> "; 
                    this.listBox1.Items.Add(sMsg);
                    sMsg = _ClientSocket.SendMsg(sMsg);
                    this.listBox2.Items.Add(sMsg);
                }
                finally
                {
                    _ClientSocket.DisConnect(false);
                    _ClientSocket = null;
                }
            }
        }

        public string ClientDataReceivedStrEvent(string msg)
        {
            return msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
        }


    }
}