qualitycontrolModule.directive('qcGrade', ['$log', '$timeout',
    function ($log, $timeout) {
        'use strict';

        return {
            restrict: 'E',
            controller: 'QcGradeController',
            templateUrl: '/app/quality-control/qc-grade/qc-grade.html',
            replace: true,
            scope: {},
            link: function (scope, element) {
                element.find("#examineTimeStart")
                    .on("keydown", function (e) {
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchOption.examineTimeStart = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.examineTimeStart.options.opened)                            scope.examineTimeStart.open();
                    });
                element.find("#examineTimeEnd")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchOption.examineTimeEnd = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.examineTimeEnd.options.opened)                            scope.examineTimeEnd.open();
                    });
            }
        }
    }
]);