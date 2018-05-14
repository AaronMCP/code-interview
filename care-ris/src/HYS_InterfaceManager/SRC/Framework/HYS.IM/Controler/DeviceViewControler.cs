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
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Controler
{
    public class DeviceViewControler : ControlerBase
    {
        public DeviceViewControler(Form frm, DeviceView deviceView, GCDeviceManager deviceMgt)
            : base( frm )
        {
            _deviceView = deviceView;
            _deviceManager = deviceMgt;
            if (_deviceManager == null
                || deviceView == null) throw (new ArgumentNullException());

            Initialize();
        }

        private DeviceView _deviceView;
        public DeviceView DeviceView
        {
            get { return _deviceView; }
        }

        private GCDeviceManager _deviceManager;
        public GCDeviceManager DeviceManager
        {
            get { return _deviceManager; }
        }

        private void Initialize()
        {
            _deviceView.SetControler(this);
        }
        public override void Refresh()
        {
            base.SetStatus("Refreshing device list.");

            GCDeviceCollection deviceList = _deviceManager.QueryDeviceList();
            _deviceView.RefreshList(deviceList);

            if (deviceList != null)
            {
                Program.Log.Write("{Device} Refresh device list succeed : " + deviceList.Count.ToString() + " items.");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Device} Refresh device list failed : " + GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(frmMain, "Refresh device list failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            base.ClearStatus();
        }
    }
}
