using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Adapter.Config.Controls;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Config.Forms
{
    public partial class FormConfigBi : Form, IMessageFilter
    {
        private IBidirectionalAdapterConfig _adapter;
        private AdapterInfoControl _generalCtrl;
        private DataProcessControl _processCtrl;
        private GarbageControl _garbageCtrl;
        private IConfigUI[] _adapterCtrls;

        public FormConfigBi()
        {
            InitializeComponent();

            if (Program.InIM || Program.InIMWizard)
            {
                this.MinimizeBox = false;
                this.TopMost = true;
            }
            if (Program.InIMWizard)
            {
                this.ControlBox = false;
                this.MaximizeBox = true;
                this.buttonCancel.Enabled = false;
            }

            Application.AddMessageFilter(this);
            this.FormClosing += new FormClosingEventHandler(FormConfig_FormClosing);

            _generalCtrl = new AdapterInfoControl();
            _generalCtrl.Dock = DockStyle.Fill;
            this.tabPageGeneral.Controls.Add(_generalCtrl);

            LoadAdapterConfig();
            LoadOptionConfig();
        }
        void FormConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        private void LoadOptionConfig()
        {
            if (_adapter == null) return;

            AdapterOption option = _adapter.Option;
            if (option == null)
            {
                Program.Log.Write(LogType.Warning,"Cannot find adapter option in this adapter.");
                return;
            }

            if (option.EnableDataProcess)
            {
                TabPage page = new TabPage("Data Process");
                _processCtrl = new DataProcessControl();
                _processCtrl.Dock = DockStyle.Fill;
                page.Controls.Add(_processCtrl);
                this.tabControlMain.TabPages.Add(page);
            }

            //if (option.EnableGarbageCollection)
            //{
            //    TabPage page = new TabPage("Other");
            //    _garbageCtrl = new GarbageControl();
            //    _garbageCtrl.Dock = DockStyle.Fill;
            //    page.Controls.Add(_garbageCtrl);
            //    this.tabControlMain.TabPages.Add(page);
            //}

            TabPage pageOther = new TabPage("Other");
            _garbageCtrl = new GarbageControl();
            if (!option.EnableGarbageCollection) _garbageCtrl.DisableGarbageSetting();
            _garbageCtrl.Dock = DockStyle.Fill;
            pageOther.Controls.Add(_garbageCtrl);
            this.tabControlMain.TabPages.Add(pageOther);
        }
        private void LoadAdapterConfig()
        {
            _adapter = Program.BiAdapter.Instance;
            if (_adapter == null)
            {
                MessageBox.Show(this, "Cannot create adapter instance.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _adapterCtrls = _adapter.GetConfigUI();

            if (_adapterCtrls == null)
            {
                Program.Log.Write(AssemblyHelper.LastError);
                MessageBox.Show(this, "Cannot create adapter config GUI.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (IConfigUI ui in _adapterCtrls)
            {
                Control ctl = ui.GetControl();
                TabPage page = new TabPage(ui.Name);
                AssemblyHelper.PrepareControl(ctl, page);
                page.Controls.Add(ctl);
                this.tabControlMain.TabPages.Add(page);
                //Program.Log.Write("Adapter config file: " + ui.FileName);
            }
        }
        
        private void LoadConfig()
        {
            AdapterConfigEntryAttribute a = Program.BiAdapter.Attribute;
            if (a != null) this.Text = a.Name;

            if (_generalCtrl!=null) _generalCtrl.LoadConfig();
            if (_processCtrl != null) _processCtrl.LoadConfig();
            if (_garbageCtrl != null) _garbageCtrl.LoadConfig();

            if (_adapterCtrls != null)
            {
                foreach (IConfigUI ui in _adapterCtrls)
                {
                    ui.LoadConfig();
                }
            }
        }
        private void SaveConfig()
        {
            if ((_generalCtrl != null) && (!_generalCtrl.BackupScriptFile())) return;

            if (_adapterCtrls != null)
            {
                foreach (IConfigUI ui in _adapterCtrls)
                {
                    if (!ui.SaveConfig()) return;
                }
            }

            if ((_garbageCtrl != null) && (!_garbageCtrl.SaveConfig())) return;
            if ((_processCtrl != null) && (!_processCtrl.SaveConfig())) return;
            if ((_generalCtrl != null) && (!_generalCtrl.SaveConfig())) return;

            Program.ExitCode = HYS.Common.Objects.Config.AdapterConfigExitCode.OK;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (Program.ConfigMgt.Config.WarnBeforeCancel)
            {
                if (MessageBox.Show(this, "Are you sure to exit configuration without saving?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            }

            this.Close();
        }
        private void FormConfigBi_Load(object sender, EventArgs e)
        {
            this.Size = Program.ConfigMgt.Config.WindowSize;
            LoadConfig();
        }

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == Win32Api.SW_SHOW)
            {
                this.Activate();
            }
            return false;
        }

        #endregion

        private void FormConfigBi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized || 
                this.WindowState != FormWindowState.Maximized )
            {
                Program.ConfigMgt.Config.WindowSize = this.Size;
                Program.ConfigMgt.Save();
            }
        }
    }
}
