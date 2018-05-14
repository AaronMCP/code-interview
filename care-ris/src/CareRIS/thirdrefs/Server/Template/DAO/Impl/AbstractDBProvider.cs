#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*   Author : Terrence Jiang                                                                       */
/*   Create Date: July.2006
/****************************************************************************/
#endregion
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
using System.Configuration;
using System.Data.SqlClient;


namespace Server.DAO.Templates.Impl
{
    public abstract class AbstractDBProvider : IDBProvider
    {

        LogManagerForServer lm = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        SelfServiceReport service = new SelfServiceReport();

        //public virtual bool AddReportTemplate(string strGuid,ReportTemplateModel model)
        //{
        //    KodakDAL dataAccess = new KodakDAL();
        //    int mark = 0;

        //    string sql =string.Format( "Insert into tReportTemplate(TemplateGuid,TemplateName,ModalityType,BodyPart,WYS,WYG,AppendInfo,TechInfo,CheckItemName,DoctorAdvice,ShortcutCode,ACRCode,ACRAnatomicDesc,ACRPathologicDesc,BodyCategory) values('{0}','{1}','{2}','{3}',@WYS,@WYG,@AppendInfo,@TechInfo,'{4}','{5}','{6}','{7}','{8}','{9}','{10}')"
        //        , strGuid, model.TemplateName, model.ModalityType, model.BodyPart, model.CheckItemName, model.DoctorAdvice, model.ShortcutCode, model.ACRCode, model.ACRAnatomicDesc, model.ACRPathologicDesc, model.BodyCategory);
        //    dataAccess.Parameters.Add("@WYS", System.Text.Encoding.Default.GetBytes(model.WYS));
        //    dataAccess.Parameters.Add("@WYG", System.Text.Encoding.Default.GetBytes(model.WYG));
        //    dataAccess.Parameters.Add("@AppendInfo", System.Text.Encoding.Default.GetBytes(model.AppendInfo));
        //    dataAccess.Parameters.Add("@TechInfo", System.Text.Encoding.Default.GetBytes(model.TechInfo));

        //    try
        //    {
        //        mark = dataAccess.ExecuteNonQuery(sql);
        //    }
        //    catch (Exception e)
        //    {
        //        lm.Error(Convert.ToInt64(ModuleEnum.Report_DA.ToString()), ModuleInstanceName.Reporting, 1, e.Message, Application.StartupPath.ToString(),
        //            new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
        //            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
        //        return false;
        //    }
        //    finally
        //    {
        //        if (dataAccess != null)
        //        {
        //            dataAccess.Dispose();
        //        }
        //    }
        //    if (mark == 1)
        //        return true;
        //    else
        //        return false;
        //}
        #region ReportTemplate
        /// <summary>
        /// Name: GetReportInfo 
        /// Function:Get back report informations for report template by report guid
        /// </summary>
        /// <param name="strReportGuid"> Report Guid</param>
        /// <returns>The return dataset have tow tables, the first one contains the infomation of report, and the second one contains other information </returns>
        public virtual DataSet GetReportInfo(string strReportGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            //EK_HI00074817 Foman 2008-8-1
            string sql = string.Format("select WYS, WYG ,AppendInfo,TechInfo,checkItemName,DoctorAdvice ,AcrCode ,AcrAnatomic,AcrPathologic from tReport  where ReportGuid='{0}'", strReportGuid);
            string sql1 = string.Format("select ModalityType,BodyPart,BodyCategory from tProcedureCode where ProcedureCode in (select ProcedureCode from tRegProcedure where ReportGuid = '{0}')", strReportGuid);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                dt1 = dataAccess.ExecuteQuery(sql1);
                ds.Tables.Add(dt);
                ds.Tables.Add(dt1);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name: ModifyReportTemplate
        /// Function:Modify the report template 
        /// </summary>
        /// <param name="strTemplateGuid"> Report template guid</param>
        /// <param name="model"> The report model contains new values of report template </param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool ModifyReportTemplate(string strTemplateGuid, ReportTemplateModel model)
        {
            RisDAL dataAccess = new RisDAL();
            model.CheckItemName = model.CheckItemName.Replace("'", "''");
            model.DoctorAdvice = model.DoctorAdvice.Replace("'", "''");
            // model.TechInfo = model.TechInfo.Replace("'", "''");
            // model.WYG = model.WYG.Replace("'", "''");
            // model.WYS =  model.WYS.Replace("'", "''");
            int mark = 0;
            string sql = string.Format("Update tReportTemplate set TemplateName='{0}',ModalityType='{1}',BodyPart='{2}',WYS=@WYS,WYG=@WYG,AppendInfo=@AppendInfo,TechInfo=@TechInfo,CheckItemName='{3}',DoctorAdvice='{4}',ShortcutCode='{5}',ACRCode='{6}',BodyCategory='{7}',Gender='{8}',Positive={9},ACRAnatomicDesc=@ACRAnatomicDesc,ACRPathologicDesc=@ACRPathologicDesc where TemplateGuid='{10}'",
                model.TemplateName, model.ModalityType, model.BodyPart, model.CheckItemName, model.DoctorAdvice, model.ShortcutCode, model.ACRCode, model.BodyCategory, model.Gender, model.Positive, strTemplateGuid);
            dataAccess.Parameters.Add("@WYS", System.Text.Encoding.Default.GetBytes(model.WYS));
            dataAccess.Parameters.Add("@WYG", System.Text.Encoding.Default.GetBytes(model.WYG));
            dataAccess.Parameters.Add("@AppendInfo", System.Text.Encoding.Default.GetBytes(model.AppendInfo));
            dataAccess.Parameters.Add("@TechInfo", System.Text.Encoding.Default.GetBytes(model.TechInfo));
            dataAccess.Parameters.Add("@ACRAnatomicDesc", model.ACRAnatomicDesc ?? "");
            dataAccess.Parameters.Add("@ACRPathologicDesc", model.ACRPathologicDesc ?? "");
            try
            {
                mark = dataAccess.ExecuteNonQuery(sql);
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
        /// <summary>
        /// Name:DeleteReportTemplate
        /// Function:Delete the report template
        /// </summary>
        /// <param name="strTemplateGuid">Report template guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteReportTemplate(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();
            int mark = 0;
            string sql = string.Format("Delete from tReportTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            try
            {
                mark = dataAccess.ExecuteNonQuery(sql);
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

        /// <summary>
        /// Name:GetReportTemplateByShortcut
        /// Function:Find if there is a report template which have a shortcut value equals strShortcutCode
        /// </summary>
        /// <param name="strShortcutCode">Shortcut value</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <param name="type">Shortcut type,Global or User</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool GetReportTemplateByShortcut(string strShortcutCode, string strUserGuid, int type)
        {
            RisDAL dataAccess = new RisDAL();
            int count = 0;
            string sql = string.Format("select TemplateGuid from tReportTemplate where ShortcutCode='{0}'", strShortcutCode);

            try
            {
                string strTemplateGuid = Convert.ToString(dataAccess.ExecuteScalar(sql));
                if (strTemplateGuid == "")
                    return false;

                string sql1;
                if (type == 1)
                {
                    sql1 = string.Format("select count(*) from tReportTemplateDirec where TemplateGuid = '{0}' and [DirectoryType]='report' and (Type<>1 or (Type=1 and UserGuid = '{1}'))", strTemplateGuid, strUserGuid);

                    count = Convert.ToInt32(dataAccess.ExecuteScalar(sql1));
                }
                else if (type == 2)
                {
                    count = GetCountForReportOrPhraseBySiteShortcut(dataAccess, strShortcutCode, strUserGuid, "report");
                }
                else
                {
                    sql1 = string.Format("select count(*) from tReportTemplateDirec where TemplateGuid = '{0}' and [DirectoryType]='report' ", strTemplateGuid);

                    count = Convert.ToInt32(dataAccess.ExecuteScalar(sql1));
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// Name:GetReportTemplateByName
        /// Funtion:Get ReportTemplate information by template name
        /// </summary>
        /// <param name="strTemplateName">Report template name</param>
        /// <returns>Retrun dataset contains one table , this table contains the report information</returns>
        public virtual DataSet GetReportTemplateByName(string strTemplateName)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from tReportTemplate where TemplateGuid='{0}'", strTemplateName);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        public virtual DataSet GetReportTemplate(string itemGuid, string templateGuid, string shortcutCode)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select * from tReportTemplateDirec d, tReportTemplate t where t.templateguid=d.templateguid";

            if (!string.IsNullOrWhiteSpace(itemGuid))
            {
                sql += " AND ItemGuid='" + itemGuid + "'";
            }

            if (!string.IsNullOrWhiteSpace(templateGuid))
            {
                sql += " AND TemplateGuid='" + templateGuid + "'";
            }

            if (!string.IsNullOrWhiteSpace(shortcutCode))
            {
                sql += " AND ShortcutCode='" + shortcutCode + "'";
            }

            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return ds;
        }

        internal int GetCountForReportOrPhraseBySiteShortcut(RisDAL dataAccess, string shortcut, string site, string directoryType)
        {
            string sql1 = string.Format("select count(*) from tReportTemplateDirec"
                + " where [DirectoryType]='" + directoryType + "' and (Type=0 or Type=1)"
                + " AND TemplateGuid in (select TemplateGuid from t" + directoryType + "Template where ShortcutCode='{0}')",
                shortcut);

            int count = Convert.ToInt32(dataAccess.ExecuteScalar(sql1));
            if (count == 0)
            {
                string sql2 = string.Format("select * from tReportTemplateDirec d"
                    + " left join t" + directoryType + "Template r on d.templateguid=r.templateguid"
                    + " where [DirectoryType]='" + directoryType + "' and d.Type=2");

                DataTable dt2 = dataAccess.ExecuteQuery(sql2);
                DataRow[] datarows2 = dt2.Select("ShortcutCode='" + shortcut + "'");

                foreach (DataRow dr22 in datarows2)
                {
                    string itemGuid = Convert.ToString(dr22["ItemGuid"]);
                    string parentID = Convert.ToString(dr22["ParentID"]);
                    string rootSite = parentID;

                    DataRow[] datarows3;
                    while ((datarows3 = dt2.Select("ItemGuid='" + parentID + "'")) != null && datarows3.Length > 0)
                    {
                        rootSite = parentID = Convert.ToString(datarows3[0]["ParentID"]);
                    }

                    if (rootSite.ToUpper() == site.ToUpper())
                    {
                        count = 1;
                        break;
                    }
                }
            }

            return count;
        }
        #endregion
        #region ReportTemplateDirec
        /// <summary>
        /// Name:AddNewNode
        /// Function:Add a new directory node of report template
        /// </summary>
        /// <param name="strItemGuid">New directory node guid</param>
        /// <param name="strParentID">New directory node's parent guid</param>
        /// <param name="depth">The depth of new directory node in the tree</param>
        /// <param name="strItemName">New directory node name</param>
        /// <param name="itemOrder">New directory node order number in the same level nodes of  the tree </param>
        /// <param name="type">Global:0  User:1</param>
        /// <param name="strUserID">Current user's UserGuid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddNewNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID)
        {
            RisDAL dataAccess = new RisDAL();
            int mark = 0;
            string strGuid = Guid.NewGuid().ToString();

            if (type != 1)
                strUserID = "";

            string sql = string.Format("Insert into tReportTemplateDirec(ItemGUID,ParentID,Depth,ItemName,ItemOrder,Type,UserGuid,Leaf,Domain,[DirectoryType]) values('{0}','{1}',{2},'{3}',{4},{5},'{6}',{7},'{8}','report')"
                , strItemGuid, strParentID, depth, strItemName, itemOrder, type, strUserID, 0, CommonGlobalSettings.Utilities.GetCurDomain());
            try
            {
                mark = dataAccess.ExecuteNonQuery(sql);
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
        /// <summary>
        /// Name:AddNewLeafNode
        /// Function:Add a New report template node to the tReportTemplateDirec table,add a new report template to the tReportTemplate
        /// </summary>
        /// <param name="strItemGuid">New report template node guid </param>
        /// <param name="strParentID">New report template node 's parent guid</param>
        /// <param name="depth">The depth of new report template node in the tree</param>
        /// <param name="strItemName">New report template node name</param>
        /// <param name="itemOrder">New report template node order number in the same level nodes of the tree</param>
        /// <param name="type">Global:0 User:1</param>
        /// <param name="strUserID">Current user's UserGuid</param>
        /// <param name="strTemplateGuid">New report template guid</param>
        /// <param name="model">This report template model contains the information of new report template </param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddNewLeafNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID, string strTemplateGuid, string strGender, ReportTemplateModel model)
        {
            if (type != 1)
                strUserID = "";

            RisDAL dataAccess = new RisDAL();
            model.CheckItemName = model.CheckItemName.Replace("'", "''");
            model.DoctorAdvice = model.DoctorAdvice.Replace("'", "''");
            //model.TechInfo  = model.TechInfo.Replace("'", "''");
            //model.WYG = model.WYG.Replace("'", "''");
            //model.WYS = model.WYS.Replace("'", "''");
            string sql = string.Format("Insert into tReportTemplateDirec(ItemGUID,ParentID,Depth,ItemName,ItemOrder,Type,UserGuid,TemplateGuid,Leaf,Domain,[DirectoryType]) values('{0}','{1}',{2},'{3}',{4},{5},'{6}','{7}',{8},'{9}','report')"
                , strItemGuid, strParentID, depth, strItemName, itemOrder, type, strUserID, strTemplateGuid, 1, CommonGlobalSettings.Utilities.GetCurDomain());
            string sql1 = string.Format("Insert into tReportTemplate(TemplateGuid,TemplateName,ModalityType,BodyPart,WYS,WYG,AppendInfo,TechInfo,CheckItemName,DoctorAdvice,ShortcutCode,ACRCode,ACRAnatomicDesc,ACRPathologicDesc,BodyCategory,Domain,Gender,Positive) values('{0}','{1}','{2}','{3}',@WYS,@WYG,@AppendInfo,@TechInfo,'{4}','{5}','{6}','{7}', @ACRAnatomicDesc, @ACRPathologicDesc,'{8}','{9}','{10}',{11})"
               , strTemplateGuid, model.TemplateName, model.ModalityType, model.BodyPart, model.CheckItemName, model.DoctorAdvice, model.ShortcutCode, model.ACRCode, model.BodyCategory, CommonGlobalSettings.Utilities.GetCurDomain(), strGender,model.Positive);

            try
            {
                dataAccess.BeginTransaction();
                dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                dataAccess.Parameters.Clear();
                dataAccess.Parameters.Add("@WYS", System.Text.Encoding.Default.GetBytes(model.WYS));
                dataAccess.Parameters.Add("@WYG", System.Text.Encoding.Default.GetBytes(model.WYG));
                dataAccess.Parameters.Add("@AppendInfo", System.Text.Encoding.Default.GetBytes(model.AppendInfo));
                dataAccess.Parameters.Add("@TechInfo", System.Text.Encoding.Default.GetBytes(model.TechInfo));
                dataAccess.Parameters.Add("@ACRAnatomicDesc", model.ACRAnatomicDesc ?? string.Empty);
                dataAccess.Parameters.Add("@ACRPathologicDesc", model.ACRPathologicDesc ?? string.Empty);
                dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                dataAccess.CommitTransaction();
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
        /// <summary>
        /// Name : EditNodeName
        /// Function:Modify the name of node,include report template directory node's name and report template node's name
        /// </summary>
        /// <param name="strItemGuid">Current node 's guid</param>
        /// <param name="strItemName">New node name</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool EditNodeName(string strItemGuid, string strItemName)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Update tReportTemplateDirec set ItemName = '{0}'where ItemGUID='{1}' and [DirectoryType]='report' ",
              strItemName, strItemGuid);

            try
            {
                if (IsLeaf(strItemGuid))
                {
                    string strTemplateGuid = GetTemplateGuid(strItemGuid);
                    if (strTemplateGuid != null && strTemplateGuid != "")
                    {
                        string sqltemp = string.Format("Update tReportTemplate set TemplateName = '{0}' where TemplateGuid = '{1}'", strItemName, strTemplateGuid);
                        dataAccess.BeginTransaction();
                        dataAccess.ExecuteNonQuery(sqltemp, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.CommitTransaction();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    dataAccess.ExecuteNonQuery(sql);
                }
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
        /// <summary>
        /// Name:EditNodeParentID
        /// Function:It have not used
        /// </summary>
        /// <param name="strItemGuid"></param>
        /// <param name="strParentID"></param>
        /// <param name="curNodeOrder"></param>
        /// <param name="nextNodeOrder"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public virtual bool EditNodeParentID(string strItemGuid, string strParentID, int curNodeOrder, int nextNodeOrder, int depth)
        {
            return false;
        }
        /// <summary>
        /// Name:AddNodeItemOrder
        /// Function:Add 1 to current node order number ,make it go down one step
        /// </summary>
        /// <param name="strParentGuid">Current node's parent guid</param>
        /// <param name="curNodeOrder">Current node's order number</param>
        /// <param name="strNextGuid">The next node guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddNodeItemOrder(string strParentGuid, int curNodeOrder, string strNextGuid)
        {
            RisDAL dataAccess = new RisDAL();
            #region DEFECT EK_HI00079654
            bool bGlobalTemplate = false;
            string strUserGuid = "";
            string strSQL = string.Format("select ParentID from tReportTemplateDirec where ItemGuid = '{0}'  and [DirectoryType]='report' ", strNextGuid);
            Object obj = dataAccess.ExecuteScalar(strSQL);
            if (obj == null || obj.ToString().Trim().ToUpper() == "GLOBALTEMPLATE")
            {
                bGlobalTemplate = true;
            }
            else
            {
                strSQL = string.Format("select UserGuid from tReportTemplateDirec where ItemGuid = '{0}'  and [DirectoryType]='report' ", strNextGuid);
                obj = dataAccess.ExecuteScalar(strSQL);
                if (obj != null)
                {
                    strUserGuid = obj.ToString();
                }
                else if (obj == null || obj.ToString().Equals(string.Empty))
                {
                    bGlobalTemplate = true;
                }
            }
            string sql1 = "";
            if (bGlobalTemplate)
            {
                sql1 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder+1 where ParentID = '{0}' and ItemOrder={1} and [DirectoryType]='report' ",
                  strParentGuid, curNodeOrder);
            }
            else
            {
                sql1 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder+1 where ParentID = '{0}' and ItemOrder={1} and UserGuid='{2}' and [DirectoryType]='report' ",
                   strParentGuid, curNodeOrder, strUserGuid);

            }
            #endregion
            string sql2 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder-1 where ParentID = '{0}' and ItemGUID='{1}' and [DirectoryType]='report' ",
             strParentGuid, strNextGuid);

            try
            {
                dataAccess.BeginTransaction();
                dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                dataAccess.ExecuteNonQuery(sql2, RisDAL.ConnectionState.KeepOpen);
                dataAccess.CommitTransaction();

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
        /// <summary>
        /// Name:MinusNodeItemOrder
        /// Function:Minus 1 to current node order number ,make it go up one step
        /// </summary>
        /// <param name="strParentGuid">Current node's parent guid</param>
        /// <param name="curNodeOrder">Current node's order number</param>
        /// <param name="strPreGuid">The prev node guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool MinusNodeItemOrder(string strParentGuid, int curNodeOrder, string strPreGuid)
        {
            RisDAL dataAccess = new RisDAL();
            #region DEFECT EK_HI00079654
            bool bGlobalTemplate = false;
            string strUserGuid = "";
            string strSQL = string.Format("select ParentID from tReportTemplateDirec where ItemGuid = '{0}' and [DirectoryType]='report' ", strPreGuid);
            Object obj = dataAccess.ExecuteScalar(strSQL);
            if (obj == null || obj.ToString().Trim().ToUpper() == "GLOBALTEMPLATE")
            {
                bGlobalTemplate = true;
            }
            else
            {
                strSQL = string.Format("select UserGuid from tReportTemplateDirec where ItemGuid = '{0}' and [DirectoryType]='report' ", strPreGuid);
                obj = dataAccess.ExecuteScalar(strSQL);
                if (obj != null)
                {
                    strUserGuid = obj.ToString();
                }
            }
            string sql1 = "";
            if (bGlobalTemplate)
            {
                sql1 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder-1 where ParentID = '{0}' and ItemOrder={1} and [DirectoryType]='report' ",
                  strParentGuid, curNodeOrder);
            }
            else
            {
                sql1 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder-1 where ParentID = '{0}' and ItemOrder={1} and UserGuid='{2}' and [DirectoryType]='report' ",
                   strParentGuid, curNodeOrder, strUserGuid);

            }
            #endregion
            string sql2 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder+1 where ParentID = '{0}' and ItemGUID='{1}' and [DirectoryType]='report'",
             strParentGuid, strPreGuid);

            try
            {
                dataAccess.BeginTransaction();
                dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                dataAccess.ExecuteNonQuery(sql2, RisDAL.ConnectionState.KeepOpen);
                dataAccess.CommitTransaction();

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
        /// <summary>
        /// Name:DeleteNode
        /// Function:Delete node
        /// </summary>
        /// <param name="strItemGuid">Current node guid</param>
        /// <param name="strParentID">Current node's parent guid</param>
        /// <param name="curNodeOrder">Current node order number</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteNode(string strItemGuid, string strParentID, int curNodeOrder)
        {
            RisDAL dataAccess = new RisDAL();

            //{Bruce Deng 20081106
            bool bGlobalTemplate = false;
            string strUserGuid = "";
            string strSQL = string.Format("select ParentID from tReportTemplateDirec where ItemGuid = '{0}' and [DirectoryType]='report' ", strItemGuid);
            Object obj = dataAccess.ExecuteScalar(strSQL);
            if (obj == null || obj.ToString().Trim().ToUpper() == "GLOBALTEMPLATE")
            {
                bGlobalTemplate = true;
            }
            else
            {
                strSQL = string.Format("select UserGuid from tReportTemplateDirec where ItemGuid = '{0}' and [DirectoryType]='report' ", strItemGuid);
                obj = dataAccess.ExecuteScalar(strSQL);
                if (obj != null)
                {
                    strUserGuid = obj.ToString();
                }
            }

            //}


            string sql1 = string.Format("Delete from tReportTemplateDirec where ItemGuid = '{0}' and [DirectoryType]='report' ",
           strItemGuid);
            //ItemOrder = ItemOrder-1 is for change the node position
            string sql2 = "";
            if (bGlobalTemplate)
            {
                sql2 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder-1 where ParentID = '{0}' and ItemOrder>{1} and [DirectoryType]='report' ",
                 strParentID, curNodeOrder);
            }
            else
            {
                sql2 = string.Format("Update tReportTemplateDirec set ItemOrder =ItemOrder-1 where ParentID = '{0}' and ItemOrder>{1} and UserGuid='{2}' and [DirectoryType]='report' ",
                   strParentID, curNodeOrder, strUserGuid);

            }

            try
            {

                if (IsLeaf(strItemGuid))
                {
                    string strTemplateGuid = GetTemplateGuid(strItemGuid);
                    if (strTemplateGuid != null && strTemplateGuid != "")
                    {
                        string sql3 = string.Format("Delete from tReportTemplate where TemplateGuid = '{0}'", strTemplateGuid);
                        dataAccess.BeginTransaction();
                        dataAccess.ExecuteNonQuery(sql3, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql2, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.CommitTransaction();

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    dataAccess.BeginTransaction();

                    dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery(sql2, RisDAL.ConnectionState.KeepOpen);
                    dataAccess.CommitTransaction();
                }

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
        /// <summary>
        /// Name:IsLeaf
        /// Function:Checking current node if is a leaf node
        /// </summary>
        /// <param name="strItemGuid">Current node guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool IsLeaf(string strItemGuid)
        {
            RisDAL dataAccess = new RisDAL();
            bool mark = false;
            string sql = string.Format("select Leaf from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='report' ",
              strItemGuid);

            try
            {
                mark = dataAccess.ExecuteScalar(sql).ToString().Equals("1");
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
            return mark;
        }
        /// <summary>
        /// Name:GetTemplateGuid
        /// Funtion:Get report template guid by its name
        /// </summary>
        /// <param name="strItemGuid">report template node guid</param>
        /// <returns>Report template guid</returns>
        public virtual string GetTemplateGuid(string strItemGuid)
        {
            RisDAL dataAccess = new RisDAL();
            string strTemplateGuid = null;
            string sql = string.Format("select TemplateGuid from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='report' ",
              strItemGuid);

            try
            {
                strTemplateGuid = dataAccess.ExecuteScalar(sql).ToString();
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return strTemplateGuid;
        }
        /// <summary>
        /// Name:GetSubNodes
        /// Function:Get child nodes of the directory node
        /// </summary>
        /// <param name="strItemGuid">Report Template directory node guid</param>
        /// <param name="strUserGuid">current user's UserGuid</param>
        /// <returns>Return dataset have one table,it contains child nodes information</returns>
        public virtual DataSet GetSubNodes(string strItemGuid, string strUserGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from tReportTemplateDirec where ParentID = '{0}' and (TYPE=0 OR TYPE=2 or (TYPE=1 AND UserGuid = '{1}'))  and [DirectoryType]='report' order by ItemOrder", strItemGuid, strUserGuid);
            try
            {

                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:GetItemGuid
        /// Function:Get report template node 's guid by its shortcut
        /// </summary>
        /// <param name="strShortcut">Report template node shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Report template node guid, if it isn't found, return null</returns>
        public virtual string GetItemGuid(string strShortcut, string strUserGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string strItemGuid = null;
            string sql1 = string.Format("select TemplateGuid from tReportTemplate where ShortcutCode= '{0}'",
              strShortcut.Trim());


            try
            {
                DataTable myTable = dataAccess.ExecuteQuery(sql1);
                if (myTable != null && myTable.Rows.Count != 0)
                {
                    foreach (DataRow myRow in myTable.Rows)
                    {
                        string sql2 = string.Format("select ItemGUID from tReportTemplateDirec where TemplateGuid = '{0}' and [DirectoryType]='report'  and (Type = 0 or (Type=1 and UserGuid='{1}'))",
                        Convert.ToString(myRow["TemplateGuid"]), strUserGuid.Trim());
                        strItemGuid = Convert.ToString(dataAccess.ExecuteScalar(sql2));
                        if (strItemGuid != null && strItemGuid != "")
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return strItemGuid;
        }
        /// <summary>
        /// Name:GetParentGuid
        /// Function:Get node's parent node
        /// </summary>
        /// <param name="strItemGuid">Node guid</param>
        /// <returns>Parent node's guid, if it isn't found , return null</returns>
        public virtual string GetParentGuid(string strItemGuid)
        {
            RisDAL dataAccess = new RisDAL();
            string strParentGuid = null;
            string sql = string.Format("select ParentID from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='report' ",
              strItemGuid);

            try
            {
                strParentGuid = dataAccess.ExecuteScalar(sql).ToString();
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return strParentGuid;
        }

        /// <summary>
        /// Name:CopyLeafNode
        /// Function:Copy a LeafNode into a Parent Node
        /// </summary>
        /// <param name="strItemID">source leaf node guid </param>
        /// <param name="strParentItemID">destination parent node guid</param>
        /// <returns>True:successful    False:failed</returns>
        /// <returns>the new template name</returns>
        /// <returns>the new item id</returns>
        /// <returns>the new item orderid</returns>
        public virtual bool CopyLeafNode(string strItemID, string strParentItemID, string strUserguid, ref string newTemplateName, ref string newItemID, ref string orderIndex)
        {
            //KodakDAL dataAccess = new KodakDAL();
            string newItemGuid = Guid.NewGuid().ToString();
            string newTemplateGuid = Guid.NewGuid().ToString();
            int type = GetItemType(strParentItemID,"report");
            string newItemName = createCopyMoveName(strItemID, strParentItemID, type, strUserguid, "report");
            int maxOrerID = GetItemMaxOrder(strParentItemID, type, strUserguid, "report");

            newItemID = newItemGuid;
            newTemplateName = newItemName;
            orderIndex = Convert.ToString(maxOrerID + 1);

            //insert into report template diectory table
            string strInsertDirecSQL = string.Format(
            @"if exists(select 1 from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='report'  )
            insert into tReportTemplateDirec  select '{1}' as ItemGUID,'{2}',Depth,'{3}',
            {4},{5} ,'{6}','{7}',Leaf,Domain,[DirectoryType] from tReportTemplateDirec where  ItemGUID = '{0}' and [DirectoryType]='report'",
            strItemID, newItemGuid, strParentItemID, newItemName, maxOrerID + 1, type, strUserguid, newTemplateGuid);

            //isnert into report template table
            string strInsertSQL = string.Format(
            @"insert into tReportTemplate select '{0}','{1}'as TemplateName,ModalityType,
            BodyPart,WYS,WYG,AppendInfo,TechInfo,CheckItemName,DoctorAdvice,'',ACRCode,ACRAnatomicDesc,ACRPathologicDesc,BodyCategory,Domain,[Gender],Positive 
            from tReportTemplate where TemplateGuid = (select TemplateGuid from tReportTemplateDirec where ItemGUID ='{2}' and [DirectoryType]='report')",
            newTemplateGuid, newItemName, strItemID);

            RisDAL dal = new RisDAL();
            try
            {

                dal.BeginTransaction();
                int result = dal.ExecuteNonQuery(strInsertDirecSQL, RisDAL.ConnectionState.KeepOpen);
                if (result != -1)
                {
                    result = dal.ExecuteNonQuery(strInsertSQL, RisDAL.ConnectionState.KeepOpen);
                    if (result != -1)
                    {
                        dal.CommitTransaction();
                    }
                    else
                    {
                        dal.RollbackTransaction();
                        return false;
                    }
                }
                else
                {
                    dal.RollbackTransaction();
                    return false;
                }

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dal.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return true;
        }


        /// <summary>
        /// Name:MoveLeafNode
        /// Function:Move a LeafNode into a Parent Node
        /// </summary>
        /// <param name="strItemID">source leaf node guid </param>
        /// <param name="strParentItemID">destination parent node guid</param>
        /// <returns>True:successful    False:failed</returns>
        /// <returns>the new template name</returns>
        /// <returns>the new item order</returns>
        public virtual bool MoveLeafNode(string strItemID, string strParentItemID, string strUserguid, ref string newTemplateName, ref string orderIndex)
        {
            
            int itemOrder = GetItemOrder(strItemID, "report");
            int type = GetItemType(strParentItemID, "report");
            string parentID = GetItemParentID(strItemID, "report");
            string newItemName = createCopyMoveName(strItemID, strParentItemID, type, strUserguid, "report");
            int maxOrerID = GetItemMaxOrder(strParentItemID, type, strUserguid, "report");
            int orginMaxOrderID = GetItemMaxOrder(parentID, type, strUserguid, "report");
            orderIndex = Convert.ToString(maxOrerID + 1);
            newTemplateName = newItemName;

            //update some field in template diectory table
            string strUpdateDirecSQL = string.Empty;
            if (type == 0)
            {
                strUpdateDirecSQL = string.Format("if exists(select 1 from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='report' ) update tReportTemplateDirec set Type = {1}, ParentID = '{2}',ItemOrder='{3}', ItemName ='{4}' where ItemGUID ='{0}' and [DirectoryType]='report' \r\n",
                           strItemID, type, strParentItemID, maxOrerID + 1, newItemName);
            }
            else
            {
                strUpdateDirecSQL = string.Format("if exists(select 1 from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='report' ) update tReportTemplateDirec set Type = {1}, ParentID = '{2}',ItemOrder='{3}', UserGuid='{4}', ItemName ='{5}' where ItemGUID ='{0}' and [DirectoryType]='report' \r\n",
                           strItemID, type, strParentItemID, maxOrerID + 1, strUserguid, newItemName);
            }

            if (orginMaxOrderID != -1)
            {
                int position = orginMaxOrderID - itemOrder;//item position
                if (position > 0)
                {
                    strUpdateDirecSQL += string.Format("update tReportTemplateDirec set ItemOrder = ItemOrder-1  where ItemOrder > {0} and ParentID = '{1}' and [DirectoryType]='report' \r\n ", itemOrder, parentID);
                    strUpdateDirecSQL += string.Format("update tReportTemplate Set TemplateName = '{0}' where TemplateGuid = (select TemplateGuid from tReportTemplateDirec where ItemGuid = '{1}' and [DirectoryType]='report') ", newItemName, strItemID);
                }
            }


            RisDAL dal = new RisDAL();
            try
            {

                dal.BeginTransaction();
                int result = dal.ExecuteNonQuery(strUpdateDirecSQL, RisDAL.ConnectionState.KeepOpen);
                if (result != -1)
                {
                    dal.CommitTransaction();
                    return true;
                }
                else
                {
                    dal.RollbackTransaction();
                    return false;
                }

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dal.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Name:GetItemMaxOrder
        /// Function:Get the max order ID in its sons
        /// </summary>
        /// <param name="strTemplateName">Template Name</param>
        /// <param name="type">Template Type</param>
        /// <returns>the max order of the Parent Item order , -1 in exception</returns>
        public int GetItemMaxOrder(string strParentItemID, int type, string userGuid, string DirectoryType)
        {
            RisDAL dal = new RisDAL();
            string strSQL = "";
            if (type == 1)//user template
            {
                strSQL = "select Max(ItemOrder) from tReportTemplateDirec where [DirectoryType]='" + DirectoryType + "' and ParentID = '" + strParentItemID + "' and Leaf = 1 and UserGuid = '" + userGuid + "'";
            }
            else
            {
                strSQL = "select Max(ItemOrder) from tReportTemplateDirec where [DirectoryType]='" + DirectoryType + "' and ParentID = '" + strParentItemID + "' and Leaf = 1";
            }

            try
            {
                DataTable dt = dal.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return -1;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return -1;//no leaf node
        }

        //get the template type 0 global, 1 user template
        public int GetItemType(string strItemID,string DirectoryType)
        {
            RisDAL dal = new RisDAL();
            string strSQL = string.Format("select distinct Type from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='" + DirectoryType + "'", strItemID);

            if (strItemID.Equals("UserTemplate"))
            {
                return 1;
            }

            if (strItemID.Equals("GlobalTemplate"))
            {
                return 0;
            }


            try
            {
                DataTable dt = dal.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }

                string sql = " select 1 from tSiteList where SITE='" + strItemID + "' ";
                dt = dal.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return 0;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return 0;

        }

        /// <summary>
        /// Name:GetItemMaxOrder
        /// Function:Get the order of the Item
        /// </summary>
        /// <param name="type">ItemID</param>
        /// <returns>the order of the Item , -1 in exception</returns>
        public int GetItemOrder(string strItemID, string DirectoryType)
        {
            RisDAL dal = new RisDAL();
            string strSQL = "select ItemOrder from tReportTemplateDirec where ItemGUID = '" + strItemID + "' and Leaf = 1 and [DirectoryType]='" + DirectoryType + "'";

            try
            {
                DataTable dt = dal.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return -1;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return -1;//not leaf node
        }

        public string GetItemParentID(string strItemID, string DirectoryType)
        {
            RisDAL dal = new RisDAL();
            string strSQL = "select ParentID from tReportTemplateDirec where ItemGUID = '" + strItemID + "' and Leaf = 1 and [DirectoryType]='"+ DirectoryType +"'";

            try
            {
                DataTable dt = dal.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return null;//not leaf node
        }

        /// <summary>
        /// Name:createCopyMoveName
        /// Function:create the copy or move naming
        /// </summary>
        /// <param name="srcItemID">Source ItemID</param>
        /// <param name="desItemID">Destinaion ItemID</param>
        /// <returns>if duplicated name plus "_1" else the item name , "" in exception</returns>
        public string createCopyMoveName(string srcItemID, string desItemID, int type, string userGuid, string DirectoryType)
        {
            RisDAL dal = new RisDAL();
            //1.whether the src name is duplicated
            string sqlSrcSameItemName = "";
            if (type == 1)//user template
            {
                sqlSrcSameItemName = string.Format(
               @"select ItemName from tReportTemplateDirec where ParentID = '{0}'
                and ItemName =(select ItemName from tReportTemplateDirec where ItemGUID = '{1}' and [DirectoryType]='" + DirectoryType + "') and UserGuid = '{2}' and [DirectoryType]='" + DirectoryType + "'"
               , desItemID, srcItemID, userGuid);
            }
            else
            {
                sqlSrcSameItemName = string.Format(
               @"select ItemName from tReportTemplateDirec where ParentID = '{0}'
                and ItemName =(select ItemName from tReportTemplateDirec where ItemGUID = '{1}' and [DirectoryType]='" + DirectoryType + "') and [DirectoryType]='" + DirectoryType + "' "
               , desItemID, srcItemID);
            }
            string sqlSrcItemName = "select ItemName from tReportTemplateDirec where [DirectoryType]='" + DirectoryType + "' and ItemGUID ='" + srcItemID + "'";//get source item node item name

            try
            {
                DataTable dtSameName = dal.ExecuteQuery(sqlSrcSameItemName);
                DataTable dtName = dal.ExecuteQuery(sqlSrcItemName);
                if (dtSameName != null && dtSameName.Rows.Count > 0)
                {
                    string newName = dtSameName.Rows[0][0].ToString() + "_1";//default duplicated name add "_1" string
                    bool stillHaveSameName = true;
                    while (stillHaveSameName)
                    {
                        DataTable dt = null;
                        string sqlSameDuplitedAgain = string.Format("select ItemName from tReportTemplateDirec where ParentID = '{0}' and ItemName ='{1}' and [DirectoryType]='" + DirectoryType + "'", desItemID, newName);
                        dt = dal.ExecuteQuery(sqlSameDuplitedAgain, RisDAL.ConnectionState.KeepOpen);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            newName = dt.Rows[0][0].ToString() + "_1";
                        }
                        else
                        {
                            stillHaveSameName = false;
                            return newName;
                        }
                    }
                }
                else if (dtName != null && dtName.Rows.Count > 0)
                {
                    return dtName.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return "";
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return "";//not leaf node
        }
        #region PrintTemplate
        /// <summary>
        /// Name:IsPrintTemplateSameName
        /// Function:Check if there is same name template name
        /// </summary>
        /// <param name="strTemplateName">Template Name</param>
        /// <param name="type">Template Type</param>
        /// <returns>True: Success  False:Fail</returns>
        public bool IsPrintTemplateSameName(string strTemplateName, int type, string site)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("select TemplateName from tPrintTemplate where TemplateName='{0}' and Type = {1} AND SITE='{2}'", 
                strTemplateName, type, site);

            try
            {
                DataTable myTable = dataAccess.ExecuteQuery(sql);
                if (myTable.Rows.Count == 0)
                    return false;
                foreach (DataRow myRow in myTable.Rows)
                {
                    if (Convert.ToString(myRow["TemplateName"]) == strTemplateName)
                    {
                        return true;

                    }
                }
                return false;
                //int count = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
                //if (count == 0)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return true;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }


        }
        /// <summary>
        /// Name:ModifyPrintTemplateName
        /// Function:Modify print template name
        /// </summary>
        /// <param name="strTemplateGuid">Template Guid</param>
        /// <param name="strTemplateName">New Template Name</param>
        /// <returns>True:Success False:Fail</returns>
        public virtual bool ModifyPrintTemplateName(string strTemplateGuid, string strTemplateName)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Update tPrintTemplate set TemplateName = '{1}' , Version = Version+1 where TemplateGuid = '{0}'", strTemplateGuid, strTemplateName);

            try
            {
                dataAccess.ExecuteNonQuery(sql);
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

        public virtual bool ModifyPrintTemplatePropertyTag(string strTemplateGuid, string strPropertyTag)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format(
                "Update tPrintTemplate set PropertyTag = '{1}' , Version = Version+1 where TemplateGuid = '{0}'",
                strTemplateGuid,
                strPropertyTag);

            try
            {
                dataAccess.ExecuteNonQuery(sql);
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

        /// <summary>
        /// Name:ModifyPrintTemplateFieldInfo
        /// Function:Modify print template content, and add version number
        /// </summary>
        /// <param name="strTemplateGuid"></param>
        /// <param name="strTemplateInfo"></param>
        /// <returns>True: Success False:Fail</returns>
        public virtual bool ModifyPrintTemplateFieldInfo(string strTemplateGuid, string strTemplateInfo)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Update tPrintTemplate set TemplateInfo = @TemplateInfo,Version = Version+1  where TemplateGuid = '{0}'", strTemplateGuid);
            dataAccess.Parameters.Add("@TemplateInfo", System.Text.Encoding.Unicode.GetBytes(strTemplateInfo));
            try
            {



                dataAccess.ExecuteNonQuery(sql);



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
        /// <summary>
        /// Name:LoadPrintTemplateField
        /// Function:Load print template optional field
        /// </summary>
        /// <param name="type">Template Type(Check Dictionary Tag 14)</param>
        /// <returns></returns>
        public virtual DataSet LoadPrintTemplateField(int type)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = string.Empty;
            //General Statistic
            if (type == 7)
            {
                sql = "select tqr.FieldName,tq.QueryName as SubType from tQueryResultColumn tqr inner join tQuery tq on tqr.QueryID =tq.ID";
            }
            else
            {
                sql = string.Format("select FieldName,SubType from tPrintTemplateFields where type={0}", type);
            }
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                ds = null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadPrintTemplateType
        /// Function:Load print template types from dictionary
        /// </summary>
        /// <returns>Return dataset contains one table, the table contains print template types</returns>
        public virtual DataSet LoadPrintTemplateType()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select Value, Text,ShortcutCode,IsDefault from tDictionaryValue where Tag=14 order by Value";
            #region Added by Blue for RC595 - US16558, 05/08/2014
            string sysSql = "select Value from tSystemProfile where ModuleID = '0400' and Name = 'Report_CanUseReturnVisit'";
            string val = dataAccess.ExecuteScalar(sysSql).ToString();
            if (val == "0")
            {
                sql = "select Value, Text,ShortcutCode,IsDefault from tDictionaryValue where Tag=14 and Text <> 'ReturnVisit' order by Value";
            }
            #endregion
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                dt.TableName = "PrintTemplateType";
                ds.Tables.Add(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string data = dr["ShortCutCode"].ToString();
                    if (data == null || data.Trim().Length < 1)
                        continue;
                    DataTable dtlevel2;// = new DataTable(dr["DictionaryValue"].ToString());
                    dtlevel2 = dataAccess.ExecuteQuery(data);
                    dtlevel2.TableName = dr["Value"].ToString();
                    ds.Tables.Add(dtlevel2);
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadSubPrintTemplateInfo
        /// Function:Load child print templates part information by type for displaying in tree
        /// </summary>
        /// <param name="type">Print template type</param>
        /// <returns>Return dataset contains one table, the table contains child print templates part information </returns>
        public virtual DataSet LoadSubPrintTemplateInfo(int type)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from tPrintTemplate where 1=1 ");
            if (type >= 0)
                sql += " AND type=" + type.ToString();
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadSubPrintTemplate
        /// Function:Load child print templates all information by type
        /// </summary>
        /// <param name="type">Print template type</param>
        /// <returns>Return dataset contains one table, the table contains child print templates all information</returns>
        public virtual string LoadPrintTemplateInfo(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();
            string strResult = "";
            string sql = string.Format("select  TemplateInfo from tPrintTemplate where TemplateGuid='{0}'", strTemplateGuid);
            try
            {
                strResult = System.Text.Encoding.Unicode.GetString(dataAccess.ExecuteScalar(sql) as byte[]);

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return strResult;
        }
        public virtual DataSet LoadPrintTemplateByName(int type, string strPrintTemplateName)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet myDataSet = new DataSet();
            DataTable mydataTable = new DataTable();
            string sql = string.Format("select  *  from tPrintTemplate where TemplateName='{0}' and type={1}", strPrintTemplateName, type);
            try
            {
                mydataTable = dataAccess.ExecuteQuery(sql);
                myDataSet.Tables.Add(mydataTable);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return myDataSet;
        }
        /// <summary>
        /// Name:AddPrintTemplate
        /// Function:Add a new print template
        /// </summary>
        /// <param name="model"> The model contains the value of pint template</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddPrintTemplate(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable myTable = model.DataSetParameter.Tables[0];
            string sql = "Insert into tPrintTemplate"
                + "(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefaultByType,Version,ModalityType,IsDefaultByModality,Domain,Site)"
                + " values(@TemplateGuid,@Type,@TemplateName,@TemplateInfo,@IsDefaultByType,0,@ModalityType,@IsDefaultByModality,@Domain,@Site)";
            dataAccess.Parameters.AddVarChar("@TemplateGuid", myTable.Rows[0]["TemplateGuid"].ToString());
            dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myTable.Rows[0]["Type"]));
            dataAccess.Parameters.AddVarChar("@TemplateName", myTable.Rows[0]["TemplateName"].ToString());
            dataAccess.Parameters.Add("@TemplateInfo", System.Text.Encoding.Unicode.GetBytes(myTable.Rows[0]["TemplateInfo"].ToString()));
            dataAccess.Parameters.AddInt("@IsDefaultByType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByType"]));
            dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myTable.Rows[0]["ModalityType"]));
            dataAccess.Parameters.AddInt("@IsDefaultByModality", Convert.ToInt32(myTable.Rows[0]["IsDefaultByModality"]));
            dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
            dataAccess.Parameters.Add("@Site", Convert.ToString(myTable.Rows[0]["Site"]));
            //  string sql1 = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and IsDefault=1", Convert.ToInt32(myTable.Rows[0]["Type"]));

            try
            {

                dataAccess.ExecuteNonQuery(sql);


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
        /// <summary>
        /// Name:DeletePrintTemplate
        /// Function:Delete the print template
        /// </summary>
        /// <param name="strTemplateGuid">Print template guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeletePrintTemplate(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Delete from tPrintTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            try
            {
                dataAccess.ExecuteNonQuery(sql);
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
        /// <summary>
        /// Name:SetDefault
        /// Function:Set the print  template to be default one
        /// </summary>
        /// <param name="type">Print template type</param>
        /// <param name="strTemplateGuid">Print template guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool SetDefault(int type, string strModalityType, string strTemplateGuid, string site)
        {
            RisDAL dataAccess = new RisDAL();


            string sql1 = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and IsDefaultByType=1 AND Site='" + site + "'", type);
            string sql2 = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and ModalityType = '{1}' and IsDefaultByModality=1 AND Site='" + site + "'", type, strModalityType);
            string sql3 = string.Format("Update tPrintTemplate set IsDefaultByType = 1 where TemplateGuid = '{0}' AND Site='" + site + "'", strTemplateGuid);
            string sql4 = string.Format("Update tPrintTemplate set IsDefaultByModality = 1 where TemplateGuid = '{0}' AND Site='" + site + "'", strTemplateGuid);

            try
            {
                if (strModalityType == "")
                {
                    Object o = dataAccess.ExecuteScalar(sql1);
                    if (o != null)
                    {
                        string lastTemplateGuid = o.ToString();

                        dataAccess.BeginTransaction();
                        string sql5 = string.Format("Update tPrintTemplate set IsDefaultByType = 0 where TemplateGuid = '{0}' AND Site='" + site + "'", lastTemplateGuid);
                        dataAccess.ExecuteNonQuery(sql5, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql3, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.CommitTransaction();


                    }
                    else
                    {
                        dataAccess.ExecuteNonQuery(sql3, RisDAL.ConnectionState.KeepOpen);
                    }
                }
                else
                {
                    Object o = dataAccess.ExecuteScalar(sql2);
                    if (o != null)
                    {
                        string lastTemplateGuid = o.ToString();

                        dataAccess.BeginTransaction();
                        string sql5 = string.Format("Update tPrintTemplate set IsDefaultByModality = 0 where TemplateGuid = '{0}' AND Site='" + site + "'", lastTemplateGuid);
                        dataAccess.ExecuteNonQuery(sql5, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql4, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.CommitTransaction();


                    }
                    else
                    {
                        dataAccess.ExecuteNonQuery(sql4, RisDAL.ConnectionState.KeepOpen);
                    }
                }


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
        /// <summary>
        /// Use Guid only
        /// </summary>
        /// <param name="strTemplateGuid"></param>
        /// <returns></returns>
        public virtual bool SetDefault(string strTemplateGuid, string site)
        {
            RisDAL dataAccess = new RisDAL();


            string sql1 = string.Format("select type from tPrintTemplate where TemplateGuid = '{0}' AND Site='" + site + "'", strTemplateGuid);
            string sql2 = string.Format("select ModalityType from tPrintTemplate where TemplateGuid= '{0}' AND Site='" + site + "'", strTemplateGuid);
            string sql3 = string.Format("Update tPrintTemplate set IsDefaultByType = 1 where TemplateGuid = '{0}' AND Site='" + site + "'", strTemplateGuid);
            string sql4 = string.Format("Update tPrintTemplate set IsDefaultByModality = 1 where TemplateGuid = '{0}' AND Site='" + site + "'", strTemplateGuid);

            try
            {
                string type = Convert.ToString(dataAccess.ExecuteScalar(sql1));
                string strModalityType = Convert.ToString(dataAccess.ExecuteScalar(sql2));

                if (strModalityType == "")
                {
                    string strGetDefalut = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and IsDefaultByType=1 AND Site='" + site + "'", type);
                    Object o = dataAccess.ExecuteScalar(strGetDefalut);
                    if (o != null)
                    {
                        string lastTemplateGuid = o.ToString();

                        dataAccess.BeginTransaction();
                        string sql5 = string.Format("Update tPrintTemplate set IsDefaultByType = 0 where TemplateGuid = '{0}' AND Site='" + site + "'", lastTemplateGuid);
                        dataAccess.ExecuteNonQuery(sql5, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql3, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.CommitTransaction();


                    }
                    else
                    {
                        dataAccess.ExecuteNonQuery(sql3, RisDAL.ConnectionState.KeepOpen);
                    }
                }
                else
                {
                    string strGetDefault = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and ModalityType = '{1}' and IsDefaultByModality=1 AND Site='" + site + "'", type, strModalityType);
                    Object o = dataAccess.ExecuteScalar(strGetDefault);
                    if (o != null)
                    {
                        string lastTemplateGuid = o.ToString();

                        dataAccess.BeginTransaction();
                        string sql5 = string.Format("Update tPrintTemplate set IsDefaultByModality = 0 where TemplateGuid = '{0}' AND Site='" + site + "'", lastTemplateGuid);
                        dataAccess.ExecuteNonQuery(sql5, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.ExecuteNonQuery(sql4, RisDAL.ConnectionState.KeepOpen);
                        dataAccess.CommitTransaction();


                    }
                    else
                    {
                        dataAccess.ExecuteNonQuery(sql4, RisDAL.ConnectionState.KeepOpen);
                    }
                }


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
        /// <summary>
        /// Name:GetLatestVersion
        /// Function:Get the print template the latest version number
        /// </summary>
        /// <param name="strTemplateGuid">Template Guid</param>
        /// <returns>The print template latest version number</returns>
        public virtual int GetLatestVersion(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("select Version from tPrintTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            int latestVersion = -1;
            try
            {
                latestVersion = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return latestVersion;
        }

        /// <summary>
        /// Name:GetLatestVersion
        /// Function:Get the print template the latest version number
        /// </summary>
        /// <param name="strPrintTemplateGuid">Template Guid</param>
        /// <returns>The print template name</returns>
        public virtual string GetPrintTemplateNameByGuid(string strPrintTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("select TemplateName from tPrintTemplate where TemplateGuid = '{0}'", strPrintTemplateGuid);
            try
            {
                object obj = dataAccess.ExecuteScalar(sql);
                if (obj != null)
                {
                    return obj.ToString();
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return "";
        }
        /// <summary>
        /// Name:GetDefaultPrintTemplate
        /// Function:Get the default print template by type or by modality type
        /// </summary>
        /// <param name="type">Print template type</param>
        /// <param name="strModalityType">modality type. if it is equals "", return the type default template , ortherwise return modality type default template</param>
        /// <returns>Default Template dataset</returns>
        public virtual DataSet GetDefaultPrintTemplate()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql;

            //{Bruce Deng 20090422  Outofmemory when open report
            sql = string.Format("select * from tPrintTemplate");


            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:GetTypeDesc
        /// Function:Get print template desciption
        /// </summary>
        /// <param name="type">Print template type(Dictionaryvalue (tag = 14))</param>
        /// <returns></returns>
        public virtual string GetTypeDesc(int type)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("select  Text from tDictionaryValue where Tag=14 and Value='{0}'", type);
            string typeDesc = "";
            try
            {
                typeDesc = Convert.ToString(dataAccess.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return typeDesc;
        }
        public virtual DataSet LoadGeneralStatType()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "Select ID,Queryname from tQuery";
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        #region ExportTemplate
        public virtual DataSet LoadExportTemplateType()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select Value, Text from tDictionaryValue where Tag=55 order by Value";
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadSubExportTemplateInfo(
        /// Function:Load child export templates part information by type for displaying in tree
        /// </summary>
        /// <param name="type">Export template type</param>
        /// <returns>Return dataset contains one table, the table contains child export templates part information </returns>
        public virtual DataSet LoadSubExportTemplateInfo()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select TemplateGuid,TemplateName,Type,ChildType,Descriptions from tExportTemplate ";
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadExportTemplateInfo
        /// Function:Load export templates by TemplateGuid for displaying in tree
        /// </summary>
        /// <param name="strTemplateGuid">TemplateGuid</param>
        /// <returns>Return dataset contains one table, the table contains  export templates information </returns>
        public virtual DataSet LoadExportTemplateInfo(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select TemplateInfo from tExportTemplate where TemplateGuid='{0}'", strTemplateGuid);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);

                // strResult = System.Text.Encoding.Unicode.GetString(dataAccess.ExecuteScalar(sql) as byte[]);

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:AddPrintTemplate
        /// Function:Add a new export template
        /// </summary>
        /// <param name="model"> The model contains the value of export template</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddExportTemplate(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable myTable = model.DataSetParameter.Tables[0];
            string sql = "Insert into tExportTemplate(TemplateGuid,Type,ChildType,TemplateName,TemplateInfo,Descriptions,IsDefaultByType,IsDefaultByChildType,Domain) values(@TemplateGuid,@Type,@ChildType,@TemplateName,@TemplateInfo,@Descriptions,@IsDefaultByType,@IsDefaultByChildType,@Domain)";
            dataAccess.Parameters.AddVarChar("@TemplateGuid", myTable.Rows[0]["TemplateGuid"].ToString());
            dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myTable.Rows[0]["Type"]));
            dataAccess.Parameters.AddVarChar("@ChildType", myTable.Rows[0]["ChildType"].ToString());
            dataAccess.Parameters.AddVarChar("@TemplateName", myTable.Rows[0]["TemplateName"].ToString());
            dataAccess.Parameters.Add("@TemplateInfo", (Byte[])myTable.Rows[0]["TemplateInfo"]);
            dataAccess.Parameters.AddVarChar("@Descriptions", myTable.Rows[0]["Descriptions"].ToString());
            dataAccess.Parameters.AddInt("@IsDefaultByType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByType"]));
            dataAccess.Parameters.AddInt("@IsDefaultByChildType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByChildType"]));
            dataAccess.Parameters.AddVarChar("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());

            //  string sql1 = string.Format("select TemplateGuid from tPrintTemplate where Type = {0} and IsDefault=1", Convert.ToInt32(myTable.Rows[0]["Type"]));

            try
            {
                dataAccess.ExecuteNonQuery(sql);
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
        /// <summary>
        /// Name:ModifyExportTemplateInfo
        /// Function:Modify export template 
        /// </summary>
        /// <param name=" model">The model contains the value of export template</param>
        /// <returns>True: Success False:Fail</returns>
        public virtual bool ModifyExportTemplateInfo(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable myTable = model.DataSetParameter.Tables[0];
            string sql = string.Format("Update tExportTemplate set TemplateInfo = @TemplateInfo,TemplateName=@TemplateName,Descriptions=@Descriptions,IsDefaultByChildType=@IsDefaultByChildType where TemplateGuid = '{0}'", myTable.Rows[0]["TemplateGuid"].ToString());

            dataAccess.Parameters.Add("@TemplateInfo", (byte[])myTable.Rows[0]["TemplateInfo"]);
            dataAccess.Parameters.AddVarChar("@TemplateName", myTable.Rows[0]["TemplateName"].ToString());
            dataAccess.Parameters.AddVarChar("@Descriptions", myTable.Rows[0]["Descriptions"].ToString());
            dataAccess.Parameters.AddInt("@IsDefaultByType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByType"]));
            dataAccess.Parameters.AddInt("@IsDefaultByChildType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByChildType"]));

            try
            {
                dataAccess.ExecuteNonQuery(sql);

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
        /// <summary>
        /// Name:ModifyExportTemplate without templateinfo
        /// Function:Modify export template 
        /// </summary>
        /// <param name=" model">The model contains the value of export template</param>
        /// <returns>True: Success False:Fail</returns>
        public virtual bool ModifyExportTemplate(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable myTable = model.DataSetParameter.Tables[0];
            string sql = string.Format("Update tExportTemplate set TemplateName=@TemplateName,Descriptions=@Descriptions,IsDefaultByChildType=@IsDefaultByChildType where TemplateGuid = '{0}'", myTable.Rows[0]["TemplateGuid"].ToString());

            dataAccess.Parameters.AddVarChar("@TemplateName", myTable.Rows[0]["TemplateName"].ToString());
            dataAccess.Parameters.AddVarChar("@Descriptions", myTable.Rows[0]["Descriptions"].ToString());
            dataAccess.Parameters.AddInt("@IsDefaultByType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByType"]));
            dataAccess.Parameters.AddInt("@IsDefaultByChildType", Convert.ToInt32(myTable.Rows[0]["IsDefaultByChildType"]));

            try
            {
                dataAccess.ExecuteNonQuery(sql);

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
        /// <summary>
        /// Name:DeleteExportTemplate
        /// Function:Delete the Export template
        /// </summary>
        /// <param name="strTemplateGuid">Export template guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteExportTemplate(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Delete from tExportTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            try
            {
                dataAccess.ExecuteNonQuery(sql);
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
        #endregion
        #endregion
        #region PhraseTemplate
        /// <summary>
        /// Name:GetModalityType
        /// Function:Get modality types from modality table
        /// </summary>
        /// <returns>Return dataset contains one table, this table contains modality types</returns>
        public virtual DataSet GetModalityType()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select ModalityType from tModalityType";
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadPhraseTemplatesByModality
        /// Function:Load phrase templates by modality type
        /// </summary>
        /// <param name="strModalityType">Modality type</param>
        /// <param name="type">Global:0 User:1</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains phrase templates</returns>
        public virtual DataSet LoadPhraseTemplatesByModality(string strModalityType, int type, string strUserGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql;
            if (type != 1)
            {
                sql = string.Format("select TemplateGuid,TemplateName,ShortcutCode from tPhraseTemplate where ModalityType = '{0}' and Type = 0", strModalityType);

            }
            else
            {
                sql = string.Format("select TemplateGuid,TemplateName,ShortcutCode from tPhraseTemplate where ModalityType = '{0}' and Type = 1 and UserGuid='{1}' ", strModalityType, strUserGuid);
            }
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        public virtual DataSet LoadChildren(string strParentGuid, int type, string strUserGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql;
            if (type != 1)
            {
                sql = string.Format("select ItemGUID, ItemName , [type], TemplateGuid ,Leaf,ItemOrder  from tReportTemplateDirec where ParentID = '{0}' and DirectoryType = 'phrase' order by ItemOrder asc", strParentGuid);

            }
            else
            {
                sql = string.Format("select ItemGUID, ItemName , [type], TemplateGuid ,Leaf,ItemOrder  from tReportTemplateDirec where ParentID = '{0}' and DirectoryType = 'phrase' and UserGuid = '{1}' order by ItemOrder asc", strParentGuid, strUserGuid);
            }
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        /// <summary>
        /// Name:LoadPhraseTemplateByGuid
        /// Function:Load phrase template by template guid
        /// </summary>
        /// <param name="strTemplateGuid">Phrase template guid</param>
        /// <returns>Return dataset contains one table, this table contains phrase template information</returns>
        public virtual DataSet LoadPhraseTemplateByGuid(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select TemplateName,TemplateInfo,ShortcutCode from tPhraseTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:AddPhraseTemplate
        /// Function:Add a new phrase template
        /// </summary>
        /// <param name="model">The model contains values of Phrase template</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddPhraseTemplate(PhraseTemplateModel model)
        {
            model.TemplateInfo = model.TemplateInfo.Replace("'", "''");
            RisDAL dataAccess = new RisDAL();
            string sql = string.Format("Insert into tPhraseTemplate(TemplateGuid,ModalityType,TemplateName,TemplateInfo,ShortcutCode,Type,UserGuid,Domain) values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}')"
                , model.TemplateGuid, model.ModalityType, model.TemplateName, model.TemplateInfo, model.ShortcutCode, model.Type, model.UserGuid, CommonGlobalSettings.Utilities.GetCurDomain());
            string sql0 = "select max(ItemOrder) from tReportTemplateDirec where ParentID='" + model.ParentGuid + "' and [DirectoryType]='phrase'";
            object obj = dataAccess.ExecuteScalar(sql0);
            int i;
            if (obj == DBNull.Value || obj == null)
            { i = 0; }
            else
            { i = (int)obj + 1; }
            string sql1 = string.Format("insert into dbo.tReportTemplateDirec values('{0}','{1}',-1,'{2}',{3},{4},'{5}','{6}',{7},'{8}','phrase')",
               model.ItemGuid, model.ParentGuid, model.TemplateName, i, model.Type, model.UserGuid, model.TemplateGuid, model.Leaf, CommonGlobalSettings.Utilities.GetCurDomain());
            try
            {
                dataAccess.BeginTransaction();
                int m = 1;
                if (model.Leaf == 1)
                {
                    m = dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }
                int n = dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                if (m == 0 || n == 0)
                {
                    dataAccess.RollbackTransaction();
                    return false;
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
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
        /// <summary>
        /// Name:EditPhraseTemplate
        /// Function:Modify phrase template value
        /// </summary>
        /// <param name="model">The model contains new values of Phrase template</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool EditPhraseTemplate(PhraseTemplateModel model)
        {
            RisDAL dataAccess = new RisDAL();
            model.TemplateInfo = model.TemplateInfo.Replace("'", "''");
            string sql;
            if (!string.IsNullOrWhiteSpace(model.ModalityType))
            {
                sql = string.Format("Update tPhraseTemplate set ModalityType='{1}',TemplateName='{2}',TemplateInfo='{3}',ShortcutCode='{4}',type={5}  where TemplateGuid='{0}'",
                    model.TemplateGuid, model.ModalityType, model.TemplateName, model.TemplateInfo, model.ShortcutCode, model.Type);
            }
            else
            {
                sql = string.Format("Update tPhraseTemplate set TemplateName='{0}'where TemplateGuid='{1}'", model.TemplateName, model.TemplateGuid);
            }
            string sql1 = string.Format("Update tReportTemplateDirec set ItemName='{0}' where ItemGUID='{1}' and DirectoryType = 'phrase'", model.TemplateName, model.ItemGuid);
            try
            {
                dataAccess.BeginTransaction();
                int i = 1;
                if (model.Leaf == 1)
                {
                    i = dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }
                int j = dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                if (i == 0 || j == 0)
                {
                    dataAccess.RollbackTransaction();
                    return false;
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
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
        /// <summary>
        /// Name:DeletePhraseTemplate
        /// Function:Delete phrase template
        /// </summary>
        /// <param name="strTemplateGuid">Phrase template guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeletePhraseTemplate(string strTemplateGuid, string strItemGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("select * from tReportTemplateDirec where ParentID = '{0}'", strItemGuid);
            string sql1 = string.Format("Delete from tPhraseTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            string sql2 = string.Format("Delete from tReportTemplateDirec where ItemGUID = '{0}' and DirectoryType = 'phrase'", strItemGuid);
            DataTable dt = dataAccess.ExecuteQuery(sql);

            try
            {
                dataAccess.BeginTransaction();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string TemplateGuid, ItemGuid;
                        TemplateGuid = row["TemplateGuid"] == DBNull.Value ? "" : row["TemplateGuid"].ToString();
                        ItemGuid = row["ItemGUID"] == DBNull.Value ? "" : row["ItemGUID"].ToString();
                        if (!DeletePhraseTemplate(TemplateGuid, ItemGuid))
                        {
                            dataAccess.RollbackTransaction();
                            return false;
                        }
                    }
                }

                int i = 1;
                if (!string.IsNullOrWhiteSpace(strTemplateGuid))
                {
                    i = dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                }
                int j = dataAccess.ExecuteNonQuery(sql2, RisDAL.ConnectionState.KeepOpen);
                if (i == 0 || j == 0)
                {
                    dataAccess.RollbackTransaction();
                    return false;
                }
                dataAccess.CommitTransaction();

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
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

        public virtual bool UpDownNode(string strItemGuid1, string strItemGuid2)
        {
            RisDAL dataAccess = new RisDAL();
            int m;
            string sql = string.Format("select max(ItemOrder) from tReportTemplateDirec where ItemGUID = '{0}'and DirectoryType = 'phrase'", strItemGuid1);
            m = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
            string sql1 = string.Format("update tReportTemplateDirec set ItemOrder = (select ItemOrder from tReportTemplateDirec where ItemGUID = '{0}' and DirectoryType = 'phrase') where ItemGUID = '{1}'and DirectoryType = 'phrase'", strItemGuid2, strItemGuid1);
            string sql2 = string.Format("update tReportTemplateDirec set ItemOrder = {0} where ItemGUID = '{1}'and DirectoryType = 'phrase'", m, strItemGuid2);

            try
            {
                dataAccess.BeginTransaction();
                int i = dataAccess.ExecuteNonQuery(sql1, RisDAL.ConnectionState.KeepOpen);
                int j = dataAccess.ExecuteNonQuery(sql2, RisDAL.ConnectionState.KeepOpen);
                if (i == 0 || j == 0)
                {
                    dataAccess.RollbackTransaction();
                    return false;
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
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

        public virtual bool CopyPhraseLeafNode(string strItemID, string strParentItemID, string strUserguid, ref string newTemplateName, ref string newItemGuid, ref string newTemplateGuid, ref string orderIndex)
        {
            //KodakDAL dataAccess = new KodakDAL();
            newItemGuid = Guid.NewGuid().ToString();
            newTemplateGuid = Guid.NewGuid().ToString();
            int type = GetItemType(strParentItemID,"phrase");
            string newItemName = createCopyMoveName(strItemID, strParentItemID, type, strUserguid, "phrase");
            int maxOrerID = GetItemMaxOrder(strParentItemID, type, strUserguid, "phrase");
           
            newTemplateName = newItemName;
            orderIndex = Convert.ToString(maxOrerID + 1);

            //insert into report template diectory table
            string strInsertDirecSQL = string.Format(
            @"if exists(select 1 from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='phrase'  )
            insert into tReportTemplateDirec  select '{1}' as ItemGUID,'{2}',Depth,'{3}',
            {4},{5} ,'{6}','{7}',Leaf,Domain,[DirectoryType] from tReportTemplateDirec where  ItemGUID = '{0}' and [DirectoryType]='phrase'",
            strItemID, newItemGuid, strParentItemID, newItemName, maxOrerID + 1, type, strUserguid, newTemplateGuid);

            //isnert into Phrase template table
            string strInsertSQL = string.Format(
            @"insert into tPhraseTemplate select '{0}',ModalityType,'{1}'as TemplateName,
            TemplateInfo,'',Type,UserGuid,Domain
            from tPhraseTemplate where TemplateGuid = (select TemplateGuid from tReportTemplateDirec where ItemGUID ='{2}' and [DirectoryType]='phrase')",
            newTemplateGuid, newItemName, strItemID);

            RisDAL dal = new RisDAL();
            try
            {

                dal.BeginTransaction();
                int result = dal.ExecuteNonQuery(strInsertDirecSQL, RisDAL.ConnectionState.KeepOpen);
                if (result != -1)
                {
                    result = dal.ExecuteNonQuery(strInsertSQL, RisDAL.ConnectionState.KeepOpen);
                    if (result != -1)
                    {
                        dal.CommitTransaction();
                    }
                    else
                    {
                        dal.RollbackTransaction();
                        return false;
                    }
                }
                else
                {
                    dal.RollbackTransaction();
                    return false;
                }

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dal.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return true;
        }

        public virtual bool MovePhraseLeafNode(string strItemID, string strParentItemID, string strUserguid, ref string newTemplateName, ref string orderIndex)
        {
            //KodakDAL dataAccess = new KodakDAL();
            int itemOrder = GetItemOrder(strItemID, "phrase");
            int type = GetItemType(strParentItemID, "phrase");
            string parentID = GetItemParentID(strItemID, "phrase");
            string newItemName = createCopyMoveName(strItemID, strParentItemID, type, strUserguid, "phrase");
            int maxOrerID = GetItemMaxOrder(strParentItemID, type, strUserguid, "phrase");
            int orginMaxOrderID = GetItemMaxOrder(parentID, type, strUserguid, "phrase");
            orderIndex = Convert.ToString(maxOrerID + 1);
            newTemplateName = newItemName;

            //update some field in template diectory table
            string strUpdateDirecSQL = string.Empty;
            if (type == 0)
            {
                strUpdateDirecSQL = string.Format("if exists(select 1 from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='phrase' ) update tReportTemplateDirec set Type = {1}, ParentID = '{2}',ItemOrder='{3}', ItemName ='{4}' where ItemGUID ='{0}' and [DirectoryType]='phrase' \r\n",
                           strItemID, type, strParentItemID, maxOrerID + 1, newItemName);
            }
            else
            {
                strUpdateDirecSQL = string.Format("if exists(select 1 from tReportTemplateDirec where ItemGUID = '{0}' and [DirectoryType]='phrase' ) update tReportTemplateDirec set Type = {1}, ParentID = '{2}',ItemOrder='{3}', UserGuid='{4}', ItemName ='{5}' where ItemGUID ='{0}' and [DirectoryType]='phrase' \r\n",
                           strItemID, type, strParentItemID, maxOrerID + 1, strUserguid, newItemName);
            }

            if (orginMaxOrderID != -1)
            {
                int position = orginMaxOrderID - itemOrder;//item position
                if (position > 0)
                {
                    strUpdateDirecSQL += string.Format("update tReportTemplateDirec set ItemOrder = ItemOrder-1  where ItemOrder > {0} and ParentID = '{1}' and [DirectoryType]='phrase' \r\n ", itemOrder, parentID);
                    strUpdateDirecSQL += string.Format("update tPhraseTemplate Set TemplateName = '{0}' where TemplateGuid = (select TemplateGuid from tReportTemplateDirec where ItemGuid = '{1}' and [DirectoryType]='phrase') ", newItemName, strItemID);
                }
            }


            RisDAL dal = new RisDAL();
            try
            {

                dal.BeginTransaction();
                int result = dal.ExecuteNonQuery(strUpdateDirecSQL, RisDAL.ConnectionState.KeepOpen);
                if (result != -1)
                {
                    dal.CommitTransaction();
                    return true;
                }
                else
                {
                    dal.RollbackTransaction();
                    return false;
                }

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dal.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return true;
        }

        /// <summary>
        /// Name:LoadPhraseTemplateByShortcut
        /// Function:Load phrase template by shortcut
        /// </summary>
        /// <param name="strShortcut">Phrase template shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains phrase template information</returns>
        public virtual DataSet LoadPhraseTemplateByShortcut(string strShortcut, string strUserGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from tPhraseTemplate where ShortcutCode = '{0}' and (Type=0 or (Type=1 and UserGuid = '{1}'))", strShortcut, strUserGuid);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:ExistExaminTemplateName
        /// Function:Check if there is the same name examin template
        /// </summary>
        /// <param name="strTemplateName">New Template Name</param>
        /// <param name="strUserGuid">User Guid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True: Success False:Fail</returns>
        public bool ExistExaminTemplateName(string strTemplateName, string strUserGuid, int type)
        {
            RisDAL dataAccess = new RisDAL();
            int count = 0;
            string sql;
            if (type == 1)
            {
                sql = string.Format("select TemplateName from tExamineTemplate where TemplateName  = '{0}' and (Type=0 or (Type=1 and UserGuid = '{1}'))", strTemplateName, strUserGuid);
            }
            else
            {
                sql = string.Format("select TemplateName from tExamineTemplate where TemplateName = '{0}' ", strTemplateName);
            }
            try
            {
                //count = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
                //Because database don't support the upper or lower charactor,so do like this
                DataTable myTable = dataAccess.ExecuteQuery(sql);
                foreach (DataRow myRow in myTable.Rows)
                {
                    if (myRow[0].ToString() == strTemplateName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return true;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

        }
        /// <summary>
        /// Name:ExistPhaseTemplateName
        /// Function:Check if there is the same name Phrase Template
        /// </summary>
        /// <param name="strTemplateName">New Template Name</param>
        /// <param name="strUserGuid">User Guid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True: Success False:Fail</returns>
        public bool ExistPhaseTemplateName(string strTemplateName, string strUserGuid, int type)
        {
            RisDAL dataAccess = new RisDAL();
            int count = 0;
            string sql;
            if (type == 1)
            {
                sql = string.Format("select TemplateName from tPhraseTemplate where TemplateName  = '{0}' and (Type=0 or (Type=1 and UserGuid = '{1}'))", strTemplateName, strUserGuid);
            }
            else
            {
                sql = string.Format("select TemplateName from tPhraseTemplate where TemplateName = '{0}' ", strTemplateName);
            }
            try
            {
                DataTable myTable = dataAccess.ExecuteQuery(sql);
                //Because database don't support the upper or lower charactor,so do like this
                foreach (DataRow myRow in myTable.Rows)
                {
                    if (myRow[0].ToString() == strTemplateName)
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return true;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

        }

        /// <summary>
        /// Name:ExistShortcut
        /// Funtion:Checking if the shortcut is existed
        /// </summary>
        /// <param name="strShortcut">Phrase template shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool ExistShortcut(string strShortcut, string strUserGuid, int type,string templateGuid)
        {
            RisDAL dataAccess = new RisDAL();
            int count = 0;
            string sql;

            try
            {
                if (type == 1)
                {
                    sql = string.Format("select count(*) from tPhraseTemplate where ShortcutCode = '{0}' and (Type<>1 or (Type=1 and UserGuid = '{1}')) and TemplateGuid <> '{2}'", strShortcut, strUserGuid, templateGuid);

                    count = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
                }
                else if (type == 2)
                {
                    count = GetCountForReportOrPhraseBySiteShortcut(dataAccess, strShortcut, strUserGuid, "phrase");
                }
                else
                {
                    sql = string.Format("select count(*) from tPhraseTemplate where ShortcutCode = '{0}'and TemplateGuid <> '{1}' ", strShortcut, templateGuid);

                    count = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
                }
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            if (count == 0)
                return false;
            else
                return true;
        }
        #endregion
        #region ExaminTemplate
        /// <summary>
        /// Name:LoadExaminTemplatesByModality
        /// Funtion:Load examine templates by modality
        /// </summary>
        /// <param name="strModalityType">Modality type</param>
        /// <param name="type">Global:0 User:1</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains examine templates</returns>
        public virtual DataSet LoadExaminTemplatesByModality(string strModalityType, int type, string strUserGuid, string site)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "";
            if (type == 0)
            {
                sql = string.Format("select TemplateGuid,TemplateName,ShortcutCode from tExamineTemplate where ModalityType = '{0}' and Type = 0", strModalityType);
            }
            else if (type == 1)
            {
                sql = string.Format("select TemplateGuid,TemplateName,ShortcutCode from tExamineTemplate where ModalityType = '{0}' and Type = 1 and UserGuid='{1}' ", strModalityType, strUserGuid);
            }
            else if (type == 2)
            {
                sql = string.Format("select TemplateGuid,TemplateName,ShortcutCode from tExamineTemplate where ModalityType = '{0}' and Type = 2 and Site='{1}' ", strModalityType, site);
            }
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:LoadExaminTemplateByGuid
        /// Function:Load examine template by guid
        /// </summary>
        /// <param name="strTemplateGuid">Examine template guid</param>
        /// <returns>Return dataset contains one table, this table contains examine template information</returns>
        public virtual DataSet LoadExaminTemplateByGuid(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select TemplateName,TemplateInfo,ShortcutCode from tExamineTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:AddExaminTemplate
        /// Function:Add a new examine template
        /// </summary>
        /// <param name="model">The model contains values of  the examine template</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddExaminTemplate(ExaminTemplateModel model)
        {
            model.TemplateInfo = model.TemplateInfo.Replace("'", "''");
            RisDAL dataAccess = new RisDAL();
            string sql = string.Format("Insert into tExamineTemplate(TemplateGuid,ModalityType,TemplateName,TemplateInfo,ShortcutCode,Type,UserGuid,Domain,Site) values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}')"
                , model.TemplateGuid, model.ModalityType, model.TemplateName, model.TemplateInfo, model.ShortcutCode, model.Type, model.UserGuid, CommonGlobalSettings.Utilities.GetCurDomain()
                , model.Site);
            try
            {
                dataAccess.ExecuteNonQuery(sql);
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
        /// <summary>
        /// Name:EditExaminTemplate
        /// Function:Modify the examine template
        /// </summary>
        /// <param name="model">The model contains values of the examine template</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool EditExaminTemplate(ExaminTemplateModel model)
        {
            RisDAL dataAccess = new RisDAL();
            model.TemplateInfo = model.TemplateInfo.Replace("'", "''");
            string sql = string.Format("Update tExamineTemplate set ModalityType='{1}',TemplateName='{2}',TemplateInfo='{3}',ShortcutCode='{4}',type={5},Site='{6}'  where TemplateGuid='{0}'",
                model.TemplateGuid, model.ModalityType, model.TemplateName, model.TemplateInfo, model.ShortcutCode, model.Type
                , model.Site);

            try
            {
                int i = dataAccess.ExecuteNonQuery(sql);
                if (i == 0)
                    return false;
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
        /// <summary>
        /// Name:DeleteExaminTemplate
        /// Function:Delete examine template
        /// </summary>
        /// <param name="strTemplateGuid">Examine template guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteExaminTemplate(string strTemplateGuid)
        {
            RisDAL dataAccess = new RisDAL();

            string sql = string.Format("Delete from tExamineTemplate where TemplateGuid = '{0}'", strTemplateGuid);
            try
            {
                int i = dataAccess.ExecuteNonQuery(sql);
                if (i == 0)
                    return false;
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
        /// <summary>
        /// Name:LoadExaminTemplateByShortcut
        /// Function:Load examine template by shortcut
        /// </summary>
        /// <param name="strShortcut">Examine template shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains examine template information</returns>
        public virtual DataSet LoadExaminTemplateByShortcut(string strShortcut, string strUserGuid, string site)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from tExamineTemplate where ShortcutCode = '{0}' and (Type=0 or (Type=1 and UserGuid = '{1}') or (Type=2 and SITE = '{2}'))",
                strShortcut, strUserGuid, site);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }
        /// <summary>
        /// Name:ExistExaminShortcut
        /// Function:Checking if the shortcut is existed
        /// </summary>
        /// <param name="strShortcut">Examine shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool ExistExaminShortcut(string strShortcut, string strUserGuid, int type, string site)
        {
            RisDAL dataAccess = new RisDAL();
            int count = 0;
            string sql;
            if (type == 1)
            {
                sql = string.Format("select count(*) from tExamineTemplate where ShortcutCode = '{0}' and (Type=0 or (Type=1 and UserGuid = '{1}'))", strShortcut, strUserGuid);
            }
            else if (type == 2)
            {
                sql = string.Format("select count(*) from tExamineTemplate where ShortcutCode = '{0}' and (Type=0 or (Type=2 and SITE = '{1}'))", strShortcut, site);
            }
            else
            {
                sql = string.Format("select count(*) from tExamineTemplate where ShortcutCode = '{0}'", strShortcut);
            }
            try
            {
                count = Convert.ToInt32(dataAccess.ExecuteScalar(sql));

            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            if (count == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region EmergencyTemplate
        public virtual bool QueryEYTemplate(DataSet ds, string strTemplateType, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = string.Format("SELECT TemplateGuid,TemplateName,NamePrefix,Birthday,Telephone,InhospitalNo,ClinicNo,BedNo,ProcedureCode,Description,Gender," +
                                "Gender,ApplyDept,ApplyDoctor,TemplateType,Site " +
                                " FROM tEmergencyTemplate where TemplateType={0}", strTemplateType);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "EYTemplate";
                ds.Tables.Add(dt);

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool SaveEYTemplate(DataSet ds, ref string strError)
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
                    string strTemplateGuid = dr["TemplateName"].ToString();
                    string strSQL = string.Format("SELECT count(*) FROM tEmergencyTemplate where TemplateName='{0}'", strTemplateGuid);
                    int nCount = (int)oKodak.ExecuteScalar(strSQL);
                    if (nCount > 0)
                    {
                        throw new Exception("Reduplicate template name");
                    }
                    strBuiler.Remove(0, strBuiler.Length);
                    strBuiler = strBuiler.AppendFormat("INSERT INTO tEmergencyTemplate(TemplateGuid,TemplateName,NamePrefix,Birthday,Gender,Telephone,InhospitalNo,ClinicNo,BedNo,")
                        .AppendFormat("ApplyDept,ApplyDoctor,ProcedureCode,Description,Domain,TemplateType,Site) ")
                        .AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}',", dr["TemplateGuid"].ToString(), dr["TemplateName"].ToString(), dr["NamePrefix"].ToString(), ((DateTime)dr["Birthday"]).ToShortDateString(), dr["Gender"].ToString(), dr["Telephone"].ToString())
                        .AppendFormat("'{0}','{1}','{2}','{3}','{4}',", dr["InhospitalNo"].ToString(), dr["ClinicNo"].ToString(), dr["BedNo"].ToString(), dr["ApplyDept"].ToString(), dr["ApplyDoctor"].ToString())
                        .AppendFormat("'{0}','{1}','{2}',{3},'{4}')", dr["ProcedureCode"].ToString(), dr["Description"].ToString(), CommonGlobalSettings.Utilities.GetCurDomain(), Convert.ToInt32(dr["TemplateType"]), dr["Site"].ToString());
                    listSQL.Add(strBuiler.ToString());
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

        public virtual bool DelEYTemplate(string strTemplateGuid, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {

                string strSQL = string.Format("DELETE FROM tEmergencyTemplate WHERE TemplateGuid='{0}'", strTemplateGuid);
                oKodak.ExecuteNonQuery(strSQL);

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
        public virtual bool UpdateEYTemplate(DataSet ds, ref string strError)
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
                    string strTemplateName = dr["TemplateName"].ToString();
                    string strTemplateGuid = dr["TemplateGuid"].ToString();

                    string strSQL = string.Format("SELECT count(*) FROM tEmergencyTemplate where TemplateGuid='{0}'", strTemplateGuid);
                    int nCount = (int)oKodak.ExecuteScalar(strSQL);
                    if (nCount == 0)
                    {
                        throw new Exception("Can not save due to the template not exists.");
                    }

                    strSQL = string.Format("SELECT count(*) FROM tEmergencyTemplate where TemplateName='{0}' AND TemplateGuid!='{1}'", strTemplateName, strTemplateGuid);
                    nCount = (int)oKodak.ExecuteScalar(strSQL);
                    if (nCount > 0)
                    {
                        throw new Exception("Reduplicate template name");
                    }


                    strBuiler.Remove(0, strBuiler.Length);
                    strBuiler = strBuiler.AppendFormat("UPDATE tEmergencyTemplate SET TemplateName='{0}',NamePrefix='{1}',Birthday='{2}',Gender='{3}',Telephone='{4}',", dr["TemplateName"].ToString(), dr["NamePrefix"].ToString(), ((DateTime)dr["Birthday"]).ToShortDateString(), dr["Gender"].ToString(), dr["Telephone"].ToString())
                    .AppendFormat("InhospitalNo='{0}',ClinicNo='{1}',BedNo='{2}',ApplyDept='{3}',", dr["InhospitalNo"].ToString(), dr["ClinicNo"].ToString(), dr["BedNo"].ToString(), dr["ApplyDept"].ToString())
                    .AppendFormat("ApplyDoctor='{0}',ProcedureCode='{1}',Description='{2}',Site='{3}' WHERE TemplateGuid='{4}'", dr["ApplyDoctor"].ToString(), dr["ProcedureCode"].ToString(), dr["Description"].ToString(), dr["Site"].ToString(), dr["TemplateGuid"].ToString());

                    listSQL.Add(strBuiler.ToString());

                    //string strApplyDept = dr["ApplyDept"].ToString();
                    //string strApplyDoctor = dr["ApplyDoctor"].ToString();
                    //strApplyDept = strApplyDept.Trim();
                    //if (strApplyDept.Length > 0)
                    //{

                    //    strSQL = string.Format("SELECT count(*) FROM tDictionaryValue WHERE (Value='{0}' or Text='{1}') and Tag=2", strApplyDept, strApplyDept);
                    //    nCount = (int)oKodak.ExecuteScalar(strSQL);
                    //    if (nCount == 0)
                    //    {
                    //        strSQL = string.Format("INSERT INTO tDictionaryValue(Tag,Value,Text,shortcutcode,Domain) VALUES(2,'{0}','{1}','','{2}')", strApplyDept, strApplyDept, CommonGlobalSettings.Utilities.GetCurDomain());
                    //        listSQL.Add(strSQL);

                    //    }
                    //}

                    //strApplyDoctor = strApplyDoctor.Trim();
                    //if (strApplyDoctor.Length > 0)
                    //{
                    //    strSQL = string.Format("SELECT count(*) FROM tDictionaryValue WHERE (Value='{0}' or Text='{1}') and Tag=8", strApplyDoctor, strApplyDoctor);
                    //    nCount = (int)oKodak.ExecuteScalar(strSQL);
                    //    if (nCount == 0)
                    //    {
                    //        strSQL = string.Format("INSERT INTO tDictionaryValue(Tag,Value,Text,shortcutcode,Domain) VALUES(8,'{0}','{1}','','{2}')", strApplyDoctor, strApplyDoctor, CommonGlobalSettings.Utilities.GetCurDomain());
                    //        listSQL.Add(strSQL);

                    //    }
                    //}
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
        public bool LockEYTemplate(string strTemplateGuid, string strOwner, string strOwnerIP, ref string strLockInfo, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            LogManager lm = new LogManager();
            DataTable dt = new DataTable();
            try
            {

                string strSQL = string.Format("SELECT * FROM tSync WHERE SyncType={0} and Guid='{1}'", 5, strTemplateGuid);
                oKodak.ExecuteQuery(strSQL, dt);
                if (dt.Rows.Count > 0)
                {
                    bReturn = false;
                    DataRow dr = dt.Rows[0];
                    strLockInfo = string.Format("Owner={0}&OwnerIP={1}", dr["Owner"].ToString(), dr["OwnerIP"].ToString());
                }
                else
                {
                    strSQL = string.Format("INSERT INTO tSync(SyncType,Guid,Owner,OwnerIP) VALUES({0},'{1}','{2}','{3}') ", 5, strTemplateGuid, strOwner, strOwnerIP);
                    lm.Debug((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 53, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    oKodak.ExecuteNonQuery(strSQL);

                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        public bool UnLockEYTemplate(string strTemplateGuid, string strOwner, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            LogManager lm = new LogManager();
            try
            {
                string strSQL = "";
                strTemplateGuid = strTemplateGuid.Trim();
                if (strTemplateGuid.Length > 0)
                {
                    strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} and Guid='{1}'", 5, strTemplateGuid);
                }
                else
                {
                    strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} AND Owner='{1}'", 5, strOwner);
                }
                lm.Debug((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 53, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                oKodak.ExecuteNonQuery(strSQL);

            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


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
        #region BookingNoticeTemplate
        #region ** 1.Add
        /// <summary>
        /// Name:AddBookingNoticeTemplate
        /// Function:Add a new Booking Notice template
        /// </summary>
        /// <param name="model">The model contains values of Booking Notice</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddBookingNoticeTemplate(BookingNoticeModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            int exeResult = -1;
            string sqlExitsSameName = string.Format("select count(1) from tBookingNoticeTemplate where ModalityType='{0}' and TemplateName = '{1}'", model.ModalityType, model.TemplateName);
            string sql = string.Format("Insert into tBookingNoticeTemplate(Guid,ModalityType,TemplateName,BookingNotice,Domain) values('{0}','{1}','{2}',@BookingNotice,'{3}')"
                , model.Guid, model.ModalityType, model.TemplateName, CommonGlobalSettings.Utilities.GetCurDomain());
            try
            {
                int exsits = Convert.ToInt32(oKodakDAL.ExecuteScalar(sqlExitsSameName));
                if (exsits > 0)
                {
                    throw new Exception("EXCEPTION.bookingnoticetemplate.exitssamename");
                }
                model.Guid = model.Guid == null ? Guid.NewGuid().ToString() : model.Guid;
                oKodakDAL.Parameters.Add("@BookingNotice", Encoding.UTF8.GetBytes(model.BookingNotice));
                exeResult = oKodakDAL.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                if (e.Message.Equals("EXCEPTION.bookingnoticetemplate.exitssamename"))
                {
                    throw new Exception("EXCEPTION.bookingnoticetemplate.exitssamename");
                }
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return true;
        }
        #endregion

        #region ** 2.Modify
        /// <summary>
        /// Name:ModiyBookingNoticeTemplate
        /// Function:Modify a Booking Notice template
        /// </summary>
        /// <param name="model">The model contains values of Booking Notice</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool ModifyBookingNoticeTemplate(BookingNoticeModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            int exeResult = -1;
            string sqlExitsSameName = string.Format("select count(1) from tBookingNoticeTemplate where ModalityType='{0}' and TemplateName = '{1}' and Guid <> '{2}'", model.ModalityType, model.TemplateName, model.Guid);
            string sqlIsExsits = string.Format("Select 1 from tBookingNoticeTemplate where guid = '{0}'", model.Guid);
            string sqlUpdate = string.Format("Update tBookingNoticeTemplate set TemplateName='{0}',BookingNotice = @BookingNotice where guid ='{1}'", model.TemplateName, model.Guid);
            try
            {
                //1. if not exist,insert it!
                exeResult = Convert.ToInt32(oKodakDAL.ExecuteScalar(sqlIsExsits));
                if (exeResult != 1)
                {
                    exeResult = Convert.ToInt32(oKodakDAL.ExecuteScalar(sqlExitsSameName));
                    if (exeResult == 1)
                    {
                        throw new Exception("EXCEPTION.bookingnoticetemplate.exitssamename");
                        return false;
                    }
                    return AddBookingNoticeTemplate(model);
                }
                else
                {
                    exeResult = Convert.ToInt32(oKodakDAL.ExecuteScalar(sqlExitsSameName));
                    if (exeResult == 1)
                    {
                        throw new Exception("EXCEPTION.bookingnoticetemplate.exitssamename");
                        return false;
                    }
                }
                //2. update it
                oKodakDAL.Parameters.Add("@BookingNotice", Encoding.UTF8.GetBytes(model.BookingNotice));
                exeResult = oKodakDAL.ExecuteNonQuery(sqlUpdate);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                if (e.Message.Equals("EXCEPTION.bookingnoticetemplate.exitssamename"))
                {
                    throw new Exception("EXCEPTION.bookingnoticetemplate.exitssamename");
                }
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return true;
        }
        #endregion

        #region ** 3.Delete
        /// <summary>
        /// Name:DeleteBookingNoticeTemplate
        /// Function:Delete a Booking Notice template
        /// </summary>
        /// <param name="model">The model contains values of Booking Notice</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteBookingNoticeTemplate(BookingNoticeModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            int exeResult = -1;
            string sqlIsExsit = string.Format("select 1 from tBookingNoticeTemplate where guid ='{0}'", model.Guid);
            string sqlDelete = string.Format("Delete tBookingNoticeTemplate where guid ='{0}'", model.Guid);
            try
            {
                //1. first check if exsits
                exeResult = Convert.ToInt32(oKodakDAL.ExecuteScalar(sqlIsExsit));
                if (exeResult != 1)
                {
                    return true;//not exsits,it is deleted by others,so return true!
                }
                //2. if exsits delete it
                exeResult = Convert.ToInt32(oKodakDAL.ExecuteNonQuery(sqlDelete));
                if (exeResult <= 0)
                {
                    return false;
                }
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
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return true;
        }
        #endregion
        #region 4.get all booking notice
        /// <summary>
        /// Name:GetAllBookingNoticeTemplate
        /// Function:get all Booking Notice template
        /// </summary>
        /// <param name="model">The model contains values of Booking Notice</param>
        /// <returns>the booking notice template dataset</returns>
        /// 
        public virtual DataSet GetAllBookingNoticeTemplate()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select Guid,ModalityType,TemplateName from tBookingNoticeTemplate");
            try
            {
                dt = oKodakDAL.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return ds;
        }
        #endregion

        #region 5.get one booking notice
        /// <summary>
        /// Name:GetOneBookingNoticeTemplate
        /// Function:get one Booking Notice template
        /// </summary>
        /// <param name="model">The model contains values of Booking Notice</param>
        /// <returns>one of the  booking notice template dataset</returns>
        /// 
        public virtual DataSet GetOneBookingNoticeTemplate(BookingNoticeModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from tBookingNoticeTemplate where guid = '{0}'", model.Guid);
            try
            {
                dt = oKodakDAL.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return ds;
        }
        #endregion
        #region 5.get booking notice by guids
        /// <summary>
        /// Name:GetBookingNoticeTemplates
        /// Function:get some Booking Notice templates by guids
        /// </summary>
        /// <param name="model">The model contains values of Booking Notice</param>
        /// <returns>one of the  booking notice template dataset</returns>
        /// 
        public virtual DataSet GetBookingNoticeTemplates(BookingNoticeModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string guids = string.IsNullOrWhiteSpace(model.Guids) ? "" : model.Guids;
            string sql = string.Format("select * from tBookingNoticeTemplate where guid {0}", convert2InCondition(guids, ","));
            try
            {
                dt = oKodakDAL.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return ds;
        }
        #endregion
        /// <summary>
        /// convert2InCondition use for multi-condition splitted by dilimiter to In clause
        /// </summary>
        /// <param name="oneCondition"></param>
        /// <param name="dilimiter"></param>
        /// <returns></returns>
        private string convert2InCondition(string oneCondition, string dilimiter)
        {
            //just want one by one condition
            if (string.IsNullOrEmpty(oneCondition) || !oneCondition.Contains(dilimiter))
            {
                return string.Format("='{0}'", oneCondition);
            }
            //multi conditions splitted by dilimiter
            string strTmp = string.Empty;
            foreach (string str in oneCondition.Split(dilimiter.ToCharArray()))
            {
                strTmp += string.Format("'{0}',", str);
            }
            strTmp = "in (" + strTmp.TrimEnd(new char[] { ',' }) + ")";
            return strTmp;
        }
        #endregion

        public void GetPrintData(string accno, string modalityType, string templateType, ref string template, ref DataTable data)
        {
            try
            {
                service.GetPDFListtoPrint(accno, modalityType, templateType, ref template, ref data);
            }
            catch (Exception ex)
            {
                lm.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
                  (new System.Diagnostics.StackFrame(true)).GetFileName(),
                  Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
        }
    }
}
