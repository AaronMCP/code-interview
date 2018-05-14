﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Common.ActionResult;

namespace Server.Business.Oam
{
    public interface IRandomInspectionService
    {
        void GetRandomInspectionPoolList(DataSetActionResult Result);
        void GetRandomInspectionPoolListByUser(DataSetActionResult Result);
        void NewRandomInspectionPool(DataSetActionResult Result);
        void GetRandomInspectionPoolInfo(DataSetActionResult Result);
        string DeleteInspectionPool(string strParam); 
        void InspectionSetScore(DataSetActionResult Result);
        string GetScoreCardSettings(string strParam, ref string version);
    }
}
