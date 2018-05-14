using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;

using System.Collections;
using Server.Utilities.LogFacility;
using System.Diagnostics;

namespace Server.DAO.Templates.Impl
{
    class OracleProvider : AbstractDBProvider
    {
        LogManagerForServer lm = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        
        public override bool AddNewLeafNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID, string strTemplateGuid, string strGender, ReportTemplateModel model)
        {
            RisDAL dataAccess = new RisDAL();
            model.CheckItemName = model.CheckItemName.Replace("'", "''");
            model.DoctorAdvice = model.DoctorAdvice.Replace("'", "''");
            //model.TechInfo  = model.TechInfo.Replace("'", "''");
            //model.WYG = model.WYG.Replace("'", "''");
            //model.WYS = model.WYS.Replace("'", "''");
            string sql = string.Format("Insert into tReportTemplateDirec(ItemGUID,ParentID,Depth,ItemName,ItemOrder,Type,UserGuid,TemplateGuid,Leaf) values('{0}','{1}',{2},'{3}',{4},{5},'{6}','{7}',{8})"
                , strItemGuid, strParentID, depth, strItemName, itemOrder, type, strUserID, strTemplateGuid, 1);
            string sql1 = string.Format("Insert into tReportTemplate(TemplateGuid,TemplateName,ModalityType,BodyPart,CheckItemName,DoctorAdvice,ShortcutCode,ACRCode,ACRAnatomicDesc,ACRPathologicDesc,BodyCategory) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')"
               , strTemplateGuid, model.TemplateName, model.ModalityType, model.BodyPart, model.CheckItemName, model.DoctorAdvice, model.ShortcutCode, model.ACRCode, model.ACRAnatomicDesc, model.ACRPathologicDesc, model.BodyCategory);

            try
            {
                dataAccess.BeginTransaction();
                dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                
                dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                byte [] byteWYS = System.Text.Encoding.Default.GetBytes(model.WYS);
                byte [] byteWYG = System.Text.Encoding.Default.GetBytes(model.WYG);
                byte [] byteAppendInfo = System.Text.Encoding.Default.GetBytes(model.AppendInfo);
                byte [] byteTechInfo = System.Text.Encoding.Default.GetBytes(model.TechInfo);
              
                dataAccess.CommitTransaction();
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "WYS", byteWYS, byteWYS.Length, RisDAL.ConnectionState.CloseOnExit);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "WYG", byteWYG, byteWYG.Length, RisDAL.ConnectionState.CloseOnExit);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "AppendInfo", byteAppendInfo, byteAppendInfo.Length, RisDAL.ConnectionState.CloseOnExit);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "TechInfo", byteTechInfo, byteTechInfo.Length, RisDAL.ConnectionState.CloseOnExit);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return true;


        }

        public override bool ModifyReportTemplate(string strTemplateGuid, ReportTemplateModel model)
        {
            RisDAL dataAccess = new RisDAL();
            model.CheckItemName = model.CheckItemName.Replace("'", "''");
            model.DoctorAdvice = model.DoctorAdvice.Replace("'", "''");
            // model.TechInfo = model.TechInfo.Replace("'", "''");
            // model.WYG = model.WYG.Replace("'", "''");
            // model.WYS =  model.WYS.Replace("'", "''");
            int mark = 0;
            string sql = string.Format("Update tReportTemplate set TemplateName='{0}',ModalityType='{1}',BodyPart='{2}',CheckItemName='{3}',DoctorAdvice='{4}',ShortcutCode='{5}',ACRCode='{6}',ACRAnatomicDesc='{7}',ACRPathologicDesc='{8}',BodyCategory='{9}'where TemplateGuid='{10}'",
                model.TemplateName, model.ModalityType, model.BodyPart, model.CheckItemName, model.DoctorAdvice, model.ShortcutCode, model.ACRCode, model.ACRAnatomicDesc, model.ACRPathologicDesc, model.BodyCategory, strTemplateGuid);
            byte[] byteWYS = System.Text.Encoding.Default.GetBytes(model.WYS);
            byte[] byteWYG = System.Text.Encoding.Default.GetBytes(model.WYG);
            byte[] byteAppendInfo = System.Text.Encoding.Default.GetBytes(model.AppendInfo);
            byte[] byteTechInfo = System.Text.Encoding.Default.GetBytes(model.TechInfo);
           
            try
            {
                
                mark = dataAccess.ExecuteNonQuery(sql);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "WYS", byteWYS, byteWYS.Length, RisDAL.ConnectionState.CloseOnExit);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "WYG", byteWYG, byteWYG.Length, RisDAL.ConnectionState.CloseOnExit);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "AppendInfo", byteAppendInfo, byteAppendInfo.Length, RisDAL.ConnectionState.CloseOnExit);
                dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", strTemplateGuid, "TechInfo", byteTechInfo, byteTechInfo.Length, RisDAL.ConnectionState.CloseOnExit);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            if (mark == 1)
                return true;
            else
                return false;
        }

        public override bool AddPrintTemplate(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable myTable = model.DataSetParameter.Tables[0];
            string sql = "Insert into tPrintTemplate(TemplateGuid,Type,TemplateName,IsDefaultByType,Version,ModalityType,IsDefaultByModality) values(:TemplateGuid,:TemplateType,:TemplateName,:IsDefaultByType,0,:ModalityType,:IsDefaultByModality)";
            dataAccess.Parameters.AddChar(":TemplateGuid", myTable.Rows[0]["TemplateGuid"].ToString());
            dataAccess.Parameters.AddInt(":TemplateType", Convert.ToInt32(myTable.Rows[0]["Type"]));
            dataAccess.Parameters.AddChar(":TemplateName", myTable.Rows[0]["TemplateName"].ToString());
            dataAccess.Parameters.AddInt(":IsDefaultByType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByType"]));
            dataAccess.Parameters.AddChar(":ModalityType", Convert.ToString(myTable.Rows[0]["ModalityType"]));
            dataAccess.Parameters.AddInt(":IsDefaultByModality", Convert.ToInt32(myTable.Rows[0]["IsDefaultByModality"]));
            //  string sql1 = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and IsDefault=1", Convert.ToInt32(myTable.Rows[0]["Type"]));
            byte[] byteTemplateInfo = System.Text.Encoding.Unicode.GetBytes(myTable.Rows[0]["TemplateInfo"].ToString());
            try
            {
                //  if (myTable.Rows[0]["IsDefault"].ToString() == "0")
                //  {
               
                dataAccess.ExecuteNonQuery(sql);
                dataAccess.WriteLargeObj("tPrintTemplate", "TemplateGuid", myTable.Rows[0]["TemplateGuid"].ToString(), "TemplateInfo", byteTemplateInfo, byteTemplateInfo.Length, RisDAL.ConnectionState.KeepOpen);
              
                //  }
                //else
                //{

                //    dataAccess.Parameters.Clear();
                //    Object o = dataAccess.ExecuteScalar(sql1);
                //    if (o == null )
                //    {
                //        sql = "Insert into tPrintTemplate(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefault,Version) values(@TemplateGuid,@Type,@TemplateName,@TemplateInfo,@IsDefault,0)";
                //        dataAccess.Parameters.AddVarChar("@TemplateGuid", myTable.Rows[0]["TemplateGuid"].ToString());
                //        dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myTable.Rows[0]["Type"]));
                //        dataAccess.Parameters.AddVarChar("@TemplateName", myTable.Rows[0]["TemplateName"].ToString());
                //        dataAccess.Parameters.Add("@TemplateInfo", System.Text.Encoding.Default.GetBytes(myTable.Rows[0]["TemplateInfo"].ToString()));
                //        dataAccess.Parameters.AddInt("IsDefault", Convert.ToInt32(myTable.Rows[0]["IsDefault"]));
                //        dataAccess.ExecuteNonQuery(sql);
                //    }
                //    else
                //    {
                //        string strTemplateGuid = o.ToString();
                //        dataAccess.BeginTransaction();
                //        sql = "Insert into tPrintTemplate(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefault,Version) values(@TemplateGuid,@Type,@TemplateName,@TemplateInfo,@IsDefault,0)";
                //        dataAccess.Parameters.AddVarChar("@TemplateGuid", myTable.Rows[0]["TemplateGuid"].ToString());
                //        dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myTable.Rows[0]["Type"]));
                //        dataAccess.Parameters.AddVarChar("@TemplateName", myTable.Rows[0]["TemplateName"].ToString());
                //        dataAccess.Parameters.Add("@TemplateInfo", System.Text.Encoding.Default.GetBytes(myTable.Rows[0]["TemplateInfo"].ToString()));
                //        dataAccess.Parameters.AddInt("IsDefault", Convert.ToInt32(myTable.Rows[0]["IsDefault"]));

                //        dataAccess.ExecuteNonQuery(sql, KodakDAL.ConnectionState.KeepOpen);
                //        string sql2 = string.Format("Update tPrintTemplate set IsDefault = 0 where TemplateGuid = '{0}'", strTemplateGuid);
                //        dataAccess.ExecuteNonQuery(sql2,KodakDAL.ConnectionState.KeepOpen);

                //        dataAccess.CommitTransaction();
                //    }
                //}

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return true;

        }

        public override bool ModifyPrintTemplateFieldInfo(string strTemplateGuid, string strTemplateInfo)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Update tPrintTemplate set Version = Version+1  where TemplateGuid = '{0}'", strTemplateGuid);

            byte[] byteTemplateInfo = System.Text.Encoding.Unicode.GetBytes(strTemplateInfo);
            try
            {
                
                dataAccess.ExecuteNonQuery(sql);
                dataAccess.WriteLargeObj("tPrintTemplate", "TemplateGuid", strTemplateGuid, "TemplateInfo", byteTemplateInfo, byteTemplateInfo.Length, RisDAL.ConnectionState.CloseOnExit);
                

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return true;

        }

        #region EmergencyTemplate


        public override bool SaveEYTemplate(DataSet ds, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                List<string> listSQL = new List<string>();
                DataTable dt = ds.Tables["EYTemplate"];
                StringBuilder strBuiler = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    string strTemplateGuid = Convert.ToString(dr["TemplateName"]);
                    string strSQL = string.Format("SELECT count(*) FROM tEmergencyTemplate where TemplateName='{0}'", strTemplateGuid);
                    Object obj = oKodak.ExecuteScalar(strSQL);
                    if(obj==null)
                    {
                        throw new Exception("Unknow error");
                    }
                    int nCount = Convert.ToInt32(obj);
                    if (nCount > 0)
                    {
                        throw new Exception("Reduplicate template name");
                    }
                    strBuiler.Remove(0, strBuiler.Length);
                    strBuiler = strBuiler.AppendFormat("INSERT INTO tEmergencyTemplate(TemplateGuid,TemplateName,NamePrefix,Birthday,Gender,Telephone,InhospitalNo,ClinicNo,BedNo,")
                        .AppendFormat("ApplyDept,ApplyDoctor,ProcedureCode,Description) ")
                        .AppendFormat("VALUES('{0}','{1}','{2}',to_date('{3}','YYYY-MM-DD'),'{4}','{5}',", Convert.ToString(dr["TemplateGuid"]), Convert.ToString(dr["TemplateName"]), Convert.ToString(dr["NamePrefix"]), Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"), Convert.ToString(dr["GenderValue"]), Convert.ToString(dr["Telephone"]))
                        .AppendFormat("'{0}','{1}','{2}','{3}','{4}',", Convert.ToString(dr["InhospitalNo"]), Convert.ToString(dr["ClinicNo"]), Convert.ToString(dr["BedNo"]), Convert.ToString(dr["ApplyDeptValue"]), Convert.ToString(dr["ApplyDoctorValue"]))
                        .AppendFormat("'{0}','{1}')", Convert.ToString(dr["ProcedureCode"]), Convert.ToString(dr["Description"]));
                    Debug.WriteLine(strBuiler.ToString());
                    listSQL.Add(strBuiler.ToString());

                    string strApplyDept = Convert.ToString(dr["ApplyDeptValue"]);
                    string strApplyDoctor = Convert.ToString(dr["ApplyDoctorValue"]);
                    strApplyDept = strApplyDept.Trim();
                    if (strApplyDept.Length > 0)
                    {

                        strSQL = string.Format("SELECT count(*) FROM tDictionaryValue WHERE (DictionaryValue='{0}' or Description='{1}') and Tag=2", strApplyDept, strApplyDept);
                        obj = oKodak.ExecuteScalar(strSQL);
                        if (obj == null)
                        {
                            throw new Exception("Unknow error");
                        }
                        nCount = Convert.ToInt32(obj);
                        if (nCount == 0)
                        {
                            strSQL = string.Format("INSERT INTO tDictionaryValue(Tag,DictionaryValue,Description,shortcutcode) VALUES(2,'{0}','{1}','')", strApplyDept, strApplyDept);
                            Debug.WriteLine(strSQL);
                            listSQL.Add(strSQL);

                        }
                    }

                    strApplyDoctor = strApplyDoctor.Trim();
                    if (strApplyDoctor.Length > 0)
                    {
                        strSQL = string.Format("SELECT count(*) FROM tDictionaryValue WHERE (DictionaryValue='{0}' or Description='{1}') and Tag=8", strApplyDoctor, strApplyDoctor);
                        obj = oKodak.ExecuteScalar(strSQL);
                        if (obj == null)
                        {
                            throw new Exception("Unknow error");
                        }
                        nCount = Convert.ToInt32(obj);
                        if (nCount == 0)
                        {
                            strSQL = string.Format("INSERT INTO tDictionaryValue(Tag,DictionaryValue,Description,shortcutcode) VALUES(8,'{0}','{1}','')", strApplyDoctor, strApplyDoctor);
                            Debug.WriteLine(strSQL);
                            listSQL.Add(strSQL);

                        }
                    }

                }

                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();


            }
            catch (Exception e)
            {
                strError = e.Message;
                bReturn = false;
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

 
        public override bool UpdateEYTemplate(DataSet ds, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                List<string> listSQL = new List<string>();
                DataTable dt = ds.Tables["EYTemplate"];
                StringBuilder strBuiler = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    string strTemplateName = Convert.ToString(dr["TemplateName"]);
                    string strTemplateGuid = Convert.ToString(dr["TemplateGuid"]);

                    string strSQL = string.Format("SELECT count(*) FROM tEmergencyTemplate where TemplateGuid='{0}'", strTemplateGuid);
                    object obj = oKodak.ExecuteScalar(strSQL);
                    if (obj == null)
                    {
                        throw new Exception("Unknow error");
                    }
                    int nCount = Convert.ToInt32(obj);
                    if (nCount == 0)
                    {
                        throw new Exception("Can not save due to the template not exists.");
                    }

                    strSQL = string.Format("SELECT count(*) FROM tEmergencyTemplate where TemplateName='{0}' AND TemplateGuid!='{1}'", strTemplateName, strTemplateGuid);
                    obj = oKodak.ExecuteScalar(strSQL);
                    if (obj == null)
                    {
                        throw new Exception("Unknow error");
                    }
                    nCount = Convert.ToInt32(obj);
                    if (nCount > 0)
                    {
                        throw new Exception("Reduplicate template name");
                    }


                    strBuiler.Remove(0, strBuiler.Length);
                    strBuiler = strBuiler.AppendFormat("UPDATE tEmergencyTemplate SET TemplateName='{0}',NamePrefix='{1}',Birthday=to_date('{2}','YYYY-MM-DD'),Gender='{3}',Telephone='{4}',", Convert.ToString(dr["TemplateName"]), Convert.ToString(dr["NamePrefix"]), Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"), Convert.ToString(dr["GenderValue"]), Convert.ToString(dr["Telephone"]))
                    .AppendFormat("InhospitalNo='{0}',ClinicNo='{1}',BedNo='{2}',ApplyDept='{3}',", dr["InhospitalNo"].ToString(), dr["ClinicNo"].ToString(), dr["BedNo"].ToString(), dr["ApplyDeptValue"].ToString())
                    .AppendFormat("ApplyDoctor='{0}',ProcedureCode='{1}',Description='{2}' WHERE TemplateGuid='{3}'", Convert.ToString(dr["ApplyDoctorValue"]), Convert.ToString(dr["ProcedureCode"]), Convert.ToString(dr["Description"]), Convert.ToString(dr["TemplateGuid"]));

                    listSQL.Add(strBuiler.ToString());

                    string strApplyDept = Convert.ToString(dr["ApplyDeptValue"]);
                    string strApplyDoctor = Convert.ToString(dr["ApplyDoctorValue"]);
                    strApplyDept = strApplyDept.Trim();
                    if (strApplyDept.Length > 0)
                    {

                        strSQL = string.Format("SELECT count(*) FROM tDictionaryValue WHERE (DictionaryValue='{0}' or Description='{1}') and Tag=2", strApplyDept, strApplyDept);
                        obj=oKodak.ExecuteScalar(strSQL);
                        if (obj == null)
                        {
                            throw new Exception("Unknow error");
                        }
                        nCount = Convert.ToInt32(obj);
                        if (nCount == 0)
                        {
                            strSQL = string.Format("INSERT INTO tDictionaryValue(Tag,DictionaryValue,Description,shortcutcode) VALUES(2,'{0}','{1}','')", strApplyDept, strApplyDept);
                            listSQL.Add(strSQL);

                        }
                    }

                    strApplyDoctor = strApplyDoctor.Trim();
                    if (strApplyDoctor.Length > 0)
                    {
                        strSQL = string.Format("SELECT count(*) FROM tDictionaryValue WHERE (DictionaryValue='{0}' or Description='{1}') and Tag=8", strApplyDoctor, strApplyDoctor);
                        obj = oKodak.ExecuteScalar(strSQL);
                        if (obj == null)
                        {
                            throw new Exception("Unknow error");
                        }
                        nCount = Convert.ToInt32(obj);
                        if (nCount == 0)
                        {
                            strSQL = string.Format("INSERT INTO tDictionaryValue(Tag,DictionaryValue,Description,shortcutcode) VALUES(8,'{0}','{1}','')", strApplyDoctor, strApplyDoctor);
                            listSQL.Add(strSQL);

                        }
                    }
                }

                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();


            }
            catch (Exception e)
            {
                strError = e.Message;
                bReturn = false;
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
      
        #endregion

    }
}
