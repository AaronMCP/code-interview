qualitycontrolModule.directive('checkPart', ['$log',
    function ($log) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/quality-control/qc-edit/qc-edit-checkPart/qc-edit-checkPart.html',
            replace: true,
            scope: {},
            controller: 'QcEditCheckPartController',
            link: function (scope) {

            }
        }
    }
]);