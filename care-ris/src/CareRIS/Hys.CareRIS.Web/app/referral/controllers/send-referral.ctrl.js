referralModule.controller('SendReferralController', ['$log', '$scope', '$modalInstance', 'orderID','referralService',
    function ($log, $scope, $modalInstance, orderID, referralService) {
        'use strict';
        $log.debug('SendReferralController.ctor()...');

        $scope.sendReferral = function (form) {
            if (!form.$valid) {
                $scope.isShowErrorMsg = true;
                return;
            }
            //judge, sent, not reject or cancel
            var newData = { orderID: orderID, targetSite: $scope.targetSite, memo: $scope.memo };
            referralService.sendReferral(newData).success(function (data) {
                $modalInstance.close(data);
            });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };

        (function initialize() {
            $log.debug('SendReferralController.initialize()...');
            $scope.isShowErrorMsg = false;
            $scope.orderID = orderID;
            $scope.targetSite = '';
            $scope.memo = '';
            referralService.getTargetSites().success(function (data) {
                $scope.targetSites = data;
                if ($scope.targetSites.length > 0)
                {
                    $scope.targetSite = $scope.targetSites[0].siteName;
                }
            });
        }());
    }
]);


