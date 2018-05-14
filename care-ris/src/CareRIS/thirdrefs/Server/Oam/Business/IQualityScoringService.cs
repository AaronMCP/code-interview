using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.ActionResult;

namespace Server.Business.Oam
{
    public interface IQualityScoringService
    {
        bool QueryQualityScoringList(string strParam, BaseActionResult bar);
        bool QueryScoringHistoryList(string strParam, BaseActionResult bar);
        //bool QueryQualityScoringList(string strParam, int nPageIndex, int nPageSize, ref int nTotalCount, DataSet ds);
        bool SaveAppraise(DataSet ds, BaseActionResult bar);
        bool GetAppraise(string RPGuid, BaseActionResult bar);
        string GetSettings(string strParam, ref string version);
        bool SaveSettings(string strParam, DataSet ds);
        bool GetAppraiseNew(string RPGuid, string type, BaseActionResult bar);
        bool SaveAppraiseNew(DataSet ds, BaseActionResult bar);
    }
}
