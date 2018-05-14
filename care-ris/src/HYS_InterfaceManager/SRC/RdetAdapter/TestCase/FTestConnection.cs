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
using HYS.RdetAdapter.Common;

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

        ServerSocket _ServerRdet;
        ClientSocket _ClientRdet;

        private void FTestConnection_Load(object sender, EventArgs e)
        {           
            //ServerRdet.OnDataReceived+=this.OnDataReceived;            
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (button1.Text == "Start")
                {

                    _ServerRdet = new ServerSocket("127.0.0.1", Convert.ToInt32(tbPort.Text), _logServer);
                    _ServerRdet.OnClientDataReceived += OnDataReceived;
                    if (!_ServerRdet.Start())
                        throw new Exception("Server Start failure! please refer error log!");

                    button1.Text = "Stop";
                    timer1.Enabled = true;
                }
                else
                {

                    _ServerRdet.Stop();
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
        

        CmdReqNewPatient ReqNewPatient = new CmdReqNewPatient();
        CmdRespBase RespBase = new CmdRespBase();
        
        /*
        CommandSendData svrCommandSendData = new CommandSendData();
        CommandRespSendData svrCommandRespSendData = new CommandRespSendData();
        CommandGetResult svrCommandGetResult = new CommandGetResult();
        CommandRespGetResult svrCommandRespGetResult = new CommandRespGetResult();

        CommandSendData cltCommandSendData = new CommandSendData();
        CommandRespSendData cltCommandRespSendData = new CommandRespSendData();
        CommandGetResult cltCommandGetResult = new CommandGetResult();
        CommandRespGetResult cltCommandRespGetResult = new CommandRespGetResult();
        */

        //ClientDataReceivedByRequest(CmdReqBase request, CmdRespBase resp);
        private void OnDataReceived(CmdReqBase request, CmdRespBase resp)
        {
            try
            {
                this.al.Clear();
                al.Add("Command="+request.Command);
                for (int i = 0; i < request.GetParamCount();i++ )
                    al.Add( request.GetParamName(i) + "=" + request.GetParamValue(i));

                resp.ErrorCode = "0";
                if (request.Command == "NewPatient")
                    resp.AddParameter("StudyInstanceUID","1");
                
            }
            catch (Exception ex)
            {
                _logServer.Write(ex);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ClientRdet == null)
                    _ClientRdet = new ClientSocket(_logClient);
                //this.lbcPackage1.Items.Add("Connect 127.0.0.1:5000 ...");
                _ClientRdet.Connect(this.tbServerIP.Text, Convert.ToInt32(this.tbServerPort.Text));
                if (_ClientRdet.IsConnected)
                {
                    //this.lbcPackage1.Items.Add("Connect 127.0.0.1:5000 Success!");    
                    MessageBox.Show("Connect " + this.tbServerIP.Text + ":" + tbPort.Text + " Success!");
                }
                else
                    //this.lbcPackage1.Items.Add("Connect 127.0.0.1:5000 failure!");
                    MessageBox.Show("Connect " + this.tbServerIP.Text + ":"  +tbPort.Text + " Failure!");
            }
            finally
            {
                _ClientRdet.DisConnect(false);
            }
            
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.listBox1.Items.Clear();
            //foreach (object item in al)
            //    this.lbsPackage1.Items.Add(item.ToString());
            //al.Clear();
            try
            {
                if (al.Count > 0)
                {
                    this.slbNewPatient.Items.Clear();
                    for (int i = 0; i < al.Count; i++)
                        slbNewPatient.Items.Add(al[i]);
                }
                al.Clear();
            }
            catch (Exception ex)
            {
                _logClient.Write(ex);
            }
        }

        
        private void tbsClear_Click(object sender, EventArgs e)
        {
           
        }

        private void tbcClear_Click(object sender, EventArgs e)
        {
            this.clbNewPatient.Items.Clear();
            this.clbRespNewPatient.Items.Clear();
        }

        private void Send1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ClientRdet == null)
                    _ClientRdet = new ClientSocket(_logClient);

                CmdReqBase cmd = new CmdReqBase();

                #region NewPatient
                if (this.rbNewPatient.Checked)
                {
                    this.ReqNewPatient.ClearParameters();
                    this.clbNewPatient.Items.Clear();
                    this.clbNewPatient.Items.Add("Command=NewPatient");

                    this.ReqNewPatient.AddParameter("PatientName", "pname1");
                    this.clbNewPatient.Items.Add("PatientName=pname1");

                    this.ReqNewPatient.AddParameter("PatientID", "1");
                    this.clbNewPatient.Items.Add("PatientID=1");

                   cmd = this.ReqNewPatient;
                }
                #endregion

                #region GetScannerStatus
                if (rbGetScannerStatus.Checked)
                {
                    cmd.Command = "GetScannerStatus";
                    this.clbNewPatient.Items.Clear();
                    this.clbNewPatient.Items.Add("Command=GetScannerStatus");
                }
                #endregion

                #region GetLocale
                if (rbGetLocale.Checked)
                {
                    cmd.Command = "GetLocale";
                    this.clbNewPatient.Items.Clear();
                    this.clbNewPatient.Items.Add("Command=GetLocale");
                }
                #endregion

                #region GetBodyParts
                if (rbGetBodyParts.Checked)
                {
                    cmd.Command = "GetBodyParts";
                    this.clbNewPatient.Items.Clear();
                    this.clbNewPatient.Items.Add("Command=GetBodyParts");
                }
                #endregion

                #region GetProjections
                if (rbGetProjections.Checked)
                {
                    cmd.Command = "GetProjections";
                    this.clbNewPatient.Items.Clear();
                    this.clbNewPatient.Items.Add("Command=GetProjections");
                }
                #endregion

                CmdRespBase resp;
                if (_ClientRdet.Connect(tbServerIP.Text, Convert.ToInt32(this.tbServerPort.Text)))
                {
                    resp = _ClientRdet.SendCommand(cmd);
                    _ClientRdet.DisConnect(false);
                }
                else
                {
                    MessageBox.Show("Cannt connect server!");
                    return;
                }


                if (resp == null)
                    MessageBox.Show("Cannot receive any response data!");
                else
                {
                    this.clbRespNewPatient.Items.Clear();
                    this.clbRespNewPatient.Items.Add("ErrorCode=" + resp.ErrorCode);
                    for(int i=0;i<resp.GetParamCount();i++) 
                    {
                        this.clbRespNewPatient.Items.Add( resp.GetParamName(i) + "=" + resp.GetParamValue(i));
                    }
                }                        

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                    
        }

        

        private void bSend4_Click(object sender, EventArgs e)
        {
            // Command 4

            //if (_ClientRdet.IsConnected)
            //{
                
            //    byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(tbcPackage4.Text);
            //    string sLen = bMsg.Length.ToString("D0" + CommandBase.PackageHeadLength);
            //    byte[] bLen = System.Text.Encoding.UTF8.GetBytes(sLen);
            //    byte[] package = new byte[bMsg.Length + CommandBase.PackageHeadLength];
            //    Array.Copy(bLen, package, bLen.Length);
            //    Array.Copy(bMsg, 0, package, bLen.Length, bMsg.Length);

            //    svrCommandGetResult.DecodePackage(package);

            //    byte[] result = _ClientRdet.SendMsg(package);
            //    if (result == null)
            //        MessageBox.Show("Cannot receive any response data!");
            //    else
            //        cpkg3 = System.Text.Encoding.UTF8.GetString(result);

            //}          
        }

        
        
    }
}