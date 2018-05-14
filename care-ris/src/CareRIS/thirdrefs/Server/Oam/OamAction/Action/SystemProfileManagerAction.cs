using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Business.Oam;

namespace Server.OamAction.Action
{
    public class SystemProfileManagerAction : BaseAction
    {
        ISystemProfileService systemProfileService = BusinessFactory.Instance.GetSystemProfileService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            string strListName = CommonGlobalSettings.Utilities.GetParameter("listName", context.Parameters);

            if (actionName == null || actionName.Equals(""))
            {
                actionName = "list";
            }

            switch (actionName)
            {
                case "list":
                    return List(context.Parameters);
                case "editSystemProfile":
                    return EditSystemProfile(context.Model as SystemModel, context.Parameters);
                case "GetAllWarningTimeSet":
                    return GetAllWarningTimeSet();
                case "GetWarningTimeSelectConditionSet":
                    return GetWarningTimeSelectConditionSet();
                case "AddNewWarningTime":
                    return AddNewWarningTime(context.Model as SystemModel);
                case "DeleteWarningTime":
                    return DeleteWarningTime(context.Model as SystemModel);
                case "UpdateWarningTime":
                    return UpdateWarningTime(context.Model as SystemModel);
                case "GetAllGridColumnOptionListNames":
                    return GetAllGridColumnOptionListNames();
                case "GetAllGridColumnOptionRows":
                    return GetAllGridColumnOptionRows(strListName);
                case "UpdateGridColumnOptionTable":
                    return UpdateGridColumnOptionTable(context.Model as SystemModel,strListName);
            }

            //default
            return List(context.Parameters);
        }

        private BaseActionResult List(string strParameter)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameter);
                DataSet dataSet = systemProfileService.GetSystemProfileDataSet(strDomain);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }


        private BaseActionResult EditSystemProfile(SystemModel model,string strParameter)
        {

            BaseActionResult result = new BaseActionResult();
            try
            {
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameter);
                if (systemProfileService.EditSystemProfile(model, strDomain))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;

        }

        #region GCRIS 2.0 CPE Part Number: 7H1634
        private BaseActionResult GetAllWarningTimeSet()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = systemProfileService.GetAllWarningTimeSet();
                //EK_HI00061788
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetWarningTimeSelectConditionSet()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = systemProfileService.GetWarningTimeSelectConditionSet();
                //EK_HI00061788
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddNewWarningTime(SystemModel oModel)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = systemProfileService.AddNewWarningTime(oModel);
                if (bResult)
                {
                    result.Result = bResult;
                    result.DataSetData = null;
                }
                else
                {
                    result.Result = !bResult;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteWarningTime(SystemModel oModel)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = systemProfileService.DeleteWarningTime(oModel);
                if (bResult)
                {
                    result.Result = bResult;
                    result.DataSetData = null;
                }
                else
                {
                    result.Result = !bResult;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult UpdateWarningTime(SystemModel oModel)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = systemProfileService.UpdateWarningTime(oModel);
                if (bResult)
                {
                    result.Result = bResult;
                    result.DataSetData = null;
                }
                else
                {
                    result.Result = !bResult;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetAllGridColumnOptionListNames()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = systemProfileService.GetAllGridColumnOptionListNames();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
        private BaseActionResult GetAllGridColumnOptionRows(string strListName)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = systemProfileService.GetAllGridColumnOptionRows(strListName);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult UpdateGridColumnOptionTable(SystemModel oModel,string strListName)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = systemProfileService.UpdateGridColumnOptionTable(oModel, strListName);
                if (bResult)
                {
                    result.Result = bResult;
                    result.DataSetData = null;
                }
                else
                {
                    result.Result = !bResult;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
        #endregion

    }
}
