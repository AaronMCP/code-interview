using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Config;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.License2;
using System.IO;
using System.Xml;

namespace LightMWLOutConfig
{
    public partial class FormMain : Form
    {
        private AdapterServiceCfgMgt _cfgMgt;

        public FormMain()
        {
            InitializeComponent();

            this.buttonOK.Enabled = LoadSetting();
        }

        private bool LoadSetting()
        {
            _cfgMgt = new AdapterServiceCfgMgt();
            _cfgMgt.FileName = Application.StartupPath + "\\" + _cfgMgt.FileName;

            if (_cfgMgt.Load())
            {
                this.textBoxDBCnn.Text = _cfgMgt.Config.DataDBConnection;
                return true;
            }
            else
            {
                MessageBox.Show(this, "Cannot open configuration file: \r\n" + _cfgMgt.FileName +
                    "\r\n\r\nError Message:\r\n" + ((_cfgMgt.LastError == null) ? "" : _cfgMgt.LastError.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool SaveSetting()
        {
            if (_cfgMgt == null) return false;

            string dbcnn = this.textBoxDBCnn.Text.Trim();

            if (string.IsNullOrEmpty(dbcnn))
            {
                MessageBox.Show(this, "Database connection string should not be empty.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            _cfgMgt.Config.DataDBConnection = dbcnn;

            if (!_cfgMgt.Save())
            {
                MessageBox.Show(this, "Cannot save configuration file: \r\n" + _cfgMgt.FileName +
                    "\r\n\r\nError Message:\r\n" + ((_cfgMgt.LastError == null) ? "" : _cfgMgt.LastError.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return UpdateMWLDBConnection(dbcnn) && UpdateDeviceDir();
        }
        private bool UpdateMWLDBConnection(string dbcnn)
        {
            string file = Path.Combine(Application.StartupPath, "MWLServer.xml");

            if (!File.Exists(file))
            {
                MessageBox.Show(this, "Cannot find file: " + file,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string xpath = @"/MWLServerConfig/GWDataDBConnection";

            try
            {
                if (!Path.IsPathRooted(file)) file = Path.Combine(Application.StartupPath, file);

                XmlDocument doc = new XmlDocument();
                doc.Load(file);

                XmlNode node = doc.SelectSingleNode(xpath);
                if (node == null)
                {
                    MessageBox.Show(this, "Cannot find XML node with XPath: " + xpath,
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    node.InnerXml = dbcnn;
                }

                doc.Save(file);

                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.ToString(),
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool UpdateDeviceDir()
        {
            DeviceDirManager deviceMgt = new DeviceDirManager();
            deviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;

            if (!deviceMgt.LoadDeviceDir())
            {
                MessageBox.Show(this, "Cannot open DeviceDir file to write. " + deviceMgt.FileName);
                return false;
            }

            deviceMgt.DeviceDirInfor.Header.RefDeviceName = GWLicenseHelper.LightMWLOutDeviceName;

            if (!deviceMgt.SaveDeviceDir())
            {
                MessageBox.Show(this, "Cannot save DeviceDir file. " + deviceMgt.FileName);
                return false;
            }

            return true;
        }
        private void TestDBConnection()
        {
            bool res;

            this.Cursor = Cursors.WaitCursor;
            DataBase db = new DataBase(this.textBoxDBCnn.Text.Trim());
            res = db.TestDBConnection();
            this.Cursor = Cursors.Default;

            if (res)
            {
                MessageBox.Show(this, "Test DB connection succeeded.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "Test DB connection failed.\r\n\r\nError Message"
                     + ((db.LastError == null) ? "" : db.LastError.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            TestDBConnection();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting()) this.Close();
        }
    }
}
