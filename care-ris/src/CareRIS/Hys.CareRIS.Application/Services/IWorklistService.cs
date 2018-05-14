using Hys.CareRIS.Application.Dtos;
using Hys.CrossCutting.Common.Interfaces;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services
{
    public interface IWorklistService : IDisposable
    {
        Task<PaginationResult> GetWorklist(WorklistSearchCriteriaDto criteria, string userID, string site, string role);
        Task<WorklistSearchResultDto> SearchWorklist(WorklistSearchCriteriaDto criteria, string userID, string site, string role);

        void AddSearchCriteriaShortcut(SearchCriteriaShortcutDto shortcut);
        SearchCriteriaShortcutDto GetSearchCriteriaShortcut(string shortcutID);
        void DeleteSearchCriteriaShortcut(string shortcutID);
        void SetDetaultSearchCriteriaShortcut(string shortcutID);
        IEnumerable<SearchCriteriaShortcutDto> GetSearchCriteriaShortcuts(string userID);
    }
}
