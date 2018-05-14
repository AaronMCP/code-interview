consultationModule.controller('RequestDiagnosisEditController', ['$log', '$scope', 'consultationService', '$modalInstance', 'result', 'loginUser', '$translate', 'openDialog',
    function ($log, $scope, consultationService, $modalInstance, result, loginUser, $translate, openDialog) {
        'use strict';
        $log.debug('RequestDiagnosisEditController.ctor()...');

        var closeWindow = function (result) {
            $modalInstance.close(result);
        };

        $scope.validateData = function () {
            var str = '';
            if ($scope.result.clinicalDiagnosis != null) {
                str = $scope.result.clinicalDiagnosis.replace(/<\/?[^>]+>/gi, '');
            }

            if ($scope.result.clinicalDiagnosis == null || str == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('ClinicalDiagnosis') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $('#request-advice-text').focus();
                    });
                return false;
            }

            return true;
        };


        $scope.updateRequestClinicaldiagnosis = function () {
            if ($scope.validateData()) {
                $scope.result.lastEditUser = $scope.userId;
                $scope.result.type = 0;
                consultationService.updateRequestClinicaldiagnosis($scope.result).success(function () {
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