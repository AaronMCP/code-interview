using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.Oam;
using Common.ActionResult.Framework;
using Server.Business.Oam;

namespace Server.OamAction.Action
{
    public class KeyPerformanceRatingAction : BaseAction
    {
        IKeyPerformanceRatingService KeyPerformanceRatingService = BusinessFactory.Instance.GetKeyPerformanceRating();

        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();
            result.DataSetData = (context.Model as BaseDataSetModel).DataSetParameter;
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            if (actionName == null || actionName.Equals(""))
            {
                actionName = "GetRatingPoolList";
            }

            try
            {

                switch (actionName)
                {
                    case "NewRatingPool":
                        KeyPerformanceRatingService.NewRatingPool(result);
                        break;
                    case "GetRatingPoolList":
                        KeyPerformanceRatingService.GetRatingPoolList(result);
                        break;
                    case "GetUnRatingAppraisers":
                        KeyPerformanceRatingService.GetUnRatingAppraisers(result);
                        break;
                    case "GetRatingPoolInfo":
                        KeyPerformanceRatingService.GetRatingPoolInfo(result);
                        break;
                    case "SetScore":
                        KeyPerformanceRatingService.SetScore(result);
                        break;
                    case "CreateSelectedCases":
                        KeyPerformanceRatingService.CreateTeaching(result);
                        break;
                    default:
                        KeyPerformanceRatingService.GetRatingPoolList(result);
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
    }
}
