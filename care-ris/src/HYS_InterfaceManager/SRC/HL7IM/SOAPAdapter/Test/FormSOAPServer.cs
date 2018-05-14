using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using HYS.Common.WCFHelper.Configurability;
using System.IO;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    public partial class FormSOAPServer : Form
    {
        public FormSOAPServer()
        {
            InitializeComponent();
        }

        internal ServiceHost _host;
        internal string _response;

        private delegate void DisplayRequestMessageHandler(string request);
        internal void DisplayRequestMessage(string request)
        {
            DisplayRequestMessageHandler handler = delegate(string req)
            {
                this.textBoxMsgRcv.Text = req;
            };
            this.Invoke(handler, new object[] { request });
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                _response = this.textBoxMsgSnd.Text.Trim();

                string uri = this.textBoxSrvURI.Text.Trim();
                string cfgFile = "HYS.MessageDevices.SOAPAdapter.Test.exe.config";
                cfgFile = Path.Combine(Application.StartupPath, cfgFile);

                _host = new ConfigurableServiceHost(cfgFile, typeof(AbstractService), new Uri(uri));
                ((ConfigurableServiceHost)_host).Tag = this;
                _host.Open();


                this.textBoxSrvURI.ReadOnly = true;
                this.textBoxMsgSnd.ReadOnly = true;
                //this.textBoxSrvAction.ReadOnly = true;
                this.buttonStart.Enabled = false;
                this.buttonStop.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.ToString(), this.Text);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_host == null) return;
                _host.Close();

                IDisposable h = _host as IDisposable;
                if (h != null) h.Dispose();

                this.textBoxSrvURI.ReadOnly = false;
                this.textBoxMsgSnd.ReadOnly = false;
                //this.textBoxSrvAction.ReadOnly = false;
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.ToString(), this.Text);
            }
        }

        private void FormSOAPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonStop_Click(sender, e);
        }
    }
}
