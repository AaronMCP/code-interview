using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace Server.DAO.Oam
{
    public interface IBulletinBoardDAO
    {
        bool AddNewBulletin(BulletinBoardModel model);
        bool DeleteBulletin(BulletinBoardModel model);
        bool UpdateBulletin(BulletinBoardModel model);
        DataSet GetAllBulletinsExceptBodyHistory(BulletinBoardModel model);
        DataSet GetOneBulletinRow(string Guid);
        DataSet GetUsers(string Roles);
        DataSet GetDictionaryValue(string tag);
        bool LockBulletin(string name);
        bool UnLockBulletin(string name);
    }
}
