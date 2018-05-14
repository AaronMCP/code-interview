configurationModule.directive('administratorTools', ['$timeout',
    function ($timeout) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/administrator-tools/administrator-tools.html',
            replace: true,
            controller: 'AdministratorToolsCtrl',
            link: function (scope, element) {
                element.find("#startDatePicker")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchCriteria.createStartTime = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.startDatePicker.options.opened)                            scope.startDatePicker.open();
                    });
                element.find("#endDatePicker")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                scope.searchCriteria.createEndTime = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                        if (!scope.endDatePicker.options.opened)                            scope.endDatePicker.open();
                    });
            }
        }
    }
]);