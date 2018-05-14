using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMain : Form
    {
        private void LoadDatabaseSetting()
        {
            this.textBoxOSQLArgument.Text = Program.ConfigMgt.Config.DBParameter.OSQLArgument;
            this.textBoxOSQLPath.Text = Program.ConfigMgt.Config.DBParameter.OSQLFileName;
        }
        private bool SaveDatabaseSetting()
        {
            Program.ConfigMgt.Config.DBParameter.OSQLArgument = this.textBoxOSQLArgument.Text.Trim();
            Program.ConfigMgt.Config.DBParameter.OSQLFileName = this.textBoxOSQLPath.Text.Trim();
            return true;
        }

        private void TestDBScript()
        {
            try
            {
                string file = this.textBoxOSQLPath.Text;
                string arg = this.textBoxOSQLArgument.Text;

                Process proc = Process.Start(file, arg);
                proc.EnableRaisingEvents = false;
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                System.Windows.Forms.MessageBox.Show(this, "Cannot invoke OSQL.EXE.", "OSQL.EXE test",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
