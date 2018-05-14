configurationModule.directive('timesliceMaintainer', ['$log',
    function ($log) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/configuration/management/timeslice-maintainer/timeslice-maintainer.html',
            controller: 'TimesliceMaintainerController',
            scope: {
                site: '@',
                modalityType: '@',
                modality: '@',
                sites: '=',
                modalityTypes: '=',
                modalityGetter: '&',
                editTimeslice: '=?',
                onOk: '&',
                onCancel: '&'
            },
            link: function (scope, element) {
                element.find("#tsm-availabledate")
                    .on("keydown", function () { return false; })
                    .on("click", function () {
                        scope.availableDatePicker.open();
                    });
            }
        }
    }])