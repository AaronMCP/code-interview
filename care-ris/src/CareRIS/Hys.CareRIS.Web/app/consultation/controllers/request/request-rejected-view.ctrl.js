consultationModule.controller('RequestRejectedController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', '$modal', 'enums', 'requestService',
    function ($log, $scope, consultationService, $stateParams, $state, $modal, enums, requestService) {
        'use strict';
        $log.debug('RequestRejectedController.ctor()...');

        $scope.applyRequest = function () {
            requestService.applyRequest({ patientCaseID: $scope.report.patientCaseID });
        };

        (function () {
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data, enums.consultationRequestStatus.Rejected);
                    $scope.report.reasonType = enums.consultationDictionaryType.rejectReason;
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

                    if ($scope.report.isDeleted === 0) {
                        if ($scope.user.isJuniorDoctor || $scope.user.isConsultationCenter) {
                            $scope.editPatientInfoPermission = true;
                            $scope.editRequestCommentPermission = true;
                        }

                        if ($scope.user.isConsultationCenter) {
                            $scope.deleteRequestPermisson = true;
                        }
                    } else if ($scope.report.isDeleted === 1 && $scope.user.isConsultationCenter) {
                        $scope.deleteRequestPermisson = false;
                        $scope.recoverRequestPermisson = true;
                    }
                });

                consultationService.getConsultationAssigns($stateParams.requestId).success(function (data) {
                    $scope.expertNames = requestService.getConsultationAssignsString(data);
                });
            } else {
                $log.debug('Request Id is null.');
            }
        }());
    }
]);