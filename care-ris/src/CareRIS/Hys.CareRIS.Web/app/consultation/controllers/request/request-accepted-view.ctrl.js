consultationModule.controller('RequestAcceptedController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', 'enums', 'loginUser', '$modal', 'requestService', '$filter', 'csdToaster','$compile',
    function ($log, $scope, consultationService, $stateParams, $state, enums, loginUser, $modal, requestService, $filter, csdToaster, $compile) {
        'use strict';
        $log.debug('RequestAcceptedController.ctor()...');

        $scope.writeAdvice = function () {
            if (!$scope.report.requestID) {
                $scope.report.requestID = $stateParams.requestId;
            }

            if ($scope.isHost) {
                //var modalInstance1 = $modal.open({
                //    templateUrl:'/app/consultation/views/request/window/report-advice-edit-window.html',
                //    controller: 'ReportAdviceEditController',
                //    windowClass: 'overflow-hidden advice-edit-window2',
                //    backdrop: 'static',
                //    keyboard: false,
                //    size: 'lg',
                //    resolve: {
                //        advice: function () {
                //            return $scope.report;
                //        }
                //    }
                //});

                //modalInstance1.result.then(function (advice) {
                //});
                $scope.reportHostWin.open();
            } else if ($scope.isExpert) {
                //var modalInstance2 = $modal.open({
                //    templateUrl:'/app/consultation/views/request/window/report-expert-advice-edit-window.html',
                //    controller: 'ReportExpertAdviceEditController',
                //    windowClass: 'overflow-hidden advice-edit-window2',
                //    backdrop: 'static',
                //    keyboard: false,
                //    size: 'lg',
                //    resolve: {
                //        advice: function () {
                //            return $scope.report;
                //        }
                //    }
                //});

                //modalInstance2.result.then(function (advice) {
                //});
                $scope.reportExpertWin.open();
            }
        };

        $scope.completeRequest = function () {
            if ($scope.report && $scope.report.consultationReportID) {
                consultationService.completeRequest($stateParams.requestId).success(function () {
                    $state.go('ris.consultation.requests', { searchCriteria: $stateParams.searchCriteria });
                });
            } else {
                csdToaster.pop('info', $filter('translate')('WriterReportAlert'), '');
            }
        };

        $scope.startMeeting = function () {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/views/request/window/request-meeting-window.html',
                controller: 'RequestMeetingController',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    requestId: function () {
                        return $stateParams.requestId;
                    }
                }
            });
        };

        $scope.ApplyCancelRequest = function () {
            if (!$scope.report.requestID) {
                $scope.report.requestID = $stateParams.requestId;
            }

            consultationService.getChangeReason($scope.report.requestID).success(function (data) {
                requestService.ApplyCancelRequest({
                    requestID: data.requestID,
                    changeReasonType: data.changeReasonType,
                    otherReason: data.otherReason,
                    reasonType: enums.consultationDictionaryType.applyCancelReason,
                    changeStatus: enums.consultationRequestStatus.ApplyCancel
                });
            });
        };

        (function () {
            $scope.enums = enums;
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) { 
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data);

                    if ($scope.report) {
                        $scope.rejectRequest = function () {
                            requestService.rejectRequest({
                                requestID: $scope.report.requestId,
                                changeReasonType: $scope.report.changeReasonType,
                                otherReason: $scope.report.otherReason,
                                reasonType: enums.consultationDictionaryType.rejectReason,
                                changeStatus: enums.consultationRequestStatus.Rejected
                            });
                        }
                    }

                    // permission
                    $scope.editReportAdvicePermission = false;
                    $scope.editRequestCommentPermission = false;
                    $scope.editReceivePermission = false;
                    $scope.isHost = false;
                    $scope.isExpert = false;
                    $scope.wirteAdvicePermisson = false;
                    $scope.completePermisson = false;
                    $scope.editPatientInfoPermission = false;
                    $scope.recoverRequestPermisson = false;
                    $scope.deleteRequestPermisson = false;

                    if ($scope.report.isDeleted === 0) {
                        consultationService.isHost($scope.report.requestId).success(function (data) {
                            $scope.isHost = data;
                            if ($scope.isHost && $scope.user.isExpert) {
                                $scope.completePermisson = true;
                            }

                            consultationService.isExpert($scope.report.requestId).success(function (data) {
                                $scope.isExpert = data;

                                $scope.wirteAdvicePermisson = ($scope.isHost || $scope.isExpert) && ($scope.user.isConsultationCenter || $scope.user.isExpert);
                            });
                        });

                        if ($scope.user.isJuniorDoctor || $scope.user.isConsultationCenter) {
                            $scope.editPatientInfoPermission = true;
                            $scope.editRequestCommentPermission = true;
                        }

                        if ($scope.user.isConsultationCenter) {
                            $scope.editReceivePermission = true;
                            $scope.editReportAdvicePermission = true;
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