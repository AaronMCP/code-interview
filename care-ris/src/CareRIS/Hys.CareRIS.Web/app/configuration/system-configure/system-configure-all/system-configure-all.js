configurationModule.directive('systemConfigureAll', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('systemConfigureAll.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/system-configure/system-configure-all/system-configure-all.html',
            replace: true,
            controller: 'SystemConfigureAllController',
            scope: {
                systemProfiles: '='
            }
        }
    }
]);