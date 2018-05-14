using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HYS.IM;
using HYS.IM.Forms;
using HYS.IM.Wizard;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Controler
{
    public class InterfaceToolControler : ControlerBase
    {
        public InterfaceToolControler(Form frm, InterfaceView view, SliderPanel panel, GCInterfaceManager interfaceMgt)
            : base(frm)
        {
            _viewPanel = panel;
            _interfaceView = view;
            _interfaceManager = interfaceMgt;
            if (_viewPanel == null ||
                _interfaceView == null ||
                _interfaceManager == null) throw (new ArgumentNullException());

            Initialize();
        }

        // "V" in MVC
        private InterfaceView _interfaceView;
        private SliderPanel _viewPanel;

        private DPanel _interfacePanel;
        private DPanelButton _btnInstallInterface;
        private DPanelButton _btnUninstallInterface;
        private DPanelButton _btnMonitorInterface;
        private DPanelButton _btnConfigInterface;
        private DPanelButton _btnStartInterface;
        private DPanelButton _btnStopInterface;
        private DPanelButton _btnImportConfig;
        private DPanelButton _btnExportConfig;
        public DPanel InterfacePanel
        {
            get { return _interfacePanel; }
        }

        // "M" in MVC
        private GCInterfaceManager _interfaceManager;
        public GCInterfaceManager InterfaceManager
        {
            get { return _interfaceManager; }
        }

        // Controler Logic
        private void Initialize()
        {
            _btnInstallInterface = new DPanelButton();
            _btnInstallInterface.Text = "Install Interface";
            _btnInstallInterface.Image = Properties.Resources.install;
            _btnInstallInterface.Click += new EventHandler(_btnInstallInterface_Click);

            _btnUninstallInterface = new DPanelButton();
            _btnUninstallInterface.Text = "Uninstall Interface";
            _btnUninstallInterface.Image = Properties.Resources.delete;
            _btnUninstallInterface.Click += new EventHandler(_btnUninstallInterface_Click);

            _btnStartInterface = new DPanelButton();
            _btnStartInterface.Text = "Start Interface";
            _btnStartInterface.Image = Properties.Resources.start;
            _btnStartInterface.Click += new EventHandler(_btnStartInterface_Click);

            _btnStopInterface = new DPanelButton();
            _btnStopInterface.Text = "Stop Interface";
            _btnStopInterface.Image = Properties.Resources.stop;
            _btnStopInterface.Click += new EventHandler(_btnStopInterface_Click);

            _btnMonitorInterface = new DPanelButton();
            _btnMonitorInterface.Text = "Monitor Interface";
            _btnMonitorInterface.Image = Properties.Resources.monitor;
            _btnMonitorInterface.Click += new EventHandler(_btnMonitorInterface_Click);

            _btnConfigInterface = new DPanelButton();
            _btnConfigInterface.Text = "Configure Interface";
            _btnConfigInterface.Image = Properties.Resources.config;
            _btnConfigInterface.Click += new EventHandler(_btnConfigInterface_Click);

            _btnExportConfig = new DPanelButton();
            _btnExportConfig.Text = "Export Configuration";
            _btnExportConfig.Image = Properties.Resources.export;
            _btnExportConfig.Click += new EventHandler(_btnExportConfig_Click);

            _btnImportConfig = new DPanelButton();
            _btnImportConfig.Text = "Import Configuration";
            _btnImportConfig.Image = Properties.Resources.import;
            _btnImportConfig.Click += new EventHandler(_btnImportConfig_Click);

            _interfacePanel = new DPanel(new DPanelButton[]{
                _btnInstallInterface, 
                _btnUninstallInterface,
                _btnStartInterface,
                _btnStopInterface,
                _btnMonitorInterface,
                _btnConfigInterface,
                _btnImportConfig,
                _btnExportConfig,
            });
            _interfacePanel.Title.BackColor = Color.DarkGray;
            _interfacePanel.Title.Text = "Interfaces Management";

            _viewPanel.OnPageRefresh += new EventHandler(_viewPanel_OnPageRefresh);
            _interfaceView.SelectionChange += new EventHandler(_interfaceView_SelectionChange);
        }

        public void InstallInterface()
        {
            base.SetStatus("Installing interface.");

            InterfaceInstallationWizard frm = new InterfaceInstallationWizard();
            if (frm.ShowDialog(frmMain) == DialogResult.OK)
            {
                GCInterface gcInterface = frm.InstalledInterface;
                if (gcInterface != null)
                {
                    Program.Log.Write("{Interface} Add interface (" + gcInterface.ToString() + ") succeed : " + gcInterface.FolderPath);

                    _interfaceView.RefreshView();
                    _interfaceView.SelectInterface(gcInterface);

                    CreateUninstallScript();
                }
            }

            base.ClearStatus();
        }
        public void UninstallInterface()
        {
            base.SetStatus("Uninstalling interface.");

            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;

            if (MessageBox.Show(frmMain, "Are you sure to uninstall the interface : " + gcInterface.InterfaceName + "?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            bool res = _interfaceManager.UninstallInterface(gcInterface);
            if (res)
            {
                Program.Log.Write("{Interface} uninstall interface (" + gcInterface.ToString() + ") succeed");

                res = _interfaceManager.RunDBUninstallScript(gcInterface);
                if (res)
                {
                    Program.Log.Write("{Interface} run (" + gcInterface.ToString() + ") DB uninstall script  succeed " + gcInterface.FolderPath);

                    res = _interfaceManager.DeleteInterfaceFromDatabase(gcInterface);
                    if (res)
                    {
                        Program.Log.Write("{Interface} remove interface (" + gcInterface.ToString() + ") from database succeed");

                        res = _interfaceManager.DeleteInterfaceFromFolder(gcInterface);
                        if (res)
                        {
                            Program.Log.Write("{Interface} remove interface (" + gcInterface.ToString() + ") directory succeed " + gcInterface.FolderPath);

                            _interfaceView.RefreshView();

                            CreateUninstallScript();
                        }
                        else
                        {
                            Program.Log.Write(LogType.Warning, "{Interface} remove interface (" + gcInterface.ToString() + ") directory failed " + gcInterface.FolderPath);
                            Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                            Program.Log.Write(GCError.LastError);
                        }
                    }
                    else
                    {
                        Program.Log.Write(LogType.Warning, "{Interface} remove interface (" + gcInterface.ToString() + ") from database failed");
                        Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                        Program.Log.Write(GCError.LastError);
                    }
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "{Interface} run (" + gcInterface.ToString() + ") DB uninstall script failed");
                    Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);
                }
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} uninstall interface (" + gcInterface.ToString() + ") failed");
                Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);
            }

            if (!res) MessageBox.Show(frmMain, "Uninstall interface failed.\r\n\r\n" + GCError.LastErrorInfor, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            base.ClearStatus();
        }
        public void StartInterface()
        {
            base.SetStatus("Starting interface.");

            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;


            if (_interfaceManager.StartInterface(gcInterface))
            {
                Program.Log.Write("{Interface} start interface (" + gcInterface.ToString() + ") succeed");

                _interfaceView.RefreshView();
                _interfaceView.SelectInterface(gcInterface);
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} start interface (" + gcInterface.ToString() + ") failed");
                Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(frmMain, "Start interface failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            base.ClearStatus();

        }
        public void StopInterface()
        {
            base.SetStatus("Stopping interface.");

            GCInterface gcInterface = _interfaceView.GetSelectedInterface();

            if (gcInterface == null) return;

            if (_interfaceManager.StopInterface(gcInterface))
            {
                Program.Log.Write("{Interface} stop interface (" + gcInterface.ToString() + ") succeed");

                _interfaceView.RefreshView();
                _interfaceView.SelectInterface(gcInterface);
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} stop interface (" + gcInterface.ToString() + ") failed");
                Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                //Stop some NT service may meets error (System.ComponentModel.Win32Exception: The service cannot accept control messages at this time),
                //especaily after these NT services are started together at the same time by bat files. 2007/07/17

                AdapterStatus status = ServiceControl.GetServiceStatus(gcInterface.InterfaceName);
                if (status != AdapterStatus.Stopped)
                {
                    MessageBox.Show(frmMain, "Stop interface failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _interfaceView.RefreshView();
                    _interfaceView.SelectInterface(gcInterface);
                }
            }

            base.ClearStatus();
        }
        public void MonitorInterface()
        {
            base.SetStatus("Monitoring interface.");

            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;

            //if (_interfaceManager.MonitorInterface(gcInterface))
            if (CallAdapterMonitorProcess(gcInterface))
            {
                Program.Log.Write("{Interface} monitor interface (" + gcInterface.ToString() + ") succeed");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} monitor interface (" + gcInterface.ToString() + ") failed");
                Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(frmMain, "Monitor interface failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            base.ClearStatus();
        }
        public void ConfigInterface()
        {
            base.SetStatus("Configing interface.");

            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;

            //if (_interfaceManager.ConfigInterface(gcInterface))
            if (CallAdapterConfigProcess(gcInterface, AdapterConfigArgument.InIM))
            {
                Program.Log.Write("{Interface} config interface (" + gcInterface.ToString() + ") succeed");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} config interface (" + gcInterface.ToString() + ") failed");
                Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(frmMain, "Config interface failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            base.ClearStatus();
        }
        public void ExportConfig()
        {
            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;

            base.SetStatus("Exporting interface configuration.");

            frmMain.Cursor = Cursors.WaitCursor;
            string[] filelist = _interfaceManager.DetectConfig(gcInterface, gcInterface.FolderPath);
            frmMain.Cursor = Cursors.Default;

            if (filelist == null)
            {
                Program.Log.Write(LogType.Warning, "{Interface} Export interface configuration failed : " + GCError.LastErrorInfor);
                if (GCError.LastError != null) Program.Log.Write(LogType.Error, GCError.LastError.ToString());

                MessageBox.Show(frmMain, "Export interface configuration failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto _ExportExit;
            }

            if (filelist.Length < 1)
            {
                MessageBox.Show(frmMain, "There is no configuration file in this interface (" + gcInterface.FolderPath + ")",
                    "Export Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                goto _ExportExit;
            }

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.Description = "Please select a folder to export interface configuration.";
            if (dlg.ShowDialog(frmMain) != DialogResult.OK) goto _ExportExit;

            string folderName = dlg.SelectedPath;
            FormFiles frm = new FormFiles(FileOperator.Export, gcInterface, filelist, folderName);
            if (frm.ShowDialog(frmMain) == DialogResult.OK)
            {
                _interfaceView.RefreshView();
                _interfaceView.SelectInterface(gcInterface);
            }

        _ExportExit:
            base.ClearStatus();
        }
        public void ImportConfig()
        {
            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;

            if (!gcInterface.Directory.Files.HasBackupableFile())
            {
                MessageBox.Show(frmMain, "There is no configuration file in this interface (" + gcInterface.FolderPath + ")",
                    "Import Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            base.SetStatus("Importing interface configuration.");

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = false;
            dlg.Description = "Please select a folder which contains interface configuration to be imported.";
            if (dlg.ShowDialog(frmMain) != DialogResult.OK) goto _ImportExit;

            frmMain.Cursor = Cursors.WaitCursor;
            string folderName = dlg.SelectedPath;
            string[] filelist = _interfaceManager.DetectConfig(gcInterface, folderName);
            frmMain.Cursor = Cursors.Default;

            if (filelist == null)
            {
                Program.Log.Write(LogType.Warning, "{Interface} Import interface configuration failed : " + GCError.LastErrorInfor);
                if (GCError.LastError != null) Program.Log.Write(LogType.Error, GCError.LastError.ToString());

                MessageBox.Show(frmMain, "Import interface configuration failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto _ImportExit;
            }

            if (filelist.Length < 1)
            {
                MessageBox.Show(frmMain, "Cannot find any configuration file in " + folderName,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto _ImportExit;
            }

            FormFiles frm = new FormFiles(FileOperator.Import, gcInterface, filelist, gcInterface.FolderPath);
            if (frm.ShowDialog(frmMain) == DialogResult.OK)
            {
                //update AdapterService configuration (ServiceName, DataDBConnection, ConfigDBConnection, etc.)
                foreach (string fileName in filelist)
                {
                    string fName = Path.GetFileName(fileName);
                    if (fName == ConfigHelper.ServiceDefaultFileName)
                    {
                        fName = gcInterface.FolderPath + "\\" + fName;
                        AdapterServiceCfgMgt mgt = new AdapterServiceCfgMgt(fName);
                        if (mgt.Load())
                        {
                            mgt.Config.ServiceName = gcInterface.InterfaceName;
                            mgt.Config.DataDBConnection = Program.ConfigMgt.Config.DataDBConnection;
                            mgt.Config.ConfigDBConnection = Program.ConfigMgt.Config.ConfigDBConnection;
                            if (mgt.Save()) Program.Log.Write(LogType.Info, "AdapterService configuration updated.");
                        }
                    }
                }

                //update SQL scripts (SP)
                if (Program.ConfigMgt.Config.ShowConfigAfterImportConfig)
                    CallAdapterConfigProcess(gcInterface, AdapterConfigArgument.InIMWizard);
            }

        _ImportExit:
            base.ClearStatus();
        }

        /// <summary>
        /// Note: when copying passive SQL interface, this is only a shadow copy. See GCInterfaceManager.CopyInterface() for details.
        /// </summary>
        public void CopyInterface()
        {
            GCInterface gcInterface = _interfaceView.GetSelectedInterface();
            if (gcInterface == null) return;

            base.SetStatus("Copying interface.");

            FormCopyInterface frm = new FormCopyInterface(_interfaceManager, gcInterface);
            if (frm.ShowDialog(frmMain) != DialogResult.OK) goto _ImportExit;

            CallAdapterConfigProcess(frm.ToInterface, AdapterConfigArgument.InIMWizard);
            _interfaceView.SelectInterface(frm.ToInterface);

        _ImportExit:
            base.ClearStatus();
        }

        private Process _cfgP;
        private GCInterface _gcInterface;
        private ManualResetEvent waitEvent;
        private bool CallAdapterMonitorProcess(GCInterface gcInterface)
        {
            return CallAdapterConfigProcess(gcInterface, null);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool CallAdapterConfigProcess(GCInterface gcInterface, string arg)
        {
            if (gcInterface == null) return false;
            if (_cfgP != null && !_cfgP.HasExited && waitEvent == null)
            {
                waitEvent = new ManualResetEvent(false);
                waitEvent.Reset();
                _cfgP.Kill();
                waitEvent.WaitOne(20000, true);
            }

            DeviceFile file = null;

            if (arg == null)
            {
                file = gcInterface.Directory.Files.FindFirstFile(DeviceFileType.MonitorAssembly);
                arg = AdapterConfigArgument.InIM;
            }
            else
            {
                file = gcInterface.Directory.Files.FindFirstFile(DeviceFileType.ConfigAssembly);
            }

            if (file == null) return false;

            string exefilename = ConfigHelper.GetFullPath(gcInterface.FolderPath + "\\" + file.Location);
            if (!System.IO.File.Exists(exefilename))
            {
                Program.Log.Write("{Interface} cannot find file : " + exefilename);
                return false;
            }


            _gcInterface = gcInterface;

            ProcessStartInfo pi = new ProcessStartInfo();
            pi.WorkingDirectory = Path.GetDirectoryName(exefilename);
            pi.Arguments = arg; // AdapterConfigArgument.InIM;
            pi.FileName = exefilename;

            Process p = Process.Start(pi);
            p.Exited += new EventHandler(p_Exited);
            p.EnableRaisingEvents = true;
            _cfgP = p;

            return true;
        }
        private delegate void OneArgHandler(GCInterface i);
        private delegate void NoArgHandler();
        void p_Exited(object sender, EventArgs e)
        {
            if (waitEvent != null) waitEvent.Set();
            waitEvent = null;

            Process p = sender as Process;
            AdapterConfigExitCode code = (AdapterConfigExitCode)p.ExitCode;
            if (code == AdapterConfigExitCode.OK)
            {
                if (_gcInterface.Status == AdapterStatus.Unknown)
                {
                    _interfaceManager.InstallInterface(_gcInterface);
                }

                _interfaceManager.RunDBUninstallScript(_gcInterface, true);
                _interfaceManager.RunDBInstallScript(_gcInterface);

                NoArgHandler nh = new NoArgHandler(_interfaceView.RefreshView);
                _interfaceView.Invoke(nh);

                OneArgHandler oh = new OneArgHandler(_interfaceView.SelectInterface);
                _interfaceView.Invoke(oh, new object[] { _gcInterface });
            }
        }

        public override void Refresh()
        {
            GCInterface gcInterface = _interfaceView.GetSelectedInterface();

            if (!IsInterfaceViewSelected() || gcInterface == null)
            {
                this._btnConfigInterface.Enabled =
                    this._btnExportConfig.Enabled =
                    this._btnImportConfig.Enabled =
                    this._btnMonitorInterface.Enabled =
                    this._btnStartInterface.Enabled =
                    this._btnStopInterface.Enabled =
                    this._btnUninstallInterface.Enabled = false;
            }
            else
            {
                this._btnConfigInterface.Enabled =
                    this._btnExportConfig.Enabled =
                    this._btnImportConfig.Enabled =
                    this._btnMonitorInterface.Enabled =
                    this._btnStartInterface.Enabled =
                    this._btnStopInterface.Enabled =
                    this._btnUninstallInterface.Enabled = true;

                switch (gcInterface.Status)
                {
                    case AdapterStatus.Running:
                        this._btnStartInterface.Enabled = false;
                        this._btnConfigInterface.Enabled = false;
                        this._btnUninstallInterface.Enabled = false;
                        this._btnImportConfig.Enabled = false;
                        break;
                    case AdapterStatus.Stopped:
                        this._btnStopInterface.Enabled = false;
                        break;
                }
            }

            if (_viewPanel.CurrentPage == _interfaceView)
            {
                if (gcInterface == null)
                {
                    base.ClearInfor();
                }
                else
                {
                    base.SetInfor("Selected interface : " + gcInterface.ToString());
                }
            }
        }
        private void SelectInterfaceView()
        {
            _viewPanel.GotoPage(_interfaceView);
        }
        private bool IsInterfaceViewSelected()
        {
            return (_viewPanel.CurrentPage == _interfaceView);
        }

        private void CreateUninstallScript()
        {
            string scriptFile = Application.StartupPath + "\\UninstallServices.bat";
            string interfacePath = Application.StartupPath + "\\" + Program.ConfigMgt.Config.InterfaceFolder;
            string[] folderList = Directory.GetDirectories(interfacePath);
            StringBuilder sb = new StringBuilder();
            foreach (string folder in folderList)
            {
                GCDevice device = FolderControl.LoadDevice(folder);
                if (device != null && device.Directory != null)
                {
                    Command[] clist = device.Directory.Commands.FindCommands(CommandType.Stop);
                    if (clist != null)
                    {
                        foreach (Command cmd in clist)
                        {
                            sb.Append("\"" + folder + "\\" + cmd.Path + "\"").Append(" ").AppendLine(cmd.Argument);
                        }
                    }
                    clist = device.Directory.Commands.FindCommands(CommandType.Uninstall);
                    if (clist != null)
                    {
                        foreach (Command cmd in clist)
                        {
                            sb.Append("\"" + folder + "\\" + cmd.Path + "\"").Append(" ").AppendLine(cmd.Argument);
                        }
                    }
                }

                string f = Path.GetFileName(folder);
                sb.Append("net stop ").AppendLine(f);
                sb.Append("sc delete ").AppendLine(f);
            }
            using (StreamWriter sw = File.CreateText(scriptFile))
            {
                sw.Write(sb.ToString());
            }
            Program.Log.Write("{Interface} Update uninstall script in " + scriptFile);
        }

        private void _viewPanel_OnPageRefresh(object sender, EventArgs e)
        {
            Refresh();
        }
        private void _interfaceView_SelectionChange(object sender, EventArgs e)
        {
            Refresh();
        }

        private void _btnImportConfig_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            ImportConfig();
        }
        private void _btnExportConfig_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            ExportConfig();
        }
        private void _btnConfigInterface_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            ConfigInterface();
        }
        private void _btnMonitorInterface_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            MonitorInterface();
        }
        private void _btnStopInterface_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            StopInterface();
        }
        private void _btnStartInterface_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            StartInterface();
        }
        private void _btnUninstallInterface_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            UninstallInterface();
        }
        private void _btnInstallInterface_Click(object sender, EventArgs e)
        {
            SelectInterfaceView();
            InstallInterface();
        }

    }
}
