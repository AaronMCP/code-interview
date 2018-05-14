webservices.factory('consultationService', ['$http', 'apiConfig', function ($http, apiConfig) {
    'use strict';
    return {
        getRequests: function (searchCriteria) {
            var config = { params: { query: JSON.stringify(searchCriteria) } };
            return $http.get('/consultationrequests', apiConfig.create(config));
        },
        getSpecialistRequests: function (searchCriteria) {
            var config = { params: { query: JSON.stringify(searchCriteria) } };
            return $http.get('/consultation/specialistrequests', apiConfig.create(config));
        },
        addShortcut: function (shortcut) {
            return $http.post('/shortcuts', shortcut, apiConfig.create());
        },
        getShortcuts: function (userId, category) {
            var query = { userId: userId, category: category };
            var config = { params: { query: JSON.stringify(query) } };
            return $http.get('/shortcuts', apiConfig.create(config));
        },
        updateShortcut: function (shortcut) {
            return $http.put('/shortcut/' + shortcut.uniqueId, shortcut, apiConfig.create());
        },
        deleteShortcut: function (shortcutId) {
            return $http.delete('/shortcut/' + shortcutId, apiConfig.create());
        },
        getPatientCases: function (searchCriteria) {
            var config = { params: { query: JSON.stringify(searchCriteria) } };
            return $http.get('/consultationpatientcases', apiConfig.create(config));
        },
        getExamModules: function (owner) {
            var config = config = { params: { owner: 0 } };
            if (owner) {
                config = { params: { owner: owner} };
            }

            return $http.get('/consultation/configuration/ExamModules', apiConfig.create(config));
        },
        updateModule: function (module) {
            return $http.post('/consultation/configuration/UpdateModule', module, apiConfig.create());
        },
        getUserDam: function () {
            return $http.get('/consultation/configuration/userdam', apiConfig.create());
        },
        getUserDamIdAsync: function () {
            return $http.get('/consultation/configuration/userdamidAsync', apiConfig.create());
        },
        getDams: function () {
            return $http.get('/consultation/configuration/dams', apiConfig.create());
        },
        getPersons: function () {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/persons', apiConfig.create(config));
        },
        createPatientCase: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/patientcase/newpatientcase', data, apiConfig.create(config));
        },
        GetCombinePatientCaseList: function (patientId, identityCard) {
            var config = { params: { patientId: patientId, identityCard: identityCard } };
            return $http.get('/patientcase/samepatientcases', apiConfig.create(config));
        },
        GetCombinePatientCaseListAsync: function (combinePatientCase) {
            var config = { params: { query: JSON.stringify(combinePatientCase) } };
            return $http.get('/patientcase/samepatientcasesasync', apiConfig.create(config));
        },
        getEMRItems: function (patientCaseId, type) {
            var config = { params: { patientCaseId: patientCaseId || '', type: type } };
            return $http.get('/getEMRItems', apiConfig.create(config));
        },
        getRoles: function () {
            return $http.get('/consultation/configuration/roles', apiConfig.create());
        },
        saveRole: function (role) {
            return $http.post('/consultation/configuration/roles', role, apiConfig.create());
        },
        validateRoleName: function (roleId, roleName) {
            return $http.get('/consultation/configuration/roles/{roleId}/names/{roleName}'.formatObj({ roleId: roleId, roleName: roleName }), apiConfig.create());
        },
        getRoleRelatedUser: function (roleId) {
            return $http.get('/consultation/configuration/roles/{0}/usernames'.format(roleId), apiConfig.create());
        },
        getHospitals: function (isCenter) {
            var config = { params: { isCenter: isCenter || false } };
            return $http.get('/consultation/configuration/hospitals', apiConfig.create(config));
        },
        saveHospital: function (hospital) {
            return $http.post('/consultation/configuration/hospitals', hospital, apiConfig.create());
        },
        getDepartments: function () {
            return $http.get('/consultation/configuration/departments', apiConfig.create());
        },
        getUsers: function (filter) {
            var config = { params: { query: JSON.stringify(filter) } };
            return $http.get('/consultation/configuration/users', apiConfig.create(config));
        },
        getUser: function (userId) {
            return $http.get('/consultation/configuration/users/' + userId, apiConfig.create());
        },
        saveUser: function (user) {
            return $http.post('/consultation/configuration/users', user, apiConfig.create());
        },
        updateUser: function (userid, user) {
            return $http.post('/consultation/configuration/users/' + userid, user, apiConfig.create());
        },
        getRecipientConfigs: function () {
            return $http.get('/consultation/configuration/recipientconfigs', apiConfig.create());
        },
        saveRecipientConfig: function (config) {
            return $http.post('/consultation/configuration/recipientconfigs/', config, apiConfig.create());
        },
        getNotificationConfigs: function () {
            return $http.get('/consultation/configuration/notification/configs', apiConfig.create());
        },
        saveNotificationConfigs: function (configs) {
            return $http.post('/consultation/configuration/notification/configs/', configs, apiConfig.create());
        },
        CombinePatientCase: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/patientcase/combinepatientcase', data, apiConfig.create(config));
        },

        getConsultationDetailAsync: function (requestId) {
            return $http.get('/consultationdetail/' + requestId, apiConfig.create());
        },
        createRequest: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/newrequest', data, apiConfig.create(config));
        },
        getDictionaryByType: function (type) {
            return $http.get('/consultation/configuration/dictionaries/' + type, apiConfig.create());
        },
        getServiceType: function () {
            return $http.get('/consultation/configuration/servicetype', apiConfig.create());
        },
        updateReportAdvice: function (data) {
            return $http.put('/consultation/reoprtadvice', data, apiConfig.create());
        },
        updateRequestPatienthistory: function (data) {
            return $http.put('/consultation/patienthistory', data, apiConfig.create());
        },
        updateRequestClinicaldiagnosis: function (data) {
            return $http.put('/consultation/clinicaldiagnosis', data, apiConfig.create());
        },
        updateRequestDescription: function (data) {
            return $http.put('/consultation/requestdescription', data, apiConfig.create());
        },
        updatePatientCaseBaseInfo: function (data) {
            return $http.put('/consultation/patientcasebaseinfo', data, apiConfig.create());
        },
        updateRequestReceive: function (data) {
            return $http.put('/consultation/requestreceive', data, apiConfig.create());
        },
        getReportHistoriesByReportId: function (reportId) {
            return $http.get('/consultation/reporthistory/' + reportId, apiConfig.create());
        },
        getConsultHospitals: function (filter) {
            var config = { params: { query: JSON.stringify(filter) } };
            return $http.get('/selectconsulthospitals', apiConfig.create(config));
        },
        getPatientCaseNoItems: function (id) {
            return $http.get('/patientcase/patientcasenoitems/' + id, apiConfig.create());
        },
        updateRequestChangeReason: function (data) {
            return $http.put('/consultation/requestchangereason', data, apiConfig.create());
        },
        getChangeReason: function (requestId) {
            return $http.get('/consultation/changereason/' + requestId, apiConfig.create());
        },
        getUserHospital: function () {
            return $http.get('/consultation/configuration/userhospital', apiConfig.create());
        },
        getConsultArea: function () {
            return $http.get('/selectconsultarea', apiConfig.create());
        },
        editPatientCase: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/patientcase/editpatientcase', data, apiConfig.create(config));
        },
        examInfoDeleteFile: function (pid, fid) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/examinfodeletefile?pid=' + pid + '&fid=' + fid, apiConfig.create(config));
        },
        examInfoDeleteItem: function (pid, itemid) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/examinfodeleteitem?pid=' + pid + '&itemid=' + itemid, apiConfig.create(config));
        },
        examInfoFileNameChanged: function (pid, fid, fname) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/examinfofilenamechanged?pid=' + pid + '&fid=' + fid + '&fname=' + fname, apiConfig.create(config));
        },
        examInfoItemAdded: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/patientcase/examinfoitemadded', data, apiConfig.create(config));
        },
        examInfoItemEdited: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/patientcase/examinfoitemedited', data, apiConfig.create(config));
        },
        acceptRequest: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/consultation/acceptrequest', data, apiConfig.create(config));
        },
        updateAcceptRequest: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.put('/consultation/acceptrequestinfo', data, apiConfig.create(config));
        },
        createReportAdvice: function (data) {
            return $http.post('/consultation/createreoprtadvice', data, apiConfig.create());
        },
        getTemplateByParentID: function (id) {
            return $http.get('/consultation/gettemplatebyparentid/' + id, apiConfig.create());
        },
        getTemplateID: function (id) {
            return $http.get('/consultation/getreporttemplate/' + id, apiConfig.create());
        },
        completeRequest: function (requestId) {
            return $http.get('/consultation/completerequest/' + requestId, apiConfig.create());
        },
        ReUploadPatientCase: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/reuploadpatientcase/' + id, apiConfig.create(config));
        },
        ReUploadExamItem: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/reuploadexamitem/' + id, apiConfig.create(config));
        },
        ReUploadFileItem: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/reuploadfileitem/' + id, apiConfig.create(config));
        },
        getRecipientConfigsReceiver: function () {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/configuration/recipientconfigsreceiver', apiConfig.create(config));
        },
        getInfoForAcceptRequest: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/infoforacceptrequest/' + id, apiConfig.create(config));
        },
        generatePatientNo: function () {
            return $http.get('/consultation/configuration/generatepatientno', apiConfig.create());
        },
        generatePatientNoAsync: function () {
            return $http.get('/consultation/configuration/generatepatientnoasync', apiConfig.create());
        },
        getConsultationAssigns: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/assigns/' + id, apiConfig.create(config));
        },
        getMeetings: function () {
            var config = { isBusyRequest: true };
            return $http.get('/meetings', apiConfig.create(config));
        },
        getExpertAdvices: function (requestid) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/expertadvices/' + requestid, apiConfig.create(config));
        },
        getAdviceReport: function (requestid) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/advicereport/' + requestid, apiConfig.create(config));
        },
        saveExpertAdvices: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post('/consultation/expertadvices', data, apiConfig.create(config));
        },
        getExpertAdviceReport: function (requestid) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/expertadvicereport/' + requestid, apiConfig.create(config));
        },
        isHost: function (requestid) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/ishost/' + requestid, apiConfig.create(config));
        },
        getHostAdviceReport: function (requestid) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/hostadvicereport/' + requestid, apiConfig.create(config));
        },
        isExpert: function (requestid) {
            var config = { isBusyRequest: true };
            return $http.get('/consultation/isexpert/' + requestid, apiConfig.create(config));
        },
        editReportPermission: function (requestid) {
            return $http.get('/consultation/editReportPermission/' + requestid, apiConfig.create());
        },
        getVNC: function () {
            var config = { isBusyRequest: false };
            return $http.get('/vnc', apiConfig.create(config));
        },
        updateMeetingStatus: function (query) {
            var config = { isBusyRequest: true, params: query };
            return $http.get('/consultation/updatemeetingstatus', apiConfig.create(config));
        },
        deleteRequest: function (data) {
            return $http.put('/consultation/requestdelete', data, apiConfig.create());
        },
        recoverRequest: function (requestid) {
            return $http.get('/consultation/requestrecover/' + requestid, apiConfig.create());
        },
        deletePatientCase: function (data) {
            return $http.put('/consultation/patientcasedelete', data, apiConfig.create());
        },
        recoverPatientCase: function (requestid) {
            return $http.get('/consultation/patientcaserecover/' + requestid, apiConfig.create());
        },
        getUserSetting: function (query) {
            var config = { params: query };
            return $http.get('/consultation/usersetting', apiConfig.create(config));
        },
        getUserSettingAsync: function (query) {
            var config = { params: query };
            return $http.get('/consultation/usersettingasync', apiConfig.create(config));
        },
        saveUserSetting: function (newData) {
            var data = JSON.stringify(newData);
            return $http.post('/consultation/usersetting', data, apiConfig.create());
        },
        updateRequestStauts: function (requestid, status) {
            return $http.get('/consultation/request/' + requestid + '/' + status, apiConfig.create());
        },
        getDicomRelations: function (params, userId) {
            //  var data = JSON.stringify(params);
            return $http.post('/patientcase/dicomrelations/' + userId, params, apiConfig.create());
        },
        getCaseInfo: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/patientcase/caseInfo/' + id, apiConfig.create(config));
        },
        getConsultationResult: function (orderId) {
            return $http.get('/patientcase/consultationResult/' + orderId, apiConfig.create());
        }
    };
}]);