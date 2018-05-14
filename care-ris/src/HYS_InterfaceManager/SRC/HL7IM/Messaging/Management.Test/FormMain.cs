using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Messaging.Management.Config;
using System.IO;
using HYS.Messaging.Management.Scripts;
using HYS.Messaging.Management.NTServices;

namespace Management.Test
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonConfigBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Management Service Config File|" + ServiceConfig.FileName + "|All Files|*.*";
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Multiselect = false;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            this.textBoxMgtSvcConfigFile.Text = dlg.FileName;
        }

        private void buttonScriptBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Bat Files|*.bat|All Files|*.*";
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Multiselect = false;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            this.textBoxScriptFileName.Text = dlg.FileName;
            this.textBoxScriptWorkingPath.Text = Path.GetDirectoryName(dlg.FileName);
        }

        private void buttonScriptLocalCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ScriptMgt.ExecuteBatFile(
                    this.textBoxScriptFileName.Text.Trim(),
                    this.textBoxScriptArgument.Text.Trim(),
                    this.textBoxScriptWorkingPath.Text.Trim(),
                    Program.Log)
                );
        }

        private void buttonScriptRemoteCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ScriptMgt.ExecuteBatFileRemote(
                    this.textBoxMgtSvcConfigFile.Text.Trim(),
                    this.textBoxScriptFileName.Text.Trim(),
                    this.textBoxScriptArgument.Text.Trim(),
                    this.textBoxScriptWorkingPath.Text.Trim(),
                    Program.Log)
                );
        }

        private void buttonSvcGetStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ServiceMgt.GetServiceStatus(
                    this.textBoxNTServiceName.Text.Trim(),
                    Program.Log).ToString()
                    );
        }

        private void buttonSvcStartLocal_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ServiceMgt.SetServiceStatus(
                    this.textBoxNTServiceName.Text.Trim(),
                    ServiceStatus.Running,
                    Program.Log).ToString()
                    );
        }

        private void buttonSvcStopLocal_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ServiceMgt.SetServiceStatus(
                    this.textBoxNTServiceName.Text.Trim(),
                    ServiceStatus.Stopped,
                    Program.Log).ToString()
                    );
        }

        private void buttonSvcStartRemote_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ServiceMgt.SetServiceStatusAndStartStyleRemote(
                    this.textBoxMgtSvcConfigFile.Text.Trim(),
                    this.textBoxNTServiceName.Text.Trim(),
                    ServiceStatus.Running,
                    Program.Log).ToString()
                    );
        }

        private void buttonSvcStopRemote_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ServiceMgt.SetServiceStatusAndStartStyleRemote(
                    this.textBoxMgtSvcConfigFile.Text.Trim(),
                    this.textBoxNTServiceName.Text.Trim(),
                    ServiceStatus.Stopped,
                    Program.Log).ToString()
                    );
        }
    }
}
