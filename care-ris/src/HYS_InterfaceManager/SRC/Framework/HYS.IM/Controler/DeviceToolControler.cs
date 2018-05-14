using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using HYS.IM;
using HYS.IM.Forms;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Config;
using HYS.Adapter.Base;
using System.IO;
using HYS.Common.Objects.Device;

namespace HYS.IM.Controler
{
    /// <summary>
    /// User interface controler class for Device Management Panel
    /// Act as "C" in the MVC design pattern.
    /// </summary>
    public class DeviceToolControler : ControlerBase
    {
        public DeviceToolControler(Form frm, DeviceView view, SliderPanel panel, GCDeviceManager deviceMgt, GCInterfaceManager interfaceMgt)
            : base( frm )
        {
            _deviceView = view;
            _viewPanel = panel;
            _deviceManager = deviceMgt;
            _interfaceMgt = interfaceMgt;
            if (_viewPanel==null||
                _deviceView==null||
                _deviceManager == null||
                _interfaceMgt == null) throw (new ArgumentNullException());

            Initialize();
        }           

        // "V" in MVC
        private DeviceView _deviceView;
        private SliderPanel _viewPanel;
        
        private DPanel _devicePanel;
        private DPanelButton _btnAddDevice;
        private DPanelButton _btnDeleteDevice;
        public DPanel DevicePanel
        {
            get { return _devicePanel; }
        }

        // "M" in MVC
        private GCDeviceManager _deviceManager;
        public GCDeviceManager DeviceManager
        {
            get { return _deviceManager; }
        }
        private GCInterfaceManager _interfaceMgt;
        public GCInterfaceManager InterfaceMgt
        {
            get { return _interfaceMgt; }
        }

        // Controler Logic
        private void Initialize()
        {
            _btnAddDevice = new DPanelButton();
            _btnAddDevice.Text = "Add Device";
            _btnAddDevice.Image = Properties.Resources.install;
            _btnAddDevice.Click += new EventHandler(_btnAddDevice_Click);

            _btnDeleteDevice = new DPanelButton();
            _btnDeleteDevice.Text = "Delete Device";
            _btnDeleteDevice.Image = Properties.Resources.delete;
            _btnDeleteDevice.Click += new EventHandler(_btnDeleteDevice_Click);

            _devicePanel = new DPanel(new DPanelButton[]{
                _btnAddDevice, _btnDeleteDevice });
            _devicePanel.Title.BackColor = Color.DarkGray;
            _devicePanel.Title.Text = "Devices Management";

            _viewPanel.OnPageRefresh += new EventHandler(_viewPanel_OnPageRefresh);
            _deviceView.SelectionChange += new EventHandler(_deviceView_SelectionChange);
        }

        public void AddDevice()
        {
            base.SetStatus("Adding device.");

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = false;
            dlg.Description = "Please select a folder which contains a device to be installed.";

            string path = Program.ConfigMgt.Config.RecentDeviceSelectionFolder;
            path = ConfigHelper.GetFullPath(path);
            if (path != null && path.Length > 0)
            {
                dlg.SelectedPath = path;
            }

            if (dlg.ShowDialog(frmMain) == DialogResult.OK)
            {
                string folderName = dlg.SelectedPath;

                string path1 = ConfigHelper.GetFullPath(folderName);
                string path2 = ConfigHelper.EnsurePathSlash(ConfigHelper.GetFullPath(_deviceManager.DevicesFolder));
                if (path1.IndexOf(path2) >= 0)
                {
                    MessageBox.Show(frmMain, "Cannot install a new device from an existing folder in the Device folder. Please select another folder instead.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    goto exit;
                }

                frmMain.Cursor = Cursors.WaitCursor;
                GCDevice device = _deviceManager.AddDevice(folderName);
                frmMain.Cursor = Cursors.Default;

                if (device != null)
                {
                    Program.Log.Write("{Device} Add device (" + device.ToString() + ") succeed : " + device.FolderPath);

                    _deviceView.RefreshView();
                    _deviceView.SelectDevice(device);
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "{Device} Add device failed : " + GCError.LastErrorInfor);
                    if (GCError.LastError != null) Program.Log.Write(LogType.Error, GCError.LastError.ToString());

                    MessageBox.Show(frmMain, "Add device failed.\r\n\r\n" + GCError.LastErrorInfor,
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            Program.ConfigMgt.Config.RecentDeviceSelectionFolder = dlg.SelectedPath;
            Program.ConfigMgt.Save();

        exit:
            base.ClearStatus();
        }
        public void DeleteDevice()
        {
            GCDevice device = _deviceView.GetSelectedDevice();
            if (device == null) return;

            base.SetStatus("Deleting device.");

            if (_interfaceMgt.HasInterface(device.DeviceID))
            {
                MessageBox.Show(frmMain, "There are some interface(s) based on this device. Please delete the device after uninstalling these interface(s).", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show(frmMain, "Device can not be restored after delete. Are you sure to delete device : "
                    + device.ToString() + " ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    frmMain.Cursor = Cursors.WaitCursor;
                    bool result = _deviceManager.DeleteDevice(device);
                    frmMain.Cursor = Cursors.Default;

                    if (result)
                    {
                        Program.Log.Write("{Device} Delete device (" + device.ToString() + ") succeed : " + device.FolderPath);

                        _deviceView.RefreshView();
                    }
                    else
                    {
                        Program.Log.Write(LogType.Warning, "{Device} Delete device (" + device.ToString() + ") failed : " + GCError.LastErrorInfor);
                        Program.Log.Write(GCError.LastError);

                        MessageBox.Show(frmMain, "Delete device (" + device.ToString() + ") failed.\r\n\r\n" + GCError.LastErrorInfor,
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            base.ClearStatus();
        }

        public void UpdateInterfaceByDevice()
        {
            GCDevice device = _deviceView.GetSelectedDevice();
            if (device == null) return;

            base.SetStatus("Updating interfaces by device.");

            if (!_interfaceMgt.HasInterface(device.DeviceID))
            {
                MessageBox.Show(frmMain, "There is no interface based on this device.", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                goto exit;
            }

            GCInterfaceCollection iList = _interfaceMgt.QueryInterfaceList(false);
            GCInterfaceCollection ulist = new GCInterfaceCollection();
            StringBuilder sbNameList = new StringBuilder();
            foreach (GCInterface i in iList)
            {
                if (i.Device.DeviceID != device.DeviceID) continue;
                sbNameList.Append(i.InterfaceName + ",");
                ulist.Add(i);
            }

            if (MessageBox.Show(frmMain, "The following interface(s) will be stopped, and the executable files of the interface(s) will be over-written. Do you want to continue?\r\n\r\n"
                + sbNameList.ToString().TrimEnd(','), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                goto exit;

            frmMain.Cursor = Cursors.WaitCursor;
            foreach (GCInterface i in ulist)
            {
                // stop interfaces
                Program.Log.Write(string.Format("Stopping interface {0}.", i.InterfaceName));
                _interfaceMgt.StopInterface(i);

                // copy files (*.exe, *.dll), not including sub folder
                string iFolder = ConfigHelper.GetFullPath(Path.Combine(_interfaceMgt.InterfacesFolder, i.InterfaceName));
                string dFolder = ConfigHelper.GetFullPath(device.FolderPath);
                string[] fList = Directory.GetFiles(dFolder, "*.exe");
                foreach (string fName in fList)
                {
                    string fn = Path.GetFileName(fName);
                    string fromPath = Path.Combine(dFolder, fn);
                    string toPath = Path.Combine(iFolder, fn);
                    Program.Log.Write(string.Format("Copy {0} to {1}", fromPath, toPath));
                    File.Copy(fromPath, toPath, true);
                }
                fList = Directory.GetFiles(dFolder, "*.dll");
                foreach (string fName in fList)
                {
                    string fn = Path.GetFileName(fName);
                    string fromPath = Path.Combine(dFolder, fn);
                    string toPath = Path.Combine(iFolder, fn);
                    Program.Log.Write(string.Format("Copy {0} to {1}", fromPath, toPath));
                    File.Copy(fromPath, toPath, true);
                }

                // modify DeviceDir (four exe from HYS to CSH.HYSIM)
                string dirFilePath = Path.Combine(iFolder, DeviceDirManager.IndexFileName);
                Program.Log.Write(string.Format("Updating file {0}.", dirFilePath));
                string dirFileContent = File.ReadAllText(dirFilePath);
                dirFileContent = dirFileContent.Replace("HYS.Adapter.Composer.exe", "HYS.IM.Adapter.Composer.exe");
                dirFileContent = dirFileContent.Replace("HYS.Adapter.Config.exe", "HYS.IM.Adapter.Config.exe");
                dirFileContent = dirFileContent.Replace("HYS.Adapter.Monitor.exe", "HYS.IM.Adapter.Monitor.exe");
                dirFileContent = dirFileContent.Replace("HYS.Adapter.Service.exe", "HYS.IM.Adapter.Service.exe");
                File.WriteAllText(dirFilePath, dirFileContent);
            }
            frmMain.Cursor = Cursors.Default;

            MessageBox.Show(frmMain, "Updating interface(s) success. Please switch to the Interface View and click the Refresh button to apply the change.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        exit:
            base.ClearStatus();
        }

        public override void Refresh()
        {
            GCDevice device = _deviceView.GetSelectedDevice();

            _btnDeleteDevice.Enabled = IsDeviceViewSelected() && (device != null);

            if (_viewPanel.CurrentPage == _deviceView)
            {
                if (device == null)
                {
                    base.ClearInfor();
                }
                else
                {
                    base.SetInfor("Selected device : " + device.ToString());
                }
            }
        }
        private void SelectDeviceView()
        {
            _viewPanel.GotoPage(_deviceView);
        }
        private bool IsDeviceViewSelected()
        {
            return (_viewPanel.CurrentPage == _deviceView);
        }

        private void _deviceView_SelectionChange(object sender, EventArgs e)
        {
            Refresh();
        }
        private void _viewPanel_OnPageRefresh(object sender, EventArgs e)
        {
            Refresh();
        }

        private void _btnDeleteDevice_Click(object sender, EventArgs e)
        {
            SelectDeviceView();
            DeleteDevice();
        }
        private void _btnAddDevice_Click(object sender, EventArgs e)
        {
            SelectDeviceView();
            AddDevice();
        }
    }
}
