using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using HYS.Messaging.Objects;
using HYS.Messaging.Objects.Entity;
using HYS.Messaging.Objects.PublishModel;
using HYS.Messaging.Objects.RequestModel;
using HYS.Messaging.Base.Config;
using HYS.Messaging.Base.Controler;
using HYS.Messaging.Queuing;
using HYS.Messaging.Queuing.LPC;
using HYS.Messaging.Queuing.MSMQ;
using HYS.Common.Logging;
using HYS.Common.Xml;

namespace HYS.Messaging.Test
{
    public partial class Form1 : Form
    {
        private EntityContainer _container;

        public Form1()
        {
            InitializeComponent();

            InitializeContainer();
        }

        public void InitializeContainer()
        {
            //EntityHostConfig hostCfg = new EntityHostConfig();

            //EntityAssemblyConfig entityCfg1 = new EntityAssemblyConfig();
            //entityCfg1.AssemblyLocation = "DemoFileAdapter.exe";
            //entityCfg1.ClassName = "DemoFileAdapter.Adapters.FileEntity1";
            //entityCfg1.InitializeArgument.ConfigFilePath = @"../../../DemoFileAdapter\bin\Debug\";

            //EntityAssemblyConfig entityCfg2 = new EntityAssemblyConfig();
            //entityCfg2.AssemblyLocation = "DemoFileAdapter.exe";
            //entityCfg2.ClassName = "DemoFileAdapter.Adapters.FileEntity2";
            //entityCfg2.InitializeArgument.ConfigFilePath = @"../../../DemoFileAdapter\bin\Debug\";

            //EntityAssemblyConfig entityCfg3 = new EntityAssemblyConfig();
            //entityCfg3.AssemblyLocation = "DemoFileAdapter.exe";
            //entityCfg3.ClassName = "DemoFileAdapter.Adapters.FileEntityRQ";
            //entityCfg3.InitializeArgument.ConfigFilePath = @"../../../DemoFileAdapter\bin\Debug\";

            //EntityAssemblyConfig entityCfg4 = new EntityAssemblyConfig();
            //entityCfg4.AssemblyLocation = "DemoFileAdapter.exe";
            //entityCfg4.ClassName = "DemoFileAdapter.Adapters.FileEntityRSP";
            //entityCfg4.InitializeArgument.ConfigFilePath = @"../../../DemoFileAdapter\bin\Debug\";

            //hostCfg.Entities.Add(entityCfg1);
            //hostCfg.Entities.Add(entityCfg2);
            //hostCfg.Entities.Add(entityCfg3);
            //hostCfg.Entities.Add(entityCfg4);

            //using (StreamWriter sw = File.CreateText("NTServiceHostConfig.xml"))
            //{
            //    sw.Write(Program.XMLHeader + hostCfg.ToXMLString());
            //}

            EntityHostConfig hostCfg = Program.ConfigMgt.Config;

            _container = new EntityContainer(hostCfg, Program.Log);
            string res = _container.Initialize(Program.ConfigMgt.Config.LogConfig, "WinForm Host").ToString();

            //MessageBox.Show(res);

            this.listBox1.Items.Clear();
            foreach (EntityAgent a in _container.EntityList)
            {
                this.listBox1.Items.Add(a);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_container.Start().ToString());
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_container.Stop().ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _container.Uninitalize();
        }
    }
}