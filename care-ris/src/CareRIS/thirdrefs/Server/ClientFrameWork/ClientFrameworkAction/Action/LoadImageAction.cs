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
    class LoadImageAction : BaseAction
    {

        LogManagerForServer logger = new LogManagerForServer("FrameworkServerLoglevel", "0100");
        public override BaseActionResult Execute(Context context)
        {
            LoadImageBusiness loadimageBusiness = new LoadImageBusiness();
            DataSetActionResult dsar = new DataSetActionResult();
            dsar.Result = false;

            try
            {

                switch (context.MessageName.Trim())
                {

                    case "GetExamInfo":
                        {

                            string strExamDomain = CommonGlobalSettings.Utilities.GetParameter("ExamDomain", context.Parameters);
                            string strAccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", context.Parameters);

                            dsar.DataSetData = loadimageBusiness.GetExamInfo(strExamDomain,strAccNo);
                            dsar.Result = true;
                        }
                        break;
                    default:
                        dsar.ReturnMessage = null;

                        break;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Framework_WS, ModuleInstanceName.Framework, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dsar.ReturnMessage = e.Message;



            }
            return dsar;

        }
    }
}
