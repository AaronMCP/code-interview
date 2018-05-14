using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Composer.Forms
{
    public partial class FormService : Form
    {
        public FormService()
        {
            InitializeComponent();

            string filename = ConfigHelper.GetFullPath(Program.ConfigMgt.Config.ServiceConfigFileName);
            configMgt = new AdapterServiceCfgMgt(filename);
            this.textBoxLocation.Text = filename;
        }

        private AdapterServiceCfgMgt configMgt;

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            configMgt.FileName = this.textBoxLocation.Text;
            if (configMgt.Load())
            {
                this.propertyGridConfig.SelectedObject = configMgt.Config;
                this.propertyGridGC.SelectedObject = configMgt.Config.GarbageCollection;
                this.propertyGridGCTime.SelectedObject = configMgt.Config.GarbageCollection.ParticularTime;
            }
            else
            {
                MessageBox.Show(this, "Load config file failed: " + configMgt.FileName + "\r\n\r\n" + configMgt.LastError);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!configMgt.Save())
            {
                MessageBox.Show(this, "Save config file failed: " + configMgt.FileName + "\r\n\r\n" + configMgt.LastError);
            }
        }

        private void buttonTestDB_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase(configMgt.Config.DataDBConnection);
            if (db.TestDBConnection())
            {
                MessageBox.Show(this, "Test DB Connection succeeded.");
            }
            else
            {
                MessageBox.Show(this, "Test DB Connection failed.");
            }
        }
    }
}