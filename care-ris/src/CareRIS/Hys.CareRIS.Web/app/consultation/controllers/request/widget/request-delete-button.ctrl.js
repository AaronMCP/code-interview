consultationModule.controller('RequestDeleteController', ['$log', '$scope', 'requestService', 'consultationService', '$translate', 'openDialog',
    function ($log, $scope, requestService, consultationService, $translate, openDialog) {
        'use strict';

        $scope.openDeleteRequestWindow = function () {
            requestService.openDeleteRequestWindow($scope.report.requestId);
        };

        $scope.recoverRequest = function () {
            consultationService.recoverRequest($scope.report.requestId).success(function () {
                consultationService.recoverRequest($scope.report.requestId).success(function () {
                    openDialog.openIconDialogOkFun(
                        openDialog.NotifyMessageType.Success,
                        $translate.instant('RecoverSuccess'),
                        '', requestService.backToWorkList('ris.consultation.requests', false));
                });
            });
        };
    }
]);