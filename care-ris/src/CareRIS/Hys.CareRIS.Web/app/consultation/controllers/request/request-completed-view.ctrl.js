consultationModule.controller('RequestCompletedController', ['$log', '$scope', 'consultationService', '$stateParams', '$state', '$modal', 'enums', 'requestService','$sce',
    function ($log, $scope, consultationService, $stateParams, $state, $modal, enums, requestService, $sce) {
        'use strict';
        $log.debug('RequestCompletedController.ctor()...');

        $scope.returnToRequests = function () {
            $state.go('ris.consultation.requests', { searchCriteria: $stateParams.searchCriteria });
        };

        $scope.printResults = function () {
            $scope.reportPrintWin.open();
        };

        (function () {
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data, enums.consultationRequestStatus.Completed);
                    $scope.report.reasonType = enums.consultationDictionaryType.rejectReason;
                    $scope.report.changeReasonTypeName = '';

                    consultationService.getDictionaryByType($scope.report.reasonType).success(function (data) {
                        var r = _.findWhere(data, { value: $scope.report.changeReasonType });
                        if (r) {
                            $scope.report.changeReasonTypeName = r.name;
                        }
                    });

                    if ($scope.report.reportHistories) {
                        angular.forEach($scope.report.reportHistories, function (item) {
                            item.consultationAdvice = $sce.trustAsHtml(item.consultationAdvice);
                        });
                    }

                    $scope.editRequestCommentPermission = false;
                    $scope.editPatientInfoPermission = false;
                    $scope.editReceivePermission = false;
                    $scope.isHost = false;
                    $scope.isExpert = false;
                    $scope.recoverRequestPermisson = false;
                    $scope.deleteRequestPermisson = false;

                    if ($scope.report.isDeleted === 0) {
                        $scope.requestReconsideration = function () {
                            requestService.requestReconsideration({
                                requestID: $scope.report.requestId,
                                changeReasonType: $scope.report.changeReasonType,
                                otherReason: $scope.report.otherReason,
                                reasonType: enums.consultationDictionaryType.applyReconsiderReason,
                                changeStatus: enums.consultationRequestStatus.Reconsider
                            });
                        };

                        if ($scope.user.isConsultationCenter) {
                            $scope.deleteRequestPermisson = true;
                        }

                    } else if ($scope.report.isDeleted === 1 && $scope.user.isConsultationCenter) {
                        $scope.deleteRequestPermisson = false;
                        $scope.recoverRequestPermisson = true;
                    }

                    $scope.editReportPermission = false;
                    if ($scope.report.isDeleted === 0) {
                        consultationService.editReportPermission($stateParams.requestId).success(function (data) {
                            $scope.editReportPermission = data && ($scope.user.isConsultationCenter || $scope.user.isExpert);
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