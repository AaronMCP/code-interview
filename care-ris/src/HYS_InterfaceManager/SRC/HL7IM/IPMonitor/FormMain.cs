using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.IPMonitor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.CenterToScreen();
            ctl = new MonitorControl(this);
            stop += buttonStop_Click;
        }

        private MonitorControl ctl;
        private EventHandler stop;

        public void RaiseStop()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(stop, new object[] { this.buttonStop, new EventArgs() });
            }
            else
            {
                this.buttonStop.PerformClick();
            }
        }        

        private void buttonTest_Click(object sender, EventArgs e)
        {
            (new FormTest()).ShowDialog(this);
        }
        private void buttonConfig_Click(object sender, EventArgs e)
        {
            (new FormConfig()).ShowDialog(this);
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            ctl.Start();
            this.buttonConfig.Enabled = false;
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            ctl.Stop();
            this.buttonConfig.Enabled = true;
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
        }
    }
}
