using System.Collections.Generic;
using System.Threading.Tasks;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Kendo.DynamicLinq;
using Hys.CareRIS.Application.Dtos;
using Hys.CrossCutting.Common;

namespace Hys.CareRIS.Application.Services
{
    public interface IUserManagementService
    {
        UserDto GetUser(string userName);
        void UpdateUserPassword(string userId, string password);
        UserDto GetUserByID(string userID);

        int LoginToOnline(OnlineClientDto onlineClientDto, string isForce, out string message);
        bool LogoutToOffline(OnlineClientDto onlineClientDto);
        bool StartToOffline(string url);
        bool IsOnline(OnlineClientDto onlineClientDto);
        bool UserOffline(string userIDIP);
        void UpdateInvalidCount(string userID, string domain);
        void UpdateInvalidCount(string userID);
        
        Task<IEnumerable<RoleDto>> GetUserRolesAsync(string userID);
        string GetUserDefaultRole(UserDto user);
        Task<bool> UpdateRisUserDefaultRoleAsync(string userID, string domain, Dictionary<string, string> data);
        bool IsPublicAccount(string email);

        DataSourceResult GetPageUsers(PageRequest<string[]> request);
        /// <summary>
        /// 本地显示名验证
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        bool DisplayNameExist(UserDto userInfo);
        /// <summary>
        /// 验证登录名
        /// </summary>
        /// <param name="logionName"></param>
        /// <returns></returns>
        bool UserLoginNameExist(string logionName);
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        bool SaveUser(UserDto userInfo);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        bool UpdateUser(UserDto userInfo);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="strUserGuid"></param>
        /// <param name="strDomain"></param>
        /// <returns></returns>
        bool DeleteUser(string strUserGuid, string strDomain);
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleDto> GetRoles();
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IEnumerable<RoleDto> GetUserRoles(string userID);
        /// <summary>
        /// 更新角色用户关系
        /// </summary>
        /// <param name="roleToUsers"></param>
        /// <returns></returns>
        bool UpdateRoleToUser(List<RoleToUserDto> roleToUsers);
        /// <summary>
        /// 获取用户权限配置
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        string GetUserProfiles(string userID);
        /// <summary>
        /// 获取影像科室
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        IEnumerable<DictionaryValueDto> GetDepart(string domain);
        ///// <summary>
        ///// 更新用户权限配置
        ///// </summary>
        ///// <param name="userRrofiles"></param>
        ///// <returns></returns>
        //bool UpdateUserProfile(List<UserProfileDto> userProfiles);
        /// <summary>
        /// 获取所有职称
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        IEnumerable<DictionaryValueDto> GetTitles(string domain);
        /// <summary>
        /// 根据角色获取用户
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetUserByRoleName(List<string> roleName, string domain);

        int UpdatePassword(UserPwdDto upwd);

    }
}
