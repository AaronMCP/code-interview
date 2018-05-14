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
using HYS.SocketAdapter.Configuration;

namespace HYS.SocketAdapter.SocketOutboundAdapter
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            adpter.Initialize(null);
        }

        AdapterMain adpter = new AdapterMain();
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                adpter.Start(null);
                button1.Text = "Stop";
            }
            else
            {
                adpter.Stop(null);
                button1.Text = "Start";
            }
        }

        private void btBuildDefaultConfiguration_Click(object sender, EventArgs e)
        {
            string sConfigFile = Application.StartupPath + "\\" + SocketOutboundAdapterConfigMgt.FileName;
            string sBakFileName = StarFile.BackupFile(sConfigFile);
            if(sBakFileName=="")
                Program.Log.Write( LogType.Error, "Cannot backup configuration file:" + sConfigFile);
            else
                Program.Log.Write( LogType.Debug, "Configuration file:" + sConfigFile+" has been backup to "+sBakFileName);

            SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig = SocketOutboundAdapterConfigMgt.BuildSampleConfig();
            SocketOutboundAdapterConfigMgt.Save(sConfigFile);

            MessageBox.Show("New Configuration have been built!");

        }

        private void btBuildEmptyConfiguration_Click(object sender, EventArgs e)
        {
            string sConfigFile = Application.StartupPath + "\\" + SocketOutboundAdapterConfigMgt.FileName;
            string sBakFileName = StarFile.BackupFile(sConfigFile);
            if(sBakFileName=="")
                Program.Log.Write( LogType.Error, "Cannot backup configuration file:" + sConfigFile);
            else
                Program.Log.Write( LogType.Debug, "Configuration file:" + sConfigFile+" has been backup to "+sBakFileName);

            SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig = new SocketOutboundAdapterConfig();
            SocketOutboundAdapterConfigMgt.Save(sConfigFile);

            MessageBox.Show("New Configuration have been built!");
        }

        
    }
}