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

using Server.Utilities.LogFacility;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace Server.ClientFrameworkAction.Action
{
    class DomainAction : BaseAction
    {
        LogManagerForServer logger = new LogManagerForServer("FrameworkServerLoglevel", "0100");
        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult bar = new BaseActionResult();
            bar.Result = false;
          
            try
            {

                switch (context.MessageName.Trim())
                {

                    case "GetCurDomain":
                        bar.ReturnString = ConfigurationManager.AppSettings["Domain"];
                        bar.Result = true;
                        break;  
                    case "GetCurSite":
                        SiteBusiness sb = new SiteBusiness();
                        bar.ReturnString = sb.GetSite(ConfigurationManager.AppSettings["Site"]);
                        if (string.IsNullOrWhiteSpace(bar.ReturnString))
                        {
                            bar.ReturnString = ConfigurationManager.AppSettings["Site"];
                        }
                        bar.Result = true;
                        break;  
                    default:
                        bar.ReturnMessage = null;
                        
                        break;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Framework_WS, ModuleInstanceName.Framework, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                bar.ReturnMessage = null;
               
             

            }
            return bar;

        }
    }
    
}
