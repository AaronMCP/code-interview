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
    public partial class FormPushChannel : Form
    {
        public enum ActionType
        {
            Add,
            Edit,
            View,
        }

        private ActionType _actionType;
        private PushChannelConfig _channelConfig;
        private XCollection<PushChannelConfig> _cfgList;
        public PushChannelConfig ChannelConfig
        {
            get { return _channelConfig; }
        }

        public FormPushChannel(PushChannelConfig cfg, ActionType actionType)
            : this(cfg, actionType, null)
        {
        }
        public FormPushChannel(PushChannelConfig cfg, ActionType actionType, XCollection<PushChannelConfig> cfgList)
        {
            InitializeComponent();

            _cfgList = cfgList;
            _channelConfig = cfg;
            _actionType = actionType;

            if (_channelConfig == null)
                _channelConfig = new PushChannelConfig();

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
            this.textBoxSubscriber.Text = _channelConfig.ReceiverEntityName + " (" + _channelConfig.ReceiverEntityID + ")";

            switch (_actionType)
            {
                case ActionType.Add:
                    {
                        this.Text = "Add Publication/Subscription Route";
                        InitializePublisher();
                        break;
                    }
                case ActionType.Edit:
                    {
                        this.Text = "Edit Publication/Subscription Route";
                        InitializePublisher();

                        SelectPublisher(_channelConfig.SenderEntityID);
                        SelectMessageTypes(_channelConfig.Subscription.MessageTypeList);

                        _protocolTypeCtrl.SetValue(_channelConfig.ProtocolType);
                        _routTypeCtrl.SetValue(_channelConfig.Subscription.Type);

                        this.textBoxXPath.Text = _channelConfig.Subscription.ContentCriteria.XPath;
                        this.textBoxPrefix.Text = _channelConfig.Subscription.ContentCriteria.XPathPrefixDefinition;
                        this.textBoxRegExp.Text = _channelConfig.Subscription.ContentCriteria.RegularExpression;
                        break;
                    }
                case ActionType.View:
                    {
                        this.Text = "View Publication/Subscription Route";

                        this.comboBoxPublisher.Enabled = false;
                        this.comboBoxPublisher.Text = _channelConfig.SenderEntityName + " (" + _channelConfig.SenderEntityID + ")";

                        this.checkedListBoxMessageType.Enabled = false;
                        foreach (MessageType t in _channelConfig.Subscription.MessageTypeList)
                        {
                            this.checkedListBoxMessageType.Items.Add(t, true);
                        }

                        _protocolTypeCtrl.Enable = false;
                        _protocolTypeCtrl.SetValue(_channelConfig.ProtocolType);

                        _routTypeCtrl.Enable = false;
                        _routTypeCtrl.SetValue(_channelConfig.Subscription.Type);
                        this.textBoxXPath.Text = _channelConfig.Subscription.ContentCriteria.XPath;
                        this.textBoxPrefix.Text = _channelConfig.Subscription.ContentCriteria.XPathPrefixDefinition;
                        this.textBoxRegExp.Text = _channelConfig.Subscription.ContentCriteria.RegularExpression;

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
                (this.comboBoxPublisher.SelectedItem as PublisherWrapper != null) &&
                (this.comboBoxProtocolType.SelectedItem != null) &&
                (this.comboBoxRoutingType.SelectedItem != null);

            ProtocolType t = _protocolTypeCtrl.GetValue();
            this.buttonAdvance.Visible =
                (_actionType == ActionType.View && t != ProtocolType.LPC) ||
                ((this.comboBoxPublisher.SelectedItem as PublisherWrapper != null) &&
                (t == ProtocolType.MSMQ));
        }
        private bool SaveSetting()
        {
            PublisherWrapper w = this.comboBoxPublisher.SelectedItem as PublisherWrapper;
            if (w == null) return false;

            if (_cfgList != null)
            {
                foreach (PushChannelConfig cfg in _cfgList)
                {
                    if (cfg == _channelConfig) continue;
                    if (cfg.SenderEntityID == w.Publisher.EntityID)
                    {
                        MessageBox.Show(this, "Publisher \"" + w.Publisher.Name + "\" is already in the list.",
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.comboBoxPublisher.Focus();
                        return false;
                    }
                }
            }

            _channelConfig.SenderEntityID = w.Publisher.EntityID;
            _channelConfig.SenderEntityName = w.Publisher.Name;

            _channelConfig.ProtocolType = _protocolTypeCtrl.GetValue();
            switch (_channelConfig.ProtocolType)
            {
                case ProtocolType.LPC:
                    ChannelHelper.GenerateLPCChannel(_channelConfig);
                    break;
                case ProtocolType.MSMQ:
                    if (_needToGenerateURI) ChannelHelper.GenerateMSMQChannel(_channelConfig);
                    break;
                default:
                    MessageBox.Show(this, "Following protocol type is not supported by now.\r\n\r\n"
                        + this.comboBoxProtocolType.Text, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.comboBoxProtocolType.Focus();
                    return false;
            }

            RoutingRuleType routType = _routTypeCtrl.GetValue();
            _channelConfig.Subscription.Type = routType;
            if (routType == RoutingRuleType.MessageType)
            {
                _channelConfig.Subscription.MessageTypeList.Clear();
                foreach (MessageType t in this.checkedListBoxMessageType.CheckedItems)
                {
                    _channelConfig.Subscription.MessageTypeList.Add(t);
                }
            }
            else if (routType == RoutingRuleType.ContentBased)
            {
                string xpath = this.textBoxXPath.Text.Trim();

                if (xpath == null || xpath.Length < 1)
                {
                    MessageBox.Show(this, "Please input the XPath to access the message content.",
                         this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBoxXPath.Focus();
                    return false;
                }

                _channelConfig.Subscription.MessageTypeList.Clear();
                _channelConfig.Subscription.ContentCriteria.XPath = xpath;
                _channelConfig.Subscription.ContentCriteria.XPathPrefixDefinition = this.textBoxPrefix.Text.Trim();
                _channelConfig.Subscription.ContentCriteria.RegularExpression = this.textBoxRegExp.Text;
            }

            return true;
        }

        private class PublisherWrapper
        {
            public readonly EntityContractBase Publisher;
            public PublisherWrapper(EntityContractBase p)
            {
                Publisher = p;
            }

            public override string ToString()
            {
                return Publisher.Name + " (" + Publisher.EntityID + ")";
            }
        }
        private void InitializePublisher()
        {
            this.comboBoxPublisher.Items.Clear();
            this.comboBoxPublisher.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (EntityContractBase c in Program.SolutionMgt.Config.Entities)
            {
                if ((c.Interaction & InteractionTypes.Publisher) == InteractionTypes.Publisher)
                    this.comboBoxPublisher.Items.Add(new PublisherWrapper(c));
            }
            if (this.comboBoxPublisher.Items.Count > 0)
                this.comboBoxPublisher.SelectedIndex = 0;
        }
        private void SelectPublisher(Guid id)
        {
            foreach (PublisherWrapper w in this.comboBoxPublisher.Items)
            {
                if (w.Publisher.EntityID == id)
                {
                    this.comboBoxPublisher.SelectedItem = w;
                    break;
                }
            }
        }

        private void RefreshMessageType()
        {
            this.checkedListBoxMessageType.Items.Clear();

            PublisherWrapper w = this.comboBoxPublisher.SelectedItem as PublisherWrapper;
            if (w == null) return;

            foreach (MessageType t in w.Publisher.Publication.MessageTypeList)
            {
                this.checkedListBoxMessageType.Items.Add(t);
            }
        }
        private void SelectMessageTypes(XCollection<MessageType> list)
        {
            List<MessageType> slist = new List<MessageType>();
            foreach (MessageType t in this.checkedListBoxMessageType.Items)
            {
                foreach (MessageType mt in list)
                {
                    if (mt.EqualsTo(t))
                    {
                        slist.Add(t);
                    }
                }
            }
            foreach (MessageType s in slist)
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
        private void FormPushChannel_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }
        private void comboBoxPublisher_SelectedIndexChanged(object sender, EventArgs e)
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