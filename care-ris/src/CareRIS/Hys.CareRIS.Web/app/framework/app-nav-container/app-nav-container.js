frameworkModule.directive('appNavContainer', ['$log',
    function ($log) {
        'use strict';

        $log.debug('appNavContainer.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/framework/app-nav-container/app-nav-container.html',
            replace: true,
            scope: {},
            controller: 'AppNavContainer'
        };
    }
]);