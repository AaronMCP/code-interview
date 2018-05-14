using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Layouts;

namespace Hys.CommonControls
{
    // Summary:
    //     Specifies the selection behavior of a list box.
    public enum CSSelectionMode
    {

        One = 1,
        //
        // Summary:
        //     Multiple items can be selected.
        MultiSimple = 2,
        //
        // Summary:
        //     Multiple items can be selected, and the user can use the SHIFT, CTRL, and
        //     arrow keys to make selections
        SameLevelOnlyOne = 3,
    }

    public class CSTreeViewComboBoxEx : RadComboBox
    {
        private RadHostItem _h;
        private CSTreeView _treeView;
        private bool _ShowRootCheckBox = true;
        private bool _enableCheckAll = true;
        private CSSelectionMode _selectMode = CSSelectionMode.MultiSimple;
        private List<string> _values = new List<string>();

        private const string TreeNodePathSeparator = "->";



        public bool ShowRootCheckBox
        {
            get { return _ShowRootCheckBox; }
            set { _ShowRootCheckBox = value; }
        }

        public bool EnableCheckAll
        {
            get { return _enableCheckAll; }
            set { _enableCheckAll = value; }
        }

        public CSSelectionMode SelectionMode
        {
            get { return _selectMode; }
            set { _selectMode = value; }
        }

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

        private string _nodeValueColumn;

        public string NodeValueColumn
        {
            get { return _nodeValueColumn; }
            set { _nodeValueColumn = value; }
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

        public List<string> CheckedValues
        {
            get { return _values; }
            set { _values = value; }
        }

        public CSTreeViewComboBoxEx()
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
                        RadTreeNode child = GenerateRadTreeNode(dr[_nodeIDColumn].ToString(), dr[_nodeNameColumn].ToString(), dr[_nodeValueColumn].ToString());

                        _treeView.Nodes.Add(child);

                        child.ShowCheckBox = _ShowRootCheckBox;

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
                    RadTreeNode child = GenerateRadTreeNode(dr[_nodeIDColumn].ToString(), dr[_nodeNameColumn].ToString(), dr[_nodeValueColumn].ToString());
                    parentNode.Nodes.Add(child);
                    LoadNodeToParentNode(child, dataSourceDT);
                }
            }
        }

        private RadTreeNode GenerateRadTreeNode(string name, string text, string value)
        {
            RadTreeNode node = new RadTreeNode();
            node.Name = name;
            node.Text = text;
            node.Tag = value;
            node.ShowCheckBox = false;
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
            this._treeView.NodeCheckedChanged -= new RadTreeView.TreeViewEventHandler(_treeView_NodeCheckedChanged);
            if (e.Node.Checked)
            {
                if (_selectMode == CSSelectionMode.One)
                {
                    UnCheckAll();
                    e.Node.Checked = true;
                }
                else if (_selectMode == CSSelectionMode.SameLevelOnlyOne)
                {
                    UnCheckSameLevelNode(e.Node);
                }
            }

            _values.Clear();
            _checkedNodePathes = string.Empty;
            foreach (RadTreeNode node in _treeView.Nodes)
            {
                SubNodeCheckedChanged(node);
            }
            _checkedNodePathes = _checkedNodePathes.TrimEnd(",".ToCharArray());
            this.Text = _checkedNodePathes;
            this._treeView.NodeCheckedChanged += new RadTreeView.TreeViewEventHandler(_treeView_NodeCheckedChanged);
        }

        private void UnCheckSameLevelNode(RadTreeNode radTreeNode)
        {
            if (radTreeNode.Parent != null)
            {
                foreach (var node in radTreeNode.Parent.Nodes)
                {
                    if (node.Index == radTreeNode.Index)
                        continue;
                    node.Checked = false;
                }
            }
        }

        public void UnCheckAll()
        {
            foreach (var node in _treeView.Nodes)
            {
                node.Checked = false;
                SubUncheck(node);
            }
        }

        private void SubUncheck(RadTreeNode radTreeNode)
        {
            foreach (var node in radTreeNode.Nodes)
            {
                node.Checked = false;
                SubUncheck(node);
            }
        }

        private void SubNodeCheckedChanged(RadTreeNode parentNode)
        {
            string strVlaue = "";
            if (parentNode.Checked)
            {
                _checkedNodePathes += parentNode.FullPath + ",";
                var tmpNode = parentNode;
                do
                {
                    strVlaue = Convert.ToString(tmpNode.Tag) + "," + strVlaue;
                    tmpNode = tmpNode.Parent;

                } while (tmpNode != null);
                _values.Add(strVlaue.TrimEnd(new char[] { ',' }));
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
