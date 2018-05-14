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
    public class LoginAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult result = new BaseActionResult();
            Login obj = new Login();
            string szTime = obj.GetDbServerTime();
            if (szTime.Length > 0)
            {
                result.Result = true;
                result.ReturnMessage = szTime;
            }
            else
            {
                result.Result = false;
                result.ReturnMessage = "";
            }

            return result;
        }
    }
}
