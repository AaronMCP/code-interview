using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HYS.Adapter.Base;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLInboundAdapterConfiguration.Forms;

namespace HYS.SQLInboundAdapterConfiguration.Controls
{
    public partial class AccessMode : UserControl
    {
        public ThrPartyAppConfig ThrPartyAppConfigObj;

        #region Constructor
        public AccessMode()
        {
            InitializeComponent();
            ThrPartyAppConfigObj = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig;
        }
        #endregion

        #region Show and Save
        public void ShowInformation()
        {
            this.numericUpDown.Value = ThrPartyAppConfigObj.TimerInterval;
            this.txtFilePath.Text = ThrPartyAppConfigObj.FilePath;
        }

        public bool Save()
        {
            //if (this.txtFilePath.Text == "")
            //{
            //    MessageBox.Show("File must be required");
            //    return false;
            //}

            ThrPartyAppConfigObj.TimerInterval = (int)numericUpDown.Value;
            ThrPartyAppConfigObj.FilePath = this.txtFilePath.Text;
            return true;
        }
        #endregion

        private void btnFilePathGet_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.Title = "Select File Path";
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = dlgOpenFile.FileName;
            }
        }
    }
}
