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

namespace Server.DAO.Oam
{
    public interface IUserDAO
    {
        DataSet LoadUserDataSet(string strIsAdmin, string currentUserGUID, string strDomain, string strSite);
        DataSet GetUserProfDetDataSet(string userGuid, string strDomain, string strSite);
        int AddUser(UserModel model, string strSite);
        int ModifyUser(UserModel model);
        int ModifyUserIKeySN(UserModel model);
        DataSet getSystemDateNow();
        int DeleteUser(string userGuid, string strDomain);
        string GetUserDepartment(string userGuid, string strDomain);
        DataTable GetUserCertByCertSN(string userGuid,string certSN);


        int CheckSyncronization(string userGuid, string strDomain);
        bool DeleteSyncronization(string userGuid);

        DataTable GetUserCerts(string userGuid);

        bool IsExistUserCert(string userGuid, string certSN);

        void AddUserCert(UserCertModel model);

        void EnableUserCert(string userGuid, string certSN);

        void RemoveUserCert(string userGuid, string certSN);

        DataTable GetUserCertInUse(string userGuid);

        #region Modified by Blue for [RC603.1] - US16931, 06/09/2014
        void ResetPassword(string strParam);
        void ChangePassword(string strParam);
        #endregion
    }
}
