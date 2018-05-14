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
    public class BillBoardAction : BaseAction
    {
        LogManagerForServer logger = new LogManagerForServer("FrameworkServerLoglevel", "0100");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {

                switch (context.MessageName.Trim())
                {

                    case "GetAllNotesInDB":
                        string userId = CommonGlobalSettings.Utilities.GetParameter("UserGuid", context.Parameters);
                        string roleName = CommonGlobalSettings.Utilities.GetParameter("Role", context.Parameters);
                        return GetAllNotesInDB(userId, roleName);
                    case "GetBillBoardDictionaryData":
                        return GetBillBoardDictionaryData();

                    default:
                        {
                            dsAr.ReturnMessage = null;
                            dsAr.Result = false;
                            return dsAr;
                        }

                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Framework_WS, ModuleInstanceName.Framework, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dsAr.ReturnMessage = null;
                dsAr.Result = false;
                return dsAr;

            }

        }
        private BaseActionResult GetAllNotesInDB(string userguid, string roleName)
        {
            DataSetActionResult result = new DataSetActionResult();
            BillBoard billBoard = new BillBoard();
            DataSet myDataSet = billBoard.GetAllNotesInDB(userguid, roleName);
            if (myDataSet != null && myDataSet.Tables != null && myDataSet.Tables.Count == 2)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult GetBillBoardDictionaryData()
        {
            DataSetActionResult result = new DataSetActionResult();
            BillBoard billBoard = new BillBoard();
            DataSet myDataSet = billBoard.GetBillBoardDictionaryData();
            if (myDataSet != null && myDataSet.Tables != null && myDataSet.Tables.Count != 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

    }
}
