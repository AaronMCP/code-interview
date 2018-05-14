using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMInterfaceConfigItem : Form
    {
        private MInterfaceConfigItem _configItem;
        public MInterfaceConfigItem ConfigItem
        {
            get { return _configItem; }
        }

        public FormMInterfaceConfigItem(MInterfaceConfigItem ci)
        {
            InitializeComponent();
            _configItem = ci;
            if (_configItem == null)
            {
                _configItem = new MInterfaceConfigItem();
                this.Text = "Add Configuration Item";
            }
            else
            {
                this.Text = "Edit Configuration Item";
            }
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxCaption.Text = _configItem.Caption;
            this.textBoxPrompt.Text = _configItem.Prompt;
            this.textBoxXPath.Text = _configItem.XPath;
            this.textBoxRegExp.Text = _configItem.ValidationRegularExpression;
            this.checkBoxCData.Checked = _configItem.UseCDataTag;
            this.checkBoxValidate.Checked = _configItem.EnableValidation;
        }
        private bool SaveSetting()
        {
            string pName = this.textBoxCaption.Text.Trim();
            if (pName.Length < 1)
            {
                MessageBox.Show(this, "Please enter the section name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxCaption.Focus();
                return false;
            }

            string xPath = this.textBoxXPath.Text.Trim();
            if (xPath.Length < 1)
            {
                MessageBox.Show(this, "Please enter the XPath location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxXPath.Focus();
                return false;
            }

            _configItem.Caption = pName;
            _configItem.Prompt = this.textBoxPrompt.Text;
            _configItem.XPath = xPath;
            _configItem.UseCDataTag = this.checkBoxCData.Checked;
            _configItem.EnableValidation = this.checkBoxValidate.Checked;
            _configItem.ValidationRegularExpression = this.textBoxRegExp.Text;
            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
