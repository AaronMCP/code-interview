using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CrossCutting.Common.Interfaces;
using Kendo.DynamicLinq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services
{
    public interface IConfigurationService
    {
        Task<IEnumerable<ModalityDto>> GetModalities();
        Task<IEnumerable<ModalityDto>> GetModalities(string site);
        Task<IEnumerable<ModalityDto>> GetModalities(string site, string type);
        Task<IEnumerable<DictionaryDto>> GetDictionaries(string site);
        Task<IDictionary<int, DictionaryDto>> GetDictionaries(string site, int[] tags);
        Task<IEnumerable<DictionaryValueDto>> GetDictionariesList(string site, int[] tags);
        Task<IEnumerable<ApplyDeptDto>> GetApplyDepts();
        Task<IEnumerable<ApplyDeptLiteDto>> GetApplyDepts(string site);
        Task<IEnumerable<ApplyDoctorDto>> GetApplyDoctors();
        Task<IEnumerable<ApplyDoctorLiteDto>> GetApplyDoctors(string site);
        string ConvertFirstPY(string name);
        Task<IEnumerable<SystemProfileDto>> GetSystemProfiles(string[] profileNames = null);
        Task<IEnumerable<SiteProfileDto>> GetSiteProfiles(string site, string[] profileNames = null);
        Task<IEnumerable<RoleProfileDto>> GetRoleProfiles(string roleName, string site, string[] profileNames = null);
        Task<IEnumerable<UserProfileDto>> GetUserProfiles(string userID, string[] profileNames = null);
        bool SaveUserProfiles(UserProfileDto userProfileDto);
        Task<ProfilesDto> GetProfiles(string userID, string site, string role);
        Task<Dictionary<string, IEnumerable<ProfileLiteDto>>> GetProfiles(string userID, string site, string role, string[] profileNames);
        Task<IEnumerable<ProfileLiteDto>> GetProfile(string userID, string site, string role, string profileName);
        SiteDto GetSite(string siteName);
        ClientConfigDto GetClientConfig(string id);
        bool SaveClientConfig(ClientConfigDto config);
        IEnumerable<SiteDto> GetAllSites();
        Task<List<string>> GetUserSites(string userID, string site, string role);
        #region 疾病编码
        DataSourceResult SearchICD(DataSourceRequest request);
        /// <summary>
        /// 下拉框分页
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        SearchResultDto SearchICDs(SearchCriteriaDto criteria,string domain);
        Task<IEnumerable<ICDTenDto>> GetAlICD();
        bool SaveICD(ICDTenDto icdInfo,string domain);
        ICDTenDto GetICDByID(string id);
        bool UpdateICD(ICDTenDto icdInfo);
        bool DelICD(ICDTenDto icdInfo);

        #endregion

        #region 检查代码
        DataSourceResult SearchProcedureCodeList(DataSourceRequest request);

        IEnumerable<ProcedureCodeDto> GetAllProcedureCode();
        IEnumerable<ModalityTypeDto> GetAllModalityType();
        IEnumerable<ModalityDto> GetModality(string cType);

        bool SaveProcedureCode(ProcedureCodeDto pcInfo,string site, string domain);

        bool UpdateProcedureCode(ProcedureCodeDto pcInfo);

        ProcedureCodeDto GetProcedureFrequency(ProcedureCodeDto pcInfo);
        bool DelProcedureCode(ProcedureCodeDto pcInfo);

        bool AddBodySystemMap(BodySystemMapDto bsmInfo);

        BodySystemMapDto GetBodySystemMap(BodySystemMapDto bsmInfo);

        bool IsBodyPartExist(BodySystemMapDto bsmInfo);

        bool UpdateFrequency(ProcedureCodeDto model);
        bool UpdateFrequencyList(List<ProcedureCodeDto> models);
        #endregion

        #region 角色管理
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<IEnumerable<RoleDirDto>> GetRoleNodes(string domain);
        /// <summary>
        /// 获取角色配置信息
        /// roleInfo.RoleName,roleInfo.Domain
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        string RoleProfile(RoleDto roleInfo, string userId);

        string GetRoleProfile(string roleName, string domain, string userId);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        bool AddRole(RoleDto roleInfo);
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        bool UpdateRole(RoleDto roleInfo);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        bool DelRole(RoleDto roleInfo);

        #endregion

        #region 系统字典
        /// <summary>
        /// 根据站点获取系统字典 站点为空获取所有
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        Task<IEnumerable<DictionaryDto>> GetSysDictionaries(string site);

        bool SaveDictionaries(List<DictionaryDto> dics);
        /// <summary>
        /// 获取指定tag 字典的值 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        Task<IEnumerable<DictionaryValueDto>> GetDictionariesList(int tag, string site);

        int AddDictionaryValue(DictionaryValueDto dvDto);

        int UpdateDictionaryValue(DictionaryValueDto dvDto);
        bool DeleteDictionaryValue(DictionaryValueDto dvDto);
        /// <summary>
        /// 字典关联
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        IEnumerable<DictionaryMapDto> GetDicMappings(string site);

        bool UpdateDicMappings(List<DictionaryMapDto> dicMaps);

        #endregion

        #region 申请部门&申请医生
        int AddApplyDept(ApplyDeptDto deptDto, string domain);
        int UpdateApplyDept(ApplyDeptDto deptDto, string domain);
        bool DeleteApplyDept(string deptId);
        int AddApplyDoctor(ApplyDoctorDto doctorDto, string domain);
        int UpdateApplyDoctor(ApplyDoctorDto doctorDto, string domain);

        bool DeleteApplyDoctor(string doctorId);
        #endregion

        #region 系统配置
        string SystemProfile(string domain, string site);

        bool SaveSystemProfile(SystemProfileDto spdto);
        #endregion

        #region 资源管理
        List<ModalityTypeDto> GetModalityTypeNode();

        List<ModalityDto> GetModalityNode(string type);

        int AddModality(ModalityDto mod);
        int UpdateModality(ModalityDto mod);
        bool DelModality(string id);

        ModalityDto GetModalityByName(string type, string name);

        List<ScanningTechDto> GetScanningTechs(string type, string site, string domain);

        int AddScanningTech(ScanningTechDto mod);
        int UpdateScanningTech(ScanningTechDto std);
        bool DelScanningTech(string id);
        #endregion
        //集团医院
        List<DomainListDto> GetDomainList();
        int UpdateDomain(DomainListDto domain);
        List<SiteDto> GetSiteList();
        SiteDto GetSite(string siteName, string domain);
        int AddSite(SiteDto site);
        int UpdateSite(SiteDto site);
        bool DeleteSite(string name);
        string SiteProfile(string domain, string site);
        bool AddProfile(SiteProfileDto siteProDto);
        bool SaveSiteProfile(List<SiteProfileDto> siteProList);

        bool DelSiteProfile(SiteProfileDto siteProDto);

        //管理员工具 
        //解锁
        PaginationResult SearchLocks(SyncSearchCriteriaDto request);
        bool DelLock(SyncDto lk);
        bool DelLocks(List<SyncDto> lks);
    }
}
