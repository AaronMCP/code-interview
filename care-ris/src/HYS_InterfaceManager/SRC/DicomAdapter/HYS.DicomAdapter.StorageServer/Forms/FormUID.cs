using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom.Net;

namespace HYS.DicomAdapter.StorageServer.Forms
{
    public partial class FormUID : Form
    {
        private StorageServiceUID _uid;
        public StorageServiceUID UID
        {
            get { return _uid; }
        }

        public FormUID(StorageServiceUID uid)
        {
            InitializeComponent();

            _uid = uid;
            if (_uid == null)
            {
                _uid = new StorageServiceUID();
                _uid.Enable = true;
            }
            else
            {
                this.Text = "Edit Storage Service";
            }
        }

        private void LoadSetting()
        {
            this.textBoxName.Text = _uid.Name;
            this.textBoxUID.Text = _uid.UID;
        }

        private bool SaveSetting()
        {
            string uid = this.textBoxUID.Text.Trim();
            string name = this.textBoxName.Text.Trim();

            foreach (StorageServiceUID u in Program.ConfigMgt.Config.StorageServiceUIDs)
            {
                if (u != _uid && (u.UID == uid || u.Name == name))
                {
                    MessageBox.Show(this, "SOP Class with the same name or UID has already existed. Please enter a different name or UID.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            _uid.Name = name;
            _uid.UID = uid;
            return true;
        }

        private void FormUID_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxUID_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.textBoxName.Text.Trim().Length > 0 &&
                this.textBoxUID.Text.Trim().Length > 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}