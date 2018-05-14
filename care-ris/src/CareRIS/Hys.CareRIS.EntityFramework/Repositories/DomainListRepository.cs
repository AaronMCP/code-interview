﻿using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class DomainListRepository : Repository<DomainList>, IDomainListRepository
    {
        public DomainListRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
