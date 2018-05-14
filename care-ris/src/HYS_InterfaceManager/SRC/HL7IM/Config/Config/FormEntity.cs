using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Base;
using HYS.IM.Common.Logging;
using System.IO;

namespace HYS.IM.Config
{
    public partial class FormEntity : Form
    {
        private IConfigUI _configUI;
        private IMessageEntityConfig _entityConfig;
        private MessageEntityConfigConfig _cfgConfig;
        private EntityConfigAgent _entityAgent;
        private EntityConfigBase _entityCfg;
        private string _cfgFilePath;

        public FormEntity(EntityAssemblyConfig entityCfg)
        {
            InitializeComponent();
            
            _cfgFilePath = ConfigHelper.GetFullPath(entityCfg.InitializeArgument.ConfigFilePath);
            _cfgFilePath = ConfigHelper.DismissDotDotInThePath(_cfgFilePath);

            LoadSetting(entityCfg);
        }

        private void BrowseForXSLTFile(TextBox tb)
        {
            string _xsltFilePath = Path.Combine(_cfgFilePath, "FrameworkTemplates");
            if (!Directory.Exists(_xsltFilePath)) _xsltFilePath = _cfgFilePath;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = _xsltFilePath;
            dlg.Filter = "XSLT Files (*.xsl,*.xslt)|*.xsl;*.xslt|All Files (*.*)|*.*";
            dlg.Title = "Select XSLT File";
            dlg.Multiselect = false;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            string fname = dlg.FileName;
            if (this.checkBoxRelativePath.Checked) tb.Text = ConfigHelper.GetRelativePath(_cfgFilePath, fname);
            else tb.Text = fname;
        }
        private void LoadSetting(EntityAssemblyConfig entityCfg)
        {
            _entityAgent = new EntityConfigAgent(entityCfg, Program.Log);
            if (!_entityAgent.Initialize(entityCfg.InitializeArgument)) return;

            LoadMainConfigTabPage();
            LoadTransformPage();

            _cfgConfig = Program.ConfigMgr.Config.GetMessageEntityConfigConfig(entityCfg.EntityInfo.Name);
            if (_cfgConfig != null) this.Size = _cfgConfig.ConfigWindowSize;
            this.Text = entityCfg.EntityInfo.Name + " Configuration";
        }
        private void LoadMainConfigTabPage()
        {
            _entityConfig = _entityAgent.EntityConfigInstance;
            IConfigUI[] uilist = _entityConfig.GetIConfigUI();
            if (uilist == null || uilist.Length < 1) return;

            _configUI = uilist[0];
            Control ctl = _configUI.GetControl();
            EntityLoader.PrepareControl(ctl, this);
            this.panelMain.Controls.Add(ctl);
            this.tabPageMain.Text = _configUI.Title;
        }
        private void LoadTransformPage()
        {
            _entityCfg = _entityConfig.GetConfiguration();
            if (_entityCfg == null) return;

            // Note: assume that an entity should be publisher or subcriber, not both of them.

            if ((_entityCfg.Interaction & InteractionTypes.Publisher) == InteractionTypes.Publisher
                && _entityCfg.PublishConfig != null)
            {
                this.panelOneWayTransform.Visible = true;
                //this.checkBoxXSLT.Text = string.Format(this.checkBoxXSLT.Text, "sending to subscriber");
                this.checkBoxXSLT.Text = string.Format(this.checkBoxXSLT.Text, "sending to publish channel");
                this.checkBoxXSLT.Checked = _entityCfg.PublishConfig.ProcessConfig.EnableXSLTTransform;
                this.textBoxXSLT.Text = _entityCfg.PublishConfig.ProcessConfig.XSLTFileLocation;
            }
            else if ((_entityCfg.Interaction & InteractionTypes.Subscriber) == InteractionTypes.Subscriber
                    && _entityCfg.SubscribeConfig != null)
            {
                this.panelOneWayTransform.Visible = true;
                //this.checkBoxXSLT.Text = string.Format(this.checkBoxXSLT.Text, "receiving from publisher");
                this.checkBoxXSLT.Text = string.Format(this.checkBoxXSLT.Text, "receiving from publish channel");
                this.checkBoxXSLT.Checked = _entityCfg.SubscribeConfig.ProcessConfig.EnableXSLTTransform;
                this.textBoxXSLT.Text = _entityCfg.SubscribeConfig.ProcessConfig.XSLTFileLocation;
            }
            else
            {
                this.panelOneWayTransform.Visible = false;
            }

            // Note: assume that an entity should be requseter or responser, not both of them.

            if ((_entityCfg.Interaction & InteractionTypes.Requester) == InteractionTypes.Requester
                && _entityCfg.RequestConfig != null)
            {
                this.panelDuplexTransform.Visible = true;
                //this.checkBoxXSLTRequest.Text = string.Format(this.checkBoxXSLTRequest.Text, "sending to responser");
                this.checkBoxXSLTRequest.Text = string.Format(this.checkBoxXSLTRequest.Text, "sending to request channel");
                this.checkBoxXSLTRequest.Checked = _entityCfg.RequestConfig.ProcessConfig.PreProcessConfig.EnableXSLTTransform;
                this.textBoxXSLTRequest.Text = _entityCfg.RequestConfig.ProcessConfig.PreProcessConfig.XSLTFileLocation;
                //this.checkBoxXSLTResponse.Text = string.Format(this.checkBoxXSLTResponse.Text, "receiving from responser");
                this.checkBoxXSLTResponse.Text = string.Format(this.checkBoxXSLTResponse.Text, "receiving from request channel");
                this.checkBoxXSLTResponse.Checked = _entityCfg.RequestConfig.ProcessConfig.PostProcessConfig.EnableXSLTTransform;
                this.textBoxXSLTResponse.Text = _entityCfg.RequestConfig.ProcessConfig.PostProcessConfig.XSLTFileLocation;
            }
            else if ((_entityCfg.Interaction & InteractionTypes.Responser) == InteractionTypes.Responser
                     && _entityCfg.ResponseConfig != null)
            {
                this.panelDuplexTransform.Visible = true;
                //this.checkBoxXSLTRequest.Text = string.Format(this.checkBoxXSLTRequest.Text, "receiving from requester");
                this.checkBoxXSLTRequest.Text = string.Format(this.checkBoxXSLTRequest.Text, "receiving from request channel");
                this.checkBoxXSLTRequest.Checked = _entityCfg.ResponseConfig.ProcessConfig.PreProcessConfig.EnableXSLTTransform;
                this.textBoxXSLTRequest.Text = _entityCfg.ResponseConfig.ProcessConfig.PreProcessConfig.XSLTFileLocation;
                //this.checkBoxXSLTResponse.Text = string.Format(this.checkBoxXSLTResponse.Text, "sending to requester");
                this.checkBoxXSLTResponse.Text = string.Format(this.checkBoxXSLTResponse.Text, "sending to request channel");
                this.checkBoxXSLTResponse.Checked = _entityCfg.ResponseConfig.ProcessConfig.PostProcessConfig.EnableXSLTTransform;
                this.textBoxXSLTResponse.Text = _entityCfg.ResponseConfig.ProcessConfig.PostProcessConfig.XSLTFileLocation;
            }
            else
            {
                this.panelDuplexTransform.Visible = false;
            }

            if (this.panelOneWayTransform.Visible == false && this.panelDuplexTransform.Visible == true)
            {
                this.panelDuplexTransform.Location = this.panelOneWayTransform.Location;
            }
        }
        private bool SaveTransform()
        {
            if (_entityCfg == null) return false;

            bool enable = this.checkBoxXSLT.Checked;
            string xslFile = this.textBoxXSLT.Text.Trim();
            bool enableRequest = this.checkBoxXSLTRequest.Checked;
            string xslFileRequest = this.textBoxXSLTRequest.Text.Trim();
            bool enableResponse = this.checkBoxXSLTResponse.Checked;
            string xslFileResponse = this.textBoxXSLTResponse.Text.Trim();

            if (enable && string.IsNullOrEmpty(xslFile))
            {
                MessageBox.Show(this, "Please input the XSLT file location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabPageTransform.Select();
                this.textBoxXSLT.Focus();
                return false;
            }

            if (enableRequest && string.IsNullOrEmpty(xslFileRequest))
            {
                MessageBox.Show(this, "Please input the XSLT file location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabPageTransform.Select();
                this.textBoxXSLTRequest.Focus();
                return false;
            }

            if (enableResponse && string.IsNullOrEmpty(xslFileResponse))
            {
                MessageBox.Show(this, "Please input the XSLT file location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabPageTransform.Select();
                this.textBoxXSLTResponse.Focus();
                return false;
            }

            // Note: assume that an entity should be publisher or subcriber, not both of them.

            if ((_entityCfg.Interaction & InteractionTypes.Publisher) == InteractionTypes.Publisher
                && _entityCfg.PublishConfig != null)
            {
                _entityCfg.PublishConfig.ProcessConfig.EnableXSLTTransform = this.checkBoxXSLT.Checked;
                _entityCfg.PublishConfig.ProcessConfig.XSLTFileLocation = this.textBoxXSLT.Text.Trim();
            }
            else if ((_entityCfg.Interaction & InteractionTypes.Subscriber) == InteractionTypes.Subscriber
                    && _entityCfg.SubscribeConfig != null)
            {
                _entityCfg.SubscribeConfig.ProcessConfig.EnableXSLTTransform = this.checkBoxXSLT.Checked;
                _entityCfg.SubscribeConfig.ProcessConfig.XSLTFileLocation = this.textBoxXSLT.Text.Trim();
            }

            // Note: assume that an entity should be requseter or responser, not both of them.

            if ((_entityCfg.Interaction & InteractionTypes.Requester) == InteractionTypes.Requester
                && _entityCfg.RequestConfig != null)
            {
                _entityCfg.RequestConfig.ProcessConfig.PreProcessConfig.EnableXSLTTransform = this.checkBoxXSLTRequest.Checked;
                _entityCfg.RequestConfig.ProcessConfig.PreProcessConfig.XSLTFileLocation = this.textBoxXSLTRequest.Text.Trim();
                _entityCfg.RequestConfig.ProcessConfig.PostProcessConfig.EnableXSLTTransform = this.checkBoxXSLTResponse.Checked;
                _entityCfg.RequestConfig.ProcessConfig.PostProcessConfig.XSLTFileLocation = this.textBoxXSLTResponse.Text.Trim();
            }
            else if ((_entityCfg.Interaction & InteractionTypes.Responser) == InteractionTypes.Responser
                     && _entityCfg.ResponseConfig != null)
            {
                _entityCfg.ResponseConfig.ProcessConfig.PreProcessConfig.EnableXSLTTransform = this.checkBoxXSLTRequest.Checked;
                _entityCfg.ResponseConfig.ProcessConfig.PreProcessConfig.XSLTFileLocation = this.textBoxXSLTRequest.Text.Trim();
                _entityCfg.ResponseConfig.ProcessConfig.PostProcessConfig.EnableXSLTTransform = this.checkBoxXSLTResponse.Checked;
                _entityCfg.ResponseConfig.ProcessConfig.PostProcessConfig.XSLTFileLocation = this.textBoxXSLTResponse.Text.Trim();
            }

            return true;
        }
        private bool SaveSetting()
        {
            if (!SaveTransform()) return false;
            if (!_configUI.ValidateConfig()) return false;
            return _entityConfig.SaveConfiguration();
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
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseForXSLTFile(this.textBoxXSLT);
        }
        private void buttonBrowseRequest_Click(object sender, EventArgs e)
        {
            BrowseForXSLTFile(this.textBoxXSLTRequest);
        }
        private void buttonBrowseResponse_Click(object sender, EventArgs e)
        {
            BrowseForXSLTFile(this.textBoxXSLTResponse);
        }
        private void FormEntity_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_entityAgent != null) _entityAgent.Uninitialize();
            if (_cfgConfig != null && this.WindowState == FormWindowState.Normal)
            {
                _cfgConfig.ConfigWindowSize = this.Size;
                if (Program.ConfigMgr.Save())
                {
                    Program.Log.Write("Create config file (windows position) succeeded. " + Program.ConfigMgr.FileName);
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Create config file (windows position) failed. " + Program.ConfigMgr.FileName);
                    Program.Log.Write(Program.ConfigMgr.LastError);
                }
            }
        }
    }
}