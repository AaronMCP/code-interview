// worklist web service proxy
webservices.factory('qualityService', ['$http', 'apiConfig', 'constants', function ($http, apiConfig, constants) {
    'use strict';
    return {
        getQcList: function (data) {
            return $http.post('/qc/scoringlist', data, apiConfig.create({ isBusyRequest: true }));
        },
        saveScoring: function (scoreInfo) {
            return $http.post('/qc/savescoring', scoreInfo, apiConfig.create({ isBusyRequest: true }));
        },
        getQcHistory: function (objId, type) {
            return $http.get('/qc/scoringhistorylist?objectId=' + objId + '&type=' + type, apiConfig.create({ isBusyRequest: true }));
        },
        getPageablePatients: function (data) {
            return $http.post('/qc/pageablepatient', data, apiConfig.create({ isBusyRequest: true }));
        },
        getOrder: function (patientGuid) {
            return $http.get('/qc/order/' + patientGuid, apiConfig.create({ isBusyRequest: true }));
        },
        getProcedure: function (orderId) {
            return $http.get('/qc/procedure/' + orderId, apiConfig.create({ isBusyRequest: true }));
        },
        updateProcedure: function (procedure) {
            return $http.post('/qc/updateprocedure', procedure, apiConfig.create({ isBusyRequest: true }));
        },
        deleteProcedure: function (id) {
            return $http.delete('/registration/procedures/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        updateOrder: function (id, order) {
            return $http.put('/registration/orders/' + id, order, apiConfig.create({ isBusyRequest: true }));
        },
        deleteOrder: function (id) {
            return $http.delete('/registration/orders/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        getLockByPatientId: function (patientId) {
            return $http.get('/report/getlockbypatientid/' + patientId, apiConfig.create());
        },
        updatePatient: function (id, patient) {
            return $http.put('/registration/patients/' + id, patient, apiConfig.create({ isBusyRequest: true }));
        },
        deletePatient: function (id) {
            return $http.delete('/registration/patients/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        mergePatient: function (mergeInfo) {
            return $http.post('/qc/mergepatient', mergeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        moveOrder: function (mergeInfo) {
            return $http.post('/qc/moveorder', mergeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        mergeOrder: function (mergeInfo) {
            return $http.post('/qc/mergeorder', mergeInfo, apiConfig.create({ isBusyRequest: true }));
        },
        moveCheckingItem: function (mergeInfo) {
            return $http.post('/qc/movecheckingitem', mergeInfo, apiConfig.create({ isBusyRequest: true }));
        }, 
        //根据权限获取角色名
        getUserByRolenames: function (rolenames) {
            return $http.post('/configuration/settings/usersbyrolenames', rolenames, apiConfig.create());
        },
        //撤销检查
        revokeProcedure: function (orderId) {
            return $http.put('/qc/revokeprocedure/' + orderId, {}, apiConfig.create({ isBusyRequest: true }));
        },
        //恢复检查
        recoverProcedure: function (orderId) {
            return $http.put('/qc/recoveryprocedure/' + orderId, {}, apiConfig.create({ isBusyRequest: true }));
        }
    };
}]);