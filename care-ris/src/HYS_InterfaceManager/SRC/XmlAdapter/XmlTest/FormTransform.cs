using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.XmlAdapter.Common.Objects;

namespace XmlTest
{
    public partial class FormTransform : Form
    {
        public FormTransform()
        {
            InitializeComponent();
        }

        #region Inbound Transform

        private IEControler _ieCtrlIn;
        private IEControler _ieCtrlXSL;
        private IEControler _ieCtrlItem;
        private IEControler _ieCtrlDataSet;
        private void InitializeInbound()
        {
            _ieCtrlIn = new IEControler(this, this.textBoxInXML, this.checkBoxInXML, true);
            _ieCtrlXSL = new IEControler(this, this.textBoxXSL, this.checkBoxXSL, true);
            _ieCtrlItem = new IEControler(this, this.textBoxXMLItem, this.checkBoxXMLItem, true);
            _ieCtrlDataSet = new IEControler(this, this.textBoxDataSetItem, this.checkBoxDataSetItem, true);

            using (StreamReader sr = File.OpenText(Program.XMLFileName))
            {
                this.textBoxInXML.Text = sr.ReadToEnd();
            }
        }

        private const string NAME = "/XMLRequestMessage/Name";
        private const string QUALIFIER = "/XMLRequestMessage/Qualifier";
        private const string XIM = "/XMLRequestMessage/XIM";
        private const string ITEM = "/XMLRequestMessage/XIM/ITEM";
        private const string SCHEDULED_PROCEDURE_STEP = "/XMLRequestMessage/XIM/ITEM/SCHEDULED_PROCEDURE_STEP";
        
        private static void SplitItem(List<XmlNode> nodeList, string xmlString)
        {
            if (nodeList == null) return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            XmlNodeList itemNodeList = xmlDoc.SelectNodes(ITEM);
            if (itemNodeList == null || itemNodeList.Count < 2)
            {
                nodeList.Add(xmlDoc);
            }
            else
            {
                for (int i = 0; i < itemNodeList.Count; i++)
                {
                    XmlNode newDoc = xmlDoc.Clone();
                    XmlNode newXimNode = newDoc.SelectSingleNode(XIM);
                    for (int j = 0; j < newXimNode.ChildNodes.Count; j++)
                    {
                        if (i == j) continue;
                        newXimNode.RemoveChild(newXimNode.ChildNodes[j]);
                    }
                    nodeList.Add(newDoc);
                }
            }

            //XmlNode ximNode = xmlDoc.SelectSingleNode(XIM);
            //if (ximNode != null)
            //{
            //    foreach (XmlNode item in ximNode.ChildNodes)
            //    {
            //        XmlNode newNode = xmlDoc.Clone();
            //        XmlNode newXimNode = newNode.SelectSingleNode(XIM);
            //        if (newXimNode != null)
            //        {
            //            newXimNode.RemoveAll();
            //            newXimNode.AppendChild(item.Clone());
            //        }
            //        nodeList.Add(newNode);
            //    }
            //}
        }
        private static void SplitProcedureStep(List<XmlNode> nodeList)
        {
            if (nodeList == null) return;

            List<XmlNode> tempList = new List<XmlNode>();
            foreach (XmlNode node in nodeList) tempList.Add(node);

            foreach (XmlNode node in tempList)
            {
                XmlNodeList spsNodeList = node.SelectNodes(SCHEDULED_PROCEDURE_STEP);
                if (spsNodeList == null || spsNodeList.Count < 2) continue;

                int index = nodeList.IndexOf(node);
                if (index < 0) continue;

                nodeList.RemoveAt(index);
                for (int i = spsNodeList.Count - 1; i >= 0; i--)
                {
                    XmlNode newNode = node.Clone();
                    XmlNode newItemNode = newNode.SelectSingleNode(ITEM);
                    XmlNodeList newSpsNodeList = newItemNode.SelectNodes(SCHEDULED_PROCEDURE_STEP);
                    for (int j = 0; j < newSpsNodeList.Count; j++)
                    {
                        if (i == j) continue;
                        newItemNode.RemoveChild(newSpsNodeList[j]);
                    }
                    nodeList.Insert(index, newNode);
                }
            }

            //foreach (XmlNode node in tempList)
            //{
            //    XmlNodeList spsNodeList = node.SelectNodes(SCHEDULED_PROCEDURE_STEP);
            //    if (spsNodeList != null || spsNodeList.Count < 1) continue;

            //    int index = nodeList.IndexOf(node);
            //    if (index < 0) continue;

            //    nodeList.RemoveAt(index);
            //    foreach (XmlNode spsNode in spsNodeList)
            //    {
            //        XmlNode newNode = node.Clone();
            //        XmlNode newItemNode = newNode.SelectSingleNode(ITEM);
            //        XmlNodeList newSpsNodeList = newItemNode.SelectNodes(SCHEDULED_PROCEDURE_STEP);
            //        foreach (XmlNode newSpsNode in newSpsNodeList) newItemNode.RemoveChild(newSpsNode);
            //        newItemNode.AppendChild(spsNode.Clone());
            //        nodeList.Insert(index, newNode);
            //    }
            //}
        }
        private string[] Transform(XMLTransformer transformer, List<XmlNode> nodeList)
        {
            if (transformer == null || nodeList == null) return null;
            List<string> list = new List<string>();
            foreach (XmlNode node in nodeList)
            {
                string outXML = "";
                string inXML = node.OuterXml;
                if (transformer.TransformString(inXML, ref outXML)) list.Add(outXML);
            }
            return list.ToArray();
        }

        private void buttonSplitItem_Click(object sender, EventArgs e)
        {
            string xmlString = this.textBoxInXML.Text;
            List<XmlNode> nodeList = new List<XmlNode>();
            SplitItem(nodeList, xmlString);
            SplitProcedureStep(nodeList);

            this.listBoxItem.Items.Clear();
            foreach (XmlNode node in nodeList)
            {
                this.listBoxItem.Items.Add(node);
            }
        }

        private void listBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlNode node = this.listBoxItem.SelectedItem as XmlNode;
            if (node != null) this.textBoxXMLItem.Text = node.OuterXml;
        }

        private void buttonTransform_Click(object sender, EventArgs e)
        {
            XMLTransformer transformer = XMLTransformer.CreateFromFile(this.textBoxXSLPath.Text.Trim());
            if (transformer == null)
            {
                MessageBox.Show(XMLTransformer.LastErrorInfor);
                return;
            }

            this.listBoxDataSet.Items.Clear();
            List<XmlNode> list = new List<XmlNode>();
            foreach(XmlNode node in this.listBoxItem.Items ) list.Add(node);
            string[] xmlList = Transform(transformer, list);
            foreach (string xml in xmlList) this.listBoxDataSet.Items.Add(xml);
        }

        private void listBoxDataSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxDataSetItem.Text = this.listBoxDataSet.SelectedItem as string;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText(this.textBoxXSLPath.Text.Trim()))
            {
                this.textBoxXSL.Text = sr.ReadToEnd();
            }
        }

        private void buttonLoadDataSet_Click(object sender, EventArgs e)
        {
            List<DataSet> dsList = new List<DataSet>();
            foreach (string xml in this.listBoxDataSet.Items)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                dsList.Add(ds);
            }

            DataSet dsResult = new DataSet();
            foreach (DataSet ds in dsList) dsResult.Merge(ds);
            this.dataGridGCDataSet.DataSource = dsResult.Tables[0];
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = File.CreateText(this.textBoxXSLPath.Text.Trim()))
            {
                sw.Write(this.textBoxXSL.Text);
            }
        }

        private void buttonEventType_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.textBoxInXML.Text);

            string strName = "";
            XmlNode nodeName = xmlDoc.SelectSingleNode(NAME);
            if (nodeName != null) strName = nodeName.InnerText;

            string strQualifier = "";
            XmlNode nodeQualifier = xmlDoc.SelectSingleNode(QUALIFIER);
            if (nodeQualifier != null) strQualifier = nodeQualifier.InnerText;

            MessageBox.Show(strName + " (" + strQualifier + ")");
        }

        #endregion

        #region Outbound Transform

        private class DataSetWrapper
        {
            public readonly DataSet DataSet;
            public DataSetWrapper(DataSet ds)
            {
                DataSet = ds;
            }
            public override string ToString()
            {
                return "DataSet";
            }
        }

        private IEControler _ieCtrlOut;
        private IEControler _ieCtrlXSLOut;
        private IEControler _ieCtrlItemOut;
        private IEControler _ieCtrlDataSetOut;
        private void InitializeOutbound()
        {
            _ieCtrlOut = new IEControler(this, this.textBoxOutXML, this.checkBoxOutXML, true);
            _ieCtrlXSLOut = new IEControler(this, this.textBoxXSLOut, this.checkBoxXSLOut, true);
            _ieCtrlItemOut = new IEControler(this, this.textBoxXMLItemOut, this.checkBoxXMLItemOut, true);
            _ieCtrlDataSetOut = new IEControler(this, this.textBoxDataSetItemOut, this.checkBoxDataSetItemOut, true);
        }

        //private const string ITEM = "/XMLRequestMessage/XIM/ITEM";
        //private const string SCHEDULED_PROCEDURE_STEP = "/XMLRequestMessage/XIM/ITEM/SCHEDULED_PROCEDURE_STEP";

        private const string PATIENT_ID = "/XMLRequestMessage/XIM/ITEM/PATIENT/IDENTIFICATION/ID/ID";
        private const string ACCESSION_NUMBER = "/XMLRequestMessage/XIM/ITEM/ORDER/IDENTIFICATION/ACCESSION_NUMBER/ID";
        private const string STUDY_INSTANCE_UID = "/XMLRequestMessage/XIM/ITEM/STUDY/IDENTIFICATION/GLOBAL_IDENTIFIER/INSTANCE_UID";
        private static string GetPKPath(int index)
        {
            switch (index)
            {
                default: return "";
                case 0: return PATIENT_ID;
                case 1: return ACCESSION_NUMBER;
                case 2: return STUDY_INSTANCE_UID;
            }
        }

        private static DataSet[] SplitDataSet(DataSet ds)
        {
            if (ds == null || ds.Tables.Count < 1) return null;

            List<DataSet> dsList = new List<DataSet>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataSet dsNew = new DataSet();
                dsNew.Merge(new DataRow[] { dr });
                dsList.Add(dsNew);
            }

            return dsList.ToArray();
        }
        private static string[] Transform(XMLTransformer transformer, List<DataSet> dataSetList)
        {
            if (transformer == null || dataSetList == null) return null;
            List<string> resultList = new List<string>();
            foreach (DataSet ds in dataSetList)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                ds.WriteXml(sw);
                string xml = sb.ToString();

                string str = "";
                if (transformer.TransformString(xml, ref str)) resultList.Add(str);
            }
            return resultList.ToArray();
        }
        private static string[] MergeProcedureStep(string[] msgList, string pkPath)
        {
            if (msgList == null || pkPath == null || pkPath.Length < 1) return null;

            int index = 0;
            Dictionary<string, XmlDocument> mergeTable = new Dictionary<string, XmlDocument>();
            foreach (string msg in msgList)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(msg);

                string pkValue = null;
                XmlNode node = doc.SelectSingleNode(pkPath);
                if (node != null) pkValue = node.InnerText;
                if (pkValue == null || pkValue.Length < 1) 
                    pkValue = "_" + (index++).ToString();

                if (mergeTable.ContainsKey(pkValue))
                {
                    XmlNode itemNode = mergeTable[pkValue].SelectSingleNode(ITEM);
                    if (itemNode != null)
                    {
                        XmlNode spsNode = doc.SelectSingleNode(SCHEDULED_PROCEDURE_STEP);
                        if (spsNode != null)
                        {
                            //itemNode.AppendChild(spsNode.Clone());
                            string xml = itemNode.InnerXml;
                            xml = xml + spsNode.OuterXml;
                            itemNode.InnerXml = xml; 
                        }
                    }
                }
                else
                {
                    mergeTable.Add(pkValue, doc);
                }
            }

            List<string> resultList = new List<string>();
            foreach (KeyValuePair<string, XmlDocument> pair in mergeTable) resultList.Add(pair.Value.OuterXml);
            return resultList.ToArray();
        }

        private void FormInbound_Load(object sender, EventArgs e)
        {
            InitializeInbound();
            InitializeOutbound();
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            this.listBoxMerged.Items.Clear();
            List<string> list = new List<string>();
            foreach (string str in this.listBoxItemOut.Items) list.Add(str);
            string[] strlist = MergeProcedureStep(list.ToArray(), PATIENT_ID);
            foreach (string str in strlist) this.listBoxMerged.Items.Add(str);
        }

        private void buttonDisplayDataSet_Click(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridGCDataSet.DataSource as DataTable;
            if (dt == null) return;

            DataSet ds = dt.DataSet;
            if (ds == null) return;

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ds.WriteXml(sw);
            this.textBoxOutXML.Text = sb.ToString();
        }

        private void buttonSplitDataSet_Click(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridGCDataSet.DataSource as DataTable;
            if (dt == null) return;

            DataSet ds = dt.DataSet;
            if (ds == null) return;

            DataSet[] dsList = SplitDataSet(ds);
            if (dsList == null) return;

            this.listBoxDataSetOut.Items.Clear();
            foreach (DataSet dsItem in dsList) this.listBoxDataSetOut.Items.Add(new DataSetWrapper(dsItem));
        }

        private void listBoxDataSetOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSetWrapper ds = this.listBoxDataSetOut.SelectedItem as DataSetWrapper;
            if (ds != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                ds.DataSet.WriteXml(sw);
                this.textBoxDataSetItemOut.Text = sb.ToString(); 
            }
        }

        private void buttonOpenOut_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText(this.textBoxXSLPathOut.Text.Trim()))
            {
                this.textBoxXSLOut.Text = sr.ReadToEnd();
            }
        }

        private void buttonSaveOut_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = File.CreateText(this.textBoxXSLPathOut.Text.Trim()))
            {
                sw.Write(this.textBoxXSLOut.Text);
            }
        }

        private void buttonTransformOut_Click(object sender, EventArgs e)
        {
            XMLTransformer transformer = XMLTransformer.CreateFromFile(this.textBoxXSLPathOut.Text.Trim());
            if (transformer == null)
            {
                MessageBox.Show(XMLTransformer.LastErrorInfor);
                return;
            }

            this.listBoxItemOut.Items.Clear();
            List<DataSet> dsList = new List<DataSet>();
            foreach (DataSetWrapper ds in this.listBoxDataSetOut.Items) dsList.Add(ds.DataSet);
            string[] strList = Transform(transformer, dsList);
            foreach (string str in strList) this.listBoxItemOut.Items.Add(str);
        }

        private void listBoxItemOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxXMLItemOut.Text = this.listBoxItemOut.SelectedItem as string;
        }

        private void listBoxMerged_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxOutXML.Text = this.listBoxMerged.SelectedItem as string;
        }

        #endregion
    }
}