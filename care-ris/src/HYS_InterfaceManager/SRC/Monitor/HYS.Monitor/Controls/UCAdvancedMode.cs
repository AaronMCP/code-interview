using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;
using HYS.Adapter.Monitor.Objects;

namespace HYS.Adapter.Monitor.Controls
{
    public partial class UCAdvancedMode : UserControl
    {
        #region Local members
        private UCListviewMode pageListview;
        private UCTextBoxMode pageTextBox;
        private FilterDataInfo _filterDaraInfo;
        public FilterDataInfo FilterDaraInfo {
            get { return _filterDaraInfo; }
            set { _filterDaraInfo = value; }
        }

        public bool IsChanged
        {
            get { return lblChanged.Visible; }
            set { lblChanged.Visible = value; }
        }
        #endregion

        public UCAdvancedMode(FilterDataInfo queryData)
        {
            InitializeComponent();

            _filterDaraInfo = queryData;
            pageListview = new UCListviewMode(_filterDaraInfo);
            pageTextBox = new UCTextBoxMode(this);


            Initialization();
        }

        private void Initialization()
        {
            if (_filterDaraInfo.FilterMode == FilterMode.AdvancedListView)
            {
                radioButtonListview.Checked = true;
                this.pMain.Controls.Add(pageListview);
                pageListview.Dock = DockStyle.Fill;
            }
            else
            {
                radioBtnStatement.Checked = true;
                this.pMain.Controls.Add(pageTextBox);
                pageTextBox.Dock = DockStyle.Fill;
            }
        }

        public string GetQueryStatement()
        {
            if (radioButtonListview.Checked)
            {
                return pageListview.GetQueryString();
            }
            else
            {
                return pageTextBox.GetQueryString();
            }
        }

        public void ShowInfo() {
            if (_filterDaraInfo.FilterMode == FilterMode.AdvancedListView)
            {
                pageListview.ShowListView();
            }
            else
            {
                pageTextBox.ShowInfo();
            }
        }

        public void Save()
        {
            if (!IsChanged)
            {
                pageListview.Save();
            }
            else
            {
                pageTextBox.Save();
            }
        }

        private void radioButtonText_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnStatement.Checked == true)
            {
                if (!IsChanged)
                {
                    this.pMain.Controls.Remove(pageListview);
                    this.pMain.Controls.Add(pageTextBox);
                    pageTextBox.Dock = DockStyle.Fill;
                    pageTextBox.CreatStatement(pageListview.FilterItemList);
                }
            }
            else
            {
                if (IsChanged)
                {
                    if (MessageBox.Show(this, "If you change the view, all the change you did for the storage procedere will be lost.\nAre you sure to change the view?", "Operation Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        radioBtnStatement.Checked = true;
                        return;
                    }
                }

                this.pMain.Controls.Remove(pageTextBox);
                this.pMain.Controls.Add(pageListview);
                pageListview.Dock = DockStyle.Fill;
                IsChanged = false;
            }
        }
    }
}
