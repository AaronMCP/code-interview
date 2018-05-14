using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.Config
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxName.Text = Program.ConfigMgr.Config.HL7GatewayInterfaceName;
            this.textBoxPath.Text = Program.ConfigMgr.Config.IntegrationSolutionPath;
            this.textBoxGroup1.Text = Program.ConfigMgr.Config.MessageEntityGroup1;
            this.textBoxGroup2.Text = Program.ConfigMgr.Config.MessageEntityGroup2;
            this.checkBoxFlipDiagram.Checked = Program.ConfigMgr.Config.FlipDiagram;
        }
        private bool SaveSetting()
        {
            Program.ConfigMgr.Config.HL7GatewayInterfaceName = this.textBoxName.Text.Trim();
            Program.ConfigMgr.Config.IntegrationSolutionPath = this.textBoxPath.Text.Trim();
            Program.ConfigMgr.Config.MessageEntityGroup1 = this.textBoxGroup1.Text.Trim();
            Program.ConfigMgr.Config.MessageEntityGroup2 = this.textBoxGroup2.Text.Trim();
            Program.ConfigMgr.Config.FlipDiagram = this.checkBoxFlipDiagram.Checked;

            if (Program.ConfigMgr.Save())
            {
                return true;
            }
            else
            {
                Program.Log.Write(Program.ConfigMgr.LastError);
                return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
