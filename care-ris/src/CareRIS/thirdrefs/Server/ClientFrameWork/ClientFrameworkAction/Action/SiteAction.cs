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


namespace Server.ClientFrameworkAction.Action
{
  

    public class SiteAction : BaseAction
    {

        DataSetActionResult bar = new DataSetActionResult();
         LogManagerForServer logger = new LogManagerForServer("FrameworkServerLoglevel", "0100");
         public override BaseActionResult Execute(Context context)
         {
           
             bar.DataSetData = new DataSet();
             try
             {

                 switch (context.MessageName.Trim())
                 {

                     case "GetFilterSite":
                         {
                             string UserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", context.Parameters);
                             string RoleName = CommonGlobalSettings.Utilities.GetParameter("RoleName", context.Parameters);
                             string CurSite = CommonGlobalSettings.Utilities.GetParameter("CurSite", context.Parameters);
                             string MatchingName = CommonGlobalSettings.Utilities.GetParameter("MatchingName", context.Parameters);
                             SiteBusiness sb = new SiteBusiness();
                             bar.DataSetData = sb.GetFilterSite(UserGuid, RoleName, CurSite, MatchingName);
                             bar.Result = true;
                             break;
                         }
                     default:
                         {
                             bar.ReturnMessage = null;
                             bar.Result = false;
                             break;
                         }

                 }
             }
             catch (Exception e)
             {
                 logger.Error((long)ModuleEnum.Framework_WS, ModuleInstanceName.Framework, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                 bar.ReturnMessage = null;
                 bar.Result = false;
                 return bar;

             }
             return bar;
         }
    }
}
