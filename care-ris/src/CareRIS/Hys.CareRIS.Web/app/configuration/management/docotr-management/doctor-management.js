configurationModule.directive('doctorManagement', ['$log',
    function ($log) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/configuration/management/docotr-management/doctor-management.html',
            controller: 'DoctorManagementCtrl'
        }
    }])