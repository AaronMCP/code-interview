using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.SQLInboundAdapterObjects;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class FormInputParameterSP : Form
    {
        private SQLInboundChanel _channel;

        public FormInputParameterSP(SQLInboundChanel chn)
        {
            InitializeComponent();

            _channel = chn;

            LoadSetting();
        }

        private void LoadSetting()
        {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;

            string str = this.labelCaption.Text;
            str = string.Format(str, _channel.Rule.GenerateInputParameterSPName(interfaceName));
            this.labelCaption.Text = str;

            str = _channel.Rule.InputParameterSPStatement;
            if (str != null && str.Length > 0)
            {
                this.textBoxSP.Text = str;
            }
            else
            {
                this.textBoxSP.Text = _channel.Rule.GenerateInputParameterSPInstallScript(interfaceName);
            }
        }
        private bool SaveSetting()
        {
            string str = this.textBoxSP.Text;

            if (str != null && str.Length > 0)
            {
                _channel.Rule.InputParameterSPStatement = str;
                return true;
            }
            else
            {
                if (MessageBox.Show(this, "Storage procedure should not be empty. Do you want to generate a new storage procedure?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
                    this.textBoxSP.Text = _channel.Rule.GenerateInputParameterSPInstallScript(interfaceName);
                }
                return false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}