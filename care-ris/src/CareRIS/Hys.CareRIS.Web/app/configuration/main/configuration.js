configurationModule.directive('configuration', ['$log',
    function ($log) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/configuration/main/configuration.html',
            controller: 'ConfigurationCtrl'
        }
    }])