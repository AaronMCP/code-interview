using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.ActionResult;

namespace Server.Business.Oam
{
    public interface IAdministratorToolService
    {
        int QueryLock(string stOwner, string stBeginTime, string stEndTime, BaseActionResult bar);
        int UnLock(int strObjectType, int nSyncType, string strObjectGuid, string strOwner, BaseActionResult bar);
        int QueryOnline(BaseActionResult bar);
        int SetOffline(string strUserGuid, BaseActionResult bar);


    }
}
