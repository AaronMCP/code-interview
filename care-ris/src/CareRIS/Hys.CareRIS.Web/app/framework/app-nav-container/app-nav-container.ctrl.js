frameworkModule.controller('AppNavContainer', ['$scope', '$log', '$state',
    function ($scope, $log, $state) {
        'use strict';
        $log.debug('AppNavContainer.ctor()...');

        (function initialize() {
            $log.debug('AppNavContainer.initialize()...');

            $scope.$state = $state;
        }());
    }]);