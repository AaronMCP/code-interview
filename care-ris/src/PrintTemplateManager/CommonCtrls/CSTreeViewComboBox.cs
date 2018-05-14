using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.ComponentModel;
using Telerik.WinControls.Primitives;
using System.Drawing;
using Telerik.WinControls.Layouts;
using System.Data;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public class CSTreeViewComboBox : RadComboBox
    {
        private RadHostItem _h;
        private CSTreeView _treeView;
        private const string TreeNodePathSeparator = "->";

        public override System.Drawing.Color BackColor
        {
            get
            {
                return (this.ComboBoxElement.TextBoxElement.Children[0] as RadTextBoxItem).BackColor;
            }
            set
            {
                (this.ComboBoxElement.TextBoxElement.Children[0] as RadTextBoxItem).BackColor = value;
                (this.ComboBoxElement.Children[0] as FillPrimitive).BackColor = value;
            }
        }

        private RadDropDownStyle dropDownStyle = RadDropDownStyle.DropDown;
        [DefaultValue(RadDropDownStyle.DropDown)]
        new public RadDropDownStyle DropDownStyle
        {
            get
            {
                return dropDownStyle;
            }
            set
            {
                if (value == RadDropDownStyle.DropDown)
                {
                    dropDownStyle = RadDropDownStyle.DropDown;
                    base.DropDownStyle = RadDropDownStyle.DropDown;
                }
                else if (((RadDropDownStyle)value) == RadDropDownStyle.DropDownList)
                {
                    if (!IsDesignMode)
                    {
                        dropDownStyle = RadDropDownStyle.DropDown;
                        ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
                        base.DropDownStyle = RadDropDownStyle.DropDown;
                    }
                    else//designmode
                    {
                        dropDownStyle = value;
                        base.DropDownStyle = (RadDropDownStyle)value;
                    }
                }
            }
        }

        private string _parentNodeIDColumn;

        /// <summary>
        /// ParentID of root node should be empty
        /// </summary>
        public string ParentNodeIDColumn
        {
            get { return _parentNodeIDColumn; }
            set { _parentNodeIDColumn = value; }
        }

        private string _nodeNameColumn;

        public string NodeNameColumn
        {
            get { return _nodeNameColumn; }
            set { _nodeNameColumn = value; }
        }

        private string _nodeIDColumn;

        public string NodeIDColumn
        {
            get { return _nodeIDColumn; }
            set { _nodeIDColumn = value; }
        }

        private DataTable _treeViewDataSource;

        public DataTable TreeViewDataSource
        {
            get { return _treeViewDataSource; }
            set
            {
                _treeViewDataSource = value;
                LoadRootNodeToTreeView(_treeViewDataSource);
            }
        }

        private string _checkedNodePathes = string.Empty;

        public string CheckedNodePathes
        {
            get { return _checkedNodePathes; }
            set
            {
                _checkedNodePathes = value;
                SetNodeChecked(_checkedNodePathes);
            }
        }
        
        public CSTreeViewComboBox()
        {
            this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.IntegralHeight = true;
            this.Virtualized = false;

            //initialize combobox height
            for (int i = 0; i < 15; i++)
            {
                this.Items.Add(new RadComboBoxItem());
            }

            _treeView = new CSTreeView();
            _treeView.CheckBoxes = true;
            _treeView.PathSeparator = TreeNodePathSeparator;
            _treeView.NodeCheckedChanged += new RadTreeView.TreeViewEventHandler(_treeView_NodeCheckedChanged);
            _treeView.MouseUp += new System.Windows.Forms.MouseEventHandler(_treeView_MouseUp);
            _treeView.Height = 600;

            _h = new RadHostItem(_treeView);
            this.DropDownMinSize = new Size(this.Width, 600);
            this.DropDownStyle = RadDropDownStyle.DropDownList;

            this.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(CSTreeViewComboBox_ToolTipTextNeeded);
        }

        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadComboBox";
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.DropDownMinSize = new Size(this.Width, 600);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
            if (this.DropDownStyle == RadDropDownStyle.DropDownList)
            {
                this.DropDownStyle = RadDropDownStyle.DropDown;
                this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
            }
        }

        protected override void OnDropDownOpening(CancelEventArgs e)
        {
            base.OnDropDownOpening(e);

            if (this.ComboBoxElement.ListBoxElement.Parent != null)
            {
                DockLayoutPanel p = (DockLayoutPanel)this.ComboBoxElement.ListBoxElement.Parent;
                p.Children.Remove(this.ComboBoxElement.ListBoxElement);
                p.Children.Add(_h);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (IsDroppedDown)
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _treeView.NodeCheckedChanged -= _treeView_NodeCheckedChanged;
                _treeView.MouseUp -= _treeView_MouseUp;
                _treeView.Dispose();
                _h.DisposeChildren();
                _h.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnDropDownClosing(RadPopupClosingEventArgs e)
        {
            if ((e.CloseReason == RadPopupCloseReason.Mouse ||
                e.CloseReason == RadPopupCloseReason.CloseCalled) &&
                _h.IsMouseOverElement)
            {
                e.Cancel = true;
            }

            base.OnDropDownClosing(e);
        }

        private void CSTreeViewComboBox_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = !string.IsNullOrWhiteSpace(this.Text) ? this.Text : string.Empty;
        }

        private void LoadRootNodeToTreeView(DataTable dataSourceDT)
        {
            if (dataSourceDT != null && dataSourceDT.Rows.Count > 0)
            {
                _treeView.Nodes.Clear();
                DataRow[] drs = dataSourceDT.Select(string.Format("{0}=''", _parentNodeIDColumn));
                if (drs != null && drs.Length > 0)
                {
                    List<DataRow> drList = drs.ToList<DataRow>();
                    drList = drList.OrderBy(dr => dr[_nodeNameColumn].ToString()).ToList();
                    foreach (DataRow dr in drList)
                    {
                        RadTreeNode child = GenerateRadTreeNode(dr[_nodeIDColumn].ToString(), dr[_nodeNameColumn].ToString());
                        _treeView.Nodes.Add(child);
                        LoadNodeToParentNode(child, dataSourceDT);
                    }
                }
                _treeView.ExpandAll();
            }
        }

        private void LoadNodeToParentNode(RadTreeNode parentNode, DataTable dataSourceDT)
        {
            DataRow[] drs = dataSourceDT.Select(string.Format("{0}='{1}'", _parentNodeIDColumn, parentNode.Name));
            if (drs != null && drs.Length > 0)
            {
                List<DataRow> drList = drs.ToList<DataRow>();
                drList = drList.OrderBy(dr => dr[_nodeNameColumn].ToString()).ToList();
                foreach (DataRow dr in drList)
                {
                    RadTreeNode child = GenerateRadTreeNode(dr[_nodeIDColumn].ToString(), dr[_nodeNameColumn].ToString());
                    parentNode.Nodes.Add(child);
                    LoadNodeToParentNode(child, dataSourceDT);
                }
            }
        }

        private RadTreeNode GenerateRadTreeNode(string name, string text)
        {
            RadTreeNode node = new RadTreeNode();
            node.Name = name;
            node.Text = text;
            return node;
        }

        private void SetNodeChecked(string checkedNodePathes)
        {
            _treeView.NodeCheckedChanged -= _treeView_NodeCheckedChanged;
            ChangeAllNodesCheckStatus(false);
            if (!string.IsNullOrWhiteSpace(checkedNodePathes))
            {
                string[] pathes = checkedNodePathes.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (pathes != null && pathes.Length > 0)
                {
                    foreach (string path in pathes)
                    {
                        RadTreeNode node = _treeView.GetNodeByPath(path, TreeNodePathSeparator);
                        if (node != null)
                        {
                            node.Checked = true;
                        }
                    }
                }
            }
            this.Text = checkedNodePathes;
            _treeView.NodeCheckedChanged += _treeView_NodeCheckedChanged;
        }

        private void ChangeAllNodesCheckStatus(bool isChecked)
        {
            foreach (RadTreeNode node in _treeView.Nodes)
            {
                SubChangeAllNodesCheckStatus(node, isChecked);
            }
        }

        private void SubChangeAllNodesCheckStatus(RadTreeNode parentNode, bool isChecked)
        {
            parentNode.Checked = isChecked;
            foreach (RadTreeNode node in parentNode.Nodes)
            {
                SubChangeAllNodesCheckStatus(node, isChecked);
            }
        }

        private void _treeView_NodeCheckedChanged(object sender, RadTreeViewEventArgs e)
        {
            _checkedNodePathes = string.Empty;
            foreach (RadTreeNode node in _treeView.Nodes)
            {
                SubNodeCheckedChanged(node);
            }
            _checkedNodePathes = _checkedNodePathes.TrimEnd(",".ToCharArray());
            this.Text = _checkedNodePathes;
        }

        private void SubNodeCheckedChanged(RadTreeNode parentNode)
        {
            if (parentNode.Checked)
            {
                _checkedNodePathes += parentNode.FullPath + ",";
            }
            foreach (RadTreeNode child in parentNode.Nodes)
            {
                SubNodeCheckedChanged(child);
            }
        }

        private void _treeView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ChangeAllNodesCheckStatus(string.IsNullOrWhiteSpace(_checkedNodePathes));
            }
        }
    }
}
