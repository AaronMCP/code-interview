using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using Hys.Platform.Application;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ScheduleService : DisposableServiceBase, IScheduleService
    {
        private IRisProContext _dbContext;
        public ScheduleService(IRisProContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ModalityTimeSliceDto>> SearchTimeSlice(string modality, int dateType)
        {
            var timeSlices = await _dbContext.Set<ModalityTimeSlice>()
                .Where(m => m.Modality == modality && m.DateType == dateType)
                .Select(t => t).OrderBy(t=>t.StartDt).ToListAsync();

            var timeSliceDtoes = AutoMapper.Mapper.Map<List<ModalityTimeSliceDto>>(timeSlices);

            timeSliceDtoes.ForEach(t =>
            {
                t.IsShared = _dbContext.Set<ModalityShare>()
                    .Where(s => s.TimeSliceGuid == t.UniqueID && s.GroupId != "Default_Hide" && s.Date == null)
                    .Count() > 0;
            });

            return timeSliceDtoes;
        }

        private bool availableDateValid(string modality, DateTime availableDate)
        {
            var maxDate = _dbContext.Set<Procedure>()
                    .Where(p => p.Modality == modality && p.Status == 10)
                    .Max(p => p.BookingBeginTime);
            return !maxDate.HasValue || availableDate.Date > maxDate.Value.Date;
        }

        private void timeSliceAdder(string modalityType, string modality, DateTime startTime,
            DateTime endTime, string description, int amount, string domain, int[] dateTypes, DateTime availableDate, int? interval = null)
        {
            if (dateTypes == null || dateTypes.Length < 1)
            {
                throw new ArgumentException("dateTypes must have value!", "dateTypes");
            }
            var formAvailableDate = availableDate.Date;

            if (!availableDateValid(modality, formAvailableDate))
            {
                throw new ArgumentException("Availabale Date is not reasonable!", "availableDate");
            }

            var startTimeOfDay = startTime.TimeOfDay;
            var endTimeOfDay = endTime.TimeOfDay;
            var formSartTime = new DateTime(2008, 8, 8, startTimeOfDay.Hours, startTimeOfDay.Minutes, startTimeOfDay.Seconds);
            var formEndTime = new DateTime(2008, 8, 8, endTimeOfDay.Hours, endTimeOfDay.Minutes, endTimeOfDay.Seconds);

            var sameType = _dbContext.Set<ModalityTimeSlice>().Where(
                    m => m.Modality == modality &&
                        (
                            (m.StartDt <= formSartTime && formSartTime < m.EndDt) ||
                            (m.EndDt >= formEndTime && formEndTime > m.StartDt)
                        ) &&
                        m.AvailableDate == formAvailableDate &&
                        m.DateType.HasValue &&
                        dateTypes.Contains(m.DateType.Value)).FirstOrDefault();
            if (sameType != null)
            {
                throw new Exception("Decsription can not be duplicated in same modality and DateType!");
            }

            var modalitySite = (from m in _dbContext.Set<Modality>()
                                join sp in _dbContext.Set<SystemProfile>() on m.Domain equals sp.Value
                                where sp.Name == "Domain" && m.ModalityName == modality
                                select m.Site).FirstOrDefault();
            var siteList = (from s in _dbContext.Set<Site>()
                            join sp in _dbContext.Set<SystemProfile>() on s.Domain equals sp.Value
                            where sp.Name == "Domain"
                            select s).ToList();

            Action<string, string> addSharedTimeSlice = (string timeSliceGuid, string site) =>
            {
                var shared = new ModalityShare
                {
                    Guid = Guid.NewGuid().ToString(),
                    TimeSliceGuid = timeSliceGuid,
                    AvailableCount = amount,
                    Date = null,
                    GroupId = "Default_Hide",
                    MaxCount = amount,
                    ShareTarget = site,
                    TargetType = 1
                };
                _dbContext.Set<ModalityShare>().Add(shared);
            };

            #region modality time slice
            Action<DateTime, DateTime, string> addTimeSlice = (DateTime firstTime, DateTime secondTime, string desc) =>
            {
                foreach (var dateType in dateTypes)
                {
                    var guid = Guid.NewGuid().ToString();
                    var timeSlice = new ModalityTimeSlice
                    {
                        TimeSliceGuid = guid,
                        ModalityType = modalityType,
                        Modality = modality,
                        StartDt = firstTime,
                        EndDt = secondTime,
                        Description = desc,
                        MaxNumber = amount,
                        Domain = domain,
                        DateType = dateType,
                        AvailableDate = formAvailableDate
                    };

                    _dbContext.Set<ModalityTimeSlice>().Add(timeSlice);

                    if (string.IsNullOrEmpty(modalitySite))
                    {
                        foreach (var site in siteList)
                        {
                            addSharedTimeSlice(guid, site.SiteName);
                        }
                    }
                    else
                    {
                        addSharedTimeSlice(guid, modalitySite);
                    }

                    var now = DateTime.Now.Date;
                    var sharedDates = (from s in _dbContext.Set<ModalityShare>()
                                       join mt in _dbContext.Set<ModalityTimeSlice>()
                                       on s.TimeSliceGuid equals mt.TimeSliceGuid
                                       where mt.Modality == modality
                                           && mt.DateType == dateType
                                           && mt.AvailableDate == formAvailableDate
                                           && s.Date != null && s.Date >= now
                                       select s.Date).Distinct().ToList();

                    foreach (var date in sharedDates)
                    {
                        var oldShare = _dbContext.Set<ModalityShare>()
                            .Where(ms => ms.Date == null && ms.TimeSliceGuid == guid).ToList();
                        oldShare.ForEach(s =>
                        {
                            _dbContext.Set<ModalityShare>().Add(new ModalityShare
                            {
                                Guid = Guid.NewGuid().ToString(),
                                TimeSliceGuid = s.TimeSliceGuid,
                                ShareTarget = s.ShareTarget,
                                TargetType = s.TargetType,
                                MaxCount = s.MaxCount,
                                AvailableCount = s.AvailableCount,
                                GroupId = s.GroupId,
                                Date = date
                            });
                        });
                    }
                }
            };
            #endregion

            var sliceInterval = interval.HasValue ? interval.Value : (formEndTime - formSartTime).TotalMinutes;

            if (sliceInterval <= 0)
            {
                throw new Exception("Start time and end time are invalid!");
            }

            var index = 1000;

            var startPoint = formSartTime;
            var endPoint = formSartTime;
            var descTemp = description;
            var descEmpty = string.IsNullOrEmpty(description) || interval.HasValue;

            while (index > 0)
            {
                if (endPoint >= formEndTime) break;

                endPoint = startPoint.AddMinutes(sliceInterval);
                if (endPoint > formEndTime)
                {
                    endPoint = formEndTime;
                }
                if (descEmpty)
                {
                    descTemp = startPoint.ToString("HH:mm") + "-" + endPoint.ToString("HH:mm");
                }

                addTimeSlice(startPoint, endPoint, descTemp);

                startPoint = endPoint;
                index--;
            }
        }

        public async Task<bool> AddModalityTimeSlice(string modalityType, string modality, DateTime startTime,
            DateTime endTime, string description, int amount, string domain, int[] dateTypes, DateTime availableDate, int? interval = null)
        {

            timeSliceAdder(modalityType, modality, startTime, endTime, description, amount, domain, dateTypes, availableDate, interval);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModifyTimeSlice(string sliceId, DateTime startTime, DateTime endTime, string description, int amount)
        {
            var timeSlice = await _dbContext.Set<ModalityTimeSlice>().Where(t => t.TimeSliceGuid == sliceId).FirstOrDefaultAsync();
            if (timeSlice == null)
            {
                return true;
            }

            var timeOfStartTime = startTime.TimeOfDay;
            var timeOfEndTime = endTime.TimeOfDay;
            var formStartTime = new DateTime(2008, 8, 8, timeOfStartTime.Hours, timeOfStartTime.Minutes, timeOfStartTime.Seconds);
            var formEndTime = new DateTime(2008, 8, 8, timeOfEndTime.Hours, timeOfEndTime.Minutes, timeOfEndTime.Seconds);

            var repeated = await _dbContext.Set<ModalityTimeSlice>()
                    .Where(t => t.TimeSliceGuid != sliceId &&
                        (
                            (t.StartDt < formStartTime && formStartTime < t.EndDt) ||
                            (t.StartDt < formEndTime && formEndTime < t.EndDt)
                        ) &&
                        t.Modality == timeSlice.Modality &&
                        t.AvailableDate == timeSlice.AvailableDate &&
                        t.DateType == timeSlice.DateType
                    ).CountAsync();
            if (repeated > 0)
            {
                throw new Exception("Duplicated timeslice");
            }

            timeSlice.StartDt = formStartTime;
            timeSlice.EndDt = formEndTime;
            timeSlice.Description = description;
            timeSlice.MaxNumber = amount;

            var sharedTimeSliceTemplete = await _dbContext.Set<ModalityShare>()
                .Where(s => s.TimeSliceGuid == sliceId && s.Date == null && s.GroupId == "Default_Hide").ToListAsync();
            sharedTimeSliceTemplete.ForEach(s =>
            {
                s.MaxCount = amount;
                s.AvailableCount = amount;
            });
            var now = DateTime.Now.Date;
            var existTimeSlice = await _dbContext.Set<ModalityShare>()
                    .Where(s => s.TimeSliceGuid == sliceId && s.Date != null && s.Date >= now && s.GroupId == "Default_Hide")
                    .ToListAsync();
            existTimeSlice.ForEach(s =>
            {
                s.AvailableCount = s.AvailableCount + amount - s.MaxCount;
                if (s.AvailableCount < 0)
                {
                    throw new Exception("The given amount is not reasonable");
                }
                s.MaxCount = amount;
            });

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DelTimeSlice(string[] sliceIds)
        {
            if (sliceIds == null || sliceIds.Length < 1) return true;

            var timeSliceSet = _dbContext.Set<ModalityTimeSlice>();
            var timeSlices = await timeSliceSet.Where(m => sliceIds.Contains(m.TimeSliceGuid)).ToListAsync();
            if (timeSlices != null)
            {
                timeSliceSet.RemoveRange(timeSlices);
            }

            var sharedSet = _dbContext.Set<ModalityShare>();
            var shared = await sharedSet.Where(s => sliceIds.Contains(s.TimeSliceGuid)).ToListAsync();
            if (shared != null)
            {
                sharedSet.RemoveRange(shared);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CopyTimeSlice(string[] sliceIds, string domain, DateTime availableDate, string modality, int dateType)
        {
            if (sliceIds == null || sliceIds.Length < 1)
            {
                return true;
            }

            var slices = _dbContext.Set<ModalityTimeSlice>().
                    Where(t => sliceIds.Contains(t.TimeSliceGuid)).
                    ToList();
            var modalityEntity = _dbContext.Set<Modality>().Where(m => m.ModalityName == modality).FirstOrDefault();
            if (modalityEntity == null)
            {
                throw new ArgumentException("invalid modality", "modality");
            }
            var dateTypes = new int[] { dateType };
            slices.ForEach(s =>
            {
                timeSliceAdder(modalityEntity.ModalityType, modalityEntity.ModalityName, s.StartDt.Value, s.EndDt.Value, s.Description, s.MaxNumber.Value, domain, dateTypes, availableDate);
            });
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ShareTimeSlice(string[] sliceIds, TimesliceSharer[] sharers)
        {
            if (sliceIds == null || sliceIds.Length < 1)
            {
                return true;
            }

            var modalityShareSet = _dbContext.Set<ModalityShare>();
            var oldShare = await modalityShareSet.
                Where(m => sliceIds.Contains(m.TimeSliceGuid) && m.Date == null).
                ToArrayAsync();

            //clear old shared timeslice
            modalityShareSet.RemoveRange(oldShare);

            var modalities = await (from m in _dbContext.Set<Modality>()
                                    join p in _dbContext.Set<SystemProfile>() on m.Domain equals p.Value
                                    where p.Name == "Domain"
                                    select m).ToListAsync();
            var siteList = await (from s in _dbContext.Set<Site>()
                                  join p in _dbContext.Set<SystemProfile>() on s.Domain equals p.Value
                                  where p.Name == "Domain"
                                  select s).ToListAsync();
            var today = DateTime.Today;
            if (sharers == null || sharers.Length < 1)
            //set default timeslice
            {
                foreach (var id in sliceIds)
                {
                    var timeslice = await _dbContext.Set<ModalityTimeSlice>()
                        .Where(m => m.TimeSliceGuid == id)
                        .FirstOrDefaultAsync();
                    if (timeslice == null)
                    {
                        throw new ArgumentException("Timeslice id is invalid.");
                    }

                    var modalityName = timeslice.Modality;
                    var maxNum = timeslice.MaxNumber;

                    var modality = modalities.Where(m => m.ModalityName == modalityName).FirstOrDefault();
                    if (modality == null)
                    {
                        throw new Exception("Cannot find modality!");
                    }

                    var site = modality.Site;
                    if (string.IsNullOrEmpty(site))
                    {
                        foreach (var siteModel in siteList)
                        {
                            var tmpShare = new ModalityShare
                            {
                                Guid = Guid.NewGuid().ToString(),
                                TimeSliceGuid = id,
                                ShareTarget = siteModel.SiteName,
                                TargetType = 1,
                                AvailableCount = maxNum,
                                MaxCount = maxNum,
                                GroupId = "Default_Hide",
                                Date = null
                            };
                            modalityShareSet.Add(tmpShare);
                        }
                    }
                    else
                    {
                        var tmpShare = new ModalityShare
                        {
                            Guid = Guid.NewGuid().ToString(),
                            TimeSliceGuid = id,
                            ShareTarget = site,
                            TargetType = 1,
                            AvailableCount = maxNum,
                            MaxCount = maxNum,
                            GroupId = "Default_Hide",
                            Date = null
                        };
                        modalityShareSet.Add(tmpShare);
                    }
                }
            }
            else
            {
                foreach (var id in sliceIds)
                {
                    var dic = new Dictionary<string, string>();
                    foreach (var share in sharers)
                    {
                        var groupId = "";
                        if (!string.IsNullOrEmpty(share.GroupId))
                        {
                            if (!dic.ContainsKey(share.GroupId))
                            {
                                groupId = Guid.NewGuid().ToString();
                                dic.Add(share.GroupId, groupId);
                            }
                            else
                            {
                                groupId = dic[share.GroupId];
                            }
                        }

                        var tmpShare = new ModalityShare
                        {
                            Guid = Guid.NewGuid().ToString(),
                            TimeSliceGuid = id,
                            ShareTarget = share.ShareTarget,
                            TargetType = 1,
                            AvailableCount = share.MaxCount,
                            MaxCount = share.MaxCount,
                            GroupId = groupId,
                            Date = null
                        };
                        modalityShareSet.Add(tmpShare);
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ModalityShareDto>> GetSharedTimeslice(string sliceId)
        {
            var shares = await _dbContext.Set<ModalityShare>()
                        .Where(s => s.TimeSliceGuid == sliceId && s.Date == null && s.GroupId != "Default_Hide")
                        .Select(s => s).OrderBy(s => s.GroupId)
                        .ToListAsync();
            var result = AutoMapper.Mapper.Map<List<ModalityShareDto>>(shares);
            return result;
        }
    }
}
