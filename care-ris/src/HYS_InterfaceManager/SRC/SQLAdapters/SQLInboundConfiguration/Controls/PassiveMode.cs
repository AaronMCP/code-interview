using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLInboundAdapterConfiguration.Forms;

namespace HYS.SQLInboundAdapterConfiguration.Controls
{
    public partial class PassiveMode : UserControl
    {
        #region Local members
        XCollection<SQLInboundChanel> channelSet;
        ConnectionConfig DBconfig;
        #endregion

        #region Constructor
        public PassiveMode()
        {
            InitializeComponent();
            DBconfig = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig;
            channelSet = SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundPassiveChanels;
        }
        #endregion

        #region Show,refresh and Save
        public void ShowInformation() {
            ShowSPList();
        }
        
        public void ShowSPList() {
            int i = 0;
            lstvSP.Items.Clear();
            foreach(SQLInboundChanel channel in channelSet){
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(channel.SPName);
                viewItem.Tag = i - 1;
                lstvSP.Items.Add(viewItem);
            }
        }

        public void Save() {
            DBconfig.ConnectionParameter.Server = "";
            DBconfig.ConnectionParameter.Database = "";
            DBconfig.ConnectionParameter.User = "";
            DBconfig.ConnectionParameter.Password = "";
            DBconfig.ConnectionParameter.ConnectionStr = "";

            DBconfig.TimerInterval = 1000;

        }
        #endregion

        #region Controls events
        private void lstvSP_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (e.IsSelected == true)
            {
                btnSPModify.Enabled = true;
                btnSPDelete.Enabled = true;
            }
            else {
                btnSPModify.Enabled = false;
                btnSPDelete.Enabled = false;
            }
        }
        
        private void lstvSP_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvSP.Columns[e.Column].Tag == null)
                this.lstvSP.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvSP.Columns[e.Column].Tag;
            if (tabK)
                this.lstvSP.Columns[e.Column].Tag = false;
            else
                this.lstvSP.Columns[e.Column].Tag = true;
            this.lstvSP.ListViewItemSorter = new ListViewSort(e.Column, this.lstvSP.Columns[e.Column].Tag);
        }
        #endregion

        #region SP events
        private void btnSPAdd_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            StorageProcedure frm = new StorageProcedure(channelSet);
            frm.ShowDialog(this);

            if (channelSet.Count > count)
            {
                ShowSPList();

                int i = 0;
                foreach (ListViewItem viewItem in lstvSP.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvSP.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }          
        }

        private void btnSPModefy_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvSP.SelectedIndices[0];
            int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

            StorageProcedure frm = new StorageProcedure(channelSet, itemIndex);
            frm.ShowDialog(this);

            ShowSPList();
            lstvSP.Items[selectIndex].Selected = true;
        }
        private void lstvSP_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvSP.SelectedIndices[0];
            int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

            StorageProcedure frm = new StorageProcedure(channelSet, itemIndex);
            frm.ShowDialog(this);

            ShowSPList();
            lstvSP.Items[selectIndex].Selected = true;
        }

        private void btnSPDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this storage procedure?", "Delete Storage Procedure Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvSP.SelectedIndices[0];
                int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

                channelSet.Remove(channelSet[itemIndex]);
                ShowSPList();
                if (channelSet.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnSPModify.Enabled = false;
                    btnSPDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= channelSet.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvSP.Items[selectIndex].Selected = true;
                }
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (channelSet.Count > 0)
            {
                if (MessageBox.Show( "If you do this operation, all the existed storage procedure will delete.\nAre you sure to go on?", "Load Default Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    LoadDefaultInboundSP();
                    ShowSPList();
                }
            }
            else
            {
                LoadDefaultInboundSP();
                ShowSPList();
            }
        }
        #endregion

        #region Load default storage procedure
        private void LoadDefaultInboundSP()
        {
            channelSet.Clear();

            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;

            //Patient Table
            SQLInboundChanel chnPatient = channelSet.Add(new SQLInboundChanel());
            chnPatient.Rule.RuleID = GWDataDBTable.Patient.ToString();
            chnPatient.SPName = "sp_" + interfaceName + "_" + GWDataDBTable.Patient.ToString();

            LoadTableParameter(chnPatient, GWDataDBTable.Index, interfaceName);
            LoadTableParameter(chnPatient, GWDataDBTable.Patient, interfaceName);
            chnPatient.SPStatement = RuleControl.GetInboundSP(interfaceName, chnPatient.Rule, false);

            //Order Table
            SQLInboundChanel chnOrder = channelSet.Add(new SQLInboundChanel());
            chnOrder.Rule.RuleID = GWDataDBTable.Order.ToString();
            chnOrder.SPName = "sp_" + interfaceName + "_" + GWDataDBTable.Order.ToString();

            LoadTableParameter(chnOrder, GWDataDBTable.Index, interfaceName);
            LoadTableParameter(chnOrder, GWDataDBTable.Patient, interfaceName);
            LoadTableParameter(chnOrder, GWDataDBTable.Order, interfaceName);
            chnOrder.SPStatement = RuleControl.GetInboundSP(interfaceName, chnOrder.Rule, false);


            //Report Table
            SQLInboundChanel chnReport = channelSet.Add(new SQLInboundChanel());
            chnReport.Rule.RuleID = GWDataDBTable.Report.ToString();
            chnReport.SPName = "sp_" + interfaceName + "_" + GWDataDBTable.Report.ToString();
            
            LoadTableParameter(chnReport, GWDataDBTable.Index, interfaceName);
            LoadTableParameter(chnReport, GWDataDBTable.Patient, interfaceName);
            LoadTableParameter(chnReport, GWDataDBTable.Order, interfaceName);
            LoadTableParameter(chnReport, GWDataDBTable.Report, interfaceName);
            chnReport.SPStatement = RuleControl.GetInboundSP(interfaceName, chnReport.Rule, false);
        }
        private void LoadTableParameter(SQLInboundChanel chn, GWDataDBTable table, string interfaceName)
        {
            GWDataDBField[] iFields = GWDataDBField.GetFields(table);
            foreach (GWDataDBField field in iFields)
            {
                if (field.IsAuto) continue;
                string paramName = field.GetFullFieldName(Program.DeviceMgt.DeviceDirInfor.Header.Name).Replace(".","_");
                SQLInQueryResultItem item = new SQLInQueryResultItem(field, paramName);
                chn.Rule.QueryResult.MappingList.Add(item);
            }
        }
        #endregion
    }
}
