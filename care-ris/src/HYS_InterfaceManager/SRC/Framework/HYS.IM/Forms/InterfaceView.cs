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
using HYS.Common.DataAccess;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Forms
{
    public partial class InterfaceView : UserControl, IView, IPage
    {
        public InterfaceView()
        {
            InitializeComponent();
            InitializeContextMenu();
            listCtrl = new ListViewControler(this.listViewInterface);
            combinationMgt = new CombinationRecManager(Program.ConfigDB);
        }

        #region Adapter message handler

        public void HandleMessage(AdapterMessage msg, bool updateWindow)
        {
            if (msg == null) return;
            Program.Log.Write("{Adapter} Received message (" + msg.ToString() + ") from adapter.");

            AdapterMessageHandler dlg = new AdapterMessageHandler(_HandleMessage);
            this.Invoke(dlg, new object[] { msg, updateWindow });
        }

        private void _HandleMessage(AdapterMessage msg, bool updateWindow)
        {
            Program.Log.Write("{Adapter} Handling message (" + msg.ToString() + ") from adapter.");

            foreach (ListViewItem i in this.listViewInterface.Items)
            {
                GCInterface d = i.Tag as GCInterface;
                if (d != null && d.InterfaceID == msg.InterfaceID)
                {
                    d.Status = msg.Status;
                    
                    if (i.SubItems.Count > 0) i.SubItems[0].Text = msg.Status.ToString();

                    Program.Log.Write("{Adapter} Handle message (" + msg.ToString() + ") from adapter succeeded.");

                    if (updateWindow && i.Selected)
                    {
                        NotifySelectionChange(this.listViewInterface, EventArgs.Empty);
                        Program.Log.Write("{Adapter} Update window by (" + msg.ToString() + ") succeeded.");
                    }
                    
                    return;
                }
            }
        }

        private delegate void AdapterMessageHandler(AdapterMessage msg, bool updateWindow);

        #endregion

        private InterfaceViewControler _viewControler;
        private InterfaceToolControler _toolControler;
        public void AttachViewControler(InterfaceViewControler viewControler)
        {
            _viewControler = viewControler;
        }
        public void AttachToolControler(InterfaceToolControler toolControler)
        {
            _toolControler = toolControler;
        }

        private void InitializeContextMenu()
        {
            this.refreshListToolStripMenuItem.Click += new EventHandler(refreshListToolStripMenuItem_Click);
            this.browseFolderToolStripMenuItem.Click += new EventHandler(browseFolderToolStripMenuItem_Click);
            this.monitorToolStripMenuItem.Click += new EventHandler(monitorToolStripMenuItem_Click);
            this.startToolStripMenuItem.Click += new EventHandler(startToolStripMenuItem_Click);
            this.stopToolStripMenuItem.Click += new EventHandler(stopToolStripMenuItem_Click);
            this.configToolStripMenuItem.Click += new EventHandler(configToolStripMenuItem_Click);
            this.importToolStripMenuItem.Click += new EventHandler(importToolStripMenuItem_Click);
            this.exportToolStripMenuItem.Click += new EventHandler(exportToolStripMenuItem_Click);
            this.uninstallToolStripMenuItem.Click += new EventHandler(uninstallToolStripMenuItem_Click);
        }
        private void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.UninstallInterface();
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.CopyInterface();
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.ExportConfig();
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.ImportConfig();
        }
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.ConfigInterface();
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.StopInterface();
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.StartInterface();
        }
        private void monitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toolControler != null) _toolControler.MonitorInterface();
        }
        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }
        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_viewControler != null) _viewControler.Refresh();
        }
        private void listViewInterface_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GCInterface gcInterface = GetSelectedInterface();

                this.browseFolderToolStripMenuItem.Enabled =
                    this.monitorToolStripMenuItem.Enabled =
                    this.exportToolStripMenuItem.Enabled = gcInterface != null;

                this.startToolStripMenuItem.Enabled =
                    this.configToolStripMenuItem.Enabled =
                    this.importToolStripMenuItem.Enabled =
                    this.uninstallToolStripMenuItem.Enabled = (gcInterface != null && gcInterface.Status == AdapterStatus.Stopped);

                this.stopToolStripMenuItem.Enabled = (gcInterface != null && gcInterface.Status == AdapterStatus.Running);

                if (gcInterface.Status == AdapterStatus.Unknown && gcInterface.Directory != null)
                {
                    this.configToolStripMenuItem.Enabled =
                    this.importToolStripMenuItem.Enabled = true;
                }
                
                this.contextMenuStripInterface.Show(this.listViewInterface, e.Location);
            }
        }

        #region User interface control

        private ListViewControler listCtrl;
        private GCInterfaceCollection interfaceList;
        private CombinationRecManager combinationMgt;
        private void RefreshDetail(GCInterface gcInterface)
        {
            try 
            { 
            this.labelName.Text = "";
            this.labelType.Text = "";
            this.labelDevice.Text = "";
            this.labelVersion.Text = "";
            this.labelDirection.Text = "";
            this.labelDescription.Text = "";
            this.listViewCombination.Items.Clear();
            if (gcInterface == null) return;

            DeviceDir dir = gcInterface.Directory;
            if (dir == null)
            {
                MessageBox.Show(this, "Invalid device index file in : " + gcInterface.FolderPath,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string deviceInfor = "";
            InterfaceRec rec = gcInterface.InterfaceRec;
            if (rec != null) deviceInfor = /*rec.DeviceID + " " + */rec.DeviceName;

            this.labelDevice.Text = deviceInfor;
            this.labelName.Text = dir.Header.Name;
            this.labelType.Text = dir.Header.Type.ToString();
            this.labelVersion.Text = dir.Header.Version;
            this.labelDirection.Text = dir.Header.Direction.ToString();
            //this.labelDescription.Text = dir.Header.Description;

            string desc = dir.Header.Description;
            if (desc.Length < 1)
            {
                desc = dir.Header.ConfigurationSummary;
            }
            else
            {
                desc += " (" + dir.Header.ConfigurationSummary + ")";
            }
            this.labelDescription.Text = desc;

            DObjectCollection olist = combinationMgt.GetCombinedInterfaces(dir.Header.Name, dir.Header.Direction);
            this.listViewCombination.Items.Clear();
            if (olist == null)
            {
                MessageBox.Show(this, "Access combination table failed.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = 1;
            foreach (InterfaceRec o in olist)
            {
                ListViewItem item = this.listViewCombination.Items.Add((index++).ToString());
                item.SubItems.Add(o.Name);
                item.SubItems.Add(DataHelper.GetDirection(o.Direction).ToString());
                item.SubItems.Add(DataHelper.GetType(o.Type).ToString());
                item.SubItems.Add(o.Description);
                item.Tag = o;
            }
           }
            catch(Exception ex)
            {
                
            
            }
        }
        public void RefreshList(GCInterfaceCollection ilist)
        {
            RefreshDetail(null);
            this.listViewInterface.Items.Clear();

            interfaceList = ilist;
            if (interfaceList == null) return;

            bool res = false;
            foreach (GCInterface gcInterface in interfaceList)
            {
                InterfaceRec ic = gcInterface.InterfaceRec;
                string statusInfor = gcInterface.Status.ToString();
                if (gcInterface.Status == AdapterStatus.Unknown) res = true;
                ListViewItem i = this.listViewInterface.Items.Add(statusInfor);
                i.SubItems.Add(ic.Name);
                i.SubItems.Add(ic.DeviceName);
                i.SubItems.Add(DataHelper.GetDirectionName(ic.Direction));
                i.SubItems.Add(DataHelper.GetTypeName(ic.Type));
                i.SubItems.Add(ic.Folder);
                i.SubItems.Add(ic.LastBackupDateTime);
                i.SubItems.Add(ic.LastBackupDir);
                i.Tag = gcInterface;
            }

            if (res)
            {
                Program.Log.Write(LogType.Warning, "{Interface} Some interface in the list don't have a NT service.");
                Program.Log.Write(LogType.Error, GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);
            }

            NotifySelectionChange(this, EventArgs.Empty);
            listCtrl.Refresh();
        }
        public void SelectInterface(GCInterface gcInterface)
        {
            if (gcInterface == null) return;
            foreach (ListViewItem i in this.listViewInterface.Items)
            {
                GCInterface d = i.Tag as GCInterface;
                if (d != null && d.InterfaceID == gcInterface.InterfaceID)
                {
                    i.Selected = true;
                    return;
                }
            }
        }
        public GCInterface GetSelectedInterface()
        {
            if (this.listViewInterface.SelectedItems.Count < 1) return null;
            return this.listViewInterface.SelectedItems[0].Tag as GCInterface;
        }

        private void listViewInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotifySelectionChange(sender, e);
            if (this.listViewInterface.SelectedItems.Count > 0)
            {
                RefreshDetail(this.listViewInterface.SelectedItems[0].Tag as GCInterface);
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
            listCtrl.GroupBy(4);    //3
        }

        public void BrowseFolder()
        {
            GCInterface device = GetSelectedInterface();
            if (device != null) FolderControl.BrowseFolder(device.FolderPath);
        }

        public void GroupByDirection()
        {
            listCtrl.GroupBy(3);    //2
        }

        public event EventHandler SelectionChange;
        private void NotifySelectionChange(object sender, EventArgs e)
        {
            if (SelectionChange != null) SelectionChange(sender, e);
        }

        private InterfaceViewControler controler;
        internal void SetControler(InterfaceViewControler ctl)
        {
            controler = ctl;
        }

        public IDevice SelectedItem
        {
            get { return GetSelectedInterface(); }
        }

        #endregion

        #region IPage Members

        public Control GetControl()
        {
            return this;
        }

        public event PageEventHandler MoveNext;

        public event PageEventHandler MovePrev;

        public event PageEventHandler CloseAll;

        private void NotifyMoveNext()
        {
            if (MoveNext != null) MoveNext(this);
        }
        private void NotifyMovePrev()
        {
            if (MovePrev != null) MovePrev(this);
        }
        private void NotifyCloseAll()
        {
            if (CloseAll != null) CloseAll(this);
        }

        #endregion
    }
}
