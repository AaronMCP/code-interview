using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Translation;

namespace HYS.Adapter.Service.Controlers
{
    public class DataOutAccessControler : DataAccessControler
    {
        public DataSet PrepareOutboundData(IOutboundRule rule, DataSet criteria)
        {
            if (rule == null) return null;

            bool result = false;
            DataSet resultData = null;
            //SafeDBConnection cn = SafeDBConnection.Instance;
            OleDbConnection cnn = new OleDbConnection(Program.ConfigMgt.Config.DataDBConnection);

            try
            {
                //OleDbCommand cmd = new OleDbCommand(GetSPName(rule), cn.Connection);
                OleDbCommand cmd = new OleDbCommand(GetSPName(rule), cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (criteria != null && criteria.Tables.Count > 0)
                {
                    Program.Log.Write("Query criteria (" + criteria.Tables[0].Rows.Count.ToString() + ": ");

                    MappingItem[] qcList = rule.GetQueryCriteriaItems();
                    if (qcList == null || qcList.Length < 1)
                    {
                        Program.Log.Write(LogType.Warning, "No query criteria mapping item found in outbound rule. ");
                    }
                    else
                    {
                        foreach (DataRow dr in criteria.Tables[0].Rows)
                        {
                            foreach (MappingItem qc in qcList)
                            {
                                // following logic is according to 
                                // public static string GetSP<TC, TR>(string interfaceName, OutboundRule<TC, TR> rule)

                                if (qc.Translating.Type == TranslatingType.FixValue) continue;

                                OleDbParameter param = new OleDbParameter();
                                param.ParameterName = "@" + qc.SourceField;
                                param.OleDbType = OleDbType.VarWChar;
                                param.Direction = ParameterDirection.Input;
                                param.Value = dr[qc.SourceField].ToString();    //.Replace("'", "''"); // db parameter will handle this
                                cmd.Parameters.Add(param);

                                Program.Log.Write(qc.SourceField + " = " + param.Value);
                            }
                        }
                    }

                    Program.Log.Write("");
                }

                resultData = new DataSet();
                OleDbDataAdapter ad = new OleDbDataAdapter();
                ad.SelectCommand = cmd;

                //cn.Open();
                cnn.Open();
                int count = ad.Fill(resultData);
                //cn.Close();
                cnn.Close();

                if (resultData.Tables.Count > 0 &&
                    (Program.ConfigMgt.Config.Composing.Enable ||
                    Program.ConfigMgt.Config.Replacement.Enable ||
                    Program.ConfigMgt.Config.Chinese2Pinyin.Enable)||
                    Program.ConfigMgt.Config.L3KanJiReplacement.Enable)
                {
                    DataTable dt = resultData.Tables[0];
                    MappingItem[] qrList = rule.GetQueryResultItems();
                    if (qrList == null || qrList.Length < 1)
                    {
                        Program.Log.Write(LogType.Warning, "No query result mapping item found in outbound rule. ");
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            foreach (MappingItem qr in qrList)
                            {
                                //string strValue = dr[qr.TargetField].ToString();
                                #region Composing
                                if (Program.ConfigMgt.Config.Composing.Enable)
                                {
                                    
                                    ComposingRuleItem comRule = Program.ConfigMgt.Config.Composing.GetComposingRule(qr.GWDataDBField);
                                    if (comRule != null && dt.Columns.Contains(qr.TargetField))
                                    {
                                        string strValue = dr[qr.TargetField].ToString();

                                        List<string> sourceList = new List<string>();
                                        foreach (GWDataDBField sourceField in comRule.FromFields)
                                        {
                                            string sourceValue = "";
                                            foreach (MappingItem sItem in qrList)
                                            {
                                                if (sItem.GWDataDBField.Table == sourceField.Table &&
                                                    sItem.GWDataDBField.FieldName == sourceField.FieldName)
                                                {
                                                    if (dt.Columns.Contains(sItem.TargetField))
                                                        sourceValue = dr[sItem.TargetField].ToString();
                                                    break;
                                                }
                                            }
                                            sourceList.Add(sourceValue);
                                        }

                                        strValue = comRule.Compose(sourceList.ToArray(), Program.Log);
                                        dr[qr.TargetField] = strValue;
                                    }
                                }
                                #endregion

                                #region Regular Replacement
                                if (Program.ConfigMgt.Config.Replacement.Enable)
                                {
                                    ReplacementRuleItem item = Program.ConfigMgt.Config.Replacement.GetReplacementRule(qr.GWDataDBField);
                                    if (item != null && dt.Columns.Contains(qr.TargetField))
                                    {
                                        string strValue = dr[qr.TargetField].ToString();
                                        strValue = item.RegularExpression.Replace(strValue, Program.Log);
                                        dr[qr.TargetField] = strValue;
                                    }
                                }
                                #endregion

                                #region Chinese2Pinyin
                                if (Program.ConfigMgt.Config.Chinese2Pinyin.Enable)
                                {
                                    Chinese2PinyinRuleItem item = Program.ConfigMgt.Config.Chinese2Pinyin.GetChinese2PinyinRule(qr.GWDataDBField);
                                    if (item != null && dt.Columns.Contains(qr.TargetField))
                                    {
                                        string strValue = dr[qr.TargetField].ToString();
                                        strValue = ChineseCode.Convert(item.ConvertType, strValue, Program.Log);
                                        strValue = PinyinFactory.GetInstance(item.Type).ConvertName(strValue, Program.Log);
                                        dr[qr.TargetField] = strValue;
                                    }
                                }
                                #endregion
                                //dr[qr.TargetField] = strValue;

                                #region Level 3 KanJi Replacement
                                if (Program.ConfigMgt.Config.L3KanJiReplacement.Enable)
                                {
                                    Level3KanJiReplacementRuleItem item = Program.ConfigMgt.Config.L3KanJiReplacement.GetLevel3KanJiReplacementRule(qr.GWDataDBField);
                                    if (item != null && dt.Columns.Contains(qr.TargetField))
                                    {
                                        string strValue = dr[qr.TargetField].ToString();
                                        strValue = item.Replace(strValue, Program.Log);
                                        dr[qr.TargetField] = strValue;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }

                Program.Log.Write("Prepare outbound data success. SP Name: " + GetSPName(rule) + ", Number of rows affected: " + count.ToString());

                result = true;
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Warning, "Prepare outbound data failed.");
                DumpDataBaseError(err, rule, criteria);
            }
            finally
            {
                //if (!result) cn.Close();
                if (!result) cnn.Close();
            }

            return resultData;
        }

        public bool SetProcessFlag(string interfaceName, string[] guidList)
        {
            if (guidList == null || interfaceName == null) return false;

            if (interfaceName.Length < 1)
            {
                Program.Log.Write(LogType.Warning, "Adapter interface name is a empty string.");
                return false;
            }

            if (guidList.Length<1)
            {
                Program.Log.Write(LogType.Warning, "Nothing in the GUID List.");
                return true;
            }

            string strSql = RuleControl.GetSetProcessFlagSQL(interfaceName, guidList);

            bool result = false;
            //SafeDBConnection cn = SafeDBConnection.Instance;
            OleDbConnection cnn = new OleDbConnection(Program.ConfigMgt.Config.DataDBConnection);

            try
            {
                //OleDbCommand cmd = new OleDbCommand(strSql,cn.Connection);
                OleDbCommand cmd = new OleDbCommand(strSql, cnn);
                cmd.CommandType = CommandType.Text;

                //cn.Open();
                cnn.Open();
                int count = cmd.ExecuteNonQuery();
                //cn.Close();
                cnn.Close();

                Program.Log.Write("Update process flag success. Interface Name: " + interfaceName + ", Number of rows affected: " + count.ToString());

                result = true;
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Warning, "Update process flag failed.");
                DumpDataBaseError(err, strSql);
            }
            finally
            {
                //if (!result) cn.Close();
                if (!result) cnn.Close();
            }

            return result;
        }
    }
}
