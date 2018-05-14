frameworkModule.directive('qcSearch', ['$log',
    function ($log) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/framework/qc-search/qc-search.html',
            replace: true,
            controller: 'QcSearchController',
            scope: {}
        };
    }
]);