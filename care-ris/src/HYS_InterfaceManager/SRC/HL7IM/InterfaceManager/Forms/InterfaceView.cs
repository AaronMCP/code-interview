using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.HL7IM.Manager.Config;
using HYS.HL7IM.Manager.Controler;
using HYS.Common.Objects.License2;
using CSH.eHeath.HL7Gateway.Manager;
using HYS.Common.Objects.Device;

namespace HYS.HL7IM.Manager.Forms
{
    public partial class InterfaceView : UserControl
    {
        public InterfaceView()
        {
            InitializeComponent();
            InitializeContextMenu();
            listCtrl = new ListViewControler(this.listViewInterface);
        }

        private InterfaceViewControler _viewControler;
        public void AttachViewControler(InterfaceViewControler viewControler)
        {
            _viewControler = viewControler;
        }

        private InterfaceViewControler controler;
        internal void SetControler(InterfaceViewControler ctl)
        {
            controler = ctl;
        }

        private void InitializeContextMenu()
        {
            this.browseFolderToolStripMenuItem.Click += new EventHandler(browseFolderToolStripMenuItem_Click);
            this.managementToolStripMenuItem.Click += new EventHandler(managementToolStripMenuItem_Click);
            this.uninstallToolStripMenuItem.Click += new EventHandler(uninstallToolStripMenuItem_Click);
        }

        private void managementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageInterface();
        }

        private void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UninstallInterface();
        }

        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void listViewInterface_MouseClick(object sender, MouseEventArgs e)
        {
            ShowContextMenu(e);
        }

        private void listViewInterface_DoubleClick(object sender, EventArgs e)
        {
            ManageInterface();
        }

        private void listViewInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnInterfaceSelectedChanged(e);
        }

        public event EventHandler InterfaceSelectedChanged;

        protected void OnInterfaceSelectedChanged(EventArgs e)
        {
            if (null != InterfaceSelectedChanged)
            {
                InterfaceSelectedChanged(this, e);
            }
        }

        #region User interface control

        private ListViewControler listCtrl;

        public void RefreshList(XCollection<HL7InterfaceConfig> ilist)
        {
            this.listViewInterface.Items.Clear();

            foreach (HL7InterfaceConfig hl7Interface in ilist)
            {
                ListViewItem i = this.listViewInterface.Items.Add(hl7Interface.InterfaceStatus.ToString());
                i.SubItems.Add(hl7Interface.InterfaceName);
                i.SubItems.Add(hl7Interface.InterfaceType.ToString());
                i.SubItems.Add(hl7Interface.InterfaceFolder);
                i.SubItems.Add(hl7Interface.InstallDate.ToShortDateString());
                i.Tag = hl7Interface;
            }

            listCtrl.Refresh();
        }

        public HL7InterfaceConfig GetSelectedInterface()
        {
            if (this.listViewInterface.SelectedItems.Count < 1) return null;
            return this.listViewInterface.SelectedItems[0].Tag as HL7InterfaceConfig;
        }

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
            listCtrl.GroupBy(2);
        }

        public void BrowseFolder()
        {
            HL7InterfaceConfig hlInterface = GetSelectedInterface();
            if (null == hlInterface)
            {
                MessageBoxHelper.ShowInformation("Please select the interface you want to browse.");
                return;
            }
            string path = Path.Combine(Application.StartupPath, hlInterface.InterfaceFolder);
            Process.Start("explorer.exe", path);
        }

        public void ManageInterface()
        {
            HL7InterfaceConfig config = GetSelectedInterface();
            if (null == config)
            {
                MessageBoxHelper.ShowInformation("Please select the interface you want to manage.");
                return;
            }

            string path = Path.Combine(Application.StartupPath, config.InterfaceFolder);
            string exeFile = Path.Combine(path,"Config\\HYS.IM.Config.exe");
            bool isRunning = false;
            Process[] ps = Process.GetProcessesByName("HYS.IM.Config");
            foreach (Process p in ps)
            {
                if (p.MainModule.FileName.Equals(exeFile,StringComparison.InvariantCultureIgnoreCase))
                {
                    Program.HandleRunningInstance(p);
                    isRunning = true;
                }
            }

            if (!isRunning)
            {
                Process.Start(exeFile);
            }
        }

        public void InstallInterface(InterfaceType type)
        {
            if (!CheckLicense(type))
            {
                return;
            }

            FormInstallInterface frm = new FormInstallInterface();
            frm.InterfaceType = type;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                this.RefreshView();
            }
        }

        private bool CheckLicense(InterfaceType type)
        {
            int icount = Program.ConfigMgt.Config.GetInterfaceCount(type); ;
            string strDeviceName;
            DeviceType dType;
            DirectionType directType;
            if (InterfaceType.Receiver == type)
            {
                strDeviceName = "HL7_RECEIVER";
                dType = DeviceType.HL7;
                directType = DirectionType.INBOUND;
            }
            else
            {
                strDeviceName = "HL7_SENDER";
                dType = DeviceType.HL7;
                directType = DirectionType.OUTBOUND;
            }
            DeviceLicense lic = Program.License.FindDevice
                       (strDeviceName, dType, directType);

            if (lic == null)
            {
                MessageBoxHelper.ShowInformation("Can not find licence for this type of interface.");
                return false;
            }

            int maxCount = lic.MaxInterfaceCount;
            if (maxCount == 0)
            {
                MessageBoxHelper.ShowInformation("You licence is disabled");
                return false;
            }

            if (lic.IsExpired(Program.License.Header.CreateDate))
            {
                MessageBoxHelper.ShowInformation("You licence is expired");
                return false;
            }

            if (icount >= maxCount)
            {
                MessageBoxHelper.ShowInformation("You interface number has reached the max count.");
                return false;
            }

            return true;
        }

        public void UninstallInterface()
        {
            HL7InterfaceConfig config = GetSelectedInterface();
            if (config == null)
            {
                MessageBoxHelper.ShowInformation("Select the interface you want to uninstall.");
                return;
            }

            if (!MessageBoxHelper.ShowConfirmBox("Are you sure to uninstall the selected interface?"))
            {
                return;
            }

            if (HL7GatewayInterfaceHelper.UninstallInterface(config))
            {
                this.RefreshView();
            }
        }

        private void ShowContextMenu(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && null != GetSelectedInterface())
            {
                this.contextMenuStripInterface.Show(this.listViewInterface, e.Location);
            }
        }

        #endregion

        internal void RefreshServiceStatus(string strServiceName, int status)
        {
            foreach (ListViewItem item in listViewInterface.Items)
            {
                HL7InterfaceConfig config = item.Tag as HL7InterfaceConfig;
                if (config.InterfaceName.Equals(strServiceName))
                {
                    config.InterfaceStatus = (InterfaceStatus)status;
                }

                item.SubItems[0].Text = config.InterfaceStatus.ToString();
            }
            listCtrl.Refresh();
        }
    }
}
