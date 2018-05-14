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
using Server.Business.Oam;
using CommonGlobalSettings;
using CommonGlobalSettings;

namespace Server.OamAction.Action
{
    public class HippaQueryAction : BaseAction
    {
        private IHippaQueryService hippaQueryService = BusinessFactory.Instance.GetHippaQueryService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            //string actionName = context.MessageName;

            switch (actionName)
            {
                case "HippaQuery":
                    return HippaQuery(context.Model as HippaModel);
                case "getAllRowCount":
                    return getAllRowCount(context.Model as HippaModel);
            }
            
            return null;
        }

        private DataSetActionResult HippaQuery(HippaModel hippaModel)
        {
            DataSetActionResult result = new DataSetActionResult();

            try
            {
                DataSet dataSet = hippaQueryService.HippaQuery(hippaModel);
                
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            
            return result;
        }

        private BaseActionResult getAllRowCount(HippaModel hippModel)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = true;
                int AllRowCount = hippaQueryService.getAllRowCount(hippModel);

                result.ReturnString = Convert.ToString(AllRowCount);
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
