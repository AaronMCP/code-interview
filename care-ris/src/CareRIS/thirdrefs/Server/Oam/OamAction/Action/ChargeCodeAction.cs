using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Common.Action;
using Server.Business.Oam;
using CommonGlobalSettings;
using CommonUtilitis = CommonGlobalSettings.Utilities;
using Common.ActionResult;

using LogServer;


namespace Server.OamAction.Action
{
    public class ChargeCodeAction : BaseAction
    {
        private IChargeCodeService chargeCodeService = BusinessFactory.Instance.GetChargeCodeService();
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");

        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                string actionName = CommonUtilitis.GetParameter("ActionName", context.Parameters).ToUpper();

                switch (actionName)
                {
                    case "GETALLCHARGECODE":
                        GetAllChargeCode(context, ref bar);
                        break;
                    case "ADDCHARGECODE":
                        AddChargeCode(context, ref bar);
                        break;
                    case "UPDATECHARGECODE":
                        UpdateChargeCode(context, ref bar);
                        break;
                    case "DELETECHARGECODE":
                        DeleteChargeCode(context, ref bar);
                        break;
                    default:
                        throw new Exception("OAM CharegeCode action mapping error!");
                }
                return bar;
            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
                logger.Error((long)ModuleEnum.Oam_WS, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            return bar;
        }

        private void GetAllChargeCode(Context context, ref BaseActionResult bar)
        {
            DataSet ds = chargeCodeService.GetAllChargeCodes();
            DataSetActionResult dsar = new DataSetActionResult();
            dsar.DataSetData = ds;
            dsar.Result = true;
            bar = dsar;
        }

        private void AddChargeCode(Context context, ref BaseActionResult bar)
        {
            ChargeCodeModel codeModel = context.Model as ChargeCodeModel;
            if (codeModel != null)
            {
                chargeCodeService.AddChargeCode(codeModel);
            }
            else
            {
                throw new Exception("chargeCode parameter is not provided!");
            }
            bar.Result = true;
        }

        private void UpdateChargeCode(Context context, ref BaseActionResult bar)
        {
            ChargeCodeModel codeModel = context.Model as ChargeCodeModel;
            if (codeModel != null)
            {
                chargeCodeService.UpdateChargeCode(codeModel);
            }
            else
            {
                throw new Exception("chargeCode parameter is not provided!");
            }
            bar.Result = true;
        }

        private void DeleteChargeCode(Context context, ref BaseActionResult bar)
        {
            string strChargeCode = CommonUtilitis.GetParameter("ChargeCode", context.Parameters);
            strChargeCode = CommonUtilitis.unescape(strChargeCode);

            chargeCodeService.DeleteChargeCode(strChargeCode);
            bar.Result = true;
        }
    }
}

