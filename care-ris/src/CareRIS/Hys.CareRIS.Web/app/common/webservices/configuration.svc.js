// configuration web service proxy
webservices.factory('configurationService', ['$http', 'apiConfig', function ($http, apiConfig) {
    'use strict';
    return {
        getProfiles: function (userId) {
            return $http.get('/configuration/profiles/' + userId, apiConfig.create());
        },

        getProfileValues: function (userId, names) {
            var config = { params: { name: names } };
            return $http.get('/configuration/profiles/' + userId + '/query', apiConfig.create(config));
        },

        getModalityTypes: function () {
            return $http.get('/common/modalitytypes', apiConfig.create());
        },
        getUserSites: function () {
            return $http.get('/configuration/usersites', apiConfig.create());
        },
        getModalities: function (site, type) {
            var config;
            if (type) {
                config = { params: { modalityType: type } };
            }
            return $http.get('/configuration/modalities/' + site, apiConfig.create(config));
        },
        getModalitiesByType: function (site, type) {
            var config = {
                params: {
                    modalityType: type,
                    site: site
                }
            };
            return $http.get('/configuration/sitemodalities', apiConfig.create(config));
        },
        getDictionaries: function (site) {
            site = site == null ? '' : site;
            return $http.get('/configuration/dictionaries/' + site, apiConfig.create());
        },

        getDictionariesByTags: function (site, tags) {
            site = site == null ? '' : site;
            var config = { params: { tag: tags } };
            return $http.get('/configuration/dictionaries/' + site + '/query', apiConfig.create(config));
        },

        getApplyDepts: function (site) {
            return $http.get('/configuration/applydepts/' + site, apiConfig.create());
        },

        getApplyDoctors: function (site) {
            return $http.get('/configuration/applydoctors/' + site, apiConfig.create());
        },

        getUserName: function (userId) {
            return $http.get('/configuration/getusername/' + userId, apiConfig.create());
        },
        SaveUserProfiles: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.put('/configuration/userprofile', data, apiConfig.create(config));
        },
        convertFirstPY: function (name) {
            return $http.get('/configuration/convertfirstpy/' + name, apiConfig.create());
        },
        getClientIP: function () {
            return $http.get('/common/clientip', apiConfig.create());
        },
        getServerDate: function () {
            return $http.get('/common/serverdate', apiConfig.create());
        },
        getServerTime: function () {
            return $http.get('/common/servertime', apiConfig.create());
        },
        getClientConfig: function (identity) {
            return $http.get('/configuration/settings/clientconfig/' + identity, apiConfig.create());
        },
        saveClientConfig: function (config) {
            return $http.post('/configuration/settings/clientconfig/', config, apiConfig.create());
        },
        getAllSite: function () {
            return $http.get('/common/allsite', apiConfig.create());
        },
        getImOnlineUser: function (site) {
            var config = { params: { site: site } };
            return $http.get('/common/imonlineuser', apiConfig.create(config));
        },
        getRisUserRoles: function (userid) {
            return $http.get('/consultation/users/{0}/roles'.format(userid), apiConfig.create());
        },
        updateRisUserDefaultRole: function (userid, domain, data) {
            return $http.post('/consultation/users/{0}/domain/{1}/defaultRole'.format(userid, domain), data, apiConfig.create());
        },
        getLicenseData: function () {
            return $http.get('/configuration/license', apiConfig.create());
        },
        getCurrentUser: function () {
            return $http.get('/common/currentuser', apiConfig.create());
        },
        getallIcds: function () {
            return $http.get('/configuration/settings/allicd', apiConfig.create());
        },
        getIcds: function (data) {
            return $http.post('/configuration/settings/icds', data, apiConfig.create({ isBusyRequest: true }));
        },
        addIcd: function (icdInfo) {
            return $http.post('/configuration/settings/icd', icdInfo, apiConfig.create({ isBusyRequest: true }));
        },
        modifyIcd: function (icdInfo) {
            return $http.post('/configuration/settings/icdupdate', icdInfo, apiConfig.create({ isBusyRequest: true }));
        },
        deleteIcd: function (icdInfo) {
            return $http.put('/configuration/settings/icddelete', icdInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //检查代码
        getCheckCodeList: function (data) {
            return $http.post('/configuration/settings/procedurecodes', data, apiConfig.create({ isBusyRequest: true }));
        },
        //删除检查代码
        deletCheckCodeRow: function (checkCodeInfo) {
            return $http.put('/configuration/settings/procedurecodedelete', checkCodeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //修改
        editCheckCodeRow: function (checkCodeInfo) {
            return $http.post('/configuration/settings/procedurecodeupdate', checkCodeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //添加
        addCheckCodeRow: function (checkCodeInfo) {
            return $http.post('/configuration/settings/procedurecodeadd', checkCodeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //获取部位分类
        getProcedureCodeText: function () {
            return $http.get('/configuration/settings/procedurecodetext', apiConfig.create());
        },
        //获取检查部位
        getBodySystemMapText: function () {
            return $http.get('/registration/bodySystemMapText', apiConfig.create());
        },
        //获取默认设备
        getModality: function (cType) {
            return $http.get('/configuration/settings/getmodality/' + cType, apiConfig.create());
        },
        //获取站点所有信息
        getSiteDictionaries: function (criteria) {
            var config = {
                params: {
                    query: JSON.stringify(criteria)
                },
                isBusyRequest: true
            }
            return $http.get('/configuration/dictionaries/query', apiConfig.create(criteria));
        },
        getTimeslice: function (modality, dateType) {
            return $http.get('/schedule/timeslice', apiConfig.create({
                isBusyRequest: true,
                params: {
                    modality: modality,
                    dateType: dateType
                }
            }));
        },
        addTimeslice: function (params) {
            return $http.post('/schedule/timeslice', params, apiConfig.create({ isBusyRequest: true }));
        },
        modifyTimeslice: function (sliceId, params) {
            return $http.put('/schedule/timeslice/' + sliceId, params, apiConfig.create({ isBusyRequest: true }));
        },
        delTimeslice: function (sliceIds) {
            return $http.post('/schedule/timeslice/del', sliceIds, apiConfig.create({ isBusyRequest: true }));
        },
        copyTimeslice: function (params) {
            return $http.post('/schedule/timeslicecopy', params, apiConfig.create({ isBusyRequest: true }));
        },
        //获取检查项目
        getCheckingItemText: function () {
            return $http.get('/configuration/settings/checkingitemtext', apiConfig.create());
        },
        //获取检查系统
        getBodySystemMap: function (dsmInfo) {
            return $http.post('/configuration/settings/bodysystem', dsmInfo, apiConfig.create());
        },
        //获取检查系统Text
        getBodySystemMapsText: function (site) {
            return $http.get('/registration/bodySystemMapsText/' + site, apiConfig.create());
        },
        //添加检查部位
        addBodySystemMap: function (bodyInfo) {
            return $http.post('/configuration/settings/bodysystemmapadd', bodyInfo, apiConfig.create());
        },
        //判断检查部位是否存在
        isBodyPartExist: function (bodyInfo) {
            return $http.post('/configuration/settings/isbodypartexist', bodyInfo, apiConfig.create());
        },
        //获取所有信息
        getAllProcedureCode: function () {
            return $http.get('/configuration/settings/allprocedurecode', apiConfig.create());
        },
        //修改频率
        updateFrequency: function (changeInfo) {
            return $http.post('/configuration/settings/updatefrequencylist', changeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //获取影像科室
        getDepart: function () {
            return $http.get('/configuration/settings/departs', apiConfig.create());
        },
        //获取用户列表
        getUserList: function (data, roles) {
            var requestData = {
                request: data,
                specify: roles
            };
            return $http.post('/configuration/userlist', requestData, apiConfig.create({ isBusyRequest: true }));
        },
        //本地显示名验证
        displayNameExist: function (userInfo) {
            return $http.post('/configuration/settings/isdisplaynameexist', userInfo, apiConfig.create());
        },
        //新增用户
        saveUser: function (userInfo) {
            return $http.post('/configuration/settings/saveuser', userInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //登录名验证
        loginNameExist: function (loginName) {
            return $http.get('/configuration/settings/loginnameexist/' + loginName, apiConfig.create());
        },
        //修改用户信息
        updateUser: function (userInfo) {
            return $http.post('/configuration/settings/updateuser', userInfo, apiConfig.create({ isBusyRequest: true }));
        },
        deleteUser: function (userId) {
            return $http.get('/configuration/settings/deleteuser/' + userId, apiConfig.create({ isBusyRequest: true }));
        },
        //获取所有角色
        getRoles: function () {
            return $http.get('/configuration/settings/allroles', apiConfig.create({ isBusyRequest: true }));
        },
        //获取用户角色
        getUserRoles: function (userID) {
            return $http.get('/configuration/settings/userroles/' + userID, apiConfig.create());
        },
        //修改角色用户关系
        updateRoleToUser: function (roleToUsers) {
            return $http.post('/configuration/settings/updateroletouser', roleToUsers, apiConfig.create());
        },
        //修改用户权限配置
        updateUserProfile: function (userProfiles) {
            return $http.post('/configuration/settings/updateuserrrofiles', userProfiles, apiConfig.create());
        },
        //获取用户权限配置
        getUserProfiles: function (userID) {
            return $http.get('/configuration/settings/userprofilesbyuserid/' + userID, apiConfig.create({ isBusyRequest: true }));
        },
        //获取所有职称
        getTitles: function () {
            return $http.get('/configuration/settings/titles', apiConfig.create());
        },
        //时间共享片
        shareTimeSlice: function (shareInfo) {
            return $http.put('/schedule/timesliceshare', shareInfo, apiConfig.create());
        },
        //获取时间片
        getSharedTimeslice: function (sliceId) {
            return $http.get('/schedule/timesliceshare/' + sliceId, apiConfig.create());
        },
        //获取频率
        getProcedureFrequency: function (icdInfo) {
            return $http.post('/configuration/settings/procedurefrequency', icdInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //获取角色管理所有角色
        getAllRoleNodes: function () {
            return $http.get('/configuration/settings/rolenodes', apiConfig.create({ isBusyRequest: true }));
        },
        //获取角色配置信息
        getRoleProfiles: function (roleInfo) {
            return $http.post('/configuration/settings/roleprofiles', roleInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //保存角色
        saveRole: function (roleInfo) {
            return $http.post('/configuration/settings/saverole', roleInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //修改角色
        updateRole: function (roleInfo) {
            return $http.post('/configuration/settings/updaterole', roleInfo, apiConfig.create({ isBusyRequest: true }));
        },
        //删除角色
        deleteRole: function (roleInfo) {
            return $http.post('/configuration/settings/delrole', roleInfo, apiConfig.create({ isBusyRequest: true }));
        },
        refreshToken: function () {
            return $http.get('/common/refreshtoken', apiConfig.create());
        },
        selectSearch: function (criteria) {
            var config = {
                params: {
                    query: JSON.stringify(criteria)
                }
            };
            return $http.get('/configuration/settings/selecticds', apiConfig.create(config));
        },
        //修改用户密码
        updatePassword: function (params) {
            return $http.post('/configuration/settings/updatepwd', params, apiConfig.create({ isBusyRequest: true }));
        },
        //获取系统字典
        getSysDictionaries: function (site) {
            var config = {
                isBusyRequest: true,
                params: {
                    site: site || ''
                }
            };
            return $http.get('/configuration/settings/sysdictionaries', apiConfig.create(config));
        },
        //修改角色用户关系
        SaveDictionaries: function (dics) {
            return $http.post('/configuration/settings/savedictionaries', dics, apiConfig.create({ isBusyRequest: true }));
        },
        getDictionariesList: function (tag, site) {
            site = site ? site : '';
            var config = {
                params: {
                    site: site,
                    tag: tag
                }
            };
            return $http.get('/configuration/settings/dictionariesbytag/', apiConfig.create(config));
        },
        SaveDictionaryValue: function (dvDto) {
            return $http.post('/configuration/settings/savedictionaryvalue', dvDto, apiConfig.create({ isBusyRequest: true }));
        },
        UpdateDictionaryValue: function (dvDto) {
            return $http.post('/configuration/settings/updatedictionaryvalue', dvDto, apiConfig.create({ isBusyRequest: true }));
        },
        DelDictionaryValue: function (dvDto) {
            return $http.post('/configuration/settings/deldictionaryvalue', dvDto, apiConfig.create({ isBusyRequest: true }));
        },
        //获取字典关联
        GetDicMappings: function (site) {
            var config = {
                isBusyRequest: true,
                params: {
                    site: site || ''
                }
            };
            return $http.get('/configuration/settings/getdicmappings', apiConfig.create(config));
        },
        saveDicMappings: function (mapdics) {
            return $http.post('/configuration/settings/savedicmappings', mapdics, apiConfig.create({ isBusyRequest: true }));
        },
        getAllApplyDepts: function () {
            return $http.get('/configuration/allapplydepts', apiConfig.create());
        },
        getAllApplyDoctors: function () {
            return $http.get('/configuration/allapplydoctors', apiConfig.create());
        },
        deleteApplyDept: function (id) {
            var config = {
                isBusyRequest: true
            };
            return $http.delete('/configuration/settings/deleteapplydept/' + id, apiConfig.create(config));
        },
        addApplyDept: function (deptDto) {
            return $http.post('/configuration/settings/addapplydept', deptDto, apiConfig.create({ isBusyRequest: true }));
        },
        updateApplyDept: function (deptDto) {
            return $http.put('/configuration/settings/updateapplydept', deptDto, apiConfig.create({ isBusyRequest: true }));
        },
        addApplyDoctor: function (doctorDto) {
            return $http.post('/configuration/settings/addapplydoctor', doctorDto, apiConfig.create({ isBusyRequest: true }));
        },
        updateApplyDoctor: function (doctorDto) {
            return $http.put('/configuration/settings/updateapplydoctor', doctorDto, apiConfig.create({ isBusyRequest: true }));
        },
        deleteApplyDoctor: function (id) {
            var config = {
                isBusyRequest: true
            };
            return $http.delete('/configuration/settings/deleteapplydoctor/' + id, apiConfig.create(config));
        },
        //获取系统配置信息
        getSystemProfiles: function (domain) {
            return $http.get('/configuration/settings/systemProfiles/' + domain, apiConfig.create({ isBusyRequest: true }));
        },
        //修改系统配置信息
        updateSystemProfiles: function (params) {
            return $http.post('/configuration/settings/savesystemprofile/', params, apiConfig.create({ isBusyRequest: true }));
        },
        //获取设备节点
        getModalityTypeNode: function () {
            return $http.get('/configuration/settings/getmodalitytypenode', apiConfig.create({ isBusyRequest: true }));
        },
        //获取设备信息
        getModalitybyName: function (param) {
            var config = {
                isBusyRequest: true,
                params: {
                    name: param.name,
                    type: param.type
                }
            }
            return $http.get('/configuration/settings/getmodalitybyName', apiConfig.create(config));
        },
        //新增设备
        addModality: function (modality) {
            return $http.post('/configuration/settings/addmodality', modality, apiConfig.create({ isBusyRequest: true }));
        },
        //删除设备
        deleteModality: function (id) {
            return $http.delete('/configuration/settings/deletemodality/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        //修改设备
        updateModality: function (modality) {
            return $http.put('/configuration/settings/updatemodality', modality, apiConfig.create({ isBusyRequest: true }));
        },
        //获取站点扫描技术
        getScanningTech: function (scan) {
            var config = {
                isBusyRequest: true,
                params: {
                    site: scan.site,
                    type: scan.type
                }
            }
            return $http.get('/configuration/settings/getscanningtechs', apiConfig.create(config));
        },
        //新增扫描技术
        addScanningTech: function (scan) {
            return $http.post('/configuration/settings/addscanningtech', scan, apiConfig.create({ isBusyRequest: true }));
        },
        //修改扫描技术
        updateScanningtech: function (scan) {
            return $http.put('/configuration/settings/updatescanningtech', scan, apiConfig.create({ isBusyRequest: true }));
        },
        //删除扫描技术
        deleteScanningtech: function (id) {
            return $http.delete('/configuration/settings/deletescanningtech/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        //获取医院
        getDomainList: function () {
            return $http.get('/configuration/settings/getdomainlist', apiConfig.create({ isBusyRequest: true }));
        },
        //修改医院
        updateDomain: function (domain) {
            return $http.put('/configuration/settings/updatedomain', domain, apiConfig.create({ isBusyRequest: true }));
        },
        //获取站点
        getSiteList: function () {
            return $http.get('/configuration/settings/getsitelist', apiConfig.create());
        },
        //获取站点详细
        getSitedli: function (param) {
            var congif = {
                params: {
                    site: param.site,
                    domain: param.domain
                }
            }
            return $http.get('/configuration/settings/getsitedli', apiConfig.create(config));
        },
        //新增站点
        addSite: function (site) {
            return $http.post('/configuration/settings/addnewsite', site, apiConfig.create({ isBusyRequest: true }));
        },
        //修改站点
        updateSite: function (site) {
            return $http.put('/configuration/settings/updatesite', site, apiConfig.create({ isBusyRequest: true }));
        },
        //删除站点
        deleteSite: function (id) {
            return $http.delete('/configuration/settings/deletesite/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        //获取站点配置
        getSiteProfile: function (param) {
            var config = {
                isBusyRequest: true,
                params: {
                    site: param.site,
                    domain: param.domain
                }
            }
            return $http.get('/configuration/settings/siteprofile', apiConfig.create(config));
        },
        //新增站点配置
        addSiteProfile: function (siteProlist) {
            return $http.post('/configuration/settings/addprofile', siteProlist, apiConfig.create({ isBusyRequest: true }));
        },
        //修改站点配置
        updateSiteProfile: function (siteProlist) {
            return $http.post('/configuration/settings/savesiteprofile', siteProlist, apiConfig.create({ isBusyRequest: true }));
        },
        //删除站点配置
        deleteSiteProfile: function (siteProDto) {
            return $http.post('/configuration/settings/delsiteprofile', siteProDto, apiConfig.create({ isBusyRequest: true }));
        },
        searchLocks: function (criteria) {
            var config = {
                params: {
                    query: JSON.stringify(criteria)
                },
                isBusyRequest: true
            };
            return $http.get('/configuration/settings/getlocks', apiConfig.create(config));
        },
        //删除设备
        openLock: function (lock) {
            return $http.post('/configuration/settings/dellock', lock, apiConfig.create({ isBusyRequest: true }));
    },
    };
}]);