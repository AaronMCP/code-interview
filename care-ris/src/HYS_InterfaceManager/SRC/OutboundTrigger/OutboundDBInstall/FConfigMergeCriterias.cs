using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;

namespace OutboundDBInstall
{
    public partial class FConfigMergeCriterias : Form
    {
        private bool _isModified = false;

        public FConfigMergeCriterias()
        {
            InitializeComponent();           
        }

        public MatchFieldTree CriteriaTree { get; set; }

        private TreeNode InitCriteriaNode(MatchFieldNode criteriaNode)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = criteriaNode.ToString();
            treeNode.Tag = criteriaNode;

            TreeNode fieldNode = null;
            foreach (MatchField field in criteriaNode.MatchFields)
            {
                fieldNode = new TreeNode();
                fieldNode.Text = field.ToString();
                fieldNode.Tag = field;
                treeNode.Nodes.Add(fieldNode);
            }
            foreach (MatchFieldNode node in criteriaNode.ChildNodes)
            {
                treeNode.Nodes.Add(InitCriteriaNode(node));
            }

            return treeNode;
        }

        private void InitCriteriaTree()
        {
            this.treeViewCriteria.Nodes.Clear();
            this.treeViewCriteria.Nodes.Add(InitCriteriaNode(CriteriaTree.Root));            

            this.treeViewCriteria.SelectedNode = this.treeViewCriteria.Nodes[0];
            this.treeViewCriteria.ExpandAll();
        }

        private void FConfigMerginggCriteria_Load(object sender, EventArgs e)
        {
            InitCriteriaTree();
            _isModified = false;
        }

        private bool SaveCriteria()
        {
            MatchFieldTree mfTree = new MatchFieldTree();
            mfTree.Root.JoinOperator = (treeViewCriteria.TopNode.Tag as MatchFieldNode).JoinOperator;

            foreach (TreeNode childNode in treeViewCriteria.TopNode.Nodes)
            {
                SaveMatchFieldNode(mfTree.Root, childNode);
            }

            CriteriaTree = mfTree;
            return true;
        }

        private void SaveMatchFieldNode(MatchFieldNode parentMFNode, TreeNode node)
        {
            MatchField mf = node.Tag as MatchField;
            if (null != mf)
            {
                parentMFNode.MatchFields.Add(mf);
                return;
            }

            MatchFieldNode mfNode = new MatchFieldNode();
            MatchFieldNode mfNodeOld = node.Tag as MatchFieldNode;
            mfNode.JoinOperator = mfNodeOld.JoinOperator;

            foreach (TreeNode childNode in node.Nodes)
            {
                SaveMatchFieldNode(mfNode, childNode);
            }

            parentMFNode.ChildNodes.Add(mfNode);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CancelConfigure();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveCriteria())
            {
                _isModified = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public void CancelConfigure()
        {
            if (!_isModified)
            {
                this.Close();
                return;
            }

            if (MessageBox.Show("Are you sure to give up all changes?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _isModified = false;
                this.Close();
            }
        }

        private void FConfigMergingCriteria_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isModified)
            {
                return;
            }

            if (MessageBox.Show("Are you sure to give up all changes?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
               e.Cancel = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            MatchFieldNode currentNode = GetCurrentFieldNode();
            if (currentNode == null)
            {
                MessageBox.Show("please select the join node which you want to add criterias. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FConfigCriteria frm = new FConfigCriteria();
            frm.SaveCriteria += new SaveCondidtionEventHandler(EditCriteria);

            frm.ShowDialog(this);
        }

        private void EditCriteria(object sender, SaveCondidtionEventArgs e)
        {
            _isModified = true;
            if (e.IsNew)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = e.Criteria.ToString();
                newNode.Tag = e.Criteria;
                this.treeViewCriteria.SelectedNode.Nodes.Add(newNode);
            }
            else
            {
                this.treeViewCriteria.SelectedNode.Text = e.Criteria.ToString();
                this.treeViewCriteria.SelectedNode.Tag = e.Criteria;
            }
        }

        public MatchFieldNode GetCurrentFieldNode()
        {
            if (this.treeViewCriteria.SelectedNode == null)
            {
                return null;
            }

            return this.treeViewCriteria.SelectedNode.Tag as MatchFieldNode;
        }

        private void buttonAddJoinNode_Click(object sender, EventArgs e)
        {
            if (this.treeViewCriteria.SelectedNode != null && this.treeViewCriteria.SelectedNode.Tag is MatchField)
            {
                MessageBox.Show("You can not add join node as child of the criteria node.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FConfigJoinNode frm = new FConfigJoinNode();
            frm.SaveNode += new SaveNodeEventHandler(EditJoinNode);
            frm.ShowDialog(this);
        }

        private void EditJoinNode(object sender, SaveNodeEventArgs e)
        {
            _isModified = true;

            if (e.IsNew)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = e.Node.ToString();
                newNode.Tag = e.Node;
                if (null == this.treeViewCriteria.SelectedNode)
                {
                    this.treeViewCriteria.Nodes.Add(newNode);
                }
                else
                {
                    this.treeViewCriteria.SelectedNode.Nodes.Add(newNode);
                }
            }
            else
            {
                this.treeViewCriteria.SelectedNode.Text = e.Node.ToString();
                this.treeViewCriteria.SelectedNode.Tag = e.Node;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditTreeNode();
        }

        private void EditTreeNode()
        {
            TreeNode node = this.treeViewCriteria.SelectedNode;

            if (null == node)
            {
                MessageBox.Show("please select the node which you want to edit. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MatchField f = node.Tag as MatchField;
            if (f != null)
            {
                FConfigCriteria frm = new FConfigCriteria();
                frm.SaveCriteria += new SaveCondidtionEventHandler(EditCriteria);
                frm.ShowDialog(this,f);

                return;
            }

            FConfigJoinNode frmNode = new FConfigJoinNode();
            frmNode.SaveNode += new SaveNodeEventHandler(EditJoinNode);
            frmNode.ShowDialog(this,node.Tag as MatchFieldNode);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeViewCriteria.SelectedNode;

            if (null == node)
            {
                MessageBox.Show("please select the node which you want to delete. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.treeViewCriteria.SelectedNode.Remove();
        }

        private void treeViewCriteria_DoubleClick(object sender, EventArgs e)
        {
            EditTreeNode();
        }

        internal DialogResult ShowDialog(IWin32Window owner, MatchFieldTree matchFieldTree, bool bWizard)
        {
            if (!bWizard)
            {
                labWizard.Text = "Please configure merging criteria";
            }

            if (null == matchFieldTree)
            {
                CriteriaTree = new MatchFieldTree();
            }
            else
            {
                CriteriaTree = matchFieldTree.Clone();
            }

            return this.ShowDialog(owner);
        }

        private void treeViewCriteria_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.treeViewCriteria.SelectedNode != null)
            {
                this.treeViewCriteria.SelectedNode.BackColor = Color.Transparent;
                this.treeViewCriteria.SelectedNode.ForeColor = Color.Black;
            }
            e.Node.BackColor = Color.Navy;
            e.Node.ForeColor = Color.White;
        }
    }
}
