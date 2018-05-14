using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Forms
{
    public partial class FormXSLTExt : Form
    {
        private MessageProcessingConfig _config;

        public FormXSLTExt(MessageProcessingConfig cfg)
        {
            _config = cfg;
            InitializeComponent();
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.checkBoxXmlNodeTransformer.Checked = (_config.XSLTExtensions & XSLTExtensionType.XmlNodeTransformer) != 0;
        }

        private void SaveSetting()
        {
            if (this.checkBoxXmlNodeTransformer.Checked)
                _config.XSLTExtensions = XSLTExtensionType.XmlNodeTransformer | _config.XSLTExtensions;
            else
                _config.XSLTExtensions = ~XSLTExtensionType.XmlNodeTransformer & _config.XSLTExtensions;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
