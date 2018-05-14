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
        private void LoadGeneralSetting()
        {
            this.textBoxSolutionID.Text = Program.ConfigMgt.Config.SolutionID.ToString();
            this.textBoxSolutionName.Text = Program.ConfigMgt.Config.Name;
            this.textBoxSolutionVersion.Text = Program.ConfigMgt.Config.SolutionVersion;
            this.textBoxSolutionDescription.Text = Program.ConfigMgt.Config.Description;

            DateTime dt = DateTime.Now;
            this.textBoxLastUpdated.Text = dt.ToShortDateString() + " " + dt.ToLongTimeString();
        }
        private bool SaveGeneralSetting()
        {
            string sName = this.textBoxSolutionName.Text.Trim();

            if (sName.Length < 1)
            {
                this.tabControlMain.SelectedTab = this.tabPageGeneral;
                this.textBoxSolutionName.Focus();

                MessageBox.Show(this, "Please enter solution name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Program.ConfigMgt.Config.Name = sName;
            Program.ConfigMgt.Config.Description = this.textBoxSolutionDescription.Text;
            Program.ConfigMgt.Config.SolutionVersion = this.textBoxSolutionVersion.Text;
            Program.ConfigMgt.Config.SolutionUpdateDateTime = this.textBoxLastUpdated.Text;
            Program.ConfigMgt.Config.SolutionID = new Guid(this.textBoxSolutionID.Text.Trim());
            
            return true;
        }

        private void GenerateSolutionID()
        {
            // prompt some warning

            this.textBoxSolutionID.Text = Guid.NewGuid().ToString();
        }
    }
}
