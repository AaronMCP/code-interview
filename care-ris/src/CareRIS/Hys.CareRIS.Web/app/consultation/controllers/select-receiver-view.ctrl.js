consultationModule.controller('SelectReceiverController', ['$log', '$scope', '$modalInstance', '$translate', 'openDialog','csdToaster','selection',
    function ($log, $scope, $modalInstance, $translate, openDialog, csdToaster,selection) {
        'use strict';
        $log.debug('SelectReceiverController.ctor()...');

        $scope.selection = selection;
        $scope.selectReceiver = function () {
            if ($scope.selection) {
                $modalInstance.close($scope.selection);
            }
            else {
                csdToaster.pop('info', $translate.instant("SelectConsultHospitalExpert"), '');
            }
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };
        $scope.removeSelection = function (value) {
            var index = _.findIndex($scope.selection, { value: value });
            if (index > -1) {
                var items = [{value:value,selected:false}];
                $scope.$broadcast('event:updateExpertItemStatus', items);
                $scope.selection.splice(index, 1);
            }
        };
    }
]);


