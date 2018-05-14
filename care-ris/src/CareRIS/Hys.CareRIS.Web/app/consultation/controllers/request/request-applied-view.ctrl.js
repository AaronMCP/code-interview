consultationModule.controller('RequestAppliedController', ['$log', '$scope', 'consultationService', '$stateParams', '$state', 'enums', '$modal', 'loginUser', 'requestService',
    function ($log, $scope, consultationService, $stateParams, $state, enums, $modal, loginUser, requestService) {
        'use strict';
        $log.debug('RequestAppliedController.ctor()...');

        $scope.acceptRequest = function () {
            requestService.acceptRequest({ requestId: $stateParams.requestId });
        };

        (function() {
            $scope.enums = enums;
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data);

                    $scope.editRequestCommentPermission = false;
                    $scope.editPatientInfoPermission = false;
                    $scope.editReceivePermission = false;
                    $scope.recoverRequestPermisson = false;
                    $scope.deleteRequestPermisson = false;

                    if ($scope.report.isDeleted === 0) {
                        if ($scope.user.isJuniorDoctor || $scope.user.isConsultationCenter) {
                            $scope.editPatientInfoPermission = true;
                            $scope.editRequestCommentPermission = true;
                            $scope.editReceivePermission = true;
                        }

                        if ($scope.user.isConsultationCenter) {
                            $scope.deleteRequestPermisson = true;
                        }
                    } else if ($scope.report.isDeleted === 1 && $scope.user.isConsultationCenter) {
                        $scope.deleteRequestPermisson = false;
                        $scope.recoverRequestPermisson = true;
                    }

                    var reasonData = {
                        requestID: $scope.report.requestId,
                        changeReasonType: $scope.report.changeReasonType,
                        otherReason: $scope.report.otherReason,
                        reasonType: enums.consultationDictionaryType.rejectReason,
                        changeStatus: enums.consultationRequestStatus.Rejected
                    };

                    $scope.rejectRequest = function () {
                        requestService.rejectRequest(reasonData);
                    };

                    $scope.cancelApplication = function () {
                        reasonData.reasonType = enums.consultationDictionaryType.cancaleReason;
                        reasonData.changeStatus = enums.consultationRequestStatus.Cancelled;
                        requestService.cancelApplication(reasonData);
                    };

                    $scope.terminatRequest = function () {
                        reasonData.reasonType = enums.consultationDictionaryType.terminateReason;
                        reasonData.changeStatus = enums.consultationRequestStatus.Terminate;
                        requestService.terminatRequest(reasonData);
                    };
                });

                consultationService.getConsultationAssigns($stateParams.requestId).success(function (data) {
                    $scope.expertNames = requestService.getConsultationAssignsString(data);
                });
            } else {
                $log.debug('Request Id is null.');
            };
        }());
    }
]);