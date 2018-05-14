using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.DAO.Oam;
using  Common.ActionResult;

namespace Server.Business.Oam.Impl
{
    public class KeyPerformanceRatingServiceImpl : IKeyPerformanceRatingService
    {
        private IKeyPerformanceRatingDAO LoginDAO = DataBasePool.Instance.GetDBProvider();

        public void GetRatingPoolList(DataSetActionResult Result)
        {
            LoginDAO.GetRatingPoolList(Result);
        }

        public void GetUnRatingAppraisers(DataSetActionResult Result)
        {
            LoginDAO.GetUnRatingAppraisers(Result);
        }

        public void GetRatingPoolInfo(DataSetActionResult Result)
        {
            LoginDAO.GetRatingPoolInfo(Result);
        }

        public void NewRatingPool(DataSetActionResult Result)
        {
            LoginDAO.NewRatingPool(Result);
        }

        public void SetScore(DataSetActionResult Result)
        {
            LoginDAO.SetScore(Result);
        }

        public void CreateTeaching(DataSetActionResult Result)
        {
            LoginDAO.CreateTeaching(Result);
        }
    }
}
