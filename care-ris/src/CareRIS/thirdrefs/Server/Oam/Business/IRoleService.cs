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
using CommonGlobalSettings;
using Common.ActionResult;

namespace Server.Business.Oam
{
    public interface IRoleService
    {
        DataSet GetRoleDataSet(string strDomain);
        DataSet GetRoleProfDetDataSet(string roleName, string strDomain, string userGuid, bool isSiteAdmin);
        int AddRole(string roleName, string description, string strDomain, string parentId);
        bool EditRole(RoleModel model, string strDomain);
        int DeleteRole(string roleName, string strDomain);
        int CopyRole(string roleName, string roleDescription, string oldRoleName, string strDomain, string parentId);
        DataSet GetRoleDir(string strDomain);
        void AddRoleNode(RoleNodeModel nodeModel);
        void DeleteRoleNode(string nodeId);
        void UpdateRoleNode(string nodeName, string parentId, string uniqueId);
    }
}
