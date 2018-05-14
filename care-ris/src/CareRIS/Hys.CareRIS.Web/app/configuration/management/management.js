configurationModule.directive('management', ['$log',
    function ($log) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/configuration/management/management.html',
            controller: 'ManagementCtrl'
        }
    }])