reportModule.controller('ReportPreviewController', [
    '$log', '$scope', 'enums', 'reportService', '$location', '$rootScope', '$injector', '$timeout',
    'risDialog', 'loginContext', 'configurationService', '$translate', '$http', '$window', 'busyRequestNotificationHub', 'application', 'openDialog', 'clientAgentService', '$state',
    function ($log, $scope, enums, reportService, $location, $rootScope, $injector, $timeout,
        risDialog, loginContext, configurationService, $translate, $http, $window, busyRequestNotificationHub, application, openDialog, clientAgentService, $state) {
        'use strict';
        $log.debug('ReportPreviewController.ctor()...');

        var returnWorkList = function () {
            //0:worklist
            //1:report edit
            if ($scope.fromSrc == '0') {
                reportService.getProcedureByID($scope.procedureId).success(function (data) {
                    $location.path('ris/worklist/registrations');
                    $location.url($location.path());
                    $rootScope.refreshSearch({ 'orderId': data.orderID });

                }).error(function (data, status, headers, config) {
                    $location.path('ris/worklist/registrations');
                    $location.url($location.path());
                    $rootScope.refreshSearch();
                });
            }
            else {
                $location.path(decodeURIComponent($scope.fromSrc));
                $location.url($location.path());
            }
        };

        var editReport = function () {
            //procedureItem.procedureID
            reportService.getProcedureByID($scope.procedureId).success(function (data) {
                var item = data;
                item.isLock = false;
                item.isLockUser = '';
                item.isLockIp = '';
                item.procedureID = item.uniqueID;
                reportService.getLockByOrderID(item.orderID).success(function (lockData) {
                    if (lockData != null && lockData != '') {
                        if (lockData.procedureIDs == '') {
                            item.isLockUser = lockData.owner;
                            item.isLockIp = lockData.ownerIP;
                        }
                        else {
                            var startIndex = -1;
                            startIndex = lockData.procedureIDs.indexOf(item.procedureID);
                            if (startIndex > -1) {
                                item.isLock = true;
                                var userStartIndex = lockData.procedureIDs.indexOf('&', startIndex + 1);
                                if (userStartIndex > -1) {
                                    var userEndIndex = lockData.procedureIDs.indexOf('&', userStartIndex + 1);
                                    if (userEndIndex > -1) {
                                        item.isLockUser = lockData.procedureIDs.substr(userStartIndex + 1, userEndIndex - userStartIndex - 1);
                                        var ipEndIndex = lockData.procedureIDs.indexOf('|', userEndIndex + 1);
                                        if (ipEndIndex > -1) {
                                            item.isLockIp = lockData.procedureIDs.substr(userEndIndex + 1, ipEndIndex - userEndIndex - 1);
                                        }
                                        else {
                                            item.isLockIp = lockData.procedureIDs.substr(userEndIndex + 1, lockData.procedureIDs.length - userEndIndex - 1);
                                        }
                                        ipEndIndex = item.isLockIp.indexOf('&');
                                        if (ipEndIndex > -1) {
                                            item.isLockIp = item.isLockIp.substr(0, ipEndIndex);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (item.isLockUser != '' && (item.isLockUser != loginContext.userId || item.isLockIp != $rootScope.clientIP)) {
                        configurationService.getUserName(item.isLockUser).success(function (data) {
                            var tipInfo = $translate.instant("LockedBy");
                            tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', item.isLockIp);

                            openDialog.openIconDialogOkCancelParam2(openDialog.NotifyMessageType.Warn, tipInfo, '', onlyReadReport, item);
                        });
                    }
                    else {
                        $state.go('ris.report', {
                            isPreview: false,
                            procedureId: item.procedureID
                        });
                    }
                });
            });
        };

        var onlyReadReport = function (item) {
            var data = {
                isPreview: false,
                isRead: true,
                procedureId: item.procedureID
            };
            if (item.status == enums.rpStatus.examination) data.reportId = 0;
            $state.go('ris.report', data);
        };

        var printReport = function (procedureId) {
            reportService.getReportPrintTemplateIDByProID(procedureId, loginContext.site, loginContext.domain).success(function (data) {
                if (!data) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('NoPrintTemplate').replace('{0}', $translate.instant('PrintReport')));
                    return;
                }
                var param = {
                    id: procedureId,
                    site: loginContext.site,
                    domain: loginContext.domain,
                    url: loginContext.apiHost + '/api/v1/report/html',
                    printtemplateid: $scope.printTemplateId,
                    printer: application.clientConfig.defaultPrinter
                };
                clientAgentService.printReport(param).success(function (data) {
                    busyRequestNotificationHub.requestEnded();
                    reportService.updateReportPrintStatusByProcedureID(procedureId, data);
                }).error(function () {
                    busyRequestNotificationHub.requestEnded();
                });
            });
        };

        var openPACSImageViewer = function () {
            if (application.clientConfig && application.clientConfig.integrationType == 2) {
                reportService.getPacsUrl($scope.procedureId).success(function (data) {
                    var pacsURL = data;
                    if (pacsURL == "") {
                        //
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("CallImageFail"));
                        return;
                    }
                    $window.open(pacsURL);
                });
            }
            else if (application.clientConfig && application.clientConfig.integrationType == 1) {
                reportService.getPacsUrlDX($scope.procedureId).success(function (data) {
                    var pacsURL = data;
                    if (pacsURL == "") {
                        //
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("CallImageFail"));
                        return;
                    }
                    $window.open(pacsURL);
                });
            }
        };

        var deleteLock = function () {
            if (!_.isUndefined($scope.procedureId) && !_.isNull($scope.procedureId) && $scope.procedureId != '') {
                reportService.getProceduresByReportID($scope.reportId).success(function (data) {
                    reportService.deleteLockByProcedureIDs(data).success(function (data) {
                    });
                });
            }
        };

        $scope.$on('event:startSearch', function (e, criteria) {
            e.preventDefault();
            //to worklist, and delete lock
            deleteLock();
        });

        // initialzation
        (function initialize() {
            $log.debug('ReportPreviewController.initialize()...');
            $scope.returnWorkList = returnWorkList;
            $scope.editReport = editReport;
            $scope.printReport = printReport;
            $scope.printTemplateId = '';
            $scope.fromSrc = '0';
            $scope.isDisabledEditReport = false;
            $scope.openPACSImageViewer = openPACSImageViewer;
            $scope.deleteLock = deleteLock;
            $scope.reportId = '';
            $scope.isPACSIntegration = application.configuration.isPACSIntegration;
            if ($scope.isPACSIntegration && application.clientConfig && application.clientConfig.integrationType == 0) {
                $scope.isPACSIntegration = false;
            }
            if ($scope.from && $scope.from != '') {
                $scope.fromSrc = $scope.from;
            }

            if ($scope.printId && $scope.printId != '') {
                $scope.printTemplateId = $scope.printId;
            }

            if ($scope.procedureId && $scope.procedureId != '') {

                var param = {
                    id: $scope.procedureId,
                    site: loginContext.site,
                    domain: loginContext.domain,
                    url: loginContext.apiHost + '/api/v1/report/getreportviewerbyprocedureid2',
                    printtemplateid: $scope.printTemplateId
                };
                clientAgentService.previewReport(param).success(function (data) {
                    busyRequestNotificationHub.requestEnded();
                    $("#iframeReport").contents().find("html").html(data);
                    $scope.reportValue = data;
                }).error(function () {
                    busyRequestNotificationHub.requestEnded();
                });

                reportService.getProcedureByID($scope.procedureId).success(function (data) {
                    $scope.reportId = data.reportID;
                    if (data.status >= enums.rpStatus.firstApprove) {
                        $scope.isDisabledEditReport = true;
                    }
                });
            }
        }());
    }
]);