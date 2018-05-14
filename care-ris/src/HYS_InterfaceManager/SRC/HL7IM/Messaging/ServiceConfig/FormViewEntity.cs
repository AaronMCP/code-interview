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

namespace HYS.IM.Messaging.ServiceConfig
{
    public partial class FormViewEntity : Form
    {
        public FormViewEntity(EntityAssemblyConfig cfg)
        {
            InitializeComponent();
            
            _cfg = cfg;
            LoadSetting();
        }

        private EntityAssemblyConfig _cfg;
        private void LoadSetting()
        {
            this.textBoxEntityID.Text = _cfg.EntityInfo.EntityID.ToString();
            this.textBoxEntityName.Text = _cfg.EntityInfo.Name;
            this.textBoxEntityDescription.Text = _cfg.EntityInfo.Description;
            this.textBoxClassName.Text = _cfg.ClassName;
            this.textBoxAssemblyLocation.Text = _cfg.AssemblyLocation;
            this.textBoxConfigPath.Text = _cfg.InitializeArgument.ConfigFilePath;
        }
    }
}