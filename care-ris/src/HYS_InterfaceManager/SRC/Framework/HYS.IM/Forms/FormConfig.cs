using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Config;
using HYS.Common.DataAccess;

namespace HYS.IM.Forms
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            LoadConfig();

            this.textBoxDBConnectionConfig.TextChanged += new EventHandler(textBoxDBConnectionConfig_TextChanged);
            this.textBoxDBConnectionData.TextChanged += new EventHandler(textBoxDBConnectionData_TextChanged);
            this.comboBoxView.SelectedIndexChanged += new EventHandler(comboBoxView_SelectedIndexChanged);
            this.textBoxOSQLPath.TextChanged += new EventHandler(textBoxOSQLPath_TextChanged);
            this.textBoxOSQLArg.TextChanged += new EventHandler(textBoxOSQLArg_TextChanged);
        }

        private void textBoxDBConnectionConfig_TextChanged(object sender, EventArgs e)
        {
            bDBChange = bChange = true;
        }
        private void textBoxDBConnectionData_TextChanged(object sender, EventArgs e)
        {
            bDBChange = bChange = true;
        }
        private void comboBoxView_SelectedIndexChanged(object sender, EventArgs e)
        {
            bChange = true;
        }
        private void textBoxOSQLPath_TextChanged(object sender, EventArgs e)
        {
            bOsqlChange = bChange = true;
        }
        private void textBoxOSQLArg_TextChanged(object sender, EventArgs e)
        {
            bOsqlChange = bChange = true;
        }
        
        private bool bChange = false;
        private bool bDBChange = false;
        private bool bOsqlChange = false;

        private void LoadConfig()
        {
            //if (!Program.ConfigMgt.Load())
            //{
            //    Program.Log.Write("Load config file failed. " + Program.ConfigMgt.FileName);
            //    Program.Log.Write(Program.ConfigMgt.LastError);

            //    MessageBox.Show(this, "Load config file failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            this.textBoxDBConnectionConfig.Text = Program.ConfigMgt.Config.ConfigDBConnection;
            this.textBoxDBConnectionData.Text = Program.ConfigMgt.Config.DataDBConnection;
            this.textBoxOSQLPath.Text = Program.ConfigMgt.Config.OSqlFileName;
            this.textBoxOSQLArg.Text = Program.ConfigMgt.Config.OSqlParameter;

            if (Program.ConfigMgt.Config.ShowDeviceViewWhenStartup)
            {
                this.comboBoxView.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxView.SelectedIndex = 1;
            }
        }
        private void SaveConfig()
        {
            Program.ConfigMgt.Config.ConfigDBConnection = this.textBoxDBConnectionConfig.Text;
            Program.ConfigMgt.Config.DataDBConnection = this.textBoxDBConnectionData.Text;
            Program.ConfigMgt.Config.OSqlFileName = this.textBoxOSQLPath.Text;
            Program.ConfigMgt.Config.OSqlParameter = this.textBoxOSQLArg.Text;

            if (this.comboBoxView.SelectedIndex == 0)
            {
                Program.ConfigMgt.Config.ShowDeviceViewWhenStartup = true;
            }
            else
            {
                Program.ConfigMgt.Config.ShowDeviceViewWhenStartup = false;
            }

            if (!Program.ConfigMgt.Save())
            {
                Program.Log.Write("Save config file failed. " + Program.ConfigMgt.FileName);
                Program.Log.Write(Program.ConfigMgt.LastError);

                MessageBox.Show(this, "Save config file failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bDBChange)
            {
                if (!ScriptControl.UpdateDBConnection(
                    Program.ConfigMgt.Config.InterfaceFolder,
                    Program.ConfigMgt.Config.DataDBConnection,
                    Program.ConfigMgt.Config.ConfigDBConnection))
                {
                    Program.Log.Write("Update database connection in interfaces failed. ");
                    Program.Log.Write(GCError.LastError);

                    MessageBox.Show(this, "Update database connection in interfaces failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show(this, "Database connection will be applied after the interfaces are restarted. Do you want to restart the interfaces now?",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    GCInterfaceManager mgt = new GCInterfaceManager(Program.ConfigDB, Program.ConfigMgt.Config.InterfaceFolder);
                    if (!mgt.RestartInterfaces())
                    {
                        Program.Log.Write("Restart interfaces failed. ");
                        Program.Log.Write(GCError.LastError);

                        MessageBox.Show(this, "Restart interfaces failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (bOsqlChange)
            {
                string path = Application.StartupPath + "\\..\\db_install";
                string fileName = path + "\\DropDB.bat";
                string logpath = path + "\\temp";
                if (File.Exists(fileName))
                {
                    Program.Log.Write("Find uninstall file: " + fileName);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("mkdir \"" + logpath + "\"");
                    sb.Append("\"" + Program.ConfigMgt.Config.OSqlFileName + "\" ");
                    sb.Append(Program.ConfigMgt.Config.OSqlParameter);
                    sb.Append(" -d master -i \"" + path + "\\DropDB.sql\"");
                    sb.Append(" > \"" + logpath + "\\GWGatewayDBUninstall.log\"");
                    string strOsql = sb.ToString();

                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.Write(strOsql);
                    }

                    Program.Log.Write("Uninstall file updated. " + fileName);
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "Cannot find uninstall file: " + fileName);
                }
            }

            if (bChange &&
                MessageBox.Show(this, "Configuration will be applied after the program is restarted. Do you want to restart the program now?",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.Restart();
            }
            else
            {
                this.Close();
            }
        }
        private void TestOSQL(string file, string arg)
        {
            try
            {
                Process proc = Process.Start(file, arg);
                proc.EnableRaisingEvents = false;
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                MessageBox.Show(this,"Cannot invoke OSQL.EXE.","OSQL.EXE test",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void TestDBConnection(string cnnString)
        {
            DataBase db = new DataBase(cnnString);
            if (db.TestDBConnection())
            {
                MessageBox.Show(this, "Connect to database successfully.", "Database Test",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string errinfor = "";
                Exception err = db.LastError;
                if (err != null) errinfor = err.Message;

                MessageBox.Show(this, "Connect to database failed : " + errinfor,
                    "Database Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonDBTestConfig_Click(object sender, EventArgs e)
        {
            TestDBConnection(this.textBoxDBConnectionConfig.Text);
        }
        private void buttonDBTestData_Click(object sender, EventArgs e)
        {
            TestDBConnection(this.textBoxDBConnectionData.Text);
        }

        private void buttonOSQLTest_Click(object sender, EventArgs e)
        {
            TestOSQL(this.textBoxOSQLPath.Text, this.textBoxOSQLArg.Text);
        }
    }
}