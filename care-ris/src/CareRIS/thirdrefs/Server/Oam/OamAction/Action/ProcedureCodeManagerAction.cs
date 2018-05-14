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
using Server.Utilities.Oam;

namespace Server.OamAction.Action
{
    public class ProcedureCodeManagerAction : BaseAction
    {
        private IProcedureCodeService procedureCodeService = BusinessFactory.Instance.GetProcedureCodeService(); 

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            if (actionName == null || actionName.Equals(""))
            {
                actionName = "list";
            }

            switch(actionName)
            {
                case "list":
                    return List(context.Parameters);
                case "add":
                    return Add(context.Model as ProcedureCodeModel);
                case "delete":
                    return Delete(context.Parameters);
                case "modify":
                    return Modify(context.Model as ProcedureCodeModel);
                case "queryExamSystem": //Query by modality
                    return QueryExamSystem(context.Parameters);
                case "queryAllExamSystem":
                    return QueryAllExamSystem();
                case "queryBodyCategory":
                    return QueryBodyCategory(context.Parameters);
                case "addBodyCategory":
                    return AddBodyCategory(context.Parameters);
                case "queryBodyPartExist":
                    return IsBodyPartExist(context.Parameters);
                case "addBodyPart":
                    return AddBodyPart(context.Parameters);
                case "queryBodyPart":
                    return QueryBodyPart(context.Parameters);
                case "getProcTimeSliceDur":
                    return getProcTimeSliceDuration(context.Parameters);
                case "QueryChargeTypeFee":
                    return QueryChargeTypeFee(context.Parameters);
                case "modifyFrequency":
                    return ModifyFrequency(context.Model as ProcedureCodeModel);
                case "Copy2Site":
                    return Copy2Site(context.Parameters);
                case "Delall4Site":
                    return Delall4Site(context.Parameters);
                case "queryCheckingItem":
                    return QueryCheckingItem(context.Parameters);
                case "GetSiteProcedureCode":
                    return GetSiteProcedureCode(context.Parameters);
            }
            
            //Default
            return List(context.Parameters);
        }


        private BaseActionResult getProcTimeSliceDuration(string timeSliceDuration)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                timeSliceDuration = CommonGlobalSettings.Utilities.GetParameter("timeSliceDuration", timeSliceDuration);
                DataSet dataSet = procedureCodeService.GetProceTimeSliceDuration(timeSliceDuration);
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
                result.Result = true;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult List(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);

                DataSet dataSet = procedureCodeService.GetProcedureCodeList(strDomain, strSite);

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

        private BaseActionResult Add(ProcedureCodeModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (procedureCodeService.AddProcedureCode(model))
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

        private BaseActionResult Delete(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string procedureCode = CommonGlobalSettings.Utilities.GetParameter("procedureCode", parameters);
            string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
            try
            {
                int code = procedureCodeService.DeleteProcedureCode(procedureCode,site);

                switch (code)
                {
                    case 1:
                        result.Result = false;
                        result.ReturnMessage = "This code has been used by registration and can't be deleted!";
                        break;
                    case 2:
                        result.Result = false;
                        result.ReturnMessage = "This code has been used by booking and can't be deleted!";
                        break;
                    default:
                        result.Result = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult Modify(ProcedureCodeModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (procedureCodeService.ModifyProcedureCode(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                    result.ReturnMessage = "Procedure code {0} does not exist.";
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QueryExamSystem(string parameters)
        {
            string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
            string bodyPart = CommonGlobalSettings.Utilities.GetParameter("bodyPart", parameters);
            DataSetActionResult result = new DataSetActionResult();

            try
            {
                DataSet dataSet = procedureCodeService.QueryExamSystem(modalityType, bodyPart);
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

        private DataSetActionResult QueryAllExamSystem()
        {
            DataSetActionResult result = new DataSetActionResult();

            try
            {
                DataSet dataSet = procedureCodeService.QueryAllExamSystem();
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

        private BaseActionResult QueryBodyCategory(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string categoryName = CommonGlobalSettings.Utilities.GetParameter("categoryName", parameters);
            string description = CommonGlobalSettings.Utilities.GetParameter("description", parameters);
            string shortcutCode = CommonGlobalSettings.Utilities.GetParameter("shortcutCode", parameters);
            try
            {
                string resultString = "result=" + procedureCodeService.QueryBodyCategory(categoryName,
                    description, shortcutCode).ToString();
                result.Result = true;
                result.ReturnString = resultString;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddBodyCategory(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            //string tag = TableConst.BodyCategoryTag;
            //string categoryName = CommonGlobalSettings.Utilities.GetParameter("categoryName", parameters);
            //string description = CommonGlobalSettings.Utilities.GetParameter("description", parameters);
            //string shortcutCode = CommonGlobalSettings.Utilities.GetParameter("shortcutCode", parameters);

            //try
            //{
            //    if (procedureCodeService.AddBodyCategory(tag, categoryName, description, shortcutCode))
            //    {
            //        result.Result = true;
            //    }
            //    else
            //    {
            //        result.Result = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.Result = false;
            //    result.ReturnMessage = ex.Message;
            //}

            return result;
        }

        private BaseActionResult IsBodyPartExist(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
            string bodyPart = CommonGlobalSettings.Utilities.GetParameter("bodyPart", parameters);
            string examSystem = CommonGlobalSettings.Utilities.GetParameter("examSystem", parameters);

            try
            {
                string resultString = "result=" + procedureCodeService.IsBodyPartExist(modalityType, bodyPart, examSystem).ToString();
                result.Result = true;
                result.ReturnString = resultString;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddBodyPart(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
            string bodyPart = CommonGlobalSettings.Utilities.GetParameter("bodyPart", parameters);
            string examSystem = CommonGlobalSettings.Utilities.GetParameter("examSystem", parameters);
            string domain = CommonGlobalSettings.Utilities.GetParameter("domain", parameters);

            try
            {
                if (procedureCodeService.AddBodyPart(modalityType, bodyPart, examSystem, domain))
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

        private DataSetActionResult QueryBodyPart(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);
            string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
            try
            {
                DataSet dataSet = procedureCodeService.QueryBodyPart(modalityType, site);

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

        private DataSetActionResult QueryChargeTypeFee(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string procedureCode = CommonGlobalSettings.Utilities.GetParameter("procedureCode", parameters);

            try
            {
                DataSet dataSet = procedureCodeService.QueryChargeTypeFee(procedureCode);

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

        private BaseActionResult ModifyFrequency(ProcedureCodeModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (procedureCodeService.ModifyProcedureCodeFrequency(model))
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

        private BaseActionResult Copy2Site(string parameters)
        {
            string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
            string domain = CommonGlobalSettings.Utilities.GetParameter("domain", parameters);
            BaseActionResult result = new BaseActionResult();

            try
            {
                result.Result = procedureCodeService.Copy2Site(site, domain);
               
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult Delall4Site(string parameters)
        {
            string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
            string domain = CommonGlobalSettings.Utilities.GetParameter("domain", parameters);
            BaseActionResult result = new BaseActionResult();

            try
            {
                result.Result = procedureCodeService.Delall4Site(site, domain);

            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QueryCheckingItem(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string modalityType = CommonGlobalSettings.Utilities.GetParameter("modalityType", parameters);

            try
            {
                DataSet dataSet = procedureCodeService.QueryCheckingItem(modalityType);

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

        private DataSetActionResult GetSiteProcedureCode(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string site = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);

            try
            {
                DataSet dataSet = procedureCodeService.GetSiteProcedureCode(site);

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
    }
}
