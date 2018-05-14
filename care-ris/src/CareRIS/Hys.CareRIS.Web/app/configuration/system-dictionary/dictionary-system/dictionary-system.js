configurationModule.directive('dictionarySystem', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        return {
            restrict: 'E',
            templateUrl: '/app/configuration/system-dictionary/dictionary-system/dictionary-system.html',
            replace: true,
            controller: 'DictionaryeSystemCtrl'
        }
    }
]);