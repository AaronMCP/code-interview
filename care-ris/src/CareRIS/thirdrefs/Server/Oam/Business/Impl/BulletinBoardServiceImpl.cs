using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Business.Oam;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    class BulletinBoardServiceImpl:IBulletinBoardService
    {
        private  IBulletinBoardDAO bulletinBoardDAO = DataBasePool.Instance.GetDBProvider();

       public bool AddNewBulletin(BulletinBoardModel model)
        {
            try
            {
                return bulletinBoardDAO.AddNewBulletin(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteBulletin(BulletinBoardModel model)
        {
            try
            {
                return bulletinBoardDAO.DeleteBulletin(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateBulletin(BulletinBoardModel model)
        {
            try
            {
                return bulletinBoardDAO.UpdateBulletin(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetAllBulletinsExceptBodyHistory(BulletinBoardModel model)
        {
            try
            {
                return bulletinBoardDAO.GetAllBulletinsExceptBodyHistory(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetOneBulletinRow(string Guid)
        {
            try
            {
                return bulletinBoardDAO.GetOneBulletinRow(Guid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetUsers(string Roles)
        {
            try
            {
                return bulletinBoardDAO.GetUsers(Roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetDictionaryValue(string tag)
        {
            try
            {
                return bulletinBoardDAO.GetDictionaryValue(tag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool LockBulletin(string name)
        {
            try
            {
                return bulletinBoardDAO.LockBulletin(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UnLockBulletin(string name)
        {
            try
            {
                return bulletinBoardDAO.UnLockBulletin(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
