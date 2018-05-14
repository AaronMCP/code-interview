registrationModule.directive('registrationTransferView', [
    '$log', '$timeout',
    function ($log, $timeout) {
        'use strict';
        $log.debug('registrationTransferView.ctor()...');
        return {
            restrict: 'E',
            templateUrl:'/app/registration/registration-transfer/views/registration-transfer-view.html',
            controller: 'RegistrationTransferController',
            scope: {
                requestInfo: '='
            },
            link: function (scope, element, attrs) {
                scope.hideModalityPopOver = function (selectorid) {
                    $('.transfer-modality-popover').popover('hide');
                }
            }
        };
    }]);