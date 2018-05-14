using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;

namespace HYS.IM.Messaging.Base.Forms
{
    public partial class FormEntity<A, E> : Form
        where A : EntryAttribute
        where E : IEntry
    {
        private ILog _log;
        public FormEntity(ILog log, bool updateEntityName)
        {
            InitializeComponent();
            
            _log = log;
            _updateEntityName = updateEntityName;

            this.Height = this.Height - this.panelStep2.Height;

            this.textBoxEntityDescription.ReadOnly =
                this.textBoxEntityName.ReadOnly = !updateEntityName;
        }
        public FormEntity(ILog log)
            : this(log, false)
        {
        }

        private EntityAssemblyConfig _entity;
        public EntityAssemblyConfig Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        private E _entry;
        private EntityConfigBase _cfg;
        private bool _updateEntityName;

        private Assembly _assembly;
        private bool LoadAssembly()
        {
            _entity = null;
            _assembly = null;

            string asmLocation = this.textBoxAssemblyLocation.Text.Trim();
            asmLocation = ConfigHelper.GetFullPath(asmLocation);
            if (!File.Exists(asmLocation))
            {
                MessageBox.Show(this, "Cannot find file: " + asmLocation,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string cfgPath = this.textBoxConfigPath.Text.Trim();
            if (cfgPath.Length < 1)
            {
                cfgPath = this.textBoxConfigPath.Text = Path.GetDirectoryName(asmLocation);
            }

            _assembly = EntityLoader.LoadAssembly(asmLocation);
            if (_assembly == null)
            {
                _log.Write(EntityLoader.LastError);
                MessageBox.Show(this, "Cannot load assembly.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Type[] tlist = EntityLoader.FindEntryType<A>(_assembly);
            if (tlist == null || tlist.Length < 1)
            {
                _log.Write(EntityLoader.LastError);
                MessageBox.Show(this, "Cannot find message entity in the assembly.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.comboBoxClassName.Items.Clear();
            foreach (Type t in tlist)
            {
                this.comboBoxClassName.Items.Add(t);
            }

            if (this.comboBoxClassName.Items.Count > 0)
                this.comboBoxClassName.SelectedIndex = 0;

            return true;
        }
        private bool LoadEntity()
        {
            _entity = null;

            Type t = this.comboBoxClassName.SelectedItem as Type;
            if (t == null) return false;

            E e = EntityLoader.CreateEntry<E>(t);
            _entry = e;

            if (e == null)
            {
                _log.Write(EntityLoader.LastError);
                MessageBox.Show(this, "Initialize message entity failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _entity = new EntityAssemblyConfig();
            _entity.ClassName = t.ToString();
            _entity.AssemblyLocation = this.textBoxAssemblyLocation.Text.Trim();
            _entity.InitializeArgument.ConfigFilePath = this.textBoxConfigPath.Text.Trim();

            if (!e.Initialize(_entity.InitializeArgument))
            {
                MessageBox.Show(this, "Initialize message entity failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            EntityConfigBase cfg = e.GetConfiguration();
            _cfg = cfg;

            if (cfg == null)
            {
                MessageBox.Show(this, "Initialize message entity failed.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.textBoxEntityName.Text = _entity.EntityInfo.Name = cfg.Name;
            this.textBoxEntityID.Text = (_entity.EntityInfo.EntityID = cfg.EntityID).ToString();
            this.textBoxEntityDescription.Text = _entity.EntityInfo.Description = cfg.Description;

            return true;
        }

        private void RefreshButton()
        {
            if (_finishFirstPage)
            {
                this.buttonOK.Enabled = this.comboBoxClassName.SelectedItem != null;
            }
            else
            {
                this.buttonOK.Enabled = this.textBoxAssemblyLocation.Text.Trim().Length > 0;
            }
        }

        private void BrowseAssembly()
        {
            string selpath = this.textBoxAssemblyLocation.Text; // add path handling mechenism according to defect EK_HI00077271
            if (selpath == null || selpath.Length < 1)
            {
                selpath = Application.StartupPath;
            }
            else
            {
                selpath = Path.GetDirectoryName(selpath);
                selpath = ConfigHelper.GetFullPath(selpath);
                selpath = Path.GetFullPath(selpath);        // transform from c:\a\..\b\c to c:\b\c
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = selpath; // Application.StartupPath;
            dlg.Filter = ".Net Assembly (*.dll,*.exe)|*.dll;*.exe|All Files (*.*)|*.*";
            dlg.Title = "Select Message Entity Assembly";
            dlg.Multiselect = false;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            
            string fname = dlg.FileName;
            if (this.checkBoxRelativePath.Checked) fname = ConfigHelper.GetRelativePath(fname);
            this.textBoxAssemblyLocation.Text = fname;

            string path = ConfigHelper.EnsurePathSlash(Path.GetDirectoryName(dlg.FileName));
            if (this.checkBoxRelativePath.Checked) path = ConfigHelper.GetRelativePath(path);
            this.textBoxConfigPath.Text = ConfigHelper.EnsurePathSlash(path);
        }
        private void BrowseConfig()
        {
            string selpath = this.textBoxConfigPath.Text;    // add path handling mechenism according to defect EK_HI00077271
            if (selpath == null || selpath.Length < 1)
            {
                selpath = Application.StartupPath;
            }
            else
            {
                selpath = ConfigHelper.GetFullPath(selpath);
                selpath = Path.GetFullPath(selpath);        // transform from c:\a\..\b\c to c:\b\c
            }

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = selpath; // Application.StartupPath;
            dlg.ShowNewFolderButton = false;
            dlg.Description = "Please select the folder which contains configuration files of the message entity.";

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            string path = ConfigHelper.EnsurePathSlash(dlg.SelectedPath);
            if (this.checkBoxRelativePath.Checked) path = ConfigHelper.GetRelativePath(path);
            this.textBoxConfigPath.Text = ConfigHelper.EnsurePathSlash(path);
        }

        private bool _finishFirstPage;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_finishFirstPage)
            {
                if (_entity == null)
                {
                    LoadAssembly();
                    LoadEntity();
                }

                if (_entity != null)
                {
                    if (_updateEntityName)
                    {
                        _log.Write(LogType.Debug, "[FormEntity] Update entity name to configuration.");

                        _entity.EntityInfo.Name = this.textBoxEntityName.Text;
                        _entity.EntityInfo.Description = this.textBoxEntityDescription.Text;

                        if (_entry != null && _cfg != null)
                        {
                            _log.Write(LogType.Debug, "[FormEntity] Update entity name to configuration file.");

                            IMessageEntityConfig c = _entry as IMessageEntityConfig;
                            if (c != null)
                            {
                                _cfg.Name = this.textBoxEntityName.Text;
                                _cfg.Description = this.textBoxEntityDescription.Text;
                                if (c.SaveConfiguration())
                                {
                                    _log.Write(LogType.Error, "Update entity name to configuration file succeeded.");
                                }
                                else
                                {
                                    _log.Write(LogType.Error, "Update entity name to configuration file failed.");
                                }
                            }
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.panelStep2.Location = this.panelStep1.Location;
                this.panelStep1.Visible = false;
                this.panelStep2.Visible = true;
                this.buttonOK.Text = "OK";

                LoadAssembly();
                _finishFirstPage = true;
                RefreshButton();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void textBoxAssemblyLocation_TextChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
        private void comboBoxClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEntity();
            RefreshButton();
        }
        private void buttonAssemblyBrowse_Click(object sender, EventArgs e)
        {
            BrowseAssembly();
        }
        private void buttonConfigBrowse_Click(object sender, EventArgs e)
        {
            BrowseConfig();
        }
    }
}