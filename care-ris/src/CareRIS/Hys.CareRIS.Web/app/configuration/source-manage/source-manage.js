configurationModule.directive('sourceManage', ['$log', '$document', '$timeout',
    function ($log, $document, $timeout) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/source-manage/source-manage.html',
            replace: true,
            controller: 'SourceManageCtrl',
            link: function (scope, element) {
                element.find("#stopTimeStart")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.modality.startDt = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.stopTimeStart.options.opened)                            scope.stopTimeStart.open();
                    });
                element.find("#stopTimeEnd")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.modality.endDt = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.stopTimeEnd.options.opened)                            scope.stopTimeEnd.open();
                    });
            }
        }
    }
]);