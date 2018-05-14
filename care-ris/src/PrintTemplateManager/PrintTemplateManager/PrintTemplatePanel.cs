using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Diagnostics;
using C1.C1Report;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Management;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Hys.PrintTemplateManager
{
    public partial class PrintTemplatePanel : Form
    {
        private DataTable _dtTemplateType = null;
        private DataTable _dtPrintTemplate = new DataTable();
        private bool IsPrint = false;
        private RadTreeNode tempTreeNode = null;
        private log4net.ILog logger = Context.Logger;
        public PrintTemplatePanel()
        {
            this.Font = new Font("Microsoft Sans Serif", 9.75F);
            InitializeComponent();
        }

        private void PanelLoad()
        {
            Utility.loadPrintTemplates(treeViewPrintTemplate, -1, out _dtTemplateType, ref _dtPrintTemplate, true);
        }
        private string lang(string key)
        {
            return Utility.Lang(key);
        }
        private void PrintTemplatePanel_Load(object sender, EventArgs e)
        {
            SRPrintTab.Visibility = ElementVisibility.Hidden;

            toolTip1.Active = false;

            btnDelete.Text = lang(btnDelete.Text);
            btnModify.Text = lang("Modify");
            btnNewTemplate.Text = lang("New");
            btnRename.Text = lang("Rename");
            btnSetDefault.Text = lang("SetDefault");
            btnSetTypeDefault.Text = lang("SetTypeDefault");
            btnShow.Text = lang("Show");
            btnZoomIn.Text = lang("ZoomIn");
            btnZoomOut.Text = lang("ZoomOut");
            lblAllDefault.Text = lang("AllDefault");
            lblModalityTypeDefault.Text = lang("ModalityTypeDefault");
            lblTypeDefault.Text = lang("TypeDefault");
            renameToolStripMenuItem.Text = btnRename.Text;
            deleteToolStripMenuItem.Text = btnDelete.Text;
            showToolStripMenuItem.Text = btnShow.Text;
            setDefaultToolStripMenuItem1.Text = btnSetDefault.Text;
            setTypeDefaultToolStripMenuItem.Text = btnSetTypeDefault.Text;
            modifyToolStripMenuItem.Text = btnModify.Text;

            this.tabControl1.SelectedIndex = 0;
            PanelLoad();
            ExportPanel_Load();

            LoadPatientType();
        }

        private string GetUserPrintTemplateDir()
        {
            var currDir = Directory.GetCurrentDirectory();
            var dir = new DirectoryInfo(currDir);
            var path = Path.Combine(currDir, "template");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path + "\\";
        }

        private bool IsExistPrinter()
        {
            if (PrinterSettings.InstalledPrinters.Count < 1)
            {
                return false;
            }
            return true;
        }

        private void GenerateBaseTemplateFile(string typeDesc)
        {
            var lcid = CultureInfo.CurrentCulture.LCID.ToString();
            string initialTemplateFileName = string.Format(
                @"{0}\PrintTemplateFiles\SystemFiles\{1}\{2}_Template_Print.xml",
                Application.StartupPath, lcid, typeDesc);
            string baseTemplateFileName = string.Format(
                @"{0}\PrintTemplateFiles\SystemFiles\{1}\Basic_Template_Print.xml",
                Application.StartupPath, lcid);
            if (!File.Exists(initialTemplateFileName))
            {
                if (File.Exists(baseTemplateFileName))
                {
                    File.Copy(baseTemplateFileName, initialTemplateFileName, true);
                }
            }
            File.SetAttributes(initialTemplateFileName, FileAttributes.Normal);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(initialTemplateFileName);
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("ConnectionString");
            if (xmlNodeList.Count == 1)
            {
                xmlNodeList.Item(0).InnerText = string.Format(
                    @"{0}\PrintTemplateFiles\SystemFiles\{1}\{2}_Field.xml",
                    Application.StartupPath, lcid, typeDesc);
            }

            xmlNodeList = xmlDocument.GetElementsByTagName("RecordSource");
            if (xmlNodeList.Count == 1)
            {
                xmlNodeList.Item(0).InnerText = @"Fields";
            }
            xmlDocument.Save(initialTemplateFileName);
        }

        private string GetTemplateType(string value)
        {
            if (_dtTemplateType == null)
                return "";
            DataRow[] foundRow;
            foundRow = _dtTemplateType.Select(string.Format("Value='{0}'", value));
            if (foundRow.Length > 0)
            {
                return foundRow[0]["Text"].ToString();
            }
            else return "";
        }

        private int GetTemplateCount(string value)
        {
            if (_dtTemplateType == null)
                return 0;
            DataRow[] foundRow;
            foundRow = _dtTemplateType.Select(string.Format("Value='{0}'", value));
            if (foundRow.Length > 0)
            {
                return Convert.ToInt32(foundRow[0]["IsDefault"].ToString());
            }
            else return 0;
        }
        private void btnNewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                RadTreeNode selectNode = treeViewPrintTemplate.SelectedNode;
                RadTreeNode typeNode = null;
                string strTemplateInfo = null;
                if (selectNode == null)
                {
                    MessageBox.Show("Please select a node!");
                    return;
                }

                if (selectNode.Level == 0 || (selectNode.Level == 1 && selectNode.Nodes.Count > 0))
                {
                    MessageBox.Show("Can't add new template to root node!");
                    return;
                }

                if (_dtTemplateType == null)
                    return;
                string strTemplateGuid = Guid.NewGuid().ToString();
                int numType = 1;
                string strType = numType.ToString();
                string strTypeDesc = "";
                string strModalityType = "";

                if (selectNode.Level == 1)
                {
                    strType = selectNode.Name;
                    //strTypeDesc = selectNode.Text.Trim();
                    strTypeDesc = GetTemplateType(selectNode.Name);
                    strModalityType = "";
                    typeNode = selectNode;
                }

                if (selectNode.Level == 2)
                {
                    if (selectNode.Nodes.Count < 1 && (selectNode.Tag == null || selectNode.Tag.ToString() != "TypeNode"))
                    {
                        strType = selectNode.Parent.Name;

                        strTypeDesc = GetTemplateType(selectNode.Parent.Name);
                        strModalityType = "";
                        typeNode = selectNode.Parent;
                        int count = GetTemplateCount(selectNode.Parent.Name);
                        if (count != 0 && count <= selectNode.Parent.Nodes.Count + 1)
                        {
                            MessageBox.Show("This template must be one!");
                            return;
                        }
                    }
                    else
                    {
                        strType = selectNode.Parent.Name;
                        strTypeDesc = GetTemplateType(selectNode.Parent.Name);
                        strModalityType = selectNode.Text;
                        typeNode = selectNode.Parent;
                    }
                }
                if (selectNode.Level == 3)
                {
                    strType = selectNode.Parent.Parent.Name;
                    strTypeDesc = GetTemplateType(selectNode.Parent.Parent.Name);
                    strModalityType = selectNode.Parent.Name;
                    typeNode = selectNode.Parent.Parent;
                }
                string strTemplateName = "";

                if (selectNode.Tag.ToString() == "TypeNode" || (selectNode.Parent != null && selectNode.Parent.Tag.ToString() == "TypeNode"))
                {
                    SetNodeNameForm frm = new SetNodeNameForm();
                    if (DialogResult.OK == frm.ShowDialog())
                    {
                        strTemplateName = frm.TemplateName;
                    }
                    else
                    {
                        return;
                    }

                }


                if (strTemplateName == null || strTemplateName == "")
                {
                    MessageBox.Show("The template must have a name!");
                    return;
                }

                string printTemplateFileDir = GetUserPrintTemplateDir();
                var lcid = CultureInfo.CurrentCulture.LCID.ToString();

                string strTemplateFieldFileName = string.Format(
                    @"{0}\PrintTemplateFiles\SystemFiles\{1}\{2}_Field.xml",
                    Application.StartupPath, lcid, strTypeDesc);

                string strInitialTemplateFileName = string.Format(
                    @"{0}\PrintTemplateFiles\SystemFiles\{1}\{2}_Template_Print.xml",
                    Application.StartupPath, lcid, strTypeDesc);

                GenerateBaseTemplateFile(strTypeDesc);
                string strNewTemplateFileName = string.Format(
                    "{0}{1}_{2}_Template_0.xml", printTemplateFileDir, strTypeDesc, strTemplateGuid);

                DataSet myDataSet = Utility.LoadPrintTemplateField(numType);
                XmlTextWriter xw = new XmlTextWriter(strTemplateFieldFileName, Encoding.Unicode);
                xw.WriteStartElement("Fields");

                DataRow[] drs = myDataSet.Tables[0].Select(string.Format("SubType='{0}'or SubType=''", strModalityType));
                foreach (DataRow myRow in drs)
                {
                    xw.WriteElementString(Convert.ToString(myRow["FieldName"]), Convert.ToString(myRow["FieldName"]));
                }
                xw.WriteEndElement();
                xw.Close();
                FileInfo newTemplateFileInfo = new FileInfo(strInitialTemplateFileName);
                newTemplateFileInfo.CopyTo(strNewTemplateFileName, true);
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = Application.StartupPath + @"\C1ReportDesigner.exe";

                procInfo.Arguments = "\"" + printTemplateFileDir + strTypeDesc + "_" + strTemplateGuid + "_Template_0.xml" + "\"";
                Process proc = Process.Start(procInfo);
                proc.WaitForExit();
                proc.Dispose();

                DialogResult dr = MessageBox.Show("Do you want to submit this print template?", "Submit Template",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.No)
                {
                    if (File.Exists(strNewTemplateFileName))
                    {
                        FileInfo deleteFileInfo = new FileInfo(strNewTemplateFileName);
                        deleteFileInfo.Delete();
                    }
                    return;
                }

                StreamReader fs = new StreamReader(strNewTemplateFileName);
                strTemplateInfo = fs.ReadToEnd();

                string strIsDefaultByType = "0";
                string strIsDefaultByModality = "0";
                var tNameExists = Utility.TemplateNameExists(strTemplateName, int.Parse(typeNode.Name), Utility.getRootNodeName(selectNode));

                if (tNameExists)
                {
                    MessageBox.Show("The template name already exists!");
                    return;
                }

                var addResult = Utility.AddPrintTemplate(strTemplateGuid, int.Parse(strType), strTemplateName, strTemplateInfo, int.Parse(strIsDefaultByType), strModalityType, int.Parse(strIsDefaultByModality), Utility.getRootNodeName(selectNode));

                if (addResult)
                {
                    RadTreeNode tempNode = new RadTreeNode();
                    tempNode.Name = strTemplateGuid;
                    tempNode.Text = strTemplateName;
                    tempNode.Tag = "LeafFile";

                    if (strModalityType == "")
                    {
                        typeNode.Nodes.Add(tempNode);

                    }
                    else
                    {
                        typeNode.Nodes[strModalityType].Nodes.Add(tempNode);
                    }
                    treeViewPrintTemplate.SelectedNode = tempNode;
                    tempNode.EnsureVisible();
                    ShowPrintTemplate(strNewTemplateFileName, "Template", typeNode.Text + "_" + treeViewPrintTemplate.SelectedNode.Name);


                }
                else
                {
                    if (File.Exists(strNewTemplateFileName))
                    {
                        FileInfo deleteTemplateFileInfo = new FileInfo(strNewTemplateFileName);
                        deleteTemplateFileInfo.Delete();
                    }
                    MessageBox.Show("Add new template failure!");
                }
                fs.Dispose();
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        #region EK_HI00091152
        private void CopyTreeNode()
        {
            tempTreeNode = null;
            if (treeViewPrintTemplate.SelectedNode != null)
            {
                tempTreeNode = (RadTreeNode)treeViewPrintTemplate.SelectedNode.Clone();
                tempTreeNode.ForeColor = Color.Black;
            }
        }

        private void PasteTreeNode()
        {
            RadTreeNode newTreeNode = (RadTreeNode)tempTreeNode.Clone();
            if (PasteTreeNode2DB(newTreeNode))
            {
                PasteTreeNode2UI(newTreeNode);
                ShowPrintTemplate();
            }

        }


        private void PasteTreeNode2UI(RadTreeNode newTreeNode)
        {
            if (treeViewPrintTemplate.SelectedNode != null)
            {
                treeViewPrintTemplate.SelectedNode.Nodes.Add(newTreeNode);
                treeViewPrintTemplate.SelectedNode = newTreeNode;
                newTreeNode.EnsureVisible();
            }

        }

        private bool PasteTreeNode2DB(RadTreeNode newTreeNode)
        {
            try
            {
                string tempInfo = "";
                bool templateInfoGot = GetTemplateFromDB(newTreeNode, treeViewPrintTemplate.SelectedNode.Parent.Text, ref tempInfo);

                if (!templateInfoGot)
                {
                    return false;
                }


                string tempGuid = Guid.NewGuid().ToString();
                string tempName = GetDiffTemplateName(treeViewPrintTemplate.SelectedNode.Parent.Name, newTreeNode.Text, Utility.getRootNodeName(treeViewPrintTemplate.SelectedNode));
                string tempModalityType = treeViewPrintTemplate.SelectedNode.Text;
                string tempType = treeViewPrintTemplate.SelectedNode.Parent.Name;
                string IsDefaultByType = "0";
                string IsDefaultByModality = "0";

                var result = Utility.AddPrintTemplate(tempGuid, int.Parse(tempType), tempName, tempInfo, 0, tempModalityType, 0, Utility.getRootNodeName(treeViewPrintTemplate.SelectedNode));
                if (result)
                {
                    newTreeNode.Name = tempGuid;
                    newTreeNode.Text = tempName;
                    newTreeNode.Tag = "LeafFile";
                }
                return result;

            }
            catch (Exception err)
            {
                logger.Error(err);
            }
            return false;

        }

        private string GetDiffTemplateName(string type, string newTemplateName, string site)
        {
            var result = Utility.TemplateNameExists(newTemplateName, int.Parse(type), site);
            if (result)
            {
                return GetDiffTemplateName(type, newTemplateName + "_1", site);
            }
            else
            {
                return newTemplateName;
            }
        }

        private bool GetTemplateFromDB(RadTreeNode previousNode, string typeDesc, ref string strTemplateInfo)
        {
            try
            {
                var strPrintTemplateFileDir = GetUserPrintTemplateDir();
                if (strPrintTemplateFileDir == "")
                    return false;

                var strLatestVersion = Utility.GetLatestVersion(previousNode.Name).ToString();
                var strTypeDesc = typeDesc;
                var strTemplateFileName = strPrintTemplateFileDir + strTypeDesc + "_" + previousNode.Name + "_Template_" + strLatestVersion + ".xml";
                FileInfo latestFileInfo = new FileInfo(strTemplateFileName);
                if (!latestFileInfo.Exists)
                {

                    strTemplateInfo = Utility.LoadPrintTemplateInfo(previousNode.Name);
                    using (StreamWriter fs = new StreamWriter(strTemplateFileName, false))
                    {
                        fs.Write(strTemplateInfo);
                    }
                }
                else
                {
                    using (StreamReader fs = new StreamReader(strTemplateFileName))
                    {
                        strTemplateInfo = fs.ReadToEnd();
                    }
                }


            }
            catch (Exception err)
            {
                logger.Error(err);
            }
            return true;
        }

        private void setRightMenuAction(RadTreeNode selectedNode)
        {
            int selectedLevel = selectedNode.Level;

            switch (selectedLevel)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    if (!selectedNode.Tag.ToString().Equals("LeafFile"))
                    {
                        setRightMenuItemAction(true);
                        treeViewPrintTemplate.SelectedNode = selectedNode;
                        contextMenuPrint.Show(MousePosition);

                        return;
                    }
                    break;
                case 3:
                    setRightMenuItemAction(false);
                    treeViewPrintTemplate.SelectedNode = selectedNode;
                    contextMenuPrint.Show(MousePosition);
                    return;
                default://not show
                    setRightMenuItemAction(false);
                    break;
            }

            if (selectedNode.Tag.ToString().Equals("LeafFile"))
            {
                setRightMenuItemAction(false);
                treeViewPrintTemplate.SelectedNode = selectedNode;
                contextMenuPrint.Show(MousePosition);
            }
        }

        private void setRightMenuItemAction(bool PasteEnable)
        {
            contextMenuPrint.Items["renameToolStripMenuItem"].Enabled = !PasteEnable && btnRename.Enabled;
            contextMenuPrint.Items["modifyToolStripMenuItem"].Enabled = !PasteEnable && btnModify.Enabled;
            contextMenuPrint.Items["showToolStripMenuItem"].Enabled = !PasteEnable;
            contextMenuPrint.Items["deleteToolStripMenuItem"].Enabled = !PasteEnable && btnDelete.Enabled;
            contextMenuPrint.Items["setDefaultToolStripMenuItem1"].Enabled = !PasteEnable && btnSetDefault.Enabled;
            contextMenuPrint.Items["setTypeDefaultToolStripMenuItem"].Enabled = !PasteEnable && btnSetTypeDefault.Enabled;
            contextMenuPrint.Items["copyToolStripMenuItem"].Enabled = !PasteEnable;
            if (tempTreeNode != null)
            {
                contextMenuPrint.Items["pasteToolStripMenuItem"].Enabled = PasteEnable && btnModify.Enabled;
            }
            else
            {
                contextMenuPrint.Items["pasteToolStripMenuItem"].Enabled = false;
            }
        }
        #endregion


        private void ShowWaitingDlg()
        {
            this.busyBar.Show();
        }
        private void CloseWaitingDlg()
        {
            this.busyBar.Hide();
        }
        /// <summary>
        /// Function:Show the print template
        /// </summary>
        /// <param name="strTemplateFileName">The print template file to show</param>
        /// <param name="strReportName">C1Report needed. Now is "Template"</param>
        /// <param name="tag">The print template tag to indentify the print preview control</param>
        private void ShowPrintTemplate(string strTemplateFileName, string strReportName, string tag)
        {

            try
            {
                if (!IsExistPrinter())
                {
                    MessageBox.Show("No printer, please install a printer!");
                    return;
                }

                btnShow.Enabled = false;
                showToolStripMenuItem.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                c1Report.Document.EndPrint += new PrintEventHandler(EndPrint);
                c1Report.Load(strTemplateFileName, strReportName);
                DataTable dt = new DataTable(c1Report.ReportName);
                foreach (Field fld in c1Report.Fields)
                {
                    if (fld.Text != null && fld.Calculated)
                    {
                        if (!dt.Columns.Contains(fld.Text))
                            dt.Columns.Add(fld.Text, System.Type.GetType("System.String"));
                    }
                    else if (fld.Picture != null)
                    {
                        C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;

                        if (ph != null && ph.IsBound && !dt.Columns.Contains(ph.FieldName))
                            dt.Columns.Add(ph.FieldName, System.Type.GetType("System.Byte[]"));
                    }
                }
                DataRow drow = dt.NewRow();
                foreach (DataColumn dataFld in dt.Columns)
                {
                    drow[dataFld] = null;
                }
                dt.Rows.Add(drow);

                c1Report.DataSource.Recordset = dt.DefaultView;
                #region check printer status
                PrinterStatus pStatus = GetPrinterStatus(c1Report.Document.PrinterSettings.PrinterName);
                if (pStatus != PrinterStatus.Idle)//only idle can print
                {
                    MessageBox.Show("The printer does not response");
                    dt.Dispose();
                    this.Cursor = Cursors.Arrow;
                    return;
                }
                #endregion
                ResetPrintPrevewControl();
                printPreviewControl.AutoZoom = true;

                printPreviewControl.Document = c1Report.Document;
                printPreviewControl.Tag = tag;
                //printPreviewControl.Document.Print();
                printPreviewControl.Zoom += 0.4;
                comboBoxZoom.Text = String.Format("{0:P}", printPreviewControl.Zoom);
                dt.Dispose();
                IsPrint = true;

            }
            catch (Exception err)
            {
                logger.Error(err);
            }

        }
        /// <summary>
        /// Get Printer Status
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns>PrinterStatus of the specified printer</returns>
        public PrinterStatus GetPrinterStatus(string printerName)
        {
            PrinterStatus pStatus;
            pStatus = PrinterStatus.Unknown;
            if (string.IsNullOrEmpty(printerName))
            {
                return PrinterStatus.Unknown;
            }
            try
            {
                ManagementObjectCollection printers = new ManagementClass("Win32_Printer").GetInstances();
                foreach (ManagementObject printer in printers)
                {
                    if (printer["Name"].ToString() != printerName)
                    {
                        continue;
                    }
                    switch (printer["PrinterStatus"].ToString())
                    {
                        case "1": pStatus = PrinterStatus.Other; break;
                        case "2": pStatus = PrinterStatus.Unknown; break;
                        case "3": pStatus = PrinterStatus.Idle; break;
                        case "4": pStatus = PrinterStatus.Printing; break;
                        case "5": pStatus = PrinterStatus.Warmup; break;
                        case "6": pStatus = PrinterStatus.Stopped; break;
                        case "7": pStatus = PrinterStatus.Offline; break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return PrinterStatus.Unknown;
            }
            return pStatus;
        }
        /// <summary>
        /// Funtion:Forbiden the show action 
        /// Becasue the print preview control or C1Report will pop up a exception if show it qucliy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginPrint(Object sender, PrintEventArgs e)
        {
            IsPrint = true;
            btnShow.Enabled = false;
            showToolStripMenuItem.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }
        /// <summary>
        /// Function:Enable the show action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndPrint(Object sender, PrintEventArgs e)
        {
            IsPrint = false;
            btnShow.Enabled = true;
            showToolStripMenuItem.Enabled = true;
            this.Cursor = Cursors.Default;

        }
        /// <summary>
        /// Name:DeleteTemplate
        /// Funtion:Delete a template
        /// </summary>
        private void DeleteTemplate()
        {
            try
            {
                string strPrintTemplateFileDir = GetUserPrintTemplateDir();
                if (strPrintTemplateFileDir == "")
                    return;
                RadTreeNode selectNode = treeViewPrintTemplate.SelectedNode;
                if (selectNode.Level == 0 || selectNode.Level == 1 || (selectNode.Level == 2 && selectNode.Nodes.Count > 0) || !Utility.isLeafFileNode(selectNode))
                {
                    MessageBox.Show("Can't delete system node!");
                    return;
                }

                if (Utility.isGlobalNode(selectNode) && selectNode.Level == 2 && selectNode.Parent.Nodes.Count == 1)
                {
                    MessageBox.Show("This type of template is unique, can not be delete!");
                    return;
                }
                if (DialogResult.No == MessageBox.Show("Do you want to delete this template?", "Notify", MessageBoxButtons.YesNo))
                {
                    return;
                }

                var result = Utility.DeletePrintTemplate(selectNode.Name);
                if (result)
                {
                    DirectoryInfo direcInfo = new DirectoryInfo(strPrintTemplateFileDir);
                    string strTypeDesc;

                    if (selectNode.Level == 3)
                    {
                        strTypeDesc = selectNode.Parent.Parent.Text;
                    }
                    else if (selectNode.Level == 2)
                    {
                        strTypeDesc = selectNode.Parent.Text;
                    }
                    else
                    {
                        return;
                    }
                    FileInfo[] deleteFileInfo = direcInfo.GetFiles(strTypeDesc + "_" + selectNode.Name + "_Template_*.xml", SearchOption.TopDirectoryOnly);
                    if (deleteFileInfo != null && deleteFileInfo.Length != 0)
                    {
                        foreach (FileInfo fi in deleteFileInfo)
                        {
                            fi.Delete();
                        }
                    }
                    if (Convert.ToString(printPreviewControl.Tag) == strTypeDesc + "_" + selectNode.Name)
                    {
                        ResetPrintPrevewControl();
                        comboBoxZoom.Text = "";
                    }

                    selectNode.Remove();
                }
                else
                {
                    MessageBox.Show("Delete the template failure!");
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteTemplate();
        }

        private void treeViewPrintTemplate_NodeMouseUp(object sender, Telerik.WinControls.UI.RadTreeViewMouseEventArgs e)
        {
            try
            {
                if (e.OriginalEventArgs.Button == MouseButtons.Right)
                {
                    setRightMenuAction(e.Node);
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void treeViewPrintTemplate_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (e.Node == null)
                return;

            //if (isTemplateNode(e.Node))
            if (3 == e.Node.Level)
            {
                _chkcbxPatientType.Enabled = true;

                LoadCurrentNodePatientType();
            }
            else
            {
                _chkcbxPatientType.Enabled = false;
                _chkcbxPatientType.CheckedNames = string.Empty;
            }

            if (e.Node.Level == 3 && e.Node.Parent.Parent.Text.Equals("¸´Õï±¨¸æ"))
            {
                _chkcbxPatientType.Enabled = false;
                _chkcbxPatientType.CheckedNames = string.Empty;
            }
        }

        private void SetDefault(bool isDefaultByModality)
        {
            try
            {
                if (treeViewPrintTemplate.SelectedNode != null)
                {
                    if (treeViewPrintTemplate.SelectedNode.Level == 0 || treeViewPrintTemplate.SelectedNode.Tag.ToString() != "LeafFile")
                    {
                        MessageBox.Show("Can't set default!");
                        return;
                    }
                    var type = treeViewPrintTemplate.SelectedNode.Parent.Parent.Name.Trim();
                    var templateGuid = treeViewPrintTemplate.SelectedNode.Name.Trim();
                    var modalityType = isDefaultByModality ? treeViewPrintTemplate.SelectedNode.Parent.Name.Trim() : "";
                    var site = Utility.getRootNodeName(treeViewPrintTemplate.SelectedNode);
                    var res = Utility.SetDefault(int.Parse(type), modalityType, templateGuid, site);

                    if (res)
                    {
                        if (isDefaultByModality)
                        {
                            foreach (RadTreeNode subNode in treeViewPrintTemplate.SelectedNode.Parent.Nodes)
                            {
                                if (subNode.ForeColor == Utility.AllDefaultColor)
                                {
                                    subNode.ForeColor = Utility.TypeDefaultColor;
                                    break;
                                }
                                else if (subNode.ForeColor == Utility.ModalityDefaultColor)
                                {
                                    subNode.ForeColor = Color.Black;
                                    break;
                                }


                            }
                            if (treeViewPrintTemplate.SelectedNode.ForeColor == Utility.TypeDefaultColor)
                            {
                                treeViewPrintTemplate.SelectedNode.ForeColor = Utility.AllDefaultColor;
                            }
                            else
                            {

                                treeViewPrintTemplate.SelectedNode.ForeColor = Utility.ModalityDefaultColor;
                            }

                        }
                        else
                        {
                            if (treeViewPrintTemplate.SelectedNode.Level > 2)
                            {
                                foreach (RadTreeNode tempNode in treeViewPrintTemplate.SelectedNode.Parent.Parent.Nodes)
                                    foreach (RadTreeNode subNode in tempNode.Nodes)
                                    {
                                        if (subNode.ForeColor == Utility.AllDefaultColor)
                                        {
                                            subNode.ForeColor = Utility.ModalityDefaultColor;
                                            break;
                                        }
                                        else if (subNode.ForeColor == Utility.TypeDefaultColor)
                                        {
                                            subNode.ForeColor = Color.Black;
                                            break;
                                        }
                                    }
                            }
                            else
                            {
                                foreach (RadTreeNode subNode in treeViewPrintTemplate.SelectedNode.Parent.Nodes)
                                {
                                    if (subNode.ForeColor == Utility.AllDefaultColor)
                                    {
                                        subNode.ForeColor = Utility.ModalityDefaultColor;
                                        break;
                                    }
                                    else if (subNode.ForeColor == Utility.TypeDefaultColor)
                                    {
                                        subNode.ForeColor = Color.Black;
                                        break;
                                    }
                                }
                            }

                            if (treeViewPrintTemplate.SelectedNode.ForeColor == Utility.ModalityDefaultColor)
                            {
                                treeViewPrintTemplate.SelectedNode.ForeColor = Utility.AllDefaultColor;
                            }
                            else
                            {
                                treeViewPrintTemplate.SelectedNode.ForeColor = Utility.TypeDefaultColor;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Set default failure!");
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }
        private void setDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDefault(true);
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTemplate();
        }
        private void ResetPrintPrevewControl()
        {
            Point oldLocation = printPreviewControl.Location;
            Size oldSize = printPreviewControl.Size;
            String oldName = printPreviewControl.Name;
            int oldTabIndex = printPreviewControl.TabIndex;

            printPreviewControl.Dispose();

            printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.printPreviewControl.Location = oldLocation;
            this.printPreviewControl.Name = oldName;
            this.printPreviewControl.Size = oldSize;
            this.printPreviewControl.TabIndex = oldTabIndex;

            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Controls.Add(this.printPreviewControl);
        }
        private void ModifyTemplate()
        {
            try
            {
                string strPrintTemplateFileDir = GetUserPrintTemplateDir();
                if (strPrintTemplateFileDir == "")
                    return;
                RadTreeNode selectNode = treeViewPrintTemplate.SelectedNode;

                if (selectNode.Level == 0 || selectNode.Level == 1 || selectNode.Tag.ToString() != "LeafFile")
                {
                    MessageBox.Show("Can't modify system node!");
                    return;
                }

                var res = Utility.GetLatestVersion(selectNode.Name);
                string strLatestVersion = res.ToString();
                string strTypeDesc;
                string strType;
                string strModalityType;

                if (selectNode.Level == 2)
                {
                    strTypeDesc = GetTemplateType(selectNode.Parent.Name);
                    strType = selectNode.Parent.Name;
                    strModalityType = selectNode.Parent.Text;
                }
                else
                {
                    strTypeDesc = GetTemplateType(selectNode.Parent.Parent.Name);
                    strType = selectNode.Parent.Parent.Name;
                    strModalityType = selectNode.Parent.Text;
                }
                string strTemplateFileName = strPrintTemplateFileDir + strTypeDesc + "_" + selectNode.Name + "_Template_" + strLatestVersion + ".xml";

                FileInfo latestFileInfo = new FileInfo(strTemplateFileName);
                if (!latestFileInfo.Exists)
                {
                    var tplInfo = Utility.LoadPrintTemplateInfo(selectNode.Name);
                    string strNewTemplateInfo = tplInfo;

                    StreamWriter fs = new StreamWriter(strTemplateFileName, false);
                    fs.Write(strNewTemplateInfo);
                    fs.Close();
                }
                var lcid = CultureInfo.CurrentCulture.LCID.ToString();
                string strTemplateFieldFileName = string.Format(
                        @"{0}\PrintTemplateFiles\SystemFiles\{1}\{2}_Field.xml",
                        Application.StartupPath, lcid, strTypeDesc);

                DataSet myDataSetTemp = Utility.LoadPrintTemplateField(int.Parse(strType));

                using (XmlTextWriter xw = new XmlTextWriter(strTemplateFieldFileName, Encoding.Unicode))
                {
                    xw.WriteStartElement("Fields");
                    DataRow[] drs = myDataSetTemp.Tables[0].Select(string.Format("SubType='{0}'or SubType=''", strModalityType));
                    foreach (DataRow myRow in drs)
                    {
                        xw.WriteElementString(Convert.ToString(myRow["FieldName"]), Convert.ToString(myRow["FieldName"]));
                    }
                    xw.WriteEndElement();
                    xw.Close();
                }
                string strInitialTemplateFileName = strTemplateFileName;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(strInitialTemplateFileName);
                XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("ConnectionString");
                if (xmlNodeList.Count == 1)
                {
                    xmlNodeList.Item(0).InnerText = string.Format(
                            @"{0}\PrintTemplateFiles\SystemFiles\{1}\{2}_Field.xml",
                            Application.StartupPath, lcid, strTypeDesc);
                }

                xmlNodeList = xmlDocument.GetElementsByTagName("RecordSource");
                if (xmlNodeList.Count == 1)
                {
                    xmlNodeList.Item(0).InnerText = "Fields";
                }
                xmlDocument.Save(strInitialTemplateFileName);

                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = string.Format(@"{0}\C1ReportDesigner.exe", Application.StartupPath);
                procInfo.WorkingDirectory = Application.StartupPath;
                procInfo.Arguments = string.Format(
                    "\"{0}{1}_{2}_Template_{3}.xml\"",
                    strPrintTemplateFileDir, strTypeDesc, selectNode.Name, strLatestVersion);

                Process proc = Process.Start(procInfo);
                proc.WaitForExit();
                proc.Dispose();

                DialogResult dr = MessageBox.Show("Submint Template", "Do you want to submit this print template?", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    if (File.Exists(strTemplateFileName))
                    {
                        FileInfo deleteFileInfo = new FileInfo(strTemplateFileName);
                        deleteFileInfo.Delete();
                    }
                    return;
                }

                StreamReader fsr = new StreamReader(strTemplateFileName);
                string strTemplateInfo = fsr.ReadToEnd();
                fsr.Close();

                var modifyResult = Utility.ModifyPrintTemplateFieldInfo(selectNode.Name, strTemplateInfo);
                if (modifyResult)
                {

                    DirectoryInfo direcInfo = new DirectoryInfo(strPrintTemplateFileDir);
                    FileInfo[] deleteFileInfo = direcInfo.GetFiles(strTypeDesc + "_" + selectNode.Name + "_Template_*.xml", SearchOption.TopDirectoryOnly);
                    if (deleteFileInfo.Length != 0)
                    {
                        foreach (FileInfo fi in deleteFileInfo)
                        {
                            fi.Delete();
                        }
                    }
                    ShowPrintTemplate();
                }
                else
                {
                    if (File.Exists(strTemplateFileName))
                    {
                        FileInfo deleteFileInfo = new FileInfo(strTemplateFileName);
                        deleteFileInfo.Delete();
                    }
                    MessageBox.Show("Modify the template failure");
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }

        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyTemplate();
        }
        private void ReName()
        {
            try
            {
                RadTreeNode selectNode = treeViewPrintTemplate.SelectedNode;
                if (selectNode.Tag != null && selectNode.Tag.ToString() != "LeafFile")
                {
                    return;
                }

                //PrintTemplate Extend
                int count = 0;
                if (selectNode.Level == 3)
                    count = GetTemplateCount(selectNode.Parent.Parent.Name);
                else if (selectNode.Level == 2)
                    count = GetTemplateCount(selectNode.Parent.Name);
                if (count == 1)
                    return;
                string strNewName = "";
                SetNodeNameForm nameForm = new SetNodeNameForm();

                if (DialogResult.OK == nameForm.ShowDialog())
                {
                    strNewName = nameForm.TemplateName;

                    string strType;

                    if (selectNode.Level == 2 && selectNode.Tag.ToString() != "TypeNode")
                    {
                        strType = selectNode.Parent.Name;
                    }
                    else
                    {
                        strType = selectNode.Parent.Parent.Name;
                    }

                    var nameExists = Utility.TemplateNameExists(strNewName, int.Parse(strType), Utility.getRootNodeName(selectNode));
                    if (nameExists)
                    {
                        MessageBox.Show("The template name is existed!");
                        return;
                    }

                    var modifyNameResult = Utility.ModifyPrintTemplateName(selectNode.Name, strNewName);
                    if (modifyNameResult)
                    {

                        selectNode.Text = strNewName;
                    }
                    else
                    {
                        MessageBox.Show("Modify the name failure!");
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReName();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowPrintTemplate();
        }
        private void ShowPrintTemplate()
        {
            try
            {

                string strPrintTemplateFileDir = GetUserPrintTemplateDir();
                if (strPrintTemplateFileDir == "")
                    return;

                if (treeViewPrintTemplate.SelectedNode.Level == 3 || (treeViewPrintTemplate.SelectedNode.Level == 2 && (treeViewPrintTemplate.SelectedNode.Nodes.Count < 1)))
                {
                    var latestVersion = Utility.GetLatestVersion(treeViewPrintTemplate.SelectedNode.Name);
                    string strLatestVersion = latestVersion.ToString();
                    string strTypeDesc;

                    if (treeViewPrintTemplate.SelectedNode.Level == 2 && treeViewPrintTemplate.SelectedNode.Tag.ToString() != "TypeNode")
                    {
                        strTypeDesc = GetTemplateType(treeViewPrintTemplate.SelectedNode.Parent.Name);
                    }
                    else
                    {
                        strTypeDesc = GetTemplateType(treeViewPrintTemplate.SelectedNode.Parent.Parent.Name);
                    }
                    string strTemplateFileName = strPrintTemplateFileDir + strTypeDesc + "_" + treeViewPrintTemplate.SelectedNode.Name + "_Template_" + strLatestVersion + ".xml";

                    FileInfo latestFileInfo = new FileInfo(strTemplateFileName);
                    if (!latestFileInfo.Exists)
                    {
                        string strNewTemplateInfo = Utility.LoadPrintTemplateInfo(treeViewPrintTemplate.SelectedNode.Name);
                        StreamWriter fs = new StreamWriter(strTemplateFileName, false);
                        fs.Write(strNewTemplateInfo);
                        fs.Close();
                    }
                    ShowPrintTemplate(strTemplateFileName, "Template", strTypeDesc + "_" + treeViewPrintTemplate.SelectedNode.Name);

                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPrintTemplate();
        }
        /// <summary>
        /// Function:Check the local file is the latest file 
        /// </summary>
        /// <param name="strTemplateGuid">Current print template guid</param>
        /// <param name="strTemplateCategory">Current print template type</param>
        /// <param name="strTemplateName">Current print template name</param>
        /// <returns>True:latest; False:no latest</returns>
        private bool IsLatestFile(string strTemplateGuid, string strTemplateCategory, string strTemplateName)
        {
            try
            {
                string strPrintTemplateFileDir = GetUserPrintTemplateDir();
                if (strPrintTemplateFileDir == "")
                    return false;
                strTemplateGuid = strTemplateGuid.Trim();
                strTemplateCategory = strTemplateCategory.Trim();
                strTemplateName = strTemplateName.Trim();

                var latestVersion = Utility.GetLatestVersion(strTemplateGuid);
                string strLatestVersion = latestVersion.ToString();
                string strTemplateFileName = strPrintTemplateFileDir + strTemplateCategory + "_" + strTemplateGuid + "_Template_" + strLatestVersion + ".xml";

                FileInfo latestFileInfo = new FileInfo(strTemplateFileName);
                return latestFileInfo.Exists;
            }
            catch (Exception err)
            {
                logger.Error(err);
                return false;
            }
        }
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (printPreviewControl.Document != null && printPreviewControl.Zoom < 10)
                {
                    printPreviewControl.Zoom += 0.1;
                    comboBoxZoom.Text = String.Format("{0:P}", printPreviewControl.Zoom);
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (printPreviewControl.Document != null && printPreviewControl.Zoom > 0.11)
                {
                    printPreviewControl.Zoom -= 0.1;
                    comboBoxZoom.Text = String.Format("{0:P}", printPreviewControl.Zoom);
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }
        private void comboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (printPreviewControl.Document != null)
                {
                    if (comboBoxZoom.Text != "")
                    {
                        printPreviewControl.Zoom = double.Parse(comboBoxZoom.Text.Split('%')[0]) / 100;
                    }
                    else
                    {
                        printPreviewControl.Zoom = 1;
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void comboBoxZoom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if (comboBoxZoom.Text != "" && Convert.ToDouble(comboBoxZoom.Text) >= 0.01 && Convert.ToDouble(comboBoxZoom.Text) <= 10)
                    {
                        double size = double.Parse(comboBoxZoom.Text);
                        if (size > 10 || size < 0.01)
                        {
                            printPreviewControl.Zoom = 1;
                            comboBoxZoom.Text = string.Format("{0:P}", printPreviewControl.Zoom);
                        }
                        else
                            printPreviewControl.Zoom = size;
                        if (comboBoxZoom.FindItemExact(string.Format("{0:P}", printPreviewControl.Zoom)) == null)
                            comboBoxZoom.Add(string.Format("{0:P}", printPreviewControl.Zoom));
                    }
                    else
                    {
                        comboBoxZoom.Text = "";
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
                comboBoxZoom.Text = "";
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            ReName();
        }
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            SetDefault(true);
        }

        private void treeViewPrintTemplate_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsPrint)
            {
                return;
            }
            RadTreeNode node = this.treeViewPrintTemplate.GetNodeAt(e.X, e.Y);
            if (node != null && node == treeViewPrintTemplate.SelectedNode)
                ShowPrintTemplate();
        }

        private void setDefaultToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            SetDefault(true);
        }
        private void setTypeDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDefault(false);
        }
        private void btnSetTypeDefault_Click(object sender, EventArgs e)
        {
            SetDefault(false);
        }
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifyTemplate();
        }

        private void reArrangeUI(string tag)
        {
            if (tag == "1")
            {
                this.btnSetTypeDefault.Location = this.btnSetDefault.Location;
                this.btnSetDefault.Visible = false;
                this.label1.Location = this.label2.Location;
                this.lblTypeDefault.Location = this.lblModalityTypeDefault.Location;
                this.lblModalityTypeDefault.Visible = false;
                this.lblAllDefault.Visible = false;
                this.label2.Visible = false;
                this.label3.Visible = false;
                this.contextMenuPrint.Items.Remove(this.setDefaultToolStripMenuItem1);
            }
        }

        #region ExportTemplate By Foman
        private DataSet tpDs = null;
        private int templateState = 0;//0 normal;1 new ;2 modify;
        private void ExportPanel_Load()
        {
            this.btnExportNew.Text = lang("New");
            this.btnExportModify.Text = lang("Modify");
            this.btnExportDelete.Text = lang("Delete");
            this.btnExportClear.Text = lang("Clear");

            toolTip1.Active = true;
            toolTip1.IsBalloon = true;

            this.toolTip1.SetToolTip(btnExportNew, "New");
            this.toolTip1.SetToolTip(btnExportModify, "Modify");
            this.toolTip1.SetToolTip(btnExportDelete, "Delete");
            this.toolTip1.SetToolTip(btnExportClear, "Clear");
            this.toolTip1.SetToolTip(lbltempName, "Template Name");
            this.toolTip1.SetToolTip(lblpath, "Excel Path");
            this.toolTip1.SetToolTip(lbldpt, "Description");

            this.toolTip1.SetToolTip(btnChose, "Chose Tempalte");
            this.toolTip1.SetToolTip(btnView, "View Tempalte");

            initExportTree();

            Color coloMustInput = Color.White;

            this.tbxName.BackColor = coloMustInput;
            this.tbxpath.BackColor = coloMustInput;
        }

        private void initExportTree()
        {
            try
            {
                var ds = Utility.LoadExportTemplateType();
                DataTable myTable = ds.Tables[0];
                RadTreeNode root = new RadTreeNode();
                root.Name = "Excel Export Template";
                root.Text = lang("ExcelTemplatePanel.ExcelTemplateNode.Text");
                treeViewTemplate.Nodes.Add(root);
                foreach (DataRow curRow in myTable.Rows)
                {
                    RadTreeNode node = new RadTreeNode();
                    node.Name = curRow["Value"].ToString().Trim();
                    node.Text = lang("PrintPanel." + curRow["Text"].ToString() + ".Node.Text");

                    root.Nodes.Add(node);

                }
                var generalDs = Utility.LoadGeneralStatType();
                var statTable = generalDs.Tables[0];
                foreach (RadTreeNode typeNode in root.Nodes)
                {

                    if (typeNode.Name == "1")
                    {
                        if (statTable == null)
                            continue;
                        foreach (DataRow r in statTable.Rows)
                        {
                            RadTreeNode newNode = new RadTreeNode();
                            newNode.Name = Convert.ToString(r["QueryName"]);
                            newNode.Text = Convert.ToString(r["QueryName"]);
                            typeNode.Nodes.Add(newNode);
                        }
                        continue;
                    }
                }
                root.Expand();

                var subTplDs = Utility.LoadSubExportTemplateInfo();
                myTable = subTplDs.Tables[0];
                foreach (RadTreeNode curNode in root.Nodes)
                {
                    foreach (DataRow curRow in myTable.Rows)
                    {
                        if (curNode.Name != curRow["Type"].ToString())
                            continue;
                        if (curNode.Nodes.Find(Convert.ToString(curRow["ChildType"]).Trim(), false).GetLength(0) < 1)
                            continue;

                        RadTreeNode node = new RadTreeNode();
                        node.Name = curRow["TemplateGuid"].ToString().Trim();
                        node.Text = curRow["TemplateName"].ToString().Trim();
                        curNode.Nodes[Convert.ToString(curRow["ChildType"]).Trim()].Nodes.Add(node);

                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void refreshTemplateData()
        {
            tpDs = Utility.LoadSubExportTemplateInfo();
        }
        private void ShowExportTemplate()
        {
            RadTreeNode node = treeViewTemplate.SelectedNode;
            if (node != null && node.Level == 3)
            {
                if (tpDs != null)
                {
                    DataTable dt = tpDs.Tables[0];
                    foreach (DataRow curRow in dt.Rows)
                    {
                        if (curRow["TemplateGuid"].ToString() == node.Name)
                        {
                            this.tbxName.Text = curRow["TemplateName"].ToString();
                            this.tbxDesp.Text = curRow["Descriptions"].ToString();
                            this.tbxpath.Text = "";
                        }
                    }
                }
            }
            else clear();
        }

        private bool isValidInuput(string str)
        {
            if (str.Trim() != "")
            {
                if (Regex.IsMatch(str, "[,|'\"]"))
                {
                    return false;
                }
                else return true;
            }
            else return false;
        }

        private void btnExportChoseTemplate_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tbxpath.Text = openFileDialog1.FileName;
            }
        }

        private void btnExportViewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewTemplate.SelectedNode == null || treeViewTemplate.SelectedNode.Level != 3)
                    return;
                {
                    string templateGuid = treeViewTemplate.SelectedNode.Name;

                    string filepath = GetUserPrintTemplateDir();
                    if (filepath == "") return;
                    string fileName = filepath + templateGuid + "_Template.xls";

                    var ds = Utility.LoadExportTemplateInfo(templateGuid);
                    byte[] file = null;
                    file = (byte[])ds.Tables[0].Rows[0]["TemplateInfo"];

                    FileInfo fi = new System.IO.FileInfo(fileName);

                    FileStream fs = fi.OpenWrite();

                    fs.Write(file, 0, file.Length);
                    fs.Close();

                    if (!File.Exists(fileName))
                    {
                        MessageBox.Show("File does not exist!");
                        return;
                    }

                    Type objExcel = Type.GetTypeFromProgID("Excel.Application");
                    if (objExcel == null)
                    {
                        MessageBox.Show("Excel not installed!");
                        return;
                    }

                    ProcessStartInfo startInfo = new ProcessStartInfo(fileName);
                    startInfo.Verb = "Open";
                    startInfo.ErrorDialog = false;
                    Process process = Process.Start(startInfo);
                }
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void saveFile(string fileName, string content)
        {
            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(content);
            sw.Close();
        }
        private void btnExportNew_Click(object sender, EventArgs e)
        {
            if (treeViewTemplate.SelectedNode != null && treeViewTemplate.SelectedNode.Level > 1)
            {
                templateState = 1;
                SaveExport();
            }
            else
            {
                MessageBox.Show("Can't add new template to root node!");
                return;
            }
        }

        private void btnExportModify_Click(object sender, EventArgs e)
        {
            if (treeViewTemplate.SelectedNode != null && treeViewTemplate.SelectedNode.Level == 3)
            {
                templateState = 2;
                SaveExport();
                return;
            }
            else
            {
                MessageBox.Show("Please select a template!");
                return;
            }

        }

        private void btnExportDelete_Click(object sender, EventArgs e)
        {
            RadTreeNode selectNode = treeViewTemplate.SelectedNode;
            if (selectNode == null || selectNode.Level != 3)
                return;
            var dialogRes = MessageBox.Show("Are you sure to delete this template?", "Notification", MessageBoxButtons.YesNo);
            if (DialogResult.No == dialogRes)
            {
                return;
            }

            Utility.DeleteExportTemplate(selectNode.Name);
            clear();
            selectNode.Remove();
            refreshTemplateData();
        }

        private void btnExportClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            this.tbxDesp.Text = "";
            this.tbxName.Text = "";
            this.tbxpath.Text = "";
        }


        //before save refresh tpds.

        private void SaveExport()
        {
            try
            {
                var selectNode = treeViewTemplate.SelectedNode;
                if (selectNode == null)
                {
                    MessageBox.Show("Please select a node!");
                    return;
                }

                if (selectNode.Level == 0 || selectNode.Level == 1)
                {
                    MessageBox.Show("Can't add new template to root node!");
                    return;
                }

                string strTemplateName = this.tbxName.Text;
                if (strTemplateName == null || strTemplateName == "")
                {
                    MessageBox.Show("The template must have a name!");
                    return;
                }
                if (!isValidInuput(strTemplateName))
                {
                    MessageBox.Show("Invalid charactor(, | ' \")");
                    return;
                }
                if (templateState == 2)
                {
                    ModifyExportTemplate();

                }
                else if (templateState == 1)
                {
                    AddExportTemplate();
                }
            }

            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void ModifyExportTemplate()
        {
            RadTreeNode selectNode = treeViewTemplate.SelectedNode;
            if (selectNode.Level != 3)
                return;

            string strTemplateGuid = selectNode.Name;
            string strTemplateName = this.tbxName.Text;
            string strTemplatePath = this.tbxpath.Text;
            string strTemplateDes = this.tbxDesp.Text;

            refreshTemplateData();
            if (tpDs != null)
            {
                DataTable dt = tpDs.Tables[0];
                DataRow tmp = null;
                foreach (DataRow curRow in dt.Rows)
                {
                    if (curRow["TemplateGuid"].ToString() == selectNode.Name)
                    {
                        tmp = curRow;
                    }
                }

                foreach (DataRow curRow in dt.Rows)
                {
                    if (curRow["TemplateGuid"].ToString() != selectNode.Name)
                    {
                        if (strTemplateName == curRow["TemplateName"].ToString())
                        {
                            if (tmp != null && tmp["Type"].ToString() == curRow["Type"].ToString() && tmp["ChildType"].ToString() == curRow["ChildType"].ToString())
                            {
                                MessageBox.Show("The template name is existed!");
                                return;
                            }
                        }
                    }
                }
            }

            if (strTemplatePath == "" || strTemplatePath == null) //update tempalte fields but not templateinfo
            {
                Utility.ModifyExportTemplate(strTemplateName, strTemplateDes, 0, 0, strTemplateGuid);
                selectNode.Text = strTemplateName;
            }
            else
            {
                if (!File.Exists(strTemplatePath))
                {
                    MessageBox.Show("File doesn't exist!");
                    return;
                }

                FileInfo fi = new FileInfo(strTemplatePath);
                FileStream fs = fi.OpenRead();

                byte[] bytes = new byte[fs.Length];

                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                var modifyResult = Utility.ModifyExportTemplateInfo(strTemplateGuid, bytes, strTemplateName, strTemplateDes, 0, 0);
                if (!modifyResult)
                {
                    MessageBox.Show("Modify template failure!");
                    return;
                }
                else
                {
                    selectNode.Text = strTemplateName;
                }
            }
            MessageBox.Show("Modify template Success!");
            refreshTemplateData();
            ShowExportTemplate();
            templateState = 0;
        }

        private void AddExportTemplate()
        {
            RadTreeNode selectNode = treeViewTemplate.SelectedNode;
            RadTreeNode typeNode = null;
            if (selectNode.Level == 0 || selectNode.Level == 1)
            {
                MessageBox.Show("Can't add new template to root node!");
                return;
            }

            string strTemplateName = this.tbxName.Text;
            string strTemplatePath = this.tbxpath.Text;
            string strTemplateDes = this.tbxDesp.Text;

            refreshTemplateData();

            string strTemplateGuid = Guid.NewGuid().ToString();
            string strType = "1";
            string ChildType = "";
            if (selectNode.Level == 2)
            {
                strType = selectNode.Parent.Name;
                ChildType = selectNode.Text;
                typeNode = selectNode.Parent;
            }
            else if (selectNode.Level == 3)
            {
                strType = selectNode.Parent.Parent.Name;
                ChildType = selectNode.Parent.Name;
                typeNode = selectNode.Parent.Parent;
            }
            else
            {
                return;
            }

            if (tpDs != null)
            {
                foreach (DataRow dr in tpDs.Tables[0].Rows)
                {
                    if (dr["Type"].ToString() == strType && dr["ChildType"].ToString() == ChildType)
                    {
                        if (dr["TemplateName"].ToString() == strTemplateName)
                        {
                            MessageBox.Show("The template name is exist!");
                            return;
                        }
                    }

                }
            }

            if (!File.Exists(strTemplatePath))
            {
                MessageBox.Show("File doesn't exist!");
                return;
            }

            FileInfo fi = new FileInfo(strTemplatePath);
            FileStream fs = fi.OpenRead();

            byte[] bytes = new byte[fs.Length];

            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            var addResult = Utility.AddExportTemplate(strTemplateGuid, int.Parse(strType), ChildType, strTemplateName, bytes, strTemplateDes, 0, 0);
            if (addResult)
            {
                RadTreeNode tempNode = new RadTreeNode();
                tempNode.Name = strTemplateGuid;
                tempNode.Text = strTemplateName;

                if (typeNode.Nodes.Count > 0)
                {
                    typeNode.Nodes[ChildType].Nodes.Add(tempNode);
                }
                else
                {
                    typeNode.Nodes.Add(tempNode);
                }
                treeViewTemplate.SelectedNode = tempNode;
                tempNode.EnsureVisible();
                templateState = 0;
            }
            else
            {
                MessageBox.Show("Add new template failure!");
                return;
            }

            refreshTemplateData();
            ShowExportTemplate();
        }
        private void UpdateControlState()
        {
            if (templateState == 0)
            {
                this.btnChose.Enabled = false;
                this.btnView.Enabled = true;
                this.btnExportModify.Enabled = true;
                this.btnExportNew.Enabled = true;
                this.tbxpath.Enabled = false;
                if (treeViewTemplate.SelectedNode != null && treeViewTemplate.SelectedNode.Level == 3)
                {
                    this.btnExportDelete.Enabled = true;
                }
                else
                {
                    this.btnExportDelete.Enabled = false;
                }
            }
            else if (templateState == 1) //New
            {
                btnChose.Enabled = true;
                btnView.Enabled = false;
                btnExportDelete.Enabled = false;
                btnExportModify.Enabled = false;
                btnExportNew.Enabled = false;
                tbxpath.Enabled = true;
            }
            else if (templateState == 2)  //Modify
            {
                btnExportNew.Enabled = false;
                btnExportModify.Enabled = false;
                btnView.Enabled = true;
                btnChose.Enabled = true;
                btnExportDelete.Enabled = false;
                tbxpath.Enabled = true;
            }
        }

        private void treeViewTemplate_SelectedNodeChanged(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (e.Node == treeViewTemplate.SelectedNode)
                ShowExportTemplate();
            templateState = 0;
        }

        #endregion

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyTreeNode();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteTreeNode();
        }

        private void LoadPatientType()
        {
            _chkcbxPatientType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            _chkcbxPatientType.Clear();
            var table = Utility.GetDictionary(5);
            foreach (DataRow dr in table.Rows)
            {
                string v = dr["Value"] as string;
                string t = dr["Text"] as string;

                v = v.ToUpper();

                _chkcbxPatientType.AddItem(v, t);
            }
        }

        private void LoadCurrentNodePatientType()
        {
            string szValue = string.Empty;

            if (treeViewPrintTemplate.SelectedNode != null &&
                System.Convert.ToString(treeViewPrintTemplate.SelectedNode.Tag) == "LeafFile")
            {
                szValue = DataUtility.GetStringFromDataTable(
                    _dtPrintTemplate,
                    "TemplateGuid",
                    treeViewPrintTemplate.SelectedNode.Name,
                    "PropertyTag");
            }

            _chkcbxPatientType.CheckedNames = szValue;
        }

        private bool isChangedPropertyTag()
        {
            string szOriginTag = DataUtility.GetStringFromDataTable(
                _dtPrintTemplate,
                "TemplateGuid",
                treeViewPrintTemplate.SelectedNode.Name,
                "PropertyTag");

            return _chkcbxPatientType.CheckedNames != szOriginTag;
        }

        private void _chkcbxPatientType_DropDownClosed(object sender, RadPopupClosedEventArgs args)
        {
            if (!isTemplateNode(treeViewPrintTemplate.SelectedNode))
            {
                if (!string.IsNullOrEmpty(_chkcbxPatientType.CheckedNames))
                {
                    MessageBox.Show("Please select one template first!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _chkcbxPatientType.CheckedNames = string.Empty;
                }

                return;
            }

            if (!isChangedPropertyTag())
                return;

            RefreshPrintTemplateData();

            if (isUsedTag())
            {
                LoadCurrentNodePatientType();

                return;
            }

            var res = Utility.ModifyPrintTemplateName(treeViewPrintTemplate.SelectedNode.Name, treeViewPrintTemplate.SelectedNode.Text.Trim());
            if (res)
            {
                DataUtility.SetValueToDataTable(
                    _dtPrintTemplate,
                    "TemplateGuid",
                    treeViewPrintTemplate.SelectedNode.Name,
                    "PropertyTag",
                    _chkcbxPatientType.CheckedNames);
            }
            else
            {
                MessageBox.Show("Failed to update the template!");
            }
        }

        private bool isUsedTag()
        {
            string[] values = _chkcbxPatientType.CheckedNames.Split(",".ToCharArray());

            foreach (string key in values)
            {
                if (string.IsNullOrEmpty(key))
                    continue;

                DataRow[] drs = _dtPrintTemplate.Select(
                    "type=" + GetCurrentNodeType()
                    + " AND ModalityType='" + GetCurrentNodeModalityType() + "'"
                    + " AND TemplateGuid<>'" + treeViewPrintTemplate.SelectedNode.Name + "'"
                    + " AND PropertyTag like '%" + key + "%'"
                    + " AND SITE='" + Utility.getRootNodeName(treeViewPrintTemplate.SelectedNode) + "'"
                    );

                if (drs.Length > 0)
                {
                    string fmt = "Tag {0} is being used by {1}!";

                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(fmt, key, System.Convert.ToString(drs[0]["TemplateName"]));

                    MessageBox.Show(sb.ToString());

                    return true;
                }
            }

            return false;
        }

        private void RefreshPrintTemplateData()
        {
            var type = GetCurrentNodeType();
            var result = Utility.LoadSubPrintTemplateInfo(int.Parse(type));
            if (result != null && result.Tables.Count > 0)
            {
                _dtPrintTemplate.Merge(result.Tables[0]);
            }
        }

        private string GetCurrentNodeType()
        {
            RadTreeNode node = treeViewPrintTemplate.SelectedNode;

            if (node != null && node.Parent != null && node.Parent.Parent != null && 3 == node.Level)
            {
                return node.Parent.Parent.Name;
            }

            return string.Empty;
        }

        private string GetCurrentNodeModalityType()
        {
            RadTreeNode node = treeViewPrintTemplate.SelectedNode;

            if (node != null && node.Parent != null && 3 == node.Level)
            {
                return node.Parent.Name;
            }

            return string.Empty;
        }

        private bool isTemplateNode(RadTreeNode node)
        {
            return node != null && 0 == string.Compare("LeafFile", System.Convert.ToString(node.Tag), true);
        }
    }

    public static class DataUtility
    {
        public static string GetStringFromDataTable(DataTable dt, string filterColumn, string filterValue, string colName)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return string.Empty;
            }

            if (!dt.Columns.Contains(colName) || !dt.Columns.Contains(filterColumn))
            {
                System.Diagnostics.Debug.Assert(false);

                return string.Empty;
            }

            DataRow[] drs = dt.Select(filterColumn + "='" + filterValue.Trim() + "'");

            if (drs.Length > 0)
            {
                return System.Convert.ToString(drs[0][colName]);
            }

            return string.Empty;
        }

        public static void SetValueToDataTable(DataTable dt, string filterColumn, string filterValue, string colName, string szValue)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            if (!dt.Columns.Contains(colName) || !dt.Columns.Contains(filterColumn))
            {
                System.Diagnostics.Debug.Assert(false);

                return;
            }

            DataRow[] drs = dt.Select(filterColumn + "='" + filterValue.Trim() + "'");

            foreach (DataRow dr in drs)
            {
                dr[colName] = szValue;
            }
        }
    }

    public enum PrinterStatus
    {
        Other = 1,
        Unknown = 2,
        Idle = 3,
        Printing = 4,
        Warmup = 5,
        Stopped = 6,
        Offline = 7,

    }
}
