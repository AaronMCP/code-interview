consultationModule.controller('RequestReconsiderController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', '$modal', 'enums', 'requestService',
    function ($log, $scope, consultationService, $stateParams, $state, $modal, enums, requestService) {
        'use strict';
        $log.debug('RequestReconsiderController.ctor()...');

        $scope.accetpRequest = function () {
            requestService.acceptRequest({ requestId: $stateParams.requestId });
        };

        (function () {
            $scope.enums = enums;
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data, $scope.enums.consultationRequestStatus.Reconsider);
                    $scope.report.reasonType = enums.consultationDictionaryType.applyReconsiderReason;
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
                        if ($scope.user.isJuniorDoctor) {
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

                    $scope.rejectRequest = function () {
                        requestService.rejectRequest({
                            requestID: $scope.report.requestId,
                            changeReasonType: $scope.report.changeReasonType,
                            otherReason: $scope.report.otherReason,
                            reasonType: enums.consultationDictionaryType.rejectReason,
                            changeStatus: enums.consultationRequestStatus.Completed
                        });
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