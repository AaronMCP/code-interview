configurationModule.directive('systemConfigure', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('systemConfigure.ctor()...');
        return {
            restrict: 'E',
            templateUrl: '/app/configuration/system-configure/system-configure.html',
            replace: true,
            controller: 'SystemConfigureCtrl'
        }
    }
]);