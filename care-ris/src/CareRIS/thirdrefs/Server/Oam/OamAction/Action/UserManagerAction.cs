using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.Oam;
using System.Windows.Forms;

namespace Server.OamAction.Action
{
    public class UserManagerAction : BaseAction
    {
        Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");
        
        IUserService userService = BusinessFactory.Instance.GetUserService();

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
                    return List(context.Parameters);
                case "addUser":
                    string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", context.Parameters);
                    return AddUser(context.Model as UserModel, strSite);
                case "modifyUser":
                    return EditUser(context.Model as UserModel);
                case "deleteUser":
                    return DeleteUser(context.Parameters);
                case "userProfile":
                    return GetUserProf(context.Parameters);
                case "getSystemDateNow":
                    return getSystemDateNow();
                case "getUserDepartMent":
                    return GetUserDepartment(context.Parameters);
                case "checkSyncronization":
                    return CheckSyncronization(context.Parameters);
                case "deleteSyncronization":
                    return DeleteSyncronization(context.Parameters);
                case "modifyUserIKeySN":
                    return modifyUserIKeySN(context.Model as UserModel);
                case "AddUserCert":
                    return AddUserCert(context.Model as UserCertModel);
                case "GetUserCerts":
                    return GetUserCerts(context.Parameters);
                case "EnableUserCert":
                    return EnableUserCert(context.Parameters);
                case "RemoveUserCert":
                    return RemoveUserCert(context.Parameters);
                case "GetUserCertInUse":
                    return GetUserCertInUse(context.Parameters);
                case "GetUserCertByCertSN":
                    return GetUserCertByCertSN(context.Parameters);
                #region Modified by Blue for [RC603.1] - US16931, 06/09/2014
                case "resetPassword":
                    return ResetPassword(context.Parameters);
                case "changePassword":
                    return ChangePassword(context.Parameters);
                #endregion
                default:
                    break;
            }

            //default
            return List(context.Parameters);
        }



        private BaseActionResult List(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string strIsAdmin = CommonGlobalSettings.Utilities.GetParameter("IsAdmin", parameters);
                string currentUserGUID = CommonGlobalSettings.Utilities.GetParameter("currentUserGUID", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
                DataSet dataSet = userService.LoadUserDataSet(strIsAdmin, currentUserGUID, strDomain, strSite);
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

        private BaseActionResult AddUser(UserModel model, string strSite)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {

                int iResultValue = userService.AddUser(model, strSite);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "User name already exists";
                    result.recode = 1;
                }
                else if (iResultValue == 2)
                {
                    result.Result = false;
                    result.ReturnMessage = "Login domain name already exists";
                    result.recode = 2;
                }
                else if (iResultValue == 3)
                {
                    result.Result = false;
                    result.ReturnMessage = "Local name already exists";
                    result.recode = 3;
                }
                else if (iResultValue == 4)
                {
                    result.Result = false;
                    result.ReturnMessage = "Deleted User name already exists";
                    result.recode = 4;
                }
                else if (iResultValue == 5)
                {
                    result.Result = false;
                    result.ReturnMessage = "Deleted local name already exists";
                    result.recode = 5;
                }
                else if (iResultValue == 6)
                {
                    result.Result = false;
                    result.ReturnMessage = "Deleted login domain name already exists";
                    result.recode = 6;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult EditUser(UserModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {

                int iResultValue = userService.ModifyUser(model);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "User name already exists";
                }
                else if (iResultValue == 2)
                {
                    result.Result = false;
                    result.ReturnMessage = "Login domain name already exists";
                }
                else if (iResultValue == 3)
                {
                    result.recode = 3;
                    result.Result = false;
                    result.ReturnMessage = "Local name already exists";
                }
                else if (iResultValue == 4)
                {
                    result.recode = 4;
                    result.Result = false;
                    result.ReturnMessage = "User name already exists";
                }
                else if (iResultValue == 6)
                {
                    result.recode = 6;
                    result.Result = false;
                    result.ReturnMessage = "Login domain name already exists";
                }
                
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult modifyUserIKeySN(UserModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                int iResultValue = userService.ModifyUserIKeySN(model);

                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.recode = 1;
                    result.ReturnMessage = "Existed iKeySN";
                    result.ReturnString = model.Comments; // occupant name
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteUser(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                int iResultValue = userService.DeleteUser(userGuid, strDomain);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "This user is deleted by another user. Please refresh.";
                }
                else if (iResultValue == 2)
                {
                    result.Result = false;
                    result.ReturnMessage = "User details is getting modified by another user.";
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult CheckSyncronization(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                int iResultValue = userService.CheckSyncronization(userGuid, strDomain);
                if (iResultValue == 0)
                {
                    result.Result = true;
                }
                else if (iResultValue == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "This user is deleted by another user. Please refresh.";
                }
                else if (iResultValue == 2)
                {
                    result.Result = false;
                    result.ReturnMessage = "User details is getting modified by another user.";
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult DeleteSyncronization(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                if (userService.DeleteSyncronization(userGuid))
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
        private BaseActionResult GetUserProf(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);
                string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
                DataSet dataSet = userService.GetUserProfDetDataSet(userGuid, strDomain, strSite);
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



        private BaseActionResult getSystemDateNow()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {

                DataSet dataSet = userService.getSystemDateNow();
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

        private BaseActionResult GetUserDepartment(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", parameters);

                string department = userService.GetUserDepartment(userGuid, strDomain);
                result.Result = true;
                result.ReturnString = department;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetUserCerts(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);

                var dt = userService.GetUserCerts(userGuid);

                result.DataSetData = new DataSet();
                result.DataSetData.Tables.Add(dt);
                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }


        private BaseActionResult EnableUserCert(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string certSN = CommonGlobalSettings.Utilities.GetParameter("CertSN", parameters);

                userService.EnableUserCert(userGuid, certSN);

                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult RemoveUserCert(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string certSN = CommonGlobalSettings.Utilities.GetParameter("CertSN", parameters);
                userService.RemoveUserCert(userGuid, certSN);

                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddUserCert(UserCertModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (userService.IsExistUserCert(model.UserGuid, model.CertInfo))
                    result.recode = 1;
                else
                {
                    userService.AddUserCert(model);
                }

                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }


        private BaseActionResult GetUserCertInUse(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);

                var dt = userService.GetUserCertInUse(userGuid);

                result.DataSetData = new DataSet();
                result.DataSetData.Tables.Add(dt);
                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult GetUserCertByCertSN(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string userGuid = CommonGlobalSettings.Utilities.GetParameter("userGuid", parameters);
                string certSN = CommonGlobalSettings.Utilities.GetParameter("certSN", parameters);

                var dt = userService.GetUserCertByCertSN(userGuid,certSN);

                result.DataSetData = new DataSet();
                result.DataSetData.Tables.Add(dt);
                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        #region Modified by Blue for [RC603.1] - US16931, 06/09/2014
        private BaseActionResult ResetPassword(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                

                userService.ResetPassword(parameters);

                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult ChangePassword(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
              
                userService.ChangePassword(parameters);

                result.Result = true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
        #endregion
    }
}
