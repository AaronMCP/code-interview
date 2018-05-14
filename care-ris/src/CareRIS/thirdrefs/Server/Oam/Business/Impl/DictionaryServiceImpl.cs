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
using Server.Business.Oam;
using Server.DAO.Oam;
using CommonGlobalSettings;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class DictionaryServiceImpl : IDictionaryService
    {
        private IDictionaryDAO dictonaryDAO = DataBasePool.Instance.GetDBProvider();

        public bool SavePhysicalCompany(BaseDataSetModel model)
        {
            try
            {
                return dictonaryDAO.SavePhysicalCompany(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetPhysicalCompany(string parameters)
        {
            try
            {
                return dictonaryDAO.GetPhysicalCompany(parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetDictionaryVaild(int DicTag)
        {
            try
            {
                return dictonaryDAO.GetDictionaryVaild(DicTag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetDictionaryDataSet(string strSite)
        {
            try
            {
                return dictonaryDAO.GetDictionaryDataSet(strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet CustomDictionaryItems(string tag, string strSite)
        {
            try
            {
                return dictonaryDAO.CustomDictionaryItems(tag, strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet DeleteDictionaryItems(string tag, string strSite)
        {
            try
            {
                return dictonaryDAO.DeleteDictionaryItems(tag, strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetSiteDictionaryItems(string tag, string strSite)
        {
            try
            {
                return dictonaryDAO.GetSiteDictionaryItems(tag, strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModifyDictionaryDefaultValue(DictionaryModel model)
        {
            try
            {
                return dictonaryDAO.ModifyDictionaryDefaultValue(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddDictionaryValue(string tag, string codeValue, string codeDescription, string shortcutCode, string strDomain, string strSite)
        {
            try
            {
                return dictonaryDAO.AddDictionaryValue(tag, codeValue, codeDescription, shortcutCode, strDomain,strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModifyDictionaryValue(string tag, string oldCodeValue, string newCodeValue, string newDescription, string shortcutCode, string dictionaryValues, string strSite)
        {
            try
            {
                return dictonaryDAO.ModifyDictionaryValue(tag, oldCodeValue, newCodeValue, newDescription, shortcutCode, dictionaryValues,strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteDictionaryValue(string tag, string codeValue, string strSite)
        {
            try
            {
                return dictonaryDAO.DeleteDictionaryValue(tag, codeValue, strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetDicMappingDataset(string strSite)
        {
            try
            {
                return dictonaryDAO.GetDicMappingDataset(strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SaveDicMapping(DictionaryModel model)
        {
            try
            {
                return dictonaryDAO.SaveDicMapping(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetTeachingCategory()
        {
            try
            {
                return dictonaryDAO.GetTeachingCategory();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet IsTeachingCategoryExists(string parentID, string categoryName)
        {
            try
            {
                return dictonaryDAO.IsTeachingCategoryExists(parentID, categoryName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddTeachingCategory(string guid, string categoryName, string optionSettingName, string parentID, int level, int orderNo)
        {
            try
            {
                return dictonaryDAO.AddTeachingCategory(guid, categoryName, optionSettingName, parentID, level, orderNo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModifyTeachingCategory(string guid, string categoryName, string optionSettingName, string originalSettingName, string path, string orginalPath)
        {
            try
            {
                return dictonaryDAO.ModifyTeachingCategory(guid, categoryName, optionSettingName, originalSettingName, path, orginalPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteTeachingCategory(string guid)
        {
            try
            {
                return dictonaryDAO.DeleteTeachingCategory(guid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet IsTeachingCategoryUsedIncludingChildren(string paths)
        {
            try
            {
                return dictonaryDAO.IsTeachingCategoryUsedIncludingChildren(paths);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetApplyDoctors()
        {
            try
            {
                return dictonaryDAO.GetApplyDoctors();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Do(int dbAction, BaseDataSetModel model, ref string dbActionMsg)
        {
            try
            {
                return dictonaryDAO.Do(dbAction, model, ref dbActionMsg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Added by Blue for [RC607] - US17706
        public DataTable GetExamNameByDomainSite(string domainName, string siteName, bool isDomain)
        {
            try
            {
                return dictonaryDAO.GetExamNameByDomainSite(domainName, siteName, isDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetCurrentDomainSiteExamName(string modalityType)
        {
            try
            {
                return dictonaryDAO.GetCurrentDomainSiteExamName(modalityType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddExamName(string examName, string parentGuid, string domain, string site)
        {
            try
            {
                return dictonaryDAO.AddExamName(examName, parentGuid, domain, site);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditExamName(string guid, string examName)
        {
            try
            {
                return dictonaryDAO.EditExamName(guid, examName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteExamName(string guid)
        {
            try
            {
                return dictonaryDAO.DeleteExamName(guid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetScanningTechByDomainSite(string modalityType, string domainName, string siteName, bool isDomain)
        {
            try
            {
                return dictonaryDAO.GetScanningTechByDomainSite(modalityType, domainName, siteName, isDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetCurrentDomainSiteScanningTech(string modalityType)
        {
            try
            {
                return dictonaryDAO.GetCurrentDomainSiteScanningTech(modalityType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddScanningTech(string scanningTech, string parentGuid, string modalityType, string domain, string site)
        {
            try
            {
                return dictonaryDAO.AddScanningTech(scanningTech, parentGuid, modalityType, domain, site);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditScanningTech(string guid, string scanningTech)
        {
            try
            {
                return dictonaryDAO.EditScanningTech(guid, scanningTech);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteScanningTech(string guid)
        {
            try
            {
                return dictonaryDAO.DeleteScanningTech(guid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Added by Blue for [RC507] - US16620
        public DataTable GetEventTypes()
        {
            try
            {
                return dictonaryDAO.GetEventTypes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateEventTypes(DataTable datasource)
        {
            try
            {
                return dictonaryDAO.UpdateEventTypes(datasource);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetEventCategories()
        {
            try
            {
                return dictonaryDAO.GetEventCategories();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
