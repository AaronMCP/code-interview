consultationModule.controller('RequestTerminateController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', '$modal', 'enums', 'requestService', 
    function ($log, $scope, consultationService, $stateParams, $state, $modal, enums, requestService) {
        'use strict';
        $log.debug('RequestTerminateController.ctor()...');

        (function () {
            $scope.enums = enums;
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data, $scope.enums.consultationRequestStatus.Terminate);
                    $scope.report.reasonType = enums.consultationDictionaryType.terminateReason;
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
                        } else {
                            $scope.recoverRequestPermisson = true;
                        }
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