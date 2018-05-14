consultationModule.controller('RequestInfoEditController', ['$log', '$scope', 'consultationService', 'result', '$modalInstance', 'loginUser', '$translate', 'openDialog',
function ($log, $scope, consultationService, result, $modalInstance, loginUser, $translate, openDialog) {
    'use strict';
    $log.debug('RequestInfoEditController.ctor()...');

    var closeWindow = function (result) {
        $modalInstance.close(result);
    };

    $scope.validateData = function () {
        var str = '';
        if ($scope.result.requestPurpose != null) {
            str = $scope.result.requestPurpose.replace(/<\/?[^>]+>/gi, '');
        }

        if ($scope.result.requestPurpose == null || str == '') {
            openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('RequestPurpose') + $translate.instant('IsRequiredErrorMsg'),
                function () {
                    $('#request-advice-text').focus();
                });
            return false;
        }

        str = '';
        if ($scope.result.requestRequirement != null) {
            str = $scope.result.requestRequirement.replace(/<\/?[^>]+>/gi, '');
        }

        if ($scope.result.requestRequirement == null || str == '') {
            openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('RequestRequirement') + $translate.instant('IsRequiredErrorMsg'),
                function () {
                    $('#request-remark-text').focus();
                });
            return false;
        }

        return true;
    };

    $scope.updateRequestDescription = function () {
        if ($scope.validateData()) {
            $scope.result.lastEditUser = $scope.userId;
            consultationService.updateRequestDescription($scope.result).success(function () {
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