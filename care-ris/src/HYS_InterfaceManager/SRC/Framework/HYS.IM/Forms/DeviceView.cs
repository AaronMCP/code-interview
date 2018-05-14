using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Controler;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.DataControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Device;

namespace HYS.IM.Forms
{
    public partial class DeviceView : UserControl, IView, IPage
    {
        public DeviceView()
        {
            InitializeComponent();
            InitializeContextMenu();
            listCtrl = new ListViewControler(this.listViewDevice);
        }

        #region Context menu control

        private DeviceViewControler _viewControler;
        private DeviceToolControler _toolControler;
        public void AttachViewControler(DeviceViewControler viewControler)
        {
            _viewControler = viewControler;
        }
        public void AttachToolControler(DeviceToolControler toolControler)
        {
            _toolControler = toolControler;
        }

        private void InitializeContextMenu()
        {
            this.deleteToolStripMenuItem.Click += new EventHandler(deleteToolStripMenuItem_Click);
            this.refreshListToolStripMenuItem.Click += new EventHandler(refreshListToolStripMenuItem_Click);
            this.browseFolderToolStripMenuItem.Click += new EventHandler(browseFolderToolStripMenuItem_Click);
            this.updateInterfaceToolStripMenuItem.Click += new EventHandler(updateInterfaceToolStripMenuItem_Click);
        }

        private void updateInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.UpdateInterfaceByDevice();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.DeleteDevice();
        }
        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_viewControler != null) _viewControler.Refresh();
        }
        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }
        private void listViewDevice_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.deleteToolStripMenuItem.Enabled =
                this.browseFolderToolStripMenuItem.Enabled = GetSelectedDevice() != null;
                this.contextMenuStripDevice.Show(this.listViewDevice, e.Location);
            }
        }

        #endregion

        #region User interface control

        private ListViewControler listCtrl;
        private GCDeviceCollection deviceList;
        private void RefreshDetail(GCDevice device)
        {
            this.labelName.Text = "";
            this.labelType.Text = "";
            this.labelVersion.Text = "";
            this.labelDirection.Text = "";
            this.labelDescription.Text = "";
            this.listViewFile.Items.Clear();
            this.listViewCommand.Items.Clear();
            if (device == null) return;

            DeviceDir dir = device.Directory;
            if (dir == null)
            {
                MessageBox.Show(this, "Invalid device index file in : " + device.FolderPath, 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.labelName.Text = dir.Header.Name;
            this.labelType.Text = dir.Header.Type.ToString();
            this.labelVersion.Text = dir.Header.Version;
            this.labelDirection.Text = dir.Header.Direction.ToString();
            this.labelDescription.Text = dir.Header.Description;

            int count = 1;
            foreach (DeviceFile f in dir.Files)
            {
                ListViewItem i = this.listViewFile.Items.Add((count++).ToString());
                i.SubItems.Add(f.Location);
                i.SubItems.Add(f.Type.ToString());
                i.SubItems.Add(f.Description);
                i.SubItems.Add(f.Backupable.ToString());
                i.Tag = f;
            }

            count = 1;
            foreach (Command c in dir.Commands)
            {
                ListViewItem i = this.listViewCommand.Items.Add((count++).ToString());
                i.SubItems.Add(c.Type.ToString());
                i.SubItems.Add(c.Path);
                i.SubItems.Add(c.Argument);
                i.SubItems.Add(c.Description);
                i.Tag = c;
            }
        }
        public void RefreshList(GCDeviceCollection dlist)
        {
            RefreshDetail(null);
            this.listViewDevice.Items.Clear();
            
            deviceList = dlist;
            if (deviceList == null) return;

            foreach (object o in deviceList)
            {
                GCDeviceAgent d = o as GCDeviceAgent;
                if (d == null) continue;

                DeviceRec dr = d.DeviceRec;
                ListViewItem i = this.listViewDevice.Items.Add(dr.ID.ToString());
                i.SubItems.Add(dr.Name);
                i.SubItems.Add(DataHelper.GetDirectionName(dr.Direction));
                i.SubItems.Add(DataHelper.GetTypeName(dr.Type));
                i.SubItems.Add(dr.Folder);
                i.Tag = d;
            }

            NotifySelectionChange(this, EventArgs.Empty);
            listCtrl.Refresh();
        }
        public void SelectDevice(GCDevice device)
        {
            if( device == null ) return;
            foreach (ListViewItem i in this.listViewDevice.Items)
            {
                GCDevice d = i.Tag as GCDevice;
                if (d != null && d.DeviceID == device.DeviceID)
                {
                    i.Selected = true;
                    return;
                }
            }
        }
        public GCDevice GetSelectedDevice()
        {
            if (this.listViewDevice.SelectedItems.Count < 1) return null;
            return this.listViewDevice.SelectedItems[0].Tag as GCDevice;
        }
        
        private void listViewDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotifySelectionChange(sender, e);
            if (this.listViewDevice.SelectedItems.Count > 0)
            {
                RefreshDetail(this.listViewDevice.SelectedItems[0].Tag as GCDevice);
            }
            else
            {
                RefreshDetail(null);
            }
        }

        #endregion

        #region IView Members

        public void HideGroup()
        {
            listCtrl.HideGroup();
        }

        public void RefreshView()
        {
            if (controler != null) controler.Refresh();
        }

        public void GroupByType()
        {
            listCtrl.GroupBy(3);
        }

        public void BrowseFolder()
        {
            GCDevice device = GetSelectedDevice();
            if (device != null) FolderControl.BrowseFolder(device.FolderPath);
        }

        public void GroupByDirection()
        {
            listCtrl.GroupBy(2);
        }

        public event EventHandler SelectionChange;
        private void NotifySelectionChange(object sender, EventArgs e)
        {
            if (SelectionChange != null) SelectionChange(sender, e);
        }

        private DeviceViewControler controler;
        internal void SetControler(DeviceViewControler ctl)
        {
            controler = ctl;
        }

        public IDevice SelectedItem
        {
            get { return GetSelectedDevice(); }
        }

        #endregion

        #region IPage Members

        public Control GetControl()
        {
            return this;
        }

        public event PageEventHandler MoveNext;

        public event PageEventHandler MovePrev;

        private void NotifyMoveNext()
        {
            if (MoveNext != null) MoveNext(this);
        }
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
