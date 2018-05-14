using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HYS.Adapter.Base;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLOutboundAdapterConfiguration.Controls;

namespace HYS.SQLOutboundAdapterConfiguration
{
    public partial class SQLOutboundConfiguration : Form, IConfigUI
    {
        #region Local members
        ConnectionConfig DBconfig;

        ActiveMode activeControl;
        PassiveMode passiveControl;
        #endregion

        #region Constructor
        public SQLOutboundConfiguration()
        {
            InitializeComponent();
            activeControl = new ActiveMode();
            passiveControl = new PassiveMode();
            Initialization();
        }
        #endregion

        #region Initialization and Save
        private void Initialization()
        {
            DBconfig = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig;

            activeControl.ShowInformation();
            passiveControl.ShowInformation();

            if (DBconfig.InteractType == InteractType.Active)
            {
                radioBtnActive.Checked = true;
                this.pContainer.Controls.Add(activeControl);
                activeControl.Dock = DockStyle.Fill;
            }
            else {
                radioBtnPassive.Checked = true;
                this.pContainer.Controls.Add(passiveControl);
                passiveControl.Dock = DockStyle.Fill;
            }
        }

        private void Save()
        {
            #region Interact Information
            if (radioBtnActive.Checked == true)
            {
                DBconfig.InteractType = InteractType.Active;
                activeControl.Save();
            }
            else
            {
                DBconfig.InteractType = InteractType.Passtive;
                //passiveControl.Save();
            }
            #endregion

            //Save to file
            if (!SQLOutAdapterConfigMgt.Save(SQLOutAdapterConfigMgt._FileName))
            {
                if (SQLOutAdapterConfigMgt.LastException != null)
                    MessageBox.Show(SQLOutAdapterConfigMgt.LastException.Message);
            }
            //this.Close();
        }
        #endregion

        #region Controls events
        private void radioBtnPassive_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnActive.Checked == true)
            {
                this.pContainer.Controls.Remove(passiveControl);
                this.pContainer.Controls.Add(activeControl);
                activeControl.Dock = DockStyle.Fill;
            }
            else
            {
                this.pContainer.Controls.Remove(activeControl);
                this.pContainer.Controls.Add(passiveControl);
                passiveControl.Dock = DockStyle.Fill;
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region IConfigUI Members
        public Control GetControl()
        {
            this.Controls.Remove(panelBottom);
            return this;
        }

        public bool LoadConfig()
        {
            return true;
        }

        public bool SaveConfig()
        {
            Save();
            return true;
        }
        #endregion
    }
}