consultationModule.controller('RecipientConfigEditController', ['$log', '$scope', '$modalInstance', '$translate', 'consultationService', 'loginUser', 'constants', 'enums', 'config',
    function ($log, $scope, $modalInstance, $translate, consultationService, loginUser, constants, enums, config) {
        'use strict';
        $log.debug('RecipientConfigEditController.ctor()...');
        $scope.loginUser = loginUser;
        $scope.isEdit = !!config;
        $scope.config = config || { uniqueID: RIS.newGuid() };

        if ($scope.config.requestType === enums.HospitalDefaultType.Expert) {
            $scope.config.selectedUserID = $scope.config.requestID;
        }

        if ($scope.config.responseID) {
            $scope.recipient = [{ value: $scope.config.responseID, text: $scope.config.responseName, type: $scope.config.responseType }];
        }

        consultationService.getUsers({ roleID: constants.doctorRoleId, hospitalID: loginUser.user.hospitalID, pageSize: 999 }).success(function (result) {
            $scope.users = result.data;
        });

        $scope.saveRecipientConfig = function () {
            if ($scope.config.selectedUserID) {
                $scope.config.requestType = enums.HospitalDefaultType.Expert;
                $scope.config.requestID = $scope.config.selectedUserID;
            } else {
                $scope.config.requestType = enums.HospitalDefaultType.Hospital;
                $scope.config.requestID = loginUser.user.hospitalID;
            }
            $scope.config.responseID = $scope.recipient[0].value;
            $scope.config.responseType = $scope.recipient[0].type;

            consultationService.saveRecipientConfig($scope.config).success(function (data) { $modalInstance.close(data); });
        }

        $scope.close = function () {
            $modalInstance.close();
        };
    }
]);