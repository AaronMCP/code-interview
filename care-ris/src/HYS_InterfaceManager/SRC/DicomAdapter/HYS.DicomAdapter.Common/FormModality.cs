using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom.Net;

namespace HYS.DicomAdapter.Common
{
    public partial class FormModality : Form
    {
        private Modality _modality;
        public Modality Modality
        {
            get { return _modality; }
        }

        public FormModality( Modality modality )
        {
            InitializeComponent();

            _modality = modality;
            if (_modality == null)
            {
                _modality = new Modality();
                this.Text = "Add Modality";
            }
            else
            {
                this.Text = "Edit Modality";
            }
        }

        private void LoadSetting()
        {
            this.textBoxAETitle.Text = _modality.AETitle;
            this.textBoxIPAddress.Text = _modality.IPAddress;
            this.textBoxDescription.Text = _modality.Description;
            //this.numericUpDownPort.Value = _modality.Port;
        }

        private void SaveSetting()
        {
            _modality.AETitle = this.textBoxAETitle.Text;
            _modality.IPAddress = this.textBoxIPAddress.Text;
            _modality.Description = this.textBoxDescription.Text;
            //_modality.Port = (int)this.numericUpDownPort.Value;
        }

        private void textBoxAETitle_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled =
                this.textBoxAETitle.Text.Trim().Length > 0 &&
                this.textBoxIPAddress.Text.Trim().Length > 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormModality_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}