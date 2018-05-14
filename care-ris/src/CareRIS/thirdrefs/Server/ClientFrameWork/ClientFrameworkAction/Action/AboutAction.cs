using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Utilities.LogFacility;
using Common.ActionResult;
using Common.Action;
using Common.ActionResult.Framework;
using Server.Business.ClientFramework;
using CommonGlobalSettings;
using System.Windows.Forms;


namespace Server.ClientFrameworkAction.Action
{
    public class AboutAction : BaseAction
    {
        LogManagerForServer lm = new LogManagerForServer("FrameworkServerLoglevel", "0100");
        About AboutBusiness =new About();
        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult bar = new BaseActionResult();

            try
            {
                switch (context.MessageName)
                {

                    case "About_VersionThanks":
                        {
                            ArrayActionResult result = new ArrayActionResult();
                            bar = result as BaseActionResult;

                            AboutBusiness.GetAboutInfo(result);

                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                bar.ReturnMessage = ex.Message;
                bar.Result = false;
                bar.recode = -1;

                lm.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            return bar;
        }
    }
}
