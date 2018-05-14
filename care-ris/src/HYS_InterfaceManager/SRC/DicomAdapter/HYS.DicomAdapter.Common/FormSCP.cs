using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Logging;

namespace HYS.DicomAdapter.Common
{
    public partial class FormSCP : Form, IConfigUI
    {
        private ILogging _log;
        private IDicomConfigMgt _mgt;
        public FormSCP(IDicomConfigMgt mgt, ILogging log)
        {
            InitializeComponent();
            _mgt = mgt;
            _log = log;
        }

        private void LoadSetting()
        {
            SCPConfig cfg = _mgt.GetSCPConfig();
            if (cfg == null) return;
            
            this.textBoxAETitle.Text = cfg.AETitle;
            this.numericUpDownPort.Value = cfg.Port;
            this.numericUpDownPDULength.Value = cfg.MaxPduLength;
            this.numericUpDownTimeOut.Value = cfg.AssociationTimeOut;
            this.radioButtonNotAllowAll.Checked = cfg.EnableModalityChecking;
            this.checkBoxEnableAETitleChecking.Checked = cfg.EnableAETitleChecking;

            RefreshModalityList();
            RefreshModalityButton();
        }
        private void SaveSetting()
        {
            SCPConfig cfg = _mgt.GetSCPConfig();
            if (cfg == null) return;

            cfg.AETitle = this.textBoxAETitle.Text;
            cfg.Port = (int) this.numericUpDownPort.Value;
            cfg.MaxPduLength = (int) this.numericUpDownPDULength.Value;
            cfg.AssociationTimeOut = (int) this.numericUpDownTimeOut.Value;
            cfg.EnableModalityChecking = this.radioButtonNotAllowAll.Checked;
            cfg.EnableAETitleChecking = this.checkBoxEnableAETitleChecking.Checked;
        }

        private void RefreshModalityList()
        {
            SCPConfig cfg = _mgt.GetSCPConfig();
            if (cfg == null) return;

            int index = 1;
            this.listViewModality.Items.Clear();
            foreach (Modality mod in cfg.KnownModalities)
            {
                ListViewItem item = this.listViewModality.Items.Add((index++).ToString());
                item.SubItems.Add(mod.AETitle);
                item.SubItems.Add(mod.IPAddress);
                //item.SubItems.Add(mod.Port.ToString());
                item.SubItems.Add(mod.Description);
                item.Tag = mod;
            }
        }
        private void RefreshModalityButton()
        {
            this.buttonDelete.Enabled =
                this.buttonEdit.Enabled =
                this.buttonEcho.Enabled = GetSelectedModality() != null;
        }
        private Modality GetSelectedModality()
        {
            if (this.listViewModality.SelectedItems.Count < 1) return null;
            return this.listViewModality.SelectedItems[0].Tag as Modality;
        }
        private void SelectModality(Modality modality)
        {
            foreach (ListViewItem item in this.listViewModality.Items)
            {
                Modality mod = item.Tag as Modality;
                if (mod == modality)
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        private void AddModality()
        {
            SCPConfig cfg = _mgt.GetSCPConfig();
            if (cfg == null) return;

            FormModality frm = new FormModality(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            Modality modality = frm.Modality;
            if (modality == null) return;

            cfg.KnownModalities.Add(modality);

            RefreshModalityList();
            SelectModality(modality);
            RefreshModalityButton();
        }
        private void EditModality()
        {
            Modality modality = GetSelectedModality();
            if (modality == null) return;

            FormModality frm = new FormModality(modality);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshModalityList();
            SelectModality(modality);
            RefreshModalityButton();
        }
        private void DeleteModality()
        {
            SCPConfig cfg = _mgt.GetSCPConfig();
            if (cfg == null) return;

            Modality modality = GetSelectedModality();
            if (modality == null) return;

            cfg.KnownModalities.Remove(modality);

            RefreshModalityList();
            RefreshModalityButton();
        }
        private void EchoModality()
        {
            Modality modality = GetSelectedModality();
            if (modality == null) return;

            string myAE = this.textBoxAETitle.Text;
            if (modality.Echo(myAE))
            {
                MessageBox.Show(this, "Echo succeeded.", "Echo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "Echo failed.", "Echo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddModality();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditModality();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteModality();
        }
        private void listViewModality_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshModalityButton();
        }
        private void radioButtonAllowAll_CheckedChanged(object sender, EventArgs e)
        {
            this.panelList.Enabled = this.radioButtonNotAllowAll.Checked;
        }
        private void buttonEcho_Click(object sender, EventArgs e)
        {
            EchoModality();
        }

        private void FormModality_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }
        private void FormModality_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting();
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