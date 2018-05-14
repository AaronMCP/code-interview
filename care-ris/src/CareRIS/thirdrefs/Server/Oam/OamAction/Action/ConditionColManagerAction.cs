using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Business.Oam;

namespace Server.OamAction.Action
{
    public class ConditionColManagerAction : BaseAction
    {
        private IConditionColService conditionColService = BusinessFactory.Instance.GetConditionColService();
        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            switch (actionName)
            {
                case "GetAllConditionItems":
                    return GetAllConditionItems();
                case "SaveColChange":
                    return SaveColChange(context.Model as BaseDataSetModel);
                case "GetAllExclusionConditions":
                    return GetAllExclusionConditions();
                case "GetOperatorMap":
                    return GetOperatorMap();
                case "GetDataSource":
                    string sql = context.Parameters.Substring(context.Parameters.IndexOf("sql") + 4);
                    return GetDataSource(sql);
                case "SaveExclusionCondition":
                    string conditionName = CommonGlobalSettings.Utilities.GetParameter("conditionName", context.Parameters);
                    string domain = CommonGlobalSettings.Utilities.GetParameter("domain", context.Parameters);
                    string site = CommonGlobalSettings.Utilities.GetParameter("Site", context.Parameters);
                    return SaveExclusionCondition((context.Model as BaseDataSetModel).DataSetParameter.Tables[0], conditionName, domain, site);
                case "GetExclusionConditionSqlByPanelTitle":
                    string title = CommonGlobalSettings.Utilities.GetParameter("title", context.Parameters);
                    return GetExclusionConditionSqlByPanelTitle(title);
                case "GetAllHotKeys":
                    return GetAllHotKeys();
                case "SaveHotKeys":
                    return SaveHotKeys(context.Model as BaseDataSetModel);
                case "GetAllMessageConfigs":
                    return GetAllMessageConfigs();
                case "SaveMessageConfigs":
                    return SaveMessageConfigs(context.Model as BaseDataSetModel);
            }
            return base.Execute(context);
        }


        private BaseActionResult GetAllConditionItems()
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds=conditionColService.GetAllConditionItems();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsar.Result = true;
                    dsar.DataSetData = ds;
                }
                else
                {
                    dsar.Result = false;
                }
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult SaveColChange(BaseDataSetModel model)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                if (model == null || model.DataSetParameter.Tables.Count < 1)
                {
                    bar.Result = false;
                    bar.ReturnMessage = "";
                    return bar;
                }
                bar.Result = conditionColService.SaveColChange(model);

            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private BaseActionResult GetAllExclusionConditions()
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = conditionColService.GetAllExclusionConditions();
                dsar.Result = true;
                dsar.DataSetData = ds;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult GetOperatorMap()
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = conditionColService.GetOperatorMap();
                dsar.Result = true;
                dsar.DataSetData = ds;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult GetDataSource(string sql)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = conditionColService.GetDataSource(sql);
                dsar.Result = true;
                dsar.DataSetData = ds;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult SaveExclusionCondition(DataTable dt, string conditionName, string domain, string site)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                if (dt == null)
                {
                    bar.Result = false;
                    bar.ReturnMessage = "";
                    return bar;
                }
                bar.Result = conditionColService.SaveExclusionCondition(dt, conditionName, domain, site);

            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private BaseActionResult GetExclusionConditionSqlByPanelTitle(string title)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = conditionColService.GetExclusionConditionSqlByPanelTitle(title);
                dsar.Result = true;
                dsar.DataSetData = ds;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult GetAllHotKeys()
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = conditionColService.GetAllHotKeys();
                dsar.Result = true;
                dsar.DataSetData = ds;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult SaveHotKeys(BaseDataSetModel model)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                if (model == null || model.DataSetParameter.Tables.Count < 1)
                {
                    bar.Result = false;
                    bar.ReturnMessage = "";
                    return bar;
                }
                bar.Result = conditionColService.SaveHotKeys(model);

            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private BaseActionResult GetAllMessageConfigs()
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = conditionColService.GetAllMessageConfigs();
                dsar.Result = true;
                dsar.DataSetData = ds;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult SaveMessageConfigs(BaseDataSetModel model)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                if (model == null || model.DataSetParameter.Tables.Count < 1)
                {
                    bar.Result = false;
                    bar.ReturnMessage = "";
                    return bar;
                }
                bar.Result = conditionColService.SaveMessageConfigs(model);

            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }
    }
}
