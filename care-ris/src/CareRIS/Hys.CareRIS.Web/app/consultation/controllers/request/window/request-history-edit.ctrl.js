consultationModule.controller('RequestHistoryEditController', ['$log', '$scope', 'consultationService', '$modalInstance', 'result', 'loginUser', '$translate', 'openDialog',
function ($log, $scope, consultationService, $modalInstance, result, loginUser, $translate, openDialog) {
    'use strict';
    $log.debug('RequestHistoryEditController.ctor()...');

    var closeWindow = function (result) {
        $modalInstance.close(result);
    };

    $scope.validateData = function () {
        var str = '';
        if ($scope.result.history != null) {
            str = $scope.result.history.replace(/<\/?[^>]+>/gi, '');
        }

        if ($scope.result.history == null || str == '') {
            openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('History') + $translate.instant('IsRequiredErrorMsg'),
                function () {
                    $('#request-advice-text').focus();
                });
            return false;
        }

        return true;
    };

    $scope.updateRequestPatienthistory = function () {
        if ($scope.validateData()) {
            $scope.result.lastEditUser = $scope.userId;
            $scope.result.type = 1;
            consultationService.updateRequestPatienthistory($scope.result).success(function () {
                closeWindow($scope.result);
            });
        }
    };

    $scope.close = function () {
        closeWindow();
    };

    (function initialize() {
        $scope.result = result;
        $scope.userId = loginUser.user.uniqueID;
    }());
}
]);