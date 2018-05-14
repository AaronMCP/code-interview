consultationModule.controller('NotificationConfigController', ['$log', '$scope', '$translate', 'consultationService', 'enums', 'loginUser', 'openDialog', 'csdToaster',
    function ($log, $scope, $translate, consultationService, enums, loginUser, openDialog, csdToaster) {
        'use strict';
        $log.debug('NotificationConfigController.ctor()...');
        $scope._ = _;
        $scope.enums = enums;
        $scope.loginUser = loginUser;
        $scope.notifyType = enums.NotifyType;
        var separator = ",";

        consultationService.getNotificationConfigs().success(function (data) {
            $scope.configs = data;
            _.map($scope.configs, function (config) {
                if (config.notifyTypes) {
                    config.notifyTypeList = config.notifyTypes.split(separator);
                }
            });
        });

        $scope.updateConfig = function () {
            _.map($scope.configs, function (config) {
                if (config.notifyTypeList) {
                    config.notifyTypes = config.notifyTypeList.join(separator);
                }
            });
            consultationService.saveNotificationConfigs($scope.configs).success(function () {
                csdToaster.pop('success', $translate.instant("SaveSuccess"), '');
                //openDialog.openIconDialog(openDialog.NotifyMessageType.Info, $translate.instant("Tips"), $translate.instant("SaveSuccess"));
            });
        }
    }
]);