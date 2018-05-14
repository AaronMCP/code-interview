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
using System.Data;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Server.Business.Common;
using CommonGlobalSettings;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Utilities.HippaLogTool;
using Server.Utilities.LogFacility;
using CommonGlobalSettings.HippaName;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;

namespace Server.Common.Action
{
    public class CommonFunctionAction : BaseAction
    {
        ICommonFunctionBusiness cfb = BusinessFactory.Instance.GetCommonFunctionService();
        LogManager logger = new LogManager();
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            string strOrderGuid = string.Empty;
            string strOrderMessage = string.Empty;
            string strApplyDoctor = string.Empty;
            string strApplyDept = string.Empty;
            string strUserID = string.Empty;
            string strUserName = string.Empty;
            string strRoleName = string.Empty;
            string strNetworkIP = string.Empty;
            string strObjectName = string.Empty;
            string strObjectDetail = string.Empty;
            string strIsDeleteAction = string.Empty;
            try
            {
                switch (context.MessageName)
                {
                    case "GetProfileInfo":
                        {

                            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
                            strRoleName = CommonGlobalSettings.Utilities.GetParameter("RoleName", context.Parameters).Trim();
                            dsAr.Result = cfb.GetProfileInfo(strRoleName, dsAr.DataSetData);

                        }
                        break;
                    case "UpdateCertificate":
                        {

                            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
                            string strUserGuid = Convert.ToString(bdsm.DataSetParameter.Tables[0].Rows[0]["UserGuid"]);
                            string strIkeySn = Convert.ToString(bdsm.DataSetParameter.Tables[0].Rows[0]["IkeySn"]);
                            string strPrivateKey = Convert.ToString(bdsm.DataSetParameter.Tables[0].Rows[0]["PrivateKey"]);
                            string strPublicKey = Convert.ToString(bdsm.DataSetParameter.Tables[0].Rows[0]["PublicKey"]);
                            string strOrgOwner = Convert.ToString(bdsm.DataSetParameter.Tables[0].Rows[0]["OrgOwner"]);


                            if (strUserGuid.Length == 0 || strIkeySn.Length == 0 || strPrivateKey.Length == 0 || strPublicKey.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.UpdateCertificate(strUserGuid, strIkeySn, strPrivateKey, strPublicKey, strOrgOwner, dsAr.DataSetData);

                        }
                        break;
                    case "GetCertificate":
                        {
                            string strUserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", context.Parameters).Trim();
                            string strIkeySn = CommonGlobalSettings.Utilities.GetParameter("IkeySn", context.Parameters).Trim();


                            dsAr.Result = cfb.GetCertificate(strUserGuid, strIkeySn, dsAr.DataSetData);

                        }
                        break;
                    case "GetCertificatePassword":
                        {
                            string strCertificatePassword = "";
                            dsAr.Result = cfb.GetCertificatePassword(ref strCertificatePassword);
                            dsAr.ReturnString = strCertificatePassword;

                        }
                        break;
                    case "SetCertificatePassword":
                        {
                            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
                            string strCertificatePassword = Convert.ToString(bdsm.DataSetParameter.Tables[0].Rows[0]["CertificatePassword"]);
                            dsAr.Result = cfb.SetCertificatePassword(strCertificatePassword);

                        }
                        break;
                    case "CheckClinic":
                        {
                            strRoleName = CommonGlobalSettings.Utilities.GetParameter("RoleName", context.Parameters);
                            dsAr.Result = cfb.CheckClinic(strRoleName);
                        }
                        break;

                    case "GetModalityType":
                        {
                            dsAr.Result = cfb.GetModalityType(dsAr.DataSetData);
                        }
                        break;
                    case "GetBodyCategory":
                        {
                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            strModalityType = strModalityType.Trim();
                            if (strModalityType.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.GetBodyCategory(strModalityType, dsAr.DataSetData);
                        }
                        break;
                    case "GetBodyPart":
                        {

                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            string strBodyCategory = CommonGlobalSettings.Utilities.GetParameter("BodyCategory", context.Parameters);
                            strModalityType = strModalityType.Trim();
                            strBodyCategory = strBodyCategory.Trim();
                            if (strModalityType.Length == 0 && strBodyCategory.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.GetBodyPart(strModalityType, strBodyCategory, dsAr.DataSetData);
                        }
                        break;
                    case "GetCheckingItem":
                        {

                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            string strBodyCategory = CommonGlobalSettings.Utilities.GetParameter("BodyCategory", context.Parameters);
                            string strBodyPart = CommonGlobalSettings.Utilities.GetParameter("BodyPart", context.Parameters);
                            strModalityType = strModalityType.Trim();
                            strBodyCategory = strBodyCategory.Trim();
                            strBodyPart = strBodyPart.Trim();
                            if (strModalityType.Length == 0 || strBodyCategory.Length == 0 || strBodyPart.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.GetCheckingItem(strModalityType, strBodyCategory, strBodyPart, dsAr.DataSetData);
                        }
                        break;
                    case "GetExamSystem":
                        {

                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            string strBodyPart = CommonGlobalSettings.Utilities.GetParameter("BodyPart", context.Parameters);
                            strModalityType = strModalityType.Trim();
                            strBodyPart = strBodyPart.Trim();

                            dsAr.Result = cfb.GetExamSystem(strModalityType, strBodyPart, dsAr.DataSetData);

                        }
                        break;
                    case "GetModality":
                        {
                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", context.Parameters);
                            string withPublicSite = CommonGlobalSettings.Utilities.GetParameter("withPublicSite", context.Parameters);//for using modality except schedule management.
                            if (withPublicSite == null || string.IsNullOrWhiteSpace(withPublicSite))
                            {//for using modality(show public + private) except schedule management(show public or private).
                                withPublicSite = "1";
                                strSite = CommonGlobalSettings.Utilities.GetCurSite();
                            }
                            strModalityType = strModalityType.Trim();

                            dsAr.Result = cfb.GetModality(strModalityType, dsAr.DataSetData, strSite, withPublicSite);
                        }
                        break;
                    case "LoadShortcut":
                        {
                            string strCatgory = "";
                            DataSet myDataSet = new DataSet();
                            strCatgory = CommonGlobalSettings.Utilities.GetParameter("Category", context.Parameters);
                            strUserID = CommonGlobalSettings.Utilities.GetParameter("UserID", context.Parameters);
                            dsAr.Result = cfb.LoadShortcut(int.Parse(strCatgory.Trim()), strUserID, ref myDataSet);
                            dsAr.DataSetData = myDataSet;

                        }
                        break;
                    case "GetShortcutName":
                        {
                            string strGuid = CommonGlobalSettings.Utilities.GetParameter("ShortcutGuid", context.Parameters);
                            dsAr.ReturnMessage = cfb.GetShorcutName(strGuid);
                        }
                        break;
                    case "AddShortcut":
                        {
                            string strType = "", strCatgory = "", strName = "", strValue = "";
                            DataSet myDataSet = new DataSet();
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            strType = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Type"].ToString().Trim();
                            strCatgory = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Category"].ToString().Trim();
                            strName = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Name"].ToString().Trim();
                            strValue = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Value"].ToString().Trim();
                            strUserID = model.DataSetParameter.Tables["Shortcut"].Rows[0]["UserID"].ToString().Trim();
                            int i = cfb.AddShortcut(int.Parse(strType.Trim()), int.Parse(strCatgory.Trim()), strName, strValue, strUserID, ref myDataSet);
                            if (i == 0)
                            {
                                dsAr.Result = false;
                            }
                            if (i == 1)
                            {
                                dsAr.Result = true;
                            }
                            if (i == 2)
                            {
                                dsAr.Result = false;
                                dsAr.ReturnMessage = "ExistName";
                            }
                            dsAr.DataSetData = myDataSet;
                        }
                        break;
                    case "EditShortcut":
                        {
                            string strGuid = "", strType = "", strCatgory = "", strName = "", strValue = "", strCurUser = "", strManageSS = "";
                            DataSet myDataSet = new DataSet();
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            strGuid = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Guid"].ToString().Trim();
                            strType = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Type"].ToString().Trim();
                            strCatgory = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Category"].ToString().Trim();
                            strName = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Name"].ToString().Trim();
                            strValue = model.DataSetParameter.Tables["Shortcut"].Rows[0]["Value"].ToString().Trim();
                            strUserID = model.DataSetParameter.Tables["Shortcut"].Rows[0]["UserID"].ToString().Trim();
                            strCurUser = model.DataSetParameter.Tables["Shortcut"].Rows[0]["CurUser"].ToString().Trim();
                            strManageSS = model.DataSetParameter.Tables["Shortcut"].Rows[0]["ManageSiteShortcut"].ToString().Trim();

                            int i = cfb.EditShortcut(strGuid, int.Parse(strType.Trim()), int.Parse(strCatgory.Trim()), strName, strValue, strUserID, strCurUser, strManageSS, ref myDataSet);
                            if (i == 0)
                            {
                                dsAr.Result = false;
                            }
                            if (i == 1)
                            {
                                dsAr.Result = true;
                            }
                            if (i == 2)
                            {
                                dsAr.Result = false;
                                dsAr.ReturnMessage = "ExistName";
                            }
                            dsAr.DataSetData = myDataSet;
                        }
                        break;
                    case "DeleteShortcut":
                        {
                            string strGuid = "", strCatgory = "", strSite = "";
                            DataSet myDataSet = new DataSet();
                            strGuid = CommonGlobalSettings.Utilities.GetParameter("Guid", context.Parameters);
                            strCatgory = CommonGlobalSettings.Utilities.GetParameter("Category", context.Parameters);
                            strUserID = CommonGlobalSettings.Utilities.GetParameter("UserID", context.Parameters);
                            strSite = CommonGlobalSettings.Utilities.GetParameter("Site", context.Parameters);
                            dsAr.Result = cfb.DeleteShortcut(strGuid, int.Parse(strCatgory.Trim()), strUserID, strSite, ref myDataSet);
                            dsAr.DataSetData = myDataSet;
                        }
                        break;
                    case "GetStaff":
                        {
                            string strDegreeName = CommonGlobalSettings.Utilities.GetParameter("DegreeName", context.Parameters);
                            strDegreeName = strDegreeName.Trim();
                            //if (strDegreeName.Length == 0 )
                            {
                                //  dsAr.Result = false;
                                //throw new Exception("Param is invalid");
                            }
                            dsAr.Result = cfb.GetStaff(strDegreeName, dsAr.DataSetData);

                        }
                        break;
                    case "GetLocalName":
                        //Modified by Paul for defect 45891. 
                        //the framework can not get local user name and needs to query the server
                        {
                            string strLoginName = CommonGlobalSettings.Utilities.GetParameter("LoginName", context.Parameters);
                            strLoginName = strLoginName.Trim();

                            dsAr.ReturnMessage = cfb.GetLocalName(strLoginName);
                        }
                        break;
                    case "GetLocalNameByUserGuid":
                        {
                            string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", context.Parameters).Trim();

                            dsAr.ReturnString = cfb.GetLocalNameByUserGuid(userGuid);
                        }
                        break;
                    case "GetProcedureDefaultValue":
                        {
                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            string strBodyCategory = CommonGlobalSettings.Utilities.GetParameter("BodyCategory", context.Parameters);
                            string strBodyPart = CommonGlobalSettings.Utilities.GetParameter("BodyPart", context.Parameters);
                            string strCheckingItem = CommonGlobalSettings.Utilities.GetParameter("CheckingItem", context.Parameters);
                            strModalityType = strModalityType.Trim();
                            strBodyCategory = strBodyCategory.Trim();
                            strBodyPart = strBodyPart.Trim();
                            strCheckingItem.Trim();
                            if (strModalityType.Length == 0 || strBodyCategory.Length == 0 || strBodyPart.Length == 0 || strCheckingItem.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.GetProcedureDefaultValue(strModalityType, strBodyCategory, strBodyPart, strCheckingItem, dsAr.DataSetData);

                        }
                        break;
                    case "GetProcedureCode":
                        {
                            string strProcedureCode = CommonGlobalSettings.Utilities.GetParameter("ProcedureCode", context.Parameters);
                            strProcedureCode.Trim();
                            string strShortcutCode = CommonGlobalSettings.Utilities.GetParameter("ShortcutCode", context.Parameters);
                            strShortcutCode.Trim();

                            if (strProcedureCode.Length == 0 && strShortcutCode.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.GetProcedureCode(strProcedureCode, strShortcutCode, dsAr.DataSetData);

                        }

                        break;
                    case "ReclaimID":
                        {
                            string strTag = CommonGlobalSettings.Utilities.GetParameter("Tag", context.Parameters);
                            string strValue = CommonGlobalSettings.Utilities.GetParameter("Value", context.Parameters);
                            string strLength = CommonGlobalSettings.Utilities.GetParameter("Length", context.Parameters);

                            strTag = strTag.Trim();
                            strValue = strValue.Trim();
                            strLength = strLength.Trim();

                            if (strTag.Length == 0 || strValue.Length == 0 || strLength.Length == 0)
                            {
                                dsAr.Result = false;
                                throw new Exception("Param is invalid");
                            }
                            int nTag = Convert.ToInt32(strTag);
                            int nLength = Convert.ToInt32(strLength);
                            dsAr.Result = cfb.ReclaimID(nTag, strValue, nLength);
                        }
                        break;
                    case "GetExtOptional":
                        {
                            string strTableName = CommonGlobalSettings.Utilities.GetParameter("TableName", context.Parameters);
                            strTableName = strTableName.Trim();

                            if (strTableName.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }

                            dsAr.Result = cfb.GetExtOptional(strTableName, dsAr.DataSetData);

                        }
                        break;
                    case "GetRequisition":
                        {
                            string strAccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", context.Parameters);
                            string strDataCenter = CommonGlobalSettings.Utilities.GetParameter("DataCenter", context.Parameters);
                            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", context.Parameters);
                            string strLoadArchive = CommonGlobalSettings.Utilities.GetParameter("LoadArchive", context.Parameters);


                            if (strAccNo.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }

                            if (strLoadArchive == "1")
                            {
                                dsAr.Result = cfb.GetRequisition(true, strAccNo, strDataCenter, strDomain, dsAr.DataSetData);
                            }
                            else if (strLoadArchive == "0")
                            {
                                dsAr.Result = cfb.GetRequisition(false, strAccNo, strDataCenter, strDomain, dsAr.DataSetData);
                            }
                            else
                            {
                                dsAr.Result = cfb.GetRequisition(strAccNo, strDataCenter, strDomain, dsAr.DataSetData);
                            }

                        }
                        break;
                    case "GetAllProcedureCode":
                        {

                            dsAr.Result = cfb.GetAllProcedureCode(dsAr.DataSetData);
                        }
                        break;
                    case "GetDateTypeByCalendar":
                        {
                            string dateType = "";
                            string availableDate = "";
                            string bookingDate = CommonGlobalSettings.Utilities.GetParameter("BookingDate", context.Parameters);
                            string modality = CommonGlobalSettings.Utilities.GetParameter("Modality", context.Parameters);
                            dsAr.Result = cfb.GetDateTypeByCalendar(bookingDate, modality, ref dateType, ref availableDate);
                            dsAr.ReturnMessage = dateType;
                            dsAr.ReturnString = availableDate;
                        }
                        break;
                   case "GetCurSiteProcedureCode":
                        {

                            dsAr.Result = cfb.GetCurSiteProcedureCode(dsAr.DataSetData);
                        }
                        break;
                   case "GetProcedureCodeBySite":
                        {
                            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", context.Parameters);
                            dsAr.Result = cfb.GetProcedureCodeBySite(strSite,dsAr.DataSetData);
                        }
                        break;


                    case "CMN_GetConditionColumn":
                    case "CMN_GETCONDITIONCOLUMN":
                    case "cmn_getconditioncolumn":
                        {
                            Dictionary<string, object> inParamMap = CommonGlobalSettings.Utilities.GetParameters(context.Parameters);

                            inParamMap.Add("UserID", context.UserID);

                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetConditionColumn(inParamMap);
                        }
                        break;

                    case "CMN_GetGridColumn":
                    case "CMN_GETGRIDCOLUMN":
                    case "cmn_getgridcolumn":
                        {
                            Dictionary<string, object> inParamMap = CommonGlobalSettings.Utilities.GetParameters(context.Parameters);

                            inParamMap.Add("UserID", context.UserID);

                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetGridColumn(inParamMap);
                        }
                        break;

                    case "CMN_GetUserList":
                    case "CMN_GETUSERLIST":
                    case "cmn_getuserlist":
                        {
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetUserList();
                        }
                        break;
                    case "CMN_GetIMUserList":
                    case "CMN_GETIMUSERLIST":
                    case "cmn_getimuserlist":
                        {
                            string type = CommonGlobalSettings.Utilities.GetParameter("Type", context.Parameters);
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetIMUserList(type);
                        }
                        break;

                    case "CMN_GetIMLog":
                    case "CMN_GETIMLOG":
                    case "cmn_getimlog":
                        {
                            string strQueryConditons = context.Parameters;
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetIMLog(strQueryConditons);
                        }
                        break;

                    case "CMN_GetConditionRelatedControlData":
                    case "CMN_GETCONDITIONRELATEDCONTROLDATA":
                    case "cmn_getconditionrelatedcontroldata":
                        {
                            Dictionary<string, object> inParamMap = CommonGlobalSettings.Utilities.GetParameters(context.Parameters);

                            inParamMap.Add("UserID", context.UserID);

                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetConditionRelatedControlData(inParamMap);
                        }
                        break;

                    case "Common.GetFtpParameters":
                        return GetFtpParameters();
                    case "Common.GetBackupFileList":
                        return GetBackupFileList(context.Parameters);
                    case "RegIntegration":
                        {

                            string actionName = CommonGlobalSettings.Utilities.GetParameter("ActionName", context.Parameters);
                            dsAr = RegIntegrationAction(actionName, context);
                        }
                        break;
                    case "CheckOrderStatus":
                        {
                            string strPatientGuid = CommonGlobalSettings.Utilities.GetParameter("PatientGuid", context.Parameters);
                            strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            string strCurUser = CommonGlobalSettings.Utilities.GetParameter("UserGuid", context.Parameters);
                            string strLockType = CommonGlobalSettings.Utilities.GetParameter("LockType", context.Parameters);
                            cfb.CheckOrderStatus(strPatientGuid, strOrderGuid, strCurUser, strLockType, dsAr);
                        }
                        break;
                    case "GetDictionaryValue":
                        string tagStr = CommonGlobalSettings.Utilities.GetParameter("Tag", context.Parameters);
                        dsAr.Result = true;
                        dsAr.DataSetData = cfb.GetDictionaryValue(tagStr);
                        break;

                    case "GetRequisitionType":
                        {
                            string strAccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", context.Parameters);
                            string strLoadArchive = CommonGlobalSettings.Utilities.GetParameter("LoadArchive", context.Parameters);
                            if (strLoadArchive == "1")
                            {
                                dsAr.recode = cfb.GetRequisitionType(true, strAccNo, dsAr.DataSetData);
                            }
                            else
                            {
                                dsAr.recode = cfb.GetRequisitionType(strAccNo, dsAr.DataSetData);
                            }
                            dsAr.Result = true;
                        }
                        break;

                    case "SaveERequisitionTemplateGuid":
                        {
                            string strAccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", context.Parameters);
                            string strPrintTemplateGuid = CommonGlobalSettings.Utilities.GetParameter("PrintTemplateGuid", context.Parameters);
                            dsAr.Result = cfb.SaveERequisitionTemplateGuid(strAccNo, strPrintTemplateGuid);
                        }
                        break;
                    case "GetBookingNoticeTemplate":
                        dsAr.Result = cfb.GetBookingNoticeTemplate(dsAr.DataSetData);
                        break;
                    case "GetDoctorSupervisor":
                        DataSet ds = cfb.GetDoctorSupervisor();
                        dsAr.Result = ds != null;
                        dsAr.DataSetData = ds;
                        break;
                    case "QueryAllOnlineOfflineUser":
                        {
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.QueryAllOnlineOfflineUser();
                        }
                        break;
                    case "GetSystemProfileValue":
                        {
                            string strName = CommonGlobalSettings.Utilities.GetParameter("Name", context.Parameters);
                            string strModuleId = CommonGlobalSettings.Utilities.GetParameter("ModuleId", context.Parameters);
                            dsAr.Result = true;
                            dsAr.ReturnString = cfb.GetSystemProfileValue(strName, strModuleId);
                        }
                        break;
                    case "GetOrderInfo":
                        {

                            strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            //string strOrderMessage = "";
                            dsAr.Result = cfb.GetOrderInfo(strOrderGuid, dsAr.DataSetData);
                            //dsAr.ReturnMessage = strOrderMessage;
                            if (!(dsAr.DataSetData.Tables[0].Rows.Count > 0 && dsAr.DataSetData.Tables[0].Rows[0]["OrderMessage"] is DBNull))
                                dsAr.ReturnMessage = (string)dsAr.DataSetData.Tables[0].Rows[0]["OrderMessage"];
                            else { dsAr.ReturnMessage = ""; }
                        }
                        break;
                    case "UpdateOrderMessage":
                        {

                            DataSet myDataSet = new DataSet();
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            strOrderGuid = model.DataSetParameter.Tables[0].Rows[0]["OrderGuid"].ToString().Trim();
                            strOrderMessage = model.DataSetParameter.Tables[0].Rows[0]["OrderMessage"].ToString().Trim();
                            strApplyDoctor = model.DataSetParameter.Tables[0].Rows[0]["ApplyDoctor"].ToString().Trim();
                            strApplyDept = model.DataSetParameter.Tables[0].Rows[0]["ApplyDept"].ToString().Trim();
                            strUserID = model.DataSetParameter.Tables[0].Rows[0]["UserID"].ToString().Trim();
                            strUserName = model.DataSetParameter.Tables[0].Rows[0]["UserName"].ToString().Trim();
                            strRoleName = model.DataSetParameter.Tables[0].Rows[0]["RoleName"].ToString().Trim();
                            strNetworkIP = model.DataSetParameter.Tables[0].Rows[0]["NetworkIP"].ToString().Trim();
                            strObjectName = model.DataSetParameter.Tables[0].Rows[0]["ObjectName"].ToString().Trim();
                            strObjectDetail = model.DataSetParameter.Tables[0].Rows[0]["ObjectDetail"].ToString().Trim();
                            strIsDeleteAction = model.DataSetParameter.Tables[0].Rows[0]["IsDeleteAction"].ToString().Trim();

                            //check if the ordermessage has already been changed.
                            string strOrderMessageOld = model.DataSetParameter.Tables[0].Rows[0]["OrderMessageOld"].ToString().Trim();
                            cfb.GetOrderInfo(strOrderGuid, myDataSet);
                            if (!(myDataSet.Tables[0].Rows.Count > 0 && myDataSet.Tables[0].Rows[0]["OrderMessage"] is DBNull))
                            {
                                if (myDataSet.Tables[0].Rows[0]["OrderMessage"].ToString().Replace("\r\n", "\n") != strOrderMessageOld)
                                {
                                    dsAr.Result = true;
                                    dsAr.recode = -2;
                                    break;
                                }
                            }

                            dsAr.Result = cfb.UpdateOrderMessage(strOrderGuid, strOrderMessage, strApplyDept, strApplyDoctor);
                            if (dsAr.Result && strIsDeleteAction == "1")
                            {
                                //(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, 
                                //string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription)
                                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Delete, "Update Order", strUserID, strUserName, strRoleName, strNetworkIP, strOrderGuid, strObjectName, strObjectDetail, "Delete Critical Sign Message or Notice Message or Flags from Order Record", true);
                            }
                        }
                        break;
                    case "IsArchived":
                        {
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            dsAr.Result = true;
                            int nArchived = 0;
                            dsAr.Result = cfb.IsArchived(strPatientID, ref nArchived);
                            dsAr.recode = nArchived;
                        }
                        break;
                    case "SendFetchCommand":
                        {
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            dsAr.Result = cfb.SendFetchCommand(strPatientID);
                        }
                        break;
                    case "COMMON_getDataTable":
                        {
                            string tableName = CommonGlobalSettings.Utilities.GetParameter("TableName", context.Parameters);
                            string matchingKey = CommonGlobalSettings.Utilities.GetParameter("matchingKey", context.Parameters);
                            string matchingValue = CommonGlobalSettings.Utilities.GetParameter("matchingValue", context.Parameters);

                            dsAr.DataSetData = cfb.COMMON_getDataTable(tableName, matchingKey, matchingValue);
                            dsAr.Result = true;
                        }
                        break;
                    case "COMMON_queryDataTable":
                        {
                            Dictionary<string, object> parameters = CommonGlobalSettings.Utilities.GetParameters(context.Parameters);

                            parameters.Add("UserID", context.UserID);

                            Dictionary<string, object> outMap = cfb.COMMON_queryDataTable(parameters) as Dictionary<string, object>;

                            if (outMap != null)
                            {
                                foreach (string key in outMap.Keys)
                                {
                                    if (key.ToUpper() == "DATASET")
                                    {
                                        dsAr.DataSetData = outMap[key] as DataSet;
                                    }
                                    else if (key.ToUpper() == "CURPAGE")
                                    {
                                        dsAr.recode = System.Convert.ToInt32(outMap[key]);
                                    }
                                }

                                dsAr.Result = true;
                            }
                            else
                            {
                                dsAr.Result = false;
                                dsAr.ReturnMessage = "Unknown Error";
                            }
                        }
                        break;
                    case "GetFieldValue":
                        {
                            string strTableName = CommonGlobalSettings.Utilities.GetParameter("TableName", context.Parameters);
                            string strFieldName = CommonGlobalSettings.Utilities.GetParameter("FieldName", context.Parameters);
                            string strUniqueFieldName = CommonGlobalSettings.Utilities.GetParameter("UniqueFieldName", context.Parameters);
                            string strUniqueFieldValue = CommonGlobalSettings.Utilities.GetParameter("UniqueFieldValue", context.Parameters);
                            string strFieldValue = cfb.GetFieldValue(strTableName, strFieldName, strUniqueFieldName, strUniqueFieldValue);
                            dsAr.ReturnString = strFieldValue;
                        }
                        break;

                    case "CMN_GetDynamicFormStructure":
                    case "CMN_GETDYNAMICFORMSTRUCTURE":
                    case "cmn_getdynamicformstructure":
                        {
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.getDynamicFormStructure();
                        }
                        break;
                    case "PostEvent":
                        {

                            dsAr.Result = cfb.PostEvent((context.Model as BaseDataSetModel).DataSetParameter.Tables[0]);
                        }
                        break;
                    case "PostMessage":
                        {
                            dsAr.Result = cfb.PostMessage((context.Model as BaseDataSetModel).DataSetParameter.Tables[0]);
                        }
                        break;
                    case "CMN_GetValidUserList":
                    case "CMN_GETVALIDUSERLIST":
                    case "cmn_getvaliduserlist":
                        {
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetValidUserList();
                        }
                        break;
                    case "GetValidApproverDoctor":
                        {
                            dsAr.Result = true;
                            dsAr.DataSetData = cfb.GetValidApproverDoctor();
                        }
                        break;

                    case "CanEditOrderMessage":
                        {
                            strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            dsAr.Result = cfb.CanEditOrderMessage(strOrderGuid);
                        }
                        break;
                    case "CMN_DIGITALSIGN":
                        {
                            string actionName =
                                CommonGlobalSettings.Utilities.GetParameter("ActionName", context.Parameters);
                            dsAr = DigitalSignAction(actionName, context);
                        }
                        break;
                    case "CMN_GETUSERTOPLEVELROLE":
                        {
                            string loginName =
                                CommonGlobalSettings.Utilities.GetParameter("LoginName", context.Parameters);
                            dsAr = GetUserTopLevelRole(loginName, context);
                        }
                        break;
                    case "CMN_GETUSERALLROLES":
                        {
                            string loginName =
                                CommonGlobalSettings.Utilities.GetParameter("LoginName", context.Parameters);
                            dsAr = GetUserAllRoles(loginName);
                        }
                        break;
                    case "CMN_ISREFERRALANDREADONLY":
                        {
                            string orderGuid =
                                CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            dsAr = IsReferralAndReadOnly(orderGuid);
                        }
                        break;
                    case "CMN_GETCLIENTPROFILE":
                    case "cmn_getclientprofile":
                    case "CMN_GetClientProfile":
                        {
                            string clientID =
                                CommonGlobalSettings.Utilities.GetParameter("ClientID", context.Parameters);
                            dsAr = GetClientProfile(clientID);
                        }
                        break;
                    case "CMN_SAVECLIENTPROFILE":
                    case "cmn_saveclientprofile":
                    case "CMN_SaveClientProfile":
                        {
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            if (model != null && model.DataSetParameter != null && model.DataSetParameter.Tables.Count > 0)
                            {
                                dsAr.Result = SaveClientProfile(model.DataSetParameter.Tables[0]);
                            }
                        }
                        break;
                    case "CMN_SAVESYSTEMPROFILE":
                    case "cmn_savesystemprofile":
                    case "CMN_SaveSystemProfile":
                        {
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            if (model != null && model.DataSetParameter != null && model.DataSetParameter.Tables.Count > 0)
                            {
                                dsAr.Result = SaveSystemProfile(model.DataSetParameter.Tables[0]);
                            }
                        }
                        break;
                    case "GetRoleBySite":
                        string strSite1 = CommonGlobalSettings.Utilities.GetParameter("Site", context.Parameters);
                        dsAr.Result = cfb.GetRoleBySite(strSite1, dsAr.DataSetData);
                        break;
                    case "cmn_LastReferralStatus":
                        {
                            string strReferralID =CommonGlobalSettings.Utilities.GetParameter("ReferralID", context.Parameters);
                            return LastReferralStatus(strReferralID);
                        }
                        break;
                    case "RemoveOrderMessageFlag":
                        {                            
                            return RemoveOrderMessageFlag(context.Parameters);
                        }
                        break;
                    case "AddOrderMessageFlag":
                        {
                            return AddOrderMessageFlag(context.Parameters);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                dsAr.ReturnMessage = ex.Message;
                dsAr.Result = false;
                dsAr.recode = -1;
                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Delete, "Update Order", strUserID, strUserName, strRoleName, strNetworkIP, strOrderGuid, strObjectName, strObjectDetail, "Delete Critical Sign Message or Notice Message or Flags from Order Record", false);
            }
            return dsAr;
        }

        private DataSetActionResult RegIntegrationAction(string actionName, Context context)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            BaseActionResult bar = dsar as BaseActionResult;
            switch (actionName)
            {
                case "NewPID":
                    {
                        cfb.GeneratePatientIDEx(bar);
                    }
                    break;
                case "NewAccNo":
                    {
                        cfb.GenerateAccNoEx(bar);
                    }
                    break;
                case "QueryPatientEx":
                    {
                        string strKey = CommonGlobalSettings.Utilities.GetParameter("Key", context.Parameters);
                        string strValue = CommonGlobalSettings.Utilities.GetParameter("Value", context.Parameters);
                        cfb.QueryPatientEx(strKey, strValue, true, dsar);
                    }
                    break;
                case "SavePatient":
                    {
                        PatientModel pModel = context.Model as PatientModel;
                        if (pModel == null)
                        {
                            throw new Exception("Param is invalid!");
                        }
                        cfb.RegIntegrationSavePatient(pModel, bar);
                    }
                    break;
                case "LocatePatientByHISInfo":
                    {
                        string localPID = CommonGlobalSettings.Utilities.GetParameter("LocalID", context.Parameters);
                        string name = CommonGlobalSettings.Utilities.GetParameter("PatientName", context.Parameters);
                        string hisID = CommonGlobalSettings.Utilities.GetParameter("HISID", context.Parameters);
                        cfb.LocatePatientByHISInfo(localPID, hisID, name, dsar);
                    }
                    break;

                default:
                    break;
            }
            return dsar;
        }

        private FTPActionResult GetFtpParameters()
        {
            FTPActionResult result = new FTPActionResult();
            FtpModel model = cfb.GetFtpParameters();
            result.Model = model;
            result.Result = true;
            return result;
        }

        private DataSetActionResult GetBackupFileList(string parameters)
        {
            //DateTime beginTime = Convert.ToDateTime(Utilities.GetParameter("beginTime", parameters));
            //DateTime endTime = Convert.ToDateTime(Utilities.GetParameter("endTime", parameters));
            string stTables = Convert.ToString(CommonGlobalSettings.Utilities.GetParameter("Tables", parameters));
            bool bFailRetry = Convert.ToBoolean(CommonGlobalSettings.Utilities.GetParameter("Retry", parameters));
            string stRetryFlag = Convert.ToString(CommonGlobalSettings.Utilities.GetParameter("RetryFlags", parameters));
            DataSetActionResult result = new DataSetActionResult();
            DataTable dataTable = cfb.GetBackupFileList(stTables, bFailRetry, stRetryFlag);
            if (dataTable != null)
            {
                result.Result = true;
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dataTable);
                result.DataSetData = dataSet;
            }

            return result;
        }

        private string GetStringFromDataCell(DataSet ds, string tableName, int rowIndex, string columnName)
        {
            try
            {
                if (ds != null &&
                    rowIndex >= 0 &&
                    ds.Tables.Contains(tableName) &&
                    ds.Tables[tableName].Rows.Count > 0 &&
                    ds.Tables[tableName].Rows.Count > rowIndex &&
                    ds.Tables[tableName].Columns.Contains(columnName))
                {
                    object obj = ds.Tables[tableName].Rows[rowIndex][columnName];

                    if (obj != null)
                    {
                        return System.Convert.ToString(obj).Trim();
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }

            return string.Empty;
        }

        private DataSetActionResult DigitalSignAction(string actionName, Context context)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            dsar.Result = true;

            try
            {
                switch (actionName.ToUpper())
                {
                    case "SAVESIGNEDHISTORY":
                        cfb.SaveSignedHistory(context.Model as SignHistoryModel);

                        break;
                    case "SAVESINGEDDATA":
                        cfb.SaveSignedData(context.Model as SignHistoryModel);
                        break;

                    case "GETLATESTSIGNEDDATA":
                        {
                            dsar.DataSetData = new DataSet();
                            dsar.Result = true;
                            string reportGuid =CommonGlobalSettings.Utilities.GetParameter("ReportGuid", context.Parameters).Trim();
                            DataTable dt = cfb.GetLatestSignedData(reportGuid);
                            dsar.DataSetData.Tables.Add(dt);
                        }
                        break;
                    case "GETLASTESTEVERYACTIONSIGNEDDATA":
                        {
                            dsar.DataSetData = new DataSet();
                            dsar.Result = true;
                            string reportGuid = CommonGlobalSettings.Utilities.GetParameter("ReportGuid", context.Parameters).Trim();
                            DataTable dt = cfb.GetLatestEveryActionSignedData(reportGuid);
                            dsar.DataSetData.Tables.Add(dt);
                        }
                        break;

                    default:
                        break;
                }
                return dsar;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
                return dsar;
            }
        }


        private DataSetActionResult GetUserTopLevelRole(string loginName, Context context)
        {
            DataSetActionResult dsar = new DataSetActionResult();

            try
            {
                dsar.Result = true;
                dsar.ReturnString = cfb.GetUserTopLevleRole(loginName);
                return dsar;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
                return dsar;
            }
        }

        private DataSetActionResult GetUserAllRoles(string loginName)
        {
            DataSetActionResult dsar = new DataSetActionResult();

            try
            {
                dsar.DataSetData = new DataSet();
                dsar.Result = true;

                var dt = cfb.GetUserAllRoles(loginName);
                dsar.DataSetData.Tables.Add(dt);

                return dsar;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
                return dsar;
            }
        }

        private DataSetActionResult IsReferralAndReadOnly(string orderGuid)
        {
            DataSetActionResult bsar = new DataSetActionResult();

            try
            {
                bsar.Result = cfb.IsReferralAndReadOnly(orderGuid);
                return bsar;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                bsar.Result = false;
                bsar.ReturnMessage = ex.Message;
                return bsar;
            }
        }

        DataSetActionResult GetClientProfile(string clientID)
        {
            DataSetActionResult dsar = new DataSetActionResult();

            try
            {
                dsar.DataSetData = new DataSet();
                dsar.Result = true;

                var dt = cfb.GetClientProfile(clientID);
                dsar.DataSetData.Tables.Add(dt);

                return dsar;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
                return dsar;
            }
        }

        bool SaveClientProfile(DataTable dt)
        {
            return cfb.SaveClientProfile(dt);
        }

        bool SaveSystemProfile(DataTable dt)
        {
            return cfb.SaveSystemProfile(dt);
        }

        BaseActionResult LastReferralStatus(string strReferralID)
        {
            BaseActionResult bar = new BaseActionResult();

            try
            {

                  bar.Result = true;
                  int nLastReferralStatus = 0;
                  bar.Result = cfb.LastReferralStatus(strReferralID, ref nLastReferralStatus);
                  bar.recode = nLastReferralStatus;

                 
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                bar.Result = false;
                bar.ReturnMessage = ex.Message;                
            }
            return bar;
        }

        BaseActionResult RemoveOrderMessageFlag(string param)
        {
            BaseActionResult bar = new BaseActionResult();

            try
            {

                bar.Result = true;
                string errorinfo = "";
                bar.Result = cfb.RemoveOrderMessageFlag(param, ref errorinfo);
                bar.ReturnMessage = errorinfo;


            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        BaseActionResult AddOrderMessageFlag(string param)
        {
            BaseActionResult bar = new BaseActionResult();

            try
            {

                bar.Result = true;
                string errorinfo = "";
                bar.Result = cfb.AddOrderMessageFlag(param, ref errorinfo);
                bar.ReturnMessage = errorinfo;


            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }
    }
}
