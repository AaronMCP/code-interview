using System;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Soap;
using HYS.DicomAdapter.Common;

namespace HYS.DicomAdapter.StorageServer.Forms
{
    public partial class FormSOAPConfig : Form, IConfigUI
    {
        public FormSOAPConfig()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void LoadSetting()
        {
            this.textBoxURL.Text = (Program.ConfigMgt.Config.SOAPClientSetting.ServerURI != null) ? Program.ConfigMgt.Config.SOAPClientSetting.ServerURI : "http://localhost:2489/Service/Service.asmx?wsdl";
            this.textBoxSOAPAction.Text = (Program.ConfigMgt.Config.SOAPClientSetting.SOAPAction != null) ? Program.ConfigMgt.Config.SOAPClientSetting.SOAPAction : "http://www.HaoYiShenghealth.com/MessageCom";
            this.checkBoxXMLasString.Checked = Program.ConfigMgt.Config.SOAPClientSetting.EnableRequestTransform & Program.ConfigMgt.Config.SOAPClientSetting.EnableResponseTransform;

            this.textBoxXSLTd2s.Text = (Program.ConfigMgt.Config.XSLTFileToTransformDICOMtoSOAP != null) ? Program.ConfigMgt.Config.XSLTFileToTransformDICOMtoSOAP : "XSLT\\DicomToStdRequest.xsl";
            this.textBoxXSLTs2d.Text = (Program.ConfigMgt.Config.XSLTFileToTransformSOAPtoDICOM != null) ? Program.ConfigMgt.Config.XSLTFileToTransformSOAPtoDICOM : "XSLT\\StdResponseToDicom.xsl";
        }
        private bool SaveSetting()
        {
            Program.ConfigMgt.Config.SOAPClientSetting.ServerURI = this.textBoxURL.Text.Trim();
            Program.ConfigMgt.Config.SOAPClientSetting.SOAPAction = this.textBoxSOAPAction.Text.Trim();
            Program.ConfigMgt.Config.XSLTFileToTransformDICOMtoSOAP = this.textBoxXSLTd2s.Text.Trim();
            Program.ConfigMgt.Config.XSLTFileToTransformSOAPtoDICOM = this.textBoxXSLTs2d.Text.Trim();

            Program.ConfigMgt.Config.SOAPClientSetting.EnableRequestTransform =
                Program.ConfigMgt.Config.SOAPClientSetting.EnableResponseTransform = this.checkBoxXMLasString.Checked;

            return true;
            //return Program.ConfigMgt.Save();
        }

        private void BrowseXSLTFile(TextBox tb)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "XSLT Files (*.xsl,*.xslt)|*.xsl;*.xslt|All Files (*.*)|*.*";
            dlg.Title = "Select XSLT File";
            dlg.Multiselect = false;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            tb.Text = dlg.FileName;
        }
        private void OpenTextFile(string fname)
        {
            FormText frm = new FormText(fname, Program.Log);
            frm.ShowDialog(this);
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
        private void buttonBrowseS2D_Click(object sender, EventArgs e)
        {
            BrowseXSLTFile(this.textBoxXSLTs2d);
        }
        private void buttonBrowseD2S_Click(object sender, EventArgs e)
        {
            BrowseXSLTFile(this.textBoxXSLTd2s);
        }

        private void buttonSoapReq_Click(object sender, EventArgs e)
        {
            OpenTextFile(XMLConfigHelper.RequestXSLTFileNameForSOAPClient);
        }
        private void buttonSoapRsp_Click(object sender, EventArgs e)
        {
            OpenTextFile(XMLConfigHelper.ResponseXSLTFileNameForSOAPClient);
        }

        private void buttonSoapAdvance_Click(object sender, EventArgs e)
        {
            OpenTextFile(SOAPConfigHelper.SOAPServerWCFConfigFileName);
        }

        #region IConfigUI Members

        public Control GetControl()
        {
            this.buttonOK.Visible = this.buttonCancel.Visible = false;
            return this;
        }

        public bool LoadConfig()
        {
            LoadSetting();
            return true;
        }

        public bool SaveConfig()
        {
            return SaveSetting();
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
