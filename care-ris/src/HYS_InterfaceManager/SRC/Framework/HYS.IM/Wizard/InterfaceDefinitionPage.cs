using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.DataControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Device;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Wizard
{
    /// <summary>
    /// user enter a interface name
    /// 
    /// press next
    ///     - check duplicated name
    ///     - copy files
    ///     - write interface name into DeviceDir file
    /// 
    /// rowback to DeviceSelectionPage: delete files
    /// </summary>
    public partial class InterfaceDefinitionPage : UserControl, IPage 
    {
        public InterfaceDefinitionPage()
        {
            InitializeComponent();
            InitializeControl();
        }

        public void SetSourceDevice(GCDevice device)
        {
            _sourceDevice = device;
            RefreshDeviceInformation();
            RefreshControl();
        }
        public GCInterface GetTargetInterface()
        {
            return _targetInterface;
        }
        public void GoCancel()
        {
            GoBack();
        }

        #region private functions

        private GCDevice _sourceDevice;
        private void RefreshDeviceInformation()
        {
            this.labelName.Text =
                this.labelType.Text =
                this.labelVersion.Text =
                this.labelDirection.Text =
                this.labelDescription.Text = "";

            if (_sourceDevice == null) return;

            DeviceDir dir = _sourceDevice.Directory;
            if (dir == null)
            {
                MessageBox.Show(this, "Invalid device index file in : " + _sourceDevice.FolderPath,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _sourceDevice = null;
            }

            if (dir != null)
            {
                this.labelName.Text = dir.Header.Name;
                this.labelVersion.Text = dir.Header.Version;
                this.labelType.Text = dir.Header.Type.ToString();
                this.labelDirection.Text = dir.Header.Direction.ToString();
                this.labelDescription.Text = dir.Header.Description;
                this.textBoxInterfaceName.Focus();
            }
        }
        private void RefreshControl()
        {
            this.buttonNext.Enabled =
                (_sourceDevice != null &&
                this.textBoxInterfaceName.Text.Trim().Length > 0);
        }

        private GCInterface _targetInterface;
        private GCInterfaceManager _interfaceMgt;
        private ProgressListener _progressListener;
        
        private void InitializeControl()
        {
            _interfaceMgt = new GCInterfaceManager(Program.ConfigDB, Program.ConfigMgt.Config.InterfaceFolder);
            _progressListener = new ProgressListener(this.labelProcess, this.progressBarProcess);
            _progressListener.AttachProgress(_interfaceMgt);
            panelProcess.Visible = false;
        }
        private bool CreateInterfaceFolder()
        {
            if (_targetInterface != null) return false;

            string name = this.textBoxInterfaceName.Text.Trim();
            string desc = this.textBoxInterfaceDescription.Text.Trim();

            if (!CheckInterfaceName(name)) return false;

            panelProcess.Visible = true;
            this.buttonNext.Enabled = this.buttonPrev.Enabled = this.buttonCancel.Enabled = false;

            bool result = false;
            _targetInterface = _interfaceMgt.AddInterfaceToFolder(_sourceDevice, name, desc);

            if (_targetInterface != null)
            {
                Program.Log.Write("{Interface} Create interface folder succeed : " + _targetInterface.FolderPath);

                if (ScriptControl.UpdateInterface(_targetInterface, Program.ConfigMgt.Config))
                {
                    Program.Log.Write("{Interface} Update interface scripts succeed : " + _targetInterface.FolderPath);
                    
                    result = true;
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "{Interface} Update interface scripts failed : " + GCError.LastErrorInfor);
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Update interface scripts failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} Create interface folder failed : " + GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(this, "Create interface folder failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.buttonNext.Enabled = this.buttonPrev.Enabled = this.buttonCancel.Enabled = true;
            panelProcess.Visible = false;

            return result;
        }
        private void DeleteInterfaceFolder()
        {
            if (_targetInterface == null) return;

            if (_interfaceMgt.DeleteInterfaceFromFolder(_targetInterface))
            {
                _targetInterface = null;
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} Delete folder " + _targetInterface.FolderPath + " failed : " + GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(this, "Delete folder " + _targetInterface.FolderPath + " failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool CheckInterfaceName(string name)
        {
            this.Cursor = Cursors.WaitCursor;
            bool result = _interfaceMgt.IsValidInterfaceName(name);
            this.Cursor = Cursors.Default;

            if (!result)
            {
                MessageBox.Show(this, "The interface name should only contains charactor or number or '_', and should begins with charactor, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textBoxInterfaceName.Focus();
                return false;
            }
            
            this.Cursor = Cursors.WaitCursor;
            result = _interfaceMgt.HasSampleInterfaceName(name);
            this.Cursor = Cursors.Default;

            if (result)
            {
                MessageBox.Show(this, "The interface name \"" + name + "\" is duplicated, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textBoxInterfaceName.Focus();
                return false;
            }

            return true;
        }

        private bool GoNext()
        {
            if (_targetInterface == null)
            {
                return CreateInterfaceFolder();
            }
            else
            {
                DeleteInterfaceFolder();
                return CreateInterfaceFolder();// UpdateInterfaceFolder();
            }
        }
        private void GoBack()
        {
            if (_targetInterface != null) DeleteInterfaceFolder();
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
            if (GoNext()) NotifyMoveNext();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NotifyCloseAll();
        }

        private void textBoxInterfaceName_TextChanged(object sender, EventArgs e)
        {
            RefreshControl();
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
