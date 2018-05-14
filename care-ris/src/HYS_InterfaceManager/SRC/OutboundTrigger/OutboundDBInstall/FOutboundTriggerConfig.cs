using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;

namespace OutboundDBInstall
{
    public partial class FOutboundTriggerConfig : Form, IConfigUI
    {
        public FOutboundTriggerConfig()
        {
            InitializeComponent();

            Program.PreLoad();

            this.dataGridView1.Parent = pSetting;
            this.tcScript.Parent = pSetting;
            //this.pConfigurationButtons.Parent = pSetting;
            //this.pSQLButtons.Parent = pSetting;

            this.tbOutboundName.Text = OutboundDBConfigMgt.DeviceDir.DeviceDirInfor.Header.Name;
            this.tbOutboundDesc.Text = OutboundDBConfigMgt.DeviceDir.DeviceDirInfor.Header.Description;

            IOChannelVeiwMgt.Init(this.dataGridView1, OutboundDBConfigMgt.Config);
            IOChannelVeiwMgt.UpdateAllView();
            this.HajickTreat();

            if (this.dataGridView1.RowCount > 0)
                this.dataGridView1.Rows[0].Selected = true;
            this.RefreshForm();
            
        }

        FIFInboundAddEdit frmAddEdit = new FIFInboundAddEdit();
       
        private void FOutboundTriggerConfig_Load(object sender, EventArgs e)
        {
            //this.tbOutboundName.Text = OutboundDBConfigMgt.DeviceDir.DeviceDirInfor.Header.Name;
            //this.tbOutboundDesc.Text = OutboundDBConfigMgt.DeviceDir.DeviceDirInfor.Header.Description;

            //IOChannelVeiwMgt.Init(this.dataGridView1, OutboundDBConfigMgt.Config.OutboundConfig);
            //IOChannelVeiwMgt.UpdateAllView();
            //this.HajickTreat();

           
           
        }

        private void HajickTreat()
        {
            if (OutboundDBConfigMgt.Config.ScriptIsHijacked)
            {
                this.bFlag = true; // Do not buildscriptt
                this.rbSQLView.Checked = true;
                tbInstallTrigger.Text = OutboundDBConfigMgt.Config.InstallTriggerScript;
                tbUninstallTrigger.Text = OutboundDBConfigMgt.Config.UninstallTriggerScript;
                this.bScriptIsHajicked = true;
            }
        }
        

       

        private void RefreshForm()
        {
            if (rbConfigurationView.Checked)
            {
                this.pConfigurationButtons.Visible = true;
                this.pConfigurationButtons.Dock = DockStyle.Bottom;
                this.dataGridView1.Visible = true;
                this.dataGridView1.Dock = DockStyle.Fill;                

                this.pSQLButtons.Visible = false;
                this.tcScript.Visible = false;
            }
            if (rbSQLView.Checked)
            {
                this.pSQLButtons.Visible = true;
                this.pSQLButtons.Dock = DockStyle.Bottom;
                this.tcScript.Visible = true;
                this.tcScript.Dock = DockStyle.Fill;                

                this.dataGridView1.Visible = false;
                this.pConfigurationButtons.Visible = false;
            }

            //btModify.Enabled = (dataGridView1.CurrentRow != null);
            btRemove.Enabled = btModify.Enabled = (dataGridView1.SelectedCells.Count > 0);
            // btModify.Enabled;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            this.frmAddEdit.ShowDialog(pMain, FIFInboundAddEdit.FormType.ftAdd, "");
            this.RefreshForm();
        }


        private void btModify_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = this.dataGridView1.CurrentRow;
            if (row == null) return;
            this.frmAddEdit.ShowDialog(pMain, FIFInboundAddEdit.FormType.ftModify, row.Cells[0].Value.ToString());
        }

        //private string GetCurrInboundInterfaceName()
        //{
        //    string sResult = "";
        //    if (dataGridView1.Rows.Count < 1)
        //        MessageBox.Show("There is no valid inbound interface");
        //    else if (dataGridView1.SelectedRows.Count > 0)
        //        sResult = dataGridView1.SelectedRows[0].Cells[0].ToString();
        //    else if (dataGridView1.CurrentCell != null)
        //        sResult = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].ToString();

        //    return sResult;
        //}

        private void btRemove_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            if (row == null) return;
            if (MessageBox.Show(pMain, "Are you sure to remove the selected interface?",
                "Waring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                IOChannelVeiwMgt.DeleteChannel(row.Cells[0].Value.ToString());
            this.RefreshForm();
        }
       

        

        private void btApply_Click(object sender, EventArgs e)
        {
            OutboundDBConfigMgt.Config.InstallTriggerScript = 
                this.tbInstallTrigger.Text;

            OutboundDBConfigMgt.Config.UninstallTriggerScript = 
                this.tpUninstallTriggerScript.Text;

            OutboundDBConfigMgt.Config.ScriptIsHijacked = bScriptIsHajicked;
        }

        private void btCancelModify_Click(object sender, EventArgs e)
        {
            OutboundDBConfigMgt.Config.BuildScriptBySetting();
            this.tbInstallTrigger.Text = OutboundDBConfigMgt.Config.InstallTriggerScript;
            this.tbUninstallTrigger.Text = OutboundDBConfigMgt.Config.UninstallTriggerScript;

            this.ScriptIsHajacked(false);
            OutboundDBConfigMgt.Config.ScriptIsHijacked = bScriptIsHajicked;

        }


        bool bFlag = false;  // if program set bFlag = true, not to set hajacked=false, else set hajacked = false;

        private void rbConfigurationView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                // From Configuration View to SQL View , no waring
                if (rbSQLView.Checked)
                {

                    if (bFlag)
                    {
                        bFlag = false;
                    }
                    else
                    {
                        OutboundDBConfigMgt.Config.BuildScriptBySetting();

                        this.tbInstallTrigger.Text =
                            OutboundDBConfigMgt.Config.InstallTriggerScript;

                        this.tbUninstallTrigger.Text =
                            OutboundDBConfigMgt.Config.UninstallTriggerScript;
                        ScriptIsHajacked(false);
                    }
                }

                // From SQL VIEW to Configuration View, and User changed SQL Script , pop up waring: changed may be override;
                if (rbConfigurationView.Checked && (bScriptIsHajicked || OutboundDBConfigMgt.Config.ScriptIsHijacked))
                {
                    if (MessageBox.Show(pMain,
                       "The Script which you have been modified will be overrided, Do you want to return Configuration View anyway?",
                       "Waring", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        OutboundDBConfigMgt.Config.BuildScriptBySetting();

                        this.tbInstallTrigger.Text =
                            OutboundDBConfigMgt.Config.InstallTriggerScript;

                        this.tbUninstallTrigger.Text =
                            OutboundDBConfigMgt.Config.UninstallTriggerScript;

                        ScriptIsHajacked(false);
                    }
                    else
                    {
                        bFlag = true;
                        rbSQLView.Checked = true;

                    }
                }

                RefreshForm();
            }
            catch (Exception ex)
            {
                Program.log.Write(ex);
            }
        }



        bool bScriptIsHajicked = false;

        public bool Save()
        {
            try
            {
                if (rbConfigurationView.Checked) //No Hijack script
                {
                    OutboundDBConfigMgt.Config.BuildScriptBySetting();
                }
                else if (bScriptIsHajicked)
                {
                    OutboundDBConfigMgt.Config.InstallTriggerScript =
                        tbInstallTrigger.Text;

                    OutboundDBConfigMgt.Config.UninstallTriggerScript =
                        tbUninstallTrigger.Text;

                    OutboundDBConfigMgt.Config.ScriptIsHijacked = true;
                }

                OutboundDBConfigMgt.Save();

                if (OutboundDBConfigMgt.Config.ScriptIsHijacked == false &&
                    OutboundDBConfigMgt.Config.IOChannels.Count < 1)
                {
                    Form frm = this.ParentForm;
                    if (MessageBox.Show(frm,
                        "You did not select any data source for this outbound interface, are you sure to save the configuration?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Program.log.Write(ex);
                return false;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void tbInstallTrigger_TextChanged(object sender, EventArgs e)
        {
            ScriptIsHajacked(true);
        }

        private void tbUninstallTrigger_TextChanged(object sender, EventArgs e)
        {
            ScriptIsHajacked(true);
        }

        private void ScriptIsHajacked(bool bHajacked)
        {
            bScriptIsHajicked = bHajacked;
            labHijack.Visible = bHajacked;

        }




      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.RefreshForm();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.RefreshForm();
        }

        private void FOutboundTriggerConfig_ClientSizeChanged(object sender, EventArgs e)
        {
            AdjustDataGridWidth();
        }

        private void AdjustDataGridWidth()
        {
            int width = dataGridView1.ClientSize.Width - dataGridView1.RowHeadersWidth;
            dataGridView1.Columns[0].Width = width * 10 / 100;
            dataGridView1.Columns[1].Width = width * 60 / 100;
            dataGridView1.Columns[2].Width = width * 30 / 100;
            
            
        }



        #region IConfigUI Members

        Control IConfigUI.GetControl()
        {
            //Program.PreLoad();
            return this.pMain;
        }

        bool IConfigUI.LoadConfig()
        {            
            return true;
        }

        string IConfigUI.Name
        {
            get { return this.Text; }
        }

        bool IConfigUI.SaveConfig()
        {
            return Save();
        }

        #endregion

        

        /*
        #region IConfigUI Members

        
        Control GetControl()
        {            
            return this.pMain;
        }

        bool LoadConfig()
        {
            Program.PreLoad();
            return true;
        }

    

        bool SaveConfig()
        {
            return Save();
            
        }

        #endregion

        
       
        */
    }
}