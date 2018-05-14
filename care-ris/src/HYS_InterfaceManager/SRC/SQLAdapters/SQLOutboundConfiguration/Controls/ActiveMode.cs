using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HYS.Adapter.Base;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLOutboundAdapterConfiguration.Forms;

namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    public partial class ActiveMode : UserControl
    {
        #region Local members
        private string connectionStr;
        private int channelIndex;
        private XCollection<SQLOutboundChanel> channelSet;
        public ConnectionConfig DBconfig;

        //private const string pattern = @"Provider(\s)*?=(\s)*?SQLOLEDB;Server(.)*?;Database(.)*?;UID(.)*?;Password(.)*?;";
        private const string pattern = @"Provider(.)*?;Data Source(.)*?;Database(.)*?;User ID(.)*?;Password(.)*?;";
        private string otherParameters;
        #endregion

        #region Constructor
        public ActiveMode()
        {
            InitializeComponent();
            DBconfig = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig;
            channelSet = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels;
        }
        #endregion

        #region Show and Save
        public void ShowInformation()
        {
            #region 3rd party connection
            this.textBoxProvider.Text = DBconfig.ConnectionParameter.Provider;
            this.txtServer.Text = DBconfig.ConnectionParameter.Server;
            this.txtDB.Text = DBconfig.ConnectionParameter.Database;
            this.txtUser.Text = DBconfig.ConnectionParameter.User;
            this.txtPassword.Text = DBconfig.ConnectionParameter.Password;
            this.connectionStr = DBconfig.ConnectionParameter.ConnectionStr;
            string head = Regex.Match(connectionStr, pattern).Value;
            this.otherParameters = connectionStr.Substring(head.Length);
            if (DBconfig.TimerInterval < this.numericUpDown.Minimum)
                this.numericUpDown.Value = this.numericUpDown.Minimum;
            else if(DBconfig.TimerInterval > this.numericUpDown.Maximum)
                this.numericUpDown.Value = this.numericUpDown.Maximum;
            else
                this.numericUpDown.Value = DBconfig.TimerInterval;
            this.chkOracleDriver.Checked = DBconfig.OracleDriver;
            #endregion

            #region ChannelInfo
            DBconfig.TimerInterval = (int)numericUpDown.Value;

            if (channelSet != null)
            {
                if (channelSet.Count != 0)
                {
                    ShowChannelSetInformation();
                }
                channelIndex = -1;     //It sign that there is no channel in the channelset
            }
            else
            {
                //Never occur as SQLOutAdapterConfigMgt.SQLOutAdapterConfig was be constuctured
                channelSet = new XCollection<SQLOutboundChanel>();
            }
            #endregion

            LoadFileConnection();
        }

        public void ShowChannelSetInformation()
        {
            int i = 0;
            lstvChannel.Items.Clear();
            foreach (SQLOutboundChanel channel in channelSet)
            {
                i++;
                //Add ID
                ListViewItem viewItem = new ListViewItem(i.ToString());
                //Add Channel name
                viewItem.SubItems.Add(channel.ChannelName);
                //Add Interact mode
                viewItem.SubItems.Add(channel.OperationType.ToString());
                //Add channel status
                if (channel.Enable)
                {
                    viewItem.SubItems.Add("Enable");
                }
                else
                {
                    viewItem.SubItems.Add("Disable");
                }
                viewItem.Tag = i - 1;
                lstvChannel.Items.Add(viewItem);
            }
        }

        public void Save()
        {
            #region Connection Save
            DBconfig.ConnectionParameter.Provider = this.textBoxProvider.Text;
            DBconfig.ConnectionParameter.Server = txtServer.Text;
            DBconfig.ConnectionParameter.Database = txtDB.Text;
            DBconfig.ConnectionParameter.User = txtUser.Text;
            DBconfig.ConnectionParameter.Password = txtPassword.Text;
            //DBconfig.ConnectionParameter.ConnectionStr = "Provider=SQLOLEDB;Server=" + txtServer.Text + ";Database=" + txtDB.Text + ";UID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";" + otherParameters;
            if (chkOracleDriver.Checked)
            {
                DBconfig.ConnectionParameter.ConnectionStr = "Data Source=" + txtServer.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";";
            }
            else
            {
                DBconfig.ConnectionParameter.ConnectionStr = "Provider=" + textBoxProvider.Text + ";Data Source=" + txtServer.Text + ";Database=" + txtDB.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";" + otherParameters;
            }
            DBconfig.TimerInterval = (int)numericUpDown.Value;
            DBconfig.OracleDriver = this.chkOracleDriver.Checked;
            #endregion

            SaveFileConnection();
        }
        #endregion

        #region Connection events
        private void llblDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowConnection frm = new ShowConnection(this);
            //connectionStr = "Provider=SQLOLEDB;Server=" + txtServer.Text + ";Database=" + txtDB.Text + ";UID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";" + otherParameters;
            if (chkOracleDriver.Checked)
            {
                connectionStr="Data Source=" + txtServer.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";";
            }
            else
            {
                connectionStr = "Provider=" + textBoxProvider.Text + ";Data Source=" + txtServer.Text + ";Database=" + txtDB.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";" + otherParameters;
            }
            frm.TextContent(connectionStr);
            frm.ShowDialog(this);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (radioButtonFile.Checked)
            {
                TestFileConnection();
                return;
            }

            if (chkOracleDriver.Checked)
            {
                connectionStr = "Data Source=" + txtServer.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";";
                
                DataBase db = new DataBase(connectionStr);

                this.Cursor = Cursors.WaitCursor;
                if (db.TestDBConnection2())
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Connect to the data source successfully!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Connect to the data source failed!  " + ((db.LastError != null) ? db.LastError.Message : ""),
                        "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
            //connectionStr = "Provider=SQLOLEDB;Server=" + txtServer.Text + ";Database=" + txtDB.Text + ";UID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";" + otherParameters;
            connectionStr = "Provider=" + textBoxProvider.Text + ";Data Source=" + txtServer.Text + ";Database=" + txtDB.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";" + otherParameters;

            DataBase db = new DataBase(connectionStr);

            this.Cursor = Cursors.WaitCursor;
            if (db.TestDBConnection())
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show( "Connect to the data source successfully!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Connect to the data source failed!  " + ((db.LastError != null) ? db.LastError.Message : ""),
                    "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        }
        //For Statement Form to invoke when connection string is changed
        public void ConnectionConfig(string conStr)
        {
            connectionStr = conStr;
            string temp = Regex.Match(connectionStr, pattern).Value;
            otherParameters = connectionStr.Substring(temp.Length);
            ResetConnPara(temp);
        }

        private void ResetConnPara(string conStr)
        {
            string provider = Regex.Match(conStr, "Provider(.)*?;").Value;
            this.textBoxProvider.Text = provider.Substring(provider.IndexOf("=") + 1).TrimEnd(';');
            string server = Regex.Match(conStr, "Data Source(.)*?;").Value;
            this.txtServer.Text = server.Substring(server.IndexOf("=") + 1).TrimEnd(';');
            string dataBase = Regex.Match(conStr, "Database(.)*?;").Value;
            this.txtDB.Text = dataBase.Substring(dataBase.IndexOf("=") + 1).TrimEnd(';');
            string user = Regex.Match(conStr, "User ID(.)*?;").Value;
            this.txtUser.Text = user.Substring(user.IndexOf("=") + 1).TrimEnd(';');
            string password = Regex.Match(conStr, "Password(.)*?;").Value;
            this.txtPassword.Text = password.Substring(password.IndexOf("=") + 1).TrimEnd(';');
        }
        #endregion

        #region Channel events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;
            SaveFileConnection();
            Channel frm = new Channel(this, channelSet);
            frm.ShowDialog(this);

            if (channelSet.Count > count)
            {  // Add successfully
                int i = 0;
                foreach (ListViewItem viewItem in lstvChannel.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvChannel.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnChannelCopy_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            SQLOutboundChanel copyChannel = channelSet[channelIndex].Clone();

            copyChannel.ChannelName += "_copy";

            Channel frm = new Channel(this, channelSet, copyChannel);
            frm.ShowDialog(this);

            if (channelSet.Count > count)
            {  // Add successfully
                channelIndex = count;
                lstvChannel.Items[channelIndex].Selected = true;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvChannel.SelectedIndices[0];
            int itemIndex = (int)lstvChannel.SelectedItems[0].Tag;

            SaveFileConnection();
            Channel frm = new Channel(this, channelSet, itemIndex);
            frm.ShowDialog(this);

            lstvChannel.Items[selectIndex].Selected = true;
        }
        private void lstvChannel_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvChannel.SelectedIndices[0];
            int itemIndex = (int)lstvChannel.SelectedItems[0].Tag;
            Channel frm = new Channel(this, channelSet, itemIndex);
            frm.ShowDialog(this);

            lstvChannel.Items[selectIndex].Selected = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this channel?", "Delete Channel Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvChannel.SelectedIndices[0];
                int itemIndex = (int)lstvChannel.SelectedItems[0].Tag;
                channelSet.Remove(channelSet[itemIndex]);
                ShowChannelSetInformation();
                if (channelSet.Count < 1)
                {
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                    btnChannelCopy.Enabled = false;
                    
                }
                else
                {
                    if (selectIndex >= channelSet.Count)
                    {
                        selectIndex--;
                    }
                    lstvChannel.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Controls events
        private void lstvChannel_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvChannel.Columns[e.Column].Tag == null)
                this.lstvChannel.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvChannel.Columns[e.Column].Tag;
            if (tabK)
                this.lstvChannel.Columns[e.Column].Tag = false;
            else
                this.lstvChannel.Columns[e.Column].Tag = true;
            this.lstvChannel.ListViewItemSorter = new ListViewSort(e.Column, this.lstvChannel.Columns[e.Column].Tag);
        }

        private void lstvChannel_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                channelIndex = e.ItemIndex;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                btnChannelCopy.Enabled = true;
            }
            else
            {
                channelIndex = -1;
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
                btnChannelCopy.Enabled = false;
            }
        }
        #endregion

        private void LoadFileConnection()
        {
            this.panelFile.Location = this.panelDB.Location;
            if (DBconfig.ConnectionParameter.FileConnection)
            {
                this.radioButtonDB.Checked = false;
                this.radioButtonFile.Checked = true;
            }
            else
            {
                this.radioButtonDB.Checked = true;
                this.radioButtonFile.Checked = false;
            }
            this.textBoxFileConnString.Text = DBconfig.ConnectionParameter.FileConnectionString;
            this.textBoxDataFolder.Text = DBconfig.ConnectionParameter.FileFolder;
            this.textBoxFileExtension.Text = DBconfig.ConnectionParameter.FileNameExtension;
            this.textBoxIndexFolder.Text = DBconfig.ConnectionParameter.IndexFileFolder;
            this.checkBoxIndexFile.Checked = DBconfig.ConnectionParameter.WriteIndexFile;
            radioButtonFile_CheckedChanged(null, null);
            checkBoxIndexFile_CheckedChanged(null, null);
        }
        private void SaveFileConnection()
        {
            DBconfig.ConnectionParameter.FileConnection = radioButtonFile.Checked;
            DBconfig.ConnectionParameter.FileConnectionString = this.textBoxFileConnString.Text.Trim();
            DBconfig.ConnectionParameter.FileFolder = this.textBoxDataFolder.Text.Trim();
            DBconfig.ConnectionParameter.FileNameExtension = this.textBoxFileExtension.Text.Trim();
            DBconfig.ConnectionParameter.IndexFileFolder = this.textBoxIndexFolder.Text.Trim();
            DBconfig.ConnectionParameter.WriteIndexFile = this.checkBoxIndexFile.Checked;

            if (radioButtonFile.Checked)
            {
                foreach (SQLOutboundChanel channel in channelSet)
                {
                    if (channel.OperationType != ThrPartyDBOperationType.Table) channel.Enable = false;
                }
            }
        }
        private void TestFileConnection()
        {
            DataBase db = new DataBase(this.textBoxFileConnString.Text.Trim());
            this.Cursor = Cursors.WaitCursor;
            if (db.TestDBConnection())
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Connect to the data source successfully!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Connect to the data source  failed! " + ((db.LastError != null) ? db.LastError.Message : ""),
                    "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void radioButtonFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFile.Checked)
            {
                this.panelFile.Visible = true;
                this.panelFile.BringToFront();
            }
            else
            {
                this.panelDB.Visible = true;
                this.panelDB.BringToFront();
            }
        }
        private void checkBoxIndexFile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIndexFile.Checked)
            {
                this.textBoxIndexFolder.ReadOnly = false;
            }
            else
            {
                this.textBoxIndexFolder.ReadOnly = true;
            }
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            bool bDBConn = radioButtonDB.Checked;
            this.toolStripMenuItemCSV.Visible = !bDBConn;
            this.toolStripMenuItemCSVX64.Visible = !bDBConn;
            this.toolStripMenuItemOracle.Visible =
                this.toolStripMenuItemSQLServer.Visible =
                this.toolStripMenuItemSybase.Visible = bDBConn;
            this.contextMenuStripConnStr.Show(this.buttonDefault,
                new Point(0, this.buttonDefault.Height));
        }
        private void toolStripMenuItemCSV_Click(object sender, EventArgs e)
        {
            this.textBoxFileExtension.Text = ".csv";
            this.textBoxDataFolder.Text = "C:\\CSVFile\\Data";
            this.textBoxIndexFolder.Text = "C:\\CSVFile\\Index";
            this.checkBoxIndexFile.Checked = false;
            this.textBoxFileConnString.Text = string.Format(
                "Provider=Microsoft.Jet.OLEDB.4.0;\r\nData Source={0};\r\nExtended Properties=\"text;HDR=No;FMT=Delimited;CharacterSet=65001\";",
                this.textBoxDataFolder.Text);
            chkOracleDriver.Checked = false;
        }
        private void toolStripMenuItemSQLServer_Click(object sender, EventArgs e)
        {
            this.textBoxProvider.Text = "SQLNCLI";
            this.txtServer.Text = "<IP address or host name>";
            this.txtDB.Text = "<database name>";
            this.txtUser.Text = "<user name>";
            this.txtPassword.Text = "<password>";
            chkOracleDriver.Checked = false;
        }
        private void toolStripMenuItemOracle_Click(object sender, EventArgs e)
        {
            this.textBoxProvider.Text = "MSDAORA.1";
            this.txtServer.Text = "<SID>";
            this.txtDB.Text = "";
            this.txtUser.Text = "<user name>";
            this.txtPassword.Text = "<password>";
            chkOracleDriver.Checked = false;
        }
        private void toolStripMenuItemSybase_Click(object sender, EventArgs e)
        {
            this.textBoxProvider.Text = "Sybase ASE OLE DB Provider";
            this.txtServer.Text = "<server name>";
            this.txtDB.Text = "<database name>";
            this.txtUser.Text = "<user name>";
            this.txtPassword.Text = "<password>";
            chkOracleDriver.Checked = false;
        }

        private void linkLabelCharSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormCharSet frm = new FormCharSet();
            frm.ShowDialog(this);
        }

        private void toolStripMenuItemCSVX64_Click(object sender, EventArgs e)
        {
            this.textBoxFileExtension.Text = ".csv";
            this.textBoxDataFolder.Text = "C:\\CSVFile\\Data";
            this.textBoxIndexFolder.Text = "C:\\CSVFile\\Index";
            this.checkBoxIndexFile.Checked = false;
            this.textBoxFileConnString.Text = string.Format(
                "Provider=Microsoft.ACE.OLEDB.12.0;\r\nData Source={0};\r\nExtended Properties=\"text;HDR=No;FMT=Delimited;CharacterSet=65001\";",
                this.textBoxDataFolder.Text);
            chkOracleDriver.Checked = false;
        }
        private void chkOracleDriver_CheckedChanged(object sender, EventArgs e)
        {
            //ChangeCtlStatus();
            if (chkOracleDriver.Checked)
            {
                llblDetail.Visible = false;  //if X64 Oracle Driver, Not allow to view more details
                textBoxProvider.ReadOnly = true;
                txtDB.ReadOnly = true;
            }
            else
            {
                llblDetail.Visible = true;
                textBoxProvider.ReadOnly = false;
                txtDB.ReadOnly = false;
            }
        }

        private void ChangeCtlStatus()
        {
            if (this.chkOracleDriver.Checked)
            {
                txtServer.Text = "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.184.193.181)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";
                textBoxProvider.Text = "";
                txtDB.Text = "";
                textBoxProvider.ReadOnly = true;
                txtDB.ReadOnly = true;
            }
            else
            {
                txtServer.Text = "";
                textBoxProvider.ReadOnly = false;
                txtDB.ReadOnly = false;
            }
        }

        private void oracleX64ConnectionStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtServer.Text = "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.184.193.181)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";
            textBoxProvider.Text = "";
            txtDB.Text = "";
            txtUser.Text = "<user name>";
            txtPassword.Text = "<password>";
            textBoxProvider.ReadOnly = true;
            txtDB.ReadOnly = true;
            chkOracleDriver.Checked = true;
        }

    }
}
