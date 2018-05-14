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
    public partial class StatementPage : UserControl
    {
        #region Local members
        SQLInboundChanel channel;
        string interfaceName;
        public bool IsChanged
        {
            get { return lblIsChanged.Visible; }
            set { lblIsChanged.Visible = value; }
        }
        #endregion

        #region Constructor
        public StatementPage(SQLInboundChanel ch)
        {
            InitializeComponent();
            channel = ch;
            interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
        }
        #endregion

        #region Creat, Show and Save SP
        public void CreatStatement()
        {
            txtSPStatement.Text = RuleControl.GetInboundSP(interfaceName, channel.Rule, false);
            lblIsChanged.Visible = false;
        }

        public void ShowStatement() {
            txtSPStatement.Text = channel.SPStatement;
            lblIsChanged.Visible = true;
        }

        public void Save() {
            if (IsChanged)
            {
                channel.SPStatement = txtSPStatement.Text;
            }
            else {
                channel.SPStatement = RuleControl.GetInboundSP(interfaceName, channel.Rule, false);
            }
        }
        #endregion

        #region Controls events
        private void txtSPStatement_TextChanged(object sender, EventArgs e)
        {
            lblIsChanged.Visible = true;
        }
        #endregion

    }
}
