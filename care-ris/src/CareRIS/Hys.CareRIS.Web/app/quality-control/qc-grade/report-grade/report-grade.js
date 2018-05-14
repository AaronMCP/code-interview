qualitycontrolModule.directive('reportGrade', ['$log',
    function ($log) {
        'use strict';

        return {
            restrict: 'E',
            controller: 'ReportGradeController',
            templateUrl: '/app/quality-control/qc-grade/report-grade/report-grade.html',
            replace: true,
            scope: {},
            link: function ($scope) {

            }
        }
    }
]);