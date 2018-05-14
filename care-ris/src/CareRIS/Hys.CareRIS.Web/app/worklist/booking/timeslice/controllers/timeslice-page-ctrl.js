worklistModule.controller('TimeslicePageController', ['$scope', '$log', 'registrationService', '$timeout', '$translate', 'loginContext', '$state', '$modal',
    function ($scope, $log, registrationService, $timeout, $translate, loginContext, $state, $modal) {
        'use strict';
        var errorTestReg = /^Error:(\w+)$/g;
        var goRegistration = function () {
            $state.go('ris.worklist.registrations');
        };
        var cancel = function () {
            $scope.timesliceSelected = false;
            goRegistration();
        };
        var unlockTimeSlice = function (callback) {
            (!angular.isFunction(callback)) && (callback = function () { });
            var params = {
                unlockGuid: $scope.timesliceValue.lockGuid,
                modality: $scope.timesliceValue.modality,
                start: $scope.timesliceValue.startDt,
                end: $scope.timesliceValue.endDt,
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
        var showNewRegistration = function (args) {
            var modalInstance = $modal.open({
                controller: 'RegistrationEditController',
                templateUrl: "/app/registration/views/registration-edit-view.html",
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    args: function () {
                        return args;
                    }
                }
            });
            modalInstance.result.then(
                function (result) {
                    goRegistration();
                },
                function () {
                    unlockTimeSlice();
                });
        };
        var selectTimeSlice = function () {
            var val = $scope.timesliceOption.getValue();

            registrationService.lockTimeSlice(val).success(function (lockId) {
                var errorType = null;
                String(lockId).replace(errorTestReg, function ($0, $1) {
                    errorType = $1;
                });
                if (errorType != null) {
                    $scope.timesliceSelected = false;
                    alert($translate.instant(errorType));
                    $scope.timesliceOption.refresh();
                } else {
                    $scope.timesliceSelected = true;
                    $scope.lockId = lockId;
                    $scope.timesliceValue = val;
                    var timesliceWraper = {
                        bookingTimeAlias: val.description,
                        modalityType: val.modalityType,
                        modality: val.modality,
                        bookingBeginTime: val.startDt,
                        bookingEndTime: val.endDt,
                        lockGuid: lockId
                    };
                    $scope.timesliceValue.lockGuid = lockId;
                    showNewRegistration({ isBooking: true, bookingSlice: timesliceWraper });
                }
            });
        };

        ; (function initialize() {

            $scope.timesliceSelected = false;
            $scope.$on('event:timesliceSelected', function (e) {
                $scope.timesliceSelected = true;
                e.stopPropagation();
            });
            $scope.$on('event:noMatchedModality', function (e) {
                alert($translate.instant('NoMatchedModality'));
                e.stopPropagation();
            });
            $scope.lockId = '';
            $scope.timesliceValue = null;
            $scope.timesliceOption = angular.extend({}, $scope.timesliceOption);
            $scope.cancel = cancel;
            $scope.selectTimeSlice = selectTimeSlice;
        })();
    }
]);