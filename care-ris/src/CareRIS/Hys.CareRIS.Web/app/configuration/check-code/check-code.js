configurationModule.directive('checkCode', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('checkCode.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/check-code/check-code.html',
            replace: true,
            controller: 'CheckCodeCtrl'
        }
    }
]);