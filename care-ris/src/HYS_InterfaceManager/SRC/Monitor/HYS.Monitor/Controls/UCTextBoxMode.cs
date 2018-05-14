using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Monitor.Utility;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;
using HYS.Adapter.Monitor.Objects;

namespace HYS.Adapter.Monitor.Controls
{
    public partial class UCTextBoxMode : UserControl
    {
        UCAdvancedMode parentControl;

        public UCTextBoxMode(UCAdvancedMode control)
        {
            InitializeComponent();
            parentControl = control;
        }

        public void CreatStatement(XCollection<QueryCriteriaItem> filterList)
        {
            txtStatement.Text = QueryRuleControl.GetFilterString(filterList);
            parentControl.IsChanged = false;
        }

        public void ShowInfo() {
            this.txtStatement.Text = parentControl.FilterDaraInfo.FilterText;
            parentControl.IsChanged = true;
        }

        private void txtStatement_TextChanged(object sender, EventArgs e)
        {
            parentControl.IsChanged = true;
        }

        public string GetQueryString() {
            return txtStatement.Text;
        }

        public void Save() {
            parentControl.FilterDaraInfo.FilterMode = FilterMode.AdvancedText;
            parentControl.FilterDaraInfo.FilterText = txtStatement.Text;
        }
    }
}
