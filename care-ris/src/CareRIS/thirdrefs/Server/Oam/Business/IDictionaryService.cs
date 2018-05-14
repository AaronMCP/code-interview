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
using CommonGlobalSettings;
using CommonGlobalSettings;

namespace Server.Business.Oam
{
    public interface IDictionaryService
    {
        bool SavePhysicalCompany(BaseDataSetModel model);
        DataSet GetPhysicalCompany(string parameters);
        DataSet GetDictionaryDataSet(string strSite);
        DataSet GetDictionaryVaild(int DicTag);
        bool ModifyDictionaryDefaultValue(DictionaryModel model);
        DataSet CustomDictionaryItems(string tag, string strSite);
        DataSet DeleteDictionaryItems(string tag, string strSite);
        DataSet GetSiteDictionaryItems(string tag, string strSite);
        bool AddDictionaryValue(string tag, string codeValue, string codeDescription, string shortcutCode, string strDomain, string strSite);
        bool ModifyDictionaryValue(string tag, string oldCodeValue, string newCodeValue, string newDescription, string shortcutCode, string dictionaryValues, string strSite);
        bool DeleteDictionaryValue(string tag, string codeValue, string strSite);
        DataSet GetDicMappingDataset(string strSite);
        bool SaveDicMapping(DictionaryModel model);
        DataSet GetTeachingCategory();
        DataSet IsTeachingCategoryExists(string parentID, string categoryName);
        #region Modified by Blue for RC569, 4/23/2014
        bool AddTeachingCategory(string guid, string categoryName, string optionSettingName, string parentID, int level, int orderNo);
        bool ModifyTeachingCategory(string guid, string categoryName, string optionSettingName, string originalSettingName, string path, string orginalPath);
        #endregion
        #region Added by Blue for [RC507] - US16620
        DataTable GetEventTypes();
        bool UpdateEventTypes(DataTable datasource);
        DataTable GetEventCategories();
        #endregion
        bool DeleteTeachingCategory(string guid);
        DataSet IsTeachingCategoryUsedIncludingChildren(string paths);
        DataTable GetApplyDoctors();
        bool Do(int dbAction, BaseDataSetModel model, ref string dbActionMsg);
        #region Added by Blue for [RC607] - US17706
        DataTable GetExamNameByDomainSite(string domainName, string siteName, bool isDomain);
        DataTable GetCurrentDomainSiteExamName(string modalityType);
        bool AddExamName(string examName, string parentGuid, string domain, string site);
        bool EditExamName(string guid, string examName);
        bool DeleteExamName(string guid);
        DataTable GetScanningTechByDomainSite(string modalityType, string domainName, string siteName, bool isDomain);
        DataTable GetCurrentDomainSiteScanningTech(string modalityType);
        bool AddScanningTech(string scanningTech, string parentGuid, string modalityType, string domain, string site);
        bool EditScanningTech(string guid, string scanningTech);
        bool DeleteScanningTech(string guid);
        #endregion
    }
}
