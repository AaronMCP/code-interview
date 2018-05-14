using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Common.ActionResult.Framework;
using Server.Business.Oam;

namespace Server.OamAction.Action
{
    public class LoginSettingsAction : BaseAction
    {

        ILoginSettings LoginSettingsService = BusinessFactory.Instance.GetLoginSettings();

        public override BaseActionResult Execute(Context context)
        {
            LoginSettingsActionResult result = new LoginSettingsActionResult();
            
            result.Model = context.Model as LoginSettingsModel;
            if (result.Model == null) result.Model = new LoginSettingsModel();
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            string site = CommonGlobalSettings.Utilities.GetParameter("site", context.Parameters);
            if (actionName == null || actionName.Equals(""))
            {
                actionName = "LoginInfo";
            }

            try
            {

                switch (actionName)
                {
                    case "LoginInfo":
                        LoginSettingsService.GetLoginInfo(result);
                        break;
                    case "Upload":
                        LoginSettingsService.WriteXML(result,site);
                        break;
                    case "Download":
                        LoginSettingsService.ReadXML(result,site);
                        break;
                    default:
                        LoginSettingsService.GetLoginInfo(result);
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
