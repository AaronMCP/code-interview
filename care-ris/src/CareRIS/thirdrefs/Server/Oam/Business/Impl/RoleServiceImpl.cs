#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using CommonGlobalSettings;
using Common.ActionResult;

namespace Server.Business.Oam.Impl
{
    public class RoleServiceImpl : IRoleService
    {
        private IRoleDAO roleDAO = DataBasePool.Instance.GetDBProvider();

        public DataSet GetRoleDataSet(string strDomain)
        {
            try
            {
                return roleDAO.GetRoleDataSet(strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetRoleProfDetDataSet(string roleName, string strDomain, string userGuid, bool isSiteAdmin)
        {
            try
            {
                return roleDAO.GetRoleProfDetDataSet(roleName, strDomain, userGuid, isSiteAdmin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AddRole(string roleName, string description, string strDomain, string parentId)
        {
            try
            {

                return roleDAO.AddRole(roleName, description, strDomain, parentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditRole(RoleModel model, string strDomain)
        {
            try
            {
                return roleDAO.EditRole(model, strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteRole(string roleName, string strDomain)
        {
            try
            {
                return roleDAO.DeleteRole(roleName, strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CopyRole(string roleName, string roleDescription, string oldRoleName, string strDomain, string strParentId)
        {
            try
            {
                return roleDAO.CopyRole(roleName, roleDescription, oldRoleName, strDomain, strParentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DataSet GetRoleDir(string strDomain)
        {
            return roleDAO.GetRoleDir(strDomain);
        }

        public void AddRoleNode(RoleNodeModel nodeModel)
        {
            nodeModel.OrderId = 0;
            nodeModel.RoleID = string.Empty;
            roleDAO.AddRoleNode(nodeModel);
        }

        public void DeleteRoleNode(string nodeId)
        {
            roleDAO.DeleteRoleNode(nodeId);
        }

        public void UpdateRoleNode(string nodeName, string parentId, string uniqueId)
        {
            roleDAO.UpdateRoleNode(nodeName, parentId, uniqueId);
        }

    }
}
