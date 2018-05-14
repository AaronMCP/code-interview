qualitycontrolModule.directive('imageGrade', ['$log',
    function ($log) {
        'use strict';

        return {
            restrict: 'E',
            controller: 'ImageGradeController',
            templateUrl: '/app/quality-control/qc-grade/image-grade/image-grade.html',
            replace: true,
            scope: {},
            link: function ($scope) {

            }
        }
    }
]);