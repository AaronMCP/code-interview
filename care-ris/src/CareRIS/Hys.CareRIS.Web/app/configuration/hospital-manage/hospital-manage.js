configurationModule.directive('hospitalManage', ['$timeout',
    function ($timeout) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/hospital-manage/hospital-manage.html',
            replace: true,
            controller: 'HospitalManageCtrl',
            link: function (scope, element) {
            }
        }
    }
]);