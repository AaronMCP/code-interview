using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Layouts;

namespace Hys.CommonControls
{
    public class CSTreeView : RadTreeView
    {
        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadTreeView"; }
        }

        public bool HideSelection
        {
            get { return false; }
            set { }
        }

        public bool LabelEdit
        {
            get { return AllowEdit; }
            set { AllowEdit = value; }
        }

        public bool Scrollable
        {
            get { return false; }
            set 
            {
                ;
            }
        }

        public RadTreeNode GetNodeByName(string name)
        {
            return InnerGetNodeByName(this.Nodes, name);
        }

        /// <summary>
        /// Select the node accroding to name.
        /// If name is string.Empty, then by text
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public void SetSelectedNode(string name, string text)
        {
            SetSelectedNode(this.Nodes, name, text);
        }

        /// <summary>
        /// It is a fake event, 
        /// You can only read e.Node.Name, e.Node.Text, e2.Node.Level, e.Node.Tag.
        /// And can only write e.Node.Text.
        /// 
        /// Caution: DO NOT use the e.Node object and its other properties.
        /// </summary>
        public event System.Windows.Forms.TreeViewEventHandler AfterSelect;

        /// <summary>
        /// It is a fake event, 
        /// You can only read e.Node.Name, e.Node.Text, e2.Node.Level, e.Node.Tag, e.Cancel.
        /// And can only write e.Node.Text, e.Cancel.
        /// 
        /// Caution: DO NOT use the e.Node object and its other properties.
        /// </summary>
        public event System.Windows.Forms.TreeViewCancelEventHandler BeforeSelect;

        #region private

        private RadTreeNode InnerGetNodeByName(RadTreeNodeCollection nodes, string name)
        {
            RadTreeNode node = null;
            if (nodes != null && nodes.Count > 0)
            {
                foreach (RadTreeNode nd in nodes)
                {
                    if (nd.Name == name)
                    {
                        node = nd;
                        break;
                    }
                    else if (nd.Nodes.Count > 0)
                    {
                        node = InnerGetNodeByName(nd.Nodes, name);
                        if (node != null)
                            break;
                        else
                            continue;
                    }
                }
            }
            return node;
        }

        private bool SetSelectedNode(RadTreeNodeCollection nodes, string name, string text)
        {
            if (nodes != null && nodes.Count > 0)
            {
                foreach (RadTreeNode nd in nodes)
                {
                    if (name != null && name != string.Empty)
                    {
                        if(nd.Name.ToUpper() == name.ToUpper())
                        {
                            this.SelectedNode = nd;

                            return true;
                        }
                    }
                    else if (text != null && text != string.Empty && nd.Text.ToUpper() == text.ToUpper())
                    {
                        this.SelectedNode = nd;

                        return true;
                    }
                }

                foreach(RadTreeNode nd in nodes)
                {
                    if(SetSelectedNode(nd.Nodes, name, text))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void OnLoad(Size desiredSize)
        {
            this.SelectedNodeChanging += new RadTreeViewCancelEventHandler(CSTreeView_SelectedNodeChanging);
            this.SelectedNodeChanged += new RadTreeViewEventHandler(CSTreeView_SelectedNodeChanged);

            base.OnLoad(desiredSize);
        }

        void CSTreeView_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (AfterSelect == null)
                return;

            TreeViewEventArgs e2 = new TreeViewEventArgs(new TreeNode());

            e2.Node.Name = e.Node.Name;
            e2.Node.Text = e.Node.Text;
            e2.Node.Tag = e.Node.Tag;

            TreeView tv = MakeFakeTreeLevel(e.Node.Level, e2.Node);

            AfterSelect(sender, e2);

            e.Node.Text = e2.Node.Text;

            tv.Dispose();
        }

        void CSTreeView_SelectedNodeChanging(object sender, RadTreeViewCancelEventArgs e)
        {
            if (BeforeSelect == null)
                return;

            TreeViewCancelEventArgs e2 = new TreeViewCancelEventArgs(
                new TreeNode(),
                false,
                System.Windows.Forms.TreeViewAction.Expand);

            e2.Node.Name = e.Node.Name;
            e2.Node.Text = e.Node.Text;
            e2.Node.Tag = e.Node.Tag;

            TreeView tv = MakeFakeTreeLevel(e.Node.Level, e2.Node);

            BeforeSelect(sender, e2);

            e.Cancel = e2.Cancel;
            e.Node.Text = e2.Node.Text;

            tv.Dispose();
        }

        /// <summary>
        /// Because TreeNode.Level is readonly.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private TreeView MakeFakeTreeLevel(int level, TreeNode fakeNode)
        {
            TreeView tree = new TreeView();
            TreeNode tn = null;
            while (level-- > 0)
            {
                if (tn == null)
                {
                    tn = new TreeNode();
                    tree.Nodes.Add(tn);
                }
                else
                {
                    //new TreeNode()
                    tn = tn.Nodes.Add("");
                }
            }

            if (tn == null)
            {
                tree.Nodes.Add(fakeNode);
            }
            else
            {
                tn.Nodes.Add(fakeNode);
            }

            return tree;
        }

        #endregion

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }

    public class CSTreeNode : RadTreeNode
    {
        public CSTreeNode GetNodeByName(string name)
        {
            return InnerGetNodeByName(this.Nodes, name);
        }

        private CSTreeNode InnerGetNodeByName(RadTreeNodeCollection nodes, string name)
        {
            CSTreeNode node = null;
            if (nodes != null && nodes.Count > 0)
            {
                foreach (CSTreeNode nd in nodes)
                {
                    if (nd.Name == name)
                    {
                        node = nd;
                        break;
                    }
                    else if (nd.Nodes.Count > 0)
                    {
                        node = InnerGetNodeByName(nd.Nodes, name);
                        break;
                    }
                }
            }
            return node;
        }
    }
}
