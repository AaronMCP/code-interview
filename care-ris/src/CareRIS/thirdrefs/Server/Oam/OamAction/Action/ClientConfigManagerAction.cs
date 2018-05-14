using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Business.Oam;

namespace Server.OamAction.Action
{
    public class ClientConfigManagerAction : BaseAction
    {
        IClientConfigService clientConfigService = BusinessFactory.Instance.GetClientConfigService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            if (actionName == null || actionName.Equals(""))
            {
                actionName = "list";
            }

            switch (actionName)
            {
                case "list":
                    return List();
            }

            //default
            return List();
        }

        private BaseActionResult List()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = clientConfigService.GetClientConfigDataSet();
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
