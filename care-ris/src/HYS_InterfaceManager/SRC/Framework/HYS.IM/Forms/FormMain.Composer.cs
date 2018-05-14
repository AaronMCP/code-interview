using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using HYS.IM;
using HYS.IM.Controler;
using HYS.IM.UIControl;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;

namespace HYS.IM.Forms
{
    partial class FormMain
    {
        // Module
        private GCDeviceManager _deviceMgt;
        private GCInterfaceManager _interfaceMgt;
        
        // View
        private DPanelContainer _panelTools;
        private SliderPanel _panelViews;
        private DeviceView _viewDevice;
        private InterfaceView _viewInterface;

        // Control
        private DeviceToolControler _ctlDeviceTool;
        private DeviceViewControler _ctlDeviceView;
        private InterfaceToolControler _ctlInterfaceTool;
        private InterfaceViewControler _ctlInterfaceView;

        private void InitializeMVC()
        {
            // Module
            _deviceMgt = new GCDeviceManager(Program.ConfigDB, Program.ConfigMgt.Config.DeviceFolder);
            _interfaceMgt = new GCInterfaceManager(Program.ConfigDB, Program.ConfigMgt.Config.InterfaceFolder);


            // View
            _viewDevice = new DeviceView();
            _viewInterface = new InterfaceView();
            
            _panelViews = new SliderPanel();
            _panelViews.Dock = DockStyle.Fill;
            _panelViews.AddPage(_viewDevice);
            _panelViews.AddPage(_viewInterface);
            _panelViews.RefreshPage();

            this.panelView.Controls.Add(_panelViews);


            // Control
            _ctlDeviceView = new DeviceViewControler(this, _viewDevice, _deviceMgt);
            _ctlDeviceTool = new DeviceToolControler(this, _viewDevice, _panelViews, _deviceMgt, _interfaceMgt);
            _viewDevice.AttachViewControler(_ctlDeviceView);
            _viewDevice.AttachToolControler(_ctlDeviceTool);
            
            _ctlInterfaceView = new InterfaceViewControler(this, _viewInterface, _interfaceMgt);
            _ctlInterfaceTool = new InterfaceToolControler(this, _viewInterface, _panelViews, _interfaceMgt);
            _viewInterface.AttachViewControler(_ctlInterfaceView);
            _viewInterface.AttachToolControler(_ctlInterfaceTool);

            _ctlDeviceTool.AttachStatusStrip(this.statusMain);
            _ctlInterfaceTool.AttachStatusStrip(this.statusMain);


            // Controler View
            _panelTools = new DPanelContainer
                (new DPanel[] {
                    _ctlDeviceTool.DevicePanel,
                    _ctlInterfaceTool.InterfacePanel,
                });
            _panelTools.Dock = DockStyle.Fill;
            this.panelTool.Controls.Add(_panelTools);
        }
    }
}
