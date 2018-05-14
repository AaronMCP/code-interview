using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using System.Data;
using Telerik.WinControls.UI.Localization;
using System.ComponentModel;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Specialized;



namespace Hys.CommonControls
{
    public class CSGridView : RadGridView
    {
        private Font _HkSupplementaryFont = new Font("SimSun-ExtB", (float)10, FontStyle.Regular);
        public bool HongKongSupplementaryCharacterSet
        {
            set;
            get;

        }

        #region Public Properties
        /// <summary>
        /// Gets or sets the last row in the current selection. 
        /// </summary>
        public int RowSel
        {
            get
            {
                if (this.SelectedRows != null && this.SelectedRows.Count > 0)
                {
                    return this.Rows.IndexOf(this.SelectedRows[this.SelectedRows.Count - 1] as GridViewDataRowInfo);
                }
                return -1;
            }
            set
            {
                if (value > -1)
                {
                    this.Rows[value].IsSelected = true;
                }
            }
        }

        /// <summary>
        /// whether can select a column or not 
        /// </summary> 
        [Description("Whether the full column can be selected or not, it is effected only the grid selection mode is cell selection")]
        public bool AllowColumnSelection
        {
            get
            {
                return _allowColumnSelection;
            }
            set
            {
                _allowColumnSelection = value;
            }
        }

        /// <summary>
        /// whether can select a column or not 
        /// </summary> 
        [Description("Whether the image column can be resized or not.")]
        public bool AllowImageColumnResize
        {
            get
            {
                return _allowImageColumnResize;
            }
            set
            {
                _allowImageColumnResize = value;
            }
        }
        public bool UseFixedRowHeight { get; set; }
        #endregion

        #region Private Fields
        private bool _supportEmbeddedControls = false;
        private string _columnFieldName = string.Empty;
        private GridViewTemplate _columnTemplate = null;

        private const int WM_MOUSEWHEEL = 0x20a;
        private bool _allowColumnSelection = false;
        private bool _allowImageColumnResize = false;
        

        private StringDictionary stringUserIDUserNameDic = new StringDictionary();

        #endregion

        public CSGridView()
            : base()
        {
            this.UseFixedRowHeight = true;
            this.SelectionMode = GridViewSelectionMode.FullRowSelect;
            this.MasterGridViewTemplate.AllowAddNewRow = false;
            this.MasterGridViewTemplate.AllowDeleteRow = false;
            this.MasterGridViewTemplate.AllowDragToGroup = false;
            this.ShowGroupPanel = false;
            this.MasterGridViewTemplate.ShowRowHeaderColumn = false;
            this.MasterGridViewTemplate.AllowColumnHeaderContextMenu = false;
            this.MasterGridViewTemplate.AllowCellContextMenu = false;
            this.MasterGridViewTemplate.EnableSorting = true;
            RadGridLocalizationProvider.CurrentProvider = new CSGridViewLocalizationProvider();
            this.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            this.ViewCellFormatting += new CellFormattingEventHandler(CSGridView_ViewCellFormatting);
            this.CellFormatting += new CellFormattingEventHandler(CSGridView_CellFormatting);
            this.RowFormatting += new RowFormattingEventHandler(CSGridView_RowFormatting);
            this.CellClick += new GridViewCellEventHandler(CSGridView_CellClick);
            this.ColumnWidthChanging += new ColumnWidthChangingEventHandler(CSGridView_ColumnWidthChanging);
            this.CellMouseMove += new CellMouseMoveEventHandler(CSGridView_CellMouseMove);

            //US27609 Report list doctor name and doctor id show incorrect
            stringUserIDUserNameDic.Add("TREPORT__CREATER", "TREPORT__CREATER");
            stringUserIDUserNameDic.Add("TREPORT__CREATER__DESC", "TREPORT__CREATER__DESC");
            stringUserIDUserNameDic.Add("TREPORT__SUBMITTER", "TREPORT__SUBMITTER");
            stringUserIDUserNameDic.Add("TREPORT__SUBMITTER__DESC", "TREPORT__SUBMITTER__DESC");
            stringUserIDUserNameDic.Add("TREPORT__FIRSTAPPROVER", "TREPORT__FIRSTAPPROVERNAME");
            stringUserIDUserNameDic.Add("TREPORT__FIRSTAPPROVER__DESC", "TREPORT__FIRSTAPPROVERNAME");
            stringUserIDUserNameDic.Add("TREPORT__SECONDAPPROVER", "TREPORT__SECONDAPPROVERNAME");
            stringUserIDUserNameDic.Add("TREPORT__SECONDAPPROVER__DESC", "TREPORT__SECONDAPPROVERNAME");
            stringUserIDUserNameDic.Add("TREPORT__BOOKER", "TREPORT__BOOKERNAME");
            stringUserIDUserNameDic.Add("TREPORT__BOOKER__DESC", "TREPORT__BOOKERNAME");
            stringUserIDUserNameDic.Add("TREPORT__REGISTRAR", "TREPORT__REGISTRARNAME");
            stringUserIDUserNameDic.Add("TREPORT__REGISTRAR__DESC", "TREPORT__REGISTRARNAME");
            stringUserIDUserNameDic.Add("TREPORT__TECHNICIAN", "TREPORT__TECHNICIANNAME");
            stringUserIDUserNameDic.Add("TREPORT__TECHNICIAN__DESC", "TREPORT__TECHNICIANNAME");
            stringUserIDUserNameDic.Add("TECHNICIAN", "TECHNICIANNAME");
            stringUserIDUserNameDic.Add("REGISTRAR", "REGISTRARNAME");
            stringUserIDUserNameDic.Add("TREPORT__MENDER", "TREPORT__MENDERNAME");
            stringUserIDUserNameDic.Add("TREPORT__MENDER__DESC", "TREPORT__MENDERNAME");
            stringUserIDUserNameDic.Add("MENDER", "MENDERNAME");
            stringUserIDUserNameDic.Add("CREATER", "CREATERNAME");
            stringUserIDUserNameDic.Add("SUBMITTER", "SUBMITTERNAME");
            stringUserIDUserNameDic.Add("FIRSTAPPROVER", "FIRSTAPPROVERNAME");
            stringUserIDUserNameDic.Add("SECONDAPPROVER", "SECONDAPPROVERNAME");

            

            this.GridBehavior = new CustomGridBehavior();
        }

        #region Public Functions
        /// <summary>
        /// Finds a row that contains a specified string.
        /// </summary>
        /// <param name="strFind">String to look for.</param>
        /// <param name="rowStart">Index of the row where the search should start.</param>
        /// <param name="colStart">Column that contains the data to be searched.</param>
        /// <param name="caseSensitive">Whether the search should be case-sensitive.</param>
        /// <param name="fullMatch">Whether a full match is required. If this parameter is set to False, searching for "John" may return a row that contains "Johnson".</param>
        /// <returns></returns>
        public int FindRow(string strFind, int rowStart, int colStart, bool caseSensitive, bool fullMatch)
        {
            if (this.Rows.Count >= 1 && !string.IsNullOrEmpty(strFind))
            {
                for (int i = rowStart; i < this.Rows.Count; i++)
                {
                    GridViewRowInfo row = this.Rows[i];
                    for (int j = colStart; j < row.Cells.Count; j++)
                    {
                        string cellValue = row.Cells[j].Value.ToString();
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            if (fullMatch
                                && cellValue.Equals(strFind, caseSensitive ?
                                StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
                            {
                                return i;
                            }
                            else if (!fullMatch)
                            {
                                if (caseSensitive && cellValue.Contains(strFind))
                                {
                                    return i;
                                }
                                else if (!caseSensitive && cellValue.ToLower().Contains(strFind.ToLower()))
                                {
                                    return i;
                                }
                            }
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Set the specified column to support embedding customized controls
        /// </summary>
        /// <param name="columnFieldName">column field name</param>
        public void SetColumn2SupportEmbedControls(string columnFieldName)
        {
            SetColumn2SupportEmbedControls(-1, columnFieldName);
        }

        /// <summary>
        /// Set the specified column to support embedding customized controls
        /// </summary>
        /// <param name="childTemplateLevel">column template level</param>
        /// <param name="columnFieldName">column field name</param>
        public void SetColumn2SupportEmbedControls(int childTemplateLevel, string columnFieldName)
        {
            _supportEmbeddedControls = true;
            _columnTemplate = childTemplateLevel < 0 ? this.MasterGridViewTemplate : this.MasterGridViewTemplate.ChildGridViewTemplates[childTemplateLevel];
            _columnFieldName = columnFieldName;
        }

        /// <summary>
        /// Enable/Disable the embedded control
        /// </summary>
        /// <param name="row"></param>
        /// <param name="enable"></param>
        public void SetCellEmbeddedControlStatus(GridViewDataRowInfo row, bool enable)
        {
            CSPropertyGridCellElement cellElement = row.Cells[_columnFieldName].CellElement as CSPropertyGridCellElement;
            if (cellElement != null)
            {
                cellElement.ActiveControl.Enabled = enable;
            }
        }

        /// <summary>
        /// Get the gridview row according to the data bound item
        /// </summary>
        /// <param name="dataBoundItem">data bound item</param>
        /// <returns></returns>
        public GridViewDataRowInfo GetRowByDataBoundItem(DataRow dataBoundItem)
        {
            GridViewDataRowInfo row = null;
            if (dataBoundItem != null)
            {
                foreach (GridViewDataRowInfo r in this.Rows)
                {
                    if ((r.DataBoundItem as DataRowView).Row.Equals(dataBoundItem))
                    {
                        row = r;
                        break;
                    }
                }
            }
            return row;
        }

        public event MouseEventHandler MyMouseWheel;

        #endregion

        #region Override Functions

        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadGridView";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }

        protected override void OnCreateCell(object sender, GridViewCreateCellEventArgs e)
        {
            base.OnCreateCell(sender, e);
            if (e.Column == null)
                return;
            if (_supportEmbeddedControls)
            {
                GridViewDataColumn column = e.Column as GridViewDataColumn;
                if (column != null
                    && column.FieldName == _columnFieldName
                    && column.OwnerTemplate.Equals(_columnTemplate)
                    && e.CellType == typeof(GridDataCellElement))
                {
                    e.Column.ReadOnly = true;
                    e.CellType = typeof(CSPropertyGridCellElement);
                }
            }
        }

        /// <summary>
        /// Remove the Set column HeaderTextAlignment code from OnCreateCell to OnDataBindingComplete,
        /// as if the code is in function onCreateCell, the column's width of childGrid of CSGrid is too narrow after the first time databinding.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataBindingComplete(GridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            foreach (GridViewDataColumn col in this.Columns)
            {
                col.HeaderTextAlignment = ContentAlignment.MiddleLeft;
            }
        }

        protected override void OnCellValueChanged(object sender, GridViewCellEventArgs e)
        {
            base.OnCellValueChanged(sender, e);

            //if (_supportEmbeddedControls
            //    && e.Column.OwnerTemplate.Equals(_columnTemplate)
            //    && (e.Column as GridViewDataColumn).FieldName.Equals(_columnFieldName))
            //{
            //    CSDataRow dr = (e.Row.DataBoundItem as DataRowView).Row as CSDataRow;
            //    if (dr.EmbeddedCtlPara.CtlCell != null)
            //    {
            //        dr.EmbeddedCtlPara.CtlCell.SetValue2EmbeddedControls(e.Value);
            //    }
            //}
        }

        protected override void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            if (sender is GridDataCellElement)
            {



                //{set the show delay for tooptip
                //RadElement element = sender as RadElement;

                //if (element == null)
                //{
                //    return;
                //}

                //ComponentBehavior behavior = (element.ElementTree.ComponentTreeHandler as IComponentTreeHandler).Behavior;
                //PropertyInfo toolTipProperty = typeof(ComponentBehavior).GetProperty("ToolTip", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty);
                //object toolTip = toolTipProperty.GetValue(behavior, null);
                //Type radToolTipType = toolTip.GetType();

                //PropertyInfo automaticProperty = radToolTipType.GetProperty("AutomaticDelay");
                //PropertyInfo autoPopDelayProperty = radToolTipType.GetProperty("AutoPopDelay");
                //PropertyInfo initialDelayProperty = radToolTipType.GetProperty("InitialDelay");
                //PropertyInfo reshowDelayProperty = radToolTipType.GetProperty("ReshowDelay");

                //int automaticDelay = 60000000;
                //int autoPopDelay = 60000000;
                //int initialDelay = 60000000;
                //int reshowDelay = 60000000;

                //automaticProperty.SetValue(toolTip, automaticDelay, null);
                //autoPopDelayProperty.SetValue(toolTip, autoPopDelay, null);
                //initialDelayProperty.SetValue(toolTip, initialDelay, null);
                //reshowDelayProperty.SetValue(toolTip, reshowDelay, null);
                //}

                //object value = (sender as GridDataCellElement).Value;
                //e.ToolTipText = value == null ? string.Empty : value.ToString().Trim();
                e.ToolTipText = (sender as GridDataCellElement).Text.Trim();




                string strColumn = ((Telerik.WinControls.UI.GridViewDataColumn)(((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).ColumnInfo)).FieldName;


                if (strColumn.ToUpper().Contains("ORDERMESSAGE"))
                {
                    try
                    {
                        string strMessageColumn = "OrderMessageXml";

                        if ((((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strMessageColumn].Value != null
                            && (((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strMessageColumn].Value != DBNull.Value
                            && (((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strMessageColumn].Value.ToString().Length > 0)
                        {
                            string strXmlOrderMessage = (((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strMessageColumn].Value.ToString();
                            DataTable dt = GetDataTable(strXmlOrderMessage);
                            string strTooltipText = "";
                            string strTooltipText1 = "";
                            string strTooltipText2 = "";
                            string strTooltipText3 = "";
                            foreach (DataRow dr in dt.Rows)
                            {

                                string strSubject = Convert.ToString(dr["Subject"]);
                                string strType = Convert.ToString(dr["@Type"]);
                                string strOwner = Convert.ToString(dr["UserName"]);
                                string strCreateDt = Convert.ToString(dr["CreateDt"]);
                                string strContext = Convert.ToString(dr["Context"]);
                                strContext = SplitLine(strSubject + ";  " + strOwner + ";  " + strCreateDt + ";  " + strContext);

                                //strTooltipText += strContext;
                                //strTooltipText += "\r\n\r\n";

                                if (strType == "a")
                                {
                                    if (strTooltipText1 == "")
                                    {
                                        strTooltipText1 = "<<--便笺-->>\r\n";
                                    }
                                    strTooltipText1 += "# " + strContext + "\r\n";
                                }
                                else if (strType == "b" || strType == "1" || strType == "2")
                                {
                                    if (strTooltipText2 == "")
                                    {
                                        strTooltipText2 = "<<--危急征象-->>\r\n";
                                    }
                                    strTooltipText2 += "# " + strContext + "\r\n";
                                }
                                else
                                {
                                    if (strTooltipText3 == "")
                                    {
                                        strTooltipText3 = "<<--标识-->>\r\n";
                                    }
                                    strTooltipText3 += "# " + strContext + "\r\n";
                                }
                            }

                            if (strTooltipText1 != "")
                            {
                                strTooltipText += strTooltipText1 + "\r\n";
                            }
                            if (strTooltipText2 != "")
                            {
                                strTooltipText += strTooltipText2 + "\r\n";
                            }
                            if (strTooltipText3 != "")
                            {
                                strTooltipText += strTooltipText3;
                            }

                            strTooltipText = strTooltipText.Trim("\r\n".ToCharArray());

                            e.ToolTipText = strTooltipText;
                        }
                    }
                    catch (Exception exx)
                    {

                    }
                }
                else
                    if (strColumn.ToUpper().Equals("RECEIVEOBJECT"))
                    {
                        try
                        {
                            if ((((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strColumn].Value != null
                                && (((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strColumn].Value != DBNull.Value
                                && (((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strColumn].Value.ToString().Length > 0)
                            {
                                string strText = ((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).Text.Trim();
                                e.ToolTipText = SplitLine(strText, 60, 8);
                            }
                        }
                        catch (Exception exx)
                        {

                        }
                    }
                    else if (this.Name.Equals("gridViewSignatureLog", StringComparison.OrdinalIgnoreCase))
                    {
                        if (sender is GridDataCellElement)
                        {
                            e.ToolTipText = Convert.ToString((((Telerik.WinControls.UI.GridCellElement)(sender as GridDataCellElement)).RowInfo).Cells[strColumn].Value);
                        }
                    }
            }
            else if (sender is GridHeaderCellElement)
            {
                e.ToolTipText = (sender as GridHeaderCellElement).Text.Trim();
            }

            base.OnToolTipTextNeeded(sender, e);
        }

        private string SplitLine(string strSource)
        {
            strSource = strSource.Replace("\r\n", " ");
            strSource = strSource.Replace("\n", " ");
            strSource = strSource.Replace("\r", " ");

            string strTarget = "";
            while (System.Text.Encoding.Default.GetByteCount(strSource) > 120)
            {
                //strTarget+=strSource.Substring(0,59);
                //strTarget+="\r\n";
                //string strTemp=strSource;
                //strSource=strTemp.Substring(60,strTemp.Length-60);

                string strSubstring = getStr(strSource, 120, "");
                strTarget += strSubstring;
                strTarget += "\r\n";

                string strTemp = strSource;
                strSource = strTemp.Remove(0, strSubstring.Length);



            }
            strTarget += strSource;
            return strTarget;
        }

        public string getStr(string s, int l, string endStr)
        {
            string temp = s.Substring(0, (s.Length < l) ? s.Length : l);

            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l)
            {
                //english
                temp = EnglishWord(s, l);

                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                //chinese
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l - endStr.Length)
                {
                    return temp + endStr;
                }
            }
            return endStr;
        }

        private string EnglishWord(string strSource, int nPosition)
        {

            string words = strSource;
            int position = nPosition;//要截取的位置
            bool flag = false;
            while (!flag && position <= words.Length - 1)
            {
                string str = words.Substring(position, 1);//要截取的位置的字符
                byte a = ASCIIEncoding.ASCII.GetBytes(str)[0];
                if (a < 65 || a > 122)
                {
                    flag = true;
                }
                else
                {
                    if (position >= strSource.Length - 1)
                    {
                        return strSource;
                    }
                    position += 1;
                }
            }
            if (position + 1 <= words.Length)
            {
                return words.Substring(0, position + 1);
            }
            else
            {
                return words;
            }
        }

        private string SplitLine(string strSource, int oneLineLength, int maxLines)
        {
            string strTarget = "";
            int lineCount = 0;
            bool bMaxLine = false;
            while (strSource.Length > oneLineLength)
            {
                strTarget += strSource.Substring(0, oneLineLength - 1);
                strTarget += "\r\n";
                string strTemp = strSource;
                strSource = strTemp.Substring(oneLineLength, strTemp.Length - oneLineLength);
                lineCount++;
                if (lineCount > maxLines)
                {
                    strTarget += "．．．";
                    bMaxLine = true;
                    break;
                }
            }
            if (!bMaxLine)
            {
                strTarget += strSource;
            }
            return strTarget;
        }

        private DataTable GetDataTable(string xmlStr)
        {
            if (string.IsNullOrEmpty(xmlStr))
            {
                return null;
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);

            XmlNodeList xlist = doc.SelectNodes("//LeaveMessage/Message");
            DataTable Dt = new DataTable();
            DataRow Dr;

            for (int i = 0; i < xlist.Count; i++)
            {
                Dr = Dt.NewRow();
                XmlElement xe = (XmlElement)xlist.Item(i);
                for (int j = 0; j < xe.Attributes.Count; j++)
                {
                    if (!Dt.Columns.Contains("@" + xe.Attributes[j].Name))
                        Dt.Columns.Add("@" + xe.Attributes[j].Name);
                    Dr["@" + xe.Attributes[j].Name] = xe.Attributes[j].Value;
                }
                if (!Dt.Columns.Contains("@IsCriticalSign")) Dt.Columns.Add("@IsCriticalSign");
                if (!Dt.Columns.Contains("@Type")) Dt.Columns.Add("@Type");
                if (xe.Attributes.Count == 0 || xe.Attributes["IsCriticalSign"] == null) Dr["@IsCriticalSign"] = "0";
                if (xe.Attributes.Count == 0 || xe.Attributes["Type"] == null)
                {
                    if (Dr["@IsCriticalSign"].ToString() == "0" || Dr["@IsCriticalSign"].ToString() == "-1")
                        Dr["@Type"] = "a";
                    else
                        Dr["@Type"] = "b";
                }
                for (int j = 0; j < xe.ChildNodes.Count; j++)
                {
                    if (!Dt.Columns.Contains(xe.ChildNodes.Item(j).Name))
                        Dt.Columns.Add(xe.ChildNodes.Item(j).Name);
                    Dr[xe.ChildNodes.Item(j).Name] = xe.ChildNodes.Item(j).InnerText;
                }
                Dt.Rows.Add(Dr);
            }
            Dt.AcceptChanges();
            DataView dv = Dt.DefaultView;
            dv.Sort = "CreateDt asc";
            DataTable dtCopy = dv.ToTable();


            return dtCopy;
        }
        private void CSGridView_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (this.FindForm() != null && e.CellElement != null)
                e.CellElement.Font = this.FindForm().Font;
        }

        private void CSGridView_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridView_CellFormatting(sender, e);

            if (e.CellElement.RowIndex < 0)
            {
                return;
            }
            GridViewDataColumn gvdc = e.CellElement.ColumnInfo as GridViewDataColumn;
            if (stringUserIDUserNameDic.ContainsKey(gvdc.FieldName))
            {
                DataTable dt = this.DataSource as DataTable;
                DataView dv = null;
                if (dt == null)
                {
                    dv = this.DataSource as DataView;
                    if (dv == null)
                        return;

                    dt = dv.Table;
                    if (dt == null)
                    {
                        return;
                    }
                }
                if (dt.Columns.Contains(stringUserIDUserNameDic[gvdc.FieldName]))
                {
                    DataRowView drv = (e.CellElement.RowInfo.DataBoundItem as DataRowView);
                    if (drv != null)
                    {
                        string userName = Convert.ToString(drv.Row[stringUserIDUserNameDic[gvdc.FieldName]]);
                        if (!string.IsNullOrEmpty(userName))
                        {
                            e.CellElement.Text = userName;
                        }
                    }
                }
            }
        }


        protected virtual void GridView_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (HongKongSupplementaryCharacterSet)
            {
                if (((Telerik.WinControls.UI.GridViewDataColumn)(e.CellElement.ColumnInfo)).FieldName.ToUpper().Contains("LOCALNAME"))
                {
                    e.CellElement.Font = _HkSupplementaryFont;
                }
            }
        }

        private void CSGridView_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement != null && UseFixedRowHeight)
            {
                e.RowElement.RowInfo.Height = 20;
            }
        }

        protected void CSGridView_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && _allowColumnSelection && this.Rows.Count > 0)
            {
                this.ClearSelection();
                this.CurrentRow = this.Rows[e.RowIndex + 1];
                this.CurrentColumn = this.Columns[e.ColumnIndex];


                foreach (GridViewRowInfo gvri in this.Rows)
                {
                    gvri.Cells[e.ColumnIndex].Selected = true;
                }
            }
        }

        protected void CSGridView_CellMouseMove(object sender, MouseEventArgs e)
        {
            //  e.
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (MyMouseWheel != null)
            {
                MyMouseWheel(this, e);
            }

            //if(base.MouseWheel != null)
            //{
            //    base.MouseWheel(this, e);
            //}

            base.OnMouseWheel(e);
        }

        private void CSGridView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= this.Columns.Count || _allowImageColumnResize)
                return;

            // prevent the image column resizing event.
            GridViewImageColumn imgcol = this.Columns[e.ColumnIndex] as GridViewImageColumn;
            if (imgcol != null)
            {
                e.Cancel = true;
            }
        }

        #endregion

    }

    public class CSGridViewLocalizationProvider : RadGridLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadGridStringId.NoDataText:
                    return string.Empty;
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }

    public class CSGridViewComboBoxColumn : GridViewComboBoxColumn
    {
        /// <summary>
        /// Use escape key to clear the GridViewComboBox's value and text
        /// </summary>
        [DefaultValue(false)]
        public bool EscapeKeyClearData { get; set; }
    }

    public class CustomGridBehavior : BaseGridBehavior
    {
        public override bool ProcessKey(KeyEventArgs keys)
        {
            if (keys.KeyCode == Keys.Escape && GridControl.CurrentRow is GridViewDataRowInfo)
            {
                if (GridControl.CurrentColumn is CSGridViewComboBoxColumn && (GridControl.CurrentColumn as CSGridViewComboBoxColumn).EscapeKeyClearData && GridControl.CurrentCell != null)
                {

                    string fieldName = (GridControl.CurrentCell.ColumnInfo as GridViewDataColumn).FieldName;
                    if (keys.KeyCode == Keys.Escape && GridControl.CurrentCell != null && GridControl.CurrentCell.Editor != null)
                    {
                        GridControl.CurrentCell.Editor.Value = " ";
                        return true;
                    }
                }
            }
            return base.ProcessKey(keys);
        }
    }
}