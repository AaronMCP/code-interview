using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Mapping;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Objects.RoutingModel;

namespace HYS.IM.Messaging.Config
{
    public partial class FormPullChannel : Form
    {
        public enum ActionType
        {
            Add,
            Edit,
            View,
        }

        private ActionType _actionType;
        private PullChannelConfig _channelConfig;
        private XCollection<PullChannelConfig> _cfgList;
        public PullChannelConfig ChannelConfig
        {
            get { return _channelConfig; }
        }

        public FormPullChannel(PullChannelConfig cfg, ActionType actionType)
            : this(cfg, actionType, null)
        {
        }
        public FormPullChannel(PullChannelConfig cfg, ActionType actionType, XCollection<PullChannelConfig> cfgList)
        {
            InitializeComponent();

            _cfgList = cfgList;
            _channelConfig = cfg;
            _actionType = actionType;

            if (_channelConfig == null)
                _channelConfig = new PullChannelConfig();

            _routTypeCtrl = new RoutingTypeControler(this.comboBoxRoutingType);
            _routTypeCtrl.Selected += new RoutingTypeSelectedHanlder(_routTypeCtrl_Selected);
            _routTypeCtrl.Initialize();

            _protocolTypeCtrl = new ProtocolTypeControler(this.comboBoxProtocolType);
            _protocolTypeCtrl.Initialize();
        }

        private RoutingTypeControler _routTypeCtrl;
        private ProtocolTypeControler _protocolTypeCtrl;
        private void _routTypeCtrl_Selected(RoutingTypeControler ctrl, RoutingRuleType t)
        {
            if (t == RoutingRuleType.MessageType)
            {
                this.panelMessageContent.Visible = false;
                this.panelMessageType.Visible = true;
                this.panelMessageType.BringToFront();
            }
            else if (t == RoutingRuleType.ContentBased)
            {
                this.panelMessageType.Visible = false;
                this.panelMessageContent.Visible = true;
                this.panelMessageContent.BringToFront();
            }
        }

        private void LoadSetting()
        {
            this.textBoxRequester.Text = _channelConfig.SenderEntityName + " (" + _channelConfig.SenderEntityID + ")";

            switch (_actionType)
            {
                case ActionType.Add:
                    {
                        this.Text = "Add Request/Response Route";
                        InitializeResponser();
                        break;
                    }
                case ActionType.Edit:
                    {
                        this.Text = "Edit Request/Response Route";
                        InitializeResponser();

                        SelectResponser(_channelConfig.ReceiverEntityID);
                        SelectMessageTypes(_channelConfig.RequestContract.MessageTypePairs);

                        _protocolTypeCtrl.SetValue(_channelConfig.ProtocolType);
                        _routTypeCtrl.SetValue(_channelConfig.RequestContract.Type);

                        this.textBoxXPath.Text = _channelConfig.RequestContract.ContentCriteria.XPath;
                        this.textBoxPrefix.Text = _channelConfig.RequestContract.ContentCriteria.XPathPrefixDefinition;
                        this.textBoxRegExp.Text = _channelConfig.RequestContract.ContentCriteria.RegularExpression;
                        break;
                    }
                case ActionType.View:
                    {
                        this.Text = "View Request/Response Route";

                        this.comboBoxResponser.Enabled = false;
                        this.comboBoxResponser.Text = _channelConfig.ReceiverEntityName + " (" + _channelConfig.ReceiverEntityID + ")";
                        
                        this.checkedListBoxMessageType.Enabled = false;
                        foreach (MessageTypePair t in _channelConfig.RequestContract.MessageTypePairs)
                        {
                            this.checkedListBoxMessageType.Items.Add(t, true);
                        }

                        _protocolTypeCtrl.Enable = false;
                        _protocolTypeCtrl.SetValue(_channelConfig.ProtocolType);

                        _routTypeCtrl.Enable = false;
                        _routTypeCtrl.SetValue(_channelConfig.RequestContract.Type);
                        this.textBoxXPath.Text = _channelConfig.RequestContract.ContentCriteria.XPath;
                        this.textBoxPrefix.Text = _channelConfig.RequestContract.ContentCriteria.XPathPrefixDefinition;
                        this.textBoxRegExp.Text = _channelConfig.RequestContract.ContentCriteria.RegularExpression;

                        this.textBoxXPath.ReadOnly = true;
                        this.textBoxPrefix.ReadOnly = true;
                        this.textBoxRegExp.ReadOnly = true;
                        break;
                    }
            }

            _needToGenerateURI = false;
        }
        private void RefreshButton()
        {
            this.buttonOK.Enabled =
                (this.comboBoxResponser.SelectedItem as ResponserWrapper != null) &&
                (this.comboBoxProtocolType.SelectedItem != null) &&
                (this.comboBoxRoutingType.SelectedItem != null);

            ProtocolType t = _protocolTypeCtrl.GetValue();
            this.buttonAdvance.Visible = 
                (_actionType == ActionType.View && t != ProtocolType.LPC )||
                 ((this.comboBoxResponser.SelectedItem as ResponserWrapper != null) &&
                 (t == ProtocolType.RPC_NamedPipe || t == ProtocolType.RPC_SOAP || t == ProtocolType.RPC_TCP));
        }
        private bool SaveSetting()
        {
            ResponserWrapper w = this.comboBoxResponser.SelectedItem as ResponserWrapper;
            if (w == null) return false;

            if (_cfgList != null)
            {
                foreach (PullChannelConfig cfg in _cfgList)
                {
                    if (cfg == _channelConfig) continue;
                    if (cfg.ReceiverEntityID == w.Responser.EntityID)
                    {
                        MessageBox.Show(this, "Responser \"" + w.Responser.Name + "\" is already in the list.",
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.comboBoxResponser.Focus();
                        return false;
                    }
                }
            }

            _channelConfig.ReceiverEntityID = w.Responser.EntityID;
            _channelConfig.ReceiverEntityName = w.Responser.Name;

            _channelConfig.ProtocolType = _protocolTypeCtrl.GetValue();
            switch (_channelConfig.ProtocolType)
            {
                case ProtocolType.LPC:
                    ChannelHelper.GenerateLPCChannel(_channelConfig);
                    break;
                case ProtocolType.RPC_NamedPipe:
                    if (_needToGenerateURI) ChannelHelper.GenerateWCFNamedPipeChannel(_channelConfig);
                    break;
                case ProtocolType.RPC_SOAP:
                    if (_needToGenerateURI) ChannelHelper.GenerateWCFSoapChannel(_channelConfig);
                    break;
                case ProtocolType.RPC_TCP:
                    if (_needToGenerateURI) ChannelHelper.GenerateWCFTcpChannel(_channelConfig);
                    break;
                default:
                    MessageBox.Show(this, "Following protocol type is not supported by now.\r\n\r\n"
                        + this.comboBoxProtocolType.Text, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.comboBoxProtocolType.Focus();
                    return false;
            }

            RoutingRuleType routType = _routTypeCtrl.GetValue();
            _channelConfig.RequestContract.Type = routType;
            if (routType == RoutingRuleType.MessageType)
            {
                _channelConfig.RequestContract.MessageTypePairs.Clear();
                foreach (MessageTypePair t in this.checkedListBoxMessageType.CheckedItems)
                {
                    _channelConfig.RequestContract.MessageTypePairs.Add(t);
                }
            }
            else if (routType == RoutingRuleType.ContentBased)
            {
                string xpath = this.textBoxXPath.Text.Trim();

                if (xpath == null || xpath.Length < 1)
                {
                    MessageBox.Show(this, "Please input the XPath to access the requesting message content.",
                         this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBoxXPath.Focus();
                    return false;
                }

                _channelConfig.RequestContract.MessageTypeList.Clear();
                _channelConfig.RequestContract.ContentCriteria.XPath = xpath;
                _channelConfig.RequestContract.ContentCriteria.XPathPrefixDefinition = this.textBoxPrefix.Text.Trim();
                _channelConfig.RequestContract.ContentCriteria.RegularExpression = this.textBoxRegExp.Text;
            }

            return true;
        }

        private class ResponserWrapper
        {
            public readonly EntityContractBase Responser;
            public ResponserWrapper(EntityContractBase p)
            {
                Responser = p;
            }

            public override string ToString()
            {
                return Responser.Name + " (" + Responser.EntityID + ")";
            }
        }
        private void InitializeResponser()
        {
            this.comboBoxResponser.Items.Clear();
            this.comboBoxResponser.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (EntityContractBase c in Program.SolutionMgt.Config.Entities)
            {
                if ((c.Interaction & InteractionTypes.Responser) == InteractionTypes.Responser)
                    this.comboBoxResponser.Items.Add(new ResponserWrapper(c));
            }
        }
        private void SelectResponser(Guid id)
        {
            foreach (ResponserWrapper w in this.comboBoxResponser.Items)
            {
                if (w.Responser.EntityID == id)
                {
                    this.comboBoxResponser.SelectedItem = w;
                    break;
                }
            }
        }

        private void RefreshMessageType()
        {
            this.checkedListBoxMessageType.Items.Clear();

            ResponserWrapper w = this.comboBoxResponser.SelectedItem as ResponserWrapper;
            if (w == null) return;

            foreach (MessageTypePair t in w.Responser.ResponseDescription.MessageTypePairList)
            {
                this.checkedListBoxMessageType.Items.Add(t);
            }
        }
        private void SelectMessageTypes(XCollection<MessageTypePair> list)
        {
            List<MessageTypePair> slist = new List<MessageTypePair>();
            foreach (MessageTypePair t in this.checkedListBoxMessageType.Items)
            {
                foreach (MessageTypePair mt in list)
                {
                    if (mt.EqualsTo(t))
                    {
                        slist.Add(t);
                    }
                }
            }
            foreach (MessageTypePair s in slist)
            {
                this.checkedListBoxMessageType.SetItemChecked
                        (this.checkedListBoxMessageType.Items.IndexOf(s), true);
            }
        }

        private bool _needToGenerateURI = false;
        private void EditAdvanceSetting()
        {
            if (_actionType == ActionType.View)
            {
                FormChannelURI frm = new FormChannelURI(_channelConfig);
                frm.SetReadOnly();
                frm.ShowDialog(this);
            }
            else
            {
                if (!SaveSetting()) return;
                FormChannelURI frm = new FormChannelURI(_channelConfig);
                if (frm.ShowDialog(this) == DialogResult.OK) _needToGenerateURI = false;
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
        private void FormPullChannel_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }
        private void comboBoxResponser_SelectedIndexChanged(object sender, EventArgs e)
        {
            _needToGenerateURI = true;
            RefreshMessageType();
            RefreshButton();
        }
        private void comboBoxProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _needToGenerateURI = true;
            RefreshButton();
        }
        private void buttonAdvance_Click(object sender, EventArgs e)
        {
            EditAdvanceSetting();
        }
    }
}