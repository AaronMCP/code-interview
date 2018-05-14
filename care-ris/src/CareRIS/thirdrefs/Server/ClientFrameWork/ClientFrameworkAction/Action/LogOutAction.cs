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
    public class LogOutAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {

            BaseActionResult result = new BaseActionResult();
            result.Result = true;
            Login obj = new Login() ;

            if(context.ClientType == "1")
                //from web client
            {
                obj.LogOut(context.Parameters, true);
            }
            else
                //from smart client
            {
                obj.LogOut(context.Parameters, false);
            }
            
            return result;
        }
    }
}
