using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLOutboundAdapterConfiguration.Forms;

namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    public partial class StatementPage : UserControl
    {
        #region Local members
        SQLOutboundChanel channel;
        string interfaceName;
        public bool IsChanged
        {
            get { return lblIsChanged.Visible; }
            set { lblIsChanged.Visible = value; }
        }
        #endregion

        #region Constructor
        public StatementPage(SQLOutboundChanel ch)
        {
            InitializeComponent();
            channel = ch;
            interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
        }
        #endregion

        #region Creat, Show and Save SP
        public void CreatStatement()
        {
            txtSPStatement.Text = RuleControl.GetOutboundSP(interfaceName, channel.Rule);
            lblIsChanged.Visible = false;
        }

        public void ShowStatement()
        {
            txtSPStatement.Text = channel.SPStatement;
            lblIsChanged.Visible = true;
        }

        public void Save()
        {
            if (IsChanged)
            {
                channel.SPStatement = txtSPStatement.Text;
            }
            else
            {
                channel.SPStatement = RuleControl.GetOutboundSP(interfaceName, channel.Rule);
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
