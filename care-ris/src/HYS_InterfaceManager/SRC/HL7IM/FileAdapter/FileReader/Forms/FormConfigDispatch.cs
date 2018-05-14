using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Config;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Forms
{
    public partial class FormConfigDispatch : Form
    {
        private MessageDispatchConfig _config;

        public FormConfigDispatch(MessageDispatchConfig cfg)
        {
            _config = cfg;
            InitializeComponent();
            LoadSetting();
        }

        private void LoadSetting()
        {
            switch (_config.Model)
            {
                case MessageDispatchModel.Publish: this.radioButtonPublish.Checked = true; break;
                case MessageDispatchModel.Request: this.radioButtonRequest.Checked = true; break;
                case MessageDispatchModel.Custom: this.radioButtonCustom.Checked = true; break;
            }

            this.textBoxXPath.Text = _config.CriteriaXPath;
            this.textBoxPrefix.Text = _config.CriteriaXPathPrefixDefinition;
            this.textBoxValueSubscriber.Text = _config.CriteriaPublishValueExpression;
            this.textBoxValueResponser.Text = _config.CriteriaRequestValueExpression;
        }

        private bool SaveSetting()
        {
            string xpath = this.textBoxXPath.Text.Trim();

            if (xpath != null && xpath.Length > 0)
            {
                _config.CriteriaXPath = xpath;
            }
            else
            {
                MessageBox.Show(this,
                    "Please enter the XPath.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.textBoxXPath.Focus();
                return false;
            }

            _config.CriteriaXPathPrefixDefinition = this.textBoxPrefix.Text;
            _config.CriteriaPublishValueExpression = this.textBoxValueSubscriber.Text;
            _config.CriteriaRequestValueExpression = this.textBoxValueResponser.Text;

            if (this.radioButtonPublish.Checked) _config.Model = MessageDispatchModel.Publish;
            else if (this.radioButtonRequest.Checked) _config.Model = MessageDispatchModel.Request;
            else if (this.radioButtonCustom.Checked) _config.Model = MessageDispatchModel.Custom;

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
    }
}
