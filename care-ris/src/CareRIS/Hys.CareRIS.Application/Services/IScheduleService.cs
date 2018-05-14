using Hys.CareRIS.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services
{
    public interface IScheduleService : IDisposable
    {
        Task<bool> AddModalityTimeSlice(string modalityType, string modality, DateTime startTime, DateTime endTime, string description, int amount, string domain, int[] dateTypes, DateTime availableDate, int? interval = null);
        Task<IEnumerable<ModalityTimeSliceDto>> SearchTimeSlice(string modality, int dateType);
        Task<bool> ModifyTimeSlice(string sliceId, DateTime startTime, DateTime endTime, string description, int amount);
        Task<bool> DelTimeSlice(string[] sliceIds);
        Task<bool> CopyTimeSlice(string[] sliceIds, string domain, DateTime availableDate, string modality, int dateType);
        Task<bool> ShareTimeSlice(string[] sliceIds, TimesliceSharer[] sharers);
        Task<IEnumerable<ModalityShareDto>> GetSharedTimeslice(string sliceId);
    }
}
