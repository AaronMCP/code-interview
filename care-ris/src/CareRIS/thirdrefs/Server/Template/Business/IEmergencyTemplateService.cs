using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.Business.Templates
{
    public interface IEmergencyTemplateService
    {
        bool QueryEYTemplate(DataSet ds,string strTemplateType,ref string strError);
        bool SaveEYTemplate(DataSet ds, ref string strError);
        bool DelEYTemplate(string strTemplateGuid, ref string strError);
        bool UpdateEYTemplate(DataSet ds, ref string strError);
        bool LockEYTemplate(string strTemplateGuid, string strOwner, string strOwnerIP, ref string strLockInfo, ref string strError);
        bool UnLockEYTemplate(string strTemplateGuid, string strOwner, ref string strError);

    }
}
