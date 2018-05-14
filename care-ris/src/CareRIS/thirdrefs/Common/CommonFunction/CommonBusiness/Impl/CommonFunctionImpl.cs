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
using Server.DAO.Common;
using LogServer;
using System.Windows.Forms;
using Common.ActionResult;
using CommonGlobalSettings;


namespace Server.Business.Common.Impl
{
    public class CommonFunctionImpl : ICommonFunctionBusiness
    {
        LogManager logger = new LogManager();
        ICommonFunctionDao CommonFunctionDao = DaoInstanceFactory.GetInstance();

        public bool GetModalityType(DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetModalityType(ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetBodyCategory(string strModalityType, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetBodyCategory(strModalityType, ds);

            }
            catch
            {
                bReturn = false;
            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetBodyPart(string strModalityType, string strBodyCategory, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetBodyPart(strModalityType, strBodyCategory, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetCheckingItem(string strModalityType, string strBodyCategory, string strBodyPart, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetCheckingItem(strModalityType, strBodyCategory, strBodyPart, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetExamSystem(string strModalityType, string strBodyPart, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetExamSystem(strModalityType, strBodyPart, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;

        }


        public bool GetModality(string strModalityType, DataSet ds, string site, string withPublic)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetModality(strModalityType, ds, site, withPublic);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;

        }


        public bool GetRoleBySite(string strSite, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetRoleBySite(strSite, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;

        }

        /// <summary>
        /// Load  Shortcuts  
        /// </summary>
        /// <param name="category"></param>
        /// <param name="strUserID"></param>
        /// <param name="reDataSet"></param>
        /// <returns></returns>
        public bool LoadShortcut(int category, string strUserID, ref DataSet reDataSet)
        {

            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.LoadShortcut(category, strUserID, ref reDataSet);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;

        }

        /// <summary>
        /// Get shortcut name
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public string GetShorcutName(string strGuid)
        {
            string result = null;
            try
            {
                result = CommonFunctionDao.GetShorcutName(strGuid);
            }
            catch
            {
                return result;
            }
            finally
            {


            }
            return result;
        }
        /// <summary>
        /// Add a Shortcut
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <param name="strUserID"></param>
        /// <param name="reDataSet"></param>
        /// <returns></returns>
        public int AddShortcut(int type, int category, string strName, string strValue, string strUserID, ref DataSet reDataSet)
        {
            int bReturn = 1;

            try
            {
                bReturn = CommonFunctionDao.AddShortcut(type, category, strName, strValue, strUserID, ref reDataSet);

            }
            catch
            {
                bReturn = 0;

            }
            finally
            {


            }
            return bReturn;
        }
        /// <summary>
        /// Edit a Shortcut
        /// </summary>
        /// <param name="strGuid"></param>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <param name="strUserID"></param>
        /// <param name="reDataSet"></param>
        /// <returns></returns>
        public int EditShortcut(string strGuid, int type, int category, string strName, string strValue, string strUserID, string strCurUser, string strManageSS, ref DataSet reDataSet)
        {

            int bReturn = 1;

            try
            {
                bReturn = CommonFunctionDao.EditShortcut(strGuid, type, category, strName, strValue, strUserID, strCurUser, strManageSS, ref reDataSet);

            }
            catch (Exception ex)
            {
                bReturn = 0;

            }
            finally
            {


            }
            return bReturn;
        }
        /// <summary>
        /// Delete a Shortcut
        /// </summary>
        /// <param name="strGuid"></param>
        /// <param name="category"></param>
        /// <param name="strUserID"></param>
        /// <param name="reDataSet"></param>
        /// <returns></returns>
        public bool DeleteShortcut(string strGuid, int category, string strUserID, string strSite, ref DataSet reDataSet)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.DeleteShortcut(strGuid, category, strUserID, strSite, ref reDataSet);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetStaff(string strDegreeName, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                DataTable dt;

                bReturn = CommonFunctionDao.GetStaff(strDegreeName, ds);

            }
            catch
            {
                bReturn = false;
            }
            finally
            {


            }
            return bReturn;
        }
        public string GetLocalName(string strLoginName)
        {
            string result = null;
            try
            {
                result = CommonFunctionDao.GetLocalName(strLoginName);
            }
            catch
            {
                return result;
            }
            finally
            {

            }
            return result;
        }

        public string GetLocalNameByUserGuid(string userGuid)
        {
            string result = null;
            try
            {
                result = CommonFunctionDao.GetLocalNameByUserGuid(userGuid);
            }
            catch
            {
                return result;
            }
            finally
            {

            }
            return result;
        }

        public bool GetProcedureDefaultValue(string strModalityType, string strBodyCategory, string strBodyPart, string strCheckingItem, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetProcedureDefaultValue(strModalityType, strBodyCategory, strBodyPart, strCheckingItem, ds);

            }
            catch
            {
                bReturn = false;
            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetProcedureCode(string strProcedureCode, string strShortcutCode, DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetProcedureCode(strProcedureCode, strShortcutCode, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }

        public bool ReclaimID(int nTag, string strValue, int nLength)
        {
            bool bReturn = true;

            try
            {
                string strTemp = strValue.Substring(strValue.Length - nLength, nLength);
                strTemp = strTemp.Trim();
                if (strTemp.Length == 0)
                {
                    throw new Exception("Param error");
                }
                int nValue = Convert.ToInt32(strTemp);
                bReturn = CommonFunctionDao.ReclaimID(nTag, nValue.ToString());

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetExtOptional(string strTableName, DataSet ds)
        {
            bool bReturn = true;

            try
            {

                strTableName = strTableName.Trim();
                if (strTableName.Length == 0)
                {
                    throw new Exception("Param error");
                }

                bReturn = CommonFunctionDao.GetExtOptional(strTableName, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }
        public bool GetRequisition(string strAccNo, string strDataCenter, string strDomain, DataSet ds)
        {

            bool bReturn = true;

            try
            {

                bReturn = CommonFunctionDao.GetRequisition(strAccNo, strDataCenter, strDomain, ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }

        public bool GetRequisition(bool loadArchive, string strAccNo, string strDataCenter, string strDomain, DataSet ds)
        {

            bool bReturn = true;

            try
            {
                if (loadArchive)
                {
                    bReturn = CommonFunctionDao.GetRequisition(loadArchive, strAccNo, strDataCenter, strDomain, ds);
                }
                else
                {
                    bReturn = CommonFunctionDao.GetRequisition(strAccNo, strDataCenter, strDomain, ds);
                }

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }

        public string GetFieldValue(string selectTable, string selectField, string uniqueIDField, string uniqueIDFieldValue)
        {
            return CommonFunctionDao.GetFieldValue(selectTable, selectField, uniqueIDField, uniqueIDFieldValue);
        }

        public FtpModel GetFtpParameters()
        {
            return CommonFunctionDao.GetFtpParameters();
        }

        public DataTable GetBackupFileList(string stTables, bool bFailRetry, string stRetryFlag)
        {
            return CommonFunctionDao.GetBackupFileList(stTables, bFailRetry, stRetryFlag);
        }
        /// <summary>
        /// Used in Backup module for updating the Backupmark in table  tRequisition
        /// </summary>
        /// <param name="stRequGUID"> the GUID of requisition record that has been backuped</param>
        public void UpdateDatabaseMark(bool bSuccess, string stUnicGUID, string stComment, string stTableName)
        {
            CommonFunctionDao.UpdateDatabaseMark(bSuccess, stUnicGUID, stComment, stTableName);
        }

        public bool CheckClinic(string roleName)
        {
            return CommonFunctionDao.CheckClinic(roleName);
        }
        public bool GetAllProcedureCode(DataSet ds)
        {

            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetAllProcedureCode(ds))
                {
                    throw new Exception("Get procedurecode error");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }

        public bool GetDateTypeByCalendar(string bookingDate, string modality, ref string dateType, ref string availableDate)
        {

            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetDateTypeByCalendar(bookingDate,  modality, ref  dateType, ref availableDate))
                {
                    throw new Exception("Get DateTypeByCalendar error");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }
        public bool GetCurSiteProcedureCode(DataSet ds)
        {

            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetCurSiteProcedureCode(ds))
                {
                    throw new Exception("Get procedurecode error");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }
        public bool GetProcedureCodeBySite(string strSite,DataSet ds)
        {

            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetProcedureCodeBySite(strSite,ds))
                {
                    throw new Exception("Get procedurecode error");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }

        public bool UpdateCertificate(string strUserGuid, string strIkeySn, string strPrivateKey, string strPublicKey, string strOrgOwner, DataSet ds)
        {

            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.UpdateCertificate(strUserGuid, strIkeySn, strPrivateKey, strPublicKey, strOrgOwner, ds))
                {
                    throw new Exception("Update certificate failure");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }
        public bool GetCertificate(string strUserGuid, string strIkeySn, DataSet ds)
        {

            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetCertificate(strUserGuid, strIkeySn, ds))
                {
                    throw new Exception("Get certificate failure");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }

        public bool GetProfileInfo(string strRoleName, DataSet ds)
        {
            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetProfileInfo(strRoleName, ds))
                {
                    throw new Exception("Get profile failure");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }

        public bool SetCertificatePassword(string strCertificatePassword)
        {
            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.SetCertificatePassword(strCertificatePassword))
                {
                    throw new Exception("Set certificate failure");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }
        public bool GetCertificatePassword(ref string strCertificatePassword)
        {
            bool bReturn = true;
            try
            {

                if (!CommonFunctionDao.GetCertificatePassword(ref strCertificatePassword))
                {
                    throw new Exception("Get certificate failure");
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
            }

            return bReturn;
        }



        public DataSet GetConditionColumn(Dictionary<string, object> paramMap)
        {
            return CommonFunctionDao.GetConditionColumn(paramMap);
        }

        public DataSet GetGridColumn(Dictionary<string, object> paramMap)
        {
            return CommonFunctionDao.GetGridColumn(paramMap);
        }

        public DataSet GetUserList()
        {
            return CommonFunctionDao.GetUserList();
        }

        public DataSet GetIMUserList(string type)
        {
            return CommonFunctionDao.GetIMUserList(type);
        }

        public DataSet GetIMLog(string condition)
        {
            return CommonFunctionDao.GetIMLog(condition);
        }

        public DataSet GetConditionRelatedControlData(Dictionary<string, object> paramMap)
        {
            return CommonFunctionDao.GetConditionRelatedControlData(paramMap);
        }

        public int CheckOrderStatus(string strPatientGuid, string strOrderGuid, string strCurUser, string strLockType, BaseActionResult bar)
        {
            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                //Check this order is locked by other of not

                int nRecode = 0;
                if (!CommonFunctionDao.CheckOrderStatus(strPatientGuid, strOrderGuid, strCurUser, strLockType, ref nRecode, ref strError))
                {
                    throw new Exception(strError);
                }
                bar.recode = nRecode;
                bar.ReturnString = strError;

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }


            return bar.recode;

        }

        #region RegIntegration

        public int GeneratePatientIDEx(BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                string strPatientID = "";
                if (!CommonFunctionDao.GeneratePatientIDEx(ref strPatientID, ref strError))
                {
                    throw new Exception(strError);
                }
                bar.ReturnString = strPatientID;

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }


            return bar.recode;

        }

        public int GenerateAccNoEx(BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                string strAccNo = "";
                int i = 1;
                if (!CommonFunctionDao.GenerateAccNoEx(ref strAccNo, ref strError, ref i))
                {
                    throw new Exception(strError);
                }
                bar.ReturnString = string.Format("AccNo={0}&Policy={1}", strAccNo, i.ToString());

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }


            return bar.recode;

        }

        public int QueryPatientEx(string strKey, string strValue, bool bIsVIP, DataSetActionResult dsar)
        {
            dsar.DataSetData = new DataSet();
            string strError = "";
            dsar.recode = 0;
            dsar.Result = true;
            DataTable dtPatient = new DataTable();
            dtPatient.TableName = "Patient";
            try
            {
                string strAccNo = "";
                if (!CommonFunctionDao.LoadPatient(strKey, strValue, dtPatient, ref strError))
                {
                    throw new Exception(strError);
                }
                dsar.DataSetData.Tables.Add(dtPatient);

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;


                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }


            return dsar.recode;

        }

        public int RegIntegrationSavePatient(PatientModel pModel, BaseActionResult bar)
        {
            bar.recode = 0;
            bar.Result = true;
            string szError = "";
            try
            {
                if (!CommonFunctionDao.RegIntegrationSavePatient(pModel, ref szError))
                {
                    bar.ReturnMessage = szError;
                    throw new Exception(szError);
                }
            }
            catch (System.Exception ex)
            {
                bar.Result = false;
                bar.recode = -1;
                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }

            return bar.recode;
        }

        public int LocatePatientByHISInfo(string strLocalPID, string HISID, string strPatientName, DataSetActionResult dsar)
        {
            dsar.DataSetData = new DataSet();
            string strError = "";
            dsar.recode = 0;
            dsar.Result = true;
            DataTable dtPatient = new DataTable();
            try
            {
                string strAccNo = "";
                if (!CommonFunctionDao.LocatePatientByHISInfo(strLocalPID, HISID, strPatientName, ref dtPatient, ref strError))
                {
                    dsar.ReturnMessage = strError;
                    dsar.recode = -1;
                    dsar.Result = false;
                }
                else
                {
                    dsar.DataSetData.Tables.Add(dtPatient);
                }

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;


                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));

            }


            return dsar.recode;
        }

        #endregion

        public DataSet GetDictionaryValue(string tag)
        {
            return CommonFunctionDao.GetDictionaryValue(tag);
        }

        public int GetRequisitionType(string strAccNo, DataSet ds)
        {
            return CommonFunctionDao.GetRequisitionType(strAccNo, ds);
        }

        public int GetRequisitionType(bool loadArchive, string strAccNo, DataSet ds)
        {
            return CommonFunctionDao.GetRequisitionType(loadArchive, strAccNo, ds);
        }

        public bool SaveERequisitionTemplateGuid(string strAccNo, string printTemplateGuid)
        {
            return CommonFunctionDao.SaveERequisitionTemplateGuid(strAccNo, printTemplateGuid);
        }

        public bool GetBookingNoticeTemplate(DataSet ds)
        {
            bool bReturn = true;

            try
            {
                bReturn = CommonFunctionDao.GetBookingNoticeTemplate(ds);

            }
            catch
            {
                bReturn = false;

            }
            finally
            {


            }
            return bReturn;
        }

        public DataSet GetDoctorSupervisor()
        {
            return CommonFunctionDao.GetDoctorSupervisor();
        }

        public DataSet QueryAllOnlineOfflineUser()
        {
            return CommonFunctionDao.QueryAllOnlineOfflineUser();
        }

        public string GetSystemProfileValue(string name, string moduleId)
        {
            return CommonFunctionDao.GetSystemProfileValue(name, moduleId);
        }
        public virtual bool GetOrderInfo(string strOrderGuid, DataSet dsOrderInfo)
        {
            return CommonFunctionDao.GetOrderInfo(strOrderGuid, dsOrderInfo);
        }
        public virtual bool UpdateOrderMessage(string strOrderGuid, string strOrderMessage, string strApplyDept, string strApplyDoctor)
        {
            return CommonFunctionDao.UpdateOrderMessage(strOrderGuid, strOrderMessage, strApplyDept, strApplyDoctor);
        }

        public bool IsArchived(string strPatientID, ref int nArchived)
        {
            return CommonFunctionDao.IsArchived(strPatientID, ref nArchived);
        }
        public bool SendFetchCommand(string strPatientID)
        {
            return CommonFunctionDao.SendFetchCommand(strPatientID);
        }

        public DataSet COMMON_getDataTable(string tableName, string matchingKey, string matchingValue)
        {
            return CommonFunctionDao.COMMON_getDataTable(tableName, matchingKey, matchingValue);
        }

        public object COMMON_queryDataTable(object parameters)
        {
            return CommonFunctionDao.COMMON_queryDataTable(parameters);
        }

        public DataSet getDynamicFormStructure()
        {
            return CommonFunctionDao.getDynamicFormStructure();
        }

        public bool PostEvent(DataTable dtModel)
        {
            return CommonFunctionDao.PostEvent(dtModel);
        }

        public bool PostMessage(DataTable dtModel)
        {
            return CommonFunctionDao.PostMessage(dtModel);
        }

        public DataSet GetValidUserList()
        {
            return CommonFunctionDao.GetValidUserList();
        }

        public DataSet GetValidApproverDoctor()
        {
            return CommonFunctionDao.GetValidApproverDoctor();
        }

        public bool CanEditOrderMessage(string strOrderGuid)
        {
            return CommonFunctionDao.CanEditOrderMessage(strOrderGuid);
        }


        public void SaveSignedHistory(SignHistoryModel model)
        {
            CommonFunctionDao.SaveSignedHistory(model);
        }

        public void SaveSignedData(SignHistoryModel model)
        {
            CommonFunctionDao.SaveSignedData(model);
        }

        public DataTable GetLatestSignedData(string reportGuid)
        {
            return CommonFunctionDao.GetLatestSignedData(reportGuid);
        }

        public DataTable GetLatestEveryActionSignedData(string reportGuid)
        {
            return CommonFunctionDao.GetLatestEveryActionSignedData(reportGuid);
        }

        public string GetUserTopLevleRole(string loginName)
        {
            return CommonFunctionDao.GetUserTopLevleRole(loginName);
        }

        public DataTable GetUserAllRoles(string loginName)
        {
            return CommonFunctionDao.GetUserAllRoles(loginName);
        }

        public bool IsReferralAndReadOnly(string orderGuid)
        {
            return CommonFunctionDao.IsReferralAndReadOnly(orderGuid);
        }

        public DataTable GetClientProfile(string clientID)
        {
            return CommonFunctionDao.GetClientProfile(clientID);
        }

        public bool SaveClientProfile(DataTable dtClientProfiles)
        {
            return CommonFunctionDao.SaveClientProfile(dtClientProfiles);
        }

        public bool SaveSystemProfile(DataTable dtProfiles)
        {
            return CommonFunctionDao.SaveSystemProfile(dtProfiles);
        }

        public bool LastReferralStatus(string strReferralID, ref int nLastRefStatus)
        {

            return CommonFunctionDao.LastReferralStatus(strReferralID, ref nLastRefStatus);
        }

        public bool RemoveOrderMessageFlag(string param, ref string errorinfo)
        {
            return CommonFunctionDao.RemoveOrderMessageFlag(param, ref errorinfo);
        }

        public bool AddOrderMessageFlag(string param, ref string errorinfo)
        {
            return CommonFunctionDao.AddOrderMessageFlag(param, ref errorinfo);
        }
    }
}
