﻿using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ModalityTypeRepository : Repository<ModalityType>, IModalityTypeRepository
    {
        public ModalityTypeRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
