configurationModule.directive('checkCodeBottom', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('checkCodeBottom.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/check-code/check-code-bottom/check-code-bottom.html',
            replace: true
        }
    }
]);