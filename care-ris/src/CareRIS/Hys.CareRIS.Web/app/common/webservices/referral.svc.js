// registration web service proxy
webservices.factory('referralService', ['$http', 'apiConfig', function ($http, apiConfig) {
    'use strict';

    var prefix = '/referral/';

    return {
        getRefferalList: function (searchCriteria) {
            var config = { params: { query: JSON.stringify(searchCriteria) } };
            return $http.get(prefix + 'referrallist', apiConfig.create(config));
        },
        reSend: function (id) {
            var config = { isBusyRequest: true };
            return $http.get(prefix + 'resend/' + id, apiConfig.create(config));
        },
        getProcedureID: function (accno, procedurecode) {
            var config = { params: { accno: accno, procedurecode: procedurecode } };
            return $http.get(prefix + 'procedureid', apiConfig.create(config));
        },
        getTargetSites: function () {
            var config = { isBusyRequest: true };
            return $http.get(prefix + 'targetsites', apiConfig.create(config));
        },
        sendReferral: function (newData) {
            var config = { isBusyRequest: true };
            var data = JSON.stringify(newData);
            return $http.post(prefix + 'sendreferral', data, apiConfig.create(config));
        },
        getCanReferral: function () {
            var config = { isBusyRequest: true };
            return $http.get(prefix + 'canreferral', apiConfig.create(config));
        },
        cancelReferral: function (id) {
            var config = { isBusyRequest: true };
            return $http.get(prefix + 'cancelreferral/' + id, apiConfig.create(config));
        }
    };
}]);