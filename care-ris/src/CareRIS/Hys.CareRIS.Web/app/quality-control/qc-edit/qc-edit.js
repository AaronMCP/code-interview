qualitycontrolModule.directive('qcEdit', ['$log', 'qualityService', '$timeout',
    function ($log, qualityService, $timeout) {
        'use strict';

        return {
            restrict: 'E',
            controller: 'QcEditController',
            templateUrl: '/app/quality-control/qc-edit/qc-edit.html',
            replace: true,
            scope: {},
            link: function (scope, element) {
                element.find("#createTimeStart")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchOption.createTimeStart = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.createTimeStart.options.opened)                            scope.createTimeStart.open();
                    });
                element.find("#createTimeEnd")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchOption.createTimeEnd = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.createTimeEnd.options.opened)                            scope.createTimeEnd.open();
                    });
            }
        }
    }
]);