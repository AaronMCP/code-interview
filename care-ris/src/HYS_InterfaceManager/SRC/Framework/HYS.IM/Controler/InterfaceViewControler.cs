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
    public class InterfaceViewControler : ControlerBase
    {
        public InterfaceViewControler(Form frm, InterfaceView interfaceView, GCInterfaceManager interfaceMgt)
            : base( frm )
        {
            _interfaceView = interfaceView;
            _interfaceManager = interfaceMgt;
            if (_interfaceManager == null
                || interfaceView == null) throw (new ArgumentNullException());

            Initialize();
        }

        private InterfaceView _interfaceView;
        public InterfaceView InterfaceView
        {
            get { return _interfaceView; }
        }

        private GCInterfaceManager _interfaceManager;
        public GCInterfaceManager InterfaceManager
        {
            get { return _interfaceManager; }
        }

        private void Initialize()
        {
            _interfaceView.SetControler(this);
        }
        public override void Refresh()
        {
            base.SetStatus("Refreshing interface list.");

            GCInterfaceCollection interfaceList = _interfaceManager.QueryInterfaceList();
            _interfaceView.RefreshList(interfaceList);

            if (interfaceList != null)
            {
                Program.Log.Write("{Interface} Refresh interface list succeed : " + interfaceList.Count.ToString() + " items.");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Interface} Refresh interface list failed : " + GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(frmMain, "Refresh interface list failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            base.ClearStatus();
        }
    }
}
