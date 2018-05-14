consultationModule.controller('ReportTemplateController', ['$log', '$scope', '$translate', 'openDialog','busyRequestNotificationHub',
    function ($log, $scope, $translate, openDialog, busyRequestNotificationHub) {
        'use strict';
        $log.debug('ReportTemplateController.ctor()...');

        $scope.onUpload = function () {
            busyRequestNotificationHub.requestStarted();
        }

        $scope.onComplete = function () {
            busyRequestNotificationHub.requestEnded();
        }

        $scope.onSuccess = function (response) {
            openDialog.openIconDialog(openDialog.NotifyMessageType.Info, $translate.instant("Tips"), response.data);
        }

        $scope.onError = function (response) {
            openDialog.openIconDialog(openDialog.NotifyMessageType.Info, $translate.instant("Tips"), $translate.instant("ImportFailed"));
        }
    }
]);