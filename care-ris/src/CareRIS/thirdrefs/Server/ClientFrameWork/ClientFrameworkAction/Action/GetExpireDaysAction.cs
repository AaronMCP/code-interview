using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.ClientFramework.Common.Data.Profile;
using Common.ActionResult.Framework;
using Server.ClientFramework.Common;
using Server.Business.ClientFramework;

namespace Server.ClientFrameworkAction.Action
{
    public class GetExpireDaysAction : BaseAction
    {
       
        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult result = new BaseActionResult();
            Login login = new Login();
            KeyValuePair<int,int> re = login.GetExpireDays(context.Parameters);
            if (re.Key == 3)
            {
                result.Result = true;
                result.ReturnString = re.Value.ToString();
            }
            else
            {
                result.Result = false;
                
            }
            return result;
        }
    }
}