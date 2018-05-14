using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace Common.LicenseEditor
{
    public partial class LicenseEditor : Form
    {
        public LicenseEditor()
        {
            InitializeComponent();
        }

        private void buttonBrowns_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "License Files(*.lc)|*.lc|All Files(*.*)|*.*";
            dlg.FileOk += new CancelEventHandler(dlg_FileOk);
            dlg.ShowDialog();
        }

        void dlg_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog dlg = sender as OpenFileDialog;
            this.textBoxLicenseFile.Text = dlg.FileName;
            LoadLicense(this.textBoxLicenseFile.Text);
        }

        private void LoadLicense(string licenseFileName)
        {
            LicenseManager<LicenseConfig> lm = new LicenseManager<LicenseConfig>(licenseFileName);
            lm.EnabledCrypto = true;
            if (!lm.Load())
            {
                ShowMessageError("Load license failed.");
                return;
            }

            LoadLicense(lm.Config);
        }

        private void LoadLicense(LicenseConfig licenseConfig)
        {
            comboBoxDeviceName.Text = licenseConfig.DeviceName;
            comboBoxEnabled.Text = licenseConfig.Enabled.ToString();
        }

        private void ShowMessageError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowMessageInfo(string msg)
        {
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            LicenseConfig config = new LicenseConfig();
            config.Type = "HL7";
            if (comboBoxDeviceName.Text.Trim() == "HL7_SENDER")
            {
                config.DeviceName = "HL7_SENDER";
                config.Direction = "OUTBOUND";
            }
            else
            {
                config.DeviceName = "HL7_RECEIVER";
                config.Direction = "INBOUND";
            }

            config.Enabled = bool.Parse(comboBoxEnabled.Text.Trim());

            LicenseManager<LicenseConfig> lm = new LicenseManager<LicenseConfig>(this.textBoxLicenseFile.Text.Trim());
            lm.Config = config;
            lm.EnabledCrypto = true;
            if (lm.Save())
            {
                ShowMessageInfo("Save license success.");
            }
            else
            {
                ShowMessageError("Save license failed.");
            }
        }
    }
}
