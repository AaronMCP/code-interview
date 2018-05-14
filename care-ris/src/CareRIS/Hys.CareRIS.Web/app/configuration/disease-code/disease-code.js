configurationModule.directive('diseaseCode', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('diseaseCode.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/disease-code/disease-code.html',
            replace: true,
            controller: 'DisCodeCtrl'
        }
    }
]);