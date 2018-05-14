using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Net;
using System.Runtime.InteropServices;
using HYS.IM.Common.Logging;
using HYS.IM.IPMonitor.Policies;
using HYS.IM.Messaging.Management.Scripts;

namespace HYS.IM.IPMonitor
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
            this.CenterToParent();
            GetNetworkInfo();
        }

        private void buttonGetAdapters_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBoxMain.Text = "";
                ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");

                //create object query
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled = True");

                //create object searcher
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                //get collection of WMI objects
                ManagementObjectCollection queryCollection = searcher.Get();

                //ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                //ManagementObjectCollection objMOC = objMC.GetInstances();

                //enumerate the collection.
                foreach (ManagementObject m in queryCollection)
                {
                    // access properties of the WMI object
                    //sb.AppendFormat("AdapterType : {0}", m["AdapterType"]);

                    StringBuilder sb = new StringBuilder();
                    //if (!m["Caption"].ToString().Contains("Intel(R) 82579LM Gigabit Network Connection"))
                    //{!m["Caption"].ToString().Contains("Connection")
                    //    continue;
                    //}
                    //if (m["IPAddress"] == null)
                    //{
                    //    continue;
                    //}
                    sb.Append("-------------------------------------------------------------------------------------------\r\n");
                    foreach (var p in m.Properties)
                    {
                        if (p.Name == "Caption")
                        {
                            sb.AppendFormat("{0}: {1}\r\n\r\n", p.Name, p.Value);
                        }
                        else if (p.Name == "IPAddress"
                            || p.Name == "IPSubnet"
                            || p.Name == "DefaultIPGateway"
                            || p.Name == "DNSServerSearchOrder")
                        {
                            string[] arr = (string[])p.Value;
                            if (arr != null)
                            {
                                sb.AppendFormat("{0}: ", p.Name);
                                foreach (string item in arr)
                                {
                                    sb.AppendFormat("{0}\r\n", item);
                                }
                                sb.AppendFormat("\r\n");
                            }
                        }
                    }

                    this.textBoxMain.AppendText(sb.ToString() + "\r\n");
                }

            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void checkBoxWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxMain.WordWrap = this.checkBoxWordWrap.Checked;
        }

        private void buttonSetIPAddress_Click(object sender, EventArgs e)
        {
            try
            {
                //netsh interface ip set address "网络连接名" static 192.168.0.88 255.255.255.0 192.168.0.1 1
                //netsh interface ip set dns "网络连接名" static 202.216.224.66

                //string cmd = "netsh";
                //string param = string.Format("interface ip set dns \"{0}\" static {1}",
                //    this.textBoxName.Text, this.textBoxIP.Text);

                //Process.Start(cmd, param);

                //ScriptMgt.ExecuteBatFile(cmd, param, Application.StartupPath, Program.Log);  
                string gateway = textBoxGateway.Text.Trim() == textBoxGateway.Tag.ToString() ? "" : textBoxGateway.Text.Trim();
                string dns = textBoxDNS.Text.Trim() == textBoxDNS.Tag.ToString() ? "" : textBoxDNS.Text.Trim();

                if (Helper.SetIP(textBoxName.Text.Trim(), textBoxIP.Text.Trim(), textBoxSubnet.Text.Trim(), gateway, dns))
                {
                    MessageBox.Show(this, "succeed in resetting IP:" + textBoxIP.Text.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Please make sure all inputs are configured properly", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        public void GetNetworkInfo()
        {
            try
            {
                ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled = True");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();
                foreach (ManagementObject m in queryCollection)
                {
                    if (m["IPAddress"] == null)
                    {
                        continue;
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (var p in m.Properties)
                    {
                        sb = new StringBuilder();
                        if (p.Name == "Caption")
                        {
                            textBoxName.Text = p.Value.ToString();
                            textBoxName.Tag = p.Value.ToString();
                        }
                        else if (p.Name == "IPAddress" || p.Name == "IPSubnet" || p.Name == "DefaultIPGateway" || p.Name == "DNSServerSearchOrder")
                        {
                            string[] arr = (string[])p.Value;
                            if (arr != null)
                            {
                                foreach (string item in arr)
                                {
                                    if (item.Trim() != "")
                                    {
                                        sb.AppendFormat("{0}", item);
                                        break;
                                    }
                                }
                            }
                            switch (p.Name)
                            {
                                case "IPAddress":
                                    textBoxIP.Text = sb.ToString();
                                    textBoxIP.Tag = sb.ToString();
                                    break;
                                case "IPSubnet":
                                    textBoxSubnet.Text = sb.ToString();
                                    textBoxSubnet.Tag = sb.ToString();
                                    break;
                                case "DefaultIPGateway":
                                    textBoxGateway.Text = sb.ToString();
                                    textBoxGateway.Tag = sb.ToString();
                                    break;
                                case "DNSServerSearchOrder":
                                    textBoxDNS.Text = sb.ToString();
                                    textBoxDNS.Tag = sb.ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void btServiceName_Click(object sender, EventArgs e)
        {
            try
            {
                string status;
                ServiceController sc = new ServiceController(textBoxServiceName.Text.Trim());

                if (sc != null)
                {
                    switch (sc.Status)
                    {
                        case ServiceControllerStatus.Running:
                            status = "Running";
                            break;
                        case ServiceControllerStatus.Stopped:
                            status = "Stopped";
                            break;
                        case ServiceControllerStatus.Paused:
                            status = "Paused";
                            break;
                        case ServiceControllerStatus.StopPending:
                            status = "Stopping";
                            break;
                        case ServiceControllerStatus.StartPending:
                            status = "Starting";
                            break;
                        case ServiceControllerStatus.ContinuePending:
                            status = "Continue Pending";
                            break;
                        case ServiceControllerStatus.PausePending:
                            status = "Pause Pending";
                            break;
                        default:
                            status = "Status Changing";
                            break;
                    }
                    MessageBox.Show(this, "This service's current status is " + status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show(this, "There is no windows service named " + textBoxServiceName.Text.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void btCableStatus_Click(object sender, EventArgs e)
        {
            try
            {
                //int temp;
                //if (CommonFunctionManager.IsNetworkAlive(out temp))
                //{
                //    MessageBox.Show(this, "The Network is working fine!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show(this, "The Network is disconnected!" + textBoxServiceName.Text.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                string status = "";
                ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();
                foreach (ManagementObject m in queryCollection)
                {
                    StringBuilder sb = new StringBuilder();
                    if (m["Caption"].ToString().Trim() == this.textBoxName.Text.Trim())
                    {
                        switch (m["NetConnectionStatus"].ToString())
                        {
                            case "0":
                                status = "Disconnected";
                                break;
                            case "1":
                                status = "Connecting";
                                break;
                            case "2":
                                status = "Connected";
                                break;
                            case "3":
                                status = "Disconnecting";
                                break;
                            case "4":
                                status = "Hardware not present";
                                break;
                            case "5":
                                status = "Hardware disabled";
                                break;
                            case "6":
                                status = "Hardware malfunction";
                                break;
                            case "7":
                                status = "Media disconnected";
                                break;
                            case "8":
                                status = "Authenticating";
                                break;
                            case "9":
                                status = "Authentication succeeded";
                                break;
                            case "10":
                                status = "Authentication failed";
                                break;
                            case "11":
                                status = "Invalid address";
                                break;
                            case "12":
                                status = "Credentials required ";
                                break;
                            default:
                                status = "Default";
                                break;
                        }
                    }
                }
                if (status == "")
                {
                    MessageBox.Show(this, "There is no network adapter named" + this.textBoxName.Text.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "The Network Adapter's current status is " + status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ServiceList form = new ServiceList())
            {
                form.SelectedText = this.textBoxServiceName.Text;
                form.ShowDialog();
                if (form.SelectedText.Trim() != "")
                {
                    this.textBoxServiceName.Text = form.SelectedText.Trim();
                }
            }
        }
    }
}
