using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Composer.Forms
{
    public partial class FormMain : Form
    {
        private FormConfig configCtrl;
        private FormDevice deviceCtrl;
        //private FormScript scriptCtrl;
        private FormService serviceCtrl;
        private FormMonitor monitorCtrl;

        public FormMain()
        {
            InitializeComponent();

            configCtrl = new FormConfig();
            AssemblyHelper.PrepareControl(configCtrl, this.tabPageConfig);
            this.tabPageConfig.Controls.Add(configCtrl);

            deviceCtrl = new FormDevice();
            AssemblyHelper.PrepareControl(deviceCtrl, this.tabPageDevice);
            this.tabPageDevice.Controls.Add(deviceCtrl);

            serviceCtrl = new FormService();
            AssemblyHelper.PrepareControl(serviceCtrl, this.tabPageService);
            this.tabPageService.Controls.Add(serviceCtrl);

            monitorCtrl = new FormMonitor();
            AssemblyHelper.PrepareControl(monitorCtrl, this.tabPageMonitor);
            this.tabPageMonitor.Controls.Add(monitorCtrl);

            //scriptCtrl = new FormScript();
            //AssemblyHelper.PrepareControl(scriptCtrl, this.tabPageScript);
            //this.tabPageScript.Controls.Add(scriptCtrl);

            //this.tabControlMain.TabPages.Remove(this.tabPageScript);
        }

        private void buttonIMParam_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = "IM Paramters";

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.ScrollBars = ScrollBars.Both;
            tb.WordWrap = false;
            tb.Multiline = true;

            foreach (string str in IMParameter.List)
            {
                tb.Text += str + "\r\n";
            }

            tb.Select(0, 0);
            frm.Controls.Add(tb);
            frm.ShowDialog(this);
        }

        private void buttonChineseCodingTest_Click(object sender, EventArgs e)
        {
            FormCode frm = new FormCode();
            frm.ShowDialog(this);
        }
    }
}