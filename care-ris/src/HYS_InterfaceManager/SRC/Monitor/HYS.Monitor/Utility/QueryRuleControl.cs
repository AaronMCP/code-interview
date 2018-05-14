using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Monitor.Objects;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.Adapter.Monitor.Utility
{
    public class QueryRuleControl
    {
        #region Filter Script
        public static string GetFilterString()
        {
            return GetFilterString(null,null);
        }

        public static string GetFilterString(XCollection<QueryCriteriaItem> filterItemList) 
        {
            return GetFilterString(null, filterItemList);
        }

        public static string GetFilterString(SimpleQuery queryInfo)
        {
            return GetFilterString(queryInfo, null);
        }

        private static string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
        public static string GetFilterString(SimpleQuery queryInfo, XCollection<QueryCriteriaItem> filterItemList)
        {
            //Get interface name from configuration file
            string indexTablename = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index) + "]";
            string patientTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]";
            string orderTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order) + "]";
            string reportTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report) + "]";

            #region Query Head
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);
            //sb.AppendLine("");

            //sb.AppendLine("SET ANSI_NULLS ON");
            //sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            //sb.AppendLine("");

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index) + "]') AND type in (N'U'))");

            sb.AppendLine("SELECT");
            sb.AppendLine("\t" + indexTablename + ".* FROM " + indexTablename);
            sb.AppendLine("\tLEFT JOIN " + patientTableName + " ON " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.p_DATA_ID) + " = " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.i_IndexGuid));
            sb.AppendLine("\tLEFT JOIN " + orderTableName + " ON " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.o_DATA_ID) + " = " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.i_IndexGuid));
            sb.AppendLine("\tLEFT JOIN " + reportTableName + " ON " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.r_DATA_ID) + " = " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.i_IndexGuid));
            #endregion

            #region Query Condition
            if (queryInfo != null || filterItemList != null)
            {
                StringBuilder sbWhere = new StringBuilder();

                if (queryInfo != null)
                {
                    #region Simple Query
                    if (queryInfo.EventType.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.EventType.Fieldname) + " like " + "\'%" + queryInfo.EventType.FieldValue.Replace("'", "''") + "%\'");
                    }
                    if (queryInfo.StartTime.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.StartTime.Fieldname) + " >= " + "\'" + queryInfo.StartTime.FieldValue.Replace("'", "''") + "\'");
                    }
                    if (queryInfo.EndTime.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.EndTime.Fieldname) + " <= " + "\'" + queryInfo.EndTime.FieldValue.Replace("'", "''") + "\'");
                    }
                    if (queryInfo.PatientID.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.PatientID.Fieldname) + " like " + "\'%" + queryInfo.PatientID.FieldValue.Replace("'", "''") + "%\'");
                    }
                    if (queryInfo.PatientName.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.PatientName.Fieldname) + " like " + "\'%" + queryInfo.PatientName.FieldValue.Replace("'", "''") + "%\'");
                    }
                    if (queryInfo.OrderNo.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.OrderNo.Fieldname) + " like " + "\'%" + queryInfo.OrderNo.FieldValue.Replace("'", "''") + "%\'");
                    }
                    if (queryInfo.AccessionNo.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.AccessionNo.Fieldname) + " like " + "\'%" + queryInfo.AccessionNo.FieldValue.Replace("'", "''") + "%\'");
                    }
                    if (queryInfo.StudyInstanceUID.Enable)
                    {
                        sbWhere.AppendLine("\tAND " + GWDBControl.GetFullFieldName(interfaceName, queryInfo.StudyInstanceUID.Fieldname) + " like " + "\'%" + queryInfo.StudyInstanceUID.FieldValue.Replace("'", "''") + "%\'");
                    }
                    #endregion
                }
                else
                {
                    #region Advanced Query
                    foreach (QueryCriteriaItem filterItem in filterItemList)
                    {
                        if (filterItem.Type == QueryCriteriaType.Or)
                        {
                            sbWhere.Append("\t OR ");
                        }
                        else
                        {
                            sbWhere.Append("\tAND ");
                        }

                        string fname = GWDBControl.GetFullFieldName(interfaceName, filterItem.GWDataDBField);
                        switch (filterItem.Operator)
                        {
                            case QueryCriteriaOperator.Like:
                                {
                                    sbWhere.AppendLine(fname + " LIKE " + "\'%" + filterItem.Translating.ConstValue + "%\'");
                                    break;
                                }
                            case QueryCriteriaOperator.Equal:
                                {
                                    sbWhere.AppendLine(fname + " = " + "\'" + filterItem.Translating.ConstValue + "\'");
                                    break;
                                }
                            case QueryCriteriaOperator.NotEqual:
                                {
                                    sbWhere.AppendLine("(" + fname + " <> " + "\'" + filterItem.Translating.ConstValue + "\'" + " OR " + fname + " IS NULL)");
                                    break;
                                }
                            case QueryCriteriaOperator.LargerThan:
                                {
                                    sbWhere.AppendLine(fname + " > " + "\'" + filterItem.Translating.ConstValue + "\'");
                                    break;
                                }
                            case QueryCriteriaOperator.SmallerThan:
                                {
                                    sbWhere.AppendLine(fname + " < " + "\'" + filterItem.Translating.ConstValue + "\'");
                                    break;
                                }
                            case QueryCriteriaOperator.EqualLargerThan:
                                {
                                    sbWhere.AppendLine(fname + " >= " + "\'" + filterItem.Translating.ConstValue + "\'");
                                    break;
                                }
                            case QueryCriteriaOperator.EqualSmallerThan:
                                {
                                    sbWhere.AppendLine(fname + " <= " + "\'" + filterItem.Translating.ConstValue + "\'");
                                    break;
                                }
                        }
                    }
                    #endregion
                }

                if (sbWhere.Length > 4)
                {
                    sbWhere.Remove(1, 4);
                    sb.AppendLine("WHERE");
                    sb.AppendLine(sbWhere.ToString());
                }
            }
            #endregion

            sb.AppendLine(" ORDER BY " + GWDataDBField.i_DataDateTime.GetFullFieldName(interfaceName));

            return sb.ToString();
        }
        #endregion

        #region Select Data Tables Script
        //All matched tables
        public static string GetTablesString(string guid) {
            string patientTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]";
            string orderTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order) + "]";
            string reportTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report) + "]";

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);
            //sb.AppendLine("");

            //sb.AppendLine("SET ANSI_NULLS ON");
            //sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            //sb.AppendLine("");

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]') AND type in (N'U'))");
            sb.AppendLine("SELECT " + patientTableName + ".* FROM " + patientTableName);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.p_DATA_ID) + " = " + "\'" + guid + "\'");
            sb.AppendLine("");

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]') AND type in (N'U'))");
            sb.AppendLine("SELECT " + orderTableName + ".* FROM " + orderTableName);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.o_DATA_ID) + " = " + "\'" + guid + "\'");
            sb.AppendLine("");

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]') AND type in (N'U'))");
            sb.AppendLine("SELECT " + reportTableName + ".* FROM " + reportTableName);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.r_DATA_ID) + " = " + "\'" + guid + "\'");
            sb.AppendLine("");

            return sb.ToString();
        }
        #endregion

        #region Select Record with query condition
        //Just one table form patient, order or report
        public static string GetSelectRecordString(string guid, GWDataDBTable table)
        {
            string tableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]";

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]') AND type in (N'U'))");
            sb.AppendLine("SELECT " + " * FROM " + tableName);
            sb.AppendLine("WHERE");

            #region Get Field Name
            string fieldname = "";
            switch (table)
            {
                case GWDataDBTable.Index:
                    {
                        fieldname = GWDataDBField.i_IndexGuid.FieldName;
                        break;
                    }
                case GWDataDBTable.Patient:
                    {
                        fieldname = GWDataDBField.p_DATA_ID.FieldName;
                        break;
                    }
                case GWDataDBTable.Order:
                    {
                        fieldname = GWDataDBField.o_DATA_ID.FieldName;
                        break;
                    }
                case GWDataDBTable.Report:
                    {
                        fieldname = GWDataDBField.r_DATA_ID.FieldName;
                        break;
                    }
            }
            #endregion

            sb.AppendLine("[" + fieldname + "] = " + "\'" + guid + "\'");

            return sb.ToString();
        }
        #endregion

        #region Select TableHeaders Script
        public static string GetHeadersQueryString(GWDataDBTable table) {
            string tableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]";
            
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]') AND type in (N'U'))");
            sb.AppendLine("SELECT TOP 0" + " * FROM " + tableName);

            return sb.ToString();
        }
        #endregion

        #region Delete Script
        public static string GetDeleteString(string guid) {
            string indexTablename = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index) + "]";
            string patientTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]";
            string orderTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order) + "]";
            string reportTableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report) + "]";

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report) + "]') AND type in (N'U'))");
            sb.AppendLine("DELETE" + " FROM " + reportTableName);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.r_DATA_ID) + " = " + "\'" + guid + "\'");
            sb.AppendLine("");

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order) + "]') AND type in (N'U'))");
            sb.AppendLine("DELETE" + " FROM " + orderTableName);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.o_DATA_ID) + " = " + "\'" + guid + "\'");
            sb.AppendLine("");

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient) + "]') AND type in (N'U'))");
            sb.AppendLine("DELETE" + " FROM " + patientTableName);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.p_DATA_ID) + " = " + "\'" + guid + "\'");
            sb.AppendLine("");

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index) + "]') AND type in (N'U'))");
            sb.AppendLine("DELETE" + " FROM " + indexTablename);
            sb.AppendLine("WHERE " + GWDBControl.GetFullFieldName(interfaceName, GWDataDBField.i_IndexGuid) + " = " + "\'" + guid + "\'");

            return sb.ToString();
        }
        #endregion

        #region Update Script
        public static string GetUpdateString( DataGridViewColumnCollection columns, DataGridViewRow row, GWDataDBTable table) {
            string tableName = "[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]";

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]') AND type in (N'U'))");
            sb.AppendLine("UPDATE " + tableName);
            sb.AppendLine("SET ");

            #region Set expression part of update script
            StringBuilder sbExp = new StringBuilder();
            int i = 0;
            foreach (DataGridViewColumn column in columns) {
                sbExp.AppendLine("[" + column.HeaderText + "] = " + "N\'" + row.Cells[i].Value.ToString().Replace("'", "''") + "\',");
                i++;
            }
            sbExp.Remove(sbExp.Length-3,3).ToString();    
            string strExp = sbExp.ToString();
            #endregion
            sb.AppendLine(strExp);

            sb.AppendLine("WHERE");
            sb.AppendLine("[" + columns[0].HeaderText + "] = " + "\'" + row.Cells[0].Value.ToString().Replace("'", "''") + "\'");

            return sb.ToString();
        }
        #endregion

        #region Add Script
        public static string GetAddString(DataGridViewColumnCollection columns, DataGridViewRow row, GWDataDBTable table)
        {
            if (table == GWDataDBTable.None) return null;

            string tablename = "[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]";
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("USE " + GWDataDB.DataBaseName);

            //sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + GWDataDB.GetTableName(interfaceName, table) + "]') AND type in (N'U'))");
            sb.AppendLine("INSERT INTO " + tablename);

            #region Fields
            StringBuilder sbField = new StringBuilder();
            foreach (DataGridViewColumn column in columns)
            {
                sbField.Append("[" + column.HeaderText + "]" + ",");
            }

            string strField = sbField.ToString();
            if (strField.Length > 0) strField = strField.TrimEnd(',');
            sb.AppendLine("			( " + strField + " )");
            #endregion
            sb.AppendLine("			VALUES");
            #region Values
            StringBuilder sbValue = new StringBuilder();
            foreach (DataGridViewCell cell in row.Cells)
            {
                sbValue.Append("N\'" + cell.Value.ToString().Replace("'", "''") + "\'" + ",");
            }
            string strValue = sbValue.ToString();
            if (strValue.Length > 0) strValue = strValue.TrimEnd(',');
            sb.AppendLine("			( " + strValue + " )");
            #endregion

            return sb.ToString();
        }
        #endregion
    }
}
