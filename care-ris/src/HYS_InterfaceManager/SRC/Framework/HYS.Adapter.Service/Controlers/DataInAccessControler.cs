using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Translation;

namespace HYS.Adapter.Service.Controlers
{
    public class DataInAccessControler : DataAccessControler
    {
        public bool ProcessInboundData(IInboundRule rule, DataSet data)
        {
            if (Program.ConfigMgt.Config.EnableTransaction) return ProcessInboundDataWithTransaction(rule, data);

            if (data == null || rule == null) return false;

            if (data.Tables.Count < 1)
            {
                Program.Log.Write(LogType.Warning, "No DataTable is found in the DataSet.");
                return true;
            }

            DataTable dt = data.Tables[0];
            if (dt == null || dt.Rows == null)
            {
                Program.Log.Write(LogType.Warning, "A <null> DataTable is found in the DataSet.");
                return true;
            }

            Program.Log.Write("Number of rows in inbound DataSet: " + dt.Rows.Count.ToString());

            bool result = false;
            //SafeDBConnection cn = SafeDBConnection.Instance;
            OleDbConnection cnn = new OleDbConnection(Program.ConfigMgt.Config.DataDBConnection);

            try
            {
                MappingItem[] qrList = rule.GetQueryResultItems();
                if (qrList == null || qrList.Length < 1)
                {
                    Program.Log.Write(LogType.Warning, "No query result mapping item found in inbound rule. ");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //OleDbCommand cmd = new OleDbCommand(GetSPName(rule), cn.Connection);
                        OleDbCommand cmd = new OleDbCommand(GetSPName(rule), cnn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        List<MappingItem> redundancyParamList = new List<MappingItem>();
                        foreach (MappingItem qr in qrList)
                        {
                            // following logic is according to 
                            // RuleControl.GetSP<TC, TR>(string interfaceName, InboundRule<TC, TR> rule)

                            bool redundancy = false;
                            foreach (MappingItem mpi in redundancyParamList)
                            {
                                if (mpi.SourceField == qr.SourceField)
                                {
                                    redundancy = true;
                                    break;
                                }
                            }
                            if (redundancy) continue;
                            redundancyParamList.Add(qr);

                            if (qr.Translating.Type == TranslatingType.FixValue) continue;

                            OleDbParameter param = new OleDbParameter();
                            param.ParameterName = "@" + qr.SourceField;
                            param.Direction = ParameterDirection.Input;
                            param.OleDbType = OleDbType.VarWChar;

                            //string strValue = dr[qr.SourceField].ToString();

                            string strValue = "";
                            if (dt.Columns.Contains(qr.SourceField))
                            {
                                strValue = dr[qr.SourceField].ToString();
                            }
                            else
                            {
                                Program.Log.Write(LogType.Warning, "Field " + qr.SourceField + " cannot be found in inbound dataset. This field will be set as empty string when calling storage procedure.");
                            }

                            if (Program.ConfigMgt.Config.Composing.Enable)
                            {
                                ComposingRuleItem comRule = Program.ConfigMgt.Config.Composing.GetComposingRule(qr.GWDataDBField);
                                if (comRule != null)
                                {
                                    List<string> sourceList = new List<string>();
                                    foreach (GWDataDBField sourceField in comRule.FromFields)
                                    {
                                        string sourceValue = "";
                                        foreach (MappingItem sItem in qrList)
                                        {
                                            if (sItem.GWDataDBField.Table == sourceField.Table &&
                                                sItem.GWDataDBField.FieldName == sourceField.FieldName)
                                            {
                                                if (dt.Columns.Contains(sItem.SourceField))
                                                {
                                                    sourceValue = dr[sItem.SourceField].ToString();

                                                    if (sItem.Translating.Type == TranslatingType.DefaultValue &&
                                                        sourceValue.Length < 1)
                                                    {
                                                        sourceValue = sItem.Translating.ConstValue;
                                                    }
                                                }
                                                else
                                                {
                                                    if (sItem.Translating.Type == TranslatingType.FixValue)
                                                    {
                                                        sourceValue = sItem.Translating.ConstValue;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        sourceList.Add(sourceValue);
                                    }
                                    strValue = comRule.Compose(sourceList.ToArray(), Program.Log);
                                }
                            }

                            if (Program.ConfigMgt.Config.Replacement.Enable)
                            {
                                ReplacementRuleItem repRule = Program.ConfigMgt.Config.Replacement.GetReplacementRule(qr.GWDataDBField);
                                if (repRule != null) strValue = repRule.RegularExpression.Replace(strValue, Program.Log);
                            }

                            if (Program.ConfigMgt.Config.Chinese2Pinyin.Enable)
                            {
                                Chinese2PinyinRuleItem item = Program.ConfigMgt.Config.Chinese2Pinyin.GetChinese2PinyinRule(qr.GWDataDBField);
                                if (item != null)
                                {
                                    strValue = ChineseCode.Convert(item.ConvertType, strValue, Program.Log);
                                    strValue = PinyinFactory.GetInstance(item.Type).ConvertName(strValue, Program.Log);
                                }
                            }

                            param.Value = strValue;     //.Replace("'", "''"); // db parameter will handle this
                            cmd.Parameters.Add(param);
                        }

                        OleDbParameter paramRet = new OleDbParameter();
                        paramRet.ParameterName = "@" + RuleControl.ReturnValueParameterName;
                        paramRet.Direction = ParameterDirection.Output;
                        paramRet.OleDbType = OleDbType.Integer;
                        cmd.Parameters.Add(paramRet);

                        //cn.Open();
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        //cn.Close();
                        cnn.Close();

                        string retMessage;
                        if ((int)paramRet.Value == 0)
                        {
                            retMessage = "Data rejected by redundancy checking.";
                        }
                        else
                        {
                            retMessage = "Data inserted.";
                        }

                        Program.Log.Write("Process inbound data success. SP Name " + GetSPName(rule) + ", " + retMessage);

                        result = true;
                    }
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Warning, "Process inbound data failed.");
                DumpDataBaseError(err, rule, data);
            }
            finally
            {
                //if (!result) cn.Close();
                if (!result) cnn.Close();
            }

            return result;
        }

        private bool ProcessInboundDataWithTransaction(IInboundRule rule, DataSet data)
        {
            if (data == null || rule == null) return false;

            Program.Log.Write(LogType.Warning, "Process inbound DataSet with transaction using default isolation level.");

            if (data.Tables.Count < 1)
            {
                Program.Log.Write(LogType.Warning, "No DataTable is found in the DataSet.");
                return true;
            }

            DataTable dt = data.Tables[0];
            if (dt == null || dt.Rows == null)
            {
                Program.Log.Write(LogType.Warning, "A <null> DataTable is found in the DataSet.");
                return true;
            }

            Program.Log.Write("Number of rows in inbound DataSet: " + dt.Rows.Count.ToString());

            bool result = false;
            //SafeDBConnection cn = SafeDBConnection.Instance;
            OleDbConnection cnn = new OleDbConnection(Program.ConfigMgt.Config.DataDBConnection);
            OleDbTransaction trans = null;

            try
            {
                //cn.Open();
                cnn.Open();

                MappingItem[] qrList = rule.GetQueryResultItems();
                if (qrList == null || qrList.Length < 1)
                {
                    Program.Log.Write(LogType.Warning, "No query result mapping item found in inbound rule. ");
                }
                else
                {
                    //trans = cn.Connection.BeginTransaction();
                    trans = cnn.BeginTransaction();

                    foreach (DataRow dr in dt.Rows)
                    {
                        //OleDbCommand cmd = new OleDbCommand(GetSPName(rule), cn.Connection);
                        OleDbCommand cmd = new OleDbCommand(GetSPName(rule), cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = trans;

                        List<MappingItem> redundancyParamList = new List<MappingItem>();
                        foreach (MappingItem qr in qrList)
                        {
                            // following logic is according to 
                            // RuleControl.GetSP<TC, TR>(string interfaceName, InboundRule<TC, TR> rule)

                            bool redundancy = false;
                            foreach (MappingItem mpi in redundancyParamList)
                            {
                                if (mpi.SourceField == qr.SourceField)
                                {
                                    redundancy = true;
                                    break;
                                }
                            }
                            if (redundancy) continue;
                            redundancyParamList.Add(qr);

                            if (qr.Translating.Type == TranslatingType.FixValue) continue;

                            OleDbParameter param = new OleDbParameter();
                            param.ParameterName = "@" + qr.SourceField;
                            param.Direction = ParameterDirection.Input;
                            param.OleDbType = OleDbType.VarWChar;

                            //string strValue = dr[qr.SourceField].ToString();

                            string strValue = "";
                            if (dt.Columns.Contains(qr.SourceField))
                            {
                                strValue = dr[qr.SourceField].ToString();
                            }
                            else
                            {
                                Program.Log.Write(LogType.Warning, "Field " + qr.SourceField + " cannot be found in inbound dataset. This field will be set as empty string when calling storage procedure.");
                            }

                            if (Program.ConfigMgt.Config.Composing.Enable)
                            {
                                ComposingRuleItem comRule = Program.ConfigMgt.Config.Composing.GetComposingRule(qr.GWDataDBField);
                                if (comRule != null)
                                {
                                    List<string> sourceList = new List<string>();
                                    foreach (GWDataDBField sourceField in comRule.FromFields)
                                    {
                                        string sourceValue = "";
                                        foreach (MappingItem sItem in qrList)
                                        {
                                            if (sItem.GWDataDBField.Table == sourceField.Table &&
                                                sItem.GWDataDBField.FieldName == sourceField.FieldName)
                                            {
                                                if (dt.Columns.Contains(sItem.SourceField))
                                                {
                                                    sourceValue = dr[sItem.SourceField].ToString();

                                                    if (sItem.Translating.Type == TranslatingType.DefaultValue &&
                                                        sourceValue.Length < 1)
                                                    {
                                                        sourceValue = sItem.Translating.ConstValue;
                                                    }
                                                }
                                                else
                                                {
                                                    if (sItem.Translating.Type == TranslatingType.FixValue)
                                                    {
                                                        sourceValue = sItem.Translating.ConstValue;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        sourceList.Add(sourceValue);
                                    }
                                    strValue = comRule.Compose(sourceList.ToArray(), Program.Log);
                                }
                            }

                            if (Program.ConfigMgt.Config.Replacement.Enable)
                            {
                                ReplacementRuleItem repRule = Program.ConfigMgt.Config.Replacement.GetReplacementRule(qr.GWDataDBField);
                                if (repRule != null) strValue = repRule.RegularExpression.Replace(strValue, Program.Log);
                            }

                            if (Program.ConfigMgt.Config.Chinese2Pinyin.Enable)
                            {
                                Chinese2PinyinRuleItem item = Program.ConfigMgt.Config.Chinese2Pinyin.GetChinese2PinyinRule(qr.GWDataDBField);
                                if (item != null)
                                {
                                    strValue = ChineseCode.Convert(item.ConvertType, strValue, Program.Log);
                                    strValue = PinyinFactory.GetInstance(item.Type).ConvertName(strValue, Program.Log);
                                }
                            }

                            param.Value = strValue;     //.Replace("'", "''"); // db parameter will handle this
                            cmd.Parameters.Add(param);
                        }

                        OleDbParameter paramRet = new OleDbParameter();
                        paramRet.ParameterName = "@" + RuleControl.ReturnValueParameterName;
                        paramRet.Direction = ParameterDirection.Output;
                        paramRet.OleDbType = OleDbType.Integer;
                        cmd.Parameters.Add(paramRet);

                        cmd.ExecuteNonQuery();

                        string retMessage;
                        if ((int)paramRet.Value == 0)
                        {
                            retMessage = "Data rejected by redundancy checking.";
                        }
                        else
                        {
                            retMessage = "Data inserted.";
                        }

                        Program.Log.Write("Process inbound data success. SP Name " + GetSPName(rule) + ", " + retMessage);
                    }

                    trans.Commit();
                    result = true;
                }
            }
            catch (Exception err)
            {
                if (trans != null) trans.Rollback();

                Program.Log.Write(LogType.Warning, "Process inbound data failed.");
                DumpDataBaseError(err, rule, data);
            }
            finally
            {
                //cn.Close();
                cnn.Close();
                if (trans != null) trans.Dispose();
            }

            return result;
        }
    }
}
