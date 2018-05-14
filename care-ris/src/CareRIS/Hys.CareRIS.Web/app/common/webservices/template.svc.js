webservices.factory('templateService', ['$http', 'apiConfig', function ($http, apiConfig) {
    'use strict';
    return {
        getTemplateByParentID: function (id) {
            return $http.get('/report/gettemplatebyparentid/' + id, apiConfig.create());
        },
        getTemplateByID: function (id) {
            return $http.get('/report/getreporttemplate/' + id, apiConfig.create({ isBusyRequest: true }));
        },
        newTemplate: function (template) {
            return $http.post('/report/reporttemplate', template, apiConfig.create({ isBusyRequest: true }))
        },
        updateTemplate: function (template) {
            return $http.put('/report/reporttemplate', template, apiConfig.create({ isBusyRequest: true }))
        },
        newPublicTemplate: function (template) {
            return $http.post('/report/publicreporttemplate', template, apiConfig.create({ isBusyRequest: true }))
        },
        updatePublicTemplate: function (template) {
            return $http.put('/report/publicreporttemplate', template, apiConfig.create({ isBusyRequest: true }))
        },
        deleteTemplate: function (id) {
            return $http.delete('/report/deletetemplatebyid/' + id, apiConfig.create({ isBusyRequest: true }))
        },
        newTemplateDirec: function (templateDirec) {
            return $http.post('/report/reporttemplatedirec', templateDirec, apiConfig.create({ isBusyRequest: true }))
        },
        updateTemplateDirec: function (reportTemplateDto) {
            return $http.put('/report/reporttemplatedirec', reportTemplateDto, apiConfig.create({ isBusyRequest: true }))
        },
        isTemplateExist: function (template) {
            return $http.put('/report/reporttemplateexist', template, apiConfig.create())
        },
        nodeUp: function (id) {
            return $http.get('/report/nodeup/' + id, apiConfig.create({ isBusyRequest: true }))
        },
        nodeDown: function (id) {
            return $http.get('/report/nodedown/' + id, apiConfig.create({ isBusyRequest: true }))
        }
    };
}]);