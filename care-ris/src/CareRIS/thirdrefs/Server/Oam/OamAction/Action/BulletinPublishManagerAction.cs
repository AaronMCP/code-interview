using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using Server.Business.Oam;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.OamAction.Action
{
    public class BulletinPublishManagerAction : BaseAction
    {
        private IBulletinBoardService bulletinBoardService = BusinessFactory.Instance.GetBulletinBoardService();
        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            string strGuid = CommonGlobalSettings.Utilities.GetParameter("guid", context.Parameters);
            string strRoles = CommonGlobalSettings.Utilities.GetParameter("roles", context.Parameters);
            string strName = CommonGlobalSettings.Utilities.GetParameter("name", context.Parameters);
            string strTag = CommonGlobalSettings.Utilities.GetParameter("tag", context.Parameters);

            switch (actionName)
            {
                case "AddNewBulletin":
                    return AddNewBulletin(context.Model as BulletinBoardModel);
                case "DeleteBulletin":
                    return DeleteBulletin(context.Model as BulletinBoardModel);
                case "UpdateBulletin":
                    return UpdateBulletin(context.Model as BulletinBoardModel);
                case "GetAllBulletinsExceptBodyHistory":
                    return GetAllBulletinsExceptBodyHistory(context.Model as BulletinBoardModel);
                case "GetOneBulletinRow":
                    return GetOneBulletinRow(strGuid);
                case "GetUsers":
                    return GetUsers(strRoles);
                case "GetDictionaryValue":
                    return GetDictionaryValue(strTag);
                case "LockBulletin":
                    return LockBulletin(strName);
                case "UnlockBulletin":
                    return UnLockBulletin(strName);
                default:
                    break;
            }

            //default
            return null;
        }

        private DataSetActionResult AddNewBulletin(BulletinBoardModel model)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = bulletinBoardService.AddNewBulletin(model);
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

        private DataSetActionResult DeleteBulletin(BulletinBoardModel model)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = bulletinBoardService.DeleteBulletin(model);
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

        private DataSetActionResult UpdateBulletin(BulletinBoardModel model)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = bulletinBoardService.UpdateBulletin(model);
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

        private DataSetActionResult GetAllBulletinsExceptBodyHistory(BulletinBoardModel model)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = bulletinBoardService.GetAllBulletinsExceptBodyHistory(model);
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

        private DataSetActionResult  GetOneBulletinRow(string Guid)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                if (Guid == null)
                {
                    result.Result = false;
                }
                DataSet dataSet = bulletinBoardService.GetOneBulletinRow(Guid);
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

        private DataSetActionResult GetUsers(string roles)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                if (roles == null)
                {
                    result.Result = false;
                }
                DataSet dataSet = bulletinBoardService.GetUsers(roles);
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

        private DataSetActionResult GetDictionaryValue(string tag)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                if (tag == null)
                {
                    result.Result = false;
                }
                DataSet dataSet = bulletinBoardService.GetDictionaryValue(tag);
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

        private DataSetActionResult LockBulletin(string name)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = bulletinBoardService.LockBulletin(name);
                result.Result = bResult;
                result.DataSetData = null;

            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult UnLockBulletin(string name)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                bool bResult = bulletinBoardService.UnLockBulletin(name);
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
    }
}
