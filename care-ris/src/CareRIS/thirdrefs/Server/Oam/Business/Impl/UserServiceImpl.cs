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
using Server.DAO.Oam.Impl;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class UserServiceImpl : IUserService
    {
        private IUserDAO userDAO = DataBasePool.Instance.GetDBProvider();

        public DataSet LoadUserDataSet(string strIsAdmin, string currentUserGUID, string strDomain, string strSite)
        {
            try
            {
                return userDAO.LoadUserDataSet(strIsAdmin, currentUserGUID, strDomain,strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetUserProfDetDataSet(string userGuid, string strDomain, string strSite)
        {
            try
            {
                return userDAO.GetUserProfDetDataSet(userGuid, strDomain,strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AddUser(UserModel model, string strSite)
        {
            try
            {
                return userDAO.AddUser(model, strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ModifyUser(UserModel model)
        {
            try
            {
                return userDAO.ModifyUser(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ModifyUserIKeySN(UserModel model)
        {
            try
            {
                return userDAO.ModifyUserIKeySN(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CheckSyncronization(string userGuid, string strDomain)
        {
            try
            {
                return userDAO.CheckSyncronization(userGuid, strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteSyncronization(string userGuid)
        {
            try
            {
                return userDAO.DeleteSyncronization(userGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeleteUser(string userGuid, string strDomain)
        {
            try
            {
                return userDAO.DeleteUser(userGuid, strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetUserDepartment(string userGuid, string strDomain)
        {
            return userDAO.GetUserDepartment(userGuid, strDomain);
        }

        public DataTable GetUserCerts(string userGuid)
        {
            return userDAO.GetUserCerts(userGuid);
        }

        public DataTable GetUserCertInUse(string userGuid)
        {
            return userDAO.GetUserCertInUse(userGuid);
        }

        public DataTable GetUserCertByCertSN(string userGuid, string certSN)
        {
            return userDAO.GetUserCertByCertSN(userGuid, certSN);
        }

        public bool IsExistUserCert(string userGuid, string certSN)
        {
            return userDAO.IsExistUserCert(userGuid, certSN);
        }

        public void AddUserCert(UserCertModel model)
        {
            userDAO.AddUserCert(model);
        }

        public void EnableUserCert(string userGuid, string certSN)
        {
            userDAO.EnableUserCert(userGuid, certSN);
        }

        public void RemoveUserCert(string userGuid, string certSN)
        {
            userDAO.RemoveUserCert(userGuid, certSN);
        }

        #region EK_HI00062351 jameswei 2007-11-22
        public DataSet getSystemDateNow()
        {
            try
            {
                return userDAO.getSystemDateNow();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Modified by Blue for [RC603.1] - US16931, 06/09/2014
        public void ResetPassword(string strParam)
        {
            userDAO.ResetPassword(strParam);
        }

        public void ChangePassword(string strParam)
        {
            userDAO.ChangePassword(strParam);
        }
        #endregion
    }
}
