using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;

namespace Server.ClientFrameworkAction.Action
{
    public class LogOutBySessionIdAction : BaseAction
    {
        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult bar = new BaseActionResult();
            bar.Result = true;

            string strSessionId = CommonGlobalSettings.Utilities.GetParameter("SessionId", context.Parameters);
            Login login = new Login();
            login.LogOutBySessionID(strSessionId);

            return bar;
        }
    }
}
