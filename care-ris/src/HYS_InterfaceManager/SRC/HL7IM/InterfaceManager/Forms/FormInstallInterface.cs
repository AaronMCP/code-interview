using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.HL7IM.Manager.Config;
using HYS.HL7IM.Manager.Controler;
using CSH.eHeath.HL7Gateway.Manager;
using System.ServiceProcess;

namespace HYS.HL7IM.Manager.Forms
{
    public partial class FormInstallInterface : Form
    {
        public FormInstallInterface()
        {
            InitializeComponent();

            this.comboBoxType.DataSource = Enum.GetNames(typeof(InterfaceType));
        }

        public InterfaceType InterfaceType { get; set; }

        private void FormInstallInterface_Load(object sender, EventArgs e)
        {
            this.comboBoxType.Text = InterfaceType.ToString();
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            this.buttonInstall.Enabled = false;
            string strServiceName = this.textBoxInterfaceName.Text.Trim();
            if (!CheckInteraceName(strServiceName))
            {
                this.buttonInstall.Enabled = true;
                return;
            }

            if (HL7GatewayInterfaceHelper.InstallInterface(strServiceName
                , (InterfaceType)Enum.Parse(typeof(InterfaceType), this.comboBoxType.Text)
                , UpdateProgross))
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                MessageBoxHelper.ShowErrorInfomation("Install interface failed.please see log for details.");
                this.buttonInstall.Enabled = true;
                this.DialogResult = DialogResult.No;
            }
        }

        private void UpdateProgross(int max, int current, string strDescription)
        {
            this.labelRemark.Text = strDescription;
            this.labelRemark.Update();
            this.progressBarStatus.Maximum = max;
            this.progressBarStatus.Value = current;
        }

        private bool CheckInteraceName(string strInterfaceName)
        {
            if (string.IsNullOrEmpty(strInterfaceName))
            {
                MessageBoxHelper.ShowInformation("Please input service name.");
                return false;
            }

            if (!CheckDuplicatedService(strInterfaceName))
            {
                MessageBoxHelper.ShowInformation("The service name of this interface exists,please select anthor one.");
                return false;
            }            

            return true;
        }

        private static bool CheckDuplicatedService(string sname)
        {
            ServiceController[] scList = ServiceController.GetServices();
            if (scList != null)
            {
                foreach (ServiceController sc in scList)
                {
                    if (sc.ServiceName == sname) return false;
                }
            }
            scList = ServiceController.GetDevices();
            if (scList != null)
            {
                foreach (ServiceController sc in scList)
                {
                    if (sc.ServiceName == sname) return false;
                }
            }

            //if (!Program.ConfigMgt.Config.CheckInteraceName(sname, InterfaceType))
            //{
            //    return false;
            //}
            return true;
        }
    }
}
