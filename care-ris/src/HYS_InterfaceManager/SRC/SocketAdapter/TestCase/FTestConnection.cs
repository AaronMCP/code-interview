using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.Command;

namespace TestCase
{
    
    public partial class FTestConnection : Form
    {
    
        
        public FTestConnection()
        {
            InitializeComponent();

            _logServer = new Logging( Application.StartupPath + "\\" + "Server.log");
            _logClient = new Logging( Application.StartupPath + "\\" + "Client.log");

        }

        
        Logging _logServer ;
        Logging _logClient;

        ServerSocket _ServerSocket;
        ClientSocket _ClientSocket;

        private void CreateServerSocket()
        {
            string sListenIP = tbListenIP.Text;
            int iListenPort = Convert.ToInt32(tbListentPort.Text);

            _ServerSocket = new ServerSocket(sListenIP, iListenPort, _logServer);
            _ServerSocket.CodePageName = EncodingPage.GetAllCodePages()[cbsCodePage.SelectedIndex].Name;

            _ServerSocket.OnClientDataReceived += OnDataReceived;
        }

        private void CreateClientSocket()
        {
            if (_ClientSocket == null)
            {
                _ClientSocket = new ClientSocket(_logClient);
                _ClientSocket.CodePageName = EncodingPage.GetAllCodePages()[cbcCodePage.SelectedIndex].Name;
            }
        }

        private void FTestConnection_Load(object sender, EventArgs e)
        {           
            //ServerSocket.OnDataReceived+=this.OnDataReceived;    
            InitCodePage();
            
        }

        private void InitCodePage()
        {
            
            this.cbcCodePage.Items.Clear();
            for (int i = 0; i < EncodingPage.GetAllCodePages().Length; i++)
            {
                EncodingInfo ei = EncodingPage.GetAllCodePages()[i];
                cbcCodePage.Items.Add(ei.DisplayName + " (" + ei.CodePage.ToString() + ")");
            }
            cbcCodePage.SelectedIndex = EncodingPage.GetIndex("utf-8");

            this.cbsCodePage.Items.Clear();
            for (int i = 0; i < EncodingPage.GetAllCodePages().Length; i++)
            {
                EncodingInfo ei = EncodingPage.GetAllCodePages()[i];
                cbsCodePage.Items.Add(ei.DisplayName + " (" + ei.CodePage.ToString() + ")");
            }

            cbsCodePage.SelectedIndex = EncodingPage.GetIndex("utf-8");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Start")
                {
                    this.CreateServerSocket();

                    if (!_ServerSocket.Start())
                        throw new Exception("Server Start failure! please refer error log!");

                    button1.Text = "Stop";
                    timer1.Enabled = true;
                }
                else
                {

                    _ServerSocket.Stop();
                    button1.Text = "Start";
                    timer1.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                _logServer.Write(ex);
                MessageBox.Show(ex.Message);
            }
        }


        System.Collections.ArrayList al = new System.Collections.ArrayList();
        string spkg1,spkg2,spkg3,spkg4;
        string cpkg1, cpkg2, cpkg3, cpkg4;
        
        //private void  OnDataReceived(string msg, ref string result)
        //{
        //    if (msg.Length > 0)
        //    {
        //        al.Clear();
        //        //listBox1.Items.Add("Rec:" + msg + DateTime.Now.ToShortTimeString());
        //        //listBox1.Items.Add("Send:" + msg + DateTime.Now.ToShortTimeString());
        //        al.Add("Rec:" + msg + DateTime.Now.ToShortTimeString());
        //        al.Add("Send:" + msg + DateTime.Now.ToShortTimeString());
        //        result = msg;

        //    }
        //}

        CommandSendData svrCommandSendData = new CommandSendData();
        CommandRespSendData svrCommandRespSendData = new CommandRespSendData();
        CommandGetResult svrCommandGetResult = new CommandGetResult();
        CommandRespGetResult svrCommandRespGetResult = new CommandRespGetResult();

        CommandSendData cltCommandSendData = new CommandSendData();
        CommandRespSendData cltCommandRespSendData = new CommandRespSendData();
        CommandGetResult cltCommandGetResult = new CommandGetResult();
        CommandRespGetResult cltCommandRespGetResult = new CommandRespGetResult();

        private string OnDataReceived(string package)
        {
            try
            {
                if (svrCommandSendData.DecodePackage(package))
                {
                    //tbsPackage1.Text = System.Text.Encoding.UTF8.GetString(package);
                    spkg1 = package;

                    _logServer.Write(LogType.Debug, "spkg1=" + spkg1, true);

                    //byte[] respSend = System.Text.Encoding.UTF8.GetBytes(tbsPackage2.Text);
                    //result = new byte[CommandBase.PackageHeadLength + respSend.Length];
                    svrCommandRespSendData.CommandGUID = svrCommandSendData.CommandGUID;
                    PacketHead ph = svrCommandSendData.PacketHead; ;
                    ph.PacketType = CommandRespSendData.PacketType;
                    svrCommandRespSendData.PacketHead = ph;
                    svrCommandRespSendData.SendResult = "1";
                    return svrCommandRespSendData.EncodePackage();
                }

                if (svrCommandGetResult.DecodePackage(package))
                {
                    //tbsPackage4.Text = System.Text.Encoding.UTF8.GetString(package);
                    spkg4 = package;

                    //byte[] respSend = System.Text.Encoding.UTF8.GetBytes(tbsPackage2.Text);
                    //result = new byte[CommandBase.PackageHeadLength + respSend.Length];
                    svrCommandRespGetResult.CommandGUID = svrCommandGetResult.CommandGUID;
                    PacketHead ph = svrCommandGetResult.PacketHead;
                    ph.PacketType = CommandGetResult.PacketType;
                    svrCommandRespGetResult.PacketHead = ph;
                    svrCommandRespGetResult.Result = "3";
                    return svrCommandRespGetResult.EncodePackage();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logServer.Write(ex);
                return null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.CreateClientSocket();

            string sServerIP = tbServerIP.Text;
            int iServerPort = Convert.ToInt32(tbServerPort.Text);
            
            
            _ClientSocket.Connect(sServerIP, iServerPort, _logClient);
            if (_ClientSocket.IsConnected())
            {
                //this.lbcPackage1.Items.Add("Connect 127.0.0.1:5000 Success!");    
                MessageBox.Show("Connect "+sServerIP+":"+iServerPort.ToString()+" Success!");
            }
            else
                //this.lbcPackage1.Items.Add("Connect 127.0.0.1:5000 failure!");
                MessageBox.Show("Connect " + sServerIP + ":" + iServerPort.ToString() + " Failure!");
            _ClientSocket.DisConnect(false);
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.listBox1.Items.Clear();
            //foreach (object item in al)
            //    this.lbsPackage1.Items.Add(item.ToString());
            //al.Clear();
            try
            {
                tbsPackage1.Text = tbsPackage1.Text + spkg1;
                spkg1 = "";

                tbsPackage4.Text = spkg4;

                tbcPackage2.Text = cpkg2;
                tbcPackage3.Text = cpkg3;
            }
            catch (Exception ex)
            {
                _logClient.Write(ex);
            }
        }

        
        private void tbsClear_Click(object sender, EventArgs e)
        {
            tbsPackage1.Text = "";
            tbsPackage4.Text = "";
        }

        private void tbcClear_Click(object sender, EventArgs e)
        {
            tbsPackage2.Text = "";
            tbsPackage3.Text = "";
        }

        private void Send1_Click(object sender, EventArgs e)
        {
            try
            {
                this.CreateClientSocket();

               string sServerIP = tbServerIP.Text;
               int iServerPort = Convert.ToInt32(tbServerPort.Text);

                _ClientSocket.Connect(sServerIP, iServerPort, _logClient);

                if (!_ClientSocket.IsConnected())
                {
                    MessageBox.Show("Cannot connect to server 127.0.0.1:6000");
                    return;
                }

                // Command 1
                             
                
                cltCommandSendData.DecodePackage(tbcPackage1.Text);


                PacketHead ph = cltCommandSendData.PacketHead;
                ph.SourceIP = _ClientSocket.LocalIP;
                ph.SourcePort = _ClientSocket.LocalPort;
                cltCommandSendData.PacketHead = ph;
                string result = _ClientSocket.SendMsg(cltCommandSendData.EncodePackage());                

                if (result == null)
                    MessageBox.Show("Cannot receive any response data!");
                else
                    cpkg2 = result;

                _ClientSocket.DisConnect(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void bSend4_Click(object sender, EventArgs e)
        {
            try
            {
                // Command 4
                this.CreateClientSocket();

                string sServerIP = tbServerIP.Text;
                int iServerPort = Convert.ToInt32(tbServerPort.Text);

                _ClientSocket.Connect(sServerIP, iServerPort, _logClient);
                if (!_ClientSocket.IsConnected())
                {
                    MessageBox.Show("Cannot connect to server 127.0.0.1:6000");
                    return;
                }
                

                cltCommandGetResult.DecodePackage(tbcPackage4.Text);

                PacketHead ph = cltCommandGetResult.PacketHead;
                ph.SourceIP = _ClientSocket.LocalIP;
                ph.SourcePort = _ClientSocket.LocalPort;
                cltCommandGetResult.PacketHead = ph;

                string result = _ClientSocket.SendMsg(cltCommandGetResult.EncodePackage());
                _ClientSocket.DisConnect(false);

                if (result == null)
                    MessageBox.Show("Cannot receive any response data!");
                else
                    cpkg3 = result;

                _ClientSocket.DisConnect(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbcCodePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ServerSocket != null)
                _ServerSocket.CodePageName = EncodingPage.GetAllCodePages()[cbsCodePage.SelectedIndex].Name;
            if (_ClientSocket != null)
                _ClientSocket.CodePageName = EncodingPage.GetAllCodePages()[cbcCodePage.SelectedIndex].Name;
        }
    }
}