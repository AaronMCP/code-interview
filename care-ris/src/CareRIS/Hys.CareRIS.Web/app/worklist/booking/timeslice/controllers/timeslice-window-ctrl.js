worklistModule.controller('TimesliceWindowController', ['$scope', '$log', 'registrationService', '$timeout', '$translate','loginContext',
    function ($scope, $log, registrationService, $timeout, $translate, loginContext) {
        'use strict';
        var closeTimesliceWin = function () {
            $scope.timesliceSelected = false;
            $scope.timesliceWin.close();
        };
        var errorTestReg = /^Error:(\w+)$/g;
        var showErrorMessage = function (msg) {
            $scope.errorHappend = true;
            $scope.errorMsg = msg;
            $timeout(function () { $scope.errorHappend = false; }, 6000);
        };

        var unlockTimeSlice = function (callback) {
            (!angular.isFunction(callback)) && (callback = function() {});
            var params = {
                unlockGuid: $scope.unlockId,
                modality: $scope.timesliceOption.modality,
                start: $scope.timesliceOption.start,
                end: $scope.timesliceOption.end,
                site: loginContext.site
            };

            if (!params.unlockGuid && !params.start && !params.end) {
                callback();
                return;
            }

            registrationService.unLockTimeSlice(params).finally(function () {
                callback();
            });
        };

        var selectTimeSlice = function () {
            var val = $scope.timesliceOption.getValue();

            unlockTimeSlice(function() {
                registrationService.lockTimeSlice(val).success(function(lockId) {
                    var errorType = null;
                    String(lockId).replace(errorTestReg, function($0, $1) {
                        errorType = $1;
                    });
                    if (errorType != null) {
                        $scope.timesliceSelected = false;
                        showErrorMessage($translate.instant(errorType));
                        $scope.timesliceOption.refresh();
                    } else {
                        $scope.timesliceSelected = true;
                        $scope.lockId = lockId;
                        $scope.timesliceValue = val;
                        $scope.timesliceWin.close();
                    }
                });
            });

        };
        var errorMsgClosed = function () {
            $scope.errorHappend = false;
        };

        ; (function initialize() {
            $scope.tWindow = angular.extend({
                open: function () {
                    $scope.timesliceWin.center().open();
                },
                close: function () {
                    $scope.timesliceWin.close();
                }
            }, $scope.tWindow);
            $scope.timesliceSelected = false;
            $scope.errorHappend = false;
            $scope.$on('event:timesliceSelected', function (e) {
                $scope.timesliceSelected = true;
                e.stopPropagation();
            });
            $scope.$on('event:noMatchedModality', function (e) {
                showErrorMessage($translate.instant('NoMatchedModality'));
                e.stopPropagation();
            });
            $scope.lockId = '';
            $scope.timesliceValue = null;
            $scope.timesliceOption = angular.extend({}, $scope.timesliceOption);
            $scope.closeTimesliceWin = closeTimesliceWin;
            $scope.selectTimeSlice = selectTimeSlice;
            $scope.errorMsgClosed = errorMsgClosed;
        })();
    }
]);