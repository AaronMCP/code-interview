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
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.Oam;
using CommonGlobalSettings;
using CommonGlobalSettings;

namespace Server.OamAction.Action
{
    public class DictionaryManagerAction : BaseAction
    {
        private IDictionaryService dictionaryService = BusinessFactory.Instance.GetDictionaryService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            if (actionName == null || actionName.Equals(""))
            {
                actionName = "list";
            }

            switch (actionName)
            {
                case "getDictionaryVaild":
                    return GetDictionaryVaild(context.Parameters);
                case "list":
                    return List(context.Parameters);
                case "GetSiteDictionaryItems":
                    return GetSiteDictionaryItems(context.Parameters);
                case "CustomDictionaryItems":
                    return CustomDictionaryItems(context.Parameters);
                case "DeleteDictionaryItems":
                    return DeleteDictionaryItems(context.Parameters);
                case "addValueItem":
                    return AddValueItem(context.Parameters);
                case "modifyValueItem":
                    return ModifyValueItem(context.Parameters);
                case "deleteValueItem":
                    return DeleteValueItem(context.Parameters);
                case "save":
                    return SaveDefaultValue(context.Model as DictionaryModel);
                case "GetDicMapping":
                    return GetDicMappingList(context.Parameters);
                case "SaveDicMapping":
                    return SaveDicMapping(context.Model as DictionaryModel);
                case "GetTeachingCategory":
                    return GetTeachingCategory();
                case "IsTeachingCategoryExists":
                    return IsTeachingCategoryExists(context.Parameters);
                case "AddTeachingCategory":
                    return AddTeachingCategory(context.Parameters);
                case "ModifyTeachingCategory":
                    return ModifyTeachingCategory(context.Parameters);
                case "IsTeachingCategoryUsedIncludingChildren":
                    return IsTeachingCategoryUsedIncludingChildren(context.Parameters);
                case "DeleteTeachingCategory":
                    return DeleteTeachingCategory(context.Parameters);
                case "GetApplyDoctors":
                    return GetApplyDoctors();
                #region Added by Blue for [RC607] - US17706
                case "GetExamNameByDomainSite":
                    return GetExamNameByDomainSite(context.Parameters);
                case "GetCurrentDomainSiteExamName":
                    return GetCurrentDomainSiteExamName(context.Parameters);
                case "AddExamName":
                    return AddExamName(context.Parameters);
                case "EditExamName":
                    return EditExamName(context.Parameters);
                case "DeleteExamName":
                    return DeleteExamName(context.Parameters);
                case "GetScanningTechByDomainSite":
                    return GetScanningTechByDomainSite(context.Parameters);
                case "GetCurrentDomainSiteScanningTech":
                    return GetCurrentDomainSiteScanningTech(context.Parameters);
                case "AddScanningTech":
                    return AddScanningTech(context.Parameters);
                case "EditScanningTech":
                    return EditScanningTech(context.Parameters);
                case "DeleteScanningTech":
                    return DeleteScanningTech(context.Parameters);
                #endregion
                #region Added by Blue for [RC507] - US16620
                case "GetEventTypes":
                    return GetEventTypes();
                case "UpdateEventTypes":
                    return UpdateEventTypes(context.Model);
                case "GetEventCategories":
                    return GetEventCategories();
                #endregion
                case "Do":
                    return Do(context);
                case "GetPhysicalCompany":
                    return GetPhysicalCompany(context.Parameters);
                case "SavePhysicalCompany":
                    return SavePhysicalCompany(context.Model as BaseDataSetModel);
                    
            }

            //default  
            return List(context.Parameters);
        }

        private BaseActionResult GetPhysicalCompany(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = dictionaryService.GetPhysicalCompany(parameters);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult SavePhysicalCompany(BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();

            try
            {
                if (dictionaryService.SavePhysicalCompany(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        #region Added by Blue for [RC507] - US16620
        private BaseActionResult UpdateEventTypes(BaseModel baseModel)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                BaseDataSetModel model = baseModel as BaseDataSetModel;
                if (dictionaryService.UpdateEventTypes(model.DataSetParameter.Tables[0]))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetEventTypes()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataTable dt = dictionaryService.GetEventTypes();
                if (dt != null)
                {
                    result.Result = true;
                    result.DataSetData = new DataSet();
                    result.DataSetData.Tables.Add(dt);
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetEventCategories()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataTable dt = dictionaryService.GetEventCategories();
                if (dt != null)
                {
                    result.Result = true;
                    result.DataSetData = new DataSet();
                    result.DataSetData.Tables.Add(dt);
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region Added by Blue for [RC607] - US17706
        private BaseActionResult GetExamNameByDomainSite(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string domainName = CommonGlobalSettings.Utilities.GetParameter("domainName", parameters);
                string siteName = CommonGlobalSettings.Utilities.GetParameter("siteName", parameters);
                string strIsDomain = CommonGlobalSettings.Utilities.GetParameter("isDomain", parameters);
                bool isDomain = true;
                bool.TryParse(strIsDomain, out isDomain);
                DataTable dt = dictionaryService.GetExamNameByDomainSite(domainName, siteName, isDomain);
                if (dt != null)
                {
                    result.Result = true;
                    result.DataSetData = new DataSet();
                    result.DataSetData.Tables.Add(dt);
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetCurrentDomainSiteExamName(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
                DataTable dt = dictionaryService.GetCurrentDomainSiteExamName(modalityType);
                if (dt != null)
                {
                    result.Result = true;
                    result.DataSetData = new DataSet();
                    result.DataSetData.Tables.Add(dt);
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteExamName(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string guid = CommonGlobalSettings.Utilities.GetParameter("guid", parameters);

            try
            {
                if (dictionaryService.DeleteExamName(guid))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult EditExamName(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string guid = CommonGlobalSettings.Utilities.GetParameter("guid", parameters);
            string examName = CommonGlobalSettings.Utilities.GetParameter("examName", parameters);

            try
            {
                if (dictionaryService.EditExamName(guid, examName))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddExamName(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string examName = CommonGlobalSettings.Utilities.GetParameter("examName", parameters);
            string parentGuid = CommonGlobalSettings.Utilities.GetParameter("parentGuid", parameters);
            string domain = CommonGlobalSettings.Utilities.GetParameter("domain", parameters);
            string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
            try
            {
                if (dictionaryService.AddExamName(examName, parentGuid, domain, site))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetScanningTechByDomainSite(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
                string domainName = CommonGlobalSettings.Utilities.GetParameter("domainName", parameters);
                string siteName = CommonGlobalSettings.Utilities.GetParameter("siteName", parameters);
                string strIsDomain = CommonGlobalSettings.Utilities.GetParameter("isDomain", parameters);
                bool isDomain = true;
                bool.TryParse(strIsDomain, out isDomain);
                DataTable dt = dictionaryService.GetScanningTechByDomainSite(modalityType, domainName, siteName, isDomain);
                if (dt != null)
                {
                    result.Result = true;
                    result.DataSetData = new DataSet();
                    result.DataSetData.Tables.Add(dt);
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetCurrentDomainSiteScanningTech(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
                DataTable dt = dictionaryService.GetCurrentDomainSiteScanningTech(modalityType);
                if (dt != null)
                {
                    result.Result = true;
                    result.DataSetData = new DataSet();
                    result.DataSetData.Tables.Add(dt);
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddScanningTech(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
            string scanningTech = CommonGlobalSettings.Utilities.GetParameter("scanningTech", parameters);
            string parentGuid = CommonGlobalSettings.Utilities.GetParameter("parentGuid", parameters);
            string domain = CommonGlobalSettings.Utilities.GetParameter("domain", parameters);
            string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
            try
            {
                if (dictionaryService.AddScanningTech(scanningTech, parentGuid, modalityType, domain, site))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult EditScanningTech(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string guid = CommonGlobalSettings.Utilities.GetParameter("guid", parameters);
            string scanningTech = CommonGlobalSettings.Utilities.GetParameter("scanningTech", parameters);

            try
            {
                if (dictionaryService.EditScanningTech(guid, scanningTech))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteScanningTech(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string guid = CommonGlobalSettings.Utilities.GetParameter("guid", parameters);

            try
            {
                if (dictionaryService.DeleteScanningTech(guid))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        #endregion

        private BaseActionResult GetDictionaryVaild(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string Tag = CommonGlobalSettings.Utilities.GetParameter("DicTag", parameters);
                int DicTag = Convert.ToInt16(Tag);
                //get the specical tag from Dictionary 
                DataSet dataSet = dictionaryService.GetDictionaryVaild(DicTag);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult List(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
                DataSet dataSet = dictionaryService.GetDictionaryDataSet(strSite);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult CustomDictionaryItems(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string tag = CommonGlobalSettings.Utilities.GetParameter("DicTag", parameters);
            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
            try
            {
                DataSet dataSet = dictionaryService.CustomDictionaryItems(tag, strSite);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteDictionaryItems(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string tag = CommonGlobalSettings.Utilities.GetParameter("DicTag", parameters);
            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
            try
            {
                DataSet dataSet = dictionaryService.DeleteDictionaryItems(tag, strSite);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetSiteDictionaryItems(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string tag = CommonGlobalSettings.Utilities.GetParameter("DicTag", parameters);
            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
            try
            {
                DataSet dataSet = dictionaryService.GetSiteDictionaryItems(tag, strSite);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddValueItem(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string tag = CommonGlobalSettings.Utilities.GetParameter("tag", parameters);
            string codeValue = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("codeValue", parameters));
            string codeDescription = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("codeDescription", parameters));
            string shortcutCode = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("shortcutCode", parameters));
            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
            //string orderid = CommonGlobalSettings.Utilities.GetParameter("orderId", parameters);
            try
            {
                if (dictionaryService.AddDictionaryValue(tag, codeValue, codeDescription, shortcutCode, strDomain, strSite))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult ModifyValueItem(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string tag = CommonGlobalSettings.Utilities.GetParameter("tag", parameters);
            string oldCodeValue = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("oldCodeValue", parameters));
            string newCodeValue = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("newCodeValue", parameters));
            string codeDescription = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("codeDescription", parameters));
            string shortcutCode = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("shortcutCode", parameters));
            string dictionaryValues = CommonGlobalSettings.Utilities.GetParameter("dictionaryValues", parameters);
            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
            try
            {
                if (dictionaryService.ModifyDictionaryValue(tag, oldCodeValue, newCodeValue, codeDescription, shortcutCode, dictionaryValues, strSite))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteValueItem(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string tag = CommonGlobalSettings.Utilities.GetParameter("tag", parameters);
            string codeValue = CommonGlobalSettings.Utilities.unescape(CommonGlobalSettings.Utilities.GetParameter("codeValue", parameters));
            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
            try
            {
                if (dictionaryService.DeleteDictionaryValue(tag, codeValue, strSite))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult SaveDefaultValue(DictionaryModel model)
        {
            BaseActionResult result = new BaseActionResult();

            try
            {
                if (dictionaryService.ModifyDictionaryDefaultValue(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetDicMappingList(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
                DataSet dataSet = dictionaryService.GetDicMappingDataset(strSite);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult SaveDicMapping(DictionaryModel model)
        {
            BaseActionResult result = new BaseActionResult();

            try
            {
                if (dictionaryService.SaveDicMapping(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetTeachingCategory()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = dictionaryService.GetTeachingCategory();
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult IsTeachingCategoryExists(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string categoryName = CommonGlobalSettings.Utilities.GetParameter("categoryName", parameters);
                string parentID = CommonGlobalSettings.Utilities.GetParameter("parentID", parameters);
                DataSet dataSet = dictionaryService.IsTeachingCategoryExists(parentID, categoryName);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.ReturnString = (dataSet.Tables[0].Rows.Count > 0).ToString();
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddTeachingCategory(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string guid = CommonGlobalSettings.Utilities.GetParameter("guid", parameters);
            string categoryName = CommonGlobalSettings.Utilities.GetParameter("categoryName", parameters);
            string parentID = CommonGlobalSettings.Utilities.GetParameter("parentID", parameters);
            int level = Convert.ToInt32(CommonGlobalSettings.Utilities.GetParameter("level", parameters));
            int orderNo = Convert.ToInt32(CommonGlobalSettings.Utilities.GetParameter("orderNo", parameters));
            #region Added by Blue for RC569, 4/23/2014
            string optionSettingName = CommonGlobalSettings.Utilities.GetParameter("optionSettingName", parameters);
            #endregion
            try
            {
                if (dictionaryService.AddTeachingCategory(guid, categoryName, optionSettingName, parentID, level, orderNo))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult ModifyTeachingCategory(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string categoryName = CommonGlobalSettings.Utilities.GetParameter("categoryName", parameters);
            string guid = CommonGlobalSettings.Utilities.GetParameter("guid", parameters);
            string path = CommonGlobalSettings.Utilities.GetParameter("path", parameters);
            string orginalPath = CommonGlobalSettings.Utilities.GetParameter("orginalPath", parameters);
            #region Added by Blue for RC569, 4/23/2014
            string optionSettingName = CommonGlobalSettings.Utilities.GetParameter("optionSettingName", parameters);
            string originalSettingName = CommonGlobalSettings.Utilities.GetParameter("originalSettingName", parameters);
            #endregion
            try
            {
                if (dictionaryService.ModifyTeachingCategory(guid, categoryName, optionSettingName, originalSettingName, path, orginalPath))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteTeachingCategory(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string guid = CommonGlobalSettings.Utilities.GetParameter("guids", parameters);
            try
            {
                if (dictionaryService.DeleteTeachingCategory(guid))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult IsTeachingCategoryUsedIncludingChildren(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string paths = CommonGlobalSettings.Utilities.GetParameter("paths", parameters);
                DataSet dataSet = dictionaryService.IsTeachingCategoryUsedIncludingChildren(paths);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.ReturnString = (dataSet.Tables[0].Rows.Count > 0).ToString();
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetApplyDoctors()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = new DataSet();
                DataTable dt = dictionaryService.GetApplyDoctors();
                if (dt != null)
                {
                    dataSet.Tables.Add(dt);
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult Do(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string action = CommonGlobalSettings.Utilities.GetParameter("Action", context.Parameters);
                string outMsg = "";
                bool actionResult = dictionaryService.Do(Convert.ToInt32(action), context.Model as BaseDataSetModel, ref outMsg);
                result.Result = true;
                result.DataSetData = (context.Model as BaseDataSetModel).DataSetParameter;
                result.ReturnMessage = outMsg;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
    }
}
