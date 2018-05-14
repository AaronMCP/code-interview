configurationModule.directive('timeManagement', ['$log', '$compile','$timeout',
    function ($log, $compile, $timeout) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/configuration/management/time-management/time-management.html',
            controller: 'TimeManagementCtrl',
            scope: {},
            link: function (scope, element) {
                element.find("#availableDatePicker")
                    .on("keydown", function (e) {
                        var e = e || window.event;
                        if (e.keyCode === 8) {
                            $timeout(function () {
                                copyOption.availableDate = '';
                            });
                        }
                        return false;
                    })
                    .on("click", function () {
                    if (!scope.availableDatePicker.options.opened)                        scope.availableDatePicker.open();
                });
            }
        }
    }])