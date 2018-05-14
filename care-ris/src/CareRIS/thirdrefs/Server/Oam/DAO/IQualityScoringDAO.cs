using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.DAO.Oam
{
    public interface IQualityScoringDAO
    {
        bool QueryQualityScoringList(string strParam, DataSet ds, ref string strError);
        bool QueryQualityScoringList(string strParam, int nPageIndex, int nPageSize, ref int nTotalCount, DataSet ds, ref string strError, int randomNum, int queryObject, string IncludingAppraised);
        bool QueryScoringHistoryList(string strParam, DataSet ds);
        bool SaveAppraise(DataSet ds,ref string errorMsg);
        bool GetAppraise(string RPGuid, DataSet ds, ref string errorMsg);
        string GetSettings(string strParam, ref string version);
        bool SaveSettings(string strParam, DataSet ds);
        bool GetAppraiseNew(string RPGuid, string type, DataSet ds, ref string errorMsg);
        bool SaveAppraiseNew(DataSet ds, ref string errorMsg);
    }
}
