consultationModule.controller('RequestDeleteReasonController', ['$log', '$scope', 'consultationService', 'deleteData', '$modalInstance', 'enums', 'openDialog', '$translate', '$state', '$stateParams', 'requestService','csdToaster',
function ($log, $scope, consultationService, deleteData, $modalInstance, enums, openDialog, $translate, $state, $stateParams, requestService, csdToaster) {
    'use strict';
    $log.debug('RequestDeleteReasonController.ctor()...');

    $scope.deleteRequest = function () {
        if (!$scope.reason.deleteReason) {
            csdToaster.pop('info', $translate.instant("DeleteReasonTips"), '');
        } else {
            if ($scope.reason.deleteType === $scope.enums.DeleteType.ConsultationRequest) {
                consultationService.deleteRequest($scope.reason).success(function () {
                    $modalInstance.dismiss();
                    openDialog.openIconDialogOkFun(
                        openDialog.NotifyMessageType.Success,
                        $translate.instant('DeleteSuccess'),
                        '', requestService.backToWorkList('ris.consultation.requests', true));
                });
            } else {
                consultationService.deletePatientCase($scope.reason).success(function () {
                    $modalInstance.dismiss();
                    openDialog.openIconDialogOkFun(
                          openDialog.NotifyMessageType.Success,
                          $translate.instant('DeleteSuccess'),
                          '', requestService.backToWorkList('ris.consultation.cases', true));
                });
            }
        }
    };

    $scope.close = function () {
        $modalInstance.close(false);
    };

    (function () {
        $scope.reason = {
            id: deleteData.id,
            deleteReason: '',
            deleteType: deleteData.deleteType,
            deleteTitle: deleteData.deleteType === enums.DeleteType.ConsultationRequest ? 'DeleteRequest' : 'DeletePatientCase'
        };

        $scope.enums = enums;
    }());
}
]);


