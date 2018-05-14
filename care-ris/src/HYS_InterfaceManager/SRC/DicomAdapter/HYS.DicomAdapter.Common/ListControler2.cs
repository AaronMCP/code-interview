using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Base;

namespace HYS.DicomAdapter.Common
{
    public class ListControler2
    {
        protected ListView _listViewElement;
        public ListView ListView
        {
            get { return _listViewElement; }
        }

        protected bool _isInbound;
        protected bool _checkLocked;
        protected bool _asQueryResult;

        private Logging _log;
        private string _gwDataDBConnection;

        protected static Color clrInvalid = Color.FromArgb(128, 128, 128);
        protected static Color clrUnchecked = Color.Black;
        protected static Color clrChecked = Color.Blue;
        protected static Color clrAuto = Color.FromArgb(128, 0, 128);
        protected static char PrefixChar = '>';

        public ListControler2(bool asQueryResult, ListView listViewElement, string gwDataDBConnection, Logging log)
        {
            _log = log;
            _gwDataDBConnection = gwDataDBConnection;

            _asQueryResult = asQueryResult;
            _listViewElement = listViewElement;
            _listViewElement.ItemCheck += new ItemCheckEventHandler(_listViewElement_ItemCheck);
            _listViewElement.ItemChecked += new ItemCheckedEventHandler(_listViewElement_ItemChecked);
            _listViewElement.SelectedIndexChanged += new EventHandler(_listViewElement_SelectedIndexChanged);
            _listViewElement.DoubleClick += new EventHandler(_listViewElement_DoubleClick);
        }

        public ListControler2(bool asQueryResult, ListView listViewElement, string gwDataDBConnection, Logging log, bool isInbound)
            : this(asQueryResult, listViewElement, gwDataDBConnection, log)
        {
            _isInbound = isInbound;
        }

        public virtual void RefreshList(IDicomMappingItem[] itemlist)
        {
            string prefix = "";
            this._listViewElement.Items.Clear();
            foreach (IDicomMappingItem qr in itemlist)
            {
                ListViewItem item = GetListViewItem(ref prefix, qr);
                if (item == null) continue;
                this._listViewElement.Items.Add(item);
                if (!qr.IsValid()) item.ForeColor = clrInvalid;
            }
            GroupByCatagory();
        }
        public virtual void SelectItem(IDicomMappingItem item)
        {
            foreach (ListViewItem i in this._listViewElement.Items)
            {
                IDicomMappingItem ii = i.Tag as IDicomMappingItem;
                if (ii == item)
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    break;
                }
            }
        }
        public virtual IDicomMappingItem GetSelectedItem()
        {
            if (this._listViewElement.SelectedItems.Count < 1) return null;
            return this._listViewElement.SelectedItems[0].Tag as IDicomMappingItem;
        }
        public virtual int GetItemCount()
        {
            return this._listViewElement.Items.Count;
        }
        public virtual void SelectAll()
        {
            foreach (ListViewItem item in this._listViewElement.Items)
            {
                item.Checked = true;
            }
        }
        public virtual void ClearAll()
        {
            foreach (ListViewItem item in this._listViewElement.Items)
            {
                item.Checked = false;
            }
        }

        public event EventHandler SelectedIndexChanged;
        public event EventHandler DoubleClick;

        public T Add<T>(Form parentForm, XCollection<T> list)
            where T : MappingItem, IDicomMappingItem, new()
        {
            T selitem = GetSelectedItem() as T;

            FormElement2<T> frm = new FormElement2<T>(null, list, selitem, false, _gwDataDBConnection, _log, _asQueryResult, _isInbound);
            BeforeShowFormElement(ButtonType.Add, frm, selitem);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return null;
            AfterShowFormElement(ButtonType.Add, frm, selitem);

            T newitem = frm.MappingItem;
            if (newitem == null) return null;

            if (selitem == null)
            {
                list.Add(newitem);
            }
            else
            {
                newitem.DPath.Catagory = selitem.DPath.Catagory;
                int index = list.IndexOf(selitem);
                list.Insert(index, newitem);
            }

            return newitem;
        }
        public T AddChild<T>(Form parentForm, XCollection<T> list)
            where T : MappingItem, IDicomMappingItem, new()
        {
            T selitem = GetSelectedItem() as T;
            if (selitem == null || selitem.DPath.VR != DVR.SQ) return null;

            FormElement2<T> frm = new FormElement2<T>(null, list, selitem, true, _gwDataDBConnection, _log, _asQueryResult, _isInbound);
            BeforeShowFormElement(ButtonType.AddChild, frm, selitem);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return null;
            AfterShowFormElement(ButtonType.AddChild, frm, selitem);

            T newitem = frm.MappingItem;
            if (newitem == null) return null;

            int index = list.IndexOf(selitem);
            bool hasChild = DicomMappingHelper.HasSequence<T>(index, list);

            if (hasChild == false)
            {
                T iBegin = new T();
                DPath.SetItemGroupPathBegin(iBegin.DPath, 0);
                T iEnd = new T();
                DPath.SetItemGroupPathEnd(iEnd.DPath,0);

                int sqIndex = index + 1;
                list.Insert(sqIndex++, iBegin);
                list.Insert(sqIndex, iEnd);
            }

            if (DicomMappingHelper.FindEndDPathIndex<T>(ref index, list))
            {
                newitem.DPath.Catagory = selitem.DPath.Catagory;
                list.Insert(index, newitem);
            }

            return newitem;
        }
        public void Delete<T>(Form parentForm, XCollection<T> list)
            where T : MappingItem, IDicomMappingItem, new()
        {
            T selitem = GetSelectedItem() as T;
            if (selitem == null) return;

            if (selitem.DPath.VR == DVR.SQ)
            {
                int index = list.IndexOf(selitem);
                if (DicomMappingHelper.HasSequence<T>(index, list))
                {
                    if (MessageBox.Show(parentForm, "Deleting SQ element will automatically delete all elements in the SQ sequence.\r\nAre you sure to continue?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DicomMappingHelper.DeleteSequence<T>(index, list);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            list.Remove(selitem);
        }
        public T Edit<T>(Form parentForm, XCollection<T> list)
            where T : MappingItem, IDicomMappingItem, new()
        {
            T selitem = GetSelectedItem() as T;
            if (selitem == null) return null;

            FormElement2<T> frm = new FormElement2<T>(selitem, list, selitem, false, _gwDataDBConnection, _log, _asQueryResult, _isInbound);
            BeforeShowFormElement(ButtonType.Edit, frm, selitem);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return null;
            AfterShowFormElement(ButtonType.Edit, frm, selitem);

            return selitem;
        }

        /// <summary>
        /// UI Logic is controled by isInbound and asQueryResult,
        /// FieldControler2, TagControler2 and ListControler2 and its inherited class is responsible for implementing UI Logic.
        /// UI View (FormElement2) does not implement UI Logic but contains UI Logic.
        /// Therefore, pass the UI View to inherited class here,
        /// so that inherited class can inject its own UI Logic.
        /// </summary>
        protected virtual void BeforeShowFormElement<T>(ButtonType type, FormElement2<T> form, T obj)
            where T : MappingItem, IDicomMappingItem, new()
        {
        }
        protected virtual void AfterShowFormElement<T>(ButtonType type, FormElement2<T> form, T obj)
            where T : MappingItem, IDicomMappingItem, new()
        {
        }
        protected enum ButtonType
        {
            Add,
            AddChild,
            Edit,
        }

        private string GetTagDisplayName(string name)
        {
            if (name == "4195392") return "ProtocolContextSequence";    //0040,0440
            return name.TrimStart('k');
        }
        private ListViewItem GetListViewItem(ref string prefix, IDicomMappingItem qr)
        {
            if (qr == null) return null;
            ListViewItem item = new ListViewItem();

            // prefix handler
            if (qr.DPath.VR == DVR.Unknown && qr.DPath.Type == DPathType.BeginItem)
            {
                prefix += PrefixChar;
            }

            // display value
            if (qr.IsValid())
            {
                item.Checked = qr.DPath.Enable;
            }
            if ( ( (_isInbound && _asQueryResult) || (!_isInbound && ! _asQueryResult )) &&
                qr.Translating.Type == TranslatingType.FixValue)
            {
                item.Text = "";
                item.SubItems.Add("");
                item.SubItems.Add("");
            }
            else
            {
                string strName = qr.DPath.GetTagName();
                string strG = qr.DPath.GetGroupString();
                string strE = qr.DPath.GetElementString();
                string strTag = "", strVR = "";
                if (qr.DPath.VR == DVR.Unknown)
                {
                    strTag = prefix + qr.DPath.Path;
                    item.Text = strTag;
                }
                else
                {
                    strVR = qr.DPath.VR.ToString();
                    strTag = prefix + "(" + strG + "," + strE + ")";
                    item.Text = strTag;
                    item.SubItems.Add(strVR);
                    item.SubItems.Add(GetTagDisplayName(strName));
                }
            }

            // display color
            int depth = prefix.Length;
            int luminance = 255 - 20 * depth;
            item.BackColor = Color.FromArgb(luminance, luminance, luminance);

            // prefix handler
            if (qr.DPath.VR == DVR.Unknown && qr.DPath.Type == DPathType.EndItem)
            {
                if (prefix.Length == 1) prefix = "";
                if (prefix.Length > 1) prefix = prefix.Substring(1);
            }

            GetGWDBField(item, qr);
            item.Tag = qr;

            if (qr.DPath.VR == DVR.Unknown) return null;
            return item;
        }
        private void GetGWDBField(ListViewItem item, IDicomMappingItem qr)
        {
            if (qr.DPath.VR == DVR.Unknown || qr.DPath.VR == DVR.SQ) return;
            GWDataDBField field = qr.GWDataDBField;
            if (field.Table == GWDataDBTable.None ||
                (qr.Translating.Type == TranslatingType.FixValue && _asQueryResult && _isInbound == false))
            {
                item.SubItems.Add("");
            }
            else
            {
                item.SubItems.Add(field.GetFullFieldName());
            }
            if (qr.Translating.Type == TranslatingType.None)
            {
                item.SubItems.Add("");
            }
            else
            {
                item.SubItems.Add(qr.Translating.ToString());
            }
            if (_isInbound && qr.RedundancyFlag)
            {
                item.SubItems.Add("True");
            }
        }
        private void GroupByCatagory()
        {
            Dictionary<string, ArrayList> gList = new Dictionary<string, ArrayList>();
            foreach (ListViewItem item in this._listViewElement.Items)
            {
                IDicomMappingItem dcmItem = item.Tag as IDicomMappingItem;
                if (dcmItem == null) continue;

                string text = dcmItem.DPath.Catagory;
                if (text.Length < 1) text = "Other";
                if (gList.ContainsKey(text))
                {
                    ArrayList list = gList[text] as ArrayList;
                    if (list != null) list.Add(item);
                }
                else
                {
                    ArrayList list = new ArrayList();
                    gList[text] = list;
                    list.Add(item);
                }
            }

            this._listViewElement.Groups.Clear();
            foreach (KeyValuePair<string, ArrayList> de in gList)
            {
                ListViewGroup g = new ListViewGroup(de.Key as string, HorizontalAlignment.Left);
                this._listViewElement.Groups.Add(g);

                ArrayList ilist = de.Value as ArrayList;
                if (ilist == null) return;

                foreach (ListViewItem i in ilist)
                {
                    i.Group = g;
                }
            }

            this._listViewElement.ShowGroups = true;
        }

        protected virtual void _listViewElement_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_checkLocked) return;
            ListViewItem item = this._listViewElement.Items[e.Index];
            IDicomMappingItem qr = item.Tag as IDicomMappingItem;
            if (qr == null || qr.DPath.VR == DVR.Unknown) e.NewValue = e.CurrentValue;
            if (qr != null && qr.DPath.VR == DVR.SQ)
            {
                string strRoot = qr.DPath.Path;
                foreach (ListViewItem i in this._listViewElement.Items)
                {
                    IDicomMappingItem subqr = i.Tag as IDicomMappingItem;
                    if (subqr != null &&
                        subqr.DPath.Path.Length > strRoot.Length &&
                        subqr.DPath.Path.IndexOf(strRoot) == 0)
                    {
                        if (e.NewValue == CheckState.Unchecked) i.Checked = false;
                        if (e.NewValue == CheckState.Checked) i.Checked = true;
                    }
                }
            }
            if (/*_asQueryResult == false &&*/
                qr != null && qr.IsValid() == false)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }
        protected virtual void _listViewElement_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            IDicomMappingItem dcmItem = e.Item.Tag as IDicomMappingItem;
            if (dcmItem == null) return;

            if (e.Item.Checked)
            {
                dcmItem.DPath.Enable = true;
                e.Item.ForeColor = clrChecked;

                string str = e.Item.Text;
                if (str.Length > 0)
                {
                    int prefixLength = str.Length - str.TrimStart(PrefixChar).Length;
                    if (prefixLength > 0)
                    {
                        for (int i = e.Item.Index - 1; i >= 0; i--)
                        {
                            ListViewItem cItem = _listViewElement.Items[i];
                            
                            string strSQ = cItem.Text;
                            if (strSQ.Length < 1) continue;

                            int prefixLengthSQ = strSQ.Length - strSQ.TrimStart(PrefixChar).Length;
                            if (prefixLength - prefixLengthSQ == 1)
                            {
                                _checkLocked = true;
                                cItem.Checked = true;
                                _checkLocked = false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                dcmItem.DPath.Enable = false;
                e.Item.ForeColor = clrUnchecked;
            }
        }
        protected virtual void _listViewElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, e);
        }
        protected virtual void _listViewElement_DoubleClick(object sender, EventArgs e)
        {
            IDicomMappingItem item = GetSelectedItem();
            if (item == null || item.GWDataDBField.Table != GWDataDBTable.None) return;
            if (DoubleClick != null) DoubleClick(this, e);
        }
    }
}
