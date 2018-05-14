using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.tabControlMain.TabPages.Remove(this.tabPageWeb);
            this.tabControlMain.TabPages.Remove(this.tabPageDatabase);
            this.tabControlMain.TabPages.Remove(this.tabPageInterface);
        }

        private void LoadSetting()
        {
            LoadGeneralSetting();
            LoadEntitySetting();
            LoadHostSetting();
            LoadDatabaseSetting();
            LoadInterfaceSetting();
            LoadWebSetting();
        }
        private bool SaveSetting()
        {
            if (!SaveGeneralSetting()) return false;
            if (!SaveEntitySetting()) return false;
            if (!SaveHostSetting()) return false;
            if (!SaveDatabaseSetting()) return false;
            if (!SaveInterfaceSetting()) return false;
            if (!SaveWebSetting()) return false;

            if (Program.ConfigMgt.Save())
            {
                ScriptGenerator.WriteCreateDBBatFile();
                ScriptGenerator.WriteDropDBBatFile();
                ScriptGenerator.WriteCreateVirtualPathBatFile();
                ScriptGenerator.WriteDropVirtualPathBatFile();
                ScriptGenerator.WriteCreateVirtualPathBatFile_iis6();
                ScriptGenerator.WriteDropVirtualPathBatFile_iis6();
                ScriptGenerator.WriteWebPortalShortCut();
                ScriptGenerator.WriteApplyWebConfigShortCut();
                return true;
            }
            else
            {
                MessageBox.Show(this, "Save solution dir file failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadSetting();
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

        #region general

        private void buttonGenerateID_Click(object sender, EventArgs e)
        {
            GenerateSolutionID();
        }

        #endregion

        #region entity

        private void buttonEntityDelete_Click(object sender, EventArgs e)
        {
            DeleteEntity();
        }

        private void listViewEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEntityButton();
        }

        #endregion

        #region host

        private void buttonHostDelete_Click(object sender, EventArgs e)
        {
            DeleteHost();
        }

        private void listViewHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshHostButton();
        }

        #endregion

        #region interface

        private void buttonInterfaceAdd_Click(object sender, EventArgs e)
        {
            AddInterface();
        }

        private void buttonInterfaceEdit_Click(object sender, EventArgs e)
        {
            EditInterface();
        }

        private void buttonInterfaceDelete_Click(object sender, EventArgs e)
        {
            DeleteInterface();
        }

        private void listViewInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshInterfaceButton();
        }


        #endregion

        #region database

        private void buttonOSQLTest_Click(object sender, EventArgs e)
        {
            TestDBScript();
        }

        #endregion

        #region web (to be obsolated)

        //private void LoadWebSetting()
        //{
        //    if (Program.ConfigMgt.Config.WebSetting.VirtualPathName.Trim().Length < 1)
        //    {
        //        this.textBoxVirtualPath.Text
        //            = Program.ConfigMgt.Config.WebSetting.VirtualPathName
        //            = Program.ConfigMgt.Config.Name;
        //    }
        //    else
        //    {
        //        this.textBoxVirtualPath.Text
        //            = Program.ConfigMgt.Config.WebSetting.VirtualPathName;
        //    }

        //    this.textBoxUserName.Text = Program.ConfigMgt.Config.WebSetting.UserName;
        //    this.textBoxPassword.Text = Program.ConfigMgt.Config.WebSetting.Password;

        //    LoadPageTree();
        //}

        //private bool SaveWebSetting()
        //{
        //    string vPath = Program.ConfigMgt.Config.WebSetting.VirtualPathName = this.textBoxVirtualPath.Text.Trim();
        //    if (vPath.Length < 1)
        //    {
        //        Program.ConfigMgt.Config.WebSetting.VirtualPathName = Program.ConfigMgt.Config.Name;
        //    }

        //    vPath = Program.ConfigMgt.Config.WebSetting.VirtualPathName.Trim().Replace(' ', '_');
        //    Program.ConfigMgt.Config.WebSetting.VirtualPathName = vPath;

        //    Program.ConfigMgt.Config.WebSetting.UserName = this.textBoxUserName.Text.Trim();
        //    Program.ConfigMgt.Config.WebSetting.Password = this.textBoxPassword.Text;

        //    return true;
        //}

        //private void LoadPageTree()
        //{
        //    this.treeViewNode.Nodes.Clear();

        //    SolutionWebSetting cfg = Program.ConfigMgt.Config.WebSetting;
            
        //    // home node

        //    TreeNode hNode = new TreeNode(cfg.HomePage.DisplayCaption + " (" + cfg.HomePage.PageUrl + ")");
        //    hNode.Tag = cfg;

        //    this.treeViewNode.Nodes.Add(hNode);

        //    // diagram node

        //    TreeNode dNode = new TreeNode(cfg.Diagrams.DisplayCaption);
        //    dNode.Tag = cfg.Diagrams;

        //    foreach (CustomizedWebPage p in cfg.Diagrams.Pages)
        //    {
        //        TreeNode pNode = new TreeNode(p.DisplayCaption + " (" + p.PageUrl + ")");
        //        pNode.Tag = p;
        //        dNode.Nodes.Add(pNode);
        //    }

        //    this.treeViewNode.Nodes.Add(dNode);
        //    dNode.Expand();

        //    // entity node

        //    TreeNode eNode = new TreeNode(cfg.Entities.DisplayCaption);
        //    eNode.Tag = cfg.Entities;

        //    foreach (EntityWebPage ep in cfg.Entities.Pages)
        //    {
        //        TreeNode epNode = new TreeNode(ep.DisplayCaption + " (" + ep.PageUrl + ")");
        //        epNode.Tag = ep;

        //        foreach (CustomizedWebPage p in ep.SubPages)
        //        {
        //            TreeNode pNode = new TreeNode(p.DisplayCaption + " (" + p.PageUrl + ")");
        //            pNode.Tag = p;
        //            epNode.Nodes.Add(pNode);
        //        }

        //        eNode.Nodes.Add(epNode);
        //    }

        //    this.treeViewNode.Nodes.Add(eNode);
        //    eNode.Expand();

        //    // wizard node

        //    TreeNode wNode = new TreeNode(cfg.Wizards.DisplayCaption);
        //    wNode.Tag = cfg.Wizards;

        //    foreach (CustomizedWebPage p in cfg.Wizards.Pages)
        //    {
        //        TreeNode pNode = new TreeNode(p.DisplayCaption + " (" + p.PageUrl + ")");
        //        pNode.Tag = p;
        //        wNode.Nodes.Add(pNode);
        //    }

        //    this.treeViewNode.Nodes.Add(wNode);
        //    wNode.Expand();

        //    RefreshTreeButton(null);
        //}

        //private void AddTreeNode()
        //{
        //    TreeNode sNode = this.treeViewNode.SelectedNode;
        //    if (sNode == null) return;

        //    FormWebPage frm = new FormWebPage(null);
        //    if (frm.ShowDialog(this) != DialogResult.OK) return;

        //    CustomizedWebPage page = frm.Page;
        //    if (page == null) return;

        //    EntityWebPage ep = sNode.Tag as EntityWebPage;
        //    if (ep != null)
        //    {
        //        ep.SubPages.Add(page);
        //        LoadPageTree();
        //    }
        //    else
        //    {
        //        CustomizedWebPageCatalog<CustomizedWebPage> cp = sNode.Tag as CustomizedWebPageCatalog<CustomizedWebPage>;
        //        if (cp != null)
        //        {
        //            cp.Pages.Add(page);
        //            LoadPageTree();
        //        }
        //    }
        //}

        //private void EditTreeNode()
        //{
        //    TreeNode sNode = this.treeViewNode.SelectedNode;
        //    if (sNode == null) return;

        //    ICustomizedWebPageCatalog catalog = sNode.Tag as ICustomizedWebPageCatalog;
        //    if (catalog == null)
        //    {
        //        CustomizedWebPage ep = sNode.Tag as CustomizedWebPage;
        //        if (ep == null)
        //        {
        //            SolutionWebSetting sw = sNode.Tag as SolutionWebSetting;
        //            if (sw != null) ep = sw.HomePage;
        //        }

        //        if (ep == null) return;

        //        FormWebPage frm = new FormWebPage(ep);
        //        if (frm.ShowDialog(this) != DialogResult.OK) return;

        //        sNode.Text = ep.DisplayCaption + " (" + ep.PageUrl + ")";
        //    }
        //    else
        //    {
        //        FormWebPageCatalog frm = new FormWebPageCatalog(catalog);
        //        if (frm.ShowDialog(this) != DialogResult.OK) return;

        //        sNode.Text = catalog.DisplayCaption;
        //    }
        //}

        //private void DeleteTreeNode()
        //{
        //    TreeNode sNode = this.treeViewNode.SelectedNode;
        //    if (sNode == null) return;

        //    CustomizedWebPage p = sNode.Tag as CustomizedWebPage;
        //    if (p != null)
        //    {
        //        TreeNode pNode = sNode.Parent;
        //        if (pNode == null) return;

        //        CustomizedWebPage pp = pNode.Tag as CustomizedWebPage;
        //        if (pp != null)
        //        {
        //            if (pp.SubPages.Contains(p))
        //            {
        //                pp.SubPages.Remove(p);
        //                //LoadPageTree();
        //                pNode.Nodes.Remove(sNode);
        //            }
        //        }
        //        else
        //        {
        //            CustomizedWebPageCatalog<CustomizedWebPage> pc = pNode.Tag as CustomizedWebPageCatalog<CustomizedWebPage>;
        //            if (pc != null)
        //            {
        //                if (pc.Pages.Contains(p))
        //                {
        //                    pc.Pages.Remove(p);
        //                    //LoadPageTree();
        //                    pNode.Nodes.Remove(sNode);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void AdvanceEntityNode()
        //{
        //    TreeNode sNode = this.treeViewNode.SelectedNode;
        //    if (sNode == null) return;

        //    EntityWebPage p = sNode.Tag as EntityWebPage;
        //    if (p == null) return;

        //    FormWebEntityConfig frm = new FormWebEntityConfig(p);
        //    frm.ShowDialog(this);
        //}

        //private void RefreshTreeButton(TreeNode n)
        //{
        //    if (n == null)
        //    {
        //        this.buttonNodeAdd.Enabled = this.buttonNodeDelete.Enabled = false;
        //    }
        //    else
        //    {
        //        this.buttonNodeAdd.Enabled = n.Tag is EntityWebPage | n.Tag is CustomizedWebPageCatalog<CustomizedWebPage>;
        //        this.buttonNodeEdit.Enabled = n.Tag is CustomizedWebPage | n.Tag is SolutionWebSetting | n.Tag is ICustomizedWebPageCatalog;
        //        this.buttonNodeDelete.Enabled = n.Tag is CustomizedWebPage && (!(n.Tag is EntityWebPage));
        //        this.buttonNodeAdvance.Visible = n.Tag is EntityWebPage;
        //    }
        //}

        //private void treeViewNode_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    if (e != null) RefreshTreeButton(e.Node);
        //}

        //private void buttonNodeDelete_Click(object sender, EventArgs e)
        //{
        //    DeleteTreeNode();
        //}

        //private void buttonNodeEdit_Click(object sender, EventArgs e)
        //{
        //    EditTreeNode();
        //}

        //private void buttonNodeAdd_Click(object sender, EventArgs e)
        //{
        //    AddTreeNode();
        //}

        //private void buttonNodeAdvance_Click(object sender, EventArgs e)
        //{
        //    AdvanceEntityNode();
        //}

        #endregion   
    }
}