using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.Oam;
using Common.ActionResult.Framework;

namespace Server.OamAction.Action
{
    public class RandomInspectionAction : BaseAction
    {
        IRandomInspectionService RandomInspectionService = BusinessFactory.Instance.GetRandomInspection();

        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();
            result.DataSetData = (context.Model as BaseDataSetModel).DataSetParameter;
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            if (actionName == null || actionName.Equals(""))
            {
                actionName = "GetRandomInspectionPoolList";
            }

            try
            {

                switch (actionName)
                {
                    case "NewRandomInspectionPool":
                        RandomInspectionService.NewRandomInspectionPool(result);
                        break;
                    case "GetRandomInspectionPoolList":
                        RandomInspectionService.GetRandomInspectionPoolList(result);
                        break;
                    case "GetRandomInspectionPoolListByUser":
                        RandomInspectionService.GetRandomInspectionPoolListByUser(result);
                        break;
                    case "GetRandomInspectionPoolInfo":
                        RandomInspectionService.GetRandomInspectionPoolInfo(result);
                        break;
                    case "DeleteInspectionPool":
                        return DeleteInspectionPool(context.Parameters);
                        break;
                    case "InspectionSetScore":
                        RandomInspectionService.InspectionSetScore(result);
                        break;
                    case "GetScoreCardSettings":
                        return GetScoreCardSetting(context.Parameters);
                    default:
                        RandomInspectionService.GetRandomInspectionPoolList (result);
                        break;
                }
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.ReturnMessage = ex.Message;
                result.Result = false;
                result.recode = -1;
            }

            return result;
        }

        private DataSetActionResult GetScoreCardSetting(string strParam)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            string version = "";
            result.DataSetData = ds;
            try
            {
                result.ReturnString = RandomInspectionService.GetScoreCardSettings(strParam, ref version);
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

        private DataSetActionResult DeleteInspectionPool(string strParam)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.ReturnString = RandomInspectionService.DeleteInspectionPool(strParam);
                result.Result = true;
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
