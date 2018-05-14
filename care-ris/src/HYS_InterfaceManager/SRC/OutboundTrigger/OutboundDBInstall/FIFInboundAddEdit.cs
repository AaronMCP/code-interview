using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OutboundDBInstall
{
    public partial class FIFInboundAddEdit : Form
    {

        public FIFInboundAddEdit()
        {
            InitializeComponent();

        }

        //DataGridView _dgv;
        string _InboundIFName;
        IOChannel _CurrIOChannel;

        public enum FormType
        {
            ftAdd,
            ftModify
        }
        FormType _FormType = FormType.ftAdd;

        public DialogResult ShowDialog(IWin32Window owner, FormType ft, string InboundIFName)
        {
            this.cbInboundInterface.Items.Clear();
            this.cbInboundInterface.Text = "";
            this.clbEventTypeList.Items.Clear();

            _FormType = ft;
            _InboundIFName = InboundIFName;
            InitInboundInterface();
            InitChannel();
            //this.ShowPKFields();
            this.ShowMergeCriteria();
            //this.ShowMergeFields();
            this.ShowMergeFieldMappings();
            this.ShowFilterFields();
            this.RefreshForm();
            return base.ShowDialog(owner);
        }

        private bool InitChannel()
        {
            string sName = cbInboundInterface.Text;
            if (sName.Trim() == "")
            {
                _CurrIOChannel = null;
                return false;
            }

            if (_FormType != FormType.ftAdd)
            {
                _CurrIOChannel = OutboundDBConfigMgt.Config.IOChannels.FindChannel(sName);
                return true;
            }

            _CurrIOChannel = new IOChannel();
            _CurrIOChannel.INameInbound = sName;
            _CurrIOChannel.EventTypeListStrInbound = OutboundDBConfigMgt.GWDBInfo.InboundInterfaceList[sName].ToString();

            // _CurrIOChannel.EventTypeListStrInbound = OutboundDBConfigMgt.GWDBInfo.InboundInterfaceList[sName].ToString();

            return true;
        }

        private void FIFInboundAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (_CurrIOChannel != null)
                {
                    this.checkBoxRedundancyChecking.Checked = _CurrIOChannel.CheckRedundancy;
                }

                InitForm();
                RefreshForm();
                this.RefreshMergeRecord(false);
                this.RefreshFilterField(false);
            }
            catch (Exception ex)
            {
                Program.log.Write(ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void InitForm()
        {
            // Init Title
            switch (_FormType)
            {
                case FormType.ftAdd:  // default value
                    this.Text = "Add a inbound interface";
                    break;
                case FormType.ftModify:
                    this.Text = "Modify a inbound interface";
                    break;
                default:
                    this.Text = "Unknow operation";
                    break;
            }


            this.InitEventTypeList();


        }

        private void InitInboundInterface()
        {
            // Init Inbound Interface Combobox
            this.cbInboundInterface.Items.Clear();

            // Fill Combobox, exclude those have been exist in IOChannels
            IOChannels chs;
            chs = OutboundDBConfigMgt.Config.IOChannels;
            foreach (string sName in OutboundDBConfigMgt.GWDBInfo.InboundInterfaceList.Keys)
                if (chs.FindChannel(sName) == null)
                    cbInboundInterface.Items.Add(sName.Trim());

            // Show appricate item
            switch (_FormType)
            {
                case FormType.ftAdd:  // default value                    
                    if (cbInboundInterface.Items.Count > 0)
                        cbInboundInterface.SelectedIndex = 0;
                    break;
                case FormType.ftModify:
                    cbInboundInterface.Items.Add(_InboundIFName.Trim());
                    cbInboundInterface.Text = _InboundIFName.Trim();
                    break;
                default:
                    this.Text = "Unknow operation";
                    break;
            }
        }

        private void InitCoflictTreatMethod()
        {
            //if (_CurrIOChannel == null)
            //    return;

            //cbConflictTreatMethod.Items.Clear();

            //System.Reflection.FieldInfo[] fis = typeof(IOChannelSettings.ConflictTreatMethodEnum).GetFields();
            //foreach( System.Reflection.FieldInfo fi in fis)
            //{
            //    if(fi.IsLiteral)
            //       cbConflictTreatMethod.Items.Add(fi.Name);
            //}

            //cbConflictTreatMethod.Text = _CurrIOChannel.IOChannelSettings.ConflictTreatMethod.ToString();

        }

        private void InitEventTypeList()
        {
            this.clbEventTypeList.Items.Clear();
            if (_CurrIOChannel == null) return;



            foreach (EventType et in _CurrIOChannel.EventTypeList)
            {
                int index = clbEventTypeList.Items.Add(et.ETName);
                if (_FormType == FormType.ftAdd)
                {
                    clbEventTypeList.SetItemChecked(index, false);
                    et.Enabled = false;
                }
                else
                {
                    clbEventTypeList.SetItemChecked(index, et.Enabled);
                }
            }

        }


        //private void InitIsMerging()
        //{

        //    switch (_FormType)
        //    {
        //        case FormType.ftAdd:  // default value
        //            //ckbIsMerging.Checked = false;
        //            break;
        //        case FormType.ftModify:
        //            ckbIsMerging.Checked = _CurrIOChannel.IOChannelSettings.Merging;
        //            break;
        //        default:
        //            this.Text = "Unknow operation";
        //            break;
        //    }            
        //}


        private void RefreshForm()
        {
            // Enable or disable interface combobox
            this.cbInboundInterface.Enabled = _FormType == FormType.ftAdd;

            // Enable or disable OK, Cancel button
            this.btOK.Enabled = this.cbInboundInterface.Items.Count > 0 &&
                                this.cbInboundInterface.SelectedIndex >= 0;




        }

        private void RefreshFilterField(bool bChecked)
        {
            if (clbEventTypeList.SelectedIndex < 0)
            {
                gBoxfilter.Enabled = false;
                btnEditFilter.Enabled = false;
                lViewFilter.Enabled = false;
            }


            gBoxfilter.Enabled = bChecked;
            btnEditFilter.Enabled = bChecked;
            lViewFilter.Enabled = bChecked;

        }

        private void RefreshMergeRecord(bool bChecked)
        {
            this.ckbIsMerging.Enabled = true;
            this.treeViewMatchCriteria.Enabled = true;
            this.lvMergeFields.Enabled = true;
            this.btSelPKFields.Enabled = true;
            this.btSelMergeFields.Enabled = true;

            if (clbEventTypeList.SelectedIndex < 0)
            {
                this.ckbIsMerging.Enabled = false;
                this.treeViewMatchCriteria.Enabled = false;
                this.lvMergeFields.Enabled = false;
                this.btSelPKFields.Enabled = false;
                this.btSelMergeFields.Enabled = false;

                return;
            }

            if (!bChecked)
            {
                this.ckbIsMerging.Enabled = false;
                this.treeViewMatchCriteria.Enabled = false;
                this.lvMergeFields.Enabled = false;
                this.btSelPKFields.Enabled = false;
                this.btSelMergeFields.Enabled = false;
                return;
            }
            if (!ckbIsMerging.Checked)
            {
                this.treeViewMatchCriteria.Enabled = false;
                this.lvMergeFields.Enabled = false;
                this.btSelPKFields.Enabled = false;
                this.btSelMergeFields.Enabled = false;
            }
        }

        /// <summary>
        /// When user change inboundinterface, system should build a new IOChannel to _CurrIOChannel
        /// and set other control on the form;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInboundInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitChannel();
            this.InitEventTypeList();
            RefreshForm();
        }


        private void SaveView2CurrIOChannel()
        {
            //_CurrIOChannel.IOChannelSettings.ConflictTreatMethod =
            //    (IOChannelSettings.ConflictTreatMethodEnum)Enum.Parse(typeof(IOChannelSettings.ConflictTreatMethodEnum), cbConflictTreatMethod.Text);

            _CurrIOChannel.CheckRedundancy = this.checkBoxRedundancyChecking.Checked;
        }

        private void btOK_Click(object sender, EventArgs e)
        {


            SaveView2CurrIOChannel();

            if (_FormType == FormType.ftAdd)
                IOChannelVeiwMgt.AddChannel(_CurrIOChannel);
            else
                IOChannelVeiwMgt.EditChannel(_CurrIOChannel);
            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }




        //private void ShowPKFields()
        //{
        //    this.lvPKFields.Items.Clear();
        //    if (_CurrIOChannel == null)
        //        return;
        //    if (_CurrEventType == null)
        //        return;
        //    PKFields pkfs = _CurrEventType.PKFields;
        //    for (int i = 0; i < pkfs.Count; i++)
        //    {
        //        ListViewItem lvi = lvPKFields.Items.Add((i + 1).ToString());
        //        PKField f = (PKField)pkfs[i];
        //        lvi.Tag = f;
        //        lvi.SubItems.Add(f.Table);
        //        lvi.SubItems.Add(f.FieldName);
        //    }
        //}

        private TreeNode InitCriteriaNode(MatchFieldNode criteriaNode)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = criteriaNode.ToString();
            treeNode.Tag = criteriaNode;

            TreeNode fieldNode = null;
            foreach (MatchField field in criteriaNode.MatchFields)
            {
                fieldNode = new TreeNode();
                fieldNode.Text = field.ToString();
                fieldNode.Tag = field;
                treeNode.Nodes.Add(fieldNode);
            }
            foreach (MatchFieldNode node in criteriaNode.ChildNodes)
            {
                treeNode.Nodes.Add(InitCriteriaNode(node));
            }

            return treeNode;
        }

        private void ShowMergeCriteria()
        {
            if (_CurrEventType == null)
            {
                return;
            }

            this.treeViewMatchCriteria.Nodes.Clear();
            this.treeViewMatchCriteria.Nodes.Add(InitCriteriaNode(_CurrEventType.MatchCriteria.Root));
            this.treeViewMatchCriteria.ExpandAll();
        }

        //private void ShowMergeFields()
        //{
        //    this.lvMergeFields.Items.Clear();
        //    if (_CurrIOChannel == null)
        //        return;
        //    if (_CurrEventType == null)
        //        return;
        //    MergeFields mgfs = _CurrEventType.MergeFields;
        //    for (int i = 0; i < mgfs.Count; i++)
        //    {
        //        ListViewItem lvi = lvMergeFields.Items.Add((i + 1).ToString());

        //        MergeField f = (MergeField)mgfs[i];
        //        lvi.Tag = f;
        //        lvi.SubItems.Add(f.Table);
        //        lvi.SubItems.Add(f.FieldName);
        //    }
        //}

        private void ShowMergeFieldMappings()
        {
            this.lvMergeFields.Items.Clear();
            if (_CurrIOChannel == null)
                return;
            if (_CurrEventType == null)
                return;
            MergeFieldMappings mfms = _CurrEventType.MergeFieldMappings;
            for (int i = 0; i < mfms.Count; i++)
            {
                ListViewItem lvi = lvMergeFields.Items.Add((i + 1).ToString());

                MergeFieldMapping m = (MergeFieldMapping)mfms[i];
                lvi.Tag = m;
                lvi.SubItems.Add(m.OutboundTable + "." + m.OutboundField + "(Out) <== " + m.InboundTable + "." + m.InboundField);
            }
        }

        private void ShowFilterFields()
        {
            this.lViewFilter.Items.Clear();
            if (_CurrIOChannel == null)
                return;
            if (_CurrEventType == null)
                return;
            FilterFieldList ffList = _CurrEventType.FilterList;
            for (int i = 0; i < ffList.Count; i++)
            {
                SubFilterFieldList sffl = (SubFilterFieldList)ffList[i];
                for (int j = 0; j < sffl.Count; j++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("(");
                    FilterField ff = (FilterField)sffl[j];
                    sb.Append(ff.Table + "." + ff.Field + " " + ff.Logic + " " + ff.LogicValue);
                    sb.Append(")");

                    if (j < sffl.Count - 1)
                    {
                        sb.Append(" OR ");
                    }

                    if (j == 0)
                    {
                        ListViewItem lvi = lViewFilter.Items.Add((i + 1).ToString() + " (AND) ");
                        lvi.Tag = i.ToString();
                        lvi.SubItems.Add(sb.ToString());
                    }
                    else
                    {
                        ListViewItem lvi = lViewFilter.Items.Add("");
                        lvi.SubItems.Add(sb.ToString());
                    }


                }


            }
        }



        EventType _CurrEventType = null;

        private void clbEventTypeList_Click(object sender, EventArgs e)
        {
            //if (clbEventTypeList.SelectedIndex < 0) return;

            //_CurrEventType = (EventType)(_CurrIOChannel.EventTypeList[clbEventTypeList.SelectedIndex]);
            //_CurrEventType.Enabled = clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex);

            //this.ShowPKFields();
            //this.ShowMergeFields();
            //this.ShowFilterFields();
            //this.ckbIsMerging.Checked = _CurrEventType.Merging;

            //RefreshForm();

            //RefreshMergeRecord(this.clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex));
            //RefreshFilterField(this.clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex));
        }

        private void clbEventTypeList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (clbEventTypeList.SelectedIndex < 0) return;

            _CurrEventType.Enabled = !this.clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex);

            RefreshForm();
            RefreshMergeRecord(_CurrEventType.Enabled);
            RefreshFilterField(_CurrEventType.Enabled);
        }



        private void btSelPKFields_Click(object sender, EventArgs e)
        {
            bool bWizard = false;
            if (sender == ckbIsMerging)
                bWizard = true;
            //FSelFields frm = new FSelFields();
            FConfigMergeCriterias frm = new FConfigMergeCriterias();
            if (frm.ShowDialog(this, _CurrEventType.MatchCriteria, bWizard) == DialogResult.OK)
            {
                //this.ShowPKFields(); 
                _CurrEventType.MatchCriteria = frm.CriteriaTree;
                this.ShowMergeCriteria();
            }
        }

        private void btSelMergeFields_Click(object sender, EventArgs e)
        {
            bool bWizard = false;
            if (sender == ckbIsMerging)
                bWizard = true;

            //FSelFields frm = new FSelFields();
            FConfigMergeMappings frm = new FConfigMergeMappings();
            if (frm.ShowDialog(this, _CurrEventType.MergeFieldMappings, bWizard) == DialogResult.OK)
            {
                _CurrEventType.MergeFieldMappings = frm.MergeFieldMappings;
                //this.ShowMergeFields();
                this.ShowMergeFieldMappings();
            }
        }

        private void ckbIsMerging_Click(object sender, EventArgs e)
        {
            if (_CurrEventType != null)
                _CurrEventType.Merging = ckbIsMerging.Checked;

            if (_CurrEventType.Merging)
            {
                this.btSelPKFields_Click(sender, e);
                //OutboundView just for prebuild sql statement.it will use real view name in trigger.
                if (!string.IsNullOrEmpty(_CurrEventType.MatchCriteria.GetSQLStatement("OutboundView")))
                {
                    this.btSelMergeFields_Click(sender, e);
                }


                if (string.IsNullOrEmpty(_CurrEventType.MatchCriteria.GetSQLStatement("OutboundView")) || this.lvMergeFields.Items.Count < 1)
                {
                    //MessageBox.Show("You must select PKCriteria and Merge Fields!");
                    this.ckbIsMerging.Checked = false;
                    this._CurrEventType.Merging = false;
                }
            }

            this.RefreshForm();
            this.RefreshMergeRecord(_CurrEventType.Enabled);
        }


        private void btnEditFilter_Click(object sender, EventArgs e)
        {
            formFilter fFilter = new formFilter();
            FilterFieldList ffList = _CurrEventType.FilterList.Clone();
            if (fFilter.ShowDialog(this, ffList) == DialogResult.OK)
            {
                _CurrEventType.FilterList = ffList.Clone();
                this.ShowFilterFields();
            }
        }

        private void clbEventTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbEventTypeList.SelectedIndex < 0) return;

            _CurrEventType = (EventType)(_CurrIOChannel.EventTypeList[clbEventTypeList.SelectedIndex]);
            _CurrEventType.Enabled = clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex);

            //this.ShowPKFields();
            this.ShowMergeCriteria();
            //this.ShowMergeFields();
            this.ShowMergeFieldMappings();
            this.ShowFilterFields();
            this.ckbIsMerging.Checked = _CurrEventType.Merging;

            RefreshForm();

            RefreshMergeRecord(this.clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex));
            RefreshFilterField(this.clbEventTypeList.GetItemChecked(clbEventTypeList.SelectedIndex));
        }



    }


}