using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.RequestModel;

namespace HYS.IM.Messaging.Config
{
    public partial class FormMessageTypePair : Form
    {
        private MessageTypePair _messageTypePair;
        public MessageTypePair MessageTypePair
        {
            get { return _messageTypePair; }
            set { _messageTypePair = value; }
        }

        public FormMessageTypePair(MessageTypePair typePair)
        {
            InitializeComponent();

            _messageTypePair = typePair;

            if (_messageTypePair == null)
            {
                _messageTypePair = new MessageTypePair();
                this.Text = "Add Message Type Pair";
            }
            else
            {
                this.Text = "Edit Message Type Pair";
            }
        }

        private void LoadSetting()
        {
            this.textBoxRQCode.Text = _messageTypePair.RequestMessageType.Code;
            this.textBoxRQCodeSystem.Text = _messageTypePair.RequestMessageType.CodeSystem;
            this.textBoxRQMeaning.Text = _messageTypePair.RequestMessageType.Meaning;
            this.textBoxRSPCode.Text = _messageTypePair.ResponseMessageType.Code;
            this.textBoxRSPCodeSystem.Text = _messageTypePair.ResponseMessageType.CodeSystem;
            this.textBoxRSPMeaning.Text = _messageTypePair.ResponseMessageType.Meaning;
            this.textBoxPairDescription.Text = _messageTypePair.Description;
        }

        private bool SaveSetting()
        {
            string codeRQ = this.textBoxRQCode.Text.Trim();
            string codeSystemRQ = this.textBoxRQCodeSystem.Text.Trim();
            string codeRSP = this.textBoxRSPCode.Text.Trim();
            string codeSystemRSP = this.textBoxRSPCodeSystem.Text.Trim();

            if (codeRQ.Length < 1 || codeSystemRQ.Length < 1 ||
                codeRSP.Length < 1 || codeSystemRSP.Length < 1)
            {
                MessageBox.Show(this, "Code and Code System should not be empty", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            _messageTypePair.RequestMessageType.Code = codeRQ;
            _messageTypePair.RequestMessageType.CodeSystem = codeSystemRQ;
            _messageTypePair.RequestMessageType.Meaning = this.textBoxRQMeaning.Text;
            _messageTypePair.ResponseMessageType.Code = codeRSP;
            _messageTypePair.ResponseMessageType.CodeSystem = codeSystemRSP;
            _messageTypePair.ResponseMessageType.Meaning = this.textBoxRSPMeaning.Text;
            _messageTypePair.Description = this.textBoxPairDescription.Text;

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormMessageType_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            string codeRQ = this.textBoxRQCode.Text.Trim();
            string codeSystemRQ = this.textBoxRQCodeSystem.Text.Trim();
            string codeRSP = this.textBoxRSPCode.Text.Trim();
            string codeSystemRSP = this.textBoxRSPCodeSystem.Text.Trim();

            this.buttonOK.Enabled = (codeRQ.Length > 0 && codeSystemRQ.Length > 0 &&
                codeRSP.Length > 0 && codeSystemRSP.Length > 0);
        }
    }
}