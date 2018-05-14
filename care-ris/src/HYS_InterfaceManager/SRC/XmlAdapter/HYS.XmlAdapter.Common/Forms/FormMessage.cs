using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Common.Controlers;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Forms
{
    public partial class FormMessage<T> : Form
        where T : XIMMessage, new()
    {
        private T _message;
        public T Message
        {
            get { return _message; }
        }

        private Logging _log;
        private bool _isCopyItem;
        private string _dbConnection;
        private XCollection<T> _messageList;

        private bool _isInbound;
        private bool _mappingAlreadyExist;
        private bool _hasChangeMessageEventType;

        public FormMessage(T message, XCollection<T> messageList, string dbConnection, Logging log, bool copyItem)
        {
            InitializeComponent();

            _isInbound = (typeof(T) == typeof(XIMInboundMessage));

            InitializeEventType();

            _log = log;
            _isCopyItem = copyItem;
            _messageList = messageList;
            _dbConnection = dbConnection;
            
            _hl7EventCtrl = new HL7EventTypeControler(this.comboBoxHL7);
            _gwEventCtrl = new GWEventTypeControler(this.comboBoxGCGateway);
            _listViewCtrl = new XmlListViewControler<XIMMappingItem>(this.listViewMain, _isInbound);

            _listViewCtrl.DoubleClick += new EventHandler(_listViewCtrl_DoubleClick);
            _listViewCtrl.SelectedIndexChanged += new EventHandler(_listViewCtrl_SelectedIndexChanged);

            _message = message;

            if (_message == null)
            {
                this.Text = "Add Message Mapping";
                _message = new T();
                Reset();
            }
            else
            {
                if (_isCopyItem)
                {
                    this.Text = "Add Message Mapping";
                }
                else
                {
                    this.Text = "Edit Message Mapping";
                }
            }
        }
        public FormMessage(T message, XCollection<T> messageList, string dbConnection, Logging log)
            : this(message, messageList, dbConnection, log, false)
        {
        }
        
        private void InitializeEventType()
        {
            if (!_isInbound)
            {
                this.buttonAdvance.Visible = true;

                this.labelGCEventType.Text = this.labelGCEventType.Text.Replace("To", "From");
                this.labelHL7EventType.Text = this.labelHL7EventType.Text.Replace("From", "To");

                int top = this.labelHL7EventType.Top;
                this.labelHL7EventType.Top = this.labelGCEventType.Top;
                this.labelGCEventType.Top = top;

                top = this.comboBoxHL7.Top;
                this.comboBoxHL7.Top = this.comboBoxGCGateway.Top;
                this.comboBoxGCGateway.Top = top;

                this.listViewMain.Columns.Remove(this.columnHeaderRedundancy);
            }
        }

        private GWEventTypeControler _gwEventCtrl;
        private HL7EventTypeControler _hl7EventCtrl;
        private XmlListViewControler<XIMMappingItem> _listViewCtrl;

        private void _listViewCtrl_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        private void _listViewCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }
        private void _hl7EventCtrl_OnSelectChanged(object sender, EventArgs e)
        {
            _hasChangeMessageEventType = true;

            if (_isInbound)
            {
                CheckExistMessage();
                return;
            }

            if (_isCopyItem)
            {
                CheckExistMessage();
                return;
            }

            HL7EventType et = _hl7EventCtrl.Value;
            if (et == null) return;

            foreach (XIMMappingItem item in _message.MappingList)
            {
                if (item.Element.GetLastName() == XmlMessage.Request.Name.GetLastName())
                {
                    item.Translating.Type = TranslatingType.FixValue;
                    item.Translating.ConstValue = et.Name;
                    _listViewCtrl.SelectItem(item);
                    break;
                }
            }

            foreach (XIMMappingItem item in _message.MappingList)
            {
                if (item.Element.GetLastName() == XmlMessage.Request.Qualifier.GetLastName())
                {
                    item.Translating.Type = TranslatingType.FixValue;
                    item.Translating.ConstValue = et.Qualifier;
                    _listViewCtrl.SelectItem(item);
                    break;
                }
            }
        }
        private void _gwEventCtrl_OnSelectChanged(object sender, EventArgs e)
        {
            _hasChangeMessageEventType = true;
            CheckExistMessage();
        }

        private void LoadSetting()
        {
            _gwEventCtrl.Value = _message.GWEventType;
            _hl7EventCtrl.Value = _message.HL7EventType;
            RefreshList();

            _hl7EventCtrl.OnSelectChanged += new EventHandler(_hl7EventCtrl_OnSelectChanged);
            _gwEventCtrl.OnSelectChanged += new EventHandler(_gwEventCtrl_OnSelectChanged);
        }
        private bool SaveSetting()
        {
            _message.MappingList.Clear();
            List<XIMMappingItem> list = _listViewCtrl.GetCurrentList();
            foreach (XIMMappingItem i in list) _message.MappingList.Add(i);

            _message.GWEventType = _gwEventCtrl.Value;
            _message.HL7EventType = _hl7EventCtrl.Value;
            XIMTransformHelper.RefreshXSLFileName(_message);
            return true;
        }

        private void Edit()
        {
            XIMMappingItem item = _listViewCtrl.GetSelectedItem();
            if (item == null) return;

            List<XIMMappingItem> list = null;
            if (_isInbound) list = _listViewCtrl.GetCurrentList();

            FormField frm = new FormField(item, list, _dbConnection, _log, _isInbound);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            //RefreshList();
            _listViewCtrl.SelectItem(item);
        }
        private void Reset()
        {
            _message.MappingList.Clear();
            List<XIMMappingItem> list = XIMHelper.GetRequestMessage<XIMMappingItem>();
            foreach (XIMMappingItem item in list) _message.MappingList.Add(item);
            _listViewCtrl.RefreshList(list);
            RefreshButtons();
        }
        private void Advance()
        {
            XIMOutboundMessage msg = _message as XIMOutboundMessage;
            if (msg == null) return;

            FormAdvance frm = new FormAdvance(msg);
            frm.ShowDialog(this);
        }
        private void RefreshList()
        {
            List<XIMMappingItem> list = new List<XIMMappingItem>();
            foreach (XIMMappingItem item in _message.MappingList) list.Add(item);
            _listViewCtrl.RefreshList(list);
        }
        private void RefreshButtons()
        {
            this.buttonEdit.Enabled = _listViewCtrl.HasSelectedItem();
            this.buttonOK.Enabled = !_mappingAlreadyExist;

            if (_isCopyItem &&
                !_hasChangeMessageEventType)
            {
                this.buttonOK.Enabled = false;
            }

            RefreshButtons2();
        }
        private void CheckExistMessage()
        {
            _mappingAlreadyExist = false;

            GWEventType gwET = _gwEventCtrl.Value;
            HL7EventType hlET = _hl7EventCtrl.Value;

            foreach (XIMMessage msg in _messageList)
            {
                if (!_isCopyItem)
                {
                    if (_message.GWEventType.Code == gwET.Code &&
                        _message.HL7EventType.Name == hlET.Name &&
                        _message.HL7EventType.Qualifier == hlET.Qualifier)
                        continue;
                }

                if (_isInbound)
                {
                    if (msg.GWEventType.Code == gwET.Code &&
                        msg.HL7EventType.Name == hlET.Name &&
                        msg.HL7EventType.Qualifier == hlET.Qualifier)
                    {
                        _mappingAlreadyExist = true;

                        string str = "from [" + hlET.ToString() + "] to [" + gwET.ToString() + "]";

                        MessageBox.Show(this, "Message mapping " + str + " is already exist.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }
                }
                else
                {
                    if (msg.GWEventType.Code == gwET.Code)
                    {
                        _mappingAlreadyExist = true;

                        string str = "from [" + gwET.ToString() + "]";

                        MessageBox.Show(this, "Message mapping " + str + " is already exist.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }
                }
            }

            RefreshButtons();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonAdvance_Click(object sender, EventArgs e)
        {
            Advance();
        }
        private void checkBoxHide_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void FormMessage_Load(object sender, EventArgs e)
        {
            LoadSetting();
            RefreshButtons();

            if (_isCopyItem)
            {
                this.buttonOK.Enabled = false;
                if (!_isInbound) this.comboBoxGCGateway.Focus();
            }
        }

        #region XIM Schema Extension Support

        private void RefreshButtons2()
        {
            XIMMappingItem item = _listViewCtrl.GetSelectedItem();

            this.buttonDelete.Enabled = (item != null);
            this.buttonAddChild.Enabled = (item != null && XIMHelper.IsComplex(item.Element.Type));
            this.buttonAddElement.Enabled = (item != null);
        }

        private void CollectChildItem(List<XIMMappingItem> list, XIMMappingItem item, List<XIMMappingItem> removelist)
        {
            foreach (XIMMappingItem i in list)
            {
                if (i.Element.IsChildOf(item.Element))
                {
                    removelist.Add(i);
                    CollectChildItem(list, i, removelist);
                }
            }
        }

        private void CollectParentItem(List<XIMMappingItem> list, XIMMappingItem item)
        {
            bool hasOtherChild = false;
            XIMMappingItem parent = null;

            foreach (XIMMappingItem i in list)
            {
                if (item.Element.IsChildOf(i.Element))
                {
                    parent = i;
                    foreach (XIMMappingItem j in list)
                    {
                        if (j.Element.IsChildOf(parent.Element))
                        {
                            hasOtherChild = true;
                            break;
                        }
                    }
                    break;
                }
            }

            if (!hasOtherChild && parent != null)
            {
                parent.Element.ClassTypeName = "";
                //list.Remove(parent);
            }
        }

        private void buttonAddElement_Click(object sender, EventArgs e)
        {
            // create new element
            XIMMappingItem item = _listViewCtrl.GetSelectedItem();
            if (item == null) return;

            XmlElement tmp = item.Element.Clone();
            tmp.XPath = item.Element.GetXDirectory();
            FormElement frm = new FormElement(tmp);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            XmlElement ele = frm.Element;

            // check if element with the same name is exist
            List<XIMMappingItem> list = _listViewCtrl.GetCurrentList();
            foreach (XIMMappingItem i in list)
            {
                if (i.Element.XPath == ele.XPath)
                {
                    MessageBox.Show(this, "This element already exists.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // insert new child
            XIMMappingItem citem = new XIMMappingItem(ele);
            int index = list.IndexOf(item);
            list.Insert(index + 1, citem);

            // display new child
            _listViewCtrl.RefreshList(list);

            foreach (XmlListViewItem<XIMMappingItem> xi in listViewMain.Items)
            {
                if (xi.Item == citem)
                {
                    xi.Selected = true;
                    xi.EnsureVisible();
                    break;
                }
            }
        }

        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            // create new child
            XIMMappingItem item = _listViewCtrl.GetSelectedItem();
            if (item == null || !XIMHelper.IsComplex(item.Element.Type)) return;

            FormElement frm = new FormElement(item.Element);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            XmlElement ele = frm.Element;

            // expand old child
            if (listViewMain.SelectedItems.Count > 0)
            {
                XmlListViewItem<XIMMappingItem> xitem = listViewMain.SelectedItems[0] as XmlListViewItem<XIMMappingItem>;
                xitem.Checked = true;
            }

            // check if child with the same name is exist
            List<XIMMappingItem> list = _listViewCtrl.GetCurrentList();
            foreach (XIMMappingItem i in list)
            {
                if (i.Element.IsChildOf(item.Element) && 
                    i.Element.GetLastName() == ele.GetLastName())
                {
                    MessageBox.Show(this, "This element already exists.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // insert new child
            XIMMappingItem citem = new XIMMappingItem(ele);
            int index = list.IndexOf(item);
            list.Insert(index + 1, citem);

            // display new child
            _listViewCtrl.RefreshList(list);

            foreach (XmlListViewItem<XIMMappingItem> xi in listViewMain.Items)
            {
                if (xi.Item == citem)
                {
                    xi.Selected = true;
                    xi.EnsureVisible();
                    break;
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // remove element
            XIMMappingItem item = _listViewCtrl.GetSelectedItem();
            if (item == null) return;

            List<XIMMappingItem> list = _listViewCtrl.GetCurrentList();
            list.Remove(item);

            // remove child
            List<XIMMappingItem> rlist = new List<XIMMappingItem>();
            CollectChildItem(list, item, rlist);
            foreach (XIMMappingItem i in rlist)
            {
                list.Remove(i);
            }

            // update parent
            CollectParentItem(list, item);

            // refresh list
            _listViewCtrl.RefreshList(list);
        }

        #endregion   
    }
}