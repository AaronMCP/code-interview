using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LinqKit;
using Newtonsoft.Json;
using Kendo.DynamicLinq;
using Hys.CrossCutting.Common;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class WorklistService : DisposableServiceBase, IWorklistService
    {
        private IRisProContext _dbContext;
        private IShortcutRepository _shortcutRepository;
        private IConfigurationService _configurationService;

        // This special value 12345 was defined in RisLite once. Now change to 54321 to distinguish that.
        // To avoid crash by old data
        public const int Category = 54321; //12345; 

        public WorklistService(IRisProContext dbContext, IShortcutRepository shortcutRepository, IConfigurationService configurationService)
        {
            _dbContext = dbContext;
            _shortcutRepository = shortcutRepository;
            _configurationService = configurationService;

            AddDisposableObject(dbContext);
            AddDisposableObject(shortcutRepository);
            AddDisposableObject(configurationService);
        }

        #region Search

        private class WorklistSearchQueryResult
        {
            public Order o { get; set; }
            public Patient p { get; set; }
            public Procedure pro { get; set; }
        }
        public async Task<PaginationResult> GetWorklist(WorklistSearchCriteriaDto criteria, string userID, string site, string role)
        {
            if (criteria == null || criteria.Pagination == null)
            {
                return null;
            }
            if (criteria.AccessSites.Count < 1)//搜索条件无站点条件
            {
                //获取用户可操作站点
                criteria.AccessSites = await GetAccessSites(userID, site, role);
            }
            // prepare date range by criteria
            var createStartDay = criteria.CreateStartDate.HasValue ? (DateTime?)criteria.CreateStartDate.Value : null;
            var createEndDay = criteria.CreateEndDate.HasValue ? (DateTime?)criteria.CreateEndDate.Value : null;
            var examineStartDay = criteria.ExamineStartDate.HasValue ? (DateTime?)criteria.ExamineStartDate.Value : null;
            var examineEndDay = criteria.ExamineEndDate.HasValue ? (DateTime?)criteria.ExamineEndDate.Value : null;

            // filter result by all criteria except time ranges
            var orderQuery = from o in _dbContext.Set<Order>()
                             join p in _dbContext.Set<Patient>() on o.PatientID equals p.UniqueID
                             join pro in _dbContext.Set<Procedure>() on o.UniqueID equals pro.OrderID
                             where (criteria.AccessSites.Count == 0 || criteria.AccessSites.Contains(o.CurrentSite) || criteria.AccessSites.Contains(o.Assign2Site)) &&
                             pro.Status > 0
                             select new WorklistSearchQueryResult
                             {
                                 o = o,
                                 p = p,
                                 pro = pro
                             };

            #region Build Query

            var isFuzzySearch = false;

            if (!String.IsNullOrEmpty(criteria.PatientNo))
            {
                string actualSearchValue = criteria.PatientNo;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientNo, out actualSearchValue);
                if (searchingType == SearchingType.Exact)
                {
                    orderQuery = orderQuery.Where(q => q.p.PatientNo.Equals(actualSearchValue));
                }
                else
                {
                    isFuzzySearch = true;
                    orderQuery = orderQuery.Where(q => q.p.PatientNo.Contains(actualSearchValue));
                }
            }
            if (!String.IsNullOrEmpty(criteria.PatientName))
            {
                string actualSearchValue = criteria.PatientName;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientName, out actualSearchValue);
                if (searchingType == SearchingType.Exact)
                {
                    orderQuery = orderQuery.Where(q => q.p.LocalName.Equals(actualSearchValue));
                }
                else
                {
                    isFuzzySearch = true;
                    orderQuery = orderQuery.Where(q => q.p.LocalName.Contains(actualSearchValue));
                }
            }
            if (!String.IsNullOrEmpty(criteria.AccNo))
            {
                string actualSearchValue = criteria.AccNo;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.AccNo, out actualSearchValue);
                if (searchingType == SearchingType.Exact)
                {
                    orderQuery = orderQuery.Where(q => q.o.AccNo.Equals(actualSearchValue));
                }
                else
                {
                    isFuzzySearch = true;
                    orderQuery = orderQuery.Where(q => q.o.AccNo.Contains(actualSearchValue));
                }
            }
            if (criteria.AccessSites.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.AccessSites.Contains(q.o.CurrentSite));
            }
            if (criteria.PatientTypes.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.PatientTypes.Contains(q.o.PatientType));
            }
            if (criteria.Statuses.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.Statuses.Contains(q.pro.Status));
            }
            if (criteria.ModalityTypes.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.ModalityTypes.Contains(q.pro.ModalityType));
            }
            if (criteria.Modalities.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.Modalities.Contains(q.pro.Modality));
            }

            // limit to search one month data for fuzzy search
            // Case 1: start(N), end(N)
            // Case 2: start(Y), end(N)
            // Case 3: strat(Y), end(Y) -> Case 3.1: end - start > 30 days, Case 3.2: end - start <= 30 days
            // Case 4: start(N), end(Y)
            if (createStartDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.o.CreateTime.HasValue && q.o.CreateTime.Value >= createStartDay.Value);
                // limit to search one month data for fuzzy search
                if (isFuzzySearch)
                {
                    if (!createEndDay.HasValue || (createEndDay.Value - createStartDay) > TimeSpan.FromDays(30))
                    {
                        createEndDay = createStartDay.Value.AddDays(30);
                    }
                }
            }
            else
            {
                // limit to search one month data for fuzzy search
                if (isFuzzySearch)
                {
                    DateTime startDate = DateTime.Today.AddDays(-30);
                    if (createEndDay.HasValue)
                    {
                        startDate = createEndDay.Value.AddDays(-30);
                    }

                    orderQuery = orderQuery.Where(q => q.o.CreateTime > startDate);
                }
            }
            if (createEndDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.o.CreateTime.HasValue && q.o.CreateTime.Value < createEndDay.Value);
            }
            if (examineStartDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.pro.ExamineTime.HasValue && q.pro.ExamineTime.Value >= examineStartDay.Value);
            }
            if (examineEndDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.pro.ExamineTime.HasValue && q.pro.ExamineTime.Value < examineEndDay.Value);
            }
            // filter create time ranges
            if (criteria.CreateTimeRanges != null && criteria.CreateTimeRanges.Count > 0)
            {
                var predicate = BuildWithInOrderCreateTimeRangesPredicate(criteria.CreateTimeRanges);
                orderQuery = orderQuery.AsExpandable().Where(predicate);
            }

            // filter examine time ranges
            if (criteria.ExamineTimeRanges != null && criteria.ExamineTimeRanges.Count > 0)
            {
                var predicate = BuildWithInExamineTimeRangesPredicate(criteria.ExamineTimeRanges);
                orderQuery = orderQuery.AsExpandable().Where(predicate);
            }

            #endregion

            // theoretically, the result should be distinct by order and patient info
            var finalQuery = orderQuery.Select(q => new
            {
                PatientID = q.p.UniqueID,
                q.p.Birthday,
                q.p.LocalName,
                q.p.PatientNo,
                OrderID = q.o.UniqueID,
                q.o.AccNo,
                q.o.PatientType,
                q.o.CurrentSite,
                q.o.ExamSite,
                q.o.CreateTime,
                q.o.CurrentAge,
                q.o.AgeInDays,
                q.o.IsScan,
                q.o.ReferralID,
                q.o.StudyInstanceUID
                //q.pro.ExamineTime//检查时间不同 ，每个检查部位 都有一个检查时间？列表 是否多个？
            }).Distinct().AsNoTracking();

            // pagination orders by create time
            // get two pages items every time, to see if there is items in next page
            var getTwoPagesItemsTask = !criteria.Pagination.NeedNoPagination ?
                finalQuery.OrderByDescending(q => q.CreateTime).Skip(criteria.Pagination.PageSize * (criteria.Pagination.PageIndex - 1)).Take(criteria.Pagination.PageSize * 2).ToListAsync() :
                finalQuery.OrderByDescending(q => q.CreateTime).ToListAsync();
            var twoPagesItems = await getTwoPagesItemsTask;
            var firstPageItems = twoPagesItems.Take(criteria.Pagination.PageSize);

            // assemble result with no procedure info
            var result = new PaginationResult
            {
                Total = finalQuery.Count(),
                Data = firstPageItems.Select(item => new OrderItemDto
                {
                    PatientID = item.PatientID,
                    Birthday = item.Birthday,
                    PatientName = item.LocalName,
                    PatientNo = item.PatientNo,
                    OrderID = item.OrderID,
                    AccNo = item.AccNo,
                    PatientType = item.PatientType,
                    CurrentSite = item.CurrentSite,
                    ExamSite = item.ExamSite,
                    CreatedTime = item.CreateTime.HasValue ? item.CreateTime.Value : DateTime.MinValue,
                    CurrentAge = item.CurrentAge,
                    AgeInDays = item.AgeInDays,
                    IsScan = item.IsScan.HasValue ? item.IsScan.Value.Equals(1) : false,
                    ReferralID = item.ReferralID,
                    StudyInstanceUID = item.StudyInstanceUID
                    //ExamineTime = item.ExamineTime
                }).ToList(),
            };

            var orderIDs = firstPageItems.Select(i => i.OrderID).ToList();
            // fetch all procedures associated with those orders
            await GetProceduresByOrderIDsTwo(orderIDs, result);

            return result;
        }
        private async Task GetProceduresByOrderIDsTwo(IEnumerable<string> orderIDs, PaginationResult result)
        {
            var procedureGroupsTask = (from o in _dbContext.Set<Order>()
                                       join pro in _dbContext.Set<Procedure>() on o.UniqueID equals pro.OrderID
                                       join r in _dbContext.Set<Report>() on pro.ReportID equals r.UniqueID
                                       into subReport
                                       from s in subReport.DefaultIfEmpty()
                                       where orderIDs.Contains(o.UniqueID)
                                       select new
                                       {
                                           pro.OrderID,
                                           pro.UniqueID,
                                           pro.Status,
                                           pro.ModalityType,
                                           pro.Modality,
                                           pro.RPDesc,
                                           pro.CreateTime,
                                           pro.RegisterTime,
                                           pro.ExamineTime,
                                           pro.ReportID,
                                           pro.IsExistImage,
                                           s.IsPrint,
                                           pro.ExamSystem
                                       }).GroupBy(p => p.OrderID).ToListAsync();

            var procedureGroups = await procedureGroupsTask;

            foreach (var o in result.Data as List<OrderItemDto>)
            {
                var procedures = procedureGroups.FirstOrDefault(g => g.Key.Equals(o.OrderID, StringComparison.OrdinalIgnoreCase));
                if (procedures != null)
                {
                    o.Procedures = procedures.Select(p => new ProcedureItemDto
                    {
                        ProcedureID = p.UniqueID,
                        Status = p.Status,
                        ModalityType = p.ModalityType,
                        RPDesc = p.RPDesc,
                        Modality = p.Modality,
                        ReportID = p.ReportID,
                        ExamineTime = p.ExamineTime,
                        IsPrint = p.IsPrint,
                        IsExistImage = p.IsExistImage,
                        OrderId = p.OrderID,
                        ExamSystem = p.ExamSystem
                    }).ToList();
                }
            }
        }
        public async Task<WorklistSearchResultDto> SearchWorklist(WorklistSearchCriteriaDto criteria, string userID, string site, string role)
        {
            if (criteria == null || criteria.Pagination == null)
            {
                return null;
            }

            criteria.AccessSites = await GetAccessSites(userID, site, role);
            // prepare date range by criteria
            var createStartDay = criteria.CreateStartDate.HasValue ? (DateTime?)criteria.CreateStartDate.Value.Date : null;
            var createEndDay = criteria.CreateEndDate.HasValue ? (DateTime?)criteria.CreateEndDate.Value.Date.AddDays(1) : null;
            var examineStartDay = criteria.ExamineStartDate.HasValue ? (DateTime?)criteria.ExamineStartDate.Value.Date : null;
            var examineEndDay = criteria.ExamineEndDate.HasValue ? (DateTime?)criteria.ExamineEndDate.Value.Date.AddDays(1) : null;

            // filter result by all criteria except time ranges
            var orderQuery = from o in _dbContext.Set<Order>()
                             join p in _dbContext.Set<Patient>() on o.PatientID equals p.UniqueID
                             join pro in _dbContext.Set<Procedure>() on o.UniqueID equals pro.OrderID
                             where (criteria.AccessSites.Count == 0 || criteria.AccessSites.Contains(o.CurrentSite) || criteria.AccessSites.Contains(o.Assign2Site)) &&
                             pro.Status > 0
                             select new WorklistSearchQueryResult
                             {
                                 o = o,
                                 p = p,
                                 pro = pro
                             };

            #region Build Query

            var isFuzzySearch = false;

            if (!String.IsNullOrEmpty(criteria.PatientNo))
            {
                string actualSearchValue = criteria.PatientNo;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientNo, out actualSearchValue);
                if (searchingType == SearchingType.Exact)
                {
                    orderQuery = orderQuery.Where(q => q.p.PatientNo.Equals(actualSearchValue));
                }
                else
                {
                    isFuzzySearch = true;
                    orderQuery = orderQuery.Where(q => q.p.PatientNo.Contains(actualSearchValue));
                }
            }
            if (!String.IsNullOrEmpty(criteria.PatientName))
            {
                string actualSearchValue = criteria.PatientName;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientName, out actualSearchValue);
                if (searchingType == SearchingType.Exact)
                {
                    orderQuery = orderQuery.Where(q => q.p.LocalName.Equals(actualSearchValue));
                }
                else
                {
                    isFuzzySearch = true;
                    orderQuery = orderQuery.Where(q => q.p.LocalName.Contains(actualSearchValue));
                }
            }
            if (!String.IsNullOrEmpty(criteria.AccNo))
            {
                string actualSearchValue = criteria.AccNo;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.AccNo, out actualSearchValue);
                if (searchingType == SearchingType.Exact)
                {
                    orderQuery = orderQuery.Where(q => q.o.AccNo.Equals(actualSearchValue));
                }
                else
                {
                    isFuzzySearch = true;
                    orderQuery = orderQuery.Where(q => q.o.AccNo.Contains(actualSearchValue));
                }
            }
            if (criteria.AccessSites.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.AccessSites.Contains(q.o.CurrentSite));
            }
            if (criteria.PatientTypes.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.PatientTypes.Contains(q.o.PatientType));
            }
            if (criteria.Statuses.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.Statuses.Contains(q.pro.Status));
            }
            if (criteria.ModalityTypes.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.ModalityTypes.Contains(q.pro.ModalityType));
            }
            if (criteria.Modalities.Count > 0)
            {
                orderQuery = orderQuery.Where(q => criteria.Modalities.Contains(q.pro.Modality));
            }

            // limit to search one month data for fuzzy search
            // Case 1: start(N), end(N)
            // Case 2: start(Y), end(N)
            // Case 3: strat(Y), end(Y) -> Case 3.1: end - start > 30 days, Case 3.2: end - start <= 30 days
            // Case 4: start(N), end(Y)
            if (createStartDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.o.CreateTime.HasValue && q.o.CreateTime.Value >= createStartDay.Value);
                // limit to search one month data for fuzzy search
                if (isFuzzySearch)
                {
                    if (!createEndDay.HasValue || (createEndDay.Value - createStartDay) > TimeSpan.FromDays(30))
                    {
                        createEndDay = createStartDay.Value.AddDays(30);
                    }
                }
            }
            else
            {
                // limit to search one month data for fuzzy search
                if (isFuzzySearch)
                {
                    DateTime startDate = DateTime.Today.AddDays(-30);
                    if (createEndDay.HasValue)
                    {
                        startDate = createEndDay.Value.AddDays(-30);
                    }

                    orderQuery = orderQuery.Where(q => q.o.CreateTime > startDate);
                }
            }
            if (createEndDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.o.CreateTime.HasValue && q.o.CreateTime.Value < createEndDay.Value);
            }
            if (examineStartDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.pro.ExamineTime.HasValue && q.pro.ExamineTime.Value >= examineStartDay.Value);
            }
            if (examineEndDay.HasValue)
            {
                orderQuery = orderQuery.Where(q => q.pro.ExamineTime.HasValue && q.pro.ExamineTime.Value < examineEndDay.Value);
            }
            // filter create time ranges
            if (criteria.CreateTimeRanges != null && criteria.CreateTimeRanges.Count > 0)
            {
                var predicate = BuildWithInOrderCreateTimeRangesPredicate(criteria.CreateTimeRanges);
                orderQuery = orderQuery.AsExpandable().Where(predicate);
            }

            // filter examine time ranges
            if (criteria.ExamineTimeRanges != null && criteria.ExamineTimeRanges.Count > 0)
            {
                var predicate = BuildWithInExamineTimeRangesPredicate(criteria.ExamineTimeRanges);
                orderQuery = orderQuery.AsExpandable().Where(predicate);
            }

            #endregion

            // theoretically, the result should be distinct by order and patient info
            var finalQuery = orderQuery.Select(q => new
            {
                PatientID = q.p.UniqueID,
                q.p.Birthday,
                q.p.LocalName,
                q.p.PatientNo,
                OrderID = q.o.UniqueID,
                q.o.AccNo,
                q.o.PatientType,
                q.o.CurrentSite,
                q.o.ExamSite,
                q.o.CreateTime,
                q.o.CurrentAge,
                q.o.AgeInDays,
                q.o.IsScan,
                q.o.ReferralID,
                q.pro.ExamineTime
            }).Distinct().AsNoTracking();

            // pagination orders by create time
            // get two pages items every time, to see if there is items in next page
            var getTwoPagesItemsTask = !criteria.Pagination.NeedNoPagination ?
                finalQuery.OrderByDescending(q => q.CreateTime).Skip(criteria.Pagination.PageSize * (criteria.Pagination.PageIndex - 1)).Take(criteria.Pagination.PageSize * 2).ToListAsync() :
                finalQuery.OrderByDescending(q => q.CreateTime).ToListAsync();
            var twoPagesItems = await getTwoPagesItemsTask;
            var firstPageItems = twoPagesItems.Take(criteria.Pagination.PageSize);

            // assemble result with no procedure info
            var result = new WorklistSearchResultDto
            {
                Pagination = new PaginationDto
                {
                    PageIndex = criteria.Pagination.PageIndex,
                    PageSize = criteria.Pagination.PageSize,
                    HasNextPage = twoPagesItems.Count > criteria.Pagination.PageSize
                },
                OrderItems = firstPageItems.Select(item => new OrderItemDto
                {
                    PatientID = item.PatientID,
                    Birthday = item.Birthday,
                    PatientName = item.LocalName,
                    PatientNo = item.PatientNo,
                    OrderID = item.OrderID,
                    AccNo = item.AccNo,
                    PatientType = item.PatientType,
                    CurrentSite = item.CurrentSite,
                    ExamSite = item.ExamSite,
                    CreatedTime = item.CreateTime.HasValue ? item.CreateTime.Value : DateTime.MinValue,
                    CurrentAge = item.CurrentAge,
                    AgeInDays = item.AgeInDays,
                    IsScan = item.IsScan.HasValue ? item.IsScan.Value.Equals(1) : false,
                    ReferralID = item.ReferralID,
                    ExamineTime = item.ExamineTime
                }).ToList(),
            };

            var orderIDs = firstPageItems.Select(i => i.OrderID).ToList();
            // fetch all procedures associated with those orders
            await GetProceduresByOrderIDs(orderIDs, result);

            return result;
        }

        private async Task<List<string>> GetAccessSites(string userID, string site, string role)
        {

            var accessSites = (await _configurationService.GetProfile(userID, site, role, Contants.ProfileKey.AccessSite)).FirstOrDefault();
            if (accessSites != null && !String.IsNullOrEmpty(accessSites.Value))
            {
                return accessSites.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return new List<string>();
        }

        private async Task GetProceduresByOrderIDs(IEnumerable<string> orderIDs, WorklistSearchResultDto result)
        {
            var procedureGroupsTask = (from o in _dbContext.Set<Order>()
                                       join pro in _dbContext.Set<Procedure>() on o.UniqueID equals pro.OrderID
                                       join r in _dbContext.Set<Report>() on pro.ReportID equals r.UniqueID
                                       into subReport
                                       from s in subReport.DefaultIfEmpty()
                                       where orderIDs.Contains(o.UniqueID)
                                       select new
                                       {
                                           pro.OrderID,
                                           pro.UniqueID,
                                           pro.Status,
                                           pro.ModalityType,
                                           pro.Modality,
                                           pro.RPDesc,
                                           pro.CreateTime,
                                           pro.RegisterTime,
                                           pro.ExamineTime,
                                           pro.ReportID,
                                           pro.IsExistImage,
                                           s.IsPrint,
                                           pro.ExamSystem
                                       }).GroupBy(p => p.OrderID).ToListAsync();

            var procedureGroups = await procedureGroupsTask;

            foreach (var o in result.OrderItems)
            {
                var procedures = procedureGroups.FirstOrDefault(g => g.Key.Equals(o.OrderID, StringComparison.OrdinalIgnoreCase));
                if (procedures != null)
                {
                    o.Procedures = procedures.Select(p => new ProcedureItemDto
                    {
                        ProcedureID = p.UniqueID,
                        Status = p.Status,
                        ModalityType = p.ModalityType,
                        RPDesc = p.RPDesc,
                        Modality = p.Modality,
                        ReportID = p.ReportID,
                        IsPrint = p.IsPrint,
                        IsExistImage = p.IsExistImage,
                        OrderId = p.OrderID,
                        ExamSystem = p.ExamSystem
                    }).ToList();
                }
            }
        }

        private static Expression<Func<WorklistSearchQueryResult, bool>> BuildWithInOrderCreateTimeRangesPredicate(List<TimeRangeDto> timeRanges)
        {
            var predicate = PredicateBuilder.False<WorklistSearchQueryResult>();
            foreach (var range in timeRanges)
            {
                predicate = predicate.Or(q => q.o.CreateTime.HasValue &&
                        DbFunctions.CreateTime(q.o.CreateTime.Value.Hour, q.o.CreateTime.Value.Minute, q.o.CreateTime.Value.Second) >= range.Start &&
                        DbFunctions.CreateTime(q.o.CreateTime.Value.Hour, q.o.CreateTime.Value.Minute, q.o.CreateTime.Value.Second) <= range.End);
            }
            return predicate;
        }

        private static Expression<Func<WorklistSearchQueryResult, bool>> BuildWithInExamineTimeRangesPredicate(List<TimeRangeDto> timeRanges)
        {
            var predicate = PredicateBuilder.False<WorklistSearchQueryResult>();
            foreach (var range in timeRanges)
            {
                predicate = predicate.Or(q => q.pro.ExamineTime.HasValue &&
                        DbFunctions.CreateTime(q.pro.ExamineTime.Value.Hour, q.pro.ExamineTime.Value.Minute, q.pro.ExamineTime.Value.Second) >= range.Start &&
                        DbFunctions.CreateTime(q.pro.ExamineTime.Value.Hour, q.pro.ExamineTime.Value.Minute, q.pro.ExamineTime.Value.Second) <= range.End);
            }
            return predicate;
        }

        #endregion

        #region Shortcut

        public void AddSearchCriteriaShortcut(SearchCriteriaShortcutDto shortcut)
        {
            var existedShortcut = _dbContext.Set<Shortcut>().Where(s => s.Owner.Equals(shortcut.Owner, StringComparison.OrdinalIgnoreCase)
                    && s.Category == WorklistService.Category
                    && s.Name.Equals(shortcut.Name, StringComparison.OrdinalIgnoreCase)).Take(1).FirstOrDefault();
            if (existedShortcut != null)
            {
                if (!shortcut.IgnoreNameDuplicated)
                {
                    throw new DuplicateNameException();
                }
                else
                {
                    shortcut.IsDefault = existedShortcut.Type == 1;
                    _shortcutRepository.Delete(existedShortcut);
                }
            }

            shortcut.Value = JsonSerializer<WorklistSearchCriteriaDto>.ToJson(shortcut.criteria);
            _shortcutRepository.Add(Mapper.Map<SearchCriteriaShortcutDto, Shortcut>(shortcut));
            _shortcutRepository.SaveChanges();
        }

        public SearchCriteriaShortcutDto GetSearchCriteriaShortcut(string shortcutID)
        {
            var shortcut = _shortcutRepository
                .Get(s => s.UniqueID.Equals(shortcutID.ToString(), StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (shortcut != null)
            {
                var result = Mapper.Map<Shortcut, SearchCriteriaShortcutDto>(shortcut);
                result.criteria = JsonSerializer<WorklistSearchCriteriaDto>.FromJson(result.Value);
                return result;
            }
            return null;
        }

        public void DeleteSearchCriteriaShortcut(string shortcutID)
        {
            var shortcut = _shortcutRepository
                .Get(s => s.UniqueID.Equals(shortcutID, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (shortcut != null)
            {
                _shortcutRepository.Delete(shortcut);
                _shortcutRepository.SaveChanges();
            }
        }

        public void SetDetaultSearchCriteriaShortcut(string shortcutID)
        {
            var shortcut = _shortcutRepository
                .Get(s => s.UniqueID.Equals(shortcutID, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (shortcut != null)
            {
                var userID = shortcut.Owner;
                var originals = _shortcutRepository
                    .Get(s => s.Owner.Equals(userID, StringComparison.OrdinalIgnoreCase) && s.Type == 1);
                foreach (var original in originals)
                {
                    original.Type = 0;
                }
                shortcut.Type = 1;
                _shortcutRepository.SaveChanges();
            }
        }

        public IEnumerable<SearchCriteriaShortcutDto> GetSearchCriteriaShortcuts(string userID)
        {
            var shortcuts = _shortcutRepository
                .Get(s => s.Owner.Equals(userID, StringComparison.OrdinalIgnoreCase) && s.Category == Category)
                .Select(s => Mapper.Map<Shortcut, SearchCriteriaShortcutDto>(s)).OrderBy(s => s.Name).ToList();
            foreach (var shortcut in shortcuts)
            {
                shortcut.criteria = JsonConvert.DeserializeObject<WorklistSearchCriteriaDto>(shortcut.Value, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            }
            return shortcuts;
        }

        #endregion
    }
}
