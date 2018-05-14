using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Business.Templates;
using Server.DAO.Templates;

namespace Server.Business.Templates.Impl
{
    public class EmergencyTemplateServiceImpl:IEmergencyTemplateService
    {
        private IDBProvider dbProvider = DataBasePool.Instance.GetDBProvider();

        public virtual bool QueryEYTemplate(DataSet ds, string strTemplateType, ref string strError)
        {
            return dbProvider.QueryEYTemplate(ds,strTemplateType,ref strError);
        }
        public virtual bool SaveEYTemplate(DataSet ds, ref string strError)
        {
            return dbProvider.SaveEYTemplate(ds, ref strError);
        }
        public virtual bool DelEYTemplate(string strTemplateGuid, ref string strError)
        {
            return dbProvider.DelEYTemplate(strTemplateGuid,ref strError);
        }
        public virtual bool UpdateEYTemplate(DataSet ds, ref string strError)
        {
            return dbProvider.UpdateEYTemplate(ds, ref strError);
        }
        public virtual bool LockEYTemplate(string strTemplateGuid, string strOwner, string strOwnerIP, ref string strLockInfo, ref string strError)
        {
            return dbProvider.LockEYTemplate(strTemplateGuid,strOwner,strOwnerIP,ref strLockInfo,ref strError);
        }
        public virtual bool UnLockEYTemplate(string strTemplateGuid, string strOwner, ref string strError)
        {
            return dbProvider.UnLockEYTemplate(strTemplateGuid, strOwner, ref strError);
        }


    }
}
