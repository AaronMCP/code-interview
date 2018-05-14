using System;
using System.Diagnostics;
using System.Windows.Forms;
using HYS.Common.Objects.Config;
using CSH.eHeath.HL7Gateway.Manager;
using HYS.HL7IM.Manager.Controler;
using HYS.HL7IM.Manager.Config;

namespace HYS.HL7IM.Manager.Forms
{
    public partial class FormMain : Form
    {
        private NotifyAdapterServer _server;
        public FormMain()
        {
            InitializeComponent();
            InitializeMVC();

            this.Text = Program.ConfigMgt.Config.AppCaption;

            RefreshList();
            GroupByType();
            RefreshMenuToolBar();

            _server = new NotifyAdapterServer("HL7Service");
        }

        // View
        private InterfaceView _viewInterface;

        // Control
        private InterfaceViewControler _ctlInterfaceView;

        private void InitializeMVC()
        {
            // View
            _viewInterface = new InterfaceView();
            _viewInterface.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            _viewInterface.InterfaceSelectedChanged += new EventHandler(_viewInterface_InterfaceSelectedChanged);
            this.panelMain.Controls.Add(_viewInterface);

            // Control
            _ctlInterfaceView = new InterfaceViewControler(this, _viewInterface);
            _viewInterface.AttachViewControler(_ctlInterfaceView);
        }

        void _viewInterface_InterfaceSelectedChanged(object sender, EventArgs e)
        {
            RefreshMenuToolBar();
        }

        private void RefreshMenuToolBar()
        {
            bool bEnabled = (null != _viewInterface.GetSelectedInterface());

            this.toolStripButtonBrowseFolder.Enabled = bEnabled;
            this.toolStripButtonManage.Enabled = bEnabled;
            this.toolStripButtonUninstall.Enabled = bEnabled;

            this.uninstallToolStripMenuItem.Enabled = bEnabled;
            this.manageToolStripMenuItem.Enabled = bEnabled;
            this.browseFolderToolStripMenuItem.Enabled = bEnabled;
        }

        #region UI control function

        private void ShowChangePassword()
        {
            FormPWD frm = new FormPWD();
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                //if (Program.ConfigMgt.Load())
                //{
                //    Program.Log.Write("Reload config succeeded after changing password. " + Program.ConfigMgt.FileName);
                //}
            }
        }

        private void GroupByType()
        {
            _viewInterface.GroupByType();
        }

        private void HideGroup()
        {
            _viewInterface.HideGroup();
        }

        private void BrowseFolder()
        {
            _viewInterface.BrowseFolder();
        }

        private void InstallSender()
        {
            _viewInterface.InstallInterface(InterfaceType.Sender);
        }

        private void InstallReceiver()
        {
            _viewInterface.InstallInterface(InterfaceType.Receiver);
        }

        private void RefreshList()
        {
            this.Cursor = Cursors.WaitCursor;
            _viewInterface.RefreshView();
            this.Cursor = Cursors.Default;
        }

        private void ShowLicense()
        {
            FormLicense frm = new FormLicense();
            frm.ShowDialog(this);
        }

        private void ShowAbout()
        {
            FormAbout frm = new FormAbout();
            frm.ShowDialog(this);
        }

        private void ShowLog()
        {
            Program.Log.View();
        }

        private void Exit()
        {
            Application.Exit();
        }

        private void ManageInferface()
        {
            _viewInterface.ManageInterface();
        }

        private void UninstallInterface()
        {
            _viewInterface.UninstallInterface();
        }

        #endregion

        #region ToolStrip event handler

        private void toolStripButtonRefreshList_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void toolStripButtonGroupByType_Click(object sender, EventArgs e)
        {
            GroupByType();
        }

        private void toolStripButtonHideGroup_Click(object sender, EventArgs e)
        {
            HideGroup();
        }

        private void toolStripButtonBrowseFolder_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        #endregion

        #region MenuStrip event handler

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void groupByTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupByType();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void logViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChangePassword();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void licenseViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicense();
        }

        private void installReceiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstallReceiver();
        }

        private void installSenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstallSender();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageInferface();
        }

        private void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UninstallInterface();
        }

        #endregion


        internal void RefreshServiceStatus(string strServiceName, int status)
        {
            _viewInterface.RefreshServiceStatus(strServiceName, status);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _server.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.Stop();
        }
    }
}