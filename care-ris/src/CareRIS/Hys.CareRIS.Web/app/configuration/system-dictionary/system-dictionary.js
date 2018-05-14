configurationModule.directive('systemDictionary', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('systemDictionary.ctor()...');
        return {
            restrict: 'E',
            templateUrl: '/app/configuration/system-dictionary/system-dictionary.html',
            replace: true,
            controller: 'SystemDictionaryeCtrl'
        }
    }
]);