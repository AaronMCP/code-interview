using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kodak.GCRIS.Client.FrameWork;
using Kodak.GCRIS.Client.FrameWork.MultiLan;
using Kodak.GCRIS.Common.Utility;
using Kodak.GCRIS.Common.GlobalSettings;
using Kodak.GCRIS.Common.ActionResult;
using System.Text.RegularExpressions;
using Kodak.GCRIS.Client.Oam;
using Kodak.GCRIS.Common.Log;

namespace Kodak.GCRIS.Client.Oam.ACRCode
{
    public partial class ACRCodeDlg : Form
    {
        private ILogManager logger = new LogManager();
        private struct AcrCode
        {
            public string strA;
            public string strP;
            public string strACR;
            AcrCode(string sA, string sP, string sACR)
            {
                strA = sA;
                strP = sP;
                strACR = sACR;
            }
            public void SetValue(AcrCode a)
            {
                strA = a.strA;
                strP = a.strP;
                strACR = a.strACR;
            }
            public bool Compare(AcrCode a)
            {
                if ((strA == a.strA) && (strP == a.strP) && (strACR == a.strACR))
                    return true;
                else
                    return false;
            }

        }

        private string MutilACRCode = null;
        private string MutilAnatomy = null;
        private string MutilPathology = null;
        private string strTotalPcode = null;
        private string strTotalPdec = null;
        private string strTotalAcode = null;
        private string strTotalAdec = null;
        private AcrCode curAcrCode;
        private AcrCode preAcrCode;
        private DataTable myTable = null;
        private int curRowIndex = 0;
        private MultiLanManager myMultiLanManager = ClientFrameworkBuilder.Instance.MultiLanManager;
        public string ACRCode
        {
            get
            {
                return strTotalAcode+'.'+strTotalPcode;
            }
        }
        public string ACodeDesc
        {
            get 
            {
                return strTotalAdec;
            }
        }
        
         public string PCodeDesc
        {
            get 
            {
                return strTotalPdec;
            }
        }
        public ACRCodeDlg(string strInitialCode)
        {
            curAcrCode.strA = "";
            curAcrCode.strP = "";
            curAcrCode.strACR = strInitialCode.Trim();
            InitializeComponent();
        }

        private void treeViewAnatomy_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeViewAnatomyAfterSelect(e.Node);
        }
        private void TreeViewAnatomyAfterSelect(TreeNode Node)
        {
            try
            {
                if (Node.Level == 0 && Node.Nodes.Count == 0)
                {
                    Context context = new Context();
                    context.MessageName = "LoadSubAnatomy";
                    context.Model = null;
                    context.Parameters = string.Format("AID={0}", Node.Name);
                    DataSetActionResult dsResult = ClientFrameworkBuilder.Instance.WebserviceManager.DoCommand(context) as DataSetActionResult;
                    DataTable reTable = dsResult.DataSetData.Tables[0];
                    if (reTable != null)
                    {
                        foreach (DataRow dr in reTable.Rows)
                        {
                            this.treeViewAnatomy.Nodes[Node.Name].Nodes.Add(dr["SID"].ToString(), dr["SID"].ToString() + ":" + dr["Description"].ToString());
                        }
                    }
                    this.treeViewAnatomy.Nodes[Node.Name].Expand();
                }
                //this.radbtnAnatomy.Checked = true;
                string strCurAcode = null;
                string strCurAdec = null;
                strTotalAcode = null;
                strTotalAdec = null;
                this.REditBoxResult.Text = null;
                TreeNode curNode = Node;
                TreeNode pNode = Utility.GetParentNode(curNode);

                if (this.treeViewPathology.Nodes.Count == 0 || pNode.Name != this.treeViewPathology.Name)
                {
                    this.treeViewPathology.Nodes.Clear();
                    this.treeViewPathology.Name = pNode.Name;
                    Context contextP = new Context();
                    contextP.MessageName = "LoadMainPathology";
                    contextP.Model = null;
                    contextP.Parameters = string.Format("AID={0}", Node.Name);
                    DataSetActionResult dsResult = ClientFrameworkBuilder.Instance.WebserviceManager.DoCommand(contextP) as DataSetActionResult;
                    DataTable reTable = dsResult.DataSetData.Tables[0];
                    if (reTable != null)
                    {
                        foreach (DataRow dr in reTable.Rows)
                        {
                            this.treeViewPathology.Nodes.Add(dr["PID"].ToString(), dr["PID"].ToString() + ":" + dr["Description"].ToString());

                        }
                    }

                }
                strCurAcode = curNode.Name;
                string temp = curNode.ToString();
                strCurAdec = temp.Substring(temp.LastIndexOf(':') + 1);

                if (curNode.Level == 0)
                {
                    strTotalAcode = strCurAcode;
                    strTotalAdec = strCurAdec;
                }
                else
                {
                    strTotalAcode = pNode.Name + curNode.Name;
                    string temp1 = pNode.ToString();
                    string temp2 = curNode.ToString();
                    strTotalAdec = temp1.Substring(temp1.LastIndexOf(':') + 1) + "-->" + temp2.Substring(temp2.LastIndexOf(':') + 1);
                }
                this.REditBoxResult.Text = this.MutilACRCode + "=" + strTotalAcode + ".\n" + this.MutilAnatomy + "=" + strTotalAdec + "\n";
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_Client, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
        }

        private void ACRCodeDlg_Load(object sender, EventArgs e)
        {
            try
            {
                lblAnatomy.Text = myMultiLanManager.GetString("ACRCodeDlg.lblAnatomy.Text", (int)ModuleEnum.Oam_Client, "Anatomy");
                lblPothology.Text = myMultiLanManager.GetString("ACRCodeDlg.lblPothology.Text", (int)ModuleEnum.Oam_Client, "Pathology");
                lblACRCode.Text = myMultiLanManager.GetString("ACRCodeDlg.lblACRCode.Text", (int)ModuleEnum.Oam_Client, "ACRCode");
                lblLocation.Text = myMultiLanManager.GetString("ACRCodeDlg.lblLocation.Text", (int)ModuleEnum.Oam_Client, "Location");
                btnSearch.Text = myMultiLanManager.GetString("ACRCodeDlg.btnSearch.Text", (int)ModuleEnum.Oam_Client, "Search Locate");
                btnOK.Text = myMultiLanManager.GetString("ACRCodeDlg.btnOK.Text", (int)ModuleEnum.Oam_Client, "OK");
                btnCancel.Text = myMultiLanManager.GetString("ACRCodeDlg.btnCancel.Text", (int)ModuleEnum.Oam_Client, "Cancel");
                this.Text = myMultiLanManager.GetString("ACRCodeDlg.form.Text", (int)ModuleEnum.Oam_Client, "Locate ACRCode");
                this.MutilACRCode = myMultiLanManager.GetString("ACRCodeDlg.ACRCode", (int)ModuleEnum.Oam_Client, "ACRCode");
                this.MutilAnatomy = myMultiLanManager.GetString("ACRCodeDlg.Anatomy", (int)ModuleEnum.Oam_Client, "Anatomy");
                this.MutilPathology = myMultiLanManager.GetString("ACRCodeDlg.Pathology", (int)ModuleEnum.Oam_Client, "Pathology");

                Context context = new Context();
                context.MessageName = "LoadMainAnatomy";
                context.Model = null;
                context.Parameters = null;
                DataSetActionResult dsResult = ClientFrameworkBuilder.Instance.WebserviceManager.DoCommand(context) as DataSetActionResult;
                DataTable reTable = dsResult.DataSetData.Tables[0];
                if (reTable != null)
                {
                    foreach (DataRow dr in reTable.Rows)
                    {
                        this.treeViewAnatomy.Nodes.Add(dr["AID"].ToString(), dr["AID"].ToString() + ":" + dr["Description"].ToString());

                    }
                }

                this.txtBoxAnatomy.Text = curAcrCode.strA;
                this.txtBoxPathology.Text = curAcrCode.strP;
                this.txtBoxACRCode.Text = curAcrCode.strACR;
                if (curAcrCode.strACR != "")
                {
                    Search();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_Client, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
        }

        private void treeViewPathology_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeViewPathologyAfterSelect(e.Node);
        }
        private void TreeViewPathologyAfterSelect(TreeNode Node)
        {
            try
            {
                if (Node.Level == 0 && Node.Nodes.Count == 0)
                {
                    Context context = new Context();
                    context.MessageName = "LoadSubPathology";
                    context.Model = null;
                    context.Parameters = string.Format("AID={0}&PID={1}", this.treeViewPathology.Name.Trim(), Node.Name.Trim());
                    DataSetActionResult dsResult = ClientFrameworkBuilder.Instance.WebserviceManager.DoCommand(context) as DataSetActionResult;
                    DataTable reTable = dsResult.DataSetData.Tables[0];
                    if (reTable != null)
                    {
                        foreach (DataRow dr in reTable.Rows)
                        {
                            this.treeViewPathology.Nodes[Node.Name].Nodes.Add(dr["SID"].ToString(), dr["SID"].ToString() + ":" + dr["Description"].ToString());
                        }
                    }
                    this.treeViewPathology.Nodes[Node.Name].Expand();
                }
                //this.radbtnPathology.Checked = true;
                string strCurPcode = null;
                string strCurPdec = null;
                strTotalPcode = null;
                strTotalPdec = null;
                this.REditBoxResult.Text = null;
                TreeNode pNode = Utility.GetParentNode(Node);
                strCurPcode = Node.Name;
                string temp = Node.ToString();
                strCurPdec = temp.Substring(temp.LastIndexOf(':') + 1);

                if (Node.Level == 0)
                {
                    strTotalPcode = strCurPcode;
                    strTotalPdec = strCurPdec;
                }
                else
                {
                    strTotalPcode = pNode.Name + Node.Name;
                    string temp1 = pNode.ToString();
                    string temp2 = Node.ToString();
                    strTotalPdec = temp1.Substring(temp1.LastIndexOf(':') + 1) + "-->" + temp2.Substring(temp2.LastIndexOf(':') + 1);
                }
                this.REditBoxResult.Text = this.MutilACRCode + "=" + strTotalAcode + "." + strTotalPcode + "\n" + this.MutilAnatomy + "=" + strTotalAdec + "\n" + this.MutilPathology + "=" + strTotalPdec;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_Client, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
       private void Search()
        {
            try
            {
                string strA = this.txtBoxAnatomy.Text.Trim();
                string strP = this.txtBoxPathology.Text.Trim();
                string strACR = this.txtBoxACRCode.Text.Trim();
                if (!Regex.IsMatch(strACR, @"^[-]?(\d+\.?\d*|\.\d+)$") || (!strACR.Contains(".")) || (strACR.Length < 1))
                {
                    MessageBox.Show("Invalid ACRCode!");
                    return;
                }

                if (RefreshLocationRecordset(strA, strP, strACR))
                {
                    if (myTable == null)
                    {
                        MessageBox.Show("Error on locating!");
                    }
                    if (myTable.Rows.Count == curRowIndex)
                    {
                        curRowIndex = 0;
                    }

                    string strAID, strASID, strPID, strPSID;
                    if (curRowIndex < myTable.Rows.Count)
                    {
                        strAID = myTable.Rows[curRowIndex]["aid"].ToString();
                        strASID = myTable.Rows[curRowIndex]["asid"].ToString();
                        strPID = myTable.Rows[curRowIndex]["pid"].ToString();
                        strPSID = myTable.Rows[curRowIndex]["psid"].ToString();
                        LocateACRCode(strAID, strASID, strPID, strPSID);

                        curRowIndex++;

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_Client, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
        }
   
       private  bool RefreshLocationRecordset(string strA, string strP, string strACR)
        {
            try
            {
                strA.Trim(); strP.Trim(); strACR.Trim();
                if (strA == null && strP == null && strACR == null)
                {
                    return false;
                }
                curAcrCode.strA = strA;
                curAcrCode.strP = strP;
                curAcrCode.strACR = strACR;
                if (curAcrCode.Compare(preAcrCode) && myTable != null)
                {
                    return true;
                }
                else
                {
                    preAcrCode.SetValue(curAcrCode);
                }
                curRowIndex = 0;
                Context context = new Context();
                context.MessageName = "SearchACRCode";
                context.Model = null;
                context.Parameters = string.Format("ADesc={0}&PDesc={1}&ACRCode={2}", strA, strP, strACR);
                DataSetActionResult dsResult = ClientFrameworkBuilder.Instance.WebserviceManager.DoCommand(context) as DataSetActionResult;
                myTable = dsResult.DataSetData.Tables[0];
                return true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_Client, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return false;
            }
        }

        private void LocateACRCode(string strAID, string strASID, string strPID, string strPSID)
        {
            try
            {
                if (strASID != "-1" || strAID != "-1")
                {
                    if (LocateATree(strAID, strASID) != null && (strPSID != "-1" || strPID != "-1"))
                        LocatePTree(strPID, strPSID);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_Client, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }

            
            
           /* if (LocateATree(strAID, strASID) != null)
            {
                LocatePTree(strPID, strPSID);
            }
            */
        }
        private TreeNode LocateATree(string strAID, string strASID)
        {
           
            if (strAID!=null && strAID != "-1")
            {
                TreeNode curNode = this.treeViewAnatomy.Nodes[strAID];
                curNode.EnsureVisible();
                curNode.ForeColor = Color.Red;
                TreeViewAnatomyAfterSelect(curNode);
                if (strASID != null && strASID != "-1")
                {
                    curNode.Expand();
                    curNode = curNode.Nodes[strASID];
                    curNode.EnsureVisible();
                   
                    curNode.ForeColor = Color.Red;
                    treeViewAnatomy.SelectedNode = curNode;
                    TreeViewAnatomyAfterSelect(curNode);
                    return curNode;
                }
                return curNode;
                
            }
            return null;
            
        }
        private void LocatePTree(string strPID, string strPSID)
        {
            if (strPID != null && strPID != "-1")
            {
                TreeNode curNode = this.treeViewPathology.Nodes[strPID];
                curNode.EnsureVisible();
                curNode.ForeColor = Color.Red;
                TreeViewPathologyAfterSelect(curNode);
                if (strPSID != null && strPSID != "-1")
                {
                    curNode.Expand();
                    curNode = curNode.Nodes[strPSID];
                    curNode.EnsureVisible();
                    curNode.ForeColor = Color.Red;
                    TreeViewPathologyAfterSelect(curNode);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}