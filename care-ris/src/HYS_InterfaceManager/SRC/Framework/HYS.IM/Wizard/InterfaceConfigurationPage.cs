using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.DataControl;
using HYS.Common.Objects.Device;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Wizard
{
    /// <summary>
    /// run config process
    /// run installation process
    /// 
    /// check windows service status to know if the installation is succeeded
    /// 
    /// succeeded -> press complete : 
    ///     - insert interface information into database
    ///     - auto start windows service (option)
    /// 
    /// failed -> press retry -> ...
    ///        -> press cancel : delete files
    /// </summary>
    public partial class InterfaceConfigurationPage : UserControl, IPage
    {
        public InterfaceConfigurationPage()
        {
            InitializeComponent();
            InitializeControl();
        }

        public void SetDefinedInterface(GCInterface gcInterface)
        {
            _definedInterface = gcInterface;
            RefreshInterfaceInformation();
        }
        public bool IsInstallationCompleted()
        {
            return _addedtodatabase && _installed;
        }
        public void GoCanel()
        {
            GoBack();
        }

        #region private functions

        private Process _cfgP;
        private void EnableMe()
        {
            EnableHandler dlg = new EnableHandler(_EnableMe);
            this.Invoke(dlg);
        }
        private void _EnableMe()
        {
            this.Enabled = true;
            Form frm = this.ParentForm;
            if (frm != null) frm.ControlBox = true;
        }
        private void DisableMe()
        {
            this.Enabled = false;
            Form frm = this.ParentForm;
            if (frm != null) frm.ControlBox = false;
        }
        private ManualResetEvent waitEvent;
        private delegate void EnableHandler();
        private void _cfgP_Exited(object sender, EventArgs e)
        {
            if(waitEvent != null) waitEvent.Set();
            waitEvent = null;
            EnableMe();
        }
        public void ActivateConfig()
        {
            if (_cfgP == null || _cfgP.HasExited) return;
            Win32Api.ShowWindow(_cfgP.MainWindowHandle, Win32Api.SW_SHOW);
            Win32Api.PostMessage(_cfgP.MainWindowHandle, Win32Api.SW_SHOW, 0, 0);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void KillConfig()
        {
            if (_cfgP == null || _cfgP.HasExited) return;

            if (waitEvent == null)
            {
                waitEvent = new ManualResetEvent(false);
                waitEvent.Reset();
            }

            _cfgP.Kill();

            if( waitEvent != null )
                waitEvent.WaitOne(20000, true);

            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(500);
            //    if (_cfgP.HasExited) return;
            //}
        }

        private bool _installed;
        private bool _addedtodatabase;
        private GCInterface _definedInterface;
        private GCInterfaceManager _interfaceMgt;
        private ProgressListener _progressListener;

        private void InitializeControl()
        {
            _interfaceMgt = new GCInterfaceManager(Program.ConfigDB, Program.ConfigMgt.Config.InterfaceFolder);
            _progressListener = new ProgressListener(this.labelProcess, this.progressBarProcess);
            _progressListener.AttachProgress(_interfaceMgt);
            panelProcess.Visible = false;
        }
        private void RefreshInterfaceInformation()
        {
            this.labelName.Text =
                this.labelType.Text =
                this.labelDevice.Text =
                this.labelDirection.Text =
                this.labelDescription.Text = "";

            if (_definedInterface == null) return;

            GCDevice device = _definedInterface.Device;
            DeviceDir dir = _definedInterface.Directory;
            if (dir == null || device == null)
            {
                MessageBox.Show(this, "Invalid device index file in interface folder : " + _definedInterface.FolderPath,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _definedInterface = null;
            }

            this.labelName.Text = dir.Header.Name;
            this.labelDevice.Text = device.ToString();
            this.labelType.Text = dir.Header.Type.ToString();
            this.labelDirection.Text = dir.Header.Direction.ToString();
            this.labelDescription.Text = dir.Header.Description;

            if (Program.ConfigMgt.Config.ShowConfigWhenInterfaceInstall)
            {
                ShowConfig();
            }
        }
        private bool PreformInstallation()
        {
            if( _definedInterface == null ) return false;

            this.Cursor = Cursors.WaitCursor;

            if (!_installed)
            {
                if (!_interfaceMgt.InstallInterface(_definedInterface))
                {
                    this.Cursor = Cursors.Default;

                    Program.Log.Write(LogType.Warning, "{Interface} Register NT service failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Register NT service failed.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
                else
                {
                    _installed = true;
                }
            }

            if (!_addedtodatabase)
            {
                if (!_interfaceMgt.AddInterfaceToDatabase(_definedInterface))
                {
                    this.Cursor = Cursors.Default;

                    Program.Log.Write(LogType.Warning, "{Interface} Insert interface to database failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Insert interface to database failed.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
                else
                {
                    _addedtodatabase = true;
                }
            }

            if (_definedInterface.InterfaceRec.Direction == "I" ||
                _definedInterface.InterfaceRec.Direction == "O" ||
                _definedInterface.InterfaceRec.DeviceName == "DAP_INOUT")
            {
                if (!_interfaceMgt.RunDBInstallScript(_definedInterface))
                {
                    this.Cursor = Cursors.Default;

                    Program.Log.Write(LogType.Warning, "{Interface} Run DB install script failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Run DB install script failed.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return true;        // user cannot go back or cancel installation process now // 20061111
                }
            }

            if (checkBoxAutoStart.Checked)
            {
                if (!_interfaceMgt.StartInterface(_definedInterface))
                {
                    this.Cursor = Cursors.Default;

                    Program.Log.Write(LogType.Warning, "{Interface} Start interface failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Installation is completed, but the interface can not be started. You can modify the configuration and try to start it again in the interface view.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return true;    // user cannot go back or cancel installation process now // 20061111
                }
            }

            this.Cursor = Cursors.Default;
            return true;
        }
        private void CanelInstallation()
        {
            KillConfig();
            if (_installed)
            {
                if (!_interfaceMgt.UninstallInterface(_definedInterface))
                {
                    Program.Log.Write(LogType.Warning, "{Interface} Unregister NT service failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Unregister NT service failed.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _installed = false;
                }
            }

            if (_addedtodatabase)
            {
                if (!_interfaceMgt.DeleteInterfaceFromDatabase(_definedInterface))
                {
                    Program.Log.Write(LogType.Warning, "{Interface} Delete interface from database failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Delete interface from database failed.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _addedtodatabase = false;
                }
            }
        }
        private void ShowConfig()
        {
            if (!_interfaceMgt.ConfigInterface(_definedInterface))
            {
                Program.Log.Write(LogType.Warning, "{Interface} Config interface : " + GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(this, "Config interface failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _cfgP = HYS.IM.BusinessControl.SystemControl.ProcessControl._process;
            if (_cfgP != null)
            {
                _cfgP.EnableRaisingEvents = true;
                _cfgP.Exited += new EventHandler(_cfgP_Exited);
                DisableMe();
            }
        }

        private void GoComplete()
        {
            this.buttonPrev.Enabled = this.buttonNext.Enabled = this.buttonCancel.Enabled = false;

            bool result = PreformInstallation();

            if (result)
            {
                NotifyCloseAll();
            }
            else
            {
                this.buttonPrev.Enabled = this.buttonNext.Enabled = this.buttonCancel.Enabled = true;
            }
        }
        private void GoBack()
        {
            CanelInstallation();
        }

        #endregion

        #region event handlers

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            GoBack();
            NotifyMovePrev();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            GoComplete();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NotifyCloseAll();
        }

        private void buttonConfiguration_Click(object sender, EventArgs e)
        {
            ShowConfig();
        }

        #endregion

        #region IPage Members

        public Control GetControl()
        {
            return this;
        }

        public event PageEventHandler MoveNext;
        private void NotifyMoveNext()
        {
            if (MoveNext != null) MoveNext(this);
        }

        public event PageEventHandler MovePrev;
        private void NotifyMovePrev()
        {
            if (MovePrev != null) MovePrev(this);
        }

        public event PageEventHandler CloseAll;
        private void NotifyCloseAll()
        {
            if (CloseAll != null) CloseAll(this);
        }

        #endregion
    }
}
