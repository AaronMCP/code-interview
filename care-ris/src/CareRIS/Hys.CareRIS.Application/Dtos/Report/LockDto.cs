using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class LockDto
    {
        
        public bool? IsLock { get; set; }
        public SyncDto Lock { get; set; }
    }
}
