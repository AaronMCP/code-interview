using Common.Action;
using Server.Business.Oam;
using CommonGlobalSettings;
using Common.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LogServer;
using System.Windows.Forms;
using Server.Utilities.LogFacility;

namespace Server.OamAction.Action
{
    public class DomainAction : BaseAction
    {
     
        IDomainService domainService = BusinessFactory.Instance.GetDomainService();
        
        public override BaseActionResult Execute(Context context)
        {
            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
            string strParameter = context.Parameters;
            BaseActionResult bar = new BaseActionResult();

            try
            {
                switch (context.MessageName)
                {

                  
                    case "OAM_DomainList":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            domainService.DomainList(dsar);

                        }
                        break;
                    case "OAM_SyncDomainSiteList":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = bdsm.DataSetParameter;
                            bar = dsar as BaseActionResult;

                            domainService.SyncDomainSiteList(dsar);

                        }
                        break;
                    case "OAM_AddDomain":
                        {
                            domainService.AddDomain(bdsm.DataSetParameter.Tables[0], bar);

                        }
                        break;
                    case "OAM_ModifyDomain":
                        {

                            domainService.ModifyDomain(bdsm.DataSetParameter.Tables[0], bar);

                        }
                        break;
                    case "OAM_DelDomain":
                        {
                            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", context.Parameters);
                            domainService.DelDomain(strDomain, bar);

                        }
                        break;
                    case "OAM_AddSite":
                        {
                            domainService.AddSite(bdsm.DataSetParameter.Tables[0], bar);
                        }
                        break;
                    case "OAM_ModifySite":
                        {

                            domainService.ModifySite(bdsm.DataSetParameter.Tables[0], bar);
                        }
                        break;
                    case "OAM_GetSiteProfile":
                        {
                            DataSetActionResult result = new DataSetActionResult();
                            try
                            {
                                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameter);
                                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameter);
                                DataSet dataSet = domainService.GetSiteProfileDataSet(strDomain, strSite);
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
                        break;
                    case "OAM_SaveSiteProfile":
                        {
                            BaseActionResult result = new BaseActionResult();
                            try
                            {
                                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameter);
                                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameter);
                                SystemModel model = context.Model as SystemModel;
                                if (domainService.EditSiteProfile(model, strDomain, strSite))
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
                        break;
                    case "OAM_AddSiteProfile":
                        {
                            BaseActionResult result = new BaseActionResult();
                            try
                            {
                                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameter);
                                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameter);
                                string strFieldName = CommonGlobalSettings.Utilities.GetParameter("FieldName", strParameter);
                                string strModuleID = CommonGlobalSettings.Utilities.GetParameter("ModuleID", strParameter);
                                if (domainService.AddSiteProfile(strDomain, strSite, strFieldName, strModuleID))
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
                        break;
                    case "OAM_DeleteSiteProfile":
                        {
                            BaseActionResult result = new BaseActionResult();
                            try
                            {
                                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameter);
                                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameter);
                                string strFieldName = CommonGlobalSettings.Utilities.GetParameter("FieldName", strParameter);
                                string strModuleID = CommonGlobalSettings.Utilities.GetParameter("ModuleID", strParameter);
                                if (domainService.DeleteSiteProfile(strDomain, strSite, strFieldName, strModuleID))
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
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                bar.ReturnMessage = ex.Message;
                bar.Result = false;
                bar.recode = -1;

         

            }
            return bar;
        }
    }
}
