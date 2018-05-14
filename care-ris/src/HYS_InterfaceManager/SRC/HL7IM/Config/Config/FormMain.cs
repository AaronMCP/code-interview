using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Management.NTServices;
using System.Diagnostics;
using System.Threading;
using HYS.IM.Messaging.Management.Scripts;
using System.Reflection;

namespace HYS.IM.Config
{
    public partial class FormMain : Form
    {
        private string _solutionDirPath;
        private string _serviceConfigPath;
        private ConfigManager<SolutionConfig> _solutionConfigMgr;
        private ConfigManager<NTServiceHostConfig> _serviceConfigMgr;
        private NTServiceHostInfo _hostInfo;
        private Label[] _labelGroup1;
        private Label[] _labelGroup2;

        public FormMain()
        {
            InitializeComponent();
            FlipDiagram();

            _labelGroup1 = new Label[] { this.labelApdater11, this.labelAdapter12, this.labelAdapter13 };
            _labelGroup2 = new Label[] { this.labelAdapter21, this.labelAdapter22 };

            this.Text = string.Format("{0} Configuration", Program.ConfigMgr.Config.HL7GatewayInterfaceName);
            this.timerLoading.Start();
        }

        private void FlipDiagram()
        {
            //this.labelAdapter21.BorderStyle = this.labelAdapter22.BorderStyle = BorderStyle.FixedSingle;
            //this.labelApdater11.BorderStyle = this.labelAdapter12.BorderStyle = this.labelAdapter13.BorderStyle = BorderStyle.FixedSingle;

            if (Program.ConfigMgr.Config.FlipDiagram)
            {
                this.SuspendLayout();
                //this.panelDiagram.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                this.panelDiagram.BackgroundImage = new Bitmap(Assembly.GetEntryAssembly().GetManifestResourceStream("HYS.IM.Config.DiagramFlip.bmp"));
                int left1 = this.labelAdapter12.Left, left2 = this.labelAdapter21.Left;
                this.labelAdapter21.Left = this.labelAdapter22.Left = left1 - 6;
                this.labelApdater11.Left = this.labelAdapter12.Left = this.labelAdapter13.Left = left2 - 6;
                this.labelAdapter21.Top = this.labelAdapter21.Top - 7;
                this.labelAdapter22.Top = this.labelAdapter22.Top - 7;
                this.labelApdater11.Top = this.labelApdater11.Top - 2;
                this.labelAdapter12.Top = this.labelAdapter12.Top - 3;
                this.labelAdapter13.Top = this.labelAdapter13.Top - 2;
                this.ResumeLayout();
            }
        }
        private void LoadSettings()
        {
            // load solution dir file

            _solutionDirPath = ConfigHelper.DismissDotDotInThePath(ConfigHelper.GetFullPath(Program.ConfigMgr.Config.IntegrationSolutionPath));
            string solutionDirFile = Path.Combine(_solutionDirPath, SolutionConfig.SolutionDirFileName);
            
            _solutionConfigMgr = new ConfigManager<SolutionConfig>(solutionDirFile);
            if (!_solutionConfigMgr.Load())
            {
                Program.Log.Write(_solutionConfigMgr.LastError);
                MessageBox.Show(this, string.Format("Cannot load configuration file of the integration solution from: \r\n{0}", solutionDirFile),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // get NT Service status

            if (_solutionConfigMgr.Config.Hosts.Count < 1)
            {
                MessageBox.Show(this, string.Format("Cannot find any NT Service Host in the integation solution: \r\n{0}", solutionDirFile),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            _hostInfo = _solutionConfigMgr.Config.Hosts[0];
            ServiceStatus status = ServiceMgt.GetServiceStatus(_hostInfo.ServiceName, Program.Log);
            RefreshServiceStatus(status);

            // get Adpater status

            _serviceConfigPath = Path.Combine(_solutionDirPath, _hostInfo.ServicePath);
            string serviceConfigFile = Path.Combine(_serviceConfigPath, NTServiceHostConfig.NTServiceHostConfigFileName);

            _serviceConfigMgr = new ConfigManager<NTServiceHostConfig>(serviceConfigFile);
            if (!_serviceConfigMgr.Load())
            {
                Program.Log.Write(_serviceConfigMgr.LastError);
                MessageBox.Show(this, string.Format("Cannot load configuration file of the NT Serivce Host from: \r\n{0}", serviceConfigFile),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            RefreshDiagram();
            RefreshAdapterList();
            RefreshAdapterButtons();
        }
        private void BindAdapterEnableUpdateHanlder()
        {
            this.listViewAdpaters.ItemChecked += delegate(object sender, ItemCheckedEventArgs e)
            {
                AdapterEnableUpdate();
            };
        }
        private EntityAssemblyConfig GetSelectedAdapterConfig()
        {
            if (this.listViewAdpaters.SelectedItems.Count < 1) return null;
            return this.listViewAdpaters.SelectedItems[0].Tag as EntityAssemblyConfig;
        }
        private void RefreshServiceStatus(ServiceStatus status)
        {
            this.labelInterfaceName.Text = string.Format("{0} ({1})", Program.ConfigMgr.Config.HL7GatewayInterfaceName, status.ToString());
            switch (status)
            {
                case ServiceStatus.Running:
                    this.buttonServiceStop.Enabled = true;
                    this.buttonServiceStart.Enabled = false;
                    this.buttonServiceConfig.Enabled = false;
                    this.panelAdapter.Enabled = false;
                    break;
                case ServiceStatus.Stopped:
                    this.buttonServiceStop.Enabled = false;
                    this.buttonServiceStart.Enabled = true;
                    this.buttonServiceConfig.Enabled = true;
                    this.panelAdapter.Enabled = true;
                    break;
                default:
                    this.buttonServiceStop.Enabled = false;
                    this.buttonServiceStart.Enabled = false;
                    this.buttonServiceConfig.Enabled = true;
                    this.panelAdapter.Enabled = true;
                    break;
            }
        }
        private void RefreshAdapterButtons()
        {
            EntityAssemblyConfig cfg = GetSelectedAdapterConfig();
            this.buttonAdapterConfig.Enabled = cfg != null;
        }
        private void RefreshAdapterList()
        {
            this.listViewAdpaters.Items.Clear();
            string[] entityList1 = Program.ConfigMgr.Config.GetMessageEntityGroup1();
            foreach (EntityAssemblyConfig entityCfg in _serviceConfigMgr.Config.Entities)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(entityCfg.EntityInfo.Name);
                item.SubItems.Add(entityCfg.EntityInfo.Description);
                item.Checked = entityCfg.Enable;
                item.Tag = entityCfg;
                if (Program.ConfigMgr.Config.FlipDiagram)
                {
                    if (entityList1.Contains<string>(entityCfg.EntityInfo.Name))
                    {
                        item.Group = this.listViewAdpaters.Groups[1];
                    }
                    else
                    {
                        item.Group = this.listViewAdpaters.Groups[0];
                    }
                }
                else
                {
                    if (entityList1.Contains<string>(entityCfg.EntityInfo.Name))
                    {
                        item.Group = this.listViewAdpaters.Groups[0];
                    }
                    else
                    {
                        item.Group = this.listViewAdpaters.Groups[1];
                    }
                }
                this.listViewAdpaters.Items.Add(item);
            }
        }
        private void RefreshDiagram()
        {
            string[] entityList1 = Program.ConfigMgr.Config.GetMessageEntityGroup1();
            for (int i = 0; i < entityList1.Length; i++)
            {
                EntityAssemblyConfig entityCfg = _serviceConfigMgr.Config.FindEntityByName(entityList1[i]);
                if (entityCfg == null) continue;
                if (i >= _labelGroup1.Length) break;
                Label lbl = _labelGroup1[i];
                lbl.Text = entityCfg.EntityInfo.Name;
                lbl.ForeColor = entityCfg.Enable ? Color.Black : Color.Gray;
                lbl.Font = new Font(lbl.Font, entityCfg.Enable ? FontStyle.Bold : FontStyle.Regular);
                this.toolTipMain.SetToolTip(lbl, string.Format("{0} ({1})", entityCfg.EntityInfo.Description, entityCfg.Enable ? "Enabled" : "Disabled"));
            }

            string[] entityList2 = Program.ConfigMgr.Config.GetMessageEntityGroup2();
            for (int i = 0; i < entityList2.Length; i++)
            {
                EntityAssemblyConfig entityCfg = _serviceConfigMgr.Config.FindEntityByName(entityList2[i]);
                if (entityCfg == null) continue;
                if (i >= _labelGroup2.Length) break;
                Label lbl = _labelGroup2[i];
                lbl.Text = entityCfg.EntityInfo.Name;
                lbl.ForeColor = entityCfg.Enable ? Color.Black : Color.Gray;
                lbl.Font = new Font(_labelGroup2[i].Font, entityCfg.Enable ? FontStyle.Bold : FontStyle.Regular);
                this.toolTipMain.SetToolTip(lbl, string.Format("{0} ({1})", entityCfg.EntityInfo.Description, entityCfg.Enable ? "Enabled" : "Disabled"));
            }
        }

        private void ServiceStart()
        {
            this.Cursor = Cursors.WaitCursor;
            if (ServiceMgt.SetServiceStatus(_hostInfo.ServiceName, ServiceStatus.Running, Program.Log))
            {
                RefreshServiceStatus(ServiceStatus.Running);
            }
            this.Cursor = Cursors.Default;
        }
        private void ServiceStop()
        {
            this.Cursor = Cursors.WaitCursor;
            if (ServiceMgt.SetServiceStatus(_hostInfo.ServiceName, ServiceStatus.Stopped, Program.Log))
            {
                RefreshServiceStatus(ServiceStatus.Stopped);
            }
            this.Cursor = Cursors.Default;
        }
        private void ServiceConfig()
        {
            try
            {
                this.Visible = false;

                //string installSvcBat = Path.Combine(_serviceConfigPath, "InstallService.bat");
                //string uninstallSvcBat = Path.Combine(_serviceConfigPath, "UninstallService.bat");
                //string registerSvcBat = Path.Combine(_serviceConfigPath, "RegisterHostToSolution.bat");
                //string unregisterSvcBat = Path.Combine(_serviceConfigPath, "UnregisterHostFromSolution.bat");
                //string backupUninstallSvcBat = Path.Combine(_serviceConfigPath, "~UninstallService.bat");
                //string backupUnregisterSvcBat = Path.Combine(_serviceConfigPath, "~UnregisterHostFromSolution.bat");
                string serviceConfigExe = Path.Combine(_serviceConfigPath, "HYS.IM.Messaging.ServiceConfig.exe");

                //Process up = Process.Start(new ProcessStartInfo(serviceConfigExe, "-s -u") { WorkingDirectory = _serviceConfigPath });
                //ScriptMgt.ExecuteBatFile(uninstallSvcBat, _serviceConfigPath, Program.Log);

                //File.Copy(uninstallSvcBat, backupUninstallSvcBat, true);
                //File.Copy(unregisterSvcBat, backupUnregisterSvcBat, true);
                
                Process p = Process.Start(new ProcessStartInfo(serviceConfigExe, "-b -d") { WorkingDirectory = _serviceConfigPath });
                p.WaitForExit();

                //Process rp = Process.Start(new ProcessStartInfo(serviceConfigExe, "-s -r") { WorkingDirectory = _serviceConfigPath });
                //ScriptMgt.ExecuteBatFile(installSvcBat, _serviceConfigPath, Program.Log);

                if (p.ExitCode == (int)ServiceConfigExitCode.OK)
                {
                    //ScriptMgt.ExecuteBatFile(backupUninstallSvcBat, _serviceConfigPath, Program.Log);
                    //ScriptMgt.ExecuteBatFile(backupUnregisterSvcBat, _serviceConfigPath, Program.Log);
                    //ScriptMgt.ExecuteBatFile(installSvcBat, _serviceConfigPath, Program.Log);
                    //ScriptMgt.ExecuteBatFile(registerSvcBat, _serviceConfigPath, Program.Log);

                    string selfFileName = ConfigHelper.GetFullPath("HYS.IM.Config.exe");
                    Process np = Process.Start(new ProcessStartInfo(selfFileName) { WorkingDirectory = Application.StartupPath });
                    np.EnableRaisingEvents = false;

                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }
            finally
            {
                this.Visible = true;
            }
        }
        private void ServiceViewLog()
        {
            try
            {
                string serviceLogPath = Path.Combine(_serviceConfigPath, "Log");
                if (!Directory.Exists(serviceLogPath)) Directory.CreateDirectory(serviceLogPath);
                Process p = Process.Start(serviceLogPath);
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }
        }
        private void AdapterConfig()
        {
            EntityAssemblyConfig cfg = GetSelectedAdapterConfig();
            if (cfg == null) return;

            // FormMain.AdapterEnableUpdate() may save NTServiceConfig.xml file, the EntityAssemblyConfig in this file should be readonly.
            // Therefore we use EntityAssemblyConfig in SolutionDir instead, as the EntityInitializeArgument and other properties in the EntityAssemblyConfig
            // may be modified when it is need to load the IMessageEntityConfig from the assembly.
            // And therefore, there is no need to deep copy EntityAssemblyConfig object.

            EntityContractBase contract = _solutionConfigMgr.Config.FindEntityByID(cfg.EntityInfo.EntityID);
            if (contract == null) return;

            EntityAssemblyConfig safeCfg = contract.AssemblyConfig;
            safeCfg.AssemblyLocation = Path.Combine(_solutionDirPath, safeCfg.AssemblyLocation);
            safeCfg.InitializeArgument.ConfigFilePath = Path.Combine(_solutionDirPath, safeCfg.InitializeArgument.ConfigFilePath);
            safeCfg.InitializeArgument.Description = "HL7 Gateway Configuration GUI";

            FormEntity frm = new FormEntity(safeCfg);
            frm.ShowDialog(this);
        }
        private void AdapterBrowseFile()
        {
            try
            {
                EntityAssemblyConfig cfg = GetSelectedAdapterConfig();
                if (cfg == null) return;
                string entityPath = ConfigHelper.DismissDotDotInThePath(Path.Combine(_serviceConfigPath, cfg.InitializeArgument.ConfigFilePath));
                string entityXSLTPath = Path.Combine(entityPath, "FrameworkTemplates");
                if (!Directory.Exists(entityXSLTPath)) Directory.CreateDirectory(entityXSLTPath);
                Process p = Process.Start(entityXSLTPath);
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }
        }
        private void AdapterEnableUpdate()
        {
            foreach (ListViewItem item in this.listViewAdpaters.Items)
            {
                EntityAssemblyConfig entityCfg = item.Tag as EntityAssemblyConfig;
                if (entityCfg != null) entityCfg.Enable = item.Checked;
            }

            this.Cursor = Cursors.WaitCursor;
            bool res = _serviceConfigMgr.Save();
            this.Cursor = Cursors.Default;

            if (res)
            {
                Program.Log.Write(string.Format("Save configuration file of the NT Service Host success. {0}", _serviceConfigMgr.FileName));
                RefreshDiagram();
            }
            else
            {
                Program.Log.Write(_serviceConfigMgr.LastError);
                MessageBox.Show(this, string.Format("Save configuration file of the NT Service Host failed.\r\n{0}",
                    _serviceConfigMgr.FileName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void timerLoading_Tick(object sender, EventArgs e)
        {
            this.timerLoading.Stop();
            LoadSettings();
        }
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControlMain.SelectedTab == this.tabPageAdapter)
            {
                this.tabControlMain.SelectedIndexChanged -= new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
                BindAdapterEnableUpdateHanlder();
            }
        }
        private void listViewAdpaters_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAdapterButtons();
        }
        private void buttonAdapterConfig_Click(object sender, EventArgs e)
        {
            AdapterConfig();
        }
        private void buttonServiceStart_Click(object sender, EventArgs e)
        {
            ServiceStart();
        }
        private void buttonServiceStop_Click(object sender, EventArgs e)
        {
            ServiceStop();
        }
        private void buttonServiceConfig_Click(object sender, EventArgs e)
        {
            ServiceConfig();
        }
        private void buttonServiceLog_Click(object sender, EventArgs e)
        {
            ServiceViewLog();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
