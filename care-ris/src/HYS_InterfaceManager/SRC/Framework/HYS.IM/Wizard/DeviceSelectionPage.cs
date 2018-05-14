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
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.License2;
using HYS.Adapter.Base;

namespace HYS.IM.Wizard
{
    /// <summary>
    /// user select a device from inbound/outbound device list
    /// 
    /// </summary>
    public partial class DeviceSelectionPage : UserControl, IPage 
    {
        public DeviceSelectionPage()
        {
            InitializeComponent();
            InitializeControl();
            RefreshControl();
        }

        public GCDevice GetSelectedDevice()
        {
            if (this.listViewDevice.SelectedItems.Count < 1) return null;
            return this.listViewDevice.SelectedItems[0].Tag as GCDevice;
        }

        #region private functions

        private GCDeviceManager deviceMgt;
        private GCInterfaceManager interfaceMgt;
        private ListViewControler listCtrl;

        private void InitializeControl()
        {
            interfaceMgt = new GCInterfaceManager(Program.ConfigDB, Program.ConfigMgt.Config.InterfaceFolder);
            deviceMgt = new GCDeviceManager(Program.ConfigDB, Program.ConfigMgt.Config.DeviceFolder);
            listCtrl = new ListViewControler(this.listViewDevice);
        }
        private void RefreshControl()
        {
            this.buttonNext.Enabled = (GetSelectedDevice() != null);
        }

        private void RefreshDeviceList()
        {
            string direction;
            GCDeviceCollection dlist;

            if (this.radioButtonIn.Checked)
            {
                direction = "inbound";
                dlist = deviceMgt.QueryInboundDevice();
            }
            else if(this.radioButtonOut.Checked)
            {
                direction = "outbound";
                dlist = deviceMgt.QueryOutboundDevice();
            }
            else if (this.radioButtonBi.Checked)
            {
                direction = "bidirectional";
                dlist = deviceMgt.QueryBidirectionalDevice();
            }
            else
            {
                return;
            }

            FillDeviceList(dlist);
            RefreshControl();

            if (dlist != null)
            {
                Program.Log.Write("{Device} Refresh device (" + direction + ") list succeed : " + dlist.Count.ToString() + " items.");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "{Device} Refresh device (" + direction + ") list failed : " + GCError.LastErrorInfor);
                Program.Log.Write(GCError.LastError);

                MessageBox.Show(this, "Refresh device (" + direction + ") list failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void FillDeviceList(GCDeviceCollection dlist)
        {
            this.listViewDevice.Items.Clear();
            if (dlist == null) return;

            List<int> deviceIDs = new List<int>();
            foreach (GCDevice d in dlist) deviceIDs.Add(d.DeviceID);
            Dictionary<int, int> countList = interfaceMgt.GetInterfaceCount(deviceIDs.ToArray());

            foreach (GCDevice o in dlist)
            {
                GCDeviceAgent d = o as GCDeviceAgent;
                if (d == null) continue;

                // don't need to read DeviceDir file

                DeviceRec dr = d.DeviceRec;
                ListViewItem i = this.listViewDevice.Items.Add(dr.ID.ToString());
                i.SubItems.Add(dr.Name);
                i.SubItems.Add(DataHelper.GetTypeName(dr.Type));
                i.SubItems.Add(dr.Description);
                i.Tag = d;

                if (countList != null)
                {
                    int icount = countList[d.DeviceID];
                    string str = icount.ToString();

                    //DeviceLicenseLevel level = Program.License.FindLicenseLevel
                    //    (dr.Name, DataHelper.GetType(dr.Type), DataHelper.GetDirection(dr.Direction));

                    DeviceLicense lic = Program.License.FindDevice
                        (dr.Name, DataHelper.GetType(dr.Type), DataHelper.GetDirection(dr.Direction));

                    string strMax = "";
                    if (lic != null)
                    {
                        int maxCount = lic.MaxInterfaceCount;
                        if (maxCount == 0)
                        {
                            strMax = " (Disable)";
                            i.ForeColor = Color.Gray;
                            i.Tag = null;
                        }
                        else
                        {
                            if (lic.IsExpired(Program.License.Header.CreateDate))
                            {
                                strMax = " (Expired)";
                                i.ForeColor = Color.Gray;
                                i.Tag = null;
                            }
                            else
                            {
                                if (maxCount == DeviceLicense.InfiniteInterfaceCount)
                                {
                                    strMax = " (Max: infinte)";
                                }
                                else
                                {
                                    strMax = " (Max: " + maxCount.ToString() + ")";
                                    if (icount >= maxCount)
                                    {
                                        i.ForeColor = Color.Gray;
                                        i.Tag = null;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        strMax = " (Unknown)";
                        //US28109-TA93905
                        #region
                        //暂时去掉加接口时的权限验证
                        //i.ForeColor = Color.Gray;
                        //i.Tag = null;
                        #endregion
                    }

                    i.SubItems.Add(str + strMax);
                }
                else
                {
                    i.ForeColor = Color.Gray;
                    i.Tag = null;
                }
            }
        }

        #endregion

        #region event handlers

        private void buttonNext_Click(object sender, EventArgs e)
        {
            NotifyMoveNext();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NotifyCloseAll();
        }

        private void listViewDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshControl();
        }

        private void radioButtonIn_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDeviceList();
        }

        private void radioButtonOut_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDeviceList();
        }

        #endregion

        #region IPage Members

        public Control GetControl()
        {
            return this;
        }

        public event PageEventHandler MoveNext;
        private void NotifyMoveNext()
        {
            if (MoveNext != null) MoveNext(this);
        }

        public event PageEventHandler MovePrev;
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
