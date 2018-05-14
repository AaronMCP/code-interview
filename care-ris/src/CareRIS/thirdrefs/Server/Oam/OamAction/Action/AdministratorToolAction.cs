using Common.Action;
using Server.Business.Oam;
using CommonGlobalSettings;
using Common.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LogServer;
using System.Windows.Forms;
using Server.Utilities.LogFacility;

namespace Server.OamAction.Action
{
    public class AdministratorToolAction : BaseAction
    {
        private IAdministratorToolService atService = BusinessFactory.Instance.GetAdministratorToolService();
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");
        public override BaseActionResult Execute(Context context)
        {
            //BaseDataSetModel bdsm = context.Model as BaseDataSetModel;

            BaseActionResult bar = new BaseActionResult();

            try
            {
                switch (context.MessageName)
                {

                    case "OAM_QueryLock":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            string strRegBeginDt = CommonGlobalSettings.Utilities.GetParameter("BeginTime", context.Parameters);
                            string strRegEndDt = CommonGlobalSettings.Utilities.GetParameter("EndTime", context.Parameters);
                            string strOwner = CommonGlobalSettings.Utilities.GetParameter("Owner", context.Parameters);

                            bar = dsar as BaseActionResult;
                            atService.QueryLock(strOwner, strRegBeginDt, strRegEndDt, bar);
                        }
                        break;

                    case "OAM_UnLock":
                        {
                            bar = new BaseActionResult();
                            string strSyncType = CommonGlobalSettings.Utilities.GetParameter("SyncType", context.Parameters);
                            string strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            string strOwner = CommonGlobalSettings.Utilities.GetParameter("Owner", context.Parameters);
                            string strObjectType = CommonGlobalSettings.Utilities.GetParameter("ObjectType", context.Parameters);
                            int nObjectType = Convert.ToInt32(strObjectType);
                            int nSyncType = Convert.ToInt32(strSyncType);
                            atService.UnLock(nObjectType, nSyncType, strOrderGuid, strOwner, bar);
                        }
                        break;

                    case "OAM_QueryOnline":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                     
                            bar = dsar as BaseActionResult;
                            atService.QueryOnline(bar);
                        }
                        break;

                    case "OAM_SetOffline":
                        {
                            bar = new BaseActionResult();
                            string strUserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", context.Parameters);
                            atService.SetOffline(strUserGuid, bar);
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

                logger.Error((long)ModuleEnum.DataCenter_WS, ModuleInstanceName.DataCenter, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            return bar;
        }
    }
}
