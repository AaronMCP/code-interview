// appWorklistSearch
// search control for worklist
frameworkModule.directive('appWorklistSearch', ['$log',
    function ($log) {
        'use strict';

        $log.debug('appWorklistSearch.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/framework/app-worklist-search/app-worklist-search.html',
            replace: true,
            controller: 'AppWorklistSearch'
        };
    }
]);