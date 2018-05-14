worklistModule.directive('timesliceLauncher', ['$log', '$compile',
function ($log, $compile) {
    'use strict';
    return {
        restrict: 'A',
        scope: {
            tOptions: '=?',
            onTimesliceSelected: '&?',
            onTimesliceCanceled: '&?',
            tOperater: '=?'
        },
        compile: function () {
            return {
                pre: function (scope, element) {
                    scope.timesliceWin = {};
                    scope.timesliceSelected = function (lockId, timeslice) {
                        var timesliceWraper = {
                            bookingTimeAlias: timeslice.description,
                            modalityType: timeslice.modalityType,
                            modality: timeslice.modality,
                            bookingBeginTime: timeslice.startDt,
                            bookingEndTime: timeslice.endDt
                        };
                        scope.onTimesliceSelected({ lockId: lockId, timeslice: timesliceWraper });
                        if (angular.isFunction(scope.selectedFunc)) {
                            scope.selectedFunc(lockId, timesliceWraper);
                        }
                    };
                    scope.timesliceCanceled = function () {
                        scope.onTimesliceCanceled();
                        if (angular.isFunction(scope.canceledFunc)) {
                            scope.canceledFunc();
                        }
                    };
                    scope.timesliceOption = {};
                    scope.unlockId = scope.tOperater.unlockId || '';
                    var timesliceWindowHtml = '<timeslice-window-view' +
                                ' t-window="timesliceWin"' +
                                ' timeslice-option="timesliceOption"' +
                                ' on-timeslice-selected="timesliceSelected(lockId,timeslice)"' +
                                ' unlock-id="unlockId"' +
                                ' on-timeslice-canceled="timesliceCanceled()"' +
                                ' ></timeslice-window-view>';
                    var winEle = $(timesliceWindowHtml).appendTo('body');
                    $compile(winEle)(scope);
                },
                post: function (scope, element) {
                    if (!scope.tOperater) {
                        scope.tOperater = {};
                    };
                    scope.optionGenerator = scope.tOptions || {};
                    scope.selectedFunc = null;
                    scope.canceledFunc = null;

                    scope.tOperater.open = function (selected, canceled) {

                        var optionSource = angular.isFunction(scope.optionGenerator) ? scope.optionGenerator() : scope.optionGenerator;
                        var optionTarget = {
                            modalityTypeEnable: optionSource.modalityTypeEnable,
                            modalityEnable: optionSource.modalityEnable,
                            modalityType: optionSource.modalityType,
                            modality: optionSource.modality,
                            start: optionSource.bookingBeginTime,
                            end: optionSource.bookingEndTime
                        };
                        scope.selectedFunc = selected;
                        scope.canceledFunc = canceled;
                        scope.timesliceOption = optionTarget;
                        scope.unlockId = scope.tOperater.unlockId || '';
                        scope.timesliceWin.open();
                    }

                }
            };
        }
    }
}
]);