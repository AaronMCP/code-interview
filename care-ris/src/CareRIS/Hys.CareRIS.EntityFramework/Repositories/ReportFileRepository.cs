﻿using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportFileRepository : Repository<ReportFile>, IReportFileRepository
    {
        public ReportFileRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
