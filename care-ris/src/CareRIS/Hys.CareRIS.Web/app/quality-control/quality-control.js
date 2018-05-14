// qualitycontrol
qualitycontrolModule.directive('qualityControl', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('qualityControl.ctor()...');

        return {
            restrict: 'E',
            controller: 'QualityController',
            templateUrl: '/app/quality-control/quality-control.html',
            replace: true,
            scope:{}
        }
    }
]);