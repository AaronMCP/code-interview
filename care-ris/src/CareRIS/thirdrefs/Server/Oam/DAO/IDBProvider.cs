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

namespace Server.DAO.Oam
{
    //每次新建一个DAO的接口,都要实现IDBProvider的功能
    public interface IDBProvider : IRandomInspectionDAO,IKeyPerformanceRatingDAO, ILoginSettingsDAO, IRoleDAO, IUserDAO, ISystemProfileDAO, IDictionaryDAO, IResourceDAO, IProcedureCodeDAO, IScheduleDAO, IACRCodeDAO, IClientConfigDAO, ITemplateIEDAO, IHippaQueryDAO, IBulletinBoardDAO, IICD10DAO, IQualityScoringDAO, IKMSDAO, IConditionColDao, IModalityTimeSliceSettingDAO, IAdministratorToolDAO, IDomainDao, IChargeCodeDao
    {

    }
}
