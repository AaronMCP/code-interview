using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;

namespace HYS.IM.Messaging.Config
{
    public partial class FormMain : Form
    {
        private interface IConfigPage
        {
            bool LoadSetting(EntityConfigBase cfg);
            bool SaveSetting();
        }

        private EntityConfigBase _config;
        private EntityConfigAgent _agent;
        private List<IConfigPage> _fxGUIList;
        private List<IConfigUI> _entityGUIList;

        public FormMain()
        {
            InitializeComponent();

            _fxGUIList = new List<IConfigPage>();
            _entityGUIList = new List<IConfigUI>();
            _agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
        }

        private void LoadSetting()
        {
            EntityConfigBase cfg = _agent.EntityConfig;
            _config = cfg;

            this.tabControlMain.TabPages.Clear();
            this.tabControlMain.TabPages.Add(this.tabPageGeneral);
            LoadGeneralInformation(cfg);

            if ((cfg.Interaction & InteractionTypes.Subscriber) == InteractionTypes.Subscriber)
            {
                this.tabControlMain.TabPages.Add(this.tabPageSubscribe);
                SubscribeConfigControler ctrl = new SubscribeConfigControler(this);
                this.tabPageSubscribe.Tag = ctrl;
                ctrl.LoadSetting(cfg);
                _fxGUIList.Add(ctrl);
            }

            if ((cfg.Interaction & InteractionTypes.Responser) == InteractionTypes.Responser)
            {
                this.tabControlMain.TabPages.Add(this.tabPageResponse);
                ResponseConfigControler ctrl = new ResponseConfigControler(this);
                this.tabPageResponse.Tag = ctrl;
                ctrl.LoadSetting(cfg);
                _fxGUIList.Add(ctrl);
            }

            IConfigUI[] guiList = _agent.EntityConfigInstance.GetIConfigUI();
            if (guiList != null)
            {
                foreach (IConfigUI gui in guiList)
                {
                    string title = gui.Title;
                    Control ctl = gui.GetControl();
                    if (ctl != null)
                    {
                        TabPage page = new TabPage(title);
                        page.AutoScroll = true;
                        page.Tag = gui;

                        EntityLoader.PrepareControl(ctl, this);
                        page.Controls.Add(ctl);
                        this.tabControlMain.TabPages.Add(page);
                        gui.LoadConfig();
                    }

                    _entityGUIList.Add(gui);
                }
            }

            if ((cfg.Interaction & InteractionTypes.Publisher) == InteractionTypes.Publisher)
            {
                this.tabControlMain.TabPages.Add(this.tabPagePublish);
                PublishConfigControler ctrl = new PublishConfigControler(this);
                this.tabPagePublish.Tag = ctrl;
                ctrl.LoadSetting(cfg);
                _fxGUIList.Add(ctrl);
            }

            if ((cfg.Interaction & InteractionTypes.Requester) == InteractionTypes.Requester)
            {
                this.tabControlMain.TabPages.Add(this.tabPageRequest);
                RequestConfigControler ctrl = new RequestConfigControler(this);
                this.tabPageRequest.Tag = ctrl;
                ctrl.LoadSetting(cfg);
                _fxGUIList.Add(ctrl);
            }
        }
        private void LoadGeneralInformation(EntityConfigBase cfg)
        {
            this.textBoxEntityID.Text = cfg.EntityID.ToString();
            this.textBoxEntityName.Text = cfg.Name;
            this.textBoxEntityDescription.Text = cfg.Description;
            this.textBoxInteraction.Text = cfg.Interaction.ToString();
        }
        private void SelectTabPage(object tag)
        {
            foreach (TabPage p in this.tabControlMain.TabPages)
            {
                if (p.Tag == tag)
                {
                    this.tabControlMain.SelectedTab = p;
                    break;
                }
            }
        }
        private bool SaveGeneralInformation(EntityConfigBase cfg)
        {
            string ename = this.textBoxEntityName.Text.Trim();

            if (!CheckDuplicatedEntityName(cfg, ename))
            {
                MessageBox.Show(this, "Message Entity \"" + ename + "\" has already exsited, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SelectTabPage(this.tabPageGeneral);
                this.textBoxEntityName.Focus();
                return false;
            }

            cfg.Name = ename;
            cfg.Description = this.textBoxEntityDescription.Text;
            cfg.EntityID = new Guid(this.textBoxEntityID.Text.Trim());
            cfg.Interaction = (InteractionTypes)Enum.Parse(typeof(InteractionTypes), this.textBoxInteraction.Text.Trim());

            return true;
        }
        private bool CheckDuplicatedEntityName(EntityConfigBase cfg, string ename)
        {
            foreach (EntityContractBase c in Program.SolutionMgt.Config.Entities)
            {
                if (c.EntityID != cfg.EntityID && c.Name == ename) return false;
            }
            return true;
        }
        private bool SaveSetting()
        {
            foreach (IConfigPage p in _fxGUIList)
            {
                if (!p.SaveSetting())
                {
                    SelectTabPage(p);
                    return false;
                }
            }

            foreach (IConfigUI u in _entityGUIList)
            {
                if (!u.ValidateConfig())
                {
                    SelectTabPage(u);
                    return false;
                }
            }

            if (!SaveGeneralInformation(_config)) return false;

            return _agent.EntityConfigInstance.SaveConfiguration();
        }

        private void UpdateConfigHostConfig()
        {
            Program.ConfigMgt.Config.EntityAssembly.EntityInfo.Name = _config.Name;
            Program.ConfigMgt.Config.EntityAssembly.EntityInfo.EntityID = _config.EntityID;
            Program.ConfigMgt.Config.EntityAssembly.EntityInfo.Description = _config.Description;

            // 20091217 please notice that the config file is save duplicately in FormMain_FormClosing()

            if (Program.ConfigMgt.Save())
            {
                Program.Log.Write("Create config file (entity infor) succeeded. " + Program.ConfigMgt.FileName);
            }
            else
            {
                Program.Log.Write(LogType.Error, "Create config file (entity infor) failed. " + Program.ConfigMgt.FileName);
                Program.Log.Write(Program.ConfigMgt.LastError);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Size = Program.ConfigMgt.Config.WindowSize;

            // 20091217 please notice that the log information (logType,moduleName) will be updated by the host

            Program.ConfigMgt.Config.EntityAssembly.InitializeArgument.LogConfig = Program.ConfigMgt.Config.LogConfig;
            Program.ConfigMgt.Config.EntityAssembly.InitializeArgument.Description = "Config GUI Host";

            _agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument);

            LoadSetting();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                UpdateConfigHostConfig();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _agent.Uninitialize();

            if (this.WindowState == FormWindowState.Normal)
            {
                Program.ConfigMgt.Config.WindowSize = this.Size;

                // 20091217 please notice that the config file is save duplicately in UpdateConfigHostConfig()

                if (Program.ConfigMgt.Save())
                {
                    Program.Log.Write("Create config file (windows position) succeeded. " + Program.ConfigMgt.FileName);
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Create config file (windows position) failed. " + Program.ConfigMgt.FileName);
                    Program.Log.Write(Program.ConfigMgt.LastError);
                }
            }
        }

        private void buttonGenerateID_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Entity ID should not be changed after entity has been registered into Integration Solution.\r\n\r\n"
                + "Entity ID should be generated only while the entity is being installed, \r\n"
                + "for example after copying existing entity from folder \"FileIn\" to a new folder \"FileIn2\",\r\n"
                + "the ID of the new entity in \"FileIn2\" should be re-generated before registering it into Integration Solution.\r\n\r\n"
                + "Are you sure to continue generating this entity ID ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            this.textBoxEntityID.Text = Guid.NewGuid().ToString();
        }
        private void buttonInteractionModify_Click(object sender, EventArgs e)
        {
            InteractionTypes t = (InteractionTypes)Enum.Parse(typeof(InteractionTypes), this.textBoxInteraction.Text.Trim());
            FormInteractionType frm = new FormInteractionType(t);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            this.textBoxInteraction.Text = frm.Types.ToString();
        }
    }
}