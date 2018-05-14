using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Config
{
    public partial class FormMessageType : Form
    {
        private MessageType _messageType;
        public MessageType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        public FormMessageType(MessageType type)
        {
            InitializeComponent();

            _messageType = type;

            if (_messageType == null)
            {
                _messageType = new MessageType();
                this.Text = "Add Message Type";
            }
            else
            {
                this.Text = "Edit Message Type";
            }
        }

        private void LoadSetting()
        {
            this.textBoxCode.Text = _messageType.Code;
            this.textBoxCodeSystem.Text = _messageType.CodeSystem;
            this.textBoxMeaning.Text = _messageType.Meaning;
        }

        private bool SaveSetting()
        {
            string code = this.textBoxCode.Text.Trim();
            string codeSystem = this.textBoxCodeSystem.Text.Trim();

            if (code.Length < 1 || codeSystem.Length < 1)
            {
                MessageBox.Show(this, "Code and Code System should not be empty", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            _messageType.Code = code;
            _messageType.CodeSystem = codeSystem;
            _messageType.Meaning = this.textBoxMeaning.Text;

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
            string code = this.textBoxCode.Text.Trim();
            string codeSystem = this.textBoxCodeSystem.Text.Trim();
            this.buttonOK.Enabled = code.Length > 0 && codeSystem.Length > 0;
        }
    }
}