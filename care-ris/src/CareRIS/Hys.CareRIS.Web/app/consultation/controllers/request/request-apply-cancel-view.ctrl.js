consultationModule.controller('RequestApplyCancelController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', '$modal', 'enums', 'loginUser', 'requestService',
    function ($log, $scope, consultationService, $stateParams, $state, $modal, enums, loginUser, requestService) {
        'use strict';
        $log.debug('RequestApplyCancelController.ctor()...');

        $scope.agree = function () {
            consultationService.updateRequestStauts($scope.report.requestId, $scope.enums.consultationRequestStatus.Cancelled).success(function () {
                $state.go('ris.consultation.requests', {
                    searchCriteria: $stateParams.searchCriteria,
                    timestamp: Date.now(),
                    reload: true
                });
            });
        };

        $scope.reject = function () {
            consultationService.updateRequestStauts($scope.report.requestId, $scope.enums.consultationRequestStatus.Accepted).success(function () {
                $state.go('ris.consultation.requests', {
                    searchCriteria: $stateParams.searchCriteria,
                    timestamp: Date.now(),
                    reload: true
                });
            });
        };

        (function () {
            $scope.enums = enums;
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data, $scope.enums.consultationRequestStatus.ApplyCancel);

                    $scope.report.reasonType = enums.consultationDictionaryType.applyCancelReason;
                    $scope.report.changeReasonTypeName = '';

                    consultationService.getDictionaryByType($scope.report.reasonType).success(function (data) {
                        var r = _.findWhere(data, { value: $scope.report.changeReasonType });
                        if (r) {
                            $scope.report.changeReasonTypeName = r.name;
                        }
                    });

                    $scope.editRequestCommentPermission = false;
                    $scope.editPatientInfoPermission = false;
                    $scope.editReceivePermission = false;
                    $scope.recoverRequestPermisson = false;
                    $scope.deleteRequestPermisson = false;

                    if ($scope.user.isConsultationCenter) {
                        if ($scope.report.isDeleted === 0) {
                            $scope.deleteRequestPermisson = true;
                        } else if ($scope.report.isDeleted === 1) {
                            $scope.recoverRequestPermisson = true;
                        }
                    }
                });

                consultationService.getConsultationAssigns($stateParams.requestId).success(function (data) {
                    $scope.expertNames = requestService.getConsultationAssignsString(data);
                });
            } else {
                $state.go('ris.consultation.requests', { searchCriteria: $stateParams.searchCriteria });
            }
        }());
    }
]);