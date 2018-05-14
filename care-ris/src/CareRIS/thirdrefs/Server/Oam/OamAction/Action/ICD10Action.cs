using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Common.Action;
using CommonGlobalSettings;
using Server.Business.Oam;
using CommonGlobalSettings;
using Common.ActionResult;

using Server.Utilities.LogFacility;

namespace Server.OamAction.Action
{
    public class ICD10Action:BaseAction
    {
        private IICD10Service icd10Service = BusinessFactory.Instance.GetICD10Service();
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");

        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dtActionResult = new DataSetActionResult();
            string action = context.MessageName;
            string strParameters = context.Parameters;

            switch (action)
            {
                case "SearchICD10":
                    return SearchICD10(context.Parameters);
                case "GetAllICD10":
                    return GetAllICD10();
                case"DeleteICD10":
                    return DeleteICD10(context.Parameters);
                case "AddICD10":
                    return AddICD10(context.Model as BaseDataSetModel);
                case"ModifyICD10":
                    return ModifyICD10(context.Model as BaseDataSetModel);
                case"ImportICD10":
                    return ImportICD10(context.Model as BaseDataSetModel, strParameters);
                default:
                break;
            }
            return base.Execute(context);
        }

        private BaseActionResult SearchICD10(string parameters)
        {
            DataSetActionResult dar = new DataSetActionResult();
            try
            {
                DataSet ds = icd10Service.SearchICD10(parameters);
                if (ds.Tables.Count > 0)
                {
                    dar.DataSetData = ds;
                    dar.Result = true;
                }
                else
                {
                    dar.Result = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                              (new System.Diagnostics.StackFrame(true)).GetFileName(),
                               Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dar.Result = false;
            }
            return dar;
        }

        private BaseActionResult GetAllICD10()
        {
            DataSetActionResult dar = new DataSetActionResult();
            try
            {
                DataSet ds = icd10Service.GetAllICD10();
                if (ds.Tables.Count > 0)
                {
                    dar.DataSetData = ds;
                    dar.Result = true;
                }
                else
                {
                    dar.Result = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                              (new System.Diagnostics.StackFrame(true)).GetFileName(),
                               Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dar.Result = false;
            }
            return dar;
        }
        private BaseActionResult DeleteICD10(string parameters)
        {
            BaseActionResult bar = new BaseActionResult();
            string id = CommonGlobalSettings.Utilities.GetParameter("ID", parameters);
            try
            {
               
                    bar.Result = icd10Service.DeleteICD10(id);
                    return bar;
               
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                              (new System.Diagnostics.StackFrame(true)).GetFileName(),
                               Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                bar.Result = false;
            }
            return bar;
        }

        private BaseActionResult AddICD10(BaseDataSetModel model)
        {
            BaseActionResult bar = new BaseActionResult();
            if (model == null || model.DataSetParameter.Tables.Count<1)
            {
                bar.Result=false;
                bar.ReturnMessage = "ICD10.Delete" + (bar.Result ? "Success" : "Failure");
                return bar;
            }
            try
            {
                string condition = string.Format("ID='{0}'",model.DataSetParameter.Tables[0].Rows[0]["ID"].ToString());

                DataSet ds = icd10Service.SearchICD10(condition);
                if (ds.Tables.Count > 0)
                {
                    bar.Result = icd10Service.AddICD10(model);
                    return bar;
                }
                else
                {
                    bar.Result = false;
                    bar.ReturnMessage = "ICD10.DuplicateID";
                    return bar;
                }
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                              (new System.Diagnostics.StackFrame(true)).GetFileName(),
                               Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                bar.Result = false;
            }
            return bar;
        }

        private BaseActionResult ModifyICD10(BaseDataSetModel model)
        {
            BaseActionResult bar = new BaseActionResult();
            if (model == null || model.DataSetParameter.Tables.Count < 1)
            {
                bar.Result = false;
                return bar;
            }

            try
            {
                bar.Result = icd10Service.ModifyICD10(model);
                return bar;
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                              (new System.Diagnostics.StackFrame(true)).GetFileName(),
                               Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                bar.Result = false;
            }
            return bar;
        }

        private BaseActionResult ImportICD10(BaseDataSetModel model,string parameter)
        {
            BaseActionResult bar = new BaseActionResult();
            if (model == null || model.DataSetParameter.Tables.Count < 1)
            {
                bar.Result = false;
                return bar;
            }

            try
            {
                bool IsClear;
                if(parameter=="0")
                {
                    IsClear=false;
                }
                else  IsClear=true;

                bar.Result = icd10Service.ImportICD10(model.DataSetParameter, IsClear);
                return bar;
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                             (new System.Diagnostics.StackFrame(true)).GetFileName(),
                              Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                bar.Result = false;
            }
            return bar;
        }
    }
}
