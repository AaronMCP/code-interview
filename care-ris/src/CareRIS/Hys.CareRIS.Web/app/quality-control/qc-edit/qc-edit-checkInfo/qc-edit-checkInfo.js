qualitycontrolModule.directive('checkInfo', ['$log',
    function ($log) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/quality-control/qc-edit/qc-edit-checkInfo/qc-edit-checkInfo.html',
            replace: true,
            scope: {},
            controller: 'QcEditCheckInfoController',
            link: function (scope) {

            }
        }
    }
]);