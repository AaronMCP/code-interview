using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Common.ActionResult;

namespace Server.Business.Oam
{
    public interface IKeyPerformanceRatingService
    {
         void GetRatingPoolList(DataSetActionResult Result);
         void GetUnRatingAppraisers(DataSetActionResult Result); 
         void GetRatingPoolInfo(DataSetActionResult Result);
         void NewRatingPool(DataSetActionResult Result);
         void SetScore(DataSetActionResult Result);
         void CreateTeaching(DataSetActionResult Result);        
    }
}
