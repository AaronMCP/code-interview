using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HYS.Adapter.Base;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLInboundAdapterConfiguration.Forms;
using HYS.SQLInboundAdapterConfiguration.Controls;

namespace HYS.SQLInboundAdapterConfiguration
{
    public partial class SQLInboundConfiguration : Form, IConfigUI
    {   
        #region Local members
        ConnectionConfig DBconfig;

        ActiveMode activeControl;
        PassiveMode passiveControl;
        AccessMode accessControl;
        #endregion

        #region Constructor
        public SQLInboundConfiguration()
        {
            InitializeComponent();
            activeControl = new ActiveMode();
            passiveControl = new PassiveMode();
            accessControl = new AccessMode();
            Initialization();
        }
        #endregion

        #region Initialization and Save
        private void Initialization()
        {
            DBconfig = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig;
            activeControl.ShowInformation();
            passiveControl.ShowInformation();
            accessControl.ShowInformation();

            if (DBconfig.InteractType == InteractType.Active)
            {
                radioBtnActive.Checked = true;
                this.pContainer.Controls.Add(activeControl);
                activeControl.Dock = DockStyle.Fill;
            
            }
            else if (DBconfig.InteractType == InteractType.Passtive)
            {
                radioBtnPassive.Checked = true;
                this.pContainer.Controls.Add(passiveControl);
                passiveControl.Dock = DockStyle.Fill;
                
            }
            else
            {
                radioBtnAccess.Checked = true;
                this.pContainer.Controls.Add(accessControl);
                accessControl.Dock = DockStyle.Fill;

            }
        }

        private void Save()
        { 
            if (radioBtnActive.Checked == true)
            {
                DBconfig.InteractType = InteractType.Active;
                activeControl.Save();
            }
            else if (radioBtnPassive.Checked == true)
            {
                DBconfig.InteractType = InteractType.Passtive;
                //passiveControl.Save();
            }
            else
            {
                DBconfig.InteractType = InteractType.Access;
                accessControl.Save();
            }

            //Save to file
            if (!SQLInAdapterConfigMgt.Save(SQLInAdapterConfigMgt._FileName))
            {
                if (SQLInAdapterConfigMgt.LastException != null)
                    MessageBox.Show(SQLInAdapterConfigMgt.LastException.Message);
            }
            //this.Close();
        }
        #endregion

        #region Controls events
        private void radioBtnPassive_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnActive.Checked == true)
            {
                this.pContainer.Controls.Clear();
                this.pContainer.Controls.Add(activeControl);
                activeControl.Dock = DockStyle.Fill;

                //activeControl.ShowInformation();
            }
            else if (radioBtnPassive.Checked == true)
            {
                this.pContainer.Controls.Clear();
                this.pContainer.Controls.Add(passiveControl);
                passiveControl.Dock = DockStyle.Fill;

                //passiveControl.ShowInformation();
            }
            else
            {
                this.pContainer.Controls.Clear();
                this.pContainer.Controls.Add(accessControl);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}