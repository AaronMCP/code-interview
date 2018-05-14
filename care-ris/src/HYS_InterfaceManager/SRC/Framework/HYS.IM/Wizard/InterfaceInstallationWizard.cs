using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;

namespace HYS.IM.Wizard
{
    public partial class InterfaceInstallationWizard : Form
    {
        public InterfaceInstallationWizard()
        {
            InitializeComponent();
            InitializePages();
        }
        public GCInterface InstalledInterface
        {
            get { return _targetInterface; }
        }

        private GCDevice _sourceDevice;
        private GCInterface _targetInterface;

        private DeviceSelectionPage _selectionPage;
        private InterfaceDefinitionPage _definitionPage;
        private InterfaceConfigurationPage _configurationPage;

        private bool _closeDirect;
        private bool CancelClose()
        {
            if (_configurationPage.IsInstallationCompleted())
            {
                this.DialogResult = DialogResult.OK;
                return false;
            }
            else
            {
                if ((!_closeDirect) &&
                    MessageBox.Show(this, "Installation is not completed now. Are you sure to exit installation wizard?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    if (_sourceDevice == null)          // cancel on step 1
                    {
                        this.DialogResult = DialogResult.Cancel;
                        return false;
                    }
                    else
                    {
                        if (_targetInterface == null)   // canel on step 2
                        {
                            this.DialogResult = DialogResult.Cancel;
                            _definitionPage.GoCancel();
                            return false;
                        }
                        else                            // canel on step 3
                        {
                            this.DialogResult = DialogResult.Cancel;
                            _configurationPage.GoCanel();
                            _definitionPage.GoCancel();
                            return false;
                        }
                    }
                }
            }
        }
        private void InitializePages()
        {
            _selectionPage = new DeviceSelectionPage();
            _selectionPage.MoveNext += new HYS.IM.UIControl.PageEventHandler(_selectionPage_MoveNext);
            _selectionPage.MovePrev += new HYS.IM.UIControl.PageEventHandler(_selectionPage_MovePrev);
            _selectionPage.CloseAll += new HYS.IM.UIControl.PageEventHandler(_selectionPage_CloseAll);

            _definitionPage = new InterfaceDefinitionPage();
            _definitionPage.MoveNext += new HYS.IM.UIControl.PageEventHandler(_definitionPage_MoveNext);
            _definitionPage.MovePrev += new HYS.IM.UIControl.PageEventHandler(_definitionPage_MovePrev);
            _definitionPage.CloseAll += new HYS.IM.UIControl.PageEventHandler(_definitionPage_CloseAll);

            _configurationPage = new InterfaceConfigurationPage();
            _configurationPage.MoveNext += new HYS.IM.UIControl.PageEventHandler(_configurationPage_MoveNext);
            _configurationPage.MovePrev += new HYS.IM.UIControl.PageEventHandler(_configurationPage_MovePrev);
            _configurationPage.CloseAll += new HYS.IM.UIControl.PageEventHandler(_configurationPage_CloseAll);

            sliderPanelMain.AddPage(_selectionPage);
            sliderPanelMain.AddPage(_definitionPage);
            sliderPanelMain.AddPage(_configurationPage);
            sliderPanelMain.RefreshPage();
        }

        private void _selectionPage_CloseAll(HYS.IM.UIControl.IPage me)
        {
            _closeDirect = true;
            this.Close();
        }
        private void _selectionPage_MovePrev(HYS.IM.UIControl.IPage me)
        {
            sliderPanelMain.PrevPage();
        }
        private void _selectionPage_MoveNext(HYS.IM.UIControl.IPage me)
        {
            _sourceDevice = _selectionPage.GetSelectedDevice();
            if (_sourceDevice == null) return;

            sliderPanelMain.NextPage();
            _definitionPage.SetSourceDevice(_sourceDevice);
        }

        private void _definitionPage_CloseAll(HYS.IM.UIControl.IPage me)
        {
            _closeDirect = true;
            this.Close();
        }
        private void _definitionPage_MovePrev(HYS.IM.UIControl.IPage me)
        {
            sliderPanelMain.PrevPage();
        }
        private void _definitionPage_MoveNext(HYS.IM.UIControl.IPage me)
        {
            _targetInterface = _definitionPage.GetTargetInterface();
            if (_targetInterface == null) return;

            sliderPanelMain.NextPage();
            _configurationPage.SetDefinedInterface(_targetInterface);
        }

        private void _configurationPage_CloseAll(HYS.IM.UIControl.IPage me)
        {
            _closeDirect = true;
            this.Close();
        }
        private void _configurationPage_MovePrev(HYS.IM.UIControl.IPage me)
        {
            sliderPanelMain.PrevPage();
        }
        private void _configurationPage_MoveNext(HYS.IM.UIControl.IPage me)
        {
            sliderPanelMain.NextPage();
        }

        private void InterfaceInstallationWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CancelClose();
        }
        private void InterfaceInstallationWizard_Activated(object sender, EventArgs e)
        {
            _configurationPage.ActivateConfig();
        }
    }
}