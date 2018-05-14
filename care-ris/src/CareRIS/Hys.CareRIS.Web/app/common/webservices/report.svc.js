// worklist web service proxy
webservices.factory('reportService', ['$http', 'apiConfig', function ($http, apiConfig) {
    'use strict';
    return {
        getReport: function (reportId) {
            var config = { isBusyRequest: true };
            return $http.get('/report/reports/' + reportId, apiConfig.create(config));
        },
        getReportByProcedureID: function (procedureId) {
            var config = { isBusyRequest: true };
            return $http.get('/report/reportbyprocedureid/' + procedureId, apiConfig.create(config));
        },
        createReport: function (newData) {
            var config = { isBusyRequest: true };
            var data =  JSON.stringify(newData);
            return $http.post('/report/reports', data, apiConfig.create(config));
        },
        updateReport: function (newData) {
            var config = { isBusyRequest: true };
            var data =  JSON.stringify(newData);
            return $http.put('/report/reports/' + newData.uniqueID, data, apiConfig.create(config));
        },
        getBaseInfo: function (procedureId) {
            var config = { isBusyRequest: true };
            return $http.get('/report/baseinfo/' + procedureId, apiConfig.create(config));
        },
        getBaseInfoDesc: function (procedureId) {
            var config = { isBusyRequest: true };
            return $http.get('/report/baseinfodesc/' + procedureId, apiConfig.create(config));
        },
        getBaseInfoDescByOrderID: function (orderId) {
            var config = { isBusyRequest: true };
            return $http.get('/report/baseinfodescbyorderid/' + orderId, apiConfig.create(config));
        },
        getBaseInfoByOrderID: function (orderId) {
            var config = { isBusyRequest: true };
            return $http.get('/report/baseinfobyorderid/' + orderId, apiConfig.create(config));
        },
        getProceduresByOrderID: function (orderId) {
            return $http.get('/report/getproceduresbyorderid/' + orderId, apiConfig.create());
        },
        getProceduresByReportID: function (reportId) {
            return $http.get('/report/getproceduresbyreportid/' + reportId, apiConfig.create());
        },
        getLock: function (reportId) {
            return $http.get('/report/getlock/' + reportId, apiConfig.create());
        },
        getLockByOrderID: function (orderId) {
            return $http.get('/report/getlockbyorderid/' + orderId, apiConfig.create());
        },
        getLockByOrderIDByAnyLockType: function (orderId) {
            return $http.get('/report/getlockbyorderidbyanylocktype/' + orderId, apiConfig.create());
        },
        addLock: function (reportId) {
            return $http.get('/report/addlockbyreportid/' + reportId, apiConfig.create());
        },
        deleteLock: function (reportId) {
            return $http.delete('/report/deletelockbyreportid/' + reportId, apiConfig.create());
        },
        addLockByOrderID: function (orderId) {
            return $http.get('/report/addlockbyorderid/' + orderId, apiConfig.create());
        },
        deleteLockByOrderID: function (orderId) {
            return $http.delete('/report/deletelockbyorderid/' + orderId, apiConfig.create());
        },

        addLockByProcedureID: function (procedureId) {
            return $http.get('/report/addlockbyprocedureid/' + procedureId, apiConfig.create());
        },
        addLockByProcedureIDs: function (ids) {
            //var config = { isBusyRequest: true };
            var data = JSON.stringify(ids);
            return $http.post('/report/addlockbyprocedureids', data, apiConfig.create());
        },
        deleteLockByProcedureID: function (procedureId) {
            return $http.delete('/report/deletelockbyprocedureid/' + procedureId, apiConfig.create());
        },
        deleteLockByProcedureIDs: function (ids) {
            //var config = { isBusyRequest: true };
            var data = JSON.stringify(ids);
            return $http.post('/report/deletelockbyprocedureids', data, apiConfig.create());
        },
        getAllUserByParentID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/userforreportbyparentid/' + id, apiConfig.create(config));
        },
        getTemplateByParentID: function (id) {
            return $http.get('/report/gettemplatebyparentid/' + id, apiConfig.create());
        },
        getTemplateID: function (id) {
            return $http.get('/report/getreporttemplate/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        getProcedureByID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getprocedurebyid/' + id, apiConfig.create(config));
        },
        getExamedProcedureByOrderID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getexamedproceduresbyorderid/' + id, apiConfig.create(config));
        },
        getReportViewerByProcedureID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getreportviewerbyprocedureid/' + id, apiConfig.create(config));
        },
        getReportPrintUrlByProcedureID: function (id) {
            var config = {isBusyRequest: true };
            return $http.get('/report/getreportprinturlbyprocedureid/' + id, apiConfig.create(config));
        },
        updateReportPrintStatusByProcedureID: function (id, printer) {
            return $http.get('/report/updatereportprintstatusbyprocedureid?id=' + id + '&printer=' + printer, apiConfig.create());
        },
        getServerTime: function () {
            return $http.get('/report/getservertime' , apiConfig.create());
        },
        updateComments: function (newData) {
            var data =  JSON.stringify(newData);
            return $http.post('/report/updatecomments', data, apiConfig.create());
        },
        getReportListByID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getreportlistbyid/' + id, apiConfig.create(config));
        },
        getOtherReportListByID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getotherreportlistbyid/' + id, apiConfig.create(config));
        },
        getOtherReportListByProcedureID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getotherreportlistbyprocedureid/' + id, apiConfig.create(config));
        },
        getPacsUrl: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getpacsurl/' + id, apiConfig.create(config));
        },
        getPacsUrlDX: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/getpacsurldx/' + id, apiConfig.create(config));
        },
        createReportTemplate: function (newData) {
            var config = { isBusyRequest: true };
            var data =  JSON.stringify(newData);
            return $http.post('/report/reporttemplate', data, apiConfig.create(config));
        },
        UpdateReportTemplate: function (newData) {
            var config = { isBusyRequest: true };
            var data =  JSON.stringify(newData);
            return $http.put('/report/reporttemplate', data, apiConfig.create(config));
        },
        deleteTemplateByID: function (id) {
            return $http.delete('/report/deletetemplatebyid/' + id, apiConfig.create());
        },
        getPrintTemplateByCriteria: function (criteria) {
            var config = { params: { json: JSON.stringify(criteria) }, isBusyRequest: true };
            return $http.get('/report/printtemplatebycriteria', apiConfig.create(config));
        },
        updateReportPrintTemplate: function (reportID, printTemplateID) {
            var config = { params: { reportID: reportID, printTemplateID: printTemplateID }, isBusyRequest: true };
            return $http.get('/report/updatereportprinttemplate', apiConfig.create(config));
        },
        isDuplicatedTemplateName: function (newData) {
            var data =  JSON.stringify(newData);
            return $http.put('/report/isduplicatedreporttemplate', data, apiConfig.create());
        },
        getOrderByProcedureID: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/report/order/' + id, apiConfig.create(config));
        },
        getReportPrintTemplateIDByProID: function (id, site, domain) {
            var config = { params: { id: id, site: site, domain: domain }, isBusyRequest: true };
            return $http.get('/report/reportprinttemplateid', apiConfig.create(config));
        },
        getOtherReportPrintID: function (accno, modalityType, templateType, site) {
            var config = { params: { accno: accno, modalityType: modalityType, templateType: templateType, site: site }, isBusyRequest: true };
            return $http.get('/report/other/printid', apiConfig.create(config));
        },
        GetImageStatus:function(id) {
            return $http.get('/report/imageStatus/' + id, apiConfig.create());
        }
    };
}]);