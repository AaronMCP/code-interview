using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Config;
using HYS.Adapter.Base;

namespace HYS.IM.Forms
{
    public partial class FormMain : Form, IMessageFilter
    {
        public FormMain()
        {
            InitializeComponent();
            InitializeMVC();
            InitializtUI();

            this.Text = Program.ConfigMgt.Config.AppCaption;
        }

        #region UI control function

        private void InitializtUI()
        {
            _panelViews.OnPageRefresh += new EventHandler(_panelViews_OnPageRefresh);
            _viewDevice.SelectionChange += new EventHandler(_viewDevice_SelectionChange);
            _viewInterface.SelectionChange += new EventHandler(_viewInterface_SelectionChange);

            _ctlInterfaceTool.Refresh();
            _ctlDeviceTool.Refresh();
        }
        private void RefreshListControlButton()
        {
            IView view = _panelViews.CurrentPage as IView;

            toolStripButtonBrowseFolder.Enabled =
                browseFolderToolStripMenuItem.Enabled =
                (view != null && view.SelectedItem != null);
        }
        private void RefreshPageControlButton()
        {
            bool res = (_panelViews.CurrentPage == _viewDevice);

            toolStripButtonDeviceView.Checked = res;
            toolStripButtonInterfaceView.Checked = !res;
            deviceViewToolStripMenuItem.Checked = res;
            interfaceViewToolStripMenuItem.Checked = !res;
        }

        private void _viewInterface_SelectionChange(object sender, EventArgs e)
        {
            RefreshListControlButton();
        }
        private void _viewDevice_SelectionChange(object sender, EventArgs e)
        {
            RefreshListControlButton();
        }
        private void _panelViews_OnPageRefresh(object sender, EventArgs e)
        {
            RefreshPageControlButton();
            RefreshListControlButton();
            AutoRefresh();
        }

        private bool _deviceShowed;
        private bool _interfaceShowed;
        private void AutoRefresh()
        {
            // do not modify the caption of main form, or the adapter's NT services won't be able to notify thire status to IM.

            if (_panelViews.CurrentPage == _viewDevice)
            {
                //this.Text = "Device View - " + AppName;
                if (!_deviceShowed)
                {
                    RefreshList();
                    RefreshPageControlButton();
                    _deviceShowed = true;
                }
                return;
            }
            if (_panelViews.CurrentPage == _viewInterface)
            {
                //this.Text = "Interface View - " + AppName;
                if (!_interfaceShowed)
                {
                    RefreshList();
                    RefreshPageControlButton();
                    _interfaceShowed = true;
                }
            }
        }
        private void ShowDeviceView()
        {
            _panelViews.GotoPage(_viewDevice);
            if (_panelTools.Panels.Length > 1)
            {
                _panelTools.Panels[0].Expand();
                _panelTools.Panels[1].Collapse();
            }
            
        }
        private void ShowInterfaceView()
        {
            _panelViews.GotoPage(_viewInterface);
            if (_panelTools.Panels.Length > 1)
            {
                _panelTools.Panels[0].Collapse();
                _panelTools.Panels[1].Expand();
            }
        }
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
        private void GroupByDirection()
        {
            IView view = _panelViews.CurrentPage as IView;
            if (view != null) view.GroupByDirection();
        }
        private void GroupByType()
        {
            IView view = _panelViews.CurrentPage as IView;
            if (view != null) view.GroupByType();
        }
        private void HideGroup()
        {
            IView view = _panelViews.CurrentPage as IView;
            if (view != null) view.HideGroup();
        }
        private void BrowseFolder()
        {
            IView view = _panelViews.CurrentPage as IView;
            if (view != null) view.BrowseFolder();
        }
        private void RefreshList()
        {
            this.Cursor = Cursors.WaitCursor;
            IView view = _panelViews.CurrentPage as IView;
            if (view != null) view.RefreshView();
            this.Cursor = Cursors.Default;
        }
        private void ShowLicense()
        {
            FormLicense frm = new FormLicense();
            frm.ShowDialog(this);
        }
        private void ShowConfig()
        {
            FormConfig frm = new FormConfig();
            frm.ShowDialog(this);
        }
        private void ShowAbout()
        {
            FormAbout frm = new FormAbout();
            frm.ShowDialog(this);
        }
        private void ShowHelp()
        {
            string fileName = ConfigHelper.GetFullPath(Program.ConfigMgt.Config.HelpFileName);
            Process p = Process.Start(fileName);    //"explorer.exe", "\"" + fileName + "\"");
            p.EnableRaisingEvents = false;
        }
        private void ShowLog()
        {
            Program.Log.View();
        }
        private void ShowLut()
        {
            FormLUTMgt lut = new FormLUTMgt();
            lut.ShowDialog(this);
        }
        private void ShowReg()
        {
            FormRegExpMgt reg = new FormRegExpMgt();
            reg.ShowDialog(this);
        }
        private void Exit()
        {
            Application.Exit();
        }

        #endregion

        #region ToolStrip event handler

        private void toolStripButtonDeviceView_Click(object sender, EventArgs e)
        {
            ShowDeviceView();
        }

        private void toolStripButtonInterfaceView_Click(object sender, EventArgs e)
        {
            ShowInterfaceView();
        }
        
        private void toolStripButtonRefreshList_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void toolStripButtonGroupByDirection_Click(object sender, EventArgs e)
        {
            GroupByDirection();
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

        private void systemConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowConfig();
        }

        private void deviceViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDeviceView();
        }

        private void interfaceViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInterfaceView();
        }

        private void groupByTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupByType();
        }

        private void groupByDirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupByDirection();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void logViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLog();
        }

        private void helpDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp();
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

        private void lookUpTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLut();
        }

        private void licenseViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicense();
        }

        private void regExpMgtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowReg();
        }

        #endregion

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == Win32Api.SW_SHOW)
            {
                this.Activate();
                return false;
            }

            AdapterMessage am = AdapterMessage.FromMessage(m);
            if (am == null) return false;

            bool update = (_panelViews.CurrentPage == _viewInterface);
            _viewInterface.HandleMessage(am,update);

            return true;
        }

        #endregion

        #region Form event handler

        private void FormMain_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);

            timerStartup.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        private void timerStartup_Tick(object sender, EventArgs e)
        {
            timerStartup.Stop();

            if (Program.ConfigMgt.Config.ShowDeviceViewWhenStartup)
            {
                ShowDeviceView();
            }
            else
            {
                ShowInterfaceView();
            }

            AutoRefresh();
        }

        #endregion
    }
}