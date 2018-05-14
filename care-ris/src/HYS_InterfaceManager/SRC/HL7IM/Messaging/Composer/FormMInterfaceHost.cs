using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMInterfaceHost : Form
    {
        private NTServiceHostInfo _host;
        public NTServiceHostInfo Host
        {
            get { return _host; }
        }

        private MInterface _mInterface;
        public FormMInterfaceHost(MInterface mi)
        {
            InitializeComponent();
            _mInterface = mi;
            LoadSetting();
        }

        private class NTServiceHostInfoWraper
        {
            public readonly NTServiceHostInfo Host;
            public NTServiceHostInfoWraper(NTServiceHostInfo hi)
            {
                Host = hi;
            }
            public override string ToString()
            {
                return Host.ServiceName;
            }
        }
        private void LoadSetting()
        {
            this.comboBoxHost.Items.Clear();
            foreach (NTServiceHostInfo hi in Program.ConfigMgt.Config.Hosts)
            {
                NTServiceHostInfoWraper w = new NTServiceHostInfoWraper(hi);
                this.comboBoxHost.Items.Add(w);
            }
            if (this.comboBoxHost.Items.Count > 0)
                this.comboBoxHost.SelectedIndex = 0;
        }
        private bool SaveSetting()
        {
            NTServiceHostInfoWraper w = this.comboBoxHost.SelectedItem as NTServiceHostInfoWraper;
            if (w == null || _mInterface == null) return false;

            foreach (NTServiceHostInfo h in _mInterface.Hosts)
            {
                if (h.ServiceName == w.Host.ServiceName)
                {
                    MessageBox.Show(this, 
                        string.Format("The host \"{0}\" has already existed in the interface", h.ServiceName),
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.comboBoxHost.Focus();
                    return false;
                }
            }

            _host = w.Host;
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
