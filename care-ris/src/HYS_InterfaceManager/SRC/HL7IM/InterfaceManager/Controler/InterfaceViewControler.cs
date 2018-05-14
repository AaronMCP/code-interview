using System;
using System.Windows.Forms;
using HYS.Common.Objects.Logging;
using HYS.HL7IM.Manager.Forms;
using CSH.eHeath.HL7Gateway.Manager;
using HYS.Common.Xml;
using HYS.HL7IM.Manager.Config;

namespace HYS.HL7IM.Manager.Controler
{
    public class InterfaceViewControler : ControlerBase
    {
        public InterfaceViewControler(Form frm, InterfaceView interfaceView)
            : base( frm )
        {
            _interfaceView = interfaceView;

            Initialize();
        }

        private InterfaceView _interfaceView;
        public InterfaceView InterfaceView
        {
            get { return _interfaceView; }
        }

        private void Initialize()
        {
            _interfaceView.SetControler(this);
        }
        public override void Refresh()
        {
            base.SetStatus("Refreshing interface list.");

            Program.ConfigMgt.Config.RefreshConfigInfo();
            XCollection<HL7InterfaceConfig> interfaceList = Program.ConfigMgt.Config.InterfaceList;
            
            _interfaceView.RefreshList(interfaceList);

            if (interfaceList != null)
            {
                Program.Log.Write("{Interface} Refresh interface list succeed : " + interfaceList.Count.ToString() + " items.");
            }
            else
            {
                Program.LogWapper.Write(LogType.Warning,"{Interface} Refresh interface list failed.");

                MessageBox.Show(frmMain, "Refresh interface list failed.\r\n\r\n",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            base.ClearStatus();
        }
    }
}
