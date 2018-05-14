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
/*                                                                          */
/*                        Author : Bruce Deng
/****************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using DataAccessLayer;
using Common.Consts;

namespace Server.DAO.Common.Impl
{
    public class AbstractCommonFunctionImpl : ICommonFunctionDao
    {
        LogManager logger = new LogManager();
        
        public virtual bool GetModalityType(DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "SELECT ModalityType,SOPClass,Site FROM tModalityType order by ModalityType";
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "ModalityType";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetAllWarningTime(DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "select * from tWarningTime where type = 'UnapprovedReport'";
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "WarningTime";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetBodyCategory(string strModalityType, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = string.Format("SELECT BodyCategory, max(Frequency) as sum_of_frequency from tProcedureCode where ModalityType='{0}' group by BodyCategory order by sum_of_frequency desc", strModalityType);
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "tBodyCategory";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetBodyPart(string strModalityType, string strBodyCategory, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                if (strModalityType.Length == 0)
                {
                    strSQL = string.Format("SELECT DISTINCT BodyPart FROM tProcedureCode where BodyCategory='{1}'", strBodyCategory);
                }
                else if (strBodyCategory.Length == 0)
                {
                    strSQL = string.Format("SELECT DISTINCT BodyPart FROM tProcedureCode where ModalityType='{0}'", strModalityType);
                }
                else
                {
                    strSQL = string.Format("SELECT BodyPart,sum(Frequency) as sum_of_frequency FROM tProcedureCode where ModalityType='{0}' and BodyCategory='{1}' group by BodyPart order by sum_of_frequency desc", strModalityType, strBodyCategory);
                }
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "tBodyPart";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetCheckingItem(string strModalityType, string strBodyCategory, string strBodyPart, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dtCheckingItem = new DataTable();

            try
            {
                string strSQL = "";
                //use for register checking item selection $$$
                if ((strModalityType.Trim() == "$" && strModalityType.Trim() == "$" && strBodyPart.Trim() == "$"))
                {
                    strSQL = string.Format("SELECT distinct CheckingItem as Value, CheckingItem as Text, ModalityType from tProcedureCode");
                }
                else
                {
                    strSQL = string.Format("SELECT CheckingItem,max(frequency) as sum_of_frequency from tProcedureCode where ModalityType='{0}' and BodyCategory='{1}' and BodyPart='{2}' group by CheckingItem order by sum_of_frequency desc", strModalityType, strBodyCategory, strBodyPart);
                }
                oKodak.ExecuteQuery(strSQL, dtCheckingItem);
                dtCheckingItem.TableName = "tCheckingItem";
                ds.Tables.Add(dtCheckingItem);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));


            }
            finally
            {
                if (dtCheckingItem != null)
                {
                    dtCheckingItem.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool GetExamSystem(string strModalityType, string strBodyPart, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                if (strModalityType.Length == 0 && strBodyPart.Length == 0)
                {
                    strSQL = "SELECT * from tBodySystemMap";
                }
                else
                {
                    strSQL = string.Format("SELECT distinct ExamSystem from tBodySystemMap where ModalityType='{0}' and Bodypart='{1}'", strModalityType, strBodyPart);
                }
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "ExamSystem";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual bool GetModality(string strModalityType, DataSet ds, string site, string withPublicSite)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                if (strModalityType.Length == 0)
                {
                    if (string.IsNullOrWhiteSpace(site))
                    {
                        strSQL = "SELECT * from tModality where Site='' or Site is null order by Modality";
                    }
                    else
                    {
                        if (withPublicSite == "1")
                        {
                            strSQL = string.Format("SELECT * from tModality where Site='{0}' or Site='' or Site is null order by Modality", site);
                        }
                        else
                        {
                            strSQL = string.Format("SELECT * from tModality where Site='{0}' order by Modality", site);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(site))
                    {
                        strSQL = string.Format("SELECT * from tModality where ModalityType='{0}' and (Site='' or Site is null) order by Modality", strModalityType);
                    }
                    else
                    {
                        if (withPublicSite == "1")
                        {
                            strSQL = string.Format("SELECT * from tModality where ModalityType='{0}' and (Site='{1}' or Site='' or Site is null) order by Modality", strModalityType, site);
                        }
                        else
                        {
                            strSQL = string.Format("SELECT * from tModality where ModalityType='{0}' and Site='{1}' order by Modality", strModalityType, site);
                        }
                    }
                }
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "Modality";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual bool GetAllModality(string strModalityType, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                if (strModalityType.Length == 0)
                {
                    strSQL = "SELECT * from tModality order by ModalityType";
                }
                else
                {
                    strSQL = string.Format("SELECT * from tModality where ModalityType='{0}' order by Modality", strModalityType);
                }                
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "AllModality";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual bool LoadShortcut(int category, string strUserID, ref DataSet reDataSet)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";

                strSQL = string.Format(
                    " SELECT * from tShortcut where Category={0} "
                    + " AND (Type=0 OR (Type=1 AND Owner='{1}') OR (Type=2 AND ','+Owner+',' like '%,{2},%')) "
                    + " AND Domain='{3}' "
                    + " ORDER BY Name",
                    category, strUserID.Trim(), CommonGlobalSettings.Utilities.GetCurSite(),
                    CommonGlobalSettings.Utilities.GetCurDomain());

                

                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "tShortcut";
                reDataSet.Tables.Add(dt);

                strSQL = string.Format(
                    " if EXISTS ( select 1 from tUserProfile where NAME='ManageSiteShortcut' AND UserGuid='{1}' AND VALUE='1' ) "
                    + " OR EXISTS ( select 1 from tRoleProfile r, tOnlineClient o where r.RoleName=o.RoleName AND o.UserGuid='{1}' "
                    + "      AND o.IsOnline=1 AND r.NAME='ManageSiteShortcut' AND r.VALUE='1' ) "
                    + " SELECT * from tShortcut where Category={0} AND (Type=0 OR (Type=1 AND Owner='{1}') OR Type=2) "
                    + " AND Domain='{3}' "
                    + " ORDER BY Name "
                    + " ELSE "
                    + " SELECT * from tShortcut where 1=2 ",
                    category, strUserID.Trim(), CommonGlobalSettings.Utilities.GetCurSite(),
                    CommonGlobalSettings.Utilities.GetCurDomain());

                DataTable dt2 = new DataTable();
                dt2 = oKodak.ExecuteQuery(strSQL);
                dt2.TableName = "tShortcut-For-Manager";
                reDataSet.Tables.Add(dt2);
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual string GetShorcutName(string strGuid)
        {
            string result = null;
            RisDAL oKodak = new RisDAL();
            string sql = string.Format(@"select Name from tShortcut where ShortcutGuid = '{0}'", strGuid.Trim());
            try
            {
                result = oKodak.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                return null;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));


            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return result;
        }

        public virtual int AddShortcut(int type, int category, string strName, string strValue, string strUserID, ref DataSet reDataSet)
        {
            int bReturn = 1;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            string strGuid = Guid.NewGuid().ToString();
            string strTempUser;

            if (type == 0)
            {
                strTempUser = "Global";
            }
            else
            {
                strTempUser = strUserID;
            }

            try
            {
                if (isDuplicatedShortcutName(strGuid, strName, category, type, strTempUser, oKodak))
                {
                    bReturn = 2;
                }
                else
                {
                    string strSQl2 = string.Format(
                        "insert into tShortcut(ShortcutGuid, Type, Category, Name, Value, Owner, Domain)  values('{0}', {1}, {2},'{3}','{4}','{5}','{6}')",
                        strGuid.Trim(), type, category, strName.Trim(), strValue.Trim(), strTempUser.Trim(), CommonGlobalSettings.Utilities.GetCurDomain());

                    oKodak.ExecuteNonQuery(strSQl2, RisDAL.ConnectionState.KeepOpen);
                }
            }
            catch (Exception ex)
            {
                bReturn = 0;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual int EditShortcut(string strGuid, int type, int category, string strName, string strValue, string strUserID, string strCurUser, string strManageSS, ref DataSet reDataSet)
        {
            int bReturn = 1;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                if (isDuplicatedShortcutName(strGuid, strName, category, type, type == 0 ? "Global" : strUserID, oKodak))
                {
                    bReturn = 2;
                    return bReturn;
                }


                string strSQL1 = string.Format(" Update tShortcut set Category = {0},Name = '{1}',Value ='{2}',Domain ='{3}' where ShortcutGuid ='{4}' ",
                    category, strName, strValue, CommonGlobalSettings.Utilities.GetCurDomain(), strGuid);

                string strSQL = string.Format("select owner from tShortcut where shortcutguid='{0}'", strGuid);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj == null || Convert.ToString(obj).Trim().Length == 0)
                {
                    throw new Exception("Can not find the owner of this shortcut");
                }
                string strOrgOwner = Convert.ToString(obj);
                string[] arrOwner = strOrgOwner.Split(",".ToCharArray());
                string strSQL2 = "";
                if (type == 0)
                {
                    strSQL2 = string.Format(" Update tShortcut set Type=0,Owner='Global' where ShortcutGuid ='{0}' ", strGuid);

                }
                else
                {
                    if (strManageSS == "0")
                    {
                        if (strUserID.ToUpper() == CommonGlobalSettings.Utilities.GetCurSite().ToUpper())
                        {

                            if (arrOwner.Length <= 1)
                            {
                                strSQL2 = string.Format(" Update tShortcut set Type=2,owner='{0}' where ShortcutGuid ='{1}' ", strUserID, strGuid);
                            }
                            else
                            {
                                strSQL2 = string.Format(" Update tShortcut set Type=2 where ShortcutGuid ='{0}' ", strGuid);
                            }



                        }
                        else if (strUserID.ToUpper() == strCurUser.ToUpper())
                        {


                            if (arrOwner.Length <= 1)
                            {
                                strSQL2 = string.Format(@"update tShortcut set Type=1,Owner='{0}' where ShortcutGuid = '{1}'", strUserID, strGuid.Trim());
                            }
                            else
                            {
                                string strTemp = "";
                                foreach (string str in arrOwner)
                                {
                                    if (str.ToUpper() != CommonGlobalSettings.Utilities.GetCurSite().ToUpper())
                                    {
                                        strTemp += str;
                                        strTemp += ",";
                                    }
                                }
                                strTemp = strTemp.TrimEnd(',');
                                strSQL2 = string.Format("update tShortcut set type=2,Owner='{0}' where ShortcutGuid = '{1}'", strTemp, strGuid.Trim());
                            }

                        }
                    }
                    else
                    {
                        strSQL2 = string.Format(" Update tShortcut set Type={0},Owner='{1}' where ShortcutGuid ='{2}' ", type, strUserID, strGuid);
                    }
                }

                oKodak.ExecuteNonQuery(strSQL1, RisDAL.ConnectionState.KeepOpen);
                oKodak.ExecuteNonQuery(strSQL2, RisDAL.ConnectionState.KeepOpen);

            }
            catch (Exception ex)
            {
                bReturn = 0;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual bool DeleteShortcut(string strGuid, int category, string strUserID, string strSite, ref DataSet reDataSet)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                int state = 0;
                string strSQL1 = "";
                string strSQL = string.Format("select owner from tShortcut where shortcutguid='{0}'", strGuid);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj == null || Convert.ToString(obj).Trim().Length == 0)
                {

                    strSQL1 = string.Format(@"Delete from tShortcut where ShortcutGuid = '{0}'", strGuid.Trim());
                }
                else
                {
                    string strTemp = Convert.ToString(obj);
                    string[] arrOwner = strTemp.Split(",".ToCharArray());
                    if (arrOwner.Length <= 1)
                    {
                        strSQL1 = string.Format(@"Delete from tShortcut where ShortcutGuid = '{0}'", strGuid.Trim());
                    }
                    else
                    {
                        strTemp = "";
                        foreach (string str in arrOwner)
                        {
                            if (str.ToUpper() != strSite.ToUpper())
                            {
                                strTemp += str;
                                strTemp += ",";
                            }
                        }
                        strTemp = strTemp.TrimEnd(',');
                        strSQL1 = string.Format("update tShortcut set Owner='{0}' where ShortcutGuid = '{1}'", strTemp, strGuid.Trim());
                    }

                }



                string strSQl2 = string.Format(@"SELECT * from tShortcut where Category={0} "
                    + " AND (Type=0 OR (Type=1 AND Owner='{1}') OR (Type=2 AND ','+Owner+',' like '%,{2},%')) order by Name",
                    category, strUserID.Trim(), CommonGlobalSettings.Utilities.GetCurSite());
                oKodak.BeginTransaction();
                state = oKodak.ExecuteNonQuery(strSQL1, RisDAL.ConnectionState.KeepOpen);
                oKodak.CommitTransaction();
                //EK_HI00118427
                //if (state == 1 || state == 2)
                //{
                dt = oKodak.ExecuteQuery(strSQl2);
                dt.TableName = "tShortcut";
                reDataSet.Tables.Add(dt);
                //}

                //else
                //    bReturn = false;

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual bool GetStaff(string strDegreeName, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                strDegreeName.Trim();

                string strSQL;
                if (strDegreeName.Length == 0)
                {
                    //throw new Exception("DegreeName must not be null");
                    strSQL = string.Format("Select tUser.LocalName,tUser.loginname,tUser.UserGuid from tUser,tUser2Domain where tUser.UserGuid=tUser2Domain.UserGuid and tUser.DeleteMark=0 and tUser2Domain.Domain='{0}' order by tUser.LocalName", CommonGlobalSettings.Utilities.GetCurDomain());
                }
                else
                {
                    string strDegreeNameValue = GetSystemProfileValue(strDegreeName, "0000");
                    strSQL = string.Format("Select DISTINCT  tUser.LocalName,tUser.loginname,tUser.UserGuid from tUser,tRole2User,tUser2Domain where (tUser.DeleteMark=0) and tUser.UserGuid=tUser2Domain.UserGuid and (tUser.UserGuid=tRole2User.UserGuid) and CharIndex(tRole2User.RoleName,'{0}')>0 and tUser2Domain.Domain='{1}' order by tUser.LocalName", strDegreeNameValue, CommonGlobalSettings.Utilities.GetCurDomain());
                    //strSQL = string.Format("Select DISTINCT  tUser.LocalName,tUser.loginname,tUser.UserGuid from tUser,tRole2User,tUser2Domain where (tUser.DeleteMark=0) and tUser.UserGuid=tUser2Domain.UserGuid and (tUser.UserGuid=tRole2User.UserGuid) and CharIndex(tRole2User.RoleName,(Select Value From tSystemProfile where Name='{0}' and ModuleID ='0000'))>0 and tUser2Domain.Domain='{1}'", strDegreeName, CommonGlobalSettings.Utilities.GetCurDomain());
                }


                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "Staff";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual string GetLocalName(string strLoginName)
        {
            string result = null;
            RisDAL oKodak = new RisDAL();
            string sql = string.Format(@"select LocalName from tUser where LoginName = '{0}'", strLoginName.Trim());
            try
            {
                result = oKodak.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return result;
        }

        public virtual string GetLocalNameByUserGuid(string userGuid)
        {
            string result = null;
            RisDAL oKodak = new RisDAL();
            string sql = string.Format(@"select LocalName from tUser where UserGuid = '{0}'", userGuid.Trim());
            try
            {
                result = oKodak.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return null;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return result;
        }

        public virtual bool GetProcedureDefaultValue(string strModalityType, string strBodyCategory, string strBodyPart, string strCheckingItem, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string strSQL = string.Format("SELECT ProcedureCode,Description,Frequency,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,Charge FROM tProcedureCode WHERE ModalityType='{0}' and BodyCategory='{1}' and BodyPart='{2}' and CheckingItem='{3}'", strModalityType, strBodyCategory, strBodyPart, strCheckingItem);
                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "tDefaultValue";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetProcedureCode(string strProcedureCode, string strShortcutCode, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                StringBuilder strBuilder = new StringBuilder();
                if (strProcedureCode.Length > 0)
                {
                    strBuilder.AppendFormat("SELECT ProcedureCode,Description,ModalityType,BodyPart,CheckingItem,Charge,Preparation,Frequency,")
                            .AppendFormat("BodyCategory,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,ShortcutCode,ApproveWarningTime,Enhance,Effective ")
                            .AppendFormat(" FROM tProcedureCode WHERE ProcedureCode='{0}'", strProcedureCode);
                }
                else
                {
                    strBuilder.AppendFormat("SELECT ProcedureCode,Description,ModalityType,BodyPart,CheckingItem,Charge,Preparation,Frequency,")
                            .AppendFormat("BodyCategory,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,ShortcutCode,ApproveWarningTime,Enhance,Effective ")
                            .AppendFormat(" FROM tProcedureCode WHERE ShortcutCode='{0}'", strShortcutCode);

                }

                oKodak.ExecuteQuery(strBuilder.ToString(), dt);
                dt.TableName = "ProcedureCode";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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


        public virtual bool ReclaimID(int nTag, string strValue)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {

                string strSQL = string.Format("select * from tIDRecycleBin where Tag={0} and Value='{1}'", nTag, strValue);
                oKodak.ExecuteQuery(strSQL, dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    strSQL = string.Format("INSERT INTO tIDRecycleBin(Tag,Value) Values({0},'{1}')", nTag, strValue);
                    oKodak.ExecuteNonQuery(strSQL);
                }



            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetExtOptional(string strTableName, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {

                string strSQL = string.Format("select TableName,Caption,Delegate,ControlType,Tag,Owner from tExtOptional where TableName='{0}' order by orderindex asc", strTableName);
                oKodak.ExecuteQuery(strSQL, dt);
                if (dt == null)
                {
                    throw new Exception("DB Access error");
                }
                dt.TableName = "ExtOptional";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        public virtual bool GetRequisition(string strAccNo, string strDataCenter, string strDomain, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = null;
            DataTable dt = new DataTable();

            try
            {
                if (strDataCenter == "1")
                {
                    oKodak = new RisDAL(2);
                }
                else
                {
                    oKodak = new RisDAL();
                }
                string strSQL = string.Format(@"SELECT RequisitionGuid,AccNo,RelativePath,FileName,ScanDt,BackupMark,Domain FROM tRequisition WHERE AccNo='{0}' and Domain='{1}'", strAccNo, strDomain);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Requisition";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
        public virtual bool GetRequisition(bool loadArchive, string strAccNo, string strDataCenter, string strDomain, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = null;
            DataTable dt = new DataTable();

            try
            {
                if (strDataCenter == "1")
                {
                    oKodak = new RisDAL(2);
                }
                else
                {
                    oKodak = new RisDAL();
                }
                string strSQLLocal = string.Format(@"SELECT RequisitionGuid,AccNo,RelativePath,FileName,ScanDt,BackupMark,Domain FROM tRequisition WHERE AccNo='{0}' and Domain='{1}'", strAccNo, strDomain);
                string strSQLArchive = string.Format(@"SELECT RequisitionGuid,AccNo,RelativePath,FileName,ScanDt,BackupMark,Domain FROM RISArchive.dbo.tRequisition WHERE AccNo='{0}' and Domain='{1}'", strAccNo, strDomain);
                dt = oKodak.ExecuteQuery(strSQLLocal);
                if (loadArchive && dt.Rows.Count == 0)
                {
                    dt = oKodak.ExecuteQuery(strSQLArchive);
                }
                dt.TableName = "Requisition";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
        public virtual FtpModel GetFtpParameters()
        {
            RisDAL dataAccess = new RisDAL();
            FtpModel model = new FtpModel();
            try
            {
                string strSQL = string.Format("select * from tDomainlist where domain='{0}' ", CommonGlobalSettings.Utilities.GetCurDomain());
                DataTable dt = dataAccess.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {

                    model.FtpServer = Convert.ToString(dt.Rows[0]["FtpServer"]);
                    model.FtpPort = Convert.ToString(dt.Rows[0]["FtpPort"]);
                    model.FtpUserID = Convert.ToString(dt.Rows[0]["FtpUser"]);
                    model.FtpPassword = Convert.ToString(dt.Rows[0]["FtpPassword"]);


                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return model;
        }
        /// <summary>
        /// Read table tRequisition and get list of all files that has not been downloaded
        /// </summary>
        /// <param name="stTables">
        /// List of tables that need to be searched, separates tables with "|".
        /// If it is null string, all tables should be downloaded(only this function is supported now)
        /// </param>
        /// <param name="bFailRetry">
        /// True means retry failed data
        /// false means download new data
        /// </param>
        /// <param name="stRetryFlag">
        /// List of flags, files that failed to be downloaded with the flag can be downloaded again.
        /// _+ Current version only support download all failed files
        /// </param>
        /// <returns></returns>
        public virtual DataTable GetBackupFileList(string stTables, bool bFailRetry, string stRetryFlag)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                if (bFailRetry == false)
                //get files that have not been downloaded
                {
                    //for requisition
                    string sql = string.Format("select 'trequisition' as tablename, RequisitionGUID as FileGUID, backupmark,backupcomment,filename, relativepath from trequisition where ((backupmark != '1' and backupmark != 'F' )  or backupmark is null )")
                        //for report files
                        + string.Format("union ")
                        + string.Format("select 'treportfile' as tablename, FileGUID,backupmark,backupcomment,filename, relativepath from treportfile where( (backupmark != '1' and backupmark != 'F'  ) or backupmark is null) ")
                        //for report printlog files
                        + string.Format("union ")
                        + string.Format("select 'treportprintlog' as tablename,FileGUID, backupmark,backupcomment,SnapShotSrvPath as filename, '' as relativepath from treportprintlog where( (backupmark != '1' and backupmark != 'F' ) or backupmark is null) ");
                    logger.Info((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, sql, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    return dataAccess.ExecuteQuery(sql);
                }
                else
                //get files that fail to download sometime before
                {
                    //for requisition
                    string sql = string.Format("select 'trequisition' as tablename, RequisitionGUID as FileGUID, backupmark,backupcomment,filename, relativepath from trequisition where (backupmark != '1' and len(backupmark)>0)")
                        //for report files
                        + string.Format("union ")
                        + string.Format("select 'treportfile' as tablename, FileGUID,backupmark,backupcomment,filename, relativepath from treportfile where (backupmark != '1' and len(backupmark)>0)")
                        //for report printlog files
                        + string.Format("union ")
                        + string.Format("select 'treportprintlog' as tablename,FileGUID, backupmark,backupcomment,SnapShotSrvPath as filename, '' as relativepath from treportprintlog where (backupmark != '1' and len(backupmark)>0)");
                    logger.Info((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, sql, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    return dataAccess.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        /// <summary>
        /// Used in Backup module for updating the Backupmark and BackupComment 
        /// </summary>
        /// <param name="bSuccess">
        /// True means backup success, or failed.
        /// </param>
        /// <param name="stUnicGUID">
        /// The unique ID of this record
        /// RequisitionGUID for tRequisitioin
        /// FileGUID for tReportfile and tReportprintlog
        /// </param>
        /// <param name="stComment">
        /// Comment for backup action with XML format. 
        /// Fail reason or backup path if success
        /// </param>
        /// <param name="stTableName"></param>
        /// Name of the table that this record comes from
        /// <param name="stRequGUID"> the GUID of requisition record that has been backuped</param>
        public virtual void UpdateDatabaseMark(bool bSuccess, string stGUID, string stComment, string stTableName)
        {

            RisDAL oKodak = new RisDAL();
            try
            {

                char[] sep = { '|' };
                string[] stRequItems = stGUID.Split(sep);
                List<string> listSQL = new List<string>();
                string stBackupMark = "1";
                if (bSuccess)
                //success
                {
                    stBackupMark = "1";
                }
                else
                {
                    stBackupMark = "F";
                }

                //get GUID col of the table
                string stGUIDColName;
                stTableName.ToLower();
                if (stTableName.CompareTo("trequisition") == 0)
                {
                    stGUIDColName = "RequisitionGUID";
                }
                else
                {
                    stGUIDColName = "FileGUID";
                }

                if (stComment != null)
                {
                    if (stComment.Contains("'"))
                    {
                        stComment = stComment.Replace("'", "''");
                    }
                }

                //
                foreach (string stCurGUID in stRequItems)
                {
                    string strSQL = string.Format("Update {0} set BackupMark = '{1}',BackupComment = '{2}' WHERE {3} ='{4}'",
                                            stTableName, stBackupMark, stComment, stGUIDColName, stCurGUID);
                    listSQL.Add(strSQL);
                }

                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, strSQL, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }
        public virtual bool CheckClinic(string roleName)
        {
            try
            {
                //string strSqlForCheckClinicRole = string.Format("select  Value from tSystemProfile where Name = 'Clinician'");
                //KodakDAL oKodakDAL = new KodakDAL();
                //string strClinicRole = Convert.ToString(oKodakDAL.ExecuteScalar(strSqlForCheckClinicRole));
                string strClinicRole = GetSystemProfileValue("Clinician", "0000");
                if (strClinicRole != null && strClinicRole != "")
                {
                    if (strClinicRole.Contains("|"))
                    {
                        string[] roleArry = strClinicRole.Split('|');
                        foreach (string tempRole in roleArry)
                        {
                            if (tempRole == roleName)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (roleName == strClinicRole)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return true;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
        }
        public virtual bool GetAllProcedureCode(DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {


                //string strSQL = "SELECT tProcedureCode.ProcedureCode, tProcedureCode.Description, tProcedureCode.EnglishDescription, tProcedureCode.ModalityType, "
                //      + "tProcedureCode.BodyPart, tProcedureCode.CheckingItem, tProcedureCode.Charge, tProcedureCode.Preparation, tProcedureCode.Frequency, "
                //      + "tProcedureCode.BodyCategory, tProcedureCode.Duration, tProcedureCode.FilmSpec, tProcedureCode.FilmCount, tProcedureCode.ContrastName, "
                //      + "tProcedureCode.ContrastDose, tProcedureCode.ImageCount, tProcedureCode.ExposalCount, tBookingNoticeTemplate.BookingNotice, "
                //      + "tProcedureCode.ShortcutCode, tProcedureCode.ApproveWarningTime,tProcedureCode.Effective,tProcedureCode.Enhance,tProcedureCode.Externals,tProcedureCode.BodypartFrequency,tProcedureCode.CheckingItemFrequency  "
                //      + "FROM tProcedureCode LEFT OUTER JOIN "
                //      + "tBookingNoticeTemplate ON tProcedureCode.BookingNotice = tBookingNoticeTemplate.Guid";

                string strSQL = "SELECT * from tProcedureCode ";

                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "ProcedureCode";
                ds.Tables.Add(dt);


            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public virtual bool GetDateTypeByCalendar(string bookingDate, string modality, ref string dateType, ref string availableDate)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();


            try
            {

                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("@Modality", modality);
                oKodak.Parameters.AddVarChar("@BookingDate", bookingDate);
                oKodak.Parameters.AddVarChar("@DateType", dateType, 256, ParameterDirection.Output);
                oKodak.Parameters.AddVarChar("@AvailableDate", availableDate, 256, ParameterDirection.Output);

                oKodak.ExecuteQuerySP("usp_GetDateTypeAvailableDate");

                dateType = Convert.ToString(oKodak.Parameters["@DateType"].Value);
                availableDate = Convert.ToString(oKodak.Parameters["@AvailableDate"].Value);
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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


        public virtual bool GetCurSiteProcedureCode(DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {

                string strSQL = string.Format("SELECT * from tProcedureCode where site='{0}'",CommonGlobalSettings.Utilities.GetCurSite());

                dt = oKodak.ExecuteQuery(strSQL);
                if (dt.Rows.Count == 0)
                {
                    //Get procedurecode from domain
                    strSQL = string.Format("SELECT * from tProcedureCode where site=''");

                    dt = oKodak.ExecuteQuery(strSQL);

                }

                dt.TableName = "ProcedureCode";

                ds.Tables.Add(dt);


            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        #region Modified by Blue Chen for US19895, 10/31/2014
        public virtual bool GetCurSiteExamSystem(string strModalityType, string strBodyPart, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                if (strModalityType.Length == 0 && strBodyPart.Length == 0)
                {
                    strSQL = string.Format("SELECT * from tBodySystemMap where Site = '{0}'", CommonGlobalSettings.Utilities.GetCurSite());
                }
                else
                {
                    strSQL = string.Format("SELECT distinct ExamSystem from tBodySystemMap where ModalityType='{0}' and Bodypart='{1}' and Site = '{2}'", strModalityType, strBodyPart, CommonGlobalSettings.Utilities.GetCurSite());
                }
                oKodak.ExecuteQuery(strSQL, dt);
                if (dt.Rows.Count == 0)
                {
                    if (strModalityType.Length == 0 && strBodyPart.Length == 0)
                    {
                        strSQL = "SELECT * from tBodySystemMap where Site = '' or Site is null";
                    }
                    else
                    {
                        strSQL = string.Format("SELECT distinct ExamSystem from tBodySystemMap where ModalityType='{0}' and Bodypart='{1}' and (Site = '' or Site is null)", strModalityType, strBodyPart);
                    }
                    oKodak.ExecuteQuery(strSQL, dt);
                }

                dt.TableName = "ExamSystem";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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
        #endregion

        public virtual bool GetProcedureCodeBySite(string strSite, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {

                string strSQL = string.Format("SELECT * from tProcedureCode where site='{0}'", strSite);

                dt = oKodak.ExecuteQuery(strSQL);               

                dt.TableName = "ProcedureCode";

                ds.Tables.Add(dt);


            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public bool UpdateCertificate(string strUserGuid, string strIkeySn, string strPrivateKey, string strPublicKey, string strOrgOwner, DataSet ds)
        {

            bool bReturn = true;
            RisDAL oKodak = new RisDAL();


            try
            {
                string strSQL = "";
                if (strOrgOwner.Trim().Length > 0)
                {
                    strSQL = string.Format("SELECT count(*) FROM tUser where IkeySn='{0}' and UserGuid='{1}'", strIkeySn, strOrgOwner);
                    object obj = oKodak.ExecuteScalar(strSQL);
                    if (obj != null && Convert.ToInt32(obj) > 0)
                    {
                        strSQL = string.Format("UPDATE tUser SET IkeySn='',PrivateKey='',PublicKey='' WHERE UserGuid='{0}'",
                                strOrgOwner);

                        oKodak.ExecuteNonQuery(strSQL);
                    }

                }

                strSQL = string.Format("UPDATE tUser SET IkeySn='{0}',PrivateKey='{1}',PublicKey='{2}' WHERE UserGuid='{3}'",
                     strIkeySn, strPrivateKey, strPublicKey, strUserGuid);

                oKodak.ExecuteNonQuery(strSQL);




            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
        public bool GetCertificate(string strUserGuid, string strIkeySn, DataSet ds)
        {

            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string strSQL = "SELECT	UserGuid,PrivateKey,PublicKey,IkeySn,LoginName,LocalName,EnglishName FROM tUser Where 1=1 ";
                if (strUserGuid.Length > 0)
                {
                    strSQL += string.Format(" AND UserGuid='{0}' ", strUserGuid);
                }
                if (strIkeySn.Length > 0)
                {
                    strSQL += string.Format(" AND IkeySn='{0}' ", strIkeySn);
                }


                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Certificate";
                ds.Tables.Add(dt);


            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public bool SetCertificatePassword(string strCertificatePassword)
        {
            bool bReturn = true;
            //KodakDAL oKodak = new KodakDAL();
            //DataTable dt = new DataTable();

            try
            {
                //string strSQL = string.Format("Update tSystemProfile set Value='{0}' where Name='CertificatePassword' and ModuleID='0000'", strCertificatePassword);
                //oKodak.ExecuteNonQuery(strSQL);
                UpdateSystemProfile("CertificatePassword", "0000", "strCertificatePassword");

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bReturn;
        }
        public bool GetCertificatePassword(ref string strCertificatePassword)
        {
            bool bReturn = true;

            try
            {
                //string strSQL = "SELECT	Value FROM      tSystemProfile Where Name='CertificatePassword' and ModuleID='0000'";

                string strValue = GetSystemProfileValue("CertificatePassword", "0000");
                //object obj = oKodak.ExecuteScalar(strSQL);
                if (strValue != string.Empty)
                {
                    strCertificatePassword = strValue;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bReturn;

        }
        public bool GetProfileInfo(string strRoleName, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                dt.Columns.Add("CertificatePassword", typeof(string));
                dt.Columns.Add("VerifyCertificatePoint", typeof(string));
                dt.Columns.Add("VerifyDigitalCertificate", typeof(string));
                DataRow dr = dt.Rows.Add();

                //string strSQL = "SELECT	Value FROM tSystemProfile Where Name='CertificatePassword' and ModuleID='0000'";
                string strValue = GetSystemProfileValue("CertificatePassword", "0000");
                //object obj = oKodak.ExecuteScalar(strSQL);
                if (strValue != string.Empty)
                {
                    dr["CertificatePassword"] = strValue;
                }


                //strSQL = "SELECT Value FROM tSystemProfile Where Name='VerifyCertificatePoint' and ModuleID='0000'";

                //obj = oKodak.ExecuteScalar(strSQL);
                strValue = GetSystemProfileValue("VerifyCertificatePoint", "0000");
                if (strValue != string.Empty)
                {
                    dr["VerifyCertificatePoint"] = strValue;
                }

                //strSQL = string.Format("SELECT Value FROM tRoleProfile Where Name='VerifyDigitalCertificate' and ModuleID='0000' and RoleName='{0}'", strRoleName);

                //obj = oKodak.ExecuteScalar(strSQL);
                strValue = GetProfileValue("VerifyDigitalCertificate", strRoleName, "0000");
                if (strValue != string.Empty)
                {
                    dr["VerifyDigitalCertificate"] = strValue;
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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



        public virtual DataSet GetConditionColumn(Dictionary<string, object> paramMap)
        {
            try
            {
                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetConditionColumn!"));
                }

                string conditionName = "", userGuid = "";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "CONDITIONNAME")
                    {
                        conditionName = paramMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = paramMap[key] as string;
                    }
                }

                //string sql = "select *,"
                //    + " (select syscolumns.xtype from sysobjects, syscolumns"
                //    + "     where syscolumns.id=sysobjects.id and sysobjects.xtype='U'"
                //    + "     and sysobjects.name=tablename and syscolumns.name=columnname) as fieldType"
                //    + " from tConditionColumn where conditionName = '" + conditionName + "' and (IsHidden = 0 or IsHiddenQuick = 0) order by orderID";
                // 2015-10-26, Oscar changed (US28040)
                var sql = ";WITH Part1 AS (SELECT o.name AS TableName, c.name AS ColumnName, c.xtype AS FieldType FROM sysobjects o JOIN syscolumns c ON c.id = o.id WHERE o.xtype = 'U')" +
                    "SELECT t.*, p.FieldType FROM dbo.tConditionColumn t LEFT JOIN Part1 p ON p.TableName = t.TableName AND p.ColumnName = t.ColumnName WHERE t.ConditionName = '" + conditionName + "' AND (IsHidden = 0 OR IsHiddenQuick = 0) ORDER BY OrderID;";

                DataSet ds = new DataSet();

                //KodakDAL dal = new KodakDAL();


                DataTable dt = new DataTable("ConditionColumn");
                using (var dal = new RisDAL())
                {
                    dal.ExecuteQuery(sql, dt);
                }

                ds.Tables.Add(dt);

                //KodakDAL okodak = null;
                //if (conditionName.StartsWith("DataCenter"))
                //{
                //    okodak = new KodakDAL(2);
                //}
                //else
                //{
                //    okodak = new KodakDAL();
                //}

                // 2015-10-26, Oscar added (US28040)
                var cache = new Dictionary<string, DataTable>(StringComparer.OrdinalIgnoreCase);
                string doctorRoleName = null;

                foreach (DataRow dr in dt.Rows)
                {
                    int itemID = System.Convert.ToInt32(dr["itemID"]);
                    int dataType = dr["DataType"] is DBNull ? 0 : System.Convert.ToInt32(dr["DataType"]);
                    string columnName = dr["ColumnName"] == DBNull.Value ? string.Empty : dr["ColumnName"].ToString();
                    string tableName = dr["TableName"] == DBNull.Value ? string.Empty : dr["TableName"].ToString();
                    string datasrc = dr["datasource"] as string;

                    if (datasrc == null || (datasrc = datasrc.Trim()).Length < 1 || ds.Tables.Contains(itemID.ToString()))
                        continue;


                    if (dataType == 3 || dataType == 2)
                    {
                        if (columnName.ToUpper().Equals("UNWRITTENCURRENTOWNER") || columnName.ToUpper().Equals("UNAPPROVEDCURRENTOWNER")
                            || columnName.ToUpper().Equals("CREATER") || columnName.ToUpper().Equals("SUBMITTER") || columnName.ToUpper().Equals("FIRSTAPPROVER")
                            || columnName.ToUpper().Equals("REJECTER") || columnName.ToUpper().Equals("REJECTTOOBJECT"))
                        {
                            //string doctorRoleName = GetProfileValue("Radiologist", "", "0000");
                            // 2015-10-26, Oscar changed (US28040)
                            if (string.IsNullOrWhiteSpace(doctorRoleName))
                                doctorRoleName = GetProfileValue("Radiologist", "", "0000");

                            sql = string.Format("begin declare @radiologistRoleSql nvarchar(1024); select @radiologistRoleSql = 'RoleName = ''' + replace('{0}', '|' ,''' or RoleName = ''') + '''' ;declare @userSql nvarchar(2048); set @userSql = 'select distinct tuser.userGuid as value, tuser.LocalName as text from tUser inner join tUser2domain on tuser.userguid=tuser2domain.userguid and ((datediff(day,tuser2domain.EndDate,GETDATE()) <=0 and tuser2domain.IsSetExpireDate=1) or tuser2domain.IsSetExpireDate=0)  and tuser.DeleteMark=0 inner join tRole2User on tUser.UserGuid = tRole2User.UserGuid and (' + @radiologistRoleSql + ') order by text' ;exec (@userSql); end", doctorRoleName);
                        }
                        else if ((tableName.ToUpper().Equals("TREPORT") && columnName.ToUpper().Equals("REPORTQUALITY"))
                            || (tableName.ToUpper().Equals("TQUALITYSCORING") && columnName.ToUpper().Equals("RESULT")))
                        {
                            sql = datasrc;
                            sql = string.Format(sql, CommonGlobalSettings.Utilities.GetCurSite());
                        }
                        else
                        {
                            sql = datasrc;
                        }

                        DataTable dt0 = new DataTable(itemID.ToString());
                        //using (var okodak = conditionName.StartsWith("DataCenter") ? new KodakDAL(2) : new KodakDAL())
                        //{
                        //    okodak.ExecuteQuery(sql, dt0);
                        //}
                        // 2015-10-26, Oscar changed (US28040)
                        CheckCache(cache, sql, ref dt0, () =>
                        {
                            using (var okodak = conditionName.StartsWith("DataCenter") ? new RisDAL(2) : new RisDAL())
                            {
                                okodak.ExecuteQuery(sql, dt0);
                            }
                        });

                        ds.Tables.Add(dt0);
                    }
                }

                foreach (var kv in cache)
                {
                    kv.Value.Dispose();
                }
                cache.Clear();
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetConditionColumn=" + ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        // 2015-10-26, Oscar added (US28040)
        void CheckCache(Dictionary<string, DataTable> cache, string sql, ref DataTable target, Action executeQuery)
        {
            DataTable result;
            if (cache.TryGetValue(sql, out result))
            {
                var name = target.TableName;
                target = result.Copy();
                target.TableName = name;
            }
            else
            {
                executeQuery();
                cache.Add(sql, target);
            }
        }

        public virtual DataSet GetGridColumn(Dictionary<string, object> paramMap)
        {
            try
            {
                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter!"));
                }

                string gridName = "", userGuid = "";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "GRIDNAME")
                    {
                        gridName = paramMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = paramMap[key] as string;
                    }
                }

                gridName = gridName == null ? "" : gridName.Trim();
                userGuid = userGuid == null ? "" : userGuid.Trim();

                if (gridName.Length > 0)
                {
                    string sql = string.Format("select tGridColumn.Guid,tGridColumn.ColumnWidth,tGridColumn.IsHidden,tGridColumnOption.ColumnID," +
                            " tGridColumnOption.TableName,tGridColumnOption.ColumnName,tGridColumnOption.Expression," +
                            " tGridColumnOption.ModuleID, tGridColumnOption.isImageColumn, tGridColumnOption.ImagePath" +
                            " from tGridColumn left join tGridColumnOption on " +
                            " tGridColumn.Guid=tGridColumnOption.Guid where " +
                            " tGridColumn.UserGuid='' AND tGridColumnOption.ListName='{0}' " +
                            " order by tGridColumn.OrderID ", gridName);

                    DataSet ds = new DataSet();

                    //KodakDAL dal = new KodakDAL();
                    using (var dal = new RisDAL())
                    {
                        dal.ExecuteQuery(sql, ds, "GridColumn");
                    }
                    return ds;
                }
                else
                {
                    throw (new Exception("Missing Parameter!"));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetGridColumn=" + ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        public virtual DataSet GetUserList()
        {
            string sql = "";

            try
            {
                //KodakDAL dal = new KodakDAL();

                //sql = "select tRole2User.rolename, tUser.userGuid, LoginName, localName, englishname,"
                //     + " department, title, address, telephone, comments, domainloginname, isnull(tRoleProfile.value, 0) as [level]"
                //     + " from tUser left join tRole2User"
                //     + "		 left join tRoleProfile on tRoleProfile.rolename=tRole2User.rolename and tRoleProfile.name='RoleLevel'"
                //     + "   on tUser.UserGuid = tRole2User.UserGuid"
                //     + " order by tRole2User.rolename, loginName";
                sql = @"select tRole2User.rolename, tUser.userGuid, LoginName, localName, englishname, 
                        tUser2Domain.department, title, address, tUser2Domain.telephone, comments, tUser2Domain.domainloginname, isnull(tRoleProfile.value, 0) as [level] 
                        from tUser left join tRole2User	on tUser.UserGuid = tRole2User.UserGuid left join tRoleProfile on tRoleProfile.rolename=tRole2User.rolename and tRoleProfile.name='RoleLevel'
                        left join tUser2Domain on tUser2Domain.UserGuid = tUser.UserGuid  order by tRole2User.rolename, LoginName";
                DataSet ds = new DataSet();
                using (var dal = new RisDAL())
                {
                    dal.ExecuteQuery(sql, ds, "UserList");
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetUserList, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        public virtual DataSet GetIMUserList(string type)
        {
            string sql = "";

            try
            {
                //KodakDAL dal = new KodakDAL();
                if (type == "0")
                {
                    sql = @"
                            select a.*,b.Value as BelongToSite from (select distinct a.UserGuid,a.LocalName,c.RoleName,c.Description RoleDesc from tUser a,tRole2User b,tRole c,tUser2Domain d,tUserProfile e where a.UserGuid = b.UserGuid and b.RoleName = c.RoleName and a.UserGuid = d.UserGuid and a.UserGuid = d.UserGuid and a.UserGuid = e.UserGuid and  ((d.IsSetExpireDate = 1  and DATEDIFF(DD,d.StartDate,GETDATE()) >=0 and DATEDIFF(DD,GETDATE(),d.EndDate) >=0 ) or d.IsSetExpireDate = 0)  and (e.Name = 'CanUseIM' and Value = 1) )
                            as a ,tUserProfile b where a.UserGuid = b.UserGuid and b.Name ='BelongToSite' and b.Domain ='{0}'";
                }
                else if (type == "1")
                {
                    sql = @" select a.*,b.Value as BelongToSite from (select distinct a.UserGuid,a.LocalName,c.RoleName,c.Description RoleDesc,d.BeginDateTime,d.EndDateTime,d.Site from tUser a left join tRole2User b on a.UserGuid = b.UserGuid left join tUserProfile e on b.UserGuid = e.UserGuid left join tRole c on b.RoleName = c.RoleName left join tReportDoctor d on d.DOCTOR_GUID = a.UserGuid left join tUser2Domain f on f.UserGuid = a.UserGuid where len(c.RoleName) >0 and (datediff(day, getdate(),EndDateTime) =1 or datediff(day, getdate(),EndDateTime) = 0) and ((f.IsSetExpireDate = 1 and DATEDIFF(DD,f.StartDate,GETDATE()) >=0 and DATEDIFF(DD,GETDATE(),f.EndDate) >=0 ) or f.IsSetExpireDate = 0) and(e.Name = 'CanUseIM' and Value = 1)) as a
                             ,tUserProfile b where a.UserGuid = b.UserGuid and b.Name ='BelongToSite' and b.Domain ='{0}'";
                }
                else
                {
                    sql = @"select a.*,b.Value as BelongToSite from
                            (select distinct a.UserGuid,a.LocalName,c.RoleName,c.Description RoleDesc from 
                            tUser a,tRole2User b,tRole c,tUser2Domain d,tUserProfile e 
                            where a.UserGuid = b.UserGuid and b.RoleName = c.RoleName and a.UserGuid = d.UserGuid 
                            and a.UserGuid = d.UserGuid and a.UserGuid = e.UserGuid and ((d.IsSetExpireDate = 1 and 
                            DATEDIFF(DD,d.StartDate,GETDATE()) >=0 and DATEDIFF(DD,GETDATE(),d.EndDate) >=0 ) or d.IsSetExpireDate = 0) 
                             and (e.Name = 'CanUseIM' and Value = 1) ) as a ,tUserProfile as b where a.UserGuid = b.UserGuid and b.Name ='BelongToSite' and b.Domain ='{0}'";
                }
                DataSet ds = new DataSet();
                sql = string.Format(sql, CommonGlobalSettings.Utilities.GetCurDomain());
                using (var dal = new RisDAL())
                {
                    dal.ExecuteQuery(sql, ds, "IMUserList");
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetIMUserList, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        public virtual DataSet GetValidUserList()
        {
            string sql = "";
            try
            {
                //KodakDAL dal = new KodakDAL();
                sql = @"select distinct a.UserGuid,a.LocalName,c.RoleName,c.Description RoleDesc,f.Department from tUser a left join tRole2User b on a.UserGuid = b.UserGuid left join tUserProfile e on b.UserGuid = e.UserGuid left join tRole c on b.RoleName = c.RoleName left join tUser2Domain f on f.UserGuid = a.UserGuid where len(c.RoleName) >0 and ((f.IsSetExpireDate = 1 and DATEDIFF(DD,f.StartDate,GETDATE()) >=0 and DATEDIFF(DD,GETDATE(),f.EndDate) >=0 ) or f.IsSetExpireDate = 0) and a.DeleteMark = 0 and a.Domain ='{0}'";
                DataSet ds = new DataSet();
                sql = string.Format(sql, CommonGlobalSettings.Utilities.GetCurDomain());
                using (var dal = new RisDAL())
                {
                    dal.ExecuteQuery(sql, ds, "ValidUserList");
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetIMUserList, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        public virtual DataSet GetValidApproverDoctor()
        {
            string sql = "";
            try
            {
                //KodakDAL dal = new KodakDAL();
                DataSet ds = new DataSet();
                string doctorRoleName = GetProfileValue("Radiologist", "", "0000");
                sql = string.Format("begin declare @radiologistRoleSql nvarchar(max); select @radiologistRoleSql = 'RoleName = ''' + replace('{0}', '|' ,''' or RoleName = ''') + '''' ;declare @userSql nvarchar(max); set @userSql = 'select distinct tuser.userGuid as value, tuser.LocalName as text from tUser inner join tUser2domain on tuser.userguid=tuser2domain.userguid and ((datediff(day,tuser2domain.EndDate,GETDATE()) <=0 and datediff(day,tuser2domain.StartDate,GETDATE()) >=0 and tuser2domain.IsSetExpireDate=1) or tuser2domain.IsSetExpireDate=0)  and tuser.DeleteMark=0 inner join tRole2User on tUser.UserGuid = tRole2User.UserGuid and (' + @radiologistRoleSql + ') inner join tRoleProfile on tRoleProfile.RoleName = dbo.tRole2User.RoleName and tRoleProfile.Name = ' +'''canApprove''' +' and tRoleProfile.value=1 order by text' ;exec (@userSql); end", doctorRoleName);
                using (var dal = new RisDAL())
                {
                    dal.ExecuteQuery(sql, ds, "ValidApproverDoctor");
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetIMUserList, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }


        public virtual DataSet GetIMLog(string queryCondition)
        {
            string sql = "";

            try
            {
                //KodakDAL dal = new KodakDAL();

                sql = @"select * from RISHippa..tIMLog ";
                if (queryCondition.Length > 0)
                {
                    sql += " where " + queryCondition;
                }
                sql += " order by EventDt Desc";
                DataSet ds = new DataSet();
                using (var dal = new RisDAL())
                {
                    dal.ExecuteQuery(sql, ds, "tIMLog");
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetIMLog, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        public virtual DataSet GetConditionRelatedControlData(Dictionary<string, object> paramMap)
        {
            string sql = string.Empty;

            try
            {
                DataSet ds = new DataSet();
                //KodakDAL dal = new KodakDAL();
                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter!"));
                }
                using (var dal = new RisDAL())
                {
                    foreach (string key in paramMap.Keys)
                    {
                        string value = "";
                        value = paramMap[key].ToString().ToUpper();

                        if (value == "MODALITYTYPE-MODALITY")
                        {
                            sql = "select distinct modality as text,modalityType, modality as value  from tModality";
                            DataTable dt = new DataTable("ModalityType_Modality");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                        else if (value == "MODALITYTYPE-EXAMSYSTEM")
                        {
                            sql = "select distinct ExamSystem as text, modalityType,ExamSystem as value  from tBodySystemMap";
                            DataTable dt = new DataTable("ModalityType_EXAMSYSTEM");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                        else if (value == "MODALITYTYPE-BODYPART")
                        {
                            sql = "select distinct bodypart as text, modalityType,bodypart as value from tProcedureCode";
                            DataTable dt = new DataTable("ModalityType_BodyPart");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                        else if (value == "MODALITYTYPE-CHECKINGITEM")
                        {
                            //EK_HI00064564 Foman 2007.12.26   "select distinct modalityType,CheckingItem as text,  CheckingItem as value  from tProcedureCode""
                            sql = "select distinct CheckingItem as text, modalityType, CheckingItem as value  from tProcedureCode";
                            DataTable dt = new DataTable("ModalityType_CheckingItem");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetConditionRelatedControlData, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        public virtual DataSet GetDoctorSupervisor()
        {
            string sql = string.Empty;
            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    //sql = "select Value from tSystemProfile where Name = 'Radiologist'";
                    string doctorRoleName = GetProfileValue("Radiologist", "", "0000");//dal.ExecuteScalar(sql).ToString();
                    if (!string.IsNullOrWhiteSpace(doctorRoleName))
                    {
                        string[] roleNameList = doctorRoleName.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (roleNameList != null && roleNameList.Length > 0)
                        {
                            string strWhere = " where";
                            foreach (string roleName in roleNameList)
                            {
                                strWhere += string.Format(" RoleName = '{0}' or", roleName);
                            }

                            sql = string.Format("select g.*,tOnlineClient.IsOnline from (select tUser.LocalName,tUser.UserGuid from tUser,(select distinct UserGuid from tRole2User {0}) u where tUser.UserGuid = u.UserGuid) g left outer join tOnlineClient on g.UserGuid = tOnlineClient.UserGuid", strWhere.TrimEnd("or".ToCharArray()));

                            DataSet ds = new DataSet();

                            ds.Tables.Add(dal.ExecuteQuery(sql));

                            return ds;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetUserList, MSG=" + ex.Message + ", SQL=" + sql,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="strOrderGuid"></param>
        /// <param name="strCurUsaer"></param>
        /// <param name="strOpenModule"></param>
        /// <param name="nRetCode">1--lock by other or module  2--exist difference status  3--not exist image</param>
        /// <param name="szError"></param>
        /// <returns></returns>
        public virtual bool CheckOrderStatus(string strPatientGuid, string strOrderGuid, string strCurUser, string strLockType, ref int nRetCode, ref string szError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {

                string strSQL = string.Format("select count(*) from tSync where Guid='{0}'", strPatientGuid);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToUInt32(obj) > 0)
                {
                    //locked by QC module
                    nRetCode = 1;
                    return true;
                }

                bool bLocked = false;
                if (strLockType.ToUpper() == "ORDER")
                {
                    strSQL = string.Format("select * from tSync where Guid='{0}'", strOrderGuid);
                    dt = oKodak.ExecuteQuery(strSQL);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //locked by other
                        DataRow dr = dt.Rows[0];
                        szError = string.Format("Owner={0}&OwnerIP={1}", Convert.ToString(dr["Owner"]), Convert.ToString(dr["OwnerIP"]));
                        nRetCode = 1;
                        return true;
                    }
                }
                else
                {
                    //
                    strSQL = string.Format("select * from tSync where Guid='{0}'", strOrderGuid);
                    dt = oKodak.ExecuteQuery(strSQL);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RPGuids"] != null && !(dt.Rows[0]["RPGuids"] is DBNull) && Convert.ToString(dt.Rows[0]["RPGuids"]).Trim().Length > 0)
                        {
                            //Locked by report module
                            string strRpGuids = Convert.ToString(dt.Rows[0]["RPGuids"]);
                            char[] sep1 = { '|' };
                            char[] sep2 = { '&' };
                            string[] arrItems1 = strRpGuids.Split(sep1);
                            foreach (string strSegment in arrItems1)
                            {
                                string[] arritems2 = strSegment.Split(sep2);
                                if (arritems2[0] == null || arritems2[0].Trim().Length == 0) //Rpguid 
                                {
                                    continue;
                                }
                                if (arritems2[1] != null && arritems2[1].Trim().Length > 0 && arritems2[1] != strCurUser)
                                {
                                    bLocked = true;
                                    break;

                                }

                            }
                            DataRow dr = dt.Rows[0];
                            szError = string.Format("Owner={0}&OwnerIP={1}", Convert.ToString(dr["Owner"]), Convert.ToString(dr["OwnerIP"]));
                        }
                        else
                        {
                            string lockOwner = Convert.ToString(dt.Rows[0]["Owner"]);

                            if (lockOwner != string.Empty && lockOwner != strCurUser)
                            {
                                bLocked = true;
                            }
                        }

                    }
                }

                if (bLocked)
                {
                    //lock by other
                    nRetCode = 1;
                    return true;
                }


                strSQL = string.Format("SELECT status,Count(*) from tRegProcedure  where OrderGuid='{0}' group by status", strOrderGuid);
                dt = oKodak.ExecuteQuery(strSQL);
                if (dt.Rows.Count > 1)
                {
                    nRetCode = 2;
                    return true;
                }
                else
                {
                    //Can not referral the RP 10,20,25,105 and 120
                    if (Convert.ToInt32(dt.Rows[0]["status"]) == 10 || Convert.ToInt32(dt.Rows[0]["status"]) == 20 || Convert.ToInt32(dt.Rows[0]["status"]) == 25 || Convert.ToInt32(dt.Rows[0]["status"]) == 105 || Convert.ToInt32(dt.Rows[0]["status"]) == 120)
                    {
                        nRetCode = 4;
                        return true;
                    }
                    nRetCode = 0;
                }



                strSQL = string.Format("select count(*) from tRegProcedure where OrderGuid='{0}' and IsExistImage=0", strOrderGuid);
                obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToUInt32(obj) > 0)
                {
                    //Not exist image
                    nRetCode = 3;
                    return true;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetConditionRelatedControlData, MSG=" + ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                bReturn = false;
            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
            return bReturn;

        }
        public virtual bool GetBookingNoticeTemplate(DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {

                string strSQL = "SELECT * from tBookingNoticeTemplate ";

                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "BookingNoticeTemplate";
                ds.Tables.Add(dt);


            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        #region RegIntegration
        public virtual bool GeneratePatientIDEx(ref string strPatientID, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dtTemp = new DataTable();

            try
            {
                bool bReady = false;
                int nMaxCount = 0;
                string strSQL = "";
                string strPrefix = "";
                //Get PatientID from idrecyclebin
                oKodak.BeginTransaction();
                object obj = null;
                //object obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='PatientIDPrefix' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                strPrefix = GetSystemProfileValue("PatientIDPrefix", "0000");

                int nLength = 0;
                string strValue = GetSystemProfileValue("PatientIDLength", "0000");
                //object obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='PatientIDLength' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                if (strValue != string.Empty)
                {
                    nLength = Convert.ToInt32(strValue);
                }

                bool bReclaim = false;
                strValue = GetSystemProfileValue("ReclaimNo", "0000");
                //obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='ReclaimNo' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                if (strValue != string.Empty)
                {
                    int iReclaim = Convert.ToInt32(strValue);
                    if (iReclaim == 0)
                        bReclaim = false;
                    else
                        bReclaim = true;
                }

                if (bReclaim)
                {

                    Object oValue = oKodak.ExecuteScalar("select top 1 Value from tIDRecycleBin with(tablockx) where Tag=2", RisDAL.ConnectionState.KeepOpen);
                    if (oValue != null)
                    {
                        nMaxCount = Convert.ToInt32(oValue);
                        string strTemp = nMaxCount.ToString();
                        Char sz = '0';
                        strPatientID = strPrefix + strTemp.PadLeft(nLength, sz);
                        strSQL = string.Format("SELECT count(*) FROM tRegPatient where PatientID='{0}'", strPatientID);

                        obj = oKodak.ExecuteScalar(strSQL, RisDAL.ConnectionState.KeepOpen);
                        if (obj == null)
                        {
                            throw new Exception("Unknown error");
                        }

                        int nCount = Convert.ToInt32(obj);
                        if (nCount == 0)
                        {
                            bReady = true;
                        }
                        strSQL = string.Format("DELETE FROM  tIDRecycleBin WHERE Tag=2 and Value='{0}'", nMaxCount);

                        oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);

                    }
                }
                if (!bReady)//Generate PatientID 
                {

                    obj = oKodak.ExecuteScalar("SELECT Value FROM tIDMaxValue with(tablockx) WHERE Tag=2", RisDAL.ConnectionState.KeepOpen);
                    if (obj == null)
                    {
                        throw new Exception("Unknown error");
                    }
                    string strMaxCount = obj.ToString();
                    strMaxCount = strMaxCount.Trim();
                    nMaxCount = Convert.ToInt32(strMaxCount) + 1;

                    strSQL = string.Format("UPDATE tIDMaxValue SET Value={0} WHERE Tag=2", nMaxCount);

                    oKodak.ExecuteQuery(strSQL, RisDAL.ConnectionState.KeepOpen);

                    string strTemp = nMaxCount.ToString();
                    Char sz = '0';
                    strPatientID = strPrefix + strTemp.PadLeft(nLength, sz);

                }
                oKodak.CommitTransaction();

            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public virtual bool GenerateAccNoEx(ref string strAccNo, ref string strError, ref int iPolicy)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {

                int nMaxCount = 0;
                Char szZero = '0';
                string strSQL = "";
                ///nPolicy=1   保持原RIS1.1的生成规则，前缀＋系统流水号。（系统流水号自增每次加1，不区分检查类型。在这种模式下，AccessionNumber在点击新建病人时产生。
                ///nPolicy=2   当天日期＋nLength数字,不分检查类型自动产生流水序号，每天都会从0001开始
                ///nPolicy=3   检查类型＋当天日期＋nLength数字 不分检查类型自动产生流水序号,每天都会从0001开始

                oKodak.BeginTransaction();

                object obj = null;
                int nPolicy = 1;
                string strValue = GetSystemProfileValue("AccNoPolicy", "0000");
                //object obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='AccNoPolicy' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                if (strValue != string.Empty)
                {
                    nPolicy = Convert.ToInt32(strValue);
                }
                else
                {
                    nPolicy = 1;
                }

                string strPrefix = "";
                //obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='AccNoPrefix' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                strPrefix = GetSystemProfileValue("AccNoPrefix", "0000");

                int nLength = 0;
                strValue = GetSystemProfileValue("AccNoLength", "0000");
                //obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='AccNoLength' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                if (strValue != string.Empty)
                {
                    nLength = Convert.ToInt32(strValue);
                }
                else
                {
                    nLength = 6;
                }

                bool bReclaimNo = false;
                strValue = GetSystemProfileValue("ReclaimNo", "0000");
                //obj = oKodak.ExecuteScalar("select value from tsystemprofile where name ='ReclaimNo' and ModuleID = '0000'", KodakDAL.ConnectionState.KeepOpen);
                if (strValue != string.Empty)
                {
                    int iReclaim = Convert.ToInt32(strValue);
                    if (iReclaim == 0)
                        bReclaimNo = false;
                    else
                        bReclaimNo = true;
                }

                if (nPolicy == 1)
                {
                    bool bReady = false;
                    if (bReclaimNo)
                    {

                        Object oValue = oKodak.ExecuteScalar("select top 1 Value from tIDRecycleBin with(tablockx) where Tag=3", RisDAL.ConnectionState.KeepOpen);
                        if (oValue != null)
                        {
                            nMaxCount = Convert.ToInt32(oValue);
                            strAccNo = AssembleAccNo(nPolicy, strPrefix, nLength, nMaxCount);
                            strSQL = string.Format("SELECT count(*) FROM tRegOrder where AccNo='{0}'", strAccNo);

                            obj = oKodak.ExecuteScalar(strSQL, RisDAL.ConnectionState.KeepOpen);
                            if (obj == null)
                            {
                                throw new Exception("Unknown error");
                            }
                            int nCount = Convert.ToInt32(obj);
                            if (nCount == 0)
                            {
                                bReady = true;
                            }
                            strSQL = string.Format("DELETE FROM  tIDRecycleBin WHERE Tag=3 and Value='{0}'", nMaxCount);

                            oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);

                        }
                    }
                    if (!bReady)
                    {

                        obj = oKodak.ExecuteScalar("SELECT Value FROM tIDMaxValue  with(tablockx) WHERE Tag=3", RisDAL.ConnectionState.KeepOpen);
                        if (obj == null)
                        {
                            throw new Exception("Unknown error");
                        }

                        string strMaxCount = obj.ToString();
                        strMaxCount = strMaxCount.Trim();
                        nMaxCount = Convert.ToInt32(strMaxCount) + 1;
                        strSQL = string.Format("UPDATE tIDMaxValue SET Value={0} WHERE Tag=3", nMaxCount);

                        oKodak.ExecuteQuery(strSQL, RisDAL.ConnectionState.KeepOpen);

                    }
                }
                else
                {

                    strSQL = string.Format("select count(*) from tIDMaxValue where CreateDt='{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                    obj = oKodak.ExecuteScalar(strSQL, RisDAL.ConnectionState.KeepOpen);
                    if (obj == null || Convert.ToInt32(obj) == 0)
                    {
                        nMaxCount = 1;
                        strSQL = string.Format("UPDATE tIDMaxValue SET Value={0},CreateDt='{1}' WHERE Tag=3", nMaxCount, DateTime.Now.ToString("yyyy-MM-dd"));
                        oKodak.ExecuteQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                    }
                    else
                    {

                        obj = oKodak.ExecuteScalar("SELECT Value FROM tIDMaxValue  with(tablockx) WHERE Tag=3", RisDAL.ConnectionState.KeepOpen);
                        if (obj == null)
                        {
                            throw new Exception("Unknown error");
                        }

                        string strMaxCount = obj.ToString();
                        strMaxCount = strMaxCount.Trim();
                        nMaxCount = Convert.ToInt32(strMaxCount) + 1;
                        strSQL = string.Format("UPDATE tIDMaxValue SET Value={0}  WHERE Tag=3", nMaxCount);
                        oKodak.ExecuteQuery(strSQL, RisDAL.ConnectionState.KeepOpen);

                    }

                }

                strAccNo = AssembleAccNo(nPolicy, strPrefix, nLength, nMaxCount);
                oKodak.CommitTransaction();
                iPolicy = nPolicy;

            }
            catch (Exception ex)
            {

                bReturn = false;
                strError = ex.Message;



                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        private string AssembleAccNo(int nPolicy, string strPrefix, int nLength, int nSequence)
        {

            string strAccNo = "";
            Char szZero = '0';
            string strTemp = string.Format("{0}", nSequence);

            try
            {

                if (nPolicy == 1)
                {
                    strAccNo = strPrefix + strTemp.PadLeft(nLength, szZero);
                }
                else if (nPolicy == 2)
                {
                    string strYear = DateTime.Now.Year.ToString();
                    string strMonth = DateTime.Now.Month.ToString();
                    string strDay = DateTime.Now.Day.ToString();
                    strMonth = strMonth.PadLeft(2, szZero);
                    strDay = strDay.PadLeft(2, szZero);

                    strAccNo = strYear + strMonth + strDay + strTemp.PadLeft(4, szZero);
                }
                else if (nPolicy == 3)
                {
                    string strYear = DateTime.Now.Year.ToString();
                    string strMonth = DateTime.Now.Month.ToString();
                    string strDay = DateTime.Now.Day.ToString();
                    strMonth = strMonth.PadLeft(2, szZero);
                    strDay = strDay.PadLeft(2, szZero);

                    strAccNo = strYear + strMonth + strDay + strTemp.PadLeft(4, szZero);
                }
                else
                {
                    throw new Exception("Set AccNo Policy bewteen 1 and 3 first please");
                }
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                  (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            return strAccNo;
        }

        public virtual bool RegIntegrationSavePatient(PatientModel pModel, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string szError = "";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("@PatientID", pModel.PatientID);
                oKodak.Parameters.AddVarChar("@PatientName", pModel.LocalName);
                oKodak.Parameters.AddVarChar("@EnglishName", pModel.EnglishName);
                oKodak.Parameters.AddVarChar("@Gender", pModel.Gender);
                oKodak.Parameters.AddDateTime("@Birthday", pModel.Birthday);
                oKodak.Parameters.AddVarChar("@Address", pModel.Address);
                oKodak.Parameters.Add("@Telephone", pModel.Telephone);
                oKodak.Parameters.AddVarChar("@HISID", pModel.HISID);
                oKodak.Parameters.AddInt("@IsVIP", Convert.ToInt32(pModel.IsVip));
                oKodak.Parameters.AddDateTime("@CreateDt", pModel.CreateDt);
                oKodak.Parameters.AddVarChar("@szError", szError, 128, ParameterDirection.Output, RisDAL.StringConversion.Trim);
                oKodak.ExecuteNonQuerySP("SP_SAVEPATIENT");
                if (szError != string.Empty)
                {
                    strError = szError;
                    bReturn = false;
                }
            }
            catch (System.Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public bool LoadPatient(string strKey, string strValue, DataTable dtPatient, ref string strError)
        {
            bool bReturn = true;

            RisDAL oKodak = new RisDAL();
            try
            {
                string strSQL = "";
                strSQL = string.Format("SELECT  distinct PatientGuid,PatientID,LocalName,EnglishName,RemotePID,Birthday,Gender,Address,Telephone,ReferenceNo,IsVIP,CreateDt,Comments,Domain FROM tRegPatient WHERE {0} ='{1}' order by tRegPatient.CreateDt desc", strKey, strValue);
                oKodak.ExecuteQuery(strSQL, dtPatient);
                dtPatient.TableName = "tPatient";

            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public bool LocatePatientByHISInfo(string strLocalPID, string HISID, string strPatientName, ref DataTable dtPatient, ref string szError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string strSQL = "";
                string sqlOrder = "";
                string sqlPatient = "";
                bool bSearchByName = false;
                if (strLocalPID.Length > 0)
                {
                    strSQL = string.Format("SELECT PatientGuid,PatientID,LocalName,EnglishName,Birthday,Gender,Address,Telephone,ReferenceNo,IsVIP,Domain,CreateDt,Comments,RemotePID FROM tRegPatient WHERE PatientID='{0}'", strLocalPID);
                    dtPatient = oKodak.ExecuteQuery(strSQL);
                    dtPatient.TableName = "tPatient";
                }
                else
                {
                    //strSQL = string.Format("SELECT Value FROM tSystemProfile where name='SearchPatientByName' AND ModuleID='0E00'");
                    //object obj = null;//oKodak.ExecuteScalar(strSQL);
                    string value = GetSystemProfileValue("SearchPatientByName", "0E00");
                    if (value == "1")
                    {
                        bSearchByName = true;
                    }
                    if (bSearchByName)
                    {

                        //search order ->search patient -> search all order
                        sqlOrder = string.Format("SELECT PATIENTGUID FROM tRegOrder WHERE HISID ='{0}'", HISID);
                        sqlPatient = string.Format("SELECT distinct A.PatientGuid,A.PatientID,A.LocalName,A.EnglishName,A.Birthday,A.Gender,A.Address,A.Telephone,A.ReferenceNo,A.IsVIP,A.Domain,A.CreateDt,A.Comments,A.RemotePID  FROM tRegPatient A  WHERE A.LocalName='{0}'", strPatientName);
                        //                                "SELECT PATIENTGUID,PATIENTID,LOCALNAME,ISVIP,GENDER,BIRTHDAY,TELEPHONE,ADDRESS,WEIGHT,RemotePID FROM TREGPATIENT WHERE LOCALNAME='{0}'", patientName);
                        dtPatient = oKodak.ExecuteQuery(sqlPatient);
                        DataTable dtpid = oKodak.ExecuteQuery(sqlOrder);
                        //if (dtpid.Rows.Count > 0)
                        if (dtPatient != null)
                            dtPatient.PrimaryKey = new DataColumn[] { dtPatient.Columns["PATIENTGUID"] };

                        foreach (DataRow dr in dtpid.Rows)
                        {
                            sqlPatient = string.Format("SELECT distinct A.PatientGuid,A.PatientID,A.LocalName,A.EnglishName,A.Birthday,A.Gender,A.Address,A.Telephone,A.ReferenceNo,A.IsVIP,A.Domain,A.CreateDt,A.Comments,A.RemotePID  FROM tRegPatient A  WHERE A.LocalName='{0}'", dr[0].ToString());
                            DataTable dtTemp = oKodak.ExecuteQuery(sqlPatient);
                            if (dtTemp != null)
                            {
                                dtPatient.Merge(dtTemp);
                            }
                        }

                    }
                    else
                    {

                        #region Search not by name
                        strSQL = string.Format("SELECT  distinct A.PatientGuid,A.PatientID,A.LocalName,A.EnglishName,A.Birthday,A.Gender,A.Address,A.Telephone,A.ReferenceNo,A.IsVIP,A.Domain,A.CreateDt,A.Comments,A.RemotePID  FROM tRegPatient A,tRegOrder C WHERE A.PatientGuid=C.PatientGuid and c.HisID='{0}'", HISID);
                        dtPatient = oKodak.ExecuteQuery(strSQL);
                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            dtPatient.TableName = "tPatient";
                        }
                        else
                        {
                            //strSQL = string.Format("SELECT VALUE FROM tSystemProfile where name='SearchByNameNotExistHisID' AND ModuleID='0E00'");
                            value = GetSystemProfileValue("SearchByNameNotExistHisID", "0E00");
                            //obj = oKodak.ExecuteScalar(strSQL);
                            if (value != string.Empty && Convert.ToInt32(value) == 1)
                            {
                                strSQL = string.Format("SELECT distinct A.PatientGuid,A.PatientID,A.LocalName,A.EnglishName,A.Birthday,A.Gender,A.Address,A.Telephone,A.ReferenceNo,A.IsVIP,A.Domain,A.CreateDt,A.Comments,A.RemotePID  FROM tRegPatient A  WHERE A.LocalName='{0}'", strPatientName);
                                dtPatient = oKodak.ExecuteQuery(strSQL);
                                dtPatient.TableName = "tPatient";
                            }

                        }
                        #endregion

                    }

                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                szError = ex.Message;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public DataSet GetDictionaryValue(string tag)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //StringBuilder sb = new StringBuilder();

            //sb.Append("SELECT     B.Value AS Value, B.Text AS Text, B.IsDefault");
            //sb.Append(" FROM      tDictionary AS A INNER JOIN");
            //sb.Append(" tDictionaryValue AS B ON A.Tag = B.Tag AND A.Tag = " + tag + " ORDER BY OrderID");

            string sb = "SELECT Value AS Value, Text AS Text, IsDefault FROM tDictionaryValue Where Tag = " + tag + " and ((Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') ORDER BY OrderID";

            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString());
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
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

        #region Requisition
        /// <summary>
        /// get the requisition type(-1(both not exists),0(Scan requisition),1(Electronic Requisition),2(both exists)) and the xml requisition if it is a ERequisition
        /// </summary>
        /// <param name="strAccNo">accession No.</param>
        /// <param name="ds">dataset for erequisition xml data</param>
        /// <returns></returns>
        public virtual int GetRequisitionType(string strAccNo, DataSet ds)
        {
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQLScanRequisition = string.Format("Select count(1) from tRequisition where AccNo ='{0}'", strAccNo);
                string strSQLERequisition = string.Format("Select ERequisition From tRegOrder where AccNo = '{0}'  and len(ERequisition)>0 and ERequisition is not NULL", strAccNo);
                object objCount = oKodak.ExecuteScalar(strSQLScanRequisition);
                DataTable dtERequisition = oKodak.ExecuteQuery(strSQLERequisition);
                if (objCount != null && Convert.ToInt32(objCount) > 0)
                {
                    if (dtERequisition != null && dtERequisition.Rows.Count >0)
                    {
                        ds.Tables.Add(dtERequisition);
                        return 2;//both exists
                    }
                    else
                    {
                        return 0;//only Scan Requisition exists
                    }
                }
                else
                {
                    if (dtERequisition != null && dtERequisition.Rows.Count > 0 && dtERequisition.Rows[0]["ERequisition"].ToString().Trim().Length > 0)
                    {
                        ds.Tables.Add(dtERequisition);
                        return 1;//Only ERequisition exists
                    }
                    else
                    {
                        return -1;//both not exists
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
            return -1;
        }

        /// <summary>
        /// get the requisition type(-1(both not exists),0(Scan requisition),1(Electronic Requisition),2(both exists)) and the xml requisition if it is a ERequisition
        /// </summary>
        /// <param name="strAccNo">accession No.</param>
        /// <param name="ds">dataset for erequisition xml data</param>
        /// <returns></returns>
        public virtual int GetRequisitionType(bool loadArchive, string strAccNo, DataSet ds)
        {
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQLScanRequisitionLocal = string.Format(@"select count(1) from tRequisition where AccNo ='{0}'", strAccNo);
                string strSQLScanRequisitionArchive = string.Format(@"select count(1) from RISArchive.dbo.tRequisition where AccNo ='{0}'", strAccNo);
                string strSQLERequisitionLocal = string.Format(@"select ERequisition From tRegOrder where AccNo = '{0}' and len(ERequisition)>0 and ERequisition is not NULL", strAccNo);
                string strSQLERequisitionArchive = string.Format(@"select ERequisition From RISArchive.dbo.tRegOrder where AccNo = '{0}' and len(ERequisition)>0 and ERequisition is not NULL", strAccNo);
                bool isArchived = false;
                DataTable dtERequisition = null;

                //1. load e-requisition.
                dtERequisition = oKodak.ExecuteQuery(strSQLERequisitionLocal);
                if (dtERequisition != null && dtERequisition.Rows.Count == 0 && loadArchive)
                {
                    dtERequisition = oKodak.ExecuteQuery(strSQLERequisitionArchive);
                    if (dtERequisition != null && dtERequisition.Rows.Count > 0)
                    {
                        isArchived = true;
                    }
                }
                //2. load scan requistion.
                int objCount = 0;
                if (isArchived && loadArchive)
                {
                    objCount = Convert.ToInt32(oKodak.ExecuteScalar(strSQLScanRequisitionArchive));
                }
                else
                {
                    objCount = Convert.ToInt32(oKodak.ExecuteScalar(strSQLScanRequisitionLocal));
                }

                if (objCount > 0)
                {
                    if (dtERequisition != null && dtERequisition.Rows.Count > 0 && dtERequisition.Rows[0]["ERequisition"].ToString().Trim().Length > 0)
                    {
                        ds.Tables.Add(dtERequisition);
                        return 2;//both exists
                    }
                    else
                    {
                        return 0;//only Scan Requisition exists
                    }
                }
                else
                {
                    if (dtERequisition != null && dtERequisition.Rows.Count > 0 && dtERequisition.Rows[0]["ERequisition"].ToString().Trim().Length > 0)
                    {
                        ds.Tables.Add(dtERequisition);
                        return 1;//Only ERequisition exists
                    }
                    else
                    {
                        return -1;//both not exists
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
            return -1;
        }

        /// <summary>
        /// SaveERequisitionTemplateGuid to tregorder's ERequisition's PrintTemplateGuid field
        /// </summary>
        /// <param name="strAccNo"></param>
        /// <param name="printTemplateGuid"></param>
        /// <returns></returns>
        public virtual bool SaveERequisitionTemplateGuid(string strAccNo, string printTemplateGuid)
        {
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQLDeletePrintTemplateGuid = string.Format(@"SET ARITHABORT ON; UPDATE tregorder SET erequisition.modify('delete(/EREQUISITION/PrintTemplateGuid)') where AccNo ='{0}'", strAccNo);
                string strSQLUpdatePrintTemplateGuid = string.Format(@"UPDATE tregorder SET erequisition.modify('insert <PrintTemplateGuid>{0}</PrintTemplateGuid> as last into (/EREQUISITION)[1]') where AccNo ='{1}'", printTemplateGuid, strAccNo);

                oKodak.BeginTransaction();
                oKodak.ExecuteNonQuery(strSQLDeletePrintTemplateGuid, RisDAL.ConnectionState.KeepOpen);
                oKodak.ExecuteNonQuery(strSQLUpdatePrintTemplateGuid, RisDAL.ConnectionState.KeepOpen);
                oKodak.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                oKodak.RollbackTransaction();
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
            return false;
        }
        /// <summary>
        /// get the filed value for web requisition's parameter using.
        /// </summary>
        /// <param name="selectTable"></param>
        /// <param name="selectField"></param>
        /// <param name="uniqueIDField"></param>
        /// <param name="uniqueIDFieldValue"></param>
        public virtual string GetFieldValue(string selectTable, string selectField, string uniqueIDField, string uniqueIDFieldValue)
        {
            //KodakDAL oKodak = new KodakDAL();
            using (var oKodak = new RisDAL())
            {
                DataTable dt = new DataTable("tFieldValue");
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("@SelectTableName", selectTable);
                oKodak.Parameters.AddVarChar("@SelectFieldName", selectField);
                oKodak.Parameters.AddVarChar("@UniqueFieldName", uniqueIDField);
                oKodak.Parameters.AddVarChar("@UniqueFieldValue", uniqueIDFieldValue);
                oKodak.ExecuteQuerySP("SP_SelectFieldValue", dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0][0]);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        public virtual DataSet QueryAllOnlineOfflineUser()
        {

            bool bReturn = true;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            RisDAL oKodak = new RisDAL();
            string strSQL = "";
            try
            {

                strSQL = "SELECT A.UserGuid, A.MachineIP,A.RoleName,A.IISUrl,A.LoginTime,A.Comments,A.SessionID,cast(A.IsOnline as nvarchar(8)) as IsOnline,A.IsOnline as offline,A.Site,A.[Domain],B.LocalName,B.LoginName FROM tOnlineClient A left join tUser B on A.UserGuid=B.UserGuid ";
                dt = oKodak.ExecuteQuery(strSQL);


                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "QueryAllOnlineOfflineUser, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return ds;
        }
        public virtual bool GetOrderInfo(string strOrderGuid, DataSet dsOrderInfo)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            string strSQL = "";
            try
            {

                strSQL = string.Format("SELECT * from tRegOrder where OrderGuid='{0}'", strOrderGuid);
                oKodak.ExecuteQuery(strSQL, dt);
                dsOrderInfo.Tables.Add(dt);
                //Object obj = oKodak.ExecuteScalar(strSQL);
                //if (obj == null)
                //{
                //    dtOrderInfo = "";
                //}
                //else
                //{
                //    dtOrderInfo = Convert.ToString(obj);
                //}

            }
            catch (Exception ex)
            {
                bReturn = false;
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetOrderMessage, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public virtual bool UpdateOrderMessage(string strOrderGuid, string strOrderMessage, string strApplyDept, string strApplyDoctor)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string strSQL = "";
            try
            {

                strSQL = string.Format("Update tRegOrder set OrderMessage='{0}' , ApplyDept ='{1}' , ApplyDoctor ='{2}' where OrderGuid='{3}'", convertStringForSQL(strOrderMessage), strApplyDept, strApplyDoctor, strOrderGuid);
                oKodak.ExecuteNonQuery(strSQL);

            }
            catch (Exception ex)
            {
                bReturn = false;
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "UpdateOrderMessage, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
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


        public virtual bool IsArchived(string strPatientID, ref int nArchived)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string strSQL = "";
            try
            {
                strSQL = string.Format("select Archive from  tPatientList where PatientID='{0}'", strPatientID);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) == 1)
                {
                    nArchived = 1;
                }
                else
                {
                    nArchived = 0;
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "IsArchived, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
        public virtual bool SendFetchCommand(string strPatientID)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string strSQL = "";
            try
            {
                Object obj = oKodak.ExecuteScalar(string.Format("select PatientGuid from  tPatientList where PatientID='{0}'", strPatientID));
                if (obj == null || Convert.ToString(obj).Trim().Length == 0)
                {
                    throw new Exception("Can not get patient guid by patient id");
                }
                string strPatientGuid = Convert.ToString(obj);

                strSQL = string.Format("if not exists(select 1 from tArchiveEvent where ObjectGuid='{0}' and type=1) ", strPatientGuid)
                        + string.Format(" insert into tArchiveEvent(type,ObjectGuid) values(1,'{0}')", strPatientGuid);
                oKodak.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                bReturn = false;
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "IsArchived, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        public DataSet COMMON_getDataTable(string tableName, string matchingKey, string matchingValue)
        {
            string strSQL = "";

            RisDAL oKodak = new RisDAL();

            DataSet ds = new DataSet();

            try
            {
                strSQL = string.Format(" select * from {0} where {1}='{2}' ", tableName, matchingKey, matchingValue);

                oKodak.ExecuteQuery(strSQL, ds, tableName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "COMMON_getTableInfo, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }

            return ds;
        }

        public object COMMON_queryDataTable(object parameters)
        {
            string strSQL = "";

            RisDAL oKodak = new RisDAL();

            DataSet ds = new DataSet();

            try
            {
                #region Parse the parameters

                Dictionary<string, object> paramMap = parameters as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    //throw (new Exception("No parameter in GetReportsListDAO!"));

                    logger.Info((long)ModuleEnum.CommonModule,
                        ModuleInstanceName.CommonModule,
                        53,
                        "No parameter in COMMON_queryDataTable!",
                        Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                }

                string id = "", viewName = "";
                string condition = "", userGuid = "";
                int nPagesize = 0, nCurpage = 0;
                string strOrderBy = "", resultColumns = "";
                bool isOnlyCalculateCount = false;
                bool isCalculateCountandFee = false;
                foreach (string key in paramMap.Keys)
                {
                    switch (key.ToUpper())
                    {
                        case "CONDITION":
                        case "SQLWHERE":
                            {
                                condition = paramMap[key] as string;

                                if (condition == null)
                                    condition = "";
                            }
                            break;
                        case "PAGESIZE":
                            {
                                nPagesize = System.Convert.ToInt32(paramMap[key] as string);
                            }
                            break;
                        case "CURPAGE":
                        case "PAGEINDEX":
                            {
                                nCurpage = System.Convert.ToInt32(paramMap[key] as string);
                            }
                            break;
                        case "USERID":
                        case "USERGUID":
                            {
                                userGuid = paramMap[key] as string;
                            }
                            break;
                        case "ID":
                        case "LISTID":
                        case "GRIDID":
                            {
                                id = paramMap[key] as string;
                            }
                            break;
                        case "VIEWNAME":
                        case "VIEW":
                        case "TABLENAME":
                            {
                                viewName = paramMap[key] as string;
                            }
                            break;
                        case "ORDERBY":
                        case "SORTING":
                            {
                                strOrderBy = paramMap[key] as string;
                            }
                            break;
                        case "RESULTCOLUMNS":
                        case "RESULTCOLUMN":
                        case "GRIDCOLUMNS":
                        case "GRIDCOLUMN":
                            {
                                resultColumns = paramMap[key] as string;
                            }
                            break;
                        case "CALCULATECOUNT":
                            {
                                isOnlyCalculateCount = ("1" == paramMap[key] as string);
                            }
                            break;
                        case "CALCULATECOUNTANDFEE":
                            {
                                isCalculateCountandFee = ("1" == paramMap[key] as string);
                            }
                            break;
                    }
                }

                condition = condition == null ? "" : condition;
                userGuid = userGuid == null ? "" : userGuid;
                id = id == null ? "" : id;
                nPagesize = nPagesize < 1 ? 30 : nPagesize;
                nCurpage = nCurpage < 1 ? 1 : nCurpage;

                #endregion

                Dictionary<string, object> outMap = new Dictionary<string, object>();

                if (viewName.ToUpper().StartsWith("USP_"))
                #region Stored Procedure
                {
                    if (isOnlyCalculateCount)
                    #region calculate count
                    {
                        if (string.IsNullOrEmpty(condition))
                            condition = "1=1";

                        oKodak.Parameters.Clear();
                        oKodak.Parameters.AddVarChar("@Conditions", condition);

                        int nTotalCount = 0;
                        oKodak.Parameters.AddInt("@TotalCount", nTotalCount, ParameterDirection.Output);

                        DataTable dt1 = new DataTable();
                        oKodak.ExecuteQuerySP(viewName, dt1);

                        dt1.TableName = "ReportList";

                        if (oKodak.Parameters["@TotalCount"].Value != null)
                        {
                            nTotalCount = Convert.ToInt32(oKodak.Parameters["@TotalCount"].Value);

                            outMap.Add("CurPage", nTotalCount);
                            outMap.Add("TotalCount", nTotalCount);
                        }
                    }
                    #endregion
                    else if (isCalculateCountandFee)
                    #region calculate count and Fee
                    {
                        if (string.IsNullOrEmpty(condition))
                            condition = "1=1";

                        strSQL = string.Format(" select count(1),sum(tRegProcedure.Charge) AS tRegProcedure__Charge"
                            + " from tRegPatient with(nolock), tRegOrder with(nolock), tRegProcedure with(nolock)"
                            + "  LEFT JOIN tReport with(nolock) on tReport.reportguid=tRegProcedure.reportguid "
                            + " where tRegPatient.PatientGuid=tRegOrder.PatientGuid AND tRegOrder.OrderGuid=tRegProcedure.OrderGuid AND tRegProcedure.Status>=0"
                            + "  AND {0} ",
                            condition);

                        DataTable tmpDt = new DataTable();

                        oKodak.ExecuteQuery(strSQL, tmpDt);

                        if (tmpDt != null && tmpDt.Rows.Count > 0)
                        {
                            ds.Tables.Add(tmpDt);
                            outMap.Add("DataSet", ds);
                        }
                    }
                    #endregion
                    else
                    #region query page data
                    {
                        if (string.IsNullOrEmpty(strOrderBy))
                        {
                            strOrderBy = "Order By (select 0)";
                        }
                        else if (!strOrderBy.Trim().ToUpper().StartsWith("ORDER BY "))
                        {
                            strOrderBy = "ORDER BY " + strOrderBy;
                        }

                        if (string.IsNullOrEmpty(resultColumns))
                            resultColumns = "*";

                        if (string.IsNullOrEmpty(condition))
                            condition = "1=1";

                        oKodak.Parameters.Clear();
                        oKodak.Parameters.AddInt("@PageIndex", nCurpage);
                        oKodak.Parameters.AddInt("@PageSize", nPagesize);
                        oKodak.Parameters.AddVarChar("@Columns", resultColumns);



                        oKodak.Parameters.AddVarChar("@Conditions", condition);
                        oKodak.Parameters.AddVarChar("@OrderBy", strOrderBy);

                        DataTable tmpDt = new DataTable();
                        oKodak.ExecuteQuerySP(viewName, tmpDt);
                        tmpDt.TableName = "PageData";
                        ds.Tables.Add(tmpDt);

                        //if (tmpDt != null)
                        //{
                        //    if (tmpDt.Rows.Count > 0)
                        //    {
                        //        if (tmpDt.Rows.Count <= (Int64)nCurpage * nPagesize)
                        //        {
                        //            int ipage = (tmpDt.Rows.Count + nPagesize - 1) / nPagesize;

                        //            nCurpage = ipage;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        nCurpage = 0;
                        //    }

                        //    StringBuilder sb = new StringBuilder();
                        //    sb.AppendFormat("rownum > {0} AND rownum <= {1}", (nCurpage - 1) * nPagesize, nCurpage * nPagesize);

                        //    tmpDt.DefaultView.RowFilter = sb.ToString();

                        //    ds.Tables.Add(tmpDt.DefaultView.ToTable("PageData"));
                        //}

                        outMap.Add("DataSet", ds);
                        outMap.Add("CurPage", nCurpage);
                        outMap.Add("TotalCount", 0);
                    }
                    #endregion
                }
                #endregion
                else
                #region VIEW
                {
                    if (isOnlyCalculateCount)
                    #region calculate count
                    {
                        if (string.IsNullOrEmpty(condition))
                            condition = "1=1";

                        strSQL = string.Format(" select count(1) from {0} where {1} ",
                            viewName, condition);

                        DataTable tmpDt = new DataTable();

                        oKodak.ExecuteQuery(strSQL, tmpDt);

                        if (tmpDt != null && tmpDt.Rows.Count > 0)
                        {
                            int cnt = System.Convert.ToInt32(tmpDt.Rows[0][0]);

                            outMap.Add("CurPage", cnt);
                            outMap.Add("TotalCount", cnt);
                        }
                    }
                    #endregion
                    else if (isCalculateCountandFee)
                    #region calculate count and Fee
                    {
                        if (string.IsNullOrEmpty(condition))
                            condition = "1=1";

                        strSQL = string.Format(" select count(1),sum(tRegProcedure__Charge) from {0} where {1} ",
                            viewName, condition);

                        DataTable tmpDt = new DataTable();

                        oKodak.ExecuteQuery(strSQL, tmpDt);

                        if (tmpDt != null && tmpDt.Rows.Count > 0)
                        {
                            ds.Tables.Add(tmpDt);
                            outMap.Add("DataSet", ds);
                        }
                    }
                    #endregion
                    else
                    #region query page data
                    {
                        if (string.IsNullOrEmpty(strOrderBy))
                        {
                            strOrderBy = "Order By (select 0)";
                        }
                        else if (!strOrderBy.Trim().ToUpper().StartsWith("ORDER BY "))
                        {
                            strOrderBy = "ORDER BY " + strOrderBy;
                        }

                        if (string.IsNullOrEmpty(resultColumns))
                            resultColumns = "*";

                        if (string.IsNullOrEmpty(condition))
                            condition = "1=1";

                        strSQL = string.Format(" select TOP {0} ROW_NUMBER() OVER({1}) ROWNUM, {2} from {3} where {4} ",
                            nPagesize * nCurpage, strOrderBy, resultColumns, viewName, condition);

                        DataTable tmpDt = new DataTable();

                        oKodak.ExecuteQuery(strSQL, tmpDt);

                        if (tmpDt != null)
                        {
                            if (tmpDt.Rows.Count > 0)
                            {
                                if (tmpDt.Rows.Count <= (Int64)nCurpage * nPagesize)
                                {
                                    int ipage = (tmpDt.Rows.Count + nPagesize - 1) / nPagesize;

                                    nCurpage = ipage;
                                }
                            }
                            else
                            {
                                nCurpage = 0;
                            }

                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("rownum > {0} AND rownum <= {1}", (nCurpage - 1) * nPagesize, nCurpage * nPagesize);

                            tmpDt.DefaultView.RowFilter = sb.ToString();

                            ds.Tables.Add(tmpDt.DefaultView.ToTable("PageData"));
                        }

                        outMap.Add("DataSet", ds);
                        outMap.Add("CurPage", nCurpage);
                        outMap.Add("TotalCount", 0);
                    }
                    #endregion
                }
                #endregion

                return outMap;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "COMMON_queryDataTable, MSG=" + ex.Message + ", SQL=" + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }

            return null;
        }

      
        public DataSet getDynamicFormStructure()
        {
            string sqlFS = "";

            RisDAL dal = new RisDAL();

            DataSet ds = new DataSet();

            try
            {
                sqlFS = " select * from dic_FormStructure order by fsOrder ";

                dal.ExecuteQuery(sqlFS, ds, "DIC_FORMSTRUCTURE");

                sqlFS = " select * from dic_FormType ";

                dal.ExecuteQuery(sqlFS, ds, "DIC_FORMTYPE");

                DataTable dtFormStructure = ds.Tables[0];
                foreach (DataRow dr in dtFormStructure.Rows)
                {
                    string dataType = dr["FSDATASOURCETYPE"] is DBNull ? "" : System.Convert.ToString(dr["FSDATASOURCETYPE"]);
                    string datasrc = System.Convert.ToString(dr["FSDATASOURCE"]);

                    if (string.IsNullOrEmpty(datasrc))
                        continue;

                    if (dataType == "2") // dictionary
                    {
                        //string sql = " if isnumeric('" + datasrc + "') <> 1 \r\n"
                        //    + " select tDictionaryValue.Value as VALUE, tDictionaryValue.Text as TEXT"
                        //    + " from tDictionary, tDictionaryValue where tDictionary.tag = tDictionaryValue.tag"
                        //    + " and tDictionary.Name = '" + datasrc + "' order by OrderID, Text \r\n"
                        //    + " else \r\n"
                        //    + " select tDictionaryValue.Value as VALUE, tDictionaryValue.Text as TEXT"
                        //    + " from tDictionary, tDictionaryValue where tDictionary.tag = tDictionaryValue.tag"
                        //    + " and tDictionary.tag = '" + datasrc + "' order by OrderID, Text ";

                        string sql = " if isnumeric('" + datasrc + "') <> 1 \r\n"
                            + " select tDictionaryValue.Value as VALUE, tDictionaryValue.Text as TEXT"
                            + " from tDictionary, tDictionaryValue where (tDictionary.tag = tDictionaryValue.tag"
                            + " and tDictionary.Name = '" + datasrc + "')and ((tDictionaryValue.Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') order by OrderID, Text \r\n"
                            + " else \r\n"
                            + " select Value as VALUE, Text as TEXT"
                            + " from tDictionaryValue where tag = '" + datasrc + "' order by OrderID, Text ";

                        SetDataSource(dal, dr, sql);
                    }
                    else if (dataType == "3") // query
                    {
                        string sql = datasrc;

                        SetDataSource(dal, dr, sql);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "getDynamicFormStructure, MSG=" + ex.Message + ", SQL=" + sqlFS,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }

            return ds;
        }

        private static void SetDataSource(RisDAL dal, DataRow dr, string sql)
        {
            const string seperator1 = "%%^%%";
            const string seperator2 = "%^%";

            try
            {
                DataTable dt2 = dal.ExecuteQuery(sql);

                string tmpstr = string.Empty;
                int cnt = dt2.Rows.Count;
                int nTmp = 0;

                foreach (DataRow tmpdr in dt2.Rows)
                {
                    tmpstr += System.Convert.ToString(tmpdr["VALUE"])
                        + seperator2
                        + System.Convert.ToString(tmpdr["TEXT"]);

                    if (nTmp++ < cnt - 1)
                    {
                        tmpstr += seperator1;
                    }
                    else
                    {
                        break;
                    }
                }

                dr["FSDATASOURCETYPE"] = 0;
                dr["FSDATASOURCE"] = tmpstr;
            }
            catch (Exception ex)
            {
                //Log.Log.error("REG", "SetDataSource, MSG=" + ex.Message + ", SQL=" + sql);
            }
        }

        private static bool isDuplicatedShortcutName(string id, string strName, int category, int type, string owner, RisDAL dal)
        {
            string strSQl1;

            if (type == 1)
            {
                // private
                strSQl1 = string.Format(
                    "select Name from tShortcut where ShortcutGuid <> '{0}' AND Type = 1 AND Name = '{1}' AND Category = {2} AND Owner = '{3}' ",
                    id.Trim(), strName.Trim(), category, owner.Trim());
            }
            else if (type == 2)
            {
                // site shared
                strSQl1 = string.Format(
                    "select Name from tShortcut where ShortcutGuid <> '{0}' AND Type = 2 and Name = '{1}' and Category = {2} ",
                    id.Trim(), strName.Trim(), category);

                string[] ownerlist = owner.Split(",;|".ToCharArray());

                string ownersql = " 1=2 ";
                foreach (string tmp in ownerlist)
                {
                    if (string.IsNullOrEmpty(tmp))
                        continue;

                    ownersql += " OR ','+Owner+',' like '%," + tmp.Trim() + ",%' ";
                }

                strSQl1 += " AND (" + ownersql + ") ";
            }
            else
            {
                // public
                strSQl1 = string.Format(
                    "select Name from tShortcut where ShortcutGuid <> '{0}' AND Type = 0 and Name = '{1}' and Category = {2} ",
                    id.Trim(), strName.Trim(), category);
            }

            DataTable nameTable = dal.ExecuteQuery(strSQl1, RisDAL.ConnectionState.KeepOpen);

            return nameTable != null && nameTable.Rows.Count > 0;
        }

        public bool CanEditOrderMessage(string strOrderGuid)
        {
            string sql = string.Format("select count(1) from tregprocedure where orderguid ='{0}' and status != 0", strOrderGuid);
            //KodakDAL oKodakDAL = new KodakDAL();
            try
            {
                using (var oKodakDAL = new RisDAL())
                {
                    int result = Convert.ToInt32(oKodakDAL.ExecuteScalar(sql));
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return false;
            }

        }

        #region Profile Method

        /// <summary>
        /// SiteProfile->SystemProfile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public virtual string GetSystemProfileValue(string name, string moduleId)
        {

            string strSqlSys = "Select Value from tSystemProfile where name=@name and Domain=@Domain and ModuleId =@ModuleID";
            string strSqlSite = "Select Value from tSiteProfile where name=@name and Domain=@Domain and Site=@site and ModuleId =@ModuleID";
            string strCurDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            string strCurSite = CommonGlobalSettings.Utilities.GetCurSite();
            try
            {
                using (RisDAL oKodak = new RisDAL())
                {
                    //SiteProfile
                    oKodak.Parameters.AddVarChar("@name", name);
                    oKodak.Parameters.AddVarChar("@Domain", strCurDomain);
                    oKodak.Parameters.AddVarChar("@site", strCurSite);
                    oKodak.Parameters.AddVarChar("@ModuleId", moduleId);
                    object obj = oKodak.ExecuteScalar(strSqlSite);
                    if (obj != null)
                    {
                        return Convert.ToString(obj);
                    }
                    //SystemProfile
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddVarChar("@name", name);
                    oKodak.Parameters.AddVarChar("@Domain", strCurDomain);
                    oKodak.Parameters.AddVarChar("@ModuleId", moduleId);
                    obj = oKodak.ExecuteScalar(strSqlSys);
                    return Convert.ToString(obj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.CommonModule,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetSystemProfileValue, MSG=" + ex.Message + ", SQL=" + strSqlSys,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return "";
        }

        /// <summary>
        /// Update tSiteProfile  is exists the rpfoiel in siteProfile
        /// else Update systemProfile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="moduleId"></param>
        /// <param name="value"></param>
        public virtual void UpdateSystemProfile(string name, string moduleId, string value)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string strSql = @" if exists (select 1 from tSiteProfile where name=@name and ModuleId=@ModuleId and Domain=@Domain and Site=@Site)
                                               update tSiteProfile set Value =@Value where name=@name and ModuleId=@ModuleId and Domain=@Domain and Site=@Site
                                    else 
                                        Update tSystemProfile set Value=@value where name=@name and ModuleId=@ModuleId and Domain=@Domain";
                string strCurDomain = CommonGlobalSettings.Utilities.GetCurDomain();
                string strCurSite = CommonGlobalSettings.Utilities.GetCurSite();
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("@name", name);
                oKodak.Parameters.AddVarChar("@Domain", strCurDomain);
                oKodak.Parameters.AddVarChar("@site", strCurSite);
                oKodak.Parameters.AddVarChar("@ModuleId", moduleId);
                oKodak.Parameters.AddVarChar("@Value", value);
                oKodak.ExecuteNonQuery(strSql);
            }
        }
        /// <summary>
        /// RoleProfile->SiteProfile->SystemProfile
        /// </summary>
        /// <param name="roleName">role of current user</param>
        /// <param name="name">name of the some profile</param>
        /// <param name="moduleId">moduleId of the profile</param>
        /// <returns></returns>
        public virtual string GetProfileValue(string name, string roleName, string moduleId)
        {
            string strSqlSys = "Select Value from tSystemProfile where name=@name and Domain=@Domain and ModuleId =@ModuleID";
            string strSqlSite = "Select Value from tSiteProfile where name=@name and Domain=@Domain and Site=@site and ModuleId =@ModuleID";
            string strSqlRole = "Select Value from tRoleProfile where name=@name and Domain=@Domain and ModuleId =@ModuleID";
            string strCurDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            string strCurSite = CommonGlobalSettings.Utilities.GetCurSite();
            try
            {
                using (RisDAL oKodak = new RisDAL())
                {
                    object obj = null;
                    if (roleName != string.Empty)
                    {
                        //RoleProfile
                        oKodak.Parameters.AddVarChar("@name", name);
                        oKodak.Parameters.AddVarChar("@Domain", strCurDomain);
                        oKodak.Parameters.AddVarChar("@ModuleId", moduleId);
                        obj = oKodak.ExecuteScalar(strSqlRole);
                    }

                    if (obj != null)
                        return Convert.ToString(obj);
                    //SiteProfile
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddVarChar("@name", name);
                    oKodak.Parameters.AddVarChar("@Domain", strCurDomain);
                    oKodak.Parameters.AddVarChar("@site", strCurSite);
                    oKodak.Parameters.AddVarChar("@ModuleId", moduleId);
                    obj = oKodak.ExecuteScalar(strSqlSite);
                    if (obj != null)
                    {
                        return Convert.ToString(obj);
                    }
                    //SystemProfile
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddVarChar("@name", name);
                    oKodak.Parameters.AddVarChar("@Domain", strCurDomain);
                    oKodak.Parameters.AddVarChar("@ModuleId", moduleId);
                    obj = oKodak.ExecuteScalar(strSqlSys);
                    return Convert.ToString(obj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                logger.Error((long)ModuleEnum.CommonModule,
                    ModuleInstanceName.CommonModule,
                    53,
                    "GetProfileValue, MSG=" + ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return "";
        }


        #endregion

        #region messagecenter
        public bool PostEvent(DataTable dtModel)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string parameter = "";
            try
            {
                if (dtModel != null && dtModel.Rows.Count > 0)
                {
                    foreach (DataColumn dc in dtModel.Columns)
                    {
                        DataAccessLayer.RisDAL.KodakDALParameter param = oKodak.Parameters.Add("@" + dc.ColumnName, dc.DataType);
                        param.Value = dtModel.Rows[0][dc];
                        parameter += param.ParameterName + "=" + param.Value + " ";
                    }
                }
                else
                {
                    return false;
                }
                oKodak.ExecuteNonQuerySP("USP_PostEvent");
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "Execute SP(USP_PostEvent)" + ex.Message + ", Parameters=" + parameter,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
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
        public bool PostMessage(DataTable dtModel)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string parameter = "";
            try
            {
                if (dtModel != null && dtModel.Rows.Count > 0)
                {
                    foreach (DataColumn dc in dtModel.Columns)
                    {
                        DataAccessLayer.RisDAL.KodakDALParameter param = oKodak.Parameters.Add("@" + dc.ColumnName, dc.DataType);
                        param.Value = dtModel.Rows[0][dc];
                        parameter += param.ParameterName + "=" + param.Value + " ";
                    }
                }
                else
                {
                    return false;
                }
                oKodak.ExecuteNonQuerySP("USP_PostMessage");
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.Register_DA,
                    ModuleInstanceName.CommonModule,
                    53,
                    "Execute SP(USP_PostMessage)" + ex.Message + ", Parameters=" + parameter,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
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

        private string convertStringForSQL(object obj)
        {
            try
            {
                return System.Convert.ToString(obj).Replace("'", "''");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("convertStringForDB, " + ex.Message);
            }

            return string.Empty;
        }

        #region DigitalSign

        public virtual void SaveSignedHistory(SignHistoryModel model)
        {
            string strSqlInsert = string.Empty;
            string strSqlDeleteOldOne = string.Empty;
            string strSqlUpdateReport = string.Empty;
            string signField = string.Empty;
            string signTimeStampField = string.Empty;
            //if (model.Action == SignAction.ApproveReport || model.Action == SignAction.Login)
            //{
            strSqlDeleteOldOne = string.Format("delete from tSignedHistory where reportguid = '{0}' and action ='{1}' and creater ='{2}'", model.ReportGuid, model.Action, model.Creater);

            strSqlInsert = "Insert into tSignedHistory(SignGuid,Action,Creater,IsSigned,OrderGuid,ReportGuid,"
                        + "CertSN,RawData,SignedData,SignedTimestamp,CreateDt,Comments,Domain,PatientID,LocalName,"
                        + "AccNo,CheckingItem,IsPositive,ClinicNo,WYSText,WYGText,ExamDt ) Values(@SignGuid,@Action,@Creater,@IsSigned,"
                        + "@OrderGuid,@ReportGuid,@CertSN,@RawData,@SignedData,@SignedTimestamp,@CreateDt,@Comments,@Domain,"
                        + "@PatientID,@LocalName,@AccNo,@CheckingItem,@IsPositive,@ClinicNo,@WYSText,@WYGText,@ExamDt)";                        

            switch (model.Action.ToLower())
            {
                case "submitreport":
                    signField = "submittersign";
                    signTimeStampField = "submittersigntimestamp";
                    break;
                case "approvereport":
                    signField = "firstapproversign";
                    signTimeStampField = "firstapproversigntimestamp";
                    break;
                case "secondapprovereport":
                    signField = "secondapproversign";
                    signTimeStampField = "secondapproversigntimestamp";
                    break;
                default:
                    break;
                
            }
            strSqlUpdateReport = string.Format("update treport set {0} = @{0},{1} = @{1} where reportguid ='{2}'", signField, signTimeStampField, model.ReportGuid);
            //}
            //else
            //{
            //    strSql = "If exists (select 1 from tSignedHistory where Creater=@Creater and OrderGuid=@OrderGuid"
            //        + " and Action =@Action) "
            //        + "  Update tSignedHistory set RawData=@RawData,SignedData =@SignedData,SignedTimestamp=@SignedTimestamp,"
            //        + " CertID =@CertID,Issigned = @IsSigned  where Creater=@Creater and OrderGuid=@OrderGuid and Action =@Action"
            //        + " Else "
            //        + " Insert into tSignedHistory(SignGuid,Action,Creater,IsSigned,OrderGuid,ReportGuid,"
            //                + " CertID,RawData,SignedData,SignedTimestamp,CreateDt,Domain ) Values(@SignGuid,@Action,@Creater,@IsSigned,"
            //                + " @OrderGuid,@ReportGuid,@CertID,@RawData,@SignedData,@SignedTimestamp,@CreateDt,@Domain)";
            //}

            using (RisDAL okodak = new RisDAL())
            {
                try
                {
                    okodak.BeginTransaction();
                    //only first and second approve need always insert,others keep one record.
                    if (!model.Action.Equals("approvereport", StringComparison.OrdinalIgnoreCase) && !model.Action.Equals("secondapprovereport", StringComparison.OrdinalIgnoreCase))
                    {
                        okodak.ExecuteNonQuery(strSqlDeleteOldOne, RisDAL.ConnectionState.KeepOpen);
                    }

                    okodak.Parameters.Add("@SignGuid", Guid.NewGuid().ToString());
                    okodak.Parameters.Add("@Action", model.Action);
                    okodak.Parameters.Add("@Creater", model.Creater);
                    okodak.Parameters.AddInt("@IsSigned", model.IsSigned);
                    okodak.Parameters.Add("@CertSN", model.CertSN);
                    okodak.Parameters.Add("@RawData", model.RawData);
                    okodak.Parameters.Add("@SignedData", model.SignedData);
                    okodak.Parameters.Add("@SignedTimestamp", model.SignedTimestamp);
                    okodak.Parameters.AddDateTime("@CreateDt", DateTime.Now);
                    okodak.Parameters.Add("@OrderGuid", model.OrderGuid);
                    okodak.Parameters.Add("@ReportGuid", model.ReportGuid);
                    okodak.Parameters.Add("@Comments", model.Comments);
                    okodak.Parameters.Add("@PatientID", model.PatientID);
                    okodak.Parameters.Add("@LocalName", model.LocalName);
                    okodak.Parameters.Add("@AccNo", model.AccNo);
                    okodak.Parameters.Add("@ClinicNo", model.ClinicNo);
                    okodak.Parameters.Add("@IsPositive", model.IsPositive);
                    okodak.Parameters.Add("@WYSText", model.WYSText);
                    okodak.Parameters.Add("@WYGText", model.WYGText);
                    okodak.Parameters.Add("@CheckingItem", model.CheckingItem);
                    okodak.Parameters.Add("@ExamDt", model.ExamDt);
                    okodak.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());

                    okodak.ExecuteNonQuery(strSqlInsert, RisDAL.ConnectionState.KeepOpen);

                    okodak.Parameters.Clear();
                    okodak.Parameters.Add("@" + signField, model.SignedData);
                    okodak.Parameters.Add("@" + signTimeStampField, model.SignedTimestamp);
                    okodak.ExecuteNonQuery(strSqlUpdateReport, RisDAL.ConnectionState.KeepOpen);
                    okodak.CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (okodak != null)
                    {
                        okodak.RollbackTransaction();
                    }
                    logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                        Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                }
            }
        }

        public virtual void SaveSignedData(SignHistoryModel model)
        {
            string strSqlApprovedSign = "select signguid from tsignedhistory where reportguid ='{0}' and action ='{1}' order by createdt desc";
            string strSql = string.Format("Update tSignedHistory set SignedData =@SignedData,SignedTimestamp =@SignedTimestamp,IsSigned ={0},CertSN = @CertSN where SignGuid =@SignGuid",model.IsSigned);
            string strSqlUpdateReport = string.Empty;
            string signField = string.Empty;
            string signTimeStampField = string.Empty;
            bool isSignedOk = false;
            switch (model.Action.ToLower())
            {
                case "submitreport":
                    signField = "submittersign";
                    signTimeStampField = "submittersigntimestamp";
                    break;
                case "approvereport":
                    signField = "firstapproversign";
                    signTimeStampField = "firstapproversigntimestamp";
                    break;
                case "secondapprovereport":
                    signField = "secondapproversign";
                    signTimeStampField = "secondapproversigntimestamp";
                    break;
                default:
                    break;
            }
            strSqlUpdateReport = string.Format("update treport set {0} = @{0},{1} = @{1} where reportguid ='{2}'", signField, signTimeStampField, model.ReportGuid);
            using (RisDAL okodak = new RisDAL())
            {
                try
                {
                    okodak.BeginTransaction();
                    DataTable dt = new DataTable();
                    if (model.Action.Equals("submitreport", StringComparison.OrdinalIgnoreCase))
                    {
                        //update report's sign info
                        okodak.Parameters.Clear();
                        okodak.Parameters.Add("@" + signField, model.SignedData);
                        okodak.Parameters.Add("@" + signTimeStampField, model.SignedTimestamp);
                        okodak.ExecuteNonQuery(strSqlUpdateReport, RisDAL.ConnectionState.KeepOpen);
                        //signedhistory
                        okodak.Parameters.Add("@SignGuid", model.SignGuid);
                        okodak.Parameters.Add("@SignedData", model.SignedData);
                        okodak.Parameters.Add("@SignedTimestamp", model.SignedTimestamp);
                        okodak.Parameters.Add("@CertSN", model.CertSN);
                        okodak.ExecuteNonQuery(strSql, RisDAL.ConnectionState.KeepOpen);
                    }
                    else//first/secondapprove report
                    {
                        dt = okodak.ExecuteQuery(string.Format(strSqlApprovedSign, model.ReportGuid,model.Action), RisDAL.ConnectionState.KeepOpen);
                        if (dt != null && dt.Rows.Count >0)//has approved record(s)
                        {
                            string latestSignGuid = Convert.ToString(dt.Rows[0]["signguid"]);
                            if (latestSignGuid.Equals(model.SignGuid, StringComparison.OrdinalIgnoreCase))//latest signguid same with requested sign guid
                            {
                                //update report's sign info
                                okodak.Parameters.Clear();
                                okodak.Parameters.Add("@" + signField, model.SignedData);
                                okodak.Parameters.Add("@" + signTimeStampField, model.SignedTimestamp);
                                okodak.ExecuteNonQuery(strSqlUpdateReport, RisDAL.ConnectionState.KeepOpen);
                            }
                            //signedhistory
                            okodak.Parameters.Add("@SignGuid", model.SignGuid);
                            okodak.Parameters.Add("@SignedData", model.SignedData);
                            okodak.Parameters.Add("@SignedTimestamp", model.SignedTimestamp);
                            okodak.Parameters.Add("@CertSN", model.CertSN);
                            okodak.ExecuteNonQuery(strSql, RisDAL.ConnectionState.KeepOpen);
                            isSignedOk = model.IsSigned == 1;
                        }
                    }
                    okodak.CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (okodak != null)
                    {
                        okodak.RollbackTransaction();
                    }
                    logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                        Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                }
            }

            if (isSignedOk)
            {
                ReSignSendToGateWay(model);
            }
        }

        public virtual DataTable GetLatestSignedData(string reportGuid)
        {
            string sqlGetLastSignedData = string.Format("select top 1 * from tSignedHistory where reportguid = '{0}' order by createdt desc",reportGuid);
            DataTable dt = new DataTable();
            using (RisDAL okodak = new RisDAL())
            {
                okodak.ExecuteQuery(sqlGetLastSignedData, dt);
            }
            return dt;
        }

        public virtual DataTable GetLatestEveryActionSignedData(string reportGuid)
        {
            string sqlGetLastSignedData = string.Format(@"          select * from  ( 
            select * from (select top 1 a.*,dbo.translateUser(a.Creater) as Creater__DESC,dbo.translateDictionaryValue(170,a.Action) as Action__DESC,b.SignPic from tSignedHistory a left join tusercerts b on a.CertSN = b.CertSN  where reportguid = '{0}'  and Action ='SubmitReport' order by CreateDt desc) t1 where t1.IsSigned = 1
                        union
            select * from (select top 1 a.*,dbo.translateUser(a.Creater) as Creater__DESC,dbo.translateDictionaryValue(170,a.Action) as Action__DESC,b.SignPic from tSignedHistory a left join tusercerts b on a.CertSN = b.CertSN  where reportguid = '{0}'  and Action ='ApproveReport' order by CreateDt desc) t2 where t2.IsSigned = 1
                        union
            select * from (select top 1 a.*,dbo.translateUser(a.Creater) as Creater__DESC,dbo.translateDictionaryValue(170,a.Action) as Action__DESC,b.SignPic from tSignedHistory a left join tusercerts b on a.CertSN = b.CertSN  where reportguid = '{0}'  and Action ='SecondApproveReport' order by CreateDt desc) t3 where t3.IsSigned = 1 
                     ) t4 order by createdt asc", reportGuid);
            DataTable dt = new DataTable("tSignHistory");
            using (RisDAL okodak = new RisDAL())
            {
                okodak.ExecuteQuery(sqlGetLastSignedData, dt);
            }
            return dt;
        }

        private void ReSignSendToGateWay(SignHistoryModel signModel)
        {
            RisDAL dal = null;
            try
            {
                string sendToGw = GetSystemProfileValue("SendToGateServer", "0E00");
                if (!sendToGw.Equals("1") || signModel == null || string.IsNullOrEmpty(signModel.OrderGuid) || string.IsNullOrEmpty(signModel.ReportGuid))
                    return;
                // Need to send gateway
                //prepare data
                string orderGuid = signModel.OrderGuid;
                string reportGuid = signModel.ReportGuid;
                string patientGuid = "";
                string sqlGetPatientInfo = "select * from tpatientlist where patientguid ='{0}'";
                string sqlGetOrderInfo = "select * from tregorder with (nolock) where orderguid = '{0}'";
                string sqlGetReportInfo = "select techinfo,status,dbo.translateUser(creater) as creatername,createdt,dbo.translateUser(firstapprover) as firstapprovername,wystext,wygtext from treport with (nolock) where reportguid ='{0}'";
                string sqlGetRpInfo = "select operationstep,ischarge,charge,rpdesc,bodypart,status,procedurecode,Registrar,RegisterDt,ExamineDt,ModalityType,Modality,dbo.translateUser(technician) as technicianname from tregprocedure with (nolock) where reportguid ='{0}'";
                DataTable dtPatient = new DataTable();
                DataTable dtOrder = new DataTable();
                DataTable dtReport = new DataTable();
                DataTable dtRP = new DataTable();
                dal = new RisDAL();
                StringBuilder sb = new StringBuilder();
                List<string> lstSQL = new List<string>();
                dal.ExecuteQuery(string.Format(sqlGetOrderInfo, orderGuid), RisDAL.ConnectionState.KeepOpen, dtOrder);
                dal.ExecuteQuery(string.Format(sqlGetReportInfo, reportGuid), RisDAL.ConnectionState.KeepOpen, dtReport);
                dal.ExecuteQuery(string.Format(sqlGetRpInfo, reportGuid), RisDAL.ConnectionState.KeepOpen, dtRP);

                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {
                    patientGuid = Convert.ToString(dtOrder.Rows[0]["PatientGuid"]);
                    dal.ExecuteQuery(string.Format(sqlGetPatientInfo,patientGuid), RisDAL.ConnectionState.KeepOpen, dtPatient);
                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                    {
                        sb.Append("insert into gw_dataindex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)");
                        sb.Append("values(newid(),getdate(),'48','ReportGuid','Local')");
                        lstSQL.Add(sb.ToString());
                        sb.Clear();

                        sb.Append(@"insert GW_Patient(DATA_ID,DATA_DT,PATIENTID,OTHER_PID,PATIENT_NAME,PATIENT_LOCAL_NAME,
                                BIRTHDATE,SEX,PATIENT_ALIAS,ADDRESS,PHONENUMBER_HOME,MARITAL_STATUS,PATIENT_TYPE,
                                PATIENT_LOCATION,VISIT_NUMBER,BED_NUMBER,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3,CUSTOMER_4,SSN_NUMBER,DRIVERLIC_NUMBER)");
                        sb.AppendFormat(@"values(newid(),getdate(),'{0}','{1}',N'{2}',N'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',
                                    'N{14}','{15}','{16}','{17}','{18}','{19}')",
                                        Convert.ToString(dtPatient.Rows[0]["PatientID"]), Convert.ToString(dtPatient.Rows[0]["RemotePID"]), Convert.ToString(dtPatient.Rows[0]["EnglishName"]),
                                        Convert.ToString(dtPatient.Rows[0]["LocalName"]), Convert.ToString(dtPatient.Rows[0]["Birthday"]), Convert.ToString(dtPatient.Rows[0]["Gender"]),
                                        Convert.ToString(dtPatient.Rows[0]["Alias"]), Convert.ToString(dtPatient.Rows[0]["Address"]), Convert.ToString(dtPatient.Rows[0]["Telephone"]),
                                        Convert.ToString(dtPatient.Rows[0]["Marriage"]), Convert.ToString(dtOrder.Rows[0]["PatientType"]), Convert.ToString(dtOrder.Rows[0]["InhospitalRegion"]),
                                        Convert.ToString(dtOrder.Rows[0]["ClinicNo"]), Convert.ToString(dtOrder.Rows[0]["BedNo"]), Convert.ToString(dtPatient.Rows[0]["EnglishName"]),
                                        Convert.ToString(dtPatient.Rows[0]["IsVIP"]), Convert.ToString(dtOrder.Rows[0]["InhospitalNo"]), getSQLString(Convert.ToString(dtPatient.Rows[0]["Comments"])),
                                        Convert.ToString(dtPatient.Rows[0]["MedicareNo"]), Convert.ToString(dtPatient.Rows[0]["ReferenceNo"]));
                        lstSQL.Add(sb.ToString());
                        sb.Clear();

                        sb.Append(@"insert GW_Order(DATA_ID,DATA_DT,ORDER_NO,PLACER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS,
                             PLACER_DEPARTMENT, PLACER, FILLER_DEPARTMENT, FILLER, REF_PHYSICIAN, REQUEST_REASON,
                             REUQEST_COMMENTS, EXAM_REQUIREMENT, SCHEDULED_DT, MODALITY, STATION_NAME, EXAM_LOCATION,
                             EXAM_DT, DURATION, TECHNICIAN, BODY_PART, PROCEDURE_CODE, PROCEDURE_DESC, EXAM_COMMENT,
                             CHARGE_STATUS, CHARGE_AMOUNT,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3)");
                        sb.AppendFormat(@"values(newid(),getdate(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',
                                    '{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','28')",
                                        Convert.ToString(dtOrder.Rows[0]["OrderGuid"]), Convert.ToString(dtOrder.Rows[0]["RemoteAccNo"]), Convert.ToString(dtOrder.Rows[0]["AccNo"]),
                                        Convert.ToString(dtPatient.Rows[0]["PatientID"]), Convert.ToString(dtRP.Rows[0]["Status"]), Convert.ToString(dtOrder.Rows[0]["ApplyDept"]),
                                        Convert.ToString(dtOrder.Rows[0]["ApplyDoctor"]), Convert.ToString(dtOrder.Rows[0]["ApplyDept"]), Convert.ToString(dtOrder.Rows[0]["ApplyDoctor"]),
                                        Convert.ToString(dtOrder.Rows[0]["ApplyDoctor"]), getSQLString(Convert.ToString(dtOrder.Rows[0]["Observation"])), Convert.ToString(dtOrder.Rows[0]["visitcomment"]),
                                        Convert.ToString(dtOrder.Rows[0]["Comments"]), Convert.ToString(dtRP.Rows[0]["RegisterDt"]), Convert.ToString(dtRP.Rows[0]["ModalityType"]),
                                        Convert.ToString(dtRP.Rows[0]["Modality"]),"", Convert.ToString(dtRP.Rows[0]["ExamineDt"]), getDuration(dtRP.Rows[0]),
                                        Convert.ToString(dtRP.Rows[0]["technicianname"]), Convert.ToString(dtRP.Rows[0]["bodypart"]), Convert.ToString(dtRP.Rows[0]["procedurecode"]),
                                        getSQLString(Convert.ToString(dtRP.Rows[0]["rpdesc"])),getSQLString(Convert.ToString(dtOrder.Rows[0]["comments"])), Convert.ToString(dtRP.Rows[0]["ischarge"]) == "1" ? "Y" : "N",
                                        Convert.ToString(dtRP.Rows[0]["charge"]), Convert.ToString(dtOrder.Rows[0]["CardNo"]), Convert.ToString(dtOrder.Rows[0]["HisID"]), 
                                        Convert.ToString(dtPatient.Rows[0]["MedicareNo"]));
                        lstSQL.Add(sb.ToString());
                        sb.Clear();

                        sb.Append("insert GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY,");
                        sb.Append(" REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD,DIAGNOSE,COMMENTS,CUSTOMER_4,CUSTOMER_1)");
                        sb.AppendFormat(@"values(newid(),getdate(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                            reportGuid, Convert.ToString(dtOrder.Rows[0]["AccNo"]), Convert.ToString(dtPatient.Rows[0]["PatientID"]),
                            Convert.ToString(dtReport.Rows[0]["Status"]), Convert.ToString(dtRP.Rows[0]["ModalityType"]), '0',
                            "",Convert.ToString(dtReport.Rows[0]["CreaterName"]),Convert.ToString(dtReport.Rows[0]["FirstApproverName"]),
                            Convert.ToString(dtReport.Rows[0]["createdt"]),Convert.ToString(dtRP.Rows[0]["OperationStep"]), getSQLString(Convert.ToString(dtReport.Rows[0]["wystext"])),
                            getSQLString(Convert.ToString(dtReport.Rows[0]["wygtext"])), getSQLString(Convert.ToString(dtReport.Rows[0]["techinfo"])),signModel.IsSigned == 1 ? "Y" : "N"
                            );
                        lstSQL.Add(sb.ToString());

                        dal.BeginTransaction();
                        foreach (string strSQL in lstSQL)
                        {
                            dal.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                        }
                        dal.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }

        private string getDuration(DataRow drRP)
        {
            DateTime dtExam = Convert.ToDateTime(drRP["ExamineDt"]);
            DateTime dtReg = Convert.ToDateTime(drRP["RegisterDt"]);
            return Convert.ToString(dtExam - dtReg);
        }

        /// <summary>
        /// convert Single quote to double Single quotes for SQL sentence
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string getSQLString(string src)
        {
            if (!string.IsNullOrEmpty(src))
            {
                return src.Replace("'", "''");
            }

            return string.Empty;
        }

        #endregion

        public virtual string GetUserTopLevleRole(string loginName)
        {
            string strSql = "select top 1 tRole2User.RoleName from tUser "
            + " inner join tRole2user on tUser.UserGuid =tRole2User.UserGuid"
            + " left join tRoleProfile on tRoleProfile.RoleName = tRole2User.RoleName and tRoleProfile.Name ='RoleLevel'"
            + " where tUser.LoginName =@loginName order by tRoleProfile.Value desc ";

            using (RisDAL okodak = new RisDAL())
            {
                okodak.Parameters.Add("@loginName", loginName);
                object obj = okodak.ExecuteScalar(strSql);

                return Convert.ToString(obj);
            }
        }

        public virtual DataTable GetUserAllRoles(string loginName)
        {
            string strSql = "select tRole2User.RoleName from tUser "
            + " inner join tRole2user on tUser.UserGuid =tRole2User.UserGuid"
            + " left join tRoleProfile on tRoleProfile.RoleName = tRole2User.RoleName and tRoleProfile.Name ='RoleLevel'"
            + " where tUser.LoginName =@loginName order by tRoleProfile.Value desc ";

            using (RisDAL okodak = new RisDAL())
            {
                okodak.Parameters.Add("@loginName", loginName);
                var dt = okodak.ExecuteQuery(strSql);

                return dt;
            }
        }

        public virtual bool IsReferralAndReadOnly(string orderGuid)
        {
            string currentSite = CommonGlobalSettings.Utilities.GetCurSite();
            string sql = @"select 1 from tregorder,tRegProcedure  where tregorder.OrderGuid = @OrderGuid and IsReferral >0 and 
                ( CurrentSite != @CurrentSite or (CurrentSite = @CurrentSite and Status = -1)) and tRegOrder.OrderGuid = tRegProcedure.OrderGuid and tRegOrder.OrderGuid = @OrderGuid ";
            using (RisDAL okodak = new RisDAL())
            {
                okodak.Parameters.Add("@OrderGuid", orderGuid);
                okodak.Parameters.Add("@CurrentSite", currentSite);
                object obj = okodak.ExecuteScalar(string.Format(sql,orderGuid));
                return string.IsNullOrEmpty(Convert.ToString(obj)) ? false : true;
            }
        }

        public virtual bool GetRoleBySite(string strSite, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("@site", strSite, 128);
                oKodak.ExecuteQuerySP("usp_getrolebysite", dt);
                dt.TableName = "role";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        public virtual DataTable GetClientProfile(string clientID)
        {
            string strSql = " select isNull(cp.value, cd.value) Value,isNull(cp.Domain, cd.Domain) Domain"
                + ",cd.ConfigName,cd.ModuleID,Exportable,PropertyDesc,PropertyOptions,Inheritance"
                + ",PropertyType,IsHidden,OrderingPos,Type,ShowInWeb,ClientProfileGuid,ClientID"
                + ",(select top 1 Title from tModule WHERE ModuleID=cd.ModuleID) ModuleName"
                + " FROM tConfigDic cd left join tClientProfile cp"
                + "   on cp.ModuleID=cd.ModuleID AND cp.ConfigName=cd.ConfigName";

            if (!string.IsNullOrWhiteSpace(clientID))
            {
                strSql += " AND cp.ClientID=@ClientID";
            }
            else
            {
                strSql += " WHERE type=1 "
                    + " UNION "
                    + " select Value, Domain "
                    + " , ConfigName, ModuleID, Exportable, PropertyDesc, PropertyOptions, Inheritance "
                    + " , PropertyType, IsHidden, OrderingPos, Type, ShowInWeb, '' ClientProfileGuid, '' ClientID "
                    + " , (select top 1 Title from tModule WHERE ModuleID=cd.ModuleID) ModuleName "
                    + "  FROM tConfigDic cd ";
            }

            strSql += " WHERE type=1 ORDER BY OrderingPos ";

            using (RisDAL okodak = new RisDAL())
            {
                okodak.Parameters.Add("@ClientID", clientID);

                DataTable dt = okodak.ExecuteQuery(strSql);

                execPropertyOptionsSQL(dt, okodak);

                return dt;
            }
        }

        public virtual bool SaveClientProfile(DataTable dtClientProfiles)
        {
            if (dtClientProfiles != null && dtClientProfiles.Rows.Count > 0)
            {
                string domain = CommonGlobalSettings.Utilities.GetCurDomain();

                string clientID = Convert.ToString(dtClientProfiles.Rows[0]["ClientID"]);

                if (!string.IsNullOrWhiteSpace(clientID))
                #region add or update my profile
                {
                    string sql = "";

                    int i = 0;

                    foreach (DataRow row in dtClientProfiles.Rows)
                    {
                        sql += " if NOT EXISTS(select 1 from tClientProfile where ModuleID=@ModuleID" + i + " AND ConfigName=@ConfigName" + i + " AND ClientID=@ClientID AND Domain=@Domain) "
                            + " insert into tClientProfile(ClientProfileGuid, ModuleID, ConfigName, ClientID, Value, Domain) Values"
                            + " (NEWID(), @ModuleID" + i + ", @ConfigName" + i + ", @ClientID, @Value" + i + ", @Domain) "
                            + " ELSE "
                            + " Update tClientProfile SET Value=@Value" + i + " WHERE ModuleID=@ModuleID" + i + " AND ConfigName=@ConfigName" + i + " AND ClientID=@ClientID AND Domain=@Domain "
                            ;

                        ++i;
                    }

                    using (RisDAL okodak = new RisDAL())
                    {
                        okodak.Parameters.Add("@ClientID", clientID);
                        okodak.Parameters.Add("@Domain", domain);

                        i = 0;

                        foreach (DataRow row in dtClientProfiles.Rows)
                        {
                            string moduleID = Convert.ToString(row["ModuleID"]);
                            string configName = Convert.ToString(row["ConfigName"]);
                            string val = Convert.ToString(row["Value"]);

                            okodak.Parameters.Add("@ModuleID" + i, moduleID);
                            okodak.Parameters.Add("@ConfigName" + i, configName);
                            okodak.Parameters.Add("@Value" + i, val);

                            ++i;
                        }

                        sql = sql.Trim(",".ToCharArray());

                        okodak.ExecuteNonQuery(sql);

                        return true;
                    }
                }
                #endregion
                else
                #region update the default value
                {
                    string sql = "";

                    int i = 0;

                    foreach (DataRow dr in dtClientProfiles.Rows)
                    {
                        sql += " update tConfigDic set Value=@Value" + i + " where ConfigName=@ConfigName" + i + " AND ModuleID=@ModuleID" + i + " ";

                        ++i;
                    }

                    i = 0;
                    using (RisDAL okodak = new RisDAL())
                    {
                        foreach (DataRow dr in dtClientProfiles.Rows)
                        {
                            string moduleID = Convert.ToString(dr["ModuleID"]);
                            string configName = Convert.ToString(dr["ConfigName"]);
                            string val = Convert.ToString(dr["Value"]);

                            okodak.Parameters.Add("@ModuleID" + i, moduleID);
                            okodak.Parameters.Add("@ConfigName" + i, configName);
                            okodak.Parameters.Add("@Value" + i, val);

                            ++i;
                        }

                        okodak.ExecuteNonQuery(sql);

                        return true;
                    }
                }
                #endregion
            }

            return false;
        }

        public virtual bool SaveSystemProfile(DataTable dtProfiles)
        {
            if (dtProfiles != null && dtProfiles.Rows.Count > 0)
            {
                string domain = CommonGlobalSettings.Utilities.GetCurDomain();


                string sql = "";

                int i = 0;

                foreach (DataRow row in dtProfiles.Rows)
                {
                    string moduleID = Convert.ToString(row["ModuleID"]);
                    string name = Convert.ToString(row["Name"]);
                    string val = Convert.ToString(row["Value"]);

                    sql += " update tSystemProfile set Value='" + val + "'"
                        + " where ModuleID='" + moduleID + "' AND Name='" + name + "' AND Domain='" + domain + "' ";

                    ++i;
                }

                using (RisDAL okodak = new RisDAL())
                {
                    sql = sql.Trim(",".ToCharArray());

                    okodak.ExecuteNonQuery(sql);

                    return true;
                }
            }

            return false;
        }

        public virtual bool LastReferralStatus(string strReferralID, ref int nLastRefStatus)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = string.Format("select top 1 RefStatus from tReferralList where ReferralID='{0}' order by CreateDt desc", strReferralID);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    nLastRefStatus = -1;
                }
                nLastRefStatus = Convert.ToInt32(obj);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
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

        void execPropertyOptionsSQL(DataTable dt, RisDAL okodak)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string propOptions = Convert.ToString(dr["PropertyOptions"]);

                try
                {
                    if (!string.IsNullOrWhiteSpace(propOptions) && 
                        propOptions.ToLower().IndexOf("select ") >= 0 && 
                        propOptions.ToLower().IndexOf(" from ") >= 0)
                    {
                        DataTable tmp = okodak.ExecuteQuery(propOptions);

                        if (tmp.Columns.Contains("Value") && tmp.Columns.Contains("Text"))
                        {
                            string strPropOptions = "";

                            foreach (DataRow drr in tmp.Rows)
                            {
                                string v = Convert.ToString(drr["Value"]);
                                string t = Convert.ToString(drr["Text"]);

                                if (string.IsNullOrWhiteSpace(t))
                                    t = v;

                                strPropOptions += v + "," + t + "|";
                            }

                            dr["PropertyOptions"] = strPropOptions.Trim(",| ".ToCharArray());
                        }
                        else if (tmp.Columns.Contains("Value"))
                        {
                            string strPropOptions = "";

                            foreach (DataRow drr in tmp.Rows)
                            {
                                string v = Convert.ToString(drr["Value"]);

                                strPropOptions += v + "|";
                            }

                            dr["PropertyOptions"] = strPropOptions.Trim(",| ".ToCharArray());
                        }
                        else if (tmp.Columns.Count > 0)
                        {
                            string strPropOptions = "";

                            foreach (DataRow drr in tmp.Rows)
                            {
                                string v = Convert.ToString(drr[0]);

                                strPropOptions += v + "|";
                            }

                            dr["PropertyOptions"] = strPropOptions.Trim(",| ".ToCharArray());
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public virtual bool RemoveOrderMessageFlag(string param, ref string errorinfo)
        {

            try
            {
                string AccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", param);
                string Type = CommonGlobalSettings.Utilities.GetParameter("Type", param);
             

                using (RisDAL oKodak = new RisDAL())
                {


                    //Insert pathology track record
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddVarChar("AccNo", AccNo);
                    oKodak.Parameters.AddVarChar("Type", Type);
                    oKodak.Parameters.AddVarChar("ErrorMessage", 256, ParameterDirection.Output);


                    oKodak.ExecuteNonQuerySP("usp_remove_ordermessageflag");
                    if (!string.IsNullOrWhiteSpace(oKodak.Parameters["ErrorMessage"].Value.ToString()))
                    {
                        throw new Exception(oKodak.Parameters["ErrorMessage"].Value.ToString());
                    }
                                     

                }

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Teaching_DA, ModuleInstanceName.Teaching, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return true;
        }
        public virtual bool AddOrderMessageFlag(string param, ref string errorinfo)
        {

            try
            {
                string AccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", param);
                string Type = CommonGlobalSettings.Utilities.GetParameter("Type", param);
                string UserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", param);
                string UserName = CommonGlobalSettings.Utilities.GetParameter("UserName", param);
                string Subject = CommonGlobalSettings.Utilities.GetParameter("Subject", param);
                

                using (RisDAL oKodak = new RisDAL())
                {


                    //Set pathology track flag
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddVarChar("AccNo", AccNo);
                    oKodak.Parameters.AddVarChar("Type", Type);
                    oKodak.Parameters.AddVarChar("UserGuid", UserGuid);
                    oKodak.Parameters.AddVarChar("UserName", UserName);
                    oKodak.Parameters.AddVarChar("Subject", Subject);
                    oKodak.Parameters.AddVarChar("Context", "");
                    oKodak.Parameters.AddVarChar("ErrorMessage", 256, ParameterDirection.Output);

                    oKodak.ExecuteNonQuerySP("usp_Insert_OrderMessage");

                    if (!string.IsNullOrWhiteSpace(oKodak.Parameters["ErrorMessage"].Value.ToString()))
                    {
                        throw new Exception(oKodak.Parameters["ErrorMessage"].Value.ToString());
                    }

                }

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Teaching_DA, ModuleInstanceName.Teaching, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return true;
        }
    }
}
