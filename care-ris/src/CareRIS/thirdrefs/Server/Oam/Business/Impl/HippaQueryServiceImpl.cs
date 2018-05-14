#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Business.Oam;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class HippaQueryServiceImpl : IHippaQueryService
    {
        private IHippaQueryDAO hippaQueryDAO = DataBasePool.Instance.GetDBProvider();

        public DataSet HippaQuery(HippaModel hippaModel)
        {
            try
            {
                return hippaQueryDAO.HippaQuery(hippaModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int getAllRowCount(HippaModel hippaModel)
        {
            try
            {
                return hippaQueryDAO.getAllRowCount(hippaModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
