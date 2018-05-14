using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.DAO.Oam;
using  Common.ActionResult;

namespace Server.Business.Oam.Impl
{
    public class RandomInspectionServiceImpl : IRandomInspectionService 
    {
        private IRandomInspectionDAO RandomInspectionDAO = DataBasePool.Instance.GetDBProvider();

        public void GetRandomInspectionPoolList(DataSetActionResult Result)
        {
            RandomInspectionDAO.GetRandomInspectionPoolList(Result);
        }
        public void GetRandomInspectionPoolListByUser(DataSetActionResult Result)
        {
            RandomInspectionDAO.GetRandomInspectionPoolListByUser(Result);
        }
        public void NewRandomInspectionPool(DataSetActionResult Result)
        {
            RandomInspectionDAO.NewRandomInspectionPool(Result);
        }

        public void GetRandomInspectionPoolInfo(DataSetActionResult Result)
        {
            RandomInspectionDAO.GetRandomInspectionPoolInfo(Result);
        }

        public string DeleteInspectionPool(string strParam)
        {
            return RandomInspectionDAO.DeleteInspectionPool(strParam);
        }

        public void InspectionSetScore(DataSetActionResult Result)
        {
            RandomInspectionDAO.InspectionSetScore(Result);
        }

        public string GetScoreCardSettings(string strParam, ref string version)
        {
            return RandomInspectionDAO.GetScoreCardSettings(strParam, ref version);
        }
    }
}
