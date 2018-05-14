qualitycontrolModule.directive('qcCheck', ['$log', '$timeout',
    function ($log, $timeout) {
        'use strict';

        return {
            restrict: 'E',
            controller: 'QcCheckInfoController',
            templateUrl: '/app/quality-control/qc-checkInfo/qc-checkInfo.html',
            replace: true,
            scope: {},
            link: function (scope, element) {
                element.find("#meregeCreateTimeStart")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchOption.createTimeStart = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function (e) {
                    if (!scope.meregeCreateTimeStart.options.opened)                        scope.meregeCreateTimeStart.open();
                    });
                element.find("#meregeCreateTimeEnd")
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
                        if (!scope.meregeCreateTimeEnd.options.opened)                            scope.meregeCreateTimeEnd.open();
                    });
            }
        }
    }
]);