using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.IPMonitor.Policies;


namespace HYS.IM.IPMonitor
{
    public partial class FormConfig : Form
    {
        private Dictionary<string, List<string>> _validations = new Dictionary<string, List<string>>();
        private XmlDocument _validationConfig;
        private Dictionary<string,string> _AvailableValidations;
        private DataTable _selectedServices;

        public FormConfig()
        {
            InitializeComponent();
            this.CenterToParent();
            this.cbFlagEnable.Enabled = false;
            this.cbIPEnable.Enabled = false; 
            if (Program.ConfigMgt.Config.PolicyFlag.Enable)
            {
                this.cbFlagEnable.Checked = true;
                this.cbIPEnable.Checked = false; 
                this.tabControlMain.SelectedTab = this.tabFlag;
                this.tabControlMain.TabPages[0].Parent = null;                
            }
            else if (Program.ConfigMgt.Config.PolicyIP.Enable)
            {
                this.cbIPEnable.Checked = true;
                this.cbFlagEnable.Checked = false;
                this.tabControlMain.SelectedTab = this.tabIP;
                this.tabControlMain.TabPages[1].Parent = null;
            }
            else
            {
                this.cbFlagEnable.Enabled = true;
                this.cbIPEnable.Enabled = true; 
            }
            LoadSetting();
        }

        private void LoadSetting()
        {
            try
            {


                //this.cbIPEnable.Checked = Program.ConfigMgt.Config.PolicyIP.Enable;  do it at FormConfig() method.
                this.textBoxInterval.Text = Program.ConfigMgt.Config.PolicyIP.IntervalInMS.ToString();
                this.textBoxTimeout.Text = Program.ConfigMgt.Config.PolicyIP.TimeOutInS4Ping.ToString();
                this.textBoxIP.Text = Program.ConfigMgt.Config.PolicyIP.IPPublic;
                this.textBoxIPPrivate.Text = Program.ConfigMgt.Config.PolicyIP.IPPrivate;
                this.textBoxSubnet.Text = Program.ConfigMgt.Config.PolicyIP.SubnetPublic;
                this.textBoxSubnetPrivate.Text = Program.ConfigMgt.Config.PolicyIP.SubnetPrivate;
                this.cboAdapterName.SelectedIndexChanged -= new EventHandler(cboAdapterName_SelectedIndexChanged);
                this.cboAdapterName.DataSource = Helper.GetAdapterNames();
                this.cboAdapterName.SelectedIndex = -1;
                this.cboAdapterName.Text = Program.ConfigMgt.Config.PolicyIP.AdapterName;
                this.textBoxServiceName.Text = Program.ConfigMgt.Config.PolicyIP.FlagService;
                this.cboAdapterName.SelectedIndexChanged += new EventHandler(cboAdapterName_SelectedIndexChanged);
                if (Program.ConfigMgt.Config.PolicyIP.GatewayPublic != null && Program.ConfigMgt.Config.PolicyIP.GatewayPublic != "")
                {
                    this.textBoxGateway.Text = Program.ConfigMgt.Config.PolicyIP.GatewayPublic;
                    this.textBoxGatewayPrivate.Text = Program.ConfigMgt.Config.PolicyIP.GatewayPrivate;
                    cbGateway.Checked = true;
                }
                if (Program.ConfigMgt.Config.PolicyIP.DNSPublic != null && Program.ConfigMgt.Config.PolicyIP.DNSPublic != "")
                {
                    this.textBoxDNS.Text = Program.ConfigMgt.Config.PolicyIP.DNSPublic;
                    this.textBoxDNSPrivate.Text = Program.ConfigMgt.Config.PolicyIP.DNSPrivate;
                    this.cbDNS.Checked = true;
                }


                //this.cbFlagEnable.Checked = Program.ConfigMgt.Config.PolicyFlag.Enable; do it at FormConfig() method.
                this.tbFlagInterval.Text = Program.ConfigMgt.Config.PolicyFlag.IntervalInMS.ToString();
                this.cboFlagAdapterName.DataSource = Helper.GetAdapterNames();
                this.cboFlagAdapterName.SelectedIndex = -1;
                this.cboFlagAdapterName.Text = Program.ConfigMgt.Config.PolicyFlag.AdapterName;
                Helper.StringToValidations(_validations, Program.ConfigMgt.Config.PolicyFlag.ServicesWithValidations);


                _selectedServices = new DataTable();
                _selectedServices.Columns.Add("Display");
                _selectedServices.Columns.Add("Value");
                _selectedServices.DefaultView.Sort = "Display";


                DataTable dt = Helper.GetServiceList();
                foreach (string key in _validations.Keys)
                {
                    DataRow[] rows = dt.Select("Value = '" + key + "'");
                    if (rows == null || rows.Length == 0)
                    {
                        _selectedServices.LoadDataRow(new object[] { "  (# " + key + " #)", key }, true);
                    }
                    else
                    {
                        _selectedServices.LoadDataRow(rows[0].ItemArray, true);
                    }
                }

                listServices.DisplayMember = "Display";
                listServices.ValueMember = "Value";
                listServices.DataSource = _selectedServices;

            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private bool SaveSetting()
        {
            if (!Validation()) return false;
            if (cbIPEnable.Checked)
            {
                Program.ConfigMgt.Config.PolicyIP.Enable = true;
                Program.ConfigMgt.Config.PolicyFlag.Enable = false;
                Program.ConfigMgt.Config.PolicyIP.IPPublic = this.textBoxIP.Text;
                Program.ConfigMgt.Config.PolicyIP.IPPrivate = this.textBoxIPPrivate.Text;
                Program.ConfigMgt.Config.PolicyIP.SubnetPublic = this.textBoxSubnet.Text;
                Program.ConfigMgt.Config.PolicyIP.SubnetPrivate = this.textBoxSubnetPrivate.Text;
                Program.ConfigMgt.Config.PolicyIP.AdapterName = this.cboAdapterName.Text;
                Program.ConfigMgt.Config.PolicyIP.FlagService = this.textBoxServiceName.Text;
                Program.ConfigMgt.Config.PolicyIP.IntervalInMS = Convert.ToInt64(this.textBoxInterval.Text.Trim());
                Program.ConfigMgt.Config.PolicyIP.TimeOutInS4Ping = Convert.ToInt32(this.textBoxTimeout.Text.Trim());
                if (cbGateway.Checked)
                {
                    Program.ConfigMgt.Config.PolicyIP.GatewayPublic = this.textBoxGateway.Text;
                    Program.ConfigMgt.Config.PolicyIP.GatewayPrivate = this.textBoxGatewayPrivate.Text;
                }
                else
                {
                    Program.ConfigMgt.Config.PolicyIP.GatewayPublic = "";
                    Program.ConfigMgt.Config.PolicyIP.GatewayPrivate = "";
                }
                if (this.cbDNS.Checked)
                {
                    Program.ConfigMgt.Config.PolicyIP.DNSPublic = this.textBoxDNS.Text;
                    Program.ConfigMgt.Config.PolicyIP.DNSPrivate = this.textBoxDNSPrivate.Text;
                }
                else
                {
                    Program.ConfigMgt.Config.PolicyIP.DNSPublic = "";
                    Program.ConfigMgt.Config.PolicyIP.DNSPrivate = "";
                }
            }
            else
            {
                Program.ConfigMgt.Config.PolicyFlag.Enable = true;
                Program.ConfigMgt.Config.PolicyIP.Enable = false;
                Program.ConfigMgt.Config.PolicyFlag.AdapterName = this.cboFlagAdapterName.Text;
                Program.ConfigMgt.Config.PolicyFlag.IntervalInMS = Convert.ToInt64(this.tbFlagInterval.Text.Trim());
                Program.ConfigMgt.Config.PolicyFlag.ServicesWithValidations = Helper.ValidationsToString(_validations);
            }

            if (Program.ConfigMgt.Save()) return true;

            Program.Log.Write(Program.ConfigMgt.LastError);
            MessageBox.Show(this, "Save configuration file failed.",
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (!SaveSetting()) return;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool Validation()
        {
            bool flag = true;
            Regex reg = new Regex(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$");
            Regex reg1 = new Regex(@"^[1-9][0-9]{3,18}$");
            Regex reg2 = new Regex(@"^[1-9][0-9]{0,1}$");

            if (cbIPEnable.Checked && cbFlagEnable.Checked)
            {
                errorProvider1.SetError(cbIPEnable, "Only one Policy can be enabled!");
                errorProvider1.SetError(cbFlagEnable, "Only one Policy can be enabled!");
                flag = false;
            }
            if (!cbIPEnable.Checked && !cbFlagEnable.Checked)
            {
                errorProvider1.SetError(cbIPEnable, "Must have one Policy enabled at least!");
                errorProvider1.SetError(cbFlagEnable, "Must have one Policy enabled at least!");
                flag = false;
            }
            if (!flag) return false;

            if (cbIPEnable.Checked)
            {
                if (cboAdapterName.Text.Trim() == "")
                {
                    errorProvider1.SetError(cboAdapterName, "Should not be empty!");
                    flag = false;
                }
                if (!reg1.IsMatch(textBoxInterval.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxInterval, "Interval should be from 1000 to " + Int64.MaxValue.ToString());
                    flag = false;
                }
                else if (Convert.ToDouble(textBoxInterval.Text.Trim()) > Int64.MaxValue)
                {
                    errorProvider1.SetError(textBoxInterval, "Interval should be from 1000 to " + Int64.MaxValue.ToString());
                    flag = false;
                }
                if (!reg2.IsMatch(textBoxTimeout.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxTimeout, "Time out should be from 1 to 99");
                    flag = false;
                }
                if (textBoxServiceName.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBoxServiceName, "Should not be empty!");
                    flag = false;
                }
                if (!reg.IsMatch(textBoxIP.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxIP, "Format is not correct!");
                    flag = false;
                }
                if (!reg.IsMatch(textBoxIPPrivate.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxIPPrivate, "Format is not correct!");
                    flag = false;
                }
                if (!reg.IsMatch(textBoxSubnet.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxSubnet, "Format is not correct!");
                    flag = false;
                }
                if (!reg.IsMatch(textBoxSubnetPrivate.Text.Trim()))
                {
                    errorProvider1.SetError(textBoxSubnetPrivate, "Format is not correct!");
                    flag = false;
                }

                if (cbGateway.Checked)
                {
                    if (!reg.IsMatch(textBoxGateway.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxGateway, "Format is not correct!");
                        flag = false;
                    }
                    if (!reg.IsMatch(textBoxGatewayPrivate.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxGatewayPrivate, "Format is not correct!");
                        flag = false;
                    }
                }

                if (cbDNS.Checked)
                {
                    if (!reg.IsMatch(textBoxDNS.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxDNS, "Format is not correct!");
                        flag = false;
                    }
                    if (!reg.IsMatch(textBoxDNSPrivate.Text.Trim()))
                    {
                        errorProvider1.SetError(textBoxDNSPrivate, "Format is not correct!");
                        flag = false;
                    }
                }
            }
            else
            {
                if (!reg1.IsMatch(tbFlagInterval.Text.Trim()))
                {
                    errorProvider1.SetError(tbFlagInterval, "Interval should be from 1000 to " + Int64.MaxValue.ToString());
                    flag = false;
                }
                if (cboFlagAdapterName.Text.Trim() == "")
                {
                    errorProvider1.SetError(cboFlagAdapterName, "Should not be empty!");
                    flag = false;
                }

                bool hasValidations = false;
                foreach (List<string> list in _validations.Values)
                {
                    if (list.Count > 0)
                    {
                        hasValidations = true;
                        break;
                    }
                }
                if (!hasValidations)
                {
                    errorProvider1.SetError(listServices, "Should have one service with validations at least!");
                    flag = false;
                }
            }
            return flag;
        }

        private void cbGateway_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBoxGateway.Enabled = cbGateway.Checked;
                this.textBoxGatewayPrivate.Enabled = cbGateway.Checked;
                if (!cbGateway.Checked)
                {
                    this.textBoxGateway.Text = "";
                    this.textBoxGatewayPrivate.Text = "";
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void cbDNS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBoxDNS.Enabled = cbDNS.Checked;
                this.textBoxDNSPrivate.Enabled = cbDNS.Checked;
                if (!cbDNS.Checked)
                {
                    this.textBoxDNS.Text = "";
                    this.textBoxDNSPrivate.Text = "";
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void cboAdapterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cboAdapterName.Text.Trim() != "")
                {
                    Helper.Adapter list = Helper.GetAdapterInfo(this.cboAdapterName.Text.Trim());
                    this.textBoxIPPrivate.Text = list.IPAdress ?? "";
                    this.textBoxSubnetPrivate.Text = list.Subnet ?? "";
                    if (this.textBoxGatewayPrivate.Enabled)
                    {
                        this.textBoxGatewayPrivate.Text = list.Gateway ?? "";
                    }
                    if (this.textBoxDNSPrivate.Enabled)
                    {
                        this.textBoxDNSPrivate.Text = list.DNS ?? "";
                    }

                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }       

        private void btAddService_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceList form = new ServiceList())
                {
                    form.SelectedServices = _selectedServices;
                    form.ShowDialog();
                    listServices.DataSource = _selectedServices;
                    if (_selectedServices.GetChanges(DataRowState.Added) != null)
                    {
                        foreach (DataRow row in _selectedServices.GetChanges(DataRowState.Added).Rows)
                        {
                            if (!_validations.ContainsKey(row["Value"].ToString()))
                            {
                                _validations.Add(row["Value"].ToString(), new List<string>());
                            }
                        }
                    }
                    if (_selectedServices.Rows.Count > 0)
                    {
                        listServices.SelectedIndex = -1;
                        listServices.SelectedIndex = 0;
                    }
                }
            }            
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void btAddValidation_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_validations.ContainsKey(listServices.SelectedValue.ToString()))
                {
                    _validations.Add(listServices.SelectedValue.ToString(), new List<string>());
                }
                if (!_validations[listServices.SelectedValue.ToString()].Contains(cbValidations.Text))
                {
                    _validations[listServices.SelectedValue.ToString()].Add(cbValidations.Text);
                    this.listValidations.DataSource = null;
                    this.listValidations.DataSource = _validations[listServices.SelectedValue.ToString()];
                    if (_validations[listServices.SelectedValue.ToString()].Count > 0)
                    {
                        this.listValidations.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show(this, "The validation already exited!", this.Text);
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void cbValidations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbValidations.SelectedValue != null)
            {
                lblDescription.Text = this.cbValidations.SelectedValue.ToString();
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = this.listServices.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Value"].ToString() == this.listServices.SelectedValue.ToString())
                    {
                        if (_validations.ContainsKey(row["Value"].ToString()))
                        {
                            _validations.Remove(row["Value"].ToString());
                        }
                        row.Delete();
                        break;
                    }
                }
                dt.AcceptChanges();
                listServices.DataSource = dt;
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void listServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.listServices.SelectedIndex == -1)
                {
                    this.btDelete.Enabled = false;
                    this.btAddValidation.Enabled = false;
                    this.btdeleteValidation.Enabled = false;
                    this.listValidations.DataSource = null;
                    this.cbValidations.DataSource = null;
                    this.lblDescription.Text = "";
                }
                else
                {
                    this.btDelete.Enabled = true;
                    this.btAddValidation.Enabled = true;
                    this.listValidations.DataSource = null;
                    if (_validations.ContainsKey(listServices.SelectedValue.ToString()))
                    {
                        this.listValidations.DataSource = _validations[listServices.SelectedValue.ToString()];
                        if (_validations[listServices.SelectedValue.ToString()].Count > 0)
                        {
                            this.listValidations.SelectedIndex = 0;
                        }
                    }
                    DecodeValidationsConfig(listServices.SelectedValue.ToString());
                    this.cbValidations.DisplayMember = "Key";
                    this.cbValidations.ValueMember = "Value";
                    this.cbValidations.DataSource = _AvailableValidations.ToList();

                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void DecodeValidationsConfig(string serviceName)
        {
            try
            {
                if (_validationConfig == null)
                {
                    _validationConfig = new XmlDocument();
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;
                    XmlReader reader = XmlReader.Create(Path.Combine(Program.ConfigPath, MonitorConfig.ValidationConfigName), settings);
                    _validationConfig.Load(reader);
                    _AvailableValidations = new Dictionary<string, string>();
                }
                _AvailableValidations.Clear();
                XmlNodeList nodes = _validationConfig.SelectNodes(string.Format("//validation[@service=''] | //validation[@service='{0}']", serviceName));
                foreach (XmlNode node in nodes)
                {
                    if (!_AvailableValidations.ContainsKey(node.ChildNodes[0].InnerText))
                    {
                        _AvailableValidations.Add(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText);
                    }
                    else
                    {
                        Program.Log.Write(LogType.Error, "Please Make sure the name is unique in ServiceValidations.xml file");
                    }
                }
                _AvailableValidations.OrderBy(p => p.Key);
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, "Please check if each node in ServiceValidations.xml has proper structure! " + ex.Message);
            }

        }

        private void btdeleteValidation_Click(object sender, EventArgs e)
        {
            try
            {
                if (_validations.ContainsKey(listServices.SelectedValue.ToString()))
                {
                    if (_validations[listServices.SelectedValue.ToString()].Contains(listValidations.Text))
                    {
                        _validations[listServices.SelectedValue.ToString()].Remove(listValidations.Text);
                        this.listValidations.DataSource = null;
                        this.listValidations.DataSource = _validations[listServices.SelectedValue.ToString()];
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void listValidations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listValidations.SelectedIndex == -1)
                {
                    btdeleteValidation.Enabled = false;
                }
                else
                {
                    btdeleteValidation.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceList form = new ServiceList())
                {
                    form.SelectedText = this.textBoxServiceName.Text;
                    form.ShowDialog();
                    if (form.SelectedText.Trim() != "")
                    {
                        this.textBoxServiceName.Text = form.SelectedText.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
            }
        }
    }
}
