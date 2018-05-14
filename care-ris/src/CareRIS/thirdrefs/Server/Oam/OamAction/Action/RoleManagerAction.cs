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
    public class RoleManagerAction : BaseAction
    {
        IRoleService roleService = BusinessFactory.Instance.GetRoleService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);



            if (actionName == null || actionName.Equals(""))
            {
                actionName = "list";
            }

            switch (actionName)
            {
                case "list":
                    string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", context.Parameters);
                    return List(strDomain);
                case "roleProfile":
                    return GetRoleProf(context.Parameters);
                case "addRole":
                    return AddRole(context.Parameters);
                case "editRole":
                    return EditRole(context.Model as RoleModel, context.Parameters);
                case "copyRole":
                    return CopyRole(context.Parameters);
                case "delete":
                    return Delete(context.Parameters);

                case "GetRoleDir":
                    return GetRoleDir(context.Parameters);
                case "AddRoleNode":
                    return AddRoleNode(context);
                case "UpdateRoleNode":
                    return UpdateRoleNode(context.Parameters);
                case "DeleteRoleNode":
                    return DeleteRoleNode(context.Parameters);
            }

            //default

            return null;
        }

        private BaseActionResult List(string strDomain)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {

                DataSet dataSet = roleService.GetRoleDataSet(strDomain);
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
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult GetRoleProf(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string roleName = CommonGlobalSettings.Utilities.GetParameter("roleName", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", parameters);
                bool isSiteAdmin = bool.Parse(CommonGlobalSettings.Utilities.GetParameter("IsSiteAdmin", parameters));
                DataSet dataSet = roleService.GetRoleProfDetDataSet(roleName, strDomain, userGuid, isSiteAdmin);
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
        private BaseActionResult AddRole(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string roleName = CommonGlobalSettings.Utilities.GetParameter("roleName", parameters);
                string roleDescription = CommonGlobalSettings.Utilities.GetParameter("roleDescription", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                string parentId = CommonGlobalSettings.Utilities.GetParameter("ParentId", parameters);
                int iResultValue = roleService.AddRole(roleName, roleDescription, strDomain, parentId);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "Role name already exists";
                }
                // unhandled exception
                else if (iResultValue == -1)
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

        private BaseActionResult EditRole(RoleModel model, string parameters)
        {

            BaseActionResult result = new BaseActionResult();
            try
            {
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                if (roleService.EditRole(model, strDomain))
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

        private BaseActionResult CopyRole(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string roleName = CommonGlobalSettings.Utilities.GetParameter("roleName", parameters);
                string roleDescription = CommonGlobalSettings.Utilities.GetParameter("roleDescription", parameters);
                string oldRoleName = CommonGlobalSettings.Utilities.GetParameter("oldRoleName", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                string parentId = CommonGlobalSettings.Utilities.GetParameter("ParentId", parameters);
                int iResultValue = roleService.CopyRole(roleName, roleDescription, oldRoleName, strDomain, parentId);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.ReturnMessage = "Role name already exists";
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

        private BaseActionResult Delete(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string roleName = CommonGlobalSettings.Utilities.GetParameter("roleName", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                int iResultValue = roleService.DeleteRole(roleName, strDomain);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "Cannot delete role as user already exists";
                }
                else if (iResultValue == 2)  //Defect #: EK_HI00040188
                {
                    result.Result = false;
                    result.ReturnMessage = "The role has been deleted by other user";
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult GetRoleDir(string parameters)
        {

            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                DataSet ds = roleService.GetRoleDir(strDomain);
                result.Result = true;
                result.DataSetData = ds;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;

        }

        private BaseActionResult AddRoleNode(Context context)
        {

            RoleNodeModel nodeModel = context.Model as RoleNodeModel;

            DataSetActionResult result = new DataSetActionResult();
            try
            {
                roleService.AddRoleNode(nodeModel);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;

        }

        private BaseActionResult DeleteRoleNode(string parameters)
        {

            string nodeId = CommonGlobalSettings.Utilities.GetParameter("NodeID", parameters);
            BaseActionResult result = new BaseActionResult();
            try
            {
                roleService.DeleteRoleNode(nodeId);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult UpdateRoleNode(string parameters)
        {
            string nodeName = CommonGlobalSettings.Utilities.GetParameter("NodeName", parameters);
            string parentId = CommonGlobalSettings.Utilities.GetParameter("ParentID", parameters);
            string uniqueId = CommonGlobalSettings.Utilities.GetParameter("UniqueId", parameters);

            BaseActionResult result = new BaseActionResult();
            try
            {
                roleService.UpdateRoleNode(nodeName, parentId, uniqueId);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
    }
}
