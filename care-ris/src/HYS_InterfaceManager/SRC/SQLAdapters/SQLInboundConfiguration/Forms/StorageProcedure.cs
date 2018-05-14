using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLInboundAdapterConfiguration.Controls;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class StorageProcedure : Form
    {
        #region Local members
        XCollection<SQLInboundChanel> channelSet;
        SQLInboundChanel channel;
        string type;
        bool IsExist {
            get { return lblExist.Visible; }
        }

        List<string> SPNameSet = new List<string>();
        ParameterPage parameterPage;
        StatementPage statementPage;
        #endregion

        #region Constructor
        public StorageProcedure(XCollection<SQLInboundChanel> channels)
        {
            InitializeComponent();
            channelSet = channels;
            channel = new SQLInboundChanel();
            type = "Add";
            this.Text = "Add Storage Procedure";

            parameterPage = new ParameterPage(channel);
            statementPage = new StatementPage(channel);
            GetSPNameSet(channels);

            Initialization();
        }

        public StorageProcedure(XCollection<SQLInboundChanel> channels,int index)
        {
            InitializeComponent();
            channelSet = channels;
            channel = channels[index];
            type = "Edit";
            this.Text = "Edit Storage Procedure";

            parameterPage = new ParameterPage(channel);
            statementPage = new StatementPage(channel);
            GetSPNameSet(channels);

            ShowInformation();
        }
        #endregion
       
        #region Initialization, Show and Save
        //Use for add SP
        private void Initialization()
        {
            this.txtSPName.Text = "sp_" + Program.DeviceMgt.DeviceDirInfor.Header.Name + "_";
            
            this.panelMain.Controls.Add(parameterPage);
            parameterPage.Dock = DockStyle.Fill;
        }

        //Use for modify SP
        private void ShowInformation() {
            txtSPName.Text = channel.SPName;
            
            if (!channel.Modified)
            {
                txtSPName.Enabled = true;

                this.panelMain.Controls.Add(parameterPage);
                parameterPage.Dock = DockStyle.Fill;
                radioBtnParameter.Checked = true;
                parameterPage.ShowParameter();
            }
            else {
                txtSPName.Enabled = false;

                this.panelMain.Controls.Add(statementPage);
                statementPage.Dock = DockStyle.Fill;
                radioBtnStatement.Checked = true;
                statementPage.ShowStatement();
            }
        }

        private void Save() {
            channel.Rule.RuleID = txtSPName.Text.Substring(SPPrefixLenth);
            channel.SPName = txtSPName.Text;
            if (statementPage.IsChanged)
            {
                channel.Modified = true;
            }
            else {
                channel.Modified = false;
            }

            parameterPage.Save();
            statementPage.Save();

            if (type == "Add") {
                channelSet.Add(channel);
            }
        }
        #endregion

        #region Controls events
        private void radioBtnStatement_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnStatement.Checked == true)
            {
                if (!IsSPNameValid(txtSPName.Text))
                {
                    radioBtnParameter.Checked = true;
                    txtSPName.Focus();
                    return;
                }

                if (IsExist)
                {
                    MessageBox.Show( "Storage procedure existed!", "Storage Procedure Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    radioBtnParameter.Checked = true;
                    txtSPName.Focus();
                    return;
                }

                if (!statementPage.IsChanged)
                {
                    txtSPName.Enabled = false;
                    channel.Rule.RuleID = txtSPName.Text.Substring(SPPrefixLenth);
                    this.panelMain.Controls.Remove(parameterPage);
                    this.panelMain.Controls.Add(statementPage);
                    statementPage.Dock = DockStyle.Fill;
                    statementPage.CreatStatement();
                }
            }
            else
            {
                txtSPName.Enabled = true;

                if (statementPage.IsChanged)
                {
                    if (MessageBox.Show( "If you change the view, all the change you did for the storage procedere will be lost.\nAre you sure to change the view?", "Operation Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        radioBtnStatement.Checked = true;
                        return;
                    }

                    this.panelMain.Controls.Remove(statementPage);
                    this.panelMain.Controls.Add(parameterPage);
                    parameterPage.Dock = DockStyle.Fill;
                    parameterPage.ShowParameter();
                    statementPage.IsChanged = false;
                }
                else
                {
                    this.panelMain.Controls.Remove(statementPage);
                    this.panelMain.Controls.Add(parameterPage);
                    parameterPage.Dock = DockStyle.Fill;
                    parameterPage.ShowParameter();
                }
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsSPNameValid(txtSPName.Text.Substring(SPPrefixLenth))) 
            {
                txtSPName.Focus();
                return;
            }

            if (IsExist)
            {
                MessageBox.Show( "Storage procedure existed!", "Storage Procedure Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSPName.Focus();
                return;
            }  
            Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Set SPName Prefix
        public string GetSPPrefix()
        {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            return "sp_" + interfaceName + "_";
        }

        private int _SPPrefixLenth;
        public int SPPrefixLenth
        {
            get
            {
                _SPPrefixLenth = GetSPPrefix().Length;
                return _SPPrefixLenth;
            }
        }
        #endregion

        #region Check the identity and valid of SP name
        private void GetSPNameSet(XCollection<SQLInboundChanel> channels)
        {
            foreach (SQLInboundChanel channel in channels)
            {
                SPNameSet.Add(channel.SPName);
            }
        }

        private bool IsSPNameValid(string name)
        {
            if (name == GetSPPrefix())
            {
                MessageBox.Show( "Storage procedure name is not valid!", "Storage Procedure Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!CheckItemValid.IsValid(name))
            {
                MessageBox.Show( "The storage procedure name should only contain character, number or '_', and after prefix \"" + GetSPPrefix() + "\", the later should begin with character, please input another name.",
                    "Storage Procedure Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsSPNameExist(string SPName) {
            if (type == "Add")
            {
                if (CheckItemValid.IsContain(SPName, SPNameSet, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            else
            {
                if (CheckItemValid.IsContain(SPName, SPNameSet, StringComparison.OrdinalIgnoreCase) && (txtSPName.Text != channel.SPName))
                {
                    return true;
                }
            }

            return false;                
        }
        #endregion

        #region TextBox SPName events
        private void txtSPName_TextChanged(object sender, EventArgs e)
        {
            if (txtSPName.Text.Length < SPPrefixLenth)
            {
                txtSPName.Text = GetSPPrefix();
                txtSPName.SelectionStart = SPPrefixLenth;
            }

            #region Check identity of SP
            if (IsSPNameExist(txtSPName.Text)) {
                lblExist.Visible = true;
            }
            else
            {
                lblExist.Visible = false;
            }
            #endregion
        }

        private void txtSPName_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSPName.SelectionStart < SPPrefixLenth)
            {
                txtSPName.SelectionStart = SPPrefixLenth;
            }
        }

        private void txtSPName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSPName.Select(SPPrefixLenth, txtSPName.Text.Length - SPPrefixLenth);
        }

        private void txtSPName_MouseLeave(object sender, EventArgs e)
        {
            if (txtSPName.SelectionStart < SPPrefixLenth)
            {
                txtSPName.SelectionStart = SPPrefixLenth;
            }
        }

        private void txtSPName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSPName.SelectionStart <= SPPrefixLenth)    //用来处理Left键以外的按键信息的
            {
                txtSPName.SelectionStart = SPPrefixLenth;     //如果按左移键，该事件在光标左移之前发生，所以光标总会在SPPrefixLenth王位置的左边一格
            }
            if (e.Shift == true)
            {
                if (e.KeyCode == Keys.Left)
                {

                }
            }
        }

        private void txtSPName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSPName.SelectionStart < SPPrefixLenth)     //处理KeyDown未能解决的一个问题，即在SPPrefixLenth临界点时重置光标
            {
                txtSPName.SelectionStart = SPPrefixLenth;
                if (e.KeyCode == Keys.Back)
                {
                    txtSPName.Select(0,SPPrefixLenth-1);
                    txtSPName.SelectedText = GetSPPrefix();
                }
            }
        }
        #endregion
    }
}