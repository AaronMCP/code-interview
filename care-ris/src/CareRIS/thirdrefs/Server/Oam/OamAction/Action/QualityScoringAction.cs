using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Business.Oam;
using CommonGlobalSettings;

namespace Server.OamAction.Action
{
    public class QualityScoringAction:BaseAction
    {
        IQualityScoringService qualityScoringService = BusinessFactory.Instance.GetQualityScoringService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            string strRPGuid = CommonGlobalSettings.Utilities.GetParameter("RPGuid", context.Parameters);
            string type = CommonGlobalSettings.Utilities.GetParameter("Type", context.Parameters);
            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
            if (actionName == null || actionName.Equals(""))
            {
                actionName = "QueryList";
            }
            switch (actionName)
            {
                case "QueryList":
                    {
                        return QueryList(context.Parameters);
                    }
                case "SaveAppraise":
                    {
                        return SaveAppraise(bdsm.DataSetParameter);
                    }
                case "GetAppraise":
                    {
                        return GetAppraise(strRPGuid);
                    }
                case "GetSettings":
                    {
                        return GetSettings(context.Parameters);
                    }
                case "SaveSettings":
                    {
                        return SaveSettings(context.Parameters, bdsm.DataSetParameter);
                    }
                case "GetAppraiseNew":
                    {
                        return GetAppraiseNew(strRPGuid, type);
                    }
                case "SaveAppraiseNew":
                    {
                        return SaveAppraiseNew(bdsm.DataSetParameter);
                    }
                case "QueryHistoryList":
                    {
                        return QueryHistoryList(context.Parameters);
                    }
                default:
                    return null;
            }
        }

        private DataSetActionResult QueryList(string strParam)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                qualityScoringService.QueryQualityScoringList(strParam, result);

            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private DataSetActionResult QueryHistoryList(string strParam)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                qualityScoringService.QueryScoringHistoryList(strParam, result);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private DataSetActionResult SaveAppraise(DataSet ds)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                qualityScoringService.SaveAppraise(ds, result);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private DataSetActionResult GetAppraise(string RPGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                qualityScoringService.GetAppraise(RPGuid, result);

            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private DataSetActionResult GetAppraiseNew(string RPGuid, string type)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                qualityScoringService.GetAppraiseNew(RPGuid, type, result);

            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private DataSetActionResult GetSettings(string strParam)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            string version = "";
            result.DataSetData = ds;
            try
            {
                result.ReturnString = qualityScoringService.GetSettings(strParam, ref version);
                result.ReturnMessage = version;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private DataSetActionResult SaveSettings(string strParam, DataSet ds)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds1 = new DataSet();
            result.DataSetData = ds1;
            try
            {
                result.Result = qualityScoringService.SaveSettings(strParam, ds);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private DataSetActionResult SaveAppraiseNew(DataSet ds)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                qualityScoringService.SaveAppraiseNew(ds, result);
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
