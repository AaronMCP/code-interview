using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Composer.Forms
{
    public partial class FormMonitor : Form
    {
        public FormMonitor()
        {
            InitializeComponent();

            string filename = ConfigHelper.GetFullPath(Program.ConfigMgt.Config.MonitorConfigFileName);
            configMgt = new AdapterMonitorCfgMgt(filename);
            this.textBoxLocation.Text = filename;
        }

        private AdapterMonitorCfgMgt configMgt;

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            configMgt.FileName = this.textBoxLocation.Text;
            if (configMgt.Load())
            {
                this.propertyGridConfig.SelectedObject = configMgt.Config;
            }
            else
            {
                MessageBox.Show(this, "Load config file failed: " + configMgt.FileName + "\r\n\r\n" + configMgt.LastError);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!configMgt.Save())
            {
                MessageBox.Show(this, "Save config file failed: " + configMgt.FileName + "\r\n\r\n" + configMgt.LastError);
            }
        }
    }
}