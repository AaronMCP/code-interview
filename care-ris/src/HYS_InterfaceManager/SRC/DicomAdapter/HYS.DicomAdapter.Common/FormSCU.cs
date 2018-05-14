using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Logging;
using HYS.Common.Dicom.Net;
using HYS.Adapter.Base;

namespace HYS.DicomAdapter.Common
{
    public partial class FormSCU : Form, IConfigUI
    {
        private ILogging _log;
        private IDicomSCUConfigMgt _mgt;
        public FormSCU(IDicomSCUConfigMgt mgt, ILogging log)
        {
            InitializeComponent();

            _mgt = mgt;
            _log = log;

            if (!_mgt.EnableTimerSetting)
            {
                this.groupBoxCharacterSet.Location = this.groupBoxTimer.Location;
                this.groupBoxTimer.Visible = false;
            }
        }

        private void LoadSetting()
        {
            Modality scp = _mgt.GetSCPConfig();
            SCUConfig scu = _mgt.GetSCUConfig();
            if (scp == null || scu == null) return;

            this.textBoxSCPAET.Text = scp.AETitle;
            this.textBoxSCPIP.Text = scp.IPAddress;
            this.numericUpDownSCPPort.Value = scp.Port;
            this.textBoxSCUAET.Text = scu.AETitle;
            this.numericUpDownPDULength.Value = scu.MaxPduLength;
            this.numericUpDownTimeOut.Value = scu.AssociationTimeOut;
            this.numericUpDownTimer.Value = _mgt.InvokeInterval;
            this.comboBoxCharacterSet.Text = _mgt.CharacterSetName;
            this.checkBoxSendCharacterSetTag.Checked = _mgt.SendCharacterSetTag;
        }
        private void SaveSetting()
        {
            Modality scp = _mgt.GetSCPConfig();
            SCUConfig scu = _mgt.GetSCUConfig();
            if (scp == null || scu == null) return;

            scp.AETitle = this.textBoxSCPAET.Text;
            scp.IPAddress = this.textBoxSCPIP.Text.Trim();
            scp.Port = (int)this.numericUpDownSCPPort.Value;
            scu.AETitle = this.textBoxSCUAET.Text;
            scu.MaxPduLength = (int)this.numericUpDownPDULength.Value;
            scu.AssociationTimeOut = (int)this.numericUpDownTimeOut.Value;
            _mgt.InvokeInterval = (int)this.numericUpDownTimer.Value;
            _mgt.CharacterSetName = this.comboBoxCharacterSet.Text;
            _mgt.SendCharacterSetTag = this.checkBoxSendCharacterSetTag.Checked;
        }

        private void FormSCU_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }
        private void FormSCU_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting();
        }
        private void buttonEcho_Click(object sender, EventArgs e)
        {
            if (Modality.Echo2(this.textBoxSCPAET.Text,
               this.textBoxSCPIP.Text.Trim(),
               (int)this.numericUpDownSCPPort.Value,
               this.textBoxSCUAET.Text))
            {
                MessageBox.Show(this, "SCP Verification Success.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "SCP Verification Failed.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region IConfigUI Members

        public Control GetControl()
        {
            return this;
        }

        public bool LoadConfig()
        {
            this.LoadSetting();
            return true;
        }

        public bool SaveConfig()
        {
            this.SaveSetting();
            return true;
        }

        string IConfigUI.Name
        {
            get
            {
                return this.Text;
            }
        }

        #endregion
    }
}
