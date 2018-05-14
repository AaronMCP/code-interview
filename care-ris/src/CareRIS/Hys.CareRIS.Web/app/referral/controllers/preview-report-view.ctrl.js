consultationModule.controller('ReferralPreviewReportController', ['$log', '$scope', '$modalInstance', '$translate', 'openDialog', 'procedureId', 'loginContext',
    '$http', 'busyRequestNotificationHub', '$window', 'application', 'clientAgentService', 'reportService',
    function ($log, $scope, $modalInstance, $translate, openDialog, procedureId, loginContext, $http,
        busyRequestNotificationHub, $window, application, clientAgentService, reportService) {
        'use strict';
        $log.debug('ReferralPreviewReportController.ctor()...');

        $scope.printReport = function () {
            reportService.getReportPrintTemplateIDByProID($scope.procedureId, loginContext.site, loginContext.domain).success(function (data) {
                if (!data) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('NoPrintTemplate').replace('{0}', $translate.instant('PrintReport')));
                    return;
                }
                
                var param = {
                    id: $scope.procedureId,
                    site: loginContext.site,
                    domain: loginContext.domain,
                    url: loginContext.apiHost + '/api/v1/report/html',
                    printtemplateid: '',
                    printer: application.clientConfig.defaultPrinter
                };
                clientAgentService.printReport(param).success(function (data) {
                    busyRequestNotificationHub.requestEnded();
                    reportService.updateReportPrintStatusByProcedureID($scope.procedureId, data).success(function (result) {
                    });
                }).error(function () {
                    busyRequestNotificationHub.requestEnded();
                });
            });
        };

        $scope.close = function () {
            $modalInstance.dismiss();
        };

        (function initialize() {
            $scope.procedureId = procedureId;

            var param = {
                id: $scope.procedureId,
                site: loginContext.site,
                domain: loginContext.domain,
                url: loginContext.apiHost + '/api/v1/report/getreportviewerbyprocedureid2',
                printtemplateid: ''
            };
            clientAgentService.previewReport(param).success(function (data) {
                busyRequestNotificationHub.requestEnded();
                $("#iframeReport").contents().find("html").html(data);
            }).error(function () {
                busyRequestNotificationHub.requestEnded();
            });

        }());
    }
]);


