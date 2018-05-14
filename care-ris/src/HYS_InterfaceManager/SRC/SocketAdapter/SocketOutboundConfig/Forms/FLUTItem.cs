using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Rule;
using HYS.SocketAdapter.Configuration;
using HYS.Common.Xml;

namespace HYS.SocketAdapter.SocketOutboundAdapterConfiguration.Forms
{
    public partial class FLUTItem : Form
    {
        #region Local members
        FLUTable parentlForm;
        string type;
        LookupItem lookupItem;
        #endregion

        #region Constructor
        public FLUTItem(FLUTable frm)
        {
            InitializeComponent();
            parentlForm = frm;
            type = "Add";
            lookupItem = new LookupItem();
        }

        public FLUTItem(FLUTable frm, int index)
        {
            InitializeComponent();
            parentlForm = frm;
            type = "Edit";
            lookupItem = parentlForm.LUItemList[index];

            ShowItem();
        }
        #endregion

        #region Show and Save
        private void ShowItem()
        {
            this.txtSource.Text = lookupItem.SourceValue;
            this.txtTarget.Text = lookupItem.TargetValue;
        }

        private void Save()
        {
            lookupItem.SourceValue = this.txtSource.Text;
            lookupItem.TargetValue = this.txtTarget.Text;

            if (type == "Add")
            {
                parentlForm.LUItemList.Add(lookupItem);
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            parentlForm.ShowLUItemList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}