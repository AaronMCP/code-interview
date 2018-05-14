// worklist web service proxy
webservices.factory('worklistService', ['$http', 'apiConfig', 'constants', function($http, apiConfig, constants) {
    'use strict';
    return {
        searchCriteria: function() {
            return {
                patientName: '',
                patientNo: '',
                accessSites: [],
                accNo: '',
                patientTypes: [],
                statuses: [],
                modalityTypes: [],
                modalities: [],
                createTimeType: 'range',
                createDays: null,
                createStartDate: null,
                createEndDate: null,
                createTimeRanges: [],
                createTimeRangeOptions: [{
                    start: null,
                    stratStr: '',
                    startMin: '0:00',
                    startMax: '23:00',
                    end: null,
                    endStr: '',
                    endMin: '1:00',
                    endMax: '23:59'
                }],
                examineTimeType: 'range',
                examineDays: null,
                examineStartDate: null,
                examineEndDate: null,
                examineTimeRanges: [],
                examineTimeRangeOptions: [{
                    start: null,
                    stratStr: '',
                    startMin: '0:00',
                    startMax: '23:00',
                    end: null,
                    endStr: '',
                    endMin: '1:00',
                    endMax: '23:59'
                }],
                pagination: {
                    pageIndex: 1,
                    pageSize: constants.pageSize
                },
                shortcutName: ''
            };
        },
        advancedSearch: function(criteria) {
            var config = {
                params: {
                    query: JSON.stringify(criteria)
                },
                isBusyRequest: true
            };
            return $http.get('/worklist/advanced/search/result', apiConfig.create(config));
        },
        worksSearch: function (criteria) {
            //Date.prototype.toJSON = function () {
            //    return this.toDateString();
            //}
            var config = {
                params: {
                    query: JSON.stringify(criteria)
                },
                isBusyRequest: true
            };
            return $http.get('/worklist/advanced/search/works', apiConfig.create(config));
        },
        getShortcuts: function() {
            var config = {
                isBusyRequest: true
            };
            return $http.get('/worklist/shortcuts', apiConfig.create(config));
        },
        addShortcut: function(data) {
            var config = {
                isBusyRequest: true
            };
            return $http.post('/worklist/shortcuts', data, apiConfig.create(config));
        },
        deleteShortcut: function(id) {
            var config = {
                isBusyRequest: true
            };
            return $http.delete('/worklist/shortcuts/' + id, apiConfig.create(config));
        },
        setDefaultShortcut: function(id) {
            var config = {
                isBusyRequest: true
            };
            return $http.get('/worklist/shortcuts/default/' + id, apiConfig.create(config));
        }
    };
}]);