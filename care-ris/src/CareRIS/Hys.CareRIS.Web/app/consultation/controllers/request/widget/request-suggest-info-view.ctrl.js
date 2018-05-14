consultationModule.controller('RequestSuggestInfoController', ['$log', '$scope', 'consultationService', '$modal', '$sce', 'requestService',
    function ($log, $scope, consultationService, $modal, $sce, requestService) {
        'use strict';
        $log.debug('RequestSuggestInfoController.ctor()...');

        $scope.hideHistory = function (status) {
            $('#request-applied-history').toggle();
            if (status === 0) {
                $scope.historyButtonText = 'ExpandHistory';
                $scope.historyButtonstatus = 1;
            } else {
                $scope.historyButtonText = 'HideHistory';
                $scope.historyButtonstatus = 0;
            }
        };

        $scope.reload = function (advice) {
            if (advice) {
                $scope.report.consultationReportID = advice.consultationReportID;
                $scope.report.consultationAdvice = advice.consultationAdvice ? $sce.trustAsHtml(advice.consultationAdvice) : '';
                $scope.report.consultationRemark = advice.consultationRemark ? $sce.trustAsHtml(advice.consultationRemark) : '';
                $scope.report.writer = advice.writer;

                consultationService.getReportHistoriesByReportId($scope.report.consultationReportID).success(function (result) {
                    if (result) {
                        angular.forEach(result, function (item) {
                            item.consultationAdvice = item.consultationAdvice ? $sce.trustAsHtml(item.consultationAdvice) : '';
                        });
                    }
                    $scope.report.reportHistories = result;
                });

                $scope.report.currentAgeT = requestService.getCurrentAge($scope.report.currentAge);

                consultationService.getConsultationAssigns(advice.requestID).success(function (data) {
                    $scope.expertNames = requestService.getConsultationAssignsString(data);
                });
            }
        };

        function openEditWindow(advice) {
            if ($scope.report) {
                consultationService.isHost($scope.report.requestId).success(function (data) {
                    $scope.isHost = data;
                    consultationService.isExpert($scope.report.requestId).success(function (data) {
                        $scope.isExpert = data;
                        if ($scope.isHost) {
                            //var modalInstance = $modal.open({
                            //    templateUrl:'/app/consultation/views/request/window/report-advice-edit-window.html',
                            //    controller: 'ReportAdviceEditController',
                            //    windowClass: 'overflow-hidden advice-edit-window2',
                            //    backdrop: 'static',
                            //    keyboard: false,
                            //    size: 'lg',
                            //    resolve: {
                            //        advice: function () {
                            //            return advice;
                            //        }
                            //    }
                            //});

                            //modalInstance.result.then(function (advice) {
                            //    reload(advice);
                            //});
                            $scope.advice = advice;
                            $scope.reportHostWin.open();
                        } else if ($scope.isExpert) {
                            //var modalInstance1 = $modal.open({
                            //    templateUrl:'/app/consultation/views/request/window/report-expert-advice-edit-window.html',
                            //    controller: 'ReportExpertAdviceEditController',
                            //    windowClass: 'overflow-hidden advice-edit-window2',
                            //    backdrop: 'static',
                            //    keyboard: false,
                            //    size: 'lg',
                            //    resolve: {
                            //        advice: function () {
                            //            return advice;
                            //        }
                            //    }
                            //});

                            //modalInstance1.result.then(function (advice) {
                            //    $scope.reload(advice);
                            //});

                            $scope.advice = advice;
                            $scope.reportExpertWin.open();
                        }

                    });
                });
            }
        };

        $scope.modifyConsiltationAdvice = function () {
            var advice = {
                consultationReportID: $scope.report.consultationReportID,
                consultationAdvice: $scope.report.consultationAdvice ? $scope.report.consultationAdvice.$$unwrapTrustedValue() : '',
                consultationRemark: $scope.report.consultationRemark ? $scope.report.consultationRemark.$$unwrapTrustedValue() : '',
                writer: $scope.report.writer,
                requestID: $scope.report.requestId
            };
            openEditWindow(advice);
        };

        (function () {
            $scope.historyButtonstatus = 0;
            $scope.historyButtonText = 'HideHistory';
            $scope.advice = null;
        }());
    }
]);